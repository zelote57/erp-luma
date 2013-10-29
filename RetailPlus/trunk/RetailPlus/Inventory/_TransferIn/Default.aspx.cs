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

namespace AceSoft.RetailPlus.Inventory._TransferIn
{
	public partial class _Default : System.Web.UI.Page
	{



		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Transfer In";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Transfer In";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "list":
						stHeading = "Transfer In list";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlList.Visible = true;
						break;
					case "add":
						stHeading = "Add new transfer in";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Update open transfer in";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlUpdate.Visible = true;
						break;
					case "additem":
						stHeading = "Update items to transfer in";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPost.Visible = true;
						break;
					case "issuegrn":
						stHeading = "Issue GRN for Transfer In";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPost.Visible = true;
						break;
					case "details":
						stHeading = "Transfer In Details";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlDetails.Visible = true;
						break;
					case "reports":
						stHeading = "Transfer In Report";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
						break;
					case "cancel":
						stHeading = "Cancel Transfer In";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlCancel.Visible = true;
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryMenu); 
			if (clsDetails.Read==false)
				Server.Transfer(Constants.ROOT_DIRECTORY + "/Home/Default.aspx");
			clsAccessRights.CommitAndDispose();
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
