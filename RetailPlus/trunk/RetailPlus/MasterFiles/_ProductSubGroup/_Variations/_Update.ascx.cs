namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._Variations
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
		
		#region Web Form Methofs

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadOptions();	
					LoadRecord();	
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
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&subgroupid=" + Common.Encrypt(lblProductSubGroupID.Text,Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&subgroupid=" + Common.Encrypt(lblProductSubGroupID.Text,Session.SessionID);			
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
        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx" + stParam);
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			lblProductSubGroupID.Text = Common.Decrypt((string)Request.QueryString["subgroupid"],Session.SessionID);
			lblProductSubGroupVariationID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			Variation clsVariation = new Variation();
			string VariationType = clsVariation.Details( Convert.ToInt32(lblProductSubGroupVariationID.Text)).VariationType;
			clsVariation.CommitAndDispose();

			ProductSubGroupVariation clsProductSubGroupVariation = new ProductSubGroupVariation();
			
			cboVariationType.DataTextField = "VariationType";
			cboVariationType.DataValueField = "VariationID";
			cboVariationType.DataSource =  clsDataClass.DataReaderToDataTable(clsProductSubGroupVariation.AvailableVariations(Convert.ToInt32(lblProductSubGroupID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			cboVariationType.DataBind();
			cboVariationType.Items.Add(new ListItem(VariationType, lblProductSubGroupVariationID.Text));
			cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;

			clsProductSubGroupVariation.CommitAndDispose();		
		}
		private void LoadRecord()
		{
			cboVariationType.SelectedIndex = cboVariationType.Items.IndexOf(cboVariationType.Items.FindByValue(lblProductSubGroupVariationID.Text));  
		}
		private void SaveRecord()
		{
			ProductSubGroupVariation clsProductSubGroupVariation = new ProductSubGroupVariation();
			ProductSubGroupVariationDetails clsDetails = new ProductSubGroupVariationDetails();

			clsDetails.SubGroupID = Convert.ToInt64(lblProductSubGroupID.Text);
			clsDetails.VariationID = Convert.ToInt32(cboVariationType.SelectedItem.Value);
			
			clsProductSubGroupVariation.Update(clsDetails, Convert.ToInt32(lblProductSubGroupVariationID.Text));
			
			clsProductSubGroupVariation.CommitAndDispose();
		}

		#endregion
}
}
