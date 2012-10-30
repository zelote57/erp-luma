namespace AceSoft.RetailPlus.Inventory._Stock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Additem : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
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

		protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cboProductCode.SelectedItem.Value.ToString() != "0" || cboProductCode.SelectedItem.Value.ToString() != null)
			{
				SaveRecord();
				ClearAddItem();
				LoadItems();
                //string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&stockid=" + Common.Encrypt(lblStockID.Text, Session.SessionID);
                //Response.Redirect("Default.aspx" + stParam);
			}
		}
		
        protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			if (cboProductCode.SelectedItem.Value.ToString() != "0" || cboProductCode.SelectedItem.Value.ToString() != null)
			{
				SaveRecord();
				ClearAddItem();
				LoadItems();
                //string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&stockid=" + Common.Encrypt(lblStockID.Text, Session.SessionID);
                //Response.Redirect("Default.aspx" + stParam);
			}
		}
		
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

            Product clsProduct = new Product(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            ProductDetails clsDetails = clsProduct.Details(ProductID);
            ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            System.Data.DataTable dtProductPurchasePriceHistory = clsProductPurchasePriceHistory.ListAsDataTable(ProductID, "PurchasePrice", SortOption.Ascending);

            ProductPackage clsProductPackage = new ProductPackage(clsProductVariationMatrix.Connection, clsProductVariationMatrix.Transaction);
            ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByBarCode(txtProductCode.Text);

            clsProductVariationMatrix.CommitAndDispose();
            
            cboProductUnit.Items.Insert(0, new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));
            if (clsProductPackageDetails.PackageID == 0)
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));
                txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#####0.##0");
            }
            else if (clsProductPackageDetails.PackageID != 0)
            {
                cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(new ListItem(clsProductPackageDetails.UnitCode, clsProductPackageDetails.UnitID.ToString()));

                txtPurchasePrice.Text = clsProductPackageDetails.PurchasePrice.ToString("#####0.##0");
            }

            if (cboProductUnit.Items.Count == 0)
            { cboProductUnit.Items.Add(new ListItem("No Unit", "0")); }
            cboVariation.SelectedIndex = cboVariation.Items.Count - 1;

            ComputeAmount();
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

                txtPurchasePrice.Text = clsProductBaseMatrixDetails.PurchasePrice.ToString("####0.##0");
                
                ComputeAmount();
            }
        }

        protected void cboProductUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductPackage clsProductPackage = new ProductPackage();
            ProductPackageDetails clsDetails = clsProductPackage.DetailsByProductIDAndUnitID(long.Parse(cboProductCode.SelectedValue), long.Parse(cboProductUnit.SelectedValue));
            if (clsDetails.PackageID == 0)
            {
                ProductUnit clsProductUnit = new ProductUnit(clsProductPackage.Connection, clsProductPackage.Transaction);
                Product clsProduct = new Product(clsProductPackage.Connection, clsProductPackage.Transaction);
                ProductDetails clsProductDetails = clsProduct.Details(long.Parse(cboProductCode.SelectedItem.Value));
                decimal decBaseUnitValue = clsProductUnit.GetBaseUnitValue(long.Parse(cboProductCode.SelectedItem.Value), int.Parse(cboProductUnit.SelectedItem.Value), 1);

                clsDetails.Price = decBaseUnitValue * clsProductDetails.Price;
                clsDetails.PurchasePrice = decBaseUnitValue * clsProductDetails.PurchasePrice;
            }
            clsProductPackage.CommitAndDispose();

            txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#####0.##0");
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

            if (cboProductCode.Items.Count == 0)
            {
                Data.ProductPackage clsProductPackage = new Data.ProductPackage();
                Data.ProductPackageDetails clsProductPackageDetails = clsProductPackage.DetailsByBarCode(txtProductCode.Text);
                if (clsProductPackageDetails.PackageID != 0)
                {
                    clsProduct = new Product(clsProductPackage.Connection, clsProductPackage.Transaction);
                    Data.ProductDetails clsProductDetails = clsProduct.Details(clsProductPackageDetails.ProductID);

                    cboProductCode.Items.Add(new ListItem(clsProductDetails.ProductCode, clsProductDetails.ProductID.ToString()));
                }
                else
                {
                    cboProductCode.Items.Add(new ListItem("No product", "0"));
                }
            }

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
			cboVariation.DataTextField = "VariationDesc";
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

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Print();
        }

        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            Print();
        }

        protected void imgAddProduct_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID) + "&windowaction=" + Common.Encrypt("close", Session.SessionID);
            string javaScript =
             "<script type='text/javascript'>\n" +
             "<!--\n" +
             "window.open('" + newWindowUrl + "');\n" +
             "// -->\n" +
             "</script>\n";
            this.Page.ClientScript.RegisterStartupScript(GetType(), "openwindow", javaScript);
        }

        protected void imgVariationAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (cboProductCode.SelectedValue != "0")
            {
                string stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
                            "&prodid=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
                string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam;
                string javaScript =
                 "<script type='text/javascript'>\n" +
                 "<!--\n" +
                 "window.open('" + newWindowUrl + "');\n" +
                 "// -->\n" +
                 "</script>\n";
                this.Page.ClientScript.RegisterStartupScript(GetType(), "openwindow", javaScript);
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Please select product first.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }

        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["StockItemID"].ToString();

                HyperLink lnkProduct = (HyperLink)e.Item.FindControl("lnkProduct");
                lnkProduct.Text = dr["ProductCode"].ToString();
                lnkProduct.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkVariation = (HyperLink)e.Item.FindControl("lnkVariation");
                if (dr["BaseVariationDescription"].ToString() != string.Empty && dr["BaseVariationDescription"].ToString() != null)
                {
                    lnkVariation.Text = dr["BaseVariationDescription"].ToString();
                    lnkVariation.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
                }

                Label lblProductUnit = (Label)e.Item.FindControl("lblProductUnit");
                lblProductUnit.Text = dr["UnitName"].ToString();

                Label lblStockType = (Label)e.Item.FindControl("lblStockType");
                lblStockType.Text = dr["StockTypeDescription"].ToString();

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

                Label lblPurchasePrice = (Label)e.Item.FindControl("lblPurchasePrice");
                lblPurchasePrice.Text = Convert.ToDecimal(dr["PurchasePrice"].ToString()).ToString("#,##0.##0");

                decimal decAmount = Convert.ToDecimal(dr["PurchasePrice"].ToString()) * Convert.ToDecimal(dr["Quantity"].ToString());
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = decAmount.ToString("#,##0.##0");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                //For anchor
                //				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

                //				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
            }
        }
        
        protected void cmdCloseTransaction_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CloseTransaction();
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        
        protected void lnkCloseTransaction_Click(object sender, EventArgs e)
        {
            CloseTransaction();
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal decPurchaseAmount = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtPurchasePrice.Text);
                txtPurchaseAmount.Text = Convert.ToDecimal(decPurchaseAmount).ToString("#,##0.##0");
            }
            catch { }
        }

        protected void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal decPurchaseAmount = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtPurchasePrice.Text);
                txtPurchaseAmount.Text = Convert.ToDecimal(decPurchaseAmount).ToString("#,##0.##0");
            }
            catch { }
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			cboProductCode.Items.Clear();
			cboProductCode.Items.Add(new ListItem("No Product", "0"));
            cboVariation.Items.Add(new ListItem("No Variation", "0"));
            cboProductUnit.Items.Add(new ListItem("N/A", "0"));

			cboProductCode_SelectedIndexChanged(null, null);
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["stockid"],Session.SessionID));
			Stock clsStock = new Stock();
			StockDetails clsDetails = clsStock.Details(iID);

            Branch clsBranch = new Branch(clsStock.Connection, clsStock.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranch.DataBind();

			clsStock.CommitAndDispose();

			lblStockID.Text = clsDetails.StockID.ToString();
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(clsDetails.BranchID.ToString()));

			lblTransactionNo.Text = clsDetails.TransactionNo;
			lblStockDate.Text = clsDetails.StockDate.ToString("MMM. dd, yyy HH:mm:ss");
			txtSupplier.Text = clsDetails.SupplierName;
			lblSupplierID.Text = clsDetails.SupplierID.ToString();
            txtStockTypeCode.Text = clsDetails.StockTypeCode;
            txtStockTypeCode.ToolTip = clsDetails.StockTypeID.ToString();
            txtStockDescription.Text = clsDetails.StockTypeDescription;
            txtStockDirection.Text = clsDetails.StockDirection.ToString("G");
            txtStockRemarks.Text = clsDetails.Remarks;

			LoadItems();
		}
		private void SaveRecord()
		{
			StockItemDetails clsDetails = new StockItemDetails();

			clsDetails.StockID = Convert.ToInt64(lblStockID.Text);
			clsDetails.ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);
			clsDetails.VariationMatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
			clsDetails.ProductUnitID = Convert.ToInt32(cboProductUnit.SelectedItem.Value);
            clsDetails.StockTypeID = Convert.ToInt16(txtStockTypeCode.ToolTip);
			clsDetails.StockDate = DateTime.Now;
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.Remarks = txtRemarks.Text;
            clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);

			StockDirections StockDirection = (StockDirections) Enum.Parse(typeof(StockDirections), txtStockDirection.Text);

            Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];

			Stock clsStock = new Stock();
            clsStock.AddItem(int.Parse(cboBranch.SelectedItem.Value), lblTransactionNo.Text, clsAccessUserDetails.Name, clsDetails, StockDirection);
			clsStock.CommitAndDispose();
		}
		private void ClearAddItem()
		{
            txtProductCode.Text = "";
            txtVariation.Text = "";
            cmdProductCode_Click(null, null);
			txtQuantity.Text = "1";
			txtRemarks.Text = "";
		}
		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			StockItem clsStockItem = new StockItem();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsStockItem.List(Convert.ToInt64(lblStockID.Text), "StockItemID",SortOption.Desscending)).DefaultView;
			lstItem.DataBind();
			clsStockItem.CommitAndDispose();

            ComputeAmount();
		}
        private void Print()
        {
            Session.Remove("tranno");
            Session.Add("tranno", lblTransactionNo.Text);

            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("stocktransaction", Session.SessionID) + "&tranno=" + Common.Encrypt(lblTransactionNo.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);


        }
        private void CloseTransaction()
        {
            Stock clsStock = new Stock();
            clsStock.TagInactive(long.Parse(lblStockID.Text));
            clsStock.CommitAndDispose();
        }

        private void ComputeAmount()
        {
            decimal decStockTotal = 0;
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblAmount = (Label)item.FindControl("lblAmount");
                decStockTotal += Convert.ToDecimal(lblAmount.Text);
            }

            lblStockTotal.Text = decStockTotal.ToString("#,###0.##0");
        }

		#endregion
        
    }
}
