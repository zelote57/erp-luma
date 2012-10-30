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
	public struct BankDetails
	{
		public int BankID;
		public string BankCode;
		public string BankName;
        public string ChequeCode;
        public string ChequeCounter;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Bank
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

		public Bank()
		{
			
		}

		public Bank(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(BankDetails Details)
		{
			try 
			{
                string SQL = "INSERT INTO tblBank (BankCode, BankName, ChequeCode, ChequeCounter) " +
                                                    "VALUES (@BankCode, @BankName, @ChequeCode, @ChequeCounter);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);			
				prmBankCode.Value = Details.BankCode;
				cmd.Parameters.Add(prmBankCode);

                MySqlParameter prmBankName = new MySqlParameter("@BankName",MySqlDbType.String);
				prmBankName.Value = Details.BankName;
				cmd.Parameters.Add(prmBankName);

                MySqlParameter prmChequeCode = new MySqlParameter("@ChequeCode",MySqlDbType.String);
                prmChequeCode.Value = Details.ChequeCode;
                cmd.Parameters.Add(prmChequeCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = Details.ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);
     
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
		public void Update(BankDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblBank SET " + 
								"BankCode		= @BankCode, " +
								"BankName		= @BankName, " +
                                "ChequeCode     = @ChequeCode, " +
                                "ChequeCounter  = @ChequeCounter " +
							"WHERE BankID = @BankID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);			
				prmBankCode.Value = Details.BankCode;
				cmd.Parameters.Add(prmBankCode);		

				MySqlParameter prmBankName = new MySqlParameter("@BankName",MySqlDbType.String);			
				prmBankName.Value = Details.BankName;
				cmd.Parameters.Add(prmBankName);

                MySqlParameter prmChequeCode = new MySqlParameter("@ChequeCode",MySqlDbType.String);
                prmChequeCode.Value = Details.ChequeCode;
                cmd.Parameters.Add(prmChequeCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = Details.ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

				MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);			
				prmBankID.Value = Details.BankID;
				cmd.Parameters.Add(prmBankID);

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
        public void UpdateChequeCounter(int BankID, string ChequeCounter)
        {
            try
            {
                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankID = @BankID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

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
        public void UpdateChequeCounter(string BankCode, string ChequeCounter)
        {
            try
            {
                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankCode	    = @BankCode ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

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
				string SQL=	"DELETE FROM tblBank WHERE BankID IN (" + IDs + ");";
				  
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
                                "BankID, " +
                                "BankCode, " +
                                "BankName, " +
                                "ChequeCode, " +
                                "ChequeCounter " +
                            "FROM tblBank ";
            return stSQL;
        }

		#region Details

		public BankDetails Details(Int32 BankID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE BankID = @BankID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);
				prmBankID.Value = BankID;
				cmd.Parameters.Add(prmBankID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				BankDetails Details = new BankDetails();

				while (myReader.Read()) 
				{
					Details.BankID = BankID;
					Details.BankCode = "" + myReader["BankCode"].ToString();
					Details.BankName = "" + myReader["BankName"].ToString();
                    Details.ChequeCode = "" + myReader["ChequeCode"].ToString();
                    Details.ChequeCounter = "" + myReader["ChequeCounter"].ToString();
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

                System.Data.DataTable dt = new System.Data.DataTable("tblBank");
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
                string SQL = SQLSelect() + 
                            "WHERE BankCode LIKE @SearchKey " +
								"or BankName LIKE @SearchKey " +
                                "or ChequeCode LIKE @SearchKey " +
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() +
                            "WHERE BankCode LIKE @SearchKey " +
                                "or BankName LIKE @SearchKey " +
                                "or ChequeCode LIKE @SearchKey " +
                            "ORDER BY " + SortField;

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

                System.Data.DataTable dt = new System.Data.DataTable("tblBank");
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

        public string getChequeNo()
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = 1";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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
        public string getChequeNo(int BankID)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankID ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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
        public string getChequeNo(string BankCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankCode ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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
        public string getChequeNo(out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = 1";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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
        public string getChequeNo(int BankID, out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankID ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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
        public string getChequeNo(string BankCode, out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankCode ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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

