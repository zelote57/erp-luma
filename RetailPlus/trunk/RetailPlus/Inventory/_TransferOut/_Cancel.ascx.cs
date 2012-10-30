namespace AceSoft.RetailPlus.Inventory._TransferOut
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
        
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["TransferOutItemID"].ToString();

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
        
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferOut();
        }

        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintTransferOut();
        }

        protected void cmdCancelTransferOut_Click(object sender, System.EventArgs e)
        {
            CancelTransferOut();
        }

        protected void imgCancelTransferOut_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CancelTransferOut();
        }

		#endregion

		#region Private Methods

        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["transferoutid"], Session.SessionID));
            TransferOut clsTransferOut = new TransferOut();
            TransferOutDetails clsDetails = clsTransferOut.Details(iID);
            clsTransferOut.CommitAndDispose();

            lblTransferOutID.Text = clsDetails.TransferOutID.ToString();
            lblTransferOutNo.Text = clsDetails.TransferOutNo;
            lblTransferOutDate.Text = clsDetails.TransferOutDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            lblSupplierID.Text = clsDetails.SupplierID.ToString();

            lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(), Session.SessionID);
            lblSupplierCode.NavigateUrl = "../_Vendor/Default.aspx" + stParam;

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
            lblTransferOutRemarks.Text = clsDetails.Remarks;

            txtTransferOutDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboTransferOutDiscountType.SelectedIndex = cboTransferOutDiscountType.Items.IndexOf(cboTransferOutDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblTransferOutDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTransferOutVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferOutFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtTransferOutDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblTransferOutSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT + clsDetails.Freight - clsDetails.Deposit).ToString("#,##0.#0");
            lblTransferOutVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            lblTransferOutTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
        }

		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			TransferOutItem clsTransferOutItem = new TransferOutItem();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsTransferOutItem.List(Convert.ToInt64(lblTransferOutID.Text), "TransferOutItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsTransferOutItem.CommitAndDispose();
		}

        private void PrintTransferOut()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("transferout", Session.SessionID) + "&transferoutid=" + Common.Encrypt(lblTransferOutID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }

        private void CancelTransferOut()
        {
            long TransferOutID = Convert.ToInt64(lblTransferOutID.Text);
            string Remarks = txtRemarks.Text;

            TransferOut clsTransferOut = new TransferOut();
            clsTransferOut.Cancel(TransferOutID, DateTime.Now, Remarks, Convert.ToInt64(Session["UID"].ToString()));
            clsTransferOut.CommitAndDispose();

            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

		#endregion
        
    }
}
