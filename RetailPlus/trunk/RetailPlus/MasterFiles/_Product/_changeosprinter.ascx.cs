using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Security;
using Microsoft.Office.Interop;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using Microsoft.Office.Interop;

    public partial class __ChangeOSPrinter : System.Web.UI.UserControl
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

                    ProductGroup clsProductGroup = new ProductGroup(clsBranch.Connection, clsBranch.Transaction);
                    cboProductGroup.DataTextField = "ProductGroupName";
                    cboProductGroup.DataValueField = "ProductGroupID";
                    cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroup.Text).DefaultView;
                    cboProductGroup.DataBind();
                    cboProductGroup.SelectedIndex = 0;

                    clsBranch.CommitAndDispose();

					LoadList();
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

        protected void imgProductGroupSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ProductGroup clsProductGroup = new ProductGroup();
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroup.Text).DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProductGroup.Items.Count > 1 && txtProductGroup.Text.Trim() != string.Empty) cboProductGroup.SelectedIndex = 1; else cboProductGroup.SelectedIndex = 0;
            clsProductGroup.CommitAndDispose();
        }

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
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

                HyperLink lnkProductSubGroupCode = (HyperLink)e.Item.FindControl("lnkProductSubGroupCode");
                lnkProductSubGroupCode.Text = dr["ProductSubGroupCode"].ToString();
                lnkProductSubGroupCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_ProductSubGroup/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductSubGroupID"].ToString(), Session.SessionID);

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr["ProductDesc"].ToString();
                lnkProductCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                CheckBox chkOrderSlipPrinter1 = (CheckBox)e.Item.FindControl("chkOrderSlipPrinter1");
                chkOrderSlipPrinter1.Checked = bool.Parse(dr["OrderSlipPrinter1"].ToString());

                CheckBox chkOrderSlipPrinter2 = (CheckBox)e.Item.FindControl("chkOrderSlipPrinter2");
                chkOrderSlipPrinter2.Checked = bool.Parse(dr["OrderSlipPrinter2"].ToString());

                CheckBox chkOrderSlipPrinter3 = (CheckBox)e.Item.FindControl("chkOrderSlipPrinter3");
                chkOrderSlipPrinter3.Checked = bool.Parse(dr["OrderSlipPrinter3"].ToString());

                CheckBox chkOrderSlipPrinter4 = (CheckBox)e.Item.FindControl("chkOrderSlipPrinter4");
                chkOrderSlipPrinter4.Checked = bool.Parse(dr["OrderSlipPrinter4"].ToString());

                CheckBox chkOrderSlipPrinter5 = (CheckBox)e.Item.FindControl("chkOrderSlipPrinter5");
                chkOrderSlipPrinter5.Checked = bool.Parse(dr["OrderSlipPrinter5"].ToString());
			}
		}				
		protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) +
                                "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
            
			switch(e.CommandName)
			{
                case "imgSaveOrderSlipPrinter":
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
        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        protected void chkOrderSlipPrinterAll_CheckedChanged(Object sender, EventArgs e)
        {
            HtmlInputCheckBox chkList = null;
            CheckBox chkOrderSlipPrinterAll = (CheckBox)sender;
            CheckBox chkOrderSlipPrinter = null;
            Int64 iProductID = 0;

            Products clsProducts = new Products();
            OrderSlipPrinter orderSlipPrinter = OrderSlipPrinter.NotApplicable;
            foreach (DataListItem item in lstItem.Items)
            {
                chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                
                iProductID = Int64.Parse(chkList.Value);

                switch (chkOrderSlipPrinterAll.ID)
                {
                    case "chkOrderSlipPrinter1All": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter1; chkOrderSlipPrinter = (CheckBox)item.FindControl("chkOrderSlipPrinter1"); break;
                    case "chkOrderSlipPrinter2All": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter2; chkOrderSlipPrinter = (CheckBox)item.FindControl("chkOrderSlipPrinter2"); break;
                    case "chkOrderSlipPrinter3All": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter3; chkOrderSlipPrinter = (CheckBox)item.FindControl("chkOrderSlipPrinter3"); break;
                    case "chkOrderSlipPrinter4All": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter4; chkOrderSlipPrinter = (CheckBox)item.FindControl("chkOrderSlipPrinter4"); break;
                    case "chkOrderSlipPrinter5All": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter5; chkOrderSlipPrinter = (CheckBox)item.FindControl("chkOrderSlipPrinter5"); break;
                }
                clsProducts.UpdateOrderSlipPrinter(iProductID, orderSlipPrinter, chkOrderSlipPrinterAll.Checked);
                chkOrderSlipPrinter.Checked = chkOrderSlipPrinterAll.Checked;
            }
            clsProducts.CommitAndDispose();
        }

        protected void chkOrderSlipPrinter_CheckedChanged(Object sender, EventArgs e)
        {
            CheckBox chkOrderSlipPrinter = (CheckBox)sender;
            DataListItem item = (DataListItem)  chkOrderSlipPrinter.NamingContainer;
            
            HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
            Int64 iProductID = Int64.Parse(chkList.Value);

            OrderSlipPrinter orderSlipPrinter = OrderSlipPrinter.NotApplicable;
            switch (chkOrderSlipPrinter.ID)
            {
                case "chkOrderSlipPrinter1": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter1; break;
                case "chkOrderSlipPrinter2": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter2; break;
                case "chkOrderSlipPrinter3": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter3; break;
                case "chkOrderSlipPrinter4": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter4; break;
                case "chkOrderSlipPrinter5": orderSlipPrinter = OrderSlipPrinter.RetailPlusOSPrinter5; break;
            }

            Products clsProducts = new Products();
            clsProducts.UpdateOrderSlipPrinter(iProductID, orderSlipPrinter, chkOrderSlipPrinter.Checked);
            clsProducts.CommitAndDispose();
        }

        #endregion

        #region Private methods

        private void PrintClosingInventorySheet()
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&type=" + Common.Encrypt("invcount", Session.SessionID) + "&prdgrpid=" + Common.Encrypt(cboProductGroup.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID) + "&typedet=" + Common.Encrypt("byprod", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openwindow", javaScript, true);
        }

        private void PrintClosingInventory(string strRefNo = "")
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&refno=" + Common.Encrypt(strRefNo, Session.SessionID) + "&prdgrpid=" + Common.Encrypt(cboProductGroup.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID) + "&typedet=" + Common.Encrypt("byprod", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updCloseInventory, this.updCloseInventory.GetType(), "openwindow", javaScript, true);
        }

        private void LoadList()
        {
            string SortField = "ProductCode";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            Int64 lngProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);

            ProductInventories clsProductInventories = new ProductInventories();
            System.Data.DataTable dt = clsProductInventories.ListAsDataTable(BranchID: int.Parse(cboBranch.SelectedItem.Value), ProductGroupID: lngProductGroupID, clsProductListFilterType: ProductListFilterType.ShowActiveOnly, SortField: "ProductSubGroupCode ASC, ProductDesc ASC, BarCode1 ", SortOrder: SortOption.Ascending);

            clsProductInventories.CommitAndDispose();

            PageData.DataSource = dt.DefaultView;

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


        #endregion
        
    }
}
