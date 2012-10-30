namespace AceSoft.RetailPlus
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for __PageLevelError.
	/// </summary>
	public partial  class __PageLevelError : System.Web.UI.UserControl
	{
		private const string defaultMessage = "";

		private string mstMessage = defaultMessage;

		public string Message
		{
			get 
			{
				return mstMessage;
			}
			set
			{
				mstMessage = value;
			}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				lblMessage.Text = mstMessage;			
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
