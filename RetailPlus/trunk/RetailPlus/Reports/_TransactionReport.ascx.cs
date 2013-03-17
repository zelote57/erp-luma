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

	public partial  class __TransactionReport : System.Web.UI.UserControl
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
            try
            {
                Branch clsBranch = new Branch();
                cboBranch.DataTextField = "BranchCode";
                cboBranch.DataValueField = "BranchID";
                cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
                cboBranch.DataBind();
                clsBranch.CommitAndDispose();
                if (cboBranch.Items.Count == 0) cboBranch.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
                cboBranch.SelectedIndex = 0;
                
                if (Request.QueryString["task"].ToString().ToLower() == "transaction" && Request.QueryString["tranno"].ToString() != null)
                {
                    txtTransactionNo.Text = Request.QueryString["tranno"].ToString();
                    GenerateHTML();
                }
            }
            catch { }
		}

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_TransactionReport.rpt"));

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
            string strFileName = "tranreport_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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
                CRSHelper.OpenExportedReport(strFileName); // OpenExportedReport(strFileName);
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
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_TransactionReport.rpt"));
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
		}

		#endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();
			DataRow drNew;

			/****************************sales transaction *****************************/
			SalesTransactions clsSalesTransactions = new SalesTransactions();
			SalesTransactionDetails clsDetails = clsSalesTransactions.Details(txtTransactionNo.Text, txtTerminalNo.Text, int.Parse(cboBranch.SelectedItem.Value));
			clsSalesTransactions.CommitAndDispose();

			if (clsDetails.isExist == true )
			{
				drNew = rptds.Transactions.NewRow();
				
				drNew["TransactionID"]		= clsDetails.TransactionID;
				drNew["TransactionNo"]		= clsDetails.TransactionNo;
				drNew["CustomerName"]		= clsDetails.CustomerName;
				drNew["CashierName"]		= clsDetails.CashierName;
				drNew["TerminalNo"]			= clsDetails.TerminalNo;
				drNew["TransactionDate"]	= clsDetails.TransactionDate;
				drNew["DateSuspended"]		= clsDetails.DateSuspended.ToString();
				drNew["DateResumed"]		= clsDetails.DateResumed;
				drNew["TransactionStatus"]	= clsDetails.TransactionStatus;
				drNew["SubTotal"]			= clsDetails.SubTotal;
				drNew["ItemsDiscount"]		= clsDetails.ItemsDiscount;
				drNew["Discount"]			= clsDetails.Discount;
				drNew["VAT"]				= clsDetails.VAT;
				drNew["VatableAmount"]		= clsDetails.VatableAmount;
				drNew["LocalTax"]			= clsDetails.LocalTax;
				drNew["AmountPaid"]			= clsDetails.AmountPaid;
				drNew["CashPayment"]		= clsDetails.CashPayment;
				drNew["ChequePayment"]		= clsDetails.ChequePayment;
				drNew["CreditCardPayment"]	= clsDetails.CreditCardPayment;
				drNew["BalanceAmount"]		= clsDetails.BalanceAmount;
				drNew["ChangeAmount"]		= clsDetails.ChangeAmount;
				drNew["DateClosed"]			= clsDetails.DateClosed;
				drNew["PaymentType"]		= clsDetails.PaymentType.ToString("d");
				drNew["ItemsDiscount"]		= clsDetails.ItemsDiscount;
				drNew["Charge"]				= clsDetails.Charge;
                drNew["CreditPayment"]      = clsDetails.CreditPayment;
                drNew["CreatedByName"]      = clsDetails.CreatedByName;

				rptds.Transactions.Rows.Add(drNew);

				/****************************sales transaction items*****************************/
				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems();
				System.Data.DataTable dt = clsSalesTransactionItems.List(clsDetails.TransactionID, clsDetails.TransactionDate,"TransactionItemsID",SortOption.Ascending);
				clsSalesTransactionItems.CommitAndDispose();

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					drNew = rptds.TransactionItems.NewRow();
				
					foreach (DataColumn dc in rptds.TransactionItems.Columns)
                        drNew[dc] = dr[dc.ColumnName]; 
				
					rptds.TransactionItems.Rows.Add(drNew);
				}
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
