namespace AceSoft.RetailPlus.Inventory._Branch
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
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
			Branch clsBranch = new Branch();
			BranchDetails clsDetails = clsBranch.Details(iID);
			clsBranch.CommitAndDispose();

			lblBranchID.Text = Convert.ToString(clsDetails.BranchID);
			txtBranchCode.Text = clsDetails.BranchCode;
			txtBranchName.Text = clsDetails.BranchName;
			txtDBIP.Text = clsDetails.DBIP;
			txtDBPort.Text = clsDetails.DBPort;
			txtAddress.Text = clsDetails.Address;
			txtRemarks.Text = clsDetails.Remarks;
		}
		private void SaveRecord()
		{
			Branch clsBranch = new Branch();
			BranchDetails clsDetails = new BranchDetails();

			clsDetails.BranchID = Convert.ToInt32(lblBranchID.Text);
			clsDetails.BranchCode = txtBranchCode.Text;
			clsDetails.BranchName = txtBranchName.Text;
			clsDetails.DBIP = txtDBIP.Text;
			clsDetails.DBPort = txtDBPort.Text;
			clsDetails.Address = txtAddress.Text;
			clsDetails.Remarks = txtRemarks.Text;

			clsBranch.Update(clsDetails);
			clsBranch.CommitAndDispose();
		}

		#endregion
    }
}
