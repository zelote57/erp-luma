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

namespace AceSoft.RetailPlus.MasterFiles._ProductGroup
{
	public partial class _Default : System.Web.UI.Page
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Product Groups";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ProductGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Product Group";	
						SearchIndex = SearchCategoryID.ProductGroups;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Product Group";
						SearchIndex = SearchCategoryID.ProductGroups;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Product Groups List";		
						SearchIndex = SearchCategoryID.ProductGroups;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Product Group details";
                        SearchIndex = SearchCategoryID.ProductGroups;
                        ctrlDetails.Visible = true;
                        break;   
					default:	
						break;
				}

				LargeHeading.Text = stHeading;
				RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
			}
		}


		#endregion

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
