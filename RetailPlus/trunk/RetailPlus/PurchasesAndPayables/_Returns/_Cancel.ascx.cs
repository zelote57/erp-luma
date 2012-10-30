namespace AceSoft.RetailPlus.PurchasesAndPayables._Returns
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
				lblReferrer.Text = Request.UrlReferrer.ToString();
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
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["retid"], Session.SessionID));
            POReturns clsPOReturns = new POReturns();
            POReturnDetails clsDetails = clsPOReturns.Details(iID);
            clsPOReturns.CommitAndDispose();

            lblDebitMemoID.Text = clsDetails.DebitMemoID.ToString();
            lblReturnNo.Text = clsDetails.MemoNo;
            lblReturnDate.Text = clsDetails.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredReturnDate.Text = clsDetails.RequiredPostingDate.ToString("yyyy-MM-dd");
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
            lblReturnRemarks.Text = clsDetails.Remarks;

            UpdateFooter(clsDetails);
        }

		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			POReturnItems clsPOReturnItems = new POReturnItems();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsPOReturnItems.List(Convert.ToInt64(lblDebitMemoID.Text), "DebitMemoItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsPOReturnItems.CommitAndDispose();
		}


		#endregion

		#region Print Report

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			PrintPOReturn();
		}

		protected void cmdPrint_Click(object sender, System.EventArgs e)
		{
			PrintPOReturn();
		}

		private void PrintPOReturn()
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("reports",Session.SessionID) + "&target=" + Common.Encrypt("poretun",Session.SessionID) + "&retid=" + Common.Encrypt(lblDebitMemoID.Text,Session.SessionID);	
			Response.Redirect("Default.aspx" + stParam);
		}

		#endregion

		protected void imgCancelPO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			CancelPOReturn();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		protected void cmdCancelPO_Click(object sender, System.EventArgs e)
		{
			CancelPOReturn();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		private void CancelPOReturn()
		{
			long DebitMemoID = Convert.ToInt64(lblDebitMemoID.Text);
			string Remarks = txtRemarks.Text;

            POReturns clsPOReturns = new POReturns();
            clsPOReturns.Cancel(DebitMemoID, DateTime.Now, Remarks, Convert.ToInt64(Session["UID"].ToString()));
            clsPOReturns.CommitAndDispose();

			Common Common = new Common();
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));		
		}


        protected void imgCancelPOReturn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CancelPOReturn();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

        private void UpdateFooter(POReturnDetails clsPOReturnDetails)
        {
            lblPODiscount.Text = clsPOReturnDetails.Discount.ToString("#,##0.#0");
            lblPOVatableAmount.Text = clsPOReturnDetails.VatableAmount.ToString("#,##0.#0");
            txtPOFreight.Text = clsPOReturnDetails.Freight.ToString("#,##0.#0");
            txtPODeposit.Text = clsPOReturnDetails.Deposit.ToString("#,##0.#0");
            lblPOSubTotal.Text = Convert.ToDecimal(clsPOReturnDetails.SubTotal - clsPOReturnDetails.VAT).ToString("#,##0.#0");
            lblPOVAT.Text = clsPOReturnDetails.VAT.ToString("#,##0.#0");
            lblPOTotal.Text = clsPOReturnDetails.SubTotal.ToString("#,##0.#0");
        }
    }
}
