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
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportDocument"] = "terminal";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "terminal")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
			txtTerminalNo.Text = "01";
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_TerminalReport.rpt"));

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_terminalrep";
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

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			System.Data.DataTable dt = clsTerminalReport.List(txtTerminalNo.Text);
			clsTerminalReport.CommitAndDispose();

			foreach(System.Data.DataRow dr in dt.Rows)
			{
				DataRow drNew = rptds.TerminalReport.NewRow();
				
				foreach (DataColumn dc in rptds.TerminalReport.Columns)
					drNew[dc] = dr[dc.ColumnName]; 
				
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

            Receipt clsReceipt = new Receipt();

			paramField = Report.DataDefinition.ParameterFields["ReportHeader1"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader1").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader2"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader2").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader3"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader3").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportHeader4"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader4").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			/**** Set the report footer ****/
			paramField = Report.DataDefinition.ParameterFields["ReportFooter1"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter1").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportFooter2"];
			discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter2").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["ReportFooter3"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter3").Value);
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

            clsReceipt.CommitAndDispose();

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
