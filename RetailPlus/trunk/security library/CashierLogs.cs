using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: March 29, 2005
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/


namespace AceSoft.RetailPlus.Security
{

	public enum CashierLogStatus 
	{
		LoggedIn			=	0,
		LoggedOut			=	1,
		LoggedOutBySystem	=	3
	}


	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct CashierLogsDetails
	{
        public Int32 BranchID;
        public string TerminalNo;
        public Int64 SyncID;
		public Int64 CashierLogsID;
		public Int64 CashierID;
		public DateTime LoginDate;
        public string BranchCode;
		public string IPAddress;
		public DateTime LogoutDate;
		public CashierLogStatus Status;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}


	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CashierLogs : POSConnection
	{
		#region Constructors and Destructors

		public CashierLogs()
            : base(null, null)
        {
        }

        public CashierLogs(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}
		
		#endregion

		#region Insert and Update

		public Int64 UpdateBeginningBalance(CashierLogsDetails Details, decimal BeginningBalanceAmount)
		{
			Int64 iRetValue = Insert(Details);

			Data.CashierReports clsCashierReport = new Data.CashierReports(base.Connection, base.Transaction);
            clsCashierReport.UpdateBeginningBalance(Details.BranchID, Details.TerminalNo, Details.CashierID, BeginningBalanceAmount);

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport(base.Connection, base.Transaction);
            clsTerminalReport.UpdateBeginningBalance(Details.BranchID, Details.TerminalNo, BeginningBalanceAmount);

			return iRetValue;
		}
		public Int64 Insert(CashierLogsDetails Details)
		{
			try 
			{
                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(CashierLogsDetails Details)
		{
			try 
			{
                Save(Details);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(CashierLogsDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveCashierLogs(@BranchID, @TerminalNo, @SyncID, @CashierLogsID, @UID, @LoginDate, @IPAddress, @LogoutDate, @Status, @BranchCode, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("CashierLogsID", Details.CashierLogsID);
                cmd.Parameters.AddWithValue("UID", Details.CashierID);
                cmd.Parameters.AddWithValue("LoginDate", Details.LoginDate);
                cmd.Parameters.AddWithValue("IPAddress", Details.IPAddress);
                cmd.Parameters.AddWithValue("LogoutDate", Details.LogoutDate);
                cmd.Parameters.AddWithValue("Status", Details.Status);
                cmd.Parameters.AddWithValue("BranchCode", Details.BranchCode);
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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"UPDATE tblCashierLogs SET " +
					"Deleted = '1' " +
					"WHERE CashierLogsID IN (" + IDs + ");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "BranchID, " +
                                "TerminalNo, " +
                                "SyncID, " +
                                "UID, " +
					            "LoginDate, " +
                                "BranchCode, " +
					            "IPAddress, " +
					            "LogoutDate, " +
					            "Status, " +
                                "CreatedOn, " +
                                "LastModified " +
					        "FROM tblCashierLogs ";
            return stSQL;
        }

		#region Details

		public CashierLogsDetails Details(Int64 CashierLogsID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE CashierLogsID = @CashierLogsID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashierLogsID = new MySqlParameter("@CashierLogsID",MySqlDbType.Int64);			
				prmCashierLogsID.Value = CashierLogsID;
				cmd.Parameters.Add(prmCashierLogsID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				CashierLogsDetails Details = new CashierLogsDetails();

				while (myReader.Read()) 
				{
					Details.CashierLogsID = CashierLogsID;
					Details.CashierID = myReader.GetInt64("CashierID");
					Details.LoginDate = myReader.GetDateTime("LoginDate");
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
					Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
					Details.IPAddress = "" + myReader["IPAddress"].ToString();
					Details.LogoutDate = myReader.GetDateTime("LogoutDate");
                    Details.Status = (CashierLogStatus)Enum.Parse(typeof(CashierLogStatus), myReader.GetString("Status"));
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTable(Int64 CashierID = 0, string SearchKey = "", string SortField = "BranchCode", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE 1=1 ";

            if (CashierID !=0 )
            {
                SQL += "AND CashierID = @CasheirID ";
                cmd.Parameters.AddWithValue("@CasheirID", CashierID);
            }
            if (!string.IsNullOrEmpty(SearchKey))
            {
                SQL += "AND (TerminalNo LIKE @SearchKey or BranchCode LIKE @SearchKey) ";
                cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
            }

            SQL += "ORDER BY " + SortField + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable LoginLogoutReport(DateTime LoginDateFrom, DateTime LoginDateTo, string User = "")
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL ="SELECT Name 'User', LoginDate, " +
					"IF(LogoutDate <> '0001-01-01 00:00:00', LogoutDate, LoginDate) 'LogoutDate' " +
					"FROM tblCashierLogs a INNER JOIN sysAccessUserDetails b ON a.UID = b.UID " +
					"WHERE 1=1 ";

                if (LoginDateFrom != DateTime.MinValue)
                {
                    SQL += " AND LoginDate >= @LoginDateFrom ";
                    cmd.Parameters.AddWithValue("LoginDateFrom", LoginDateFrom);
                }
                if (LoginDateTo != DateTime.MinValue)
                {
                    SQL += " AND LoginDate <= @LoginDateTo ";
                    cmd.Parameters.AddWithValue("LoginDateTo", LoginDateTo);
                }
                if (!string.IsNullOrEmpty(User))
                {
                    SQL += " AND Name = @User ";
                    cmd.Parameters.AddWithValue("User", User);
                }
				SQL += "GROUP BY Name, LoginDate ";		
				SQL += "ORDER BY Name, LoginDate ASC ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}			

		#endregion

		#region Methods: Logout, Status

		public void Logout(CashierLogsDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCashierLogs SET " + 
					            "LogoutDate		=	@LogoutDate, " +
					            "Status			=	@Status " + 
					        "WHERE UID		=	@UID " +
					            "AND LoginDate		=	@LoginDate " +
                                "AND BranchID		=	@BranchID " +
					            "AND TerminalNo		=	@TerminalNo " +
					            "AND IPAddress		=	@IPAddress; ";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("UID", Details.CashierID);
                cmd.Parameters.AddWithValue("LoginDate", Details.LoginDate);
                cmd.Parameters.AddWithValue("IPAddress", Details.IPAddress);
                cmd.Parameters.AddWithValue("LogoutDate", Details.LogoutDate);
                cmd.Parameters.AddWithValue("Status", CashierLogStatus.LoggedOut.ToString("d"));

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Logout(Int64 CashierLogsID)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL	= "UPDATE tblCashierLogs SET " + 
					            "LogoutDate			=	@LogoutDate, " +
					            "Status				=	@Status " + 
					        "WHERE CashierLogsID	=	@CashierLogsID ";
	 			
                cmd.Parameters.AddWithValue("CashierLogsID", CashierLogsID);
                cmd.Parameters.AddWithValue("LogoutDate", DateTime.Now);
                cmd.Parameters.AddWithValue("Status", CashierLogStatus.LoggedOut.ToString("d"));

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public CashierLogStatus Status(Int64 CashierID, DateTime LoginDate, int BranchID, string TerminalNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
					            "Status " +
					        "FROM tblCashierLogs " +
					        "WHERE UID = @UID " + 
					        "AND CAST(LoginDate AS DATE) = CAST(@LoginDate AS DATE) " +
                            "AND BranchID = @BranchID " +
                            "AND TerminalNo = @TerminalNo " +
					        "ORDER BY LoginDate DESC; ";
	 			
                cmd.Parameters.AddWithValue("UID", CashierID);
                cmd.Parameters.AddWithValue("LoginDate", LoginDate);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CashierLogStatus status = CashierLogStatus.LoggedOut;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    status = (CashierLogStatus) Enum.Parse(typeof(CashierLogStatus), dr["Status"].ToString());
                }

				return status;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public CashierLogStatus LastStatus(Int64 CashierID, out long CashierLogsID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text; 

				string SQL=	"SELECT Status, CashierLogsID FROM tblCashierLogs WHERE UID = @UID ORDER BY LoginDate DESC LIMIT 1; ";

                cmd.Parameters.AddWithValue("UID", CashierID);

				cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                CashierLogStatus status = CashierLogStatus.LoggedOut;
                CashierLogsID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    status = (CashierLogStatus)Enum.Parse(typeof(CashierLogStatus), dr["Status"].ToString());
                    CashierLogsID = Int64.Parse(dr["CashierLogsID"].ToString());
                }

				return status;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion
	}
}