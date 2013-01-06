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

namespace AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "General Ledger";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Chart Of Accounts.";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ChartOfAccounts;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.GeneralLedger;
			
			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Account";	
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Account";
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Chart Of Accounts";		
						ctrlList.Visible = true;
						break;
                    case "chartofaccountsreport":
                        stHeading = "Chart of Accounts Reports";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlReports.Visible = true; ctrlProcessing.Visible = false;  ctrlProcessing.Dispose();
                        break;
                    case "balancesheetreport":
                        stHeading = "Balance Sheet Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlBalanceSheet.Visible = true;
                        break;
                    case "glreport":
                        stHeading = "General Ledger Report";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlGeneralLedger.Visible = true;
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
