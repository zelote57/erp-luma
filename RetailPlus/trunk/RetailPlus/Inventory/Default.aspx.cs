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

namespace AceSoft.RetailPlus.Inventory
{
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Inventory";
			string stHeading = defaultHeading;			

			const string defaultTitle = "RetailPlus System Inventory Management";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "list":
						stHeading = "List of product inventory";		
						SearchIndex = SearchCategoryID.InventoryList;
						ctrlList.Visible = true;
						break;
                    case "inventoryanalyst":
                        stHeading = "Inventory Analyst";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlInventoryAnalyst.Visible = true;
                        break;
                    case "closeinventory":
                        stHeading = "Close Month End Inventory";
                        SearchIndex = SearchCategoryID.CloseInventory;
                        ctrlCloseInventory.Visible = true;
                        break;
                    case "closeinventoryproduct":
                        stHeading = "Close Month End Inventory";
                        SearchIndex = SearchCategoryID.CloseInventory;
                        ctrlCloseInventoryProduct.Visible = true;
                        break;
                    case "closeinventorydetailed":
                        stHeading = "Close Month End Inventory";
                        SearchIndex = SearchCategoryID.CloseInventory;
                        ctrlCloseInventoryDetailed.Visible = true;
                        break;
                    //case "closeinventorysubgroup":
                    //    stHeading = "Close Month End Inventory";
                    //    SearchIndex = SearchCategoryID.CloseInventory;
                    //    ctrlCloseInventorySubGroup.Visible = true;
                    //    break;
                    case "invadjustment":
                        stHeading = "Inventory adjustments";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlInvAdjustment.Visible = true;
                        break;
                    case "invthreshold":
                        stHeading = "Set Inventory Threshold";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlInvThreshold.Visible = true;
                        break;	
                    case "inventoryrep":
                        stHeading = "View Inventory Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductInventoryReport.Visible = true;
                        break;
                    case "closinginventoryrep":
                        stHeading = "View Closing Inventory Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlClosingInventoryReport.Visible = true;
                        break;
                    case "inventoryperbranchrep":
                        stHeading = "View Inventory per Branch Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductBranchInventoryReport.Visible = true;
                        break;
                    case "expiredinventoryrep":
                        stHeading = "View Expired Inventory Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductInventoryReport.Visible = true;
                        break;	
                    case "itemsforreorderrep":
                        stHeading = "View Items for Reorder Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductInventoryReport.Visible = true;
                        break;	
                    case "overstockrep":
                        stHeading = "View Over Stock Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductInventoryReport.Visible = true;
                        break;
                    case "totalstockrep":
                        stHeading = "View Total Stock Report";
                        SearchIndex = SearchCategoryID.InventoryList;
                        ctrlProductInventoryReport.Visible = true;
                        break;
					default:	
						break;
				}
			}
			LargeHeading.Text = stHeading;
			RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryMenu); 
			if (clsDetails.Read==false)
				Server.Transfer("/RetailPlus/Home/Default.aspx");
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
