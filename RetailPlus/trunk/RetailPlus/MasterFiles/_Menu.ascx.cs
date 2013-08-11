using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles
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

                lnkChangeRewardPoints.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("changerewardpoints", Session.SessionID);
                lnkChangeProductPrice.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("changeprice", Session.SessionID);
                lnkChangeTax.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("changetax", Session.SessionID);

                lnkSynchronize.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("synchronize", Session.SessionID);

				lnkCardType.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_CardType/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

				lnkChargeType.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ChargeType/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkVariation.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkUnit.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
                lnkProducts.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID) + "&view=" + Common.Encrypt("compacked", Session.SessionID);
                lnkProductsDetailed.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
				
				
				lnkContact.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkContactGroup.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ContactGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkDiscount.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Discount/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkProductGroup.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkProductSubGroup.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductSubGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);
				lnkPromo.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Promo/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID);

                lnkPositions.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Position/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                lnkDepartments.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Department/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);

                lnkContactDetailed.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ContactDetailed/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID);
                
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Products);
            Menu_Product.Visible = clsDetails.Read;
            lnkProducts.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductGroups);
            lnkProductGroup.Visible = Menu_Product.Visible && clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductSubGroups);
            lnkProductSubGroup.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Promos);
            lnkPromo.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Variations);
            lnkVariation.Visible = clsDetails.Read; 

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.UnitMeasurement);
            lnkUnit.Visible = clsDetails.Read; 
			
            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SynchronizeBranchProducts);
            lnkSynchronize.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.RewardPointsSetup);
            lnkChangeRewardPoints.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ChangeProductPrices);
            lnkChangeProductPrice.Visible = clsDetails.Write;
            lnkChangeTax.Visible = clsDetails.Write;

			
            // Contacts
            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ContactGroups);
            lnkContactGroup.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Contacts); 
			lnkContact.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CustomerManagement);
            lnkContactDetailed.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Position);
            lnkPositions.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Department);
            lnkDepartments.Visible = clsDetails.Read;

            // Miscellaneous
			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Discounts); 
			lnkDiscount.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CardType);
            lnkChargeType.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ChargeType);
            lnkChargeType.Visible = clsDetails.Read; 

			
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
