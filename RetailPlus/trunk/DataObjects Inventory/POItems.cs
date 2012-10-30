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

	#region POItemDetails

	public struct POItemDetails
	{
		public long POItemID;
		public long POID;
		public long ProductID;
		public string ProductCode;
		public string BarCode;
		public string Description;
		public int ProductUnitID;
		public string ProductUnitCode;
		public decimal Quantity;
		public decimal UnitCost;
		public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
		public decimal Amount;
		public decimal VAT;
		public decimal VatableAmount;
		public decimal EVAT;
		public decimal EVatableAmount;
		public decimal LocalTax;
        public bool isVATInclusive;
		public long VariationMatrixID;
		public string MatrixDescription;
		public string ProductGroup;
		public string ProductSubGroup;
		public POItemStatus POItemStatus;
		public bool IsVatable;
		public string Remarks;
        public int ChartOfAccountIDPurchase;
        public int ChartOfAccountIDTaxPurchase;
        public int ChartOfAccountIDInventory;

        // Added Dec 31, 2009 6:49PM
        public decimal SellingPrice;
        public decimal SellingVAT;
        public decimal SellingEVAT;
        public decimal SellingLocalTax;
        // Added April 28, 2010 6:49PM
        public decimal OldSellingPrice;
        // Aug 6, 2011 RequiredInventoryDays
        public long RID;

        public decimal OriginalQuantity;
        public POItemReceivedStatus POItemReceivedStatus;
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
	public class POItem
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		public MySqlConnection Connection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction Transaction
		{
			get { return mTransaction;	}
		}


		#region Constructors and Destructors

		public POItem()
		{
			
		}

		public POItem(MySqlConnection Connection, MySqlTransaction Transaction)
		{
			mConnection = Connection;
			mTransaction = Transaction;
			
		}

		public void CommitAndDispose() 
		{
			if (!TransactionFailed)
			{
				if (IsInTransaction)
				{
					mTransaction.Commit();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}
			}
		}

		public MySqlConnection GetConnection()
		{
			if (mConnection==null)
			{
				mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
				mConnection.Open();
				
				mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
			}

			IsInTransaction = true;
			return mConnection;
		} 


		#endregion

		#region Insert and Update

		public long Insert(POItemDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPOItems (" +
								"POID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
                                "OriginalQuantity, " +
								"UnitCost, " +
								"Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
                                "isVATInclusive, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"POItemStatus, " +
								"IsVatable, " +
								"Remarks, " +
                                "ChartOfAccountIDPurchase, " +
                                "ChartOfAccountIDTaxPurchase, " +
                                "ChartOfAccountIDInventory," +
                                "SellingPrice," +
                                "SellingVAT," +
                                "SellingEVAT," +
                                "SellingLocalTax," +
                                "OldSellingPrice," +
                                "RID" +
							") VALUES (" +
								"@POID, " +
								"@ProductID, " +
								"@ProductCode, " +
								"@BarCode, " +
								"@Description, " +
								"@ProductUnitID, " +
								"@ProductUnitCode, " +
								"@Quantity, " +
                                "@Quantity, " +
								"@UnitCost, " +
								"@Discount, " +
                                "@DiscountApplied, " +
                                "@DiscountType, " +
								"@Amount, " +
								"@VAT, " +
								"@VatableAmount, " +
								"@EVAT, " +
								"@EVatableAmount, " +
								"@LocalTax, " +
                                "@isVATInclusive, " +
								"@VariationMatrixID, " +
								"@MatrixDescription, " +
								"@ProductGroup, " +
								"@ProductSubGroup, " +
								"@POItemStatus, " +
								"@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID)," +
                                "@SellingPrice," +
                                "@SellingVAT," +
                                "@SellingEVAT," +
                                "@SellingLocalTax," +
                                "@OldSellingPrice," +
                                "@RID" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);
				prmPOID.Value = Details.POID;
				cmd.Parameters.Add(prmPOID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
								 
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);
		 
				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
				prmBarCode.Value = Details.BarCode;
				cmd.Parameters.Add(prmBarCode);			 
				
				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);	

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);	

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",System.Data.DbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",System.Data.DbType.Decimal);			
				prmUnitCost.Value = Details.UnitCost;
				cmd.Parameters.Add(prmUnitCost);
					
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",System.Data.DbType.Decimal);			
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);			
				prmDiscountType.Value = (int) Details.DiscountType;
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
								 
				MySqlParameter prmVAT = new MySqlParameter("@VAT",System.Data.DbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);			 

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",System.Data.DbType.Decimal);			
				prmVatableAmount.Value = Details.VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",System.Data.DbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);			 

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",System.Data.DbType.Decimal);			
				prmEVatableAmount.Value = Details.EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",System.Data.DbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

                MySqlParameter prmisVATInclusive = new MySqlParameter("@isVATInclusive",MySqlDbType.Int16);
                prmisVATInclusive.Value = Convert.ToInt16(Details.isVATInclusive);
                cmd.Parameters.Add(prmisVATInclusive);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",System.Data.DbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);			
				prmMatrixDescription.Value = Details.MatrixDescription;
				cmd.Parameters.Add(prmMatrixDescription);	

				MySqlParameter prmProductGroup = new MySqlParameter("@ProductGroup",MySqlDbType.String);			
				prmProductGroup.Value = Details.ProductGroup;
				cmd.Parameters.Add(prmProductGroup);

                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@POItemStatus", Details.POItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);
                cmd.Parameters.AddWithValue("@RID", Details.RID);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

                PO clsPO = new PO(Connection, Transaction);
                clsPO.SynchronizeAmount(Details.POID);

				return iID;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		public void Update(POItemDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblPOItems SET " + 
								"POID					=	@POID, " +
								"ProductID				=	@ProductID, " +
								"ProductCode			=	@ProductCode, " +
								"BarCode				=	@BarCode, " +
								"Description			=	@Description, " +
								"ProductUnitID			=	@ProductUnitID, " +
								"ProductUnitCode		=	@ProductUnitCode, " +
								"Quantity				=	@Quantity, " +
                                "OriginalQuantity       =   @Quantity, " +
								"UnitCost				=	@UnitCost, " +
								"Discount				=	@Discount, " +
                                "DiscountApplied		=	@DiscountApplied, " +
                                "DiscountType			=	@DiscountType, " +
								"Amount					=	@Amount, " +
								"VAT					=	@VAT, " +
								"VatableAmount			=	@VatableAmount, " +
								"EVAT					=	@EVAT, " +
								"EVatableAmount			=	@EVatableAmount, " +
								"LocalTax				=	@LocalTax, " +
                                "isVATInclusive			=	@isVATInclusive, " +
								"VariationMatrixID		=	@VariationMatrixID, " +
								"MatrixDescription		=	@MatrixDescription, " +
								"ProductGroup			=	@ProductGroup, " +
								"ProductSubGroup		=	@ProductSubGroup, " +
								"POItemStatus			=	@POItemStatus, " +
								"IsVatable				=	@IsVatable, " +
								"Remarks				=	@Remarks, " +
                                "ChartOfAccountIDPurchase       = (SELECT ChartOfAccountIDPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxPurchase    = (SELECT ChartOfAccountIDTaxPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory      = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID), " +
                                "SellingPrice			=	@SellingPrice, " +
                                "SellingVAT				=	@SellingVAT, " +
                                "SellingEVAT			=	@SellingEVAT, " +
                                "SellingLocalTax		=	@SellingLocalTax, " +
                                "OldSellingPrice		=	@OldSellingPrice, " +
                                "RID		            =	@RID " +
							"WHERE POItemID = @POItemID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);
				prmPOID.Value = Details.POID;
				cmd.Parameters.Add(prmPOID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
								 
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);
		 
				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
				prmBarCode.Value = Details.BarCode;
				cmd.Parameters.Add(prmBarCode);			 
				
				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);	

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);	

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",System.Data.DbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",System.Data.DbType.Decimal);			
				prmUnitCost.Value = Details.UnitCost;
				cmd.Parameters.Add(prmUnitCost);
					
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",System.Data.DbType.Decimal);			
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = (int)Details.DiscountType;
                cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
								 
				MySqlParameter prmVAT = new MySqlParameter("@VAT",System.Data.DbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);			 

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",System.Data.DbType.Decimal);			
				prmVatableAmount.Value = Details.VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",System.Data.DbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);			 

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",System.Data.DbType.Decimal);			
				prmEVatableAmount.Value = Details.EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",System.Data.DbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

                MySqlParameter prmisVATInclusive = new MySqlParameter("@isVATInclusive",MySqlDbType.Int16);
                prmisVATInclusive.Value = Convert.ToInt16(Details.isVATInclusive);
                cmd.Parameters.Add(prmisVATInclusive);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",System.Data.DbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);			
				prmMatrixDescription.Value = Details.MatrixDescription;
				cmd.Parameters.Add(prmMatrixDescription);	

				MySqlParameter prmProductGroup = new MySqlParameter("@ProductGroup",MySqlDbType.String);			
				prmProductGroup.Value = Details.ProductGroup;
				cmd.Parameters.Add(prmProductGroup);

                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@POItemStatus", Details.POItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);
                cmd.Parameters.AddWithValue("@RID", Details.RID);
                cmd.Parameters.AddWithValue("@POItemID", Details.POItemID);

				cmd.ExecuteNonQuery();

                PO clsPO = new PO(Connection, Transaction);
                clsPO.SynchronizeAmount(Details.POID);
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

        public void UpdateReceiveStatus(long POItemID, POItemReceivedStatus POItemReceivedStatus, decimal ReceivedQuantity)
        {
            try
            {
                string SQL = "UPDATE tblPOItems SET " +
                                "POItemReceivedStatus   =   @POItemReceivedStatus, " +
                                "OriginalQuantity       =   Quantity, " +
                                "Quantity               =   @ReceivedQuantity " +
                            "WHERE POItemID = @POItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOItemReceivedStatus = new MySqlParameter("@POItemReceivedStatus",MySqlDbType.Int16);
                prmPOItemReceivedStatus.Value = POItemReceivedStatus.ToString("d");
                cmd.Parameters.Add(prmPOItemReceivedStatus);

                MySqlParameter prmReceivedQuantity = new MySqlParameter("@ReceivedQuantity", System.Data.DbType.Decimal);
                prmReceivedQuantity.Value = ReceivedQuantity;
                cmd.Parameters.Add(prmReceivedQuantity);

                MySqlParameter prmPOItemID = new MySqlParameter("@POItemID",MySqlDbType.Int64);
                prmPOItemID.Value = POItemID;
                cmd.Parameters.Add(prmPOItemID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

		public void Post(long POID)
		{
			try 
			{
				string SQL=	"UPDATE tblPOItems SET " + 
								"POItemStatus			=	@POItemStatus, " +
                                "POItemReceivedStatus   =   @POItemReceivedStatus " +
							"WHERE POID = @POID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOItemStatus = new MySqlParameter("@POItemStatus",MySqlDbType.Int16);			
				prmPOItemStatus.Value = POItemStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmPOItemStatus);

                MySqlParameter prmPOItemReceivedStatus = new MySqlParameter("@POItemReceivedStatus",MySqlDbType.Int16);
                prmPOItemReceivedStatus.Value = POItemReceivedStatus.Received.ToString("d");
                cmd.Parameters.Add(prmPOItemReceivedStatus);

				MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);
				prmPOID.Value = POID;
				cmd.Parameters.Add(prmPOID);

				cmd.ExecuteNonQuery();
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		public void Cancel(long POID)
		{
			try 
			{
				string SQL=	"UPDATE tblPOItems SET " + 
								"POItemStatus			=	@POItemStatus, " +
                                "POItemReceivedStatus   =   @POItemReceivedStatus " +
							"WHERE POID = @POID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOItemStatus = new MySqlParameter("@POItemStatus",MySqlDbType.Int16);			
				prmPOItemStatus.Value = POItemStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmPOItemStatus);

                MySqlParameter prmPOItemReceivedStatus = new MySqlParameter("@POItemReceivedStatus",MySqlDbType.Int16);
                prmPOItemReceivedStatus.Value = POItemReceivedStatus.NotYetReceived.ToString("d");
                cmd.Parameters.Add(prmPOItemReceivedStatus);

				MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);
				prmPOID.Value = POID;
				cmd.Parameters.Add(prmPOID);

				cmd.ExecuteNonQuery();
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		
		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPOItems WHERE POItemID IN (" + IDs + ");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.ExecuteNonQuery();

				return true;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
								"POItemID, " +
								"POID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
                                "OriginalQuantity, " +
								"UnitCost, " +
								"Discount, " +
                                "DiscountApplied, " +
								"DiscountType, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
                                "isVATInclusive, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"POItemStatus, " +
                                "POItemReceivedStatus, " +
								"IsVatable, " +
								"Remarks, " +
                                "ChartOfAccountIDPurchase, " +
                                "ChartOfAccountIDTaxPurchase, " +
                                "ChartOfAccountIDInventory, " +
                                "SellingPrice, " +
                                "SellingVAT, " +
                                "SellingEVAT, " +
                                "SellingLocalTax, " +
                                "OldSellingPrice, " +
                                "RID " +
							"FROM tblPOItems ";
            return stSQL;
        }

		#region Details

		public POItemDetails Details(long POItemID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE POItemID = @POItemID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPOItemID = new MySqlParameter("@POItemID",System.Data.DbType.Int64);
				prmPOItemID.Value = POItemID;
				cmd.Parameters.Add(prmPOItemID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				POItemDetails Details = new POItemDetails();

				while (myReader.Read()) 
				{
					Details.POItemID = POItemID;
					Details.POID = myReader.GetInt64("POID");
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.BarCode = "" + myReader["BarCode"].ToString();
					Details.Description = "" + myReader["Description"].ToString();
					Details.ProductUnitID = myReader.GetInt16("ProductUnitID");
					Details.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
					Details.Quantity = myReader.GetDecimal("Quantity");
                    Details.OriginalQuantity = myReader.GetDecimal("OriginalQuantity");
					Details.UnitCost = myReader.GetDecimal("UnitCost");
					Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes) Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
					Details.Amount = myReader.GetDecimal("Amount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.VatableAmount = myReader.GetDecimal("VatableAmount");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.isVATInclusive = myReader.GetBoolean("isVATInclusive");
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
					Details.ProductGroup = "" + myReader["ProductGroup"].ToString();
					Details.ProductSubGroup = "" + myReader["ProductSubGroup"].ToString();
					Details.POItemStatus = (POItemStatus) Enum.Parse(typeof(POItemStatus), myReader.GetString("POItemStatus"));
                    Details.POItemReceivedStatus = (POItemReceivedStatus)Enum.Parse(typeof(POItemReceivedStatus), myReader.GetString("POItemReceivedStatus"));
                    
					if (myReader["IsVatable"].ToString() == "1")
						Details.IsVatable = true;
					Details.Remarks = "" + myReader["Remarks"].ToString();

                    //Added Jan 1, 2010 4:20PM : For selling information
                    Details.SellingPrice = myReader.GetDecimal("SellingPrice");
                    Details.SellingVAT = myReader.GetDecimal("SellingVAT");
                    Details.SellingEVAT = myReader.GetDecimal("SellingEVAT");
                    Details.SellingLocalTax = myReader.GetDecimal("SellingLocalTax");

                    //Added April 28, 2010 4:20PM : For selling information
                    Details.OldSellingPrice = myReader.GetDecimal("OldSellingPrice");

                    //Aug 9, 2011 : For Requried Inventory Days
                    Details.RID = myReader.GetInt64("RID");
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTable(long POID = 0, string SortField = "POItemID", SortOption SortOrder = SortOption.Desscending)
        {
            MySqlCommand cmd = new MySqlCommand();
            string SQL = SQLSelect();

            if (POID != 0)
            {
                SQL += "WHERE POID = @POID ";

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);
            }

            SQL += "ORDER BY " + SortField;
            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();

            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("POItems");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;

        }
        
		public MySqlDataReader List(long POID = 0, string SortField = "POItemID", SortOption SortOrder = SortOption.Desscending)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect();

                if (POID != 0)
                {
                    SQL += "WHERE POID = @POID ";

                    MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                    prmPOID.Value = POID;
                    cmd.Parameters.Add(prmPOID);
                }

                SQL += "ORDER BY " + SortField;
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlConnection cn = GetConnection();

				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}
		public MySqlDataReader List(POItemStatus POItemstatus, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POItemID";

				string SQL = SQLSelect() + "WHERE POItemStatus = @POItemStatus " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@POItemStatus", POItemstatus.ToString("d"));

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POItemID";

				string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
										"or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
										"or Remarks LIKE @SearchKey) " +
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}		
		public MySqlDataReader Search(POItemStatus POItemstatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POItemID";

				string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
									"or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
									"or Remarks LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@POItemStatus", POItemstatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}		
		
		#endregion
	}
}

