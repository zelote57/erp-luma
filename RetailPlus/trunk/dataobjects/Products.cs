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
	#region Struct

	public struct ProductDetails
	{
		public Int64 ProductID;
		public string ProductCode;
		public string BarCode;
        public string BarCode1;
		public string BarCode2;
		public string BarCode3;
		public string ProductDesc;
		public Int64 ProductGroupID;
		public string ProductGroupCode;
		public string ProductGroupName;
		public Int64 ProductSubGroupID;
		public string ProductSubGroupCode;
		public string ProductSubGroupName;
		public Int32 BaseUnitID;
		public string BaseUnitCode;
		public string BaseUnitName;
		public string UnitCode;
		public string UnitName;
		public DateTime DateCreated;
		public bool Deleted;
		public decimal Price;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
		public decimal Quantity;
		public string ConvertedQuantity;
		public decimal MinThreshold;
		public decimal MaxThreshold;
		public long RID;
		public Int64 SupplierID;
		public string SupplierCode;
		public string SupplierName;
		public OrderSlipPrinter OrderSlipPrinter;
		public int ChartOfAccountIDPurchase;
		public int ChartOfAccountIDSold;
		public int ChartOfAccountIDInventory;
		public int ChartOfAccountIDTaxPurchase;
		public int ChartOfAccountIDTaxSold;
		public bool IsItemSold;
		public bool WillPrintProductComposition;
		public long UpdatedBy;
		public DateTime UpdatedOn;
		public bool Active;
		/**
		 * Feb 26,2010
		 **/
		public decimal PercentageCommision;
		/**
		 * May 10,2010
		 **/
		public decimal QuantityIN;
		public decimal QuantityOUT;
		/**
		 * July 1,2010
		 **/
		public decimal WSPrice;
		/**
		 * June 11,2011
		 **/
		public decimal ActualQuantity;
		/**
		 * Oct 17,2011
		 **/
		public decimal RewardPoints;

		public long SequenceNo;
		public int BranchID;

        public long MatrixID;
        public string MatrixDescription;

        public long PackageID;

        public bool IsLock;
	}

	/// <summary>
	/// Use for selecting the required columns for select.
	/// Column value should be equal to TRUE if will be included in the select statement
	/// </summary>
	public struct ProductColumns
	{
		public bool IncludeAllPackages;
		public bool ProductID;
		public bool ProductCode;
		public bool BarCode;
        public bool BarCode1;
		public bool BarCode2;
		public bool BarCode3;
		public bool ProductDesc;
		public bool ProductGroupID;
		public bool ProductGroupCode;
		public bool ProductGroupName;
		public bool ProductSubGroupID;
		public bool ProductSubGroupCode;
		public bool ProductSubGroupName;
		public bool BaseUnitID;
		public bool BaseUnitCode;
		public bool BaseUnitName;
		public bool UnitID;
		public bool UnitCode;
		public bool UnitName;
		public bool DateCreated;
		public bool Deleted;
		public bool Price;
		public bool PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public bool VAT;
		public bool EVAT;
		public bool LocalTax;
		public bool MinThreshold;
		public bool MaxThreshold;
		public bool RID;
		public bool SupplierID;
		public bool SupplierCode;
		public bool SupplierName;
		public bool OrderSlipPrinter;
		public bool ChartOfAccountIDPurchase;
		public bool ChartOfAccountIDSold;
		public bool ChartOfAccountIDInventory;
		public bool ChartOfAccountIDTaxPurchase;
		public bool ChartOfAccountIDTaxSold;
		public bool IsItemSold;
		public bool WillPrintProductComposition;
		public bool UpdatedBy;
		public bool UpdatedOn;
		public bool Active;
        public bool IsLock;
		public bool PercentageCommision;
		public bool WSPrice;
		public bool VariationCount;
		public bool Quantity;
		public bool QuantityIN;
		public bool QuantityOUT;
		public bool ActualQuantity;
		public bool ReorderQty;
		public bool RIDMinThreshold;
		public bool RIDMaxThreshold;
		public bool RIDReorderQty;
		public bool BranchID;
		public bool RewardPoints;
		public bool SequenceNo;
	}

	public struct ProductColumnNames
	{
		public const string ProductID = "ProductID";
		public const string ProductCode = "ProductCode";
        public const string PackageID = "PackageID";
        public const string BarCode = "BarCode";
        public const string BarCode1 = "BarCode1";
		public const string BarCode2 = "BarCode2";
		public const string BarCode3 = "BarCode3";
        public const string BarCode4 = "BarCode4";
		public const string ProductDesc = "ProductDesc";
		public const string ProductGroupID = "ProductGroupID";
		public const string ProductGroupCode = "ProductGroupCode";
		public const string ProductGroupName = "ProductGroupName";
		public const string ProductSubGroupID = "ProductSubGroupID";
		public const string ProductSubGroupCode = "ProductSubGroupCode";
		public const string ProductSubGroupName = "ProductSubGroupName";
		public const string BaseUnitID = "BaseUnitID";
		public const string BaseUnitCode = "BaseUnitCode";
		public const string BaseUnitName = "BaseUnitName";
		public const string UnitID = "UnitID";
		public const string UnitCode = "UnitCode";
		public const string UnitName = "UnitName";
		public const string DateCreated = "DateCreated";
		public const string Deleted = "Deleted";
		public const string Price = "Price";
		public const string PurchasePrice = "PurchasePrice";
		public const string IncludeInSubtotalDiscount = "IncludeInSubtotalDiscount";
		public const string VAT = "VAT";
		public const string EVAT = "EVAT";
		public const string LocalTax = "LocalTax";
		public const string MinThreshold = "MinThreshold";
		public const string MaxThreshold = "MaxThreshold";
		public const string RID = "RID";
		public const string SupplierID = "SupplierID";
		public const string SupplierCode = "SupplierCode";
		public const string SupplierName = "SupplierName";
		public const string OrderSlipPrinter = "OrderSlipPrinter";
		public const string ChartOfAccountIDPurchase = "ChartOfAccountIDPurchase";
		public const string ChartOfAccountIDSold = "ChartOfAccountIDSold";
		public const string ChartOfAccountIDInventory = "ChartOfAccountIDInventory";
		public const string ChartOfAccountIDTaxPurchase = "ChartOfAccountIDTaxPurchase";
		public const string ChartOfAccountIDTaxSold = "ChartOfAccountIDTaxSold";
		public const string IsItemSold = "IsItemSold";
		public const string WillPrintProductComposition = "WillPrintProductComposition";
		public const string UpdatedBy = "UpdatedBy";
		public const string UpdatedOn = "UpdatedOn";
		public const string Active = "Active";
        public const string IsLock = "IsLock";
		public const string PercentageCommision = "PercentageCommision";
		public const string WSPrice = "WSPrice";
		public const string VariationCount = "VariationCount";
		public const string Quantity = "Quantity";
		public const string ConvertedQuantity = "ConvertedQuantity";
		public const string QuantityIN = "QuantityIN";
		public const string QuantityOUT = "QuantityOUT";
		public const string ActualQuantity = "ActualQuantity";
		public const string ConvertedActualQuantity = "ConvertedActualQuantity";
		public const string ReorderQty = "ReorderQty";
		public const string RIDMinThreshold = "RIDMinThreshold";
		public const string RIDMaxThreshold = "RIDMaxThreshold";
		public const string RIDReorderQty = "RIDReorderQty";
		public const string BranchID = "BranchID";
		public const string RewardPoints = "RewardPoints";
		public const string SequenceNo = "SequenceNo";
	}

	public enum PRODUCT_INVENTORY_MOVEMENT
	{
		ADD_PURCHASE,
		ADD_TRANSFER_IN,
        ADD_BRANCH_TRANSFER_TO,
		ADD_STOCK_INVENTORY,
		ADD_INVENTORY_ADJUSTMENT,
		ADD_RETURN_ITEM,
		ADD_REFUND_ITEM,
		ADD_SALES_RETURN,
		ADD_RESERVE_AND_COMMIT_VOID_ITEM,
		ADD_RESERVE_AND_COMMIT_CHANGE_QTY,
		ADD_PRODUCT_VARIATION_CREATION,
		DEDUCT_PURCHASE_RETURN,
		DEDUCT_SOLD_RETAIL,
		DEDUCT_SOLD_WHOLESALE,
		DEDUCT_TRANSFER_OUT,
        DEDUCT_BRANCH_TRANSFER_FROM,
		DEDUCT_STOCK_INVENTORY,
		DEDUCT_INVENTORY_ADJUSTMENT,
		DEDUCT_QTY_RESERVE_AND_COMMIT_VOID_ITEM,
		DEDUCT_QTY_RESERVE_AND_COMMIT_RETURN_ITEM,
		DEDUCT_PRODUCT_VARIATION_DELETE,
		SYS_AUTO_ADJ_OF_MATRIX_QTY_FROM_PRODUCT_QTY_AS_BASIS,
		SYS_AUTO_ADJ_OF_PRODUCT_QTY_FROM_SUM_OF_MATRIX_QTY_AS_BASIS,
        PARKING_IN,
        PARKING_OUT
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
	public class Products : POSConnection
	{
		public const string DEFAULT_CREDIT_PAYMENT_BARCODE = "CREDIT PAYMENT";
		public const string DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE = "ADVNTGE CARD - MEMBERSHIP FEE";
		public const string DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE = "ADVNTGE CARD - RENEWAL FEE";
		public const string DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE = "ADVNTGE CARD - REPLACEMENT FEE";
		public const string DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE = "CREDIT CARD - MEMBERSHIP FEE";
		public const string DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE = "CREDIT CARD - RENEWAL FEE";
		public const string DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE = "CREDIT CARD - REPLACEMENT FEE";
		public const string DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE = "SUPER CARD - MEMBERSHIP FEE";
		public const string DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE = "SUPER CARD - RENEWAL FEE";
		public const string DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE = "SUPER CARD - REPLACEMENT FEE";

		public const int DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT = 7;

		#region Constructors and Destructors

		public Products()
            : base(null, null)
        {
        }

        public Products(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}
		
		#endregion

		#region Insert and Update
		public Int64 Insert(ProductDetails Details)
		{
			try  
			{
                if (IsExist(Details.ProductID, Details.BarCode, Details.BarCode2, Details.BarCode3))
                {
                    throw new EntryPointNotFoundException("This product already exist, please check the barcode.");
                }

				string SQL =	"INSERT INTO tblProducts (" +
									"ProductCode, " + 
									"ProductDesc, " +  
									"ProductSubGroupID, " + 
									"BaseUnitID, " + 
									"DateCreated, " +
									"PercentageCommision, " +
									"IncludeInSubtotalDiscount, " +
									"MinThreshold, " +
									"MaxThreshold, " +
									"SupplierID, " +
									"IsItemSold, " +
									"WillPrintProductComposition"+
								") VALUES (" +
									"@ProductCode, " + 
									"@ProductDesc, " +   
									"@ProductSubGroupID, " + 
									"@BaseUnitID, " + 
									"@DateCreated," + 
									"@PercentageCommision, " +
									"@IncludeInSubtotalDiscount, " +
									"@MinThreshold, " +
									"@MaxThreshold, " +
									"@SupplierID, " +
									"@IsItemSold, " +
									"@WillPrintProductComposition);"; 

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmProductDesc = new MySqlParameter("@ProductDesc",MySqlDbType.String);			
				prmProductDesc.Value = Details.ProductDesc;
				cmd.Parameters.Add(prmProductDesc);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);			
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.String);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);			
				prmDateCreated.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateCreated);

				MySqlParameter prmPercentageCommision = new MySqlParameter("@PercentageCommision",MySqlDbType.Decimal);			
				prmPercentageCommision.Value = Details.PercentageCommision;
				cmd.Parameters.Add(prmPercentageCommision);

				MySqlParameter prmIncludeInSubtotalDiscount = new MySqlParameter("@IncludeInSubtotalDiscount",MySqlDbType.Int16);			
				prmIncludeInSubtotalDiscount.Value = Details.IncludeInSubtotalDiscount;
				cmd.Parameters.Add(prmIncludeInSubtotalDiscount);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Decimal);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Decimal);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);			
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmIsItemSold = new MySqlParameter("@IsItemSold",MySqlDbType.Int16);
				prmIsItemSold.Value = Convert.ToInt16(Details.IsItemSold);
				cmd.Parameters.Add(prmIsItemSold);

				MySqlParameter prmWillPrintProductComposition = new MySqlParameter("@WillPrintProductComposition",MySqlDbType.Int16);
				prmWillPrintProductComposition.Value = Convert.ToInt16(Details.WillPrintProductComposition);
				cmd.Parameters.Add(prmWillPrintProductComposition);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				ProductPackageDetails clsProductPackageDetails = new ProductPackageDetails();
				clsProductPackageDetails.ProductID = iID;
				clsProductPackageDetails.BarCode1 = Details.BarCode;
				clsProductPackageDetails.BarCode2 = Details.BarCode2;
				clsProductPackageDetails.BarCode3 = Details.BarCode3;
				clsProductPackageDetails.UnitID = Details.BaseUnitID;
				clsProductPackageDetails.Price = Details.Price;
				clsProductPackageDetails.WSPrice = Details.WSPrice;
                clsProductPackageDetails.PurchasePrice = Details.PurchasePrice;
                clsProductPackageDetails.Quantity = 1;
				clsProductPackageDetails.VAT = Details.VAT;
				clsProductPackageDetails.EVAT = Details.EVAT;
				clsProductPackageDetails.LocalTax = Details.LocalTax;

				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
				clsProductPackage.Insert(clsProductPackageDetails);

				// Oct 28, 2011 : Lemu
				// Added to cater branch inventory
                //BranchInventory clsBranchInventory = new BranchInventory(base.Connection, base.Transaction);
                //clsBranchInventory.CreateToAllBranches(iID);

				//base.ExecuteNonQuery(cmd);
				
				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductDetails Details)
		{
			try 
			{
                if (IsExist(Details.ProductID, Details.BarCode, Details.BarCode2, Details.BarCode3))
                {
                    throw new Exception("This product already exist, please check the barcode.");
                }

				string SQL =	"UPDATE tblProducts SET " +
									"ProductCode		= @ProductCode, " + 
									"ProductDesc		= @ProductDesc, " +  
									"ProductSubGroupID	= @ProductSubGroupID, " + 
									"BaseUnitID			= @BaseUnitID, " +
									"PercentageCommision        =   @PercentageCommision, " +
									"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
									"MinThreshold		= @MinThreshold, " +
									"MaxThreshold		= @MaxThreshold, " +
									"SupplierID			= @SupplierID, " +
									"IsItemSold			= @IsItemSold, " +
									"WillPrintProductComposition    =   @WillPrintProductComposition " +
								"WHERE ProductID	= @ProductID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmProductDesc = new MySqlParameter("@ProductDesc",MySqlDbType.String);			
				prmProductDesc.Value = Details.ProductDesc;
				cmd.Parameters.Add(prmProductDesc);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);						
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);

				MySqlParameter prmIncludeInSubtotalDiscount = new MySqlParameter("@IncludeInSubtotalDiscount",MySqlDbType.Int16);			
				prmIncludeInSubtotalDiscount.Value = Details.IncludeInSubtotalDiscount;
				cmd.Parameters.Add(prmIncludeInSubtotalDiscount);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Decimal);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Decimal);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);			
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmIsItemSold = new MySqlParameter("@IsItemSold",MySqlDbType.Int16);
				prmIsItemSold.Value = Convert.ToInt16(Details.IsItemSold);
				cmd.Parameters.Add(prmIsItemSold);

				MySqlParameter prmWillPrintProductComposition = new MySqlParameter("@WillPrintProductComposition",MySqlDbType.Int16);
				prmWillPrintProductComposition.Value = Convert.ToInt16(Details.WillPrintProductComposition);
				cmd.Parameters.Add(prmWillPrintProductComposition);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				base.ExecuteNonQuery(cmd);

				ProductPackageDetails clsDetails = new ProductPackageDetails();
				clsDetails.ProductID = Details.ProductID;
				clsDetails.Quantity = 1;
				clsDetails.Price = Details.Price;
				clsDetails.WSPrice = Details.WSPrice;
                clsDetails.PurchasePrice = Details.PurchasePrice;
				clsDetails.VAT = Details.VAT;
				clsDetails.EVAT = Details.EVAT;
				clsDetails.LocalTax = Details.LocalTax;
				clsDetails.UnitID = Details.BaseUnitID;
				clsDetails.UnitCode = Details.BaseUnitCode;
				clsDetails.UnitName = Details.BaseUnitName;
				clsDetails.BarCode1 = Details.BarCode;
				clsDetails.BarCode2 = Details.BarCode2;
				clsDetails.BarCode3 = Details.BarCode3;

				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
				clsProductPackage.UpdateByProductIDUnitIDAndQuantity(clsDetails, Details.UpdatedBy, Details.UpdatedOn, "Edit product module.");
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void UpdateBarcode(long ProductID, string Barcode)
		{
			try
			{
                if (IsExist(ProductID, Barcode, Barcode, Barcode))
                {
                    throw new EntryPointNotFoundException("This product already exist, please check the barcode.");
                }

                //string SQL = "UPDATE tblProducts SET " +
                //                    "BarCode			= @BarCode " +
                //                "WHERE ProductID	= @ProductID;";

                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = SQL;

                //cmd.Parameters.AddWithValue("@BarCode", Barcode);
                //cmd.Parameters.AddWithValue("@ProductID", ProductID);

                //base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void UpdateVariationCount(long ProductID)
		{
			// Added August 2, 2009 to monitor if product still has/have variations
			try
			{
				string SQL = "CALL procProductVariationCountUpdate(@ProductID);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void UpdateInvDetails(int BranchID, long ProductID, long MatrixID, decimal QuantityNow, decimal MinThresholdNow, decimal MaxThresholdNow, string Remarks, DateTime TransactionDate, string TransactionNo, string AdjustedBy)
		{
			try
			{
                string SQL = "CALL procProductUpdateInvDetails(@BranchID, @ProductID, @MatrixID, @QuantityNow, @MinThresholdNow, @MaxThresholdNow, @strRemarks, @dteTransactionDate, @strTransactionNo, @AdjustedBy);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
				cmd.Parameters.AddWithValue("@QuantityNow", QuantityNow);
				cmd.Parameters.AddWithValue("@MinThresholdNow", MinThresholdNow);
				cmd.Parameters.AddWithValue("@MaxThresholdNow", MaxThresholdNow);
                cmd.Parameters.AddWithValue("@strRemarks", Remarks);
                cmd.Parameters.AddWithValue("@dteTransactionDate", TransactionDate);
                cmd.Parameters.AddWithValue("@strTransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@AdjustedBy", AdjustedBy);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdatePurchasing(long ProductID, long MatrxID, long SupplierID, int BaseUnitID, decimal UnitCost)
		{
			try 
			{
				string SQL =	"UPDATE tblProducts SET " +
									"SupplierID		= @SupplierID " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

				base.ExecuteNonQuery(cmd);

				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
                clsProductPackage.UpdatePurchasing(ProductID, MatrxID, BaseUnitID, 1, UnitCost);

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateSellingPrice(long ProductID, long MatrixID, long SupplierID, int BaseUnitID, decimal SellingPrice)
		{
			try 
			{
				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
                clsProductPackage.UpdateSelling(ProductID, MatrixID, BaseUnitID, 1, SellingPrice);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdateSellingWSPrice(long ProductID, long MatrixID, long SupplierID, int BaseUnitID, decimal WholesalePrice)
		{
			try
			{
				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
                clsProductPackage.UpdateSellingWSPrice(ProductID, MatrixID, BaseUnitID, 1, WholesalePrice);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdateCommision(long ProductID, decimal PercentageCommision)
		{
			// Added March 1, 2010
			try
			{
				string SQL = "CALL procProductCommisionUpdate(@ProductID, @PercentageCommision);";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@PercentageCommision", PercentageCommision);

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		public void UpdateFinancialInformation(int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
									"ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
									"ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDPurchase);

				MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
				prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
				cmd.Parameters.Add(prmChartOfAccountIDSold);

				MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
				prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
				cmd.Parameters.Add(prmChartOfAccountIDInventory);

				MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

				MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
				prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
				cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		public void UpdateFinancialInformation(long ProductID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
									"ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
									"ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
								"WHERE ProductID	= @ProductID; ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDPurchase);

				MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
				prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
				cmd.Parameters.Add(prmChartOfAccountIDSold);

				MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
				prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
				cmd.Parameters.Add(prmChartOfAccountIDInventory);

				MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

				MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
				prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
				cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		public void UpdateFinancialInformationByGroup(long ProductGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
									"ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
									"ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
								"WHERE ProductSubGroupID in (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID); ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDPurchase);

				MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
				prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
				cmd.Parameters.Add(prmChartOfAccountIDSold);

				MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
				prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
				cmd.Parameters.Add(prmChartOfAccountIDInventory);

				MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

				MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
				prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
				cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
				prmProductGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		public void UpdateFinancialInformationBySubGroup(long ProductSubGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
									"ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
									"ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
									"ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
									"ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
									"ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
								"WHERE ProductSubGroupID = @ProductSubGroupID; ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDPurchase);

				MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
				prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
				cmd.Parameters.Add(prmChartOfAccountIDSold);

				MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
				prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
				cmd.Parameters.Add(prmChartOfAccountIDInventory);

				MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
				prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
				cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

				MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
				prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
				cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
				prmProductSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		public void TagActive(long ProductID)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				TagActiveInActive(ProductID, true);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void TagInactive(long ProductID)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				TagActiveInActive(ProductID, false);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		private void TagActiveInActive(long ProductID, bool bolActive)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				string SQL = "CALL procProductTagActiveInactive(@ProductID, @ProductListFilterType);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@ProductListFilterType", Convert.ToInt16(bolActive));

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		/// <summary>
		/// Lemu - 06-20-2011
		/// </summary>
		/// <param name="ProductID">Put zero(0) if you want to update all products</param>
		/// <param name="Quantity"></param>
		/// <returns></returns>
		public bool UpdateActualQuantity(int BranchID, long lngProductID, decimal decQuantity)
		{
			bool boRetValue = false;
			try
			{
				string SQL = "CALL procProductUpdateActualQuantity(@BranchID, @lngProductID, @decQuantity);";

				MySqlCommand cmd = new MySqlCommand();
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@BranchID", BranchID);
				cmd.Parameters.AddWithValue("@lngProductID", lngProductID);
				cmd.Parameters.AddWithValue("@decQuantity", decQuantity);

				if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}

			return boRetValue;
		}

        public bool ZeroOutActualQuantityBySupplier(int BranchID, long lngSupplierID)
        {
            bool boRetValue = false;
            try
            {
                string SQL = "CALL procProductZeroOutActualQuantityBySupplier(@BranchID, @lngSupplierID);";

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@lngSupplierID", lngSupplierID);

                if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

            return boRetValue;
        }

        public bool LockUnlockForSellingBySupplier(int BranchID, long lngSupplierID, bool isLock)
        {
            bool boRetValue = false;
            try
            {
                string SQL = "CALL LockUnlockProductForSellingBySupplier(@BranchID, @lngSupplierID, @bolisLock);";

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@lngSupplierID", lngSupplierID);
                cmd.Parameters.AddWithValue("@bolisLock", isLock);

                if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

            return boRetValue;
        }

		public bool UpdateRewardPoints(long lngProductGroupID, long lngProductSubGroupID, long lngProductID, decimal decRewardPoints)
		{
			bool boRetValue = false;
			try
			{
				string SQL = "CALL procProductUpdateRewardPoints(@lngProductGroupID, @lngProductSubGroupID, @lngProductID, @decRewardPoints);";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductGroupID", lngProductGroupID);
				cmd.Parameters.AddWithValue("@lngProductSubGroupID", lngProductSubGroupID);
				cmd.Parameters.AddWithValue("@lngProductID", lngProductID);
				cmd.Parameters.AddWithValue("@decRewardPoints", decRewardPoints);

				if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

			return boRetValue;
		}

		#endregion

		#region Add and Subtract Quantity

		/// <summary>
		/// Jul 26, 2011: Lemuel E. Aceron
		/// Add the quantity to Products and tblProductBaseVariationsMatrix then save to tblProductMovement for historical record
		/// </summary>
		/// <param name="ProductID"></param>
		/// <param name="MatrixID"></param>
		/// <param name="Quantity"></param>
		/// <param name="Remarks"></param>
		/// <param name="TransactionDate"></param>
		/// <param name="TransactionNo"></param>
		public bool AddQuantity(int BranchID, long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo, string CreatedBy)
		{
			bool boRetValue = false;
			try
			{
				string SQL = "CALL procProductAddQuantity(@intBranchID, @lngProductID, @lngMatrixID, @decQuantity, @strRemarks, @dteTransactionDate, @strTransactionNo, @strCreatedBy)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@intBranchID", BranchID);
				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@lngMatrixID", MatrixID);
				cmd.Parameters.AddWithValue("@decQuantity", Quantity);
				cmd.Parameters.AddWithValue("@strRemarks", Remarks);
				cmd.Parameters.AddWithValue("@dteTransactionDate", TransactionDate);
				cmd.Parameters.AddWithValue("@strTransactionNo", TransactionNo);
				cmd.Parameters.AddWithValue("@strCreatedBy", CreatedBy);

				if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}

			return boRetValue;
		}

		/// <summary>
		/// Depreciated, use "public void Products.AddQuantity(long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo)" instead
		/// </summary>
		/// <param name="ProductID"></param>
		/// <param name="Quantity"></param>
		//public void AddQuantity(Int64 ProductID, decimal Quantity)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//                            "Quantity			= Quantity + @Quantity, " +
		//                            "QuantityIN			= QuantityIN + @Quantity " +
		//                        "WHERE ProductID	= @ProductID;";
				  
		//        
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        
		//        
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
		//        prmQuantity.Value = Quantity;
		//        cmd.Parameters.Add(prmQuantity);

		//        MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
		//        prmProductID.Value = ProductID;
		//        cmd.Parameters.Add(prmProductID);

		//        base.ExecuteNonQuery(cmd);

		//        /*** 
		//         * July 26, 2011 Remove this 
		//         * 
		//            //ProductComposition clsProductComposition = new ProductComposition(base.Connection, base.Transaction);
		//            //MySqlDataReader myReader = clsProductComposition.List(ProductID, "CompositionID", SortOption.Ascending);
		//            //while (myReader.Read())
		//            //{
		//            //    long compProductID = myReader.GetInt64("ProductID");
		//            //    long compVariationMatrixID = myReader.GetInt64("VariationMatrixID");
		//            //    decimal comQuantity = myReader.GetDecimal("Quantity");

		//            //    if (compVariationMatrixID !=0)
		//            //    {
		//            //        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
		//            //        clsProductVariationsMatrix.AddQuantity(compVariationMatrixID, Quantity * comQuantity);
		//            //    }

		//            //    AddQuantity(compProductID, Quantity * comQuantity);
		//            //}
		//            //myReader.Close();
		//         * **/
		//    }

		//    catch (Exception ex)
		//    {
		//        
		//        
		//        {
		//            
		//            
		//            
		//            
		//        }

		//        throw base.ThrowException(ex);
		//    }	
		//}

		/// <summary>
		/// Jul 26, 2011: Lemuel E. Aceron
		/// Subtract the quantity from Products and tblProductBaseVariationsMatrix then save to tblProductMovement for historical record
		/// </summary>
		/// <param name="ProductID"></param>
		/// <param name="MatrixID"></param>
		/// <param name="Quantity"></param>
		/// <param name="Remarks"></param>
		/// <param name="TransactionDate"></param>
		/// <param name="TransactionNo"></param>
		public bool SubtractQuantity(int BranchID, long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo, string CreatedBy)
		{
			bool boRetValue = false;
			try
			{
				string SQL = "CALL procProductSubtractQuantity(@intBranchID, @lngProductID, @lngMatrixID, @decQuantity, @strRemarks, @dteTransactionDate, @strTransactionNo, @strCreatedBy)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@intBranchID", BranchID);
				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@lngMatrixID", MatrixID);
				cmd.Parameters.AddWithValue("@decQuantity", Quantity);
				cmd.Parameters.AddWithValue("@strRemarks", Remarks);
				cmd.Parameters.AddWithValue("@dteTransactionDate", TransactionDate);
				cmd.Parameters.AddWithValue("@strTransactionNo", TransactionNo);
				cmd.Parameters.AddWithValue("@strCreatedBy", CreatedBy);

				if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}

			return boRetValue;
		}

		/// <summary>
		/// Depreciated, use "public void Products.SubtractQuantity(long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo, string CreatedBy)" instead
		/// </summary>
		/// <param name="ProductID"></param>
		/// <param name="Quantity"></param>
		//public void SubtractQuantity(Int64 ProductID, decimal Quantity)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//                            "Quantity			= Quantity - @Quantity, " +
		//                            "ActualQuantity		= ActualQuantity - @Quantity, " +
		//                            "QuantityOUT		= QuantityOUT + @Quantity " +
		//                        "WHERE ProductID	= @ProductID;";
				  
		//        
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        
		//        
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
		//        prmQuantity.Value = Quantity;
		//        cmd.Parameters.Add(prmQuantity);

		//        MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
		//        prmProductID.Value = ProductID;
		//        cmd.Parameters.Add(prmProductID);

		//        base.ExecuteNonQuery(cmd);

		//        ProductComposition clsProductComposition = new ProductComposition(base.Connection, base.Transaction);
		//        MySqlDataReader myReader = clsProductComposition.List(ProductID, "CompositionID", SortOption.Ascending);
		//        while (myReader.Read())
		//        {
		//            long compProductID = myReader.GetInt64("ProductID");
		//            long compVariationMatrixID = myReader.GetInt64("VariationMatrixID");
		//            decimal comQuantity = myReader.GetDecimal("Quantity");

		//            if (compVariationMatrixID !=0)
		//            {
		//                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
		//                clsProductVariationsMatrix.SubtractQuantity(compVariationMatrixID, Quantity * comQuantity);
		//            }

		//            SubtractQuantity(compProductID, Quantity * comQuantity);
		//        }
		//        myReader.Close();
		//    }

		//    catch (Exception ex)
		//    {
		//        
		//        
		//        {
		//            
		//            
		//            
		//            
		//        }

		//        throw base.ThrowException(ex);
		//    }	
		//}


		public bool AddActualQuantity(int BranchID, string Barcode, decimal ActualQuantity)
		{
			bool boRetValue = false;

			try
			{
				//string SQL = "UPDATE tblProducts SET " +
				//                    "ActualQuantity	    = ActualQuantity + @ActualQuantity " +
				//                "WHERE Barcode	        = @Barcode;";

				string SQL = "UPDATE tblProductInventory SET " +
									"ActualQuantity	    = ActualQuantity + @ActualQuantity " +
								"WHERE ProductID = (SELECT ProductID From tblProducts WHERE Barcode = @Barcode LIMIT 1) " +
								"   AND BranchID = @BranchID ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmActualQuantity = new MySqlParameter("@ActualQuantity",MySqlDbType.Decimal);
				prmActualQuantity.Value = ActualQuantity;
				cmd.Parameters.Add(prmActualQuantity);

				MySqlParameter prmBarcode = new MySqlParameter("@Barcode",MySqlDbType.String);
				prmBarcode.Value = Barcode;
				cmd.Parameters.Add(prmBarcode);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = BranchID;
				cmd.Parameters.Add(prmBranchID);

				if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;

			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

			return boRetValue;
		}

		public void UpdateRID(long ProductID, long RID)
		{
			try
			{
				string SQL = "CALL procProductUpdateRID(@lngProductID, @lngRID)";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@lngRID", RID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdateProductReorderOverStock(DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStock(@detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public void UpdateProductReorderOverStockPerGroup(long GroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerGroup(@lngGroupID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngGroupID", GroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public void UpdateProductReorderOverStockPerSubGroup(long SubGroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSubGroup(@lngSubGroupID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSubGroupID", SubGroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public void UpdateProductReorderOverStockPerSupplier(long SupplierID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplier(@lngSupplierID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
			
		}

		public void UpdateProductReorderOverStockPerSupplierPerGroup(long SupplierID, long GroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerGroup(@lngSupplierID, @lngGroupID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngGroupID", GroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public void UpdateProductReorderOverStockPerSupplierPerSubGroup(long SupplierID, long SubGroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerSubGroup(@lngSupplierID, @lngSubGroupID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngSubGroupID", SubGroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public void UpdateProductReorderOverStockPerSupplier(long SupplierID, long RID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerRID(@lngSupplierID, @lngRID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngRID", RID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		public decimal getReorderQtyByRID(long ProductID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerProduct(@lngProductID, @detStartDate, @dteEndDate)";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT RID, RIDMinThreshold, RIDMaxThreshold, Quantity FROM tblProducts WHERE ProductID = @lngProductID;";
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.Parameters.AddWithValue("@lngProductID", ProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                

				decimal decRetValue = 0;
				decimal decIDC;
				decimal decQuantity;
				foreach(System.Data.DataRow dr in dt.Rows)
				{ 
					decQuantity = decimal.Parse(dr["Quantity"].ToString());
					if (decQuantity == 0)
						decIDC = 0;
					else if (decimal.Parse(dr["RIDMinThreshold"].ToString()) == 0)
						decIDC = decQuantity;
					else
                        decIDC = decimal.Round(decQuantity / decimal.Parse(dr["RIDMinThreshold"].ToString()));

					if (decimal.Parse(dr["RIDMinThreshold"].ToString()) != 0) 
					{
						if (decimal.Parse(dr["RID"].ToString()) > 0)
						{
							if (decimal.Parse(dr["RID"].ToString()) > decIDC)
								decRetValue = decimal.Round((decimal.Parse(dr["RID"].ToString()) * decimal.Parse(dr["RIDMinThreshold"].ToString())) - decQuantity);
							else
								decRetValue = 0;
						}
					}
					else {decRetValue = 0;}
				}

				return decRetValue;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

		}

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL=	"DELETE FROM tblProductPackage WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductUnitMatrix WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblMatrixPackage WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductVariations WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProducts WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		private string SQLSelect()
		{
            string stSQL = "SELECT " +
                                    "a.ProductID, " +
                                    "a.ProductCode, " +
                                    "pkg.PackageID, " +
                                    "pkg.BarCode1, " +
                                    "pkg.BarCode2, " +
                                    "pkg.BarCode3, " +
                                    "IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode, " +
                                    "a.ProductDesc, " +
                                    "a.ProductSubGroupID, " +
                                    "b.ProductSubGroupCode, " +
                                    "b.ProductSubGroupName, " +
                                    "b.ProductGroupID, " +
                                    "c.ProductGroupCode, " +
                                    "c.ProductGroupName, " +
                                    "a.BaseUnitID, " +
                                    "d.UnitID, " +
                                    "d.UnitCode 'BaseUnitCode', " +
                                    "d.UnitName 'BaseUnitName', " +
                                    "d.UnitCode, " +
                                    "d.UnitName, " +
                                    "a.DateCreated, " +
                                    "a.Deleted, " +
                                    "a.Active, " +
                                    "pkg.Price, " +
                                    "pkg.WSPrice, " +
                                    "pkg.PurchasePrice, " +
                                    "a.PercentageCommision, " +
                                    "a.IncludeInSubtotalDiscount, " +
                                    "pkg.VAT, " +
                                    "pkg.EVAT, " +
                                    "pkg.LocalTax, " +
                                    "IFNULL(inv.Quantity,0) Quantity, " +
                                    "fnProductQuantityConvert(a.ProductID, IFNULL(inv.Quantity,0), a.BaseUnitID) AS ConvertedQuantity, " +
                                    "a.MinThreshold, " +
                                    "a.MaxThreshold, " +
                                    "a.RID, " +
                                    "a.SupplierID, " +
                                    "e.ContactCode AS SupplierCode, " +
                                    "e.ContactName AS SupplierName, " +
                                    "c.OrderSlipPrinter, " +
                                    "a.ChartOfAccountIDPurchase, " +
                                    "a.ChartOfAccountIDSold, " +
                                    "a.ChartOfAccountIDInventory, " +
                                    "a.ChartOfAccountIDTaxPurchase, " +
                                    "a.ChartOfAccountIDTaxSold, " +
                                    "a.IsItemSold, " +
                                    "a.WillPrintProductComposition, " +
                                    "a.VariationCount, " +
                                    "IFNULL(inv.QuantityIN,0) QuantityIN, " +
                                    "IFNULL(inv.QuantityOUT,0) QuantityOUT, " +
                                    "IFNULL(inv.ActualQuantity,0) ActualQuantity, " +
                                    "IFNULL(inv.IsLock,0) IsLock, " +
                                    "a.MaxThreshold - IFNULL(inv.Quantity,0) AS ReorderQty, " +
                                    "a.RIDMinThreshold, " +
                                    "a.RIDMaxThreshold, " +
                                    "a.RIDMaxThreshold - IFNULL(inv.Quantity,0) AS RIDReorderQty, " +
                                    "a.RewardPoints, " +
                                    "IFNULL(inv.MatrixID,0) MatrixID, " +
                                    "IFNULL(mtrx.Description,'') MatrixDescription " +
                                "FROM tblProducts a " +
                                "   INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
                                "   INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID " +
                                "   INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID " +
                                "   INNER JOIN tblContacts e ON a.SupplierID = e.ContactID " +
                                "   INNER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID " +
                                "   LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND pkg.MatrixID = mtrx.MatrixID " +
                                "   LEFT OUTER JOIN (" +
                                "        SELECT BranchID, ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity, IsLock FROM tblProductInventory WHERE BranchID=1 GROUP BY BranchID, ProductID, MatrixID, IsLock " +
						        "    ) inv ON inv.ProductID = a.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0) ";
            //ON a.ProductID =  inv.ProductID AND pkg.MatrixID = inv.MatrixID 
			return stSQL;
		}
		private string SQLSelect(int BranchID)
		{
            string stSQL = "SELECT " +
                                    "a.ProductID, " +
                                    "a.ProductCode, " +
                                    "pkg.PackageID, " +
                                    "IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode, " +
							        "pkg.BarCode1, " +
							        "pkg.BarCode2, " +
							        "pkg.BarCode3, " +
                                    "a.ProductDesc, " +
                                    "a.ProductSubGroupID, " +
                                    "b.ProductSubGroupCode, " +
                                    "b.ProductSubGroupName, " +
                                    "b.ProductGroupID, " +
                                    "c.ProductGroupCode, " +
                                    "c.ProductGroupName, " +
                                    "a.BaseUnitID, " +
                                    "d.UnitID, " +
                                    "d.UnitCode 'BaseUnitCode', " +
                                    "d.UnitName 'BaseUnitName', " +
                                    "d.UnitCode, " +
                                    "d.UnitName, " +
                                    "a.DateCreated, " +
                                    "a.Deleted, " +
                                    "a.Active, " +
                                    "pkg.Price, " +
                                    "pkg.WSPrice, " +
                                    "pkg.PurchasePrice, " +
                                    "a.PercentageCommision, " +
                                    "a.IncludeInSubtotalDiscount, " +
                                    "pkg.VAT, " +
                                    "pkg.EVAT, " +
                                    "pkg.LocalTax, " +
                                    "IFNULL(inv.Quantity,0) Quantity, " +
                                    "IFNULL(inv.IsLock,0) IsLock, " +
                                    "fnProductQuantityConvert(a.ProductID, IFNULL(inv.Quantity,0), a.BaseUnitID) AS ConvertedQuantity, " +
                                    "a.MinThreshold, " +
                                    "a.MaxThreshold, " +
                                    "a.RID, " +
                                    "a.SupplierID, " +
                                    "e.ContactCode AS SupplierCode, " +
                                    "e.ContactName AS SupplierName, " +
                                    "c.OrderSlipPrinter, " +
                                    "a.ChartOfAccountIDPurchase, " +
                                    "a.ChartOfAccountIDSold, " +
                                    "a.ChartOfAccountIDInventory, " +
                                    "a.ChartOfAccountIDTaxPurchase, " +
                                    "a.ChartOfAccountIDTaxSold, " +
                                    "a.IsItemSold, " +
                                    "a.WillPrintProductComposition, " +
                                    "a.VariationCount, " +
                                    "IFNULL(inv.QuantityIN,0) QuantityIN, " +
                                    "IFNULL(inv.QuantityOUT,0) QuantityOUT, " +
                                    "IFNULL(inv.ActualQuantity,0) ActualQuantity, " +
                                    "a.MaxThreshold - IFNULL(inv.Quantity,0) AS ReorderQty, " +
                                    "a.RIDMinThreshold, " +
                                    "a.RIDMaxThreshold, " +
                                    "a.RIDMaxThreshold - IFNULL(inv.Quantity,0) AS RIDReorderQty, " +
                                    "a.RewardPoints, " +
                                    "IFNULL(inv.MatrixID,0) MatrixID, " +
                                    "IFNULL(mtrx.Description,'') MatrixDescription " +
                                "FROM tblProducts a " +
                                "   INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
                                "   INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID " +
                                "   INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID " +
                                "   INNER JOIN tblContacts e ON a.SupplierID = e.ContactID " +
                                "   INNER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID " +
                                //"   LEFT OUTER JOIN tblProductInventory inv ON a.ProductID =  inv.ProductID AND pkg.MatrixID = inv.MatrixID " +
                                //"   LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON a.ProductID = mtrx.ProductID AND mtrx.MatrixID = inv.MatrixID " +
                                "   LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND pkg.MatrixID = mtrx.MatrixID " +
                                "   LEFT OUTER JOIN (" +
                                "        SELECT BranchID, ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity, IsLock FROM tblProductInventory WHERE BranchID=" + BranchID + " GROUP BY BranchID, ProductID, MatrixID, IsLock " +
                                "    ) inv ON inv.ProductID = a.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0) " +
                                "WHERE 1=1 ";

            stSQL += BranchID == 0 ? "" : "AND IFNULL(inv.BranchID,1) = " + BranchID + " ";

			return stSQL;
		}
		private string SQLSelectSimple()
		{
            string stSQL = "SELECT " +
                                    "a.ProductID, " +
                                    "a.ProductCode, " +
                                    "a.BarCode1, " +
                                    "a.BarCode2, " +
                                    "a.BarCode3, " +
                                    "a.ProductDesc, " +
                                    "a.ProductSubGroupID, " +
                                    "a.BaseUnitID, " +
                                    "a.DateCreated, " +
                                    "a.Deleted, " +
                                    "a.Active, " +
                                    "a.Price, " +
                                    "a.WSPrice, " +
                                    "a.PurchasePrice, " +
                                    "a.PercentageCommision, " +
                                    "a.IncludeInSubtotalDiscount, " +
                                    "a.VAT, " +
                                    "a.EVAT, " +
                                    "a.LocalTax, " +
                                    "a.Quantity, " +
                                    "a.SupplierID, " +
                                    "a.ActualQuantity, " +
                                    "a.RewardPoints " +
                                "FROM tblProducts a " +
                                "INNER JOIN tblProductPackage b ON a.ProductID = b.ProductID AND ";
			return stSQL;
		}
		private string SQLSelectSimple(int BranchID)
		{
			string stSQL = "SELECT " +
                                    "pkg.PackageID, " +
                                    "prd.ProductID, " +
                                    "pkg.MatrixID, " +
                                    "prd.ProductCode, " +
                                    "prd.ProductDesc, " +
                                    "prd.ProductSubGroupID, " +
                                    "prd.BaseUnitID, " +
                                    "prd.DateCreated, " +
                                    "prd.PercentageCommision, " +
                                    "prd.IncludeInSubtotalDiscount, " +
                                    "prd.SupplierID, " +
                                    "prd.RewardPoints, " +
                                    "prd.MinThreshold, " +
                                    "prd.MaxThreshold, " +
                                    "pkg.BarCode1, " +
                                    "pkg.BarCode2, " +
                                    "pkg.BarCode3, " +
                                    "IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode, " +

                                    "pkg.Price, " +
                                    "pkg.WSPrice, " +
                                    "pkg.PurchasePrice, " +
                                    "pkg.VAT, " +
                                    "pkg.EVAT, " +
                                    "pkg.LocalTax, " +

                                    "IFNULL(inv.Quantity,0) Quantity, " +
                                    "IFNULL(inv.IsLock,0) IsLock, " +
                                    //"fnProductQuantityConvert(inv.ProductID, inv.Quantity, prd.BaseUnitID) AS ConvertedQuantity, " +

                                    "IFNULL(inv.ActualQuantity,0) ActualQuantity, " +
                                    "IFNULL(mtrx.Description,'') MatrixDescription " +

								"FROM tblProducts prd " +
                                "   INNER JOIN tblProductPackage pkg ON prd.ProductID = pkg.ProductID " +
                                //"   LEFT OUTER JOIN tblProductInventory inv ON prd.ProductID = inv.ProductID AND pkg.MatrixID = inv.MatrixID " +
                                //"   LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON prd.ProductID = mtrx.ProductID AND mtrx.MatrixID = inv.MatrixID " +
                                "     LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID " +
                                "     LEFT OUTER JOIN (" +
                                "          SELECT BranchID, ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity, IsLock FROM tblProductInventory WHERE BranchID=" + BranchID + " GROUP BY BranchID, ProductID, MatrixID, IsLock " +
                                "      ) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0) " +
                                "WHERE prd.deleted = 0 AND IFNULL(mtrx.deleted, 0) = 0 ";
            
            stSQL += BranchID == 0 ? "" : "AND IFNULL(inv.BranchID,1) = " + BranchID.ToString() + " ";

			return stSQL;
		}

		private string SQLSelect(ProductColumns clsProductColumns)
		{
			string stSQL = "SELECT ";

			if (clsProductColumns.ProductCode) stSQL += "tblProducts." + ProductColumnNames.ProductCode + ", ";

            if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.PackageID + ", ";
            if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.BarCode + " AS BarCode, ";
            if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.BarCode + ", ";
            if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.BarCode1 + ", ";
            if (clsProductColumns.BarCode2) stSQL += "tblProductPackage." + ProductColumnNames.BarCode2 + ", ";
            if (clsProductColumns.BarCode3) stSQL += "tblProductPackage." + ProductColumnNames.BarCode3 + ", ";

			if (clsProductColumns.ProductDesc) stSQL += "tblProducts." + ProductColumnNames.ProductDesc + ", ";
			if (clsProductColumns.ProductSubGroupID) stSQL += "tblProducts." + ProductColumnNames.ProductSubGroupID + ", ";
			if (clsProductColumns.ProductSubGroupCode) stSQL += "tblProductSubGroup." + ProductColumnNames.ProductSubGroupCode + ", ";
			if (clsProductColumns.ProductSubGroupName) stSQL += "tblProductSubGroup." + ProductColumnNames.ProductSubGroupName + ", ";
			if (clsProductColumns.ProductGroupID) stSQL += "tblProductSubGroup." + ProductColumnNames.ProductGroupID + ", ";
			if (clsProductColumns.ProductGroupCode) stSQL += "tblProductGroup." + ProductColumnNames.ProductGroupCode + ", ";
			if (clsProductColumns.ProductGroupName) stSQL += "tblProductGroup." + ProductColumnNames.ProductGroupName + ", ";
			if (clsProductColumns.BaseUnitID) stSQL += "tblProducts." + ProductColumnNames.BaseUnitID + ", ";
			if (clsProductColumns.BaseUnitCode) stSQL += "tblUnit." + ProductColumnNames.UnitCode + " 'BaseUnitCode', ";
			if (clsProductColumns.BaseUnitName) stSQL += "tblUnit." + ProductColumnNames.UnitName + " 'BaseUnitName', ";
            
            if (clsProductColumns.UnitID) stSQL += "tblProductPackage." + ProductColumnNames.UnitID + ", ";
            if (clsProductColumns.UnitCode) stSQL += "tblUnitPackage." + ProductColumnNames.UnitCode + ", ";
            if (clsProductColumns.UnitName) stSQL += "tblUnitPackage." + ProductColumnNames.UnitName + ", ";

			if (clsProductColumns.DateCreated) stSQL += "tblProducts." + ProductColumnNames.DateCreated + ", ";
			if (clsProductColumns.Deleted) stSQL += "tblProducts." + ProductColumnNames.Deleted + ", ";
			if (clsProductColumns.Active) stSQL += "tblProducts." + ProductColumnNames.Active+ ", ";

            if (clsProductColumns.Price) stSQL += "tblProductPackage." + ProductColumnNames.Price + ", ";
            if (clsProductColumns.WSPrice) stSQL += "tblProductPackage." + ProductColumnNames.WSPrice + ", ";
            if (clsProductColumns.PurchasePrice) stSQL += "tblProductPackage." + ProductColumnNames.PurchasePrice + ", ";

			if (clsProductColumns.PercentageCommision) stSQL += "tblProducts." + ProductColumnNames.PercentageCommision + ", ";
			if (clsProductColumns.IncludeInSubtotalDiscount) stSQL += "tblProducts." + ProductColumnNames.IncludeInSubtotalDiscount + ", ";
			if (clsProductColumns.VAT) stSQL += "tblProductPackage." + ProductColumnNames.VAT + ", ";
            if (clsProductColumns.EVAT) stSQL += "tblProductPackage." + ProductColumnNames.EVAT + ", ";
            if (clsProductColumns.LocalTax) stSQL += "tblProductPackage." + ProductColumnNames.LocalTax + ", ";
            if (clsProductColumns.Quantity) stSQL += "tblProductInventory." + ProductColumnNames.Quantity + " AS Quantity, ";
            if (clsProductColumns.Quantity) stSQL += "fnProductQuantityConvert(tblProducts.ProductID, tblProductInventory." + ProductColumnNames.Quantity + ", tblProducts.BaseUnitID) AS ConvertedQuantity, ";
			if (clsProductColumns.MinThreshold) stSQL += "tblProducts." + ProductColumnNames.MinThreshold + ", ";
			if (clsProductColumns.MaxThreshold) stSQL += "tblProducts." + ProductColumnNames.MaxThreshold + ", ";
			if (clsProductColumns.RID) stSQL += "tblProducts." + ProductColumnNames.RID + ", ";
			if (clsProductColumns.SupplierID) stSQL += "tblProducts." + ProductColumnNames.SupplierID + ", ";
			if (clsProductColumns.SupplierCode) stSQL += "tblContacts.ContactCode AS SupplierCode, ";
			if (clsProductColumns.SupplierName) stSQL += "tblContacts.ContactName AS SupplierName, ";
			if (clsProductColumns.OrderSlipPrinter) stSQL += "tblProductGroup." + ProductColumnNames.OrderSlipPrinter + ", ";
			if (clsProductColumns.ChartOfAccountIDPurchase) stSQL += "tblProducts." + ProductColumnNames.ChartOfAccountIDPurchase + ", ";
			if (clsProductColumns.ChartOfAccountIDSold) stSQL += "tblProducts." + ProductColumnNames.ChartOfAccountIDSold + ", ";
			if (clsProductColumns.ChartOfAccountIDInventory) stSQL += "tblProducts." + ProductColumnNames.ChartOfAccountIDInventory + ", ";
			if (clsProductColumns.ChartOfAccountIDTaxPurchase) stSQL += "tblProducts." + ProductColumnNames.ChartOfAccountIDTaxPurchase + ", ";
			if (clsProductColumns.ChartOfAccountIDTaxSold) stSQL += "tblProducts." + ProductColumnNames.ChartOfAccountIDTaxSold + ", ";
			if (clsProductColumns.IsItemSold) stSQL += "tblProducts." + ProductColumnNames.IsItemSold + ", ";
			if (clsProductColumns.WillPrintProductComposition) stSQL += "tblProducts." + ProductColumnNames.WillPrintProductComposition + ", ";
			if (clsProductColumns.VariationCount) stSQL += "tblProducts." + ProductColumnNames.VariationCount + ", ";
            if (clsProductColumns.QuantityIN) stSQL += "tblProductInventory." + ProductColumnNames.QuantityIN + " AS QuantityIN, ";
            if (clsProductColumns.QuantityOUT) stSQL += "tblProductInventory." + ProductColumnNames.QuantityOUT + " AS QuantityOUT, ";
            if (clsProductColumns.ActualQuantity) stSQL += "tblProductInventory." + ProductColumnNames.ActualQuantity + " AS ActualQuantity, ";
            if (clsProductColumns.ActualQuantity) stSQL += "fnProductQuantityConvert(tblProducts.ProductID, tblProductInventory." + ProductColumnNames.ActualQuantity + ", tblProducts.BaseUnitID) AS ConvertedActualQuantity, ";
            if (clsProductColumns.ReorderQty) stSQL += "tblProducts." + ProductColumnNames.MaxThreshold + " - tblProductInventory." + ProductColumnNames.Quantity + " AS ReorderQty, ";
			if (clsProductColumns.RIDMinThreshold) stSQL += "tblProducts." + ProductColumnNames.RIDMinThreshold + ", ";
			if (clsProductColumns.RIDMaxThreshold) stSQL += "tblProducts." + ProductColumnNames.RIDMaxThreshold + ", ";
            if (clsProductColumns.RIDReorderQty) stSQL += "tblProducts.RIDMaxThreshold - tblProductInventory.Quantity AS RIDReorderQty, ";
			if (clsProductColumns.BranchID) stSQL += "tblProductInventory." + ProductColumnNames.BranchID + ", ";
			if (clsProductColumns.RewardPoints) stSQL += "tblProducts." + ProductColumnNames.RewardPoints + ", ";
			if (clsProductColumns.SequenceNo) stSQL += "tblProducts." + ProductColumnNames.SequenceNo + ", ";

			stSQL += "tblProducts.ProductID FROM tblProducts ";
            stSQL += "INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID ";
            stSQL += "INNER JOIN tblProductGroup ON tblProductSubGroup.ProductGroupID = tblProductGroup.ProductGroupID ";
            stSQL += "INNER JOIN tblUnit ON tblProducts.BaseUnitID = tblUnit.UnitID ";
            stSQL += "INNER JOIN tblProductPackage ON tblProducts.ProductID = tblProductPackage.ProductID ";
            stSQL += "LEFT OUTER JOIN tblProductInventory ON tblProducts.ProductID = tblProductInventory.ProductID ";
            
            stSQL += "INNER JOIN tblUnit tblUnitPackage ON tblProductPackage.UnitID = tblUnitPackage.UnitID ";
		    
            stSQL += "INNER JOIN tblContacts ON tblProducts.SupplierID = tblContacts.ContactID ";

			return stSQL;
		}

		#region Details

        public bool IsExist(long ProductID, string BarCode1, string BarCode2, string BarCode3)
        {
            try
            {
                string SQL = "CALL procProductIsExist(@ProductID, @BarCode);";
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                bool boRetValue = false;

                if (!string.IsNullOrEmpty(BarCode1))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode1);
                    string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                    base.MySqlDataAdapterFill(cmd, dt);
                    
                    foreach(System.Data.DataRow dr in dt.Rows)
                    {
                        if (Int64.Parse(dr["ProductCount"].ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(BarCode2))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode2);
                    string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                    base.MySqlDataAdapterFill(cmd, dt);
                    
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (Int64.Parse(dr["ProductCount"].ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(BarCode3))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode3);
                    string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                    base.MySqlDataAdapterFill(cmd, dt);
                    
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (Int64.Parse(dr["ProductCount"].ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }

                return boRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
		public bool WillPrintProductComposition(long ProductID)
		{
			bool bolRetValue = false;

			try
			{
				string SQL = "SELECT WillPrintProductComposition FROM tblProducts a WHERE a.ProductId = @ProductID;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					bolRetValue = bool.Parse(dr["WillPrintProductComposition"].ToString());
				}
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}

			return bolRetValue;
		}
		public int get_BaseUnitID(long ProductID)
		{
			try
			{
				string SQL = "Select BaseUnitID FROM tblProducts WHERE ProductID = @ProductID;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                

                int iRetValue = 1;
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					iRetValue = Int32.Parse(dr["BaseUnitID"].ToString());
				}

				return iRetValue;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}
		public ProductDetails Details(int BranchID, long ProductID, long MatrixID = 0)
		{
			try
			{
                string SQL = SQLSelect(BranchID) + "AND a.ProductId = @ProductID AND IFNULL(inv.MatrixID,0) = @MatrixID ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = MatrixID;
                cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
				ProductDetails Details = SetDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        //public ProductDetails Details(string BarCode)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE (BarCode1 = @BarCode OR BarCode2 = @BarCode OR BarCode3 = @BarCode);";
				
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
        //        prmBarCode.Value = BarCode;
        //        cmd.Parameters.Add(prmBarCode);

        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        ProductDetails Details = SetDetails(dt);

        //        return Details;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}
        //public ProductDetails DetailsByCode(string ProductCode, int BranchID = 0)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect(BranchID) + "AND ProductCode = @ProductCode";

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        ProductDetails Details = SetDetails(dt);

        //        return Details;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        public ProductDetails Details(long ProductID, long MatrixID = 0, int BranchID = 0)
        {
            return getDetails(ProductID, MatrixID, string.Empty, BranchID);
        }

        public ProductDetails Details(string BarCode, int BranchID = 0)
        {
            return getDetails(0, 0, BarCode, BranchID);
        }

        private ProductDetails getDetails(long ProductID, long MatrixID, string BarCode, int BranchID = 0)
        {
            try
            {
                string SQL = "CALL procProductMainDetails(@BranchID, @ProductID, @MatrixID, @BarCode)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@BarCode", BarCode);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ProductDetails Details = SetDetails(dt);

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public ProductDetails Details1(int BranchID, long ProductID)
		{
            return getDetails(ProductID, 0, string.Empty, BranchID);
		}
        public ProductDetails Details(int BranchID, string BarCode, bool ShowItemMoreThanZeroQty = false, decimal Quantity = 1)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
				string SQL = SQLSelect(BranchID) + "AND (BarCode1 = @BarCode OR BarCode2 = @BarCode OR BarCode3 = @BarCode OR BarCode4 = @Barcode) ";

                if (ShowItemMoreThanZeroQty)
                {
                    SQL += "AND IFNULL(inv.Quantity,0) >= @Quantity ";

                    MySqlParameter prmQuantity = new MySqlParameter("@Quantity", MySqlDbType.Decimal);
                    prmQuantity.Value = Quantity;
                    cmd.Parameters.Add(prmQuantity);
                }
                SQL += "ORDER BY mtrx.Description ASC LIMIT 1";

                cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
				prmBarCode.Value = BarCode;
				cmd.Parameters.Add(prmBarCode);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				ProductDetails Details = SetDetails(dt);
                
				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public ProductDetails DetailsByCode(int BranchID, string ProductCode)
		{
			try
			{
				string SQL = SQLSelect(BranchID) + "AND ProductCode = @ProductCode;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                

				ProductDetails Details = SetDetails(dt);
                
				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		private ProductDetails SetDetails(MySqlDataReader myReader)
		{
			try
			{
				ProductDetails Details = new ProductDetails();
				Details.ProductID = 0;

				while (myReader.Read())
				{
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.BarCode = "" + myReader["BarCode"].ToString();
					Details.BarCode2 = "" + myReader["BarCode2"].ToString();
					Details.BarCode3 = "" + myReader["BarCode3"].ToString();
					Details.ProductDesc = "" + myReader["ProductDesc"].ToString();
					Details.ProductGroupID = myReader.GetInt64("ProductGroupID");
					Details.ProductGroupCode = "" + myReader["ProductGroupCode"].ToString();
					Details.ProductGroupName = "" + myReader["ProductGroupName"].ToString();
					Details.ProductSubGroupID = myReader.GetInt64("ProductSubGroupID");
					Details.ProductSubGroupCode = "" + myReader["ProductSubGroupCode"].ToString();
					Details.ProductSubGroupName = "" + myReader["ProductSubGroupName"].ToString();
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitCode = "" + myReader["UnitCode"].ToString();
					Details.BaseUnitName = "" + myReader["UnitName"].ToString();
					Details.DateCreated = myReader.GetDateTime("DateCreated");
					Details.Deleted = myReader.GetBoolean("Deleted");
					Details.Active = myReader.GetBoolean("Active");
					Details.Price = myReader.GetDecimal("Price");
					Details.WSPrice = myReader.GetDecimal("WSPrice");
					Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
					Details.PercentageCommision = myReader.GetDecimal("PercentageCommision");
					Details.IncludeInSubtotalDiscount = myReader.GetBoolean("IncludeInSubtotalDiscount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
					Details.Quantity = myReader.GetDecimal("Quantity");
					Details.ConvertedQuantity = "" + myReader["ConvertedQuantity"].ToString();
                    Details.IsLock = myReader.GetBoolean("IsLock");
					Details.MinThreshold = myReader.GetDecimal("MinThreshold");
					Details.MaxThreshold = myReader.GetDecimal("MaxThreshold");
					Details.RID = myReader.GetInt64("RID");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierName = "" + myReader["SupplierName"].ToString();
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), myReader.GetString("OrderSlipPrinter"));

					/*** Added for Financial Information  ***/
					/*** January 12, 2009 ***/
					Details.ChartOfAccountIDPurchase = myReader.GetInt32("ChartOfAccountIDPurchase");
					Details.ChartOfAccountIDSold = myReader.GetInt32("ChartOfAccountIDSold");
					Details.ChartOfAccountIDInventory = myReader.GetInt32("ChartOfAccountIDInventory");
					Details.ChartOfAccountIDTaxPurchase = myReader.GetInt32("ChartOfAccountIDTaxPurchase");
					Details.ChartOfAccountIDTaxSold = myReader.GetInt32("ChartOfAccountIDTaxSold");

					Details.IsItemSold = myReader.GetBoolean("IsItemSold");
					Details.WillPrintProductComposition = myReader.GetBoolean("WillPrintProductComposition");

					/*** Inventory Tracking ***/
					/*** May 10, 2010 ***/
					Details.QuantityIN = myReader.GetDecimal("QuantityIN");
					Details.QuantityOUT = myReader.GetDecimal("QuantityOUT");

					Details.ActualQuantity = myReader.GetDecimal("ActualQuantity");

					Details.RewardPoints = myReader.GetDecimal("RewardPoints");

                    Details.PackageID = myReader.GetInt64("PackageID");
				}
                myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        private ProductDetails SetDetails(System.Data.DataTable dt)
        {
            try
            {
                ProductDetails Details = new ProductDetails();
                Details.ProductID = 0;

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                    Details.ProductCode = "" + dr["ProductCode"].ToString();
                    Details.BarCode = "" + dr["BarCode"].ToString();
                    Details.BarCode1 = "" + dr["BarCode1"].ToString();
                    Details.BarCode2 = "" + dr["BarCode2"].ToString();
                    Details.BarCode3 = "" + dr["BarCode3"].ToString();
                    Details.ProductDesc = "" + dr["ProductDesc"].ToString();
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductGroupCode = "" + dr["ProductGroupCode"].ToString();
                    Details.ProductGroupName = "" + dr["ProductGroupName"].ToString();
                    Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
                    Details.ProductSubGroupCode = dr["ProductSubGroupCode"].ToString();
                    Details.ProductSubGroupName = dr["ProductSubGroupName"].ToString();
                    Details.BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString());
                    Details.BaseUnitCode = dr["UnitCode"].ToString();
                    Details.BaseUnitName = dr["UnitName"].ToString();
                    Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.Deleted = Boolean.Parse(dr["Deleted"].ToString());
                    Details.Active = Boolean.Parse(dr["Active"].ToString());
                    Details.Price = Decimal.Parse(dr["Price"].ToString());
                    Details.WSPrice = Decimal.Parse(dr["WSPrice"].ToString());
                    Details.PurchasePrice = Decimal.Parse(dr["PurchasePrice"].ToString());
                    Details.PercentageCommision = Decimal.Parse(dr["PercentageCommision"].ToString());
                    Details.IncludeInSubtotalDiscount = Boolean.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                    Details.VAT = Decimal.Parse(dr["VAT"].ToString());
                    Details.EVAT = Decimal.Parse(dr["EVAT"].ToString());
                    Details.LocalTax = Decimal.Parse(dr["LocalTax"].ToString());
                    Details.Quantity = Decimal.Parse(dr["Quantity"].ToString());
                    Details.ConvertedQuantity = dr["ConvertedQuantity"].ToString();
                    Details.IsLock = Convert.ToBoolean(int.Parse(dr["IsLock"].ToString()));
                    Details.MinThreshold = Decimal.Parse(dr["MinThreshold"].ToString());
                    Details.MaxThreshold = Decimal.Parse(dr["MaxThreshold"].ToString());
                    Details.RID = Int64.Parse(dr["RID"].ToString());
                    Details.SupplierID = Int64.Parse(dr["SupplierID"].ToString());
                    Details.SupplierCode = dr["SupplierCode"].ToString();
                    Details.SupplierName = dr["SupplierName"].ToString();
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    /*** Added for Financial Information  ***/
                    /*** January 12, 2009 ***/
                    Details.ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    Details.ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString());
                    Details.ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString());
                    Details.ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());
                    Details.ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString());

                    Details.IsItemSold = Boolean.Parse(dr["IsItemSold"].ToString());
                    Details.WillPrintProductComposition = Boolean.Parse(dr["WillPrintProductComposition"].ToString());

                    /*** Inventory Tracking ***/
                    /*** May 10, 2010 ***/
                    Details.QuantityIN = Decimal.Parse(dr["QuantityIN"].ToString());
                    Details.QuantityOUT = Decimal.Parse(dr["QuantityOUT"].ToString());

                    Details.ActualQuantity = Decimal.Parse(dr["ActualQuantity"].ToString());

                    Details.RewardPoints = Decimal.Parse(dr["RewardPoints"].ToString());

                    Details.MatrixID = Int64.Parse(dr["MatrixID"].ToString());
                    Details.MatrixDescription = "" + dr["MatrixDescription"].ToString();

                    Details.PackageID = Int64.Parse(dr["PackageID"].ToString());
                }

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Streams

		public System.Data.DataTable ProductIDAsDataTable()
		{
			try
			{
				string SQL = "SELECT ProductID FROM tblProducts WHERE Deleted = 0";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = '0' ";
				
				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public System.Data.DataTable ListAsDataTable(long ProductSubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = '0' AND a.ProductSubGroupID = @ProductSubGroupID ";
				
				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				
				return dt;

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, int Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 ";

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";


				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable ListAsDataTableActiveInactive(ProductListFilterType clsProductListFilterType, string SortField, SortOption SortOrder, int Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 ";

				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				else if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public ProductDetails[] List(int BranchID = 0)
		{
			try
			{
				System.Collections.ArrayList items = new System.Collections.ArrayList();
				System.Data.DataTable dtProductIDs = ProductIDAsDataTable();
				foreach (System.Data.DataRow drProductID in dtProductIDs.Rows)
				{
                    items.Add(Details1(BranchID, long.Parse(drProductID["ProductID"].ToString())));
				}
				
				ProductDetails[] arrProductDetails = new ProductDetails[0];

				if (items != null)
				{
					arrProductDetails = new ProductDetails[items.Count];
					items.CopyTo(arrProductDetails);
				}

				return arrProductDetails;
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
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 ";
				
				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				return base.ExecuteReader(cmd);	
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(Int64 ProductSubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 AND a.ProductSubGroupID = @ProductSubGroupID ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int16);			
				prmProductSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				return base.ExecuteReader(cmd);			
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
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 " +
											"AND a.Active = 1 " +
											"AND (ProductCode LIKE @SearchKey " +
											"OR BarCode LIKE @SearchKey " +
											"OR BarCode2 LIKE @SearchKey " +
											"OR BarCode3 LIKE @SearchKey " +
											"OR ProductDesc LIKE @SearchKey " +
											"OR ProductGroupName LIKE @SearchKey " +
											"OR ProductSubGroupName LIKE @SearchKey) ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				SQL += "LIMIT 100;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				string SQL =SQLSelect() + "WHERE a.Deleted = 0 " +
											"AND a.Active = 1 " +
											"AND (Barcode LIKE @SearchKey " +
											"OR ProductCode LIKE @SearchKey " +
											"OR ProductDesc LIKE @SearchKey) ";

				if (isQuantityGreaterThanZERO)
					SQL += "AND a.Quantity > 0 ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		public MySqlDataReader AdvanceSearch(string ProductCode, string ProductDesc, 
			string ProductGroupName, string ProductSubGroupName, string UnitName,
			string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 ";
				
				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;

				if (ProductCode != "" && ProductCode != null)
				{
					SQL += "AND ProductCode = @ProductCode ";
					MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
					prmProductCode.Value = ProductCode;
					cmd.Parameters.Add(prmProductCode);
				}
				if (ProductDesc != "" && ProductDesc != null)
				{
					SQL += "AND ProductDesc = @ProductDesc ";
					MySqlParameter prmProductDesc = new MySqlParameter("@ProductDesc",MySqlDbType.String);
					prmProductDesc.Value = ProductDesc;
					cmd.Parameters.Add(prmProductDesc);
				}
				if (ProductGroupName != "" && ProductGroupName != null)
				{
					SQL += "AND ProductGroupName = @ProductGroupName ";
					MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);
					prmProductGroupName.Value = ProductGroupName;
					cmd.Parameters.Add(prmProductGroupName);
				}
				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{
					SQL += "AND ProductSubGroupName = @ProductSubGroupName ";
					MySqlParameter prmProductSubGroupName = new MySqlParameter("@ProductSubGroupName",MySqlDbType.String);
					prmProductSubGroupName.Value = ProductSubGroupName;
					cmd.Parameters.Add(prmProductSubGroupName);
				}
				if (UnitName != "" && UnitName != null)
				{
					SQL += "AND c.UnitName = @UnitName ";
					MySqlParameter prmUnitName = new MySqlParameter("@UnitName",MySqlDbType.String);
					prmUnitName.Value = UnitName;
					cmd.Parameters.Add(prmUnitName);
				}

				SQL += "ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";
				
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		
		

		public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(ProductListFilterType.ShowActiveOnly, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, false, SortField, SortOrder);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public System.Data.DataTable SearchDataTableActiveInactive(ProductListFilterType clsProductListFilterType, string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(clsProductListFilterType, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, false, SortField, SortOrder);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchSaleableDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(ProductListFilterType.ShowActiveOnly, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, true, SortField, SortOrder);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchSaleableDataTableByGroup(string ProductGroupCode, string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				string SQL = "SELECT " +
									"ProductID, " +
									"ProductCode, " +
									"BarCode, " +
									"BarCode2, " +
									"BarCode3, " +
									"ProductDesc, " +
									"a.Price, " +
									"a.WSPrice " +
								"FROM tblProducts a " +
								"INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
								"INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID " +
								"INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID " +
								"WHERE a.Deleted = 0 AND a.Active = 1 AND IsItemSold = 1 " +
									"AND ProductGroupCode  LIKE @ProductGroupCode " +
									"AND (Barcode LIKE @SearchKey " +
									"OR ProductCode LIKE @SearchKey " +
									"OR ProductDesc LIKE @SearchKey) ";

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode ",MySqlDbType.String);
				prmProductGroupCode.Value = ProductGroupCode + "%";
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchDataTable(ProductListFilterType clsProductListFilterType, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 ";

				if (CheckIItemisSold) SQL += "AND a.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (SearchKey != string.Empty)
				{
					SQL += "AND (Barcode LIKE @SearchKey " +
											"OR ProductCode LIKE @SearchKey " +
											"OR ProductSubGroupCode LIKE @SearchKey " +
											"OR ProductGroupCode LIKE @SearchKey " +
											"OR ProductDesc LIKE @SearchKey) ";
				}
				if (SupplierID != 0)
					SQL += "AND (SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductGroupID != Constants.ZERO)
				{ SQL += "AND b.ProductGroupID = " + ProductGroupID + " "; }

				if (ProductGroupName != "" && ProductGroupName != null)
				{ SQL += "AND ProductGroupName = '" + ProductGroupName + "' "; }
				else { SQL += "AND ProductGroupName <> '' "; }

				if (ProductSubGroupID != Constants.ZERO)
				{ SQL += "AND a.ProductSubGroupID = " + ProductSubGroupID + " "; }

				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{ SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' "; }
				else { SQL += "AND ProductSubGroupName <> '' "; }

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

				if (SortField == string.Empty) SortField = "ProductCode";
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchDataTableSimple(ProductListFilterType clsProductListFilterType, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelectSimple() + "WHERE a.Deleted = 0 ";

				if (CheckIItemisSold) SQL += "AND a.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (SearchKey != string.Empty)
				{
					SQL += "AND (Barcode LIKE @SearchKey " +
											"OR ProductCode LIKE @SearchKey " +
											"OR ProductDesc LIKE @SearchKey) ";
				}
				if (SupplierID != 0)
					SQL += "AND (SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductGroupID != Constants.ZERO)
				{ SQL += "AND a.ProductGroupID = " + ProductGroupID + " "; }

				//if (ProductGroupName != "" && ProductGroupName != null)
				//{ SQL += "AND a.ProductGroupID = (SELECT ProductGroupName = '" + ProductGroupName + "') "; }
				//else { SQL += "AND ProductGroupName <> '' "; }

				//if (ProductSubGroupID != Constants.ZERO)
				//{ SQL += "AND a.ProductSubGroupID = " + ProductSubGroupID + " "; }

				//if (ProductSubGroupName != "" && ProductSubGroupName != null)
				//{ SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' "; }
				//else { SQL += "AND ProductSubGroupName <> '' "; }

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

				if (SortField == string.Empty) SortField = "ProductCode";
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchDataTableSimple(int BranchID, ProductListFilterType clsProductListFilterType, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelectSimple(BranchID);

				if (CheckIItemisSold) SQL += "AND a.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (!string.IsNullOrEmpty(SearchKey))
				{ SQL += "AND (Barcode LIKE @SearchKey OR Barcode2 LIKE @SearchKey OR ProductCode LIKE @SearchKey) "; }

				if (SupplierID != 0)
					SQL += "AND (SupplierID = " + SupplierID + " OR a.ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductGroupID != Constants.ZERO)
				{ SQL += "AND a.ProductGroupID = " + ProductGroupID + " "; }

				if (isQuantityGreaterThanZERO == true)
					SQL += "AND a.Quantity > 0 ";

                if (!string.IsNullOrEmpty(SortField)) SortField = "ProductCode";
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				
				
				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable ProductIDandCodeDataTable(ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive, string SearchKey="", long SupplierID = 0, long ProductGroupID = 0, string ProductGroupName = "", long ProductSubGroupID=0, string ProductSubGroupName="", Int32 Limit=100, bool isQuantityGreaterThanZERO=false, bool CheckIItemisSold=true, string SortField="ProductCode", SortOption SortOrder=SortOption.Ascending)
		{
			try
			{
                string SQL = "CALL procProductCodeSelect(@BranchID, @BarCode, @ProductCode, @SupplierID, @ShowActiveAndInactive, @isQuantityGreaterThanZERO, @lngLimit, @SortField, @SortOrder)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", 0);
                cmd.Parameters.AddWithValue("@BarCode", SearchKey);
                cmd.Parameters.AddWithValue("@ProductCode", SearchKey);
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@ShowActiveAndInactive", clsProductListFilterType.ToString("d"));
                cmd.Parameters.AddWithValue("@isQuantityGreaterThanZERO", isQuantityGreaterThanZERO);
                cmd.Parameters.AddWithValue("@lngLimit", Limit);
                cmd.Parameters.AddWithValue("@SortField", SortField);
                switch (SortOrder)
                {
                    case SortOption.Ascending:
                        cmd.Parameters.AddWithValue("@SortOrder", "ASC");
                        break;
                    case SortOption.Desscending:
                        cmd.Parameters.AddWithValue("@SortOrder", "DESC");
                        break;
                }

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}		
		public System.Data.DataTable InventoryReport(string ProductGroupName, string ProductSubGroupName, string ProductCode)
		{
			try
			{
				string SQL ="SELECT " +
								"a.ProductID, " +
								"a.ProductCode, " +
								"a.BarCode, " +
								"a.BarCode2, " +
								"a.BarCode3, " +
								"a.ProductDesc, " +
								"d.ProductGroupName 'ProductGroupName', " +
								"b.ProductSubGroupName 'ProductSubGroupName', " +
								"c.UnitName 'BaseUnitName', " +
								"a.DateCreated, " +
								"a.Price, " +
								"a.WSPrice, " +
								"a.Quantity, " +
								"a.Quantity AS MainQuantity, " +
								"a.MinThreshold, " +
								"a.MaxThreshold, " +
								"a.PurchasePrice, " +
								"e.ContactName AS SupplierName, " +
								"a.QuantityIN, " +
								"a.QuantityOUT " +
								"a.QuantityIN AS MainQuantityIN, " +
								"a.QuantityOUT AS QuantityOut " +
							"FROM tblProducts a " +
							"LEFT JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
							"LEFT JOIN tblProductGroup d ON b.ProductGroupID = d.ProductGroupID " +  
							"LEFT JOIN tblUnit c ON a.BaseUnitID = c.UnitID " +
							"INNER JOIN tblContacts e ON a.SupplierID = e.ContactID " +
							"WHERE a.Deleted = 0 AND a.Active = 1 ";

				SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 ";

				if (ProductGroupName != "" && ProductGroupName != null)
				{
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
				}
				else { SQL += "AND ProductGroupName <> '' "; }

				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
				}
				else { SQL += "AND ProductSubGroupName <> '' "; }

				if (ProductCode != "" && ProductCode != null)
				{
					string stSQL = "";
					foreach (string stProductCode in ProductCode.Split(';'))
					{
						stSQL += "OR ProductCode LIKE '%" + stProductCode + "%' ";
					}
					SQL += "AND (" + stSQL.Remove(0,2) + ")";
				}

				SQL += "ORDER BY ProductCode ASC";

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(SQL, dt);
				
				return dt;		
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		
		//public System.Data.DataTable ForReorder(string ProductGroupName, string ProductSubGroupName)
		//{
		//    try
		//    {
		//        System.Data.DataTable dt = ForReorder(0, ProductGroupName, ProductSubGroupName);

		//        return dt;		
		//    }
		//    catch (Exception ex)
		//    {
		//        throw base.ThrowException(ex);
		//    }	
		//}		

		public System.Data.DataTable ForReorder(long SupplierID = 0, string ProductGroupName = "", string ProductSubGroupName = "")
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 AND a.Quantity <= a.MinThreshold ";

				if (SupplierID != 0)
					SQL += "AND (SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductGroupName != "" && ProductGroupName != null)
				{
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
				}
				else { SQL += "AND ProductGroupName <> '' "; }

				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
				}
				else { SQL += "AND ProductSubGroupName <> '' "; }

				SQL += "ORDER BY a.Quantity ASC";

				

				
				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}		

		public System.Data.DataTable OverStock(string ProductGroupName, string ProductSubGroupName)
		{
			try
			{
				string SQL ="SELECT " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"BarCode2, " +
								"BarCode3, " +
								"ProductDesc, " + 
								"d.ProductGroupName, " +
								"b.ProductSubGroupName, " +
								"c.UnitName 'BaseUnitName', " +
								"DateCreated, " +
								"a.Price, " +
								"a.WSPrice, " +
								"a.Quantity, " +
								"a.MinThreshold, " +
								"a.MaxThreshold " +
							"FROM tblProducts a " +
							"INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
							"INNER JOIN tblProductGroup d ON b.ProductGroupID = d.ProductGroupID " +  
							"INNER JOIN tblUnit c ON a.BaseUnitID = c.UnitID " +
							"WHERE a.Deleted = 0 AND a.Active = 1 AND a.Quantity > a.MaxThreshold ";

				SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 AND a.Quantity > a.MaxThreshold ";
				if (ProductGroupName != "" && ProductGroupName != null)
				{
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
				}
				else { SQL += "AND ProductGroupName <> '' "; }

				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
				}
				else { SQL += "AND ProductSubGroupName <> '' "; }

				SQL += "ORDER BY a.Quantity DESC";

				
				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;		
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        private System.Data.DataTable ListAsDataTable(int BranchID = 0, string BarCode = "", string ProductCode = "", Int64 SupplierID = 0, ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive,
                bool isQuantityGreaterThanZERO = false, Int32 Limit = 0, string SortField = "", SortOption SortOrder = SortOption.Ascending)
        {
            string SQL = "CALL procProductSelect(@BranchID, @BarCode, @ProductCode, @SupplierID, @ShowActiveAndInactive, @isQuantityGreaterThanZERO, @lngLimit, @SortField, @SortOrder)";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@BarCode", BarCode);
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ShowActiveAndInactive", clsProductListFilterType.ToString("d"));
            cmd.Parameters.AddWithValue("@isQuantityGreaterThanZERO", isQuantityGreaterThanZERO);
            cmd.Parameters.AddWithValue("@lngLimit", Limit);
            cmd.Parameters.AddWithValue("@SortField", SortField);
            switch (SortOrder)
            {
                case SortOption.Ascending:
                    cmd.Parameters.AddWithValue("@SortOrder", "ASC");
                    break;
                case SortOption.Desscending:
                    cmd.Parameters.AddWithValue("@SortOrder", "DESC");
                    break;
            } 

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable ListAsDataTable(int BranchID = 0, ProductDetails clsSearchKeys = new ProductDetails(), ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive,
                    long SequenceNoStart = 0, System.Data.SqlClient.SortOrder SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 Limit = 0, bool isQuantityGreaterThanZERO = false, string SortField = "ProductCode", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
                System.Data.DataTable dt = ListAsDataTable(BranchID, clsSearchKeys.BarCode, clsSearchKeys.ProductCode, clsSearchKeys.SupplierID, clsProductListFilterType, 
                                                            isQuantityGreaterThanZERO, Limit, SortField, SortOrder);
                return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public System.Data.DataTable ListAsDataTable(ProductColumns clsProductColumns, int BranchID, ProductListFilterType clsProductListFilterType,
            long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder,
            ProductColumns SearchColumns, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder)
        {
            try
            {
                clsProductColumns.IncludeAllPackages = true;

                // include branchid in the selection if branchid is not zero
                if (BranchID != 0) clsProductColumns.BranchID = true;

                string SQL = SQLSelect(clsProductColumns) + "WHERE tblProducts.deleted=0 ";

                if (SequenceNoStart != Constants.ZERO)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND tblProducts.SequenceNo < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND tblProducts.SequenceNo > " + SequenceNoStart.ToString() + " ";
                }

                if (BranchID != Constants.ZERO) SQL += "AND tblProductInventory.BranchID = " + BranchID.ToString() + " ";
                if (CheckIItemisSold) SQL += "AND tblProducts.IsItemSold = 1 ";
                if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND tblProducts.Active = 1 ";
                if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND tblProducts.Active = 0 ";

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.BarCode)
                    { if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode LIKE @SearchKey "; }

                    if (SearchColumns.BarCode2)
                    { if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode2 LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode2 LIKE @SearchKey "; }

                    if (SearchColumns.BarCode3)
                    { if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode3 LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode3 LIKE @SearchKey "; }

                    if (SearchColumns.ProductCode)
                    { if (SQLSearch == string.Empty) SQLSearch += "tblProducts.ProductCode LIKE @SearchKey "; else SQLSearch += "OR tblProducts.ProductCode LIKE @SearchKey "; }

                    if (SearchColumns.ProductDesc)
                    { if (SQLSearch == string.Empty) SQLSearch += "tblProducts.ProductDesc LIKE @SearchKey "; else SQLSearch += "OR tblProducts.ProductDesc LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";
                }

                if (SupplierID != Constants.ZERO)
                    SQL += "AND (tblProducts.SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

                if (ProductSubGroupID != Constants.ZERO)
                { SQL += "AND tblProducts.ProductSubGroupID = " + ProductSubGroupID + " "; }

                if (ProductSubGroupName != string.Empty && ProductSubGroupName != null)
                { SQL += "AND tblProductSubGroup.ProductSubGroupName = '" + ProductSubGroupName + "' "; }

                if (ProductGroupID != Constants.ZERO)
                { SQL += "AND tblProductSubGroup.ProductGroupID = " + ProductGroupID + " "; }

                if (ProductGroupName != string.Empty && ProductGroupName != null)
                { SQL += "AND tblProductGroup.ProductGroupName = '" + ProductGroupName + "' "; }

                if (isQuantityGreaterThanZERO)
                { SQL += "AND tblProductInventory.Quantity > 0 "; }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder == SortOption.Ascending)
                        SQL += "ASC ";
                    else
                        SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;


                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey", MySqlDbType.String);
                prmSearchKey.Value = SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public System.Data.DataTable ListAsDataTable(ProductColumns clsProductColumns, int BranchID, ProductListFilterType clsProductListFilterType,
			long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder,
			ProductColumns SearchColumns, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder, string GroupBy)
		{
			try
			{
				clsProductColumns.IncludeAllPackages = true;
				// include branchid in the selection if branchid is not zero
				if (BranchID != 0) clsProductColumns.BranchID = true;

				string SQL = SQLSelect(clsProductColumns) + "WHERE tblProducts.deleted=0 ";

				if (SequenceNoStart != Constants.ZERO)
				{
					if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
						SQL += "AND tblProducts.SequenceNo < " + SequenceNoStart.ToString() + " ";
					else
						SQL += "AND tblProducts.SequenceNo > " + SequenceNoStart.ToString() + " ";
				}

				if (BranchID != Constants.ZERO) SQL += "AND tblProductInventory.BranchID = " + BranchID.ToString() + " ";
				if (CheckIItemisSold) SQL += "AND tblProducts.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND tblProducts.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND tblProducts.Active = 0 ";

				if (SearchKey != string.Empty)
				{
					string SQLSearch = string.Empty;

					if (SearchColumns.BarCode)
					{ if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode LIKE @SearchKey "; }

					if (SearchColumns.BarCode2)
					{ if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode2 LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode2 LIKE @SearchKey "; }

					if (SearchColumns.BarCode3)
					{ if (SQLSearch == string.Empty) SQLSearch += "tblProducts.Barcode3 LIKE @SearchKey "; else SQLSearch += "OR tblProducts.Barcode3 LIKE @SearchKey "; }

					if (SearchColumns.ProductCode)
					{ if (SQLSearch == string.Empty) SQLSearch += "tblProducts.ProductCode LIKE @SearchKey "; else SQLSearch += "OR tblProducts.ProductCode LIKE @SearchKey "; }

					if (SearchColumns.ProductDesc)
					{ if (SQLSearch == string.Empty) SQLSearch += "tblProducts.ProductDesc LIKE @SearchKey "; else SQLSearch += "OR tblProducts.ProductDesc LIKE @SearchKey "; }

					if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";
				}

				if (SupplierID != Constants.ZERO)
					SQL += "AND (tblProducts.SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductSubGroupID != Constants.ZERO)
				{ SQL += "AND tblProducts.ProductSubGroupID = " + ProductSubGroupID + " "; }

				if (ProductSubGroupName != string.Empty && ProductSubGroupName != null)
				{ SQL += "AND tblProductSubGroup.ProductSubGroupName = '" + ProductSubGroupName + "' "; }

				if (ProductGroupID != Constants.ZERO)
				{ SQL += "AND tblProductSubGroup.ProductGroupID = " + ProductGroupID + " "; }

				if (ProductGroupName != string.Empty && ProductGroupName != null)
				{ SQL += "AND tblProductGroup.ProductGroupName = '" + ProductGroupName + "' "; }

                if (isQuantityGreaterThanZERO)
                { SQL += "AND tblProductInventory.Quantity > 0 "; }

				if (GroupBy != string.Empty || GroupBy != null)
					SQL += "GROUP BY " + GroupBy + " ";

				if (SortField == string.Empty) SortField = "ProductCode";
				SQL += "ORDER BY " + SortField + " ";

				if (SortOrder == SortOption.Ascending)
					SQL += "ASC ";
				else
					SQL += "DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				

				return dt;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

        public System.Data.DataTable ListAsDataTable(int BranchID, string BarCode, ProductListFilterType clsProductListFilterType, Int32 Limit, bool isQuantityGreaterThanZERO, string SortField = "ProductCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelectSimple(BranchID);
                
                if (clsProductListFilterType != ProductListFilterType.ShowInactiveOnly)
                {
                    SQL += "AND prd.Active = @Active ";
                    MySqlParameter prmActive = new MySqlParameter("@Active", MySqlDbType.Int16);
                    if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) prmActive.Value = 1;
                    if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) prmActive.Value = 0;
                    cmd.Parameters.Add(prmActive);
                }
                if (isQuantityGreaterThanZERO)
                {
                    SQL += "AND inv.Quantity > 0 ";
                }

                if (!string.IsNullOrEmpty(BarCode))
                {
                    SQL += "AND (Barcode1 LIKE @BarCode OR Barcode2 LIKE @BarCode OR BarCode3 LIKE @BarCode OR BarCode4 LIKE @BarCode OR prd.ProductCode LIKE @BarCode OR prd.ProductDesc LIKE @BarCode) ";

                    MySqlParameter prmBarcode = new MySqlParameter("@BarCode", MySqlDbType.String);
                    prmBarcode.Value = BarCode + "%";
                    cmd.Parameters.Add(prmBarcode);
                }

                SQL += "ORDER BY inv.Quantity ASC ";

                if (!string.IsNullOrEmpty(SortField))
                {
                    SQL +="," + SortField + " ";

                    if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                        SQL += "ASC ";
                    else
                        SQL += "DESC ";
                }

                if (Limit != 0) SQL += "LIMIT " + Limit + " ";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		
		#endregion

		#region Inheritance

		public void InheritSubGroupVariations(Int64 ProductSubGroupID, Int64 ProductID)
		{
			try 
			{
				string SQL	= "INSERT INTO tblProductVariations (ProductID, VariationID) " + 
									"SELECT @ProductID, VariationID FROM tblProductSubGroupVariations " + 
								"WHERE SubGroupID = @SubGroupID;";

				
				
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int32);			
				prmSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void InheritSubGroupVariationsMatrix(Int64 ProductSubGroupID, Int64 ProductID, ProductDetails prodDetails)
		{
			try 
			{	
				

				ProductBaseMatrixDetails clsBaseDetails;

				ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(base.Connection, base.Transaction);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
				ProductVariationsMatrixDetails  clsProductVariationsMatrixDetails = new ProductVariationsMatrixDetails();

				MySqlDataReader clsProductSubGroupVariationsMatrixList;
				MySqlDataReader clsProductSubGroupBaseVariationsMatrixList = clsProductSubGroupVariationsMatrix.BaseList(ProductSubGroupID,"MatrixID",SortOption.Ascending);

				Int64 SubGroupBaseMatrixID = 0;

				while (clsProductSubGroupBaseVariationsMatrixList.Read())
				{
					clsBaseDetails = new ProductBaseMatrixDetails();
					clsBaseDetails.ProductID = ProductID;
					clsBaseDetails.Description = "" + clsProductSubGroupBaseVariationsMatrixList["Description"].ToString();
					clsBaseDetails.UnitID = Convert.ToInt32(clsProductSubGroupBaseVariationsMatrixList["UnitID"]);
					clsBaseDetails.Price = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["Price"]);
					clsBaseDetails.WSPrice = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["WSPrice"]);
					clsBaseDetails.PurchasePrice = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["PurchasePrice"]);
					clsBaseDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(clsProductSubGroupBaseVariationsMatrixList["IncludeInSubtotalDiscount"]);
					clsBaseDetails.VAT = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["VAT"]);
					clsBaseDetails.EVAT = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["EVAT"]);
					clsBaseDetails.LocalTax = Convert.ToDecimal(clsProductSubGroupBaseVariationsMatrixList["LocalTax"]);
					clsBaseDetails.Quantity = 0;
					clsBaseDetails.MinThreshold = prodDetails.MinThreshold;
					clsBaseDetails.MaxThreshold = prodDetails.MaxThreshold;

					clsBaseDetails.MatrixID = clsProductVariationsMatrix.InsertBaseVariation(clsBaseDetails);

					SubGroupBaseMatrixID = clsProductSubGroupBaseVariationsMatrixList.GetInt64(0);
					clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(base.Connection, base.Transaction);
					clsProductSubGroupVariationsMatrixList =  clsProductSubGroupVariationsMatrix.List(SubGroupBaseMatrixID);

					while (clsProductSubGroupVariationsMatrixList.Read())
					{
						clsProductVariationsMatrixDetails = new ProductVariationsMatrixDetails();
						clsProductVariationsMatrixDetails.MatrixID = clsBaseDetails.MatrixID;
						clsProductVariationsMatrixDetails.ProductID = ProductID;
						clsProductVariationsMatrixDetails.VariationID = Convert.ToInt64(clsProductSubGroupVariationsMatrixList["VariationID"]);
						clsProductVariationsMatrixDetails.Description = "" + clsProductSubGroupVariationsMatrixList["Description"].ToString();
						clsProductVariationsMatrix.InsertVariation(clsProductVariationsMatrixDetails);
					}
					clsProductSubGroupVariationsMatrixList.Close(); 

				}
				clsProductSubGroupBaseVariationsMatrixList.Close();

			}
				
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void InheritSubGroupUnitMatrix(Int64 ProductSubGroupID, Int64 ProductID)
		{
			try 
			{	
				

				ProductSubGroupUnitsMatrix clsProductSubGroupUnitsMatrix = new ProductSubGroupUnitsMatrix(base.Connection, base.Transaction);

				ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(base.Connection, base.Transaction);
				ProductUnitsMatrixDetails clsProductUnitsMatrixDetails = new ProductUnitsMatrixDetails();

				MySqlDataReader clsProductSubGroupUnitsMatrixList = clsProductSubGroupUnitsMatrix.List(ProductSubGroupID,"MatrixID",SortOption.Ascending);
				
				while (clsProductSubGroupUnitsMatrixList.Read())
				{
					clsProductUnitsMatrixDetails.ProductID = Convert.ToInt32(ProductID);
					clsProductUnitsMatrixDetails.BaseUnitID = Convert.ToInt32(clsProductSubGroupUnitsMatrixList.GetInt32(2));
					clsProductUnitsMatrixDetails.BaseUnitValue = Convert.ToDecimal(clsProductSubGroupUnitsMatrixList.GetDecimal(4));
					clsProductUnitsMatrixDetails.BottomUnitID = Convert.ToInt32(clsProductSubGroupUnitsMatrixList.GetInt32(5));
					clsProductUnitsMatrixDetails.BottomUnitValue = Convert.ToDecimal(clsProductSubGroupUnitsMatrixList.GetDecimal(7));
					clsUnitMatrix.Insert(clsProductUnitsMatrixDetails);

				}
				clsProductSubGroupUnitsMatrixList.Close();
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Change VAT, EVAT and Localtax

		// Dec 10, 2011 : Obsolete, change to ChangeTax
		//public void ChangeVAT(decimal OldVAT, decimal NewVAT)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//                            "VAT		= @NewVAT " +
		//                        "WHERE VAT		= @OldVAT;";
				  
		//        
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        
		//        
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);			
		//        prmNewVAT.Value = NewVAT;
		//        cmd.Parameters.Add(prmNewVAT);

		//        MySqlParameter prmOldVAT = new MySqlParameter("@OldVAT",MySqlDbType.Decimal);			
		//        prmOldVAT.Value = OldVAT;
		//        cmd.Parameters.Add(prmOldVAT);

		//        base.ExecuteNonQuery(cmd);

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
		//        clsMatrixPackage.ChangeVAT(OldVAT, NewVAT);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
		//        clsProductVariationsMatrix.ChangeVAT(OldVAT, NewVAT);

		//        ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
		//        clsProductPackage.ChangeVAT(OldVAT, NewVAT);
		//    }

		//    catch (Exception ex)
		//    {
		//        
		//        
		//        {
		//            
		//            
		//            
		//            
		//        }

		//        throw base.ThrowException(ex);
		//    }	
		//}

		//// Dec 10, 2011 : Obsolete, change to ChangeTax
		//public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//            "EVAT		= @NewEVAT " +
		//            "WHERE EVAT		= @OldEVAT;";
				  
		//        
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        
		//        
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);			
		//        prmNewEVAT.Value = NewEVAT;
		//        cmd.Parameters.Add(prmNewEVAT);

		//        MySqlParameter prmOldEVAT = new MySqlParameter("@OldEVAT",MySqlDbType.Decimal);			
		//        prmOldEVAT.Value = OldEVAT;
		//        cmd.Parameters.Add(prmOldEVAT);

		//        base.ExecuteNonQuery(cmd);

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
		//        clsMatrixPackage.ChangeEVAT(OldEVAT, NewEVAT);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
		//        clsProductVariationsMatrix.ChangeEVAT(OldEVAT, NewEVAT);

		//        ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
		//        clsProductPackage.ChangeEVAT(OldEVAT, NewEVAT);
		//    }

		//    catch (Exception ex)
		//    {
		//        
		//        
		//        {
		//            
		//            
		//            
		//            
		//        }

		//        throw base.ThrowException(ex);
		//    }	
		//}

		//// Dec 10, 2011 : Obsolete, change to ChangeTax
		//public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//            "LocalTax		= @NewLocalTax " +
		//            "WHERE LocalTax		= @OldLocalTax;";
				  
		//        
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        
		//        
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);			
		//        prmNewLocalTax.Value = NewLocalTax;
		//        cmd.Parameters.Add(prmNewLocalTax);

		//        MySqlParameter prmOldLocalTax = new MySqlParameter("@OldLocalTax",MySqlDbType.Decimal);			
		//        prmOldLocalTax.Value = OldLocalTax;
		//        cmd.Parameters.Add(prmOldLocalTax);

		//        base.ExecuteNonQuery(cmd);

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
		//        clsMatrixPackage.ChangeLocalTax(OldLocalTax, NewLocalTax);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
		//        clsProductVariationsMatrix.ChangeLocalTax(OldLocalTax, NewLocalTax);

		//        ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
		//        clsProductPackage.ChangeLocalTax(OldLocalTax, NewLocalTax);
		//    }
		//    catch (Exception ex)
		//    {
		//        
		//        
		//        {
		//            
		//            
		//            
		//            
		//        }

		//        throw base.ThrowException(ex);
		//    }	
		//}

		/// <summary>
		/// Update Products VAT, EVAT and LocalTax
		/// This will also update ProductPackage, ProductVariationsMatrix and MatrixPackage
		/// </summary>
		/// <param name="ProductGroupID"></param>
		/// <param name="ProductSubGroupID"></param>
		/// <param name="ProductID"></param>
		/// <param name="NewVAT"></param>
		/// <param name="NewEVAT"></param>
		/// <param name="NewLocalTax"></param>
		public void ChangeTax(long ProductGroupID, long ProductSubGroupID, long ProductID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"VAT		= @NewVAT, " +
									"EVAT		= @NewEVAT, " +
									"LocalTax	= @NewLocalTax ";
				if (ProductID != 0) SQL += "WHERE ProductID = @ProductID;";
				else if (ProductSubGroupID != 0) SQL += "WHERE ProductSubGroupID = @ProductSubGroupID;";
				else if (ProductGroupID != 0) SQL += "WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID);";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);
				prmNewVAT.Value = NewVAT;
				cmd.Parameters.Add(prmNewVAT);

				MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);
				prmNewEVAT.Value = NewEVAT;
				cmd.Parameters.Add(prmNewEVAT);

				MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);
				prmNewLocalTax.Value = NewLocalTax;
				cmd.Parameters.Add(prmNewLocalTax);

				if (ProductID != 0)
				{
					MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
					prmProductID.Value = ProductID;
					cmd.Parameters.Add(prmProductID);
				}
				else if (ProductSubGroupID != 0)
				{
					MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
					prmProductSubGroupID.Value = ProductSubGroupID;
					cmd.Parameters.Add(prmProductSubGroupID);
				}
				else if (ProductGroupID != 0)
				{
					MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
					prmProductGroupID.Value = ProductGroupID;
					cmd.Parameters.Add(prmProductGroupID);
				}

				base.ExecuteNonQuery(cmd);

				ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
				clsProductPackage.ChangeTax(ProductGroupID, ProductSubGroupID, ProductID, NewVAT, NewEVAT, NewLocalTax);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
				clsProductVariationsMatrix.ChangeTax(ProductGroupID, ProductSubGroupID, ProductID, NewVAT, NewEVAT, NewLocalTax);
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}

		#endregion

		#region AddQuantityBranchField

        //public bool AddQuantityBranchField(int ID)
        //{
        //    try
        //    {
				
        //        MySqlCommand cmd = new MySqlCommand();
        //        string SQL;

        //        SQL = "ALTER TABLE tblProducts ADD Quantity" + ID.ToString() + " decimal(18,2) NOT NULL DEFAULT 0;";
        //        cmd = new MySqlCommand();
				
				
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
        //        base.ExecuteNonQuery(cmd);

        //        return true;
        //    }

        //    catch (Exception ex)
        //    {
				
				
        //        {
					
					
					
					
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

		#endregion

		public static string getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT _PRODUCT_INVENTORY_MOVEMENT)
		{
			string stRetValue = string.Empty;

			switch (_PRODUCT_INVENTORY_MOVEMENT)
			{
				case PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE: stRetValue = "PURCHASE"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_TRANSFER_IN: stRetValue  = "TRANSFER IN"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_STOCK_INVENTORY: stRetValue  =  "STOCK IN"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_INVENTORY_ADJUSTMENT: stRetValue  =  "ADDED INVENTORY_ADJUSTMENT"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_RETURN_ITEM: stRetValue = "RETURN ITEM"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_REFUND_ITEM: stRetValue = "REFUND ITEM"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_SALES_RETURN: stRetValue = "SALES RETURN"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_VOID_ITEM: stRetValue = "RESERVED AND COMMIT VOID ITEM"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_CHANGE_QTY: stRetValue = "RESERVED AND COMMIT CHANGE QTY"; break;
				case PRODUCT_INVENTORY_MOVEMENT.ADD_PRODUCT_VARIATION_CREATION: stRetValue = "SYSTEM AUTO ADD DURING VARIATION CREATION"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PURCHASE_RETURN: stRetValue = "PURCHASE RETURN"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL: stRetValue = "SOLD AS RETAIL"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_WHOLESALE: stRetValue = "SOLD AS WHOLESALE"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_TRANSFER_OUT: stRetValue = "TRANSFER OUT"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_STOCK_INVENTORY: stRetValue = "STOCK OUT"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_INVENTORY_ADJUSTMENT: stRetValue = "DEDUCT INVENTORY_ADJUSTMENT"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_VOID_ITEM: stRetValue = "RESERVED AND COMMIT VOID ITEM REFUND"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_RETURN_ITEM: stRetValue = "RESERVED AND COMMIT RETURN ITEM"; break;
				case PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PRODUCT_VARIATION_DELETE: stRetValue = "DELETE PRODUCT VARIATION"; break;
				case PRODUCT_INVENTORY_MOVEMENT.SYS_AUTO_ADJ_OF_MATRIX_QTY_FROM_PRODUCT_QTY_AS_BASIS: stRetValue = "SYSTEM AUTO ADJUSTMENT OF MATRIX QTY FROM PRODUCT QTY AS BASIS"; break;
				case PRODUCT_INVENTORY_MOVEMENT.SYS_AUTO_ADJ_OF_PRODUCT_QTY_FROM_SUM_OF_MATRIX_QTY_AS_BASIS: stRetValue = "SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM SUM OF MATRIX QTY AS BASIS"; break;
			}
			return stRetValue;
		}
	}
}

