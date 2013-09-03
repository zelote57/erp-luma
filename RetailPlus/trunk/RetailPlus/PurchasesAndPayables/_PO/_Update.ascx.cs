namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
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
		protected void cboSupplier_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Data.Contacts clsContact = new Data.Contacts();
			Data.ContactDetails clsDetails = clsContact.Details(Convert.ToInt64(cboSupplier.SelectedItem.Value));
			clsContact.CommitAndDispose();
			
			txtSupplierContact.Text = clsDetails.ContactName;
			txtSupplierTelephoneNo.Text = clsDetails.TelephoneNo;
			lblTerms.Text = clsDetails.Terms.ToString("##0");
            lblModeOfterms.Text = clsDetails.ModeOfTerms.ToString("G");
			txtSupplierAddress.Text = clsDetails.Address;
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
            cboSupplier.DataTextField = "ContactName";
            cboSupplier.DataValueField = "ContactID";
            cboSupplier.DataSource = clsContact.SuppliersAsDataTable(null, 0, "ContactName", SortOption.Ascending).DefaultView;
			cboSupplier.DataBind();
			clsContact.CommitAndDispose();
			cboSupplier.SelectedIndex = 0;
			cboSupplier_SelectedIndexChanged(null, null);

			Branch clsBranch = new Branch();
			cboBranch.DataTextField = "BranchCode";
			cboBranch.DataValueField = "BranchID";
			cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
			cboBranch.DataBind();
			clsBranch.CommitAndDispose();
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));
			cboBranch_SelectedIndexChanged(null, null);
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["poid"],Session.SessionID));
			PO clsPO = new PO();
			PODetails clsDetails = clsPO.Details(iID);
			clsPO.CommitAndDispose();

			lblPOID.Text = clsDetails.POID.ToString();
			lblPONo.Text = clsDetails.PONo;
			lblPODate.Text = clsDetails.PODate.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            txtRID.Text = clsDetails.RID.ToString();
			cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(clsDetails.SupplierID.ToString()));
			txtSupplierContact.Text = clsDetails.SupplierContact;
			txtSupplierTelephoneNo.Text = clsDetails.SupplierTelephoneNo;
			lblTerms.Text = clsDetails.SupplierTerms.ToString("##0");
			switch (clsDetails.SupplierModeOfTerms)
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
			txtSupplierAddress.Text = clsDetails.SupplierAddress;
			cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(clsDetails.BranchID.ToString()));
			txtBranchAddress.Text = clsDetails.BranchAddress;
			txtRemarks.Text = clsDetails.Remarks;
		}
		private void SaveRecord()
		{
			PODetails clsDetails = new PODetails();

			clsDetails.POID = Convert.ToInt64(lblPOID.Text);
			clsDetails.PONo = lblPONo.Text;
			clsDetails.PODate = Convert.ToDateTime(lblPODate.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
			clsDetails.SupplierCode = cboSupplier.SelectedItem.Text;
			clsDetails.SupplierContact = txtSupplierContact.Text;
			clsDetails.SupplierAddress = txtSupplierAddress.Text;
			clsDetails.SupplierTelephoneNo = txtSupplierTelephoneNo.Text;
			switch (lblModeOfterms.Text)
			{
				case "Days":
					clsDetails.SupplierModeOfTerms = 0;
					break;
				case "Months":
					clsDetails.SupplierModeOfTerms = 1;
					break;
				case "Years":
					clsDetails.SupplierModeOfTerms = 2;
					break;
			}
			clsDetails.RequiredDeliveryDate = Convert.ToDateTime(txtRequiredDeliveryDate.Text);
            clsDetails.RID = Convert.ToInt64(txtRID.Text);
			clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
			clsDetails.PurchaserID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.PurchaserName = Session["Name"].ToString();
			clsDetails.Status = POStatus.Open;
			clsDetails.Remarks = txtRemarks.Text;

			PO clsPO = new PO();
			clsPO.Update(clsDetails);
			clsPO.CommitAndDispose();
		}

		#endregion

    }
}
