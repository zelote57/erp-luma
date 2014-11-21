namespace AceSoft.RetailPlus.PurchasesAndPayables._Returns
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Post : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
				LoadItems();
				cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
				imgDelete.Attributes.Add("onClick", "return confirm_delete();");
				cmdEdit.Attributes.Add("onClick", "return confirm_select();");
				imgEdit.Attributes.Add("onClick", "return confirm_select();");
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
			Common Common = new Common();
			Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
			Response.Redirect(Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
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
		protected void imgPost_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Post();
		}
		protected void cmdPost_Click(object sender, System.EventArgs e)
		{
			Post();
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
			{ cboVariation.Items.Add(new ListItem("No Variation", "0"));}
			cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);

			cboProductUnit.DataTextField = "BottomUnitCode";
			cboProductUnit.DataValueField = "BottomUnitID";
			cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID,"a.MatrixID",SortOption.Ascending).DefaultView;
			cboProductUnit.DataBind();

            Products clsProduct = new Products(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            ProductDetails clsDetails = clsProduct.Details(ProductID: ProductID, MatrixID: Int64.Parse(cboVariation.SelectedItem.Value));

            //ProductPackage clsProductPackage = new ProductPackage(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            //ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByProductID(ProductID, Int64.Parse(cboVariation.SelectedItem.Value));

            clsProductVariationsMatrix.CommitAndDispose();

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
                ProductDetails clsProductDetails = clsProducts.Details(ProductID: ProductID, MatrixID: MatrixID);
                clsProducts.CommitAndDispose();

                txtPrice.Text = clsProductDetails.PurchasePrice.ToString("####0.##0");
                txtSellingPrice.Text = clsProductDetails.Price.ToString("#####0.##0");
                txtOldSellingPrice.Text = clsProductDetails.Price.ToString("#####0.##0");
                decimal decMargin = clsProductDetails.Price - clsProductDetails.PurchasePrice;
                try { decMargin = decMargin / clsProductDetails.PurchasePrice; }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                txtMargin.Text = decMargin.ToString("#,##0.##0");
                txtVAT.Text = clsProductDetails.VAT.ToString("#,##0.##0");
                txtEVAT.Text = clsProductDetails.EVAT.ToString("#,##0.##0");
                txtLocalTax.Text = clsProductDetails.LocalTax.ToString("#,##0.##0");

                if (clsProductDetails.VAT > 0)
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
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(SearchKey: stSearchKey, limit: 100);
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
            cboVariation.DataSource = clsProductVariationsMatrix.BaseListAsDataTable(ProductID, MatrixDescription:  stSearchKey, SortField: "VariationDesc").DefaultView;
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
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["DebitMemoItemID"].ToString();

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
                lblUnitCost.Text = Convert.ToDecimal(dr["UnitCost"].ToString()).ToString("#,##0.#0");

                Label lblDiscountApplied = (Label)e.Item.FindControl("lblDiscountApplied");
                lblDiscountApplied.Text = Convert.ToDecimal(dr["DiscountApplied"].ToString()).ToString("#,##0.#0");

                DiscountTypes DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                if (DiscountType == DiscountTypes.Percentage)
                {
                    Label lblPercent = (Label)e.Item.FindControl("lblPercent");
                    lblPercent.Visible = true;
                }

                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

                Label lblVAT = (Label)e.Item.FindControl("lblVAT");
                lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0");

                Label lblEVAT = (Label)e.Item.FindControl("lblEVAT");
                lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0");

                Label lblisVATInclusive = (Label)e.Item.FindControl("lblisVATInclusive");
                lblisVATInclusive.Text = Convert.ToBoolean(Convert.ToInt16(dr["isVATInclusive"].ToString())).ToString();

                Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                ImageButton imgItemReceive = (ImageButton)e.Item.FindControl("imgItemReceive");
                Label lblPODebitItemReceivedStatus = (Label)e.Item.FindControl("lblPODebitItemReceivedStatus");
                DebitMemoItemReceivedStatus clsDebitMemoItemReceivedStatus = (DebitMemoItemReceivedStatus)Enum.Parse(typeof(DebitMemoItemReceivedStatus), dr["DebitMemoItemReceivedStatus"].ToString());
                lblPODebitItemReceivedStatus.Text = clsDebitMemoItemReceivedStatus.ToString("d");

                if (clsDebitMemoItemReceivedStatus == DebitMemoItemReceivedStatus.Received)
                {
                    imgItemReceive.ToolTip = "Tag item as " + POItemReceivedStatus.NotYetReceived.ToString("G");
                    e.Item.CssClass = "ms-item-received";
                }
                else
                {
                    imgItemReceive.ToolTip = "Tag item as " + DebitMemoItemReceivedStatus.Received.ToString("G");
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
                    Label lblPODebitItemReceivedStatus = (Label)e.Item.FindControl("lblPODebitItemReceivedStatus");
                    DebitMemoItemReceivedStatus clsDebitMemoItemReceivedStatus = (DebitMemoItemReceivedStatus)Enum.Parse(typeof(DebitMemoItemReceivedStatus), lblPODebitItemReceivedStatus.Text);

                    if (clsDebitMemoItemReceivedStatus == DebitMemoItemReceivedStatus.Received)
                    {
                        clsDebitMemoItemReceivedStatus = DebitMemoItemReceivedStatus.NotYetReceived;
                    }
                    else
                    {
                        clsDebitMemoItemReceivedStatus = DebitMemoItemReceivedStatus.Received;
                        e.Item.CssClass = "ms-item-received";
                    }
                    UpdateItemReceiveStatus(long.Parse(chkList.Value), clsDebitMemoItemReceivedStatus, decimal.Parse(lblQuantity.Text));
                    lblPODebitItemReceivedStatus.Text = clsDebitMemoItemReceivedStatus.ToString("d");
                    lstItemFixCssClass();
                    break;
            }
        }
        private void lstItemFixCssClass()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblPODebitItemReceivedStatus = (Label)item.FindControl("lblPODebitItemReceivedStatus");
                DebitMemoItemReceivedStatus itemDebitMemoItemReceivedStatus = (DebitMemoItemReceivedStatus)Enum.Parse(typeof(DebitMemoItemReceivedStatus), lblPODebitItemReceivedStatus.Text);
                if (itemDebitMemoItemReceivedStatus == DebitMemoItemReceivedStatus.Received)
                    item.CssClass = "ms-item-received";
                else if (item.ItemType == ListItemType.Item)
                    item.CssClass = "";
                else if (item.ItemType == ListItemType.AlternatingItem)
                    item.CssClass = "ms-alternating";

            }
        }
        protected void cmdUpdateHeader_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            UpdateHeader();
        }
        protected void imgUpdateHeader_Click(object sender, System.EventArgs e)
        {
            UpdateHeader();
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintPOReturn();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintPOReturn();
        }
        protected void txtPODebitMemoDiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void cboPODebitMemoDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void txtPODebitMemoFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtPODebitMemoDeposit_TextChanged(object sender, EventArgs e)
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
        protected void chkIsVatInclusive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                long POReturnsID = long.Parse(lblDebitMemoID.Text);

                POReturns clsPOReturns = new POReturns();
                clsPOReturns.UpdateIsVatInclusive(POReturnsID, chkIsVatInclusive.Checked);

                POReturnDetails clsPOReturnDetails = clsPOReturns.Details(POReturnsID);
                clsPOReturns.CommitAndDispose();

                UpdateFooter(clsPOReturnDetails);
            }
            catch (Exception ex) { throw ex; }
        }
        //protected void imgAddProduct_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID) + "&windowaction=" + Common.Encrypt("close", Session.SessionID);
        //    string javaScript =
        //     "<script type='text/javascript'>\n" +
        //     "<!--\n" +
        //     "window.open('" + newWindowUrl + "');\n" +
        //     "// -->\n" +
        //     "</script>\n";
        //    this.Page.ClientScript.RegisterStartupScript(GetType(), "openwindow", javaScript);
        //}

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

			cboProductCode_SelectedIndexChanged(null, null);

            txtQuantity.Text = "1";
            txtPrice.Text = "0.00";
            txtDiscount.Text = "0";
            txtSellingQuantity.Text = "0";
            txtMargin.Text = "10";
            txtSellingPrice.Text = "0";
            txtOldSellingPrice.Text = "0";

            txtRemarks.Text = "";
            ComputeItemAmount();
            lblPODebitMemoItemID.Text = "0";

			txtPostDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
            lnkAddProduct.NavigateUrl = newWindowUrl;

            //imgProductHistory.Visible = false;
            //imgProductPriceHistory.Visible = false;
            //imgChangePrice.Visible = false;
            //imgEditNow.Visible = false;
            //lnkProductDetails.Visible = false;
            //cmdVariationSearch.Visible = false;
            //imgVariationQuickAdd.Visible = false;
            //lnkVariationAdd.Visible = false;
            //lnkProductUnitMatrix.Visible = false;
            //lblPurchasePriceHistory.Visible = false;
            ShowCommandButtons(false);
		}
		private void LoadRecord()
		{
			Common Common = new Common();
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["retid"],Session.SessionID));
			POReturns clsPOReturns = new POReturns();
			POReturnDetails clsDetails = clsPOReturns.Details(iID);
			clsPOReturns.CommitAndDispose();

			lblDebitMemoID.Text = clsDetails.DebitMemoID.ToString();
			lnkReturnNo.Text = clsDetails.MemoNo;
            lnkReturnNo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&retid=" + Common.Encrypt(clsDetails.DebitMemoID.ToString(), Session.SessionID);
			lblReturnDate.Text = clsDetails.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
			lblRequiredReturnDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
			lblSupplierID.Text = clsDetails.SupplierID.ToString();
			lnkSupplierCode.Text = clsDetails.SupplierCode.ToString();
            lnkSupplierCode.NavigateUrl = "../_Vendor/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(), Session.SessionID);	
			lnkSupplierContact.Text = clsDetails.SupplierContact;
            lnkSupplierContact.NavigateUrl = "../_Vendor/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(), Session.SessionID);	
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
			lblReturnRemarks.Text = clsDetails.Remarks;

            txtPODebitMemoDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboPODebitMemoDiscountType.SelectedIndex = cboPODebitMemoDiscountType.Items.IndexOf(cboPODebitMemoDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblPODebitMemoDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODebitMemoDiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboPODebitMemoDiscount2Type.SelectedIndex = cboPODebitMemoDiscount2Type.Items.IndexOf(cboPODebitMemoDiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblPODebitMemoDiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODebitMemoDiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboPODebitMemoDiscount3Type.SelectedIndex = cboPODebitMemoDiscount3Type.Items.IndexOf(cboPODebitMemoDiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblPODebitMemoDiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblPODebitMemoVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtPODebitMemoFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtPODebitMemoDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblPODebitMemoSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
            lblPODebitMemoVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblPODebitMemoTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
		}
		private void SaveRecord()
		{
			POReturnItemDetails clsDetails = new POReturnItemDetails();

			Products clsProducts = new Products();
            ProductDetails clsProductDetails = clsProducts.Details1(Constants.BRANCH_ID_MAIN, Convert.ToInt64(cboProductCode.SelectedItem.Value));
			
			Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(Int32.Parse(Session["BranchID"].ToString()), Session["TerminalNo"].ToString());
			clsProducts.CommitAndDispose();

			clsDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
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
                if (chkInPercent.Checked == true)
                    clsDetails.DiscountType = DiscountTypes.Percentage;
                else
                    clsDetails.DiscountType = DiscountTypes.FixedValue;
            }
            else
            {
                clsDetails.DiscountType = DiscountTypes.NotApplicable;
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

			POReturnItems clsPOReturnItems = new POReturnItems();
            if (lblPODebitMemoItemID.Text != "0")
			{
                clsDetails.DebitMemoItemID = Convert.ToInt64(lblPODebitMemoItemID.Text);
				clsPOReturnItems.Update(clsDetails);
			}
			else
				clsPOReturnItems.Insert(clsDetails);

            POReturnDetails clsPOReturnDetails = new POReturnDetails();
            clsPOReturnDetails.DebitMemoID = clsDetails.DebitMemoID;
            clsPOReturnDetails.DiscountApplied = Convert.ToDecimal(txtPODebitMemoDiscountApplied.Text);
            clsPOReturnDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscountType.SelectedItem.Value);

            clsPOReturnDetails.Discount2Applied = Convert.ToDecimal(txtPODebitMemoDiscount2Applied.Text);
            clsPOReturnDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscount2Type.SelectedItem.Value);

            clsPOReturnDetails.Discount3Applied = Convert.ToDecimal(txtPODebitMemoDiscount3Applied.Text);
            clsPOReturnDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscount3Type.SelectedItem.Value);

            POReturns clsPOReturns = new POReturns(clsPOReturnItems.Connection, clsPOReturnItems.Transaction);
            clsPOReturns.UpdateDiscount(clsDetails.DebitMemoID, clsPOReturnDetails.DiscountApplied, clsPOReturnDetails.DiscountType, clsPOReturnDetails.Discount2Applied, clsPOReturnDetails.Discount2Type, clsPOReturnDetails.Discount3Applied, clsPOReturnDetails.Discount3Type);

			clsPOReturnDetails = clsPOReturns.Details(clsDetails.DebitMemoID);
			clsPOReturnItems.CommitAndDispose();

            UpdateFooter(clsPOReturnDetails);
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

			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
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
				POReturnItems clsPOReturnItems = new POReturnItems();
				clsPOReturnItems.Delete( stIDs.Substring(0,stIDs.Length-1));

				POReturns clsPOReturns = new POReturns(clsPOReturnItems.Connection, clsPOReturnItems.Transaction);
				clsPOReturns.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));

				POReturnDetails clsPOReturnDetails = clsPOReturns.Details(Convert.ToInt64(lblDebitMemoID.Text));
				clsPOReturnItems.CommitAndDispose();

                UpdateFooter(clsPOReturnDetails);
			}

			return boRetValue;
		}
		private void UpdateItem()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					POReturnItems clsPOReturnItems = new POReturnItems();
                    POReturnItemDetails clsPOReturnItemDetails = clsPOReturnItems.Details(Convert.ToInt64(stID));
					clsPOReturnItems.CommitAndDispose();

					cboProductCode.Items.Clear();
					cboVariation.Items.Clear();
					cboProductUnit.Items.Clear();

                    cboProductCode.Items.Add(new ListItem(clsPOReturnItemDetails.ProductCode, clsPOReturnItemDetails.ProductID.ToString()));
                    cboProductCode.SelectedIndex = 0;
                    if (clsPOReturnItemDetails.VariationMatrixID == 0)
                    { cboVariation.Items.Add(new ListItem("No Variation", "0")); }
                    else
                    { cboVariation.Items.Add(new ListItem(clsPOReturnItemDetails.MatrixDescription, clsPOReturnItemDetails.VariationMatrixID.ToString())); }
                    cboVariation.SelectedIndex = 0;
                    cboProductUnit.Items.Add(new ListItem(clsPOReturnItemDetails.ProductUnitCode, clsPOReturnItemDetails.ProductUnitID.ToString()));
                    cboProductUnit.SelectedIndex = 0;
                    txtQuantity.Text = clsPOReturnItemDetails.Quantity.ToString("###0.#0");
                    txtPrice.Text = clsPOReturnItemDetails.UnitCost.ToString("###0.#0");
                    txtDiscount.Text = clsPOReturnItemDetails.Discount.ToString("###0.#0");
                    if (clsPOReturnItemDetails.DiscountType == DiscountTypes.Percentage)
                        chkInPercent.Checked = true;
                    else
                    {
                        chkInPercent.Checked = false;
                    }
                    txtAmount.Text = clsPOReturnItemDetails.Amount.ToString("###0.#0");
                    txtRemarks.Text = clsPOReturnItemDetails.Remarks;
                    lblPODebitMemoItemID.Text = stID;
                    chkIsTaxable.Checked = clsPOReturnItemDetails.IsVatable;
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
            POReturnItems clsPOReturnItems = new POReturnItems();
            POReturnItemDetails clsPOReturnItemsDetails = clsPOReturnItems.Details(Convert.ToInt64(stID));
            clsPOReturnItems.CommitAndDispose();

            cboProductCode.Items.Clear();
            cboVariation.Items.Clear();
            cboProductUnit.Items.Clear();

            txtProductCode.Text = clsPOReturnItemsDetails.BarCode;
            cmdProductCode_Click(null, null);

            cboProductCode.SelectedIndex = cboProductCode.Items.IndexOf(new ListItem(clsPOReturnItemsDetails.ProductCode, clsPOReturnItemsDetails.ProductID.ToString()));

            if (clsPOReturnItemsDetails.VariationMatrixID == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0")); cboVariation.SelectedIndex = 0; }
            else
            { cboVariation.SelectedIndex = cboVariation.Items.IndexOf(new ListItem(clsPOReturnItemsDetails.MatrixDescription, clsPOReturnItemsDetails.VariationMatrixID.ToString())); }

            if (clsPOReturnItemsDetails.ProductUnitID == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); cboProductUnit.SelectedIndex = 0; }
            else
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsPOReturnItemsDetails.ProductUnitCode, clsPOReturnItemsDetails.ProductUnitID.ToString()));
            }

            txtQuantity.Text = clsPOReturnItemsDetails.Quantity.ToString("###0.##0");
            txtPrice.Text = clsPOReturnItemsDetails.UnitCost.ToString("###0.##0");
            txtDiscount.Text = clsPOReturnItemsDetails.DiscountApplied.ToString("###0.##0");

            if (clsPOReturnItemsDetails.DiscountType == DiscountTypes.Percentage)
                chkInPercent.Checked = true;
            else
            {
                chkInPercent.Checked = false;
            }
            txtAmount.Text = clsPOReturnItemsDetails.Amount.ToString("###0.##0");
            txtRemarks.Text = clsPOReturnItemsDetails.Remarks;
            lblPODebitMemoItemID.Text = stID;
            chkIsTaxable.Checked = clsPOReturnItemsDetails.IsVatable;

            ////Added Jan 1, 2010 4:20PM : For selling information
            //txtSellingQuantity.Text = "1";
            //try
            //{ txtMargin.Text = decimal.Parse(Convert.ToString(((clsPOReturnItemsDetails.SellingPrice - clsPOReturnItemsDetails.UnitCost) / clsPOReturnItemsDetails.UnitCost) * 100)).ToString("###0.##0"); }
            //catch { txtMargin.Text = "0.00"; }
            //txtSellingPrice.Text = clsPOReturnItemsDetails.SellingPrice.ToString("###0.##0");
            //txtVAT.Text = clsPOReturnItemsDetails.SellingVAT.ToString("###0.##0");
            //txtEVAT.Text = clsPOReturnItemsDetails.SellingEVAT.ToString("###0.##0");
            //txtLocalTax.Text = clsPOReturnItemsDetails.SellingLocalTax.ToString("###0.##0");

            ////Added April 28, 2010 4:20PM : For selling information
            //txtOldSellingPrice.Text = clsPOReturnItemsDetails.OldSellingPrice.ToString("###0.##0");

            //// Aug 9, 2011 : Lemu
            //// For Required Inventory Days
            //txtRID.Text = clsPOReturnItemsDetails.RID.ToString();

            txtProductCode.Focus();
            ShowCommandButtons(true);
        }
        private void UpdateItemReceiveStatus(long DebitMemoItemID, DebitMemoItemReceivedStatus clsDebitMemoItemReceivedStatus, decimal ReceivedQuantity)
        {

            POReturnItems clsPOReturnItems = new POReturnItems();
            clsPOReturnItems.UpdateReceiveStatus(DebitMemoItemID, clsDebitMemoItemReceivedStatus, ReceivedQuantity);
            clsPOReturnItems.CommitAndDispose();

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

			POReturnItems clsPOReturnItems = new POReturnItems();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsPOReturnItems.List(Convert.ToInt64(lblDebitMemoID.Text), "DebitMemoItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsPOReturnItems.CommitAndDispose();
            lstItemFixCssClass();
		}
		private void Post()
		{
			DateTime DeliveryDate = Convert.ToDateTime(txtPostDate.Text);

			ERPConfig clsERPConfig = new ERPConfig();
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
			clsERPConfig.CommitAndDispose();
			
			if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
			{
				long DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
				string SupplierDocNo = txtSupplierDocNo.Text;

				POReturns clsPOReturns = new POReturns();
				clsPOReturns.Post(DebitMemoID, SupplierDocNo, DeliveryDate);
				clsPOReturns.CommitAndDispose();

				Common Common = new Common();
				string stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + "&retid=" + Common.Encrypt(DebitMemoID.ToString(),Session.SessionID);	
				Response.Redirect("Default.aspx" + stParam);
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Sorry you cannot post using the delivery date: " + txtPostDate.Text + ". Please enter an allowable posting date.')";
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
            txtAmount.Text = amount.ToString("####0.#0");
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
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
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
			
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						iCount += 1;
						if (iCount >= 2)
						{	return false;	}
					}
				}
			}
			return boChkListSingle;
		}
        private void PrintPOReturn()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("poretun", Session.SessionID) + "&retid=" + Common.Encrypt(lblDebitMemoID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Returns/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }
        private void UpdateHeader()
        {
            string stID = lblDebitMemoID.Text;

            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&retid=" + Common.Encrypt(stID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void UpdatePODiscount()
        {
            POReturnDetails clsPOReturnDetails = new POReturnDetails();
            clsPOReturnDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsPOReturnDetails.DiscountApplied = Convert.ToDecimal(txtPODebitMemoDiscountApplied.Text);
            clsPOReturnDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscountType.SelectedItem.Value);

            clsPOReturnDetails.Discount2Applied = Convert.ToDecimal(txtPODebitMemoDiscount2Applied.Text);
            clsPOReturnDetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscount2Type.SelectedItem.Value);

            clsPOReturnDetails.Discount3Applied = Convert.ToDecimal(txtPODebitMemoDiscount3Applied.Text);
            clsPOReturnDetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODebitMemoDiscount3Type.SelectedItem.Value);


            POReturns clsPOReturns = new POReturns();
            clsPOReturns.UpdateDiscount(clsPOReturnDetails.DebitMemoID, clsPOReturnDetails.DiscountApplied, clsPOReturnDetails.DiscountType, clsPOReturnDetails.Discount2Applied, clsPOReturnDetails.Discount2Type, clsPOReturnDetails.Discount3Applied, clsPOReturnDetails.Discount3Type);
            clsPOReturns.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));
            clsPOReturnDetails = clsPOReturns.Details(Convert.ToInt64(lblDebitMemoID.Text));
            clsPOReturns.CommitAndDispose();

            UpdateFooter(clsPOReturnDetails);
        }
        private void UpdateFreight()
        {
            POReturnDetails clsPOReturnDetails = new POReturnDetails();
            clsPOReturnDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsPOReturnDetails.Freight = Convert.ToDecimal(txtPODebitMemoFreight.Text);

            POReturns clsPOReturns = new POReturns();
            clsPOReturns.UpdateFreight(clsPOReturnDetails.DebitMemoID, clsPOReturnDetails.Freight);
            clsPOReturns.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));
            clsPOReturnDetails = clsPOReturns.Details(Convert.ToInt64(lblDebitMemoID.Text));
            clsPOReturns.CommitAndDispose();

            UpdateFooter(clsPOReturnDetails);
        }
        private void UpdateDeposit()
        {
            POReturnDetails clsPOReturnDetails = new POReturnDetails();
            clsPOReturnDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsPOReturnDetails.Deposit = Convert.ToDecimal(txtPODebitMemoDeposit.Text);

            POReturns clsPOReturns = new POReturns();
            clsPOReturns.UpdateDeposit(clsPOReturnDetails.DebitMemoID, clsPOReturnDetails.Deposit);
            clsPOReturns.SynchronizeAmount(clsPOReturnDetails.DebitMemoID);
            clsPOReturnDetails = clsPOReturns.Details(clsPOReturnDetails.DebitMemoID);
            clsPOReturns.CommitAndDispose();

            UpdateFooter(clsPOReturnDetails);
        }
        private void UpdateFooter(POReturnDetails clsPOReturnDetails)
        {
            lblPODebitMemoDiscount.Text = clsPOReturnDetails.Discount.ToString("#,##0.#0");
            lblPODebitMemoDiscount2.Text = clsPOReturnDetails.Discount2.ToString("#,##0.#0");
            lblPODebitMemoDiscount3.Text = clsPOReturnDetails.Discount3.ToString("#,##0.#0");

            lblTotalDiscount1.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal + clsPOReturnDetails.Discount + clsPOReturnDetails.Discount2 + clsPOReturnDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal + clsPOReturnDetails.Discount2 + clsPOReturnDetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal + clsPOReturnDetails.Discount3).ToString("#,##0.#0");

            lblPODebitMemoVatableAmount.Text = clsPOReturnDetails.VatableAmount.ToString("#,##0.#0");
            txtPODebitMemoFreight.Text = clsPOReturnDetails.Freight.ToString("#,##0.#0");
            txtPODebitMemoDeposit.Text = clsPOReturnDetails.Deposit.ToString("#,##0.#0");
            lblPODebitMemoVAT.Text = clsPOReturnDetails.VAT.ToString("#,##0.#0");
            if (chkIsVatInclusive.Checked)
            {
                lblPODebitMemoSubTotal.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal - clsPOReturnDetails.VAT).ToString("#,##0.#0");
                lblPODebitMemoTotal.Text = clsPOReturnDetails.SubTotal.ToString("#,##0.#0");
            }
            else
            {
                lblPODebitMemoSubTotal.Text = clsPOReturnDetails.SubTotal.ToString("#,##0.#0");
                lblPODebitMemoTotal.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal + clsPOReturnDetails.VAT).ToString("#,##0.#0");
            }
        }

		#endregion

    }
}
