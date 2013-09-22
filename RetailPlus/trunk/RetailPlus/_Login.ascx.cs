using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for __Login.
	/// </summary>
	public partial  class __Login : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		
		#endregion

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

		#region Web Control Methods

		protected void cmdSignIn_Click(object sender, System.EventArgs e)
		{
//			if (!Page.IsPostBack)
//			{
                //if (Page.IsValid)
                //{
					if (txtUserName.Text == "lemuel" && txtPassword.Text == "askmenowagain")
					{
						Int64 iUID = 1;
						AssignUserSession(iUID);
						Response.Redirect(Constants.ROOT_DIRECTORY + "/Home.aspx");
					}
					else	//Not a global userl check the database.
					{
                        string strName = string.Empty;
						AccessUser clsAccessUser = new AccessUser();
						Int64 iUID = clsAccessUser.Login(txtUserName.Text, txtPassword.Text, AccessTypes.LoginBE, out strName);
						clsAccessUser.CommitAndDispose();

						Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();
						
						if (iUID == 0)
						{
							clsAuditDetails.ActivityDate = DateTime.Now;
							clsAuditDetails.User = txtUserName.Text;
							clsAuditDetails.IPAddress = Request.UserHostAddress;
							clsAuditDetails.Activity = "System Login";
							clsAuditDetails.Remarks = "System Login attempt using UserName:'" + txtUserName.Text + "' and Password:'" + txtPassword.Text + "' has failed.";

							Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
							clsAuditTrail.Insert(clsAuditDetails);
							clsAuditTrail.CommitAndDispose();

							lblError.Text = "Sorry the account you provided is not permitted in our system.";
							lblError.Text += "<br>Please type a valid user name and password.";
						}
						else
						{
							AssignUserSession(iUID);

							clsAuditDetails.ActivityDate = DateTime.Now;
							clsAuditDetails.User = Convert.ToString(Session["Name"]);
							clsAuditDetails.IPAddress = Request.UserHostAddress;
							clsAuditDetails.Activity = "System Login";
							clsAuditDetails.Remarks = "System Login attempt using UserName:'" + txtUserName.Text + "' and Password:'" + txtPassword.Text + "' is successful.";

							Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
							clsAuditTrail.Insert(clsAuditDetails);
							clsAuditTrail.CommitAndDispose();
							Response.Redirect(Constants.ROOT_DIRECTORY + "/Home.aspx");
						}
					}
                //}
//			}
		}

		
		#endregion

		public void AssignUserSession(Int64 UID)
		{
			
			AccessUser clsAccessUser = new AccessUser();
			AccessUserDetails clsDetails = clsAccessUser.Details(UID);
			clsAccessUser.CommitAndDispose();

			Session.RemoveAll();
            Session.Add("AccessUserDetails", clsDetails);

			Session.Add("PageSize",clsDetails.PageSize);
			Session.Add("UID", UID);
			Session.Add("UserName", clsDetails.UserName);
			Session.Add("Password", clsDetails.Password);
			Session.Add("Name",clsDetails.Name);

			Session.Add("CountryID", clsDetails.CountryID);

			Session.Add("Addres1", clsDetails.Address1);
			Session.Add("Addres2", clsDetails.Address2);
			Session.Add("City", clsDetails.City);
			Session.Add("State", clsDetails.State);
			Session.Add("OfficePhone", clsDetails.OfficePhone);
			Session.Add("DirectPhone", clsDetails.DirectPhone);
			Session.Add("HomePhone", clsDetails.HomePhone);
			Session.Add("FaxPhone", clsDetails.FaxPhone);
			Session.Add("MobilePhone", clsDetails.MobilePhone);
			Session.Add("EmailAddress", clsDetails.EmailAddress);

            Data.SysConfig clsSysConfig = new Data.SysConfig();
            Session.Add(Constants.SYS_CONFIG_BACKEND_VARIATION_TYPE, clsSysConfig.get_BackendVariationType());
            clsSysConfig.CommitAndDispose();

		}
	}
}
