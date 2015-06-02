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

    public partial class __InvThreshold : System.Web.UI.UserControl
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

                    ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsBranch.Connection, clsBranch.Transaction);
                    cboProductSubGroup.DataTextField = "ProductSubGroupName";
                    cboProductSubGroup.DataValueField = "ProductSubGroupID";
                    cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(txtProductSubGroup.Text).DefaultView;
                    cboProductSubGroup.DataBind();
                    cboProductSubGroup.SelectedIndex = 0;

                    clsBranch.CommitAndDispose();

					ManageSecurity();
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

        protected void imgProductSubGroupSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ProductSubGroup clsProductSubGroup = new ProductSubGroup();
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(txtProductSubGroup.Text).DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProductSubGroup.Items.Count > 1 && txtProductSubGroup.Text.Trim() != string.Empty) cboProductSubGroup.SelectedIndex = 1; else cboProductSubGroup.SelectedIndex = 0;
            clsProductSubGroup.CommitAndDispose();
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

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				Label lnkProductCode = (Label) e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr["ProductCode"].ToString();

                Label lnkVariationDesc = (Label)e.Item.FindControl("lnkVariationDesc");
                lnkVariationDesc.Text = dr["MatrixDescription"].ToString();

                TextBox txtMinThreshold = (TextBox)e.Item.FindControl("txtMinThreshold");
                txtMinThreshold.Text = Convert.ToDecimal(dr["MinThreshold"]).ToString("#,##0.##");

                TextBox txtMaxThreshold = (TextBox)e.Item.FindControl("txtMaxThreshold");
                txtMaxThreshold.Text = Convert.ToDecimal(dr["MaxThreshold"]).ToString("#,##0.##");

                TextBox txtBranchMinThreshold = (TextBox)e.Item.FindControl("txtBranchMinThreshold");
                txtBranchMinThreshold.Text = Convert.ToDecimal(dr["BranchMinThreshold"]).ToString("#,##0.##");

                TextBox txtBranchMaxThreshold = (TextBox)e.Item.FindControl("txtBranchMaxThreshold");
                txtBranchMaxThreshold.Text = Convert.ToDecimal(dr["BranchMaxThreshold"]).ToString("#,##0.##");

                TextBox txtRIDBranch = (TextBox)e.Item.FindControl("txtRIDBranch");
                txtRIDBranch.Text = Convert.ToDecimal(dr["RIDBranch"]).ToString("#,##0");

                TextBox txtRIDBranchMinThreshold = (TextBox)e.Item.FindControl("txtRIDBranchMinThreshold");
                txtRIDBranchMinThreshold.Text = Convert.ToDecimal(dr["RIDBranchMinThreshold"]).ToString("#,##0.##");

                TextBox txtRIDBranchMaxThreshold = (TextBox)e.Item.FindControl("txtRIDBranchMaxThreshold");
                txtRIDBranchMaxThreshold.Text = Convert.ToDecimal(dr["RIDBranchMaxThreshold"]).ToString("#,##0.##");

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
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) +
                                "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
            
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
                case "imgSaveThresholds":
                    {
                        TextBox txtBranchMinThreshold = (TextBox)e.Item.FindControl("txtBranchMinThreshold");
                        TextBox txtBranchMaxThreshold = (TextBox)e.Item.FindControl("txtBranchMaxThreshold");
                        TextBox txtRIDBranch = (TextBox)e.Item.FindControl("txtRIDBranch");
                        TextBox txtRIDBranchMinThreshold = (TextBox)e.Item.FindControl("txtRIDBranchMinThreshold");
                        TextBox txtRIDBranchMaxThreshold = (TextBox)e.Item.FindControl("txtRIDBranchMaxThreshold");

                        try { decimal.Parse(txtBranchMinThreshold.Text); }
                        catch 
                        {
                            string stScript = "<Script>";
                            stScript += "window.alert('Please enter a VALID Mininum Threshold.')";
                            stScript += "</Script>";
                            Response.Write(stScript);
                            break;
                        }
                        try { decimal.Parse(txtBranchMaxThreshold.Text); }
                        catch
                        {
                            string stScript = "<Script>";
                            stScript += "window.alert('Please enter a VALID Maximum Threshold.')";
                            stScript += "</Script>";
                            Response.Write(stScript);
                            break;
                        }
                        try { Int32.Parse(txtRIDBranch.Text); }
                        catch
                        {
                            string stScript = "<Script>";
                            stScript += "window.alert('Please enter a VALID RID.')";
                            stScript += "</Script>";
                            Response.Write(stScript);
                            break;
                        }
                        ProductInventories clsProductInventories = new ProductInventories();
                        clsProductInventories.UpdateThresholds(Int32.Parse(cboBranch.SelectedItem.Value), Int64.Parse(chkList.Value), Int64.Parse(chkMatrixID.Value), 
                                                                    decimal.Parse(txtBranchMinThreshold.Text), decimal.Parse(txtBranchMaxThreshold.Text), Int32.Parse(txtRIDBranch.Text),
                                                                    decimal.Parse(txtRIDBranchMinThreshold.Text), decimal.Parse(txtRIDBranchMaxThreshold.Text));
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
        protected void imgSaveThresholds_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveThresholds();
        }
        protected void cmdSaveThresholds_Click(object sender, EventArgs e)
        {
            SaveThresholds();
        }
        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }

        #endregion

        #region Private methods

        private void PrintClosingInventorySheet()
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&type=" + Common.Encrypt("invcount", Session.SessionID) + "&prdgrpid=" + Common.Encrypt(cboProductSubGroup.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openwindow", javaScript, true);
        }

        private void PrintClosingInventory(string strRefNo = "")
        {
            string stParam = "?task=" + Common.Encrypt("closinginventoryrep", Session.SessionID) + "&refno=" + Common.Encrypt(strRefNo, Session.SessionID) + "&prdsubgrpid=" + Common.Encrypt(cboProductSubGroup.SelectedItem.Value, Session.SessionID) + "&branchid=" + Common.Encrypt(cboBranch.SelectedItem.Value, Session.SessionID); ;
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "openwindow", javaScript, true);
        }

        private void ManageSecurity()
        {
            Int64 UID = Convert.ToInt64(Session["UID"]);
            //AccessRights clsAccessRights = new AccessRights();
            //AccessRightsDetails clsDetails = new AccessRightsDetails();

            //clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CloseInventory);
            //imgZeroOutActualQuantity.Visible = clsDetails.Write;
            //cmdZeroOutActualQuantity.Visible = clsDetails.Write;
            //imgCloseInventory.Visible = clsDetails.Write;
            //cmdCloseInventory.Visible = clsDetails.Write;
            //lblSeparator1.Visible = clsDetails.Write;
            ////lblSeparator2.Visible = clsDetails.Write;

            //clsAccessRights.CommitAndDispose();
        }

        private void LoadList()
        {
            string SortField = "ProductCode";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            Int64 lngProductSubGroupID = Convert.ToInt64(cboProductSubGroup.SelectedItem.Value);

            ProductInventories clsProductInventories = new ProductInventories();
            System.Data.DataTable dt = clsProductInventories.ListAsDataTable(BranchID: int.Parse(cboBranch.SelectedItem.Value), ProductSubGroupID: lngProductSubGroupID, clsProductListFilterType: ProductListFilterType.ShowActiveOnly, SortField: "ProductCode ASC, MatrixDescription ASC, BarCode1", SortOrder: SortOption.Desscending);

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

        private void SaveThresholds()
        {
            HtmlInputCheckBox chkMatrixID = null;
            HtmlInputCheckBox chkList = null;
            TextBox txtBranchMinThreshold = null;
            TextBox txtBranchMaxThreshold = null;
            TextBox txtRIDBranch = null;
            TextBox txtRIDBranchMinThreshold = null;
            TextBox txtRIDBranchMaxThreshold = null;
            int intBranchID = int.Parse(cboBranch.SelectedItem.Value);

            ProductInventories clsProductInventories = new ProductInventories();
            foreach (DataListItem item in lstItem.Items)
            {
                txtBranchMinThreshold = (TextBox)item.FindControl("txtBranchMinThreshold");
                txtBranchMaxThreshold = (TextBox)item.FindControl("txtBranchMaxThreshold");
                txtRIDBranch = (TextBox)item.FindControl("txtRIDBranch");
                txtRIDBranchMinThreshold = (TextBox)item.FindControl("txtRIDBranchMinThreshold");
                txtRIDBranchMaxThreshold = (TextBox)item.FindControl("txtRIDBranchMaxThreshold");

                chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                chkMatrixID = (HtmlInputCheckBox)item.FindControl("chkMatrixID");
                try
                {
                    clsProductInventories.UpdateThresholds(Int32.Parse(cboBranch.SelectedItem.Value), Int64.Parse(chkList.Value), Int64.Parse(chkMatrixID.Value),
                                                                decimal.Parse(txtBranchMinThreshold.Text), decimal.Parse(txtBranchMaxThreshold.Text), Int32.Parse(txtRIDBranch.Text),
                                                                decimal.Parse(txtRIDBranchMinThreshold.Text), decimal.Parse(txtRIDBranchMaxThreshold.Text));

                    //decimal decActualQuantity = decimal.Parse(txtActualQuantity.Text);
                    //clsProductInventories.UpdateActualQuantity(intBranchID, long.Parse(chkList.Value), long.Parse(chkMatrixID.Value), decActualQuantity);
                    //txtActualQuantity.Text = Server.HtmlEncode(decActualQuantity.ToString("#,##0.#0"));
                }
                catch { }
            }
            clsProductInventories.CommitAndDispose();
            LoadList();
        }

        #endregion
        
    }
}
