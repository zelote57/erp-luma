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
	public struct CashCountDetails
	{
		public Int64 CashCountID;
		public Int64 CashierID;
		public string CashierName;
        public int BranchID;
        public string BranchCode;
		public string TerminalNo;
		public DateTime DateCreated;
		public Int32 DenominationID;
		public decimal DenominationValue;
		public string DenominationCode;
		public string ImagePath;
		public Int32 DenominationCount;
		public decimal DenominationAmount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CashCount
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

		public CashCount()
		{
			
		}

		public CashCount(MySqlConnection Connection, MySqlTransaction Transaction)
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

		private Int64 Insert(CashCountDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblCashCount (" +
								"CashierID, " +
								"CashierName, " +
                                "BranchID, " +
                                "BranchCode, " +
								"TerminalNo, " +
								"DateCreated, " +
								"DenominationID, " +
								"DenominationCount" +
							") VALUES (" +
								"@CashierID, " +
								"@CashierName, " +
                                "@BranchID, " +
                                "(SELECT BranchCode FROM tblBranch WHERE BranchID = @BranchID), " +
								"@TerminalNo, " +
								"@DateCreated, " +
								"@DenominationID, " +
								"@DenominationCount" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);			
				prmCashierName.Value = Details.CashierName;
				cmd.Parameters.Add(prmCashierName);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);			
				prmDateCreated.Value = Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateCreated);

				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int32);			
				prmDenominationID.Value = Details.DenominationID;
				cmd.Parameters.Add(prmDenominationID);

				MySqlParameter prmAmount = new MySqlParameter("@DenominationCount",MySqlDbType.Int32);			
				prmAmount.Value = Details.DenominationCount;
				cmd.Parameters.Add(prmAmount);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}

		public void Insert(CashCountDetails[] Details)
		{
			try 
			{
				if (Details.Length > 0)
				{
					MySqlConnection cn = GetConnection();
					Int64 CashierID = Details[0].CashierID;
					string TerminalNo =Details[0].TerminalNo;
                    int BranchID = Details[0].BranchID;
					decimal Amount = 0;

					foreach(CashCountDetails details in Details)
					{
						Insert(details);	
						Amount += details.DenominationAmount;
					}
					CashierReport clsCashierReport = new CashierReport(cn, mTransaction);
                    clsCashierReport.UpdateCashCount(BranchID, CashierID, TerminalNo, Amount);
				}
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblCashCount WHERE CashCountID IN (" + IDs + ");";
				  
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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}


		#endregion

		#region Delete

		public CashCountDetails Details(Int64 CashCountID)
		{
			try
			{
				string SQL=	"SELECT CashCountID, " +
								"CashierID, " +
								"CashierName, " +
                                "BranchID," +
                                "BranchCode," +
								"TerminalNo," +
								"DateCreated, " +
								"a.DenominationID, " +
								"DenominationCode, " +
								"DenominationValue, " +
								"ImagePath, " +
								"DenominationCount " +
							"FROM tblCashCount a " +
							"INNER JOIN tblDenomination b ON a.DenominationID = b.DenominationID " +
							"WHERE CashCountID = @CashCountID ";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashCountID = new MySqlParameter("@CashCountID",MySqlDbType.Int16);
				prmCashCountID.Value = CashCountID;
				cmd.Parameters.Add(prmCashCountID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				CashCountDetails Details = new CashCountDetails();

				while (myReader.Read()) 
				{
					Details.CashCountID = myReader.GetInt64("CashCountID");
					Details.CashierID = myReader.GetInt64("CashierID");
					Details.CashierName = "" + myReader["CashierName"].ToString();
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
					Details.DateCreated = myReader.GetDateTime("DateCreated");
					Details.DenominationID = myReader.GetInt32("DenominationID");
					Details.DenominationCode = "" + myReader["DenominationCode"].ToString();
					Details.DenominationValue = myReader.GetDecimal("DenominationValue");
					Details.ImagePath = "" + myReader["ImagePath"].ToString();
					Details.DenominationCount = myReader.GetInt32("DenominationCount");
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}


		#endregion

        public string SQLSelect()
        {
            string SQL = "SELECT CashCountID, " +
                                "CashierID, " +
                                "CashierName, " +
                                "BranchID," +
                                "BranchCode," +
                                "TerminalNo," +
                                "DateCreated, " +
                                "a.DenominationID, " +
                                "DenominationCode, " +
                                "DenominationValue, " +
                                "ImagePath, " +
                                "DenominationCount, " +
                                "DenominationAmount " +
                            "FROM tblCashCount a " +
                            "INNER JOIN tblDenomination b ON a.DenominationID = b.DenominationID ";

            return SQL;
        }

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
                CashCountDetails clsCashCountDetails = new CashCountDetails();

                MySqlDataReader myReader = List(clsCashCountDetails, string.Empty, SortOption.Ascending);

                return myReader;		
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}

		public MySqlDataReader List(Int64 CashierID, string TerminalNo, string SortField, SortOption SortOrder)
		{
			try
			{
                CashCountDetails clsCashCountDetails = new CashCountDetails();
                clsCashCountDetails.CashierID = CashierID;
                clsCashCountDetails.TerminalNo = TerminalNo;

				MySqlDataReader myReader = List(clsCashCountDetails, string.Empty, SortOption.Ascending);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
        public MySqlDataReader List(CashCountDetails clsSearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect();

                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND CashierID = @CashierID ";

                    MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                    prmCashierID.Value = clsSearchKey.CashierID;
                    cmd.Parameters.Add(prmCashierID);
                }
                if (clsSearchKey.TerminalNo != string.Empty && clsSearchKey.TerminalNo != null)
                {
                    SQL += "AND TerminalNo = @TerminalNo ";

                    MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                    prmTerminalNo.Value = clsSearchKey.TerminalNo;
                    cmd.Parameters.Add(prmTerminalNo);
                }
                if (clsSearchKey.CashierName != string.Empty && clsSearchKey.CashierName != null)
                {
                    SQL += "AND CashierName = @CashierName ";

                    MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
                    prmCashierName.Value = clsSearchKey.CashierName;
                    cmd.Parameters.Add(prmCashierName);
                }
                if (clsSearchKey.DenominationCode != string.Empty && clsSearchKey.DenominationCode != null)
                {
                    SQL += "AND DenominationCode = @DenominationCode ";

                    MySqlParameter prmDenominationCode = new MySqlParameter("@DenominationCode",MySqlDbType.String);
                    prmDenominationCode.Value = clsSearchKey.DenominationCode;
                    cmd.Parameters.Add(prmDenominationCode);
                }
                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField;

                    if (SortOrder == SortOption.Ascending)
                        SQL += " ASC";
                    else
                        SQL += " DESC";
                }

                MySqlConnection cn = GetConnection();

                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                    mTransaction.Rollback();

                mTransaction.Dispose();
                mConnection.Close();
                mConnection.Dispose();

                throw ex;
            }
        }

        public System.Data.DataTable ListAsDataTable(CashCountDetails clsSearchKey, string SortField, SortOption SortOrder)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect();

                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND CashierID = @CashierID ";

                    MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                    prmCashierID.Value = clsSearchKey.CashierID;
                    cmd.Parameters.Add(prmCashierID);
                }
                if (clsSearchKey.TerminalNo != string.Empty && clsSearchKey.TerminalNo != null)
                {
                    SQL += "AND TerminalNo = @TerminalNo ";

                    MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                    prmTerminalNo.Value = clsSearchKey.TerminalNo;
                    cmd.Parameters.Add(prmTerminalNo);
                }
                if (clsSearchKey.CashierName != string.Empty && clsSearchKey.CashierName != null)
                {
                    SQL += "AND CashierName = @CashierName ";

                    MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
                    prmCashierName.Value = clsSearchKey.CashierName;
                    cmd.Parameters.Add(prmCashierName);
                }
                if (clsSearchKey.DenominationCode != string.Empty && clsSearchKey.DenominationCode != null)
                {
                    SQL += "AND DenominationCode = @DenominationCode ";

                    MySqlParameter prmDenominationCode = new MySqlParameter("@DenominationCode",MySqlDbType.String);
                    prmDenominationCode.Value = clsSearchKey.DenominationCode;
                    cmd.Parameters.Add(prmDenominationCode);
                }
                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField;

                    if (SortOrder == SortOption.Ascending)
                        SQL += " ASC";
                    else
                        SQL += " DESC";
                }

                MySqlConnection cn = GetConnection();

                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblCashCount");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

		#endregion
	}
}

