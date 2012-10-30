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

namespace AceSoft.RetailPlus.Inventory._Stock
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Inventory";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Stock Transactions";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.StockTrans;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Stock Transaction";	
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlInsert.Visible = true;
						break;
					case "additem":
						stHeading = "Modify/Add Stock Items";
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlAddItem.Visible = true;
						break;   
					case "list":
						stHeading = "Stock Transactions List";		
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlList.Visible = true;
						break;	
					case "transfer":
						stHeading = "Stock Transfer";		
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlTransfer.Visible = true;
						break;	
					case "upload":
						stHeading = "Stock Upload";		
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlUpload.Visible = true;
						break;	
					case "details":
						stHeading = "Stock Details";		
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlDetails.Visible = true;
						break;	
					case "reports":
						stHeading = "Stock Transaction Report";
						SearchIndex = SearchCategoryID.StockTrans;
						ctrlReports.Visible = true;
						break;
					default:	
						break;
				}

				LargeHeading.Text = stHeading;
				RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
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
		private void InitializeComponent()
		{    

		}

		#endregion
	}
}
