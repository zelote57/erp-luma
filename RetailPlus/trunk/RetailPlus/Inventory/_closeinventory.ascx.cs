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

	public partial  class __CloseInventory : System.Web.UI.UserControl
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
                    clsBranch.CommitAndDispose();
                    cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));

                    try
                    {
                        //AccessUserDetails clsAccessUserDetails = (AccessUserDetails) Session["AccessUserDetails"];
                        txtLimit.Text = Session["PageSize"].ToString();
                    }
                    catch { }
                    txtClosingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					ManageSecurity();
					LoadList();
                    cmdZeroOutActualQuantity.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
                    imgZeroOutActualQuantity.Attributes.Add("onClick", "return confirm_zeroout_inventory();");
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
                // LoadList();
        }

        protected void cmdCloseInventory_Click(object sender, EventArgs e)
        {
            if (CloseInventory())
                Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID));
                // LoadList();
        }

        protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        protected void imgUpload_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (UpdateActualQuantity())
                LoadList();
        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            if (UpdateActualQuantity())
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
                if (dr["Active"].ToString() == "1")
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
                        Product clsProduct = new Product();

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
                        Product clsProduct = new Product();
                        clsProduct.UpdateActualQuantity(int.Parse(cboBranch.SelectedItem.Value), long.Parse(chkList.Value), decimal.Parse(txtActualQuantity.Text));
                        clsProduct.CommitAndDispose();
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

                    Product clsProduct = new Product(clsERPConfig.Connection, clsERPConfig.Transaction);
                    clsProduct.CloseInventory(int.Parse(cboBranch.SelectedItem.Value), clsAccessUserDetails.UID, DateTime.Parse(txtClosingDate.Text), Constants.CLOSE_INVENTORY_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsERPConfig.get_LastClosingNo(), false);
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

            Product clsProduct = new Product();
            boRetValue = clsProduct.UpdateActualQuantity(int.Parse(cboBranch.SelectedItem.Value), 0, 0);
            clsProduct.CommitAndDispose();
            boRetValue = true;

            return boRetValue;
        }

        private bool UpdateActualQuantity()
        {
            bool boRetValue = false;

            if (txtPath.HasFile)
            {
                string fn = System.IO.Path.GetFileName(txtPath.PostedFile.FileName);

                if (!fn.Contains(Constants.CLOSE_INVENTORY_FILE_NAME_CODE))
                {
                    string stScript = "<Script>";
                    stScript += "window.alert('Please select a VALID Inventory file to upload.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return boRetValue;
                }
                
                string SaveLocation = Constants.ROOT_DIRECTORY + "/temp/uploaded_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + fn;

                txtPath.PostedFile.SaveAs(SaveLocation);

                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjExcel.DisplayAlerts = false;
                ObjExcel.Visible = false;

                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(SaveLocation, false, false,
                                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                try
                {
                    ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[Constants.CLOSE_INVENTORY_SHEET_NAME];
                }
                catch 
                {
                    ObjWorkBook.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlDoNotSaveChanges, Type.Missing, Type.Missing);
                    ObjExcel.Quit();
                    ObjExcel = null;

                    string stScript = "<Script>";
                    stScript += "window.alert('The file does not contain a valid inventory sheet. Please double check that the sheet name is named INVENTORY.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return boRetValue;
                }

                Microsoft.Office.Interop.Excel.Range rangeQuantityError; int iRowQuantityError = 1;
                Microsoft.Office.Interop.Excel.Worksheet ObjWokSheetQuantityError;
                try 
                { 
                    ObjWokSheetQuantityError = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[Constants.CLOSE_INVENTORY_SHEET_NAME_QUANTITY_ERROR]; 
                }
                catch 
                { 
                    ObjWokSheetQuantityError = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets.Add(Type.Missing, ObjWorkSheet, Type.Missing, Type.Missing);
                    ObjWokSheetQuantityError.Name = Constants.CLOSE_INVENTORY_SHEET_NAME_QUANTITY_ERROR;
                }

                Microsoft.Office.Interop.Excel.Range rangeInvalidProduct; int iRowInvalidProduct = 1;
                Microsoft.Office.Interop.Excel.Worksheet ObjWokSheetInvalidProduct;
                try
                { 
                    ObjWokSheetInvalidProduct = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[Constants.CLOSE_INVENTORY_SHEET_NAME_INVALID_PRODUCT]; 
                }
                catch
                {
                    ObjWokSheetInvalidProduct = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets.Add(Type.Missing, ObjWorkSheet, Type.Missing, Type.Missing);
                    ObjWokSheetInvalidProduct.Name = Constants.CLOSE_INVENTORY_SHEET_NAME_INVALID_PRODUCT;
                }

                Product clsProduct = new Product();
                for (int iRow = 1; iRow <= 10000; iRow++)
                {
                    Microsoft.Office.Interop.Excel.Range rangeProducts = ObjWorkSheet.get_Range("A" + iRow.ToString(), "C" + iRow.ToString());
                    System.Array myvalues = (System.Array)rangeProducts.Cells.Value2;
                    string[] strArray = ConvertToStringArray(myvalues);

                    if (strArray[0] == string.Empty)
                        break;
                    else
                    {
                        if (strArray[1] == string.Empty) strArray[1] = "1";
                        decimal decQuantity = 0;
                        try 
                        {
                            // update the actual quantity
                            decQuantity = decimal.Parse(strArray[1]);
                            bool boAddQuantity = clsProduct.AddActualQuantity(int.Parse(cboBranch.SelectedItem.Value), strArray[0], decQuantity);

                            if (!boAddQuantity)
                            {
                                // if invalid product; report as not error
                                rangeProducts.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);

                                // if invalid product; include in second sheet
                                rangeInvalidProduct = (Microsoft.Office.Interop.Excel.Range)ObjWokSheetInvalidProduct.Cells[iRowInvalidProduct, "A"];
                                rangeInvalidProduct.Value2 = strArray[0];

                                rangeInvalidProduct = (Microsoft.Office.Interop.Excel.Range)ObjWokSheetInvalidProduct.Cells[iRowInvalidProduct, "B"];
                                rangeInvalidProduct.Value2 = strArray[1];

                                iRowInvalidProduct += 1;
                            }
                        }
                        catch 
                        {
                            // if error; report as not error
                            rangeProducts.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            // if error; include in second sheet
                            rangeQuantityError = (Microsoft.Office.Interop.Excel.Range)ObjWokSheetQuantityError.Cells[iRowQuantityError, "A"];
                            rangeQuantityError.Value2 = strArray[0];

                            rangeQuantityError = (Microsoft.Office.Interop.Excel.Range)ObjWokSheetQuantityError.Cells[iRowQuantityError, "B"];
                            rangeQuantityError.Value2 = strArray[1];

                            iRowQuantityError += 1;
                        }
                    }
                }
                clsProduct.CommitAndDispose();
                
                ObjWorkBook.Save();
                ObjWorkBook.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlSaveChanges, Type.Missing, Type.Missing);
                ObjExcel.Quit();
                ObjExcel = null;

                boRetValue = true;

                string stScriptReport = "<Script>";
                stScriptReport += "window.open(" + SaveLocation + ", 'Redirector', 'toolbar=0,location=0,status=0,menubar=0');";
                stScriptReport += "window.opener = self;";
                stScriptReport += "</Script>";
                Response.Write(stScriptReport);
                
            }
            else
            {
                string stScript = "<Script>";
                stScript += "window.alert('Cannot upload an empty filename. Please select the file to be uploaded.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }

            return boRetValue;
        }
        
        private string[] ConvertToStringArray(System.Array values)
        {

            // create a new string array
            string[] theArray = new string[values.Length];

            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int iRow = 1; iRow <= values.Length; iRow++)
            {
                if (values.GetValue(1, iRow) == null)
                    theArray[iRow-1] = string.Empty;
                else
                    theArray[iRow-1] = (string)values.GetValue(1, iRow).ToString();
            }

            return theArray;
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
            lblSeparator2.Visible = clsDetails.Write;

            clsAccessRights.CommitAndDispose();
        }

        private void LoadList()
        {
            Product clsProduct = new Product();
            Common Common = new Common();

            string SortField = "ProductDesc";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive;
            if (rdoShowActiveOnly.Checked == true) clsProductListFilterType = ProductListFilterType.ShowActiveOnly;
            if (rdoShowInactiveOnly.Checked == true) clsProductListFilterType = ProductListFilterType.ShowInactiveOnly;

            int intLimit = 0;
            try { intLimit = int.Parse(txtLimit.Text); }
            catch { }

            string SearchKey = txtSearch.Text;
            if (Request.QueryString["Search"] != null)
            { SearchKey = Common.Decrypt((string)Request.QueryString["search"], Session.SessionID); }

            //PageData.DataSource = clsProduct.SearchDataTableActiveInactive(clsProductListFilterType, SearchKey, SortField, sortoption, intLimit, false).DefaultView;
            PageData.DataSource = clsProduct.SearchDataTableSimple(int.Parse(cboBranch.SelectedItem.Value), clsProductListFilterType, SearchKey, 0, 0, string.Empty, 0, string.Empty, intLimit, false, false, SortField, sortoption).DefaultView;

            clsProduct.CommitAndDispose();

            int iPageSize = Convert.ToInt16(Session["PageSize"]);

            PageData.AllowPaging = true;
            PageData.PageSize = iPageSize; // 5000;
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
            HtmlInputCheckBox chkList = null;
            TextBox txtActualQuantity = null;
            Product clsProduct = new Product();
            foreach (DataListItem item in lstItem.Items)
            {
                txtActualQuantity = (TextBox)item.FindControl("txtActualQuantity");
                chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                try
                {
                    decimal decActualQuantity = decimal.Parse(txtActualQuantity.Text);
                    clsProduct.UpdateActualQuantity(int.Parse(cboBranch.SelectedItem.Value), long.Parse(chkList.Value), decActualQuantity);
                    txtActualQuantity.Text = Server.HtmlEncode(decActualQuantity.ToString("#,##0.#0"));
                }
                catch { }
            }
            clsProduct.CommitAndDispose();
        }

        #endregion

        
    }
}
