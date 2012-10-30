namespace AceSoft.RetailPlus.SalesAndReceivables._Returns
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Insert : System.Web.UI.UserControl
	{
        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                lblReferrer.Text = Request.UrlReferrer.ToString();
                if (Visible)
                    LoadOptions();
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

        protected void cmdSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Int64 RetID = SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&retid=" + Common.Encrypt(RetID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void imgSaveAddItem_Click(object sender, System.EventArgs e)
        {
            Int64 RetID = SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&retid=" + Common.Encrypt(RetID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }
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
        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Data.Branch clsBranch = new Data.Branch();
            Data.BranchDetails clsDetails = clsBranch.Details(Convert.ToInt16(cboBranch.SelectedItem.Value));
            clsBranch.CommitAndDispose();

            txtBranchAddress.Text = clsDetails.Address;
        }
        protected void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Data.Contact clsContact = new Data.Contact();
            Data.ContactDetails clsDetails = clsContact.Details(Convert.ToInt64(cboCustomer.SelectedItem.Value));
            clsContact.CommitAndDispose();

            txtCustomerContact.Text = clsDetails.ContactName;
            txtCustomerTelephoneNo.Text = clsDetails.TelephoneNo;
            lblTerms.Text = clsDetails.Terms.ToString("##0");
            lblModeOfterms.Text = clsDetails.ModeOfTerms.ToString("G");
            txtCustomerAddress.Text = clsDetails.Address;
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            Contact clsContact = new Contact();
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

            NewTransaction();
        }
        private void NewTransaction()
        {
            lblReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtRequiredReturnDate.Text = Convert.ToDateTime(lblReturnDate.Text).AddDays(30).ToString("yyyy-MM-dd");

            lblReturnNo.Text = "WILL BE ASSIGNED AFTER SAVING";

        }
        private long SaveRecord()
        {
            SOReturns clsSOReturns = new SOReturns();
            clsSOReturns.GetConnection();
            lblReturnNo.Text = Constants.SALES_RETURN_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsSOReturns.LastTransactionNo();

            SOReturnDetails clsDetails = new SOReturnDetails();

            clsDetails.CNNo = lblReturnNo.Text;
            clsDetails.CNDate = Convert.ToDateTime(lblReturnDate.Text);
            clsDetails.CustomerID = Convert.ToInt64(cboCustomer.SelectedItem.Value);
            clsDetails.CustomerCode = cboCustomer.SelectedItem.Text;
            clsDetails.CustomerContact = txtCustomerContact.Text;
            clsDetails.CustomerAddress = txtCustomerAddress.Text;
            clsDetails.CustomerTelephoneNo = txtCustomerTelephoneNo.Text;
            clsDetails.CustomerTerms = Convert.ToInt32(lblTerms.Text);
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
            clsDetails.ReturnStatus = SOReturnStatus.Open;
            clsDetails.Remarks = txtRemarks.Text;

            long id = clsSOReturns.Insert(clsDetails);
            clsSOReturns.CommitAndDispose();

            return id;
        }

        #endregion

    }
}
