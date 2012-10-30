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

namespace AceSoft.RetailPlus.SalesAndReceivables._Customer
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Sales and Receivables";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Person or Company customer.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Customers;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.SalesAndReceivables;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Customer";	
						SearchIndex = SearchCategoryID.Customers;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Customer";
						SearchIndex = SearchCategoryID.Customers;
						ctrlUpdate.Visible = true;
						break;   
					case "details":
						stHeading = "Customer Information";
						SearchIndex = SearchCategoryID.Customers;
						ctrlDetails.Visible = true;
						break;   
					case "list":
						stHeading = "Customers List";		
						SearchIndex = SearchCategoryID.Customers;
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
