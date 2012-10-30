namespace AceSoft.RetailPlus.GeneralLedger._AccountCategory
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();	
				}
			}
		}

		private void LoadOptions()
		{
			AccountSummary clsAccountSummary = new AccountSummary();
			DataClass clsDataClass = new DataClass();
			
			cboAccountSummary.DataTextField = "AccountSummaryName";
			cboAccountSummary.DataValueField = "AccountSummaryID";
			cboAccountSummary.DataSource = clsDataClass.DataReaderToDataTable(clsAccountSummary.List("AccountSummaryName",SortOption.Ascending)).DefaultView;
			cboAccountSummary.DataBind();
			cboAccountSummary.SelectedIndex = cboAccountSummary.Items.Count - 1;
			clsAccountSummary.CommitAndDispose();	
		}

		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			AccountCategory clsAccountCategory = new AccountCategory();
			AccountCategoryDetails clsDetails = clsAccountCategory.Details(iID);
			clsAccountCategory.CommitAndDispose();

			lblAccountCategoryID.Text = clsDetails.AccountCategoryID.ToString();
			cboAccountSummary.SelectedIndex = cboAccountSummary.Items.IndexOf( cboAccountSummary.Items.FindByValue(clsDetails.AccountSummaryID.ToString()));
			txtAccountCategoryCode.Text = clsDetails.AccountCategoryCode;
			txtAccountCategoryName.Text = clsDetails.AccountCategoryName;
		}

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
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		private void SaveRecord()
		{
			AccountCategoryDetails clsDetails = new AccountCategoryDetails();

			clsDetails.AccountCategoryID = Convert.ToInt16(lblAccountCategoryID.Text);
			clsDetails.AccountSummaryID = Convert.ToInt32(cboAccountSummary.SelectedItem.Value);
			clsDetails.AccountCategoryCode = txtAccountCategoryCode.Text;
			clsDetails.AccountCategoryName = txtAccountCategoryName.Text;
			
			AccountCategory clsAccountCategory = new AccountCategory();
			clsAccountCategory.Update(clsDetails);
			clsAccountCategory.CommitAndDispose();
		}

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

	}
}
