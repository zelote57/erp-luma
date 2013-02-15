namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using AceSoft.RetailPlus.Data;
    using AceSoft.RetailPlus.Security;

	public partial  class __ListDetailed : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
                ManageSecurity();
				LoadList();
				cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
				imgDelete.Attributes.Add("onClick", "return confirm_delete();");
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

        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
        protected void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Delete())
				LoadList();
		}
		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadList();
		}
        protected void idEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}
		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
        protected void idCompose_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Compose();
		}
		protected void cmdCompose_Click(object sender, System.EventArgs e)
		{
			Compose();
		}
        protected void idFinance_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SetupFinance();
        }
        protected void cmdFinance_Click(object sender, EventArgs e)
        {
            SetupFinance();
        }
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            LoadList();
		}
		protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                string stParam = string.Empty;
				Common Common = new Common();
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr[ProductColumnNames.ProductID].ToString();

                ImageButton imgProductTag = (ImageButton)e.Item.FindControl("imgProductTag");
                if (Boolean.Parse(dr[ProductColumnNames.Active].ToString()))
                {
                    imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtagact.gif";
                    imgProductTag.ToolTip = "Tag this product as INACTIVE."; 
                }
                else //if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly)
                {
                    imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtaginact.gif";
                    imgProductTag.ToolTip = "Tag this product as ACTIVE."; 
                }

                //HyperLink lnkVariations = (HyperLink)e.Item.FindControl("lnkVariations");
                stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);

                HyperLink lnkPackage = (HyperLink)e.Item.FindControl("lnkPackage");
                lnkPackage.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Package/Default.aspx" + stParam;

                HyperLink lnkBarCode = (HyperLink)e.Item.FindControl("lnkBarCode");
                lnkBarCode.Text = dr[ProductColumnNames.BarCode].ToString();
                lnkBarCode.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                HyperLink lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr[ProductColumnNames.ProductCode].ToString();
                lnkProductCode.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                HyperLink lnkDescription = (HyperLink)e.Item.FindControl("lnkDescription");
				lnkDescription.Text = dr[ProductColumnNames.ProductDesc].ToString();
                lnkDescription.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkGroup = (HyperLink)e.Item.FindControl("lnkGroup");
				lnkGroup.Text = dr[ProductColumnNames.ProductGroupCode].ToString();
                lnkGroup.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductGroup/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr[ProductColumnNames.ProductGroupID].ToString(), Session.SessionID); ;

                HyperLink lnkUnit = (HyperLink)e.Item.FindControl("lnkUnit");
				lnkUnit.Text = dr[ProductColumnNames.BaseUnitCode].ToString();
                lnkUnit.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr[ProductColumnNames.BaseUnitID].ToString(), Session.SessionID); ;

				Label lblPrice = (Label) e.Item.FindControl("lblPrice");
                lblPrice.Text = Convert.ToDecimal(dr[ProductColumnNames.Price].ToString()).ToString("#,##0.#0");

				Label lblPurchasePrice = (Label) e.Item.FindControl("lblPurchasePrice");
				lblPurchasePrice.Text = Convert.ToDecimal(dr[ProductColumnNames.PurchasePrice].ToString()).ToString("#,##0.#0");

                Label lblMargin = (Label)e.Item.FindControl("lblMargin");
                decimal decMargin = Convert.ToDecimal(dr[ProductColumnNames.Price].ToString()) - Convert.ToDecimal(dr[ProductColumnNames.PurchasePrice].ToString());
                lblMargin.Text = decMargin.ToString("#,##0.#0");

                try { decMargin = decMargin / Convert.ToDecimal(dr[ProductColumnNames.PurchasePrice].ToString()); }
                catch { decMargin = 1; }
                decMargin = decMargin * 100;
                lblMargin.Text += " (" + decMargin.ToString("#,##0.#0") + "%)";

                Label lnkSubGroup = (Label)e.Item.FindControl("lnkSubGroup");
                lnkSubGroup.Text = dr[ProductColumnNames.ProductSubGroupCode].ToString();

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                lblQuantity.Text += dr[ProductColumnNames.ConvertedMainQuantity].ToString();
                //if (dr[ProductColumnNames.ConvertedMainQuantity].ToString().Split(';').Length > 0)
                //{ lblQuantity.Text += " (" + dr[ProductColumnNames.ConvertedMainQuantity].ToString() + ") "; }
                //else
                //{ lblQuantity.Text = Convert.ToDecimal(dr[ProductColumnNames.MainQuantity].ToString()).ToString("#,##0.#0"); }

                HyperLink lnkSupplierName = (HyperLink)e.Item.FindControl("lnkSupplierName");
                lnkSupplierName.Text = dr[ProductColumnNames.SupplierName].ToString();
                lnkSupplierName.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx" + "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr[ProductColumnNames.SupplierID].ToString(), Session.SessionID);

                CheckBox chkIncludeInSubtotalDiscount = (CheckBox)e.Item.FindControl("chkIncludeInSubtotalDiscount");
                chkIncludeInSubtotalDiscount.Checked = (bool) dr[ProductColumnNames.IncludeInSubtotalDiscount];

				Label lblVAT = (Label) e.Item.FindControl("lblVAT");
				lblVAT.Text = Convert.ToDecimal(dr[ProductColumnNames.VAT].ToString()).ToString("#,##0.#0") + " %";

				Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
				lblEVAT.Text = Convert.ToDecimal(dr[ProductColumnNames.EVAT].ToString()).ToString("#,##0.#0") + " %"; 

				Label lblLocalTax = (Label) e.Item.FindControl("lblLocalTax");
				lblLocalTax.Text = Convert.ToDecimal(dr[ProductColumnNames.LocalTax].ToString()).ToString("#,##0.#0") + " %";

                //For anchor
                HtmlGenericControl divInsertVariation = (HtmlGenericControl)e.Item.FindControl("divInsertVariation");
                HtmlAnchor imgVariationsAdd = (HtmlAnchor)e.Item.FindControl("imgVariationsAdd");
                imgVariationsAdd.HRef = "javascript:ToggleDiv('" + divInsertVariation.ClientID + "')";

                DropDownList cboVariationType = (DropDownList)e.Item.FindControl("cboVariationType");
			    ProductVariation clsVariation = new ProductVariation();
			    cboVariationType.DataTextField = "VariationType";
			    cboVariationType.DataValueField = "VariationID";
			    cboVariationType.DataSource = clsVariation.AvailableVariationsDataTable(long.Parse(chkList.Value), "VariationType",SortOption.Ascending).DefaultView;
			    cboVariationType.DataBind();
			    cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;
			    clsVariation.CommitAndDispose();	

                imgProductTag.Enabled = cmdEdit.Visible;

                LinkButton cmdSaveVariation = (LinkButton)e.Item.FindControl("cmdSaveVariation");
                cmdSaveVariation.Enabled = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));

                ImageButton imgSaveVariation = (ImageButton)e.Item.FindControl("imgSaveVariation");
                imgSaveVariation.Enabled = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));
                if (!imgSaveVariation.Enabled) imgSaveVariation.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgVariations = (ImageButton)e.Item.FindControl("imgVariations");
                imgVariations.Enabled = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));
                if (!imgVariations.Enabled) imgVariations.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgVariationsMatrix = (ImageButton)e.Item.FindControl("imgVariationsMatrix");
                imgVariationsMatrix.Enabled = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));
                if (!imgVariationsMatrix.Enabled) imgVariationsMatrix.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgUnitsMatrix = (ImageButton)e.Item.FindControl("imgUnitsMatrix");
                imgUnitsMatrix.Enabled = Convert.ToBoolean(Convert.ToInt16(lblUnitMatrixAccess.Text));
                if (!imgUnitsMatrix.Enabled) imgUnitsMatrix.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                imgVariationsAdd.Visible = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));

                ImageButton imgVariationsMatrixAdd = (ImageButton)e.Item.FindControl("imgVariationsMatrixAdd");
                imgVariationsMatrixAdd.Enabled = Convert.ToBoolean(Convert.ToInt16(lblVariationsAccess.Text));
                if (!imgVariationsMatrixAdd.Enabled) imgVariationsMatrixAdd.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgUnitsMatrixAdd = (ImageButton)e.Item.FindControl("imgUnitsMatrixAdd");
                imgUnitsMatrixAdd.Enabled = Convert.ToBoolean(Convert.ToInt16(lblUnitMatrixAccess.Text));
                if (!imgUnitsMatrixAdd.Enabled) imgUnitsMatrixAdd.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgPackageAdd = (ImageButton)e.Item.FindControl("imgPackageAdd");
                imgPackageAdd.Enabled = Convert.ToBoolean(Convert.ToInt16(lblProductPackageAccess.Text));
                if (!imgPackageAdd.Enabled) imgPackageAdd.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgInventoryAdjustment = (ImageButton)e.Item.FindControl("imgInventoryAdjustment");
                imgInventoryAdjustment.Enabled = Convert.ToBoolean(Convert.ToInt16(lblInvAdjustmentAccess.Text));
                if (!imgInventoryAdjustment.Enabled) imgInventoryAdjustment.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgProductHistory = (ImageButton)e.Item.FindControl("imgProductHistory");
                imgProductHistory.Enabled = Convert.ToBoolean(Convert.ToInt16(lblProductsListReportAccess.Text));
                if (!imgProductHistory.Enabled) imgProductHistory.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgProductPriceHistory = (ImageButton)e.Item.FindControl("imgProductPriceHistory");
                imgProductPriceHistory.Enabled = Convert.ToBoolean(Convert.ToInt16(lblPricesReportAccess.Text));
                if (!imgProductPriceHistory.Enabled) imgProductPriceHistory.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgChangePrice = (ImageButton)e.Item.FindControl("imgChangePrice");
                imgChangePrice.Enabled = Convert.ToBoolean(Convert.ToInt16(lblChangePriceAccess.Text));
                if (!imgChangePrice.Enabled) imgChangePrice.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";

                ImageButton imgEditNow = (ImageButton)e.Item.FindControl("imgEditNow");
                imgEditNow.Enabled = cmdEdit.Visible;
                if (!imgEditNow.Enabled) imgEditNow.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
			}
		}
        protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			Common Common = new Common();
			HtmlInputCheckBox chkList = null;
			string stParam = null;

            HyperLink lnkProductCode = null;
			chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
			Common = new Common();
			stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
				"&prodid=" + Common.Encrypt(chkList.Value,Session.SessionID);

			switch(e.CommandName)
			{
                case "imgProductTag":
                    {
                        ImageButton imgProductTag = (ImageButton) e.Item.FindControl("imgProductTag");
                        Products clsProduct = new Products();

                        if (imgProductTag.ToolTip == "Tag this product as INACTIVE.")
                            clsProduct.TagInactive(long.Parse(chkList.Value));
                        else
                            clsProduct.TagActive(long.Parse(chkList.Value));

                        clsProduct.CommitAndDispose();
                        LoadList();
                    }
                    break;

                case "cmdSaveVariationClick":
                    {
                        DropDownList cboVariationType = (DropDownList)e.Item.FindControl("cboVariationType");
                        SaveVariation(long.Parse(chkList.Value), int.Parse(cboVariationType.SelectedItem.Value), cboVariationType.SelectedItem.Text);
                        ProductVariation clsVariation = new ProductVariation();
                        cboVariationType.DataTextField = "VariationType";
                        cboVariationType.DataValueField = "VariationID";
                        cboVariationType.DataSource = clsVariation.AvailableVariationsDataTable(long.Parse(chkList.Value), "VariationType", SortOption.Ascending).DefaultView;
                        cboVariationType.DataBind();
                        cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;
                        clsVariation.CommitAndDispose();
                    }
                    break;
                case "imgSaveVariationClick":
                    {
                        DropDownList cboVariationType = (DropDownList)e.Item.FindControl("cboVariationType");
                        SaveVariation(long.Parse(chkList.Value), int.Parse(cboVariationType.SelectedItem.Value), cboVariationType.SelectedItem.Text);
                        ProductVariation clsVariation = new ProductVariation();
                        cboVariationType.DataTextField = "VariationType";
                        cboVariationType.DataValueField = "VariationID";
                        cboVariationType.DataSource = clsVariation.AvailableVariationsDataTable(long.Parse(chkList.Value), "VariationType", SortOption.Ascending).DefaultView;
                        cboVariationType.DataBind();
                        cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;
                        clsVariation.CommitAndDispose();	
                    }
                    break;
                case "imgVariationsClick":
                    stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Variations/Default.aspx" + stParam);
                    break;
                case "imgVariationsMatrixClick":
                    stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
                    break;
                case "imgUnitsMatrixClick":
                    stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
                    break;
                case "imgVariationsAddClick":
                    stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
                        "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Variations/Default.aspx" + stParam);
                    break;
                case "imgVariationsMatrixAddClick":
                    stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
                        "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
                    break;
                case "imgUnitsMatrixAddClick":
                    stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
                        "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
                    break;
                case "imgPackageMatrixAddClick":
                    stParam = "?task=" + Common.Encrypt("add", Session.SessionID) +
                        "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Package/Default.aspx" + stParam);
                    break;
                case "imgInventoryAdjustmentClick":
                    lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
                    stParam = "?task=" + Common.Encrypt("invadjustment", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(lnkProductCode.Text, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
                    break;
                case "imgProductHistoryClick":
                    lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
                    stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(lnkProductCode.Text, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
                    break;
                case "imgProductPriceHistoryClick":
                    lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
                    stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(lnkProductCode.Text, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
                    break;
                case "imgChangePriceClick":
                    lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
                    stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(lnkProductCode.Text, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgFinanceClick":
                    stParam = "?task=" + Common.Encrypt("finance", Session.SessionID) +
                                "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
                    break;
                case "imgEditNowClick":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);	
                    Response.Redirect("Default.aspx" + stParam);
                    break;
			}
		}
		private void imgPrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateProductPrice();
		}
		private void cmdProductPriceUpdate_Click(object sender, System.EventArgs e)
		{
			UpdateProductPrice();
		}
		
		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Products); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			idEdit.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;
            lblUnitMatrixAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductComposition); 
			cmdCompose.Visible = clsDetails.Write; 
			idCompose.Visible = clsDetails.Write; 
			lblSeparator3.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ItemSetupFinancial);
            cmdFinance.Visible = clsDetails.Write;
            idFinance.Visible = clsDetails.Write;
            lblSeparator4.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.Variations);
            lblVariationsAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductPackage);
            lblProductPackageAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.InvAdjustment);
            lblInvAdjustmentAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductsListReport);
            lblProductsListReportAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ProductsListReport);
            lblProductsListReportAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.PricesReport);
            lblPricesReportAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ChangeProductPrices);
            lblChangePriceAccess.Text = Convert.ToInt16(clsDetails.Write).ToString();

			clsAccessRights.CommitAndDispose();
		}
        private void LoadSortFieldOptions(DataListItemEventArgs e)
		{
			Common Common = new Common();
			string stParam = null;		

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
				sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

			if (sortoption == SortOption.Ascending)
				stParam += "?sortoption=" + Common.Encrypt(SortOption.Desscending.ToString("G"), Session.SessionID);
			else if (sortoption == SortOption.Desscending)
				stParam += "?sortoption=" + Common.Encrypt(SortOption.Ascending.ToString("G"), Session.SessionID);

			System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString;;
			foreach(string querystring in querystrings.AllKeys)
			{
                if (Server.UrlDecode(querystring.ToLower()) != "sortfield" && Server.UrlDecode(querystring.ToLower()) != "sortoption") 
					stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
			}

			HyperLink SortByProductCode = (HyperLink) e.Item.FindControl("SortByProductCode");
			HyperLink SortByBarCode = (HyperLink) e.Item.FindControl("SortByBarCode");
			HyperLink SortByDescription = (HyperLink) e.Item.FindControl("SortByDescription");
			HyperLink SortByGroupName = (HyperLink) e.Item.FindControl("SortByGroupName");
			HyperLink SortByUnit = (HyperLink) e.Item.FindControl("SortByUnit");

			SortByProductCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductCode", Session.SessionID);
			SortByBarCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BarCode", Session.SessionID);
			SortByDescription.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductDesc", Session.SessionID);
			SortByGroupName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductGroupName", Session.SessionID);
			SortByUnit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("UnitName", Session.SessionID);
		}
		private void LoadList()
		{	
			Products clsProduct = new Products();
			Common Common = new Common();

			string SortField = "ProductDesc";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive;
            if (rdoShowActiveOnly.Checked == true) clsProductListFilterType = ProductListFilterType.ShowActiveOnly;
            if (rdoShowInactiveOnly.Checked == true) clsProductListFilterType = ProductListFilterType.ShowInactiveOnly;

            int intLimit = 0;
            try { intLimit = int.Parse(txtLimit.Text); }
            catch { }

            string SearchKey = string.Empty;
            if (Request.QueryString["Search"] != null)
            { SearchKey = Server.UrlDecode(Common.Decrypt((string)Request.QueryString["search"], Session.SessionID)); }
            else if (Session["Search"] != null)
            { SearchKey = Server.UrlDecode(Common.Decrypt(Session["Search"].ToString(), Session.SessionID)); }

            try { Session.Remove("Search"); } catch { }
            if (SearchKey == null) { SearchKey = string.Empty; }
            else if (SearchKey != string.Empty) { Session.Add("Search", Common.Encrypt(SearchKey, Session.SessionID)); }

            ProductColumns clsProductColumns = new ProductColumns();
            clsProductColumns.Active = true;

            clsProductColumns.BarCode = true;
            clsProductColumns.ProductCode = true;
            clsProductColumns.ProductDesc = true;
            clsProductColumns.ProductGroupID = true;
            clsProductColumns.ProductGroupCode = true;
            clsProductColumns.BaseUnitID = true;
            clsProductColumns.BaseUnitCode = true;
            clsProductColumns.Price = true;
            clsProductColumns.PurchasePrice = true;
            clsProductColumns.ProductSubGroupCode = true;
            clsProductColumns.MainQuantity = true;
            clsProductColumns.SupplierName = true;
            clsProductColumns.SupplierID = true;
            clsProductColumns.IncludeInSubtotalDiscount = true;
            clsProductColumns.VAT = true;
            clsProductColumns.EVAT = true;
            clsProductColumns.LocalTax = true;

            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = SearchKey;
            clsSearchKeys.BarCode2 = SearchKey;
            clsSearchKeys.BarCode3 = SearchKey;
            clsSearchKeys.ProductCode = SearchKey;

            System.Data.DataTable dt = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKeys, clsProductListFilterType, 0, System.Data.SqlClient.SortOrder.Ascending, 100, false, SortField, SortOption.Ascending);

            if (dt.Rows.Count == 0)
            {
                clsSearchKeys = new Data.ProductDetails();
                clsSearchKeys.ProductCode = SearchKey;
                dt = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKeys, clsProductListFilterType, 0, System.Data.SqlClient.SortOrder.Ascending, 100, false, SortField, SortOption.Ascending);
            }
            PageData.DataSource = dt.DefaultView;
            //PageData.DataSource = clsProduct.ListAsDataTable(clsProductColumns, 0, clsProductListFilterType, 0, System.Data.SqlClient.SortOrder.Ascending, clsSearchColumns, SearchKey, 0, 0, string.Empty, 0, string.Empty, 100, false, false, SortField, SortOption.Ascending).DefaultView;

			clsProduct.CommitAndDispose();

			int iPageSize = Convert.ToInt16(Session["PageSize"]) ;
			
			PageData.AllowPaging = true;
			PageData.PageSize = iPageSize;
			try
			{
				PageData.CurrentPageIndex = Convert.ToInt16(cboCurrentPage.SelectedItem.Value) - 1;				
				lstItem.DataSource = PageData;
				lstItem.DataBind();
			}
			catch
			{
				PageData.CurrentPageIndex = 1;
				lstItem.DataSource = PageData;
				lstItem.DataBind();
			}			
			
			cboCurrentPage.Items.Clear();
			for (int i=0; i < PageData.PageCount;i++)
			{
				int iValue = i + 1;
				cboCurrentPage.Items.Add(new ListItem(iValue.ToString(),iValue.ToString()));
				if (PageData.CurrentPageIndex == i)
				{	cboCurrentPage.Items[i].Selected = true;}
				else
				{	cboCurrentPage.Items[i].Selected = false;}
			}
			lblDataCount.Text = " of " + " " + PageData.PageCount;
		}
		private bool Delete()
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
				Products clsProduct = new Products();
				clsProduct.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsProduct.CommitAndDispose();

				Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();

				clsAuditDetails.ActivityDate = DateTime.Now;
				clsAuditDetails.User = Convert.ToString(Session["Name"]);
				clsAuditDetails.IPAddress = Request.UserHostAddress;
				clsAuditDetails.Activity = "Products";
				clsAuditDetails.Remarks = "Delete Product(s). IDs:'" + stIDs + "'";

				Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
				clsAuditTrail.Insert(clsAuditDetails);
				clsAuditTrail.CommitAndDispose();
			}

			return boRetValue;
		}
		private void Update()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("Default.aspx" + stParam);
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
		private void Compose()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("compose",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot update product composition of more than one record. Please select at least one record to compose.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
        private void SetupFinance()
        {
            if (isChkListSingle() == true)
            {
                string stID = GetFirstID();
                if (stID != null)
                {
                    Common Common = new Common();
                    string stParam = "?task=" + Common.Encrypt("finance", Session.SessionID) + "&id=" + Common.Encrypt(stID, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
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
		private void UpdateProductPrice()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("ProductPrice",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);
					Response.Redirect("Default.aspx" + stParam);
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
        private void SaveVariation(long ProductID, int VariationID, string VariationType)
        {
            ProductVariationDetails clsDetails = new ProductVariationDetails();

            clsDetails.ProductID = ProductID;
            clsDetails.VariationID = VariationID;
            clsDetails.VariationType = VariationType;

            ProductVariation clsProdVariation = new ProductVariation();
            int id = clsProdVariation.Insert(clsDetails);
            clsProdVariation.CommitAndDispose();
        }

		#endregion

        protected void rdoShowAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void rdoShowActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void rdoShowInactiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }
}
}
