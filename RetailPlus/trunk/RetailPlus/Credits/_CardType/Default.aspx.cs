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

namespace AceSoft.RetailPlus.Credits._CardType
{

	public partial class _Default : System.Web.UI.Page
    {

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Credits";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Update an existing card type for internal use.";
			
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.CardTypes;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Credits;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
                    case "add":
                        stHeading = "Create New Credit Card Type";
                        SearchIndex = SearchCategoryID.CardTypes;
                        ctrlInsert.Visible = true;
                        break;
					case "edit":
						stHeading = "Modify Credit Card Type";
						SearchIndex = SearchCategoryID.CardTypes;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Credit Card Types List";
						SearchIndex = SearchCategoryID.CardTypes;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Credit Card Type details";
                        SearchIndex = SearchCategoryID.CardTypes;
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
