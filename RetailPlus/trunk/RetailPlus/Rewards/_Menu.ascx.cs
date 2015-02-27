using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Rewards
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Menu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ManageSecurity();

                lnkMembers.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/_Members/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                
                lnkCheckRewardPoints.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/_Members/Default.aspx?task=" + Common.Encrypt("listwithrewards", Session.SessionID);
                lnkRewardsReddem.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/Default.aspx?task=" + Common.Encrypt("redeemrewards", Session.SessionID);

                lnkRewardsMovement.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/Default.aspx?task=" + Common.Encrypt(ReportTypes.RewardsHistory, Session.SessionID);
                lnkRewardsSummary.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/Default.aspx?task=" + Common.Encrypt(ReportTypes.RewardsSummary, Session.SessionID);
                lnkRewardsSummaryStatistics.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/Default.aspx?task=" + Common.Encrypt(ReportTypes.RewardsSummaryStatistics, Session.SessionID);
            }
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.RewardPointsSetup); 
			lnkMembers.Visible = clsDetails.Read;
            lnkRewardsMovement.Visible = clsDetails.Read;
            lnkCheckRewardPoints.Visible = clsDetails.Read;
            lnkRewardsSummary.Visible = clsDetails.Read;
            lnkRewardsSummaryStatistics.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.RewardPointsReedemption); 
            lnkRewardsReddem.Visible = clsDetails.Read;

            clsAccessRights.CommitAndDispose();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
