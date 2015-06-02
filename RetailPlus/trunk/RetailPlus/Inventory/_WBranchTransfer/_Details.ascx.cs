namespace AceSoft.RetailPlus.Inventory._WBranchTransfer
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
    using System.IO;
    using System.Xml;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadRecord();	
					LoadItems();
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

		protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
        protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));		
		}
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintWBranchTransfer();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintWBranchTransfer();
        }
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["WBranchTransferItemID"].ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr["Barcode"].ToString();

                HyperLink lnkDescription = (HyperLink)e.Item.FindControl("lnkDescription");
                lnkDescription.Text = dr["Description"].ToString();
                lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

                HyperLink lnkMatrixDescription = (HyperLink)e.Item.FindControl("lnkMatrixDescription");
                if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null)
                {
                    lnkMatrixDescription.Visible = true;
                    lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
                    lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
                }

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

                Label lblProductUnitID = (Label)e.Item.FindControl("lblProductUnitID");
                lblProductUnitID.Text = dr["ProductUnitID"].ToString();

                Label lblProductUnitCode = (Label)e.Item.FindControl("lblProductUnitCode");
                lblProductUnitCode.Text = dr["ProductUnitCode"].ToString();

                //Label lblDiscountApplied = (Label)e.Item.FindControl("lblDiscountApplied");
                //lblDiscountApplied.Text = Convert.ToDecimal(dr["DiscountApplied"].ToString()).ToString("#,##0.#0");

                //DiscountTypes DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                //if (DiscountType == DiscountTypes.Percentage)
                //{
                //    Label lblPercent = (Label)e.Item.FindControl("lblPercent");
                //    lblPercent.Visible = true;
                //}

                //Label lblAmount = (Label) e.Item.FindControl("lblAmount");
                //lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

                //Label lblVAT = (Label) e.Item.FindControl("lblVAT");
                //lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0");

                //Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
                //lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0");

                //Label lblisVATInclusive = (Label)e.Item.FindControl("lblisVATInclusive");
                //lblisVATInclusive.Text = Convert.ToBoolean(Convert.ToInt16(dr["isVATInclusive"].ToString())).ToString();

                //Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                //lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                //For anchor
                HtmlGenericControl divExpCollAsst = (HtmlGenericControl)e.Item.FindControl("divExpCollAsst");

                HtmlAnchor anchorDown = (HtmlAnchor)e.Item.FindControl("anchorDown");
                anchorDown.HRef = "javascript:ToggleDiv('" + divExpCollAsst.ClientID + "')";
            }
        }

		#endregion

		#region Private Methods

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["WBranchTransferID"],Session.SessionID));
			WBranchTransfer clsWBranchTransfer = new WBranchTransfer();
			WBranchTransferDetails clsDetails = clsWBranchTransfer.Details(iID);
			clsWBranchTransfer.CommitAndDispose();

            lblWBranchTransferID.Text = clsDetails.WBranchTransferID.ToString();
            lblWBranchTransferNo.Text = clsDetails.WBranchTransferNo;
            lblWBranchTransferDate.Text = clsDetails.WBranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            lblBranchCodeFrom.Text = clsDetails.BranchCodeFrom;
            lblBranchCodeTo.Text = clsDetails.BranchCodeTo;
            lblWBranchTransferRemarks.Text = clsDetails.Remarks;

            txtWBranchTransferDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboWBranchTransferDiscountType.SelectedIndex = cboWBranchTransferDiscountType.Items.IndexOf(cboWBranchTransferDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblWBranchTransferDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblWBranchTransferVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtWBranchTransferFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtWBranchTransferDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblWBranchTransferSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT + clsDetails.Freight - clsDetails.Deposit).ToString("#,##0.#0");
            lblWBranchTransferVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblWBranchTransferTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
		}
		private void LoadItems()
		{
			WBranchTransferItem clsWBranchTransferItem = new WBranchTransferItem();
			lstItem.DataSource = clsWBranchTransferItem.ListAsDataTable(Convert.ToInt64(lblWBranchTransferID.Text), "WBranchTransferItemID",SortOption.Ascending).DefaultView;
			lstItem.DataBind();
			clsWBranchTransferItem.CommitAndDispose();
		}
        private void PrintWBranchTransfer()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("WBranchTransfer", Session.SessionID) + "&WBranchTransferID=" + Common.Encrypt(lblWBranchTransferID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_WBranchTransfer/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }
		#endregion

        
    }
}
