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

            bool boShowAdminFiles = false;
            bool boShowActionBar = false;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CompanyInfo); 
			lnkCompany.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkCompany.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowAdminFiles) boShowAdminFiles = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Terminal); 
			lnkTerminal.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkTerminal.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowAdminFiles) boShowAdminFiles = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessGroups); 
			lnkAccessGroup.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkAccessGroup.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowAdminFiles) boShowAdminFiles = true;

			lnkAccessGroupAdd.Visible = clsDetails.Write;
            if (!clsDetails.Write) divlnkAccessGroupAdd.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Write && !boShowActionBar) boShowActionBar = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessUsers); 
			lnkAccessUser.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkAccessUser.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowAdminFiles) boShowAdminFiles = true;

			lnkAccessUserAdd.Visible = clsDetails.Write;
            if (!clsDetails.Write) divlnkAccessUserAdd.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Write && !boShowActionBar) boShowActionBar = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReportFormat); 
			lnkReceiptFormat.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkReceiptFormat.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowAdminFiles) boShowAdminFiles = true;

			lnkReceiptFormatEdit.Visible = clsDetails.Write;
            if (!clsDetails.Write) divlnkReceiptFormatEdit.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Write && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.RewardPointsSetup);
            lnkRewardPointSystem.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkRewardPointSystem.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Write && !boShowActionBar) boShowActionBar = true;

			clsAccessRights.CommitAndDispose();

            if (!boShowAdminFiles)
            {
                divlblAdminFiles.Style.Add("display", "none");
                divtblAdminFiles.Style.Add("display", "none");
            }

            if (!boShowActionBar)
            {
                divlblActionBar.Style.Add("display", "none");
            }
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
