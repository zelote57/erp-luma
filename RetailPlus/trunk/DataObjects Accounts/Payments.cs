
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
	public struct PaymentsDetails
	{
		public long PaymentID;
        public int BankID;
        public string BankCode;
        public string BankName;
		public DateTime ChequeDate;
		public string ChequeNo;
		public long PayeeID;
		public string PayeeCode;
		public string PayeeName;
		public string Particulars;
		public AccountPaymentsStatus Status;
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
	public class Payments
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

		public Payments()
		{
			
		}

		public Payments(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public long Insert(PaymentsDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPayment (" +
                                "BankID, " +
                                "BankCode, " +
								"ChequeDate, " +
								"ChequeNo, " +
								"PayeeID, " +
								"PayeeCode, " +
								"PayeeName, " +
								"Particulars, " +
								"Status, " +
								"TotalDebitAmount, " +
								"TotalCreditAmount" +
							") VALUES (" +
                                "@BankID, " +
                                "@BankCode, " +
								"@ChequeDate, " +
								"@ChequeNo, " +
								"@PayeeID, " +
								"@PayeeCode, " +
								"@PayeeName, " +
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

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = Details.BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = Details.BankCode;
                cmd.Parameters.Add(prmBankCode);

				MySqlParameter prmChequeDate = new MySqlParameter("@ChequeDate",System.Data.DbType.Date);
				prmChequeDate.Value = Details.ChequeDate.ToString("yyyy-MM-dd");
				cmd.Parameters.Add(prmChequeDate);

				MySqlParameter prmChequeNo = new MySqlParameter("@ChequeNo",MySqlDbType.String);			
				prmChequeNo.Value = Details.ChequeNo;
				cmd.Parameters.Add(prmChequeNo);

				MySqlParameter prmPayeeID = new MySqlParameter("@PayeeID",System.Data.DbType.Int64);
				prmPayeeID.Value = Details.PayeeID;
				cmd.Parameters.Add(prmPayeeID);

				MySqlParameter prmPayeeCode = new MySqlParameter("@PayeeCode",MySqlDbType.String);
				prmPayeeCode.Value = Details.PayeeCode;
				cmd.Parameters.Add(prmPayeeCode);

				MySqlParameter prmPayeeName = new MySqlParameter("@PayeeName",MySqlDbType.String);	
				prmPayeeName.Value = Details.PayeeName;
				cmd.Parameters.Add(prmPayeeName);

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",System.Data.DbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",System.Data.DbType.Decimal);			
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
		public void Update(PaymentsDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblPayment SET " +
                                "BankID			    =	@BankID, " +
                                "BankCode			=	@BankCode, " +
								"ChequeDate			=	@ChequeDate, " +
								"ChequeNo			=	@ChequeNo, " +
								"PayeeID			=	@PayeeID, " +
								"PayeeCode			=	@PayeeCode, " +
								"PayeeName			=	@PayeeName, " +
								"Particulars		=	@Particulars, " +
								"Status				=	@Status, " +
								"TotalDebitAmount	=	@TotalDebitAmount, " +
								"TotalCreditAmount	=	@TotalCreditAmount " +
							"WHERE PaymentID = @PaymentID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = Details.BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = Details.BankCode;
                cmd.Parameters.Add(prmBankCode);

				MySqlParameter prmChequeDate = new MySqlParameter("@ChequeDate",System.Data.DbType.Date);
				prmChequeDate.Value = Details.ChequeDate.ToString("yyyy-MM-dd");
				cmd.Parameters.Add(prmChequeDate);

				MySqlParameter prmChequeNo = new MySqlParameter("@ChequeNo",MySqlDbType.String);			
				prmChequeNo.Value = Details.ChequeNo;
				cmd.Parameters.Add(prmChequeNo);

				MySqlParameter prmPayeeID = new MySqlParameter("@PayeeID",System.Data.DbType.Int64);
				prmPayeeID.Value = Details.PayeeID;
				cmd.Parameters.Add(prmPayeeID);

				MySqlParameter prmPayeeCode = new MySqlParameter("@PayeeCode",MySqlDbType.String);
				prmPayeeCode.Value = Details.PayeeCode;
				cmd.Parameters.Add(prmPayeeCode);

				MySqlParameter prmPayeeName = new MySqlParameter("@PayeeName",MySqlDbType.String);	
				prmPayeeName.Value = Details.PayeeName;
				cmd.Parameters.Add(prmPayeeName);

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",System.Data.DbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",System.Data.DbType.Decimal);			
				prmTotalCreditAmount.Value = Details.TotalCreditAmount;
				cmd.Parameters.Add(prmTotalCreditAmount);

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);	
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

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
                string SQL = "UPDATE tblPayment SET Status = @CancelledStatus AND CancelledDate = @CancelledDate WHERE PaymentID IN (" + IDs + ");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmCancelledStatus = new MySqlParameter("@CancelledStatus",MySqlDbType.Int16);
                prmCancelledStatus.Value = AccountPaymentsStatus.Cancelled.ToString("d");
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
                                "PaymentID, " +
                                "a.BankID, " +
                                "a.BankCode, " +
                                "BankName, " +
                                "ChequeDate, " +
                                "ChequeNo, " +
                                "PayeeID, " +
                                "PayeeCode, " +
                                "PayeeName, " +
                                "Particulars, " +
                                "Status, " +
                                "TotalDebitAmount, " +
                                "TotalCreditAmount " +
                            "FROM tblPayment a " +
                                "INNER JOIN tblBank b ON a.BankID = b.BankID ";
            return stSQL;
        }

		#region Details

		public PaymentsDetails Details(long PaymentID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE PaymentID = @PaymentID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				PaymentsDetails Details = new PaymentsDetails();

				while (myReader.Read()) 
				{
					Details.PaymentID = PaymentID;
                    Details.BankID = myReader.GetInt32("BankID");
                    Details.BankCode = "" + myReader["BankCode"].ToString();
                    Details.BankName = "" + myReader["BankName"].ToString();
					Details.ChequeDate = myReader.GetDateTime("ChequeDate");
					Details.ChequeNo = "" + myReader["ChequeNo"].ToString();
					Details.PayeeID = myReader.GetInt64("PayeeID");
					Details.PayeeCode = "" + myReader["PayeeCode"].ToString();
					Details.PayeeName = "" + myReader["PayeeName"].ToString();
					Details.Particulars = "" + myReader["Particulars"].ToString();
                    Details.Status = (AccountPaymentsStatus)Enum.Parse(typeof(AccountPaymentsStatus), myReader.GetString("Status"));
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
				string SQL =SQLSelect() + "WHERE ChequeNo LIKE @SearchKey " +
								                "or ChequeDate LIKE @SearchKey " +
								                "or PayeeCode LIKE @SearchKey " +
								                "or PayeeName LIKE @SearchKey " +
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

		public void Post(long PaymentID, DateTime PostingDate)
		{
			try 
			{
				string SQL = "UPDATE tblPayment SET " + 
								"PostingDate		=	@PostingDate, " +
								"Status				=	@Status " +
							"WHERE PaymentID = @PaymentID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPostingDate = new MySqlParameter("@PostingDate",System.Data.DbType.Date);
				prmPostingDate.Value = PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = AccountPaymentsStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",System.Data.DbType.Int64);	
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				cmd.ExecuteNonQuery();


				PaymentsDebit clsPaymentsDebit = new PaymentsDebit(Connection, Transaction);
				clsPaymentsDebit.Post(PaymentID);
			
				PaymentsCredit clsPaymentsCredit = new PaymentsCredit(Connection, Transaction);
				clsPaymentsCredit.Post(PaymentID);
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

