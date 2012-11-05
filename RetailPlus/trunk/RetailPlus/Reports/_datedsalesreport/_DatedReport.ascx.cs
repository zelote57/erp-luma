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

	public partial  class __DatedReport : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboContactName;

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
			cboReportType.Items.Clear();

            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesPerHour, ReportTypes.SalesPerHour));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesPerDay, ReportTypes.SalesPerDay));
            cboReportType.Items.Add(new ListItem(ReportTypes.SummarizeDailySales, ReportTypes.SummarizeDailySales));
            cboReportType.Items.Add(new ListItem(ReportTypes.SummarizeDailySalesWithTF, ReportTypes.SummarizeDailySalesWithTF));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactions, ReportTypes.SalesTransactions));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactionPerCustomer, ReportTypes.SalesTransactionPerCustomer));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactionPerCashier, ReportTypes.SalesTransactionPerCashier));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactionPerCashierPerCustomer, ReportTypes.SalesTransactionPerCashierPerCustomer));
            cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactionPerTerminal, ReportTypes.SalesTransactionPerTerminal));
			cboReportType.Items.Add(new ListItem(ReportTypes.SalesTransactionPerItem, ReportTypes.SalesTransactionPerItem));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
			cboReportType.Items.Add(new ListItem(ReportTypes.CashSalesDaily, ReportTypes.CashSalesDaily));
			cboReportType.Items.Add(new ListItem(ReportTypes.CashSalesMonthly, ReportTypes.CashSalesMonthly));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
			cboReportType.Items.Add(new ListItem(ReportTypes.ChequeSalesDaily, ReportTypes.ChequeSalesDaily));
			cboReportType.Items.Add(new ListItem(ReportTypes.ChequeSalesMonthly, ReportTypes.ChequeSalesMonthly));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.CreditCardSalesDaily, ReportTypes.CreditCardSalesDaily));
            cboReportType.Items.Add(new ListItem(ReportTypes.CreditCardSalesMonthly, ReportTypes.CreditCardSalesMonthly));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.DailySalesTransaction, ReportTypes.DailySalesTransaction));
            cboReportType.Items.Add(new ListItem(ReportTypes.WeeklySalesTransaction, ReportTypes.WeeklySalesTransaction));
            cboReportType.Items.Add(new ListItem(ReportTypes.MonthlySalesTransaction, ReportTypes.MonthlySalesTransaction));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.PaidOut, ReportTypes.PaidOut));
            cboReportType.Items.Add(new ListItem(ReportTypes.Disburse, ReportTypes.Disburse));
            cboReportType.Items.Add(new ListItem(ReportTypes.RecieveOnAccount, ReportTypes.RecieveOnAccount));
			cboReportType.SelectedIndex = 0;

			cboTransactionStatus.Items.Clear();
			foreach(string status in Enum.GetNames(typeof(TransactionStatus)))
			{
				cboTransactionStatus.Items.Add(new ListItem(status, status));
			}
			cboTransactionStatus.SelectedIndex = cboTransactionStatus.Items.IndexOf( cboTransactionStatus.Items.FindByText(TransactionStatus.NotYetApplied.ToString()));

			cboPaymentType.Items.Clear();
			foreach(string PaymentType in Enum.GetNames(typeof(PaymentTypes)))
			{
				cboPaymentType.Items.Add(new ListItem(PaymentType, PaymentType));
			}
			cboPaymentType.SelectedIndex = cboPaymentType.Items.IndexOf( cboPaymentType.Items.FindByText(PaymentTypes.NotYetAssigned.ToString()));

			txtStartTransactionDate.Text = Common.ToShortDateString(DateTime.Now.AddDays(-1));
            txtEndTransactionDate.Text = Common.ToShortDateString(DateTime.Now);

            Customer clsCustomer = new Customer();
            cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (txtContactName.Text.Trim() != string.Empty) cboContactName.SelectedIndex = 1; else cboContactName.SelectedIndex = 0;

            Terminal clsTerminal = new Terminal(clsCustomer.Connection, clsCustomer.Transaction);
            cboTerminalNo.DataTextField = "TerminalNo";
            cboTerminalNo.DataValueField = "TerminalNo";
            cboTerminalNo.DataSource = clsTerminal.ListAsDataTable(txtTerminalNo.Text);
            cboTerminalNo.DataBind();
            cboTerminalNo.Items.Insert(0, new ListItem(Constants.ALL, Constants.ALL));
            if (txtTerminalNo.Text.Trim() != string.Empty) cboTerminalNo.SelectedIndex = 1; else cboTerminalNo.SelectedIndex = 0;

            Security.AccessUser clsAccessUser = new Security.AccessUser(clsCustomer.Connection, clsCustomer.Transaction);
            cboCashierName.DataTextField = "Name";
            cboCashierName.DataValueField = "UID";
            cboCashierName.DataSource = clsAccessUser.Cashiers(txtCashierName.Text, 0);
            cboCashierName.DataBind();
            cboCashierName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (txtCashierName.Text.Trim() != string.Empty) cboCashierName.SelectedIndex = 1; else cboCashierName.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsCustomer.Connection, clsCustomer.Transaction);
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupName";
            cboProductGroup.DataSource = clsProductGroup.DataTable("ProductGroupName", SortOption.Ascending);
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductGroup.SelectedIndex = 0;
            clsCustomer.CommitAndDispose();

            #region Sales Per Day
            cboMonth.Items.Add(new ListItem("January", "1"));
            cboMonth.Items.Add(new ListItem("February", "2"));
            cboMonth.Items.Add(new ListItem("March", "3"));
            cboMonth.Items.Add(new ListItem("April", "4"));
            cboMonth.Items.Add(new ListItem("May", "5"));
            cboMonth.Items.Add(new ListItem("June", "6"));
            cboMonth.Items.Add(new ListItem("July", "7"));
            cboMonth.Items.Add(new ListItem("August", "8"));
            cboMonth.Items.Add(new ListItem("September", "9"));
            cboMonth.Items.Add(new ListItem("October", "10"));
            cboMonth.Items.Add(new ListItem("November", "11"));
            cboMonth.Items.Add(new ListItem("Decemeber", "12"));
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;

            int x = 2007;
            while (x <= DateTime.Now.Year)
            {
                cboYear.Items.Add(new ListItem(x.ToString(), x.ToString()));
                x++;
            }
            cboYear.SelectedIndex = cboYear.Items.Count - 1;
            #endregion
        }

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            string strReportType = cboReportType.SelectedValue;

            switch (strReportType)
            {
                case ReportTypes.SalesPerHour:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesPerHour.rpt"));
                    break;
                case ReportTypes.SalesPerDay:
                case ReportTypes.SalesPerDayWithTF:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesPerDay.rpt"));
                    break;
                case ReportTypes.SummarizeDailySales:
                case ReportTypes.SummarizeDailySalesWithTF:
                    {
                        if (optActualAndEffective.Checked)
                            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSummarizedPerDayWithActualEffective.rpt"));
                        else if (optEffective.Checked)
                            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSummarizedPerDayWithEffective.rpt"));
                        else if (optActual.Checked)
                            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSummarizedPerDayWithActual.rpt"));
                    }
                    break;
                case ReportTypes.SalesTransactions:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionsReport.rpt"));
                    break;
                case ReportTypes.SalesTransactionPerCustomer:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerCustomer.rpt"));
                    break;
                case ReportTypes.SalesTransactionPerCashier:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerCashier.rpt"));
                    break;
                case ReportTypes.SalesTransactionPerCashierPerCustomer:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerCashierPerCustomer.rpt"));
                    break;
                case ReportTypes.SalesTransactionPerTerminal:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerTerminal.rpt"));
                    break;
                case ReportTypes.SalesTransactionPerItem:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerItem.rpt"));
                    break;
                case ReportTypes.CashSalesDaily:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportCashSalesDaily.rpt"));
                    break;
                case ReportTypes.CashSalesMonthly:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportCashSalesMonthly.rpt"));
                    break;
                case ReportTypes.ChequeSalesDaily:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportChequeSalesDaily.rpt"));
                    break;
                case ReportTypes.ChequeSalesMonthly:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportChequeSalesMonthly.rpt"));
                    break;
                case ReportTypes.CreditCardSalesDaily:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportCreditCardSalesDaily.rpt"));
                    break;
                case ReportTypes.CreditCardSalesMonthly:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportCreditCardSalesMonthly.rpt"));
                    break;
                case ReportTypes.DailySalesTransaction:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerDay.rpt"));
                    break;
                case ReportTypes.WeeklySalesTransaction:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerWeek.rpt"));
                    break;
                case ReportTypes.MonthlySalesTransaction:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportSalesTransactionPerMonth.rpt"));
                    break;
                case ReportTypes.PaidOut:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportPaidOut.rpt"));
                    break;
                case ReportTypes.Disburse:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportDisburse.rpt"));
                    break;
                case ReportTypes.RecieveOnAccount:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_datedsalesreport/_DatedReportROC.rpt"));
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
            string strFileName = "datedrep_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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
            ReportDocument rpt = getReportDocument();
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
        }

        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
            string TransactionNo = txtTransactionNo.Text;
            string CustomerName = cboContactName.SelectedItem.Text == Constants.ALL ? string.Empty : cboContactName.SelectedItem.Text;
            string CashierName = cboCashierName.SelectedItem.Text == Constants.ALL ? string.Empty : cboCashierName.SelectedItem.Text;
            string TerminalNo = cboTerminalNo.SelectedItem.Text == Constants.ALL ? string.Empty : cboTerminalNo.SelectedItem.Text;
            DateTime StartTransactionDate = DateTime.MinValue;
            try
            { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text + " " + txtStartTime.Text); }
            catch { }
            DateTime EndTransactionDate = DateTime.MinValue;
            try
            { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text + " " + txtEndTime.Text); }
            catch { }
            TransactionStatus Status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), cboTransactionStatus.SelectedItem.Value);
            PaymentTypes PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), cboPaymentType.SelectedItem.Value);

            DataTable dt = new DataTable();

			ReportDataset rptds = new ReportDataset();

			SalesTransactions clsSalesTransactions;
			SalesTransactionItems clsSalesTransactionItems;

            SalesTransactionsColumns clsSalesTransactionsColumns = new SalesTransactionsColumns();
            #region clsSalesTransactionsColumns

            clsSalesTransactionsColumns.TransactionNo = true;
            clsSalesTransactionsColumns.CustomerName = true;
            clsSalesTransactionsColumns.CashierName = true;
            clsSalesTransactionsColumns.TerminalNo = true;
            clsSalesTransactionsColumns.TransactionDate = true;
            clsSalesTransactionsColumns.DateSuspended = true;
            clsSalesTransactionsColumns.DateResumed = true;
            clsSalesTransactionsColumns.TransactionStatus = true;
            clsSalesTransactionsColumns.SubTotal = true;
            clsSalesTransactionsColumns.Discount = true;
            clsSalesTransactionsColumns.VAT = true;
            clsSalesTransactionsColumns.VatableAmount = true;
            clsSalesTransactionsColumns.LocalTax = true;
            clsSalesTransactionsColumns.AmountPaid = true;
            clsSalesTransactionsColumns.CashPayment = true;
            clsSalesTransactionsColumns.ChequePayment = true;
            clsSalesTransactionsColumns.CreditCardPayment = true;
            clsSalesTransactionsColumns.BalanceAmount = true;
            clsSalesTransactionsColumns.ChangeAmount = true;
            clsSalesTransactionsColumns.DateClosed = true;
            clsSalesTransactionsColumns.PaymentType = true;
            clsSalesTransactionsColumns.ItemsDiscount = true;
            clsSalesTransactionsColumns.Charge = true;
            clsSalesTransactionsColumns.CreditPayment = true;
            clsSalesTransactionsColumns.CreatedByName = true;

            #endregion

            SalesTransactionDetails clsSearchKey = new SalesTransactionDetails();
            clsSearchKey = new SalesTransactionDetails();
            clsSearchKey.TransactionNo = TransactionNo;
            clsSearchKey.CustomerName = CustomerName;
            clsSearchKey.CashierName = CashierName;
            clsSearchKey.TerminalNo = TerminalNo;
            clsSearchKey.TransactionDateFrom = StartTransactionDate;
            clsSearchKey.TransactionDateTo = EndTransactionDate;
            clsSearchKey.TransactionStatus = Status;
            clsSearchKey.PaymentType = PaymentType;

            bool boWithTrustFund = true;

			string strReportType = cboReportType.SelectedValue;

            switch (strReportType)
			{
                case ReportTypes.SalesPerHour:
                    #region Sales Per Hour
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.SalesPerHour(StartTransactionDate, EndTransactionDate,0, cboTerminalNo.SelectedItem.Text);
			        clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.SalesPerHour.NewRow();

                        foreach (DataColumn dc in rptds.SalesPerHour.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.SalesPerHour.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.SalesPerDay:
                case ReportTypes.SalesPerDayWithTF:
                    #region Sales Per Day
                    if (strReportType == ReportTypes.SalesPerDay) boWithTrustFund = false;
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.SalesPerDay(StartTransactionDate, EndTransactionDate, boWithTrustFund);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.SalesPerDay.NewRow();

                        foreach (DataColumn dc in rptds.SalesPerDay.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.SalesPerDay.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.SummarizeDailySales:
                case ReportTypes.SummarizeDailySalesWithTF:
                    #region SummarizeDailySales

                    if (strReportType == ReportTypes.SummarizeDailySales) boWithTrustFund = false;
                    Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
                    dt = clsTerminalReportHistory.SummarizedDailySalesReport(0, boWithTrustFund, TerminalNo, StartTransactionDate, EndTransactionDate);
                    clsTerminalReportHistory.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.SalesReport.NewRow();

                        foreach (DataColumn dc in rptds.SalesReport.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.SalesReport.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.SalesTransactions:
                    #region Sales Transactions
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.List(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TransactionNo", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Transactions.NewRow();

                        foreach (DataColumn dc in rptds.Transactions.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Transactions.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.SalesTransactionPerCustomer:
                case ReportTypes.SalesTransactionPerCashierPerCustomer:
                    #region Sales Transaction Per Customer
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.List(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "CustomerName", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Transactions.NewRow();

                        foreach (DataColumn dc in rptds.Transactions.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Transactions.Rows.Add(drNew);
                    }
                    break;
                    #endregion
                case ReportTypes.SalesTransactionPerCashier:
                    #region Sales Transaction Per Cashier/Customer & Per Cashier
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.List(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "CashierName", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Transactions.NewRow();

                        foreach (DataColumn dc in rptds.Transactions.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Transactions.Rows.Add(drNew);
                    }
					break;
                    #endregion

                case ReportTypes.DailySalesTransaction:
                case ReportTypes.WeeklySalesTransaction:
                case ReportTypes.MonthlySalesTransaction:
                    #region Daily, Weekely, Monthly Sales Transaction

                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.List(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TransactionDate", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Transactions.NewRow();

                        foreach (DataColumn dc in rptds.Transactions.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Transactions.Rows.Add(drNew);
                    }
					break;
                    #endregion

                case ReportTypes.SalesTransactionPerTerminal:
                    #region Sales Transaction Per Terminal
                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.List(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TerminalNo", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Transactions.NewRow();

                        foreach (DataColumn dc in rptds.Transactions.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Transactions.Rows.Add(drNew);
                    }
					break;
                    #endregion

                case ReportTypes.SalesTransactionPerItem:
                    #region Sales Transaction Per Item

                    MySqlDataReader myreader;
                    SaleperItemFilterType enumSaleperItemFilterType = SaleperItemFilterType.ShowBothPositiveAndNegative;
                    if (rdoShowPositiveOnly.Checked) enumSaleperItemFilterType = SaleperItemFilterType.ShowPositiveOnly;
                    if (rdoShowNegativeOnly.Checked) enumSaleperItemFilterType = SaleperItemFilterType.ShowNegativeOnly;

					clsSalesTransactionItems = new SalesTransactionItems();
                    if (cboProductGroup.Text == Constants.ALL)
					    myreader = clsSalesTransactionItems.SalesPerItem(TransactionNo, CustomerName, CashierName,
                            TerminalNo, StartTransactionDate, EndTransactionDate, Status, PaymentType, enumSaleperItemFilterType);
                    else
                        myreader = clsSalesTransactionItems.SalesPerItemByGroup(cboProductGroup.SelectedItem.Value, TransactionNo, CustomerName, CashierName,
                            TerminalNo, StartTransactionDate, EndTransactionDate, Status, PaymentType, enumSaleperItemFilterType);

					clsSalesTransactionItems.CommitAndDispose();
					while(myreader.Read())
					{
						DataRow drSalesTransactionPerItem = rptds.SalesTransactionPerItem.NewRow();

						foreach (DataColumn dc in rptds.SalesTransactionPerItem.Columns)
							drSalesTransactionPerItem[dc] = myreader[dc.ColumnName]; 

						rptds.SalesTransactionPerItem.Rows.Add(drSalesTransactionPerItem);
					}
					break;
                    #endregion

                case ReportTypes.CashSalesDaily:
                case ReportTypes.CashSalesMonthly:
                    #region Cash-Sales Daily & Cash-Sales Monthly
                    clsSalesTransactionsColumns = new SalesTransactionsColumns();
                    #region clsSalesTransactionsColumns

                    clsSalesTransactionsColumns.TransactionDate = true;
                    clsSalesTransactionsColumns.TransactionStatus = true;
                    clsSalesTransactionsColumns.TransactionStatusName = true;
                    clsSalesTransactionsColumns.CashPayment = true;

                    #endregion

                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.Cash_Cheque_CreditCard_Credit_Sales(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TerminalNo", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.CashSales.NewRow();

                        foreach (DataColumn dc in rptds.CashSales.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.CashSales.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.ChequeSalesDaily:
                case ReportTypes.ChequeSalesMonthly:
                    #region Cheque-Sales Daily & Cheque-Sales Monthly
                    clsSalesTransactionsColumns = new SalesTransactionsColumns();
                    #region clsSalesTransactionsColumns

                    clsSalesTransactionsColumns.TransactionDate = true;
                    clsSalesTransactionsColumns.TransactionStatus = true;
                    clsSalesTransactionsColumns.TransactionStatusName = true;
                    clsSalesTransactionsColumns.ChequePayment = true;

                    #endregion

                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.Cash_Cheque_CreditCard_Credit_Sales(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TerminalNo", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.ChequeSales.NewRow();

                        foreach (DataColumn dc in rptds.ChequeSales.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.ChequeSales.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.CreditCardSalesDaily:
                case ReportTypes.CreditCardSalesMonthly:
                    #region Card-Sales Daily    Card-Sales Monthly

                    clsSalesTransactionsColumns = new SalesTransactionsColumns();
                    #region clsSalesTransactionsColumns

                    clsSalesTransactionsColumns.TransactionDate = true;
                    clsSalesTransactionsColumns.TransactionStatus = true;
                    clsSalesTransactionsColumns.TransactionStatusName = true;
                    clsSalesTransactionsColumns.CreditCardPayment = true;

                    #endregion

                    clsSalesTransactions = new SalesTransactions();
                    dt = clsSalesTransactions.Cash_Cheque_CreditCard_Credit_Sales(clsSalesTransactionsColumns, clsSearchKey, System.Data.SqlClient.SortOrder.Ascending, 0, "TerminalNo", System.Data.SqlClient.SortOrder.Ascending);
                    clsSalesTransactions.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.CreditCardSales.NewRow();

                        foreach (DataColumn dc in rptds.CreditCardSales.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.CreditCardSales.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.PaidOut:
                    #region PaidOut Report
                    PaidOutColumns clsPaidOutColumns = new PaidOutColumns();
                    clsPaidOutColumns.BranchID = true;
                    clsPaidOutColumns.TerminalNo = true;
                    clsPaidOutColumns.Amount = true;
                    clsPaidOutColumns.PaymentType = true;
                    clsPaidOutColumns.DateCreated = true;
                    clsPaidOutColumns.TerminalNo = true;
                    clsPaidOutColumns.CashierID = true;
                    clsPaidOutColumns.CashierName = true;
                    clsPaidOutColumns.Remarks = true;

                    PaidOutDetails clsPaidOutSeachKey = new PaidOutDetails();
                    clsPaidOutSeachKey.StartTransactionDate = StartTransactionDate;
                    clsPaidOutSeachKey.EndTransactionDate = EndTransactionDate;

                    PaidOut clsPaidOut = new PaidOut();
                    dt = clsPaidOut.ListAsDataTable(clsPaidOutColumns, clsPaidOutSeachKey, 0, string.Empty, System.Data.SqlClient.SortOrder.Ascending);
                    clsPaidOut.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.PaidOut.NewRow();

                        foreach (DataColumn dc in rptds.PaidOut.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.PaidOut.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.Disburse:
                    #region Disburse Report
                    DisburseColumns clsDisburseColumns = new DisburseColumns();
                    clsDisburseColumns.BranchID = true;
                    clsDisburseColumns.TerminalNo = true;
                    clsDisburseColumns.Amount = true;
                    clsDisburseColumns.PaymentType = true;
                    clsDisburseColumns.DateCreated = true;
                    clsDisburseColumns.TerminalNo = true;
                    clsDisburseColumns.CashierID = true;
                    clsDisburseColumns.CashierName = true;
                    clsDisburseColumns.Remarks = true;

                    DisburseDetails clsDisburseSeachKey = new DisburseDetails();
                    clsDisburseSeachKey.StartTransactionDate = StartTransactionDate;
                    clsDisburseSeachKey.EndTransactionDate = EndTransactionDate;

                    Disburse clsDisburse = new Disburse();
                    dt = clsDisburse.ListAsDataTable(clsDisburseColumns, clsDisburseSeachKey, 0, string.Empty, System.Data.SqlClient.SortOrder.Ascending);
                    clsDisburse.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Disburse.NewRow();

                        foreach (DataColumn dc in rptds.Disburse.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Disburse.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.RecieveOnAccount:
                    #region WithHold Report
                    WithHoldColumns clsWithHoldColumns = new WithHoldColumns();
                    clsWithHoldColumns.BranchID = true;
                    clsWithHoldColumns.TerminalNo = true;
                    clsWithHoldColumns.Amount = true;
                    clsWithHoldColumns.PaymentType = true;
                    clsWithHoldColumns.DateCreated = true;
                    clsWithHoldColumns.TerminalNo = true;
                    clsWithHoldColumns.CashierID = true;
                    clsWithHoldColumns.CashierName = true;
                    clsWithHoldColumns.Remarks = true;

                    WithHoldDetails clsWithHoldSeachKey = new WithHoldDetails();
                    clsWithHoldSeachKey.StartTransactionDate = StartTransactionDate;
                    clsWithHoldSeachKey.EndTransactionDate = EndTransactionDate;

                    WithHold clsWithHold = new WithHold();
                    dt = clsWithHold.ListAsDataTable(clsWithHoldColumns, clsWithHoldSeachKey, 0, string.Empty, System.Data.SqlClient.SortOrder.Ascending);
                    clsWithHold.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Withhold.NewRow();

                        foreach (DataColumn dc in rptds.Withhold.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Withhold.Rows.Add(drNew);
                    }
                    break;
                    #endregion

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

            switch (cboReportType.SelectedItem.Text)
            {
                case ReportTypes.SalesPerDay:
                case ReportTypes.SalesPerDayWithTF:
                    paramField = Report.DataDefinition.ParameterFields["Month"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = cboMonth.SelectedItem.Text;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    
                    paramField = Report.DataDefinition.ParameterFields["Year"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = cboYear.SelectedItem.Text;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    break;
                default:
                    DateTime StartTransactionDate = DateTime.MinValue;
                    try
                    { StartTransactionDate = Convert.ToDateTime(txtStartTransactionDate.Text + " " + txtStartTime.Text); }
                    catch { }
                    paramField = Report.DataDefinition.ParameterFields["StartTransactionDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = StartTransactionDate;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    DateTime EndTransactionDate = DateTime.MinValue;
                    try
                    { EndTransactionDate = Convert.ToDateTime(txtEndTransactionDate.Text + " " + txtEndTime.Text); }
                    catch { }
                    paramField = Report.DataDefinition.ParameterFields["EndTransactionDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = EndTransactionDate;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;
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

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            holderTranDate.Visible = true;
            holderTerminaNo.Visible = false; holderSummarizedDailySales.Visible = false;
            holderTransaction.Visible = false; holderSalesperItem.Visible = false;
            holderSalesPerDay.Visible = false; 

            switch (cboReportType.SelectedItem.Text)
            {
                case ReportTypes.SalesPerHour:
                    holderTerminaNo.Visible = true;
                    break;
                case ReportTypes.SalesPerDay:
                case ReportTypes.SalesPerDayWithTF:
                    holderSalesPerDay.Visible = true; 
                    holderTranDate.Visible = false;
                    break;
                case ReportTypes.SummarizeDailySales:
                case ReportTypes.SummarizeDailySalesWithTF:
                    holderTerminaNo.Visible = true;
                    holderSummarizedDailySales.Visible = true;
                    break;

                case ReportTypes.SalesTransactions:
                case ReportTypes.SalesTransactionPerCustomer:
                case ReportTypes.SalesTransactionPerCashier:
                case ReportTypes.DailySalesTransaction:
                case ReportTypes.WeeklySalesTransaction:
                case ReportTypes.MonthlySalesTransaction:
                case ReportTypes.SalesTransactionPerTerminal:
                case ReportTypes.CashSalesDaily:
                case ReportTypes.CashSalesMonthly:
                case ReportTypes.ChequeSalesDaily:
                case ReportTypes.ChequeSalesMonthly:
                case ReportTypes.CreditCardSalesDaily:
                case ReportTypes.CreditCardSalesMonthly:
                case ReportTypes.SalesTransactionPerCashierPerCustomer:
                    holderTransaction.Visible = true;
                    holderTerminaNo.Visible = true;
                    break;

                case ReportTypes.SalesTransactionPerItem:
                    holderSalesperItem.Visible = true;
                    break;

                case ReportTypes.PaidOut:
                    break;

                case ReportTypes.Disburse:
                    break;

                case ReportTypes.RecieveOnAccount:
                    break;

                default:
                    return;

            }
        }

        protected void imgTerminalNoSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Terminal clsTerminal = new Terminal();
            cboTerminalNo.DataTextField = "TerminalNo";
            cboTerminalNo.DataValueField = "TerminalNo";
            cboTerminalNo.DataSource = clsTerminal.ListAsDataTable(txtTerminalNo.Text);
            cboTerminalNo.DataBind();
            cboTerminalNo.Items.Insert(0, new ListItem(Constants.ALL, Constants.ALL));
            if (cboTerminalNo.Items.Count > 1 && txtTerminalNo.Text.Trim() != string.Empty) cboTerminalNo.SelectedIndex = 1; else cboTerminalNo.SelectedIndex = 0;
            clsTerminal.CommitAndDispose();
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

        protected void imgCashierNameSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Security.AccessUser clsAccessUser = new Security.AccessUser();
            cboCashierName.DataTextField = "Name";
            cboCashierName.DataValueField = "UID";
            cboCashierName.DataSource = clsAccessUser.Cashiers(txtCashierName.Text, 0);
            cboCashierName.DataBind();
            cboCashierName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboCashierName.Items.Count > 1 && txtCashierName.Text.Trim() != string.Empty) cboCashierName.SelectedIndex = 1; else cboCashierName.SelectedIndex = 0;
            clsAccessUser.CommitAndDispose();
        }

        #endregion

    }
}