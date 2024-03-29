using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Rewards
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Reports : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                GenerateHTML();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "rewards";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "rewards")
                    try { CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"]; } catch { }
            }
        }

		private void LoadOptions()
		{
            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.RewardsHistory, ReportTypes.RewardsHistory));
            cboReportType.Items.Add(new ListItem(ReportTypes.RewardsSummary, ReportTypes.RewardsSummary));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.RewardsSummaryStatistics, ReportTypes.RewardsSummaryStatistics));

            if (Request.QueryString["task"] == null)
            { 
                cboReportType.SelectedIndex = 0; 
            }
            else
            {
                string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                switch (task)
                {
                    case ReportTypes.RewardsHistory:
                        cboReportType.SelectedIndex = 1;
                        break;
                    case ReportTypes.RewardsSummary:
                        cboReportType.SelectedIndex = 2;
                        break;
                    case ReportTypes.RewardsSummaryStatistics:
                        cboReportType.SelectedIndex = 4;
                        break;
                    default:
                        cboReportType.SelectedIndex = 0;
                        break;
                }
            }

            Customer clsCustomer = new Customer();
			cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContactName.Items.Count > 1 && txtContactName.Text.Trim() != string.Empty) cboContactName.SelectedIndex = 1; else cboContactName.SelectedIndex = 0;
            clsCustomer.CommitAndDispose();

            txtStartTransactionDate.Text = Common.ToShortDateString(DateTime.Now.AddDays(-1));
            txtEndTransactionDate.Text = Common.ToShortDateString(DateTime.Now);

            cboReportType_SelectedIndexChanged(null, null);
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            string strReportType = cboReportType.SelectedItem.Value;

            switch (strReportType)
            {
                case ReportTypes.RewardsHistory:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_rewards/_rewardsmovement.rpt"));
                    break;
                case ReportTypes.RewardsSummary:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_rewards/_rewardssummary.rpt"));
                    break;
                case ReportTypes.RewardsSummaryStatistics:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_rewards/_rewardssummarystatistics.rpt"));
                    break;
                default:
                    return null;
            }
            
            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_rewards";
                CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
            }
            else
            {
                CRViewer.ReportSource = rpt;
                Session["ReportDocument"] = rpt;
            }
        }

        #endregion

        #region GeneratePDF
        private void GeneratePDF() { Export(ExportFormatType.PortableDocFormat); }
        #endregion

        #region GenerateWord
        private void GenerateWord() { Export(ExportFormatType.WordForWindows); }
        #endregion

        #region GenerateExcel
        private void GenerateExcel() { Export(ExportFormatType.Excel); }
        #endregion

        #region GenerateHTML
        private void GenerateHTML() { Export(ExportFormatType.HTML40); }
        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();
            System.Data.DataTable dt;

            DateTime StartTransactionDate = DateTime.MinValue;
            try
            { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text); }
            catch { }
            DateTime EndTransactionDate = DateTime.MinValue;
            try
            { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text); }
            catch { }

            ContactReward clsContactReward;
            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.RewardsHistory:
                    #region RewardsHistory
                    clsContactReward = new ContactReward();
                    dt = clsContactReward.RewardsMovement(StartTransactionDate, EndTransactionDate, long.Parse(cboContactName.SelectedItem.Value));
                    clsContactReward.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.RewardsMovement.NewRow();

                        foreach (DataColumn dc in rptds.RewardsMovement.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.RewardsMovement.Rows.Add(drNew);
                    }

                    break;
                    #endregion

                case ReportTypes.RewardsSummary:
                    #region RewardsSummary
                    clsContactReward = new ContactReward();
                    dt = clsContactReward.RewardsSummary(StartTransactionDate, EndTransactionDate, long.Parse(cboContactName.SelectedItem.Value));
                    clsContactReward.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Rewards.NewRow();

                        foreach (DataColumn dc in rptds.Rewards.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Rewards.Rows.Add(drNew);
                    }

                    break;
                    #endregion

                case ReportTypes.RewardsSummaryStatistics:
                    #region RewardsSummaryStatistics
                    clsContactReward = new ContactReward();
                    dt = clsContactReward.SummarizedStatistics();
                    clsContactReward.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.RewardsSummary.NewRow();

                        foreach (DataColumn dc in rptds.RewardsSummary.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.RewardsSummary.Rows.Add(drNew);
                    }

                    break;
                    #endregion
                
            }
			Report.SetDataSource(rptds); 
			SetParameters(Report);
		}

		#endregion

		#region SetParameters

		private void SetParameters (ReportDocument Report)
		{
			ParameterFieldDefinition paramField;
			ParameterValues currentValues;
			ParameterDiscreteValue discreteParam;

			paramField = Report.DataDefinition.ParameterFields["CompanyName"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = CompanyDetails.CompanyName;
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["PrintedBy"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = Session["Name"].ToString();
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

            switch (cboReportType.SelectedItem.Value)
            {
                case ReportTypes.RewardsHistory:
                case ReportTypes.RewardsSummary:
                    paramField = Report.DataDefinition.ParameterFields["CustomerName"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = cboContactName.SelectedItem.Text;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["RewardCardNo"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = cboContactName.SelectedItem.Text;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    DateTime StartTransactionDate = DateTime.MinValue;
                    try
                    { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text); }
                    catch { }
                    paramField = Report.DataDefinition.ParameterFields["StartTransactionDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = StartTransactionDate;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    DateTime EndTransactionDate = DateTime.MinValue;
                    try
                    { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text); }
                    catch { }
                    paramField = Report.DataDefinition.ParameterFields["EndTransactionDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = EndTransactionDate;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;
                default:
                    break;
            }
		}

		#endregion

		#region Web Form Designer generated code
		
        override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}

		#endregion

        #region Web Control Methods

        protected void cmdView_Click(object sender, System.EventArgs e)
		{
			switch (Convert.ToInt16(cboReportOptions.SelectedItem.Value))
			{
				case 0:
					 GenerateHTML();
					break;
				case 1:
					GeneratePDF();
					break;
				case 2:
					GenerateWord();
					break;
				case 3:
					GenerateExcel();
					break;
			}
		}

        protected void imgContactNameSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Customer clsCustomer = new Customer();
            cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContactName.Items.Count > 1 && txtContactName.Text.Trim() != string.Empty) cboContactName.SelectedIndex = 1; else cboContactName.SelectedIndex = 0;
            clsCustomer.CommitAndDispose();
        }

        protected void cboContactName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdView_Click(null, null);
        }

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panHolder.Visible = false;
            switch (cboReportType.SelectedItem.Text)
            {
                case ReportTypes.RewardsHistory:
                case ReportTypes.RewardsSummary:
                    panHolder.Visible = true;
                    break;
                case ReportTypes.RewardsSummaryStatistics:
                    panHolder.Visible = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
