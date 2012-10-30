using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.PurchasesAndPayables
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Menu : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.HyperLink lnkProductUnit;
		protected System.Web.UI.WebControls.HyperLink lnkProductUnitAdd;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ManageSecurity();

				lnkVendors.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkPurchaseOrders.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
                lnkPostedPurchaseOrder.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID) + "&status=" + Common.Encrypt(POStatus.Posted.ToString("d"), Session.SessionID);
				lnkPurchaseReturns.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkPurchaseDebitMemo.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_DebitMemo/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkPostedPO.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedpo",Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO",Session.SessionID);
				lnkPostedPOReturns.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedporeturns",Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO Returns",Session.SessionID);
				lnkPostedDebitMemo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("posteddebitmemo",Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted Debit Memo",Session.SessionID);
				lnkPurchaseAnalysis.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("purchaseanalysis",Session.SessionID) + "&reporttype=" + Common.Encrypt("By Vendor",Session.SessionID);

				lnkPOAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);
				lnkInvoiceAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Payments/Default.aspx?task=" + Common.Encrypt("add",Session.SessionID);
			
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Contacts); 
			lnkVendors.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseOrders);
            lnkPostedPurchaseOrder.Visible = clsDetails.Read;
			lnkPurchaseOrders.Visible = clsDetails.Read; 
			lnkPOAdd.Visible = clsDetails.Write; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseAnalysis); 
			lnkPurchaseAnalysis.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseReturns); 
			lnkPurchaseReturns.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseDebitMemo); 
			lnkPurchaseDebitMemo.Visible = clsDetails.Read;

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
