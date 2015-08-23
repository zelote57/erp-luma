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
                    cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
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
                    cmdCopyPOSToActual.Attributes.Add("onClick", "return confirm_copypostoactual_inventory();");
                    imgCopyPOSToActual.Attributes.Add("onClick", "return confirm_copypostoactual_inventory();");
                    //cmdCloseInventory.Attributes.Add("onClick", "return confirm_close_inventory();");
                    //imgCloseInventory.Attributes.Add("onClick", "return confirm_close_inventory();");
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

        protected void imgCopyPOSToActual_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (CopyPOSToActual())
                LoadList();
        }

        protected void cmdCopyPOSToActual_Click(object sender, System.EventArgs e)
        {
            if (CopyPOSToActual())
                LoadList();
        }

        protected void imgCloseInventory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string strRefrenceNo = CloseInventory();

            if (!string.IsNullOrEmpty(strRefrenceNo))
            {
                PrintClosingInventory(strRefrenceNo);
                LoadList();
                LockUnlockForSelling("Unlock");
            }
        }

        protected void cmdCloseInventory_Click(object sender, EventArgs e)
        {
            string strRefrenceNo = CloseInventory();

            if (!string.IsNullOrEmpty(strRefrenceNo))
            {
                PrintClosingInventory(strRefrenceNo);
                LoadList();
                LockUnlockForSelling("Unlock");
            }
        }

        protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }


        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                mlngItemNo += 1;

				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["ProductID"].ToString();

                HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");
                chkMatrixID.Value = dr["MatrixID"].ToString();

                Label lblItemNo = (Label)e.Item.FindControl("lblItemNo");
                lblItemNo.Text = mlngItemNo.ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				Label lnkProductCode = (Label) e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr["ProductCode"].ToString();

                Label lnkVariationDesc = (Label)e.Item.FindControl("lnkVariationDesc");
                lnkVariationDesc.Text = dr["MatrixDescription"].ToString();

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
                if (chkMatrixID.Value == "0")
                {
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
                }
                else
                {
                    imgProductTag.Visible = false;
                }
			}
		}				
		protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");
			string stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
					            "&prodid=" + Common.Encrypt(chkList.Value,Session.SessionID);

            
			switch(e.CommandName)
			{
                case "imgProductTag":
                    {
                        ImageButton imgProductTag = (ImageButton)e.Item.FindControl("imgProductTag");
                        Products clsProduct = new Products();

                        if (imgProductTag.ToolTip == "Tag this product as INACTIVE.")
                        {
                            clsProduct.TagInactive(long.Parse(chkList.Value));
                            imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtaginact.gif";
                            imgProductTag.ToolTip = "Tag this product as ACTIVE.";
                        }
                        else
                        {
                            clsProduct.TagActive(long.Parse(chkList.Value));
                            imgProductTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtagact.gif";
                            imgProductTag.ToolTip = "Tag this product as INACTIVE.";
                        }
                        clsProduct.CommitAndDispose();
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
                        ProductInventories clsProductInventories = new ProductInventories();
                        clsProductInventories.UpdateActualQuantity(int.Parse(cboBranch.SelectedItem.Value), long.Parse(chkList.Value), long.Parse(chkMatrixID.Value), decimal.Parse(txtActualQuantity.Text));
                        clsProductInventories.CommitAndDispose();
                    }
                    break;

			}
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintClosingInventorySheet();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintClosingInventorySheet();
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

        private void PrintClosingInventorySheet()
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&type=" + Common.Encrypt("invcount", Session.SessionID) + "&contactid=" + Common.Encrypt(cboContact.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openwindow", javaScript, true);
        }

        private void PrintClosingInventory(string strRefNo = "")
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&refno=" + Common.Encrypt(strRefNo, Session.SessionID) + "&contactid=" + Common.Encrypt(cboContact.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID); ;
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updCloseInventory, this.updCloseInventory.GetType(), "openwindow", javaScript, true);
        }

        private string CloseInventory()
        {
            string strRetValue = "";

            try
            {
                DateTime DeliveryDate = Convert.ToDateTime(txtClosingDate.Text);

                ERPConfig clsERPConfig = new ERPConfig();
                ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

                if (clsERPConfigDetails.PostingDateFrom <= DeliveryDate && clsERPConfigDetails.PostingDateTo >= DeliveryDate)
                {
                    string strReferenceNo = Constants.CLOSE_INVENTORY_CODE + CompanyDetails.BECompanyCode + DateTime.Now.Year.ToString() + clsERPConfig.get_LastClosingNo();

                    AccessUserDetails clsAccessUserDetails = (AccessUserDetails)Session["AccessUserDetails"];

                    ProductInventories clsProductInventories = new ProductInventories(clsERPConfig.Connection, clsERPConfig.Transaction);
                    clsProductInventories.CloseInventoryBySupplier(int.Parse(cboBranch.SelectedItem.Value), clsAccessUserDetails.UID, DateTime.Parse(txtClosingDate.Text), strReferenceNo, long.Parse(cboContact.SelectedItem.Value), cboContact.SelectedItem.Text);

                    Products clsProducts = new Products(clsERPConfig.Connection, clsERPConfig.Transaction);
                    clsProducts.LockUnlockForSellingBySupplier(int.Parse(cboBranch.SelectedItem.Value), long.Parse(cboContact.SelectedItem.Value), false);

                    clsERPConfig.CommitAndDispose();
                    strRetValue = strReferenceNo;
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
            return strRetValue;
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

        private bool CopyPOSToActual()
        {
            bool boRetValue = false;

            Products clsProduct = new Products();
            boRetValue = clsProduct.CopyPOSToActualBySupplier(int.Parse(cboBranch.SelectedItem.Value), long.Parse(cboContact.SelectedItem.Value));
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
            string SortField = "ProductCode";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            Int64 lngSupplierID = Convert.ToInt64(cboContact.SelectedItem.Value);

            ProductInventories clsProductInventories = new ProductInventories();
            System.Data.DataTable dt = clsProductInventories.ListAsDataTable(BranchID: int.Parse(cboBranch.SelectedItem.Value), SupplierID: lngSupplierID, clsProductListFilterType: ProductListFilterType.ShowActiveOnly, SortField: "ProductCode ASC, MatrixDescription ASC, BarCode1", SortOrder: SortOption.Desscending);

            Contacts clsContacts = new Contacts(clsProductInventories.Connection, clsProductInventories.Transaction);
            ContactDetails clsContactDetails = clsContacts.Details(lngSupplierID);

            clsProductInventories.CommitAndDispose();

            PageData.DataSource = dt.DefaultView;

            if (!clsContactDetails.isLock)
            {
                cmdLockUnlockProduct.Text = "Lock Supplier [<label class='ms-error'>Note:</label>Products under this supplier are <u>CURRENTLY ALLOWED</u> for Selling]";
                cmdLockUnlockProduct.ToolTip = "unlock";
            }
            else
            {
                cmdLockUnlockProduct.Text = "UnLock Supplier [<label class='ms-error'>Note:</label>Products under this supplier are <u>CURRENTLY NOT ALLOWED</u> for Selling]";
                cmdLockUnlockProduct.ToolTip = "lock";
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
            HtmlInputCheckBox chkList = null;
            TextBox txtActualQuantity = null;
            int intBranchID = int.Parse(cboBranch.SelectedItem.Value);
            ProductInventories clsProductInventories = new ProductInventories();
            foreach (DataListItem item in lstItem.Items)
            {
                txtActualQuantity = (TextBox)item.FindControl("txtActualQuantity");
                chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                chkMatrixID = (HtmlInputCheckBox)item.FindControl("chkMatrixID");
                try
                {
                    decimal decActualQuantity = decimal.Parse(txtActualQuantity.Text);
                    clsProductInventories.UpdateActualQuantity(intBranchID, long.Parse(chkList.Value), long.Parse(chkMatrixID.Value), decActualQuantity);
                    txtActualQuantity.Text = Server.HtmlEncode(decActualQuantity.ToString("#,##0.#0"));
                }
                catch { }
            }
            clsProductInventories.CommitAndDispose();
            LoadList();
        }

        private void LockUnlockForSelling(string UnLock = "")
        {
            bool isLock = false;

            if (UnLock == "Unlock")
            {
                isLock = false;
            }
            else if (cmdLockUnlockProduct.ToolTip == "unlock")
            {
                isLock = true;
            }

            Products clsProducts = new Products();
            clsProducts.LockUnlockForSellingBySupplier(int.Parse(cboBranch.SelectedItem.Value), long.Parse(cboContact.SelectedItem.Value), isLock);
            clsProducts.CommitAndDispose();

            if (!isLock)
            {
                cmdLockUnlockProduct.Text = "Lock Supplier [<label class='ms-error'>Note:</label>Products under this supplier are <u>CURRENTLY ALLOWED</u> for Selling]";
                cmdLockUnlockProduct.ToolTip = "unlock";
            }
            else
            {
                cmdLockUnlockProduct.Text = "UnLock Supplier [<label class='ms-error'>Note:</label>Products under this supplier are <u>CURRENTLY NOT ALLOWED</u> for Selling]";
                cmdLockUnlockProduct.ToolTip = "lock";
            }
        }
        #endregion
        
    }
}
