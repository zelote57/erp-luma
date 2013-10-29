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

namespace AceSoft.RetailPlus.SalesAndReceivables._SO
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Sales Order";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Sales Orders to customers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.SalesOrders;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.SalesAndReceivables;
			
			if (Request.QueryString["task"]!=null)
			{
                string strUnparsedTask = Request.QueryString["task"].ToString();
                if (strUnparsedTask.ToLower() == "reportfromposted")
                {
                    stHeading = "Sales Order Report";
                    ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                }
                else
                {
                    string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                    switch (task)
                    {
                        case "list":
                            stHeading = "Sales order list";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlList.Visible = true;
                            break;
                        case "add":
                            stHeading = "Add new sales order";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlInsert.Visible = true;
                            break;
                        case "edit":
                            stHeading = "Update open sales order";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlUpdate.Visible = true;
                            break;
                        case "additem":
                            stHeading = "Update items to sell";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlPost.Visible = true;
                            break;
                        case "issuegrn":
                            stHeading = "Issue GRN for Sales Orders";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlPost.Visible = true;
                            break;
                        case "details":
                            stHeading = "Sales Order Details";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlDetails.Visible = true;
                            break;
                        case "reports":
                            stHeading = "Sales Order Report";
                            SearchIndex = SearchCategoryID.SalesOrders;
                            ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                            break;
                        case "cancel":
                            stHeading = "Cancel Sales Order";
                            SearchIndex = SearchCategoryID.PurchaseOrders;
                            ctrlCancel.Visible = true;
                            break;
                        default:
                            break;
                    }
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesAndReceivablesMenu); 
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
