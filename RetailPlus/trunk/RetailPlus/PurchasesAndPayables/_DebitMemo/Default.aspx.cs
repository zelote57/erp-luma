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

namespace AceSoft.RetailPlus.PurchasesAndPayables._DebitMemo
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Purchase Debit Memo";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Purchase Debit Memo to suppliers";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.PurchaseReturns;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.PurchasesAndPayables;
			
			if (Request.QueryString["task"]!=null)
			{
                string strUnparsedTask = Request.QueryString["task"].ToString();
                if (strUnparsedTask.ToLower() == "reportfromposted")
                {
                    stHeading = "Purchase Debit Memo Report";
                    ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                }
                else
                {
                    string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                    switch (task)
                    {
                        case "list":
                            stHeading = "Purchase Debit Memo list";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlList.Visible = true;
                            break;
                        case "listesales":
                            stHeading = "Purchase Debit Memo list";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrlListeSales.Visible = true;
                            break;
                        case "elist":
                            stHeading = "Manage Purchase Debit Memo for eSales";
                            SearchIndex = SearchCategoryID.NotApplicable;
                            ctrleList.Visible = true;
                            break;
                        case "add":
                            stHeading = "Add new Debit Memo";
                            SearchIndex = SearchCategoryID.PurchaseReturns;
                            ctrlInsert.Visible = true;
                            break;
                        case "edit":
                            stHeading = "Update open Purchase Debit Memo";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlUpdate.Visible = true;
                            break;
                        case "additem":
                            stHeading = "Update items to debit";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlPost.Visible = true;
                            break;
                        case "post":
                            stHeading = "Post Purchase Debit Memo";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlPost.Visible = true;
                            break;
                        case "details":
                            stHeading = "Purchase Debit Memo Details";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlDetails.Visible = true;
                            break;
                        case "reports":
                            stHeading = "Purchase Debit Memo Report";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
                            ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                            break;
                        case "cancel":
                            stHeading = "Cancel Purchase Debit Memo";
                            SearchIndex = SearchCategoryID.PurchaseDebitMemo;
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
