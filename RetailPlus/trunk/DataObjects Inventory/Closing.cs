using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]

	#region ClosingDetails

	public struct ClosingDetails
	{
		public long ClosingID;
		public string ClosingNo;
		public DateTime ClosingDate;
		public long SupplierID;
		public string SupplierCode;
		public string SupplierContact;
		public string SupplierAddress;
		public string SupplierTelephoneNo;
		public int SupplierModeOfTerms;
		public int SupplierTerms;
		public DateTime RequiredDeliveryDate;
		public int BranchID;
		public string BranchCode;
		public string BranchName;
		public string BranchAddress;
		public long TransferredByID;
		public decimal ClosingSubTotal;
		public decimal ClosingDiscount;
		public decimal ClosingVAT;
		public decimal ClosingVatableAmount;
		public ClosingStatus ClosingStatus;
		public string ClosingRemarks;
		public string SupplierDRNo;
		public DateTime DeliveryDate;
	}

	#endregion

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Closing : POSConnection
	{
		#region Constructors and Destructors

		public Closing()
            : base(null, null)
        {
        }

        public Closing(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(ClosingDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblClosing (" +
								"ClosingNo, " +
								"ClosingDate, " +
								"SupplierID, " +
								"SupplierCode, " +
								"SupplierContact, " +
								"SupplierAddress, " +
								"SupplierTelephoneNo, " +
								"SupplierModeOfTerms, " +
								"SupplierTerms, " +
								"RequiredDeliveryDate, " +
								"BranchID, " +
								"TransferredByID, " +
								"ClosingSubTotal, " +
								"ClosingDiscount, " +
								"ClosingVAT, " +
								"ClosingVatableAmount, " +
								"ClosingStatus, " +
								"ClosingRemarks " +
							") VALUES (" +
								"@ClosingNo, " +
								"@ClosingDate, " +
								"@SupplierID, " +
								"@SupplierCode, " +
								"@SupplierContact, " +
								"@SupplierAddress, " +
								"@SupplierTelephoneNo, " +
								"@SupplierModeOfTerms, " +
								"@SupplierTerms, " +
								"@RequiredDeliveryDate, " +
								"@BranchID, " +
								"@TransferredByID, " +
								"@ClosingSubTotal, " +
								"@ClosingDiscount, " +
								"@ClosingVAT, " +
								"@ClosingVatableAmount, " +
								"@ClosingStatus, " +
								"@ClosingRemarks " +
							");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingNo = new MySqlParameter("@ClosingNo",MySqlDbType.String);
				prmClosingNo.Value = Details.ClosingNo;
				cmd.Parameters.Add(prmClosingNo);

				MySqlParameter prmClosingDate = new MySqlParameter("@ClosingDate",MySqlDbType.DateTime);
				prmClosingDate.Value = Details.ClosingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmClosingDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);
								 
				MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
				prmSupplierCode.Value = Details.SupplierCode;
				cmd.Parameters.Add(prmSupplierCode);
		 
				MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
				prmSupplierContact.Value = Details.SupplierContact;
				cmd.Parameters.Add(prmSupplierContact);			 
				
				MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
				prmSupplierAddress.Value = Details.SupplierAddress;
				cmd.Parameters.Add(prmSupplierAddress);	
				
				MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
				prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
				cmd.Parameters.Add(prmSupplierTelephoneNo);	

				MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
				prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
				cmd.Parameters.Add(prmSupplierModeOfTerms);	

				MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
				prmSupplierTerms.Value = Details.SupplierTerms;
				cmd.Parameters.Add(prmSupplierTerms);			 
							 
				MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
				prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredDeliveryDate);
	 
				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);				 
				
				MySqlParameter prmTransferredByID = new MySqlParameter("@TransferredByID",MySqlDbType.Int64);						
				prmTransferredByID.Value = Details.TransferredByID;
				cmd.Parameters.Add(prmTransferredByID);
								 
				MySqlParameter prmClosingSubTotal = new MySqlParameter("@ClosingSubTotal",MySqlDbType.Decimal);			
				prmClosingSubTotal.Value = Details.ClosingSubTotal;
				cmd.Parameters.Add(prmClosingSubTotal);
				
				MySqlParameter prmClosingDiscount = new MySqlParameter("@ClosingDiscount",MySqlDbType.Decimal);			
				prmClosingDiscount.Value = Details.ClosingDiscount;
				cmd.Parameters.Add(prmClosingDiscount);

				MySqlParameter prmClosingVAT = new MySqlParameter("@ClosingVAT",MySqlDbType.Decimal);			
				prmClosingVAT.Value = Details.ClosingVAT;
				cmd.Parameters.Add(prmClosingVAT);

				MySqlParameter prmClosingVatableAmount = new MySqlParameter("@ClosingVatableAmount",MySqlDbType.Decimal);			
				prmClosingVatableAmount.Value = Details.ClosingVatableAmount;
				cmd.Parameters.Add(prmClosingVatableAmount);
								 
				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);			
				prmClosingStatus.Value = Details.ClosingStatus.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmClosingRemarks = new MySqlParameter("@ClosingRemarks",MySqlDbType.String);			
				prmClosingRemarks.Value = Details.ClosingRemarks;
				cmd.Parameters.Add(prmClosingRemarks);	

				base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ClosingDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblClosing SET " + 
								"ClosingNo					=	@ClosingNo, " +
								"ClosingDate					=	@ClosingDate, " +
								"SupplierID				=	@SupplierID, " +
								"SupplierCode			=	@SupplierCode, " +
								"SupplierContact		=	@SupplierContact, " +
								"SupplierAddress		=	@SupplierAddress, " +
								"SupplierTelephoneNo	=	@SupplierTelephoneNo, " +
								"SupplierModeOfTerms	=	@SupplierModeOfTerms, " +
								"SupplierTerms			=	@SupplierTerms, " +
								"RequiredDeliveryDate	=	@RequiredDeliveryDate, " +
								"BranchID				=	@BranchID, " +
								"TransferredByID			=	@TransferredByID, " +
