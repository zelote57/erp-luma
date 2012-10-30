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
using AceSoft.RetailPlus.Security._AccessGroup;

namespace AceSoft.RetailPlus.PurchasesAndPayables._Vendor
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Person or Company supplier.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Vendors;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.PurchasesAndPayables;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Vendor";	
						SearchIndex = SearchCategoryID.Vendors;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Vendor";
						SearchIndex = SearchCategoryID.Vendors;
						ctrlUpdate.Visible = true;
						break;   
					case "details":
						stHeading = "Vendor Information";
						SearchIndex = SearchCategoryID.Vendors;
						ctrlDetails.Visible = true;
						break;   
					case "list":
						stHeading = "Vendors List";		
						SearchIndex = SearchCategoryID.Vendors;
						ctrlList.Visible = true;
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
