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

namespace AceSoft.RetailPlus.PurchasesAndPayables._Returns
{
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Purchase Returns";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Purchase Returns to suppliers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.PurchaseReturns;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.PurchasesAndPayables;
			
			if (Request.QueryString["task"]!=null)
			{
                 string strUnparsedTask = Request.QueryString["task"].ToString();
                 if (strUnparsedTask.ToLower() == "reportfromposted")
                 {
                     stHeading = "Purchase Return Report";
                     ctrlReports.Visible = true;
                 }
                 else
                 {
                     string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                     switch (task)
                     {
                         case "list":
                             stHeading = "Purchase returns list";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlList.Visible = true;
                             break;
                         case "add":
                             stHeading = "Add new purchase returns";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlInsert.Visible = true;
                             break;
                         case "edit":
                             stHeading = "Update open purchase returns";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlUpdate.Visible = true;
                             break;
                         case "additem":
                             stHeading = "Update items to return";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlPost.Visible = true;
                             break;
                         case "post":
                             stHeading = "Post Purchase returns";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlPost.Visible = true;
                             break;
                         case "details":
                             stHeading = "Purchase Return Details";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlDetails.Visible = true;
                             break;
                         case "reports":
                             stHeading = "Purchase Return Report";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
                             ctrlReports.Visible = true;
                             break;
                         case "cancel":
                             stHeading = "Cancel Purchase Return";
                             SearchIndex = SearchCategoryID.PurchaseReturns;
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
