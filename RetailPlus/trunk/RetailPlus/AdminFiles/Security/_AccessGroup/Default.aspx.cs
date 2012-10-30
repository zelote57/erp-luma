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

namespace AceSoft.RetailPlus.Security._AccessGroup
{
	public partial class _Default : System.Web.UI.Page
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Administration Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "RetailPlus System Security Administration";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.AccessGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.AdministrationFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Access Group";	
						SearchIndex = SearchCategoryID.AccessGroups;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Access Group";
						SearchIndex = SearchCategoryID.AccessGroups;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Access Group List";		
						SearchIndex = SearchCategoryID.AccessGroups;
						ctrlList.Visible = true;
						break;	
					case "accessrights":
						Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
						AccessGroup clsAccessGroup = new AccessGroup();
						AccessGroupDetails clsDetails = clsAccessGroup.Details(iID);
						clsAccessGroup.CommitAndDispose();

						stHeading = "Update access rights of <b>" + clsDetails.GroupName + "</b>";

						SearchIndex = SearchCategoryID.AccessGroups;
						ctrlAccessRights.Visible = true;
						break;
                    case "details":
                        stHeading = "Access Group Details";
                        SearchIndex = SearchCategoryID.AccessGroups;
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
