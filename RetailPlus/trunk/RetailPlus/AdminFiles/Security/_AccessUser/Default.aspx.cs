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

namespace AceSoft.RetailPlus.Security._AccessUser
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
					case "add":
						stHeading = "Create New Access User";	
						SearchIndex = SearchCategoryID.AccessUsers;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Access User";
						SearchIndex = SearchCategoryID.AccessUsers;
						ctrlUpdate.Visible = true;
						break;
					case "list":
						stHeading = "Access User List";		
						SearchIndex = SearchCategoryID.AccessUsers;
						ctrlList.Visible = true;
						break;					
					case "accessrights":
						Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
						AccessUser clsAccessUser = new AccessUser();
						AccessUserDetails clsDetails = clsAccessUser.Details(iID);
						clsAccessUser.CommitAndDispose();

						stHeading = "Update access rights of <b>" + clsDetails.Name + "</b>";

						SearchIndex = SearchCategoryID.AccessUsers;
						ctrlAccessRights.Visible = true;
						break;	
					case "custom":
						stHeading = "Customize your account.";

						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlAccount.Visible = true;
						break;
                    case "details":
                        stHeading = "Account Details";
                        SearchIndex = SearchCategoryID.AccessUsers;
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
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
