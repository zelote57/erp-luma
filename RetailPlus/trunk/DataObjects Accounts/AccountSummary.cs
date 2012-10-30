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
	public struct AccountSummaryDetails
	{
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
	public class AccountSummary
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

		public AccountSummary()
		{
			
		}

		public AccountSummary(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(AccountSummaryDetails Details)
		{
			try 
			{
                string SQL = "INSERT INTO tblAccountSummary (AccountSummaryCode, AccountSummaryName, AccountClassificationID) " +
                                                    "VALUES (@AccountSummaryCode, @AccountSummaryName, @AccountClassificationID);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmAccountSummaryCode = new MySqlParameter("@AccountSummaryCode",MySqlDbType.String);			
				prmAccountSummaryCode.Value = Details.AccountSummaryCode;
				cmd.Parameters.Add(prmAccountSummaryCode);

                MySqlParameter prmAccountSummaryName = new MySqlParameter("@AccountSummaryName",MySqlDbType.String);
				prmAccountSummaryName.Value = Details.AccountSummaryName;
				cmd.Parameters.Add(prmAccountSummaryName);

                MySqlParameter prmAccountClassificationID = new MySqlParameter("@AccountClassificationID",MySqlDbType.Int16);
                prmAccountClassificationID.Value = Details.AccountClassificationID;
                cmd.Parameters.Add(prmAccountClassificationID);
     
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

		public void Update(AccountSummaryDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblAccountSummary SET " + 
								"AccountSummaryCode		= @AccountSummaryCode, " +
								"AccountSummaryName		= @AccountSummaryName, " +
                                "AccountClassificationID= @AccountClassificationID " +
							"WHERE AccountSummaryID = @AccountSummaryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountSummaryCode = new MySqlParameter("@AccountSummaryCode",MySqlDbType.String);			
				prmAccountSummaryCode.Value = Details.AccountSummaryCode;
				cmd.Parameters.Add(prmAccountSummaryCode);		

				MySqlParameter prmAccountSummaryName = new MySqlParameter("@AccountSummaryName",MySqlDbType.String);			
				prmAccountSummaryName.Value = Details.AccountSummaryName;
				cmd.Parameters.Add(prmAccountSummaryName);

                MySqlParameter prmAccountClassificationID = new MySqlParameter("@AccountClassificationID",MySqlDbType.Int16);
                prmAccountClassificationID.Value = Details.AccountClassificationID;
                cmd.Parameters.Add(prmAccountClassificationID);

				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);			
				prmAccountSummaryID.Value = Details.AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

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
				string SQL=	"DELETE FROM tblAccountSummary WHERE AccountSummaryID IN (" + IDs + ");";
				  
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
                                "AccountSummaryID, " +
                                "AccountSummaryCode, " +
                                "AccountSummaryName, " +
                                "a.AccountClassificationID, " +
                                "AccountClassificationCode, " +
                                "AccountClassificationName, " +
                                "AccountClassificationType " +
                            "FROM tblAccountSummary a INNER JOIN " +
                                "tblAccountClassification b ON a.AccountClassificationID = b.AccountClassificationID ";
            return stSQL;
        }

		#region Details

		public AccountSummaryDetails Details(Int32 AccountSummaryID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE AccountSummaryID = @AccountSummaryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);
				prmAccountSummaryID.Value = AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				AccountSummaryDetails Details = new AccountSummaryDetails();

				while (myReader.Read()) 
				{
					Details.AccountSummaryID = AccountSummaryID;
					Details.AccountSummaryCode = "" + myReader["AccountSummaryCode"].ToString();
					Details.AccountSummaryName = "" + myReader["AccountSummaryName"].ToString();
                    Details.AccountClassificationID = myReader.GetInt16("AccountClassificationID");
                    Details.AccountClassificationCode = "" + myReader["AccountClassificationCode"].ToString();
                    Details.AccountClassificationName = "" + myReader["AccountClassificationName"].ToString();
                    Details.AccountClassificationType = (AccountClassificationType) Enum.Parse(typeof(AccountClassificationType), myReader.GetString("AccountClassificationType"));
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
				string SQL =SQLSelect() + "ORDER BY " + SortField;

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
                string SQL = SQLSelect() + 
                            "WHERE AccountSummaryCode LIKE @SearchKey " +
								"or AccountSummaryName LIKE @SearchKey " +
                                "or AccountClassificationCode LIKE @SearchKey " +
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

