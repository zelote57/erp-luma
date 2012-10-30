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

namespace AceSoft.RetailPlus.Security._Terminals
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Administration Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "RetailPlus System Security Administration";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.AccessUsers;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.AdministrationFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "edit":
						stHeading = "Modify Terminal Details";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlUpdate.Visible = true;
						break;
                    case "updaterewards":
                        stHeading = "Update Reward Point System";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlUpdateRewards.Visible = true;
                        break;
					case "list":
						stHeading = "Terminal List";		
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlList.Visible = true;
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
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
