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

	public partial  class __TerminalReport : System.Web.UI.UserControl
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
			txtTerminalNo.Text = "01";
		}

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_TerminalReport.rpt"));

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
            string strFileName = "termreport_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_TerminalReport.rpt"));
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
        }

        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			MySqlDataReader myreader = clsTerminalReport.List(txtTerminalNo.Text);
			clsTerminalReport.CommitAndDispose();

			while (myreader.Read())
			{
				DataRow drNew = rptds.TerminalReport.NewRow();
				
				foreach (DataColumn dc in rptds.TerminalReport.Columns)
					drNew[dc] = myreader[dc.ColumnName]; 
				
				rptds.TerminalReport.Rows.Add(drNew);
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

			ReceiptFormat clsReceiptFormat = new ReceiptFormat();
			ReceiptFormatDetails clsDetails = clsReceiptFormat.Details();
			clsReceiptFormat.CommitAndDispose();

			paramField = Report.DataDefinition.ParameterFields["ReportHeader1"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportHeader1);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader2"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportHeader2);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader3"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportHeader3);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader4"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportHeader4);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			/**** Set the report footer ****/
			paramField = Report.DataDefinition.ParameterFields["ReportFooter1"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportFooter1);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportFooter2"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportFooter2);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportFooter3"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsDetails.ReportFooter3);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

		}

		private string GetReceiptFormatParameter(string stReceiptFormat)
		{
			string stRetValue = "";

//			try
//			{
				if (stReceiptFormat == "")
				{
					stRetValue = "";
				}
				else if (stReceiptFormat == Environment.NewLine.ToString())
				{
					stRetValue = "";
				}
				else if (stReceiptFormat == "{DateNow}")
				{
					stRetValue = DateTime.Now.ToString("MMM. dd, yyyy HH:mm:ss");
				}
				else if (stReceiptFormat == "{Cashier}")
				{
					stRetValue = "Cashier: N/A";
				}
				else if (stReceiptFormat == "{TerminalNo}")
				{
					stRetValue = "Terminal No.: " + txtTerminalNo.Text;
				}
				else if (stReceiptFormat == "{MachineSerialNo}")
				{
					stRetValue = "MIN: " + System.Configuration.ConfigurationManager.AppSettings["MachineSerialNo"].ToString();
				}
				else if (stReceiptFormat == "{AccreditationNo}")
				{
                    stRetValue = "Acc. No.: " + System.Configuration.ConfigurationManager.AppSettings["AccreditationNo"].ToString();
				}
				else if (stReceiptFormat == "{InvoiceNo}")
				{
					stRetValue = "Invoice #: N/A";
				}
				else
				{
					stRetValue = stReceiptFormat;
				}
//			}
//			catch 
//			{
//				stRetValue = stReceiptFormat;
//			}

			return stRetValue;
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
