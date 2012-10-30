using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                lblReferrer.Text = Request.UrlReferrer.ToString();
				if (Visible)
				{
                    ManageSecurity();
					LoadOptions();
					LoadRecord();
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
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgProductHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void cmdProductHistory_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgProductPriceHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void cmdProductPriceHistory_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgChangePrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdChangePrice_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void imgEditNow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdEditNow_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
		protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupCode = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
            clsSearchKeys.ProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);

            ProductSubGroup clsSubGroup = new ProductSubGroup();
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0, System.Data.SqlClient.SortOrder.Ascending, 0, ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder.Ascending);
            cboProductSubGroup.DataBind();
            cboProductSubGroup.Items.Insert(0, new ListItem(Constants.ALL,Constants.ZERO_STRING));
            cboProductSubGroup.SelectedIndex = 0;
            clsSubGroup.CommitAndDispose();

			cboProductSubGroup_SelectedIndexChanged(sender, e);
		}
		protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboProductSubGroup.Items.Count != 0)
			{
				ProductSubGroup clsProductSubGroup = new ProductSubGroup();
				ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(Convert.ToInt32(cboProductSubGroup.SelectedItem.Value));
				cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf( cboProductUnit.Items.FindByValue(clsProductSubGroupDetails.BaseUnitID.ToString()));
				txtProductPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
				clsProductSubGroup.CommitAndDispose();	
			}
		}
        protected void imgInvAdjustment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("invadjustment", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
        }
        protected void cmdInvAdjustment_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("invadjustment", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(txtProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
        }

		#endregion

		#region Private Methods

        private void ManageSecurity()
        {
            long UID =long.Parse(Session["UID"].ToString());
            Security.AccessRights clsAccessRights = new Security.AccessRights();
            Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductsListReport);
            lblSeparator1.Visible = clsDetails.Write; imgProductHistory.Visible = clsDetails.Write; cmdProductHistory.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PricesReport);
            lblSeparator2.Visible = clsDetails.Write; imgProductPriceHistory.Visible = clsDetails.Write; cmdProductPriceHistory.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ChangePrice);
            lblSeparator3.Visible = clsDetails.Write; imgChangePrice.Visible = clsDetails.Write; cmdChangePrice.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Products);
            lblSeparator4.Visible = clsDetails.Write; imgEditNow.Visible = clsDetails.Write; cmdEditNow.Visible = clsDetails.Write;

            clsAccessRights.CommitAndDispose();
        }
		private void LoadOptions()
		{
            //DataClass clsDataClass = new DataClass();
            //Common Common = new Common();
            //long iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));

            //ProductGroup clsProductGroup = new ProductGroup();
            //cboProductGroup.DataTextField = "ProductGroupName";
            //cboProductGroup.DataValueField = "ProductGroupID";
            //cboProductGroup.DataSource = clsDataClass.DataReaderToDataTable(clsProductGroup.List("ProductGroupName",SortOption.Ascending)).DefaultView;
            //cboProductGroup.DataBind();
            //cboProductGroup.SelectedIndex = cboProductGroup.Items.Count - 1;

            //ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsProductGroup.Connection, clsProductGroup.Transaction);
            //cboProductSubGroup.DataTextField = "ProductSubGroupName";
            //cboProductSubGroup.DataValueField = "ProductSubGroupID";
            //cboProductSubGroup.DataSource = clsDataClass.DataReaderToDataTable(clsProductSubGroup.List("ProductSubGroupName",SortOption.Ascending)).DefaultView;
            //cboProductSubGroup.DataBind();
            //cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;

            //UnitMeasurements clsUnit = new UnitMeasurements(clsProductGroup.Connection, clsProductGroup.Transaction);
            //cboProductUnit.DataTextField = "UnitName";
            //cboProductUnit.DataValueField = "UnitID";
            //cboProductUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List("UnitName",SortOption.Ascending)).DefaultView;
            //cboProductUnit.DataBind();
            //cboProductUnit.SelectedIndex = cboProductUnit.Items.Count - 1;
            //clsUnit.CommitAndDispose();
	
            //Contact clsContact = new Contact(clsProductGroup.Connection , clsProductGroup.Transaction);
            //cboSupplier.DataTextField = "ContactName";
            //cboSupplier.DataValueField = "ContactID";			
            //cboSupplier.DataSource = clsDataClass.DataReaderToDataTable(clsContact.Suppliers(null,0,"ContactID",SortOption.Ascending)).DefaultView;
            //cboSupplier.DataBind();
            //cboSupplier.SelectedIndex = cboSupplier.Items.Count - 1;

            ////ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductGroup.Connection, clsProductGroup.Transaction);
            ////ProductUnitsMatrixDetails clsUnitDetails = clsUnitMatrix.LastDetails(iID);

            ////if (clsUnitDetails.BottomUnitName == null)
            ////{
            ////    cboProductUnit.Enabled = true;
            ////}
            //clsProductGroup.CommitAndDispose();	
		}
		private void LoadRecord()
		{
			Common Common = new Common();
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Product clsProduct = new Product();
			ProductDetails clsDetails = clsProduct.Details(iID);
			clsProduct.CommitAndDispose();

			lblProductID.Text = clsDetails.ProductID.ToString();
			txtProductCode.Text = clsDetails.ProductCode;
			txtBarcode.Text = clsDetails.BarCode;
            txtBarcode2.Text = clsDetails.BarCode2;
            txtBarcode3.Text = clsDetails.BarCode3;
			txtProductDesc.Text = clsDetails.ProductDesc;
            cboProductGroup.Items.Add(new ListItem(clsDetails.ProductGroupName, clsDetails.ProductGroupID.ToString()));
            cboProductSubGroup.Items.Add(new ListItem(clsDetails.ProductSubGroupName, clsDetails.ProductSubGroupID.ToString()));
			// cboProductGroup.SelectedIndex = cboProductGroup.Items.IndexOf(cboProductGroup.Items.FindByValue(clsDetails.ProductGroupID.ToString()));
			//cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.IndexOf(cboProductSubGroup.Items.FindByValue(clsDetails.ProductSubGroupID.ToString()));
			txtProductDesc.Text = clsDetails.ProductDesc;
            cboProductUnit.Items.Add(new ListItem(clsDetails.BaseUnitName, clsDetails.BaseUnitID.ToString()));
			//cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(cboProductUnit.Items.FindByValue(clsDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
            txtWSPrice.Text = clsDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
            txtPercentageCommision.Text = clsDetails.PercentageCommision.ToString("#,##0.#0");
            decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");

            decMargin = clsDetails.WSPrice - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtWSPriceMarkUp.Text = decMargin.ToString("#,##0.#0");

            if (clsDetails.IncludeInSubtotalDiscount == 0)
				chkIncludeInSubtotalDiscount.Checked = false;
			else
				chkIncludeInSubtotalDiscount.Checked = true;
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
			txtQuantity.Text = clsDetails.Quantity.ToString("#,##0.#0");
			txtMinThreshold.Text = clsDetails.MinThreshold.ToString("#,##0.#0");
			txtMaxThreshold.Text = clsDetails.MaxThreshold.ToString("#,##0.#0");
			cboSupplier.Items.Add(new ListItem(clsDetails.SupplierName, clsDetails.SupplierID.ToString()));
            //cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(clsDetails.SupplierID.ToString()));
            chkIsItemSold.Checked = clsDetails.IsItemSold;

            txtRID.Text = clsDetails.RID.ToString("###0");
		}
		private void SaveRecord()
		{
			ProductDetails clsDetails = new ProductDetails();

			clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.ProductCode  = txtProductCode.Text;
			clsDetails.BarCode  = txtBarcode.Text;
			clsDetails.ProductDesc = txtProductDesc.Text;
			clsDetails.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value); 
			clsDetails.ProductSubGroupID  = Convert.ToInt64(cboProductSubGroup.SelectedItem.Value);
			clsDetails.ProductDesc  = txtProductDesc.Text;
			clsDetails.BaseUnitID  = Convert.ToInt32(cboProductUnit.SelectedItem.Value); 
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.IncludeInSubtotalDiscount = Convert.ToInt16(chkIncludeInSubtotalDiscount.Checked);
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsDetails.MinThreshold = Convert.ToDecimal(txtMinThreshold.Text);
			clsDetails.MaxThreshold = Convert.ToDecimal(txtMaxThreshold.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value); 

			Product clsProduct = new Product();
			clsProduct.Update(clsDetails);
			clsProduct.CommitAndDispose();
		}

		#endregion

    }
}
