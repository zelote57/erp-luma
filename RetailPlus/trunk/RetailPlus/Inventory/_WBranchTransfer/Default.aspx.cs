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

namespace AceSoft.RetailPlus.Inventory._WBranchTransfer
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Warehouse -> Branch Transfer";
			string stHeading = defaultHeading;

            const string defaultTitle = "Issue Warehouse -> Branch Transfers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;

            if (Request.QueryString["task"] != null)
            {
                string strUnparsedTask = Request.QueryString["task"].ToString();
                if (strUnparsedTask.ToLower() == "reportfromposted")
                {
                    stHeading = "Warehouse -> Branch Transfer Report";
                    //ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                    //ctrlProcessing.Visible = false;
                    //ctrlProcessing.Dispose();
                }
                else
                {
                    string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                    switch (task)
                    {
                        case "list":
                            stHeading = "Warehouse -> Branch Transfer list";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlList.Visible = true;
                            break;
                        case "add":
                            stHeading = "Add new warehouse -> branch transfer";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlInsert.Visible = true;
                            break;
                        case "edit":
                            stHeading = "Update open warehouse -> branch transfer";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlUpdate.Visible = true;
                            break;
                        case "additem":
                            stHeading = "Update items to purchase";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlPost.Visible = true;
                            break;
                        case "issuegrn":
                            stHeading = "Issue GRN for Warehouse -> Branch Transfers";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlPost.Visible = true;
                            break;
                        case "details":
                            stHeading = "Warehouse -> Branch Transfer Details";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlDetails.Visible = true;
                            break;
                        case "reports":
                            stHeading = "warehouse -> Branch Transfer Report";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                            break;
                        case "cancel":
                            stHeading = "Cancel warehouse -> Branch Transfer";
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.WarehouseToBranchTransfer);
            if (!clsDetails.Read) Server.Transfer(Constants.ROOT_DIRECTORY + "/Home/Default.aspx");
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
