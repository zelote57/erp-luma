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

namespace AceSoft.RetailPlus.Rewards._Members
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Loyalty Program";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Customer reward's information";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Customers;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Rewards;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Customer Reward";
                        SearchIndex = SearchCategoryID.RewardMembers;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Customer Reward Info";
                        SearchIndex = SearchCategoryID.RewardMembers;
						ctrlUpdate.Visible = true;
						break;   
					case "details":
						stHeading = "Customer Reward Information";
                        SearchIndex = SearchCategoryID.RewardMembers;
						ctrlDetails.Visible = true;
						break;   
					case "list":
						stHeading = "Reward Members List";		
						SearchIndex = SearchCategoryID.RewardMembers;
						ctrlList.Visible = true;
						break;
                    case "listwithrewards":
                        stHeading = "Reward Members List";
                        SearchIndex = SearchCategoryID.RewardMembers;
                        ctrlListWithRewards.Visible = true;
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
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
