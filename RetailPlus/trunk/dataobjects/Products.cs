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
		public decimal MainQuantity;
		public string ConvertedMainQuantity;
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
		public decimal Quantity;
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
		public bool PercentageCommision;
		public bool WSPrice;
		public bool VariationCount;
		public bool MainQuantity;
		public bool MainQuantityIN;
		public bool MainQuantityOUT;
		public bool MainActualQuantity;
		public bool ReorderQty;
		public bool RIDMinThreshold;
		public bool RIDMaxThreshold;
		public bool RIDReorderQty;
		public bool BranchID;
		public bool BranchQuantityIN;
		public bool BranchQuantityOUT;
		public bool BranchQuantity;
		public bool BranchActualQuantity;
		public bool RewardPoints;
		public bool SequenceNo;
	}

	public struct ProductColumnNames
	{
		public const string ProductID = "ProductID";
		public const string ProductCode = "ProductCode";
		public const string BarCode = "BarCode";
		public const string BarCode2 = "BarCode2";
		public const string BarCode3 = "BarCode3";
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
		public const string PercentageCommision = "PercentageCommision";
		public const string WSPrice = "WSPrice";
		public const string VariationCount = "VariationCount";
		public const string MainQuantity = "MainQuantity";
		public const string ConvertedMainQuantity = "ConvertedMainQuantity";
		public const string MainQuantityIN = "MainQuantityIN";
		public const string MainQuantityOUT = "MainQuantityOUT";
		public const string MainActualQuantity = "MainActualQuantity";
		public const string MainConvertedActualQuantity = "ConvertedMainActualQuantity";
		public const string ReorderQty = "ReorderQty";
		public const string RIDMinThreshold = "RIDMinThreshold";
		public const string RIDMaxThreshold = "RIDMaxThreshold";
		public const string RIDReorderQty = "RIDReorderQty";
		public const string BranchID = "BranchID";
		public const string BranchQuantityIN = "BranchQuantityIN";
		public const string BranchQuantityOUT = "BranchQuantityOUT";
		public const string BranchQuantity = "BranchQuantity";
		public const string ConvertedBranchQuantity = "ConvertedBranchQuantity";
		public const string BranchActualQuantity = "BranchActualQuantity";
		public const string ConvertedBranchActualQuantity = "ConvertedBranchActualQuantity";
		public const string RewardPoints = "RewardPoints";
		public const string SequenceNo = "SequenceNo";
	}

	public enum PRODUCT_INVENTORY_MOVEMENT
	{
		ADD_PURCHASE,
		ADD_TRANSFER_IN,
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
		DEDUCT_STOCK_INVENTORY,
		DEDUCT_INVENTORY_ADJUSTMENT,
		DEDUCT_QTY_RESERVE_AND_COMMIT_VOID_ITEM,
		DEDUCT_QTY_RESERVE_AND_COMMIT_RETURN_ITEM,
		DEDUCT_PRODUCT_VARIATION_DELETE,
		SYS_AUTO_ADJ_OF_MATRIX_QTY_FROM_PRODUCT_QTY_AS_BASIS,
		SYS_AUTO_ADJ_OF_PRODUCT_QTY_FROM_SUM_OF_MATRIX_QTY_AS_BASIS
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
	public class Product
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

		public Product()
		{
			
		}

		public Product(MySqlConnection Connection, MySqlTransaction Transaction)
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

		
		#endregion

		public MySqlConnection GetConnection()
		{
			if (mConnection==null)
			{
				mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
				mConnection.Open(); 
				
				mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
				IsInTransaction = true;
			}

			return mConnection;
		} 


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
									"BarCode, " +
									"BarCode2, " +
									"BarCode3, " + 
									"ProductDesc, " +  
									"ProductSubGroupID, " + 
									"BaseUnitID, " + 
									"DateCreated, " +
									"Price, " +
									"WSPrice, " +
									"PurchasePrice, " +
									"PercentageCommision, " +
									"IncludeInSubtotalDiscount, " +
									"VAT, " +
									"EVAT," +
									"LocalTax, " +
									"Quantity, " +
									"MinThreshold, " +
									"MaxThreshold, " +
									"SupplierID, " +
									"IsItemSold, " +
									"WillPrintProductComposition"+
								") VALUES (" +
									"@ProductCode, " + 
									"@BarCode, " +
									"@BarCode2, " +
									"@BarCode3, " + 
									"@ProductDesc, " +   
									"@ProductSubGroupID, " + 
									"@BaseUnitID, " + 
									"@DateCreated," + 
									"@Price, " +
									"@WSPrice, " +
									"@PurchasePrice, " +
									"@PercentageCommision, " +
									"@IncludeInSubtotalDiscount, " +
									"@VAT, " +
									"@EVAT," +
									"@LocalTax, " +
									"@Quantity, " +
									"@MinThreshold, " +
									"@MaxThreshold, " +
									"@SupplierID, " +
									"@IsItemSold, " +
									"@WillPrintProductComposition);"; 

				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);			
				prmBarCode.Value = Details.BarCode;
				cmd.Parameters.Add(prmBarCode);

				MySqlParameter prmBarCode2 = new MySqlParameter("@BarCode2",MySqlDbType.String);
				prmBarCode2.Value = Details.BarCode2;
				cmd.Parameters.Add(prmBarCode2);
				
				MySqlParameter prmBarCode3 = new MySqlParameter("@BarCode3",MySqlDbType.String);
				prmBarCode3.Value = Details.BarCode3;
				cmd.Parameters.Add(prmBarCode3);

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

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Details.Price;
				cmd.Parameters.Add(prmPrice);

				cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
				cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
				cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);

				MySqlParameter prmIncludeInSubtotalDiscount = new MySqlParameter("@IncludeInSubtotalDiscount",MySqlDbType.Int16);			
				prmIncludeInSubtotalDiscount.Value = Details.IncludeInSubtotalDiscount;
				cmd.Parameters.Add(prmIncludeInSubtotalDiscount);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

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

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

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

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.Insert(clsProductPackageDetails);

				// Oct 28, 2011 : Lemu
				// Added to cater branch inventory
				BranchInventory clsBranchInventory = new BranchInventory(mConnection, mTransaction);
				clsBranchInventory.CreateToAllBranches(iID);

				cmd.ExecuteNonQuery();
				
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

		public void Update(ProductDetails Details)
		{
			try 
			{
                if (IsExist(Details.ProductID, Details.BarCode, Details.BarCode2, Details.BarCode3))
                {
                    throw new EntryPointNotFoundException("This product already exist, please check the barcode.");
                }

				string SQL =	"UPDATE tblProducts SET " +
									"ProductCode		= @ProductCode, " + 
									"BarCode			= @BarCode, " +
									"BarCode2			= @BarCode2, " +
									"BarCode3			= @BarCode3, " + 
									"ProductDesc		= @ProductDesc, " +  
									"ProductSubGroupID	= @ProductSubGroupID, " + 
									"BaseUnitID			= @BaseUnitID, " +
									"Price				= @Price, " +
									"WSPrice		    = @WSPrice, " +
									"PurchasePrice		= @PurchasePrice, " +
									"PercentageCommision        =   @PercentageCommision, " +
									"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
									"VAT				= @VAT, " +
									"EVAT				= @EVAT, " +
									"LocalTax			= @LocalTax, " +
									"Quantity			= @Quantity, " +
									"MinThreshold		= @MinThreshold, " +
									"MaxThreshold		= @MaxThreshold, " +
									"SupplierID			= @SupplierID, " +
									"IsItemSold			= @IsItemSold, " +
									"WillPrintProductComposition    =   @WillPrintProductComposition " +
								"WHERE ProductID	= @ProductID;";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);			
				prmBarCode.Value = Details.BarCode;
				cmd.Parameters.Add(prmBarCode);

				MySqlParameter prmBarCode2 = new MySqlParameter("@BarCode2",MySqlDbType.String);
				prmBarCode2.Value = Details.BarCode2;
				cmd.Parameters.Add(prmBarCode2);

				MySqlParameter prmBarCode3 = new MySqlParameter("@BarCode3",MySqlDbType.String);
				prmBarCode3.Value = Details.BarCode3;
				cmd.Parameters.Add(prmBarCode3);

				MySqlParameter prmProductDesc = new MySqlParameter("@ProductDesc",MySqlDbType.String);			
				prmProductDesc.Value = Details.ProductDesc;
				cmd.Parameters.Add(prmProductDesc);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",System.Data.DbType.Int64);			
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",System.Data.DbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Details.Price;
				cmd.Parameters.Add(prmPrice);

				cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
				cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
				cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);

				MySqlParameter prmIncludeInSubtotalDiscount = new MySqlParameter("@IncludeInSubtotalDiscount",MySqlDbType.Int16);			
				prmIncludeInSubtotalDiscount.Value = Details.IncludeInSubtotalDiscount;
				cmd.Parameters.Add(prmIncludeInSubtotalDiscount);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

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

				cmd.ExecuteNonQuery();

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

				ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
				clsProductPackage.UpdateByProductIDUnitIDAndQuantity(clsDetails, Details.UpdatedBy, Details.UpdatedOn, "Edit product module.");
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
		public void UpdateBarcode(long ProductID, string Barcode)
		{
			try
			{
                if (IsExist(ProductID, Barcode, Barcode, Barcode) )
                {
                    throw new EntryPointNotFoundException("This product already exist, please check the barcode.");
                }

				string SQL = "UPDATE tblProducts SET " +
									"BarCode			= @BarCode " +
								"WHERE ProductID	= @ProductID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@BarCode", Barcode);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);

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
		public void UpdateVariationCount(long ProductID)
		{
			// Added August 2, 2009 to monitor if product still has/have variations
			try
			{
				string SQL = "CALL procProductVariationCountUpdate(@ProductID);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);

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

		public void UpdateByPackage(long ProductID, int BaseUnitID, decimal Price, decimal WSPrice, decimal PurchasePrice, decimal VAT, decimal EVAT, decimal LocalTax)
		{
			try 
			{
				string SQL =	"UPDATE tblProducts SET " +
									"Price				= @Price, " +
									"WSPrice			= @WSPrice, " +
									"PurchasePrice		= @PurchasePrice, " +
									"VAT				= @VAT, " +
									"EVAT				= @EVAT, " +
									"LocalTax			= @LocalTax " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID ";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@Price", Price);
				cmd.Parameters.AddWithValue("@WSPrice", WSPrice);
				cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
				cmd.Parameters.AddWithValue("@VAT", VAT);
				cmd.Parameters.AddWithValue("@EVAT", EVAT);
				cmd.Parameters.AddWithValue("@LocalTax", LocalTax);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

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

		public void UpdateInvDetails(long ProductID, decimal QuantityNow, decimal MinThresholdNow, decimal MaxThresholdNow)
		{
			try
			{
				string SQL = "CALL procProductUpdateInvDetails(@ProductID, @QuantityNow, @MinThresholdNow, @MaxThresholdNow);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@QuantityNow", QuantityNow);
				cmd.Parameters.AddWithValue("@MinThresholdNow", MinThresholdNow);
				cmd.Parameters.AddWithValue("@MaxThresholdNow", MaxThresholdNow);

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

		public void UpdatePurchasing(long ProductID, long SupplierID, int BaseUnitID, decimal UnitCost)
		{
			try 
			{
				string SQL =	"UPDATE tblProducts SET " +
									"PurchasePrice	= @PurchasePrice, " +
									"WSPrice	    = @PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100)), " +
									"SupplierID		= @SupplierID " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID;";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@PurchasePrice", UnitCost);
				cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

				cmd.ExecuteNonQuery();

				ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
				clsProductPackage.UpdatePurchasing(ProductID, BaseUnitID, 1, UnitCost);

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
		public void UpdatePurchasingIncludingAllMatrix(long ProductID, long SupplierID, int BaseUnitID, decimal UnitCost)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"PurchasePrice	= @PurchasePrice, " +
									"WSPrice	    = @PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100)), " +
									"SupplierID		= @SupplierID " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@PurchasePrice", UnitCost);
				cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

				cmd.ExecuteNonQuery();

				ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
				clsProductPackage.UpdatePurchasing(ProductID, BaseUnitID, 1, UnitCost);

				clsProductPackage.CopyToMatrixPackage(ProductID);
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

		private void UpdateSellingPrivate(long ProductID, long SupplierID, int BaseUnitID, decimal SellingPrice)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"Price			= @Price " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@Price", SellingPrice);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

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
		private void UpdateSellingPrivateWSPrice(long ProductID, long SupplierID, int BaseUnitID, decimal WholesalePrice)
		{
			try
			{
				string SQL = "UPDATE tblProducts SET " +
									"WSPrice			= @WSPrice " +
								"WHERE ProductID	= @ProductID " +
									"AND BaseUnitID	= @BaseUnitID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@WSPrice", WholesalePrice);
				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@BaseUnitID", BaseUnitID);

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

		public void UpdateSelling(long ProductID, long SupplierID, int BaseUnitID, decimal SellingPrice)
		{
			try 
			{
				UpdateSellingPrivate(ProductID, SupplierID, BaseUnitID, SellingPrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSelling(ProductID, BaseUnitID, 1, SellingPrice);
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
		public void UpdateSellingWSPrice(long ProductID, long SupplierID, int BaseUnitID, decimal WholesalePrice)
		{
			try
			{
				UpdateSellingPrivateWSPrice(ProductID, SupplierID, BaseUnitID, WholesalePrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSellingWSPrice(ProductID, BaseUnitID, 1, WholesalePrice);
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

		public void UpdateSellingIncludingAllMatrix(long ProductID, long SupplierID, int BaseUnitID, decimal SellingPrice)
		{
			try
			{
				UpdateSellingPrivate(ProductID, SupplierID, BaseUnitID, SellingPrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSelling(ProductID, BaseUnitID, 1, SellingPrice);

				clsProductPackage.CopyToMatrixPackage(ProductID);
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
		public void UpdateSellingIncludingAllMatrixWithSameQuantityAndUnit(long ProductID, long SupplierID, int BaseUnitID, decimal SellingPrice)
		{
			try
			{
				UpdateSellingPrivate(ProductID, SupplierID, BaseUnitID, SellingPrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSelling(ProductID, BaseUnitID, 1, SellingPrice);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, Transaction);
				clsProductVariationsMatrix.UpdateSellingWithSameQuantityAndUnit(ProductID, SupplierID, BaseUnitID, SellingPrice);

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

		public void UpdateSellingIncludingAllMatrixWSPrice(long ProductID, long SupplierID, int BaseUnitID, decimal WholesalePrice)
		{
			try
			{
				UpdateSellingPrivateWSPrice(ProductID, SupplierID, BaseUnitID, WholesalePrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSellingWSPrice(ProductID, BaseUnitID, 1, WholesalePrice);

				clsProductPackage.CopyToMatrixPackage(ProductID);
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
		public void UpdateSellingIncludingAllMatrixWithSameQuantityAndUnitWSPrice(long ProductID, long SupplierID, int BaseUnitID, decimal WholesalePrice)
		{
			try
			{
				UpdateSellingPrivateWSPrice(ProductID, SupplierID, BaseUnitID, WholesalePrice);

				ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
				clsProductPackage.UpdateSellingWSPrice(ProductID, BaseUnitID, 1, WholesalePrice);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, Transaction);
				clsProductVariationsMatrix.UpdateSellingWithSameQuantityAndUnitWSPrice(ProductID, SupplierID, BaseUnitID, WholesalePrice);

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

		public void UpdateCommision(long ProductID, decimal PercentageCommision)
		{
			// Added March 1, 2010
			try
			{
				string SQL = "CALL procProductCommisionUpdate(@ProductID, @PercentageCommision);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@PercentageCommision", PercentageCommision);

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

		public void TagActive(long ProductID)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				TagActiveInActive(ProductID, true);
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
		public void TagInactive(long ProductID)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				TagActiveInActive(ProductID, false);
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
		private void TagActiveInActive(long ProductID, bool bolActive)
		{
			// Added October 28, 2009 to monitor if product if Active or Inactive
			try
			{
				string SQL = "CALL procProductTagActiveInactive(@ProductID, @ProductListFilterType);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductID", ProductID);
				cmd.Parameters.AddWithValue("@ProductListFilterType", Convert.ToInt16(bolActive));

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@BranchID", BranchID);
				cmd.Parameters.AddWithValue("@lngProductID", lngProductID);
				cmd.Parameters.AddWithValue("@decQuantity", decQuantity);

				if (cmd.ExecuteNonQuery() > 0) boRetValue = true;
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

			return boRetValue;
		}

		public bool UpdateRewardPoints(long lngProductGroupID, long lngProductSubGroupID, long lngProductID, decimal decRewardPoints)
		{
			bool boRetValue = false;
			try
			{
				string SQL = "CALL procProductUpdateRewardPoints(@lngProductGroupID, @lngProductSubGroupID, @lngProductID, @decRewardPoints);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductGroupID", lngProductGroupID);
				cmd.Parameters.AddWithValue("@lngProductSubGroupID", lngProductSubGroupID);
				cmd.Parameters.AddWithValue("@lngProductID", lngProductID);
				cmd.Parameters.AddWithValue("@decRewardPoints", decRewardPoints);

				if (cmd.ExecuteNonQuery() > 0) boRetValue = true;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				if (cmd.ExecuteNonQuery() > 0) boRetValue = true;
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
				  
		//        MySqlConnection cn = GetConnection();
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
		//        prmQuantity.Value = Quantity;
		//        cmd.Parameters.Add(prmQuantity);

		//        MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
		//        prmProductID.Value = ProductID;
		//        cmd.Parameters.Add(prmProductID);

		//        cmd.ExecuteNonQuery();

		//        /*** 
		//         * July 26, 2011 Remove this 
		//         * 
		//            //ProductComposition clsProductComposition = new ProductComposition(mConnection, mTransaction);
		//            //MySqlDataReader myReader = clsProductComposition.List(ProductID, "CompositionID", SortOption.Ascending);
		//            //while (myReader.Read())
		//            //{
		//            //    long compProductID = myReader.GetInt64("ProductID");
		//            //    long compVariationMatrixID = myReader.GetInt64("VariationMatrixID");
		//            //    decimal comQuantity = myReader.GetDecimal("Quantity");

		//            //    if (compVariationMatrixID !=0)
		//            //    {
		//            //        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, mTransaction);
		//            //        clsProductVariationsMatrix.AddQuantity(compVariationMatrixID, Quantity * comQuantity);
		//            //    }

		//            //    AddQuantity(compProductID, Quantity * comQuantity);
		//            //}
		//            //myReader.Close();
		//         * **/
		//    }

		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose(); 
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				if (cmd.ExecuteNonQuery() > 0) boRetValue = true;
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
				  
		//        MySqlConnection cn = GetConnection();
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
		//        prmQuantity.Value = Quantity;
		//        cmd.Parameters.Add(prmQuantity);

		//        MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
		//        prmProductID.Value = ProductID;
		//        cmd.Parameters.Add(prmProductID);

		//        cmd.ExecuteNonQuery();

		//        ProductComposition clsProductComposition = new ProductComposition(mConnection, mTransaction);
		//        MySqlDataReader myReader = clsProductComposition.List(ProductID, "CompositionID", SortOption.Ascending);
		//        while (myReader.Read())
		//        {
		//            long compProductID = myReader.GetInt64("ProductID");
		//            long compVariationMatrixID = myReader.GetInt64("VariationMatrixID");
		//            decimal comQuantity = myReader.GetDecimal("Quantity");

		//            if (compVariationMatrixID !=0)
		//            {
		//                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, mTransaction);
		//                clsProductVariationsMatrix.SubtractQuantity(compVariationMatrixID, Quantity * comQuantity);
		//            }

		//            SubtractQuantity(compProductID, Quantity * comQuantity);
		//        }
		//        myReader.Close();
		//    }

		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose(); 
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
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

				string SQL = "UPDATE tblBranchInventory SET " +
									"ActualQuantity	    = ActualQuantity + @ActualQuantity " +
								"WHERE ProductID = (SELECT ProductID From tblProducts WHERE Barcode = @Barcode LIMIT 1) " +
								"   AND BranchID = @BranchID ";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmActualQuantity = new MySqlParameter("@ActualQuantity", System.Data.DbType.Decimal);
				prmActualQuantity.Value = ActualQuantity;
				cmd.Parameters.Add(prmActualQuantity);

				MySqlParameter prmBarcode = new MySqlParameter("@Barcode",MySqlDbType.String);
				prmBarcode.Value = Barcode;
				cmd.Parameters.Add(prmBarcode);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = BranchID;
				cmd.Parameters.Add(prmBranchID);

				if (cmd.ExecuteNonQuery() > 0) boRetValue = true;

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

			return boRetValue;
		}

		public void CloseInventory(int intBranchID, long lngCloseByUserID, DateTime dteClosingDate, string strReferenceNo, bool bolUseVariationAsReference)
		{
			try
			{
				string SQL = "CALL procCloseInventory(@intBranchID, @lngUID, @dteClosingDate, @strReferenceNo, @lngContactID, @strContactCode, @bolUseVariationAsReference);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@intBranchID", intBranchID);
				cmd.Parameters.AddWithValue("@lngUID", lngCloseByUserID);
				cmd.Parameters.AddWithValue("@dteClosingDate", dteClosingDate);
				cmd.Parameters.AddWithValue("@strReferenceNo", strReferenceNo);
				cmd.Parameters.AddWithValue("@lngContactID", Contact.DEFAULT_SUPPLIER_ID);
				cmd.Parameters.AddWithValue("@strContactCode", Contact.DEFAULT_SUPPLIER_NAME);
				cmd.Parameters.AddWithValue("@bolUseVariationAsReference", Convert.ToInt16(bolUseVariationAsReference));

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

		public void UpdateRID(long ProductID, long RID)
		{
			try
			{
				string SQL = "CALL procProductUpdateRID(@lngProductID, @lngRID)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@lngRID", RID);

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

		public void UpdateProductReorderOverStock(DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStock(@detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerGroup(long GroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerGroup(@lngGroupID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngGroupID", GroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerSubGroup(long SubGroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSubGroup(@lngSubGroupID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSubGroupID", SubGroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerSupplier(long SupplierID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplier(@lngSupplierID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerSupplierPerGroup(long SupplierID, long GroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerGroup(@lngSupplierID, @lngGroupID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngGroupID", GroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerSupplierPerSubGroup(long SupplierID, long SubGroupID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerSubGroup(@lngSupplierID, @lngSubGroupID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngSubGroupID", SubGroupID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public void UpdateProductReorderOverStockPerSupplier(long SupplierID, long RID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerSupplierPerRID(@lngSupplierID, @lngRID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngSupplierID", SupplierID);
				cmd.Parameters.AddWithValue("@lngRID", RID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		public decimal getReorderQtyByRID(long ProductID, DateTime IDC_StartDate, DateTime IDC_EndDate)
		{
			try
			{
				string SQL = "CALL procUpdateProductReorderOverStockPerProduct(@lngProductID, @detStartDate, @dteEndDate)";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngProductID", ProductID);
				cmd.Parameters.AddWithValue("@detStartDate", IDC_StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@dteEndDate", IDC_EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

				cmd.ExecuteNonQuery();

				SQL = "SELECT RID, RIDMinThreshold, RIDMaxThreshold, Quantity FROM tblProducts WHERE ProductID = @lngProductID;";
				cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.Parameters.AddWithValue("@lngProductID", ProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

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
				MySqlConnection cn = GetConnection();
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL=	"DELETE FROM tblProductPackage WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductUnitMatrix WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblMatrixPackage WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductBaseVariationsMatrix WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductVariations WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProducts WHERE ProductID IN (" + IDs + ");";
				cmd = new MySqlCommand();
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
									"a.ProductID, " +
									"a.ProductCode, " +
									"a.BarCode, " +
									"a.BarCode2, " +
									"a.BarCode3, " +
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
									"a.Price, " +
									"a.WSPrice, " +
									"a.PurchasePrice, " +
									"a.PercentageCommision, " +
									"a.IncludeInSubtotalDiscount, " +
									"a.VAT, " +
									"a.EVAT, " +
									"a.LocalTax, " +
									"a.Quantity, " +
									"a.Quantity MainQuantity," +
									"fnProductQuantityConvert(a.ProductID, a.Quantity, a.BaseUnitID) AS ConvertedMainQuantity, " +
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
									"a.QuantityIN, " +
									"a.QuantityOUT, " +
									"a.ActualQuantity, " +
									"a.MaxThreshold - a.Quantity AS ReorderQty, " +
									"a.RIDMinThreshold, " +
									"a.RIDMaxThreshold, " +
									"a.RIDMaxThreshold - a.Quantity AS RIDReorderQty, " +
									"a.RewardPoints " +
								"FROM tblProducts a " +
								"INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
								"INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID " +
								"INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID " +
								"INNER JOIN tblContacts e ON a.SupplierID = e.ContactID ";
			return stSQL;
		}
		private string SQLSelect(int BranchID)
		{
			string stSQL = "SELECT " +
									"a.ProductID, " +
									"a.ProductCode, " +
									"a.BarCode, " +
									"a.BarCode2, " +
									"a.BarCode3, " +
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
									"a.Price, " +
									"a.WSPrice, " +
									"a.PurchasePrice, " +
									"a.PercentageCommision, " +
									"a.IncludeInSubtotalDiscount, " +
									"a.VAT, " +
									"a.EVAT, " +
									"a.LocalTax, " +
									"a.Quantity AS MainQuantity, " +
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
									"a.QuantityIN AS MainQuantityIN, " +
									"a.QuantityOUT AS MainQuantityOUT, " +
									"a.ActualQuantity AS MainActualQuantity, " +
									"a.MaxThreshold - a.Quantity AS ReorderQty, " +
									"a.RIDMinThreshold, " +
									"a.RIDMaxThreshold, " +
									"a.RIDMaxThreshold - a.Quantity AS RIDReorderQty, " +
									"f.QuantityIN AS QuantityIN, " +
									"f.QuantityOUT AS QuantityOUT, " +
									"f.Quantity AS Quantity, " +
									"f.ActualQuantity AS ActualQuantity, " +
									"fnProductQuantityConvert(f.ProductID, f.Quantity, a.BaseUnitID) AS ConvertedMainQuantity, " +
									"a.RewardPoints " +
								"FROM tblProducts a " +
								"   INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID " +
								"   INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID " +
								"   INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID " +
								"   INNER JOIN tblContacts e ON a.SupplierID = e.ContactID " +
								"   INNER JOIN tblBranchInventory f ON a.ProductID = f.ProductID " +
								"WHERE f.BranchID = " + BranchID + " ";
			return stSQL;
		}
		private string SQLSelectSimple()
		{
			string stSQL = "SELECT " +
									"a.ProductID, " +
									"a.ProductCode, " +
									"a.BarCode, " +
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
									//"a.MinThreshold, " +
									//"a.MaxThreshold, " +
									//"a.RID, " +
									"a.SupplierID, " +
									//"a.ChartOfAccountIDPurchase, " +
									//"a.ChartOfAccountIDSold, " +
									//"a.ChartOfAccountIDInventory, " +
									//"a.ChartOfAccountIDTaxPurchase, " +
									//"a.ChartOfAccountIDTaxSold, " +
									//"a.IsItemSold, " +
									//"a.WillPrintProductComposition, " +
									//"a.VariationCount, " +
									//"a.QuantityIN, " +
									//"a.QuantityOUT, " +
									"a.ActualQuantity, " +
									//"a.MaxThreshold - a.Quantity AS ReorderQty, " +
									//"a.RIDMinThreshold, " +
									//"a.RIDMaxThreshold, " +
									//"a.RIDMaxThreshold - a.Quantity AS RIDReorderQty, " +
									"a.RewardPoints " +
								"FROM tblProducts a ";
			return stSQL;
		}
		private string SQLSelectSimple(int BranchID)
		{
			string stSQL = "SELECT " +
									"a.ProductID, " +
									"a.ProductCode, " +
									"a.BarCode, " +
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
									"a.Quantity AS MainQuantity, " +
									"b.Quantity, " +
									"a.SupplierID, " +
									"a.ActualQuantity AS MainActualQuantity, " +
									"fnProductQuantityConvert(b.ProductID, b.Quantity, a.BaseUnitID) AS ConvertedMainQuantity, " +
									"b.ActualQuantity, " +
									"a.RewardPoints " +
								"FROM tblProducts a " +
								"   INNER JOIN tblBranchInventory b ON a.ProductID = b.ProductID " +
								"WHERE a.Deleted = 0 AND b.BranchID = " + BranchID.ToString() + " ";
			return stSQL;
		}

		private string SQLSelect(ProductColumns clsProductColumns)
		{
			string stSQL = "SELECT ";

			if (clsProductColumns.ProductCode) stSQL += "tblProducts." + ProductColumnNames.ProductCode + ", ";
			if (clsProductColumns.IncludeAllPackages)
			{
				if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.BarCode + "1 AS BarCode, ";
				if (clsProductColumns.BarCode) stSQL += "tblProductPackage." + ProductColumnNames.BarCode + "1, ";
				if (clsProductColumns.BarCode2) stSQL += "tblProductPackage." + ProductColumnNames.BarCode2 + ", ";
				if (clsProductColumns.BarCode3) stSQL += "tblProductPackage." + ProductColumnNames.BarCode3 + ", ";
			}
			else
			{
				if (clsProductColumns.BarCode) stSQL += "tblProducts." + ProductColumnNames.BarCode + ", ";
				if (clsProductColumns.BarCode) stSQL += "tblProducts." + ProductColumnNames.BarCode + " AS BarCode1, ";
				if (clsProductColumns.BarCode2) stSQL += "tblProducts." + ProductColumnNames.BarCode2 + ", ";
				if (clsProductColumns.BarCode3) stSQL += "tblProducts." + ProductColumnNames.BarCode3 + ", ";
			}
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
			if (clsProductColumns.IncludeAllPackages)
			{
				if (clsProductColumns.UnitID) stSQL += "tblProductPackage." + ProductColumnNames.UnitID + ", ";
				if (clsProductColumns.UnitCode) stSQL += "tblUnitPackage." + ProductColumnNames.UnitCode + ", ";
				if (clsProductColumns.UnitName) stSQL += "tblUnitPackage." + ProductColumnNames.UnitName + ", ";
			}
			else 
			{
				if (clsProductColumns.UnitID) stSQL += "tblProducts." + ProductColumnNames.UnitID + ", ";
				if (clsProductColumns.UnitCode) stSQL += "tblUnit." + ProductColumnNames.UnitCode + ", ";
				if (clsProductColumns.UnitName) stSQL += "tblUnit." + ProductColumnNames.UnitName + ", ";
				
			}
			if (clsProductColumns.DateCreated) stSQL += "tblProducts." + ProductColumnNames.DateCreated + ", ";
			if (clsProductColumns.Deleted) stSQL += "tblProducts." + ProductColumnNames.Deleted + ", ";
			if (clsProductColumns.Active) stSQL += "tblProducts." + ProductColumnNames.Active+ ", ";
			if (clsProductColumns.IncludeAllPackages)
			{
				if (clsProductColumns.Price) stSQL += "tblProductPackage." + ProductColumnNames.Price + ", ";
				if (clsProductColumns.WSPrice) stSQL += "tblProductPackage." + ProductColumnNames.WSPrice + ", ";
				if (clsProductColumns.PurchasePrice) stSQL += "tblProductPackage." + ProductColumnNames.PurchasePrice + ", ";
			}
			else 
			{
				if (clsProductColumns.Price) stSQL += "tblProducts." + ProductColumnNames.Price + ", ";
				if (clsProductColumns.WSPrice) stSQL += "tblProducts." + ProductColumnNames.WSPrice + ", ";
				if (clsProductColumns.PurchasePrice) stSQL += "tblProducts." + ProductColumnNames.PurchasePrice + ", ";
			}
			if (clsProductColumns.PercentageCommision) stSQL += "tblProducts." + ProductColumnNames.PercentageCommision + ", ";
			if (clsProductColumns.IncludeInSubtotalDiscount) stSQL += "tblProducts." + ProductColumnNames.IncludeInSubtotalDiscount + ", ";
			if (clsProductColumns.VAT) stSQL += "tblProducts." + ProductColumnNames.VAT + ", ";
			if (clsProductColumns.EVAT) stSQL += "tblProducts." + ProductColumnNames.EVAT + ", ";
			if (clsProductColumns.LocalTax) stSQL += "tblProducts." + ProductColumnNames.LocalTax + ", ";
			if (clsProductColumns.MainQuantity) stSQL += "tblProducts." + ProductColumnNames.MainQuantity.Replace("Main",string.Empty) + " AS MainQuantity, ";
			if (clsProductColumns.MainQuantity) stSQL += "tblProducts." + ProductColumnNames.MainQuantity.Replace("Main", string.Empty) + " AS Quantity, ";
			if (clsProductColumns.MainQuantity) stSQL += "fnProductQuantityConvert(tblProducts.ProductID, tblProducts." + ProductColumnNames.MainQuantity.Replace("Main", string.Empty) + ", tblProducts.BaseUnitID) AS ConvertedMainQuantity, ";
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
			if (clsProductColumns.MainQuantityIN) stSQL += "tblProducts." + ProductColumnNames.MainQuantityIN.Replace("Main", string.Empty) + " AS MainQuantityIN, ";
			if (clsProductColumns.MainQuantityOUT) stSQL += "tblProducts." + ProductColumnNames.MainQuantityOUT.Replace("Main", string.Empty) + " AS MainQuantityOUT, ";
			if (clsProductColumns.MainActualQuantity) stSQL += "tblProducts." + ProductColumnNames.MainActualQuantity.Replace("Main", string.Empty) + " AS MainActualQuantity, ";
			if (clsProductColumns.MainActualQuantity) stSQL += "fnProductQuantityConvert(tblProducts.ProductID, tblProducts." + ProductColumnNames.MainActualQuantity.Replace("Main", string.Empty) + ", tblProducts.BaseUnitID) AS ConvertedMainActualQuantity, ";
			if (clsProductColumns.ReorderQty) stSQL += "tblProducts." + ProductColumnNames.MaxThreshold + " - tblProducts." + ProductColumnNames.MainQuantity.Replace("Main", string.Empty) + " AS ReorderQty, ";
			if (clsProductColumns.RIDMinThreshold) stSQL += "tblProducts." + ProductColumnNames.RIDMinThreshold + ", ";
			if (clsProductColumns.RIDMaxThreshold) stSQL += "tblProducts." + ProductColumnNames.RIDMaxThreshold + ", ";
			if (clsProductColumns.RIDReorderQty) stSQL += "tblProducts.RIDMaxThreshold - tblProducts.Quantity AS RIDReorderQty, ";
			if (clsProductColumns.BranchID) stSQL += "tblBranchInventory." + ProductColumnNames.BranchID + ", ";
            if (clsProductColumns.BranchQuantityIN) stSQL += "tblBranchInventory." + ProductColumnNames.BranchQuantityIN.Replace("Branch", string.Empty) + " AS BranchQuantityIN, "; else stSQL += "0 AS BranchQuantityIN, ";
            if (clsProductColumns.BranchQuantityOUT) stSQL += "tblBranchInventory." + ProductColumnNames.BranchQuantityOUT.Replace("Branch", string.Empty) + " AS BranchQuantityOUT, "; else stSQL += "0 AS BranchQuantityOUT, ";
            if (clsProductColumns.BranchQuantity) stSQL += "tblBranchInventory." + ProductColumnNames.BranchQuantity.Replace("Branch", string.Empty) + " AS BranchQuantity, "; else stSQL += "0 AS BranchQuantity, ";
            if (clsProductColumns.BranchQuantity) stSQL += "fnProductQuantityConvert(tblBranchInventory.ProductID, tblBranchInventory." + ProductColumnNames.BranchQuantity.Replace("Branch", string.Empty) + ", tblProducts.BaseUnitID) AS ConvertedBranchQuantity, "; else stSQL += "0 AS ConvertedBranchQuantity, ";
            if (clsProductColumns.BranchActualQuantity) stSQL += "tblBranchInventory." + ProductColumnNames.BranchActualQuantity.Replace("Branch", string.Empty) + " AS BranchActualQuantity, "; else stSQL += "0 AS BranchActualQuantity, ";
            if (clsProductColumns.BranchActualQuantity) stSQL += "fnProductQuantityConvert(tblBranchInventory.ProductID, tblBranchInventory." + ProductColumnNames.BranchActualQuantity.Replace("Branch", string.Empty) + ", tblProducts.BaseUnitID) AS ConvertedBranchActualQuantity, "; else stSQL += "0 AS ConvertedBranchActualQuantity, ";
			if (clsProductColumns.RewardPoints) stSQL += "tblProducts." + ProductColumnNames.RewardPoints + ", ";
			if (clsProductColumns.SequenceNo) stSQL += "tblProducts." + ProductColumnNames.SequenceNo + ", ";

			stSQL += "tblProducts.ProductID FROM tblProducts ";
			if (clsProductColumns.IncludeAllPackages)
				stSQL += "INNER JOIN tblProductPackage ON tblProducts.ProductID = tblProductPackage.ProductID ";

			if (clsProductColumns.ProductSubGroupCode || clsProductColumns.ProductSubGroupName || clsProductColumns.ProductGroupID ||
				clsProductColumns.ProductGroupCode || clsProductColumns.ProductGroupName || clsProductColumns.OrderSlipPrinter)
				stSQL += "INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID ";

			if (clsProductColumns.ProductGroupCode || clsProductColumns.ProductGroupName || clsProductColumns.OrderSlipPrinter)
				stSQL += "INNER JOIN tblProductGroup ON tblProductSubGroup.ProductGroupID = tblProductGroup.ProductGroupID ";

			if (clsProductColumns.BaseUnitCode || clsProductColumns.BaseUnitName)
				stSQL += "INNER JOIN tblUnit ON tblProducts.BaseUnitID = tblUnit.UnitID ";

			if (clsProductColumns.IncludeAllPackages)
				if (clsProductColumns.UnitCode || clsProductColumns.UnitName)
					stSQL += "INNER JOIN tblUnit tblUnitPackage ON tblProductPackage.UnitID = tblUnitPackage.UnitID ";

			if (clsProductColumns.SupplierCode || clsProductColumns.SupplierName)
				stSQL += "INNER JOIN tblContacts ON tblProducts.SupplierID = tblContacts.ContactID ";

			if (clsProductColumns.BranchID || clsProductColumns.BranchQuantityIN || clsProductColumns.BranchQuantityOUT || clsProductColumns.BranchQuantity || clsProductColumns.BranchActualQuantity)
				stSQL += "INNER JOIN tblBranchInventory ON tblProducts.ProductID = tblBranchInventory.ProductID ";
			
			return stSQL;
		}

		#region Details

        public bool IsExist(long ProductID, string BarCode1, string BarCode2, string BarCode3)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = "CALL procProductIsExist(@ProductID, @BarCode);";

                MySqlConnection cn = GetConnection();

                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                bool boRetValue = false;

                if (BarCode1 != string.Empty && BarCode1 != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode1);
                    System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    foreach(System.Data.DataRow dr in dt.Rows)
                    {
                        if (Int64.Parse(dr["ProductCount"].ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }

                if (BarCode2 != string.Empty && BarCode2 != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode2);
                    System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        if (Int64.Parse(dr["ProductCount"].ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }

                if (BarCode3 != string.Empty && BarCode3 != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@BarCode", BarCode3);
                    System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
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
		public bool WillPrintProductComposition(long ProductID)
		{
			bool bolRetValue = false;

			try
			{
				string SQL = "SELECT WillPrintProductComposition FROM tblProducts a WHERE a.ProductId = @ProductID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					bolRetValue = bool.Parse(dr["WillPrintProductComposition"].ToString());
				}
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

			return bolRetValue;
		}
		public int get_BaseUnitID(long ProductID)
		{
			try
			{
				string SQL = "Select BaseUnitID FROM tblProducts WHERE ProductID = @ProductID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                int iRetValue = 1;
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					iRetValue = Int32.Parse(dr["BaseUnitID"].ToString());
				}

				return iRetValue;
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
		public ProductDetails Details(long ProductID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE a.ProductId = @ProductID AND a.deleted=0;";

				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);

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
		public ProductDetails Details(string BarCode)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE (BarCode = @BarCode OR BarCode2 = @BarCode OR BarCode3 = @BarCode);";

				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
				prmBarCode.Value = BarCode;
				cmd.Parameters.Add(prmBarCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);

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
		public ProductDetails DetailsByCode(string ProductCode)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ProductCode = @ProductCode;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);

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

		public ProductDetails Details(int BranchID, long ProductID)
		{
			try
			{
				string SQL = SQLSelect(BranchID) + "AND a.ProductId = @ProductID;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);
                
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
		public ProductDetails Details(int BranchID, string BarCode)
		{
			try
			{
				string SQL = SQLSelect(BranchID) + "AND BarCode = @BarCode OR BarCode2 = @BarCode OR BarCode3 = @BarCode;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
				prmBarCode.Value = BarCode;
				cmd.Parameters.Add(prmBarCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);
                
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
		public ProductDetails DetailsByCode(int BranchID, string ProductCode)
		{
			try
			{
				string SQL = SQLSelect(BranchID) + "AND ProductCode = @ProductCode;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

				ProductDetails Details = SetDetails(dt);
                
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
					Details.MainQuantity = myReader.GetDecimal("MainQuantity");
					Details.ConvertedMainQuantity = "" + myReader["ConvertedMainQuantity"].ToString();
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
                    Details.MainQuantity = Decimal.Parse(dr["MainQuantity"].ToString());
                    Details.ConvertedMainQuantity = dr["ConvertedMainQuantity"].ToString();
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
                }

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

		public System.Data.DataTable ProductIDAsDataTable()
		{
			try
			{
				string SQL = "SELECT ProductID FROM tblProducts WHERE Deleted = 0";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;

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

		public ProductDetails[] List()
		{
			try
			{
				System.Collections.ArrayList items = new System.Collections.ArrayList();
				System.Data.DataTable dtProductIDs = ProductIDAsDataTable();
				foreach (System.Data.DataRow drProductID in dtProductIDs.Rows)
				{
					items.Add(Details(long.Parse(drProductID["ProductID"].ToString())));
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int16);			
				prmProductSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

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

		public MySqlDataReader AdvanceSearch(string ProductCode, string ProductDesc, 
			string ProductGroupName, string ProductSubGroupName, string UnitName,
			string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.Deleted = 0 AND a.Active = 1 ";
				
				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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
		

		public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(ProductListFilterType.ShowActiveOnly, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, false, SortField, SortOrder);

				return dt;
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
		public System.Data.DataTable SearchDataTableActiveInactive(ProductListFilterType clsProductListFilterType, string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(clsProductListFilterType, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, false, SortField, SortOrder);

				return dt;
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
		public System.Data.DataTable SearchSaleableDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				System.Data.DataTable dt = SearchDataTable(ProductListFilterType.ShowActiveOnly, SearchKey, Constants.ZERO, Constants.ZERO, string.Empty, Constants.ZERO, string.Empty, Limit, isQuantityGreaterThanZERO, true, SortField, SortOrder);

				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode ",MySqlDbType.String);
				prmProductGroupCode.Value = ProductGroupCode + "%";
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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
		public System.Data.DataTable SearchDataTableSimple(int BranchID, ProductListFilterType clsProductListFilterType, string SearchKey, long SupplierID, long ProductGroupID, string ProductGroupName, long ProductSubGroupID, string ProductSubGroupName, Int32 Limit, bool isQuantityGreaterThanZERO, bool CheckIItemisSold, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelectSimple(BranchID);

				if (CheckIItemisSold) SQL += "AND a.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (SearchKey != string.Empty)
				{ SQL += "AND (Barcode LIKE @SearchKey OR Barcode2 LIKE @SearchKey OR ProductCode LIKE @SearchKey) "; }

				if (SupplierID != 0)
					SQL += "AND (SupplierID = " + SupplierID + " OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = " + SupplierID + ")) ";

				if (ProductGroupID != Constants.ZERO)
				{ SQL += "AND a.ProductGroupID = " + ProductGroupID + " "; }

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);
				
				return dt;
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
		public System.Data.DataTable ProductIDandCodeDataTable(ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive, string SearchKey="", long SupplierID = 0, long ProductGroupID = 0, string ProductGroupName = "", long ProductSubGroupID=0, string ProductSubGroupName="", Int32 Limit=100, bool isQuantityGreaterThanZERO=false, bool CheckIItemisSold=true, string SortField="ProductCode", SortOption SortOrder=SortOption.Ascending)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				string SQL = "SELECT " +
									"a.ProductID, " +
									"a.ProductCode " +
								"FROM tblProducts a ";
				SQL += "WHERE a.Deleted = 0 ";

				if (CheckIItemisSold) SQL += "AND a.IsItemSold = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
				if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

				if (SearchKey != string.Empty)
				{
					SQL += "AND (Barcode LIKE @SearchKey " +
											"OR Barcode2 LIKE @SearchKey " +
											"OR Barcode3 LIKE @SearchKey " +
											"OR ProductCode LIKE @SearchKey " +
											"OR ProductDesc LIKE @SearchKey) ";
				}
				if (SupplierID != 0)
				{
					SQL += "AND (SupplierID = @SupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = @SupplierID)) ";

					MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
					prmSupplierID.Value = SupplierID;
					cmd.Parameters.Add(prmSupplierID);
				}

				if (ProductGroupID != Constants.ZERO)
				{
					SQL += "AND a.ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID) ";

					MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
					prmProductGroupID.Value = ProductGroupID;
					cmd.Parameters.Add(prmProductGroupID);
				}

				if (ProductSubGroupID != Constants.ZERO)
				{
					SQL += "AND a.ProductSubGroupID = @ProductSubGroupID ";

					MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
					prmProductSubGroupID.Value = ProductSubGroupID;
					cmd.Parameters.Add(prmProductSubGroupID);
				}

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

				MySqlConnection cn = GetConnection();

				
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();
				System.Data.DataTable dt = new System.Data.DataTable("Products");
				MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, cn);
				adapter.Fill(dt);

				return dt;		
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
		
		//public System.Data.DataTable ForReorder(string ProductGroupName, string ProductSubGroupName)
		//{
		//    try
		//    {
		//        System.Data.DataTable dt = ForReorder(0, ProductGroupName, ProductSubGroupName);

		//        return dt;		
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
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

				

				MySqlConnection cn = GetConnection();
				System.Data.DataTable dt = new System.Data.DataTable("Products");
				MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, cn);
				adapter.Fill(dt);

				return dt;
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

				MySqlConnection cn = GetConnection();
				System.Data.DataTable dt = new System.Data.DataTable("Products");
				MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, cn);
				adapter.Fill(dt);

				return dt;		
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

		public System.Data.DataTable ListAsDataTable(ProductColumns clsProductColumns, ProductDetails clsSearchKeys, ProductListFilterType clsProductListFilterType,
					long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, Int32 Limit, bool isQuantityGreaterThanZERO, string SortField, SortOption SortOrder)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				clsProductColumns.IncludeAllPackages = true; 

				// include branchid in the selection if branchid is not zero
				if (clsSearchKeys.BranchID != 0) clsProductColumns.BranchID = true;
				if (clsSearchKeys.BarCode != string.Empty) clsProductColumns.BarCode = true;
				if (clsSearchKeys.BarCode2 != string.Empty) clsProductColumns.BarCode2 = true;
				if (clsSearchKeys.BarCode3 != string.Empty) clsProductColumns.BarCode3 = true;

				string SQL = SQLSelect(clsProductColumns) + "WHERE tblProducts.deleted=0 ";

				if (SequenceNoStart != Constants.ZERO)
				{
					if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
						SQL += "AND tblProducts.SequenceNo < " + SequenceNoStart.ToString() + " ";
					else
						SQL += "AND tblProducts.SequenceNo > " + SequenceNoStart.ToString() + " ";
				}

				if (clsSearchKeys.BranchID != Constants.ZERO)
				{
					SQL += "AND tblBranchInventory.BranchID = @BranchID ";
					MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
					prmBranchID.Value = clsSearchKeys.BranchID;
					cmd.Parameters.Add(prmBranchID);
				}
				if (clsSearchKeys.IsItemSold)
				{
					SQL += "AND tblProducts.IsItemSold = @IsItemSold ";
					MySqlParameter prmIsItemSold = new MySqlParameter("@IsItemSold",MySqlDbType.Int16);
					prmIsItemSold.Value = clsSearchKeys.IsItemSold;
					cmd.Parameters.Add(prmIsItemSold);
				}
				if (clsSearchKeys.SupplierID !=0 )
				{
					SQL += "AND tblProducts.SupplierID = @SupplierID ";
					MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
					prmSupplierID.Value = clsSearchKeys.SupplierID;
					cmd.Parameters.Add(prmSupplierID);
				}
				if (clsSearchKeys.ProductSubGroupID != 0)
				{
					SQL += "AND tblProducts.ProductSubGroupID = @ProductSubGroupID ";
					MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
					prmProductSubGroupID.Value = clsSearchKeys.ProductSubGroupID;
					cmd.Parameters.Add(prmProductSubGroupID);
				}
				if (clsSearchKeys.ProductSubGroupName != string.Empty && clsSearchKeys.ProductSubGroupName != null)
				{
					SQL += "AND tblProductSubGroup.ProductSubGroupName LIKE @ProductSubGroupName ";
					MySqlParameter prmProductSubGroupName = new MySqlParameter("@ProductSubGroupName",MySqlDbType.String);
					prmProductSubGroupName.Value = clsSearchKeys.ProductSubGroupName + "%";
					cmd.Parameters.Add(prmProductSubGroupName);
				}
				if (clsSearchKeys.ProductGroupID != 0)
				{
					SQL += "AND tblProductSubGroup.ProductGroupID = @ProductGroupID ";
					MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
					prmProductGroupID.Value = clsSearchKeys.ProductGroupID;
					cmd.Parameters.Add(prmProductGroupID);
				}
				if (clsSearchKeys.ProductGroupName != string.Empty && clsSearchKeys.ProductGroupName != null)
				{
					SQL += "AND tblProductGroup.ProductGroupName LIKE @ProductGroupName ";
					MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);
					prmProductGroupName.Value = clsSearchKeys.ProductGroupName + "%";
					cmd.Parameters.Add(prmProductGroupName);
				}
				if (clsProductListFilterType != ProductListFilterType.ShowActiveAndInactive)
				{
					SQL += "AND tblProducts.Active = @Active ";
					MySqlParameter prmActive = new MySqlParameter("@Active",MySqlDbType.Int16);
					if (clsProductListFilterType == ProductListFilterType.ShowActiveOnly) prmActive.Value = 1;
					if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly) prmActive.Value = 0;
					cmd.Parameters.Add(prmActive);
				}
				if (isQuantityGreaterThanZERO)
				{
					if (clsSearchKeys.BranchID == Constants.ZERO)
						SQL += "AND tblProducts.Quantity > 0 ";
					else
						SQL += "AND tblBranchInventory.Quantity > 0 ";
				}

				string SQLSearch = string.Empty;
				string SQLPackage = string.Empty;
				if (clsProductColumns.IncludeAllPackages)
				{
					if (clsSearchKeys.BarCode != string.Empty && clsSearchKeys.BarCode != null)
					{
						if (SQLSearch != string.Empty) SQLSearch += "OR ";
						SQLSearch += "tblProducts.BarCode LIKE @BarCode ";

						MySqlParameter prmBarcode = new MySqlParameter("@BarCode",MySqlDbType.String);
						prmBarcode.Value = clsSearchKeys.BarCode + "%";
						cmd.Parameters.Add(prmBarcode);
					}
					if (clsSearchKeys.BarCode2 != string.Empty && clsSearchKeys.BarCode2 != null)
					{
						if (SQLSearch != string.Empty) SQLSearch += "OR ";
						SQLSearch += "tblProducts.BarCode2 LIKE @BarCode ";

						MySqlParameter prmBarCode2 = new MySqlParameter("@BarCode2",MySqlDbType.String);
						prmBarCode2.Value = clsSearchKeys.BarCode2 + "%";
						cmd.Parameters.Add(prmBarCode2);
					}
					if (clsSearchKeys.BarCode3 != string.Empty && clsSearchKeys.BarCode3 != null)
					{
						if (SQLSearch != string.Empty) SQLSearch += "OR ";
						SQLSearch += "tblProducts.BarCode3 LIKE @BarCode ";

						MySqlParameter prmBarCode3 = new MySqlParameter("@BarCode3",MySqlDbType.String);
						prmBarCode3.Value = clsSearchKeys.BarCode3 + "%";
						cmd.Parameters.Add(prmBarCode3);
					}
				}
				else
				{
					if (clsSearchKeys.BarCode != string.Empty && clsSearchKeys.BarCode != null)
					{
						if (SQLPackage != string.Empty) SQLPackage += "OR ";
						SQLPackage += "tblProductPackage.BarCode1 LIKE @BarCode ";

						MySqlParameter prmBarcode = new MySqlParameter("@BarCode",MySqlDbType.String);
						prmBarcode.Value = clsSearchKeys.BarCode + "%";
						cmd.Parameters.Add(prmBarcode);
					}
					if (clsSearchKeys.BarCode2 != string.Empty && clsSearchKeys.BarCode2 != null)
					{
						if (SQLPackage != string.Empty) SQLPackage += "OR ";
						SQLPackage += "tblProductPackage.BarCode2 LIKE @BarCode ";

						MySqlParameter prmBarCode2 = new MySqlParameter("@BarCode2",MySqlDbType.String);
						prmBarCode2.Value = clsSearchKeys.BarCode2 + "%";
						cmd.Parameters.Add(prmBarCode2);
					}
					if (clsSearchKeys.BarCode3 != string.Empty && clsSearchKeys.BarCode3 != null)
					{
						if (SQLPackage != string.Empty) SQLPackage += "OR ";
						SQLPackage += "tblProductPackage.BarCode3 LIKE @BarCode ";

						MySqlParameter prmBarCode3 = new MySqlParameter("@BarCode3",MySqlDbType.String);
						prmBarCode3.Value = clsSearchKeys.BarCode3 + "%";
						cmd.Parameters.Add(prmBarCode3);
					}
					if (SQLPackage != string.Empty)
						SQLSearch += "AND (SELECT DISTINCT(ProductID) FROM tblProductPackage WHERE " + SQLPackage + ")";
				}
				if (clsSearchKeys.ProductCode != string.Empty && clsSearchKeys.ProductCode != null)
				{
					if (SQLSearch != string.Empty) SQLSearch += "OR ";
					SQLSearch += "tblProducts.ProductCode LIKE @ProductCode ";

					MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
					prmProductCode.Value = clsSearchKeys.ProductCode + "%";
					cmd.Parameters.Add(prmProductCode);
				}
				if (clsSearchKeys.ProductDesc != string.Empty && clsSearchKeys.ProductDesc != null)
				{
					if (SQLSearch != string.Empty) SQLSearch += "OR ";
					SQLSearch += "tblProducts.ProductDesc LIKE @ProductDesc ";

					MySqlParameter prmProductDesc = new MySqlParameter("@ProductDesc",MySqlDbType.String);
					prmProductDesc.Value = clsSearchKeys.ProductDesc + "%";
					cmd.Parameters.Add(prmProductDesc);
				}
				if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder == SortOption.Ascending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0) SQL += "LIMIT " + Limit + " ";

				MySqlConnection cn = GetConnection();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				if (BranchID != Constants.ZERO) SQL += "AND tblBranchInventory.BranchID = " + BranchID.ToString() + " ";
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
					if (BranchID == Constants.ZERO)
						SQL += "AND tblProducts.Quantity > 0 ";
					else
						SQL += "AND tblBranchInventory.Quantity > 0 ";

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

				if (BranchID != Constants.ZERO) SQL += "AND tblBranchInventory.BranchID = " + BranchID.ToString() + " ";
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
					if (BranchID == Constants.ZERO)
						SQL += "AND tblProducts.Quantity > 0 ";
					else
						SQL += "AND tblBranchInventory.Quantity > 0 ";

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;


				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				System.Data.DataTable dt = new System.Data.DataTable("tblProducts");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

		#region Inheritance

		public void InheritSubGroupVariations(Int64 ProductSubGroupID, Int64 ProductID)
		{
			try 
			{
				string SQL	= "INSERT INTO tblProductVariations (ProductID, VariationID) " + 
									"SELECT @ProductID, VariationID FROM tblProductSubGroupVariations " + 
								"WHERE SubGroupID = @SubGroupID;";

				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int32);			
				prmSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				cmd.CommandText = SQL;
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
		public void InheritSubGroupVariationsMatrix(Int64 ProductSubGroupID, Int64 ProductID, ProductDetails prodDetails)
		{
			try 
			{	
				MySqlConnection cn = GetConnection();

				ProductBaseMatrixDetails clsBaseDetails;

				ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(mConnection, mTransaction);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, mTransaction);
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
					clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(mConnection, mTransaction);
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
		public void InheritSubGroupUnitMatrix(Int64 ProductSubGroupID, Int64 ProductID)
		{
			try 
			{	
				MySqlConnection cn = GetConnection();

				ProductSubGroupUnitsMatrix clsProductSubGroupUnitsMatrix = new ProductSubGroupUnitsMatrix(mConnection, mTransaction);

				ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(mConnection, mTransaction);
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

		#region Change VAT, EVAT and Localtax

		// Dec 10, 2011 : Obsolete, change to ChangeTax
		//public void ChangeVAT(decimal OldVAT, decimal NewVAT)
		//{
		//    try 
		//    {
		//        string SQL =	"UPDATE tblProducts SET " +
		//                            "VAT		= @NewVAT " +
		//                        "WHERE VAT		= @OldVAT;";
				  
		//        MySqlConnection cn = GetConnection();
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);			
		//        prmNewVAT.Value = NewVAT;
		//        cmd.Parameters.Add(prmNewVAT);

		//        MySqlParameter prmOldVAT = new MySqlParameter("@OldVAT",MySqlDbType.Decimal);			
		//        prmOldVAT.Value = OldVAT;
		//        cmd.Parameters.Add(prmOldVAT);

		//        cmd.ExecuteNonQuery();

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(cn, Transaction);
		//        clsMatrixPackage.ChangeVAT(OldVAT, NewVAT);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(cn, mTransaction);
		//        clsProductVariationsMatrix.ChangeVAT(OldVAT, NewVAT);

		//        ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
		//        clsProductPackage.ChangeVAT(OldVAT, NewVAT);
		//    }

		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose(); 
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
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
				  
		//        MySqlConnection cn = GetConnection();
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);			
		//        prmNewEVAT.Value = NewEVAT;
		//        cmd.Parameters.Add(prmNewEVAT);

		//        MySqlParameter prmOldEVAT = new MySqlParameter("@OldEVAT",MySqlDbType.Decimal);			
		//        prmOldEVAT.Value = OldEVAT;
		//        cmd.Parameters.Add(prmOldEVAT);

		//        cmd.ExecuteNonQuery();

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(cn, Transaction);
		//        clsMatrixPackage.ChangeEVAT(OldEVAT, NewEVAT);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(cn, mTransaction);
		//        clsProductVariationsMatrix.ChangeEVAT(OldEVAT, NewEVAT);

		//        ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
		//        clsProductPackage.ChangeEVAT(OldEVAT, NewEVAT);
		//    }

		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose(); 
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
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
				  
		//        MySqlConnection cn = GetConnection();
				
		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);			
		//        prmNewLocalTax.Value = NewLocalTax;
		//        cmd.Parameters.Add(prmNewLocalTax);

		//        MySqlParameter prmOldLocalTax = new MySqlParameter("@OldLocalTax",MySqlDbType.Decimal);			
		//        prmOldLocalTax.Value = OldLocalTax;
		//        cmd.Parameters.Add(prmOldLocalTax);

		//        cmd.ExecuteNonQuery();

		//        MatrixPackage clsMatrixPackage = new MatrixPackage(cn, Transaction);
		//        clsMatrixPackage.ChangeLocalTax(OldLocalTax, NewLocalTax);

		//        ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(cn, mTransaction);
		//        clsProductVariationsMatrix.ChangeLocalTax(OldLocalTax, NewLocalTax);

		//        ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
		//        clsProductPackage.ChangeLocalTax(OldLocalTax, NewLocalTax);
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose(); 
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				cmd.ExecuteNonQuery();

				ProductPackage clsProductPackage = new ProductPackage(cn, mTransaction);
				clsProductPackage.ChangeTax(ProductGroupID, ProductSubGroupID, ProductID, NewVAT, NewEVAT, NewLocalTax);

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(cn, mTransaction);
				clsProductVariationsMatrix.ChangeTax(ProductGroupID, ProductSubGroupID, ProductID, NewVAT, NewEVAT, NewLocalTax);
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

		#region AddQuantityBranchField

		public bool AddQuantityBranchField(int ID)
		{
			try
			{
				MySqlConnection cn = GetConnection();
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL = "ALTER TABLE tblProducts ADD Quantity" + ID.ToString() + " decimal(18,2) NOT NULL DEFAULT 0;";
				cmd = new MySqlCommand();
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

