namespace AceSoft.RetailPlus.Security._AccessGroup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Update : System.Web.UI.UserControl
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

		private void SaveRecord()
		{
			AccessGroup clsAccessGroup = new AccessGroup();
			AccessGroupDetails clsDetails = new AccessGroupDetails();

			clsDetails.GroupID = Convert.ToInt16(lblGroupID.Text);
			clsDetails.GroupName = txtGroupName.Text;
			clsDetails.Remarks = txtRemarks.Text;

			clsAccessGroup.Update(clsDetails);
			clsAccessGroup.CommitAndDispose();
		}
		private void LoadOptions()
		{
						
		}
		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
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
