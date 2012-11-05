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
	public struct AccountCategoryDetails
	{
		public int AccountCategoryID;
		public string AccountCategoryCode;
		public string AccountCategoryName;
		public int AccountSummaryID;
		public string AccountSummaryCode;
		public string AccountSummaryName;
        public short AccountClassificationID;
        public string AccountClassificationCode;
        public string AccountClassificationName;
        public AccountClassificationType AccountClassificationType;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccountCategory
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

		public AccountCategory()
		{
			
		}

		public AccountCategory(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(AccountCategoryDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblAccountCategory (AccountSummaryID, AccountCategoryCode, AccountCategoryName) VALUES (@AccountSummaryID, @AccountCategoryCode, @AccountCategoryName);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);			
				prmAccountSummaryID.Value = Details.AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

				MySqlParameter prmAccountCategoryCode = new MySqlParameter("@AccountCategoryCode",MySqlDbType.String);			
				prmAccountCategoryCode.Value = Details.AccountCategoryCode;
				cmd.Parameters.Add(prmAccountCategoryCode);

				MySqlParameter prmAccountCategoryName = new MySqlParameter("@AccountCategoryName",MySqlDbType.String);			
				prmAccountCategoryName.Value = Details.AccountCategoryName;
				cmd.Parameters.Add(prmAccountCategoryName);
     
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

		public void Update(AccountCategoryDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblAccountCategory SET " + 
								"AccountSummaryID			= @AccountSummaryID, " +
								"AccountCategoryCode		= @AccountCategoryCode, " +
								"AccountCategoryName		= @AccountCategoryName " +
							"WHERE AccountCategoryID = @AccountCategoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);			
				prmAccountSummaryID.Value = Details.AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

				MySqlParameter prmAccountCategoryCode = new MySqlParameter("@AccountCategoryCode",MySqlDbType.String);			
				prmAccountCategoryCode.Value = Details.AccountCategoryCode;
				cmd.Parameters.Add(prmAccountCategoryCode);		

				MySqlParameter prmAccountCategoryName = new MySqlParameter("@AccountCategoryName",MySqlDbType.String);			
				prmAccountCategoryName.Value = Details.AccountCategoryName;
				cmd.Parameters.Add(prmAccountCategoryName);

				MySqlParameter prmAccountCategoryID = new MySqlParameter("@AccountCategoryID",MySqlDbType.Int16);			
				prmAccountCategoryID.Value = Details.AccountCategoryID;
				cmd.Parameters.Add(prmAccountCategoryID);

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
				string SQL=	"DELETE FROM tblAccountCategory WHERE AccountCategoryID IN (" + IDs + ");";
				  
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
                                    "AccountCategoryID, " +
                                    "a.AccountSummaryID, " +
                                    "AccountSummaryCode, " +
                                    "AccountSummaryName, " +
                                    "AccountCategoryCode, " +
                                    "AccountCategoryName, " +
                                    "b.AccountClassificationID, " +
                                    "AccountClassificationCode, " +
                                    "AccountClassificationName, " +
                                    "AccountClassificationType " +
                                "FROM tblAccountCategory a " +
                                "INNER JOIN tblAccountSummary b ON a.AccountSummaryID = b.AccountSummaryID " +
                                "INNER JOIN tblAccountClassification c ON b.AccountClassificationID = c.AccountClassificationID "; 
            return stSQL;
        }

		#region Details

		public AccountCategoryDetails Details(Int32 AccountCategoryID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE AccountCategoryID = @AccountCategoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountCategoryID = new MySqlParameter("@AccountCategoryID",MySqlDbType.Int16);
				prmAccountCategoryID.Value = AccountCategoryID;
				cmd.Parameters.Add(prmAccountCategoryID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				AccountCategoryDetails Details = new AccountCategoryDetails();

				while (myReader.Read()) 
				{
					Details.AccountCategoryID = AccountCategoryID;
					Details.AccountCategoryCode = "" + myReader["AccountCategoryCode"].ToString();
					Details.AccountCategoryName = "" + myReader["AccountCategoryName"].ToString();
					Details.AccountSummaryID = myReader.GetInt32("AccountSummaryID");
					Details.AccountSummaryCode = "" + myReader["AccountSummaryCode"].ToString();
					Details.AccountSummaryName = "" + myReader["AccountSummaryName"].ToString();
                    Details.AccountClassificationID = myReader.GetInt16("AccountClassificationID");
                    Details.AccountClassificationCode = "" + myReader["AccountClassificationCode"].ToString();
                    Details.AccountClassificationName = "" + myReader["AccountClassificationName"].ToString();
                    Details.AccountClassificationType = (AccountClassificationType)Enum.Parse(typeof(AccountClassificationType), myReader.GetString("AccountClassificationType"));
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
        public MySqlDataReader List(int AccountSummaryID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.AccountSummaryID = @AccountSummaryID ORDER BY " + SortField;

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

                MySqlParameter prmAccountSummaryID  = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int32);
                prmAccountSummaryID.Value = AccountSummaryID;
                cmd.Parameters.Add(prmAccountSummaryID);

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
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + 
                            "WHERE AccountCategoryCode LIKE @SearchKey " +
								"or AccountCategoryName LIKE @SearchKey " +
								"or AccountSummaryCode LIKE @SearchKey " +
								"or AccountSummaryName LIKE @SearchKey " +
                                "or AccountClassificationName LIKE @SearchKey " +
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

		#endregion
	}
}
