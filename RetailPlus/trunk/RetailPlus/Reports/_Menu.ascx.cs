using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AceSoft.RetailPlus.Reports
{
	public partial  class __Menu : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					ManageSecurity();

					lnkProducts.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("products",Session.SessionID);
					lnkProductHistory.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("producthistory",Session.SessionID);
					lnkInventory.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("inventory",Session.SessionID);
                    //lnkExpiredInventory.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("expiredinventory",Session.SessionID);
                    //lnkReorder.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("itemsforreorder",Session.SessionID);
                    //lnkOverStock.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("overstock",Session.SessionID);
				
                    //lnkProductPriceHistory.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("pricehistory", Session.SessionID);
					lnkTransaction.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("transaction",Session.SessionID);
					lnkStockTransaction.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("stocktransaction",Session.SessionID);
					lnkTerminalReports.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("terminalreport",Session.SessionID);
					lnkContacts.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("contacts",Session.SessionID);

					lnkCustomerCredit.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("customercredit",Session.SessionID);
                    //lnkCustomersWithCreditReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("customerwithcredit",Session.SessionID);
                    //lnkMostSalableItems.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("mostsalableitems",Session.SessionID);
                    //lnkLeastSalableItems.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("leastsalableitems",Session.SessionID);
					lnkDatedReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("datedreport",Session.SessionID);
                    lnkManagementReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("managementreport", Session.SessionID);
                    lnkAnalyticsReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("analyticsreport", Session.SessionID);
                    lnkeSalesReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("esalesreport", Session.SessionID);

                    //lnkSalesReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("salesreport",Session.SessionID);

                    //lnkSalesPerDay.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("salesperday",Session.SessionID);
                    //lnkSalesPerHour.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("salesperhour", Session.SessionID);

					lnkLoginLogoutReport.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("loginlogoutreport",Session.SessionID);

					lnkPurchaseAnalysis.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("purchaseanalysis",Session.SessionID);
                    lnkAgentsCommision.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("agentscommision", Session.SessionID);
                    lnkAgentsSales.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("agentssales", Session.SessionID);
				}
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			Security.AccessRights clsAccessRights = new Security.AccessRights(); 
			Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductsListReport); 
			lnkProducts.Visible = clsDetails.Read;

            //if (clsDetails.Read || clsAccessRights.Details(UID,(int) AccessTypes.PricesReport).Read)
            lnkProductHistory.Visible = clsDetails.Read || clsAccessRights.Details(UID, (int)AccessTypes.PricesReport).Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryReport); 
			lnkInventory.Visible = clsDetails.Read;

            lnkTransaction.Visible = false;
            lnkDatedReport.Visible = false;
            lnkTerminalReports.Visible = false;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesTransactionReport);
            if (clsDetails.Read)
            {
                lnkTransaction.Visible = true;
                lnkDatedReport.Visible = true;
                lnkTerminalReports.Visible = true;
            }
            else if (clsAccessRights.Details(UID, (int)AccessTypes.SummarizedDailySales).Read)
            {
                lnkDatedReport.Visible = true;
            }
            else if (clsAccessRights.Details(UID, (int)AccessTypes.PaidOutDisburseROC).Read)
            {
                lnkDatedReport.Visible = true;
            }

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ManagementReports);
            lnkManagementReport.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.AnalyticsReports);
            lnkAnalyticsReport.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SummarizedDailySalesWithTF);
            lnkeSalesReport.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesTransactionReport); 
			lnkStockTransaction.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ContactsReport); 
			lnkContacts.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CustomerCreditReport); 
			lnkCustomerCredit.Visible = clsDetails.Read;
 
            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CustomersWithCreditReport); 
            //lnkCustomersWithCreditReport.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.MostSalableItemsReport); 
            //lnkMostSalableItems.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.LeastSalableItemsReport); 
            //lnkLeastSalableItems.Visible = clsDetails.Read; 
			
			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.LoginLogoutReport); 
			lnkLoginLogoutReport.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseAnalysis); 
			lnkPurchaseAnalysis.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.AgentCommisionReport); 
            lnkAgentsCommision.Visible = clsDetails.Read;

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
