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

namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Purchase Order";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Purchase Orders to suppliers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.PurchaseOrders;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.PurchasesAndPayables;

            if (Request.QueryString["task"] != null)
            {
                string strUnparsedTask = Request.QueryString["task"].ToString();
                if (strUnparsedTask.ToLower() == "reportfromposted")
                {
                    stHeading = "Purchase Order Report";
                    ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                }
                else
                {
                    string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                    switch (task)
                    {
                        case "list":
                            stHeading = "Purchase order list";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlList.Visible = true;
                            break;
                        case "listesales":
                            stHeading = "Purchase order list";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlListeSales.Visible = true;
                            break;
                        case "elist":
                            stHeading = "Manage Purchase order for eSales";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrleList.Visible = true;
                            break;
                        case "add":
                            stHeading = "Add new purchase order";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlInsert.Visible = true;
                            break;
                        case "edit":
                            stHeading = "Update open purchase order";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlUpdate.Visible = true;
                            break;
                        case "additem":
                            stHeading = "Update items to purchase";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlPost.Visible = true;
                            break;
                        case "issuegrn":
                            stHeading = "Issue GRN for Purchase Orders";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlPost.Visible = true;
                            break;
                        case "details":
                            stHeading = "Purchase Order Details";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlDetails.Visible = true;
                            break;
                        case "reports":
                            stHeading = "Purchase Order Report";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                            break;
                        case "cancel":
                            stHeading = "Cancel Purchase Order";
                            SearchIndex = SearchCategoryID.NotApplicable;
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchasesAndPayablesMenu); 
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
