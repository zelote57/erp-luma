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

namespace AceSoft.RetailPlus
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Login";
			const string defaultTitle = "RetailPlus System";
			const SearchCategoryID defaultAllSourcesIndex = SearchCategoryID.AllSources;
			
			LargeHeading.Text = defaultHeading;

			SiteTitle.Title = defaultTitle;

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.Home;

			RightBodySectionSearch.SearchIDSelectedItem = defaultAllSourcesIndex;

			PageLevelError.Visible = false;	

//			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
//			Data.TerminalReportDetails details = clsTerminalReport.Details(CompanyDetails.TerminalNo);
//			clsTerminalReport.CommitAndDispose();
//
//			if (Convert.ToInt64(details.EndingTransactionNo) >= 50)
//			{
//				Session["ErrorCurrentExecutionFilePath"] = Request.CurrentExecutionFilePath;
//				Session["ErrMessage"] = Constants.DEMO_EXPIRED_MESSAGE;
//				Session["ErrorMessage"] = Constants.DEMO_EXPIRED_MESSAGE;
//				Session["ErrorSource"] = Constants.DEMO_EXPIRED_HEADER;
//				Session["ErrorExceptionType"] = null;
//				Session["ErrorStackTrace"] = null;
//
//				Response.Redirect("/RetailPlus/GenericError.aspx");
//			}
//			else
//			{
				//Check if a user is currently login

				if (Session["UID"] != null)
				{
					Login.AssignUserSession(Convert.ToInt16(Session["UID"]));
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/Home/Default.aspx");
				}
				else
				{	HorizontalNavBar.Visible = false;
					RightBodySectionSearch.Visible = false;

                    Data.Terminal clsTerminal = new Data.Terminal();
                    clsTerminal.UpdateBEVersion("2.0.1.5");
                    clsTerminal.CommitAndDispose();
				}
//			}
			
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
