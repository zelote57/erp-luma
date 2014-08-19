namespace AceSoft.RetailPlus.SalesAndReceivables._SO
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
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            SaveRecord();
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

            txtCustomerContact.ToolTip = clsDetails.ContactCode;
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
            DataClass clsDataClass = new DataClass();

            Contacts clsContact = new Contacts();
            cboCustomer.DataTextField = "ContactName";
            cboCustomer.DataValueField = "ContactID";
            cboCustomer.DataSource = clsContact.CustomersDataTable(null).DefaultView;
            cboCustomer.DataBind();
            clsContact.CommitAndDispose();
            cboCustomer.SelectedIndex = 0;
            cboCustomer_SelectedIndexChanged(null, null);

            Branch clsBranch = new Branch();
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            clsBranch.CommitAndDispose();
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));
            cboBranch_SelectedIndexChanged(null, null);
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["soid"], Session.SessionID));
            SO clsSO = new SO();
            SODetails clsDetails = clsSO.Details(iID);
            clsSO.CommitAndDispose();

            lblSOID.Text = clsDetails.SOID.ToString();
            lblSONo.Text = clsDetails.SONo;
            lblSODate.Text = clsDetails.SODate.ToString("yyyy-MM-dd HH:mm:ss");
            txtRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
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
            SODetails clsDetails = new SODetails();

            clsDetails.SOID = Convert.ToInt64(lblSOID.Text);
            clsDetails.SONo = lblSONo.Text;
            clsDetails.SODate = Convert.ToDateTime(lblSODate.Text);
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
            clsDetails.RequiredDeliveryDate = Convert.ToDateTime(txtRequiredDeliveryDate.Text);
            clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
            clsDetails.SellerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.SellerName = Session["Name"].ToString();
            clsDetails.Status = SOStatus.Open;
            clsDetails.Remarks = txtRemarks.Text;

            SO clsSO = new SO();
            clsSO.Update(clsDetails);
            clsSO.CommitAndDispose();
        }

        #endregion

	}
}