//								"ClosingSubTotal				=	@ClosingSubTotal, " +
//								"ClosingDiscount				=	@ClosingDiscount, " +
//								"ClosingVAT					=	@ClosingVAT, " +
//								"ClosingVatableAmount		=	@ClosingVatableAmount, " +
//								"ClosingStatus				=	@ClosingStatus, " +
								"ClosingRemarks				=	@ClosingRemarks " +
							"WHERE ClosingID = @ClosingID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingNo = new MySqlParameter("@ClosingNo",MySqlDbType.String);
				prmClosingNo.Value = Details.ClosingNo;
				cmd.Parameters.Add(prmClosingNo);

				MySqlParameter prmClosingDate = new MySqlParameter("@ClosingDate",MySqlDbType.DateTime);
				prmClosingDate.Value = Details.ClosingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmClosingDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);
								 
				MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
				prmSupplierCode.Value = Details.SupplierCode;
				cmd.Parameters.Add(prmSupplierCode);
		 
				MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
				prmSupplierContact.Value = Details.SupplierContact;
				cmd.Parameters.Add(prmSupplierContact);			 
				
				MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
				prmSupplierAddress.Value = Details.SupplierAddress;
				cmd.Parameters.Add(prmSupplierAddress);	
				
				MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
				prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
				cmd.Parameters.Add(prmSupplierTelephoneNo);	

				MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
				prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
				cmd.Parameters.Add(prmSupplierModeOfTerms);	

				MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
				prmSupplierTerms.Value = Details.SupplierTerms;
				cmd.Parameters.Add(prmSupplierTerms);			 
							 
				MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
				prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredDeliveryDate);
	 
				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);				 
				
				MySqlParameter prmTransferredByID = new MySqlParameter("@TransferredByID",MySqlDbType.Int64);						
				prmTransferredByID.Value = Details.TransferredByID;
				cmd.Parameters.Add(prmTransferredByID);
								 
