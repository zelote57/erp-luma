namespace AceSoft.RetailPlus.MasterFiles._Variation
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

		#region Web Form Methods

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

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
						
		}
		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Variation clsVariation = new Variation();
			VariationDetails clsDetails = clsVariation.Details(iID);
			clsVariation.CommitAndDispose();

			lblVariationID.Text = clsDetails.VariationID.ToString();
			txtVariationCode.Text = clsDetails.VariationCode;
			txtVariationType.Text = clsDetails.VariationType;
		}
		private void SaveRecord()
		{
			Variation clsVariation = new Variation();
			VariationDetails clsDetails = new VariationDetails();

			clsDetails.VariationID = Convert.ToInt16(lblVariationID.Text);
			clsDetails.VariationCode = txtVariationCode.Text;
			clsDetails.VariationType = txtVariationType.Text;

			clsVariation.Update(clsDetails);
			clsVariation.CommitAndDispose();
		}

		#endregion

    }
}
