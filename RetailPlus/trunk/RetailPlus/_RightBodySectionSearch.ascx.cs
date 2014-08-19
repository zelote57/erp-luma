namespace AceSoft.RetailPlus
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __RightBodySectionSearch : System.Web.UI.UserControl
	{

		private const SearchCategoryID defaultSearchIDSelectedItem = SearchCategoryID.AllSources;
		
		private SearchCategoryID mSearchIDSelectedItem = defaultSearchIDSelectedItem;

		public SearchCategoryID SearchIDSelectedItem
		{
			set
			{
				mSearchIDSelectedItem = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				cboSearchID.Items.Clear();
//				cboSearchID.Enabled = false;
				if (mSearchIDSelectedItem == SearchCategoryID.NotApplicable)
				{	
					//cboSearchID.Enabled = false;
					txtSearchKeyword.Enabled = false;
                    try { Session.Remove("Search"); }
                    catch { }
				}
				else
				{
					cboSearchID.Items.Add(new ListItem("All Sources","AllSources"));
					cboSearchID.Items.Add(new ListItem("Access Groups","AccessGroups"));
					cboSearchID.Items.Add(new ListItem("Access Users","AccessUsers"));
					cboSearchID.Items.Add(new ListItem("Contacts","Contacts"));	//3
					cboSearchID.Items.Add(new ListItem("Contact Groups","ContactGroups"));
					cboSearchID.Items.Add(new ListItem("Countries","Countries"));
					cboSearchID.Items.Add(new ListItem("Products","Products"));	//6
					cboSearchID.Items.Add(new ListItem("Variations","Variations"));
					cboSearchID.Items.Add(new ListItem("Product Variations","ProductVariations"));
					cboSearchID.Items.Add(new ListItem("Product Groups","ProductGroups"));
					cboSearchID.Items.Add(new ListItem("Product Sub Groups","ProductSubGroups"));
					cboSearchID.Items.Add(new ListItem("Unit of Measurements","Units"));
					cboSearchID.Items.Add(new ListItem("Discounts","Discounts"));
					cboSearchID.Items.Add(new ListItem("Inventory List","InventoryList"));
					cboSearchID.Items.Add(new ListItem("Stock Types","StockTypes"));
					cboSearchID.Items.Add(new ListItem("Stock Trans","StockTrans"));
					cboSearchID.Items.Add(new ListItem("Promotional Plans","Promos"));
					cboSearchID.Items.Add(new ListItem("Not Applicable","NotApplicable"));
					cboSearchID.Items.Add(new ListItem("Product Groups Variations","ProductGroupVariations"));
					cboSearchID.Items.Add(new ListItem("Product Groups Variations Matrix","ProductGroupVariationsMatrix"));
					cboSearchID.Items.Add(new ListItem("Charge Types","ChargeType"));
					cboSearchID.Items.Add(new ListItem("Product Group Additional Charges", "ProductGroupAdditionalCharges"));
					cboSearchID.Items.Add(new ListItem("Card Types","CardTypes"));
					cboSearchID.Items.Add(new ListItem("Product Matrix","ProductVariationsMatrix"));
					cboSearchID.Items.Add(new ListItem("Branch","Branch"));
					cboSearchID.Items.Add(new ListItem("Purchase Orders","PurchaseOrders"));
					cboSearchID.Items.Add(new ListItem("Vendors","Vendors"));
					cboSearchID.Items.Add(new ListItem("Purchase Journals","PurchaseJournals"));
					cboSearchID.Items.Add(new ListItem("Account Summary","AccountSummary"));
					cboSearchID.Items.Add(new ListItem("Account Category","AccountCategory"));
					cboSearchID.Items.Add(new ListItem("Chart Of Accounts","ChartOfAccounts"));
					cboSearchID.Items.Add(new ListItem("Payments","PaymentJournals"));
					cboSearchID.Items.Add(new ListItem("Returns","PurchaseReturns"));
					cboSearchID.Items.Add(new ListItem("Debit Memo","PurchaseDebitMemo"));
                    cboSearchID.Items.Add(new ListItem("Customers", SearchCategoryID.Customers.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Sales Orders", SearchCategoryID.SalesOrders.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Sales Journals", SearchCategoryID.SalesJournals.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Sales Returns", SearchCategoryID.SalesReturns.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Sales Credit Memo", SearchCategoryID.SalesCreditMemo.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Transfer In", SearchCategoryID.TransferIn.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Transfer Out", SearchCategoryID.TransferOut.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Inv Adjustment", SearchCategoryID.InvAdjustment.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Close Inventory", SearchCategoryID.CloseInventory.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("General Journals", SearchCategoryID.GeneralJournals.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Positions", SearchCategoryID.Positions.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Departments", SearchCategoryID.Departments.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Reward Members", SearchCategoryID.RewardMembers.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Customer Management", SearchCategoryID.CustomerDetailed.ToString("G")));
                    cboSearchID.Items.Add(new ListItem("Banks", SearchCategoryID.Banks.ToString("G")));

					cboSearchID.SelectedIndex  = Convert.ToInt16(mSearchIDSelectedItem);
				}
			}
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
			this.cmdSearch.Click += new System.Web.UI.ImageClickEventHandler(this.cmdSearch_Click);
			this.cmdSearch1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdSearch1_Click);

		}
		#endregion

		private void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Search();
		}

		private void cmdSearch1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Search();
		}

		private void Search()
		{
			if (txtSearchKeyword.Enabled)
			{

				SearchCategoryID SearchIndex = (SearchCategoryID) Enum.Parse(typeof(SearchCategoryID), cboSearchID.SelectedItem.Value.ToString());
				string stParam = "?task=" +  Common.Encrypt("list",Session.SessionID) + "&search=" +  Common.Encrypt(Server.UrlEncode(txtSearchKeyword.Text),Session.SessionID);

				System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString;
				foreach(string querystring in querystrings.AllKeys)
				{
                    if (querystring.ToLower() != "task" && querystring.ToLower() != "search")
                        stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
                    //else if (querystring.ToLower() == "task")
                    //    stParam = stParam.Replace(Common.Encrypt("list", Session.SessionID), Common.Encrypt(querystrings[querystring].ToString(), Session.SessionID));
				}

				switch(SearchIndex)
				{
					case SearchCategoryID.AllSources:		
						break;
					case SearchCategoryID.AccessGroups:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessGroup/Default.aspx" + stParam);
						break;
					case SearchCategoryID.AccessUsers:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessUser/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Contacts:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ContactGroups:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ContactGroup/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Countries:
						break;
					case SearchCategoryID.Products:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Variations:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductVariations:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Variations/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductGroups:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductGroup/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ChargeType:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ChargeType/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductSubGroups:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductSubGroup/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Units:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Discounts:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Discount/Default.aspx" + stParam);
						break;
					case SearchCategoryID.InventoryList:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
						break;
					case SearchCategoryID.StockTypes:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_StockType/Default.aspx" + stParam);
						break;
					case SearchCategoryID.StockTrans:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Promos:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Promo/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductGroupVariations:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductGroup/_Variations/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductGroupVariationsMatrix:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductGroup/_VariationsMatrix/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductGroupAdditionalCharges:
						break;
					case SearchCategoryID.CardTypes:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_CardType/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ProductVariationsMatrix:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Branch:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx" + stParam);
						break;
					case SearchCategoryID.PurchaseOrders:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Vendors:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx" + stParam);
						break;
					case SearchCategoryID.PurchaseJournals:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PurchaseJournals/Default.aspx" + stParam);
						break;
					case SearchCategoryID.AccountSummary:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/GeneralLedger/_AccountSummary/Default.aspx" + stParam);
						break;
					case SearchCategoryID.AccountCategory:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/GeneralLedger/_AccountCategory/Default.aspx" + stParam);
						break;
					case SearchCategoryID.ChartOfAccounts:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/GeneralLedger/_ChartOfAccounts/Default.aspx" + stParam);
						break;
					case SearchCategoryID.PaymentJournals:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Payments/Default.aspx" + stParam);
						break;
					case SearchCategoryID.PurchaseReturns:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx" + stParam);
						break;
					case SearchCategoryID.PurchaseDebitMemo:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_DebitMemo/Default.aspx" + stParam);
						break;
					case SearchCategoryID.Customers:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Customer/Default.aspx" + stParam);
						break;
					case SearchCategoryID.SalesOrders:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SO/Default.aspx" + stParam);
						break;
					case SearchCategoryID.SalesJournals:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SalesJournals/Default.aspx" + stParam);
						break;
					case SearchCategoryID.SalesReturns:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Returns/Default.aspx" + stParam);
						break;
					case SearchCategoryID.SalesCreditMemo:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_CreditNote/Default.aspx" + stParam);
						break;
					case SearchCategoryID.TransferIn:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_TransferIn/Default.aspx" + stParam);
						break;
					case SearchCategoryID.TransferOut:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx" + stParam);
						break;
					case SearchCategoryID.InvAdjustment:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/_InvAdjustment/Default.aspx" + stParam);
						break;
					case SearchCategoryID.CloseInventory:
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
						break;
                    case SearchCategoryID.RewardMembers:
                        Response.Redirect(Constants.ROOT_DIRECTORY + "/Rewards/_Members/Default.aspx" + stParam);
                        break;
                    case SearchCategoryID.Banks:
                        Response.Redirect(Constants.ROOT_DIRECTORY + "/GeneralLedger/_Bank/Default.aspx" + stParam);
                        break;
					default:
						break;
				}
			}
		}
	}
}
