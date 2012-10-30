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

	#region ERPConfigDetails

	public struct ERPConfigDetails
	{
		public string LastPONo;
		public DateTime PostingDateFrom;
		public DateTime PostingDateTo;
	}

	#endregion

    #region APLinkConfigDetails

    public struct APLinkConfigDetails
    {
        public int ChartOfAccountIDAPTracking;
        public int ChartOfAccountIDAPBills;
        public int ChartOfAccountIDAPFreight;
        public int ChartOfAccountIDAPVDeposit;
        public int ChartOfAccountIDAPContra;
        public int ChartOfAccountIDAPLatePayment;
    }

    public struct ARLinkConfigDetails
    {
        public int ChartOfAccountIDARTracking;
        public int ChartOfAccountIDARBills;
        public int ChartOfAccountIDARFreight;
        public int ChartOfAccountIDARVDeposit;
        public int ChartOfAccountIDARContra;
        public int ChartOfAccountIDARLatePayment;
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
	public class ERPConfig
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

		public ERPConfig()
		{
			
		}

		public ERPConfig(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public void UpdatePostingDate(DateTime PostingDateFrom, DateTime PostingDateTo)
		{
			try 
			{
				string SQL=	"UPDATE tblERPConfig SET " + 
								"PostingDateFrom		=	@PostingDateFrom, " +
								"PostingDateTo			=	@PostingDateTo;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

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

        public void UpdateAPLinkConfig(APLinkConfigDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblERPConfig SET " +
                                "ChartOfAccountIDAPTracking     = @ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills        = @ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight      = @ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit     = @ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra       = @ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment  = @ChartOfAccountIDAPLatePayment;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChartOfAccountIDAPTracking = new MySqlParameter("@ChartOfAccountIDAPTracking",MySqlDbType.Int32);
                prmChartOfAccountIDAPTracking.Value = Details.ChartOfAccountIDAPTracking;
                cmd.Parameters.Add(prmChartOfAccountIDAPTracking);

                MySqlParameter prmChartOfAccountIDAPBills = new MySqlParameter("@ChartOfAccountIDAPBills",MySqlDbType.Int32);
                prmChartOfAccountIDAPBills.Value = Details.ChartOfAccountIDAPBills;
                cmd.Parameters.Add(prmChartOfAccountIDAPBills);

                MySqlParameter prmChartOfAccountIDAPFreight = new MySqlParameter("@ChartOfAccountIDAPFreight",MySqlDbType.Int32);
                prmChartOfAccountIDAPFreight.Value = Details.ChartOfAccountIDAPFreight;
                cmd.Parameters.Add(prmChartOfAccountIDAPFreight);

                MySqlParameter prmChartOfAccountIDAPVDeposit = new MySqlParameter("@ChartOfAccountIDAPVDeposit",MySqlDbType.Int32);
                prmChartOfAccountIDAPVDeposit.Value = Details.ChartOfAccountIDAPVDeposit;
                cmd.Parameters.Add(prmChartOfAccountIDAPVDeposit);

                MySqlParameter prmChartOfAccountIDAPContra = new MySqlParameter("@ChartOfAccountIDAPContra",MySqlDbType.Int32);
                prmChartOfAccountIDAPContra.Value = Details.ChartOfAccountIDAPContra;
                cmd.Parameters.Add(prmChartOfAccountIDAPContra);

                MySqlParameter prmChartOfAccountIDAPLatePayment = new MySqlParameter("@ChartOfAccountIDAPLatePayment",MySqlDbType.Int32);
                prmChartOfAccountIDAPLatePayment.Value = Details.ChartOfAccountIDAPLatePayment;
                cmd.Parameters.Add(prmChartOfAccountIDAPLatePayment);

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

		#region Details

		public ERPConfigDetails Details()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastPONo, " +
								"PostingDateFrom, " +
								"PostingDateTo " +
							"FROM tblERPConfig;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ERPConfigDetails Details = new ERPConfigDetails();

				while (myReader.Read()) 
				{
					Details.LastPONo = "" + myReader["LastPONo"].ToString();
					Details.PostingDateFrom = myReader.GetDateTime("PostingDateFrom");
					Details.PostingDateTo = myReader.GetDateTime("PostingDateTo");
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
        public APLinkConfigDetails APLinkDetails()
        {
            try
            {
                string SQL = "SELECT " +
                                "ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment " +
                            "FROM tblERPConfig;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                APLinkConfigDetails Details = new APLinkConfigDetails();

                while (myReader.Read())
                {
                    Details.ChartOfAccountIDAPTracking = myReader.GetInt32("ChartOfAccountIDAPTracking");
                    Details.ChartOfAccountIDAPBills = myReader.GetInt32("ChartOfAccountIDAPBills");
                    Details.ChartOfAccountIDAPFreight = myReader.GetInt32("ChartOfAccountIDAPFreight");
                    Details.ChartOfAccountIDAPVDeposit = myReader.GetInt32("ChartOfAccountIDAPVDeposit");
                    Details.ChartOfAccountIDAPContra = myReader.GetInt32("ChartOfAccountIDAPContra");
                    Details.ChartOfAccountIDAPLatePayment = myReader.GetInt32("ChartOfAccountIDAPLatePayment");
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
        public ARLinkConfigDetails ARLinkDetails()
        {
            try
            {
                string SQL = "SELECT " +
                                "ChartOfAccountIDARTracking, " +
                                "ChartOfAccountIDARBills, " +
                                "ChartOfAccountIDARFreight, " +
                                "ChartOfAccountIDARVDeposit, " +
                                "ChartOfAccountIDARContra, " +
                                "ChartOfAccountIDARLatePayment " +
                            "FROM tblERPConfig;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ARLinkConfigDetails Details = new ARLinkConfigDetails();

                while (myReader.Read())
                {
                    Details.ChartOfAccountIDARTracking = myReader.GetInt32("ChartOfAccountIDARTracking");
                    Details.ChartOfAccountIDARBills = myReader.GetInt32("ChartOfAccountIDARBills");
                    Details.ChartOfAccountIDARFreight = myReader.GetInt32("ChartOfAccountIDARFreight");
                    Details.ChartOfAccountIDARVDeposit = myReader.GetInt32("ChartOfAccountIDARVDeposit");
                    Details.ChartOfAccountIDARContra = myReader.GetInt32("ChartOfAccountIDARContra");
                    Details.ChartOfAccountIDARLatePayment = myReader.GetInt32("ChartOfAccountIDARLatePayment");
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

		#region Streams: DataList, List, Search

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblPO");

			dt.Columns.Add("LastPONo");
			dt.Columns.Add("PostingDateFrom");
			dt.Columns.Add("PostingDateTo");
			
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["LastPONo"] = "" + myReader["LastPONo"].ToString();
				dr["PostingDateFrom"] = myReader.GetDateTime("PostingDateFrom");
				dr["PostingDateTo"] = myReader.GetDateTime("PostingDateTo");
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}
		
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"LastPONo, " +
								"PostingDateFrom, " +
								"PostingDateTo " +
							"FROM tblERPConfig " +
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
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"LastPONo, " +
								"PostingDateFrom, " +
								"PostingDateTo " +
							"FROM tblERPConfig " +
							"WHERE LastPONo LIKE @SearchKey " +
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

		
		#endregion

		#region Public Modifiers: get_LastPONo, get_LastPOReturnNo, get_LastDebitMemoNo

		public string get_LastPONo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastPONo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastPONo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastPONo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastPONo = LastPONo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastPONo = @LastPONo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastPONo = new MySqlParameter("@LastPONo",MySqlDbType.String);
				prmLastPONo.Value = LastPONo;
				cmd.Parameters.Add(prmLastPONo);

				cmd.ExecuteNonQuery();

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

		public string get_LastPOReturnNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastPOReturnNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastPOReturnNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastPOReturnNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastPOReturnNo = LastPOReturnNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastPOReturnNo = @LastPOReturnNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastPOReturnNo = new MySqlParameter("@LastPOReturnNo",MySqlDbType.String);
				prmLastPOReturnNo.Value = LastPOReturnNo;
				cmd.Parameters.Add(prmLastPOReturnNo);

				cmd.ExecuteNonQuery();

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

		public string get_LastDebitMemoNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastDebitMemoNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastDebitMemoNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastDebitMemoNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastDebitMemoNo = LastDebitMemoNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastDebitMemoNo = @LastDebitMemoNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastDebitMemoNo = new MySqlParameter("@LastDebitMemoNo",MySqlDbType.String);
				prmLastDebitMemoNo.Value = LastDebitMemoNo;
				cmd.Parameters.Add(prmLastDebitMemoNo);

				cmd.ExecuteNonQuery();

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

        public string get_LastBranchTransferNo()
        {
            try
            {
                string SQL = "SELECT " +
                                "LastBranchTransferNo " +
                            "FROM tblERPConfig";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = String.Empty;
                int iLen = 10;

                while (myReader.Read())
                {
                    if (myReader.GetString(0) != null && myReader.GetString(0) != "")
                    {
                        stRetValue = "" + myReader["LastBranchTransferNo"].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                myReader.Close();

                if (stRetValue == String.Empty)
                    throw new NullReferenceException();

                string LastBranchTransferNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                LastBranchTransferNo = LastBranchTransferNo.PadLeft(iLen, '0');

                SQL = "UPDATE tblERPConfig SET LastBranchTransferNo = @LastBranchTransferNo;";

                cmd.CommandText = SQL;

                MySqlParameter prmLastBranchTransferNo = new MySqlParameter("@LastBranchTransferNo",MySqlDbType.String);
                prmLastBranchTransferNo.Value = LastBranchTransferNo;
                cmd.Parameters.Add(prmLastBranchTransferNo);

                cmd.ExecuteNonQuery();

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

		#region get_LastSONo, get_LastSOReturnNo, get_LastCreditMemoNo

		public string get_LastSONo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastSONo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastSONo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastSONo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastSONo = LastSONo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastSONo = @LastSONo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastSONo = new MySqlParameter("@LastSONo",MySqlDbType.String);
				prmLastSONo.Value = LastSONo;
				cmd.Parameters.Add(prmLastSONo);

				cmd.ExecuteNonQuery();

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

		public string get_LastSOReturnNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastSOReturnNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastSOReturnNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastSOReturnNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastSOReturnNo = LastSOReturnNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastSOReturnNo = @LastSOReturnNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastSOReturnNo = new MySqlParameter("@LastSOReturnNo",MySqlDbType.String);
				prmLastSOReturnNo.Value = LastSOReturnNo;
				cmd.Parameters.Add(prmLastSOReturnNo);

				cmd.ExecuteNonQuery();

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

		public string get_LastCreditMemoNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastCreditMemoNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastCreditMemoNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastCreditMemoNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastCreditMemoNo = LastCreditMemoNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastCreditMemoNo = @LastCreditMemoNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastCreditMemoNo = new MySqlParameter("@LastCreditMemoNo",MySqlDbType.String);
				prmLastCreditMemoNo.Value = LastCreditMemoNo;
				cmd.Parameters.Add(prmLastCreditMemoNo);

				cmd.ExecuteNonQuery();

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

		#region get_LastTransferInNo, get_LastTransferOutNo
			
		public string get_LastTransferInNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastTransferInNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastTransferInNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastTransferInNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastTransferInNo = LastTransferInNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastTransferInNo = @LastTransferInNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastTransferInNo = new MySqlParameter("@LastTransferInNo",MySqlDbType.String);
				prmLastTransferInNo.Value = LastTransferInNo;
				cmd.Parameters.Add(prmLastTransferInNo);

				cmd.ExecuteNonQuery();

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

		public string get_LastTransferOutNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastTransferOutNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastTransferOutNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastTransferOutNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastTransferOutNo = LastTransferOutNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastTransferOutNo = @LastTransferOutNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastTransferOutNo = new MySqlParameter("@LastTransferOutNo",MySqlDbType.String);
				prmLastTransferOutNo.Value = LastTransferOutNo;
				cmd.Parameters.Add(prmLastTransferOutNo);

				cmd.ExecuteNonQuery();

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

		#region get_LastInvAdjustmentNo, get_LastClosingNo

		public string get_LastInvAdjustmentNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastInvAdjustmentNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastInvAdjustmentNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastInvAdjustmentNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastInvAdjustmentNo = LastInvAdjustmentNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastInvAdjustmentNo = @LastInvAdjustmentNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastInvAdjustmentNo = new MySqlParameter("@LastInvAdjustmentNo",MySqlDbType.String);
				prmLastInvAdjustmentNo.Value = LastInvAdjustmentNo;
				cmd.Parameters.Add(prmLastInvAdjustmentNo);

				cmd.ExecuteNonQuery();

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
		public string get_LastClosingNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"LastClosingNo " +
							"FROM tblERPConfig";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				int iLen = 10;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["LastClosingNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string LastClosingNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				LastClosingNo = LastClosingNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblERPConfig SET LastClosingNo = @LastClosingNo;";

				cmd.CommandText = SQL;

				MySqlParameter prmLastClosingNo = new MySqlParameter("@LastClosingNo",MySqlDbType.String);
				prmLastClosingNo.Value = LastClosingNo;
				cmd.Parameters.Add(prmLastClosingNo);

				cmd.ExecuteNonQuery();

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

        #region get_LastCreditCardNo, get_LastRewardCardNo

        public string get_LastCreditCardNo()
        {
            try
            {
                string SQL = "SELECT " +
                                "LastCreditCardNo " +
                            "FROM tblERPConfig";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = String.Empty;
                int iLen = 10;

                while (myReader.Read())
                {
                    if (myReader.GetString(0) != null && myReader.GetString(0) != "")
                    {
                        stRetValue = "" + myReader["LastCreditCardNo"].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                myReader.Close();

                if (stRetValue == String.Empty)
                    throw new NullReferenceException();

                string strLastCreditCardNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                strLastCreditCardNo = strLastCreditCardNo.PadLeft(iLen, '0');

                SQL = "UPDATE tblERPConfig SET LastCreditCardNo = @LastCreditCardNo;";

                cmd.CommandText = SQL;

                MySqlParameter prmLastCreditCardNo = new MySqlParameter("@LastCreditCardNo",MySqlDbType.String);
                prmLastCreditCardNo.Value = strLastCreditCardNo;
                cmd.Parameters.Add(prmLastCreditCardNo);

                cmd.ExecuteNonQuery();

                stRetValue = DateTime.Now.ToString("yyyy") + stRetValue;

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
        public string get_LastRewardCardNo()
        {
            try
            {
                string SQL = "SELECT " +
                                "LastRewardCardNo " +
                            "FROM tblERPConfig";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = String.Empty;
                int iLen = 10;

                while (myReader.Read())
                {
                    if (myReader.GetString(0) != null && myReader.GetString(0) != "")
                    {
                        stRetValue = "" + myReader["LastRewardCardNo"].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                myReader.Close();

                if (stRetValue == String.Empty)
                    throw new NullReferenceException();

                string strLastRewardCardNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                strLastRewardCardNo = strLastRewardCardNo.PadLeft(iLen, '0');

                SQL = "UPDATE tblERPConfig SET LastRewardCardNo = @LastRewardCardNo;";

                cmd.CommandText = SQL;

                MySqlParameter prmLastRewardCardNo = new MySqlParameter("@LastRewardCardNo",MySqlDbType.String);
                prmLastRewardCardNo.Value = strLastRewardCardNo;
                cmd.Parameters.Add(prmLastRewardCardNo);

                cmd.ExecuteNonQuery();

                stRetValue = DateTime.Now.ToString("yyyy") + stRetValue;

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

