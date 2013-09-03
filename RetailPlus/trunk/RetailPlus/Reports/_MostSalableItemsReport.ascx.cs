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

	public partial  class __MostSalableItemsReport : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
			txtStartTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			txtEndTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            if (!chkGroupItems.Checked)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_MostSalableItemsReport.rpt"));
            else
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_MostSalableItemsReportGrouped.rpt"));

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
                string strFileName = Session["UserName"].ToString() + "_mostsaleable";
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
//			ReportDataset rptds = new ReportDataset();

			DateTime StartTransactionDate = DateTime.MinValue;
			try
			{ StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text + " " + txtStartTime.Text);	}
			catch{}
			DateTime EndTransactionDate = DateTime.MinValue;
			try
            { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text + " " + txtEndTime.Text); }
			catch{}

			Int32 Limit = 0;
			try
			{	Limit = Convert.ToInt32(txtLimit.Text);			}
			catch{}

			System.Data.DataSet ds = new System.Data.DataSet();
			SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems();
			ds.Tables.Add(clsSalesTransactionItems.MostSalableItems(StartTransactionDate, EndTransactionDate, Limit));
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

			paramField = Report.DataDefinition.ParameterFields["GroupItems"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = chkGroupItems.Checked;
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["TransactionDate"];
			discreteParam = new ParameterDiscreteValue();
			if ((txtStartTransactionDate.Text != string.Empty || txtStartTransactionDate.Text != "") && (txtEndTransactionDate.Text != string.Empty || txtEndTransactionDate.Text != ""))
				discreteParam.Value = txtStartTransactionDate.Text + " - " + txtEndTransactionDate.Text;
			else if (txtStartTransactionDate.Text != string.Empty || txtStartTransactionDate.Text != "" )
				discreteParam.Value = txtStartTransactionDate.Text;
			else if (txtEndTransactionDate.Text != string.Empty && txtEndTransactionDate.Text != "")
				discreteParam.Value = txtStartTransactionDate.Text;
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
            //fraViewer.Visible = true;

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
