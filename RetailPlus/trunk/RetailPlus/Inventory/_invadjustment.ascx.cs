using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Inventory
{
    public partial class __InvAdjusment : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void cboBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
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
            if (cboProductCode.SelectedItem.Text == "No product" || cboProductCode.SelectedItem.Text == "No Product; Enter product to search.")
            {
                lblVariationMatrix.Visible = false;
                lnkVariationMatrixAdd.Visible = false;
                lstVariationMatrix.Visible = false;
                lblUnitName.Visible = false;
                txtQuantityBefore.Visible = false;
                txtDifference.Visible = false;
                txtQuantityNow.Visible = false;
                txtMinThreshold.Visible = false;
                txtMaxThreshold.Visible = false;
                imgProductHistory.Visible = false;
                imgProductPriceHistory.Visible = false;
                imgChangePrice.Visible = false;
                imgEditNow.Visible = false;
            }
            else
            {
                lblVariationMatrix.Visible = true;
                lnkVariationMatrixAdd.Visible = true;
                lnkVariationMatrixAdd.ToolTip = "Add new variation matrix for " + cboProductCode.SelectedItem.Text;
                lnkVariationMatrixAdd.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("add", Session.SessionID) + "&prodid=" + Common.Encrypt(cboProductCode.SelectedValue, Session.SessionID);
                lblUnitName.Visible = true;
                txtQuantityBefore.Visible = true;
                txtDifference.Visible = true;
                txtQuantityNow.Visible = true;
                txtMinThreshold.Visible = true;
                txtMaxThreshold.Visible = true;
                imgProductHistory.Visible = true;
                imgProductPriceHistory.Visible = true;
                imgChangePrice.Visible = true;
                imgEditNow.Visible = true;

                Products clsProduct = new Products();
                ProductDetails clsProductDetails = clsProduct.Details1(int.Parse(cboBranch.SelectedItem.Value), Convert.ToInt64(cboProductCode.SelectedValue));

                txtProductCode.Text = cboProductCode.SelectedItem.Text;
                lblProductDesc.Text = clsProductDetails.ProductDesc;
                lblUnitName.ToolTip = clsProductDetails.BaseUnitID.ToString();
                lblUnitName.Text = clsProductDetails.BaseUnitCode;
                txtQuantityBefore.Text = clsProductDetails.Quantity.ToString("#,##0.#0");
                txtDifference.Text = "0";
                txtQuantityNow.Text = txtQuantityBefore.Text;
                txtMinThreshold.ToolTip = clsProductDetails.MinThreshold.ToString("#,##0.#0");
                txtMinThreshold.Text = clsProductDetails.MinThreshold.ToString("#,##0.#0");
                txtMaxThreshold.ToolTip = clsProductDetails.MaxThreshold.ToString("#,##0.#0");
                txtMaxThreshold.Text = clsProductDetails.MaxThreshold.ToString("#,##0.#0");

                ProductInventories clsProductInventory = new ProductInventories(clsProduct.Connection, clsProduct.Transaction);
                lstVariationMatrix.DataSource = clsProductInventory.ListAsDataTable(int.Parse(cboBranch.SelectedItem.Value), Convert.ToInt64(cboProductCode.SelectedValue)).DefaultView;
                lstVariationMatrix.DataBind();

                clsProduct.CommitAndDispose();

                lstVariationMatrix.Visible = true;
                txtDifference.Enabled = false;
                txtQuantityNow.Enabled = false;
                txtMinThreshold.Enabled = false;
                txtMaxThreshold.Enabled = false;
            }
        }
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataClass clsDataClass = new DataClass();

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";

            ProductDetails clsSearchKeys = new ProductDetails();
            clsSearchKeys.ProductCode = txtProductCode.Text;

            cboProductCode.DataSource = clsProduct.ListAsDataTable(clsSearchKeys: clsSearchKeys, limit: 100).DefaultView;
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0)
                cboProductCode.Items.Add(new ListItem("No product", "0"));

            cboProductCode.SelectedIndex = 0;

            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void lstVariationMatrix_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.Item.FindControl("chkMatrixID");
                chkMatrixID.Value = dr["MatrixID"].ToString();

                Label lblUnitCode = (Label)e.Item.FindControl("lblUnitCode");
                lblUnitCode.Text = dr["UnitCode"].ToString();
                lblUnitCode.ToolTip = dr["UnitID"].ToString();

                Label lblVariationDesc = (Label)e.Item.FindControl("lblVariationDesc");
                lblVariationDesc.Text = chkMatrixID.Value == Constants.ZERO_STRING ? "MAIN product " : dr["MatrixDescription"].ToString();
                //lblVariationDesc.Text = lblVariationDesc.Text.Length > 40 ? lblVariationDesc.Text.Substring(0, 40) : lblVariationDesc.Text;

                TextBox txtQuantityBefore = (TextBox)e.Item.FindControl("txtQuantityBefore");
                txtQuantityBefore.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

                TextBox txtDifference = (TextBox)e.Item.FindControl("txtDifference");
                txtDifference.Text = "0";

                TextBox txtQuantityNow = (TextBox)e.Item.FindControl("txtQuantityNow");
                txtQuantityNow.Text = txtQuantityBefore.Text;

                TextBox txtMinThreshold = (TextBox)e.Item.FindControl("txtMinThreshold");
                txtMinThreshold.Text = Convert.ToDecimal(dr["MinThreshold"].ToString()).ToString("#,##0.#0");
                txtMinThreshold.ToolTip = Convert.ToDecimal(dr["MinThreshold"].ToString()).ToString("#,##0.#0");

                TextBox txtMaxThreshold = (TextBox)e.Item.FindControl("txtMaxThreshold");
                txtMaxThreshold.Text = Convert.ToDecimal(dr["MaxThreshold"].ToString()).ToString("#,##0.#0");
                txtMaxThreshold.ToolTip = Convert.ToDecimal(dr["MaxThreshold"].ToString()).ToString("#,##0.#0");

            }
        }
        protected void imgSaveTwo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void cmdSaveTwo_Click(object sender, EventArgs e)
        {
            SaveRecord();
            cboProductCode_SelectedIndexChanged(null, null);
        }
        protected void imgProductHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) + "&reptype=" + Common.Encrypt("pdf", Session.SessionID) + "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPanelSearchProduct, this.updPanelSearchProduct.GetType(), "openwindow", javaScript, true);

        }
        protected void imgProductPriceHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) + "&reptype=" + Common.Encrypt("pdf", Session.SessionID) +"&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPanelSearchProduct, this.updPanelSearchProduct.GetType(), "openwindow", javaScript, true);
        }
        protected void imgChangePrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) + "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPanelSearchProduct, this.updPanelSearchProduct.GetType(), "openwindow", javaScript, true);
        }
        protected void imgEditNow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPanelSearchProduct, this.updPanelSearchProduct.GetType(), "openwindow", javaScript, true);
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            Branch clsBranch = new Branch();
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            clsBranch.CommitAndDispose();

            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));

            string strproductcode = string.Empty;
            try { strproductcode = Server.UrlDecode(Common.Decrypt(Server.UrlDecode(Request.QueryString["productcode"].ToString()), Session.SessionID)); }
            catch { }

            if (strproductcode == string.Empty)
            {
                cboProductCode.Items.Clear();
                cboProductCode.Items.Add(new ListItem("No Product; Enter product to search.", "0"));
            }
            else
            {
                txtProductCode.Text = strproductcode;
                cmdProductCode_Click(null, null);
            }
		}
		private void SaveRecord()
		{
            Security.AccessUserDetails clsDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
            DateTime dteChangeDate = DateTime.Now;

            Products clsProduct = new Products();
            clsProduct.GetConnection();

            InvAdjustment clsInvAdjustment = new InvAdjustment(clsProduct.Connection, clsProduct.Transaction);
            InvAdjustmentDetails clsInvAdjustmentDetails;

            long lngProductID = long.Parse(cboProductCode.SelectedValue);

            if (lstVariationMatrix.Items.Count > 0)
            {
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
                foreach (DataListItem e in lstVariationMatrix.Items)
                {
                    HtmlInputCheckBox chkMatrixID = (HtmlInputCheckBox)e.FindControl("chkMatrixID");
                    Label lblVariationDesc = (Label)e.FindControl("lblVariationDesc");
                    Label lblUnitNameMatrix = (Label)e.FindControl("lblUnitCode");
                    TextBox txtQuantityBeforeMatrix = (TextBox)e.FindControl("txtQuantityBefore");
                    //TextBox txtDifferenceMatrix = (TextBox)e.FindControl("txtDifference");
                    TextBox txtQuantityNowMatrix = (TextBox)e.FindControl("txtQuantityNow");
                    TextBox txtMinThresholdMatrix = (TextBox)e.FindControl("txtMinThreshold");
                    TextBox txtMaxThresholdMatrix = (TextBox)e.FindControl("txtMaxThreshold");

                    decimal decQuantityBeforeMatrix = decimal.Parse(txtQuantityBeforeMatrix.Text);
                    //decimal decDifferenceMatrix = decimal.Parse(txtDifference.Text);
                    decimal decQuantityNowMatrix = decimal.Parse(txtQuantityNowMatrix.Text);
                    decimal decMinThresholdMatrixBefore = decimal.Parse(txtMinThresholdMatrix.ToolTip);
                    decimal decMinThresholdMatrixNow = decimal.Parse(txtMinThresholdMatrix.Text);
                    decimal decMaxThresholdMatrixBefore = decimal.Parse(txtMaxThresholdMatrix.ToolTip);
                    decimal decMaxThresholdMatrixNow = decimal.Parse(txtMaxThresholdMatrix.Text);

                    if (decQuantityBeforeMatrix != decQuantityNowMatrix || decMinThresholdMatrixBefore != decMinThresholdMatrixNow || decMaxThresholdMatrixBefore != decMaxThresholdMatrixNow)
                    {
                        clsInvAdjustmentDetails = new InvAdjustmentDetails();
                        clsInvAdjustmentDetails.UID = clsDetails.UID;
                        clsInvAdjustmentDetails.InvAdjustmentDate = dteChangeDate;
                        clsInvAdjustmentDetails.ProductID = long.Parse(cboProductCode.SelectedValue);
                        clsInvAdjustmentDetails.ProductCode = cboProductCode.SelectedItem.Text;
                        clsInvAdjustmentDetails.Description = cboProductCode.SelectedItem.Text;
                        clsInvAdjustmentDetails.VariationMatrixID = long.Parse(chkMatrixID.Value);
                        clsInvAdjustmentDetails.MatrixDescription = lblVariationDesc.Text;
                        clsInvAdjustmentDetails.UnitID = int.Parse(lblUnitNameMatrix.ToolTip);
                        clsInvAdjustmentDetails.UnitCode = lblUnitNameMatrix.Text;
                        clsInvAdjustmentDetails.QuantityBefore = decQuantityBeforeMatrix;
                        clsInvAdjustmentDetails.QuantityNow = decQuantityNowMatrix;
                        clsInvAdjustmentDetails.MinThresholdBefore = decMinThresholdMatrixBefore;
                        clsInvAdjustmentDetails.MinThresholdNow = decMinThresholdMatrixNow;
                        clsInvAdjustmentDetails.MaxThresholdBefore = decMaxThresholdMatrixBefore;
                        clsInvAdjustmentDetails.MaxThresholdNow = decMaxThresholdMatrixNow;
                        clsInvAdjustmentDetails.Remarks = txtRemarks.Text;
                        clsInvAdjustment.Insert(clsInvAdjustmentDetails);

                        if (decQuantityBeforeMatrix > decQuantityNowMatrix)
                        { clsProduct.UpdateInvDetails(int.Parse(cboBranch.SelectedItem.Value), lngProductID, long.Parse(chkMatrixID.Value), decQuantityNowMatrix, decMinThresholdMatrixNow, decMaxThresholdMatrixNow, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_INVENTORY_ADJUSTMENT), dteChangeDate, "SYS-ADJ" + dteChangeDate.ToString("yyyyMMddHHmmss"), clsDetails.Name); }
                        else if (decQuantityBeforeMatrix < decQuantityNowMatrix)
                        { clsProduct.UpdateInvDetails(int.Parse(cboBranch.SelectedItem.Value), lngProductID, long.Parse(chkMatrixID.Value), decQuantityNowMatrix, decMinThresholdMatrixNow, decMaxThresholdMatrixNow, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_INVENTORY_ADJUSTMENT), dteChangeDate, "SYS-ADJ" + dteChangeDate.ToString("yyyyMMddHHmmss"), clsDetails.Name); }

                    }
                }
            }
            clsProduct.CommitAndDispose();

		}

		#endregion

    }
}
