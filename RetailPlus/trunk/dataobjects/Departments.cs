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
	public struct DepartmentDetails
	{
		public Int16 DepartmentID;
		public string DepartmentCode;
		public string DepartmentName;

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
	public class Department : POSConnection
	{
        public const string DEFAULT_ALL_DEPARTMENTS = "All Departments";

		#region Constructors and Destructors

		public Department()
            : base(null, null)
        {
        }

        public Department(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

        public Int32 Insert(DepartmentDetails Details)
        {
            try
            {
                Save(Details);

                return Int32.Parse(base.getLAST_INSERT_ID(this));
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void Update(DepartmentDetails Details)
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

        public Int32 Save(DepartmentDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveDepartment(@DepartmentID, @DepartmentCode, @DepartmentName, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("DepartmentID", Details.DepartmentID);
                cmd.Parameters.AddWithValue("DepartmentCode", Details.DepartmentCode);
                cmd.Parameters.AddWithValue("DepartmentName", Details.DepartmentName);
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

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "DepartmentID, " +
                                "DepartmentCode, " +
                                "DepartmentName " +
                            "FROM tblDepartments ";
            return stSQL;
        }

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblDepartments WHERE DepartmentID IN (" + IDs + ");";
				  
				
	 			
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

		#region Details

		public DepartmentDetails Details(Int16 DepartmentID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	SQLSelect() + "WHERE DepartmentID = @DepartmentID;";

                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DepartmentDetails Details = new DepartmentDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.DepartmentID = Int16.Parse(dr["DepartmentID"].ToString());
                    Details.DepartmentCode = "" + dr["DepartmentCode"].ToString();
                    Details.DepartmentName = "" + dr["DepartmentName"].ToString();
                }

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public DepartmentDetails Details(string DepartmentName)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE DepartmentName = @DepartmentName;";

                cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DepartmentDetails Details = new DepartmentDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.DepartmentID = Int16.Parse(dr["DepartmentID"].ToString());
                    Details.DepartmentCode = "" + dr["DepartmentCode"].ToString();
                    Details.DepartmentName = "" + dr["DepartmentName"].ToString();
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

        public System.Data.DataTable ListAsDataTable(string SearchKey = null, string SortField = "DepartmentName", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + " ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE (DepartmentCode LIKE @SearchKey OR DepartmentName LIKE @SearchKey) ";
                    cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "DepartmentName") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

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
	}
}

