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

namespace AceSoft.RetailPlus.MasterFiles._Discount
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Selling types at a reduced price.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Discounts;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Discount";	
						SearchIndex = SearchCategoryID.Discounts;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Discount";
						SearchIndex = SearchCategoryID.Discounts;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Discounts List";		
						SearchIndex = SearchCategoryID.Discounts;
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
