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
		public Int64 CashierLogsID;
		public Int64 CashierID;
		public DateTime LoginDate;
        public int BranchID;
        public string BranchCode;
		public string TerminalNo;
		public string IPAddress;
		public DateTime LogoutDate;
		public CashierLogStatus Status;
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

			Data.CashierReport clsCashierReport = new Data.CashierReport(base.Connection, base.Transaction);
            clsCashierReport.UpdateBeginningBalance(Details.BranchID, BeginningBalanceAmount, Details.CashierID);

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport(base.Connection, base.Transaction);
            clsTerminalReport.UpdateBeginningBalance(Details.BranchID, Details.TerminalNo, BeginningBalanceAmount);

			return iRetValue;
		}
		public Int64 Insert(CashierLogsDetails Details)
		{
			try 
			{
				string SQL		=	"INSERT INTO tblCashierLogs (" +
					"UID, LoginDate, BranchID, BranchCode, TerminalNo, IPAddress, " +
					"LogoutDate, Status " +
					") VALUES (" +
                    "@UID, @LoginDate, @BranchID, (SELECT BranchCode FROM tblBranch WHERE BranchID = @BranchID), @TerminalNo, @IPAddress, " +
					"@LogoutDate, @Status);";
					
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = Details.CashierID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmLoginDate = new MySqlParameter("@LoginDate",MySqlDbType.DateTime);
				prmLoginDate.Value = Details.LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLoginDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmIPAddress = new MySqlParameter("@IPAddress",MySqlDbType.String);
				prmIPAddress.Value = Details.IPAddress;
				cmd.Parameters.Add(prmIPAddress);

				MySqlParameter prmLogoutDate = new MySqlParameter("@LogoutDate",MySqlDbType.DateTime);
				prmLogoutDate.Value = Details.LogoutDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLogoutDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Byte);
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();
				
				return iID;

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
				string SQL			=	"UPDATE tblCashierLogs SET " + 
				        "UID				=	@UID, " +
				        "LoginDate			=	@LoginDate, " +
                        "BranchID			=	@BranchID, " +
				        "TerminalNo			=	@TerminalNo, " +
				        "IPAddress			=	@IPAddress, " +
				        "LogoutDate			=	@LogoutDate, " +
				        "Status				=	@Status " +  
				        "WHERE CashierLogsID	=	@CashierLogsID;";
					
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = Details.CashierID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmLoginDate = new MySqlParameter("@LoginDate",MySqlDbType.DateTime);
				prmLoginDate.Value = Details.LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLoginDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmIPAddress = new MySqlParameter("@IPAddress",MySqlDbType.String);
				prmIPAddress.Value = Details.IPAddress;
				cmd.Parameters.Add(prmIPAddress);

				MySqlParameter prmLogoutDate = new MySqlParameter("@LogoutDate",MySqlDbType.DateTime);
				prmLogoutDate.Value = Details.LogoutDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLogoutDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Byte);
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmCashierLogsID = new MySqlParameter("@CashierLogsID",MySqlDbType.Int64);
				prmCashierLogsID.Value = Details.CashierLogsID;
				cmd.Parameters.Add(prmCashierLogsID);

				base.ExecuteNonQuery(cmd);
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
					            "UID, " +
					            "LoginDate, " +
                                "BranchID, " +
                                "BranchCode, " +
					            "TerminalNo, " +
					            "IPAddress, " +
					            "LogoutDate, " +
					            "Status " +
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect();

				if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder == SortOption.Ascending)
                        SQL += "ASC ";
                    else
                        SQL += "DESC ";
                }

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(Int64 CashierID, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect();

                SQL += "WHERE 1=1 AND UID = @UID ";

				if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder == SortOption.Ascending)
                        SQL += "ASC ";
                    else
                        SQL += "DESC ";
                }

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = CashierID;
				cmd.Parameters.Add(prmUID);
				
				return base.ExecuteReader(cmd);			
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
                string SQL = SQLSelect() + "WHERE TerminalNo LIKE @SearchKey " +
                                                "OR IPAddress LIKE @SearchKey ";

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder == SortOption.Ascending)
                        SQL += "ASC ";
                    else
                        SQL += "DESC ";
                }

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
				
		public System.Data.DataTable LoginLogoutReport(DateTime LoginDateFrom, DateTime LoginDateTo, string User)
		{
			try
			{
				string SQL ="SELECT Name 'User', LoginDate, " +
					"IF(LogoutDate <> '0001-01-01 00:00:00', LogoutDate, LoginDate) 'LogoutDate' " +
					"FROM tblCashierLogs a INNER JOIN sysAccessUserDetails b ON a.UID = b.UID " +
					"WHERE 1=1 ";

				if (LoginDateFrom != DateTime.MinValue)
					SQL += " AND LoginDate >= '" + LoginDateFrom.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
				if (LoginDateTo != DateTime.MinValue)
					SQL += " AND LoginDate <= '" + LoginDateTo.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
				if (User != null && User != string.Empty)
					SQL += " AND Name = '" + User + "' ";

				SQL += "GROUP BY Name, LoginDate ";		
				SQL += "ORDER BY Name, LoginDate ASC ";

                System.Data.DataTable dt = new System.Data.DataTable(this.GetType().FullName);
                base.MySqlDataAdapterFill(SQL, dt);
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
				string SQL = "UPDATE tblCashierLogs SET " + 
					            "LogoutDate		=	@LogoutDate, " +
					            "Status			=	@Status " + 
					        "WHERE UID		=	@UID " +
					            "AND LoginDate		=	@LoginDate " +
                                "AND BranchID		=	@BranchID " +
					            "AND TerminalNo		=	@TerminalNo " +
					            "AND IPAddress		=	@IPAddress; ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = Details.CashierID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmLoginDate = new MySqlParameter("@LoginDate",MySqlDbType.DateTime);
				prmLoginDate.Value = Details.LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLoginDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmIPAddress = new MySqlParameter("@IPAddress",MySqlDbType.String);
				prmIPAddress.Value = Details.IPAddress;
				cmd.Parameters.Add(prmIPAddress);

				MySqlParameter prmLogoutDate = new MySqlParameter("@LogoutDate",MySqlDbType.DateTime);
				prmLogoutDate.Value = Details.LogoutDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLogoutDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Byte);
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

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
				string SQL	= "UPDATE tblCashierLogs SET " + 
					            "LogoutDate			=	@LogoutDate, " +
					            "Status				=	@Status " + 
					        "WHERE CashierLogsID	=	@CashierLogsID ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmCashierLogsID = new MySqlParameter("@CashierLogsID",MySqlDbType.Int64);
				prmCashierLogsID.Value = CashierLogsID;
				cmd.Parameters.Add(prmCashierLogsID);

				MySqlParameter prmLogoutDate = new MySqlParameter("@LogoutDate",MySqlDbType.DateTime);
				prmLogoutDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLogoutDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Byte);
				prmStatus.Value = CashierLogStatus.LoggedOut.ToString("d");
				cmd.Parameters.Add(prmStatus);
				
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
				string SQL=	"SELECT " +
					            "Status " +
					        "FROM tblCashierLogs " +
					        "WHERE UID = @UID " + 
					        "AND CAST(LoginDate AS DATE) = CAST(@LoginDate AS DATE) " +
                            "AND BranchID = @BranchID " +
                            "AND TerminalNo = @TerminalNo " +
					        "ORDER BY LoginDate DESC; ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = CashierID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmLoginDate = new MySqlParameter("@LoginDate",MySqlDbType.DateTime);
				prmLoginDate.Value = LoginDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLoginDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				CashierLogStatus status = CashierLogStatus.LoggedOut;

				while (myReader.Read()) 
				{
					status = (CashierLogStatus) Enum.Parse(typeof (CashierLogStatus), myReader.GetByte(0).ToString());
				}

				myReader.Close();

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
				CashierLogsID = 0;

				string SQL=	"SELECT " +
					"Status, " +
					"CashierLogsID " +
					"FROM tblCashierLogs " +
					"WHERE UID = @UID " + 
					"ORDER BY LoginDate DESC " +
					"LIMIT 1; ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = CashierID;
				cmd.Parameters.Add(prmUID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				CashierLogStatus status = CashierLogStatus.LoggedOut;

				while (myReader.Read()) 
				{
					status = (CashierLogStatus) Enum.Parse(typeof (CashierLogStatus), myReader.GetByte(0).ToString());
					CashierLogsID = myReader.GetInt64("CashierLogsID");
				}

				myReader.Close();

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