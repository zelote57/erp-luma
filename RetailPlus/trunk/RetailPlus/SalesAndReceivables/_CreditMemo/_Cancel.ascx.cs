namespace AceSoft.RetailPlus.SalesAndReceivables._CreditMemo
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
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);
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
		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["CreditMemoItemID"].ToString();

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
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["retid"], Session.SessionID));
            CreditMemos clsCreditMemos = new CreditMemos();
            CreditMemoDetails clsDetails = clsCreditMemos.Details(iID);
            clsCreditMemos.CommitAndDispose();

            lblCreditMemoID.Text = clsDetails.CreditMemoID.ToString();
            lblReturnNo.Text = clsDetails.CNNo;
            lblReturnDate.Text = clsDetails.CNDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredReturnDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
            lblCustomerID.Text = clsDetails.CustomerID.ToString();

            lblCustomerCode.Text = clsDetails.CustomerCode.ToString();
            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.CustomerID.ToString(), Session.SessionID);
            lblCustomerCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Customer/Default.aspx" + stParam;

            lblCustomerContact.Text = clsDetails.CustomerContact;
            lblCustomerTelephoneNo.Text = clsDetails.CustomerTelephoneNo;
            lblTerms.Text = clsDetails.CustomerTerms.ToString("##0");
            switch (clsDetails.CustomerModeOfTerms)
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
            lblCustomerAddress.Text = clsDetails.CustomerAddress;
            lblBranchID.Text = clsDetails.BranchID.ToString();
            lblBranchCode.Text = clsDetails.BranchCode;
            lblBranchAddress.Text = clsDetails.BranchAddress;
            lblReturnRemarks.Text = clsDetails.Remarks;

            UpdateFooter(clsDetails);
        }

		private void LoadItems()
		{
			CreditMemoItems clsCreditMemoItems = new CreditMemoItems();
			lstItem.DataSource = clsCreditMemoItems.ListAsDataTable(Convert.ToInt64(lblCreditMemoID.Text)).DefaultView;
			lstItem.DataBind();
			clsCreditMemoItems.CommitAndDispose();
		}


		#endregion

		#region Print Report

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			PrintCreditMemo();
		}

		protected void cmdPrint_Click(object sender, System.EventArgs e)
		{
			PrintCreditMemo();
		}

		private void PrintCreditMemo()
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("reports",Session.SessionID) + "&target=" + Common.Encrypt("poretun",Session.SessionID) + "&retid=" + Common.Encrypt(lblCreditMemoID.Text,Session.SessionID);	
			Response.Redirect("Default.aspx" + stParam);
		}

		#endregion

		protected void imgCancelSO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			CancelCreditMemo();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		protected void cmdCancelSO_Click(object sender, System.EventArgs e)
		{
			CancelCreditMemo();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		private void CancelCreditMemo()
		{
			long CreditMemoID = Convert.ToInt64(lblCreditMemoID.Text);
			string Remarks = txtRemarks.Text;

            CreditMemos clsCreditMemos = new CreditMemos();
            clsCreditMemos.Cancel(CreditMemoID, DateTime.Now, Remarks, Convert.ToInt64(Session["UID"].ToString()));
            clsCreditMemos.CommitAndDispose();

			Common Common = new Common();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));		
		}


        protected void imgCancelCreditMemo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CancelCreditMemo();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

        private void UpdateFooter(CreditMemoDetails clsCreditMemoDetails)
        {
            lblSODiscount.Text = clsCreditMemoDetails.Discount.ToString("#,##0.#0");
            lblSOVatableAmount.Text = clsCreditMemoDetails.VatableAmount.ToString("#,##0.#0");
            txtSOFreight.Text = clsCreditMemoDetails.Freight.ToString("#,##0.#0");
            txtSODeposit.Text = clsCreditMemoDetails.Deposit.ToString("#,##0.#0");
            lblSOSubTotal.Text = Convert.ToDecimal(clsCreditMemoDetails.SubTotal - clsCreditMemoDetails.VAT).ToString("#,##0.#0");
            lblSOVAT.Text = clsCreditMemoDetails.VAT.ToString("#,##0.#0");
            lblSOTotal.Text = clsCreditMemoDetails.SubTotal.ToString("#,##0.#0");
        }
    }
}
