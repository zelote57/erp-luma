using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Credits
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

                lnkCardTypes.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_CardType/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                
                lnkCreditorsWithoutGuarantors.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkCreditorsWithoutGurantorPurchases.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("purchases", Session.SessionID);
                lnkCreditorsWithoutGuarantorPayments.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("payments", Session.SessionID);
                lnkCreditorsLedgerSummary.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("ledger", Session.SessionID);
                lnkSummarizedStatistics.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("stat", Session.SessionID);

                lnkCreditorsWithGuarantors.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkCreditorsWithGurantorPurchases.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("purchases", Session.SessionID);
                lnkCreditorsWithGuarantorPayments.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("payments", Session.SessionID);
                lnkGuarantorsLedger.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("ledger", Session.SessionID);
                lnkSummarizedStatisticsWG.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID) + "&reporttype=" + Common.Encrypt("stat", Session.SessionID);

                lnkChangeCreditType.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("changecardtype", Session.SessionID) + "&id=" + Common.Encrypt("0", Session.SessionID);
                lnkChangeCreditTypeWG.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("changecardtype", Session.SessionID) + "&id=" + Common.Encrypt("0", Session.SessionID);
                lnkChangeGuarantor.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("changeguarantor", Session.SessionID) + "&id=" + Common.Encrypt("0", Session.SessionID);
            }
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InternalCreditCardSetup);
            lnkCardTypes.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithoutGuarantor);
            lnkCreditorsWithoutGuarantors.Visible = clsDetails.Read;
            lnkChangeCreditType.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithoutGuarantorPurchases);
            lnkCreditorsWithoutGurantorPurchases.Visible = clsDetails.Read;
            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithoutGuarantorPayments);
            lnkCreditorsWithoutGuarantorPayments.Visible = clsDetails.Read;
            lnkSummarizedStatistics.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsLedgerSummary);
            lnkGuarantorsLedger.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithGuarantor);
            lnkCreditorsWithGuarantors.Visible = clsDetails.Read;
            lnkChangeCreditTypeWG.Visible = clsDetails.Read;
            lnkChangeCreditType.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithGuarantorPurchases);
            lnkCreditorsWithGurantorPurchases.Visible = clsDetails.Read;
            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditorsWithGuarantorPayments);
            lnkCreditorsWithGuarantorPayments.Visible = clsDetails.Read;
            lnkSummarizedStatisticsWG.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.GuarantorsLedgerSummary);
            lnkGuarantorsLedger.Visible = clsDetails.Read;

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
