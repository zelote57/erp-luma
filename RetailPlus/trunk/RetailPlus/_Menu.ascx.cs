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
	///		Summary description for __HomeMenu.
	/// </summary>
	public partial  class __Menu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ManageSecurity();

                lnkUpload.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("upload", Session.SessionID);
                lnkSynchronize.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("synchronize", Session.SessionID);

                lnkProducts.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkProductAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID);

                lnkContact.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkRewards.NavigateUrl = Constants.ROOT_DIRECTORY + "/Rewards/_Members/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);

                lnkInventoryList.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkStock.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);

                lnkAccessUserAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessUser/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID);
                lnkReceiptFormatEdit.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Reports/_Format/Default.aspx?task=" + Common.Encrypt("edit", Session.SessionID);
			}
		}

		private void ManageSecurity()
		{
            Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.Products); 
			lnkProducts.Visible = clsDetails.Read; 
			lnkProductAdd.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (int)AccessTypes.SynchronizeBranchProducts);
            lnkSynchronize.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.Contacts); 
			lnkContact.Visible = clsDetails.Read;
            lnkRewards.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.InventoryList); 
			lnkInventoryList.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.StockTransactions); 
			lnkStock.Visible = clsDetails.Read; 
			lnkUpload.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.AccessUsers); 
			lnkAccessUserAdd.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(clsAccessUserDetails.UID, (Int16)AccessTypes.ReportFormat); 
			lnkReceiptFormatEdit.Visible = clsDetails.Write; 

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
