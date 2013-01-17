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
	///		Summary description for __HorizontalNavBar.
	/// </summary>
	public partial  class __HorizontalNavBar : System.Web.UI.UserControl
	{
		private const HorizontalNavID defaultPageNavigatorID = HorizontalNavID.Login;
		private HorizontalNavID mPageNavigatorID = defaultPageNavigatorID;
		
		public HorizontalNavID PageNavigatorid
		{
			get
			{
				return mPageNavigatorID;
			}
			set
			{
				mPageNavigatorID = value;
			}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				SwitchPageNavigator(mPageNavigatorID);
				if (Convert.ToInt64(Session["UID"]) != 0)
					{
					ManageSecurity();
				}
			}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Home); 
			NavHome.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.MasterFilesMenu); 
			NavMasterFiles.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.RewardCardIssuance);
            NavRewards.Visible = clsDetails.Read; 

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CreditCardIssuance);
            NavCredits.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.GeneralLedgerMenu); 
			NavGeneralLedger.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PurchasesAndPayablesMenu); 
			NavPurchasesAndPayables.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SalesAndReceivablesMenu); 
			NavSalesAndReceivables.Visible = clsDetails.Read;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.InventoryMenu); 
			NavInventory.Visible = clsDetails.Read;			

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ReportsMenu); 
			NavReports.Visible = clsDetails.Read; 

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AdministrationFilesMenu); 
			NavAdministrationFiles.Visible = clsDetails.Read; 

			clsAccessRights.CommitAndDispose();
		}

		private void SwitchPageNavigator(HorizontalNavID ID)
		{
			switch(ID)
			{
				case HorizontalNavID.Home:				
					NavHome.Attributes.Add("class","Ms-phnavmidc0Sel");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				case HorizontalNavID.MasterFiles:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc0Sel");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
                case HorizontalNavID.Rewards:
                    NavHome.Attributes.Add("class", "Ms-phnavmidc1");
                    NavMasterFiles.Attributes.Add("class", "Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc0Sel");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
                    NavGeneralLedger.Attributes.Add("class", "Ms-phnavmidc1");
                    NavPurchasesAndPayables.Attributes.Add("class", "Ms-phnavmidc1");
                    NavSalesAndReceivables.Attributes.Add("class", "Ms-phnavmidc1");
                    NavInventory.Attributes.Add("class", "Ms-phnavmidc1");
                    NavReports.Attributes.Add("class", "Ms-phnavmidc1");
                    NavAdministrationFiles.Attributes.Add("class", "Ms-phnavmidc1");
                    break;
                case HorizontalNavID.Credits:
                    NavHome.Attributes.Add("class", "Ms-phnavmidc1");
                    NavMasterFiles.Attributes.Add("class", "Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc0Sel");
                    NavGeneralLedger.Attributes.Add("class", "Ms-phnavmidc1");
                    NavPurchasesAndPayables.Attributes.Add("class", "Ms-phnavmidc1");
                    NavSalesAndReceivables.Attributes.Add("class", "Ms-phnavmidc1");
                    NavInventory.Attributes.Add("class", "Ms-phnavmidc1");
                    NavReports.Attributes.Add("class", "Ms-phnavmidc1");
                    NavAdministrationFiles.Attributes.Add("class", "Ms-phnavmidc1");
                    break;
				case HorizontalNavID.GeneralLedger:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc0Sel");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				case HorizontalNavID.PurchasesAndPayables:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc0Sel");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				case HorizontalNavID.SalesAndReceivables:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc0Sel");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				case HorizontalNavID.Inventory:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc0Sel");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				case HorizontalNavID.AdministrationFiles:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc0Sel");
					break;
				case HorizontalNavID.Reports:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc0Sel"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;
				default:
					NavHome.Attributes.Add("class","Ms-phnavmidc1");
					NavMasterFiles.Attributes.Add("class","Ms-phnavmidc1");
                    NavRewards.Attributes.Add("class", "Ms-phnavmidc1");
                    NavCredits.Attributes.Add("class", "Ms-phnavmidc1");
					NavGeneralLedger.Attributes.Add("class","Ms-phnavmidc1");
					NavPurchasesAndPayables.Attributes.Add("class","Ms-phnavmidc1");
					NavSalesAndReceivables.Attributes.Add("class","Ms-phnavmidc1");
					NavInventory.Attributes.Add("class","Ms-phnavmidc1");
					NavReports.Attributes.Add("class","Ms-phnavmidc1"); 
					NavAdministrationFiles.Attributes.Add("class","Ms-phnavmidc1");
					break;				
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
