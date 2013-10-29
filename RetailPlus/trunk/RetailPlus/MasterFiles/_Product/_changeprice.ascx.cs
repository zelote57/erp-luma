using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	public partial  class __ChangePrice : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
			    LoadOptions();			
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

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cboProductCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int intProductBaseUnitID = 0;
            decimal decCommision = 0;

            if (cboProductCode.SelectedItem.Value != Constants.ZERO_STRING)
            {
                Products clsProduct = new Products();
                try
                {
                    ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(cboProductCode.SelectedValue));
                    intProductBaseUnitID = clsDetails.BaseUnitID; decCommision = clsDetails.PercentageCommision;
                }
                catch { }

                txtProductCode.ToolTip = intProductBaseUnitID.ToString();
                lblProductID.ToolTip = decCommision.ToString();
                long ProductID = Convert.ToInt64(cboProductCode.SelectedValue);

                ProductPackage clsProductPackage = new ProductPackage(clsProduct.Connection, clsProduct.Transaction);
                lstProductPackages.DataSource = clsProductPackage.ListAsDataTable(Convert.ToInt64(cboProductCode.SelectedValue)).DefaultView;
                lstProductPackages.DataBind();

                ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(clsProduct.Connection, clsProduct.Transaction);
                System.Data.DataTable dtProductPurchasePriceHistory = clsProductPurchasePriceHistory.ListAsDataTable(Convert.ToInt64(cboProductCode.SelectedValue), "PurchasePrice", SortOption.Ascending);
                clsProduct.CommitAndDispose();

                string strPurchasePriceHistory = string.Empty;
                foreach (System.Data.DataRow dr in dtProductPurchasePriceHistory.Rows)
                {
                    DateTime dtePurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                    decimal decPurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                    string strSupplierName = "" + dr["PurchaserName"].ToString();
                    string strPurchaserName = "" + dr["SupplierName"].ToString();

                    strPurchasePriceHistory += dtePurchaseDate.ToString("ddMMMyyyy HH:mm") + ": " + strPurchaserName.PadRight(50) + " - " + decPurchasePrice.ToString("#,##0.#0").PadLeft(10) + " " + strSupplierName + "\r\n<br />" + Environment.NewLine;
                }
                lblPurchasePriceHistory.Text = "<br /><b>PURCHASE PRICE HISTORY: </b><br /><br />" + strPurchasePriceHistory;

                lblProductPackage.Visible = true;
                lnkProductPackageAdd.Visible = true;
                lstProductPackages.Visible = true;
                imgProductHistory.Visible = true;
                imgProductPriceHistory.Visible = true;
                imgInventoryAdjustment.Visible = true;
                imgEditNow.Visible = true;
                lnkProductPackageAdd.ToolTip = "Add new package for " + cboProductCode.SelectedItem.Text;
                lnkProductPackageAdd.NavigateUrl = "_Package/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID) + "&prodid=" + Common.Encrypt(cboProductCode.SelectedValue, Session.SessionID) + "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
                txtProductCode.Text = cboProductCode.SelectedItem.Text;
                lblPurchasePriceHistory.Visible = true;
            }
            else if (cboProductCode.SelectedItem.Text == Constants.ZERO_STRING)
            {
                lblProductPackage.Visible = false;
                lnkProductPackageAdd.Visible = false;
                lstProductPackages.Visible = false;
                imgProductHistory.Visible = false;
                imgProductPriceHistory.Visible = false;
                imgInventoryAdjustment.Visible = false;
                imgEditNow.Visible = false;
                lblPurchasePriceHistory.Visible = false;
                txtProductCode.ToolTip = intProductBaseUnitID.ToString();
                lblProductID.ToolTip = decCommision.ToString(); 
            }
           
        }
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataClass clsDataClass = new DataClass();

            Data.ProductColumns clsProductColumns = new Data.ProductColumns();
            clsProductColumns.ProductID = true;
            clsProductColumns.ProductCode = true;

            string strSearchKey = txtProductCode.Text.Trim();
            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = strSearchKey;
            clsSearchKeys.BarCode2 = strSearchKey;
            clsSearchKeys.BarCode3 = strSearchKey;
            clsSearchKeys.ProductCode = strSearchKey;

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ListAsDataTable(clsSearchKeys: clsSearchKeys, Limit: 100).DefaultView;
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0)
                cboProductCode.Items.Add(new ListItem("No product", "0"));

            cboProductCode.SelectedIndex = 0;

            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void lstProductPackages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkProductPackageID = (HtmlInputCheckBox)e.Item.FindControl("chkProductPackageID");
                chkProductPackageID.Value = dr["PackageID"].ToString();

                Label lblProductDesc = (Label)e.Item.FindControl("lblProductDesc");
                lblProductDesc.Text = dr["ProductDesc"].ToString();

                Label lblProductPackageID = (Label)e.Item.FindControl("lblProductPackageID");
                lblProductPackageID.Text = dr["PackageID"].ToString();

                Label lblUnitName = (Label)e.Item.FindControl("lblUnitName");
                lblUnitName.Text = dr["UnitName"].ToString();
                lblUnitName.ToolTip = dr["UnitID"].ToString();

                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                txtQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

                TextBox txtPurchasePrice = (TextBox)e.Item.FindControl("txtPurchasePrice");
                txtPurchasePrice.Text = Convert.ToDecimal(dr["PurchasePrice"].ToString()).ToString("#,##0.#0");

                TextBox txtSellingPrice = (TextBox)e.Item.FindControl("txtSellingPrice");
                txtSellingPrice.Text = Convert.ToDecimal(dr["Price"].ToString()).ToString("#,##0.#0");

                decimal decMargin = Convert.ToDecimal(dr["Price"].ToString()) - Convert.ToDecimal(dr["PurchasePrice"].ToString());
                try { decMargin = decMargin / Convert.ToDecimal(dr["PurchasePrice"].ToString()); }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                TextBox txtMargin = (TextBox)e.Item.FindControl("txtMargin");
                txtMargin.Text = decMargin.ToString("#,##0.#0");

                TextBox txtWSPrice = (TextBox)e.Item.FindControl("txtWSPrice");
                txtWSPrice.Text = Convert.ToDecimal(dr["WSPrice"].ToString()).ToString("#,##0.#0");

                decMargin = Convert.ToDecimal(dr["WSPrice"].ToString()) - Convert.ToDecimal(dr["PurchasePrice"].ToString());
                try { decMargin = decMargin / Convert.ToDecimal(dr["PurchasePrice"].ToString()); }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                TextBox txtWSPriceMarkUp = (TextBox)e.Item.FindControl("txtWSPriceMarkUp");
                txtWSPriceMarkUp.Text = decMargin.ToString("#,##0.#0");

                TextBox txtCommision = (TextBox)e.Item.FindControl("txtCommision");
                txtCommision.Text = lblProductID.ToolTip;

                Label lblVAT = (Label)e.Item.FindControl("lblVAT");
                lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0");

                Label lblEVAT = (Label)e.Item.FindControl("lblEVAT");
                lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0");

                Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0");

                TextBox txtBarCode1 = (TextBox)e.Item.FindControl("txtBarCode1");
                txtBarCode1.Text = dr["BarCode1"].ToString();

                TextBox txtBarCode2 = (TextBox)e.Item.FindControl("txtBarCode2");
                txtBarCode2.Text = dr["BarCode2"].ToString();

                TextBox txtBarCode3 = (TextBox)e.Item.FindControl("txtBarCode3");
                txtBarCode3.Text = dr["BarCode3"].ToString();

                ImageButton cmdDelProductPackage = (ImageButton)e.Item.FindControl("cmdDelProductPackage");
                ImageButton cmdPrintShelvesBarCode1 = (ImageButton)e.Item.FindControl("cmdPrintShelvesBarCode1");
                ImageButton cmdPrintShelvesBarCode2 = (ImageButton)e.Item.FindControl("cmdPrintShelvesBarCode2");
                ImageButton cmdPrintShelvesBarCode3 = (ImageButton)e.Item.FindControl("cmdPrintShelvesBarCode3");
                if (txtQuantity.Text == "1.00" && dr["UnitID"].ToString() == txtProductCode.ToolTip)
                {
                    cmdDelProductPackage.Enabled = false;
                    cmdDelProductPackage.ToolTip = string.Empty;
                    cmdDelProductPackage.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }

                //if (txtBarCode1.Text == string.Empty) { cmdPrintShelvesBarCode1.Enabled = false; } else { cmdPrintShelvesBarCode1.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode1.Text; }
                //if (txtBarCode2.Text == string.Empty) { cmdPrintShelvesBarCode2.Enabled = false; } else { cmdPrintShelvesBarCode2.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode2.Text; }
                //if (txtBarCode3.Text == string.Empty) { cmdPrintShelvesBarCode3.Enabled = false; } else { cmdPrintShelvesBarCode3.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode3.Text; }

                if (txtBarCode1.Text == string.Empty) { cmdPrintShelvesBarCode1.Enabled = false; cmdPrintShelvesBarCode1.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif"; } else { cmdPrintShelvesBarCode1.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode1.Text; }
                if (txtBarCode2.Text == string.Empty) { cmdPrintShelvesBarCode2.Enabled = false; cmdPrintShelvesBarCode2.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif"; } else { cmdPrintShelvesBarCode2.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode2.Text; }
                if (txtBarCode3.Text == string.Empty) { cmdPrintShelvesBarCode3.Enabled = false; cmdPrintShelvesBarCode3.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif"; } else { cmdPrintShelvesBarCode3.ToolTip = "Print Shelves Tag using barcode: " + txtBarCode3.Text; }

            }
        }
        protected void lstProductPackages_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            TextBox txtSellingPrice = (TextBox)e.Item.FindControl("txtSellingPrice");
            TextBox txtBarCode1 = (TextBox)e.Item.FindControl("txtBarCode1");
            TextBox txtBarCode2 = (TextBox)e.Item.FindControl("txtBarCode2");
            TextBox txtBarCode3 = (TextBox)e.Item.FindControl("txtBarCode3");

            switch (e.CommandName)
            {
                case "cmdDelProductPackage":
                    Label lblProductPackageID = (Label)e.Item.FindControl("lblProductPackageID");

                    ProductPackage clsProductPackage = new ProductPackage();
                    clsProductPackage.Delete(lblProductPackageID.Text);
                    clsProductPackage.CommitAndDispose();

                    cboProductCode_SelectedIndexChanged(null, null);
                    break;
                case "cmdPrintShelvesBarCode1":
                    AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter1 = new ThermalBarCodePrinter();
                    clsThermalBarCodePrinter1.PrintShelvesTag(cboProductCode.SelectedItem.Text, txtBarCode1.Text, Convert.ToDecimal(txtSellingPrice.Text).ToString("#,##0.#0"));
                    break;
                case "cmdPrintShelvesBarCode2":
                    AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter2 = new ThermalBarCodePrinter();
                    clsThermalBarCodePrinter2.PrintShelvesTag(cboProductCode.SelectedItem.Text, txtBarCode2.Text, Convert.ToDecimal(txtSellingPrice.Text).ToString("#,##0.#0"));
                    break;
                case "cmdPrintShelvesBarCode3":
                    AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter3 = new ThermalBarCodePrinter();
                    clsThermalBarCodePrinter3.PrintShelvesTag(cboProductCode.SelectedItem.Text, txtBarCode3.Text, Convert.ToDecimal(txtSellingPrice.Text).ToString("#,##0.#0"));
                    break;
            }
        }

        protected void imgSaveCopyToAllMatrix_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
        }
        protected void cmdSaveCopyToAllMatrix_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        protected void imgProductHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgProductPriceHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgInventoryAdjustment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("invadjustment", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
        }
        protected void imgEditNow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) +
                                "&id=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            lblProductID.ToolTip = "0"; //default zero for Commision

            string strproductcode = string.Empty;
            try { strproductcode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
            catch { }

            if (strproductcode == string.Empty)
            {
                cboProductCode.Items.Clear();
                cboProductCode.Items.Add(new ListItem("No Product; Enter product to search.", "0"));
            }
            else{
                txtProductCode.Text = strproductcode;
                cmdProductCode_Click(null, null);
            }
		}
		private void SaveRecord()
		{
            long lngUID = long.Parse(Session["UID"].ToString());
            DateTime dteChangeDate = DateTime.Now;

            ProductPackage clsProductPackage = new ProductPackage();
            clsProductPackage.GetConnection();

            bool boIsFirstRecord = true;
            ProductPackageDetails clsProductPackageDetails;
            foreach (DataListItem e in lstProductPackages.Items)
            {
                if (boIsFirstRecord == true)
                {
                    TextBox txtCommision = (TextBox)e.FindControl("txtCommision");
                    Products clsProduct = new Products(clsProductPackage.Connection, clsProductPackage.Transaction);
                    clsProduct.UpdateCommision(long.Parse(cboProductCode.SelectedValue), Convert.ToDecimal(txtCommision.Text));

                    boIsFirstRecord = false;
                }

                HtmlInputCheckBox chkProductPackageID = (HtmlInputCheckBox)e.FindControl("chkProductPackageID");
                TextBox txtBarCode1 = (TextBox)e.FindControl("txtBarCode1");
                TextBox txtBarCode2 = (TextBox)e.FindControl("txtBarCode2");
                TextBox txtBarCode3 = (TextBox)e.FindControl("txtBarCode3");
                Label lblUnitName = (Label)e.FindControl("lblUnitName");
                TextBox txtQuantity = (TextBox)e.FindControl("txtQuantity");
                TextBox txtPurchasePrice = (TextBox)e.FindControl("txtPurchasePrice");
                TextBox txtSellingPrice = (TextBox)e.FindControl("txtSellingPrice");
                TextBox txtWSPrice = (TextBox)e.FindControl("txtWSPrice");
                Label lblVAT = (Label)e.FindControl("lblVAT");
                Label lblEVAT = (Label)e.FindControl("lblEVAT");
                Label lblLocalTax = (Label)e.FindControl("lblLocalTax");
                
                clsProductPackageDetails = new ProductPackageDetails();
                clsProductPackageDetails.PackageID = Convert.ToInt64(chkProductPackageID.Value);
                clsProductPackageDetails.ProductID = Convert.ToInt64(cboProductCode.SelectedValue);
                clsProductPackageDetails.UnitID = Convert.ToInt32(lblUnitName.ToolTip);
                clsProductPackageDetails.Price = Convert.ToDecimal(txtSellingPrice.Text);
                clsProductPackageDetails.WSPrice = Convert.ToDecimal(txtWSPrice.Text);
                clsProductPackageDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
                clsProductPackageDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
                clsProductPackageDetails.VAT = Convert.ToDecimal(lblVAT.Text);
                clsProductPackageDetails.EVAT = Convert.ToDecimal(lblEVAT.Text);
                clsProductPackageDetails.LocalTax = Convert.ToDecimal(lblLocalTax.Text);
                clsProductPackageDetails.BarCode1 = txtBarCode1.Text;
                clsProductPackageDetails.BarCode2 = txtBarCode2.Text;
                clsProductPackageDetails.BarCode3 = txtBarCode3.Text;

                clsProductPackage.Update(clsProductPackageDetails, lngUID, dteChangeDate, "Change price adjustment.");

            }

            clsProductPackage.CommitAndDispose();
		}
        
		#endregion

    }
}
