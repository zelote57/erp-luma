namespace AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts
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
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();
				}
			}
		}

		private void LoadOptions()
		{
			AccountCategory clsAccountCategory = new AccountCategory();
			DataClass clsDataClass = new DataClass();
			
			cboAccountCategory.DataTextField = "AccountCategoryName";
            cboAccountCategory.DataValueField = "AccountCategoryID";
			cboAccountCategory.DataSource = clsDataClass.DataReaderToDataTable(clsAccountCategory.List("AccountCategoryName",SortOption.Ascending)).DefaultView;
			cboAccountCategory.DataBind();
			cboAccountCategory.SelectedIndex = cboAccountCategory.Items.Count - 1;
			clsAccountCategory.CommitAndDispose();	
		}

		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ChartOfAccount clsChartOfAccount = new ChartOfAccount();
			ChartOfAccountDetails clsDetails = clsChartOfAccount.Details(iID);
			clsChartOfAccount.CommitAndDispose();

			lblAccountID.Text = clsDetails.ChartOfAccountID.ToString();
			cboAccountCategory.SelectedIndex = cboAccountCategory.Items.IndexOf( cboAccountCategory.Items.FindByValue(clsDetails.AccountCategoryID.ToString()));
			txtAccountCode.Text = clsDetails.ChartOfAccountCode;
			txtAccountName.Text = clsDetails.ChartOfAccountName;
            txtDebit.Text = clsDetails.Debit.ToString("###0.#0");
            txtCredit.Text = clsDetails.Credit.ToString("###0.#0");
		}

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

		private void SaveRecord()
		{
			ChartOfAccountDetails clsDetails = new ChartOfAccountDetails();

			clsDetails.ChartOfAccountID = Convert.ToInt16(lblAccountID.Text);
			clsDetails.AccountCategoryID = Convert.ToInt32(cboAccountCategory.SelectedItem.Value);
			clsDetails.ChartOfAccountCode = txtAccountCode.Text;
			clsDetails.ChartOfAccountName = txtAccountName.Text;
            clsDetails.Debit = Convert.ToDecimal(txtDebit.Text);
            clsDetails.Credit = Convert.ToDecimal(txtCredit.Text);

			ChartOfAccount clsChartOfAccount = new ChartOfAccount();
			clsChartOfAccount.Update(clsDetails);
			clsChartOfAccount.CommitAndDispose();
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

    }
}
