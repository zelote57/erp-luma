namespace AceSoft.RetailPlus.Inventory._TransferIn
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

	public partial  class __Post : System.Web.UI.UserControl
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
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
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

            //ProductPackage clsProductPackage = new ProductPackage(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            //ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByProductID(ProductID, Int64.Parse(cboVariation.SelectedItem.Value));

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

                strPurchasePriceHistory += dtePurchaseDate.ToString("ddMMMyyyy HH:mm") + ": " + decPurchasePrice.ToString("#,##0.##0").PadLeft(10) + " " + strSupplierName + "\r\n<br>" + Environment.NewLine;
            }
            lblPurchasePriceHistory.Text = "<br>" + strPurchasePriceHistory;

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
                chkList.Value = dr["TransferInItemID"].ToString();

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
                Label lblTransferInItemReceivedStatus = (Label)e.Item.FindControl("lblTransferInItemReceivedStatus");
                TransferInItemReceivedStatus clsTransferInItemReceivedStatus = (TransferInItemReceivedStatus)Enum.Parse(typeof(TransferInItemReceivedStatus), dr["TransferInItemReceivedStatus"].ToString());
                lblTransferInItemReceivedStatus.Text = clsTransferInItemReceivedStatus.ToString("d");

                if (clsTransferInItemReceivedStatus == TransferInItemReceivedStatus.Received)
                {
                    imgItemReceive.ToolTip = "Tag item as " + TransferInItemReceivedStatus.NotYetReceived.ToString("G");
                    e.Item.CssClass = "ms-item-received";
                }
                else
                {
                    imgItemReceive.ToolTip = "Tag item as " + TransferInItemReceivedStatus.Received.ToString("G");
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
                    Label lblTransferInItemReceivedStatus = (Label)e.Item.FindControl("lblTransferInItemReceivedStatus");
                    TransferInItemReceivedStatus clsTransferInItemReceivedStatus = (TransferInItemReceivedStatus)Enum.Parse(typeof(TransferInItemReceivedStatus), lblTransferInItemReceivedStatus.Text);

                    if (clsTransferInItemReceivedStatus == TransferInItemReceivedStatus.Received)
                    {
                        clsTransferInItemReceivedStatus = TransferInItemReceivedStatus.NotYetReceived;
                    }
                    else
                    {
                        clsTransferInItemReceivedStatus = TransferInItemReceivedStatus.Received;
                        e.Item.CssClass = "ms-item-received";
                    }
                    UpdateItemReceiveStatus(long.Parse(chkList.Value), clsTransferInItemReceivedStatus, decimal.Parse(lblQuantity.Text));
                    lblTransferInItemReceivedStatus.Text = clsTransferInItemReceivedStatus.ToString("d");
                    lstItemFixCssClass();
                    break;
            }
        }
        private void lstItemFixCssClass()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblitemTransferInItemReceivedStatus = (Label)item.FindControl("lblTransferInItemReceivedStatus");
                TransferInItemReceivedStatus itemTransferInItemReceivedStatus = (TransferInItemReceivedStatus)Enum.Parse(typeof(TransferInItemReceivedStatus), lblitemTransferInItemReceivedStatus.Text);
                if (itemTransferInItemReceivedStatus == TransferInItemReceivedStatus.Received)
                    item.CssClass = "ms-item-received";
                else if (item.ItemType == ListItemType.Item)
                    item.CssClass = "";
                else if (item.ItemType == ListItemType.AlternatingItem)
                    item.CssClass = "ms-alternating";

            }
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferIn();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintTransferIn();
        }
        protected void imgPrintSelling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferInSelling();
        }
        protected void cmdPrintSelling_Click(object sender, EventArgs e)
        {
            PrintTransferInSelling();
        }
        protected void cmdUpdateHeader_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            UpdateHeader();
        }
        protected void imgUpdateHeader_Click(object sender, System.EventArgs e)
        {
            UpdateHeader();
        }
        protected void txtTransferInDiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdateTransferInDiscount();
        }
        protected void cboTransferInDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransferInDiscount();
        }
        protected void txtTransferInFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtTransferInDeposit_TextChanged(object sender, EventArgs e)
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
                long TransferInID = long.Parse(lblTransferInID.Text);

                TransferIn clsTransferIn = new TransferIn();
                clsTransferIn.UpdateIsVatInclusive(TransferInID, chkIsVatInclusive.Checked);

                TransferInDetails clsTransferInDetails = clsTransferIn.Details(TransferInID);
                clsTransferIn.CommitAndDispose();

                UpdateFooter(clsTransferInDetails);
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
            txtOldSellingPrice.Text = "0";

            txtRemarks.Text = "";
            ComputeItemAmount();
            lblTransferInItemID.Text = "0";

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
            try { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["transferinid"], Session.SessionID)); }
            catch { }
            try
            { if (iID == 0) iID = Convert.ToInt64(lblTransferInID.Text); }
            catch { }

			TransferIn clsTransferIn = new TransferIn();
			TransferInDetails clsDetails = clsTransferIn.Details(iID);
			clsTransferIn.CommitAndDispose();

            lblTransferInID.Text = clsDetails.TransferInID.ToString();
            lnkTransferInNo.Text = clsDetails.TransferInNo;
            lnkTransferInNo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(clsDetails.TransferInID.ToString(), Session.SessionID);

            lblTransferInDate.Text = clsDetails.TransferInDate.ToString("yyyy-MM-dd HH:mm:ss");
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
            lblTransferInRemarks.Text = clsDetails.Remarks;

            txtTransferInDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboTransferInDiscountType.SelectedIndex = cboTransferInDiscountType.Items.IndexOf(cboTransferInDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblTransferInDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferInDiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboTransferInDiscount2Type.SelectedIndex = cboTransferInDiscount2Type.Items.IndexOf(cboTransferInDiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblTransferInDiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferInDiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboTransferInDiscount3Type.SelectedIndex = cboTransferInDiscount3Type.Items.IndexOf(cboTransferInDiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblTransferInDiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblTransferInVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferInFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtTransferInDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblTransferInSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
            lblTransferInVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblTransferInTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
		}
		private void SaveRecord()
		{
			TransferInItemDetails clsDetails = new TransferInItemDetails();

			Products clsProducts = new Products();
            ProductDetails clsProductDetails = clsProducts.Details1(Constants.BRANCH_ID_MAIN, Convert.ToInt64(cboProductCode.SelectedItem.Value));
			
			Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
			TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);
			clsProducts.CommitAndDispose();

            clsDetails.TransferInID = Convert.ToInt64(lblTransferInID.Text);
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

            TransferInItem clsTransferInItem = new TransferInItem();
            if (lblTransferInItemID.Text != "0")
            {
                clsDetails.TransferInItemID = Convert.ToInt64(lblTransferInItemID.Text);
                clsTransferInItem.Update(clsDetails);
            }
            else
                clsTransferInItem.Insert(clsDetails);

            TransferInDetails clsTransferInDetails = new TransferInDetails();
            clsTransferInDetails.TransferInID = clsDetails.TransferInID;
            clsTransferInDetails.DiscountApplied = Convert.ToDecimal(txtTransferInDiscountApplied.Text);
            clsTransferInDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscountType.SelectedItem.Value);

            clsTransferInDetails.Discount2Applied = Convert.ToDecimal(txtTransferInDiscount2Applied.Text);
            clsTransferInDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount2Type.SelectedItem.Value);

            clsTransferInDetails.Discount3Applied = Convert.ToDecimal(txtTransferInDiscount3Applied.Text);
            clsTransferInDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount3Type.SelectedItem.Value);

            TransferIn clsTransferIn = new TransferIn(clsTransferInItem.Connection, clsTransferInItem.Transaction);
            clsTransferIn.UpdateDiscount(clsDetails.TransferInID, clsTransferInDetails.DiscountApplied, clsTransferInDetails.DiscountType, clsTransferInDetails.Discount2Applied, clsTransferInDetails.Discount2Type, clsTransferInDetails.Discount3Applied, clsTransferInDetails.Discount3Type);

            clsTransferInDetails = clsTransferIn.Details(clsDetails.TransferInID);
            clsTransferInItem.CommitAndDispose();

            UpdateFooter(clsTransferInDetails);
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
                TransferInItem clsTransferInItem = new TransferInItem();
                clsTransferInItem.Delete(stIDs.Substring(0, stIDs.Length - 1));

                TransferIn clsTransferIn = new TransferIn(clsTransferInItem.Connection, clsTransferInItem.Transaction);
                clsTransferIn.SynchronizeAmount(Convert.ToInt64(lblTransferInID.Text));

                TransferInDetails clsTransferInDetails = clsTransferIn.Details(Convert.ToInt64(lblTransferInID.Text));

                clsTransferInItem.CommitAndDispose();

                UpdateFooter(clsTransferInDetails);
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
            TransferInItem clsTransferInItem = new TransferInItem();
            TransferInItemDetails clsTransferInItemDetails = clsTransferInItem.Details(Convert.ToInt64(stID));
            clsTransferInItem.CommitAndDispose();

            cboProductCode.Items.Clear();
            cboVariation.Items.Clear();
            cboProductUnit.Items.Clear();

            txtProductCode.Text = clsTransferInItemDetails.BarCode;
            cmdProductCode_Click(null, null);

            cboProductCode.SelectedIndex = cboProductCode.Items.IndexOf(new ListItem(clsTransferInItemDetails.ProductCode, clsTransferInItemDetails.ProductID.ToString()));

            if (clsTransferInItemDetails.VariationMatrixID == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0")); cboVariation.SelectedIndex = 0; }
            else
            { cboVariation.SelectedIndex = cboVariation.Items.IndexOf(new ListItem(clsTransferInItemDetails.MatrixDescription, clsTransferInItemDetails.VariationMatrixID.ToString())); }

            if (clsTransferInItemDetails.ProductUnitID == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); cboProductUnit.SelectedIndex = 0; }
            else
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsTransferInItemDetails.ProductUnitCode, clsTransferInItemDetails.ProductUnitID.ToString()));
            }

            txtQuantity.Text = clsTransferInItemDetails.Quantity.ToString("###0.##0");
            txtPrice.Text = clsTransferInItemDetails.UnitCost.ToString("###0.##0");
            txtDiscount.Text = clsTransferInItemDetails.DiscountApplied.ToString("###0.##0");

            if (clsTransferInItemDetails.DiscountType == DiscountTypes.Percentage)
                chkInPercent.Checked = true;
            else
            {
                chkInPercent.Checked = false;
            }
            txtAmount.Text = clsTransferInItemDetails.Amount.ToString("###0.##0");
            txtRemarks.Text = clsTransferInItemDetails.Remarks;
            lblTransferInItemID.Text = stID;
            chkIsTaxable.Checked = clsTransferInItemDetails.IsVatable;

            //Added Jan 1, 2010 4:20PM : For selling information
            txtSellingQuantity.Text = "1";
            try
            { txtMargin.Text = decimal.Parse(Convert.ToString(((clsTransferInItemDetails.SellingPrice - clsTransferInItemDetails.UnitCost) / clsTransferInItemDetails.UnitCost) * 100)).ToString("###0.##0"); }
            catch { txtMargin.Text = "0.00"; }
            txtSellingPrice.Text = clsTransferInItemDetails.SellingPrice.ToString("###0.##0");
            txtVAT.Text = clsTransferInItemDetails.SellingVAT.ToString("###0.##0");
            txtEVAT.Text = clsTransferInItemDetails.SellingEVAT.ToString("###0.##0");
            txtLocalTax.Text = clsTransferInItemDetails.SellingLocalTax.ToString("###0.##0");

            //Added April 28, 2010 4:20PM : For selling information
            txtOldSellingPrice.Text = clsTransferInItemDetails.OldSellingPrice.ToString("###0.##0");

            // Aug 9, 2011 : Lemu
            // For Required Inventory Days
            //txtRID.Text = clsTransferInItemDetails.RID.ToString();

            txtProductCode.Focus();
            ShowCommandButtons(true);
        }
        private void UpdateItemReceiveStatus(long TransferInItemID, TransferInItemReceivedStatus clsTransferInItemReceivedStatus, decimal ReceivedQuantity)
        {

            TransferInItem clsTransferInItem = new TransferInItem();
            clsTransferInItem.UpdateReceiveStatus(TransferInItemID, clsTransferInItemReceivedStatus, ReceivedQuantity);
            clsTransferInItem.CommitAndDispose();

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

			TransferInItem clsTransferInItem = new TransferInItem();
			lstItem.DataSource = clsTransferInItem.ListAsDataTable(Convert.ToInt64(lblTransferInID.Text)).DefaultView;
			lstItem.DataBind();
			clsTransferInItem.CommitAndDispose();
		}
		private void IssueGRN()
		{
            DateTime DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);

            ERPConfig clsERPConfig = new ERPConfig();
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
            clsERPConfig.CommitAndDispose();

            if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
            {
                long TransferInID = Convert.ToInt64(lblTransferInID.Text);
                string SupplierDRNo = txtSupplierDRNo.Text;

                TransferIn clsTransferIn = new TransferIn();
                clsTransferIn.IssueGRN(TransferInID, SupplierDRNo, DeliveryDate);
                clsTransferIn.CommitAndDispose();

                string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&transferinid=" + Common.Encrypt(TransferInID.ToString(), Session.SessionID);
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
        private void PrintTransferIn()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("transferinreport", Session.SessionID) + "&transferinid=" + Common.Encrypt(lblTransferInID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferIn/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }
        private void PrintTransferInSelling()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("transferinreport", Session.SessionID) + "&transferinid=" + Common.Encrypt(lblTransferInID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("TransferInReportSellingPrice", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferIn/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrintSellingPrice, this.updPrintSellingPrice.GetType(), "openwindow", javaScript, true);
        }
        private void UpdateHeader()
        {
            string stID = lblTransferInID.Text;

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&transferinid=" + Common.Encrypt(stID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void GenerateItems()
        {
            TransferIn clsTransferIn = new TransferIn();
            clsTransferIn.GenerateItemsForReorder(Convert.ToInt64(lblTransferInID.Text));
            clsTransferIn.CommitAndDispose();
        }
        private void UpdateTransferInDiscount()
        {
            TransferInDetails clsTransferInDetails = new TransferInDetails();
            clsTransferInDetails.TransferInID = Convert.ToInt64(lblTransferInID.Text);
            clsTransferInDetails.DiscountApplied = Convert.ToDecimal(txtTransferInDiscountApplied.Text);
            clsTransferInDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscountType.SelectedItem.Value);

            clsTransferInDetails.Discount2Applied = Convert.ToDecimal(txtTransferInDiscount2Applied.Text);
            clsTransferInDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount2Type.SelectedItem.Value);

            clsTransferInDetails.Discount3Applied = Convert.ToDecimal(txtTransferInDiscount3Applied.Text);
            clsTransferInDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount3Type.SelectedItem.Value);

            TransferIn clsTransferIn = new TransferIn();
            clsTransferIn.UpdateDiscount(clsTransferInDetails.TransferInID, clsTransferInDetails.DiscountApplied, clsTransferInDetails.DiscountType, clsTransferInDetails.Discount2Applied, clsTransferInDetails.Discount2Type, clsTransferInDetails.Discount3Applied, clsTransferInDetails.Discount3Type);
            clsTransferIn.SynchronizeAmount(Convert.ToInt64(lblTransferInID.Text));
            clsTransferInDetails = clsTransferIn.Details(Convert.ToInt64(lblTransferInID.Text));
            clsTransferIn.CommitAndDispose();

            UpdateFooter(clsTransferInDetails);
        }
        private void UpdateFreight()
        {
            TransferInDetails clsTransferInDetails = new TransferInDetails();
            clsTransferInDetails.TransferInID = Convert.ToInt64(lblTransferInID.Text);
            clsTransferInDetails.Freight = Convert.ToDecimal(txtTransferInFreight.Text);

            TransferIn clsTransferIn = new TransferIn();
            clsTransferIn.UpdateFreight(clsTransferInDetails.TransferInID, clsTransferInDetails.Freight);
            clsTransferIn.SynchronizeAmount(Convert.ToInt64(lblTransferInID.Text));
            clsTransferInDetails = clsTransferIn.Details(Convert.ToInt64(lblTransferInID.Text));
            clsTransferIn.CommitAndDispose();

            UpdateFooter(clsTransferInDetails);
        }
        private void UpdateDeposit()
        {
            TransferInDetails clsTransferInDetails = new TransferInDetails();
            clsTransferInDetails.TransferInID = Convert.ToInt64(lblTransferInID.Text);
            clsTransferInDetails.Deposit = Convert.ToDecimal(txtTransferInDeposit.Text);

            TransferIn clsTransferIn = new TransferIn();
            clsTransferIn.UpdateDeposit(clsTransferInDetails.TransferInID, clsTransferInDetails.Deposit);
            clsTransferIn.SynchronizeAmount(Convert.ToInt64(lblTransferInID.Text));
            clsTransferInDetails = clsTransferIn.Details(Convert.ToInt64(lblTransferInID.Text));
            clsTransferIn.CommitAndDispose();

            UpdateFooter(clsTransferInDetails);
        }
        private void UpdateFooter(TransferInDetails clsTransferInDetails)
        {
            lblTransferInDiscount.Text = clsTransferInDetails.Discount.ToString("#,##0.#0");
            lblTransferInDiscount2.Text = clsTransferInDetails.Discount2.ToString("#,##0.#0");
            lblTransferInDiscount3.Text = clsTransferInDetails.Discount3.ToString("#,##0.#0");

            lblTotalDiscount1.Text = Convert.ToDecimal(clsTransferInDetails.SubTotal + clsTransferInDetails.Discount + clsTransferInDetails.Discount2 + clsTransferInDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsTransferInDetails.SubTotal + clsTransferInDetails.Discount2 + clsTransferInDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsTransferInDetails.SubTotal + clsTransferInDetails.Discount3).ToString("#,##0.#0");

            lblTransferInVatableAmount.Text = clsTransferInDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferInFreight.Text = clsTransferInDetails.Freight.ToString("#,##0.#0");
            txtTransferInDeposit.Text = clsTransferInDetails.Deposit.ToString("#,##0.#0");
            lblTransferInVAT.Text = clsTransferInDetails.VAT.ToString("#,##0.#0");
            if (chkIsVatInclusive.Checked)
            {
                lblTransferInSubTotal.Text = Convert.ToDecimal(clsTransferInDetails.SubTotal - clsTransferInDetails.VAT).ToString("#,##0.#0");
                lblTransferInTotal.Text = clsTransferInDetails.SubTotal.ToString("#,##0.#0");
            }
            else
            {
                lblTransferInSubTotal.Text = clsTransferInDetails.SubTotal.ToString("#,##0.#0");
                lblTransferInTotal.Text = Convert.ToDecimal(clsTransferInDetails.SubTotal + clsTransferInDetails.VAT).ToString("#,##0.#0");
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

                TransferIn clsTransferIn = new TransferIn();
                clsTransferIn.GetConnection();
                TransferInDetails clsTransferInDetails = new TransferInDetails();

                TransferInItem clsTransferInItem = new TransferInItem(clsTransferIn.Connection, clsTransferIn.Transaction);
                TransferInItemDetails clsTransferInItemDetails;

                Contacts clsContact = new Contacts(clsTransferIn.Connection, clsTransferIn.Transaction);
                ContactDetails clsContactDetails;

                ContactGroup clsContactGroup = new ContactGroup(clsTransferIn.Connection, clsTransferIn.Transaction);
                ContactGroupDetails clsContactGroupDetails;

                Data.Unit clsUnit = new Data.Unit(clsTransferIn.Connection, clsTransferIn.Transaction);
                UnitDetails clsUnitDetails;

                ProductGroup clsProductGroup = new Data.ProductGroup(clsTransferIn.Connection, clsTransferIn.Transaction);
                ProductGroupDetails clsProductGroupDetails;

                ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup(clsTransferIn.Connection, clsTransferIn.Transaction);
                ProductSubGroupDetails clsProductSubGroupDetails;

                Products clsProduct = new Products(clsTransferIn.Connection, clsTransferIn.Transaction);
                ProductDetails clsProductDetails;

                ProductVariation clsProductVariation = new ProductVariation(clsTransferIn.Connection, clsTransferIn.Transaction);
                ProductVariationDetails clsProductVariationDetails;

                Branch clsBranch = new Branch(clsTransferIn.Connection, clsTransferIn.Transaction);
                BranchDetails clsBranchDetails;

                long lngProductID = 0; long lngProductCtr = 0;

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (xmlReader.Name == "TransferInDetails")
                            {
                                clsTransferInDetails.TransferInNo = lnkTransferInNo.Text;
                                clsTransferInDetails.TransferInDate = DateTime.Parse(lblTransferInDate.Text);

                                clsTransferInDetails.SupplierCode = xmlReader.GetAttribute("SupplierCode").ToString();
                                clsTransferInDetails.SupplierContact = xmlReader.GetAttribute("SupplierContact").ToString();
                                clsTransferInDetails.SupplierAddress = xmlReader.GetAttribute("SupplierAddress").ToString();
                                clsTransferInDetails.SupplierTelephoneNo = xmlReader.GetAttribute("SupplierTelephoneNo").ToString();
                                clsTransferInDetails.SupplierModeOfTerms = int.Parse(xmlReader.GetAttribute("SupplierModeOfTerms").ToString());
                                clsTransferInDetails.SupplierTerms = int.Parse(xmlReader.GetAttribute("SupplierTerms").ToString());
                                clsTransferInDetails.SupplierID = clsContact.Details(xmlReader.GetAttribute("SupplierCode").ToString()).ContactID;
                                if (clsTransferInDetails.SupplierID == 0)
                                {
                                    clsContactDetails = new ContactDetails();
                                    clsContactDetails.ContactCode = clsTransferInDetails.SupplierCode;
                                    clsContactDetails.ContactName = xmlReader.GetAttribute("SupplierName").ToString();
                                    clsContactDetails.BusinessName = clsTransferInDetails.SupplierContact;
                                    clsContactDetails.Address = clsTransferInDetails.SupplierAddress;
                                    clsContactDetails.TelephoneNo = clsTransferInDetails.SupplierTelephoneNo;
                                    clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsTransferInDetails.SupplierModeOfTerms.ToString());
                                    clsContactDetails.Terms = clsTransferInDetails.SupplierTerms;
                                    clsContactDetails.Remarks = "Added in from Imported TransferIn #";
                                    clsContactDetails.ContactGroupID = int.Parse(Contacts.DEFAULT_SUPPLIER_ID.ToString("d"));
                                    clsContactDetails.DateCreated = DateTime.Now;
                                    clsTransferInDetails.SupplierID = clsContact.Insert(clsContactDetails);
                                }
                                clsTransferInDetails.RequiredDeliveryDate = DateTime.Parse(xmlReader.GetAttribute("RequiredDeliveryDate").ToString());
                                clsTransferInDetails.BranchID = clsBranch.Details(xmlReader.GetAttribute("BranchCode")).BranchID;
                                if (clsTransferInDetails.BranchID == 0)
                                {
                                    clsBranchDetails = new BranchDetails();
                                    clsBranchDetails.BranchCode = xmlReader.GetAttribute("BranchCode");
                                    clsBranchDetails.BranchName = xmlReader.GetAttribute("BranchName");
                                    clsBranchDetails.Address = xmlReader.GetAttribute("BranchAddress");
                                    clsBranchDetails.DBIP = xmlReader.GetAttribute("BranchDBIP");
                                    clsBranchDetails.DBPort = xmlReader.GetAttribute("BranchDBPort");
                                    clsBranchDetails.Remarks = xmlReader.GetAttribute("BranchRemarks");
                                    clsTransferInDetails.BranchID = clsBranch.Insert(clsBranchDetails);
                                }

                                clsTransferInDetails.TransferrerID = long.Parse(xmlReader.GetAttribute("TransferrerID"));
                                clsTransferInDetails.TransferrerName = xmlReader.GetAttribute("TransferrerName");

                                clsTransferInDetails.SubTotal = decimal.Parse(xmlReader.GetAttribute("SubTotal"));
                                clsTransferInDetails.Discount = decimal.Parse(xmlReader.GetAttribute("Discount"));
                                clsTransferInDetails.DiscountApplied = decimal.Parse(xmlReader.GetAttribute("DiscountApplied"));
                                clsTransferInDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("DiscountType"));
                                clsTransferInDetails.VAT = decimal.Parse(xmlReader.GetAttribute("VAT"));
                                clsTransferInDetails.VatableAmount = decimal.Parse(xmlReader.GetAttribute("VatableAmount"));
                                clsTransferInDetails.EVAT = decimal.Parse(xmlReader.GetAttribute("EVAT"));
                                clsTransferInDetails.EVatableAmount = decimal.Parse(xmlReader.GetAttribute("EVatableAmount"));
                                clsTransferInDetails.LocalTax = decimal.Parse(xmlReader.GetAttribute("LocalTax"));
                                clsTransferInDetails.Freight = decimal.Parse(xmlReader.GetAttribute("Freight"));
                                clsTransferInDetails.Deposit = decimal.Parse(xmlReader.GetAttribute("Deposit"));
                                clsTransferInDetails.UnpaidAmount = decimal.Parse(xmlReader.GetAttribute("UnpaidAmount"));
                                clsTransferInDetails.PaidAmount = decimal.Parse(xmlReader.GetAttribute("PaidAmount"));
                                clsTransferInDetails.TotalItemDiscount = decimal.Parse(xmlReader.GetAttribute("TotalItemDiscount"));
                                clsTransferInDetails.Status = (TransferInStatus)Enum.Parse(typeof(TransferInStatus), xmlReader.GetAttribute("Status"));
                                clsTransferInDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsTransferInDetails.SupplierDRNo = xmlReader.GetAttribute("SupplierDRNo");
                                clsTransferInDetails.DeliveryDate = DateTime.Parse(xmlReader.GetAttribute("DeliveryDate"));
                                clsTransferInDetails.CancelledDate = DateTime.Parse(xmlReader.GetAttribute("CancelledDate"));
                                clsTransferInDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsTransferInDetails.CancelledRemarks = xmlReader.GetAttribute("CancelledRemarks");
                                clsTransferInDetails.CancelledByID = long.Parse(xmlReader.GetAttribute("CancelledByID"));

                                clsTransferIn.Update(clsTransferInDetails);

                            }
                            else if (xmlReader.Name == "TransferInItem")
                            {
                                clsTransferInItemDetails = new TransferInItemDetails();
                                clsTransferInItemDetails.TransferInID = long.Parse(lblTransferInID.Text);

                                clsTransferInItemDetails.ProductCode = xmlReader.GetAttribute("ProductCode");
                                clsTransferInItemDetails.BarCode = xmlReader.GetAttribute("BarCode");
                                clsTransferInItemDetails.Description = xmlReader.GetAttribute("ProductDesc");
                                clsTransferInItemDetails.ProductSubGroup = xmlReader.GetAttribute("ItemProductSubGroup");
                                clsTransferInItemDetails.ProductGroup = xmlReader.GetAttribute("ItemProductGroup");
                                clsTransferInItemDetails.ProductUnitID = Convert.ToInt32(xmlReader.GetAttribute("ItemProductUnitID"));
                                clsTransferInItemDetails.ProductUnitCode = xmlReader.GetAttribute("ItemProductUnitCode");
                                clsTransferInItemDetails.Quantity = Convert.ToDecimal(xmlReader.GetAttribute("ItemQuantity"));
                                clsTransferInItemDetails.UnitCost = Convert.ToDecimal(xmlReader.GetAttribute("ItemUnitCost"));
                                clsTransferInItemDetails.Discount = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscount"));
                                clsTransferInItemDetails.DiscountApplied = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscountApplied"));
                                clsTransferInItemDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("ItemDiscountType"));
                                clsTransferInItemDetails.Amount = Convert.ToDecimal(xmlReader.GetAttribute("ItemAmount"));
                                clsTransferInItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsTransferInItemDetails.VatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemVatableAmount"));
                                clsTransferInItemDetails.EVatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVatableAmount"));
                                clsTransferInItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsTransferInItemDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemVAT"));
                                clsTransferInItemDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVAT"));
                                clsTransferInItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsTransferInItemDetails.isVATInclusive = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemisVATInclusive")));
                                clsTransferInItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsTransferInItemDetails.TransferInItemStatus = (TransferInItemStatus)Enum.Parse(typeof(TransferInItemStatus), xmlReader.GetAttribute("ItemTransferInItemStatus"));
                                clsTransferInItemDetails.VariationMatrixID = Convert.ToInt64(xmlReader.GetAttribute("ItemVariationMatrixID"));
                                clsTransferInItemDetails.MatrixDescription = xmlReader.GetAttribute("ItemBaseVariationDescription");
                                clsTransferInItemDetails.ProductGroup = xmlReader.GetAttribute("ProductGroup");
                                clsTransferInItemDetails.ProductSubGroup = xmlReader.GetAttribute("ProductSubGroup");
                                clsTransferInItemDetails.Remarks = xmlReader.GetAttribute("ItemRemarks");
                                clsTransferInItemDetails.SellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingPrice"));
                                clsTransferInItemDetails.SellingVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingVAT"));
                                clsTransferInItemDetails.SellingEVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingEVAT"));
                                clsTransferInItemDetails.SellingLocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingLocalTax"));
                                clsTransferInItemDetails.OldSellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemOldSellingPrice"));

                                clsTransferInItemDetails.ProductID = clsProduct.Details(clsTransferInItemDetails.BarCode).ProductID;
                                lngProductID = clsTransferInItemDetails.ProductID;
                                if (clsTransferInItemDetails.ProductID == 0)
                                {
                                    clsTransferInItemDetails.ProductID = clsProduct.Details(clsTransferInItemDetails.ProductCode).ProductID;
                                    if (clsTransferInItemDetails.ProductID == 0)
                                    {
                                        //insert new product
                                        clsProductDetails = new ProductDetails();
                                        clsProductDetails.BarCode = clsTransferInItemDetails.BarCode;
                                        clsProductDetails.ProductCode = clsTransferInItemDetails.ProductCode;
                                        clsProductDetails.ProductDesc = clsTransferInItemDetails.Description;
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

                                        clsProductDetails.SupplierCode = clsTransferInDetails.SupplierCode;
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

                                            clsContactDetails.ContactCode = clsTransferInDetails.SupplierCode;
                                            clsContactDetails.ContactName = clsTransferInDetails.SupplierContact;

                                            clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsTransferInDetails.SupplierModeOfTerms.ToString());
                                            clsContactDetails.Terms = clsTransferInDetails.SupplierTerms;
                                            clsContactDetails.Address = clsTransferInDetails.SupplierAddress;
                                            clsContactDetails.BusinessName = clsTransferInDetails.SupplierContact;
                                            clsContactDetails.TelephoneNo = clsTransferInDetails.SupplierTelephoneNo;
                                            clsContactDetails.Remarks = "Added in XML import";
                                            clsContactDetails.Debit = 0;
                                            clsContactDetails.Credit = 0;
                                            clsContactDetails.IsCreditAllowed = 0;
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
                                            clsProductGroupDetails.BaseUnitID = clsProductDetails.BaseUnitID;
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

                                        clsTransferInItemDetails.ProductID = clsProduct.Insert(clsProductDetails);
                                    }
                                    else
                                    {
                                        //product code already exist but not the same barcode
                                        clsProduct.UpdateBarcode(clsTransferInItemDetails.ProductID, clsTransferInItemDetails.BarCode);
                                    }
                                    lngProductID = clsTransferInItemDetails.ProductID;
                                }

                                clsTransferInItem.Insert(clsTransferInItemDetails);

                                clsTransferInDetails = new TransferInDetails();
                                clsTransferInDetails.TransferInID = clsTransferInItemDetails.TransferInID;
                                clsTransferInDetails.DiscountApplied = Convert.ToDecimal(txtTransferInDiscountApplied.Text);
                                clsTransferInDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscountType.SelectedItem.Value);

                                clsTransferInDetails.Discount2Applied = Convert.ToDecimal(txtTransferInDiscount2Applied.Text);
                                clsTransferInDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount2Type.SelectedItem.Value);

                                clsTransferInDetails.Discount3Applied = Convert.ToDecimal(txtTransferInDiscount2Applied.Text);
                                clsTransferInDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboTransferInDiscount3Type.SelectedItem.Value);

                                clsTransferIn = new TransferIn(clsTransferInItem.Connection, clsTransferInItem.Transaction);
                                clsTransferIn.UpdateDiscount(clsTransferInItemDetails.TransferInID, clsTransferInDetails.DiscountApplied, clsTransferInDetails.DiscountType, clsTransferInDetails.Discount2Applied, clsTransferInDetails.Discount2Type, clsTransferInDetails.Discount3Applied, clsTransferInDetails.Discount3Type);

                                clsTransferInDetails = clsTransferIn.Details(clsTransferInItemDetails.TransferInID);
                                UpdateFooter(clsTransferInDetails);

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
                                //lblError.Text += "<b>" + xmlReader.Name + ":</b>" + xmlReader.Value + "<br>";
                            }
                            break;
                        case XmlNodeType.Text:
                            //lblError.Text += "<b>" + xmlReader.LocalName + ":</b>" + xmlReader.Value + "<br>";
                            break;
                    }
                }
                xmlReader.Close();

                clsTransferIn.CommitAndDispose();
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

		#endregion
    
    }
}
