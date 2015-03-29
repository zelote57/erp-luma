namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
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
			Int64 POID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&poid=" + Common.Encrypt(POID.ToString(),Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);
		}

        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
		{
			Int64 POID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&poid=" + Common.Encrypt(POID.ToString(),Session.SessionID) + "#itemsection";	
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

            // 23Mar2015 As per request by Sweetie
            txtSupplierTINNo.Text = clsDetails.TINNo;
            txtSupplierLTONo.Text = clsDetails.LTONo;
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
			
			Branch clsBranch = new Branch(clsContact.Connection, clsContact.Transaction);
			cboBranch.DataTextField = "BranchCode";
			cboBranch.DataValueField = "BranchID";
			cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
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
			lblPODate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = Convert.ToDateTime(lblPODate.Text).AddDays(30).ToString("yyyy-MM-dd");

			lblPONo.Text = "WILL BE ASSIGNED AFTER SAVING";
		}

		private Int64 SaveRecord()
		{
			PO clsPO = new PO();
			clsPO.GetConnection();
			lblPONo.Text = Constants.PURCHASE_ORDER_CODE + CompanyDetails.BECompanyCode + DateTime.Now.Year.ToString() + clsPO.LastTransactionNo();

			PODetails clsDetails = new PODetails();

			clsDetails.PONo = lblPONo.Text;
			clsDetails.PODate = Convert.ToDateTime(lblPODate.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
			clsDetails.SupplierCode = cboSupplier.SelectedItem.Text;
			clsDetails.SupplierContact = txtSupplierContact.Text;
			clsDetails.SupplierAddress = txtSupplierAddress.Text;
			clsDetails.SupplierTelephoneNo = txtSupplierTelephoneNo.Text;
            clsDetails.SupplierTerms = Convert.ToInt32(lblTerms.Text);
            clsDetails.SupplierTINNo = txtSupplierTINNo.Text;
            clsDetails.SupplierLTONo = txtSupplierLTONo.Text;
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
			
			Int64 id = clsPO.Insert(clsDetails);
			clsPO.CommitAndDispose();

			return id;
		}


		#endregion

    }
}
