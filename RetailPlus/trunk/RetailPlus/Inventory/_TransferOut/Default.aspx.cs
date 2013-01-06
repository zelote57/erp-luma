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

namespace AceSoft.RetailPlus.Inventory._TransferOut
{
	public partial class _Default : System.Web.UI.Page
	{



		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Transfer Out";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Issue Transfer Out";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.TransferOut;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Inventory;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "list":
						stHeading = "Transfer Out list";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlList.Visible = true;
						break;
					case "add":
						stHeading = "Add new Transfer Out";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Update open Transfer Out";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlUpdate.Visible = true;
						break;
					case "additem":
						stHeading = "Update items to Transfer Out";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlPost.Visible = true;
						break;
					case "issuegrn":
						stHeading = "Issue GRN for Transfer Out";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlPost.Visible = true;
						break;
					case "details":
						stHeading = "Transfer Out Details";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlDetails.Visible = true;
						break;
					case "reports":
						stHeading = "Transfer Out Report";
						SearchIndex = SearchCategoryID.TransferOut;
						ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                        ctrlProcessing.Visible = false;
                        ctrlProcessing.Dispose();
						break;
					case "cancel":
						stHeading = "Cancel Transfer Out";
						SearchIndex = SearchCategoryID.TransferOut;
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
				Server.Transfer(Constants.ROOT_DIRECTORY + "/Home.aspx");
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
