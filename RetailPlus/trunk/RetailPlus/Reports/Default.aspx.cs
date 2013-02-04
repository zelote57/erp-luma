using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Reports
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

			const string defaultHeading = "Reports";
			string stHeading = defaultHeading;			

			const string defaultTitle = "RetailPlus System Reports Generation";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			RightBodySectionSearch.SearchIDSelectedItem = defaultSearchIndex;

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Reports;
			
			if (Request.QueryString["task"]!=null)
			{
                string strUnparsedTask = Request.QueryString["task"].ToString();
                if (strUnparsedTask.ToLower() == "transaction")
                {
                    stHeading = "View Transaction Report";
                    ctrlTransactionReport.Visible = true;
                }
                else if (strUnparsedTask.ToLower() == "transactions")
                {
                    stHeading = "View Transactions Report";
                    ctrlDatedReport.Visible = true;
                }
                else if (strUnparsedTask.ToLower() == "producthistory")
                {
                    stHeading = "Product History Report";
                    ctrlProductHistoryReport.Visible = true;
                }
                else
                {
                    string task = Common.Decrypt(strUnparsedTask, Session.SessionID);
                    switch (task)
                    {
                        case "contacts":
                            stHeading = "View Contacts Report";
                            ctrlContacts.Visible = true;
                            break;
                        case "inventory":
                            stHeading = "View Inventory Report";
                            ctrlProductInventoryReport.Visible = true;
                            break;
                        case "expiredinventory":
                            stHeading = "View Expired Inventory Report";
                            ctrlProductInventoryReport.Visible = true;
                            break;
                        //case "itemsforreorder":
                        //    stHeading = "Items For Reorder";
                        //    ctrlItemsForReorder.Visible = true;
                        //    break;
                        //case "overstock":
                        //    stHeading = "Over Stock Items";
                        //    ctrlOverStock.Visible = true;
                        //    break;
                        //case "pricehistory":
                        //    stHeading = "Product Price History Report";
                        //    ctrlProductHistoryReport.Visible = true;
                        //    break;
                        case "products":
                            stHeading = "View Products Report";
                            ctrlProducts.Visible = true;
                            break;
                        case "transaction":
                            stHeading = "View Transaction Report";
                            ctrlTransactionReport.Visible = true;
                            break;
                        case "audittrail":
                            stHeading = "Audit Trail Report";
                            //						ctrlAuditTrail.Visible = true;
                            break;
                        case "stocktransaction":
                            stHeading = "Stock Transaction Report";
                            ctrlStockTransactionReport.Visible = true;
                            break;
                        case "customercredit":
                            stHeading = "Customer Credit Report";
                            ctrlCustomerCredit.Visible = true;
                            break;
                        //case "customerwithcredit":
                        //    stHeading = "Customer With Credit Report";
                        //    ctrlCustomerWithCredit.Visible = true;
                        //    break;
                        //case "mostsalableitems":
                        //    stHeading = "Most Salable Items Report";
                        //    ctrlMostSalableItemsReport.Visible = true;
                        //    break;
                        //case "leastsalableitems":
                        //    stHeading = "Least Salable Items Report";
                        //    ctrlLeastSalableItemsReport.Visible = true;
                        //    break;
                        case "datedreport":
                            stHeading = "Dated Transactions Report";
                            ctrlDatedReport.Visible = true;
                            break;
                        case "terminalreport":
                            stHeading = "Terminal Report";
                            ctrlTerminalReport.Visible = true;
                            break;
                        //case "salesreport":
                        //    stHeading = "Sales Report";
                        //    ctrlSalesReport.Visible = true;
                        //    break;
                        case "loginlogoutreport":
                            stHeading = "Login-Logout Report";
                            ctrlLoginLogoutReport.Visible = true;
                            break;
                        //case "salesperday":
                        //    stHeading = "Sales Per Day Report";
                        //    ctrlSalesPerDay.Visible = true;
                        //    break;
                        //case "salesperhour":
                        //    stHeading = "Sales Per Hour Report";
                        //    ctrlSalesPerHour.Visible = true;
                        //    break;
                        case "purchaseanalysis":
                            stHeading = "Purchase Analysis Report";
                            ctrlPurchasesAndPayables.Visible = true;
                            break;
                        case "producthistory":
                            stHeading = "Product History Report";
                            ctrlProductHistoryReport.Visible = true;    //ctrlProcessing.Visible = false;     ctrlProcessing.Dispose();
                            break;
                        case "agentscommision":
                            stHeading = "Agents Commision Report";
                            ctrlAgentsCommision.Visible = true;
                            break;
                        case "agentssales":
                            stHeading = "Agents Sales Report";
                            ctrlAgentsSales.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
			}
			LargeHeading.Text = stHeading;
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReportsMenu); 
			if (clsDetails.Read==false)
				Server.Transfer(Constants.ROOT_DIRECTORY + "/Home.aspx");
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
