using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Security;
using Microsoft.Office.Interop;

namespace AceSoft.RetailPlus.Inventory
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using Microsoft.Office.Interop;

	public partial  class __CloseInventoryDetailed : System.Web.UI.UserControl
	{
        long mlngItemNo = 0;
		protected PagedDataSource PageData = new PagedDataSource();

        #region Web Form Methods
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
                    Branch clsBranch = new Branch();
                    cboBranch.DataTextField = "BranchCode";
                    cboBranch.DataValueField = "BranchID";
                    cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
                    cboBranch.DataBind();
                    
                    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));

                    mlngItemNo = 0;

                    Contacts clsContact = new Contacts(clsBranch.Connection, clsBranch.Transaction);
                    cboContact.DataTextField = "ContactName";
                    cboContact.DataValueField = "ContactID";
                    cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text, 100).DefaultView;
                    cboContact.DataBind();
                    cboContact.SelectedIndex = 0;

                    clsBranch.CommitAndDispose();

                    txtClosingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					ManageSecurity();
					LoadList();
                    cmdZeroOutActualQuantity.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
                    imgZeroOutActualQuantity.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
                    cmdZeroOutActualQuantity1.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
                    imgZeroOutActualQuantity1.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
                    cmdCloseInventory.Attributes.Add("onClick", "return confirm_close_inventory();");
                    imgCloseInventory.Attributes.Add("onClick", "return confirm_close_inventory();");
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

        protected void imgContactCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Contacts clsContact = new Contacts();
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text, 100).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContact.Items.Count > 1 && txtContactCode.Text.Trim() != string.Empty) cboContact.SelectedIndex = 1; else cboContact.SelectedIndex = 0;
            clsContact.CommitAndDispose();
        }

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
        }

		protected void imgZeroOutActualQuantity_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            if (ZeroOutActualQuantity())
                LoadList();
		}

		protected void cmdZeroOutActualQuantity_Click(object sender, System.EventArgs e)
		{
            if (ZeroOutActualQuantity())
                LoadList();
		}

        protected void imgCloseInventory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (CloseInventory())
                Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID));
                //LoadList();
        }

        protected void cmdCloseInventory_Click(object sender, EventArgs e)
        {
            if (CloseInventory())
                Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID));
                //LoadList();
        }

        protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        //protected void imgUpload_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    if (UpdateActualQuantity())
        //        LoadList();
        //}

        //protected void cmdUpload_Click(object sender, EventArgs e)
        //{
        //    if (UpdateActualQuantity())
        //        LoadList();
        //}

        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                mlngItemNo += 1;

				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["ProductID"].ToString();

                Label lblItemNo = (Label)e.Item.FindControl("lblItemNo");
                lblItemNo.Text = mlngItemNo.ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				Label lnkProductCode = (Label) e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr["ProductCode"].ToString();

                decimal decPOSQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                decimal decActualQuantity = Convert.ToDecimal(dr["ActualQuantity"].ToString());
                decimal decDifference = decPOSQuantity - decActualQuantity;

                TextBox txtPOSQuantity = (TextBox)e.Item.FindControl("txtPOSQuantity");
                txtPOSQuantity.Text = decPOSQuantity.ToString("#,##0.#0");

                TextBox txtActualQuantity = (TextBox)e.Item.FindControl("txtActualQuantity");
                txtActualQuantity.Text = decActualQuantity.ToString("#,##0.#0");

                TextBox txtShort = (TextBox)e.Item.FindControl("txtShort");
                TextBox txtOver = (TextBox)e.Item.FindControl("txtOver");

                TextBox txtAmountShort = (TextBox)e.Item.FindControl("txtAmountShort");
                TextBox txtAmountOver = (TextBox)e.Item.FindControl("txtAmountOver");

                TextBox txtPurchasePrice = (TextBox)e.Item.FindControl("txtPurchasePrice");
                decimal decPurchasePrice = Convert.ToDecimal(dr["Purchaseprice"].ToString());
                txtPurchasePrice.Text = decPurchasePrice.ToString("#,##0.#0");

                if (decDifference > 0) 
                {
                    txtShort.Text = decDifference.ToString("#,##0.#0");
                    txtOver.Text = "0.00";

                    txtAmountShort.Text = Convert.ToDecimal(decPurchasePrice * decDifference).ToString("#,##0.#0");
                    txtAmountOver.Text = "0.00";
                }
                else
                {
                    decDifference = decDifference * decimal.Parse("-1");

                    txtShort.Text = "0.00";
                    txtOver.Text = decDifference.ToString("#,##0.#0");

                    txtAmountShort.Text = "0.00";
                    txtAmountOver.Text = Convert.ToDecimal(decPurchasePrice * decDifference).ToString("#,##0.#0");
                }

                ImageButton imgProductTag = (ImageButton)e.Item.FindControl("imgProductTag");
                if (Convert.ToBoolean(dr["Active"].ToString()))
                {
                    imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtagact.gif";
                    imgProductTag.ToolTip = "Tag this product as INACTIVE.";
                }
                else //if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly)
                {
                    imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtaginact.gif";
                    imgProductTag.ToolTip = "Tag this product as ACTIVE.";
                }

                // Populate Variations
                long ProductID = Convert.ToInt64(dr["ProductID"].ToString());

                DataList lstVariationMatrix = (DataList)e.Item.FindControl("lstVariationMatrix");
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                System.Data.DataTable dt = clsProductVariationsMatrix.BaseListSimpleAsDataTable(ProductID, SortField: "VariationDesc");
                lstVariationMatrix.DataSource = dt.DefaultView;
                lstVariationMatrix.DataBind();
                clsProductVariationsMatrix.CommitAndDispose();

                if (dt.Rows.Count > 0)
                {
                    txtActualQuantity.Enabled = false;
                    txtActualQuantity.CssClass = "ms-short-disabled";
                }
			}
		}				
		protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			HtmlInputCheckBox chkList = null;
			string stParam = null;

			chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
			stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
					"&prodid=" + Common.Encrypt(chkList.Value,Session.SessionID);

            
			switch(e.CommandName)
			{
                case "imgProductTag":
                    {
                        ImageButton imgProductTag = (ImageButton)e.Item.FindControl("imgProductTag");
                        Products clsProduct = new Products();

                        if (imgProductTag.ToolTip == "Tag this product as INACTIVE.")
                            clsProduct.TagInactive(long.Parse(chkList.Value));
                        else
                            clsProduct.TagActive(long.Parse(chkList.Value));

                        clsProduct.CommitAndDispose();
                        LoadList();
                    }
                    break;
                case "imgSaveActualQuantity":
                    {
                        TextBox txtActualQuantity = (TextBox)e.Item.FindControl("txtActualQuantity");
                        try { decimal.Parse(txtActualQuantity.Text); }
                        catch 
                        {
                            string stScript = "<Script>";
                            stScript += "window.alert('Please enter a VALID actual quantity.')";
                            stScript += "</Script>";
                            Response.Write(stScript);
                            break;
                        }
                        Products clsProduct = new Products();
                        clsProduct.UpdateActualQuantity(int.Parse(cboBranch.SelectedItem.Value), long.Parse(chkList.Value), decimal.Parse(txtActualQuantity.Text));
                        clsProduct.CommitAndDispose();
                    }
                    break;

			}
        }
        protected void lstVariationMatrix_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");
                chkMatrixID.Value = dr["MatrixID"].ToString();

                Label lblProductID = (Label)e.Item.FindControl("lblProductID");
                lblProductID.Text = dr["ProductID"].ToString();

                Label lblVariationDesc = (Label)e.Item.FindControl("lblVariationDesc");
                lblVariationDesc.Text = dr["Description"].ToString();

                decimal decPOSQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                decimal decActualQuantity = Convert.ToDecimal(dr["ActualQuantity"].ToString());
                decimal decDifference = decPOSQuantity - decActualQuantity;

                TextBox txtPOSQuantity = (TextBox)e.Item.FindControl("txtPOSQuantity");
                txtPOSQuantity.Text = decPOSQuantity.ToString("#,##0.#0");

                TextBox txtActualQuantity = (TextBox)e.Item.FindControl("txtActualQuantity");
                txtActualQuantity.Text = decActualQuantity.ToString("#,##0.#0");

                TextBox txtShort = (TextBox)e.Item.FindControl("txtShort");
                TextBox txtOver = (TextBox)e.Item.FindControl("txtOver");

                TextBox txtAmountShort = (TextBox)e.Item.FindControl("txtAmountShort");
                TextBox txtAmountOver = (TextBox)e.Item.FindControl("txtAmountOver");

                TextBox txtPurchasePrice = (TextBox)e.Item.FindControl("txtPurchasePrice");
                decimal decPurchasePrice = Convert.ToDecimal(dr["Purchaseprice"].ToString());
                txtPurchasePrice.Text = decPurchasePrice.ToString("#,##0.#0");

                if (decDifference > 0)
                {
                    txtShort.Text = decDifference.ToString("#,##0.#0");
                    txtOver.Text = "0.00";

                    txtAmountShort.Text = Convert.ToDecimal(decPurchasePrice * decDifference).ToString("#,##0.#0");
                    txtAmountOver.Text = "0.00";
                }
                else
                {
                    decDifference = decDifference * decimal.Parse("-1");

                    txtShort.Text = "0.00";
                    txtOver.Text = decDifference.ToString("#,##0.#0");

                    txtAmountShort.Text = "0.00";
                    txtAmountOver.Text = Convert.ToDecimal(decPurchasePrice * decDifference).ToString("#,##0.#0");
                }

                ImageButton imgMatrixDelete = (ImageButton)e.Item.FindControl("imgMatrixDelete");
                imgMatrixDelete.Attributes.Add("onClick", "return confirm_item_delete();");

                Label lblBranchID = (Label)e.Item.FindControl("lblBranchID");
                Label lblBranchCode = (Label)e.Item.FindControl("lblBranchCode");

                lblBranchID.Text = dr["BranchID"].ToString();
                if (dr["BranchID"].ToString() == cboBranch.SelectedItem.Value)
                { 
                    lblBranchCode.Text = "";
                }
                else 
                { 
                    lblBranchCode.Text = dr["BranchCode"].ToString();
                    
                }
            }
        }
        protected void lstVariationMatrix_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");

            switch (e.CommandName)
            {
                case "imgMatrixDelete":
                    {
                        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                        clsProductVariationsMatrix.Delete(long.Parse(chkMatrixID.Value));
                        clsProductVariationsMatrix.CommitAndDispose();
                        LoadList();
                    }
                    break;
                case "imgSaveActualQuantity":
                    {
                        Label lblProductID = (Label)e.Item.FindControl("lblProductID");
                        TextBox txtActualQuantity = (TextBox)e.Item.FindControl("txtActualQuantity");
                        try { decimal.Parse(txtActualQuantity.Text); }
                        catch
                        {
                            string stScript = "<Script>";
                            stScript += "window.alert('Please enter a VALID actual quantity.')";
                            stScript += "</Script>";
                            Response.Write(stScript);
                            break;
                        }
                        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                        clsProductVariationsMatrix.UpdateActualQuantity(long.Parse(lblProductID.Text), long.Parse(chkMatrixID.Value), decimal.Parse(txtActualQuantity.Text));
                        clsProductVariationsMatrix.CommitAndDispose();
                        LoadList();
                    }
                    break;

            }
        }
        protected void imgSaveActualQuantity_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveAllActualQuantity();
        }
        protected void cmdSaveActualQuantity_Click(object sender, EventArgs e)
        {
            SaveAllActualQuantity();
        }
        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        protected void cboContact_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }
        protected void imgLockUnlockProduct_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LockUnlockForSelling();
        }
        protected void cmdLockUnlockProduct_Click(object sender, EventArgs e)
        {
            LockUnlockForSelling();
        }
        

        #endregion

        #region Private methods

        private bool CloseInventory()
        {
            bool boRetValue = false;

            try
            {
                DateTime DeliveryDate = Convert.ToDateTime(txtClosingDate.Text);

                ERPConfig clsERPConfig = new ERPConfig();
                ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

                if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
                {
                    AccessUserDetails clsAccessUserDetails = (AccessUserDetails)Session["AccessUserDetails"];

                    Products clsProduct = new Products(clsERPConfig.Connection, clsERPConfig.Transaction);
                    clsProduct.CloseInventory(int.Parse(cboBranch.SelectedItem.Value), clsAccessUserDetails.UID, DateTime.Parse(txtClosingDate.Text), Constants.CLOSE_INVENTORY_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsERPConfig.get_LastClosingNo(), true);
                    clsERPConfig.CommitAndDispose();
                    boRetValue = true;
                }
                else
                {
                    clsERPConfig.CommitAndDispose();
                    string stScript = "<Script>";
                    stScript += "window.alert('Sorry you cannot close using the closing date: " + txtClosingDate.Text + ". Please enter an allowable posting date.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                }
            }
            catch (Exception ex)
            {
                string stScript = "<Script>";
                stScript += "window.alert('An error has occured while closing the inventory. Details:' " + ex.Message + ")";
                stScript += "</Script>";
                Response.Write(stScript);
            }
            return boRetValue;
        }

        private bool ZeroOutActualQuantity()
        {
            bool boRetValue = false;

            Products clsProduct = new Products();
            boRetValue = clsProduct.ZeroOutActualQuantityBySupplier(int.Parse(cboBranch.SelectedItem.Value), long.Parse(cboContact.SelectedItem.Value));
            clsProduct.CommitAndDispose();
            boRetValue = true;

            return boRetValue;
        }

        private void ManageSecurity()
        {
            Int64 UID = Convert.ToInt64(Session["UID"]);
            AccessRights clsAccessRights = new AccessRights();
            AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CloseInventory);
            imgZeroOutActualQuantity.Visible = clsDetails.Write;
            cmdZeroOutActualQuantity.Visible = clsDetails.Write;
            imgCloseInventory.Visible = clsDetails.Write;
            cmdCloseInventory.Visible = clsDetails.Write;
            lblSeparator1.Visible = clsDetails.Write;
            //lblSeparator2.Visible = clsDetails.Write;

            clsAccessRights.CommitAndDispose();
        }

        private void LoadList()
        {
            Products clsProduct = new Products();
            Common Common = new Common();

            string SortField = "ProductCode";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveOnly;

            Int64 lngSupplierID = Convert.ToInt64(cboContact.SelectedItem.Value);

            PageData.DataSource = clsProduct.SearchDataTableSimple(int.Parse(cboBranch.SelectedItem.Value), clsProductListFilterType, string.Empty, lngSupplierID, 0, string.Empty, 0, string.Empty, 0, false, false, SortField, sortoption).DefaultView;

            Contacts clsContacts = new Contacts(clsProduct.Connection, clsProduct.Transaction);
            ContactDetails clsContactDetails = clsContacts.Details(lngSupplierID);

            clsProduct.CommitAndDispose();

            if (!clsContactDetails.isLock)
            {
                cmdLockUnlockProduct.Text = "Lock Supplier [Products under this supplier are allowed for Selling]";
                cmdLockUnlockProduct1.Text = "Lock Supplier [Products under this supplier are allowed for Selling]";
            }
            else
            {
                cmdLockUnlockProduct.Text = "UnLock Supplier [Current Products under this supplier are NOT allowed for Selling]";
                cmdLockUnlockProduct1.Text = "UnLock Supplier [Current Products under this supplier are NOT allowed for Selling]";
            }

            int iPageSize = Convert.ToInt16(Session["PageSize"]);

            PageData.AllowPaging = true;
            PageData.PageSize = 5000;
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
            for (int iRow = 0; iRow < PageData.PageCount; iRow++)
            {
                int iValue = iRow + 1;
                cboCurrentPage.Items.Add(new ListItem(iValue.ToString(), iValue.ToString()));
                if (PageData.CurrentPageIndex == iRow)
                { cboCurrentPage.Items[iRow].Selected = true; }
                else
                { cboCurrentPage.Items[iRow].Selected = false; }
            }
            lblDataCount.Text = " of " + " " + PageData.PageCount;
        }

        private void SaveAllActualQuantity()
        {
            HtmlInputCheckBox chkMatrixID = null;
            Label lblProductID = null;
            TextBox txtActualQuantity = null;
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
            foreach (DataListItem item in lstItem.Items)
            {
                DataList lstVariationMatrix = (DataList)item.FindControl("lstVariationMatrix");

                foreach (DataListItem itemVariation in lstVariationMatrix.Items)
                {
                    txtActualQuantity = (TextBox)itemVariation.FindControl("txtActualQuantity");
                    lblProductID = (Label)itemVariation.FindControl("lblProductID");
                    chkMatrixID = (HtmlInputCheckBox)itemVariation.FindControl("chkMatrixID");
                    try
                    {
                        decimal decActualQuantity = decimal.Parse(txtActualQuantity.Text);
                        clsProductVariationsMatrix.UpdateActualQuantity(long.Parse(lblProductID.Text), long.Parse(chkMatrixID.Value), decActualQuantity);
                        txtActualQuantity.Text = Server.HtmlEncode(decActualQuantity.ToString("#,##0.#0"));
                    }
                    catch { }
                }
                
            }
            clsProductVariationsMatrix.CommitAndDispose();
            LoadList();
        }

        private void LockUnlockForSelling()
        {
            bool isLock = false;

            if (cmdLockUnlockProduct.Text == "Lock Supplier [Products under this supplier are allowed for Selling]")
            {
                isLock = true;
            }

            Products clsProducts = new Products();
            clsProducts.LockUnlockForSellingBySupplier(int.Parse(cboBranch.SelectedItem.Value), long.Parse(cboContact.SelectedItem.Value), isLock);
            clsProducts.CommitAndDispose();

            if (!isLock)
            {
                cmdLockUnlockProduct.Text = "Lock Supplier [Products under this supplier are allowed for Selling]";
                cmdLockUnlockProduct1.Text = "Lock Supplier [Products under this supplier are allowed for Selling]";
            }
            else
            {
                cmdLockUnlockProduct.Text = "UnLock Supplier [Current Products under this supplier are NOT allowed for Selling]";
                cmdLockUnlockProduct1.Text = "UnLock Supplier [Current Products under this supplier are NOT allowed for Selling]";
            }
        }
        #endregion
        
    }
}
