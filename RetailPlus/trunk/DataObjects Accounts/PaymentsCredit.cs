
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
	public struct PaymentsCreditDetails
	{
		public long PaymentCreditID;
		public long PaymentID;
		public int ChartOfAccountID;
		public decimal Amount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PaymentsCredit
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

		public PaymentsCredit()
		{
			
		}

		public PaymentsCredit(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public long Insert(PaymentsCreditDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPaymentCredit (" +
								"PaymentID, " +
								"ChartOfAccountID, " +
								"Amount" +
							") VALUES (" +
								"@PaymentID, " +
								"@ChartOfAccountID, " +
								"@Amount" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",System.Data.DbType.Int64);
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
     
				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				long iID = 0;

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

		public void Update(PaymentsCreditDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblPaymentCredit SET " + 
								"PaymentID			=	@PaymentID, " +
								"ChartOfAccountID	=	@ChartOfAccountID, " +
								"Amount				=	@Amount " +
							"WHERE PaymentCreditID	=	@PaymentCreditID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",System.Data.DbType.Int64);
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlParameter prmPaymentCreditID = new MySqlParameter("@PaymentCreditID",System.Data.DbType.Int64);	
				prmPaymentCreditID.Value = Details.PaymentCreditID;
				cmd.Parameters.Add(prmPaymentCreditID);

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
				string SQL=	"DELETE FROM tblPaymentCredit WHERE PaymentCreditID IN (" + IDs + ");";
				  
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

		#region Details

		public PaymentsCreditDetails Details(long PaymentCreditID)
		{
			try
			{
				string SQL =	"SELECT " +
									"PaymentCreditID, " +
									"PaymentID, " +
									"a.ChartOfAccountID, " +
									"ChartOfAccountCode, " +
									"ChartOfAccountName, " +
									"Amount " +
								"FROM tblPaymentCredit a INNER JOIN tblChartOfAccount b " +
									"ON a.ChartOfAccountID = b.ChartOfAccountID " +
								"WHERE PaymentCreditID = @PaymentCreditID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentCreditID = new MySqlParameter("@PaymentCreditID",System.Data.DbType.Int64);
				prmPaymentCreditID.Value = PaymentCreditID;
				cmd.Parameters.Add(prmPaymentCreditID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				PaymentsCreditDetails Details = new PaymentsCreditDetails();

				while (myReader.Read()) 
				{
					Details.PaymentCreditID = PaymentCreditID;
					Details.PaymentID = myReader.GetInt64("PaymentID");
					Details.ChartOfAccountID = myReader.GetInt32("ChartOfAccountID");
					Details.Amount = myReader.GetDecimal("Amount");
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
				string SQL ="SELECT " +
								"PaymentCreditID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentCredit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
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
		
		public MySqlDataReader List(long PaymentID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentCreditID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentCredit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID = @PaymentID " +
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
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

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
				string SQL ="SELECT " +
								"PaymentCreditID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentCredit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID LIKE @SearchKey " +
								"or ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey " +
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

		public MySqlDataReader Search(long PaymentID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentCreditID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentCredit a INNER JOIN tblChartOfAccount b " +
								"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID = @PaymentID " +
								"AND (ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey) " +
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
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

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

		public void Post(long PaymentID)
		{
			try 
			{
				GetConnection();
				ChartOfAccount clsChartOfAccount = new ChartOfAccount(Connection, Transaction);

				MySqlDataReader myReader = List(PaymentID, "PaymentCreditID",SortOption.Ascending);
				while (myReader.Read())
				{
					int ChartOfAccountID = myReader.GetInt32("ChartOfAccountID");
					decimal Amount = myReader.GetDecimal("Amount");

					clsChartOfAccount.UpdateCredit(ChartOfAccountID, Amount);
				}
				myReader.Close();

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
	}
}

