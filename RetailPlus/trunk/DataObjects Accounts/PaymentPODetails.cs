
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
    public struct PaymentPODetailDetails
	{
		public long PaymentDetailID;
		public long PaymentID;
		public long POID;
		public decimal Amount;
        public POPaymentStatus PaymentStatus;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PaymentPODetails
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

		public PaymentPODetails()
		{
			
		}

        public PaymentPODetails(MySqlConnection Connection, MySqlTransaction Transaction)
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

        public long Insert(PaymentPODetailDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPaymentPODetails (" +
								"PaymentID, " +
								"POID, " +
								"Amount, " +
                                "PaymentStatus" +
							") VALUES (" +
								"@PaymentID, " +
								"@POID, " +
								"@Amount, " +
                                "@PaymentStatus" +
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

				MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);
				prmPOID.Value = Details.POID;
				cmd.Parameters.Add(prmPOID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = Details.PaymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

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

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPaymentPODetails WHERE PaymentDetailID IN (" + IDs + ");";
				  
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
        public bool Delete(long POID)
        {
            try
            {
                string SQL = "DELETE FROM tblPaymentPODetails WHERE POID = @POID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                                "PaymentDetailID, " +
                                "PaymentID, " +
                                "a.POID, " +
                                "PONo, " +
                                "PODate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "SupplierDRNo, " +
                                "DeliveryDate, " +
                                "BranchCode, " +
                                "a.Amount, " +
                                "b.PaidAmount, " +
                                "b.UnpaidAmount, " +
                                "b.PaymentStatus " +
                            "FROM tblPaymentPODetails a " +
                                "INNER JOIN tblPO b ON a.POID = b.POID " +
                                "INNER JOIN tblBranch c ON b.BranchID = c.BranchID ";
            return stSQL;
        }

		#region Details

        public PaymentPODetailDetails Details(long PaymentDetailID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE PaymentDetailID = @PaymentDetailID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentDetailID = new MySqlParameter("@PaymentDetailID",System.Data.DbType.Int64);
				prmPaymentDetailID.Value = PaymentDetailID;
				cmd.Parameters.Add(prmPaymentDetailID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                PaymentPODetailDetails Details = new PaymentPODetailDetails();

				while (myReader.Read()) 
				{
					Details.PaymentDetailID = PaymentDetailID;
					Details.PaymentID = myReader.GetInt64("PaymentID");
					Details.POID = myReader.GetInt32("POID");
					Details.Amount = myReader.GetDecimal("Amount");
                    Details.PaymentStatus = (POPaymentStatus) Enum.Parse(typeof(POPaymentStatus), myReader.GetString("PaymentStatus"));
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
		public MySqlDataReader List(long PaymentID, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE PaymentID = @PaymentID " +
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
                string SQL = SQLSelect() + "WHERE PaymentDetailID = @SearchKey " +
								            "or SupplierCode LIKE @SearchKey " +
								            "or PONo LIKE @SearchKey " +
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
				string SQL =SQLSelect() + "WHERE PaymentID = @PaymentID " +
                                             "or SupplierCode LIKE @SearchKey " +
                                            "or PONo LIKE @SearchKey " +
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

	}
}

