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

namespace AceSoft.RetailPlus.MasterFiles._ChargeType
{
	public partial class _Default : System.Web.UI.Page
    {

        #region Web Form Designer generated code
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Credit types in w/ a customer receives goods before paying.";
			
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
						stHeading = "Create New Charge Type";	
						SearchIndex = SearchCategoryID.ChargeType;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Charge Type";
						SearchIndex = SearchCategoryID.ChargeType;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Charge Types List";		
						SearchIndex = SearchCategoryID.ChargeType;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Charge Type details";
                        SearchIndex = SearchCategoryID.ChargeType;
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
