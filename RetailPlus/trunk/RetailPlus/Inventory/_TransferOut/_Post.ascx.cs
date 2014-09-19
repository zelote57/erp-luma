namespace AceSoft.RetailPlus.Inventory._TransferOut
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using AceSoft.RetailPlus.Data;
    using System.Xml;
    using System.Text;

    public partial class __Post : System.Web.UI.UserControl
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
                    LoadItems();
                    cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
                    imgDelete.Attributes.Add("onClick", "return confirm_delete();");
                    //cmdEdit.Attributes.Add("onClick", "return confirm_select();");
                    //imgEdit.Attributes.Add("onClick", "return confirm_select();");
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

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (cboProductCode.SelectedItem.Value.ToString() != "0") //|| cboProductCode.SelectedItem.Value.ToString() != null)
            {
                SaveRecord();
                LoadItems();
                LoadOptions();
            }
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            if (cboProductCode.SelectedItem.Value.ToString() != "0") //|| cboProductCode.SelectedItem.Value.ToString() != null)
            {
                SaveRecord();
                LoadItems();
                LoadOptions();
            }
        }
        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        protected void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (DeleteItems())
                LoadItems();
        }
        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            if (DeleteItems())
                LoadItems();
        }
        protected void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            UpdateItem();
        }
        protected void cmdEdit_Click(object sender, System.EventArgs e)
        {
            UpdateItem();
        }
        protected void imgClear_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadOptions();
        }
        protected void cmdClear_Click(object sender, System.EventArgs e)
        {
            LoadOptions();
        }
        protected void imgGRN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            IssueGRN();
        }
        protected void cmdGRN_Click(object sender, System.EventArgs e)
        {
            IssueGRN();
        }
        protected void cboProductCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboProductCode.Items.Count == 0)
                return;

            if (cboProductCode.Items.Count == 1 && cboProductCode.SelectedValue == Constants.ZERO_STRING)
                return;

            DataClass clsDataClass = new DataClass();
            long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
            cboVariation.DataTextField = "MatrixDescription";
            cboVariation.DataValueField = "MatrixID";
            cboVariation.DataSource = clsProductVariationsMatrix.BaseListSimpleAsDataTable(ProductID, SortField: "VariationDesc").DefaultView;
            cboVariation.DataBind();

            if (cboVariation.Items.Count == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", Constants.ZERO_STRING)); }
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            cboProductUnit.DataTextField = "BottomUnitCode";
            cboProductUnit.DataValueField = "BottomUnitID";
            cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID, "a.MatrixID", SortOption.Ascending).DefaultView;
            cboProductUnit.DataBind();

            Products clsProduct = new Products(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            ProductDetails clsDetails = clsProduct.Details(ProductID);
            ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            System.Data.DataTable dtProductPurchasePriceHistory = clsProductPurchasePriceHistory.ListAsDataTable(ProductID, "PurchasePrice", SortOption.Ascending);

            clsProductVariationsMatrix.CommitAndDispose();

            string strPurchasePriceHistory = string.Empty;
            foreach (System.Data.DataRow dr in dtProductPurchasePriceHistory.Rows)
            {
                DateTime dtePurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                decimal decPurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                long lngSupplierID = long.Parse(dr["SupplierID"].ToString());
                string strSupplierName = "" + dr["SupplierName"].ToString();
                if (lngSupplierID == long.Parse(lblSupplierID.Text))
                    clsDetails.PurchasePrice = decPurchasePrice;

                strPurchasePriceHistory += dtePurchaseDate.ToString("ddMMMyyyy HH:mm") + ": " + decPurchasePrice.ToString("#,##0.##0").PadLeft(10) + " " + strSupplierName + "\r\n<br />" + Environment.NewLine;
            }
            lblPurchasePriceHistory.Text = "<br />" + strPurchasePriceHistory;

            cboProductUnit.Items.Insert(0, new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));

            cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));
            txtPrice.Text = clsDetails.PurchasePrice.ToString("#####0.##0");
            txtSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
            txtOldSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
            decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.##0");
            txtVAT.Text = clsDetails.VAT.ToString("#,##0.##0");
            txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.##0");
            txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.##0");

            if (clsDetails.VAT > 0) chkIsTaxable.Checked = true;
            else chkIsTaxable.Checked = false;

            if (cboProductUnit.Items.Count == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); }
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ComputeItemAmount();
            cboVariation_SelectedIndexChanged(null, null);
            lstItemFixCssClass();
        }
        protected void cboVariation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            long MatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
            if (MatrixID != 0)
            {
                long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

                Products clsProducts = new Products();
                ProductDetails clsDetails = clsProducts.Details(ProductID: ProductID, MatrixID: MatrixID);
                clsProducts.CommitAndDispose();

                txtPrice.Text = clsDetails.PurchasePrice.ToString("####0.##0");
                txtSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
                txtOldSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
                decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
                try { decMargin = decMargin / clsDetails.PurchasePrice; }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                txtMargin.Text = decMargin.ToString("#,##0.##0");
                txtVAT.Text = clsDetails.VAT.ToString("#,##0.##0");
                txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.##0");
                txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.##0");

                if (clsDetails.VAT > 0)
                    chkIsTaxable.Checked = true;
                else
                    chkIsTaxable.Checked = false;

                ComputeItemAmount();
            }
        }
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataClass clsDataClass = new DataClass();

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";

            string stSearchKey = txtProductCode.Text;
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(SearchKey: stSearchKey, Limit: 100);
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            bool bolShowCommandButtons = false;
            if (cboProductCode.Items.Count == 0)
            {
                cboProductCode.Items.Add(new ListItem("No product", "0"));
                bolShowCommandButtons = false;
            }
            else
            {
                bolShowCommandButtons = true;

            }
            ShowCommandButtons(bolShowCommandButtons);

            cboProductCode.SelectedIndex = 0;

            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void cmdVariationSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stSearchKey = txtVariation.Text.ToString();

            if (txtVariation.Text == null) stSearchKey = "";

            DataClass clsDataClass = new DataClass();
            long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
            cboVariation.DataTextField = "MatrixDescription";
            cboVariation.DataValueField = "MatrixID";
            cboVariation.DataSource = clsProductVariationsMatrix.BaseListAsDataTable(ProductID, MatrixDescription: stSearchKey, SortField: "VariationDesc").DefaultView;
            cboVariation.DataBind();

            if (cboVariation.Items.Count == 0)
            {
                cboVariation.Items.Add(new ListItem("No Variation", "0"));
            }
            cboVariation.SelectedIndex = 0;
            clsProductVariationsMatrix.CommitAndDispose();
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["TransferOutItemID"].ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();

                HyperLink lnkDescription = (HyperLink)e.Item.FindControl("lnkDescription");
                lnkDescription.Text = dr["Description"].ToString();
                lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkMatrixDescription = (HyperLink)e.Item.FindControl("lnkMatrixDescription");
                if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null)
                {
                    lnkMatrixDescription.Visible = true;
                    lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
                    lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
                }

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.##0");

                Label lblProductUnitID = (Label)e.Item.FindControl("lblProductUnitID");
                lblProductUnitID.Text = dr["ProductUnitID"].ToString();

                Label lblProductUnitCode = (Label)e.Item.FindControl("lblProductUnitCode");
                lblProductUnitCode.Text = dr["ProductUnitCode"].ToString();

                Label lblUnitCost = (Label)e.Item.FindControl("lblUnitCost");
                lblUnitCost.Text = Convert.ToDecimal(dr["UnitCost"].ToString()).ToString("#,##0.##0");

                Label lblDiscountApplied = (Label)e.Item.FindControl("lblDiscountApplied");
                lblDiscountApplied.Text = Convert.ToDecimal(dr["DiscountApplied"].ToString()).ToString("#,##0.##0");

                DiscountTypes DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                if (DiscountType == DiscountTypes.Percentage)
                {
                    Label lblPercent = (Label)e.Item.FindControl("lblPercent");
                    lblPercent.Visible = true;
                }

                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.##0");

                Label lblVAT = (Label)e.Item.FindControl("lblVAT");
                lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.##0");

                Label lblEVAT = (Label)e.Item.FindControl("lblEVAT");
                lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.##0");

                Label lblisVATInclusive = (Label)e.Item.FindControl("lblisVATInclusive");
                lblisVATInclusive.Text = Convert.ToBoolean(Convert.ToInt16(dr["isVATInclusive"].ToString())).ToString();

                Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.##0");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                ImageButton imgItemReceive = (ImageButton)e.Item.FindControl("imgItemReceive");
                Label lblTransferOutItemReceivedStatus = (Label)e.Item.FindControl("lblTransferOutItemReceivedStatus");
                TransferOutItemReceivedStatus clsTransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), dr["TransferOutItemReceivedStatus"].ToString());
                lblTransferOutItemReceivedStatus.Text = clsTransferOutItemReceivedStatus.ToString("d");

                if (clsTransferOutItemReceivedStatus == TransferOutItemReceivedStatus.Received)
                {
                    imgItemReceive.ToolTip = "Tag item as " + TransferOutItemReceivedStatus.NotYetReceived.ToString("G");
                    e.Item.CssClass = "ms-item-received";
                }
                else
                {
                    imgItemReceive.ToolTip = "Tag item as " + TransferOutItemReceivedStatus.Received.ToString("G");
                }

                //For anchor
                HtmlGenericControl divExpCollAsst = (HtmlGenericControl)e.Item.FindControl("divExpCollAsst");

                HtmlAnchor anchorDown = (HtmlAnchor)e.Item.FindControl("anchorDown");
                anchorDown.HRef = "javascript:ToggleDiv('" + divExpCollAsst.ClientID + "')";
            }
        }
        protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");

            switch (e.CommandName)
            {
                case "imgItemUpdateClick":
                    LoadItem(chkList.Value);
                    lstItemFixCssClass();
                    break;

                case "imgItemReceive":
                    Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                    Label lblTransferOutItemReceivedStatus = (Label)e.Item.FindControl("lblTransferOutItemReceivedStatus");
                    TransferOutItemReceivedStatus clsTransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), lblTransferOutItemReceivedStatus.Text);

                    if (clsTransferOutItemReceivedStatus == TransferOutItemReceivedStatus.Received)
                    {
                        clsTransferOutItemReceivedStatus = TransferOutItemReceivedStatus.NotYetReceived;
                    }
                    else
                    {
                        clsTransferOutItemReceivedStatus = TransferOutItemReceivedStatus.Received;
                        e.Item.CssClass = "ms-item-received";
                    }
                    UpdateItemReceiveStatus(long.Parse(chkList.Value), clsTransferOutItemReceivedStatus, decimal.Parse(lblQuantity.Text));
                    lblTransferOutItemReceivedStatus.Text = clsTransferOutItemReceivedStatus.ToString("d");
                    lstItemFixCssClass();
                    break;
            }
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferOut();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintTransferOut();
        }
        protected void imgPrintSelling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferOutSelling();
        }
        protected void cmdPrintSelling_Click(object sender, EventArgs e)
        {
            PrintTransferOutSelling();
        }
        protected void cmdUpdateHeader_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            UpdateHeader();
        }
        protected void imgUpdateHeader_Click(object sender, System.EventArgs e)
        {
            UpdateHeader();
        }
        protected void txtTransferOutDiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdateTransferOutDiscount();
        }
        protected void cboTransferOutDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransferOutDiscount();
        }
        protected void txtTransferOutFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtTransferOutDeposit_TextChanged(object sender, EventArgs e)
        {
            UpdateDeposit();
        }
        protected void imgProductQuickAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductCode.Text))
                {
                    ProductDetails clsDetails = new ProductDetails();

                    clsDetails.ProductCode = txtProductCode.Text;
                    clsDetails.BarCode = txtProductCode.Text;
                    clsDetails.ProductDesc = txtProductCode.Text;
                    clsDetails.ProductGroupID = ProductGroup.DEFAULT_GROUP_ID;
                    clsDetails.ProductSubGroupID = ProductSubGroup.DEFAULT_SUB_GROUP_ID;
                    clsDetails.BaseUnitID = Data.Unit.DEFAULT_UNIT_ID;
                    clsDetails.Price = 0;
                    clsDetails.PurchasePrice = 0;
                    clsDetails.IncludeInSubtotalDiscount = true;
                    clsDetails.VAT = Constants.DEFAULTS_VAT;
                    clsDetails.EVAT = 0;
                    clsDetails.LocalTax = 0;
                    clsDetails.Quantity = 0;
                    clsDetails.MinThreshold = 0;
                    clsDetails.MaxThreshold = 0;
                    clsDetails.SupplierID = Contacts.DEFAULT_SUPPLIER_ID;
                    clsDetails.IsItemSold = true;
                    clsDetails.WillPrintProductComposition = false;

                    Products clsProduct = new Products();
                    long id = clsProduct.Insert(clsDetails);
                    clsDetails.ProductID = id;

                    long lngUID = long.Parse(Session["UID"].ToString());
                    InvAdjustmentDetails clsInvAdjustmentDetails = new InvAdjustmentDetails();
                    clsInvAdjustmentDetails.UID = lngUID;
                    clsInvAdjustmentDetails.InvAdjustmentDate = DateTime.Now;
                    clsInvAdjustmentDetails.ProductID = id;
                    clsInvAdjustmentDetails.ProductCode = clsDetails.ProductCode;
                    clsInvAdjustmentDetails.Description = clsDetails.ProductDesc;
                    clsInvAdjustmentDetails.VariationMatrixID = 0;
                    clsInvAdjustmentDetails.MatrixDescription = null;
                    clsInvAdjustmentDetails.UnitID = clsDetails.BaseUnitID;
                    clsInvAdjustmentDetails.UnitCode = cboProductUnit.SelectedItem.Text;
                    clsInvAdjustmentDetails.QuantityBefore = 0;
                    clsInvAdjustmentDetails.QuantityNow = clsDetails.Quantity;
                    clsInvAdjustmentDetails.MinThresholdBefore = 0;
                    clsInvAdjustmentDetails.MinThresholdNow = clsDetails.MinThreshold;
                    clsInvAdjustmentDetails.MaxThresholdBefore = 0;
                    clsInvAdjustmentDetails.MaxThresholdNow = clsDetails.MaxThreshold;
                    clsInvAdjustmentDetails.Remarks = "newly added. beginning balance.";

                    InvAdjustment clsInvAdjustment = new InvAdjustment(clsProduct.Connection, clsProduct.Transaction);
                    clsInvAdjustment.Insert(clsInvAdjustmentDetails);

                    clsProduct.InheritSubGroupVariations(clsDetails.ProductSubGroupID, clsDetails.ProductID);

                    clsProduct.CommitAndDispose();

                    cmdProductCode_Click(null, null);
                }
            }
            catch { }
        }
        protected void imgVariationQuickAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                long.Parse(cboProductCode.SelectedItem.Value);
                if (txtVariation.Text != null || txtVariation.Text.Trim() != string.Empty || txtVariation.Text.Trim() != "")
                {
                    Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
                    ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                    clsProductVariationsMatrix.InsertBaseVariationEasy(long.Parse(cboProductCode.SelectedItem.Value), txtVariation.Text, clsAccessUserDetails.Name);
                    clsProductVariationsMatrix.CommitAndDispose();

                    cmdVariationSearch_Click(null, null);
                }
            }
            catch { }
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
        protected void imgChangePrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }
        protected void imgEditNow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) +
                                "&id=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }
        protected void cboProductUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductPackage clsProductPackage = new ProductPackage();
            ProductPackageDetails clsDetails = clsProductPackage.DetailsByProductIDAndUnitID(long.Parse(cboProductCode.SelectedValue), long.Parse(cboProductUnit.SelectedValue));
            if (clsDetails.PackageID == 0)
            {
                ProductUnit clsProductUnit = new ProductUnit(clsProductPackage.Connection, clsProductPackage.Transaction);
                Products clsProduct = new Products(clsProductPackage.Connection, clsProductPackage.Transaction);
                ProductDetails clsProductDetails = clsProduct.Details(long.Parse(cboProductCode.SelectedItem.Value));
                decimal decBaseUnitValue = clsProductUnit.GetBaseUnitValue(long.Parse(cboProductCode.SelectedItem.Value), int.Parse(cboProductUnit.SelectedItem.Value), 1);

                clsDetails.Price = decBaseUnitValue * clsProductDetails.Price;
                clsDetails.PurchasePrice = decBaseUnitValue * clsProductDetails.PurchasePrice;
            }
            clsProductPackage.CommitAndDispose();


            txtPrice.Text = clsDetails.PurchasePrice.ToString("#####0.##0");
            txtSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
            txtOldSellingPrice.Text = clsDetails.Price.ToString("#####0.##0");
            decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.##0");
            txtVAT.Text = clsDetails.VAT.ToString("#,##0.##0");
            txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.##0");
            txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.##0");

            ComputeItemAmount();
            lstItemFixCssClass();
        }
        protected void imgImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Import();
        }
        protected void cmdImport_Click(object sender, EventArgs e)
        {
            Import();
        }
        protected void chkIsVatInclusive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                long TransferOutID = long.Parse(lblTransferOutID.Text);

                TransferOut clsTransferOut = new TransferOut();
                clsTransferOut.UpdateIsVatInclusive(TransferOutID, chkIsVatInclusive.Checked);

                TransferOutDetails clsTransferOutDetails = clsTransferOut.Details(TransferOutID);
                clsTransferOut.CommitAndDispose();

                UpdateFooter(clsTransferOutDetails);
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            cboProductCode.Items.Clear();
            cboVariation.Items.Clear();
            cboProductUnit.Items.Clear();

            cboProductCode.Items.Add(new ListItem("No Product; Enter product to search.", "0"));

            cboProductCode_SelectedIndexChanged(null, null);
            cboVariation.Items.Add(new ListItem("No Variation", "0"));
            cboProductUnit.Items.Add(new ListItem("No Unit", "0"));

            txtQuantity.Text = "1";
            txtPrice.Text = "0.00";
            txtDiscount.Text = "0";
            txtSellingQuantity.Text = "0";
            txtMargin.Text = "10";
            txtSellingPrice.Text = "0";
            txtOldSellingPrice.Text = "0";

            txtRemarks.Text = "";
            ComputeItemAmount();
            lblTransferOutItemID.Text = "0";

            txtDeliveryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
            lnkAddProduct.NavigateUrl = newWindowUrl;

            imgProductHistory.Visible = false;
            imgProductPriceHistory.Visible = false;
            imgChangePrice.Visible = false;
            imgEditNow.Visible = false;
            lnkProductDetails.Visible = false;
            cmdVariationSearch.Visible = false;
            imgVariationQuickAdd.Visible = false;
            lnkVariationAdd.Visible = false;
            lnkProductUnitMatrix.Visible = false;
            lblPurchasePriceHistory.Visible = false;
        }
        private void LoadRecord()
        {
            Int64 iID = 0;
            try { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["TransferOutid"], Session.SessionID)); }
            catch { }
            try
            { if (iID == 0) iID = Convert.ToInt64(lblTransferOutID.Text); }
            catch { }

            TransferOut clsTransferOut = new TransferOut();
            TransferOutDetails clsDetails = clsTransferOut.Details(iID);
            clsTransferOut.CommitAndDispose();

            lblTransferOutID.Text = clsDetails.TransferOutID.ToString();
            lnkTransferOutNo.Text = clsDetails.TransferOutNo;
            
            lblTransferOutDate.Text = clsDetails.TransferOutDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            //lblRID.Text = clsDetails.RID.ToString();
            lblSupplierID.Text = clsDetails.SupplierID.ToString();

            lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
            lblSupplierCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(), Session.SessionID);

            lblSupplierContact.Text = clsDetails.SupplierContact;
            lblSupplierTelephoneNo.Text = clsDetails.SupplierTelephoneNo;
            lblTerms.Text = clsDetails.SupplierTerms.ToString("##0");
            switch (clsDetails.SupplierModeOfTerms)
            {
                case 0:
                    lblModeOfterms.Text = "Days";
                    break;
                case 1:
                    lblModeOfterms.Text = "Months";
                    break;
                case 2:
                    lblModeOfterms.Text = "Years";
                    break;
            }
            lblSupplierAddress.Text = clsDetails.SupplierAddress;
            lblBranchID.Text = clsDetails.BranchID.ToString();
            lblBranchCode.Text = clsDetails.BranchCode;
            lblBranchAddress.Text = clsDetails.BranchAddress;
            lblTransferOutRemarks.Text = clsDetails.Remarks;

            txtTransferOutDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboTransferOutDiscountType.SelectedIndex = cboTransferOutDiscountType.Items.IndexOf(cboTransferOutDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblTransferOutDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferOutDiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboTransferOutDiscount2Type.SelectedIndex = cboTransferOutDiscount2Type.Items.IndexOf(cboTransferOutDiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblTransferOutDiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferOutDiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboTransferOutDiscount3Type.SelectedIndex = cboTransferOutDiscount3Type.Items.IndexOf(cboTransferOutDiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblTransferOutDiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblTransferOutVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferOutFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtTransferOutDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblTransferOutSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
            lblTransferOutVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblTransferOutTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
        }
        private void SaveRecord()
        {
            TransferOutItemDetails clsDetails = new TransferOutItemDetails();

            Products clsProducts = new Products();
            ProductDetails clsProductDetails = clsProducts.Details1(Constants.BRANCH_ID_MAIN, Convert.ToInt64(cboProductCode.SelectedItem.Value));

            Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(Int32.Parse(Session["BranchID"].ToString()), Session["TerminalNo"].ToString());
            clsProducts.CommitAndDispose();

            clsDetails.TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
            clsDetails.ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);
            clsDetails.ProductCode = clsProductDetails.ProductCode;
            clsDetails.BarCode = clsProductDetails.BarCode;
            clsDetails.Description = clsProductDetails.ProductDesc;
            clsDetails.ProductUnitID = Convert.ToInt32(cboProductUnit.SelectedItem.Value);
            clsDetails.ProductUnitCode = cboProductUnit.SelectedItem.Text;
            clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
            clsDetails.UnitCost = Convert.ToDecimal(txtPrice.Text);
            clsDetails.Discount = getItemTotalDiscount();
            clsDetails.DiscountApplied = Convert.ToDecimal(txtDiscount.Text);
            if (clsDetails.DiscountApplied == 0)
            {
                clsDetails.DiscountType = DiscountTypes.NotApplicable;
            }
            else
            {
                if (chkInPercent.Checked == true)
                    clsDetails.DiscountType = DiscountTypes.Percentage;
                else
                    clsDetails.DiscountType = DiscountTypes.FixedValue;
            }

            clsDetails.IsVatable = chkIsTaxable.Checked;
            clsDetails.Amount = ComputeItemAmount();

            if (clsDetails.IsVatable)
            {
                clsDetails.VatableAmount = clsDetails.Amount;
                clsDetails.EVatableAmount = clsDetails.Amount;
                clsDetails.LocalTax = clsDetails.Amount;

                if (clsTerminalDetails.IsVATInclusive == false)
                {
                    if (clsDetails.VatableAmount < clsDetails.Discount) clsDetails.VatableAmount = 0;
                    if (clsDetails.EVatableAmount < clsDetails.Discount) clsDetails.EVatableAmount = 0;
                    if (clsDetails.LocalTax < clsDetails.Discount) clsDetails.LocalTax = 0;
                }
                else
                {
                    if (clsDetails.VatableAmount >= clsDetails.Discount) clsDetails.VatableAmount = (clsDetails.VatableAmount) / (1 + (clsTerminalDetails.VAT / 100)); else clsDetails.VatableAmount = 0;
                    if (clsDetails.EVatableAmount >= clsDetails.Discount) clsDetails.EVatableAmount = (clsDetails.EVatableAmount) / (1 + (clsTerminalDetails.VAT / 100)); else clsDetails.EVatableAmount = 0;
                    if (clsDetails.LocalTax >= clsDetails.Discount) clsDetails.LocalTax = (clsDetails.LocalTax) / (1 + (clsTerminalDetails.LocalTax / 100)); else clsDetails.LocalTax = 0;
                }

                clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
                clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
                clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);

                //if (!clsTerminalDetails.IsVATInclusive) clsDetails.Amount += (clsDetails.VAT + clsDetails.LocalTax);
                //if (!clsTerminalDetails.EnableEVAT) clsDetails.Amount += clsDetails.EVAT;
            }
            else
            {
                clsDetails.VAT = 0;
                clsDetails.VatableAmount = 0;
                clsDetails.EVAT = 0;
                clsDetails.EVatableAmount = 0;
                clsDetails.LocalTax = 0;
            }

            clsDetails.isVATInclusive = clsTerminalDetails.IsVATInclusive;
            clsDetails.VariationMatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
            if (clsDetails.VariationMatrixID != 0)
                clsDetails.MatrixDescription = cboVariation.SelectedItem.Text;
            clsDetails.ProductGroup = clsProductDetails.ProductGroupCode;
            clsDetails.ProductSubGroup = clsProductDetails.ProductSubGroupCode;
            clsDetails.Remarks = txtRemarks.Text;

            // Added jan 1, 2010 4:20PM : for selling information
            clsDetails.SellingPrice = decimal.Parse(txtSellingPrice.Text);
            clsDetails.SellingVAT = decimal.Parse(txtVAT.Text);
            clsDetails.SellingEVAT = decimal.Parse(txtEVAT.Text);
            clsDetails.SellingLocalTax = decimal.Parse(txtLocalTax.Text);
            clsDetails.OldSellingPrice = decimal.Parse(txtOldSellingPrice.Text);

            // Aug 9, 2011 : Lemu
            // For Required Inventory Days
            //clsDetails.RID = long.Parse(txtRID.Text);

            TransferOutItem clsTransferOutItem = new TransferOutItem();
            if (lblTransferOutItemID.Text != "0")
            {
                clsDetails.TransferOutItemID = Convert.ToInt64(lblTransferOutItemID.Text);
                clsTransferOutItem.Update(clsDetails);
            }
            else
                clsTransferOutItem.Insert(clsDetails);

            TransferOutDetails clsTransferOutDetails = new TransferOutDetails();
            clsTransferOutDetails.TransferOutID = clsDetails.TransferOutID;
            clsTransferOutDetails.DiscountApplied = Convert.ToDecimal(txtTransferOutDiscountApplied.Text);
            clsTransferOutDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscountType.SelectedItem.Value);

            clsTransferOutDetails.Discount2Applied = Convert.ToDecimal(txtTransferOutDiscount2Applied.Text);
            clsTransferOutDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount2Type.SelectedItem.Value);

            clsTransferOutDetails.Discount3Applied = Convert.ToDecimal(txtTransferOutDiscount3Applied.Text);
            clsTransferOutDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount3Type.SelectedItem.Value);

            TransferOut clsTransferOut = new TransferOut(clsTransferOutItem.Connection, clsTransferOutItem.Transaction);
            clsTransferOut.UpdateDiscount(clsDetails.TransferOutID, clsTransferOutDetails.DiscountApplied, clsTransferOutDetails.DiscountType, clsTransferOutDetails.Discount2Applied, clsTransferOutDetails.Discount2Type, clsTransferOutDetails.Discount3Applied, clsTransferOutDetails.Discount3Type);

            clsTransferOutDetails = clsTransferOut.Details(clsDetails.TransferOutID);
            clsTransferOutItem.CommitAndDispose();

            UpdateFooter(clsTransferOutDetails);
        }
        private void ClearAddItem()
        {
            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
            txtRemarks.Text = "";
            ComputeItemAmount();
        }
        private bool DeleteItems()
        {
            bool boRetValue = false;
            string stIDs = "";

            foreach (DataListItem item in lstItem.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
                {
                    if (chkList.Checked == true)
                    {
                        stIDs += chkList.Value + ",";
                        boRetValue = true;
                    }
                }
            }
            if (boRetValue)
            {
                TransferOutItem clsTransferOutItem = new TransferOutItem();
                clsTransferOutItem.Delete(stIDs.Substring(0, stIDs.Length - 1));

                TransferOut clsTransferOut = new TransferOut(clsTransferOutItem.Connection, clsTransferOutItem.Transaction);
                clsTransferOut.SynchronizeAmount(Convert.ToInt64(lblTransferOutID.Text));

                TransferOutDetails clsTransferOutDetails = clsTransferOut.Details(Convert.ToInt64(lblTransferOutID.Text));

                clsTransferOutItem.CommitAndDispose();

                UpdateFooter(clsTransferOutDetails);
            }

            return boRetValue;
        }
        private void UpdateItem()
        {
            if (isChkListSingle() == true)
            {
                string stID = GetFirstID();
                if (stID != null)
                {
                    LoadItems();
                }
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Cannot update more than one record. Please select at least one record to update.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }
        private void LoadItem(string stID)
        {
            TransferOutItem clsTransferOutItem = new TransferOutItem();
            TransferOutItemDetails clsTransferOutItemDetails = clsTransferOutItem.Details(Convert.ToInt64(stID));
            clsTransferOutItem.CommitAndDispose();

            cboProductCode.Items.Clear();
            cboVariation.Items.Clear();
            cboProductUnit.Items.Clear();

            txtProductCode.Text = clsTransferOutItemDetails.BarCode;
            cmdProductCode_Click(null, null);

            cboProductCode.SelectedIndex = cboProductCode.Items.IndexOf(new ListItem(clsTransferOutItemDetails.ProductCode, clsTransferOutItemDetails.ProductID.ToString()));

            if (clsTransferOutItemDetails.VariationMatrixID == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0")); cboVariation.SelectedIndex = 0; }
            else
            { cboVariation.SelectedIndex = cboVariation.Items.IndexOf(new ListItem(clsTransferOutItemDetails.MatrixDescription, clsTransferOutItemDetails.VariationMatrixID.ToString())); }

            if (clsTransferOutItemDetails.ProductUnitID == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); cboProductUnit.SelectedIndex = 0; }
            else
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsTransferOutItemDetails.ProductUnitCode, clsTransferOutItemDetails.ProductUnitID.ToString()));
            }

            txtQuantity.Text = clsTransferOutItemDetails.Quantity.ToString("###0.##0");
            txtPrice.Text = clsTransferOutItemDetails.UnitCost.ToString("###0.##0");
            txtDiscount.Text = clsTransferOutItemDetails.DiscountApplied.ToString("###0.##0");

            if (clsTransferOutItemDetails.DiscountType == DiscountTypes.Percentage)
                chkInPercent.Checked = true;
            else
            {
                chkInPercent.Checked = false;
            }
            txtAmount.Text = clsTransferOutItemDetails.Amount.ToString("###0.##0");
            txtRemarks.Text = clsTransferOutItemDetails.Remarks;
            lblTransferOutItemID.Text = stID;
            chkIsTaxable.Checked = clsTransferOutItemDetails.IsVatable;

            //Added Jan 1, 2010 4:20PM : For selling information
            txtSellingQuantity.Text = "1";
            try
            { txtMargin.Text = decimal.Parse(Convert.ToString(((clsTransferOutItemDetails.SellingPrice - clsTransferOutItemDetails.UnitCost) / clsTransferOutItemDetails.UnitCost) * 100)).ToString("###0.##0"); }
            catch { txtMargin.Text = "0.00"; }
            txtSellingPrice.Text = clsTransferOutItemDetails.SellingPrice.ToString("###0.##0");
            txtVAT.Text = clsTransferOutItemDetails.SellingVAT.ToString("###0.##0");
            txtEVAT.Text = clsTransferOutItemDetails.SellingEVAT.ToString("###0.##0");
            txtLocalTax.Text = clsTransferOutItemDetails.SellingLocalTax.ToString("###0.##0");

            //Added April 28, 2010 4:20PM : For selling information
            txtOldSellingPrice.Text = clsTransferOutItemDetails.OldSellingPrice.ToString("###0.##0");

            // Aug 9, 2011 : Lemu
            // For Required Inventory Days
            //txtRID.Text = clsTransferOutItemDetails.RID.ToString();

            txtProductCode.Focus();
            ShowCommandButtons(true);
        }
        private void UpdateItemReceiveStatus(long TransferOutItemID, TransferOutItemReceivedStatus clsTransferOutItemReceivedStatus, decimal ReceivedQuantity)
        {

            TransferOutItem clsTransferOutItem = new TransferOutItem();
            clsTransferOutItem.UpdateReceiveStatus(TransferOutItemID, clsTransferOutItemReceivedStatus, ReceivedQuantity);
            clsTransferOutItem.CommitAndDispose();

        }
        private void ShowCommandButtons(bool bolShowCommandButtons)
        {
            imgProductHistory.Visible = bolShowCommandButtons;
            imgProductPriceHistory.Visible = bolShowCommandButtons;
            imgChangePrice.Visible = bolShowCommandButtons;
            imgEditNow.Visible = bolShowCommandButtons;
            lnkProductDetails.Visible = bolShowCommandButtons;
            cmdVariationSearch.Visible = bolShowCommandButtons;
            imgVariationQuickAdd.Visible = bolShowCommandButtons;
            lnkVariationAdd.Visible = bolShowCommandButtons;
            lnkProductUnitMatrix.Visible = bolShowCommandButtons;
            lblPurchasePriceHistory.Visible = bolShowCommandButtons;

            if (bolShowCommandButtons)
            {
                string stParam = "?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
                string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
                lnkProductDetails.NavigateUrl = newWindowUrl;

                stParam = "?task=" + Common.Encrypt("add", Session.SessionID) + "&prodid=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
                newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam;
                lnkVariationAdd.NavigateUrl = newWindowUrl;

                stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&prodid=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
                newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam;
                lnkProductUnitMatrix.NavigateUrl = newWindowUrl;
            }
        }
        private void LoadItems()
        {
            DataClass clsDataClass = new DataClass();

            TransferOutItem clsTransferOutItem = new TransferOutItem();
            lstItem.DataSource = clsTransferOutItem.ListAsDataTable(Convert.ToInt64(lblTransferOutID.Text)).DefaultView;
            lstItem.DataBind();
            clsTransferOutItem.CommitAndDispose();
            lstItemFixCssClass();
        }
        private void IssueGRN()
        {
            DateTime DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);

            ERPConfig clsERPConfig = new ERPConfig();
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
            clsERPConfig.CommitAndDispose();

            if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
            {
                long TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
                string SupplierDRNo = txtSupplierDRNo.Text;

                TransferOut clsTransferOut = new TransferOut();
                clsTransferOut.IssueGRN(TransferOutID, SupplierDRNo, DeliveryDate);
                clsTransferOut.CommitAndDispose();

                string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(TransferOutID.ToString(), Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Sorry you cannot post using the delivery date: " + txtDeliveryDate.Text + ". Please enter an allowable posting date.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }
        private decimal ComputeItemAmount()
        {
            decimal quantity = Convert.ToDecimal(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal discount = Convert.ToDecimal(txtDiscount.Text);
            decimal amount = 0;
            if (chkInPercent.Checked == true)
            {
                amount = (quantity * (price - (price * discount / 100)));
            }
            else
            { amount = (quantity * (price - discount)); }
            txtAmount.Text = amount.ToString("####0.##0");
            return amount;
        }
        private decimal getItemTotalDiscount()
        {
            decimal quantity = Convert.ToDecimal(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal discount = Convert.ToDecimal(txtDiscount.Text);
            decimal amount = 0;
            decimal totaldiscount = 0;

            if (chkInPercent.Checked == true)
            {
                amount = (quantity * (price - (price * discount / 100)));
            }
            else
            { amount = (quantity * (price - discount)); }

            totaldiscount = (quantity * price) - amount;
            return totaldiscount;
        }
        private string GetFirstID()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
                {
                    if (chkList.Checked == true)
                    {
                        return chkList.Value;
                    }
                }
            }
            return null;
        }
        private bool isChkListSingle()
        {
            bool boChkListSingle = true;
            int iCount = 0;

            foreach (DataListItem item in lstItem.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
                {
                    if (chkList.Checked == true)
                    {
                        iCount += 1;
                        if (iCount >= 2)
                        { return false; }
                    }
                }
            }
            return boChkListSingle;
        }
        private void PrintTransferOut()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("TransferOutreport", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(lblTransferOutID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }
        private void PrintTransferOutSelling()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("TransferOutreport", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(lblTransferOutID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("TransferOutReportSellingPrice", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrintSellingPrice, this.updPrintSellingPrice.GetType(), "openwindow", javaScript, true);
        }
        private void UpdateHeader()
        {
            string stID = lblTransferOutID.Text;

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(stID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void GenerateItems()
        {
            TransferOut clsTransferOut = new TransferOut();
            clsTransferOut.GenerateItemsForReorder(Int32.Parse(Session["TerminalID"].ToString()), Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOut.CommitAndDispose();
        }
        private void UpdateTransferOutDiscount()
        {
            TransferOutDetails clsTransferOutDetails = new TransferOutDetails();
            clsTransferOutDetails.TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
            clsTransferOutDetails.DiscountApplied = Convert.ToDecimal(txtTransferOutDiscountApplied.Text);
            clsTransferOutDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscountType.SelectedItem.Value);

            clsTransferOutDetails.Discount2Applied = Convert.ToDecimal(txtTransferOutDiscount2Applied.Text);
            clsTransferOutDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount2Type.SelectedItem.Value);

            clsTransferOutDetails.Discount3Applied = Convert.ToDecimal(txtTransferOutDiscount3Applied.Text);
            clsTransferOutDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount3Type.SelectedItem.Value);

            TransferOut clsTransferOut = new TransferOut();
            clsTransferOut.UpdateDiscount(clsTransferOutDetails.TransferOutID, clsTransferOutDetails.DiscountApplied, clsTransferOutDetails.DiscountType, clsTransferOutDetails.Discount2Applied, clsTransferOutDetails.Discount2Type, clsTransferOutDetails.Discount3Applied, clsTransferOutDetails.Discount3Type);
            clsTransferOut.SynchronizeAmount(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOutDetails = clsTransferOut.Details(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOut.CommitAndDispose();

            UpdateFooter(clsTransferOutDetails);
        }
        private void UpdateFreight()
        {
            TransferOutDetails clsTransferOutDetails = new TransferOutDetails();
            clsTransferOutDetails.TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
            clsTransferOutDetails.Freight = Convert.ToDecimal(txtTransferOutFreight.Text);

            TransferOut clsTransferOut = new TransferOut();
            clsTransferOut.UpdateFreight(clsTransferOutDetails.TransferOutID, clsTransferOutDetails.Freight);
            clsTransferOut.SynchronizeAmount(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOutDetails = clsTransferOut.Details(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOut.CommitAndDispose();

            UpdateFooter(clsTransferOutDetails);
        }
        private void UpdateDeposit()
        {
            TransferOutDetails clsTransferOutDetails = new TransferOutDetails();
            clsTransferOutDetails.TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
            clsTransferOutDetails.Deposit = Convert.ToDecimal(txtTransferOutDeposit.Text);

            TransferOut clsTransferOut = new TransferOut();
            clsTransferOut.UpdateDeposit(clsTransferOutDetails.TransferOutID, clsTransferOutDetails.Deposit);
            clsTransferOut.SynchronizeAmount(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOutDetails = clsTransferOut.Details(Convert.ToInt64(lblTransferOutID.Text));
            clsTransferOut.CommitAndDispose();

            UpdateFooter(clsTransferOutDetails);
        }
        private void UpdateFooter(TransferOutDetails clsTransferOutDetails)
        {
            lblTransferOutDiscount.Text = clsTransferOutDetails.Discount.ToString("#,##0.#0");
            lblTransferOutDiscount2.Text = clsTransferOutDetails.Discount2.ToString("#,##0.#0");
            lblTransferOutDiscount3.Text = clsTransferOutDetails.Discount3.ToString("#,##0.#0");

            lblTotalDiscount1.Text = Convert.ToDecimal(clsTransferOutDetails.SubTotal + clsTransferOutDetails.Discount + clsTransferOutDetails.Discount2 + clsTransferOutDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsTransferOutDetails.SubTotal + clsTransferOutDetails.Discount2 + clsTransferOutDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsTransferOutDetails.SubTotal + clsTransferOutDetails.Discount3).ToString("#,##0.#0");

            lblTransferOutVatableAmount.Text = clsTransferOutDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferOutFreight.Text = clsTransferOutDetails.Freight.ToString("#,##0.#0");
            txtTransferOutDeposit.Text = clsTransferOutDetails.Deposit.ToString("#,##0.#0");
            lblTransferOutVAT.Text = clsTransferOutDetails.VAT.ToString("#,##0.#0");
            if (chkIsVatInclusive.Checked)
            {
                lblTransferOutSubTotal.Text = Convert.ToDecimal(clsTransferOutDetails.SubTotal - clsTransferOutDetails.VAT).ToString("#,##0.#0");
                lblTransferOutTotal.Text = clsTransferOutDetails.SubTotal.ToString("#,##0.#0");
            }
            else
            {
                lblTransferOutSubTotal.Text = clsTransferOutDetails.SubTotal.ToString("#,##0.#0");
                lblTransferOutTotal.Text = Convert.ToDecimal(clsTransferOutDetails.SubTotal + clsTransferOutDetails.VAT).ToString("#,##0.#0");
            }
        }
        private void Import()
        {
            if (txtPath.HasFile)
            {
                string fn = System.IO.Path.GetFileName(txtPath.PostedFile.FileName);

                if (fn.Contains("_" + Constants.PURCHASE_ORDER_CODE) == false)
                {
                    string stScript = "<Script>";
                    stScript += "window.alert('Please select a VALID Transfer In file to upload.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return;
                }

                string SaveLocation = "/RetailPlus/temp/uploaded_" + fn;

                txtPath.PostedFile.SaveAs(SaveLocation);
                XmlTextReader xmlReader = new XmlTextReader(SaveLocation);
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;

                TransferOut clsTransferOut = new TransferOut();
                clsTransferOut.GetConnection();
                TransferOutDetails clsTransferOutDetails = new TransferOutDetails();

                TransferOutItem clsTransferOutItem = new TransferOutItem(clsTransferOut.Connection, clsTransferOut.Transaction);
                TransferOutItemDetails clsTransferOutItemDetails;

                Contacts clsContact = new Contacts(clsTransferOut.Connection, clsTransferOut.Transaction);
                ContactDetails clsContactDetails;

                ContactGroups clsContactGroup = new ContactGroups(clsTransferOut.Connection, clsTransferOut.Transaction);
                ContactGroupDetails clsContactGroupDetails;

                Data.Unit clsUnit = new Data.Unit(clsTransferOut.Connection, clsTransferOut.Transaction);
                UnitDetails clsUnitDetails;

                ProductGroup clsProductGroup = new Data.ProductGroup(clsTransferOut.Connection, clsTransferOut.Transaction);
                ProductGroupDetails clsProductGroupDetails;

                ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup(clsTransferOut.Connection, clsTransferOut.Transaction);
                ProductSubGroupDetails clsProductSubGroupDetails;

                Products clsProduct = new Products(clsTransferOut.Connection, clsTransferOut.Transaction);
                ProductDetails clsProductDetails;

                ProductVariations clsProductVariation = new ProductVariations(clsTransferOut.Connection, clsTransferOut.Transaction);
                ProductVariationDetails clsProductVariationDetails;

                Branch clsBranch = new Branch(clsTransferOut.Connection, clsTransferOut.Transaction);
                BranchDetails clsBranchDetails;

                long lngProductID = 0; long lngProductCtr = 0;

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (xmlReader.Name == "TransferOutDetails")
                            {
                                clsTransferOutDetails.TransferOutNo = lnkTransferOutNo.Text;
                                clsTransferOutDetails.TransferOutDate = DateTime.Parse(lblTransferOutDate.Text);

                                clsTransferOutDetails.SupplierCode = xmlReader.GetAttribute("SupplierCode").ToString();
                                clsTransferOutDetails.SupplierContact = xmlReader.GetAttribute("SupplierContact").ToString();
                                clsTransferOutDetails.SupplierAddress = xmlReader.GetAttribute("SupplierAddress").ToString();
                                clsTransferOutDetails.SupplierTelephoneNo = xmlReader.GetAttribute("SupplierTelephoneNo").ToString();
                                clsTransferOutDetails.SupplierModeOfTerms = int.Parse(xmlReader.GetAttribute("SupplierModeOfTerms").ToString());
                                clsTransferOutDetails.SupplierTerms = int.Parse(xmlReader.GetAttribute("SupplierTerms").ToString());
                                clsTransferOutDetails.SupplierID = clsContact.Details(xmlReader.GetAttribute("SupplierCode").ToString()).ContactID;
                                if (clsTransferOutDetails.SupplierID == 0)
                                {
                                    clsContactDetails = new ContactDetails();
                                    clsContactDetails.ContactCode = clsTransferOutDetails.SupplierCode;
                                    clsContactDetails.ContactName = xmlReader.GetAttribute("SupplierName").ToString();
                                    clsContactDetails.BusinessName = clsTransferOutDetails.SupplierContact;
                                    clsContactDetails.Address = clsTransferOutDetails.SupplierAddress;
                                    clsContactDetails.TelephoneNo = clsTransferOutDetails.SupplierTelephoneNo;
                                    clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsTransferOutDetails.SupplierModeOfTerms.ToString());
                                    clsContactDetails.Terms = clsTransferOutDetails.SupplierTerms;
                                    clsContactDetails.Remarks = "Added in from Imported TransferOut #";
                                    clsContactDetails.ContactGroupID = int.Parse(Contacts.DEFAULT_SUPPLIER_ID.ToString("d"));
                                    clsContactDetails.DateCreated = DateTime.Now;
                                    clsTransferOutDetails.SupplierID = clsContact.Insert(clsContactDetails);
                                }
                                clsTransferOutDetails.RequiredDeliveryDate = DateTime.Parse(xmlReader.GetAttribute("RequiredDeliveryDate").ToString());
                                clsTransferOutDetails.BranchID = clsBranch.Details(xmlReader.GetAttribute("BranchCode")).BranchID;
                                if (clsTransferOutDetails.BranchID == 0)
                                {
                                    clsBranchDetails = new BranchDetails();
                                    clsBranchDetails.BranchCode = xmlReader.GetAttribute("BranchCode");
                                    clsBranchDetails.BranchName = xmlReader.GetAttribute("BranchName");
                                    clsBranchDetails.Address = xmlReader.GetAttribute("BranchAddress");
                                    clsBranchDetails.DBIP = xmlReader.GetAttribute("BranchDBIP");
                                    clsBranchDetails.DBPort = xmlReader.GetAttribute("BranchDBPort");
                                    clsBranchDetails.Remarks = xmlReader.GetAttribute("BranchRemarks");
                                    clsTransferOutDetails.BranchID = clsBranch.Insert(clsBranchDetails);
                                }

                                clsTransferOutDetails.TransferrerID = long.Parse(xmlReader.GetAttribute("TransferrerID"));
                                clsTransferOutDetails.TransferrerName = xmlReader.GetAttribute("TransferrerName");

                                clsTransferOutDetails.SubTotal = decimal.Parse(xmlReader.GetAttribute("SubTotal"));
                                clsTransferOutDetails.Discount = decimal.Parse(xmlReader.GetAttribute("Discount"));
                                clsTransferOutDetails.DiscountApplied = decimal.Parse(xmlReader.GetAttribute("DiscountApplied"));
                                clsTransferOutDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("DiscountType"));
                                clsTransferOutDetails.VAT = decimal.Parse(xmlReader.GetAttribute("VAT"));
                                clsTransferOutDetails.VatableAmount = decimal.Parse(xmlReader.GetAttribute("VatableAmount"));
                                clsTransferOutDetails.EVAT = decimal.Parse(xmlReader.GetAttribute("EVAT"));
                                clsTransferOutDetails.EVatableAmount = decimal.Parse(xmlReader.GetAttribute("EVatableAmount"));
                                clsTransferOutDetails.LocalTax = decimal.Parse(xmlReader.GetAttribute("LocalTax"));
                                clsTransferOutDetails.Freight = decimal.Parse(xmlReader.GetAttribute("Freight"));
                                clsTransferOutDetails.Deposit = decimal.Parse(xmlReader.GetAttribute("Deposit"));
                                clsTransferOutDetails.UnpaidAmount = decimal.Parse(xmlReader.GetAttribute("UnpaidAmount"));
                                clsTransferOutDetails.PaidAmount = decimal.Parse(xmlReader.GetAttribute("PaidAmount"));
                                clsTransferOutDetails.TotalItemDiscount = decimal.Parse(xmlReader.GetAttribute("TotalItemDiscount"));
                                clsTransferOutDetails.Status = (TransferOutStatus)Enum.Parse(typeof(TransferOutStatus), xmlReader.GetAttribute("Status"));
                                clsTransferOutDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsTransferOutDetails.SupplierDRNo = xmlReader.GetAttribute("SupplierDRNo");
                                clsTransferOutDetails.DeliveryDate = DateTime.Parse(xmlReader.GetAttribute("DeliveryDate"));
                                clsTransferOutDetails.CancelledDate = DateTime.Parse(xmlReader.GetAttribute("CancelledDate"));
                                clsTransferOutDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsTransferOutDetails.CancelledRemarks = xmlReader.GetAttribute("CancelledRemarks");
                                clsTransferOutDetails.CancelledByID = long.Parse(xmlReader.GetAttribute("CancelledByID"));

                                clsTransferOut.Update(clsTransferOutDetails);

                            }
                            else if (xmlReader.Name == "TransferOutItem")
                            {
                                clsTransferOutItemDetails = new TransferOutItemDetails();
                                clsTransferOutItemDetails.TransferOutID = long.Parse(lblTransferOutID.Text);

                                clsTransferOutItemDetails.ProductCode = xmlReader.GetAttribute("ProductCode");
                                clsTransferOutItemDetails.BarCode = xmlReader.GetAttribute("BarCode");
                                clsTransferOutItemDetails.Description = xmlReader.GetAttribute("ProductDesc");
                                clsTransferOutItemDetails.ProductSubGroup = xmlReader.GetAttribute("ItemProductSubGroup");
                                clsTransferOutItemDetails.ProductGroup = xmlReader.GetAttribute("ItemProductGroup");
                                clsTransferOutItemDetails.ProductUnitID = Convert.ToInt32(xmlReader.GetAttribute("ItemProductUnitID"));
                                clsTransferOutItemDetails.ProductUnitCode = xmlReader.GetAttribute("ItemProductUnitCode");
                                clsTransferOutItemDetails.Quantity = Convert.ToDecimal(xmlReader.GetAttribute("ItemQuantity"));
                                clsTransferOutItemDetails.UnitCost = Convert.ToDecimal(xmlReader.GetAttribute("ItemUnitCost"));
                                clsTransferOutItemDetails.Discount = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscount"));
                                clsTransferOutItemDetails.DiscountApplied = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscountApplied"));
                                clsTransferOutItemDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("ItemDiscountType"));
                                clsTransferOutItemDetails.Amount = Convert.ToDecimal(xmlReader.GetAttribute("ItemAmount"));
                                clsTransferOutItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsTransferOutItemDetails.VatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemVatableAmount"));
                                clsTransferOutItemDetails.EVatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVatableAmount"));
                                clsTransferOutItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsTransferOutItemDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemVAT"));
                                clsTransferOutItemDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVAT"));
                                clsTransferOutItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsTransferOutItemDetails.isVATInclusive = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemisVATInclusive")));
                                clsTransferOutItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsTransferOutItemDetails.TransferOutItemStatus = (TransferOutItemStatus)Enum.Parse(typeof(TransferOutItemStatus), xmlReader.GetAttribute("ItemTransferOutItemStatus"));
                                clsTransferOutItemDetails.VariationMatrixID = Convert.ToInt64(xmlReader.GetAttribute("ItemVariationMatrixID"));
                                clsTransferOutItemDetails.MatrixDescription = xmlReader.GetAttribute("ItemBaseVariationDescription");
                                clsTransferOutItemDetails.ProductGroup = xmlReader.GetAttribute("ProductGroup");
                                clsTransferOutItemDetails.ProductSubGroup = xmlReader.GetAttribute("ProductSubGroup");
                                clsTransferOutItemDetails.Remarks = xmlReader.GetAttribute("ItemRemarks");
                                clsTransferOutItemDetails.SellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingPrice"));
                                clsTransferOutItemDetails.SellingVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingVAT"));
                                clsTransferOutItemDetails.SellingEVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingEVAT"));
                                clsTransferOutItemDetails.SellingLocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingLocalTax"));
                                clsTransferOutItemDetails.OldSellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemOldSellingPrice"));

                                clsTransferOutItemDetails.ProductID = clsProduct.Details(clsTransferOutItemDetails.BarCode).ProductID;
                                lngProductID = clsTransferOutItemDetails.ProductID;
                                if (clsTransferOutItemDetails.ProductID == 0)
                                {
                                    clsTransferOutItemDetails.ProductID = clsProduct.Details(clsTransferOutItemDetails.ProductCode).ProductID;
                                    if (clsTransferOutItemDetails.ProductID == 0)
                                    {
                                        //insert new product
                                        clsProductDetails = new ProductDetails();
                                        clsProductDetails.BarCode = clsTransferOutItemDetails.BarCode;
                                        clsProductDetails.ProductCode = clsTransferOutItemDetails.ProductCode;
                                        clsProductDetails.ProductDesc = clsTransferOutItemDetails.Description;
                                        clsProductDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                                        clsProductDetails.ProductGroupName = xmlReader.GetAttribute("ProductGroupName");
                                        clsProductDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                                        clsProductDetails.ProductSubGroupName = xmlReader.GetAttribute("ProductSubGroupName");
                                        clsProductDetails.BaseUnitCode = xmlReader.GetAttribute("BaseUnitCode");
                                        clsProductDetails.BaseUnitName = xmlReader.GetAttribute("BaseUnitName");
                                        clsProductDetails.DateCreated = DateTime.Now;
                                        clsProductDetails.Price = Convert.ToDecimal(xmlReader.GetAttribute("Price"));
                                        clsProductDetails.PurchasePrice = Convert.ToDecimal(xmlReader.GetAttribute("PurchasePrice"));
                                        clsProductDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(xmlReader.GetAttribute("IncludeInSubtotalDiscount"));
                                        clsProductDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("VAT"));
                                        clsProductDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("EVAT"));
                                        clsProductDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("LocalTax"));
                                        clsProductDetails.Quantity = 0;
                                        clsProductDetails.MinThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MinThreshold"));
                                        clsProductDetails.MaxThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MaxThreshold"));
                                        clsProductDetails.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), xmlReader.GetAttribute("OrderSlipPrinter"));
                                        clsProductDetails.ChartOfAccountIDPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDPurchase"));
                                        clsProductDetails.ChartOfAccountIDSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDSold"));
                                        clsProductDetails.ChartOfAccountIDInventory = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDInventory"));
                                        clsProductDetails.ChartOfAccountIDTaxPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxPurchase"));
                                        clsProductDetails.ChartOfAccountIDTaxSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxSold"));
                                        clsProductDetails.IsItemSold = Convert.ToBoolean(xmlReader.GetAttribute("IsItemSold"));
                                        clsProductDetails.WillPrintProductComposition = Convert.ToBoolean(xmlReader.GetAttribute("WillPrintProductComposition"));
                                        clsProductDetails.UpdatedBy = long.Parse(xmlReader.GetAttribute("UpdatedBy"));
                                        clsProductDetails.UpdatedOn = Convert.ToDateTime(xmlReader.GetAttribute("UpdatedOn"));
                                        clsProductDetails.PercentageCommision = decimal.Parse(xmlReader.GetAttribute("PercentageCommision"));
                                        clsProductDetails.QuantityIN = clsProductDetails.Quantity;
                                        clsProductDetails.QuantityOUT = 0;

                                        clsProductDetails.SupplierCode = clsTransferOutDetails.SupplierCode;
                                        clsProductDetails.SupplierID = clsContact.Details(clsProductDetails.SupplierCode).ContactID;
                                        if (clsProductDetails.SupplierID == 0)
                                        {
                                            clsContactDetails = new ContactDetails();
                                            clsContactDetails.ContactGroupID = clsContactGroup.Details(int.Parse(ContactGroupCategory.SUPPLIER.ToString("d"))).ContactGroupID;
                                            if (clsContactDetails.ContactGroupID == 0)
                                            {
                                                clsContactGroupDetails = new ContactGroupDetails();
                                                clsContactGroupDetails.ContactGroupCode = xmlReader.GetAttribute("SUP");
                                                clsContactGroupDetails.ContactGroupName = xmlReader.GetAttribute("Default Supplier Group");
                                                clsContactGroupDetails.ContactGroupCategory = ContactGroupCategory.SUPPLIER;
                                                clsContactDetails.ContactGroupID = clsContactGroup.Insert(clsContactGroupDetails);
                                            }

                                            clsContactDetails.ContactCode = clsTransferOutDetails.SupplierCode;
                                            clsContactDetails.ContactName = clsTransferOutDetails.SupplierContact;

                                            clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsTransferOutDetails.SupplierModeOfTerms.ToString());
                                            clsContactDetails.Terms = clsTransferOutDetails.SupplierTerms;
                                            clsContactDetails.Address = clsTransferOutDetails.SupplierAddress;
                                            clsContactDetails.BusinessName = clsTransferOutDetails.SupplierContact;
                                            clsContactDetails.TelephoneNo = clsTransferOutDetails.SupplierTelephoneNo;
                                            clsContactDetails.Remarks = "Added in XML import";
                                            clsContactDetails.Debit = 0;
                                            clsContactDetails.Credit = 0;
                                            clsContactDetails.IsCreditAllowed = false;
                                            clsContactDetails.CreditLimit = 0;
                                            clsProductDetails.SupplierID = clsContact.Insert(clsContactDetails);
                                        }

                                        clsProductDetails.BaseUnitID = clsUnit.Details(clsProductDetails.BaseUnitCode).UnitID;
                                        if (clsProductDetails.BaseUnitID == 0)
                                        {
                                            clsUnitDetails = new UnitDetails();
                                            clsUnitDetails.UnitCode = clsProductDetails.BaseUnitCode;
                                            clsUnitDetails.UnitName = clsProductDetails.BaseUnitName;
                                            clsProductDetails.BaseUnitID = clsUnit.Insert(clsUnitDetails);
                                        }

                                        clsProductDetails.ProductGroupID = clsProductGroup.Details(clsProductDetails.ProductGroupCode).ProductGroupID;
                                        if (clsProductDetails.ProductGroupID == 0)
                                        {
                                            clsProductGroupDetails = new ProductGroupDetails();
                                            clsProductGroupDetails.ProductGroupCode = clsProductDetails.ProductGroupCode;
                                            clsProductGroupDetails.ProductGroupName = clsProductDetails.ProductGroupName;
                                            clsProductGroupDetails.UnitDetails = new UnitDetails
                                            {
                                                UnitID = clsProductDetails.BaseUnitID
                                            };
                                            clsProductGroupDetails.Price = clsProductDetails.Price;
                                            clsProductGroupDetails.PurchasePrice = clsProductDetails.PurchasePrice;
                                            clsProductGroupDetails.IncludeInSubtotalDiscount = clsProductDetails.IncludeInSubtotalDiscount;
                                            clsProductGroupDetails.VAT = clsProductDetails.VAT;
                                            clsProductGroupDetails.EVAT = clsProductDetails.EVAT;
                                            clsProductGroupDetails.LocalTax = clsProductDetails.LocalTax;
                                            clsProductDetails.ProductGroupID = clsProductGroup.Insert(clsProductGroupDetails);
                                        }

                                        clsProductDetails.ProductSubGroupID = clsProductSubGroup.Details(clsProductDetails.ProductSubGroupCode).ProductSubGroupID;
                                        if (clsProductDetails.ProductSubGroupID == 0)
                                        {
                                            clsProductSubGroupDetails = new ProductSubGroupDetails();
                                            clsProductSubGroupDetails.ProductGroupID = clsProductDetails.ProductGroupID;
                                            clsProductSubGroupDetails.ProductSubGroupCode = clsProductDetails.ProductSubGroupCode;
                                            clsProductSubGroupDetails.ProductSubGroupName = clsProductDetails.ProductSubGroupName;
                                            clsProductSubGroupDetails.BaseUnitID = clsProductDetails.BaseUnitID;
                                            clsProductSubGroupDetails.Price = clsProductDetails.Price;
                                            clsProductSubGroupDetails.PurchasePrice = clsProductDetails.PurchasePrice;
                                            clsProductSubGroupDetails.IncludeInSubtotalDiscount = clsProductDetails.IncludeInSubtotalDiscount;
                                            clsProductSubGroupDetails.VAT = clsProductDetails.VAT;
                                            clsProductSubGroupDetails.EVAT = clsProductDetails.EVAT;
                                            clsProductSubGroupDetails.LocalTax = clsProductDetails.LocalTax;
                                            clsProductDetails.ProductSubGroupID = clsProductSubGroup.Insert(clsProductSubGroupDetails);
                                        }

                                        clsTransferOutItemDetails.ProductID = clsProduct.Insert(clsProductDetails);
                                    }
                                    else
                                    {
                                        //product code already exist but not the same barcode
                                        clsProduct.UpdateBarcode(clsTransferOutItemDetails.ProductID, clsTransferOutItemDetails.BarCode);
                                    }
                                    lngProductID = clsTransferOutItemDetails.ProductID;
                                }

                                clsTransferOutItem.Insert(clsTransferOutItemDetails);

                                clsTransferOutDetails = new TransferOutDetails();
                                clsTransferOutDetails.TransferOutID = clsTransferOutItemDetails.TransferOutID;
                                clsTransferOutDetails.DiscountApplied = Convert.ToDecimal(txtTransferOutDiscountApplied.Text);
                                clsTransferOutDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscountType.SelectedItem.Value);

                                clsTransferOutDetails.Discount2Applied = Convert.ToDecimal(txtTransferOutDiscount2Applied.Text);
                                clsTransferOutDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount2Type.SelectedItem.Value);

                                clsTransferOutDetails.Discount3Applied = Convert.ToDecimal(txtTransferOutDiscount2Applied.Text);
                                clsTransferOutDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferOutDiscount3Type.SelectedItem.Value);

                                clsTransferOut = new TransferOut(clsTransferOutItem.Connection, clsTransferOutItem.Transaction);
                                clsTransferOut.UpdateDiscount(clsTransferOutItemDetails.TransferOutID, clsTransferOutDetails.DiscountApplied, clsTransferOutDetails.DiscountType, clsTransferOutDetails.Discount2Applied, clsTransferOutDetails.Discount2Type, clsTransferOutDetails.Discount3Applied, clsTransferOutDetails.Discount3Type);

                                clsTransferOutDetails = clsTransferOut.Details(clsTransferOutItemDetails.TransferOutID);
                                UpdateFooter(clsTransferOutDetails);

                                lngProductCtr++;
                            }
                            else if (xmlReader.Name == "Variation")
                            {
                                if (lngProductID != 0)
                                {
                                    clsProductVariationDetails = new ProductVariationDetails();

                                    clsProductVariationDetails.VariationID = clsProductVariation.Details(lngProductID, xmlReader.GetAttribute("VariationCode")).VariationID;
                                    if (clsProductVariationDetails.VariationID == 0)
                                    {
                                        clsProductVariationDetails.ProductID = lngProductID;
                                        clsProductVariationDetails.VariationCode = xmlReader.GetAttribute("VariationCode");
                                        clsProductVariationDetails.VariationType = xmlReader.GetAttribute("VariationType");

                                        clsProductVariation.Insert(clsProductVariationDetails);
                                    }
                                }
                            }
                            else
                            {
                                //lblError.Text += "<b>" + xmlReader.Name + ":</b>" + xmlReader.Value + "<br />";
                            }
                            break;
                        case XmlNodeType.Text:
                            //lblError.Text += "<b>" + xmlReader.LocalName + ":</b>" + xmlReader.Value + "<br />";
                            break;
                    }
                }
                xmlReader.Close();

                clsTransferOut.CommitAndDispose();
                LoadRecord();
                LoadItems();
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Please select Transfer In file to upload.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }
        private void lstItemFixCssClass()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblitemTransferOutItemReceivedStatus = (Label)item.FindControl("lblTransferOutItemReceivedStatus");
                TransferOutItemReceivedStatus itemTransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), lblitemTransferOutItemReceivedStatus.Text);
                if (itemTransferOutItemReceivedStatus == TransferOutItemReceivedStatus.Received)
                    item.CssClass = "ms-item-received";
                else if (item.ItemType == ListItemType.Item)
                    item.CssClass = "";
                else if (item.ItemType == ListItemType.AlternatingItem)
                    item.CssClass = "ms-alternating";

            }
        }

        #endregion

    }
}
