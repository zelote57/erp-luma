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

namespace AceSoft.RetailPlus.MasterFiles._Contact
{
	public partial class _Default : System.Web.UI.Page
    {

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Person or Company who might be a supplier or a customer.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Contacts;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Contact";	
						SearchIndex = SearchCategoryID.Contacts;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Contact";
						SearchIndex = SearchCategoryID.Contacts;
						ctrlUpdate.Visible = true;
						break;   
					case "details":
						stHeading = "Contact Information";
						SearchIndex = SearchCategoryID.Contacts;
						ctrlDetails.Visible = true;
						break;   
					case "list":
						stHeading = "Contacts List";		
						SearchIndex = SearchCategoryID.Contacts;
						ctrlList.Visible = true;
						break;		
			        case "pricelevel":
                        stHeading = "Price Level";		
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPriceLevel.Visible = true;
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
