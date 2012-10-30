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

namespace AceSoft.RetailPlus.MasterFiles._Department
{

	public partial class _Default : System.Web.UI.Page
    {

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Departments that can be use for categorizing Contacts and Users.";
			
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Departments;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Department";	
						SearchIndex = SearchCategoryID.Departments;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Department";
						SearchIndex = SearchCategoryID.Departments;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Departments List";		
						SearchIndex = SearchCategoryID.Departments;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Department details";
                        SearchIndex = SearchCategoryID.Departments;
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
