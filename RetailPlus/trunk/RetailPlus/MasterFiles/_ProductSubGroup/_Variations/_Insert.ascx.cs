namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._Variations
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

		#region Web Form Methods

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

			ProductSubGroupVariation clsVariation = new ProductSubGroupVariation();
			
			cboVariationType.DataTextField = "VariationType";
			cboVariationType.DataValueField = "VariationID";
			cboVariationType.DataSource = clsDataClass.DataReaderToDataTable(clsVariation.AvailableVariations(Convert.ToInt32(lblProductSubGroupID.Text), "VariationType",SortOption.Ascending)).DefaultView;
			cboVariationType.DataBind();
			cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;

			clsVariation.CommitAndDispose();	
		}
		private Int32 SaveRecord()
		{
			ProductSubGroupVariation clsProdSubVariation = new ProductSubGroupVariation();
			ProductSubGroupVariationDetails clsDetails = new ProductSubGroupVariationDetails();

			clsDetails.SubGroupID = Convert.ToInt16(lblProductSubGroupID.Text);
			clsDetails.VariationID = Convert.ToInt16(cboVariationType.SelectedItem.Value);
			clsDetails.VariationType = cboVariationType.SelectedItem.Text;

			int id = clsProdSubVariation.Insert(clsDetails);
			
			clsProdSubVariation.CommitAndDispose();

			return id;
		}


		#endregion

    }
}
