using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Inventory
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

				lnkBranch.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
                lnkBranchTransfer.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_BranchTransfer/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
				lnkInventoryList.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkStockType.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_StockType/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkStock.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkTransferIn.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferIn/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkTransferOut.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

                lnkInvAdjustment.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("invadjustment", Session.SessionID);

                lnkInventoryAnalyst.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("inventoryanalyst", Session.SessionID);
				lnkCloseInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closeinventory",Session.SessionID);
                lnkCloseInventoryDetailed.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closeinventorydetailed", Session.SessionID);
				lnkUpload.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("upload",Session.SessionID);

                lnkSynchronize.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("synchronizeinv", Session.SessionID);
                lnkExport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("exportinv", Session.SessionID);
                lnkImport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("importinv", Session.SessionID);

                lnkInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("inventoryrep", Session.SessionID);
                lnkBranchInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("inventoryperbranchrep", Session.SessionID);
                //lnkExpiredInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("expiredinventoryrep", Session.SessionID);
                //lnkReorder.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("itemsforreorderrep",Session.SessionID);
                //lnkOverStock.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("overstockrep",Session.SessionID);
                //lnkTotalStock.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("totalstockrep", Session.SessionID);

                lnkCLosingInventoryReport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Branch); 
			lnkBranch.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryList); 
			lnkInventoryList.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.StockTypes); 
			lnkStockType.Visible = clsDetails.Read;
 
			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.StockTransactions); 
			lnkStock.Visible = clsDetails.Read; 
			lnkUpload.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TransferIn);
			lnkTransferIn.Visible = clsDetails.Read;
            lnkBranchTransfer.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TransferOut);
			lnkTransferOut.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InvAdjustment); 
			lnkInvAdjustment.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InventoryAnalyst);
            lnkInventoryAnalyst.Visible = false;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CloseInventory);
            lnkCloseInventory.Visible = clsDetails.Read;
            lnkCloseInventoryDetailed.Visible = clsDetails.Read;
            lnkCLosingInventoryReport.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SynchronizeInventoryCount);
            lnkSynchronize.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ExportInventoryCount);
            lnkExport.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ImportInventoryCount);
            lnkImport.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryReport); 
			lnkInventory.Visible = clsDetails.Read;

            lnkBranchInventory.Visible = clsDetails.Read; 

            //lnkExpiredInventory.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReorderReport); 
            //lnkReorder.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.OverStockReport); 
            //lnkOverStock.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TotalStockReport);
            //lnkTotalStock.Visible = clsDetails.Read; 

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