//				MySqlParameter prmClosingSubTotal = new MySqlParameter("@ClosingSubTotal",MySqlDbType.Decimal);			
//				prmClosingSubTotal.Value = Details.ClosingSubTotal;
//				cmd.Parameters.Add(prmClosingSubTotal);
//				
//				MySqlParameter prmClosingDiscount = new MySqlParameter("@ClosingDiscount",MySqlDbType.Decimal);			
//				prmClosingDiscount.Value = Details.ClosingDiscount;
//				cmd.Parameters.Add(prmClosingDiscount);
//
//				MySqlParameter prmClosingVAT = new MySqlParameter("@ClosingVAT",MySqlDbType.Decimal);			
//				prmClosingVAT.Value = Details.ClosingVAT;
//				cmd.Parameters.Add(prmClosingVAT);
//
//				MySqlParameter prmClosingVatableAmount = new MySqlParameter("@ClosingVatableAmount",MySqlDbType.Decimal);			
//				prmClosingVatableAmount.Value = Details.ClosingVatableAmount;
//				cmd.Parameters.Add(prmClosingVatableAmount);
//								 
//				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);			
//				prmClosingStatus.Value = Details.ClosingStatus.ToString("d");
//				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmClosingRemarks = new MySqlParameter("@ClosingRemarks",MySqlDbType.String);			
				prmClosingRemarks.Value = Details.ClosingRemarks;
				cmd.Parameters.Add(prmClosingRemarks);

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",MySqlDbType.Int64);						
				prmClosingID.Value = Details.ClosingID;
				cmd.Parameters.Add(prmClosingID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void IssueGRN(long ClosingID, string SupplierDRNo, DateTime DeliveryDate)
		{
			try 
			{
				string SQL=	"UPDATE tblClosing SET " + 
								"SupplierDRNo			=	@SupplierDRNo, " +
								"DeliveryDate			=	@DeliveryDate, " +
								"ClosingStatus				=	@ClosingStatus " +
							"WHERE ClosingID = @ClosingID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSupplierDRNo = new MySqlParameter("@SupplierDRNo",MySqlDbType.String);
				prmSupplierDRNo.Value = SupplierDRNo;
				cmd.Parameters.Add(prmSupplierDRNo);

				MySqlParameter prmDeliveryDate = new MySqlParameter("@DeliveryDate",MySqlDbType.DateTime);
				prmDeliveryDate.Value = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDeliveryDate);

				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);
				prmClosingStatus.Value = ClosingStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",MySqlDbType.Int64);						
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				ClosingItem clsClosingItem = new ClosingItem(base.Connection, base.Transaction);
				clsClosingItem.Post(ClosingID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(ClosingID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		private void AddItemToInventory(long ClosingID)
		{

			ClosingDetails clsClosingDetails = Details(ClosingID);
			SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			ClosingItem clsClosingItem = new ClosingItem(base.Connection, base.Transaction);
			ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
			Products clsProduct = new Products(base.Connection, base.Transaction);
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);

			MySqlDataReader myReader = clsClosingItem.List(ClosingID, "ClosingItemID", SortOption.Ascending);

			while (myReader.Read())
			{
                long lngProductID = myReader.GetInt64("ProductID");
				int intProductUnitID = myReader.GetInt16("ProductUnitID");

				decimal decItemQuantity = myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngMatrixID = myReader.GetInt64("VariationMatrixID");
				string strMatrixDescription = "" + myReader["MatrixDescription"].ToString();
				string strProductCode = "" + myReader["ProductCode"].ToString();
				decimal decUnitCost = myReader.GetDecimal("UnitCost");
				decimal decItemCost = myReader.GetDecimal("Amount");
				decimal decVAT = myReader.GetDecimal("VAT");

				/*******************************************
				 * Update Purchasing Information
				 * ****************************************/
                clsProduct.UpdatePurchasing(lngProductID, lngMatrixID, clsClosingDetails.SupplierID, intProductUnitID, (decItemQuantity * decUnitCost) / decQuantity);

                ///*******************************************
                // * Add to Inventory
                // * ****************************************/
                //clsProduct.AddQuantity(ProductID, Quantity);
                //if (VariationMatrixID != 0)
                //{ clsProductVariationsMatrix.AddQuantity(VariationMatrixID, Quantity);}
                // July 26, 2011: change the above codes to the following
                clsProduct.AddQuantity(clsClosingDetails.BranchID, lngProductID, lngMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE) + " @ " + decUnitCost.ToString("#,##0.#0"), DateTime.Now, clsClosingDetails.ClosingNo, clsClosingDetails.TransferredByID.ToString());

				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				InventoryDetails clsInventoryDetails = new InventoryDetails();
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsClosingDetails.DeliveryDate;
				clsInventoryDetails.ReferenceNo = clsClosingDetails.ClosingNo;
				clsInventoryDetails.ContactID = clsClosingDetails.SupplierID;
				clsInventoryDetails.ContactCode = clsClosingDetails.SupplierCode;
                clsInventoryDetails.ProductID = lngProductID;
				clsInventoryDetails.ProductCode = strProductCode;
				clsInventoryDetails.VariationMatrixID = lngMatrixID;
				clsInventoryDetails.MatrixDescription = strMatrixDescription;
				clsInventoryDetails.ClosingQuantity = decQuantity;
				clsInventoryDetails.ClosingCost = decItemCost - decVAT;
				clsInventoryDetails.ClosingVAT = decItemCost;	// Closing Cost with VAT

				clsInventory.Insert(clsInventoryDetails);

			}
			myReader.Close();

		}

		
		public void Cancel(long ClosingID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try 
			{
				string SQL=	"UPDATE tblClosing SET " + 
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"ClosingStatus		=	@ClosingStatus " +
							"WHERE ClosingID = @ClosingID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
				prmCancelledDate.Value = CancelledDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmCancelledDate);

				MySqlParameter prmCancelledRemarks = new MySqlParameter("@CancelledRemarks",MySqlDbType.String);
				prmCancelledRemarks.Value = Remarks;
				cmd.Parameters.Add(prmCancelledRemarks);

				MySqlParameter prmCancelledByID = new MySqlParameter("@CancelledByID",MySqlDbType.Int64);						
				prmCancelledByID.Value = CancelledByID;
				cmd.Parameters.Add(prmCancelledByID);

				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);
				prmClosingStatus.Value = ClosingStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",MySqlDbType.Int64);						
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				ClosingItem clsClosingItem = new ClosingItem(base.Connection, base.Transaction);
				clsClosingItem.Cancel(ClosingID);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void GenerateItemsForReorder(long ClosingID)
		{
			try 
			{
				GetConnection();

                Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
				TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);

				ClosingDetails clsClosingDetails = Details(ClosingID);

                Products clsProduct = new Products(base.Connection, base.Transaction);
				System.Data.DataTable dt = clsProduct.ForReorder(clsClosingDetails.SupplierID);

				ClosingItem clsClosingItem = new ClosingItem(base.Connection, base.Transaction);
				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					ClosingItemDetails clsDetails = new ClosingItemDetails();

					clsDetails.ClosingID = ClosingID;
					clsDetails.ProductID = Convert.ToInt64(dr["ProductID"]);
					clsDetails.ProductCode = dr["ProductCode"].ToString();
					clsDetails.BarCode = dr["BarCode"].ToString();
					clsDetails.Description = dr["ProductDesc"].ToString();
					clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
					clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
					clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"].ToString());
					clsDetails.ProductUnitCode = dr["UnitName"].ToString();
					clsDetails.Quantity = Convert.ToDecimal(dr["ReorderQty"].ToString());
					clsDetails.UnitCost = Convert.ToDecimal(dr["Price"].ToString());
					clsDetails.Discount = 0;
					clsDetails.InPercent = false;
					clsDetails.TotalDiscount = 0;
					clsDetails.Remarks = "added using auto generation";
				
					decimal amount = clsDetails.Quantity * clsDetails.UnitCost;

					if (Convert.ToDecimal(dr["VAT"]) > 0)
					{				
						clsDetails.VatableAmount = amount;
						clsDetails.EVatableAmount = amount;
						clsDetails.LocalTax = amount;
					
						clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
						clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
						clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
						clsDetails.IsVatable = true;
					}
					else
					{
						clsDetails.VAT = 0;
						clsDetails.VatableAmount = 0;
						clsDetails.EVAT = 0;
						clsDetails.EVatableAmount = 0;
						clsDetails.LocalTax = 0;
						clsDetails.IsVatable = false;
					}
					clsDetails.Amount = amount + clsDetails.VAT;
					
					System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsClosingDetails.SupplierID);
					if (dtmatrix.Rows.Count > 0)
						foreach(System.Data.DataRow drmatrix in dtmatrix.Rows)
						{
							amount = clsDetails.Quantity * clsDetails.UnitCost;

							clsDetails.ProductUnitID = Convert.ToInt32(drmatrix["UnitID"]);
							clsDetails.ProductUnitCode = drmatrix["UnitName"].ToString();
							clsDetails.Quantity = Convert.ToDecimal(drmatrix["ReorderQty"]);
							clsDetails.UnitCost = Convert.ToDecimal(drmatrix["Price"]);

							if (Convert.ToDecimal(drmatrix["VAT"]) > 0)
							{				
								clsDetails.VatableAmount = amount;
								clsDetails.EVatableAmount = amount;
								clsDetails.LocalTax = amount;
					
								clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
								clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
								clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
								clsDetails.IsVatable = true;
							}
							else
							{
								clsDetails.VAT = 0;
								clsDetails.VatableAmount = 0;
								clsDetails.EVAT = 0;
								clsDetails.EVatableAmount = 0;
								clsDetails.LocalTax = 0;
								clsDetails.IsVatable = false;
							}
							clsDetails.Amount = amount + clsDetails.VAT;

							clsDetails.VariationMatrixID = Convert.ToInt64(drmatrix["MatrixID"]);
							clsDetails.MatrixDescription = drmatrix["VariationDesc"].ToString();
							clsClosingItem.Insert(clsDetails);
						}
					else
					{
						clsDetails.VariationMatrixID = 0;
						clsDetails.MatrixDescription = string.Empty;
						clsClosingItem.Insert(clsDetails);
					}
					
				}
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblClosing WHERE ClosingID IN (" + IDs + ");";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "ClosingID, " +
                                "ClosingNo, " +
                                "ClosingDate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierAddress, " +
                                "SupplierTelephoneNo, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "RequiredDeliveryDate, " +
                                "a.BranchID, " +
                                "BranchCode, " +
                                "BranchName, " +
                                "b.Address BranchAddress, " +
                                "TransferredByID, " +
                                "ClosingSubTotal, " +
                                "ClosingDiscount, " +
                                "ClosingVAT, " +
                                "ClosingVatableAmount, " +
                                "ClosingStatus, " +
                                "ClosingRemarks, " +
                                "SupplierDRNo, " +
                                "DeliveryDate " +
                            "FROM tblClosing a INNER JOIN tblBranch b ON a.BranchID = b.BranchID ";
            return stSQL;
        }

		#region Details

		public ClosingDetails Details(long ClosingID)
		{
			try
			{
				string SQL=SQLSelect() + "WHERE ClosingID = @ClosingID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",MySqlDbType.Int16);
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ClosingDetails Details = new ClosingDetails();

				while (myReader.Read()) 
				{
					Details.ClosingID = ClosingID;
					Details.ClosingNo = "" + myReader["ClosingNo"].ToString();
					Details.ClosingDate = myReader.GetDateTime("ClosingDate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierContact = "" + myReader["SupplierContact"].ToString();
					Details.SupplierAddress = "" + myReader["SupplierAddress"].ToString();
					Details.SupplierTelephoneNo = "" + myReader["SupplierTelephoneNo"].ToString();
					Details.SupplierModeOfTerms = myReader.GetInt16("SupplierModeofTerms");
					Details.SupplierTerms = myReader.GetInt16("SupplierTerms");
					Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
					Details.BranchID = myReader.GetInt16("BranchID");
					Details.BranchCode = "" + myReader["BranchCode"].ToString();
					Details.BranchName = "" + myReader["BranchName"].ToString();
					Details.BranchAddress = "" + myReader["BranchAddress"].ToString();
					Details.TransferredByID = myReader.GetInt64("TransferredByID");
					Details.ClosingSubTotal = myReader.GetDecimal("ClosingSubTotal");
					Details.ClosingDiscount = myReader.GetDecimal("ClosingDiscount");
					Details.ClosingVAT = myReader.GetDecimal("ClosingVAT");
					Details.ClosingVatableAmount = myReader.GetDecimal("ClosingVatableAmount");
                    Details.ClosingStatus = (ClosingStatus)Enum.Parse(typeof(ClosingStatus), myReader.GetString("ClosingStatus"));
					Details.ClosingRemarks = "" + myReader["ClosingRemarks"].ToString();
					Details.SupplierDRNo = "" + myReader["SupplierDRNo"].ToString();
					Details.DeliveryDate = myReader.GetDateTime("DeliveryDate");
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Streams

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblClosing");

			dt.Columns.Add("ClosingID");
			dt.Columns.Add("ClosingNo");
			dt.Columns.Add("ClosingDate");
			dt.Columns.Add("SupplierID");
			dt.Columns.Add("SupplierCode");
			dt.Columns.Add("SupplierContact");
			dt.Columns.Add("SupplierAddress");
			dt.Columns.Add("SupplierTelephoneNo");
			dt.Columns.Add("SupplierModeOfTerms");
			dt.Columns.Add("SupplierTerms");
			dt.Columns.Add("RequiredDeliveryDate");
			dt.Columns.Add("BranchID");
			dt.Columns.Add("BranchCode");
			dt.Columns.Add("BranchName");
			dt.Columns.Add("BranchAddress");
			dt.Columns.Add("TransferredByID");
			dt.Columns.Add("ClosingSubTotal");
			dt.Columns.Add("ClosingDiscount");
			dt.Columns.Add("ClosingVAT");
			dt.Columns.Add("ClosingVatableAmount");
			dt.Columns.Add("ClosingStatus");
			dt.Columns.Add("ClosingRemarks");
			dt.Columns.Add("SupplierDRNo");
			dt.Columns.Add("DeliveryDate");
			
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ClosingID"] = myReader.GetInt32("ClosingID");
				dr["ClosingNo"] = "" + myReader["ClosingNo"].ToString();
				dr["ClosingDate"] = myReader.GetDateTime("ClosingDate");
				dr["SupplierID"] = myReader.GetInt64("SupplierID");
				dr["SupplierCode"] = "" + myReader["SupplierCode"].ToString();
				dr["SupplierContact"] = "" + myReader["SupplierContact"].ToString();
				dr["SupplierAddress"] = "" + myReader["SupplierAddress"].ToString();
				dr["SupplierTelephoneNo"] = "" + myReader["SupplierTelephoneNo"].ToString();
				dr["SupplierModeofTerms"] = myReader.GetInt16("SupplierModeofTerms");
				dr["SupplierTerms"] = myReader.GetInt16("SupplierTerms");
				dr["RequiredDeliveryDate"] = myReader.GetDateTime("RequiredDeliveryDate");
				dr["BranchID"] = myReader.GetInt16("BranchID");
				dr["BranchCode"] = "" + myReader["BranchCode"].ToString();
				dr["BranchName"] = "" + myReader["BranchName"].ToString();
				dr["BranchAddress"] = "" + myReader["BranchAddress"].ToString();
				dr["TransferredByID"] = myReader.GetInt64("TransferredByID");
				dr["ClosingSubTotal"] = myReader.GetDecimal("ClosingSubTotal");
				dr["ClosingDiscount"] = myReader.GetDecimal("ClosingDiscount");
				dr["ClosingVAT"] = myReader.GetDecimal("ClosingVAT");
				dr["ClosingVatableAmount"] = myReader.GetDecimal("ClosingVatableAmount");
				dr["ClosingStatus"] = myReader.GetByte("ClosingStatus");
				dr["ClosingRemarks"] = "" + myReader["ClosingRemarks"].ToString();
				dr["SupplierDRNo"] = "" + myReader["SupplierDRNo"].ToString();
				dr["DeliveryDate"] = myReader.GetDateTime("DeliveryDate");
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}
		public MySqlDataReader List(long ClosingID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(ClosingStatus postatus, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ClosingStatus = @ClosingStatus ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);			
				prmClosingStatus.Value = postatus.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(ClosingStatus postatus, long SupplierID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ClosingStatus =@ClosingStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);			
				prmClosingStatus.Value = postatus.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public MySqlDataReader List(ClosingStatus postatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE ClosingStatus = @ClosingStatus AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY ClosingID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
                prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartDate);

                MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
                prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndDate);

                MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);
                prmClosingStatus.Value = postatus.ToString("d");
                cmd.Parameters.Add(prmClosingStatus);

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE (ClosingNo LIKE @SearchKey or ClosingDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
										"or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(ClosingStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ClosingStatus = @ClosingStatus AND (ClosingNo LIKE @SearchKey or ClosingDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
										"or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingStatus = new MySqlParameter("@ClosingStatus",MySqlDbType.Int16);			
				prmClosingStatus.Value = postatus.ToString("d");
				cmd.Parameters.Add(prmClosingStatus);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		
		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
				stRetValue = clsERPConfig.get_LastClosingNo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void SynchronizeAmount(long ClosingID)
		{
			try 
			{
				string SQL=	"UPDATE tblClosing SET " + 
								"ClosingSubTotal				=	(SELECT SUM(Amount) from tblClosingItems WHERE ClosingID = @ClosingID), " +
								"ClosingDiscount				=	(SELECT SUM(TotalDiscount) from tblClosingItems WHERE ClosingID = @ClosingID), " +
								"ClosingVAT					=	(SELECT SUM(VAT) from tblClosingItems WHERE ClosingID = @ClosingID), " +
								"ClosingVatableAmount		=	(SELECT SUM(VatableAmount) from tblClosingItems WHERE ClosingID = @ClosingID) " +
							"WHERE ClosingID = @ClosingID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",MySqlDbType.Int64);						
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void SynchronizeAmount(string ClosingNo)
		{
			try 
			{
				string SQL=	"UPDATE tblClosing SET " + 
					            "ClosingSubTotal				=	(SELECT SUM(Amount) from tblClosingItems WHERE ClosingID = @ClosingID), " +
					            "ClosingDiscount				=	(SELECT SUM(TotalDiscount) from tblClosingItems WHERE ClosingID = @ClosingID), " +
					            "ClosingVAT					=	(SELECT SUM(VAT) from tblClosingItems WHERE ClosingID = @ClosingID), " +
					            "ClosingVatableAmount		=	(SELECT SUM(VatableAmount) from tblClosingItems WHERE ClosingID = @ClosingID) " +
					        "WHERE ClosingNo = @ClosingNo;";
        				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingNo = new MySqlParameter("@ClosingNo",MySqlDbType.String);			
				prmClosingNo.Value = ClosingNo;
				cmd.Parameters.Add(prmClosingNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

	}
}

