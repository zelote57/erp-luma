namespace AceSoft.RetailPlus.SalesAndReceivables._CreditMemo
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Update : System.Web.UI.UserControl
	{

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack && Visible)
            {
                lblReferrer.Text = Request.UrlReferrer.ToString();
                LoadOptions();
                LoadRecord();
            }
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

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdSaveBack_Click(object sender, System.EventArgs e)
        {
            SaveRecord();
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
        protected void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Data.Contacts clsContact = new Data.Contacts();
            Data.ContactDetails clsDetails = clsContact.Details(Convert.ToInt64(cboCustomer.SelectedItem.Value));
            clsContact.CommitAndDispose();

            txtCustomerContact.Text = clsDetails.ContactName;
            txtCustomerTelephoneNo.Text = clsDetails.TelephoneNo;
            lblTerms.Text = clsDetails.Terms.ToString("##0");
            lblModeOfterms.Text = clsDetails.ModeOfTerms.ToString("G");
            txtCustomerAddress.Text = clsDetails.Address;
        }
        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Data.Branch clsBranch = new Data.Branch();
            Data.BranchDetails clsDetails = clsBranch.Details(Convert.ToInt16(cboBranch.SelectedItem.Value));
            clsBranch.CommitAndDispose();

            txtBranchAddress.Text = clsDetails.Address;
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            Contacts clsContact = new Contacts();
            cboCustomer.DataTextField = "ContactName";
            cboCustomer.DataValueField = "ContactID";
            cboCustomer.DataSource = clsContact.CustomersDataTable(null, 0, "ContactCode", SortOption.Ascending).DefaultView;
            cboCustomer.DataBind();

            Branch clsBranch = new Branch(clsContact.Connection, clsContact.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranch.DataBind();
            clsContact.CommitAndDispose();

            cboCustomer.SelectedIndex = 0;
            cboCustomer_SelectedIndexChanged(null, null);
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));
            cboBranch_SelectedIndexChanged(null, null);
        }
        private void LoadRecord()
        {
            Common Common = new Common();
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["retid"], Session.SessionID));
            CreditMemos clsCreditMemos = new CreditMemos();
            CreditMemoDetails clsDetails = clsCreditMemos.Details(iID);
            clsCreditMemos.CommitAndDispose();

            lblCreditMemoID.Text = clsDetails.CreditMemoID.ToString();
            lnkReturnNo.Text = clsDetails.CNNo;
            lnkReturnNo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&retid=" + Common.Encrypt(clsDetails.CreditMemoID.ToString(), Session.SessionID);

            lblReturnDate.Text = clsDetails.CNDate.ToString("yyyy-MM-dd HH:mm:ss");
            txtRequiredReturnDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
            cboCustomer.SelectedIndex = cboCustomer.Items.IndexOf(cboCustomer.Items.FindByValue(clsDetails.CustomerID.ToString()));
            txtCustomerContact.Text = clsDetails.CustomerContact;
            txtCustomerTelephoneNo.Text = clsDetails.CustomerTelephoneNo;
            lblTerms.Text = clsDetails.CustomerTerms.ToString("##0");
            switch (clsDetails.CustomerModeOfTerms)
            {
                case 0:
                    lblModeOfterms.Text = "Days";
                    break;
                case 1:
                    lblModeOfterms.Text = "Months";
                    break;
                case 2:
                    lblModeOfterms.Text = "Years";
                    break;
            }
            txtCustomerAddress.Text = clsDetails.CustomerAddress;
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(clsDetails.BranchID.ToString()));
            txtBranchAddress.Text = clsDetails.BranchAddress;
            txtRemarks.Text = clsDetails.Remarks;
        }
        private void SaveRecord()
        {
            CreditMemoDetails clsDetails = new CreditMemoDetails();

            clsDetails.CreditMemoID = Convert.ToInt64(lblCreditMemoID.Text);
            clsDetails.CNNo = lnkReturnNo.Text;
            clsDetails.CNDate = Convert.ToDateTime(lblReturnDate.Text);
            clsDetails.CustomerID = Convert.ToInt64(cboCustomer.SelectedItem.Value);
            clsDetails.CustomerCode = cboCustomer.SelectedItem.Text;
            clsDetails.CustomerContact = txtCustomerContact.Text;
            clsDetails.CustomerAddress = txtCustomerAddress.Text;
            clsDetails.CustomerTelephoneNo = txtCustomerTelephoneNo.Text;
            switch (lblModeOfterms.Text)
            {
                case "Days":
                    clsDetails.CustomerModeOfTerms = 0;
                    break;
                case "Months":
                    clsDetails.CustomerModeOfTerms = 1;
                    break;
                case "Years":
                    clsDetails.CustomerModeOfTerms = 2;
                    break;
            }
            clsDetails.RequiredPostingDate = Convert.ToDateTime(txtRequiredReturnDate.Text);
            clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
            clsDetails.SellerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.SellerName = Session["Name"].ToString();
            clsDetails.CreditMemoStatus = CreditMemoStatus.Open;
            clsDetails.Remarks = txtRemarks.Text;

            CreditMemos clsCreditMemos = new CreditMemos();
            clsCreditMemos.Update(clsDetails);
            clsCreditMemos.CommitAndDispose();
        }

        #endregion

    }
}
