namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
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
                chkList.Value = dr["POItemID"].ToString();

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
            PrintPO();
        }

        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintPO();
        }

        protected void cmdCancelPO_Click(object sender, System.EventArgs e)
        {
            CancelPO();
        }

        protected void imgCancelPO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CancelPO();
        }

		#endregion

		#region Private Methods

        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["poid"], Session.SessionID));
            PO clsPO = new PO();
            PODetails clsDetails = clsPO.Details(iID);
            clsPO.CommitAndDispose();

            lblPOID.Text = clsDetails.POID.ToString();
            lblPONo.Text = clsDetails.PONo;
            ///lblPONo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(clsDetails.POID.ToString(), Session.SessionID);

            lblPODate.Text = clsDetails.PODate.ToString("yyyy-MM-dd HH:mm:ss");
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
            lblPORemarks.Text = clsDetails.Remarks;

            txtPODiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboPODiscountType.SelectedIndex = cboPODiscountType.Items.IndexOf(cboPODiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblPODiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboPODiscount2Type.SelectedIndex = cboPODiscount2Type.Items.IndexOf(cboPODiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblPODiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtPODiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboPODiscount3Type.SelectedIndex = cboPODiscount3Type.Items.IndexOf(cboPODiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblPODiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblPOVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtPOFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtPODeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblPOVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
            chkIsVatInclusive.Checked = clsDetails.IsVatInclusive;
            if (clsDetails.IsVatInclusive)
            {
                lblPOSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
                lblPOTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
            }
            else
            {
                lblPOSubTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
                lblPOTotal.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.VAT).ToString("#,##0.#0");
            }
        }

		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			POItem clsPOItem = new POItem();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsPOItem.List(Convert.ToInt64(lblPOID.Text), "POItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsPOItem.CommitAndDispose();
		}

        private void PrintPO()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("po", Session.SessionID) + "&poid=" + Common.Encrypt(lblPOID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }

        private void CancelPO()
        {
            long POID = Convert.ToInt64(lblPOID.Text);
            string Remarks = txtRemarks.Text;

            PO clsPO = new PO();
            clsPO.Cancel(POID, DateTime.Now, Remarks, Convert.ToInt64(Session["UID"].ToString()));
            clsPO.CommitAndDispose();

            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

		#endregion
        
    }
}
