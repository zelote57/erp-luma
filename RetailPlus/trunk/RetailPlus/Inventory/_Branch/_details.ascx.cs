namespace AceSoft.RetailPlus.Inventory._Branch
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
			Int16 iID = Convert.ToInt16(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
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

		#endregion
    }
}
