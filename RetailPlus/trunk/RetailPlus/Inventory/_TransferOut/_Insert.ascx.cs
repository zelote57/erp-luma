namespace AceSoft.RetailPlus.Inventory._TransferOut
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	/// <summary>
	///		Summary description for __Insert.
	/// </summary>
	public partial  class __Insert : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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

        protected void imgSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Int64 TransferOutID = SaveRecord();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&transferoutid=" + Common.Encrypt(TransferOutID.ToString(), Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);
		}
        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
		{
			Int64 TransferOutID = SaveRecord();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&transferoutid=" + Common.Encrypt(TransferOutID.ToString(), Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);	
		}
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
		protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Data.Branch clsBranch = new Data.Branch();
			Data.BranchDetails clsDetails = clsBranch.Details(Convert.ToInt16(cboBranch.SelectedItem.Value));
			clsBranch.CommitAndDispose();
			
			txtBranchAddress.Text = clsDetails.Address;
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

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			Contacts clsContact = new Contacts();
			cboSupplier.DataTextField = "ContactName";
			cboSupplier.DataValueField = "ContactID";
			cboSupplier.DataSource = clsDataClass.DataReaderToDataTable(clsContact.Suppliers(null, 0, "ContactName", SortOption.Ascending)).DefaultView;
			cboSupplier.DataBind();
			
            Branch clsBranch = new Branch(clsContact.Connection, clsContact.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranch.DataBind();
            clsContact.CommitAndDispose();

            cboSupplier.SelectedIndex = 0;
            cboSupplier_SelectedIndexChanged(null, null);
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));
            cboBranch_SelectedIndexChanged(null, null);

			NewTransaction();
		}
		private void NewTransaction()
		{
			lblTransferOutDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = Convert.ToDateTime(lblTransferOutDate.Text).AddDays(30).ToString("yyyy-MM-dd");

			lblTransferOutNo.Text = "WILL BE ASSIGNED AFTER SAVING";
		}
		private Int64 SaveRecord()
		{
			TransferOut clsTransferOut = new TransferOut();
			clsTransferOut.GetConnection();
			lblTransferOutNo.Text = Constants.TRANSFER_OUT_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsTransferOut.LastTransactionNo();

			TransferOutDetails clsDetails = new TransferOutDetails();

			clsDetails.TransferOutNo = lblTransferOutNo.Text;
			clsDetails.TransferOutDate = Convert.ToDateTime(lblTransferOutDate.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
			clsDetails.SupplierCode = cboSupplier.SelectedItem.Text;
			clsDetails.SupplierContact = txtSupplierContact.Text;
			clsDetails.SupplierAddress = txtSupplierAddress.Text;
			clsDetails.SupplierTelephoneNo = txtSupplierTelephoneNo.Text;
            clsDetails.SupplierTerms = Convert.ToInt32(lblTerms.Text);
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
			clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
			clsDetails.TransferrerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.TransferrerName = Session["Name"].ToString();
			clsDetails.Status = TransferOutStatus.Open;
			clsDetails.Remarks = txtRemarks.Text;
			
			Int64 id = clsTransferOut.Insert(clsDetails);
			clsTransferOut.CommitAndDispose();

			return id;
		}

		#endregion

    }
}
