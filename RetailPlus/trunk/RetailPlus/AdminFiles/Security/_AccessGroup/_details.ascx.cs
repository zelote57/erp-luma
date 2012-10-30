namespace AceSoft.RetailPlus.Security._AccessGroup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Details : System.Web.UI.UserControl
	{

		#region Web Form Methods

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

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdBack_Click(object sender, System.EventArgs e)
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
			int iID = int.Parse(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			AccessGroup clsAccessGroup = new AccessGroup();
			AccessGroupDetails clsDetails = clsAccessGroup.Details(iID);
			clsAccessGroup.CommitAndDispose();

			lblGroupID.Text = clsDetails.GroupID.ToString();
			txtGroupName.Text = clsDetails.GroupName;
			txtRemarks.Text = clsDetails.Remarks;
		}

		#endregion
}
}
