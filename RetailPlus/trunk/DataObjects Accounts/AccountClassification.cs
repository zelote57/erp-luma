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
	public struct AccountClassificationDetails
	{
		public int AccountClassificationID;
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
	public class AccountClassification
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

		public AccountClassification()
		{
			
		}

		public AccountClassification(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(AccountClassificationDetails Details)
		{
			try 
			{
                string SQL = "INSERT INTO tblAccountClassification (AccountClassificationCode, AccountClassificationName, AccountClassificationType) VALUES (@AccountClassificationCode, @AccountClassificationName, @AccountClassificationType);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmAccountClassificationCode = new MySqlParameter("@AccountClassificationCode",MySqlDbType.String);			
				prmAccountClassificationCode.Value = Details.AccountClassificationCode;
				cmd.Parameters.Add(prmAccountClassificationCode);

                MySqlParameter prmAccountClassificationName = new MySqlParameter("@AccountClassificationName",MySqlDbType.String);
                prmAccountClassificationName.Value = Details.AccountClassificationName;
                cmd.Parameters.Add(prmAccountClassificationName);

                MySqlParameter prmAccountClassificationType = new MySqlParameter("@AccountClassificationType",MySqlDbType.Int16);
                prmAccountClassificationType.Value = Details.AccountClassificationType.ToString("d");
                cmd.Parameters.Add(prmAccountClassificationType);
     
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

		public void Update(AccountClassificationDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblAccountClassification SET " + 
								"AccountClassificationCode		= @AccountClassificationCode, " +
                                "AccountClassificationName		= @AccountClassificationName, " +
								"AccountClassificationType		= @AccountClassificationType " +
							"WHERE AccountClassificationID = @AccountClassificationID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountClassificationCode = new MySqlParameter("@AccountClassificationCode",MySqlDbType.String);			
				prmAccountClassificationCode.Value = Details.AccountClassificationCode;
				cmd.Parameters.Add(prmAccountClassificationCode);

                MySqlParameter prmAccountClassificationName = new MySqlParameter("@AccountClassificationName",MySqlDbType.String);
                prmAccountClassificationName.Value = Details.AccountClassificationName;
                cmd.Parameters.Add(prmAccountClassificationName);

                MySqlParameter prmAccountClassificationType = new MySqlParameter("@AccountClassificationType",MySqlDbType.Int16);
                prmAccountClassificationType.Value = Details.AccountClassificationType.ToString("d");
                cmd.Parameters.Add(prmAccountClassificationType);

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
				string SQL=	"DELETE FROM tblAccountClassification WHERE AccountClassificationID IN (" + IDs + ");";
				  
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
                                    "AccountClassificationID, " +
                                    "AccountClassificationCode, " +
                                    "AccountClassificationName, " +
                                    "AccountClassificationType " +
                                "FROM tblAccountClassification ";
            return stSQL;
        }

		#region Details

		public AccountClassificationDetails Details(Int32 AccountClassificationID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE AccountClassificationID = @AccountClassificationID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountClassificationID = new MySqlParameter("@AccountClassificationID",MySqlDbType.Int16);
				prmAccountClassificationID.Value = AccountClassificationID;
				cmd.Parameters.Add(prmAccountClassificationID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				AccountClassificationDetails Details = new AccountClassificationDetails();

				while (myReader.Read()) 
				{
					Details.AccountClassificationID = AccountClassificationID;
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
				string SQL = SQLSelect() + "WHERE AccountClassificationName LIKE @SearchKey " +
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

