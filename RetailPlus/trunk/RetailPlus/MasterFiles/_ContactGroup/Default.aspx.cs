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

namespace AceSoft.RetailPlus.MasterFiles._ContactGroup
{
	public partial class _Default : System.Web.UI.Page
    {

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Category of related contacts.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ContactGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Contact Group";	
						SearchIndex = SearchCategoryID.ContactGroups;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Contact Group";
						SearchIndex = SearchCategoryID.ContactGroups;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Contact Groups List";		
						SearchIndex = SearchCategoryID.ContactGroups;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Contact Group Details";
                        SearchIndex = SearchCategoryID.ContactGroups;
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
		private void InitializeComponent()
		{    

		}

		#endregion
	}
}
