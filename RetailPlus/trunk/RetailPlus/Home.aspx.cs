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

namespace AceSoft.RetailPlus
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Home : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Home";
            string stHeading = defaultHeading;

			const string defaultTitle = "Welcome to RetailPlus System";

            const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
            SearchCategoryID SearchIndex = defaultSearchIndex;

			SiteTitle.Title = defaultTitle;

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Home;

            if (Request.QueryString["task"] != null)
            {
                ctrlHome.Visible = false;
                string task = Common.Decrypt(Request.QueryString["task"].ToString(), Session.SessionID);
                switch (task)
                {
                    case "redeemrewards":
                        //stHeading = "Redeem Rewards";
                        //SearchIndex = SearchCategoryID.NotApplicable;
                        //ctrlRedeemRewards.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                stHeading = "Home";
                SearchIndex = SearchCategoryID.NotApplicable;
                ctrlHome.Visible = true;
            }

            LargeHeading.Text = stHeading;
            RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;

			PageLevelError.Visible = false;

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
