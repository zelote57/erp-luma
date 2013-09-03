namespace AceSoft.RetailPlus.GeneralLedger._AccountSummary
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
		protected System.Web.UI.WebControls.DropDownList cboStatus;
		protected System.Web.UI.WebControls.TextBox txtProductAttributeName;

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();			
				}
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
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		#region Web Control Methods

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


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            AccountClassification clsAccountClassification = new AccountClassification();
            DataClass clsDataClass = new DataClass();

            cboAccountClassification.DataTextField = "AccountClassificationName";
            cboAccountClassification.DataValueField = "AccountClassificationID";
            cboAccountClassification.DataSource = clsDataClass.DataReaderToDataTable(clsAccountClassification.List("AccountClassificationID", SortOption.Ascending)).DefaultView;
            cboAccountClassification.DataBind();
            cboAccountClassification.SelectedIndex = 1;
            clsAccountClassification.CommitAndDispose();
		}

		private Int32 SaveRecord()
		{
			AccountSummaryDetails clsDetails = new AccountSummaryDetails();

            clsDetails.AccountClassificationID = Convert.ToInt16(cboAccountClassification.SelectedItem.Value);
			clsDetails.AccountSummaryCode = txtAccountSummaryCode.Text;
			clsDetails.AccountSummaryName = txtAccountSummaryName.Text;
			
			AccountSummary clsAccountSummary = new AccountSummary();
			Int32 id = clsAccountSummary.Insert(clsDetails);
			clsAccountSummary.CommitAndDispose();

			return id;
		}


		#endregion
	}
}
