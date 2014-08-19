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
        public Int64 ERPConfigID;
		public string LastPONo;
        public string LastPOReturnNo;
        public string LastDebitMemoNo;
        public string LastSONo;
        public string LastSOReturnNo;
        public string LastCreditMemoNo;
        public string LastTransferInNo;
        public string LastTransferOutNo;
        public string LastInvAdjustmentNo;
        public string LastClosingNo;
        public DateTime PostingDateFrom;
        public DateTime PostingDateTo;
        public APLinkConfigDetails APLinkConfigDetails;
        public ARLinkConfigDetails ARLinkConfigDetails;
        public string LastCreditCardNo;
        public string LastRewardCardNo;
        public string DBVersion;
        public string DBVersionSales;
        public string LastBranchTransferNo;
        public string LastCustomerCode;        

        public DateTime CreatedOn;
        public DateTime LastModified;
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

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
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
    public class ERPConfig : POSConnection
	{
		#region Constructors and Destructors

		public ERPConfig()
            : base(null, null)
        {
        }

        public ERPConfig(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

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
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
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

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(ERPConfigDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveERPConfig(@ERPConfigID, @LastPONo, @LastPOReturnNo, @LastDebitMemoNo, @LastSONo, " +
                                "@LastSOReturnNo, @LastCreditMemoNo, @LastTransferInNo, @LastTransferOutNo, " +
                                "@LastInvAdjustmentNo, @LastClosingNo, @PostingDateFrom, @PostingDateTo, " +
                                "@ChartOfAccountIDAPTracking, @ChartOfAccountIDAPBills, @ChartOfAccountIDAPFreight, " +
                                "@ChartOfAccountIDAPVDeposit, @ChartOfAccountIDAPContra, @ChartOfAccountIDAPLatePayment, " +
                                "@ChartOfAccountIDARTracking, @ChartOfAccountIDARBills, @ChartOfAccountIDARFreight, " +
                                "@ChartOfAccountIDARVDeposit, @ChartOfAccountIDARContra, @ChartOfAccountIDARLatePayment, " +
                                "@LastCreditCardNo, @LastRewardCardNo, @DBVersion, @DBVersionSales, @LastBranchTransferNo,  " +
								"@LastCustomerCode, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ERPConfigID", Details.ERPConfigID);
                cmd.Parameters.AddWithValue("LastPONo", Details.LastPONo);
                cmd.Parameters.AddWithValue("LastPOReturnNo", Details.LastPOReturnNo);
                cmd.Parameters.AddWithValue("LastDebitMemoNo", Details.LastDebitMemoNo);
                cmd.Parameters.AddWithValue("LastSONo", Details.LastSONo);
                cmd.Parameters.AddWithValue("LastSOReturnNo", Details.LastSOReturnNo);
                cmd.Parameters.AddWithValue("LastCreditMemoNo", Details.LastCreditMemoNo);
                cmd.Parameters.AddWithValue("LastTransferInNo", Details.LastTransferInNo);
                cmd.Parameters.AddWithValue("LastTransferOutNo", Details.LastTransferOutNo);
                cmd.Parameters.AddWithValue("LastInvAdjustmentNo", Details.LastInvAdjustmentNo);
                cmd.Parameters.AddWithValue("LastClosingNo", Details.LastClosingNo);
                cmd.Parameters.AddWithValue("PostingDateFrom", Details.PostingDateFrom);
                cmd.Parameters.AddWithValue("PostingDateTo", Details.PostingDateTo);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPTracking", Details.APLinkConfigDetails.ChartOfAccountIDAPTracking);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPBills", Details.APLinkConfigDetails.ChartOfAccountIDAPBills);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPFreight", Details.APLinkConfigDetails.ChartOfAccountIDAPFreight);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPVDeposit", Details.APLinkConfigDetails.ChartOfAccountIDAPVDeposit);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPContra", Details.APLinkConfigDetails.ChartOfAccountIDAPContra);
                cmd.Parameters.AddWithValue("ChartOfAccountIDAPLatePayment", Details.APLinkConfigDetails.ChartOfAccountIDAPLatePayment);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARTracking", Details.ARLinkConfigDetails.ChartOfAccountIDARTracking);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARBills", Details.ARLinkConfigDetails.ChartOfAccountIDARBills);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARFreight", Details.ARLinkConfigDetails.ChartOfAccountIDARFreight);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARVDeposit", Details.ARLinkConfigDetails.ChartOfAccountIDARVDeposit);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARContra", Details.ARLinkConfigDetails.ChartOfAccountIDARContra);
                cmd.Parameters.AddWithValue("ChartOfAccountIDARLatePayment", Details.ARLinkConfigDetails.ChartOfAccountIDARLatePayment);
                cmd.Parameters.AddWithValue("LastCreditCardNo", Details.LastCreditCardNo);
                cmd.Parameters.AddWithValue("LastRewardCardNo", Details.LastRewardCardNo);
                cmd.Parameters.AddWithValue("DBVersion", Details.DBVersion);
                cmd.Parameters.AddWithValue("DBVersionSales", Details.DBVersionSales);
                cmd.Parameters.AddWithValue("LastBranchTransferNo", Details.LastBranchTransferNo);
                cmd.Parameters.AddWithValue("LastCustomerCode", Details.LastCustomerCode);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
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
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				ERPConfigDetails Details = new ERPConfigDetails();

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					Details.LastPONo = "" + dr["LastPONo"].ToString();
					Details.PostingDateFrom = DateTime.Parse(dr["PostingDateFrom"].ToString());
					Details.PostingDateTo = DateTime.Parse(dr["PostingDateTo"].ToString());
				}

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
                APLinkConfigDetails Details = new APLinkConfigDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ChartOfAccountIDAPTracking = Int32.Parse(dr["ChartOfAccountIDAPTracking"].ToString());
                    Details.ChartOfAccountIDAPBills = Int32.Parse(dr["ChartOfAccountIDAPBills"].ToString());
                    Details.ChartOfAccountIDAPFreight = Int32.Parse(dr["ChartOfAccountIDAPFreight"].ToString());
                    Details.ChartOfAccountIDAPVDeposit = Int32.Parse(dr["ChartOfAccountIDAPVDeposit"].ToString());
                    Details.ChartOfAccountIDAPContra = Int32.Parse(dr["ChartOfAccountIDAPContra"].ToString());
                    Details.ChartOfAccountIDAPLatePayment = Int32.Parse(dr["ChartOfAccountIDAPLatePayment"].ToString());
                }

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
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

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ARLinkConfigDetails Details = new ARLinkConfigDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ChartOfAccountIDARTracking = Int32.Parse(dr["ChartOfAccountIDARTracking"].ToString());
                    Details.ChartOfAccountIDARBills = Int32.Parse(dr["ChartOfAccountIDARBills"].ToString());
                    Details.ChartOfAccountIDARFreight = Int32.Parse(dr["ChartOfAccountIDARFreight"].ToString());
                    Details.ChartOfAccountIDARVDeposit = Int32.Parse(dr["ChartOfAccountIDARVDeposit"].ToString());
                    Details.ChartOfAccountIDARContra = Int32.Parse(dr["ChartOfAccountIDARContra"].ToString());
                    Details.ChartOfAccountIDARLatePayment = Int32.Parse(dr["ChartOfAccountIDARLatePayment"].ToString());
                }


                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Streams: DataList, List, Search

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
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

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

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

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		

		
		#endregion

		#region Public Modifiers: get_LastPONo, get_LastPOReturnNo, get_LastDebitMemoNo

		public string get_LastPONo()
		{
			try
			{
                return getNewTransactionNo(LastPONo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public string get_LastPOReturnNo()
		{
			try
			{
                return getNewTransactionNo(LastPOReturnNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public string get_LastDebitMemoNo()
		{
			try
			{
                return getNewTransactionNo(LastDebitMemoNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public string get_LastBranchTransferNo()
        {
            try
			{
                return getNewTransactionNo(LastBranchTransferNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
        }

		#endregion

		#region get_LastSONo, get_LastSOReturnNo, get_LastCreditMemoNo

        public string get_LastSONo()
		{
			try
			{
                return getNewTransactionNo(LastSONo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public string get_LastSOReturnNo()
		{
			try
			{
                return getNewTransactionNo(LastSOReturnNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public string get_LastCreditMemoNo()
		{
			try
			{
                return getNewTransactionNo(LastCreditMemoNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		#endregion

		#region get_LastTransferInNo, get_LastTransferOutNo
        
		public string get_LastTransferInNo()
		{
			try
			{
                return getNewTransactionNo(LastTransferInNo);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public string get_LastTransferOutNo()
		{
            try
            {
                return getNewTransactionNo(LastTransferOutNo);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}


		#endregion

		#region get_LastInvAdjustmentNo, get_LastClosingNo

		public string get_LastInvAdjustmentNo()
		{
            try
            {
                return getNewTransactionNo(LastInvAdjustmentNo);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}
		public string get_LastClosingNo()
		{
            try
            {
                return getNewTransactionNo(LastClosingNo);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}

		#endregion

        #region get_LastCreditCardNo, get_LastRewardCardNo

        public string get_LastCreditCardNo()
        {
            try
            {
                return DateTime.Now.ToString("yyyy") + getNewTransactionNo(LastCreditCardNo);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public string get_LastRewardCardNo()
        {
            try
            {
                return DateTime.Now.ToString("yyyy") + getNewTransactionNo(LastRewardCardNo);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public string get_LastCustomerCode()
        {
            try
            {
                return getNewTransactionNo(LastCustomerCode);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        private static string LastPONo = "LastPONo";
        private static string LastPOReturnNo = "LastPOReturnNo";
        private static string LastDebitMemoNo = "LastDebitMemoNo";
        private static string LastBranchTransferNo = "LastBranchTransferNo";
        private static string LastSONo = "LastSONo";
        private static string LastSOReturnNo = "LastSOReturnNo";
        private static string LastCreditMemoNo = "LastCreditMemoNo";
        private static string LastTransferInNo = "LastTransferInNo";
        private static string LastTransferOutNo = "LastTransferOutNo";
        private static string LastInvAdjustmentNo = "LastInvAdjustmentNo";
        private static string LastClosingNo = "LastClosingNo";
        private static string LastCreditCardNo = "LastCreditCardNo";
        private static string LastRewardCardNo = "LastRewardCardNo";

        private static string LastCustomerCode = "LastCustomerCode";

        private string getNewTransactionNo(string ColumnName)
        {
            try
            {
                string SQL = "SELECT " + ColumnName + " " +
                            "FROM tblERPConfig";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string stRetValue = String.Empty;
                int iLen = 10;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr[ColumnName].ToString() != null && dr[ColumnName].ToString() != "")
                    {
                        stRetValue = "" + dr[ColumnName].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                if (stRetValue == String.Empty)
                    throw new NullReferenceException();

                string ColumnValue = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                ColumnValue = ColumnValue.PadLeft(iLen, '0');

                SQL = "UPDATE tblERPConfig SET " + ColumnName + " = @" + ColumnName + ";";

                cmd.CommandText = SQL;

                MySqlParameter prmLastSONo = new MySqlParameter("@" + ColumnName, MySqlDbType.String);
                prmLastSONo.Value = ColumnValue;
                cmd.Parameters.Add(prmLastSONo);

                base.ExecuteNonQuery(cmd);

                return stRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
	}
}

