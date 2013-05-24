namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
    using System.IO;
    using System.Xml;

	public partial  class __Post : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
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

            if (cboProductCode.Items.Count == 1 && cboProductCode.SelectedValue == "0")
                return;

            DataClass clsDataClass = new DataClass();
            long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

            ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
            cboVariation.DataTextField = "VariationDescOnly";
            cboVariation.DataValueField = "MatrixID";
            cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.BaseList(ProductID, "VariationDesc", SortOption.Ascending)).DefaultView;
            cboVariation.DataBind();

            if (cboVariation.Items.Count == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0")); }
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            cboProductUnit.DataTextField = "BottomUnitCode";
            cboProductUnit.DataValueField = "BottomUnitID";
            cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID, "a.MatrixID", SortOption.Ascending).DefaultView;
            cboProductUnit.DataBind();

            Products clsProduct = new Products(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            ProductDetails clsDetails = clsProduct.Details(ProductID);
            ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            System.Data.DataTable dtProductPurchasePriceHistory = clsProductPurchasePriceHistory.ListAsDataTable(ProductID, "PurchasePrice", SortOption.Ascending);

            ProductPackage clsProductPackage = new ProductPackage(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByBarCode(txtProductCode.Text);

            clsProductVariationMatrix.CommitAndDispose();

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
            txtRID.Text = clsDetails.RID.ToString();

            if (clsProductPackageDetails.PackageID == 0)
            {
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
            }
            else if (clsProductPackageDetails.PackageID != 0)
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsProductPackageDetails.UnitCode, clsProductPackageDetails.UnitID.ToString()));

                txtPrice.Text = clsProductPackageDetails.PurchasePrice.ToString("#####0.##0");
                txtSellingPrice.Text = clsProductPackageDetails.Price.ToString("#####0.##0");
                txtOldSellingPrice.Text = clsProductPackageDetails.Price.ToString("#####0.##0");
                decimal decMargin = clsProductPackageDetails.Price - clsProductPackageDetails.PurchasePrice;
                try { decMargin = decMargin / clsProductPackageDetails.PurchasePrice; }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                txtMargin.Text = decMargin.ToString("#,##0.##0");
                txtVAT.Text = clsProductPackageDetails.VAT.ToString("#,##0.##0");
                txtEVAT.Text = clsProductPackageDetails.EVAT.ToString("#,##0.##0");
                txtLocalTax.Text = clsProductPackageDetails.LocalTax.ToString("#,##0.##0");

                if (clsProductPackageDetails.VAT > 0) chkIsTaxable.Checked = true;
                else chkIsTaxable.Checked = false;
            }

            if (cboProductUnit.Items.Count == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); }
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ComputeItemAmount();
            cboVariation_SelectedIndexChanged(null, null);

            //if (ProductID != 0)
            //{
            //    lnkVariationAdd.Visible = true;
            //    string stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
            //                "&prodid=" + Common.Encrypt(ProductID.ToString(), Session.SessionID);
            //    lnkVariationAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam;
            //}
            //else { lnkVariationAdd.Visible = false; }
        }
        protected void cboVariation_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			long VariationMatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
			if (VariationMatrixID != 0)
			{
				long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

				ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
				ProductBaseMatrixDetails clsProductBaseMatrixDetails = clsProductVariationMatrix.BaseDetails(VariationMatrixID, ProductID);
				clsProductVariationMatrix.CommitAndDispose();

                txtPrice.Text = clsProductBaseMatrixDetails.PurchasePrice.ToString("####0.##0");
                txtSellingPrice.Text = clsProductBaseMatrixDetails.Price.ToString("#####0.##0");
                txtOldSellingPrice.Text = clsProductBaseMatrixDetails.Price.ToString("#####0.##0");
                decimal decMargin = clsProductBaseMatrixDetails.Price - clsProductBaseMatrixDetails.PurchasePrice;
                try { decMargin = decMargin / clsProductBaseMatrixDetails.PurchasePrice; }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                txtMargin.Text = decMargin.ToString("#,##0.##0");
                txtVAT.Text = clsProductBaseMatrixDetails.VAT.ToString("#,##0.##0");
                txtEVAT.Text = clsProductBaseMatrixDetails.EVAT.ToString("#,##0.##0");
                txtLocalTax.Text = clsProductBaseMatrixDetails.LocalTax.ToString("#,##0.##0");

				if (clsProductBaseMatrixDetails.VAT > 0)
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
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(ProductListFilterType.ShowInactiveOnly, stSearchKey, 0, 0, string.Empty, 0, string.Empty, 100, false, false, "ProductCode", SortOption.Ascending);
			cboProductCode.DataBind();
			clsProduct.CommitAndDispose();

            bool bolShowCommandButtons = false;
            if (cboProductCode.Items.Count == 0)
            {
                Data.ProductPackage clsProductPackage = new Data.ProductPackage();
                Data.ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByBarCode(txtProductCode.Text);
                if (clsProductPackageDetails.PackageID != 0)
                {
                    clsProduct = new Products(clsProductPackage.Connection, clsProductPackage.Transaction);
                    Data.ProductDetails clsProductDetails = clsProduct.Details(clsProductPackageDetails.ProductID);

                    cboProductCode.Items.Add(new ListItem(clsProductDetails.ProductCode, clsProductDetails.ProductID.ToString()));
                }
                else
                {
                    cboProductCode.Items.Add(new ListItem("No product", "0"));
                    bolShowCommandButtons = false;
                }
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

			ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
            cboVariation.DataTextField = "Description";
			cboVariation.DataValueField = "MatrixID";
			cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.Search(ProductID, stSearchKey, "VariationDesc",SortOption.Ascending)).DefaultView;
			cboVariation.DataBind();

			if (cboVariation.Items.Count == 0)
			{
				cboVariation.Items.Add(new ListItem("No Variation", "0"));
			}
			cboVariation.SelectedIndex = cboVariation.Items.Count - 1;
			clsProductVariationMatrix.CommitAndDispose();
		}				
		protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["POItemID"].ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();

				HyperLink lnkDescription = (HyperLink) e.Item.FindControl("lnkDescription");
				lnkDescription.Text = dr["Description"].ToString();
				lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				HyperLink lnkMatrixDescription = (HyperLink) e.Item.FindControl("lnkMatrixDescription");
				if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null)
                {
                    lnkMatrixDescription.Visible = true;
					lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
					lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
				}
				
				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.##0");

				Label lblProductUnitID = (Label) e.Item.FindControl("lblProductUnitID");
				lblProductUnitID.Text = dr["ProductUnitID"].ToString();

				Label lblProductUnitCode = (Label) e.Item.FindControl("lblProductUnitCode");
				lblProductUnitCode.Text = dr["ProductUnitCode"].ToString();

				Label lblUnitCost = (Label) e.Item.FindControl("lblUnitCost");
                lblUnitCost.Text = Convert.ToDecimal(dr["UnitCost"].ToString()).ToString("#,##0.##0");

                Label lblDiscountApplied = (Label)e.Item.FindControl("lblDiscountApplied");
                lblDiscountApplied.Text = Convert.ToDecimal(dr["DiscountApplied"].ToString()).ToString("#,##0.##0");

                DiscountTypes DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                if (DiscountType == DiscountTypes.Percentage)
                {
                    Label lblPercent = (Label)e.Item.FindControl("lblPercent");
                    lblPercent.Visible = true;
                }

				Label lblAmount = (Label) e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.##0");

				Label lblVAT = (Label) e.Item.FindControl("lblVAT");
                lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.##0");

				Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
                lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.##0");

                Label lblisVATInclusive = (Label)e.Item.FindControl("lblisVATInclusive");
                lblisVATInclusive.Text = Convert.ToBoolean(Convert.ToInt16(dr["isVATInclusive"].ToString())).ToString();

                Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.##0");

				Label lblRemarks = (Label) e.Item.FindControl("lblRemarks");
				lblRemarks.Text = dr["Remarks"].ToString();

                ImageButton imgItemReceive = (ImageButton)e.Item.FindControl("imgItemReceive");
                Label lblPOItemReceivedStatus = (Label)e.Item.FindControl("lblPOItemReceivedStatus");
                POItemReceivedStatus clsPOItemReceivedStatus = (POItemReceivedStatus)Enum.Parse(typeof(POItemReceivedStatus), dr["POItemReceivedStatus"].ToString());
                lblPOItemReceivedStatus.Text = clsPOItemReceivedStatus.ToString("d");

                if (clsPOItemReceivedStatus == POItemReceivedStatus.Received)
                {
                    imgItemReceive.ToolTip = "Tag item as " + POItemReceivedStatus.NotYetReceived.ToString("G");
                    e.Item.CssClass = "ms-item-received";
                }
                else
                {
                    imgItemReceive.ToolTip = "Tag item as " + POItemReceivedStatus.Received.ToString("G");
                }

				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
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
                    Label lblPOItemReceivedStatus = (Label)e.Item.FindControl("lblPOItemReceivedStatus");
                    POItemReceivedStatus clsPOItemReceivedStatus = (POItemReceivedStatus)Enum.Parse(typeof(POItemReceivedStatus), lblPOItemReceivedStatus.Text);

                    if (clsPOItemReceivedStatus == POItemReceivedStatus.Received)
                    {
                        clsPOItemReceivedStatus = POItemReceivedStatus.NotYetReceived;
                    }
                    else 
                    {
                        clsPOItemReceivedStatus = POItemReceivedStatus.Received;
                        e.Item.CssClass = "ms-item-received";
                    }
                    UpdateItemReceiveStatus(long.Parse(chkList.Value), clsPOItemReceivedStatus, decimal.Parse(lblQuantity.Text));
                    lblPOItemReceivedStatus.Text = clsPOItemReceivedStatus.ToString("d");
                    lstItemFixCssClass();
                    break;
            }
        }
        private void lstItemFixCssClass()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblitemPOItemReceivedStatus = (Label)item.FindControl("lblPOItemReceivedStatus");
                POItemReceivedStatus itemPOItemReceivedStatus = (POItemReceivedStatus)Enum.Parse(typeof(POItemReceivedStatus), lblitemPOItemReceivedStatus.Text);
                if (itemPOItemReceivedStatus == POItemReceivedStatus.Received)
                    item.CssClass = "ms-item-received";
                else if (item.ItemType == ListItemType.Item)
                    item.CssClass = "";
                else if (item.ItemType == ListItemType.AlternatingItem)
                    item.CssClass = "ms-alternating";

            }
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintPO();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintPO();
        }
        protected void cmdPrintSelling_Click(object sender, EventArgs e)
        {
            PrintPOSelling();
        }
        protected void imgPrintSelling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintPOSelling();
        }
        protected void imgExport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToFile();
        }
        protected void cmdExport_Click(object sender, EventArgs e)
        {
            ExportToFile();
        }
        protected void cmdUpdateHeader_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            UpdateHeader();
        }
        protected void imgUpdateHeader_Click(object sender, System.EventArgs e)
        {
            UpdateHeader();
        }
        protected void imgGenerate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            GenerateItemsByRID();
            LoadRecord();
            LoadItems();
        }
        protected void cmdGenerate_Click(object sender, System.EventArgs e)
        {
            GenerateItemsByRID();
            LoadRecord();
            LoadItems();
        }
        protected void imgGenerateByThreshold_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            GenerateItemsByThreshold();
            LoadRecord();
            LoadItems();
        }
        protected void cmdGenerateByThreshold_Click(object sender, System.EventArgs e)
        {
            GenerateItemsByThreshold();
            LoadRecord();
            LoadItems();
        }
        protected void txtPODiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void cboPODiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void txtRID_TextChanged(object sender, EventArgs e)
        {
            UpdateQuantityByRID();
        }
        protected void txtPOFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtPODeposit_TextChanged(object sender, EventArgs e)
        {
            UpdateDeposit();
        }
        protected void imgProductQuickAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (txtProductCode.Text != null || txtProductCode.Text.Trim() != string.Empty || txtProductCode.Text.Trim() != "")
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
                    ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                    clsProductVariationsMatrix.InsertBaseVariationEasy(long.Parse(cboProductCode.SelectedItem.Value), txtVariation.Text);
                    clsProductVariationsMatrix.CommitAndDispose();

                    cmdVariationSearch_Click(null, null);
                }
            }
            catch{}
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
                long POID = long.Parse(lblPOID.Text);

                PO clsPO = new PO();
                clsPO.UpdateIsVatInclusive(POID, chkIsVatInclusive.Checked);

                PODetails clsPODetails = clsPO.Details(POID);
                clsPO.CommitAndDispose();

                UpdateFooter(clsPODetails);
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

			txtQuantity.Text = "1";
            txtRID.Text = "0";
            txtPrice.Text = "0.00";
			txtDiscount.Text = "0";
            txtSellingQuantity.Text = "0";
            txtMargin.Text = "10";
            txtSellingPrice.Text = "0";
            txtOldSellingPrice.Text = "0";
            txtOldSellingPrice.Text = "0";

			txtRemarks.Text = "";
			ComputeItemAmount();
			lblPOItemID.Text = "0";

			txtDeliveryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtIDCStartDate.Text = DateTime.Now.AddDays(-30).ToString("yyy-MM-dd");
            txtIDCEndDate.Text = DateTime.Now.ToString("yyy-MM-dd");

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
            try { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["poid"], Session.SessionID)); }
            catch { }
            try
            {   if (iID == 0) iID = Convert.ToInt64(lblPOID.Text);} catch { }

			PO clsPO = new PO();
			PODetails clsDetails = clsPO.Details(iID);
			clsPO.CommitAndDispose();

			lblPOID.Text = clsDetails.POID.ToString();
            lnkPONo.Text = clsDetails.PONo;
            lnkPONo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(clsDetails.POID.ToString(), Session.SessionID);

			lblPODate.Text = clsDetails.PODate.ToString("yyyy-MM-dd HH:mm:ss");
			lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            lblRID.Text = clsDetails.RID.ToString();
			lblSupplierID.Text = clsDetails.SupplierID.ToString();

			lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
			lblSupplierCode.NavigateUrl = "../_Vendor/Default.aspx?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);	

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
			lblPORemarks.Text = clsDetails.Remarks;

            txtPODiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboPODiscountType.SelectedIndex = cboPODiscountType.Items.IndexOf(cboPODiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblPODiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboPODiscount2Type.SelectedIndex = cboPODiscount2Type.Items.IndexOf(cboPODiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblPODiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboPODiscount3Type.SelectedIndex = cboPODiscount3Type.Items.IndexOf(cboPODiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblPODiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblPOVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtPOFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtPODeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblPOSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
            lblPOVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblPOTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");

		}
		private void SaveRecord()
		{
			POItemDetails clsDetails = new POItemDetails();

			Products clsProducts = new Products();
            ProductDetails clsProductDetails = clsProducts.Details(Constants.BRANCH_ID_MAIN, Convert.ToInt64(cboProductCode.SelectedItem.Value));
			
			Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
			TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);
			clsProducts.CommitAndDispose();

			clsDetails.POID = Convert.ToInt64(lblPOID.Text);
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
            clsDetails.RID = long.Parse(txtRID.Text);
            
            POItem clsPOItem = new POItem();
            if (lblPOItemID.Text != "0")
            {
                clsDetails.POItemID = Convert.ToInt64(lblPOItemID.Text);
                clsPOItem.Update(clsDetails);
            }
            else
                clsPOItem.Insert(clsDetails);

            PODetails clsPODetails = new PODetails();
            clsPODetails.POID = clsDetails.POID;
            clsPODetails.DiscountApplied = Convert.ToDecimal(txtPODiscountApplied.Text);
            clsPODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscountType.SelectedItem.Value);

            clsPODetails.Discount2Applied = Convert.ToDecimal(txtPODiscount2Applied.Text);
            clsPODetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount2Type.SelectedItem.Value);

            clsPODetails.Discount3Applied = Convert.ToDecimal(txtPODiscount3Applied.Text);
            clsPODetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount3Type.SelectedItem.Value);

            PO clsPO = new PO(clsPOItem.Connection, clsPOItem.Transaction);
            clsPO.UpdateDiscount(clsDetails.POID, clsPODetails.DiscountApplied, clsPODetails.DiscountType, clsPODetails.Discount2Applied, clsPODetails.Discount2Type, clsPODetails.Discount3Applied, clsPODetails.Discount3Type);
            
            clsPODetails = clsPO.Details(clsDetails.POID);
            clsPOItem.CommitAndDispose();

            UpdateFooter(clsPODetails);
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
				POItem clsPOItem = new POItem();
				clsPOItem.Delete( stIDs.Substring(0,stIDs.Length-1));

				PO clsPO = new PO(clsPOItem.Connection, clsPOItem.Transaction);
				clsPO.SynchronizeAmount(Convert.ToInt64(lblPOID.Text));

				PODetails clsPODetails = clsPO.Details(Convert.ToInt64(lblPOID.Text));

				clsPOItem.CommitAndDispose();

                UpdateFooter(clsPODetails);
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
            POItem clsPOItem = new POItem();
            POItemDetails clsPOItemDetails = clsPOItem.Details(Convert.ToInt64(stID));
            clsPOItem.CommitAndDispose();

            cboProductCode.Items.Clear();
            cboVariation.Items.Clear();
            cboProductUnit.Items.Clear();

            txtProductCode.Text = clsPOItemDetails.BarCode;
            cmdProductCode_Click(null, null);

            cboProductCode.SelectedIndex = cboProductCode.Items.IndexOf(new ListItem(clsPOItemDetails.ProductCode, clsPOItemDetails.ProductID.ToString()));

            if (clsPOItemDetails.VariationMatrixID == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0")); cboVariation.SelectedIndex = 0; }
            else
            { cboVariation.SelectedIndex = cboVariation.Items.IndexOf(new ListItem(clsPOItemDetails.MatrixDescription, clsPOItemDetails.VariationMatrixID.ToString()));}
            
            if (clsPOItemDetails.ProductUnitID == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); cboProductUnit.SelectedIndex = 0; }
            else
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsPOItemDetails.ProductUnitCode, clsPOItemDetails.ProductUnitID.ToString()));
            }
            
            txtQuantity.Text = clsPOItemDetails.Quantity.ToString("###0.##0");
            txtPrice.Text = clsPOItemDetails.UnitCost.ToString("###0.##0");
            txtDiscount.Text = clsPOItemDetails.DiscountApplied.ToString("###0.##0");

            if (clsPOItemDetails.DiscountType == DiscountTypes.Percentage)
                chkInPercent.Checked = true;
            else
            {
                chkInPercent.Checked = false;
            }
            txtAmount.Text = clsPOItemDetails.Amount.ToString("###0.##0");
            txtRemarks.Text = clsPOItemDetails.Remarks;
            lblPOItemID.Text = stID;
            chkIsTaxable.Checked = clsPOItemDetails.IsVatable;

            //Added Jan 1, 2010 4:20PM : For selling information
            txtSellingQuantity.Text = "1";
            try
            { txtMargin.Text = decimal.Parse(Convert.ToString(((clsPOItemDetails.SellingPrice - clsPOItemDetails.UnitCost) / clsPOItemDetails.UnitCost) * 100)).ToString("###0.##0");}
            catch { txtMargin.Text = "0.00"; }
            txtSellingPrice.Text = clsPOItemDetails.SellingPrice.ToString("###0.##0");
            txtVAT.Text = clsPOItemDetails.SellingVAT.ToString("###0.##0");
            txtEVAT.Text = clsPOItemDetails.SellingEVAT.ToString("###0.##0");
            txtLocalTax.Text = clsPOItemDetails.SellingLocalTax.ToString("###0.##0");

            //Added April 28, 2010 4:20PM : For selling information
            txtOldSellingPrice.Text = clsPOItemDetails.OldSellingPrice.ToString("###0.##0");

            // Aug 9, 2011 : Lemu
            // For Required Inventory Days
            txtRID.Text = clsPOItemDetails.RID.ToString();

            txtProductCode.Focus();
            ShowCommandButtons(true);
        }
        private void UpdateItemReceiveStatus(long POItemID, POItemReceivedStatus clsPOItemReceivedStatus, decimal ReceivedQuantity)
        {

            POItem clsPOItem = new POItem();
            clsPOItem.UpdateReceiveStatus(POItemID, clsPOItemReceivedStatus, ReceivedQuantity);
            clsPOItem.CommitAndDispose();

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

			POItem clsPOItem = new POItem();
			lstItem.DataSource = clsPOItem.ListAsDataTable(Convert.ToInt64(lblPOID.Text)).DefaultView;
			lstItem.DataBind();
			clsPOItem.CommitAndDispose();
		}
		private void IssueGRN()
		{
			DateTime DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);

			ERPConfig clsERPConfig = new ERPConfig();
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
			clsERPConfig.CommitAndDispose();
			
			if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
			{
				long POID = Convert.ToInt64(lblPOID.Text);
				string SupplierDRNo = txtSupplierDRNo.Text;

				PO clsPO = new PO();
				clsPO.IssueGRN(POID, SupplierDRNo, DeliveryDate);
				clsPO.CommitAndDispose();

				string stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + "&poid=" + Common.Encrypt(POID.ToString(),Session.SessionID);	
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
			{	amount = (quantity * (price - discount));	}
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
			{	amount = (quantity * (price - discount));	}

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
        private void PrintPO()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("po", Session.SessionID) + "&poid=" + Common.Encrypt(lblPOID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void PrintPOSelling()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("po", Session.SessionID) + "&poid=" + Common.Encrypt(lblPOID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("POReportSellingPrice", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void UpdateHeader()
        {
            string stID = lblPOID.Text;

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&poid=" + Common.Encrypt(stID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void GenerateItemsByRID()
        {
            PO clsPO = new PO();
            clsPO.GenerateItemsForReorderByRID(Convert.ToInt64(lblPOID.Text), Convert.ToInt64(lblRID.Text), Convert.ToDateTime(txtIDCStartDate.Text + " 00:00:00"), Convert.ToDateTime(txtIDCEndDate.Text + " 23:59:59"));
            clsPO.CommitAndDispose();
        }
        private void GenerateItemsByThreshold()
        {
            PO clsPO = new PO();
            clsPO.GenerateItemsForReorder(Convert.ToInt64(lblPOID.Text));
            clsPO.CommitAndDispose();
        }
        private void UpdatePODiscount()
        {
            PODetails clsPODetails = new PODetails();
            clsPODetails.POID = Convert.ToInt64(lblPOID.Text);
            clsPODetails.DiscountApplied = Convert.ToDecimal(txtPODiscountApplied.Text);
            clsPODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscountType.SelectedItem.Value);

            clsPODetails.Discount2Applied = Convert.ToDecimal(txtPODiscount2Applied.Text);
            clsPODetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount2Type.SelectedItem.Value);

            clsPODetails.Discount3Applied = Convert.ToDecimal(txtPODiscount3Applied.Text);
            clsPODetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount3Type.SelectedItem.Value);

            PO clsPO = new PO();
            clsPO.UpdateDiscount(clsPODetails.POID, clsPODetails.DiscountApplied, clsPODetails.DiscountType, clsPODetails.Discount2Applied, clsPODetails.Discount2Type, clsPODetails.Discount3Applied, clsPODetails.Discount3Type);
            clsPO.SynchronizeAmount(Convert.ToInt64(lblPOID.Text));
            clsPODetails = clsPO.Details(Convert.ToInt64(lblPOID.Text));
            clsPO.CommitAndDispose();

            UpdateFooter(clsPODetails);
        }
        private void UpdateFreight()
        {
            PODetails clsPODetails = new PODetails();
            clsPODetails.POID = Convert.ToInt64(lblPOID.Text);
            clsPODetails.Freight = Convert.ToDecimal(txtPOFreight.Text);

            PO clsPO = new PO();
            clsPO.UpdateFreight(clsPODetails.POID, clsPODetails.Freight);
            clsPO.SynchronizeAmount(Convert.ToInt64(lblPOID.Text));
            clsPODetails = clsPO.Details(Convert.ToInt64(lblPOID.Text));
            clsPO.CommitAndDispose();

            UpdateFooter(clsPODetails);
        }
        private void UpdateQuantityByRID()
        {
            long lngProductID = 0;
            try { 
                lngProductID = long.Parse(cboProductCode.SelectedValue);

                if (lngProductID != 0)
                {
                    Products clsProduct = new Products();
                    txtQuantity.Text = clsProduct.getReorderQtyByRID(lngProductID, Convert.ToDateTime(txtIDCStartDate.Text + " 00:00:00"), Convert.ToDateTime(txtIDCEndDate.Text + " 23:59:59")).ToString("#,##0.##0");
                    clsProduct.CommitAndDispose();

                    ComputeItemAmount();
                }
            }
            catch { }
            
        }
        private void UpdateDeposit()
        {
            PODetails clsPODetails = new PODetails();
            clsPODetails.POID = Convert.ToInt64(lblPOID.Text);
            clsPODetails.Deposit = Convert.ToDecimal(txtPODeposit.Text);

            PO clsPO = new PO();
            clsPO.UpdateDeposit(clsPODetails.POID, clsPODetails.Deposit);
            clsPO.SynchronizeAmount(Convert.ToInt64(lblPOID.Text));
            clsPODetails = clsPO.Details(Convert.ToInt64(lblPOID.Text));
            clsPO.CommitAndDispose();

            UpdateFooter(clsPODetails);
        }
        private void UpdateFooter(PODetails clsPODetails)
        {
            lblPODiscount.Text = clsPODetails.Discount.ToString("#,##0.#0");
            lblPODiscount2.Text = clsPODetails.Discount2.ToString("#,##0.#0");
            lblPODiscount3.Text = clsPODetails.Discount3.ToString("#,##0.#0");

            lblTotalDiscount1.Text = Convert.ToDecimal(clsPODetails.SubTotal + clsPODetails.Discount + clsPODetails.Discount2 + clsPODetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsPODetails.SubTotal + clsPODetails.Discount2 + clsPODetails.Discount3).ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsPODetails.SubTotal + clsPODetails.Discount3).ToString("#,##0.#0");

            lblPOVatableAmount.Text = clsPODetails.VatableAmount.ToString("#,##0.#0");
            txtPOFreight.Text = clsPODetails.Freight.ToString("#,##0.#0");
            txtPODeposit.Text = clsPODetails.Deposit.ToString("#,##0.#0");
            lblPOVAT.Text = clsPODetails.VAT.ToString("#,##0.#0");
            if (chkIsVatInclusive.Checked)
            {
                lblPOSubTotal.Text = Convert.ToDecimal(clsPODetails.SubTotal - clsPODetails.VAT).ToString("#,##0.#0");
                lblPOTotal.Text = clsPODetails.SubTotal.ToString("#,##0.#0");
            }
            else
            {
                lblPOSubTotal.Text = clsPODetails.SubTotal.ToString("#,##0.#0");
                lblPOTotal.Text = Convert.ToDecimal(clsPODetails.SubTotal + clsPODetails.VAT).ToString("#,##0.#0");
            }
        }
        private void ExportToFile()
        {

            DataClass clsDataClass = new DataClass();

            PO clsPO = new PO();
            PODetails clsPODetails = clsPO.Details(long.Parse(lblPOID.Text));

            POItem clsPOItem = new POItem(clsPO.Connection, clsPO.Transaction);
            DataTable dtaPOItems = clsPOItem.ListAsDataTable(clsPODetails.POID, null, SortOption.Ascending);

            Branch clsBranch = new Branch(clsPO.Connection, clsPO.Transaction);
            BranchDetails clsBranchDetails;

            Contacts clsContact = new Contacts(clsPO.Connection, clsPO.Transaction);
            ContactDetails clsContactDetails;

            Products clsProduct = new Products(clsPO.Connection, clsPO.Transaction);
            ProductDetails clsProductDetails;

            ProductVariation clsProductVariation = new ProductVariation(clsPO.Connection, clsPO.Transaction);
            DataTable dtaProductVariation;

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsPO.Connection, clsPO.Transaction);
            DataTable dtaProductVariationsMatrix;

            string xmlFileName = Server.MapPath(@"\RetailPlus\temp\" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsPODetails.PONo + "_" + clsPODetails.PODate.ToString("yyyyMMddHHmmssffff") + ".xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteComment("This file represents the Purchase Order Details of PO No: '" + clsPODetails.PONo + "' for " + lblBranchCode.Text + " branch.");
            writer.WriteComment("Save this in your local file. Goto 'File', click 'Save As', select the location in your local directory, click 'Save'.");
            writer.WriteStartElement("PODetails");
            writer.WriteAttributeString("POID", XmlConvert.ToString(clsPODetails.POID));
            writer.WriteAttributeString("PONo", clsPODetails.PONo);
            writer.WriteAttributeString("PODate", clsPODetails.PODate.ToString("MM/dd/yyyy HH:mm:ss"));

            /******Supplier information******/
            clsContactDetails = clsContact.Details(clsPODetails.SupplierID);
            writer.WriteAttributeString("SupplierID", XmlConvert.ToString(clsContactDetails.ContactID));
            writer.WriteAttributeString("SupplierCode", clsContactDetails.ContactCode);
            writer.WriteAttributeString("SupplierName", clsContactDetails.ContactName);
            writer.WriteAttributeString("SupplierContact", clsContactDetails.BusinessName);
            writer.WriteAttributeString("SupplierAddress", clsPODetails.SupplierAddress);
            writer.WriteAttributeString("SupplierTelephoneNo", clsPODetails.SupplierTelephoneNo);
            writer.WriteAttributeString("SupplierModeOfTerms", XmlConvert.ToString(clsPODetails.SupplierModeOfTerms));
            writer.WriteAttributeString("SupplierTerms", XmlConvert.ToString(clsPODetails.SupplierTerms));
            writer.WriteAttributeString("SupplierContactGroupName", clsContactDetails.ContactGroupName);
            /******End Of Supplier Information******/

            writer.WriteAttributeString("RequiredDeliveryDate", clsPODetails.RequiredDeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));

            /******Branch & Purchaser Information******/
            clsBranchDetails = clsBranch.Details(short.Parse(clsPODetails.BranchID.ToString()));
            writer.WriteAttributeString("BranchID", XmlConvert.ToString(clsPODetails.BranchID));
            writer.WriteAttributeString("BranchCode", clsPODetails.BranchCode);
            writer.WriteAttributeString("BranchName", clsPODetails.BranchName);
            writer.WriteAttributeString("BranchAddress", clsPODetails.BranchAddress);
            writer.WriteAttributeString("BranchDBIP", clsBranchDetails.DBIP);
            writer.WriteAttributeString("BranchDBPort", clsBranchDetails.DBPort);
            writer.WriteAttributeString("BranchRemarks", clsBranchDetails.Remarks);
            writer.WriteAttributeString("PurchaserID", clsPODetails.PurchaserID.ToString());
            writer.WriteAttributeString("PurchaserName", clsPODetails.PurchaserName);
            /******End Of Branch & Purchaser Information******/

            /******Amount Information******/
            writer.WriteAttributeString("SubTotal", XmlConvert.ToString(clsPODetails.SubTotal));
            writer.WriteAttributeString("Discount", XmlConvert.ToString(clsPODetails.Discount));
            writer.WriteAttributeString("DiscountApplied", XmlConvert.ToString(clsPODetails.DiscountApplied));
            writer.WriteAttributeString("DiscountType", clsPODetails.DiscountType.ToString("d"));
            writer.WriteAttributeString("VAT", XmlConvert.ToString(clsPODetails.VAT));
            writer.WriteAttributeString("VatableAmount", XmlConvert.ToString(clsPODetails.VatableAmount));
            writer.WriteAttributeString("EVAT", XmlConvert.ToString(clsPODetails.EVAT));
            writer.WriteAttributeString("EVatableAmount", XmlConvert.ToString(clsPODetails.EVatableAmount));
            writer.WriteAttributeString("LocalTax", XmlConvert.ToString(clsPODetails.LocalTax));
            writer.WriteAttributeString("Freight", XmlConvert.ToString(clsPODetails.Freight));
            writer.WriteAttributeString("Deposit", XmlConvert.ToString(clsPODetails.Deposit));
            writer.WriteAttributeString("UnpaidAmount", XmlConvert.ToString(clsPODetails.UnpaidAmount));
            writer.WriteAttributeString("PaidAmount", XmlConvert.ToString(clsPODetails.PaidAmount));
            writer.WriteAttributeString("TotalItemDiscount", XmlConvert.ToString(clsPODetails.TotalItemDiscount));
            writer.WriteAttributeString("Status", clsPODetails.Status.ToString("d"));
            writer.WriteAttributeString("Remarks", clsPODetails.Remarks);
            writer.WriteAttributeString("SupplierDRNo", clsPODetails.SupplierDRNo);
            writer.WriteAttributeString("DeliveryDate", clsPODetails.DeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledDate", clsPODetails.CancelledDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledRemarks", clsPODetails.CancelledRemarks);
            writer.WriteAttributeString("CancelledByID", XmlConvert.ToString(clsPODetails.CancelledByID));
            /******End Of Branch Information******/

            foreach (DataRow row in dtaPOItems.Rows)
            {
                clsProductDetails = new ProductDetails();
                clsProductDetails = clsProduct.Details(Convert.ToInt64(row["ProductID"].ToString()));

                writer.WriteStartElement("POItem");
                writer.WriteAttributeString("POItemID", row["POItemID"].ToString());
                writer.WriteAttributeString("POID", row["POID"].ToString());
                writer.WriteAttributeString("ProductID", row["ProductID"].ToString());

                /*****Product Information*****/
                writer.WriteAttributeString("ProductCode", clsProductDetails.ProductCode);
                writer.WriteAttributeString("BarCode", clsProductDetails.BarCode);
                writer.WriteAttributeString("ProductDesc", clsProductDetails.ProductDesc);
                writer.WriteAttributeString("MatrixDescription", row["MatrixDescription"].ToString());
                writer.WriteAttributeString("ProductSubGroupID", clsProductDetails.ProductSubGroupID.ToString());
                writer.WriteAttributeString("ProductSubGroupCode", clsProductDetails.ProductSubGroupCode);
                writer.WriteAttributeString("ProductSubGroupName", clsProductDetails.ProductSubGroupName);
                writer.WriteAttributeString("ProductGroupID", clsProductDetails.ProductGroupID.ToString());
                writer.WriteAttributeString("ProductGroupCode", clsProductDetails.ProductGroupCode);
                writer.WriteAttributeString("ProductGroupName", clsProductDetails.ProductGroupName);
                writer.WriteAttributeString("BaseUnitID", clsProductDetails.BaseUnitID.ToString());
                writer.WriteAttributeString("BaseUnitCode", clsProductDetails.BaseUnitCode);
                writer.WriteAttributeString("BaseUnitName", clsProductDetails.BaseUnitName);
                writer.WriteAttributeString("DateCreated", clsProductDetails.DateCreated.ToString("MM/dd/yy HH:mm:ss"));
                writer.WriteAttributeString("Deleted", clsProductDetails.Deleted.ToString());
                writer.WriteAttributeString("Price", clsProductDetails.Price.ToString());
                writer.WriteAttributeString("PurchasePrice", clsProductDetails.PurchasePrice.ToString());
                writer.WriteAttributeString("IncludeInSubtotalDiscount", clsProductDetails.IncludeInSubtotalDiscount.ToString());
                writer.WriteAttributeString("VAT", clsProductDetails.VAT.ToString());
                writer.WriteAttributeString("EVAT", clsProductDetails.EVAT.ToString());
                writer.WriteAttributeString("LocalTax", clsProductDetails.LocalTax.ToString());
                writer.WriteAttributeString("Quantity", clsProductDetails.Quantity.ToString());
                writer.WriteAttributeString("MinThreshold", clsProductDetails.MinThreshold.ToString());
                writer.WriteAttributeString("MaxThreshold", clsProductDetails.MaxThreshold.ToString());
                writer.WriteAttributeString("OrderSlipPrinter", clsProductDetails.OrderSlipPrinter.ToString("d"));
                writer.WriteAttributeString("ChartOfAccountIDPurchase", clsProductDetails.ChartOfAccountIDPurchase.ToString());
                writer.WriteAttributeString("ChartOfAccountIDSold", clsProductDetails.ChartOfAccountIDSold.ToString());
                writer.WriteAttributeString("ChartOfAccountIDInventory", clsProductDetails.ChartOfAccountIDInventory.ToString());
                writer.WriteAttributeString("ChartOfAccountIDTaxPurchase", clsProductDetails.ChartOfAccountIDTaxPurchase.ToString());
                writer.WriteAttributeString("ChartOfAccountIDTaxSold", clsProductDetails.ChartOfAccountIDTaxSold.ToString());
                writer.WriteAttributeString("IsItemSold", clsProductDetails.IsItemSold.ToString());
                writer.WriteAttributeString("WillPrintProductComposition", clsProductDetails.WillPrintProductComposition.ToString());
                writer.WriteAttributeString("UpdatedBy", clsProductDetails.UpdatedBy.ToString());
                writer.WriteAttributeString("UpdatedOn", clsProductDetails.UpdatedOn.ToString("MM/dd/yyyy HH:mm"));
                writer.WriteAttributeString("PercentageCommision", clsProductDetails.PercentageCommision.ToString());
                writer.WriteAttributeString("Active", clsProductDetails.Active.ToString());
                /*****End Of Product Information*****/

                writer.WriteAttributeString("ItemProductGroup", row["ProductGroup"].ToString());
                writer.WriteAttributeString("ItemProductSubGroup", row["ProductSubGroup"].ToString());
                writer.WriteAttributeString("ItemVariationMatrixID", row["VariationMatrixID"].ToString());
                writer.WriteAttributeString("ItemBaseVariationDescription", row["MatrixDescription"].ToString());
                writer.WriteAttributeString("ItemProductUnitID", row["ProductUnitID"].ToString());
                writer.WriteAttributeString("ItemProductUnitCode", row["ProductUnitCode"].ToString());
                writer.WriteAttributeString("ItemQuantity", row["Quantity"].ToString());
                writer.WriteAttributeString("ItemUnitCost", row["UnitCost"].ToString());
                writer.WriteAttributeString("ItemDiscount", row["Discount"].ToString());
                writer.WriteAttributeString("ItemDiscountApplied", row["DiscountApplied"].ToString());
                writer.WriteAttributeString("ItemDiscountType", row["DiscountType"].ToString());
                writer.WriteAttributeString("ItemAmount", row["Amount"].ToString());
                writer.WriteAttributeString("ItemVAT", row["VAT"].ToString());
                writer.WriteAttributeString("ItemVatableAmount", row["VatableAmount"].ToString());
                writer.WriteAttributeString("ItemEVAT", row["EVAT"].ToString());
                writer.WriteAttributeString("ItemEVatableAmount", row["EVatableAmount"].ToString());
                writer.WriteAttributeString("ItemLocalTax", row["LocalTax"].ToString());
                writer.WriteAttributeString("ItemisVATInclusive", row["isVATInclusive"].ToString());
                writer.WriteAttributeString("ItemPOItemStatus", row["POItemStatus"].ToString());
                writer.WriteAttributeString("ItemIsVatable", row["IsVatable"].ToString());
                writer.WriteAttributeString("ItemSellingPrice", row["SellingPrice"].ToString());
                writer.WriteAttributeString("ItemSellingVAT", row["SellingVAT"].ToString());
                writer.WriteAttributeString("ItemSellingEVAT", row["SellingEVAT"].ToString());
                writer.WriteAttributeString("ItemSellingLocalTax", row["SellingLocalTax"].ToString());
                writer.WriteAttributeString("ItemOldSellingPrice", row["OldSellingPrice"].ToString());
                writer.WriteAttributeString("ItemRemarks", row["Remarks"].ToString());

                dtaProductVariation = clsProductVariation.ListAsDataTable(clsProductDetails.ProductID, null, System.Data.SqlClient.SortOrder.Ascending);
                foreach (DataRow rowVariation in dtaProductVariation.Rows)
                {
                    writer.WriteStartElement("Variation", null);
                    writer.WriteAttributeString("VariationCode", rowVariation["VariationCode"].ToString());
                    writer.WriteAttributeString("VariationType", rowVariation["VariationType"].ToString());
                    writer.WriteEndElement();
                }

                dtaProductVariationsMatrix = clsProductVariationsMatrix.ProductVariationsMatrixListAsDataTable(long.Parse(row["VariationMatrixID"].ToString()), null, System.Data.SqlClient.SortOrder.Ascending);
                foreach (DataRow rowVariationsMatrix in dtaProductVariationsMatrix.Rows)
                {
                    writer.WriteStartElement("VariationMatrix", null);
                    writer.WriteAttributeString("MatriXID", rowVariationsMatrix["MatriXID"].ToString());
                    writer.WriteAttributeString("VariationID", rowVariationsMatrix["VariationID"].ToString());
                    writer.WriteAttributeString("Description", rowVariationsMatrix["Description"].ToString());
                    writer.WriteAttributeString("VariationCode", rowVariationsMatrix["VariationCode"].ToString());
                    writer.WriteAttributeString("VariationType", rowVariationsMatrix["VariationType"].ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            //Write the XML to file and close the writer
            writer.Flush();
            writer.Close();

            clsPO.CommitAndDispose();

            string stScript = "<Script>";
            stScript += "window.open('/RetailPlus/temp/" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsPODetails.PONo + "_" + clsPODetails.PODate.ToString("yyyyMMddHHmmssffff") + ".xml')";
            stScript += "</Script>";
            Response.Write(stScript);
        }
        private void Import()
        {
            if (txtPath.HasFile)
            {
                string fn = System.IO.Path.GetFileName(txtPath.PostedFile.FileName);

                if (fn.Contains("_" + Constants.PURCHASE_ORDER_CODE) == false)
                {
                    string stScript = "<Script>";
                    stScript += "window.alert('Please select a VALID Purchase Order file to upload.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return;
                }

                string SaveLocation = "/RetailPlus/temp/uploaded_" + fn;

                txtPath.PostedFile.SaveAs(SaveLocation);
                XmlTextReader xmlReader = new XmlTextReader(SaveLocation);
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;

                PO clsPO = new PO();
                clsPO.GetConnection();
                PODetails clsPODetails = new PODetails();

                POItem clsPOItem = new POItem(clsPO.Connection, clsPO.Transaction);
                POItemDetails clsPOItemDetails;

                Contacts clsContact = new Contacts(clsPO.Connection, clsPO.Transaction);
                ContactDetails clsContactDetails;

                ContactGroup clsContactGroup = new ContactGroup(clsPO.Connection, clsPO.Transaction);
                ContactGroupDetails clsContactGroupDetails;

                Data.Unit clsUnit = new Data.Unit(clsPO.Connection, clsPO.Transaction);
                UnitDetails clsUnitDetails;

                ProductGroup clsProductGroup = new Data.ProductGroup(clsPO.Connection, clsPO.Transaction);
                ProductGroupDetails clsProductGroupDetails;

                ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup(clsPO.Connection, clsPO.Transaction);
                ProductSubGroupDetails clsProductSubGroupDetails;

                Products clsProduct = new Products(clsPO.Connection, clsPO.Transaction);
                ProductDetails clsProductDetails;

                ProductVariation clsProductVariation = new ProductVariation(clsPO.Connection, clsPO.Transaction);
                ProductVariationDetails clsProductVariationDetails;

                Branch clsBranch = new Branch(clsPO.Connection, clsPO.Transaction);
                BranchDetails clsBranchDetails;

                long lngProductID = 0; long lngProductCtr = 0;

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (xmlReader.Name == "PODetails")
                            {
                                clsPODetails.PONo = lnkPONo.Text;
                                clsPODetails.PODate = DateTime.Parse(lblPODate.Text);

                                clsPODetails.SupplierCode = xmlReader.GetAttribute("SupplierCode").ToString();
                                clsPODetails.SupplierContact = xmlReader.GetAttribute("SupplierContact").ToString();
                                clsPODetails.SupplierAddress = xmlReader.GetAttribute("SupplierAddress").ToString();
                                clsPODetails.SupplierTelephoneNo = xmlReader.GetAttribute("SupplierTelephoneNo").ToString();
                                clsPODetails.SupplierModeOfTerms = int.Parse(xmlReader.GetAttribute("SupplierModeOfTerms").ToString());
                                clsPODetails.SupplierTerms = int.Parse(xmlReader.GetAttribute("SupplierTerms").ToString());
                                clsPODetails.SupplierID = clsContact.Details(xmlReader.GetAttribute("SupplierCode").ToString()).ContactID;
                                if (clsPODetails.SupplierID == 0)
                                {
                                    clsContactDetails = new ContactDetails();
                                    clsContactDetails.ContactCode = clsPODetails.SupplierCode;
                                    clsContactDetails.ContactName = xmlReader.GetAttribute("SupplierName").ToString();
                                    clsContactDetails.BusinessName = clsPODetails.SupplierContact;
                                    clsContactDetails.Address = clsPODetails.SupplierAddress;
                                    clsContactDetails.TelephoneNo = clsPODetails.SupplierTelephoneNo;
                                    clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsPODetails.SupplierModeOfTerms.ToString());
                                    clsContactDetails.Terms = clsPODetails.SupplierTerms;
                                    clsContactDetails.Remarks = "Added in from Imported PO #";
                                    clsContactDetails.ContactGroupID = int.Parse(Contacts.DEFAULT_SUPPLIER_ID.ToString("d"));
                                    clsContactDetails.DateCreated = DateTime.Now;
                                    clsPODetails.SupplierID = clsContact.Insert(clsContactDetails);
                                }
                                clsPODetails.RequiredDeliveryDate = DateTime.Parse(xmlReader.GetAttribute("RequiredDeliveryDate").ToString());
                                clsPODetails.BranchID = clsBranch.Details(xmlReader.GetAttribute("BranchCode")).BranchID;
                                if (clsPODetails.BranchID == 0)
                                {
                                    clsBranchDetails = new BranchDetails();
                                    clsBranchDetails.BranchCode = xmlReader.GetAttribute("BranchCode");
                                    clsBranchDetails.BranchName = xmlReader.GetAttribute("BranchName");
                                    clsBranchDetails.Address = xmlReader.GetAttribute("BranchAddress");
                                    clsBranchDetails.DBIP = xmlReader.GetAttribute("BranchDBIP");
                                    clsBranchDetails.DBPort = xmlReader.GetAttribute("BranchDBPort");
                                    clsBranchDetails.Remarks = xmlReader.GetAttribute("BranchRemarks");
                                    clsPODetails.BranchID = clsBranch.Insert(clsBranchDetails);
                                }

                                clsPODetails.PurchaserID = long.Parse(xmlReader.GetAttribute("PurchaserID"));
                                clsPODetails.PurchaserName = xmlReader.GetAttribute("PurchaserName");

                                clsPODetails.SubTotal = decimal.Parse(xmlReader.GetAttribute("SubTotal"));
                                clsPODetails.Discount = decimal.Parse(xmlReader.GetAttribute("Discount"));
                                clsPODetails.DiscountApplied = decimal.Parse(xmlReader.GetAttribute("DiscountApplied"));
                                clsPODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("DiscountType"));
                                clsPODetails.VAT = decimal.Parse(xmlReader.GetAttribute("VAT"));
                                clsPODetails.VatableAmount = decimal.Parse(xmlReader.GetAttribute("VatableAmount"));
                                clsPODetails.EVAT = decimal.Parse(xmlReader.GetAttribute("EVAT"));
                                clsPODetails.EVatableAmount = decimal.Parse(xmlReader.GetAttribute("EVatableAmount"));
                                clsPODetails.LocalTax = decimal.Parse(xmlReader.GetAttribute("LocalTax"));
                                clsPODetails.Freight = decimal.Parse(xmlReader.GetAttribute("Freight"));
                                clsPODetails.Deposit = decimal.Parse(xmlReader.GetAttribute("Deposit"));
                                clsPODetails.UnpaidAmount = decimal.Parse(xmlReader.GetAttribute("UnpaidAmount"));
                                clsPODetails.PaidAmount = decimal.Parse(xmlReader.GetAttribute("PaidAmount"));
                                clsPODetails.TotalItemDiscount = decimal.Parse(xmlReader.GetAttribute("TotalItemDiscount"));
                                clsPODetails.Status = (POStatus)Enum.Parse(typeof(POStatus), xmlReader.GetAttribute("Status"));
                                clsPODetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsPODetails.SupplierDRNo = xmlReader.GetAttribute("SupplierDRNo");
                                clsPODetails.DeliveryDate = DateTime.Parse(xmlReader.GetAttribute("DeliveryDate"));
                                clsPODetails.CancelledDate = DateTime.Parse(xmlReader.GetAttribute("CancelledDate"));
                                clsPODetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsPODetails.CancelledRemarks = xmlReader.GetAttribute("CancelledRemarks");
                                clsPODetails.CancelledByID = long.Parse(xmlReader.GetAttribute("CancelledByID"));

                                clsPO.Update(clsPODetails);

                            }
                            else if (xmlReader.Name == "POItem")
                            {
                                clsPOItemDetails = new POItemDetails();
                                clsPOItemDetails.POID = long.Parse(lblPOID.Text);

                                clsPOItemDetails.ProductCode = xmlReader.GetAttribute("ProductCode");
                                clsPOItemDetails.BarCode = xmlReader.GetAttribute("BarCode");
                                clsPOItemDetails.Description = xmlReader.GetAttribute("ProductDesc");
                                clsPOItemDetails.ProductSubGroup = xmlReader.GetAttribute("ItemProductSubGroup");
                                clsPOItemDetails.ProductGroup = xmlReader.GetAttribute("ItemProductGroup");
                                clsPOItemDetails.ProductUnitID = Convert.ToInt32(xmlReader.GetAttribute("ItemProductUnitID"));
                                clsPOItemDetails.ProductUnitCode = xmlReader.GetAttribute("ItemProductUnitCode");
                                clsPOItemDetails.Quantity = Convert.ToDecimal(xmlReader.GetAttribute("ItemQuantity"));
                                clsPOItemDetails.UnitCost = Convert.ToDecimal(xmlReader.GetAttribute("ItemUnitCost"));
                                clsPOItemDetails.Discount = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscount"));
                                clsPOItemDetails.DiscountApplied = Convert.ToDecimal(xmlReader.GetAttribute("ItemDiscountApplied"));
                                clsPOItemDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), xmlReader.GetAttribute("ItemDiscountType"));
                                clsPOItemDetails.Amount = Convert.ToDecimal(xmlReader.GetAttribute("ItemAmount"));
                                clsPOItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsPOItemDetails.VatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemVatableAmount"));
                                clsPOItemDetails.EVatableAmount = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVatableAmount"));
                                clsPOItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsPOItemDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemVAT"));
                                clsPOItemDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemEVAT"));
                                clsPOItemDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemLocalTax"));
                                clsPOItemDetails.isVATInclusive = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemisVATInclusive")));
                                clsPOItemDetails.IsVatable = Convert.ToBoolean(Convert.ToInt16(xmlReader.GetAttribute("ItemIsVatable")));
                                clsPOItemDetails.POItemStatus = (POItemStatus)Enum.Parse(typeof(POItemStatus), xmlReader.GetAttribute("ItemPOItemStatus"));
                                clsPOItemDetails.VariationMatrixID = Convert.ToInt64(xmlReader.GetAttribute("ItemVariationMatrixID"));
                                clsPOItemDetails.MatrixDescription = xmlReader.GetAttribute("ItemBaseVariationDescription");
                                clsPOItemDetails.ProductGroup = xmlReader.GetAttribute("ProductGroup");
                                clsPOItemDetails.ProductSubGroup = xmlReader.GetAttribute("ProductSubGroup");
                                clsPOItemDetails.Remarks = xmlReader.GetAttribute("ItemRemarks");
                                clsPOItemDetails.SellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingPrice"));
                                clsPOItemDetails.SellingVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingVAT"));
                                clsPOItemDetails.SellingEVAT = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingEVAT"));
                                clsPOItemDetails.SellingLocalTax = Convert.ToDecimal(xmlReader.GetAttribute("ItemSellingLocalTax"));
                                clsPOItemDetails.OldSellingPrice = Convert.ToDecimal(xmlReader.GetAttribute("ItemOldSellingPrice"));

                                clsPOItemDetails.ProductID = clsProduct.Details(clsPOItemDetails.BarCode).ProductID;
                                lngProductID = clsPOItemDetails.ProductID;
                                if (clsPOItemDetails.ProductID == 0)
                                {
                                    clsPOItemDetails.ProductID = clsProduct.Details(clsPOItemDetails.ProductCode).ProductID;
                                    if (clsPOItemDetails.ProductID == 0)
                                    {
                                        //insert new product
                                        clsProductDetails = new ProductDetails();
                                        clsProductDetails.BarCode = clsPOItemDetails.BarCode;
                                        clsProductDetails.ProductCode = clsPOItemDetails.ProductCode;
                                        clsProductDetails.ProductDesc = clsPOItemDetails.Description;
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

                                        clsProductDetails.SupplierCode = clsPODetails.SupplierCode;
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

                                            clsContactDetails.ContactCode = clsPODetails.SupplierCode;
                                            clsContactDetails.ContactName = clsPODetails.SupplierContact;

                                            clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), clsPODetails.SupplierModeOfTerms.ToString());
                                            clsContactDetails.Terms = clsPODetails.SupplierTerms;
                                            clsContactDetails.Address = clsPODetails.SupplierAddress;
                                            clsContactDetails.BusinessName = clsPODetails.SupplierContact;
                                            clsContactDetails.TelephoneNo = clsPODetails.SupplierTelephoneNo;
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

                                        clsPOItemDetails.ProductID = clsProduct.Insert(clsProductDetails);
                                    }
                                    else
                                    {
                                        //product code already exist but not the same barcode
                                        clsProduct.UpdateBarcode(clsPOItemDetails.ProductID, clsPOItemDetails.BarCode);
                                    }
                                    lngProductID = clsPOItemDetails.ProductID;
                                }

                                clsPOItem.Insert(clsPOItemDetails);

                                clsPODetails = new PODetails();
                                clsPODetails.POID = clsPOItemDetails.POID;
                                clsPODetails.DiscountApplied = Convert.ToDecimal(txtPODiscountApplied.Text);
                                clsPODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscountType.SelectedItem.Value);

                                clsPODetails.Discount2Applied = Convert.ToDecimal(txtPODiscount2Applied.Text);
                                clsPODetails.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount2Type.SelectedItem.Value);

                                clsPODetails.Discount3Applied = Convert.ToDecimal(txtPODiscount2Applied.Text);
                                clsPODetails.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscount3Type.SelectedItem.Value);

                                clsPO = new PO(clsPOItem.Connection, clsPOItem.Transaction);
                                clsPO.UpdateDiscount(clsPOItemDetails.POID, clsPODetails.DiscountApplied, clsPODetails.DiscountType, clsPODetails.Discount2Applied, clsPODetails.Discount2Type, clsPODetails.Discount3Applied, clsPODetails.Discount3Type);

                                clsPODetails = clsPO.Details(clsPOItemDetails.POID);
                                UpdateFooter(clsPODetails);

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

                clsPO.CommitAndDispose();
                LoadRecord();
                LoadItems();
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Please select Purchase Order file to upload.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }

		#endregion
        
    }
}
