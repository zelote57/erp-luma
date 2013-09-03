namespace AceSoft.RetailPlus.PurchasesAndPayables._PO
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
            PrintPO();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            PrintPO();
        }
        protected void cmdPrintSelling_Click(object sender, EventArgs e)
        {
            PrintPOSelling();
        }
        protected void imgPrintSelling_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PrintPOSelling();
        }
        protected void  imgExport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportPO2File();
        }
        protected void  cmdExport_Click(object sender, EventArgs e)
        {
            ExportPO2File();
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
                chkList.Value = dr["POItemID"].ToString();

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
                lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.##0");

                Label lblProductUnitID = (Label)e.Item.FindControl("lblProductUnitID");
                lblProductUnitID.Text = dr["ProductUnitID"].ToString();

                Label lblProductUnitCode = (Label)e.Item.FindControl("lblProductUnitCode");
                lblProductUnitCode.Text = dr["ProductUnitCode"].ToString();

                Label lblUnitCost = (Label)e.Item.FindControl("lblUnitCost");
                lblUnitCost.Text = Convert.ToDecimal(dr["UnitCost"].ToString()).ToString("#,##0.##0");

                Label lblDiscountApplied = (Label)e.Item.FindControl("lblDiscountApplied");
                lblDiscountApplied.Text = Convert.ToDecimal(dr["DiscountApplied"].ToString()).ToString("#,##0.##0");

                DiscountTypes DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                if (DiscountType == DiscountTypes.Percentage)
                {
                    Label lblPercent = (Label)e.Item.FindControl("lblPercent");
                    lblPercent.Visible = true;
                }

                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.##0");

                Label lblVAT = (Label)e.Item.FindControl("lblVAT");
                lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.##0");

                Label lblEVAT = (Label)e.Item.FindControl("lblEVAT");
                lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.##0");

                Label lblisVATInclusive = (Label)e.Item.FindControl("lblisVATInclusive");
                lblisVATInclusive.Text = Convert.ToBoolean(Convert.ToInt16(dr["isVATInclusive"].ToString())).ToString();

                Label lblLocalTax = (Label)e.Item.FindControl("lblLocalTax");
                lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.##0");

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
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["poid"],Session.SessionID));
			PO clsPO = new PO();
			PODetails clsDetails = clsPO.Details(iID);
			clsPO.CommitAndDispose();

			lblPOID.Text = clsDetails.POID.ToString();
			lnkPONo.Text = clsDetails.PONo;
            lnkPONo.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(clsDetails.POID.ToString(), Session.SessionID);

			lblPODate.Text = clsDetails.PODate.ToString("yyyy-MM-dd HH:mm:ss");
			lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
			lblSupplierID.Text = clsDetails.SupplierID.ToString();

			lblSupplierCode.Text = clsDetails.SupplierCode.ToString();
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);	
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
        private void PrintPOSelling()
        {
            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("po", Session.SessionID) + "&poid=" + Common.Encrypt(lblPOID.Text, Session.SessionID) + "&reporttype=" + Common.Encrypt("POReportSellingPrice", Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrintSellingPrice, this.updPrintSellingPrice.GetType(), "openwindow", javaScript, true);
        }
        private void ExportPO2File()
        {

            DataClass clsDataClass = new DataClass();

            PO clsPO = new PO();
            PODetails clsPODetails = clsPO.Details(long.Parse(lblPOID.Text));

            POItem clsPOItem = new POItem(clsPO.Connection, clsPO.Transaction);
            DataTable dtaPOItems = clsPOItem.ListAsDataTable(clsPODetails.POID, null, SortOption.Ascending);

            //Contact clsContact = new Contact(clsStock.Connection, clsStock.Transaction);
            //ContactDetails clsPODetails = clsContact.Details(clsPODetails.SupplierID);

            Products clsProduct = new Products(clsPO.Connection, clsPO.Transaction);
            ProductDetails clsProductDetails;

            ProductVariation clsProductVariation = new ProductVariation(clsPO.Connection, clsPO.Transaction);
            DataTable dtaProductVariation;

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsPO.Connection, clsPO.Transaction);
            DataTable dtaProductVariationsMatrix;

            string xmlFileName = Server.MapPath(@"\RetailPlus\temp\" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsPODetails.PONo + "_" + clsPODetails.PODate.ToString("yyyyMMddHHmmssffff") + ".xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteComment("This file represents the Purchase Order Details of PO No: '" + clsPODetails.PONo + "' for " + lblBranchCode.Text + " branch.");
            writer.WriteComment("Save this in your local file. Goto 'File', click 'Save As', select the location in your local directory, click 'Save'.");
            writer.WriteStartElement("PODetails");
            writer.WriteAttributeString("POID", XmlConvert.ToString(clsPODetails.POID));
            writer.WriteAttributeString("PONo", clsPODetails.PONo);
            writer.WriteAttributeString("PODate", clsPODetails.PODate.ToString("MM/dd/yyyy HH:mm:ss"));

            /******Supplier information******/
            writer.WriteAttributeString("SupplierID", XmlConvert.ToString(clsPODetails.SupplierID));
            writer.WriteAttributeString("SupplierCode", clsPODetails.SupplierCode);
            writer.WriteAttributeString("SupplierContact", clsPODetails.SupplierContact);
            writer.WriteAttributeString("SupplierAddress", clsPODetails.SupplierAddress);
            writer.WriteAttributeString("SupplierTelephoneNo", clsPODetails.SupplierTelephoneNo);
            writer.WriteAttributeString("SupplierModeOfTerms", XmlConvert.ToString(clsPODetails.SupplierModeOfTerms));
            writer.WriteAttributeString("SupplierTerms", XmlConvert.ToString(clsPODetails.SupplierTerms));
            /******End Of Supplier Information******/

            writer.WriteAttributeString("RequiredDeliveryDate", clsPODetails.RequiredDeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));
            
            /******Branch & Purchaser Information******/
            writer.WriteAttributeString("BranchID", XmlConvert.ToString(clsPODetails.BranchID));
            writer.WriteAttributeString("BranchCode", clsPODetails.BranchCode);
            writer.WriteAttributeString("BranchName", clsPODetails.BranchName);
            writer.WriteAttributeString("BranchAddress", clsPODetails.BranchAddress);
            writer.WriteAttributeString("PurchaserID", clsPODetails.PurchaserID.ToString());
            writer.WriteAttributeString("PurchaserName", clsPODetails.PurchaserName);
            /******End Of Branch & Purchaser Information******/

            /******Amount Information******/
            writer.WriteAttributeString("SubTotal", XmlConvert.ToString(clsPODetails.SubTotal));
            writer.WriteAttributeString("Discount", XmlConvert.ToString(clsPODetails.Discount));
            writer.WriteAttributeString("DiscountApplied", XmlConvert.ToString(clsPODetails.DiscountApplied));
            writer.WriteAttributeString("DiscountType", clsPODetails.DiscountType.ToString("d"));
            writer.WriteAttributeString("VAT", XmlConvert.ToString(clsPODetails.VAT));
            writer.WriteAttributeString("VatableAmount", XmlConvert.ToString(clsPODetails.VatableAmount));
            writer.WriteAttributeString("EVAT", XmlConvert.ToString(clsPODetails.EVAT));
            writer.WriteAttributeString("EVatableAmount", XmlConvert.ToString(clsPODetails.EVatableAmount));
            writer.WriteAttributeString("LocalTax", XmlConvert.ToString(clsPODetails.LocalTax));
            writer.WriteAttributeString("Freight", XmlConvert.ToString(clsPODetails.Freight));
            writer.WriteAttributeString("Deposit", XmlConvert.ToString(clsPODetails.Deposit));
            writer.WriteAttributeString("UnpaidAmount", XmlConvert.ToString(clsPODetails.UnpaidAmount));
            writer.WriteAttributeString("PaidAmount", XmlConvert.ToString(clsPODetails.PaidAmount));
            writer.WriteAttributeString("TotalItemDiscount", XmlConvert.ToString(clsPODetails.TotalItemDiscount));
            writer.WriteAttributeString("Status", clsPODetails.Status.ToString("d"));
            writer.WriteAttributeString("Remarks", clsPODetails.Remarks);
            writer.WriteAttributeString("SupplierDRNo", clsPODetails.SupplierDRNo);
            writer.WriteAttributeString("DeliveryDate", clsPODetails.DeliveryDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledDate", clsPODetails.CancelledDate.ToString("MM/dd/yyyy HH:mm:ss"));
            writer.WriteAttributeString("CancelledRemarks", clsPODetails.CancelledRemarks);
            writer.WriteAttributeString("CancelledByID", XmlConvert.ToString(clsPODetails.CancelledByID));
            /******End Of Branch Information******/
            
            foreach (DataRow row in dtaPOItems.Rows)
            {
                clsProductDetails = new ProductDetails();
                clsProductDetails = clsProduct.Details(Convert.ToInt64(row["ProductID"].ToString()));

                writer.WriteStartElement("POItem");
                writer.WriteAttributeString("POItemID", row["POItemID"].ToString());
                writer.WriteAttributeString("POID", row["POID"].ToString());
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
                writer.WriteAttributeString("ProductUnitCode", clsProductDetails.BaseUnitCode);
                writer.WriteAttributeString("ProductUnitName", clsProductDetails.BaseUnitName);
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
                writer.WriteAttributeString("IsItemSold", clsProductDetails.IsItemSold.ToString());
                writer.WriteAttributeString("Active", clsProductDetails.Active.ToString());
                writer.WriteAttributeString("PercentageCommision", clsProductDetails.PercentageCommision.ToString());
                /*****End Of Product Information*****/

                writer.WriteAttributeString("ItemVariationMatrixID", row["VariationMatrixID"].ToString());
                writer.WriteAttributeString("ItemBaseVariationDescription", row["MatrixDescription"].ToString());
                writer.WriteAttributeString("ItemProductUnitID", row["ProductUnitID"].ToString());
                writer.WriteAttributeString("ItemUnitCode", row["ProductUnitCode"].ToString());
                writer.WriteAttributeString("isVATInclusive", row["isVATInclusive"].ToString());
                writer.WriteAttributeString("POItemStatus", row["POItemStatus"].ToString());
                writer.WriteAttributeString("IsVatable", row["IsVatable"].ToString());
                writer.WriteAttributeString("SellingPrice", row["SellingPrice"].ToString());
                writer.WriteAttributeString("SellingVAT", row["SellingVAT"].ToString());
                writer.WriteAttributeString("SellingEVAT", row["SellingEVAT"].ToString());
                writer.WriteAttributeString("SellingLocalTax", row["SellingLocalTax"].ToString());
                writer.WriteAttributeString("OldSellingPrice", row["OldSellingPrice"].ToString());

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

            clsPO.CommitAndDispose();

            string stScript = "<Script>";
            stScript += "window.open('/RetailPlus/temp/" + lblBranchCode.Text.Replace(" ", "").Trim() + "_" + clsPODetails.PONo + "_" + clsPODetails.PODate.ToString("yyyyMMddHHmmssffff") + ".xml')";
            stScript += "</Script>";
            Response.Write(stScript);
        }

		#endregion

        
    }
}
