namespace AceSoft.RetailPlus.SalesAndReceivables._SO
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

        protected void imgSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Int64 SOID = SaveRecord();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&soid=" + Common.Encrypt(SOID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }

        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
        {
            Int64 SOID = SaveRecord();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&soid=" + Common.Encrypt(SOID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }

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

        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Data.Branch clsBranch = new Data.Branch();
            Data.BranchDetails clsDetails = clsBranch.Details(Convert.ToInt16(cboBranch.SelectedItem.Value));
            clsBranch.CommitAndDispose();

            txtBranchAddress.Text = clsDetails.Address;
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


        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            DataClass clsDataClass = new DataClass();

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

            NewTransaction();
        }

        private void NewTransaction()
        {
            lblSODate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtRequiredDeliveryDate.Text = Convert.ToDateTime(lblSODate.Text).AddDays(30).ToString("yyyy-MM-dd");

            lblSONo.Text = "WILL BE ASSIGNED AFTER SAVING";
        }

        private Int64 SaveRecord()
        {
            SO clsSO = new SO();
            clsSO.GetConnection();
            lblSONo.Text = Constants.SALES_ORDER_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsSO.LastTransactionNo();

            SODetails clsDetails = new SODetails();

            clsDetails.SONo = lblSONo.Text;
            clsDetails.SODate = Convert.ToDateTime(lblSODate.Text);
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
            clsDetails.RequiredDeliveryDate = Convert.ToDateTime(txtRequiredDeliveryDate.Text);
            clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
            clsDetails.SellerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.SellerName = Session["Name"].ToString();
            clsDetails.Status = SOStatus.Open;
            clsDetails.Remarks = txtRemarks.Text;

            Int64 id = clsSO.Insert(clsDetails);
            clsSO.CommitAndDispose();

            return id;
        }


        #endregion

	}
}
