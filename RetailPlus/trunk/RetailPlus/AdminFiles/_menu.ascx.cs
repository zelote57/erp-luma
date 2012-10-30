namespace AceSoft.RetailPlus.Security
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

    public partial class __Menu : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ManageSecurity();

				lnkCompany.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Company/Default.aspx?task=" + Common.Encrypt("det",Session.SessionID);

				lnkTerminal.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Terminals/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkAccessGroup.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkAccessGroupAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessGroup/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);

				lnkAccessUser.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessUser/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkAccessUserAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessUser/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);

				lnkReceiptFormat.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Reports/_Format/Default.aspx?task=" + Common.Encrypt("det",Session.SessionID);
				lnkReceiptFormatEdit.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Reports/_Format/Default.aspx?task=" + Common.Encrypt("edit",Session.SessionID);

                lnkRewardPointSystem.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Terminals/Default.aspx?task=" + Common.Encrypt("updaterewards", Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CompanyInfo); 
			lnkCompany.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Terminal); 
			lnkTerminal.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessGroups); 
			lnkAccessGroup.Visible = clsDetails.Read; 
			lnkAccessGroupAdd.Visible = clsDetails.Write; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessUsers); 
			lnkAccessUser.Visible = clsDetails.Read; 
			lnkAccessUserAdd.Visible = clsDetails.Write; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReportFormat); 
			lnkReceiptFormat.Visible = clsDetails.Read; 
			lnkReceiptFormatEdit.Visible = clsDetails.Write; 

            clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.RewardPointsSetup);
            lnkRewardPointSystem.Visible = clsDetails.Read; 

			clsAccessRights.CommitAndDispose();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            //this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
