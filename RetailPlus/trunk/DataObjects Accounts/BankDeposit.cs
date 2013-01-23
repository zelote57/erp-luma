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
	public struct BankDepositDetails
	{
		public long BankDepositID;
		public DateTime TransactionDate;
		public BankDepositStatus DepositStatus;
        public int DepositInAccountID;
        public string DepositInAccountName;
        public string DepositMemo;
        public DateTime DepositItemDate;
        public BankDepositType DepositItemType;
		public int DepositItemAccountID;
        public string DepositItemAccountName;
        public string DepositItemReference;
        public decimal DepositItemAmount;
        public int CashBackAccountID;
        public string CashBackAccountName;
        public decimal CashBackAmount;
        public string CashBackMemo;
        public long CreatedByID;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class BankDeposit
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

		public BankDeposit()
		{
			
		}

		public BankDeposit(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(BankDepositDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblBankDeposit (" +
                                "TransactionDate, " +
								"DepositStatus, " +
								"DepositInAccountID, " +
                                "DepositMemo, " +
                                "DepositItemDate, " +
                                "DepositItemType, " +
                                "DepositItemAccountID, " +
                                "DepositItemReference, " +
                                "DepositItemAmount, " +
                                "CashBackAccountID, " +
                                "CashBackAmount, " +
                                "CashBackMemo, " +
                                "CreatedByID" +
							") VALUES (" +
                                "@TransactionDate, " +
                                "@DepositStatus, " +
                                "@DepositInAccountID, " +
                                "@DepositMemo, " +
                                "@DepositItemDate, " +
                                "@DepositItemType, " +
                                "@DepositItemAccountID, " +
                                "@DepositItemReference, " +
                                "@DepositItemAmount, " +
                                "@CashBackAccountID, " +
                                "@CashBackAmount, " +
                                "@CashBackMemo, " +
                                "@CreatedByID" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@TransactionDate", Details.TransactionDate);
                cmd.Parameters.AddWithValue("@DepositStatus", Details.DepositStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@DepositInAccountID", Details.DepositInAccountID);
                cmd.Parameters.AddWithValue("@DepositMemo", Details.DepositMemo);
                cmd.Parameters.AddWithValue("@DepositItemDate", Details.DepositItemDate);
                cmd.Parameters.AddWithValue("@DepositItemType", Details.DepositItemType.ToString("d"));
                cmd.Parameters.AddWithValue("@DepositItemAccountID", Details.DepositItemAccountID);
                cmd.Parameters.AddWithValue("@DepositItemReference", Details.DepositItemReference);
                cmd.Parameters.AddWithValue("@DepositItemAmount", Details.DepositItemAmount);
                cmd.Parameters.AddWithValue("@CashBackAccountID", Details.CashBackAccountID);
                cmd.Parameters.AddWithValue("@CashBackAmount", Details.CashBackAmount);
                cmd.Parameters.AddWithValue("@CashBackMemo", Details.CashBackMemo);
                cmd.Parameters.AddWithValue("@CreatedByID", Details.CreatedByID);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int32 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt32(0);
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

		public void Update(BankDepositDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblBankDeposit SET " +
                                "TransactionDate        = @TransactionDate, " +
                                "DepositStatus          = @DepositStatus, " +
                                "DepositInAccountID     = @DepositInAccountID, " +
                                "DepositMemo            = @DepositMemo, " +
                                "DepositItemDate        = @DepositItemDate, " +
                                "DepositItemType        = @DepositItemType, " +
                                "DepositItemAccountID   = @DepositItemAccountID, " +
                                "DepositItemReference   = @DepositItemReference, " +
                                "DepositItemAmount      = @DepositItemAmount, " +
                                "CashBackAccountID      = @CashBackAccountID, " +
                                "CashBackAmount         = @CashBackAmount, " +
                                "CashBackMemo           = @CashBackMemo " +
							"WHERE BankDepositID = @BankDepositID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionDate", Details.TransactionDate);
                cmd.Parameters.AddWithValue("@DepositStatus", Details.DepositStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@DepositInAccountID", Details.DepositInAccountID);
                cmd.Parameters.AddWithValue("@DepositMemo", Details.DepositMemo);
                cmd.Parameters.AddWithValue("@DepositItemDate", Details.DepositItemDate);
                cmd.Parameters.AddWithValue("@DepositItemType", Details.DepositItemType.ToString("d"));
                cmd.Parameters.AddWithValue("@DepositItemAccountID", Details.DepositItemAccountID);
                cmd.Parameters.AddWithValue("@DepositItemReference", Details.DepositItemReference);
                cmd.Parameters.AddWithValue("@DepositItemAmount", Details.DepositItemAmount);
                cmd.Parameters.AddWithValue("@CashBackAccountID", Details.CashBackAccountID);
                cmd.Parameters.AddWithValue("@CashBackAmount", Details.CashBackAmount);
                cmd.Parameters.AddWithValue("@CashBackMemo", Details.CashBackMemo);
                cmd.Parameters.AddWithValue("@CreatedByID", Details.CreatedByID);
                cmd.Parameters.AddWithValue("@BankDepositID", Details.BankDepositID);

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
				string SQL=	"DELETE FROM tblBankDeposit WHERE BankDepositID IN (" + IDs + ");";
				  
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
                                    "BankDepositID, " +
                                    "TransactionDate, " +
                                    "DepositStatus, " +
                                    "DepositInAccountID, " +
                                    "b.ChartOfAccountName AS DepositInAccountName, " +
                                    "DepositMemo, " +
                                    "DepositItemDate, " +
                                    "DepositItemType, " +
                                    "DepositItemAccountID, " +
                                    "c.ChartOfAccountName AS DepositItemAccountName, " +
                                    "DepositItemReference, " +
                                    "DepositItemAmount, " +
                                    "CashBackAccountID, " +
                                    "d.ChartOfAccountName AS CashBackAccountName, " +
                                    "CashBackAmount, " +
                                    "CashBackMemo, " +
                                    "CreatedByID " +
                                "FROM tblBankDeposit a " +
                                    "INNER JOIN tblChartOfAccounts b ON a.DepositInAccountID = b.ChartOfAccountID " +
                                    "LEFT OUTER JOIN tblChartOfAccounts c ON a.DepositItemAccountID = c.ChartOfAccountID " +
                                    "LEFT OUTER JOIN tblChartOfAccounts d ON a.CashBackAccountID = d.ChartOfAccountID ";
            return stSQL;
        }

		#region Details

		public BankDepositDetails Details(long BankDepositID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE BankDepositID = @BankDepositID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBankDepositID = new MySqlParameter("@BankDepositID",MySqlDbType.Int64);			
				prmBankDepositID.Value = BankDepositID;
				cmd.Parameters.Add(prmBankDepositID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				BankDepositDetails Details = new BankDepositDetails();

				while (myReader.Read()) 
				{
					Details.BankDepositID = BankDepositID;
                    Details.TransactionDate = myReader.GetDateTime("TransactionDate");
					Details.DepositStatus = (BankDepositStatus) Enum.Parse(typeof(BankDepositStatus), myReader.GetString("DepositStatus"));
                    Details.DepositInAccountID = myReader.GetInt32("DepositInAccountID");
                    Details.DepositInAccountName = "" + myReader["DepositInAccountName"].ToString();
					Details.DepositMemo = "" + myReader["DepositMemo"].ToString();
					Details.DepositItemDate = myReader.GetDateTime("DepositItemDate");
                    Details.DepositItemType = (BankDepositType)Enum.Parse(typeof(BankDepositType), myReader.GetString("DepositItemType"));
                    Details.DepositItemAccountID = myReader.GetInt32("DepositItemAccountID");
                    Details.DepositItemAccountName = "" + myReader["DepositItemAccountName"].ToString();
                    Details.DepositItemReference = "" + myReader["DepositItemReference"].ToString();
                    Details.DepositItemAmount = myReader.GetDecimal("DepositItemAmount");
                    Details.CashBackAccountID = myReader.GetInt32("CashBackAccountID");
                    Details.CashBackAccountName = "" + myReader["CashBackAccountName"].ToString();
                    Details.CashBackAmount = myReader.GetDecimal("CashBackAmount");
                    Details.CashBackMemo = "" + myReader["CashBackMemo"].ToString();
                    Details.CreatedByID = myReader.GetInt64("CreatedByID");
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

                System.Data.DataTable dt = new System.Data.DataTable("tblBankDeposit");
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

        public MySqlDataReader List(BankDepositStatus DepositStatus, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.DepositStatus = @DepositStatus  ORDER BY " + SortField;

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
                cmd.Parameters.AddWithValue("@DepositStatus", DepositStatus.ToString("d"));

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
        public System.Data.DataTable ListAsDataTable(int DepositStatus, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.DepositStatus = @DepositStatus  ORDER BY " + SortField;


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
                cmd.Parameters.AddWithValue("@DepositStatus", DepositStatus.ToString("d"));

                System.Data.DataTable dt = new System.Data.DataTable("tblBankDeposit");
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

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + 
                            "WHERE TransactionDate LIKE @SearchKey " +
								"or DepositMemo LIKE @SearchKey " +
								"or DepositItemReference LIKE @SearchKey " +
                                 "or CashBackMemo LIKE @SearchKey " +
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
				prmSearchKey.Value = "%" + SearchKey +"%";
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
        {
            try
            {
                string SQL = SQLSelect() +
                            "WHERE TransactionDate LIKE @SearchKey " +
                                "or DepositMemo LIKE @SearchKey " +
                                "or DepositItemReference LIKE @SearchKey " +
                                 "or CashBackMemo LIKE @SearchKey " +
                            "ORDER BY " + SortField;

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

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblBankDeposit");
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

		public void Cancel(int BankDepositID)
		{
			try 
			{
				string SQL = "UPDATE tblBankDeposit SET " +
                                "DepositStatus      = @DepositStatus " +
							"WHERE BankDepositID    = @BankDepositID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DepositStatus", BankDepositStatus.Cancelled.ToString("d"));
				cmd.Parameters.AddWithValue("@BankDepositID", BankDepositID);

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
		public void Post(long BankDepositID, decimal Amount)
		{
			try 
			{
                string SQL = "UPDATE tblBankDeposit SET " +
                                "DepositStatus      = @DepositStatus " +
                            "WHERE BankDepositID    = @BankDepositID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DepositStatus", BankDepositStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@BankDepositID", BankDepositID);

                cmd.ExecuteNonQuery();

                BankDepositDetails clsBankDepositDetails = Details(BankDepositID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);
                
                clsChartOfAccount.UpdateDebit(clsBankDepositDetails.DepositInAccountID, clsBankDepositDetails.DepositItemAmount);
                clsChartOfAccount.UpdateCredit(clsBankDepositDetails.DepositInAccountID, clsBankDepositDetails.CashBackAmount);

                clsChartOfAccount.UpdateDebit(clsBankDepositDetails.CashBackAccountID, clsBankDepositDetails.CashBackAmount);

                clsChartOfAccount.UpdateCredit(clsBankDepositDetails.DepositItemAccountID, clsBankDepositDetails.DepositItemAmount);

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

