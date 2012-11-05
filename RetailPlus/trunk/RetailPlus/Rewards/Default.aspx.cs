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
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Rewards
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Rewards";
			string stHeading = defaultHeading;

			const string defaultTitle = "RetailPlus Rewards Management";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Rewards;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
                    case "postedso":
                        stHeading = "Posted Sales Orders Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        //ctrlSalesAndReceivables.Visible = true;
                        break;
                    case "postedsoreturns":
                        stHeading = "Posted Sales Returns Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        //ctrlSalesAndReceivables.Visible = true;
                        break;
                    case "postedcreditmemo":
                        stHeading = "Posted Sales Credit Memo Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        //ctrlSalesAndReceivables.Visible = true;
                        break;
                    case "salesanalysis":
                        stHeading = "Sales Analysis Reports";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        //ctrlSalesAndReceivables.Visible = true;
                        break;				
					default:	
						break;
				}
			}
			LargeHeading.Text = stHeading;
			RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.RewardCardIssuance);
            clsAccessRights.CommitAndDispose();

            if (clsDetails.Read==false)
				Server.Transfer("/RetailPlus/Home.aspx");
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
