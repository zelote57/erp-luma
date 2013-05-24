namespace AceSoft.RetailPlus.Inventory._Stock
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

	public partial  class __Transfer : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();	
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
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);
			this.imgTransfer.Click += new System.Web.UI.ImageClickEventHandler(this.imgTransfer_Click);

		}
		#endregion

		#region Web COntrol Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["StockItemID"].ToString();

				HyperLink lnkProduct = (HyperLink) e.Item.FindControl("lnkProduct");
				lnkProduct.Text = dr["ProductCode"].ToString();
				lnkProduct.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				HyperLink lnkVariation = (HyperLink) e.Item.FindControl("lnkVariation");
				if (dr["BaseVariationDescription"].ToString() == string.Empty || dr["BaseVariationDescription"].ToString() == null)
					lnkVariation.Text = "_";
				else
				{
					lnkVariation.Text = dr["BaseVariationDescription"].ToString();
					lnkVariation.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
				}
				
				Label lblProductUnit = (Label) e.Item.FindControl("lblProductUnit");
				lblProductUnit.Text = dr["UnitName"].ToString();

				Label lblStockType = (Label) e.Item.FindControl("lblStockType");
				lblStockType.Text = dr["StockTypeDescription"].ToString();

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = dr["Quantity"].ToString();

				Label lblRemarks = (Label) e.Item.FindControl("lblRemarks");
				lblRemarks.Text = dr["Remarks"].ToString();

				//For anchor
				//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}

		private void imgTransfer_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Transfer();	
		}

		protected void cmdTransfer_Click(object sender, System.EventArgs e)
		{
			Transfer();
		}


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			Branch clsBranch = new Branch();
			cboBranch.DataTextField = "BranchCode";
			cboBranch.DataValueField = "BranchID";
			cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
			cboBranch.DataBind();
			cboBranch.SelectedIndex = 0;
			clsBranch.CommitAndDispose();
			
			if (cboBranch.Items.Count == 0)
			{
				imgTransfer.Visible = false;
				cmdTransfer.Enabled = false;
				cboBranch.Items.Add(new ListItem("No Branch", "0"));
			}
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["stockid"],Session.SessionID));
			Stock clsStock = new Stock();
			StockDetails clsStockDetails = clsStock.Details(iID);
			clsStock.CommitAndDispose();

			lblStockID.Text = clsStockDetails.StockID.ToString();
			lblTransactionNo.Text = clsStockDetails.TransactionNo;
			lblStockDate.Text = clsStockDetails.StockDate.ToString("MMM. dd, yyyy HH:mm:ss");
			lblSupplier.Text = clsStockDetails.SupplierName;
			lblSupplierID.Text = clsStockDetails.SupplierID.ToString();
			lblStockTypeCode.Text = clsStockDetails.StockTypeCode; 
			lblStockTypeCode.ToolTip = clsStockDetails.StockTypeID.ToString(); 
			lblStockDirection.Text = clsStockDetails.StockDirection.ToString("G");

			LoadItems();
		}

		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			StockItem clsStockItem = new StockItem();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsStockItem.List(Convert.ToInt64(lblStockID.Text), "StockItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsStockItem.CommitAndDispose();
		}

		private void Transfer()
		{

			DataClass clsDataClass = new DataClass();

			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["stockid"],Session.SessionID));
			Stock clsStock = new Stock();
			StockDetails clsStockDetails = clsStock.Details(iID);

			StockItem clsStockItem = new StockItem(clsStock.Connection, clsStock.Transaction);
			DataTable dtaStockItemList = clsDataClass.DataReaderToDataTable(clsStockItem.List(Convert.ToInt64(lblStockID.Text), "StockItemID",SortOption.Ascending));
			
			Contacts clsContact = new Contacts(clsStock.Connection, clsStock.Transaction);
			ContactDetails clsContactDetails = clsContact.Details(clsStockDetails.SupplierID);
			
			Products clsProduct = new Products(clsStock.Connection, clsStock.Transaction);
			ProductDetails clsProductDetails;

			ProductVariation clsProductVariation = new ProductVariation(clsStock.Connection, clsStock.Transaction);
			DataTable dtaProductVariation;

			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsStock.Connection, clsStock.Transaction);
			DataTable dtaProductVariationsMatrix;
			
			string xmlFileName = Server.MapPath(@"\RetailPlus\temp\" + cboBranch.SelectedItem.Text.Replace(" ", "").Trim() + clsStockDetails.TransactionNo + ".xml");
			XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);
			
			writer.Formatting = Formatting.Indented;
			writer.WriteStartDocument();
			writer.WriteComment("This file represents a new stock transaction for branch: '" + cboBranch.SelectedItem.Text + "'.");
			writer.WriteComment("Save this in your local file. Goto 'File', click 'Save As', select the location in your local directory, click 'Save'.");
			writer.WriteStartElement("Stock");
			writer.WriteAttributeString("StockID",XmlConvert.ToString(clsStockDetails.StockID));
			writer.WriteAttributeString("TransactionNo",clsStockDetails.TransactionNo);
			writer.WriteAttributeString("StockTypeID",XmlConvert.ToString(clsStockDetails.StockTypeID));
			writer.WriteAttributeString("StockTypeCode",clsStockDetails.StockTypeCode);
			writer.WriteAttributeString("StockTypeDescription",clsStockDetails.StockTypeDescription);
			writer.WriteAttributeString("StockDirection",clsStockDetails.StockDirection.ToString());
			writer.WriteAttributeString("StockDate",clsStockDetails.StockDate.ToString("MM/dd/yyyy HH:mm:ss"));
			
			/******Supplier information******/
			writer.WriteAttributeString("ContactID",XmlConvert.ToString(clsContactDetails.ContactID));
			writer.WriteAttributeString("ContactCode",clsContactDetails.ContactCode);
			writer.WriteAttributeString("ContactName",clsContactDetails.ContactName);
			writer.WriteAttributeString("ContactGroupID",clsContactDetails.ContactGroupID.ToString());
			writer.WriteAttributeString("ContactGroupName",clsContactDetails.ContactGroupName);
			writer.WriteAttributeString("ModeOfTerms",clsContactDetails.ModeOfTerms.ToString());
			writer.WriteAttributeString("Terms",clsContactDetails.Terms.ToString());
			writer.WriteAttributeString("Address",clsContactDetails.Address);
			writer.WriteAttributeString("BusinessName",clsContactDetails.BusinessName);
			writer.WriteAttributeString("TelephoneNo",clsContactDetails.TelephoneNo);
			writer.WriteAttributeString("Remarks",clsContactDetails.Remarks);
			writer.WriteAttributeString("Debit",clsContactDetails.Debit.ToString());
			writer.WriteAttributeString("Credit",clsContactDetails.Credit.ToString());
			writer.WriteAttributeString("CreditLimit",clsContactDetails.CreditLimit.ToString());
			writer.WriteAttributeString("IsCreditAllowed",clsContactDetails.IsCreditAllowed.ToString());
			writer.WriteAttributeString("DateCreated",clsContactDetails.DateCreated.ToString("MM/dd/yy HH:mm:ss"));
			writer.WriteAttributeString("Deleted",clsContactDetails.Deleted.ToString());
			/******End Of Supplier Information******/

			writer.WriteAttributeString("StockRemarks",clsStockDetails.Remarks);
				foreach (DataRow row in dtaStockItemList.Rows) {
					clsProductDetails = new ProductDetails();
                    clsProductDetails = clsProduct.Details(Convert.ToInt64(row["ProductID"].ToString()));

					writer.WriteStartElement("Item");
						writer.WriteAttributeString("ItemStockItemID", row["StockItemID"].ToString());
						writer.WriteAttributeString("ItemStockID", row["StockID"].ToString());
						writer.WriteAttributeString("ItemProductID", row["ProductID"].ToString());

						/*****Product Information*****/
						writer.WriteAttributeString("ProductCode", clsProductDetails.ProductCode);	
						writer.WriteAttributeString("BarCode", clsProductDetails.BarCode);	
						writer.WriteAttributeString("ProductDesc", clsProductDetails.ProductDesc);	
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
						/*****End Of Product Information*****/
						
						writer.WriteAttributeString("ItemVariationMatrixID", row["VariationMatrixID"].ToString());
						writer.WriteAttributeString("ItemBaseVariationDescription", row["BaseVariationDescription"].ToString());
						writer.WriteAttributeString("ItemProductUnitID", row["ProductUnitID"].ToString());
						writer.WriteAttributeString("ItemUnitCode", row["UnitCode"].ToString());
						writer.WriteAttributeString("ItemUnitName", row["UnitName"].ToString());
						writer.WriteAttributeString("ItemStockTypeID", row["StockTypeID"].ToString());
						writer.WriteAttributeString("ItemStockTypeDescription", row["StockTypeDescription"].ToString());
						writer.WriteAttributeString("ItemStockDate", row["StockDate"].ToString());
						writer.WriteAttributeString("ItemQuantity", row["Quantity"].ToString());
						writer.WriteAttributeString("ItemRemarks", row["Remarks"].ToString());

                        dtaProductVariation = clsProductVariation.ListAsDataTable(clsProductDetails.ProductID, "a.VariationID", System.Data.SqlClient.SortOrder.Ascending);
						foreach(DataRow rowVariation in dtaProductVariation.Rows)
						{
							writer.WriteStartElement("Variation", null);
							writer.WriteAttributeString("VariationCode", rowVariation["VariationCode"].ToString());
							writer.WriteAttributeString("VariationType", rowVariation["VariationType"].ToString());
							writer.WriteEndElement();
						}

                        dtaProductVariationsMatrix = clsProductVariationsMatrix.ProductVariationsMatrixListAsDataTable(Convert.ToInt64(row["VariationMatrixID"].ToString()), "MatrixID", System.Data.SqlClient.SortOrder.Ascending);
						foreach(DataRow rowVariationsMatrix in dtaProductVariationsMatrix.Rows)
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
			
			clsStock.CommitAndDispose();

			string stScript = "<Script>";
			stScript += "window.open('/RetailPlus/temp/" + cboBranch.SelectedItem.Text.Replace(" ", "").Trim() + clsStockDetails.TransactionNo + ".xml')";
			stScript += "</Script>";
			Response.Write(stScript);
		}


		#endregion
	}
}
