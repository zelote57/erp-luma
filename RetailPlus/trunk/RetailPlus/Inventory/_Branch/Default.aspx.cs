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

namespace AceSoft.RetailPlus.Inventory._Branch
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Inventory";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Branch.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Branch;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Branch";	
						SearchIndex = SearchCategoryID.Branch;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Branch";
						SearchIndex = SearchCategoryID.Branch;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Branch List";		
						SearchIndex = SearchCategoryID.Branch;
						ctrlList.Visible = true;
						break;
                    case "synchronizeinv":
                        stHeading = "Synchronize Branch Inventory Count";
                        SearchIndex = SearchCategoryID.Branch;
                        ctrlSynchronize.Visible = true;
                        break;
                    case "details":
                        stHeading = "Branch Details";
                        SearchIndex = SearchCategoryID.Branch;
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
