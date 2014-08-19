namespace AceSoft.RetailPlus.MasterFiles._Product._Group._Variations
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Details : System.Web.UI.UserControl
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
		}
		private void InitializeComponent()
		{

		}

		#endregion

		#region Web Control Methods

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx" + stParam);
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			lblProductGroupID.Text = Common.Decrypt((string)Request.QueryString["groupid"],Session.SessionID);
			lblProductGroupVariationID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			Variation clsVariation = new Variation();
			string VariationType = clsVariation.Details( Convert.ToInt32(lblProductGroupVariationID.Text)).VariationType;
			clsVariation.CommitAndDispose();

			ProductGroupVariations clsProductGroupVariation = new ProductGroupVariations();
			
			cboVariationType.DataTextField = "VariationType";
			cboVariationType.DataValueField = "VariationID";
			cboVariationType.DataSource =  clsDataClass.DataReaderToDataTable(clsProductGroupVariation.AvailableVariations(Convert.ToInt32(lblProductGroupID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			cboVariationType.DataBind();
			cboVariationType.Items.Add(new ListItem(VariationType, lblProductGroupVariationID.Text));
			cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;

			clsProductGroupVariation.CommitAndDispose();		
		}
		private void LoadRecord()
		{
			cboVariationType.SelectedIndex = cboVariationType.Items.IndexOf(cboVariationType.Items.FindByValue(lblProductGroupVariationID.Text));  
		}

		#endregion

    }
}
