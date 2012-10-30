namespace AceSoft.RetailPlus
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Expiry : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

            //if (Session.Count == 0)
            //{
            //    Response.Redirect(Constants.ROOT_DIRECTORY + "");
            //}
            //else 
            //{
            //string SerialNumber = null;
            //AceSoft.RetailPlus.Key clsKey = new AceSoft.RetailPlus.Key();
            //RegistrationType regIsDemoExpired = clsKey.IsDemoExpired(out SerialNumber);

            //if (regIsDemoExpired == RegistrationType.DEMO_Expired)
            //{
            //    Session.RemoveAll();
            //    Session["ErrorCurrentExecutionFilePath"] = Constants.ROOT_DIRECTORY + "";
            //    Session["ErrMessage"] = Constants.DEMO_EXPIRED_MESSAGE;
            //    Session["ErrorMessage"] = Constants.DEMO_EXPIRED_MESSAGE;
            //    Session["ErrorSource"] = Constants.DEMO_EXPIRED_HEADER;
            //    Session["ErrorExceptionType"] = null;
            //    Session["ErrorStackTrace"] = null;

            //    Response.Redirect(Constants.ROOT_DIRECTORY + "/GenericError.aspx");
            //}
            //else if (regIsDemoExpired == RegistrationType.Error)
            //{
            //    Session.RemoveAll();
            //    Session["ErrorCurrentExecutionFilePath"] = Constants.ROOT_DIRECTORY + "";
            //    Session["ErrMessage"] = Constants.DEMO_EXPIRED_MESSAGE;
            //    Session["ErrorMessage"] = SerialNumber;
            //    Session["ErrorSource"] = Constants.DEMO_EXPIRED_HEADER;
            //    Session["ErrorExceptionType"] = null;
            //    Session["ErrorStackTrace"] = null;

            //    Response.Redirect(Constants.ROOT_DIRECTORY + "/GenericError.aspx");
            //}
            //}
		}

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect(Constants.ROOT_DIRECTORY + "");
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
