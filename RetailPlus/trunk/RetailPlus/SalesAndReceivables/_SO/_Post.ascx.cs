namespace AceSoft.RetailPlus.SalesAndReceivables._SO
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
                    cmdEdit.Attributes.Add("onClick", "return confirm_select();");
                    imgEdit.Attributes.Add("onClick", "return confirm_select();");
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
                //LoadOptions();
                LoadItems();
                LoadOptions();
            }
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            if (cboProductCode.SelectedItem.Value.ToString() != "0") //|| cboProductCode.SelectedItem.Value.ToString() != null)
            {
                SaveRecord();
                //LoadOptions();
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

            DataClass clsDataClass = new DataClass();
            long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

            ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
            cboVariation.DataTextField = "VariationDescOnly";
            cboVariation.DataValueField = "MatrixID";
            cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.BaseList(ProductID, "VariationDesc", SortOption.Ascending)).DefaultView;
            cboVariation.DataBind();

            if (cboVariation.Items.Count == 0)
            { cboVariation.Items.Add(new ListItem("No Variation", "0"));}
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);

            cboProductUnit.DataTextField = "BottomUnitCode";
            cboProductUnit.DataValueField = "BottomUnitID";
            cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID, "a.MatrixID", SortOption.Ascending).DefaultView;
            cboProductUnit.DataBind();

            Product clsProduct = new Product(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            ProductDetails clsDetails = clsProduct.Details(ProductID);
            clsProductVariationMatrix.CommitAndDispose();
            cboProductUnit.Items.Insert(0, new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));
            cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));

            txtPrice.Text = clsDetails.WSPrice.ToString("#####0.#0");
            txtSellingPrice.Text = clsDetails.Price.ToString("#####0.#0");
            decimal decMargin = clsDetails.Price - clsDetails.WSPrice;
            try { decMargin = decMargin / clsDetails.WSPrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");
            txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
            txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");

            if (clsDetails.VAT > 0)
                chkIsTaxable.Checked = true;
            else
                chkIsTaxable.Checked = false;

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

                txtPrice.Text = clsProductBaseMatrixDetails.WSPrice.ToString("####0.#0");
                txtSellingPrice.Text = clsProductBaseMatrixDetails.Price.ToString("#####0.#0");
                decimal decMargin = clsProductBaseMatrixDetails.Price - clsProductBaseMatrixDetails.WSPrice;
                try { decMargin = decMargin / clsProductBaseMatrixDetails.WSPrice; }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                txtMargin.Text = decMargin.ToString("#,##0.#0");
                txtVAT.Text = clsProductBaseMatrixDetails.VAT.ToString("#,##0.#0");
                txtEVAT.Text = clsProductBaseMatrixDetails.EVAT.ToString("#,##0.#0");
                txtLocalTax.Text = clsProductBaseMatrixDetails.LocalTax.ToString("#,##0.#0");

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

            Data.Product clsProduct = new Data.Product();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";

            string stSearchKey = txtProductCode.Text;
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(ProductListFilterType.ShowActiveAndInactive, stSearchKey, 0, 0, string.Empty, 0, string.Empty, 100, false, false, "ProductCode", SortOption.Ascending);
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
                string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
                string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
                lnkAddProduct.NavigateUrl = newWindowUrl;

                stParam = "?task=" + Common.Encrypt("add", Session.SessionID) + "&prodid=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
                newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam;
                lnkVariationAdd.NavigateUrl = newWindowUrl;
            }
            imgProductHistory.Visible = bolShowCommandButtons;
            imgProductPriceHistory.Visible = bolShowCommandButtons;
            imgChangePrice.Visible = bolShowCommandButtons;
            imgEditNow.Visible = bolShowCommandButtons;
            lnkAddProduct.Visible = bolShowCommandButtons;
            cmdVariationSearch.Visible = bolShowCommandButtons;
            imgVariationQuickAdd.Visible = bolShowCommandButtons;
            lnkVariationAdd.Visible = bolShowCommandButtons;

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
            cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.Search(ProductID, stSearchKey, "VariationDesc", SortOption.Ascending)).DefaultView;
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["SOItemID"].ToString();

                HyperLink lnkDescription = (HyperLink)e.Item.FindControl("lnkDescription");
                lnkDescription.Text = dr["Description"].ToString();
                lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkMatrixDescription = (HyperLink)e.Item.FindControl("lnkMatrixDescription");
                if (dr["MatrixDescription"].ToString() == string.Empty || dr["MatrixDescription"].ToString() == null)
                    lnkMatrixDescription.Text = "_";
                else
                {
                    lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
                    lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
                }

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

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

                //For anchor
                HtmlGenericControl divExpCollAsst = (HtmlGenericControl)e.Item.FindControl("divExpCollAsst");

                HtmlAnchor anchorDown = (HtmlAnchor)e.Item.FindControl("anchorDown");
                anchorDown.HRef = "javascript:ToggleDiv('" + divExpCollAsst.ClientID + "')";
            }
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintSO();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintSO();
        }
        protected void imgPrintSellingPrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintSOSellingPrice();
        }
        protected void cmdPrintSellingPrice_Click(object sender, EventArgs e)
        {
            PrintSOSellingPrice();
        }
        protected void imgPrintQuotation_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintQuotation();
        }
        protected void cmdPrintQuotation_Click(object sender, EventArgs e)
        {
            PrintQuotation();
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
            GenerateItems();
            LoadRecord();
            LoadItems();
        }
        protected void cmdGenerate_Click(object sender, System.EventArgs e)
        {
            GenerateItems();
            LoadRecord();
            LoadItems();
        }
        protected void txtSODiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdateSODiscount();
        }
        protected void cboSODiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSODiscount();
        }
        protected void txtSOFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtSODeposit_TextChanged(object sender, EventArgs e)
        {
            UpdateDeposit();
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

            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
            txtRemarks.Text = "";
            ComputeItemAmount();
            lblSOItemID.Text = "0";

            txtDeliveryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //string stProductAddLink = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID);
            //lnkProductAdd.NavigateUrl = stProductAddLink;
            //lnkProductAdd.Attributes.Add("onclick", "javascript:w=window.open(this.href','','width=400,height=400');"); 
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["soid"], Session.SessionID));
            SO clsSO = new SO();
            SODetails clsDetails = clsSO.Details(iID);
            clsSO.CommitAndDispose();

            lblSOID.Text = clsDetails.SOID.ToString();
            lnkSONo.Text = clsDetails.SONo;
            lnkSONo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&soid=" + Common.Encrypt(clsDetails.SOID.ToString(), Session.SessionID);

            lblSODate.Text = clsDetails.SODate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            lblCustomerID.Text = clsDetails.CustomerID.ToString();

            lblCustomerCode.Text = clsDetails.CustomerCode.ToString();
            lblCustomerCode.NavigateUrl = "../_Customer/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.CustomerID.ToString(), Session.SessionID);

            lblCustomerContact.Text = clsDetails.CustomerContact;
            lblCustomerTelephoneNo.Text = clsDetails.CustomerTelephoneNo;
            lblTerms.Text = clsDetails.CustomerTerms.ToString("##0");
            switch (clsDetails.CustomerModeOfTerms)
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
            lblCustomerAddress.Text = clsDetails.CustomerAddress;
            lblBranchID.Text = clsDetails.BranchID.ToString();
            lblBranchCode.Text = clsDetails.BranchCode;
            lblBranchAddress.Text = clsDetails.BranchAddress;
            lblSORemarks.Text = clsDetails.Remarks;

            txtSODiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboSODiscountType.SelectedIndex = cboSODiscountType.Items.IndexOf(cboSODiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblSODiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblSOVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtSOFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtSODeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblSOSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
            lblSOVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblSOTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
        }
        private void SaveRecord()
        {
            SOItemDetails clsDetails = new SOItemDetails();

            Product clsProducts = new Product();
            ProductDetails clsProductDetails = clsProducts.Details(Convert.ToInt64(cboProductCode.SelectedItem.Value));

            Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);
            clsProducts.CommitAndDispose();

            clsDetails.SOID = Convert.ToInt64(lblSOID.Text);
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

            // Added Jul 1, 2010 4:20PM : for suggested selling information
            clsDetails.SellingPrice = decimal.Parse(txtSellingPrice.Text);
            clsDetails.SellingVAT = decimal.Parse(txtVAT.Text);
            clsDetails.SellingEVAT = decimal.Parse(txtEVAT.Text);
            clsDetails.SellingLocalTax = decimal.Parse(txtLocalTax.Text);

            SOItem clsSOItem = new SOItem();
            if (lblSOItemID.Text != "0")
            {
                clsDetails.SOItemID = Convert.ToInt64(lblSOItemID.Text);
                clsSOItem.Update(clsDetails);
            }
            else
                clsSOItem.Insert(clsDetails);

            SODetails clsSODetails = new SODetails();
            clsSODetails.SOID = clsDetails.SOID;
            clsSODetails.DiscountApplied = Convert.ToDecimal(txtSODiscountApplied.Text);
            clsSODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboSODiscountType.SelectedItem.Value);

            SO clsSO = new SO(clsSOItem.Connection, clsSOItem.Transaction);
            clsSO.UpdateDiscount(clsDetails.SOID, clsSODetails.DiscountApplied, clsSODetails.DiscountType);

            clsSODetails = clsSO.Details(clsDetails.SOID);
            clsSOItem.CommitAndDispose();

            UpdateFooter(clsSODetails);
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
                SOItem clsSOItem = new SOItem();
                clsSOItem.Delete(stIDs.Substring(0, stIDs.Length - 1));

                SO clsSO = new SO(clsSOItem.Connection, clsSOItem.Transaction);
                clsSO.SynchronizeAmount(Convert.ToInt64(lblSOID.Text));

                SODetails clsSODetails = clsSO.Details(Convert.ToInt64(lblSOID.Text));

                clsSOItem.CommitAndDispose();

                UpdateFooter(clsSODetails);
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
                    SOItem clsSOItem = new SOItem();
                    SOItemDetails clsSOItemDetails = clsSOItem.Details(Convert.ToInt64(stID));
                    clsSOItem.CommitAndDispose();

                    cboProductCode.Items.Clear();
                    cboVariation.Items.Clear();
                    cboProductUnit.Items.Clear();

                    cboProductCode.Items.Add(new ListItem(clsSOItemDetails.ProductCode, clsSOItemDetails.ProductID.ToString()));
                    cboProductCode.SelectedIndex = 0;
                    if (clsSOItemDetails.VariationMatrixID == 0)
                    { cboVariation.Items.Add(new ListItem("No Variation", "0")); }
                    else
                    { cboVariation.Items.Add(new ListItem(clsSOItemDetails.MatrixDescription, clsSOItemDetails.VariationMatrixID.ToString())); }
                    cboVariation.SelectedIndex = 0;
                    cboProductUnit.Items.Add(new ListItem(clsSOItemDetails.ProductUnitCode, clsSOItemDetails.ProductUnitID.ToString()));
                    cboProductUnit.SelectedIndex = 0;
                    txtQuantity.Text = clsSOItemDetails.Quantity.ToString("###0.#0");
                    txtPrice.Text = clsSOItemDetails.UnitCost.ToString("###0.#0");
                    txtDiscount.Text = clsSOItemDetails.Discount.ToString("###0.#0");
                    if (clsSOItemDetails.DiscountType == DiscountTypes.Percentage)
                        chkInPercent.Checked = true;
                    else
                    {
                        chkInPercent.Checked = false;
                    }
                    txtAmount.Text = clsSOItemDetails.Amount.ToString("###0.#0");
                    txtRemarks.Text = clsSOItemDetails.Remarks;
                    lblSOItemID.Text = stID;
                    chkIsTaxable.Checked = clsSOItemDetails.IsVatable;

                    //Added Jul 1, 2010 4:20PM : For selling information
                    txtSellingQuantity.Text = "1";
                    txtMargin.Text = decimal.Parse(Convert.ToString(((clsSOItemDetails.SellingPrice - clsSOItemDetails.UnitCost) / clsSOItemDetails.UnitCost) * 100)).ToString("###0.#0");
                    txtSellingPrice.Text = clsSOItemDetails.SellingPrice.ToString("###0.#0");
                    txtVAT.Text = clsSOItemDetails.SellingVAT.ToString("###0.#0");
                    txtEVAT.Text = clsSOItemDetails.SellingEVAT.ToString("###0.#0");
                    txtLocalTax.Text = clsSOItemDetails.SellingLocalTax.ToString("###0.#0");
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
        private void LoadItems()
        {
            DataClass clsDataClass = new DataClass();

            SOItem clsSOItem = new SOItem();
            lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsSOItem.List(Convert.ToInt64(lblSOID.Text), "SOItemID", SortOption.Desscending)).DefaultView;
            lstItem.DataBind();
            clsSOItem.CommitAndDispose();
        }
        private void IssueGRN()
        {
            DateTime DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);

            ERPConfig clsERPConfig = new ERPConfig();
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
            clsERPConfig.CommitAndDispose();

            if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
            {
                long SOID = Convert.ToInt64(lblSOID.Text);
                string CustomerDRNo = txtCustomerDRNo.Text;

                SO clsSO = new SO();
                clsSO.IssueGRN(SOID, CustomerDRNo, DeliveryDate);
                clsSO.CommitAndDispose();

                string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&soid=" + Common.Encrypt(SOID.ToString(), Session.SessionID);
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
        private void PrintSO()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&soid=" + Common.Encrypt(lblSOID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("SOReport", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void PrintSOSellingPrice()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&soid=" + Common.Encrypt(lblSOID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("SOReportSellingPrice", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void PrintQuotation()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&soid=" + Common.Encrypt(lblSOID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("SOReportQuotation", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void UpdateHeader()
        {
            string stID = lblSOID.Text;

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&soid=" + Common.Encrypt(stID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void GenerateItems()
        {
            SO clsSO = new SO();
            clsSO.GenerateItemsForReorder(Convert.ToInt64(lblSOID.Text));
            clsSO.CommitAndDispose();
        }
        private void UpdateSODiscount()
        {
            SODetails clsSODetails = new SODetails();
            clsSODetails.SOID = Convert.ToInt64(lblSOID.Text);
            clsSODetails.DiscountApplied = Convert.ToDecimal(txtSODiscountApplied.Text);
            clsSODetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboSODiscountType.SelectedItem.Value);

            SO clsSO = new SO();
            clsSO.UpdateDiscount(clsSODetails.SOID, clsSODetails.DiscountApplied, clsSODetails.DiscountType);
            clsSO.SynchronizeAmount(Convert.ToInt64(lblSOID.Text));
            clsSODetails = clsSO.Details(Convert.ToInt64(lblSOID.Text));
            clsSO.CommitAndDispose();

            UpdateFooter(clsSODetails);
        }
        private void UpdateFreight()
        {
            SODetails clsSODetails = new SODetails();
            clsSODetails.SOID = Convert.ToInt64(lblSOID.Text);
            clsSODetails.Freight = Convert.ToDecimal(txtSOFreight.Text);

            SO clsSO = new SO();
            clsSO.UpdateFreight(clsSODetails.SOID, clsSODetails.Freight);
            clsSO.SynchronizeAmount(Convert.ToInt64(lblSOID.Text));
            clsSODetails = clsSO.Details(Convert.ToInt64(lblSOID.Text));
            clsSO.CommitAndDispose();

            UpdateFooter(clsSODetails);
        }
        private void UpdateDeposit()
        {
            SODetails clsSODetails = new SODetails();
            clsSODetails.SOID = Convert.ToInt64(lblSOID.Text);
            clsSODetails.Deposit = Convert.ToDecimal(txtSODeposit.Text);

            SO clsSO = new SO();
            clsSO.UpdateDeposit(clsSODetails.SOID, clsSODetails.Deposit);
            clsSO.SynchronizeAmount(Convert.ToInt64(lblSOID.Text));
            clsSODetails = clsSO.Details(Convert.ToInt64(lblSOID.Text));
            clsSO.CommitAndDispose();

            UpdateFooter(clsSODetails);
        }
        private void UpdateFooter(SODetails clsSODetails)
        {
            lblSODiscount.Text = clsSODetails.Discount.ToString("#,##0.#0");
            lblSOVatableAmount.Text = clsSODetails.VatableAmount.ToString("#,##0.#0");
            txtSOFreight.Text = clsSODetails.Freight.ToString("#,##0.#0");
            txtSODeposit.Text = clsSODetails.Deposit.ToString("#,##0.#0");
            lblSOSubTotal.Text = Convert.ToDecimal(clsSODetails.SubTotal - clsSODetails.VAT).ToString("#,##0.#0");
            lblSOVAT.Text = clsSODetails.VAT.ToString("#,##0.#0");
            lblSOTotal.Text = clsSODetails.SubTotal.ToString("#,##0.#0");
        }

        #endregion

}
}
