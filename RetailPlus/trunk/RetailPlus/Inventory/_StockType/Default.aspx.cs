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

namespace AceSoft.RetailPlus.Inventory._StockType
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Inventory";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Stock Types";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.StockTypes;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Stock Type";	
						SearchIndex = SearchCategoryID.StockTypes;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Stock Type";
						SearchIndex = SearchCategoryID.StockTypes;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Stock Type List";		
						SearchIndex = SearchCategoryID.StockTypes;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Stock Type Details";
                        SearchIndex = SearchCategoryID.StockTypes;
                        ctrlDetails.Visible = true;
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
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}

		#endregion
	}
}
