namespace AceSoft.RetailPlus.PurchasesAndPayables._DebitMemo
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
            if (!IsPostBack )
			{
                try { lblReferrer.Text = Request.UrlReferrer.ToString(); }
                catch { }
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
            }
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
            if (cboProductCode.SelectedItem.Value.ToString() != "0") //|| cboProductCode.SelectedItem.Value.ToString() != null)
            {
				SaveRecord();
                //LoadOptions();
				LoadItems();
			}
		}
		protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Common Common = new Common();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
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
			if (cboProductCode.Items.Count ==0)
				return;

			DataClass clsDataClass = new DataClass();
			long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

			ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
			cboVariation.DataTextField = "Description";
			cboVariation.DataValueField = "MatrixID";
			cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.BaseList(ProductID, "VariationDesc",SortOption.Ascending)).DefaultView;
			cboVariation.DataBind();

			if (cboVariation.Items.Count == 0)
			{
				cboVariation.Items.Add(new ListItem("No Variation", "0"));
			}
			cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
			cboProductUnit.DataTextField = "BottomUnitCode";
			cboProductUnit.DataValueField = "BottomUnitID";
			cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID,"a.MatrixID",SortOption.Ascending).DefaultView;
			cboProductUnit.DataBind();

            Product clsProduct = new Product(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
			ProductDetails clsDetails = clsProduct.Details(ProductID);
            clsProductVariationMatrix.CommitAndDispose();

            cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));

            txtPrevPrice.Text = clsDetails.PurchasePrice.ToString("#####0.#0");
			txtPrice.Text = clsDetails.PurchasePrice.ToString("#####0.#0");
			if (clsDetails.VAT >0)
				chkIsTaxable.Checked = true;
			else
				chkIsTaxable.Checked = false;
			
			ComputeItemAmount();
			cboVariation_SelectedIndexChanged(null, null);
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

                txtPrevPrice.Text = clsProductBaseMatrixDetails.PurchasePrice.ToString("####0.#0");
				txtPrice.Text = clsProductBaseMatrixDetails.PurchasePrice.ToString("####0.#0");
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

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["DebitMemoItemID"].ToString();

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

                Label lblPrevUnitCost = (Label)e.Item.FindControl("lblPrevUnitCost");
                lblPrevUnitCost.Text = Convert.ToDecimal(dr["PrevUnitCost"].ToString()).ToString("#,##0.#0");

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
            PrintDebitMemo();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintDebitMemo();
        }
        protected void txtPODiscountApplied_TextChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void cboPODiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePODiscount();
        }
        protected void txtPOFreight_TextChanged(object sender, EventArgs e)
        {
            UpdateFreight();
        }
        protected void txtPODeposit_TextChanged(object sender, EventArgs e)
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

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			cboProductCode.Items.Clear();
			cboVariation.Items.Clear();
			cboProductUnit.Items.Clear();

			cboProductCode.Items.Add(new ListItem("No Product", "0"));

			cboProductCode_SelectedIndexChanged(null, null);

			txtQuantity.Text = "1";
			txtDiscount.Text = "0";
			txtRemarks.Text = "";
			ComputeItemAmount();
			lblDebitMemoItemID.Text = "0";

			txtPostDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
		}
		private void LoadRecord()
		{
			Common Common = new Common();
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["memoid"],Session.SessionID));
			DebitMemos clsDebitMemos = new DebitMemos();
			DebitMemoDetails clsDetails = clsDebitMemos.Details(iID);
			clsDebitMemos.CommitAndDispose();

			lblDebitMemoID.Text = clsDetails.DebitMemoID.ToString();
			lnkMemoNo.Text = clsDetails.MemoNo;
			lblMemoDate.Text = clsDetails.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
			lblRequiredPostingDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
			lblSupplierID.Text = clsDetails.SupplierID.ToString();

			lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);	
			lblSupplierCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx" + stParam;

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
			lblRemarks.Text = clsDetails.Remarks;

            UpdateFooter(clsDetails);
		}
		private void SaveRecord()
		{
			DebitMemoItemDetails clsDetails = new DebitMemoItemDetails();

			Product clsProducts = new Product();
			ProductDetails clsProductDetails = clsProducts.Details(Convert.ToInt64(cboProductCode.SelectedItem.Value));
			
			Terminal clsTerminal = new Terminal(clsProducts.Connection, clsProducts.Transaction);
			TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);
			clsProducts.CommitAndDispose();

            clsDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsDetails.ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);
            clsDetails.ProductCode = clsProductDetails.ProductCode;
            clsDetails.BarCode = clsProductDetails.BarCode;
            clsDetails.Description = clsProductDetails.ProductDesc;
            clsDetails.ProductUnitID = Convert.ToInt32(cboProductUnit.SelectedItem.Value);
            clsDetails.ProductUnitCode = cboProductUnit.SelectedItem.Text;
            clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
            clsDetails.PrevUnitCost = Convert.ToDecimal(txtPrevPrice.Text);
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

			DebitMemoItems clsDebitMemoItems = new DebitMemoItems();
			if (lblDebitMemoItemID.Text != "0")
			{
				clsDetails.DebitMemoItemID = Convert.ToInt64(lblDebitMemoItemID.Text);
				clsDebitMemoItems.Update(clsDetails);
			}
			else
				clsDebitMemoItems.Insert(clsDetails);
			
			DebitMemos clsDebitMemos = new DebitMemos(clsDebitMemoItems.Connection, clsDebitMemoItems.Transaction);
			DebitMemoDetails clsDebitMemoDetails = clsDebitMemos.Details(clsDetails.DebitMemoID);

			clsDebitMemoItems.CommitAndDispose();

            UpdateFooter(clsDebitMemoDetails);
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
				DebitMemoItems clsDebitMemoItems = new DebitMemoItems();
				clsDebitMemoItems.Delete( stIDs.Substring(0,stIDs.Length-1));

				DebitMemos clsDebitMemos = new DebitMemos(clsDebitMemoItems.Connection, clsDebitMemoItems.Transaction);
				clsDebitMemos.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));

				DebitMemoDetails clsDebitMemoDetails = clsDebitMemos.Details(Convert.ToInt64(lblDebitMemoID.Text));

				clsDebitMemoItems.CommitAndDispose();

                UpdateFooter(clsDebitMemoDetails);
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
					DebitMemoItems clsDebitMemoItems = new DebitMemoItems();
                    DebitMemoItemDetails clsDebitMemoItemDetails = clsDebitMemoItems.Details(Convert.ToInt64(stID));
					clsDebitMemoItems.CommitAndDispose();

					cboProductCode.Items.Clear();
					cboVariation.Items.Clear();
					cboProductUnit.Items.Clear();

                    cboProductCode.Items.Add(new ListItem(clsDebitMemoItemDetails.ProductCode, clsDebitMemoItemDetails.ProductID.ToString()));
                    cboProductCode.SelectedIndex = 0;
                    if (clsDebitMemoItemDetails.VariationMatrixID == 0)
                    { cboVariation.Items.Add(new ListItem("No Variation", "0")); }
                    else
                    { cboVariation.Items.Add(new ListItem(clsDebitMemoItemDetails.MatrixDescription, clsDebitMemoItemDetails.VariationMatrixID.ToString())); }
                    cboVariation.SelectedIndex = 0;
                    cboProductUnit.Items.Add(new ListItem(clsDebitMemoItemDetails.ProductUnitCode, clsDebitMemoItemDetails.ProductUnitID.ToString()));
                    cboProductUnit.SelectedIndex = 0;
                    txtQuantity.Text = clsDebitMemoItemDetails.Quantity.ToString("###0.#0");
                    txtPrevPrice.Text = clsDebitMemoItemDetails.PrevUnitCost.ToString("###0.#0");
                    txtPrice.Text = clsDebitMemoItemDetails.UnitCost.ToString("###0.#0");
                    txtDiscount.Text = clsDebitMemoItemDetails.Discount.ToString("###0.#0");
                    if (clsDebitMemoItemDetails.DiscountType == DiscountTypes.Percentage)
                        chkInPercent.Checked = true;
                    else
                    {
                        chkInPercent.Checked = false;
                    }
                    txtAmount.Text = clsDebitMemoItemDetails.Amount.ToString("###0.#0");
                    txtRemarks.Text = clsDebitMemoItemDetails.Remarks;
                    lblDebitMemoItemID.Text = stID;
                    chkIsTaxable.Checked = clsDebitMemoItemDetails.IsVatable;
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

			DebitMemoItems clsDebitMemoItems = new DebitMemoItems();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsDebitMemoItems.List(Convert.ToInt64(lblDebitMemoID.Text), "DebitMemoItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsDebitMemoItems.CommitAndDispose();
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
				string SupplierDRNo = txtSupplierDocNo.Text;

				DebitMemos clsDebitMemos = new DebitMemos();
				clsDebitMemos.Post(DebitMemoID, SupplierDRNo, DeliveryDate);
				clsDebitMemos.CommitAndDispose();

				Common Common = new Common();
				string stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + "&memoid=" + Common.Encrypt(DebitMemoID.ToString(),Session.SessionID);	
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
            decimal price = Convert.ToDecimal(txtPrevPrice.Text) - Convert.ToDecimal(txtPrice.Text);
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
            decimal price = Convert.ToDecimal(txtPrevPrice.Text) - Convert.ToDecimal(txtPrice.Text);
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
        private void PrintDebitMemo()
        {
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("debitmemo", Session.SessionID) + "&memoid=" + Common.Encrypt(lblDebitMemoID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void UpdateHeader()
        {
            string MemoID = lblDebitMemoID.Text;

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&memoid=" + Common.Encrypt(MemoID, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void UpdatePODiscount()
        {
            DebitMemoDetails clsDebitMemoDetails = new DebitMemoDetails();
            clsDebitMemoDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsDebitMemoDetails.DiscountApplied = Convert.ToDecimal(txtPODiscountApplied.Text);
            clsDebitMemoDetails.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), cboPODiscountType.SelectedItem.Value);

            DebitMemos clsDebitMemos = new DebitMemos();
            clsDebitMemos.UpdateDiscount(clsDebitMemoDetails.DebitMemoID, clsDebitMemoDetails.DiscountApplied, clsDebitMemoDetails.DiscountType);
            clsDebitMemos.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));
            clsDebitMemoDetails = clsDebitMemos.Details(Convert.ToInt64(lblDebitMemoID.Text));
            clsDebitMemos.CommitAndDispose();

            UpdateFooter(clsDebitMemoDetails);
        }
        private void UpdateFreight()
        {
            DebitMemoDetails clsDebitMemoDetails = new DebitMemoDetails();
            clsDebitMemoDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsDebitMemoDetails.Freight = Convert.ToDecimal(txtPOFreight.Text);

            DebitMemos clsDebitMemos = new DebitMemos();
            clsDebitMemos.UpdateFreight(clsDebitMemoDetails.DebitMemoID, clsDebitMemoDetails.Freight);
            clsDebitMemos.SynchronizeAmount(Convert.ToInt64(lblDebitMemoID.Text));
            clsDebitMemoDetails = clsDebitMemos.Details(Convert.ToInt64(lblDebitMemoID.Text));
            clsDebitMemos.CommitAndDispose();

            UpdateFooter(clsDebitMemoDetails);
        }
        private void UpdateDeposit()
        {
            DebitMemoDetails clsDebitMemoDetails = new DebitMemoDetails();
            clsDebitMemoDetails.DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            clsDebitMemoDetails.Deposit = Convert.ToDecimal(txtPODeposit.Text);

            DebitMemos clsDebitMemos = new DebitMemos();
            clsDebitMemos.UpdateDeposit(clsDebitMemoDetails.DebitMemoID, clsDebitMemoDetails.Deposit);
            clsDebitMemos.SynchronizeAmount(clsDebitMemoDetails.DebitMemoID);
            clsDebitMemoDetails = clsDebitMemos.Details(clsDebitMemoDetails.DebitMemoID);
            clsDebitMemos.CommitAndDispose();

            UpdateFooter(clsDebitMemoDetails);
        }
        private void UpdateFooter(DebitMemoDetails clsDebitMemoDetails)
        {
            lblPODiscount.Text = clsDebitMemoDetails.Discount.ToString("#,##0.#0");
            lblPOVatableAmount.Text = clsDebitMemoDetails.VatableAmount.ToString("#,##0.#0");
            txtPOFreight.Text = clsDebitMemoDetails.Freight.ToString("#,##0.#0");
            txtPODeposit.Text = clsDebitMemoDetails.Deposit.ToString("#,##0.#0");
            lblPOSubTotal.Text = Convert.ToDecimal(clsDebitMemoDetails.SubTotal - clsDebitMemoDetails.VAT).ToString("#,##0.#0");
            lblPOVAT.Text = clsDebitMemoDetails.VAT.ToString("#,##0.#0");
            lblPOTotal.Text = clsDebitMemoDetails.SubTotal.ToString("#,##0.#0");
        }

		#endregion

    }
}
