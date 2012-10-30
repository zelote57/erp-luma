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

namespace AceSoft.RetailPlus.MasterFiles
{
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

			const string defaultHeading = "Master Files";
			const string defaultTitle = "RetailPlus Master Files Setup";
			const SearchCategoryID defaultAllSourcesIndex = SearchCategoryID.NotApplicable;
			
			LargeHeading.Text = defaultHeading;

			SiteTitle.Title = defaultTitle;

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;

			RightBodySectionSearch.SearchIDSelectedItem = defaultAllSourcesIndex;

			PageLevelError.Visible = false;	
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.MasterFilesMenu); 
			if (clsDetails.Read==false)
				Server.Transfer(Constants.ROOT_DIRECTORY + "/Home.aspx");
			clsAccessRights.CommitAndDispose();
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
