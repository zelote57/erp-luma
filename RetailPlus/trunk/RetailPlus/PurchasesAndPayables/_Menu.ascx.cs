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
				lnkPurchaseReturns.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkPurchaseDebitMemo.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_DebitMemo/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

                lnkPurchaseAnalysis.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("purchaseanalysis", Session.SessionID) + "&reporttype=" + Common.Encrypt("By Vendor", Session.SessionID);

                lnkPostedPO.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedpo", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO", Session.SessionID);
                lnkPostedPOReturns.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedporeturns", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO Returns", Session.SessionID);
                lnkPostedDebitMemo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("posteddebitmemo", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted Debit Memo", Session.SessionID);
                
                lnkPOAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID);
                lnkInvoiceAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Payments/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID);

                //lnkPostedPurchaseOrder.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID) + "&status=" + Common.Encrypt(POStatus.Posted.ToString("d"), Session.SessionID);

                // 05Jun2015
                lnkPurchaseOrdereSales.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx?task=" + Common.Encrypt("listesales", Session.SessionID);
                lnkPurchaseReturnseSales.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx?task=" + Common.Encrypt("listesales", Session.SessionID);
                lnkPurchaseDebitMemoeSales.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_DebitMemo/Default.aspx?task=" + Common.Encrypt("listesales", Session.SessionID);

                lnkPurchaseAnalysiseSales.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("purchaseanalysisesales", Session.SessionID) + "&reporttype=" + Common.Encrypt("By Vendor", Session.SessionID);

                lnkPostedPOeSales.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedpoesales", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO", Session.SessionID);
                lnkPostedPOReturnseSales.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("postedporeturnsesales", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted PO Returns", Session.SessionID);
                lnkPostedDebitMemoeSales.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("posteddebitmemoesales", Session.SessionID) + "&reporttype=" + Common.Encrypt("Posted Debit Memo", Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            bool boShowFinancialReports = false;
            bool boShoweSalesReports = false;
            bool boShowActionBar = false;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Contacts);
            if (!clsDetails.Read) divlnkVendors.Style.Add("display", "none");
            lnkVendors.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchaseOrders);
            lnkPurchaseOrders.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkPurchaseOrders.Style.Add("display", "none");

            lnkPostedPO.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkPostedPurchaseOrder.Style.Add("display", "none");

            if (!clsDetails.Read) divlnkPostedPO.Style.Add("display", "none");

            lnkPOAdd.Visible = clsDetails.Write;
            if (!clsDetails.Write) divlnkPOAdd.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShowFinancialReports) boShowFinancialReports = true;
            if (clsDetails.Write && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseReturns);
            lnkPurchaseReturns.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseReturns.Style.Add("display", "none");

            lnkPostedPOReturns.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPostedPOReturns.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShowFinancialReports) boShowFinancialReports = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseDebitMemo);
            lnkPurchaseDebitMemo.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseDebitMemo.Style.Add("display", "none");

            lnkPostedDebitMemo.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPostedDebitMemo.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShowFinancialReports) boShowFinancialReports = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseAnalysis);
            lnkPurchaseAnalysis.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseAnalysis.Style.Add("display", "none");

            // esales access
            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseOrderseSales);
            lnkPurchaseOrdereSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseOrdereSales.Style.Add("display", "none");

            lnkPostedPOeSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPostedPOeSales.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShoweSalesReports) boShoweSalesReports = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseReturnseSales);
            lnkPurchaseReturnseSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseReturnseSales.Style.Add("display", "none");

            lnkPostedPOReturnseSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPostedPOReturnseSales.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShoweSalesReports) boShoweSalesReports = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseDebitMemoeSales);
            lnkPurchaseDebitMemoeSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseDebitMemoeSales.Style.Add("display", "none");

            lnkPostedDebitMemoeSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPostedDebitMemoeSales.Style.Add("display", "none");

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PurchaseAnalysiseSales);
            lnkPurchaseAnalysiseSales.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkPurchaseAnalysiseSales.Style.Add("display", "none");

            // show label for reporting
            if (clsDetails.Write && !boShoweSalesReports) boShoweSalesReports = true;

			clsAccessRights.CommitAndDispose();

            // show the actual label for reporting
            if (!boShowFinancialReports)
            {
                divlblFinancialReports.Style.Add("display", "none");
                divtblFinancialReports.Style.Add("display", "none");
            }

            // show the actual label for reporting
            if (!boShoweSalesReports)
            {
                divlbleSalesReports.Style.Add("display", "none");
                divtbleSalesReports.Style.Add("display", "none");
            }

            // show the actual label for reporting
            if (!boShowActionBar)
            {
                divlblActionBar.Style.Add("display", "none");
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
