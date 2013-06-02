namespace AceSoft.RetailPlus.PurchasesAndPayables._DebitMemo
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Cancel : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadRecord();
			    LoadItems();
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
			Common Common = new Common();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
        protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));		
		}
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Print();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            Print();
        }
        protected void imgCancelDebitMemo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CancelDebitMemo();
        }
        protected void cmdCancelDebitMemo_Click(object sender, System.EventArgs e)
        {
            CancelDebitMemo();
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

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

		#endregion

		#region Private Methods

		private void LoadRecord()
		{
            Common Common = new Common();
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["memoid"], Session.SessionID));
            DebitMemos clsDebitMemos = new DebitMemos();
            DebitMemoDetails clsDetails = clsDebitMemos.Details(iID);
            clsDebitMemos.CommitAndDispose();

            lblDebitMemoID.Text = clsDetails.DebitMemoID.ToString();
            lblMemoNo.Text = clsDetails.MemoNo;
            lblMemoDate.Text = clsDetails.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredPostingDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
            lblSupplierID.Text = clsDetails.SupplierID.ToString();

            lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(), Session.SessionID);
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
		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			DebitMemoItems clsDebitMemoItems = new DebitMemoItems();
			lstItem.DataSource = clsDebitMemoItems.ListAsDataTable(Convert.ToInt64(lblDebitMemoID.Text)).DefaultView;
			lstItem.DataBind();
			clsDebitMemoItems.CommitAndDispose();
		}
        private void Print()
        {
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("debitmemo", Session.SessionID) + "&memoid=" + Common.Encrypt(lblDebitMemoID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        private void CancelDebitMemo()
        {
            long DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
            string Remarks = txtRemarks.Text;

            DebitMemos clsDebitMemos = new DebitMemos();
            clsDebitMemos.Cancel(DebitMemoID, DateTime.Now, Remarks, Convert.ToInt64(Session["UID"].ToString()));
            clsDebitMemos.CommitAndDispose();

            Common Common = new Common();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
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
