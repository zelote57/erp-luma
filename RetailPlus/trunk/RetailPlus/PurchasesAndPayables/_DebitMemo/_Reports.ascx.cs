using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.PurchasesAndPayables._DebitMemo
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
                if (!string.IsNullOrEmpty(Request.QueryString["reporttype"]))
                    lblReportType.Text = Common.Decrypt(Request.QueryString["reporttype"].ToString(), Session.SessionID);
                else
                    lblReportType.Text = "DebitMemoReport";

                LoadOptions();
                if (!string.IsNullOrEmpty(Request.QueryString["target"]))
                    GeneratePDF();
                else
                    GenerateHTML();

                Session["ReportDocument"] = null;
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null)
            {
                CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
			
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/DebitMemoReport.rpt"));

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
                string strFileName = Session["UserName"].ToString() + "_salesorder";
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
            long iID = 0;
            try
            {
                if (Request.QueryString["task"].ToString().ToLower() == "reportfromposted" && Request.QueryString["memoid"].ToString() != null)
                { iID = Convert.ToInt64(Request.QueryString["memoid"].ToString()); }
                else { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["memoid"].ToString(), Session.SessionID)); }
                lblReferrer.ToolTip = iID.ToString();
            }
            catch { iID = long.Parse(lblReferrer.ToolTip); }

			ReportDataset rptds = new ReportDataset();

			DebitMemos clsDebitMemos = new DebitMemos();
			MySqlDataReader myreader = clsDebitMemos.List(iID, "DebitMemoID", SortOption.Ascending);

			while(myreader.Read())
			{
				DataRow drNew = rptds.DebitMemo.NewRow();
				
				foreach (DataColumn dc in rptds.DebitMemo.Columns)
					drNew[dc] = "" + myreader[dc.ColumnName]; 
				
				rptds.DebitMemo.Rows.Add(drNew);
			}
            myreader.Close();

            DebitMemoItems clsDebitMemoItems = new DebitMemoItems(clsDebitMemos.Connection, clsDebitMemos.Transaction);
            System.Data.DataTable dt = clsDebitMemoItems.ListAsDataTable(iID);
            clsDebitMemos.CommitAndDispose();

			foreach(System.Data.DataRow dr in dt.Rows)
			{
				DataRow drNew = rptds.DebitMemoItem.NewRow();
				
				foreach (DataColumn dc in rptds.DebitMemoItem.Columns)
					drNew[dc] = "" + dr[dc.ColumnName]; 
				
				rptds.DebitMemoItem.Rows.Add(drNew);
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

        protected void imgView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
