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

	public struct ProductPurchasePriceHistoryDetails
	{
        public Int64 ProductPurchasePriceHistoryID;
		public Int64 ProductID;
        public ProductDetails ProductDetails;
        public Int64 MatrixID;
        public Int64 SupplierID;
        public ContactDetails SupplierDetails;
        public decimal PurchasePrice;
        public DateTime PurchaseDate;
        public string Remarks;
        public string PurchaserName;
        public DateTime DateCreated;
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
	public class ProductPurchasePriceHistory
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

		public ProductPurchasePriceHistory()
		{
			
		}

		public ProductPurchasePriceHistory(MySqlConnection Connection, MySqlTransaction Transaction)
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
        public void AddToList(ProductPurchasePriceHistoryDetails Details)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(Details.ProductID, "PurchasePrice", SortOption.Desscending);
                if (dt.Rows.Count < DataConstants.MAX_PURCHASE_PRICE_SUPPLIER)
                {
                    //insert new purchase price if price levels are lower than max
                    Insert(Details);
                }
                else
                {
                    long lngCtr = 0;
                    //update purchase price
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        decimal decPurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                        long lngProductPurchasePriceHistoryID = long.Parse(dr["ProductPurchasePriceHistoryID"].ToString());
                        lngCtr += 1;
                        if (decPurchasePrice >= Details.PurchasePrice )
                        {
                            Details.ProductPurchasePriceHistoryID = lngProductPurchasePriceHistoryID;
                            Update(Details);
                            break;
                        }
                        else if(dt.Rows.Count == lngCtr && decPurchasePrice ==0)
                        {
                            Details.ProductPurchasePriceHistoryID = lngProductPurchasePriceHistoryID;
                            Update(Details);
                            break;
                        }
                    }
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

		private Int64 Insert(ProductPurchasePriceHistoryDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblProductPurchasePriceHistory (" +
									"ProductID, " + 
									"MatrixID, " + 
									"SupplierID, " +  
									"PurchasePrice, " + 
									"PurchaseDate, " + 
                                    "Remarks," +
                                    "PurchaserName," +
                                    "DateCreated" +
								") VALUES (" +
                                    "@ProductID, " +
                                    "@MatrixID, " +
                                    "@SupplierID, " +
                                    "@PurchasePrice, " + 
									"@PurchaseDate, " +
                                    "@Remarks," +
                                    "@PurchaserName," +
                                    "now());"; 

				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@ProductID",Details.ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", Details.PurchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);

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

        private void Update(ProductPurchasePriceHistoryDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblProductPurchasePriceHistory SET " +
									"SupplierID     = @SupplierID, " + 
									"PurchasePrice  = @PurchasePrice, " +  
									"PurchaseDate	= @PurchaseDate, " + 
									"Remarks        = @Remarks, " +
                                    "PurchaserName       = @PurchaserName " +
								"WHERE ProductPurchasePriceHistoryID	= @ProductPurchasePriceHistoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductPurchasePriceHistoryID", Details.ProductPurchasePriceHistoryID);
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", Details.PurchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);

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
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL=	"DELETE FROM tblProductPurchasePriceHistory WHERE ProductPurchasePriceHistoryID IN (" + IDs + ");";
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
                                    "a.ProductPurchasePriceHistoryID, " +
                                    "a.ProductID, " +
                                    "a.MatrixID, " +
                                    "a.SupplierID, " +
                                    "c.ContactCode AS 'SupplierCode', " +
                                    "c.ContactName AS 'SupplierName', " +
                                    "a.PurchasePrice, " +
                                    "a.PurchaseDate, " +
                                    "a.Remarks, " +
                                    "a.PurchaserName " +
                                "FROM tblProductPurchasePriceHistory a " +
                                "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                                "INNER JOIN tblContacts c ON a.SupplierID = c.ContactID ";
            return stSQL;
        }

		#region Details

		public ProductPurchasePriceHistoryDetails Details(long ProductPurchasePriceHistoryID)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE a.ProductPurchasePriceHistoryID = @ProductPurchasePriceHistoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductPurchasePriceHistoryID", ProductPurchasePriceHistoryID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPurchasePriceHistoryDetails Details = SetDetails(myReader);

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
		public ProductPurchasePriceHistoryDetails DetailsByProductID(long ProductID)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE a.ProductID = @ProductID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ProductPurchasePriceHistoryDetails Details = SetDetails(myReader);

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

        private ProductPurchasePriceHistoryDetails SetDetails(MySqlDataReader myReader)
        {
            try
            {
                ProductPurchasePriceHistoryDetails Details = new ProductPurchasePriceHistoryDetails();
                Details.ProductPurchasePriceHistoryID = 0;

                while (myReader.Read())
                {
                    Details.ProductPurchasePriceHistoryID = myReader.GetInt64("ProductPurchasePriceHistoryID");
                    Details.MatrixID = myReader.GetInt64("MatrixID");
                    Details.SupplierID = myReader.GetInt64("SupplierID");
                    Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
                    Details.PurchaseDate = myReader.GetDateTime("PurchaseDate");
                    Details.Remarks = "" + myReader["Remarks"].ToString();

                    Product clsProduct = new Product(mConnection, mTransaction);
                    Details.ProductDetails = clsProduct.Details(Details.ProductID);

                    Contact clsContact = new Contact(mConnection, mTransaction);
                    Details.SupplierDetails = clsContact.Details(Details.SupplierID);
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

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
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
				
				System.Data.DataTable dt = new System.Data.DataTable("tblProductPurchasePriceHistory");
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
		public System.Data.DataTable ListAsDataTable(long ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE a.ProductID = @ProductID ORDER BY " + SortField;


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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPurchasePriceHistory");
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
        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, int Limit)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 ";

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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPurchasePriceHistory");
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
        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, int Limit, long ProductID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE ProductID = @ProductID ";

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

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPurchasePriceHistory");
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

