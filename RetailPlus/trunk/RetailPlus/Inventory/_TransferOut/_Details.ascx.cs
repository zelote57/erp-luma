namespace AceSoft.RetailPlus.Inventory._TransferOut
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
    using System.Xml;
    using System.Text;

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
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;

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

                Label lblTransferOutItemReceivedStatus = (Label)e.Item.FindControl("lblTransferOutItemReceivedStatus");
                TransferOutItemReceivedStatus clsTransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), dr["TransferOutItemReceivedStatus"].ToString());
                lblTransferOutItemReceivedStatus.Text = clsTransferOutItemReceivedStatus.ToString("d");

                //For anchor
                HtmlGenericControl divExpCollAsst = (HtmlGenericControl)e.Item.FindControl("divExpCollAsst");

                HtmlAnchor anchorDown = (HtmlAnchor)e.Item.FindControl("anchorDown");
                anchorDown.HRef = "javascript:ToggleDiv('" + divExpCollAsst.ClientID + "')";
			}
		}
        protected void cmdExport_Click(object sender, EventArgs e)
        {
            ExportToFile();
        }
        protected void imgExport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToFile();
        }
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferOut();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintTransferOut();
        }
        protected void imgPrintSelling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintTransferOutSelling();
        }
        protected void cmdPrintSelling_Click(object sender, EventArgs e)
        {
            PrintTransferOutSelling();
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
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);
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
			lblTransferOutRemarks.Text = clsDetails.Remarks;

            txtTransferOutDiscountApplied.Text = clsDetails.DiscountApplied.ToString("###0.#0");
            cboTransferOutDiscountType.SelectedIndex = cboTransferOutDiscountType.Items.IndexOf(cboTransferOutDiscountType.Items.FindByValue(clsDetails.DiscountType.ToString("d")));
            lblTransferOutDiscount.Text = clsDetails.Discount.ToString("#,##0.#0");
            lblTotalDiscount1.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferOutDiscount2Applied.Text = clsDetails.Discount2Applied.ToString("###0.#0");
            cboTransferOutDiscount2Type.SelectedIndex = cboTransferOutDiscount2Type.Items.IndexOf(cboTransferOutDiscount2Type.Items.FindByValue(clsDetails.Discount2Type.ToString("d")));
            lblTransferOutDiscount2.Text = clsDetails.Discount2.ToString("#,##0.#0");
            lblTotalDiscount2.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount2 + clsDetails.Discount3).ToString("#,##0.#0");

            txtTransferOutDiscount3Applied.Text = clsDetails.Discount3Applied.ToString("###0.#0");
            cboTransferOutDiscount3Type.SelectedIndex = cboTransferOutDiscount3Type.Items.IndexOf(cboTransferOutDiscountType.Items.FindByValue(clsDetails.Discount3Type.ToString("d")));
            lblTransferOutDiscount3.Text = clsDetails.Discount3.ToString("#,##0.#0");
            lblTotalDiscount3.Text = Convert.ToDecimal(clsDetails.SubTotal + clsDetails.Discount3).ToString("#,##0.#0");

            lblTransferOutVatableAmount.Text = clsDetails.VatableAmount.ToString("#,##0.#0");
            txtTransferOutFreight.Text = clsDetails.Freight.ToString("#,##0.#0");
            txtTransferOutDeposit.Text = clsDetails.Deposit.ToString("#,##0.#0");
            lblTransferOutSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
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
            lstItemFixCssClass();
		}
        private void lstItemFixCssClass()
        {
            foreach (DataListItem item in lstItem.Items)
            {
                Label lblitemTransferOutItemReceivedStatus = (Label)item.FindControl("lblTransferOutItemReceivedStatus");
                TransferOutItemReceivedStatus itemTransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), lblitemTransferOutItemReceivedStatus.Text);
                if (itemTransferOutItemReceivedStatus == TransferOutItemReceivedStatus.Received)
                    item.CssClass = "ms-item-received-det";
                else if (item.ItemType == ListItemType.Item)
                    item.CssClass = "";
                else if (item.ItemType == ListItemType.AlternatingItem)
                    item.CssClass = "ms-alternating";

            }
        }
        private void PrintTransferOut()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("TransferOutreport", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(lblTransferOutID.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }
        private void PrintTransferOutSelling()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("TransferOutreport", Session.SessionID) + "&TransferOutid=" + Common.Encrypt(lblTransferOutID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("TransferOutReportSellingPrice", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_TransferOut/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrintSellingPrice, this.updPrintSellingPrice.GetType(), "openwindow", javaScript, true);
        }
        private void ExportToFile()
        {

            TransferOut clsTransferOut = new TransferOut();
            TransferOutDetails clsTransferOutDetails = clsTransferOut.Details(long.Parse(lblTransferOutID.Text));

            TransferOutItem clsTransferOutItem = new TransferOutItem(clsTransferOut.Connection, clsTransferOut.Transaction);
            DataTable dtaTransferOutItems = clsTransferOutItem.ListAsDataTable(clsTransferOutDetails.TransferOutID, null, SortOption.Ascending);

            Branch clsBranch = new Branch(clsTransferOut.Connection, clsTransferOut.Transaction);
            BranchDetails clsBranchDetails;

            Contacts clsContact = new Contacts(clsTransferOut.Connection, clsTransferOut.Transaction);
            ContactDetails clsContactDetails;

            Products clsProduct = new Products(clsTransferOut.Connection, clsTransferOut.Transaction);
            ProductDetails clsProductDetails;

            ProductVariations clsProductVariation = new ProductVariations(clsTransferOut.Connection, clsTransferOut.Transaction);
            DataTable dtaProductVariation;

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsTransferOut.Connection, clsTransferOut.Transaction);
            DataTable dtaProductVariationsMatrix;

            string xmlFileName = Server.MapPath(@"\RetailPlus\temp\" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsTransferOutDetails.TransferOutNo + "_" + clsTransferOutDetails.TransferOutDate.ToString("yyyyMMddHHmmssffff") + ".xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteComment("This file represents the TransferOut Details of TransferOut No: '" + clsTransferOutDetails.TransferOutNo + "' for " + lblBranchCode.Text + " branch.");
            writer.WriteComment("Save this in your local file. Goto 'File', click 'Save As', select the location in your local directory, click 'Save'.");
            writer.WriteStartElement("TransferOutDetails");
            writer.WriteAttributeString("TransferOutID", XmlConvert.ToString(clsTransferOutDetails.TransferOutID));
            writer.WriteAttributeString("TransferOutNo", clsTransferOutDetails.TransferOutNo);
            writer.WriteAttributeString("TransferOutDate", clsTransferOutDetails.TransferOutDate.ToString("MM/dd/yyyy HH:mm:ss"));

            /******Supplier information******/
            clsContactDetails = clsContact.Details(clsTransferOutDetails.SupplierID);
            writer.WriteAttributeString("SupplierID", XmlConvert.ToString(clsContactDetails.ContactID));
            writer.WriteAttributeString("SupplierCode", clsContactDetails.ContactCode);
            writer.WriteAttributeString("SupplierName", clsContactDetails.ContactName);
            writer.WriteAttributeString("SupplierContact", clsContactDetails.BusinessName);
            writer.WriteAttributeString("SupplierAddress", clsTransferOutDetails.SupplierAddress);
            writer.WriteAttributeString("SupplierTelephoneNo", clsTransferOutDetails.SupplierTelephoneNo);
            writer.WriteAttributeString("SupplierModeOfTerms", XmlConvert.ToString(clsTransferOutDetails.SupplierModeOfTerms));
            writer.WriteAttributeString("SupplierTerms", XmlConvert.ToString(clsTransferOutDetails.SupplierTerms));
            writer.WriteAttributeString("SupplierContactGroupName", clsContactDetails.ContactGroupName);
            /******End Of Supplier Information******/

            writer.WriteAttributeString("RequiredDeliveryDate", clsTransferOutDetails.RequiredDeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));

            /******Branch & Transferrer Information******/
            clsBranchDetails = clsBranch.Details(short.Parse(clsTransferOutDetails.BranchID.ToString()));
            writer.WriteAttributeString("BranchID", XmlConvert.ToString(clsTransferOutDetails.BranchID));
            writer.WriteAttributeString("BranchCode", clsTransferOutDetails.BranchCode);
            writer.WriteAttributeString("BranchName", clsTransferOutDetails.BranchName);
            writer.WriteAttributeString("BranchAddress", clsTransferOutDetails.BranchAddress);
            writer.WriteAttributeString("BranchDBIP", clsBranchDetails.DBIP);
            writer.WriteAttributeString("BranchDBPort", clsBranchDetails.DBPort);
            writer.WriteAttributeString("BranchRemarks", clsBranchDetails.Remarks);
            writer.WriteAttributeString("TransferrerID", clsTransferOutDetails.TransferrerID.ToString());
            writer.WriteAttributeString("TransferrerName", clsTransferOutDetails.TransferrerName);
            /******End Of Branch & Transferrer Information******/

            /******Amount Information******/
            writer.WriteAttributeString("SubTotal", XmlConvert.ToString(clsTransferOutDetails.SubTotal));
            writer.WriteAttributeString("Discount", XmlConvert.ToString(clsTransferOutDetails.Discount));
            writer.WriteAttributeString("DiscountApplied", XmlConvert.ToString(clsTransferOutDetails.DiscountApplied));
            writer.WriteAttributeString("DiscountType", clsTransferOutDetails.DiscountType.ToString("d"));
            writer.WriteAttributeString("VAT", XmlConvert.ToString(clsTransferOutDetails.VAT));
            writer.WriteAttributeString("VatableAmount", XmlConvert.ToString(clsTransferOutDetails.VatableAmount));
            writer.WriteAttributeString("EVAT", XmlConvert.ToString(clsTransferOutDetails.EVAT));
            writer.WriteAttributeString("EVatableAmount", XmlConvert.ToString(clsTransferOutDetails.EVatableAmount));
            writer.WriteAttributeString("LocalTax", XmlConvert.ToString(clsTransferOutDetails.LocalTax));
            writer.WriteAttributeString("Freight", XmlConvert.ToString(clsTransferOutDetails.Freight));
            writer.WriteAttributeString("Deposit", XmlConvert.ToString(clsTransferOutDetails.Deposit));
            writer.WriteAttributeString("UnpaidAmount", XmlConvert.ToString(clsTransferOutDetails.UnpaidAmount));
            writer.WriteAttributeString("PaidAmount", XmlConvert.ToString(clsTransferOutDetails.PaidAmount));
            writer.WriteAttributeString("TotalItemDiscount", XmlConvert.ToString(clsTransferOutDetails.TotalItemDiscount));
            writer.WriteAttributeString("Status", clsTransferOutDetails.Status.ToString("d"));
            writer.WriteAttributeString("Remarks", clsTransferOutDetails.Remarks);
            writer.WriteAttributeString("SupplierDRNo", clsTransferOutDetails.SupplierDRNo);
            writer.WriteAttributeString("DeliveryDate", clsTransferOutDetails.DeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledDate", clsTransferOutDetails.CancelledDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledRemarks", clsTransferOutDetails.CancelledRemarks);
            writer.WriteAttributeString("CancelledByID", XmlConvert.ToString(clsTransferOutDetails.CancelledByID));
            /******End Of Branch Information******/

            foreach (DataRow row in dtaTransferOutItems.Rows)
            {
                clsProductDetails = new ProductDetails();
                clsProductDetails = clsProduct.Details(Convert.ToInt64(row["ProductID"].ToString()));

                writer.WriteStartElement("TransferOutItem");
                writer.WriteAttributeString("TransferOutItemID", row["TransferOutItemID"].ToString());
                writer.WriteAttributeString("TransferOutID", row["TransferOutID"].ToString());
                writer.WriteAttributeString("ProductID", row["ProductID"].ToString());

                /*****Product Information*****/
                writer.WriteAttributeString("ProductCode", clsProductDetails.ProductCode);
                writer.WriteAttributeString("BarCode", clsProductDetails.BarCode);
                writer.WriteAttributeString("ProductDesc", clsProductDetails.ProductDesc);
                writer.WriteAttributeString("MatrixDescription", row["MatrixDescription"].ToString());
                writer.WriteAttributeString("ProductSubGroupID", clsProductDetails.ProductSubGroupID.ToString());
                writer.WriteAttributeString("ProductSubGroupCode", clsProductDetails.ProductSubGroupCode);
                writer.WriteAttributeString("ProductSubGroupName", clsProductDetails.ProductSubGroupName);
                writer.WriteAttributeString("ProductGroupID", clsProductDetails.ProductGroupID.ToString());
                writer.WriteAttributeString("ProductGroupCode", clsProductDetails.ProductGroupCode);
                writer.WriteAttributeString("ProductGroupName", clsProductDetails.ProductGroupName);
                writer.WriteAttributeString("BaseUnitID", clsProductDetails.BaseUnitID.ToString());
                writer.WriteAttributeString("BaseUnitCode", clsProductDetails.BaseUnitCode);
                writer.WriteAttributeString("BaseUnitName", clsProductDetails.BaseUnitName);
                writer.WriteAttributeString("DateCreated", clsProductDetails.DateCreated.ToString("MM/dd/yy HH:mm:ss"));
                writer.WriteAttributeString("Deleted", clsProductDetails.Deleted.ToString());
                writer.WriteAttributeString("Price", clsProductDetails.Price.ToString());
                writer.WriteAttributeString("PurchasePrice", clsProductDetails.PurchasePrice.ToString());
                writer.WriteAttributeString("IncludeInSubtotalDiscount", clsProductDetails.IncludeInSubtotalDiscount.ToString());
                writer.WriteAttributeString("VAT", clsProductDetails.VAT.ToString());
                writer.WriteAttributeString("EVAT", clsProductDetails.EVAT.ToString());
                writer.WriteAttributeString("LocalTax", clsProductDetails.LocalTax.ToString());
                writer.WriteAttributeString("Quantity", clsProductDetails.Quantity.ToString());
                writer.WriteAttributeString("MinThreshold", clsProductDetails.MinThreshold.ToString());
                writer.WriteAttributeString("MaxThreshold", clsProductDetails.MaxThreshold.ToString());
                writer.WriteAttributeString("ChartOfAccountIDPurchase", clsProductDetails.ChartOfAccountIDPurchase.ToString());
                writer.WriteAttributeString("ChartOfAccountIDSold", clsProductDetails.ChartOfAccountIDSold.ToString());
                writer.WriteAttributeString("ChartOfAccountIDInventory", clsProductDetails.ChartOfAccountIDInventory.ToString());
                writer.WriteAttributeString("ChartOfAccountIDTaxPurchase", clsProductDetails.ChartOfAccountIDTaxPurchase.ToString());
                writer.WriteAttributeString("ChartOfAccountIDTaxSold", clsProductDetails.ChartOfAccountIDTaxSold.ToString());
                writer.WriteAttributeString("IsItemSold", clsProductDetails.IsItemSold.ToString());
                writer.WriteAttributeString("WillPrintProductComposition", clsProductDetails.WillPrintProductComposition.ToString());
                writer.WriteAttributeString("UpdatedBy", clsProductDetails.UpdatedBy.ToString());
                writer.WriteAttributeString("UpdatedOn", clsProductDetails.UpdatedOn.ToString("MM/dd/yyyy HH:mm"));
                writer.WriteAttributeString("PercentageCommision", clsProductDetails.PercentageCommision.ToString());
                writer.WriteAttributeString("Active", clsProductDetails.Active.ToString());
                /*****End Of Product Information*****/

                writer.WriteAttributeString("ItemProductGroup", row["ProductGroup"].ToString());
                writer.WriteAttributeString("ItemProductSubGroup", row["ProductSubGroup"].ToString());
                writer.WriteAttributeString("ItemVariationMatrixID", row["VariationMatrixID"].ToString());
                writer.WriteAttributeString("ItemBaseVariationDescription", row["MatrixDescription"].ToString());
                writer.WriteAttributeString("ItemProductUnitID", row["ProductUnitID"].ToString());
                writer.WriteAttributeString("ItemProductUnitCode", row["ProductUnitCode"].ToString());
                writer.WriteAttributeString("ItemQuantity", row["Quantity"].ToString());
                writer.WriteAttributeString("ItemUnitCost", row["UnitCost"].ToString());
                writer.WriteAttributeString("ItemDiscount", row["Discount"].ToString());
                writer.WriteAttributeString("ItemDiscountApplied", row["DiscountApplied"].ToString());
                writer.WriteAttributeString("ItemDiscountType", row["DiscountType"].ToString());
                writer.WriteAttributeString("ItemAmount", row["Amount"].ToString());
                writer.WriteAttributeString("ItemVAT", row["VAT"].ToString());
                writer.WriteAttributeString("ItemVatableAmount", row["VatableAmount"].ToString());
                writer.WriteAttributeString("ItemEVAT", row["EVAT"].ToString());
                writer.WriteAttributeString("ItemEVatableAmount", row["EVatableAmount"].ToString());
                writer.WriteAttributeString("ItemLocalTax", row["LocalTax"].ToString());
                writer.WriteAttributeString("ItemisVATInclusive", row["isVATInclusive"].ToString());
                writer.WriteAttributeString("ItemTransferOutItemStatus", row["TransferOutItemStatus"].ToString());
                writer.WriteAttributeString("ItemIsVatable", row["IsVatable"].ToString());
                writer.WriteAttributeString("ItemSellingPrice", row["SellingPrice"].ToString());
                writer.WriteAttributeString("ItemSellingVAT", row["SellingVAT"].ToString());
                writer.WriteAttributeString("ItemSellingEVAT", row["SellingEVAT"].ToString());
                writer.WriteAttributeString("ItemSellingLocalTax", row["SellingLocalTax"].ToString());
                writer.WriteAttributeString("ItemOldSellingPrice", row["OldSellingPrice"].ToString());
                writer.WriteAttributeString("ItemRemarks", row["Remarks"].ToString());

                dtaProductVariation = clsProductVariation.ListAsDataTable(clsProductDetails.ProductID, null, System.Data.SqlClient.SortOrder.Ascending);
                foreach (DataRow rowVariation in dtaProductVariation.Rows)
                {
                    writer.WriteStartElement("Variation", null);
                    writer.WriteAttributeString("VariationCode", rowVariation["VariationCode"].ToString());
                    writer.WriteAttributeString("VariationType", rowVariation["VariationType"].ToString());
                    writer.WriteEndElement();
                }

                dtaProductVariationsMatrix = clsProductVariationsMatrix.ProductVariationsMatrixListAsDataTable(long.Parse(row["VariationMatrixID"].ToString()), null, System.Data.SqlClient.SortOrder.Ascending);
                foreach (DataRow rowVariationsMatrix in dtaProductVariationsMatrix.Rows)
                {
                    writer.WriteStartElement("VariationMatrix", null);
                    writer.WriteAttributeString("MatriXID", rowVariationsMatrix["MatriXID"].ToString());
                    writer.WriteAttributeString("VariationID", rowVariationsMatrix["VariationID"].ToString());
                    writer.WriteAttributeString("Description", rowVariationsMatrix["Description"].ToString());
                    writer.WriteAttributeString("VariationCode", rowVariationsMatrix["VariationCode"].ToString());
                    writer.WriteAttributeString("VariationType", rowVariationsMatrix["VariationType"].ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            //Write the XML to file and close the writer
            writer.Flush();
            writer.Close();

            clsTransferOut.CommitAndDispose();

            string stScript = "<Script>";
            stScript += "window.open('/RetailPlus/temp/" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsTransferOutDetails.TransferOutNo + "_" + clsTransferOutDetails.TransferOutDate.ToString("yyyyMMddHHmmssffff") + ".xml')";
            stScript += "</Script>";
            Response.Write(stScript);
        }

		#endregion
        
    }
}
