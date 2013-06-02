namespace AceSoft.RetailPlus.Inventory._Stock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	using System.Xml;
	using System.IO;

	/// <summary>
	///		Summary description for __Upload.
	/// </summary>
	public partial  class __Upload : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
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
			this.imgUpload.Click += new System.Web.UI.ImageClickEventHandler(this.imgUpload_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		private void imgUpload_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Upload();
		}

		protected void cmdUpload_Click(object sender, System.EventArgs e)
		{
			Upload();
		}


		#endregion

		#region Private Methods

		private void Upload()
		{
			if( ( txtPath.PostedFile != null ) && ( txtPath.PostedFile.ContentLength > 0 ) )
			{
				string fn = System.IO.Path.GetFileName(txtPath.PostedFile.FileName);
				string SaveLocation = "/RetailPlus/temp/uploaded_" + fn;

				txtPath.PostedFile.SaveAs(SaveLocation);
				XmlTextReader reader = new XmlTextReader(SaveLocation);
				reader.WhitespaceHandling = WhitespaceHandling.None;

				Stock clsStock = new Stock();
				clsStock.GetConnection();

				string strStockTransactionNo = null;
				long StockID = 0;
				long ContactID = 0;
				int UnitID = 0;
				long ProductGroupID = 0;
				long ProductSubGroupID = 0;
				long ProductID = 0;
				long ProductBaseMatrixID = 0;

				VariationDetails clsVariationDetails;
				int VariationID = 0;

				StockItemDetails clsStockItemDetails = new StockItemDetails();

				while (reader.Read()) 
				{
					switch (reader.NodeType) 
					{
						case XmlNodeType.Element:
							if (reader.Name == "Stock") 
							{
								strStockTransactionNo = reader.GetAttribute("TransactionNo");

								StockID = clsStock.Details(reader.GetAttribute("TransactionNo")).StockID;
								if (StockID > 0)
								{
									clsStock.CommitAndDispose();
									Label1.Text = "<b>This file has already been added to inventory.<br>";
									Label1.Text += "Please refer to transaction No: " + strStockTransactionNo + ".</b>";
									reader.Close();
									return;
								}
								Contacts clsContact = new Contacts(clsStock.Connection, clsStock.Transaction);
								ContactID = clsContact.Details(reader.GetAttribute("ContactCode")).ContactID;
								if (ContactID == 0)
								{
									ContactDetails clsContactDetails = new ContactDetails();
									clsContactDetails.ContactCode = reader.GetAttribute("ContactCode");
									clsContactDetails.ContactName = reader.GetAttribute("ContactName");
									clsContactDetails.ContactGroupID = Convert.ToInt32(reader.GetAttribute("ContactGroupID"));
                                    clsContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), reader.GetAttribute("ModeOfTerms"));
									clsContactDetails.Terms = Convert.ToInt32(reader.GetAttribute("Terms"));
									clsContactDetails.Address = reader.GetAttribute("Address");
									clsContactDetails.BusinessName = reader.GetAttribute("BusinessName");
									clsContactDetails.TelephoneNo = reader.GetAttribute("TelephoneNo");
									clsContactDetails.Remarks = reader.GetAttribute("Remarks");
									clsContactDetails.Debit = Convert.ToDecimal(reader.GetAttribute("Debit"));
									clsContactDetails.Credit = Convert.ToDecimal(reader.GetAttribute("Credit"));
									clsContactDetails.IsCreditAllowed = Convert.ToInt16(reader.GetAttribute("IsCreditAllowed"));
									clsContactDetails.CreditLimit = Convert.ToDecimal(reader.GetAttribute("CreditLimit"));
									ContactID = clsContact.Insert(clsContactDetails);
								}

                                StockDetails clsStockDetails = new StockDetails();
								clsStockDetails.TransactionNo = reader.GetAttribute("TransactionNo");
								clsStockDetails.StockTypeID = Convert.ToInt16(Constants.STOCK_TYPE_TRANSFER_FROM_BRANCH_ID);
								clsStockDetails.StockDate = DateTime.Now;
								clsStockDetails.SupplierID = ContactID;
								clsStockDetails.Remarks = reader.GetAttribute("StockRemarks") + Environment.NewLine + "Original Stock Date: " + reader.GetAttribute("StockDate");
								
								StockItemDetails[] itemDetails = new StockItemDetails[0];
								clsStockDetails.StockItems = itemDetails;

								StockID = clsStock.Insert(clsStockDetails);
							}
							else if (reader.Name == "Item") 
							{
								Data.Unit clsUnit = new Data.Unit(clsStock.Connection, clsStock.Transaction);
								UnitID = clsUnit.Details(reader.GetAttribute("ProductUnitCode")).UnitID;
								if (UnitID == 0) 
								{
									UnitDetails clsUnitDetails = new UnitDetails();
									clsUnitDetails.UnitCode = reader.GetAttribute("ProductUnitCode");
									clsUnitDetails.UnitName = reader.GetAttribute("ProductUnitName");
									UnitID = clsUnit.Insert(clsUnitDetails);
								}

								ProductGroup clsProductGroup = new ProductGroup(clsStock.Connection, clsStock.Transaction);
								ProductGroupID = clsProductGroup.Details(reader.GetAttribute("ProductGroupCode")).ProductGroupID;
								if (ProductGroupID == 0) 
								{
									Label1.Text += "inserting product group....";
									ProductGroupDetails clsProductGroupDetails = new ProductGroupDetails();
									clsProductGroupDetails.ProductGroupCode = reader.GetAttribute("ProductGroupCode");
									clsProductGroupDetails.ProductGroupName = reader.GetAttribute("ProductGroupName");
									clsProductGroupDetails.BaseUnitID = UnitID;
									clsProductGroupDetails.Price = Convert.ToDecimal(reader.GetAttribute("Price"));
									clsProductGroupDetails.PurchasePrice = Convert.ToDecimal(reader.GetAttribute("PurchasePrice"));
									clsProductGroupDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(reader.GetAttribute("IncludeInSubtotalDiscount"));
									clsProductGroupDetails.VAT = Convert.ToDecimal(reader.GetAttribute("VAT"));
									clsProductGroupDetails.EVAT = Convert.ToDecimal(reader.GetAttribute("EVAT"));
									clsProductGroupDetails.LocalTax = Convert.ToDecimal(reader.GetAttribute("LocalTax"));
									ProductGroupID = clsProductGroup.Insert(clsProductGroupDetails);
								}
								
								ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsStock.Connection, clsStock.Transaction);
								ProductSubGroupID = clsProductSubGroup.Details(reader.GetAttribute("ProductSubGroupCode")).ProductSubGroupID;
								if (ProductSubGroupID == 0) 
								{
									Label1.Text += "inserting product sub-group....";
									ProductSubGroupDetails clsProductSubGroupDetails = new ProductSubGroupDetails();
									clsProductSubGroupDetails.ProductGroupID = ProductGroupID;
									clsProductSubGroupDetails.ProductSubGroupCode = reader.GetAttribute("ProductSubGroupCode");
									clsProductSubGroupDetails.ProductSubGroupName = reader.GetAttribute("ProductSubGroupName");
									clsProductSubGroupDetails.BaseUnitID = UnitID;
									clsProductSubGroupDetails.Price = Convert.ToDecimal(reader.GetAttribute("Price"));
									clsProductSubGroupDetails.PurchasePrice = Convert.ToDecimal(reader.GetAttribute("PurchasePrice"));
									clsProductSubGroupDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(reader.GetAttribute("IncludeInSubtotalDiscount"));
									clsProductSubGroupDetails.VAT = Convert.ToDecimal(reader.GetAttribute("VAT"));
									clsProductSubGroupDetails.EVAT = Convert.ToDecimal(reader.GetAttribute("EVAT"));
									clsProductSubGroupDetails.LocalTax = Convert.ToDecimal(reader.GetAttribute("LocalTax"));
									ProductSubGroupID = clsProductSubGroup.Insert(clsProductSubGroupDetails);
								}

								Products clsProduct = new Products(clsStock.Connection, clsStock.Transaction);
                                ProductID = clsProduct.Details(reader.GetAttribute("BarCode")).ProductID;
								if (ProductID == 0) 
								{
									Label1.Text += "inserting product....";
									ProductDetails clsProductDetails = new ProductDetails();
									clsProductDetails.ProductCode  = reader.GetAttribute("ProductCode");
									clsProductDetails.BarCode  = reader.GetAttribute("BarCode");
									clsProductDetails.ProductDesc = reader.GetAttribute("ProductDesc");
									clsProductDetails.ProductGroupID = ProductGroupID; 
									clsProductDetails.ProductSubGroupID  = ProductSubGroupID;
									clsProductDetails.ProductDesc  = reader.GetAttribute("ProductDesc");
									clsProductDetails.BaseUnitID = UnitID;
									clsProductDetails.Price = Convert.ToDecimal(reader.GetAttribute("Price")); 
									clsProductDetails.PurchasePrice = Convert.ToDecimal(reader.GetAttribute("PurchasePrice")); 
									clsProductDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(reader.GetAttribute("IncludeInSubtotalDiscount")); 
									clsProductDetails.VAT = Convert.ToDecimal(reader.GetAttribute("VAT")); 
									clsProductDetails.EVAT = Convert.ToDecimal(reader.GetAttribute("EVAT")); 
									clsProductDetails.LocalTax = Convert.ToDecimal(reader.GetAttribute("LocalTax")); 
									clsProductDetails.Quantity = 0;
									clsProductDetails.MinThreshold = Convert.ToDecimal(reader.GetAttribute("MinThreshold"));
									clsProductDetails.MaxThreshold = Convert.ToDecimal(reader.GetAttribute("MaxThreshold"));
									clsProductDetails.SupplierID = Contacts.DEFAULT_SUPPLIER_ID;
									ProductID = clsProduct.Insert(clsProductDetails);
								}

                                //ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsStock.Connection, clsStock.Transaction);
                                //ProductBaseMatrixID = clsProductVariationsMatrix.BaseDetails(0, 0, ProductID, reader["ItemBaseVariationDescription"].ToString()).MatrixID;
                                //if (ProductBaseMatrixID == 0)
                                //{
                                //    ProductBaseMatrixDetails clsBaseDetails = new ProductBaseMatrixDetails();
                                //    clsBaseDetails.ProductID = ProductID;
                                //    clsBaseDetails.Description = reader.GetAttribute("ItemBaseVariationDescription");
                                //    clsBaseDetails.UnitID = UnitID;
                                //    clsBaseDetails.Price = Convert.ToDecimal(reader.GetAttribute("Price"));
                                //    clsBaseDetails.PurchasePrice = Convert.ToDecimal(reader.GetAttribute("PurchasePrice"));
                                //    clsBaseDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(reader.GetAttribute("IncludeInSubtotalDiscount"));
                                //    clsBaseDetails.Quantity = 0;
                                //    clsBaseDetails.VAT = Convert.ToDecimal(reader.GetAttribute("VAT"));
                                //    clsBaseDetails.EVAT = Convert.ToDecimal(reader.GetAttribute("EVAT"));
                                //    clsBaseDetails.LocalTax = Convert.ToDecimal(reader.GetAttribute("LocalTax"));
                                //    clsBaseDetails.MinThreshold = Convert.ToDecimal(reader.GetAttribute("MinThreshold"));
                                //    clsBaseDetails.MaxThreshold = Convert.ToDecimal(reader.GetAttribute("MaxThreshold"));
                                //    ProductBaseMatrixID = clsProductVariationsMatrix.InsertBaseVariation(clsBaseDetails);
                                //    clsBaseDetails.MatrixID = ProductBaseMatrixID;
                                //}

								clsStockItemDetails = new StockItemDetails();
								clsStockItemDetails.StockID = StockID;
								clsStockItemDetails.ProductID = ProductID;
								clsStockItemDetails.VariationMatrixID = ProductBaseMatrixID;
								clsStockItemDetails.ProductUnitID = UnitID;
								clsStockItemDetails.StockTypeID = Convert.ToInt16(Constants.STOCK_TYPE_TRANSFER_FROM_BRANCH_ID);
								clsStockItemDetails.StockDate = DateTime.Now;
								clsStockItemDetails.Quantity = Convert.ToDecimal(reader.GetAttribute("ItemQuantity"));
								clsStockItemDetails.Remarks = reader.GetAttribute("ItemRemarks");

                                Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
                                clsStock.AddItem(Constants.BRANCH_ID_MAIN, strStockTransactionNo, clsAccessUserDetails.Name, clsStockItemDetails, StockDirections.Increment);
							}
							else if (reader.Name == "Variation" && reader.GetAttribute("VariationCode") != null)
							{
								Variation clsVariation = new Variation(clsStock.Connection, clsStock.Transaction);
								VariationID = clsVariation.Details(reader.GetAttribute("VariationCode")).VariationID;
								if (VariationID == 0 ) 
								{	
									clsVariationDetails = new VariationDetails();
									clsVariationDetails.VariationCode = reader.GetAttribute("VariationCode");
									clsVariationDetails.VariationType = reader.GetAttribute("VariationType");
									VariationID = clsVariation.Insert(clsVariationDetails);
								}

								ProductVariationDetails clsProductVariationDetails = new ProductVariationDetails();
								clsProductVariationDetails.ProductID = ProductID;
								clsProductVariationDetails.VariationID = VariationID;

								ProductVariation clsProductVariation = new ProductVariation(clsStock.Connection, clsStock.Transaction);
								if (clsProductVariation.isExist(clsProductVariationDetails) == false)
								{
									long ProductVariationID = clsProductVariation.Insert(clsProductVariationDetails);
								}

							}
							else if (reader.Name == "VariationMatrix" && reader.GetAttribute("VariationCode") != null)
							{
								Variation clsVariation = new Variation(clsStock.Connection, clsStock.Transaction);
								VariationID = clsVariation.Details(reader.GetAttribute("VariationCode")).VariationID;
								if (VariationID == 0) 
								{
									clsVariationDetails = new VariationDetails();
									clsVariationDetails.VariationCode = reader.GetAttribute("VariationCode");
									clsVariationDetails.VariationType = reader.GetAttribute("VariationType");
									VariationID = clsVariation.Insert(clsVariationDetails);
								}

								ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsStock.Connection, clsStock.Transaction);
								if (clsProductVariationsMatrix.isExist(ProductBaseMatrixID, VariationID) == false)
								{
									ProductVariationsMatrixDetails clsProductVariationsMatrixDetails = new ProductVariationsMatrixDetails();
									clsProductVariationsMatrixDetails.MatrixID = ProductBaseMatrixID;
									clsProductVariationsMatrixDetails.VariationID = VariationID;
									clsProductVariationsMatrixDetails.Description = reader.GetAttribute("Description");
									clsProductVariationsMatrix.InsertVariation(clsProductVariationsMatrixDetails);
								}
							}
							else
							{
								Label1.Text = "<b>Reader Name:<b>" + reader.Name + "<br>";
							}
							break;
						case XmlNodeType.Text:
							Label1.Text = "<b>" + reader.LocalName + ":<b>" + reader.Value + "<br>";
							break;
					}       
				}
				reader.Close();
				
				clsStock.CommitAndDispose();
				Label1.Text = "<b>Transaction No.: " + strStockTransactionNo + " has been successfully transferred.<br>";
			}
			else
			{
				Response.Write("Please select a file to upload.");
			}

		}


		#endregion
	}
}
