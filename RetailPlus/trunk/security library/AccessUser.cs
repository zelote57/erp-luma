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
        public string Zip;
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
	public class AccessUser : POSConnection
	{
		
		#region Constructors and Destructors

		public AccessUser()
            : base(null, null)
        {
        }

        public AccessUser(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(AccessUserDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQLUser = "INSERT INTO sysAccessUsers (UserName, Password, DateCreated, CreatedOn, LastModified) VALUES (@UserName, @Password, @DateCreated, @CreatedOn, @LastModified);";
				
                cmd.Parameters.AddWithValue("UserName", Details.UserName);
                cmd.Parameters.AddWithValue("Password", Details.Password);
                Details.DateCreated = DateTime.Now;
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("CreatedOn", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("LastModified", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));

				cmd.CommandText = SQLUser;
				base.ExecuteNonQuery(cmd);

				Int64 iID = Int64.Parse(base.getLAST_INSERT_ID(this));

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQLDetails = "INSERT INTO sysAccessUserDetails (" +
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
                                        "GroupID, CreatedOn, LastModified) VALUES ( " +
                                        "@UID," +
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
                                        "@GroupID, @CreatedOn, @LastModified);";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("UID", iID);
                cmd.Parameters.AddWithValue("Name", Details.Name);
                cmd.Parameters.AddWithValue("Address1", Details.Address1);
                cmd.Parameters.AddWithValue("Address2", Details.Address2);
                cmd.Parameters.AddWithValue("City", Details.City);
                cmd.Parameters.AddWithValue("State", Details.State);
                cmd.Parameters.AddWithValue("CountryID", Details.CountryID);
                cmd.Parameters.AddWithValue("OfficePhone", Details.OfficePhone);
                cmd.Parameters.AddWithValue("DirectPhone", Details.DirectPhone);
                cmd.Parameters.AddWithValue("HomePhone", Details.HomePhone);
                cmd.Parameters.AddWithValue("FaxPhone", Details.FaxPhone);
                cmd.Parameters.AddWithValue("MobilePhone", Details.MobilePhone);
                cmd.Parameters.AddWithValue("EmailAddress", Details.EmailAddress);
                cmd.Parameters.AddWithValue("GroupID", Details.GroupID);
                cmd.Parameters.AddWithValue("CreatedOn", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("LastModified", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));

				cmd.CommandText = SQLDetails;
                base.ExecuteNonQuery(cmd);

				InsertAccessRights(iID, Details.GroupID);

				return iID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(AccessUserDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                //string SQLUser		=	"UPDATE sysAccessUsers SET " + 
                //                            "UserName = @UserName," +  
                //                            "Password = @Password " + 
                //                        "WHERE UID = @UID;";
				
                //cmd.Parameters.AddWithValue("UserName", Details.UserName);
                //cmd.Parameters.AddWithValue("Password", Details.Password);
                //cmd.Parameters.AddWithValue("UID", Details.UID);

                //cmd.CommandText = SQLUser;
                //base.ExecuteNonQuery(cmd);

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQLDetails = "UPDATE sysAccessUserDetails SET " +
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
                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Name", Details.Name);
                cmd.Parameters.AddWithValue("Address1", Details.Address1);
                cmd.Parameters.AddWithValue("Address2", Details.Address2);
                cmd.Parameters.AddWithValue("City", Details.City);
                cmd.Parameters.AddWithValue("State", Details.State);
                cmd.Parameters.AddWithValue("CountryID", Details.CountryID);
                cmd.Parameters.AddWithValue("OfficePhone", Details.OfficePhone);
                cmd.Parameters.AddWithValue("DirectPhone", Details.DirectPhone);
                cmd.Parameters.AddWithValue("HomePhone", Details.HomePhone);
                cmd.Parameters.AddWithValue("FaxPhone", Details.FaxPhone);
                cmd.Parameters.AddWithValue("MobilePhone", Details.MobilePhone);
                cmd.Parameters.AddWithValue("EmailAddress", Details.EmailAddress);
                cmd.Parameters.AddWithValue("GroupID", Details.GroupID);
                if (Details.PageSize == 0) Details.PageSize = 10;
                cmd.Parameters.AddWithValue("PageSize", Details.PageSize);
                cmd.Parameters.AddWithValue("UID", Details.UID);

                cmd.CommandText = SQLDetails;
                base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdatePassword(Int64 UID, string Password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQLUser = "UPDATE sysAccessUsers SET " +
                                            "Password = @Password " +
                                        "WHERE UID = @UID;";

                cmd.Parameters.AddWithValue("Password", Password);
                cmd.Parameters.AddWithValue("UID", UID);

                cmd.CommandText = SQLUser;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(AccessUserDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveSysAccessUsers(@UID, @UserName, @Password, @DateCreated, @Deleted, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("UID", Details.UID);
                cmd.Parameters.AddWithValue("UserName", Details.UserName);
                cmd.Parameters.AddWithValue("Password", Details.Password);
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("Deleted", Details.Deleted);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 SaveDetails(AccessUserDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveSysAccessUserDetails(@UID, @Name, @Address1, @Address2, @City, @State, @Zip, @CountryID," + 
                                                               "@OfficePhone, @DirectPhone, @HomePhone, @FaxPhone, @MobilePhone, " +
										                       "@EmailAddress, @GroupID, @PageSize, @CreatedOn, @LastModified);";

                
                cmd.Parameters.AddWithValue("UID", Details.UID);
                cmd.Parameters.AddWithValue("Name", Details.Name);
                cmd.Parameters.AddWithValue("Address1", Details.Address1);
                cmd.Parameters.AddWithValue("Address2", Details.Address2);
                cmd.Parameters.AddWithValue("City", Details.City);
                cmd.Parameters.AddWithValue("State", Details.State);
                cmd.Parameters.AddWithValue("Zip", Details.Zip);
                cmd.Parameters.AddWithValue("CountryID", Details.CountryID);
                cmd.Parameters.AddWithValue("OfficePhone", Details.OfficePhone);
                cmd.Parameters.AddWithValue("DirectPhone", Details.DirectPhone);
                cmd.Parameters.AddWithValue("HomePhone", Details.HomePhone);
                cmd.Parameters.AddWithValue("FaxPhone", Details.FaxPhone);
                cmd.Parameters.AddWithValue("MobilePhone", Details.MobilePhone);
                cmd.Parameters.AddWithValue("EmailAddress", Details.EmailAddress);
                cmd.Parameters.AddWithValue("GroupID", Details.GroupID);
                cmd.Parameters.AddWithValue("PageSize", Details.PageSize);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE sysAccessUsers SET " +
							"Deleted = '1' " +
							"WHERE UID IN (" + IDs + ");";
				  
				
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

		#region Details

		public AccessUserDetails Details(Int64 UID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE a.UID = @UID;";

                cmd.Parameters.AddWithValue("UID", UID);

				cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                AccessUserDetails Details = new AccessUserDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.UID = UID;
                    Details.UserName = "" + dr["UserName"].ToString();
                    Details.Password = "" + dr["Password"].ToString();
                    Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.Deleted = bool.Parse(dr["Deleted"].ToString());
                    Details.Name = "" + dr["Name"].ToString();
                    Details.Address1 = "" + dr["Address1"].ToString();
                    Details.Address2 = "" + dr["Address2"].ToString();
                    Details.City = "" + dr["City"].ToString();
                    Details.State = "" + dr["State"].ToString();
                    Details.CountryID = Int32.Parse(dr["CountryID"].ToString());
                    Details.CountryName = "" + dr["CountryName"].ToString();
                    Details.OfficePhone = "" + dr["OfficePhone"].ToString();
                    Details.DirectPhone = "" + dr["DirectPhone"].ToString();
                    Details.HomePhone = "" + dr["HomePhone"].ToString();
                    Details.FaxPhone = "" + dr["FaxPhone"].ToString();
                    Details.MobilePhone = "" + dr["MobilePhone"].ToString();
                    Details.EmailAddress = "" + dr["EmailAddress"].ToString();
                    Details.GroupID = Int32.Parse(dr["GroupID"].ToString());
                    Details.GroupName = "" + dr["GroupName"].ToString();
                    Details.PageSize = Int32.Parse(dr["PageSize"].ToString());
                    Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
                }

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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
							"Name as WaiterName, " +
                            "a.CreatedOn, " +
                            "a.LastModified " +
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
								"PageSize, " +
                                "a.CreatedOn, " +
                                "a.LastModified " +
							"FROM sysAccessUsers a " +
							"INNER JOIN sysAccessUserDetails b ON a.UID = b.UID	" +
							"INNER JOIN sysAccessGroups c ON b.GroupID = c.GroupID " +
							"LEFT OUTER JOIN tblCountry d ON b.CountryID = d.CountryID ";
			}
			
			return stSQL;
		}
        public System.Data.DataTable ListAsDataTable(AccessGroupTypes clsAccessGroupTypes = AccessGroupTypes.All, string SearchKey = "", Int32 limit = 0, Int32 UserGroupID = 0, string SortField = "Name", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect(clsAccessGroupTypes) + "WHERE deleted = '0' ";

                if (clsAccessGroupTypes == AccessGroupTypes.Waiters || clsAccessGroupTypes == AccessGroupTypes.Bagger)
                {
                    SQL += "AND c.GroupName IN ('Waiters','Bagger') ";
                    cmd.Parameters.AddWithValue("@GroupName", clsAccessGroupTypes.ToString("G"));
                }
				else if (clsAccessGroupTypes != AccessGroupTypes.All)
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
                                "OR GroupName LIKE @SearchKey) ";
					cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
				}

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "Name") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                if (clsAccessGroupTypes == AccessGroupTypes.Waiters)
                    dt = new System.Data.DataTable("tblWaiters");
                else
                    dt = new System.Data.DataTable("tblAccessUser");
                base.MySqlDataAdapterFill(cmd, dt);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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
				throw base.ThrowException(ex);
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
				throw base.ThrowException(ex);
			}
		}	

		#endregion

		#region Login

		public Int64 Login(string UserName, string Password)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL ="SELECT UID FROM sysAccessUsers " +
							"WHERE UserName = @UserName AND Password = @Password AND Deleted = 0";

                cmd.Parameters.AddWithValue("UserName", UserName);
                cmd.Parameters.AddWithValue("Password", Password);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr["UID"].ToString());
                }

				return iID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public Int64 Login(string UserName, string Password, AccessTypes accesstype, out string pstrName)
		{
			try
			{
				pstrName = string.Empty;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL ="SELECT a.UID, Name FROM sysAccessUsers a " +
							"INNER JOIN sysAccessRights b ON a.UID = b.UID " +
							"INNER JOIN sysAccessTypes c ON b.TranTypeID = c.TypeID " +
							"INNER JOIN sysAccessUserDetails d ON a.UID = d.UID " +
							"WHERE UserName = @UserName " +
							"       AND Password = @Password AND Deleted = 0 " +
							"       AND TranTypeID = @TranTypeID AND AllowWrite = 1 AND Enabled =1 ";

				cmd.Parameters.AddWithValue("UserName", UserName);
				cmd.Parameters.AddWithValue("Password", Password);
				cmd.Parameters.AddWithValue("TranTypeID", accesstype.ToString("d"));

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr["UID"].ToString());
                    pstrName = dr["Name"].ToString();
                }

				return iID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		#endregion

		#region Private Modifiers

		private void InsertAccessRights(Int64 UID, int GroupID)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite, CreatedOn, LastModified) " +
                              "SELECT @UID, TranTypeID, AllowRead, AllowWrite, @CreatedOn, @CreatedOn FROM sysAccessGroupRights WHERE GroupID = @GroupID;";
				
                cmd.Parameters.AddWithValue("UID", UID);
                cmd.Parameters.AddWithValue("GroupID", GroupID);
                cmd.Parameters.AddWithValue("CreatedOn", DateTime.Now);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Public Modifiers

		public void SynchronizeAccessRightsFromGroup(long UID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL AccessuserSynchronizeAccessRights(@UID);";

				cmd.Parameters.AddWithValue("@UID", UID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void SynchronizeAccessRightsFromGroup(long UID, int GroupID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL AccessuserSynchronizeAccessRightsFromGroup(@UID, @GroupID);";

				cmd.Parameters.AddWithValue("@UID", UID);
				cmd.Parameters.AddWithValue("GroupID", GroupID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		#endregion

	}
}

