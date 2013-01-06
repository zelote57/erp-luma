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

namespace AceSoft.RetailPlus.SalesAndReceivables._Returns
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Sales Returns";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Sales Returns to suppliers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.SalesReturns;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.SalesAndReceivables;
			
			if (Request.QueryString["task"]!=null)
			{
                 string strUnparsedTask = Request.QueryString["task"].ToString();
                 if (strUnparsedTask.ToLower() == "reportfromposted")
                 {
                     stHeading = "Sales Return Report";
                     ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                 }
                 else
                 {
                     string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                     switch (task)
                     {
                         case "list":
                             stHeading = "Sales returns list";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlList.Visible = true;
                             break;
                         case "add":
                             stHeading = "Add new sales returns";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlInsert.Visible = true;
                             break;
                         case "edit":
                             stHeading = "Update open sales returns";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlUpdate.Visible = true;
                             break;
                         case "additem":
                             stHeading = "Update items to return";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlPost.Visible = true;
                             break;
                         case "post":
                             stHeading = "Post Sales returns";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlPost.Visible = true;
                             break;
                         case "details":
                             stHeading = "Sales Return Details";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlDetails.Visible = true;
                             break;
                         case "reports":
                             stHeading = "Sales Return Report";
                             SearchIndex = SearchCategoryID.SalesReturns;
                             ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                             break;
                         case "cancel":
                             stHeading = "Cancel Sales Return";
                             SearchIndex = SearchCategoryID.SalesReturns;
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
