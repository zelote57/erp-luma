namespace AceSoft.RetailPlus
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// </summary>
	public partial  class ___HorizontalNavBarCorner : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			lnkMyAccount.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessUser/Default.aspx?task=" + Common.Encrypt("custom", Session.SessionID);

			//Check if a user is logged inl if not then hide the links
			if (Session.Count == 0)
			{
				lnkMyAccount.Visible = false;
				cmdLogout.Visible = false;
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

		protected void cmdLogout_Click(object sender, System.EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect(Constants.ROOT_DIRECTORY + "/Default.aspx");
		}
	}
}
