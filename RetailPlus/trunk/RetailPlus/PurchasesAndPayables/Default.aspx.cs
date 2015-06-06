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

namespace AceSoft.RetailPlus.PurchasesAndPayables
{
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Purchases & Payables";
			string stHeading = defaultHeading;			

			const string defaultTitle = "RetailPlus Purchases & Payables Management";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.PurchasesAndPayables;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "postedpo":
						stHeading = "Posted Purchase Orders Report";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPurchasesAndPayables.Visible = true;
						break;
					case "postedporeturns":
						stHeading = "Posted Purchase Returns Report";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPurchasesAndPayables.Visible = true;
						break;
					case "posteddebitmemo":
						stHeading = "Posted Purchase Debit Memo Report";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPurchasesAndPayables.Visible = true;
						break;
					case "purchaseanalysis":
						stHeading = "Purchase Analysis Reports";
						SearchIndex = SearchCategoryID.NotApplicable;
						ctrlPurchasesAndPayables.Visible = true;
						break;

                    case "postedpoesales":
                        stHeading = "Posted Purchase Orders Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlPurchasesAndPayables.Visible = true;
                        break;
                    case "postedporeturnsesales":
                        stHeading = "Posted Purchase Returns Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlPurchasesAndPayables.Visible = true;
                        break;
                    case "posteddebitmemoesales":
                        stHeading = "Posted Purchase Debit Memo Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlPurchasesAndPayables.Visible = true;
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchasesAndPayablesMenu);
            clsAccessRights.CommitAndDispose();

			if (clsDetails.Read==false)
				Server.Transfer(Constants.ROOT_DIRECTORY + "/Home.aspx");
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
