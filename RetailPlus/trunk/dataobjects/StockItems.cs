using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;

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
	public struct StockItemDetails
	{
		public Int64 StockItemID;
		public Int64 StockID;
		public Int64 ProductID;
		public Int64 VariationMatrixID;
		public Int32 ProductUnitID;
		public Int16 StockTypeID;
		public DateTime StockDate;
		public decimal Quantity;
		public string Remarks;
        public decimal PurchasePrice;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class StockItem
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

		public StockItem()
		{
			
		}

		public StockItem(MySqlConnection Connection, MySqlTransaction Transaction)
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
				IsInTransaction = true;
			}

			return mConnection;
		} 


		#endregion

		#region Insert and Update

		public Int64 Insert(StockItemDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblStockItems (" +
								"StockID, " + 
								"ProductID, " + 
								"VariationMatrixID, " + 
								"ProductUnitID, " +  
								"StockTypeID, " + 
								"StockDate, " + 
								"Quantity, " +
								"Remarks, " +
                                "PurchasePrice" +
								") VALUES (" +
								"@StockID, " + 
								"@ProductID, " + 
								"@VariationMatrixID, " + 
								"@ProductUnitID, " +  
								"@StockTypeID, " + 
								"@StockDate, " + 
								"@Quantity, " +
								"@Remarks, " +
                                "@PurchasePrice);"; 

				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = Details.StockID;
				cmd.Parameters.Add(prmStockID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);	
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
				
				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);	
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);
				
				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);	
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
	
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

                MySqlParameter prmPurchasePrice  = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);
                prmPurchasePrice.Value = Details.PurchasePrice;
                cmd.Parameters.Add(prmPurchasePrice);

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


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblStockItems WHERE StockItemID IN (" + IDs + ");";
				  
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

		public bool Delete(Int64 StockID)
		{
			try 
			{
				string SQL=	"DELETE FROM tblStockItems WHERE StockID = " + StockID + ";";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

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

		#region Details

		public StockItemDetails[] Details(Int64 StockID)
		{
			try
			{
				MySqlDataReader myReader = List(StockID, "StockItemID", SortOption.Ascending);
				
				ArrayList items = new ArrayList();

				while (myReader.Read()) 
				{
					StockItemDetails itemDetails = new StockItemDetails();

					itemDetails.StockItemID = myReader.GetInt64("StockItemID");
					itemDetails.StockID = StockID;
					itemDetails.ProductID = myReader.GetInt64("ProductID");
					itemDetails.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					itemDetails.ProductUnitID = myReader.GetInt32("ProductUnitID");
					itemDetails.StockTypeID = myReader.GetInt16("StockTypeID");
					itemDetails.StockDate = myReader.GetDateTime("StockDate");
					itemDetails.Quantity = myReader.GetDecimal("Quantity");
					itemDetails.Remarks = "" + myReader["Remarks"].ToString();
                    itemDetails.PurchasePrice = myReader.GetDecimal("PurchasePrice");
					items.Add(itemDetails);
				}

				myReader.Close();

				StockItemDetails[] StockItems = new StockItemDetails[0];

				if (items != null)
				{
					StockItems = new StockItemDetails[items.Count];
					items.CopyTo(StockItems);
				}
				
				return StockItems;
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

		private string SQLSelect()
		{
			string stSQL = "SELECT " +
									"StockItemID, " +
									"StockID, " +
									"a.ProductID, " +
									"ProductCode, " +
									"VariationMatrixID, " + 
									"c.Description AS BaseVariationDescription, " +
									"ProductUnitID, " +
									"d.UnitCode, " +
									"d.UnitName, " +
									"a.StockTypeID, " +
									"e.Description AS StockTypeDescription, " +
									"StockDate, " +
									"a.Quantity, " +
									"a.Remarks, " +
                                    "a.PurchasePrice " +
								"FROM (((tblStockItems a " +
								    "LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID) " +
								    "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID) " +  
								    "LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID) " +
								    "LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID " +
								"WHERE 1=1 ";
			return stSQL;
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
		
		public MySqlDataReader List(Int64 StockID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND StockID = @StockID " +
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
				
				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

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
		

        //public MySqlDataReader ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();

        //        string SQL = "SELECT " +
        //                        "StockItemID, " +
        //                        "IFNULL(c.Description, b.ProductCode) AS MatrixDescription, " +
        //                        "CASE StockDirection " +
        //                        "	WHEN 0 THEN a.Quantity " +
        //                        "	WHEN 1 THEN -a.Quantity " +
        //                        "END AS Quantity, " +
        //                        "d.UnitCode, " +
        //                        "CONCAT(e.Description, ':' , a.Remarks) AS Remarks, " +
        //                        "a.StockDate AS TransactionDate, " +
        //                        "TransactionNo " +
        //                    "FROM (((tblStockItems a " +
        //                    "INNER JOIN tblStock f ON a.StockID = f.StockID " +
        //                    "LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID) " +
        //                    "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID) " +
        //                    "LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID) " +
        //                    "LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID " +
        //                    "WHERE 1=1 ";

        //        SQL += "AND a.StockDate >= @StartDate ";

        //        MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
        //        prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmStartDate); 

        //        SQL += "AND a.StockDate <= @EndDate ";

        //        MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
        //        prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmEndDate); 

        //        if (ProductID != 0)
        //        {	
        //            SQL += "AND a.ProductID = @ProductID ";

        //            MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
        //            prmProductID.Value = ProductID;
        //            cmd.Parameters.Add(prmProductID); 
        //        }

        //        SQL += "ORDER BY a.StockDate DESC";

        //        MySqlConnection cn = GetConnection();

        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
        //        return myReader;			
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
        public MySqlDataReader ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = "CALL procGenerateProductHistory(@SessionID, @StartTransactionDate, @EndTransactionDate, @ProductID);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID", clsRandom.Next(1234567, 99999999));

                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                            "HistoryID, " +
                            "MatrixDescription, " +
                            "Quantity, " +
                            "UnitCode, " +
                            "Remarks, " +
                            "TransactionDate, " +
                            "TransactionNo " +
                    "FROM tblProductHistory " +
                    "WHERE SessionID = @SessionID " +
                    "ORDER BY TransactionDate;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                SQL = "DELETE FROM tblProductHistory WHERE SessionID = @SessionID;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }

        public MySqlDataReader ProductMovementReport(long ProductID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = "CALL procProductMovementSelect(@lngProductID, @dteStartTransactionDate, @dteEndTransactionDate);";

                cmd.Parameters.AddWithValue("@lngProductID", ProductID);
                cmd.Parameters.AddWithValue("@dteStartTransactionDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@dteEndTransactionDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                //SQL = "SELECT " +
                //        "ProductID, " +
                //        "ProductCode, " +
                //        "ProductDescription, " +
                //        "MatrixID, " +
                //        "MatrixDescription,  " +
                //        "QuantityFrom, " +
                //        "Quantity, " +
                //        "QuantityTo, " +
                //        "matrixQuantity, " +
                //        "UnitCode, " +
                //        "Remarks, " +
                //        "TransactionDate, " +
                //        "TransactionNo, " +
                //        "CreatedBy " +
                //    "FROM tblProductMovement " +
                //    "WHERE QuantityMovementType = 0 ";

                //if (ProductID != 0)
                //{ SQL += "AND ProductID = @lngProductID "; cmd.Parameters.AddWithValue("@lngProductID", ProductID); }

                //if (StartDate != DateTime.MinValue)
                //{ SQL += "AND TransactionDate >= @dteStartTransactionDate "; cmd.Parameters.AddWithValue("@dteStartTransactionDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss")); }

                //if (StartDate != DateTime.MinValue)
                //{ SQL += "AND TransactionDate <= @dteEndTransactionDate "; cmd.Parameters.AddWithValue("@dteEndTransactionDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss")); }

                //SQL += "ORDER BY TransactionDate DESC ";

                cmd.Connection = GetConnection();
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SQL;
                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }
		
		#endregion
	}
}

