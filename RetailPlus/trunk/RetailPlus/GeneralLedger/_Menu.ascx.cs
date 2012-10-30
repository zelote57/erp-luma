using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.GeneralLedger
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

                lnkBankDeposits.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_BankDeposits/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkWriteCheque.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Bank/Default.aspx?task=" + Common.Encrypt("writecheques", Session.SessionID);
                lnkFundTransfers.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_FundTransfers/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkReconcileAccounts.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("reconcileaccounts", Session.SessionID);

				lnkAccountSummaries.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_AccountSummary/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkAccountCategory.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_AccountCategory/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkChartOfAccounts.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

                lnkGeneralJournals.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Payments/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
				lnkPaymentJournals.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Payments/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkPostingDates.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Setup/Default.aspx?task=" + Common.Encrypt("postingdates",Session.SessionID);
                lnkBankAccounts.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Bank/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkLinkAccountsProduct.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Setup/Default.aspx?task=" + Common.Encrypt("linkaccountsprod", Session.SessionID);
                lnkLinkAccountsAP.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Setup/Default.aspx?task=" + Common.Encrypt("linkaccountsap", Session.SessionID);

                lnkBalanceSheetReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("balancesheetreport", Session.SessionID);
                lnkChartOfAccountsReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("chartofaccountsreport", Session.SessionID);
                lnkGeneralLedgerReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("chartofaccountsreport", Session.SessionID);
                lnkGeneralJournalsReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID);
                lnkPaymentJournalReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID);
                lnkGeneralLedgerReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx?task=" + Common.Encrypt("glreport", Session.SessionID);
                lnkProfitAndLossReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID);
                lnkTrialBalanceReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/Default.aspx?task=" + Common.Encrypt("reports", Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.BankDeposits);
            lnkBankDeposits.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.WriteCheques);
            lnkWriteCheque.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.FundTransfers);
            lnkFundTransfers.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ReconcileAccounts);
            lnkReconcileAccounts.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccountSummary); 
			lnkAccountSummaries.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccountCategory); 
			lnkAccountCategory.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ChartOfAccounts); 
			lnkChartOfAccounts.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PaymentJournals);
            lnkGeneralJournals.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PaymentJournals); 
			lnkPaymentJournals.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PostingDates); 
			lnkPostingDates.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Banks);
            lnkBankAccounts.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.APLinkConfig);
            lnkLinkAccountsProduct.Visible = clsDetails.Read;
            lnkLinkAccountsAP.Visible = clsDetails.Read;

            //lnkBalanceSheetReport.Visible = false;
            //lnkChartOfAccountsReport.Visible = true;
            //lnkGeneralLedgerReport.Visible = false;
            lnkGeneralJournalsReport.Visible = false;
            lnkPaymentJournalReport.Visible = false;
            lnkProfitAndLossReport.Visible = false;
            lnkTrialBalanceReport.Visible = false;

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
