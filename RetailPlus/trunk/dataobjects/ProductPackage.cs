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
	public struct ProductPackageDetails
	{
		public Int64 PackageID;
		public Int64 ProductID;
		public Int32 UnitID;
		public string UnitCode;
		public string UnitName;
		public decimal Price;
        public decimal WSPrice;
		public decimal PurchasePrice;
		public decimal Quantity;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public string BarCode1;
        public string BarCode2;
        public string BarCode3;
        public string ProductDesc;
	}

    public struct ProductPackageColumns
    {
        public bool PackageID;
        public bool ProductID;
        public bool UnitID;
        public bool UnitCode;
        public bool UnitName;
        public bool Price;
        public bool WSPrice;
        public bool PurchasePrice;
        public bool Quantity;
        public bool VAT;
        public bool EVAT;
        public bool LocalTax;
        public bool BarCode1;
        public bool BarCode2;
        public bool BarCode3;
        public bool ProductDesc;
    }

    public struct ProductPackageColumnNames
    {
        public const string PackageID = "PackageID";
        public const string ProductID = "ProductID";
        public const string UnitID = "UnitID";
        public const string UnitCode = "UnitCode";
        public const string UnitName = "UnitName";
        public const string Price = "Price";
        public const string WSPrice = "WSPrice";
        public const string PurchasePrice = "PurchasePrice";
        public const string Quantity = "Quantity";
        public const string VAT = "VAT";
        public const string EVAT = "EVAT";
        public const string LocalTax = "LocalTax";
        public const string BarCode1 = "BarCode1";
        public const string BarCode2 = "BarCode2";
        public const string BarCode3 = "BarCode3";
        public const string ProductDesc = "ProductDesc";
    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductPackage
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


		#region Constrcutors and Destructors

		public ProductPackage()
		{
			
		}

		public ProductPackage(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public void Insert(ProductPackageDetails Details)
		{
			try  
			{
				string SQL="INSERT INTO tblProductPackage (" +
								"ProductID, " +
                                "BarCode1, " +
                                "BarCode2, " +
                                "BarCode3, " +
								"UnitID, " +
								"Price, " +
                                "WSPrice, " +
								"PurchasePrice, " +
								"Quantity, " +
								"VAT, " +
								"EVAT, " +
								"LocalTax" +
							") VALUES (" +
								"@ProductID, " +
                                "@BarCode1, " +
                                "@BarCode2, " +
                                "@BarCode3, " +
								"@UnitID, " +
								"@Price, " +
                                "@WSPrice, " +
								"@PurchasePrice, " +
								"@Quantity, " +
								"@VAT, " +
								"@EVAT, " +
								"@LocalTax" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@BarCode1", Details.BarCode1);
                cmd.Parameters.AddWithValue("@BarCode2", Details.BarCode2);
                cmd.Parameters.AddWithValue("@BarCode3", Details.BarCode3);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);

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
        public void Update(ProductPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
		{
			try  
			{
                MySqlConnection cn = GetConnection();

                // Update ProductPackagePriceHistory first to get the history
                ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                clsProductPackagePriceHistoryDetails.UID = pvtUID;
                clsProductPackagePriceHistoryDetails.PackageID = Details.PackageID;
                clsProductPackagePriceHistoryDetails.ChangeDate = pvtChangeDate;
                clsProductPackagePriceHistoryDetails.PurchasePrice = Details.PurchasePrice;
                clsProductPackagePriceHistoryDetails.Price = Details.Price;
                clsProductPackagePriceHistoryDetails.VAT = Details.VAT;
                clsProductPackagePriceHistoryDetails.EVAT = Details.EVAT;
                clsProductPackagePriceHistoryDetails.LocalTax = Details.LocalTax;
                clsProductPackagePriceHistoryDetails.Remarks = pvtHistoryRemarks;

                ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(mConnection, mTransaction);
                clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);

                string SQL = "CALL procProductPackageUpdate(@PackageID, @ProductID, @UnitID, @Price, @WSPrice, @PurchasePrice, @Quantity, @VAT, @EVAT, @LocalTax, @BarCode1, @BarCode2, @BarCode3);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
				cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
				cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@BarCode1", Details.BarCode1);
                cmd.Parameters.AddWithValue("@BarCode2", Details.BarCode2);
                cmd.Parameters.AddWithValue("@BarCode3", Details.BarCode3);

				cmd.ExecuteNonQuery();

				if (Details.Quantity == 1)
				{
					Product clsProduct = new Product(cn, mTransaction);
                    clsProduct.UpdateByPackage(Details.ProductID, Details.UnitID, Details.Price, Details.WSPrice, Details.PurchasePrice, Details.VAT, Details.EVAT, Details.LocalTax);
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
		}
        public void UpdateByProductIDUnitIDAndQuantity(ProductPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
		{
			try  
			{
                string SQL = "SELECT PackageID FROM tblProductPackage WHERE ProductID = @ProductID " +
                                "AND UnitID		=	@UnitID " +
                                "AND Quantity	=	@Quantity;";

				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                while (myReader.Read())
                { Details.PackageID = myReader.GetInt64("PackageID"); }

                myReader.Close();

                Update(Details, pvtUID, pvtChangeDate, pvtHistoryRemarks);
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

		public void UpdatePurchasing(long ProductID, int UnitID, decimal Quantity, decimal PurchasePrice)
		{
			try  
			{
				string SQL="UPDATE tblProductPackage SET " +
								"PurchasePrice	=	@PurchasePrice, " +
                                "WSPrice	    =   @PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100)) " +
							"WHERE ProductID	=	@ProductID " +
							    "AND UnitID		=	@UnitID " +
							    "AND Quantity	=	@Quantity;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
				
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
		
		public void UpdateSelling(long ProductID, int UnitID, decimal Quantity, decimal SellingPrice)
		{
			try  
			{
				string SQL="UPDATE tblProductPackage SET " +
								"Price			=	@Price " +
							"WHERE ProductID	=	@ProductID " +
							"AND UnitID		=	@UnitID " +
							"AND Quantity	=	@Quantity;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.Int32);			
				prmUnitID.Value = UnitID;
				cmd.Parameters.Add(prmUnitID);

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = SellingPrice;
				cmd.Parameters.Add(prmPrice);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Quantity;
				cmd.Parameters.Add(prmQuantity);
				
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
        public void UpdateSellingWSPrice(long ProductID, int UnitID, decimal Quantity, decimal WholeSalePrice)
        {
            try
            {
                string SQL = "UPDATE tblProductPackage SET " +
                                "WSPrice			=	@WSPrice " +
                            "WHERE ProductID	=	@ProductID " +
                            "AND UnitID		=	@UnitID " +
                            "AND Quantity	=	@Quantity;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@WSPrice", WholeSalePrice);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

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
        //        string SQL =	"UPDATE tblProductPackage SET " +
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
        //        string SQL="UPDATE tblProductPackage SET " +
        //                        "EVAT		=	@NewEVAT " +
        //                    "WHERE EVAT		=	@OldEVAT;";
				  
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
        //        string SQL =	"UPDATE tblProductPackage SET " +
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

        public void ChangeTax(long ProductGroupID, long ProductSubGroupID, long ProductID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblProductPackage SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";
                if (ProductID != 0) SQL += "WHERE ProductID = @ProductID;";
                else if (ProductSubGroupID != 0) SQL += "WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = @ProductSubGroupID);";
                else if (ProductGroupID != 0) SQL += "WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID));";

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
				string SQL=	"DELETE FROM tblProductPackage WHERE PackageID IN (" + IDs + ");";
				  
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
                                "PackageID, " +
                                "a.ProductID, " +
                                "ProductDesc, " +
                                "a.UnitID, " +
                                "c.UnitCode, " +
                                "c.UnitName, " +
                                "a.Price, " +
                                "a.WSPrice, " +
                                "a.PurchasePrice, " +
                                "a.Quantity, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax, " +
                                "a.BarCode1, " +
                                "a.BarCode2, " +
                                "a.BarCode3 " +
                            "FROM tblProductPackage a " +
                            "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                            "INNER JOIN tblUnit c ON a.UnitID = c.UnitID ";

            return stSQL;
        }

        private string SQLSelect(ProductPackageColumns clsProductPackageColumns)
        {
            string stSQL = "SELECT ";

            if (clsProductPackageColumns.ProductID) stSQL += "tblProductPackage.ProductID, ";
            if (clsProductPackageColumns.UnitID) stSQL += "tblProductPackage.UnitID, ";
            if (clsProductPackageColumns.UnitCode) stSQL += "tblUnit.UnitCode, ";
            if (clsProductPackageColumns.UnitName) stSQL += "tblUnit.UnitName, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price, ";
            if (clsProductPackageColumns.WSPrice) stSQL += "tblProductPackage.WSPrice, ";
            if (clsProductPackageColumns.PurchasePrice) stSQL += "tblProductPackage.PurchasePrice, ";
            if (clsProductPackageColumns.Quantity) stSQL += "tblProductPackage.Quantity, ";
            if (clsProductPackageColumns.VAT) stSQL += "tblProductPackage.VAT, ";
            if (clsProductPackageColumns.EVAT) stSQL += "tblProductPackage.EVAT, ";
            if (clsProductPackageColumns.LocalTax) stSQL += "tblProductPackage.LocalTax, ";
            if (clsProductPackageColumns.BarCode1) stSQL += "tblProductPackage.BarCode1, ";
            if (clsProductPackageColumns.BarCode2) stSQL += "tblProductPackage.BarCode2, ";
            if (clsProductPackageColumns.BarCode3) stSQL += "tblProductPackage.BarCode3, ";
            if (clsProductPackageColumns.ProductDesc) stSQL += "tblProducts.ProductDesc, ";

            stSQL += "tblProductPackage.PackageID ";
            stSQL += "FROM tblProductPackage ";

            if (clsProductPackageColumns.ProductDesc)
                stSQL += "INNER JOIN tblProducts ON tblProductPackage.ProductID = tblProducts.ProductID ";

            if (clsProductPackageColumns.UnitCode || clsProductPackageColumns.UnitName)
                stSQL += "INNER JOIN tblUnit ON tblProductPackage.UnitID = tblUnit.UnitID ";

            return stSQL;
        }

		#region Details

        public ProductPackageColumns getAllPackageColumns()
        {
            ProductPackageColumns clsProductPackageColumns = new ProductPackageColumns();
            clsProductPackageColumns.PackageID = true;
            clsProductPackageColumns.ProductID = true;
            clsProductPackageColumns.UnitID = true;
            clsProductPackageColumns.UnitCode = true;
            clsProductPackageColumns.UnitName = true;
            clsProductPackageColumns.Price = true;
            clsProductPackageColumns.PurchasePrice = true;
            clsProductPackageColumns.Quantity = true;
            clsProductPackageColumns.VAT = true;
            clsProductPackageColumns.EVAT = true;
            clsProductPackageColumns.LocalTax = true;
            clsProductPackageColumns.BarCode1 = true;
            clsProductPackageColumns.BarCode2 = true;
            clsProductPackageColumns.BarCode3 = true;
            clsProductPackageColumns.ProductDesc = true;

            return clsProductPackageColumns;
        }
        public long GetPackageID(long ProductID, int UnitID)
        {
            try
            {
                string SQL = "SELECT PackageID FROM tblProductPackage WHERE ProductID = @ProductID AND UnitID = @UnitID AND Quantity = 1;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                long lngRetValue = 0;

                while (myReader.Read())
                {
                    lngRetValue = myReader.GetInt64("PackageID");
                }

                myReader.Close();

                return lngRetValue;
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
		public ProductPackageDetails Details(Int64 PackageID)
		{
			try
			{
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.PackageID = @PackageID ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPackageID = new MySqlParameter("@PackageID",MySqlDbType.Int64);
                prmPackageID.Value = PackageID;
                cmd.Parameters.Add(prmPackageID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPackageDetails clsDetails = setDetails(myReader);

                return clsDetails;
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
		public ProductPackageDetails DetailsByProductID(Int64 ProductID)
		{
			try
			{
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID ";
                SQL += "ORDER BY tblProductPackage.PackageID ASC LIMIT 1;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPackageDetails clsDetails = setDetails(myReader);

                return clsDetails;
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
        public ProductPackageDetails DetailsByProductIDAndUnitID(Int64 ProductID, long UnitID)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID ";
                SQL += "    AND tblProductPackage.UnitID = @UnitID ";
                SQL += "LIMIT 1;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.Int32);
                prmUnitID.Value = UnitID;
                cmd.Parameters.Add(prmUnitID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPackageDetails clsDetails = setDetails(myReader);

                return clsDetails;
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
        public ProductPackageDetails DetailsByProductIDAndBarCode(Int64 ProductID, string BarCode)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID ";
                SQL += "    AND (tblProductPackage.Barcode1 LIKE @BarCode OR tblProductPackage.Barcode2 LIKE @BarCode OR tblProductPackage.Barcode3 LIKE @BarCode) ";
                SQL += "LIMIT 1;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = BarCode;
                cmd.Parameters.Add(prmBarCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPackageDetails clsDetails = setDetails(myReader);

                return clsDetails;
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
        public ProductPackageDetails DetailsByBarCode(string BarCode)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.Barcode1 LIKE @BarCode OR tblProductPackage.Barcode2 LIKE @BarCode OR tblProductPackage.Barcode3 LIKE @BarCode ";
                SQL += "LIMIT 1;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = BarCode;
                cmd.Parameters.Add(prmBarCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPackageDetails clsDetails = setDetails(myReader);

                return clsDetails;
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
        private ProductPackageDetails setDetails(MySqlDataReader myReader)
        {
            ProductPackageDetails clsDetails = new ProductPackageDetails(); ;

            while (myReader.Read())
            {
                clsDetails.PackageID = Convert.ToInt64(myReader[ProductPackageColumnNames.PackageID].ToString());
                clsDetails.ProductID = Convert.ToInt64(myReader[ProductPackageColumnNames.ProductID].ToString());
                clsDetails.UnitID = Convert.ToInt32(myReader[ProductPackageColumnNames.UnitID].ToString());
                clsDetails.UnitCode = "" + myReader[ProductPackageColumnNames.UnitCode].ToString();
                clsDetails.UnitName = "" + myReader[ProductPackageColumnNames.UnitName].ToString();
                clsDetails.Price = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price].ToString());
                clsDetails.PurchasePrice = Convert.ToDecimal(myReader[ProductPackageColumnNames.PurchasePrice].ToString());
                clsDetails.Quantity = Convert.ToDecimal(myReader[ProductPackageColumnNames.Quantity].ToString());
                clsDetails.VAT = Convert.ToDecimal(myReader[ProductPackageColumnNames.VAT].ToString());
                clsDetails.EVAT = Convert.ToDecimal(myReader[ProductPackageColumnNames.EVAT].ToString());
                clsDetails.LocalTax = Convert.ToDecimal(myReader[ProductPackageColumnNames.LocalTax].ToString());
                clsDetails.BarCode1 = "" + myReader[ProductPackageColumnNames.BarCode1].ToString();
                clsDetails.BarCode2 = "" + myReader[ProductPackageColumnNames.BarCode2].ToString();
                clsDetails.BarCode3 = "" + myReader[ProductPackageColumnNames.BarCode3].ToString();
            }

            myReader.Close();

            return clsDetails;
        }

		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField; 

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

		public MySqlDataReader List(string SortField, SortOption SortOrder, string ProductIDs)
		{
			try
			{
				string SQL = "SELECT " +
								"a.PackageID, " +
								"a.ProductID, " +
								"ProductDesc, " +
								"a.UnitID, " +
								"c.UnitCode, " +
								"c.UnitName, " +
								"a.Price, " +
								"a.PurchasePrice, " +
								"a.Quantity, " +
								"a.VAT, " +
								"a.EVAT, " +
								"a.LocalTax " +
							"FROM tblProductPackage a " +
							"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
							"INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
							"INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID " +
							"INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +	
							"WHERE 1=1 ";

				if (ProductIDs != "" && ProductIDs != null)
					SQL += "AND a.ProductID in (" + ProductIDs + ") ";

				SQL += "ORDER BY " + SortField; 

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

		public MySqlDataReader List(string SortField, SortOption SortOrder, string ProductGroupName, string ProductSubGroupName)
		{
			try
			{
				string SQL = "SELECT " +
								"a.PackageID, " +
								"a.ProductID, " +
								"ProductDesc, " +
								"a.UnitID, " +
								"c.UnitCode, " +
								"c.UnitName, " +
								"a.Price, " +
								"a.PurchasePrice, " +
								"a.Quantity, " +
								"a.VAT, " +
								"a.EVAT, " +
								"a.LocalTax " +
							"FROM tblProductPackage a " +
							"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
							"INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
							"INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID " +
							"INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +	
							"WHERE 1=1 ";

				if (ProductGroupName != "" && ProductGroupName != null)
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
			
				if (ProductSubGroupName != "" && ProductSubGroupName != null)
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";

				SQL += "ORDER BY " + SortField; 

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

		public MySqlDataReader List(Int64 ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND a.ProductID = @ProductID ORDER BY " + SortField; 

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
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

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

        public System.Data.DataTable ListAsDataTable(Int64 ProductID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 AND a.ProductID = @ProductID ORDER BY " + SortField;

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

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                System.Data.DataTable dtRetValue = new System.Data.DataTable("ProductPackages");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtRetValue);

                return dtRetValue;	

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

        public System.Data.DataTable ListAsDataTable(ProductPackageColumns clsProductPackageColumns, ProductPackageColumns SearchColumns, string SearchKey, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                string SQL = SQLSelect(clsProductPackageColumns) + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND PackageID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND PackageID > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.BarCode1)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode1 LIKE @SearchKey "; else SQLSearch += "OR Barcode1 LIKE @SearchKey "; }

                    if (SearchColumns.BarCode2)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode2 LIKE @SearchKey "; else SQLSearch += "OR Barcode2 LIKE @SearchKey "; }

                    if (SearchColumns.BarCode3)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode3 LIKE @SearchKey "; else SQLSearch += "OR Barcode3 LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";

                    if (SearchColumns.ProductID)
                    { SQL += "AND tblProductPackage.ProductID = " + SearchKey + " "; }
                }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
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
                if (SearchKey.StartsWith("%") == true)
                    prmSearchKey.Value = SearchKey + "%";
                else
                    prmSearchKey.Value = "%" + SearchKey + "%";

                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
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

		#region Public Modifiers

		public int CountPackage(long ProductID)
		{
			try
			{
				string SQL = "SELECT Count(ProductID) FROM tblProductPackage WHERE ProductID = @ProductID "; 
				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				myReader.Read();

				int recCtr = myReader.GetInt32(0);

				myReader.Close();

				return recCtr;
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
        public void CopyToMatrixPackage(long ProductID)
        {
            try
            {
                string SQL = "CALL procProductPackageCopyToMatrixPackage(@ProductID);";
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

		#endregion
	}
}
