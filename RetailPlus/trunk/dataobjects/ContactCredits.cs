using System;
using System.Data;
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

	public struct ContactCreditDetails
	{
		public long ContactID;
        public long GuarantorID;
        public CreditType CreditType;
        public string CreditCardNo;
        public DateTime CreditAwardDate;
        public decimal TotalPurchases;
        public decimal CreditPaid;
        public CreditCardStatus CreditCardStatus;
        public DateTime ExpiryDate;

        public bool CreditActive;
        public decimal CreditLimit;
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
	public class ContactCredit
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

		public ContactCredit()
		{
			
		}

        public ContactCredit(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public bool Insert(ContactCreditDetails Details)
		{
			try  
			{
                string SQL = "CALL procContactCreditModify(@lngCustomerID, @lngGuarantorID, @intCreditType, @strCreditCardNo, @dteCreditAwardDate, @intCreditCardStatus, @dteExpiryDate, @intCreditActive, @decCreditLimit);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngCustomerID", Details.ContactID);
                cmd.Parameters.AddWithValue("@lngGuarantorID", Details.GuarantorID);
                cmd.Parameters.AddWithValue("@intCreditType", Convert.ToInt16(Details.CreditType));
                cmd.Parameters.AddWithValue("@strCreditCardNo", Details.CreditCardNo);
                cmd.Parameters.AddWithValue("@dteCreditAwardDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@intCreditCardStatus", Details.CreditCardStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@dteExpiryDate", Details.ExpiryDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@intCreditActive", Convert.ToInt16(Details.CreditActive));
                cmd.Parameters.AddWithValue("@decCreditLimit", Details.CreditLimit);

				bool bolRetValue = false;
                if (cmd.ExecuteNonQuery() > 0) bolRetValue = true;
                return bolRetValue;
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
		public bool Update(ContactCreditDetails Details)
		{
			try 
			{
                return Insert(Details);
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
				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";
				  
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
                                "CustomerID, " +
                                "GuarantorID, " +
                                "CreditType, " +
                                "CreditCardNo, " +
                                "CreditAwardDate, " +
                                "TotalPurchases, " +
                                "CreditPaid, " +
                                "CreditCardStatus, " +
                                "ExpiryDate " +
                            "FROM tblContactCreditCardInfo ";
            return stSQL;
        }

		#region Details

		public ContactCreditDetails Details(long ContactID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE CustomerID = @ContactID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ContactID", ContactID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ContactCreditDetails Details = new ContactCreditDetails();

				while (myReader.Read()) 
				{
					Details.ContactID = myReader.GetInt64("CustomerID");
                    Details.GuarantorID = myReader.GetInt64("GuarantorID");
                    Details.CreditType = (CreditType)Enum.Parse(typeof(CreditType), myReader.GetString("CreditType"));
                    Details.CreditCardNo = "" + myReader["CreditCardNo"].ToString();
                    Details.CreditAwardDate = myReader.GetDateTime("CreditAwardDate");
                    Details.TotalPurchases = myReader.GetDecimal("TotalPurchases");
                    Details.CreditPaid = myReader.GetDecimal("CreditPaid");
                    Details.CreditCardStatus = (CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), myReader.GetString("CreditCardStatus"));
                    Details.ExpiryDate = myReader.GetDateTime("ExpiryDate");
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
		public ContactCreditDetails Details(string CreditCardNo)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE CreditCardNo = @CreditCardNo;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ContactCreditDetails Details = new ContactCreditDetails();

                while (myReader.Read())
                {
                    Details.ContactID = myReader.GetInt64("CustomerID");
                    Details.GuarantorID = myReader.GetInt64("GuarantorID");
                    Details.CreditType = (CreditType)Enum.Parse(typeof(CreditType), myReader.GetString("CreditType"));
                    Details.CreditCardNo = "" + myReader["CreditCardNo"].ToString();
                    Details.CreditAwardDate = myReader.GetDateTime("CreditAwardDate");
                    Details.TotalPurchases = myReader.GetDecimal("TotalPurchases");
                    Details.CreditPaid = myReader.GetDecimal("CreditPaid");
                    Details.CreditCardStatus = (CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), myReader.GetString("CreditCardStatus"));
                    Details.ExpiryDate = myReader.GetDateTime("ExpiryDate");
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
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) " +
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

        public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE ORDER BY " + SortField;

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

            System.Data.DataTable dt = new System.Data.DataTable("tblContactCredits");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE (CreditCardNo LIKE @SearchKey or CreditActive LIKE @SearchKey) ";

            SQL += "ORDER BY " + SortField;

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

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");

            System.Data.DataTable dt = new System.Data.DataTable("tblContactCredits");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

		#endregion

		#region Public Modifiers

        public void AddPurchase(long ContactID, decimal Amount)
        {
            try
            {
                string SQL = "CALL procContactCreditsAddPurchase(@ContactID, @Amount);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Amount", Amount);

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
		public void AddCredit(long ContactID, decimal Amount)
		{
			try 
			{
                Contact clsContact = new Contact(mConnection, mTransaction);
                clsContact.AddCredit(ContactID, Amount);
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
        public void DeductCredit(long ContactID, decimal Amount)
		{
			try 
			{
                Contact clsContact = new Contact(mConnection, mTransaction);
                clsContact.AddCredit(ContactID, -Amount);
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

        public void AddMovement(long lngCustomerID, DateTime dteCreditDate, decimal decCreditPointsBefore, decimal decCreditPointsAdjustment, decimal decCreditPointsAfter, DateTime dteCreditExpiryDate, string strCreditReason, string strTerminalNo, string strCashierName, string strTransactionNo)
        {
            try
            {
                string SQL = "CALL procContactCreditsMovementInsert(@lngCustomerID, @dteCreditDate, @decCreditPointsBefore, @decCreditPointsAdjustment, @decCreditPointsAfter, @dteCreditExpiryDate, @strCreditReason, @strTerminalNo, @strCashierName, @strTransactionNo);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngCustomerID", lngCustomerID);
                cmd.Parameters.AddWithValue("@dteCreditDate", dteCreditDate);
                cmd.Parameters.AddWithValue("@decCreditPointsBefore", decCreditPointsBefore);
                cmd.Parameters.AddWithValue("@decCreditPointsAdjustment", decCreditPointsAdjustment);
                cmd.Parameters.AddWithValue("@decCreditPointsAfter", decCreditPointsAfter);
                cmd.Parameters.AddWithValue("@dteCreditExpiryDate", dteCreditExpiryDate);
                cmd.Parameters.AddWithValue("@strCreditReason", strCreditReason);
                cmd.Parameters.AddWithValue("@strTerminalNo", strTerminalNo);
                cmd.Parameters.AddWithValue("@strCashierName", strCashierName);
                cmd.Parameters.AddWithValue("@strTransactionNo", strTransactionNo);

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
	}
}