using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

    public partial class __AgentsSalesReport : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "agentsales";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "agentsales")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
            
			Contacts clsContact = new Contacts();
			cboContactCode.DataTextField = "ContactName";
			cboContactCode.DataValueField = "ContactID";
			cboContactCode.DataSource = clsContact.AgentsAsDataTable(null, 0, "ContactCode",SortOption.Ascending);
			cboContactCode.DataBind();
            cboContactCode.Items.Add(new ListItem("Summarized", "0"));
            cboContactCode.SelectedIndex = cboContactCode.Items.Count - 1;
            
            cboReportType.Items.Add(new ListItem("Summarized Report", "0"));
            cboReportType.Items.Add(new ListItem("Summarized With Details", "1"));
            cboReportType.SelectedIndex = cboReportType.Items.Count - 1;

            Positions clsPosition = new Positions(clsContact.Connection, clsContact.Transaction);
            cboPosition.DataTextField = "PositionName";
            cboPosition.DataValueField = "PositionID";
            cboPosition.DataSource = clsPosition.ListAsDataTable(null, SortOption.Ascending, 0);
            cboPosition.DataBind();
            cboPosition.Items.Add(new ListItem(Positions.DEFAULT_ALL_POSITIONS, "0"));
            cboPosition.SelectedIndex = cboPosition.Items.Count - 1;

            Department clsDepartment = new Department(clsContact.Connection, clsContact.Transaction);
            cboDepartment.DataTextField = "DepartmentName";
            cboDepartment.DataValueField = "DepartmentID";
            cboDepartment.DataSource = clsDepartment.ListAsDataTable(null, SortOption.Ascending, 0);
            cboDepartment.DataBind();
            cboDepartment.Items.Add(new ListItem(Department.DEFAULT_ALL_DEPARTMENTS, "0"));
            cboDepartment.SelectedIndex = cboDepartment.Items.Count - 1;

            clsContact.CommitAndDispose();

            txtStartTransactionDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtEndTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            switch (cboContactCode.SelectedItem.Value)
            {
                case ("0"):
                    if (cboReportType.SelectedItem.Value == "0")
                        rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/AgentsSalesReportSummary.rpt"));
                    else
                        rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/AgentsSalesReportDetailed.rpt"));
                    break;
                default:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/AgentsSalesReport.rpt")); break;
            }    

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_agentscommision";
                CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
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
            DateTime StartTransactionDate = DateTime.MinValue;
            try
            { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text + " " + txtStartTime.Text); }
            catch { }
            DateTime EndTransactionDate = DateTime.MinValue;
            try
            { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text + " " + txtEndTime.Text); }
            catch { }

            System.Data.DataSet ds = new System.Data.DataSet();
            SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems();
            if (cboContactCode.SelectedItem.Value != "0")
                ds.Tables.Add(clsSalesTransactionItems.AgentsCommision(long.Parse(cboContactCode.SelectedValue), StartTransactionDate, EndTransactionDate));
            else
                ds.Tables.Add(clsSalesTransactionItems.AgentsCommision(cboDepartment.SelectedItem.Text, cboPosition.SelectedItem.Text, StartTransactionDate, EndTransactionDate));

            clsSalesTransactionItems.CommitAndDispose();

            Report.SetDataSource(ds); 
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

            DateTime StartTransactionDate = DateTime.MinValue;
            try
            { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text + " " + txtStartTime.Text); }
            catch { }
            DateTime EndTransactionDate = DateTime.MinValue;
            try
            { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text + " " + txtEndTime.Text); }
            catch { }

            paramField = Report.DataDefinition.ParameterFields["StartTransactionDate"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = StartTransactionDate.ToString("yyyy-MM-dd HH:mm");
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["EndTransactionDate"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = EndTransactionDate.ToString("yyyy-MM-dd HH:mm");
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            if (cboContactCode.SelectedItem.Value != "0")
            {
                Contacts clsContact = new Contacts();
                ContactDetails clsContactDetails = clsContact.Details(long.Parse(cboContactCode.SelectedValue));
                clsContact.CommitAndDispose();

                paramField = Report.DataDefinition.ParameterFields["AgentsName"];
                discreteParam = new ParameterDiscreteValue();
                discreteParam.Value = clsContactDetails.ContactName;
                currentValues = new ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = Report.DataDefinition.ParameterFields["Address"];
                discreteParam = new ParameterDiscreteValue();
                discreteParam.Value = clsContactDetails.Address;
                currentValues = new ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = Report.DataDefinition.ParameterFields["ContactNo"];
                discreteParam = new ParameterDiscreteValue();
                discreteParam.Value = clsContactDetails.TelephoneNo;
                currentValues = new ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);
            }
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
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
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        #endregion

	}
}
