using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __SalesAndReceivables : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadOptions();
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
			txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
			txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

			cboReportType.Items.Clear();
			
			cboReportType.Items.Add(new ListItem("Select Report Type", "0"));
			cboReportType.Items.Add(new ListItem("Posted SO", "Posted SO"));
			cboReportType.Items.Add(new ListItem("Posted SO Returns", "Posted SO Returns"));
			cboReportType.Items.Add(new ListItem("Posted Credit Memo", "Posted Credit Memo"));
			cboReportType.Items.Add(new ListItem("By Vendor", "By Vendor"));
			
			cboReportType.SelectedIndex = 0;
			
			if (Request.QueryString["reporttype"] != null)
			{
				string stReportType = Common.Decrypt(Request.QueryString["reporttype"],Session.SessionID);
				switch (stReportType)
				{
					case "Posted SO":
						cboReportType.SelectedIndex = 1;
						break;
					case "Posted SO Returns":
						cboReportType.SelectedIndex = 2;
						break;
					case "Posted Credit Memo":
						cboReportType.SelectedIndex = 3;
						break;
					case "By Vendor":
						cboReportType.SelectedIndex = 4;
						break;
				}

				GenerateHTML();
			}
		}

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = new ReportDocument();
            switch (cboReportType.SelectedItem.Text)
            {
                case "Posted SO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedSO.rpt"));
                    break;
                case "Posted SO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedSOReturns.rpt"));
                    break;
                case "Posted Credit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedCreditMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesSalesAnalysis.rpt"));
                    break;
                default:
                    return;

            }

            SetDataSource(rpt);

            ExportOptions exportop = new ExportOptions();
            DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
            string strPath = Server.MapPath(@"\retailplus\temp\");
            string strFileExtensionName = ".pdf";
            switch (pvtExportFormatType)
            {
                case ExportFormatType.PortableDocFormat: strFileExtensionName = ".pdf"; exportop.ExportFormatType = ExportFormatType.PortableDocFormat; break;
                case ExportFormatType.WordForWindows: strFileExtensionName = ".doc"; exportop.ExportFormatType = ExportFormatType.WordForWindows; break;
                case ExportFormatType.Excel: strFileExtensionName = ".xls"; exportop.ExportFormatType = ExportFormatType.Excel; break;
            }
            string strFileName = "sales_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
            if (System.IO.File.Exists(strPath + strFileName))
                System.IO.File.Delete(strPath + strFileName);

            dest.DiskFileName = strPath + strFileName;
            exportop.DestinationOptions = dest;
            exportop.ExportDestinationType = ExportDestinationType.DiskFile;
            rpt.Export(exportop); //rpt.Close(); rpt.Dispose();

            if (pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                rpt.Close(); rpt.Dispose();
                Response.Redirect(Constants.ROOT_DIRECTORY + "/temp/" + strFileName, false);
            }
            else
            {
                CRViewer.ReportSource = rpt;
                Session["ReportDocument"] = rpt;
                OpenExportedReport(strFileName);
            }

        }

        private void OpenExportedReport(string FileName)
        {
            try
            {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.DownloadFile(Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName), @"c:\" + FileName);

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"c:\" + FileName; //Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName);
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                p.Start();
            }
            catch (Exception ex) { throw ex; }
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

        private void GenerateHTML()
        {
            ReportDocument rpt = new ReportDocument();
            switch (cboReportType.SelectedItem.Text)
            {
                case "Posted SO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedSO.rpt"));
                    break;
                case "Posted SO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedSOReturns.rpt"));
                    break;
                case "Posted Credit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesPostedCreditMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_SalesAndReceivablesSalesAnalysis.rpt"));
                    break;
                default:
                    return;

            }
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
        }

        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
            ReportDataset rptds = new ReportDataset();

			DateTime PostingDateFrom = DateTime.MinValue;
			try
			{	PostingDateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text);	}
			catch{}
			DateTime PostingDateTo = DateTime.MinValue;
			try
			{	PostingDateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text);	}
			catch{}

			DataClass clsDataClass = new DataClass();
			System.Data.DataTable dt;

			string ReportType = cboReportType.SelectedItem.Text;

			switch (ReportType)
			{
				case "Posted SO":
					Data.SO clsSO = new Data.SO();
					dt =  clsDataClass.DataReaderToDataTable(clsSO.List(SOStatus.Posted, PostingDateFrom, PostingDateTo));
					clsSO.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drSO = rptds.SO.NewRow();

						foreach (DataColumn dc in rptds.SO.Columns)
							drSO[dc] = dr[dc.ColumnName];

						rptds.SO.Rows.Add(drSO);
					}
					break;
				case "Posted SO Returns":
					Data.SOReturns clsSOReturns = new Data.SOReturns();
					dt =  clsDataClass.DataReaderToDataTable(clsSOReturns.List(SOReturnStatus.Posted, PostingDateFrom, PostingDateTo));
					clsSOReturns.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drSOReturns = rptds.SOReturns.NewRow();

						foreach (DataColumn dc in rptds.SOReturns.Columns)
							drSOReturns[dc] = dr[dc.ColumnName];

						rptds.SOReturns.Rows.Add(drSOReturns);
					}
					break;
				case "Posted Credit Memo":
					Data.CreditMemos clsCreditMemos = new Data.CreditMemos();
					dt =  clsDataClass.DataReaderToDataTable(clsCreditMemos.List(CreditMemoStatus.Posted, PostingDateFrom, PostingDateTo));
					clsCreditMemos.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drCreditMemos = rptds.CreditMemo.NewRow();

						foreach (DataColumn dc in rptds.CreditMemo.Columns)
							drCreditMemos[dc] = dr[dc.ColumnName];

						rptds.CreditMemo.Rows.Add(drCreditMemos);
					}
					break;
				case "By Vendor":
					Data.SalesAnalysis clsSalesAnalysis = new Data.SalesAnalysis();
					dt = clsSalesAnalysis.ByVendor(PostingDateFrom, PostingDateTo);
					clsSalesAnalysis.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drPAByVendor = rptds.SalesAnalysisPerCustomer.NewRow();

                        foreach (DataColumn dc in rptds.SalesAnalysisPerCustomer.Columns)
							drPAByVendor[dc] = dr[dc.ColumnName];

                        rptds.SalesAnalysisPerCustomer.Rows.Add(drPAByVendor);
					}
					break;

				default:
					break;
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

            DateTime PostingDateFrom = DateTime.MinValue;
            try
            { PostingDateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["PostingDateFrom"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = PostingDateFrom;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);


            DateTime PostingDateTo = DateTime.MinValue;
            try
            { PostingDateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["PostingDateTo"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = PostingDateTo;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);


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
