using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Credits
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Menu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ManageSecurity();

                lnkCardTypes.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_CardType/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkCustomers.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkGuarantors.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Guarantors/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                
                lnkBillingReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/_Customers/Default.aspx?task=" + Common.Encrypt("listwithcredits", Session.SessionID);
                lnkBillingHistory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Credits/Default.aspx?task=" + Common.Encrypt("print", Session.SessionID);

            }
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InternalCreditCardSetup);
            lnkCardTypes.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditCardIssuance);
            lnkCustomers.Visible = clsDetails.Read;
            lnkGuarantors.Visible = clsDetails.Read;
            lnkBillingReport.Visible = clsDetails.Read;
            lnkBillingHistory.Visible = clsDetails.Read;

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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
