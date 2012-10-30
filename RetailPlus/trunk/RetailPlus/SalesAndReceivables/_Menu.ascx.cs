using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.SalesAndReceivables
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

				lnkCustomers.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Customer/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkSalesOrders.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SO/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkSalesJournals.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SalesJournals/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkSalesReturns.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Returns/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkCreditMemos.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_CreditMemo/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
                lnkPostedSalesOrders.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SO/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID) + "&status=" + Common.Encrypt(SOStatus.Posted.ToString("d"), Session.SessionID);

                lnkPostedSO.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedso", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted SO", Session.SessionID);
                lnkPostedSOReturns.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedsoreturns", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted SO Returns", Session.SessionID);
                lnkPostedCreditMemo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedcreditmemo", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted Credit Memo", Session.SessionID);
                lnkSalesAnalysis.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("salesanalysis", Session.SessionID) + "&reporttype=" + Common.Encrypt("By Vendor", Session.SessionID);

				lnkSOAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_SO/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);
				//lnkInvoiceAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Payments/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Contacts); 
			lnkCustomers.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesOrders); 
			lnkSalesOrders.Visible = clsDetails.Read;
            lnkPostedSalesOrders.Visible = clsDetails.Read;
            lnkSalesAnalysis.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesJournals); 
			lnkSalesJournals.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesReturns); 
			lnkSalesReturns.Visible = clsDetails.Read;
            lnkPostedSOReturns.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesCreditMemos); 
			lnkCreditMemos.Visible = clsDetails.Read;
            lnkPostedCreditMemo.Visible = clsDetails.Read; 	

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
