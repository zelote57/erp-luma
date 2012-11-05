using System;
using System.Data;
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
	

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct AccessUserDetails
	{
		public Int64 UID;
		public string UserName;
		public string Password;
		public DateTime DateCreated;
		public bool Deleted;
		public string Name;
		public string Address1;
		public string Address2;
		public string City;
		public string State;
		public Int32 CountryID;
		public string CountryName;
		public string OfficePhone;
		public string DirectPhone;
		public string HomePhone;
		public string FaxPhone;
		public string MobilePhone;
		public string EmailAddress;
		public Int32 GroupID;
		public string GroupName;
		public int PageSize;
	}

	
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccessUser
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

		public AccessUser()
		{
			
		}

		public AccessUser(MySqlConnection Connection, MySqlTransaction Transaction)
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


		#endregion

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


		#region Insert and Update

		public Int64 Insert(AccessUserDetails Details)
		{
			try 
			{
				string SQLUser		=	"INSERT INTO sysAccessUsers (UserName, Password, DateCreated) VALUES (@UserName, @Password, @DateCreated);";
				string SQLDetails	=	"INSERT INTO sysAccessUserDetails (" +
										"UID," +
										"Name," +
										"Address1," +
										"Address2," +
										"City," +
										"State," +
										"CountryID," +
										"OfficePhone," +
										"DirectPhone," +
										"HomePhone," +
										"FaxPhone," +
										"MobilePhone," +
										"EmailAddress," +
										"GroupID ) VALUES ( " +
										"LAST_INSERT_ID()," +
										"@Name," +
										"@Address1," +
										"@Address2," +
										"@City," +
										"@State," +
										"@CountryID," +
										"@OfficePhone," +
										"@DirectPhone," +
										"@HomePhone," +
										"@FaxPhone," +
										"@MobilePhone," +
										"@EmailAddress," +
										"@GroupID);";
	
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmUserName = new MySqlParameter("@UserName",MySqlDbType.String);
				prmUserName.Value = Details.UserName;
				cmd.Parameters.Add(prmUserName);

				MySqlParameter prmPassword = new MySqlParameter("@Password",MySqlDbType.String);
				prmPassword.Value = Details.Password;
				cmd.Parameters.Add(prmPassword);

				MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);
				prmDateCreated.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateCreated);

				cmd.CommandText = SQLUser;
				cmd.ExecuteNonQuery();

				string SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

				MySqlCommand cmdDetails = new MySqlCommand();
				cmdDetails.Connection = cn;
				cmd.Transaction = mTransaction;
				cmdDetails.CommandType = System.Data.CommandType.Text;

				MySqlParameter prmName = new MySqlParameter("@Name",MySqlDbType.String);
				prmName.Value = Details.Name;
				cmdDetails.Parameters.Add(prmName);

				MySqlParameter prmAddress1 = new MySqlParameter("@Address1",MySqlDbType.String);
				prmAddress1.Value = Details.Address1;
				cmdDetails.Parameters.Add(prmAddress1);

				MySqlParameter prmAddress2 = new MySqlParameter("@Address2",MySqlDbType.String);
				prmAddress2.Value = Details.Address2;
				cmdDetails.Parameters.Add(prmAddress2);

				MySqlParameter prmCity = new MySqlParameter("@City",MySqlDbType.String);
				prmCity.Value = Details.City;
				cmdDetails.Parameters.Add(prmCity);

				MySqlParameter prmState = new MySqlParameter("@State",MySqlDbType.String);
				prmState.Value = Details.State;
				cmdDetails.Parameters.Add(prmState);

				MySqlParameter prmCountryID = new MySqlParameter("@CountryID",System.Data.DbType.Int32);
				prmCountryID.Value = Details.CountryID;
				cmdDetails.Parameters.Add(prmCountryID);

				MySqlParameter prmOfficePhone = new MySqlParameter("@OfficePhone",MySqlDbType.String);
				prmOfficePhone.Value = Details.OfficePhone;
				cmdDetails.Parameters.Add(prmOfficePhone);

				MySqlParameter prmDirectPhone = new MySqlParameter("@DirectPhone",MySqlDbType.String);
				prmDirectPhone.Value = Details.DirectPhone;
				cmdDetails.Parameters.Add(prmDirectPhone);

				MySqlParameter prmHomePhone = new MySqlParameter("@HomePhone",MySqlDbType.String);
				prmHomePhone.Value = Details.HomePhone;
				cmdDetails.Parameters.Add(prmHomePhone);

				MySqlParameter prmFaxPhone = new MySqlParameter("@FaxPhone",MySqlDbType.String);
				prmFaxPhone.Value = Details.FaxPhone;
				cmdDetails.Parameters.Add(prmFaxPhone);

				MySqlParameter prmMobilePhone = new MySqlParameter("@MobilePhone",MySqlDbType.String);
				prmMobilePhone.Value = Details.MobilePhone;
				cmdDetails.Parameters.Add(prmMobilePhone);

				MySqlParameter prmEmailAddress = new MySqlParameter("@EmailAddress",MySqlDbType.String);
				prmEmailAddress.Value = Details.EmailAddress;
				cmdDetails.Parameters.Add(prmEmailAddress);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",System.Data.DbType.Int32);
				prmGroupID.Value = Details.GroupID;
				cmdDetails.Parameters.Add(prmGroupID);

				cmdDetails.CommandText = SQLDetails;
				cmdDetails.ExecuteNonQuery();

				InsertAccessRights(iID, Details.GroupID);

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

		public void Update(AccessUserDetails Details)
		{
			try 
			{
				string SQLUser		=	"UPDATE sysAccessUsers SET " + 
										"UserName = @UserName," +  
										"Password = @Password, " + 
										"DateCreated = @DateCreated " + 
										"WHERE UID = @UID;";

				string SQLDetails	=	"UPDATE sysAccessUserDetails SET " +
											"Name			=	@Name, " +
											"Address1		=	@Address1, " +
											"Address2		=	@Address2, " +
											"City			=	@City, " +
											"State			=	@State, " +
											"CountryID		=	@CountryID, " +
											"OfficePhone	=	@OfficePhone, " +
											"DirectPhone	=	@DirectPhone, " +
											"HomePhone		=	@HomePhone, " +
											"FaxPhone		=	@FaxPhone, " +
											"MobilePhone	=	@MobilePhone, " +
											"EmailAddress	=	@EmailAddress, " +
											"GroupID		=	@GroupID, " +
											"PageSize		=	@PageSize " +
										"WHERE UID		=	@UID;";

				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmUserName = new MySqlParameter("@UserName",MySqlDbType.String);
				prmUserName.Value = Details.UserName;
				cmd.Parameters.Add(prmUserName);

				MySqlParameter prmPassword = new MySqlParameter("@Password",MySqlDbType.String);
				prmPassword.Value = Details.Password;
				cmd.Parameters.Add(prmPassword);

				MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);
				prmDateCreated.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateCreated);

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int32);
				prmUID.Value = Details.UID;
				cmd.Parameters.Add(prmUID);

				cmd.CommandText = SQLUser;
				cmd.ExecuteNonQuery();

				MySqlCommand cmdDetails = new MySqlCommand();
				cmdDetails.Connection = cn;
				cmd.Transaction = mTransaction;
				cmdDetails.CommandType = System.Data.CommandType.Text;

				MySqlParameter prmName = new MySqlParameter("@Name",MySqlDbType.String);
				prmName.Value = Details.Name;
				cmdDetails.Parameters.Add(prmName);

				MySqlParameter prmAddress1 = new MySqlParameter("@Address1",MySqlDbType.String);
				prmAddress1.Value = Details.Address1;
				cmdDetails.Parameters.Add(prmAddress1);

				MySqlParameter prmAddress2 = new MySqlParameter("@Address2",MySqlDbType.String);
				prmAddress2.Value = Details.Address2;
				cmdDetails.Parameters.Add(prmAddress2);

				MySqlParameter prmCity = new MySqlParameter("@City",MySqlDbType.String);
				prmCity.Value = Details.City;
				cmdDetails.Parameters.Add(prmCity);

				MySqlParameter prmState = new MySqlParameter("@State",MySqlDbType.String);
				prmState.Value = Details.State;
				cmdDetails.Parameters.Add(prmState);

				MySqlParameter prmCountryID = new MySqlParameter("@CountryID",System.Data.DbType.Int32);
				prmCountryID.Value = Details.CountryID;
				cmdDetails.Parameters.Add(prmCountryID);

				MySqlParameter prmOfficePhone = new MySqlParameter("@OfficePhone",MySqlDbType.String);
				prmOfficePhone.Value = Details.OfficePhone;
				cmdDetails.Parameters.Add(prmOfficePhone);

				MySqlParameter prmDirectPhone = new MySqlParameter("@DirectPhone",MySqlDbType.String);
				prmDirectPhone.Value = Details.DirectPhone;
				cmdDetails.Parameters.Add(prmDirectPhone);

				MySqlParameter prmHomePhone = new MySqlParameter("@HomePhone",MySqlDbType.String);
				prmHomePhone.Value = Details.HomePhone;
				cmdDetails.Parameters.Add(prmHomePhone);

				MySqlParameter prmFaxPhone = new MySqlParameter("@FaxPhone",MySqlDbType.String);
				prmFaxPhone.Value = Details.FaxPhone;
				cmdDetails.Parameters.Add(prmFaxPhone);

				MySqlParameter prmMobilePhone = new MySqlParameter("@MobilePhone",MySqlDbType.String);
				prmMobilePhone.Value = Details.MobilePhone;
				cmdDetails.Parameters.Add(prmMobilePhone);

				MySqlParameter prmEmailAddress = new MySqlParameter("@EmailAddress",MySqlDbType.String);
				prmEmailAddress.Value = Details.EmailAddress;
				cmdDetails.Parameters.Add(prmEmailAddress);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",System.Data.DbType.Int32);
				prmGroupID.Value = Details.GroupID;
				cmdDetails.Parameters.Add(prmGroupID);

				if (Details.PageSize == 0) Details.PageSize = 10;
				MySqlParameter prmPageSize = new MySqlParameter("@PageSize",System.Data.DbType.Int32);
				prmPageSize.Value = Details.PageSize;
				cmdDetails.Parameters.Add(prmPageSize);

				prmUID = new MySqlParameter("@UID",MySqlDbType.Int32);
				prmUID.Value = Details.UID;
				cmdDetails.Parameters.Add(prmUID);

				cmdDetails.CommandText = SQLDetails;
				cmdDetails.ExecuteNonQuery();
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
				string SQL=	"UPDATE sysAccessUsers SET " +
							"Deleted = '1' " +
							"WHERE UID IN (" + IDs + ");";
				  
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

		#region Details

		public AccessUserDetails Details(Int64 UID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.UID = @UID;";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",System.Data.DbType.Int64);
				prmUID.Value = UID;
				cmd.Parameters.Add(prmUID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				AccessUserDetails Details = new AccessUserDetails();

				while (myReader.Read()) 
				{
					Details.UID = UID;
					Details.UserName = "" + myReader["UserName"].ToString();
					Details.Password = "" + myReader["Password"].ToString();
					Details.DateCreated = myReader.GetDateTime("DateCreated");
					Details.Deleted = myReader.GetBoolean("Deleted");
					Details.Name = "" + myReader["Name"].ToString();
					Details.Address1 = "" + myReader["Address1"].ToString();
					Details.Address2 = "" + myReader["Address2"].ToString();
					Details.City = "" + myReader["City"].ToString();
					Details.State = "" + myReader["State"].ToString();
					Details.CountryID = myReader.GetInt32("CountryID");
					Details.CountryName = "" + myReader["CountryName"].ToString();
					Details.OfficePhone = "" + myReader["OfficePhone"].ToString();
					Details.DirectPhone = "" + myReader["DirectPhone"].ToString();
					Details.HomePhone = "" + myReader["HomePhone"].ToString();
					Details.FaxPhone = "" + myReader["FaxPhone"].ToString();
					Details.MobilePhone = "" + myReader["MobilePhone"].ToString();
					Details.EmailAddress = "" + myReader["EmailAddress"].ToString();
					Details.GroupID = myReader.GetInt32("GroupID");
					Details.GroupName = "" + myReader["GroupName"].ToString();
					Details.PageSize = myReader.GetInt32("PageSize");
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

		#region Streams

		private string SQLSelect(AccessGroupTypes clsAccessGroupTypes = AccessGroupTypes.All)
		{
			string stSQL = string.Empty;

			if (clsAccessGroupTypes == AccessGroupTypes.Waiters)
			{
				stSQL = "SELECT " +
							"a.UID as WaiterID, " +
							"UserName as WaiterCode, " +
							"Name as WaiterName " +
						"FROM sysAccessUsers a " +
						"INNER JOIN sysAccessUserDetails b ON a.UID = b.UID	" +
						"INNER JOIN sysAccessGroups c ON b.GroupID = c.GroupID " +
						"LEFT OUTER JOIN tblCountry d ON b.CountryID = d.CountryID ";
			}
			else
			{
				stSQL = "SELECT " +
								"a.UID, " +
								"UserName, " +
								"Password, " +
								"DateCreated, " +
								"Deleted, " +
								"Name, " +
								"Address1, " +
								"Address2, " +
								"City, " +
								"State, " +
								"b.CountryID, " +
								"d.CountryName, " +
								"OfficePhone, " +
								"DirectPhone, " +
								"HomePhone, " +
								"FaxPhone, " +
								"MobilePhone, " +
								"EmailAddress, " +
								"b.GroupID, " +
								"GroupName, " +
								"PageSize " +
							"FROM sysAccessUsers a " +
							"INNER JOIN sysAccessUserDetails b ON a.UID = b.UID	" +
							"INNER JOIN sysAccessGroups c ON b.GroupID = c.GroupID " +
							"LEFT OUTER JOIN tblCountry d ON b.CountryID = d.CountryID ";
			}
			
			return stSQL;
		}
		//public MySqlDataReader List(string SortField, SortOption SortOrder)
		//{
		//    try
		//    {
		//        string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' ORDER BY " + SortField; 

		//        if (SortOrder == SortOption.Ascending)
		//            SQL += " ASC;";
		//        else
		//            SQL += " DESC;";

		//        MySqlConnection cn = GetConnection();

		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
		//        return myReader;			
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//            mTransaction.Rollback();

		//        mTransaction.Dispose(); 
		//        mConnection.Close();
		//        mConnection.Dispose();

		//        throw ex;
		//    }	
		//}
		public System.Data.DataTable ListAsDataTable(AccessGroupTypes clsAccessGroupTypes = AccessGroupTypes.All, string SearchKey = "", Int32 Limit = 0, Int32 UserGroupID = 0, string SortField = "UID", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				string SQL = SQLSelect(clsAccessGroupTypes) + "WHERE deleted = '0' ";

				if (clsAccessGroupTypes != AccessGroupTypes.All)
				{
					SQL += "AND c.GroupName = @GroupName ";
					cmd.Parameters.AddWithValue("@GroupName", clsAccessGroupTypes.ToString("G"));
				}
				if (UserGroupID != 0)
				{
					SQL += "AND b.GroupID = @GroupID ";
					cmd.Parameters.AddWithValue("@GroupID", UserGroupID);
				}
				if (SearchKey != "" && SearchKey != string.Empty)
				{
					SQL += "AND (UserName LIKE @SearchKey " +
								"OR Name LIKE @SearchKey " +
								"OR Address1 LIKE @SearchKey " +
								"OR CountryName LIKE @SearchKey " +
								"OR GroupName LIKE @SearchKey) " +
					cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
				}

				SQL += "ORDER BY " + SortField;
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit.ToString();

				MySqlConnection cn = GetConnection();

				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				System.Data.DataTable dt;
				if (clsAccessGroupTypes == AccessGroupTypes.Waiters)
					dt = new System.Data.DataTable("tblWaiters");
				else
					dt = new System.Data.DataTable("tblAccessUser");

				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
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

		public DataTable Waiters(string SearchKey = "", Int32 Limit = 0, string SortField = "UID", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
				System.Data.DataTable dt = ListAsDataTable(AccessGroupTypes.Waiters, SearchKey, Limit, 0, SortField, SortOrder);

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

		public DataTable Cashiers(string SearchKey = "", Int32 Limit = 0, string SortField = "UID", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
				System.Data.DataTable dt = ListAsDataTable(AccessGroupTypes.Cashiers, SearchKey, Limit, 0, SortField, SortOrder);

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

		//public System.Data.DataTable DataList(int GroupID, string SortField, SortOption SortOrder)
		//{
		//    try
		//    {
		//        if (SortField == string.Empty || SortField == null) SortField = "UID";

		//        string SQL = SQLSelect() + "WHERE b.GroupID = @GroupID AND deleted = '0' ORDER BY " + SortField;

		//        if (SortOrder == SortOption.Ascending)
		//            SQL += " ASC;";
		//        else
		//            SQL += " DESC;";

		//        MySqlConnection cn = GetConnection();

		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;

		//        cmd.Parameters.AddWithValue("@GroupID", GroupID);

		//        System.Data.DataTable dt = new System.Data.DataTable("tblAccessUser");
		//        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
		//        adapter.Fill(dt);

		//        return dt;
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//            mTransaction.Rollback();

		//        mTransaction.Dispose();
		//        mConnection.Close();
		//        mConnection.Dispose();

		//        throw ex;
		//    }
		//}
		//public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		//{
		//    try
		//    {
		//        string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
		//                        "AND (UserName LIKE @SearchKey " +
		//                        "OR Name LIKE @SearchKey " +
		//                        "OR Address1 LIKE @SearchKey " +
		//                        "OR CountryName LIKE @SearchKey " +
		//                        "OR GroupName LIKE @SearchKey) " +
		//                        "ORDER BY " + SortField;

		//        if (SortOrder == SortOption.Ascending)
		//            SQL += " ASC";
		//        else
		//            SQL += " DESC";

		//        MySqlConnection cn = GetConnection();

		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;
				
		//        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
		//        prmSearchKey.Value = "%" + SearchKey + "%";
		//        cmd.Parameters.Add(prmSearchKey);

		//        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
		//        return myReader;			
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//            mTransaction.Rollback();

		//        mTransaction.Dispose(); 
		//        mConnection.Close();
		//        mConnection.Dispose();

		//        throw ex;
		//    }	
		//}
		//public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
		//{
		//    try
		//    {
		//        if (SortField == string.Empty || SortField == null) SortField = "UID";

		//        string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
		//                        "AND (UserName LIKE @SearchKey " +
		//                        "OR Name LIKE @SearchKey " +
		//                        "OR Address1 LIKE @SearchKey " +
		//                        "OR CountryName LIKE @SearchKey " +
		//                        "OR GroupName LIKE @SearchKey) " +
		//                        "ORDER BY " + SortField;

		//        if (SortOrder == SortOption.Ascending)
		//            SQL += " ASC";
		//        else
		//            SQL += " DESC";

		//        MySqlConnection cn = GetConnection();

		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;

		//        cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

		//        System.Data.DataTable dt = new System.Data.DataTable("tblAccessUser");
		//        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
		//        adapter.Fill(dt);

		//        return dt;
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//            mTransaction.Rollback();

		//        mTransaction.Dispose();
		//        mConnection.Close();
		//        mConnection.Dispose();

		//        throw ex;
		//    }
		//}
		//public MySqlDataReader Waiters(AccessGroupTypes clsAccessGroupTypes, string SearchKey = "", string SortField = "UID", SortOption SortOrder = SortOption.Ascending)
		//{
		//    try
		//    {
		//        string SQL = "SELECT " +
		//                            "a.UID as WaiterID, " +
		//                            "UserName as WaiterCode, " +
		//                            "Name as WaiterName " +
		//                        "FROM sysAccessUsers a " +
		//                        "INNER JOIN sysAccessUserDetails b ON a.UID = b.UID	" +
		//                        "INNER JOIN sysAccessGroups c ON b.GroupID = c.GroupID " +
		//                        "LEFT OUTER JOIN tblCountry d ON b.CountryID = d.CountryID " +
		//                        "WHERE 1=1 AND deleted = '0' AND b.GroupID = 5 " +
		//                        "AND (UserName LIKE @SearchKey " +
		//                        "OR Name LIKE @SearchKey " +
		//                        "OR Address1 LIKE @SearchKey " +
		//                        "OR CountryName LIKE @SearchKey " +
		//                        "OR GroupName LIKE @SearchKey) " +
		//                        "ORDER BY " + SortField;

		//        if (SortOrder == SortOption.Ascending)
		//            SQL += " ASC ";
		//        else
		//            SQL += " DESC ";

		//        if (Limit != 0)
		//            SQL += "LIMIT " + Limit.ToString();

		//        MySqlConnection cn = GetConnection();

		//        MySqlCommand cmd = new MySqlCommand();
		//        cmd.Connection = cn;
		//        cmd.Transaction = mTransaction;
		//        cmd.CommandType = System.Data.CommandType.Text;
		//        cmd.CommandText = SQL;

		//        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
		//        prmSearchKey.Value = "%" + SearchKey + "%";
		//        cmd.Parameters.Add(prmSearchKey);

		//        MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

		//        return myReader;
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//            mTransaction.Rollback();

		//        mTransaction.Dispose();
		//        mConnection.Close();
		//        mConnection.Dispose();

		//        throw ex;
		//    }
		//}
		//public DataTable WaitersDataTable(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
		//{
		//    try
		//    {
		//        System.Data.DataTable dt = new System.Data.DataTable("tblWaiters");

		//        dt.Columns.Add("WaiterID");
		//        dt.Columns.Add("WaiterCode");
		//        dt.Columns.Add("WaiterName");

		//        MySqlDataReader myReader = Waiters(SearchKey, Limit, SortField, SortOrder);

		//        while (myReader.Read())
		//        {
		//            System.Data.DataRow dr = dt.NewRow();

		//            dr["WaiterID"] = myReader.GetInt64("WaiterID");
		//            dr["WaiterCode"] = "" + myReader["WaiterCode"].ToString();
		//            dr["WaiterName"] = "" + myReader["WaiterName"].ToString();

		//            dt.Rows.Add(dr);
		//        }
		//        myReader.Close();

		//        return dt;
		//    }
		//    catch (Exception ex)
		//    {
		//        TransactionFailed = true;
		//        if (IsInTransaction)
		//        {
		//            mTransaction.Rollback();
		//            mTransaction.Dispose();
		//            mConnection.Close();
		//            mConnection.Dispose();
		//        }

		//        throw ex;
		//    }
		//}				

		#endregion

		#region Login

		public Int64 Login(string UserName, string Password)
		{
			try
			{
				string SQL ="SELECT UID FROM sysAccessUsers " +
							"WHERE UserName = @UserName " +
							"AND Password = @Password AND Deleted = 0";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUserName = new MySqlParameter("@UserName",MySqlDbType.String);			
				prmUserName.Value = UserName;
				cmd.Parameters.Add(prmUserName);

				MySqlParameter prmPassword = new MySqlParameter("@Password",MySqlDbType.String);			
				prmPassword.Value = Password;
				cmd.Parameters.Add(prmPassword);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
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
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
		public Int64 Login(string UserName, string Password, AccessTypes accesstype, out string pstrName)
		{
			try
			{
				pstrName = string.Empty;

				string SQL ="SELECT a.UID, Name FROM sysAccessUsers a " +
							"INNER JOIN sysAccessRights b ON a.UID = b.UID " +
							"INNER JOIN sysAccessTypes c ON b.TranTypeID = c.TypeID " +
							"INNER JOIN sysAccessUserDetails d ON a.UID = d.UID " +
							"WHERE UserName = @UserName " +
							"       AND Password = @Password AND Deleted = 0 " +
							"       AND TranTypeID = @TranTypeID AND AllowWrite = 1 AND Enabled =1 ";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@UserName", UserName);
				cmd.Parameters.AddWithValue("@Password", Password);
				cmd.Parameters.AddWithValue("@TranTypeID", accesstype.ToString("d"));

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;
				
				while (myReader.Read())
				{
					iID = myReader.GetInt64("UID");
					pstrName = "" + myReader["Name"].ToString();
				}
			
				myReader.Close();

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
		
		#endregion

		#region Private Modifiers

		private void InsertAccessRights(Int64 UID, int GroupID)
		{
			try 
			{
				string SQL	= "INSERT INTO sysAccessRights " + 
								"SELECT @UID, TranTypeID, AllowRead, AllowWrite " +  
								"FROM sysAccessGroupRights " + 
								"WHERE GroupID = @GroupID;";

				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",MySqlDbType.Int64);
				prmUID.Value = UID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",System.Data.DbType.Int32);
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				cmd.ExecuteNonQuery();
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

		#region Public Modifiers

		public void SynchronizeAccessRightsFromGroup(long UID)
		{
			try
			{
				string SQL = "CALL AccessuserSynchronizeAccessRights(@UID);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@UID", UID);

				cmd.ExecuteNonQuery();
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
		public void SynchronizeAccessRightsFromGroup(long UID, int GroupID)
		{
			try
			{
				string SQL = "CALL AccessuserSynchronizeAccessRightsFromGroup(@UID, @GroupID);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@UID", UID);
				cmd.Parameters.AddWithValue("GroupID", GroupID);

				cmd.ExecuteNonQuery();
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

	}
}
