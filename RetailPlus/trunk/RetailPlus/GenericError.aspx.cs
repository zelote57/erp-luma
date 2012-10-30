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
using AceSoft.RetailPlus;

namespace AceSoft.RetailPlus
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _GenericError : System.Web.UI.Page
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			HorizontalNavBar.Visible = false;
			RightBodySectionSearch.Visible = false;	

			if (!IsPostBack)
			{

				try
				{
					const string defaultHeading = "<font color=red>Error</font>";
					const string defaultTitle = Constants.RETAILPLUS_BUSINESS_SOLUTIONS;
					const SearchCategoryID defaultAllSourcesIndex = SearchCategoryID.NotApplicable;
			
					LargeHeading.Text = defaultHeading;

					SiteTitle.Title = defaultTitle;

					RightBodySectionSearch.SearchIDSelectedItem = defaultAllSourcesIndex;
							
					if (Session["ErrorMessage"]!=null || Session["ErrorMessage"].ToString() != string.Empty)
					{
						string ErrorMessage = ""; try{	ErrorMessage = Session["ErrorMessage"].ToString();} catch{}
						string ExceptionType = "";	try{	ExceptionType = Session["ErrorExceptionType"].ToString();} catch{}

						if (ErrorMessage.IndexOf("MySQLDriverCS Exception: MySQLDriverCS Error: wrong query.") != -1)
						{
							lblMessage.Text = "We're sorry, but an error has occurred.";
							lblErrorMessage.Text  = "<b>Message :</b>" + ErrorMessage.Replace("MySQLDriverCS Exception: MySQLDriverCS Error: wrong query.","");
						}
						else if (ExceptionType.IndexOf("System.NullReferenceException") != -1)
						{
							lblMessage.Text = "We're sorry, but an error has occurred.";
							lblErrorMessage.Text  = "<b>Message :</b>System.NullReferenceException, please double check the data.";
							lblStackTrace.Text = "<b>Stack Trace :</b>" + Session["ErrorStackTrace"];
						}
						else if(Session["ErrorSource"].ToString() == Constants.DEMO_EXPIRED_HEADER)
						{
							lblMessage.Text = Constants.RETAILPLUS_BUSINESS_SOLUTIONS;
							lblErrorMessage.Text  = "<b>Message :</b>" + Session["ErrorMessage"];
							lblSource.Text = "<b>Source :</b>" + Session["ErrorSource"];
							cmdContinue.Visible = false;
						}
						else
						{
							lblMessage.Text = "We're sorry, but an unhandled error has occurred. Please email the error below to <a href='mailto:admin@myretailplus.com'>admin@myretailplus.com</a>";
							lblErrorMessage.Text  = "<b>Message :</b>" + Session["ErrorMessage"];
							lblSource.Text = "<b>Source :</b>" + Session["ErrorSource"];
							lblExceptionType.Text = "<b>Exception Type  :</b>" + Session["ErrorExceptionType"];
							lblStackTrace.Text = "<b>Stack Trace :</b>" + Session["ErrorStackTrace"];
						}
					}
					else
					{
						lblMessage.Text = "We apologize, but an unrecoverable error has occurred. Please click the back button on your browser and try again.";
					}
				}
				catch (Exception ex)
				{
					lblMessage.Text = "We apologize, but an unrecoverable error has occurred. Please click the back button on your browser and try again.";

					lblErrorMessage.Text  = "<b>Message :</b>" + ex.Message;
					lblSource.Text = "<b>Source :</b>" + ex.Source;
					lblExceptionType.Text = "<b>Exception Type  :</b>" + ex.GetType().ToString();
					lblStackTrace.Text = "<b>Stack Trace :</b>" + ex.StackTrace.ToString();
				}
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
