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

namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup
{
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Product Sub Groups.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ProductSubGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Product Sub Group";	
						SearchIndex = SearchCategoryID.ProductSubGroups;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Product Sub Group";
						SearchIndex = SearchCategoryID.ProductSubGroups;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Product Sub Groups List";		
						SearchIndex = SearchCategoryID.ProductSubGroups;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Product Sub Groups Details";
                        SearchIndex = SearchCategoryID.ProductSubGroups;
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
