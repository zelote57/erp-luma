namespace AceSoft.RetailPlus.Credits._Customers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;

    public partial class __Details : System.Web.UI.UserControl
	{

        #region Web Control Methods

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
                if (Visible)
                {
                    LoadOptions();
                    LoadRecord();
                }
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
        private void InitializeComponent()
        {

        }

        #endregion

        #region Web Control Methods

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        
        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        
        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void lstPurchases_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["TransactionID"].ToString();

                HyperLink lnkTransactionNo = (HyperLink)e.Item.FindControl("lnkTransactionNo");
                lnkTransactionNo.Text = dr["TransactionNo"].ToString();
                lnkTransactionNo.NavigateUrl = Constants.ROOT_DIRECTORY + "/Reports/Default.aspx?task=transaction&tranno=" + dr["TransactionNo"].ToString() + "&termno=" + dr["TerminalNo"].ToString() + "&branchid=" + dr["BranchID"].ToString();

                Label lblTransactionDate = (Label)e.Item.FindControl("lblTransactionDate");
                lblTransactionDate.Text = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd-MMM-yyyy hh:mm tt");

                Label lblCreditReason = (Label)e.Item.FindControl("lblCreditReason");
                lblCreditReason.Text = dr["CreditReason"].ToString();

                Label lblCredit = (Label)e.Item.FindControl("lblCredit");
                lblCredit.Text = decimal.Parse(dr["Credit"].ToString()).ToString("#,##0.#0");

                Label lblCreditPaid = (Label)e.Item.FindControl("lblCreditPaid");
                lblCreditPaid.Text = decimal.Parse(dr["CreditPaid"].ToString()).ToString("#,##0.#0");

                Label lblBalance = (Label)e.Item.FindControl("lblBalance");
                lblBalance.Text = decimal.Parse(dr["Balance"].ToString()).ToString("#,##0.#0");

            }
        }

        protected void cmdPrintBilling_Click(object sender, System.EventArgs e)
        {
            PrintBillingReport();
        }

        protected void imgPrintBilling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintBillingReport();
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            lblContactID.Text = iID.ToString();

            ContactGroups clsContactGroup = new ContactGroups();

            cboGroup.DataTextField = "ContactGroupName";
            cboGroup.DataValueField = "ContactGroupID";
            cboGroup.DataSource = clsContactGroup.ListAsDataTable().DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

            Department clsDepartment = new Department(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboDepartment.DataTextField = "DepartmentName";
            cboDepartment.DataValueField = "DepartmentID";
            cboDepartment.DataSource = clsDepartment.ListAsDataTable("DepartmentName", SortOption.Ascending, 0).DefaultView;
            cboDepartment.DataBind();
            cboDepartment.SelectedIndex = 0;

            Positions clsPosition = new Positions(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboPosition.DataTextField = "PositionName";
            cboPosition.DataValueField = "PositionID";
            cboPosition.DataSource = clsPosition.ListAsDataTable("PositionName", SortOption.Ascending, 0).DefaultView;
            cboPosition.DataBind();
            cboPosition.SelectedIndex = 0;

            cboCreditCardStatus.Items.Clear();
            foreach (CreditCardStatus selection in Enum.GetValues(typeof(CreditCardStatus)))
            {
                cboCreditCardStatus.Items.Add(new ListItem(selection.ToString("G"), selection.ToString("d")));
            }
            cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(CreditCardStatus.All.ToString("d")));

            Data.CardType clsCardType = new Data.CardType(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboCreditCardType.Items.Clear();
            cboCreditCardType.DataTextField = "CardTypeName";
            cboCreditCardType.DataValueField = "CardTypeID";
            cboCreditCardType.DataSource = clsCardType.ListAsDataTable(new CardTypeDetails(CreditCardTypes.Internal)).DefaultView;
            cboCreditCardType.DataBind();
            cboCreditCardType.SelectedIndex = 0;

            Billing clsBilling = new Billing(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboBillingDate.DataTextField = "BillingDate";
            cboBillingDate.DataValueField = "BillingFile";
            cboBillingDate.DataSource = clsBilling.ListBillingDateAsDataTable(CreditType.Individual, long.Parse(lblContactID.Text), limit: 10).DefaultView;
            cboBillingDate.DataBind();
            cboBillingDate.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.PLEASE_SELECT));
            cboBillingDate.SelectedIndex = 0;

            Salutation clsSalutation = new Salutation(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboSalutation.DataTextField = "SalutationName";
            cboSalutation.DataValueField = "SalutationCode";
            cboSalutation.DataSource = clsSalutation.ListAsDataTable().DefaultView;
            cboSalutation.DataBind();
            cboSalutation.SelectedIndex = 0;
            cboSalutation.SelectedIndex = cboSalutation.Items.IndexOf(cboSalutation.Items.FindByValue("MR"));

            clsContactGroup.CommitAndDispose();
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            Contacts clsContact = new Contacts();
            ContactDetails clsDetails = clsContact.Details(iID);

            clsContact.CommitAndDispose();

            lblContactID.Text = clsDetails.ContactID.ToString();
            txtContactCode.Text = clsDetails.ContactCode;
            txtContactName.Text = clsDetails.ContactName;
            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(clsDetails.ContactGroupID.ToString()));
            cboModeOfTerms.SelectedIndex = cboModeOfTerms.Items.IndexOf(cboModeOfTerms.Items.FindByValue(clsDetails.ModeOfTerms.ToString("d")));
            txtTerms.Text = clsDetails.Terms.ToString("###0");
            txtAddress.Text = clsDetails.Address;
            txtBusinessName.Text = clsDetails.BusinessName;
            txtTelephoneNo.Text = clsDetails.TelephoneNo;
            txtRemarks.Text = clsDetails.Remarks;
            txtDebit.Text = clsDetails.Debit.ToString("###0.#0");
            chkIsCreditAllowed.Checked = clsDetails.IsCreditAllowed;
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(clsDetails.DepartmentID.ToString()));
            cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(clsDetails.PositionID.ToString()));

            txtCreditCardNo.Text = clsDetails.CreditDetails.CreditCardNo;
            cboCreditCardType.SelectedIndex = cboCreditCardType.Items.IndexOf(cboCreditCardType.Items.FindByValue(clsDetails.CreditDetails.CardTypeDetails.CardTypeID.ToString()));
            txtCreditAwardDate.Text = clsDetails.CreditDetails.CreditAwardDate.ToString("yyyy-MMM-dd");
            txtExpiryDate.Text = clsDetails.CreditDetails.ExpiryDate.ToString("yyyy-MMM-dd");
            cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(clsDetails.CreditDetails.CreditCardStatus.ToString("d")));
            lblCreditCardActive.Text = clsDetails.CreditDetails.CreditActive ? "Active" : "InActive (Hold/Suspended)";
            txtCreditLimit.Text = clsDetails.CreditLimit.ToString("###0.#0");
            txtCredit.Text = clsDetails.Credit.ToString("###0.#0");
            txtPaidAmount.Text = "0.00";
            txtCurrentBalance.Text = (clsDetails.CreditLimit - clsDetails.Credit).ToString("###0.#0");
            lblLastBillingDate.Text = "Last Billing Date:" + clsDetails.CreditDetails.LastBillingDate.ToString("yyyy-MMM-dd");

            // 26Oct2014 - add the additional information
            cboSalutation.SelectedIndex = cboSalutation.Items.IndexOf(cboSalutation.Items.FindByValue(clsDetails.AdditionalDetails.Salutation));
            txtFirstName.Text = clsDetails.AdditionalDetails.FirstName;
            txtMiddleName.Text = clsDetails.AdditionalDetails.MiddleName;
            txtLastName.Text = clsDetails.AdditionalDetails.LastName;
            txtBirthDate.Text = clsDetails.AdditionalDetails.BirthDate.ToString("yyyy-MM-dd");
            txtMobileNo.Text = clsDetails.AdditionalDetails.MobileNo;

            LoadPurchases(clsDetails.ContactID);
            
        }
        private void LoadPurchases(Int64 CreditorID)
        {
            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
            System.Data.DataTable dt = clsSalesTransactions.ListForPaymentDataTable(CreditorID);
            clsSalesTransactions.CommitAndDispose();
            lstPurchases.DataSource = dt.DefaultView;
            lstPurchases.DataBind();
        }
        private void PrintBillingReport()
        {

            string stParam = cboBillingDate.SelectedItem.Value;
            if (stParam != Constants.PLEASE_SELECT)
            {
                string newWindowUrl = Constants.ROOT_DIRECTORY_BILLING_WoutG + "/" + stParam;
                string javaScript = "window.open('" + newWindowUrl + "','_blank');";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.cmdPrintBilling, this.cmdPrintBilling.GetType(), "openwindow", javaScript, true);
            }
        }

        #endregion
    }
}
