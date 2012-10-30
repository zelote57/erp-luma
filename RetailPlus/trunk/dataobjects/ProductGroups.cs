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
	public struct ProductGroupDetails
	{
		public Int64 ProductGroupID;
		public string ProductGroupCode;
		public string ProductGroupName;
		public Int32 BaseUnitID;
		public string BaseUnitName;
		public decimal Price;
		public decimal PurchasePrice;
		public Int16 IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public OrderSlipPrinter OrderSlipPrinter;
        public int ChartOfAccountIDPurchase;
        public int ChartOfAccountIDSold;
        public int ChartOfAccountIDInventory;
        public int ChartOfAccountIDTaxPurchase;
        public int ChartOfAccountIDTaxSold;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductGroup
	{
        public const long DEFAULT_GROUP_ID = 1;
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

		public ProductGroup()
		{
			
		}

		public ProductGroup(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(ProductGroupDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductGroup (" +
								"ProductGroupCode, " +
								"ProductGroupName, " +
								"BaseUnitID, " +
								"Price, " +
								"PurchasePrice, " +
								"IncludeInSubtotalDiscount, " +
								"VAT, " +
								"EVAT, " +
								"LocalTax," +
                                "OrderSlipPrinter" +
							") VALUES (" +
								"@ProductGroupCode, " +
								"@ProductGroupName, " +
								"@BaseUnitID, " +
								"@Price, " +
								"@PurchasePrice, " +
								"@IncludeInSubtotalDiscount, " +
								"@VAT, " +
								"@EVAT, " +
								"@LocalTax," +
                                "@OrderSlipPrinter);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);			
				prmProductGroupCode.Value = Details.ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);			
				prmProductGroupName.Value = Details.ProductGroupName;
				cmd.Parameters.Add(prmProductGroupName);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Details.Price;
				cmd.Parameters.Add(prmPrice);

				MySqlParameter prmPurchasePrice = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);			
				prmPurchasePrice.Value = Details.PurchasePrice;
				cmd.Parameters.Add(prmPurchasePrice);

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

                MySqlParameter prmOrderSlipPrinter = new MySqlParameter("@OrderSlipPrinter",MySqlDbType.Int16);
                prmOrderSlipPrinter.Value = (int) Details.OrderSlipPrinter;
                cmd.Parameters.Add(prmOrderSlipPrinter);
                
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
		public void Update(ProductGroupDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductGroup SET " + 
								"ProductGroupCode		= @ProductGroupCode, " + 
								"ProductGroupName		= @ProductGroupName, " +  
								"BaseUnitID				= @BaseUnitID, " +
								"Price					= @Price, " +
								"PurchasePrice			= @PurchasePrice, " +
								"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
								"VAT					= @VAT, " +
								"EVAT					= @EVAT, " +
								"LocalTax				= @LocalTax, " +
                                "OrderSlipPrinter       = @OrderSlipPrinter " +
							"WHERE ProductGroupID = @ProductGroupID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);			
				prmProductGroupCode.Value = Details.ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);			
				prmProductGroupName.Value = Details.ProductGroupName;
				cmd.Parameters.Add(prmProductGroupName);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Details.Price;
				cmd.Parameters.Add(prmPrice);

				MySqlParameter prmPurchasePrice = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);			
				prmPurchasePrice.Value = Details.PurchasePrice;
				cmd.Parameters.Add(prmPurchasePrice);

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

                MySqlParameter prmOrderSlipPrinter = new MySqlParameter("@OrderSlipPrinter",MySqlDbType.Int16);
                prmOrderSlipPrinter.Value = (int) Details.OrderSlipPrinter;
                cmd.Parameters.Add(prmOrderSlipPrinter);
                
				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",System.Data.DbType.Int64);			
				prmProductGroupID.Value = Details.ProductGroupID;
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

        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeVAT(decimal OldVAT, decimal NewVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroup SET " +
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(cn, mTransaction);
        //        clsProductGroupVariationsMatrix.ChangeVAT(OldVAT, NewVAT);
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
        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroup SET " +
        //                            "EVAT		= @NewEVAT " +
        //                        "WHERE EVAT		= @OldEVAT;";
				  
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(cn, mTransaction);
        //        clsProductGroupVariationsMatrix.ChangeEVAT(OldEVAT, NewEVAT);

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
        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroup SET " +
        //                            "LocalTax		= @NewLocalTax " +
        //                        "WHERE LocalTax		= @OldLocalTax;";
				  
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(cn, mTransaction);
        //        clsProductGroupVariationsMatrix.ChangeLocalTax(OldLocalTax, NewLocalTax);
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

        public void ChangeTax(long ProductGroupID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblProductGroup SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";
                if (ProductGroupID !=0) SQL += "WHERE ProductGroupID		= @ProductGroupID;";

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

                if (ProductGroupID != 0)
                {
                    MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
                    prmProductGroupID.Value = ProductGroupID;
                    cmd.Parameters.Add(prmProductGroupID);
                }

                cmd.ExecuteNonQuery();

                ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(cn, mTransaction);
                clsProductGroupVariationsMatrix.ChangeTax(ProductGroupID, NewVAT, NewEVAT, NewLocalTax);

                ProductSubGroup clsProductSubGroup = new ProductSubGroup(cn, mTransaction);
                clsProductSubGroup.ChangeTax(ProductGroupID, 0, NewVAT, NewEVAT, NewLocalTax);
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
                string SQL = "UPDATE tblProductGroup SET " +
                                    "ChartOfAccountIDPurchase	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		    = @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	    = @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase    = @ChartOfAccountIDTaxPurchase, " +
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
        public void UpdateFinancialInformation(long ProductGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductGroup SET " +
                                    "ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
                                    "ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
                                "WHERE ProductGroupID	= @ProductGroupID; ";

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

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd;;
				string SQL;

				SQL=	"DELETE FROM tblProductGroupUnitMatrix WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();
				
				SQL=	"DELETE FROM tblProductGroupVariations WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductGroupVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductGroupBaseVariationsMatrix WHERE GroupID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductGroupBaseVariationsMatrix WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				cmd.Connection = cn;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblProductGroup WHERE ProductGroupID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				cmd.Connection = cn;
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
                                "a.ProductGroupID, " +
                                "a.ProductGroupCode, " +
                                "a.ProductGroupName, " +
                                "a.BaseUnitID, " +
                                "b.UnitName 'BaseUnitName', " +
                                "a.Price, " +
                                "a.PurchasePrice, " +
                                "a.IncludeInSubtotalDiscount, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax, " +
                                "OrderSlipPrinter, " +
                                "a.ChartOfAccountIDPurchase, " +
                                "a.ChartOfAccountIDSold, " +
                                "a.ChartOfAccountIDInventory, " +
                                "a.ChartOfAccountIDTaxPurchase, " +
                                "a.ChartOfAccountIDTaxSold " +
                            "FROM tblProductGroup a " +
                            "INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID ";
            return stSQL;
        }

        private string SQLSelectSimple()
        {
            string stSQL = "SELECT " +
                                "a.ProductGroupID, " +
                                "a.ProductGroupCode, " +
                                "a.ProductGroupName, " +
                                "a.SequenceNo " +
                            "FROM tblProductGroup a ";
            return stSQL;
        }

		#region Details

		public ProductGroupDetails Details(Int64 ProductGroupID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE ProductGroupID = @ProductGroupID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int16);
				prmProductGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductGroupDetails Details = new ProductGroupDetails();

				while (myReader.Read()) 
				{
					Details.ProductGroupID = ProductGroupID;
					Details.ProductGroupCode = "" + myReader["ProductGroupCode"].ToString();
					Details.ProductGroupName = "" + myReader["ProductGroupName"].ToString();
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.Price = myReader.GetDecimal("Price");
					Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
					Details.IncludeInSubtotalDiscount = myReader.GetInt16("IncludeInSubtotalDiscount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), myReader.GetString("OrderSlipPrinter"));
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = myReader.GetInt32("ChartOfAccountIDPurchase");
                    Details.ChartOfAccountIDSold = myReader.GetInt32("ChartOfAccountIDSold");
                    Details.ChartOfAccountIDInventory = myReader.GetInt32("ChartOfAccountIDInventory");
                    Details.ChartOfAccountIDTaxPurchase = myReader.GetInt32("ChartOfAccountIDTaxPurchase");
                    Details.ChartOfAccountIDTaxSold = myReader.GetInt32("ChartOfAccountIDTaxSold");
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

		public ProductGroupDetails Details(string ProductGroupCode)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE ProductGroupCode = @ProductGroupCode;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);
				prmProductGroupCode.Value = ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductGroupDetails Details = new ProductGroupDetails();

				while (myReader.Read()) 
				{
					Details.ProductGroupID = myReader.GetInt64("ProductGroupID");
					Details.ProductGroupCode = "" + myReader["ProductGroupCode"].ToString();
					Details.ProductGroupName = "" + myReader["ProductGroupName"].ToString();
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.Price = myReader.GetDecimal("Price");
					Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
					Details.IncludeInSubtotalDiscount = myReader.GetInt16("IncludeInSubtotalDiscount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), myReader.GetString("OrderSlipPrinter"));
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = myReader.GetInt32("ChartOfAccountIDPurchase");
                    Details.ChartOfAccountIDSold = myReader.GetInt32("ChartOfAccountIDSold");
                    Details.ChartOfAccountIDInventory = myReader.GetInt32("ChartOfAccountIDInventory");
                    Details.ChartOfAccountIDTaxPurchase = myReader.GetInt32("ChartOfAccountIDTaxPurchase");
                    Details.ChartOfAccountIDTaxSold = myReader.GetInt32("ChartOfAccountIDTaxSold");
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
				string SQL =SQLSelect() + "WHERE 1=1 " +
							            "AND (ProductGroupCode LIKE @SearchKey " +
							            "OR ProductGroupName LIKE @SearchKey " +
							            "OR UnitName LIKE @SearchKey) " +
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField = "ProductGroupName", SortOption SortOrder = SortOption.Ascending, int Limit=Constants.C_DEFAULT_LIMIT_OF_RECORD_TO_SHOW)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 " +
                                        "AND (ProductGroupCode LIKE @SearchKey " +
                                        "OR ProductGroupName LIKE @SearchKey) ";

                if (SortField == string.Empty) SortField = "ProductGroupCode";
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
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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

        public System.Data.DataTable DataTable(string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "ORDER BY " + SortField;

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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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
        public System.Data.DataTable DataTableSimple(long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelectSimple() + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND SequenceNo <= " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND SequenceNo >= " + SequenceNoStart.ToString() + " ";
                }

                if (SortField == string.Empty) SortField = "SequenceNo";
                SQL += "ORDER BY " + SortField + " ";

                if (SortOrder == SortOption.Ascending)
                    SQL += "ASC ";
                else
                    SQL += "DESC ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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
                string SQL = SQLSelect() + "ORDER BY " + SortField;

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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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
	}
}

