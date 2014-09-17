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
	public struct BranchDetails
	{
		public Int32 BranchID;
		public string BranchCode;
		public string BranchName;
		public string DBIP;
		public string DBPort;
		public string Address;
		public string Remarks;

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
	public class Branch : POSConnection
    {
		#region Constructors and Destructors

		public Branch()
            : base(null, null)
        {
        }

        public Branch(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(BranchDetails Details)
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

		public void Update(BranchDetails Details)
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

        public Int32 Save(BranchDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveBranch(@BranchID, @BranchCode, @BranchName, @DBIP, @DBPort, @Address, @Remarks, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("BranchCode", Details.BranchCode);
                cmd.Parameters.AddWithValue("BranchName", Details.BranchName);
                cmd.Parameters.AddWithValue("DBIP", Details.DBIP);
                cmd.Parameters.AddWithValue("DBPort", Details.DBPort);
                cmd.Parameters.AddWithValue("Address", Details.Address);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
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

				string SQL=	"DELETE FROM tblBranch WHERE BranchID IN (" + IDs + ");";
	 			
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
					            "BranchCode, " +
					            "BranchName, " + 
					            "DBIP, " +
					            "DBPort, " +
					            "Address, " +
					            "Remarks " +
					        "FROM tblBranch ";

            return stSQL;
        }

		#region Details

		public BranchDetails Details(Int32 BranchID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE BranchID = @BranchID;";
				  
                cmd.Parameters.AddWithValue("@BranchID", BranchID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                BranchDetails Details = new BranchDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                    Details.BranchCode = dr["BranchCode"].ToString();
                    Details.BranchName = dr["BranchName"].ToString();
                    Details.DBIP = dr["DBIP"].ToString();
                    Details.DBPort = dr["DBPort"].ToString();
                    Details.Address = dr["Address"].ToString();
                    Details.Remarks = dr["Remarks"].ToString();
                }

                return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public BranchDetails Details(string BranchCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE BranchCode = @BranchCode;";

                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                BranchDetails Details = new BranchDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                    Details.BranchCode = dr["BranchCode"].ToString();
                    Details.BranchName = dr["BranchName"].ToString();
                    Details.DBIP =  dr["DBIP"].ToString();
                    Details.DBPort = dr["DBPort"].ToString();
                    Details.Address = dr["Address"].ToString();
                    Details.Remarks = dr["Remarks"].ToString();
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

        public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "BranchCode", SortOption SortOrder=SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + " ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE (BranchCode LIKE @SearchKey or BranchName LIKE @SearchKey) ";
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
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	}
}

