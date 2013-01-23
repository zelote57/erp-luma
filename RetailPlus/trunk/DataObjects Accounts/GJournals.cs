
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
	public struct GJournalsDetails
	{
		public long GJournalID;
		public string Particulars;
		public AccountGJournalsStatus Status;
		public DateTime PostingDate;
		public DateTime CancelledDate;
		public decimal TotalDebitAmount;
		public decimal TotalCreditAmount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class GJournals
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

		public GJournals()
		{
			
		}

		public GJournals(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public long Insert(GJournalsDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblGJournal (" +
								"Particulars, " +
								"Status, " +
								"TotalDebitAmount, " +
								"TotalCreditAmount" +
							") VALUES (" +
								"@Particulars, " +
								"@Status, " +
								"@TotalDebitAmount, " +
								"@TotalCreditAmount" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",MySqlDbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",MySqlDbType.Decimal);			
				prmTotalCreditAmount.Value = Details.TotalCreditAmount;
				cmd.Parameters.Add(prmTotalCreditAmount);
     
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
		public void Update(GJournalsDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblGJournal SET " +
								"Particulars		=	@Particulars, " +
								"Status				=	@Status, " +
								"TotalDebitAmount	=	@TotalDebitAmount, " +
								"TotalCreditAmount	=	@TotalCreditAmount " +
							"WHERE GJournalID = @GJournalID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",MySqlDbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",MySqlDbType.Decimal);			
				prmTotalCreditAmount.Value = Details.TotalCreditAmount;
				cmd.Parameters.Add(prmTotalCreditAmount);

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);				
				prmGJournalID.Value = Details.GJournalID;
				cmd.Parameters.Add(prmGJournalID);

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

		#region Cancel

		public bool Cancel(string IDs)
		{
			try 
			{
                string SQL = "UPDATE tblGJournal SET Status = @CancelledStatus AND CancelledDate = @CancelledDate WHERE GJournalID IN (" + IDs + ");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmCancelledStatus = new MySqlParameter("@CancelledStatus",MySqlDbType.Int16);
                prmCancelledStatus.Value = AccountGJournalsStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmCancelledStatus);

                MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
                prmCancelledDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmCancelledDate);

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
                                "GJournalID, " +
                                "Particulars, " +
                                "Status, " +
                                "TotalDebitAmount, " +
                                "TotalCreditAmount " +
                            "FROM tblGJournal ";
            return stSQL;
        }

		#region Details

		public GJournalsDetails Details(long GJournalID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE GJournalID = @GJournalID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				GJournalsDetails Details = new GJournalsDetails();

				while (myReader.Read()) 
				{
					Details.GJournalID = GJournalID;
					Details.Particulars = "" + myReader["Particulars"].ToString();
                    Details.Status = (AccountGJournalsStatus)Enum.Parse(typeof(AccountGJournalsStatus), myReader.GetString("Status"));
					Details.TotalDebitAmount = myReader.GetDecimal("TotalDebitAmount");
					Details.TotalCreditAmount = myReader.GetDecimal("TotalCreditAmount");
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
				string SQL =SQLSelect() + "WHERE Particulars LIKE @SearchKey " +
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

		#region Public Modifiers

		public void Post(long GJournalID, DateTime PostingDate)
		{
			try 
			{
				string SQL = "UPDATE tblGJournal SET " + 
								"PostingDate		=	@PostingDate, " +
								"Status				=	@Status " +
							"WHERE GJournalID = @GJournalID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPostingDate = new MySqlParameter("@PostingDate",MySqlDbType.Date);
				prmPostingDate.Value = PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = AccountGJournalsStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);				
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				cmd.ExecuteNonQuery();


				GJournalsDebit clsGJournalsDebit = new GJournalsDebit(Connection, Transaction);
				clsGJournalsDebit.Post(GJournalID);
			
				GJournalsCredit clsGJournalsCredit = new GJournalsCredit(Connection, Transaction);
				clsGJournalsCredit.Post(GJournalID);
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

