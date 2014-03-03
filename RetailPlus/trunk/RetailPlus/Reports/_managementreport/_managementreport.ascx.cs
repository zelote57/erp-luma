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

	public partial  class __ManagementReport : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboContactName;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "managementreport";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "managementreport")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
            Int64 UID = Convert.ToInt64(Session["UID"]);
            Security.AccessRights clsAccessRights = new Security.AccessRights();

            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.MANAGEMENT_PerBranch, ReportTypes.MANAGEMENT_PerBranch));
            cboReportType.Items.Add(new ListItem(ReportTypes.MANAGEMENT_PerBranchPerMonth, ReportTypes.MANAGEMENT_PerBranchPerMonth));
            cboReportType.Items.Add(new ListItem(ReportTypes.MANAGEMENT_PerBranchPerMonthWithCovers, ReportTypes.MANAGEMENT_PerBranchPerMonthWithCovers));
            cboReportType.Items.Add(new ListItem(ReportTypes.MANAGEMENT_PerBranchPerDay, ReportTypes.MANAGEMENT_PerBranchPerDay));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.MANAGEMENT_PerCustomerGroupPerDay, ReportTypes.MANAGEMENT_PerCustomerGroupPerDay));
			cboReportType.SelectedIndex = 0;

            cboConsignment.Items.Clear();
            cboConsignment.Items.Add(new ListItem("Both", "-1"));
            cboConsignment.Items.Add(new ListItem("Yes", true.ToString()));
            cboConsignment.Items.Add(new ListItem("No", false.ToString()));

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
            if (string.IsNullOrEmpty(txtContactName.Text))
                cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboContactName.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtContactName.Text, Constants.ZERO_STRING));
            cboContactName.SelectedIndex = 0;

            cboAgent.Items.Clear();
            Contacts clsContact = new Contacts(clsCustomer.Connection, clsCustomer.Transaction);
            cboAgent.DataTextField = "ContactName";
            cboAgent.DataValueField = "ContactID";
            cboAgent.DataSource = clsContact.AgentsAsDataTable(txtAgent.Text, 0, "ContactName", SortOption.Ascending);
            cboAgent.DataBind();
            if (string.IsNullOrEmpty(txtAgent.Text))
                cboAgent.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboAgent.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtAgent.Text, Constants.ZERO_STRING));
            cboAgent.SelectedIndex = 0;

            Terminal clsTerminal = new Terminal(clsCustomer.Connection, clsCustomer.Transaction);
            cboTerminalNo.DataTextField = "TerminalNo";
            cboTerminalNo.DataValueField = "TerminalNo";
            cboTerminalNo.DataSource = clsTerminal.ListAsDataTable();
            cboTerminalNo.DataBind();
            cboTerminalNo.Items.Insert(0, new ListItem(Constants.ALL, Constants.ALL));
            cboTerminalNo.SelectedIndex = 0;

            Branch clsBranch = new Branch(clsCustomer.Connection, clsCustomer.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            cboBranch.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboBranch.SelectedIndex = 0;

            Security.AccessUser clsAccessUser = new Security.AccessUser(clsCustomer.Connection, clsCustomer.Transaction);
            cboCashierName.DataTextField = "Name";
            cboCashierName.DataValueField = "UID";
            cboCashierName.DataSource = clsAccessUser.Cashiers(txtCashierName.Text, 0);
            cboCashierName.DataBind();
            if (string.IsNullOrEmpty(txtCashierName.Text))
                cboCashierName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboCashierName.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtCashierName.Text, Constants.ZERO_STRING));
            cboCashierName.SelectedIndex = 0;

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
                case ReportTypes.MANAGEMENT_PerBranch:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_managementreport/_PerBranch.rpt"));
                    break;
                case ReportTypes.MANAGEMENT_PerBranchPerMonth:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_managementreport/_PerBranchPerMonth.rpt"));
                    break;
                case ReportTypes.MANAGEMENT_PerBranchPerMonthWithCovers:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_managementreport/_PerBranchPerMonthCovers.rpt"));
                    break;
                case ReportTypes.MANAGEMENT_PerBranchPerDay:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_managementreport/_PerBranchPerDay.rpt"));
                    break;
                case ReportTypes.MANAGEMENT_PerCustomerGroupPerDay:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_managementreport/_PerCustomerGroupPerMonth.rpt"));
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
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_sales";
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
            string strProductGroup = cboProductGroup.SelectedItem.Text == Constants.ALL ? string.Empty : cboProductGroup.SelectedItem.Text;
            string TransactionNo = txtTransactionNo.Text;
            string CustomerName = cboContactName.SelectedItem.Text.Substring(0, 3).Trim() == Constants.ALL ? cboContactName.SelectedItem.Text.Replace("ALL", "").Replace("LIKE","").Trim() : cboContactName.SelectedItem.Text;
            string AgentName = cboAgent.SelectedItem.Text.Substring(0, 3).Trim() == Constants.ALL ? cboAgent.SelectedItem.Text.Replace("ALL", "").Replace("LIKE", "").Trim() : cboAgent.SelectedItem.Text;
            string CashierName = cboCashierName.SelectedItem.Text.Substring(0, 3).Trim() == Constants.ALL ? cboCashierName.SelectedItem.Text.Replace("ALL", "").Replace("LIKE", "").Trim() : cboCashierName.SelectedItem.Text;
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

            clsSalesTransactionsColumns.BranchCode = true;
            clsSalesTransactionsColumns.TransactionNo = true;
            clsSalesTransactionsColumns.CustomerName = true;
            clsSalesTransactionsColumns.CustomerGroupName = true;
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
            clsSalesTransactionsColumns.AgentName = true;
            clsSalesTransactionsColumns.PaxNo = true;

            #endregion

            SalesTransactionDetails clsSearchKey = new SalesTransactionDetails();
            clsSearchKey = new SalesTransactionDetails();
            clsSearchKey.TransactionNo = TransactionNo;
            clsSearchKey.CustomerName = CustomerName;
            clsSearchKey.CashierName = CashierName;
            clsSearchKey.TerminalNo = TerminalNo;
            clsSearchKey.BranchID = int.Parse(cboBranch.SelectedItem.Value);
            clsSearchKey.TransactionDateFrom = StartTransactionDate;
            clsSearchKey.TransactionDateTo = EndTransactionDate;
            clsSearchKey.TransactionStatus = Status;
            clsSearchKey.PaymentType = PaymentType;
            clsSearchKey.AgentName = AgentName;
            clsSearchKey.isConsignmentSearch = cboConsignment.SelectedItem.Value;
            if (clsSearchKey.isConsignmentSearch != "-1")
            {
                clsSearchKey.isConsignment = bool.Parse(cboConsignment.SelectedItem.Value);
            }

            bool boWithTrustFund = true;

			string strReportType = cboReportType.SelectedValue;

            switch (strReportType)
			{
                case ReportTypes.MANAGEMENT_PerBranch:
                case ReportTypes.MANAGEMENT_PerBranchPerMonth:
                case ReportTypes.MANAGEMENT_PerBranchPerMonthWithCovers:
                case ReportTypes.MANAGEMENT_PerBranchPerDay:
                case ReportTypes.MANAGEMENT_PerCustomerGroupPerDay:
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
                case ReportTypes.MANAGEMENT_PerBranch:
                case ReportTypes.MANAGEMENT_PerBranchPerMonth:
                case ReportTypes.MANAGEMENT_PerBranchPerDay:
                case ReportTypes.MANAGEMENT_PerCustomerGroupPerDay:
                    holderTransaction.Visible = true;
                    holderTerminaNo.Visible = true;
                    break;
                default:
                    return;

            }
        }

        //protected void imgTerminalNoSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    Terminal clsTerminal = new Terminal();
        //    cboTerminalNo.DataTextField = "TerminalNo";
        //    cboTerminalNo.DataValueField = "TerminalNo";
        //    cboTerminalNo.DataSource = clsTerminal.ListAsDataTable(txtTerminalNo.Text);
        //    cboTerminalNo.DataBind();
        //    cboTerminalNo.Items.Insert(0, new ListItem(Constants.ALL, Constants.ALL));
        //    if (cboTerminalNo.Items.Count > 1 && txtTerminalNo.Text.Trim() != string.Empty) cboTerminalNo.SelectedIndex = 1; else cboTerminalNo.SelectedIndex = 0;
        //    clsTerminal.CommitAndDispose();
        //}

        protected void imgContactNameSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Customer clsCustomer = new Customer();
            cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            clsCustomer.CommitAndDispose();
            if (string.IsNullOrEmpty(txtContactName.Text))
                cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboContactName.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtContactName.Text, Constants.ZERO_STRING));
            cboContactName.SelectedIndex = 0;
            
        }

        protected void imgCashierNameSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Security.AccessUser clsAccessUser = new Security.AccessUser();
            cboCashierName.DataTextField = "Name";
            cboCashierName.DataValueField = "UID";
            cboCashierName.DataSource = clsAccessUser.Cashiers(txtCashierName.Text, 0);
            cboCashierName.DataBind();
            clsAccessUser.CommitAndDispose();
            if (string.IsNullOrEmpty(txtCashierName.Text))
                cboCashierName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboCashierName.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtCashierName.Text, Constants.ZERO_STRING));
            cboCashierName.SelectedIndex = 0;            
        }

        protected void imgAgentSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            cboAgent.Items.Clear();
            Contacts clsContact = new Contacts();
            cboAgent.DataTextField = "ContactName";
            cboAgent.DataValueField = "ContactID";
            cboAgent.DataSource = clsContact.AgentsAsDataTable(txtAgent.Text, 0, "ContactName", SortOption.Ascending); 
            cboAgent.DataBind();
            if (string.IsNullOrEmpty(txtAgent.Text))
                cboAgent.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            else
                cboAgent.Items.Insert(0, new ListItem(Constants.ALL + " LIKE " + txtAgent.Text, Constants.ZERO_STRING));
            cboAgent.SelectedIndex = 0;
            clsContact.CommitAndDispose();
        }
        #endregion

    }
}
