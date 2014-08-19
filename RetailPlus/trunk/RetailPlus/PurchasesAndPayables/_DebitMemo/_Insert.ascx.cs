namespace AceSoft.RetailPlus.PurchasesAndPayables._DebitMemo
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
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Int64 MemoID = SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&memoid=" + Common.Encrypt(MemoID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
        {
            Int64 MemoID = SaveRecord();
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&memoid=" + Common.Encrypt(MemoID.ToString(), Session.SessionID) + "#itemsection";
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			
			Common Common = new Common();
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
			lblMemoDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredPostingDate.Text = Convert.ToDateTime(lblMemoDate.Text).AddDays(30).ToString("yyyy-MM-dd");

			lblMemoNo.Text = "WILL BE ASSIGNED AFTER SAVING";
		}
		private Int64 SaveRecord()
		{
			DebitMemos clsDebitMemos = new DebitMemos();
			clsDebitMemos.GetConnection();
			lblMemoNo.Text = Constants.PURCHASE_DEBITMEMO_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsDebitMemos.LastTransactionNo();

			DebitMemoDetails clsDetails = new DebitMemoDetails();

			clsDetails.MemoNo = lblMemoNo.Text;
			clsDetails.MemoDate = Convert.ToDateTime(lblMemoDate.Text);
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
			clsDetails.RequiredPostingDate = Convert.ToDateTime(txtRequiredPostingDate.Text);
			clsDetails.BranchID = Convert.ToInt16(cboBranch.SelectedItem.Value);
			clsDetails.PurchaserID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.PurchaserName = Session["Name"].ToString();
			clsDetails.DebitMemoStatus = DebitMemoStatus.Open;
			clsDetails.Remarks = txtRemarks.Text;
			
			Int64 id = clsDebitMemos.Insert(clsDetails);
			clsDebitMemos.CommitAndDispose();

			return id;
		}

		#endregion

    }
}
