namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Finance : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
                    LoadOptions();	
                    LoadRecord();	
                    //LoadItems();
                    //cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
                    //imgDelete.Attributes.Add("onClick", "return confirm_delete();");
				}
			}
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

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
        }
        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        protected void cmdSaveBack_Click(object sender, EventArgs e)
        {
            SaveRecord();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            
            DataClass clsDataClass = new DataClass();
            ChartOfAccounts clsChartOfAccount = new ChartOfAccounts();
            System.Data.DataTable dtList = clsDataClass.DataReaderToDataTable(clsChartOfAccount.List("ChartOfAccountName", SortOption.Ascending));
            clsChartOfAccount.CommitAndDispose();

            cboChartOfAccountPurchase.DataTextField = "ChartOfAccountName";
            cboChartOfAccountPurchase.DataValueField = "ChartOfAccountID";
            cboChartOfAccountPurchase.DataSource = dtList.DefaultView;
            cboChartOfAccountPurchase.DataBind();
            cboChartOfAccountPurchase.SelectedIndex = cboChartOfAccountPurchase.Items.Count - 1;

            cboChartOfAccountSold.DataTextField = "ChartOfAccountName";
            cboChartOfAccountSold.DataValueField = "ChartOfAccountID";
            cboChartOfAccountSold.DataSource = dtList.DefaultView;
            cboChartOfAccountSold.DataBind();
            cboChartOfAccountSold.SelectedIndex = cboChartOfAccountSold.Items.Count - 1;

            cboChartOfAccountInventory.DataTextField = "ChartOfAccountName";
            cboChartOfAccountInventory.DataValueField = "ChartOfAccountID";
            cboChartOfAccountInventory.DataSource = dtList.DefaultView;
            cboChartOfAccountInventory.DataBind();
            cboChartOfAccountInventory.SelectedIndex = cboChartOfAccountInventory.Items.Count - 1;

            cboChartOfAccountIDTaxPurchase.DataTextField = "ChartOfAccountName";
            cboChartOfAccountIDTaxPurchase.DataValueField = "ChartOfAccountID";
            cboChartOfAccountIDTaxPurchase.DataSource = dtList.DefaultView;
            cboChartOfAccountIDTaxPurchase.DataBind();
            cboChartOfAccountIDTaxPurchase.SelectedIndex = cboChartOfAccountIDTaxPurchase.Items.Count - 1;

            cboChartOfAccountIDTaxSold.DataTextField = "ChartOfAccountName";
            cboChartOfAccountIDTaxSold.DataValueField = "ChartOfAccountID";
            cboChartOfAccountIDTaxSold.DataSource = dtList.DefaultView;
            cboChartOfAccountIDTaxSold.DataBind();
            cboChartOfAccountIDTaxSold.SelectedIndex = cboChartOfAccountIDTaxSold.Items.Count - 1;
            
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Products clsProduct = new Products();
            ProductDetails clsDetails = clsProduct.Details(iID);

			Contacts clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
			ContactDetails clsContactDetails = clsContact.Details(clsDetails.SupplierID);

			clsProduct.CommitAndDispose();

			lblProductID.Text = clsDetails.ProductID.ToString();
			lblQuantity.Text = clsDetails.Quantity.ToString("#,##0.#0");
			lblUnitCode.Text = clsDetails.BaseUnitCode;
			lblProductCode.Text = clsDetails.ProductCode;
			lblBarcode.Text = clsDetails.BarCode;
			lblProductDesc.Text = clsDetails.ProductDesc;
			lblProductGroup.Text = clsDetails.ProductGroupCode + "/" + clsDetails.ProductGroupName;
			lblProductSubGroup.Text = clsDetails.ProductSubGroupCode + "/" + clsDetails.ProductSubGroupName;

			lblSupplierCode.Text = clsContactDetails.ContactCode.ToString();
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);	
			lblSupplierCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx" + stParam;

			lblSupplierContact.Text = clsContactDetails.BusinessName;
			lblSupplierTelephoneNo.Text = clsContactDetails.TelephoneNo;

            cboChartOfAccountPurchase.SelectedIndex = cboChartOfAccountPurchase.Items.IndexOf(cboChartOfAccountPurchase.Items.FindByValue(clsDetails.ChartOfAccountIDPurchase.ToString()));
            cboChartOfAccountSold.SelectedIndex = cboChartOfAccountSold.Items.IndexOf(cboChartOfAccountSold.Items.FindByValue(clsDetails.ChartOfAccountIDSold.ToString()));
            cboChartOfAccountInventory.SelectedIndex = cboChartOfAccountInventory.Items.IndexOf(cboChartOfAccountInventory.Items.FindByValue(clsDetails.ChartOfAccountIDInventory.ToString()));
            cboChartOfAccountIDTaxPurchase.SelectedIndex = cboChartOfAccountIDTaxPurchase.Items.IndexOf(cboChartOfAccountIDTaxPurchase.Items.FindByValue(clsDetails.ChartOfAccountIDTaxPurchase.ToString()));
            cboChartOfAccountIDTaxSold.SelectedIndex = cboChartOfAccountIDTaxSold.Items.IndexOf(cboChartOfAccountIDTaxSold.Items.FindByValue(clsDetails.ChartOfAccountIDTaxSold.ToString()));
			
		}

		private void SaveRecord()
		{
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            int iChartOfAccountIDPurchase = Convert.ToInt32(cboChartOfAccountPurchase.SelectedItem.Value);
            int iChartOfAccountIDSold = Convert.ToInt32(cboChartOfAccountSold.SelectedItem.Value);
            int iChartOfAccountIDInventory = Convert.ToInt32(cboChartOfAccountInventory.SelectedItem.Value);
            int iChartOfAccountIDTaxPurchase = Convert.ToInt32(cboChartOfAccountIDTaxPurchase.SelectedItem.Value);
            int iChartOfAccountIDTaxSold = Convert.ToInt32(cboChartOfAccountIDTaxSold.SelectedItem.Value);

            Products clsProduct = new Products();
            clsProduct.UpdateFinancialInformation(iID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
            clsProduct.CommitAndDispose();
		}

		#endregion

    }
}