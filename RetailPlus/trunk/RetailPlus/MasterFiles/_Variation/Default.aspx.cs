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

namespace AceSoft.RetailPlus.MasterFiles._Variation
{

    public partial class _Default : System.Web.UI.Page
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Variations";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Variations;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create new variation";	
						SearchIndex = SearchCategoryID.Variations;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify variation";
						SearchIndex = SearchCategoryID.Variations;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Variations list";		
						SearchIndex = SearchCategoryID.Variations;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Variation details";
                        SearchIndex = SearchCategoryID.Variations;
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
