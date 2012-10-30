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
	public struct StockDetails
	{
		public long StockID;
        public int BranchID;
		public string TransactionNo;
		public short StockTypeID;
		public string StockTypeCode;
		public string StockTypeDescription;
		public StockDirections StockDirection;
		public DateTime StockDate;
		public long SupplierID;
		public string SupplierCode;
		public string SupplierName;
		public string Remarks;
		public StockItemDetails[] StockItems;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Stock
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

		public Stock()
		{
			
		}

		public Stock(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(StockDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblStock (" +
                                    "BranchID, " +
									"TransactionNo, " +
									"StockTypeID, " +
									"StockDate, " + 
									"SupplierID, " + 
									"Remarks " +
								") VALUES (" +
                                    "@BranchID, " +
									"@TransactionNo, " +
									"@StockTypeID, " +
									"@StockDate, " + 
									"@SupplierID, " + 
									"@Remarks);"; 

				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);	
				prmTransactionNo.Value = Details.TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);
				
				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);	
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
				
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);	
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);	
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);
	
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
		public void Update(StockDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblStock SET " +
									"StockTypeID		= @StockTypeID, " + 
									"StockDate			= @StockDate, " +  
									"SupplierID			= @SupplierID, " +  
									"Remarks			= @Remarks " +
								"WHERE StockID = @StockID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);	
				prmTransactionNo.Value = Details.TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);
				
				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);	
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
				
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);	
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);	
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);	
				prmStockID.Value = Details.StockID;
				cmd.Parameters.Add(prmStockID);

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
        public void TagActive(long StockID)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                TagActiveInActive(StockID, true);
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
        public void TagInactive(long StockID)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                TagActiveInActive(StockID, false);
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
        private void TagActiveInActive(long StockID, bool bolActive)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                string SQL = "CALL procStockTagActiveInactive(@StockID, @TransactionListFilterType);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@StockID", StockID);
                cmd.Parameters.AddWithValue("@TransactionListFilterType", Convert.ToInt16(bolActive));

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

				string SQL=	"DELETE FROM tblStockItems WHERE StockID IN (" + IDs + ");";

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				cmd.ExecuteNonQuery();

				SQL=	"DELETE FROM tblStock WHERE StockID IN (" + IDs + ");";
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
                                    "StockID, " +
                                    "BranchID, " +
                                    "TransactionNo, " +
                                    "a.StockTypeID, " +
                                    "StockTypeCode, " +
                                    "Description AS StockTypeDescription, " +
                                    "StockDirection, " +
                                    "StockDate, " +
                                    "SupplierID, " +
                                    "ContactCode AS SupplierCode, " +
                                    "ContactName AS SupplierName, " +
                                    "a.Remarks, " +
                                    "a.Active " +
                                "FROM tblStock a " +
                                "INNER JOIN tblStockType b ON a.StockTypeID = b.StockTypeID " +
                                "INNER JOIN tblContacts c ON a.SupplierID = c.ContactID ";
            return stSQL;
        }

		#region Details

		public StockDetails Details(Int64 StockID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE StockID = @StockID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				StockDetails Details = new StockDetails();

				while (myReader.Read()) 
				{
					Details.StockID = StockID;
                    Details.BranchID = myReader.GetInt32("BranchID");
					Details.TransactionNo = "" + myReader["TransactionNo"].ToString();
					Details.StockTypeID = myReader.GetInt16("StockTypeID");
					Details.StockTypeCode = "" + myReader["StockTypeCode"].ToString();
					Details.StockTypeDescription = "" + myReader["StockTypeDescription"].ToString();
                    Details.StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), myReader.GetString("StockDirection"));
					Details.StockDate = myReader.GetDateTime("StockDate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierName = "" + myReader["SupplierName"].ToString();
					Details.Remarks = "" + myReader["Remarks"].ToString();
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

		public StockDetails Details(string TransactionNo)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE TransactionNo = @TransactionNo;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				StockDetails Details = new StockDetails();

				while (myReader.Read()) 
				{
					Details.StockID = myReader.GetInt64("StockID");
                    Details.BranchID = myReader.GetInt32("BranchID");
					Details.TransactionNo = "" + myReader["TransactionNo"].ToString();
					Details.StockTypeID = myReader.GetInt16("StockTypeID");
					Details.StockTypeCode = "" + myReader["StockTypeCode"].ToString();
					Details.StockTypeDescription = "" + myReader["StockTypeDescription"].ToString();
                    Details.StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), myReader.GetString("StockDirection"));
					Details.StockDate = myReader.GetDateTime("StockDate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierName = "" + myReader["SupplierName"].ToString();
					Details.Remarks = "" + myReader["Remarks"].ToString();
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

		public MySqlDataReader Search(string TransactionNo)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE TransactionNo = @TransactionNo ";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

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
				string SQL =	SQLSelect() + "(TransactionNo LIKE @SearchKey " +
								"OR StockTypeCode LIKE @SearchKey " +
								"OR a.Remarks LIKE @SearchKey) " +
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

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

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

        public System.Data.DataTable ListAsDataTableActiveInactive(TransactionListFilterType clsTransactionListFilterType, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (clsTransactionListFilterType == TransactionListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
                else if (clsTransactionListFilterType == TransactionListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblStock");
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
        public System.Data.DataTable SearchDataTableActiveInactive(TransactionListFilterType clsTransactionListFilterType, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 " +
                                                "AND (TransactionNo LIKE @SearchKey " +
                                                "OR StockTypeCode LIKE @SearchKey " +
                                                "OR a.Remarks LIKE @SearchKey) ";

                if (clsTransactionListFilterType == TransactionListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
                if (clsTransactionListFilterType == TransactionListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblStock");
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

		#region AddItem

        /// <summary>
        /// Jul 28, 2011 : Lemu
        //  - Remove the adding/subtracting and SynchronizeQuantity in clsProductVariationsMatrix,
        //    already included in the new AddQuantity and SubtractQuantity.
        /// </summary>
        /// <param name="Details"></param>
        /// <param name="StockDirection"></param>
        /// <returns></returns>
		public Int64 AddItem(int BranchID, string TransactionNo, string CreatedBy, StockItemDetails Details, StockDirections StockDirection)
		{
			try  
			{
				MySqlConnection cn = GetConnection();
	 			
				StockItem clsStockItem = new StockItem(cn, mTransaction);
				Int64 StockItemID = clsStockItem.Insert(Details);

				ProductUnit clsProductUnit = new ProductUnit(cn, mTransaction);
				decimal Quantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity);
                string strRemarks = string.Empty;

				Product clsProduct = new Product(cn, mTransaction);
				if (StockDirection == StockDirections.Decrement)
                {
                    strRemarks = Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_STOCK_INVENTORY) + " " + Details.Remarks;
                    clsProduct.SubtractQuantity(BranchID, Details.ProductID, Details.VariationMatrixID, Quantity, strRemarks, Details.StockDate, TransactionNo, CreatedBy);	
                }
				else
                {
                    strRemarks = Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_STOCK_INVENTORY) + " " + Details.Remarks;
                    clsProduct.AddQuantity(BranchID, Details.ProductID, Details.VariationMatrixID, Quantity, strRemarks, Details.StockDate, TransactionNo, CreatedBy);	
                }

                // Removed
                //ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(cn, mTransaction);
                //if (Details.VariationMatrixID != 0)
                //{
                //    if (StockDirection == StockDirections.Decrement)
                //    {	clsProductVariationsMatrix.SubtractQuantity(Details.VariationMatrixID, Quantity);	}
                //    else
                //    {	clsProductVariationsMatrix.AddQuantity(Details.VariationMatrixID, Quantity);	}

                //    clsProductVariationsMatrix.SynchronizeQuantity(Details.ProductID);
                //}
				
				return StockItemID;
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

