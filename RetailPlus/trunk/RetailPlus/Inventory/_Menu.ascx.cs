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
                lnkWarehouseTransfer.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_WBranchTransfer/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
				lnkInventoryList.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkStockType.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_StockType/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkStock.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkTransferIn.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferIn/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkTransferOut.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

                lnkInvAdjustment.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("invadjustment", Session.SessionID);
                lnkInvThreshold.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("invthreshold", Session.SessionID);

                lnkInventoryAnalyst.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("inventoryanalyst", Session.SessionID);
                lnkCloseInventoryProduct.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closeinventoryproduct", Session.SessionID);
                lnkCloseInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closeinventory",Session.SessionID);
                lnkCloseInventoryDetailed.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closeinventorydetailed", Session.SessionID);
				lnkUpload.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("upload",Session.SessionID);

                lnkSynchronize.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("synchronizeinv", Session.SessionID);
                lnkExport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("exportinv", Session.SessionID);
                lnkImport.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("importinv", Session.SessionID);

                lnkInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("inventoryrep", Session.SessionID);
                lnkeInventory.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("einventoryrep", Session.SessionID);
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

            bool boShowInventory = false;
            bool boShowActionBar = false;
            bool boShowReports = false;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Branch); 
			lnkBranch.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkBranch.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryList); 
			lnkInventoryList.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkInventoryList.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.StockTypes); 
			lnkStockType.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkStockType.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.StockTransactions); 
			lnkStock.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkStock.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			lnkUpload.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkUpload.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.BranchInventoryTransfer);
            lnkBranchTransfer.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkBranchTransfer.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.WarehouseToBranchTransfer);
            lnkWarehouseTransfer.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkWarehouseTransfer.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TransferIn);
			lnkTransferIn.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkTransferIn.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TransferOut);
			lnkTransferOut.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkTransferOut.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InvAdjustment); 
			lnkInvAdjustment.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkInvAdjustment.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InvThreshold);
            lnkInvThreshold.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkInvThreshold.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowInventory) boShowInventory = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InventoryAnalyst);
            lnkInventoryAnalyst.Visible = clsDetails.Write;
            if (!clsDetails.Write) divlnkInventoryAnalyst.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowInventory = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.CloseInventory);
            lnkCloseInventory.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkCloseInventory.Style.Add("display", "none"); 
            
            lnkCloseInventoryProduct.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkCloseInventoryProduct.Style.Add("display", "none");

            lnkCloseInventoryDetailed.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkCloseInventoryDetailed.Style.Add("display", "none");

            lnkCLosingInventoryReport.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkCLosingInventoryReport.Style.Add("display", "none");
            
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SynchronizeInventoryCount);
            lnkSynchronize.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkSynchronize.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ExportInventoryCount);
            lnkExport.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkExport.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowActionBar = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ImportInventoryCount);
            lnkImport.Visible = clsDetails.Read;
            if (!clsDetails.Write) divlnkImport.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowActionBar) boShowActionBar = true;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryReport); 
			lnkInventory.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkInventory.Style.Add("display", "none");

            lnkBranchInventory.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkBranchInventory.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowReports) boShowReports = true;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.eInventoryReport); 
            lnkeInventory.Visible = clsDetails.Read;
            if (!clsDetails.Read) divlnkeInventory.Style.Add("display", "none");
            // show label for reporting
            if (clsDetails.Read && !boShowReports) boShowReports = true;

            //lnkExpiredInventory.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReorderReport); 
            //lnkReorder.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.OverStockReport); 
            //lnkOverStock.Visible = clsDetails.Read; 

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.TotalStockReport);
            //lnkTotalStock.Visible = clsDetails.Read; 

			clsAccessRights.CommitAndDispose();

            if (!boShowInventory)
            {
                divlblInventory.Style.Add("display", "none");
                divtblInventory.Style.Add("display", "none");
            }

            if (!boShowActionBar)
            {
                divlblActionBar.Style.Add("display", "none");
            }

            if (!boShowReports)
            {
                divlblReports.Style.Add("display", "none");
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
