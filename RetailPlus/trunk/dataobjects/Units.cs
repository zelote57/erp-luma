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
	public struct UnitDetails
	{
		public int UnitID;
		public string UnitCode;
		public string UnitName;

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
	public class Unit : POSConnection
	{
        public const Int32 DEFAULT_UNIT_ID = 1;
		
		#region Constructors and Destructors

		public Unit()
            : base(null, null)
        {
        }

        public Unit(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(UnitDetails Details)
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

		public void Update(UnitDetails Details)
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

        public Int32 Save(UnitDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveUnit(@UnitID, @UnitCode, @UnitName, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("UnitCode", Details.UnitCode);
                cmd.Parameters.AddWithValue("UnitName", Details.UnitName);
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
				string SQL=	"DELETE FROM tblUnit WHERE UnitID IN (" + IDs + ");";
				  
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

		public UnitDetails Details(Int32 UnitID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT UnitID, UnitCode, UnitName FROM tblUnit WHERE UnitID = @UnitID;";

                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                UnitDetails Details = new UnitDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.UnitID = Int32.Parse(dr["UnitID"].ToString());
                    Details.UnitCode = dr["UnitCode"].ToString();
                    Details.UnitName = dr["UnitName"].ToString();
                }

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public UnitDetails Details(string UnitCode)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT UnitID, UnitCode, UnitName FROM tblUnit WHERE UnitCode = @UnitCode;";
				  
                cmd.Parameters.AddWithValue("@UnitCode", UnitCode);
                cmd.CommandText = SQL;
                
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                UnitDetails Details = new UnitDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.UnitID = Int32.Parse(dr["UnitID"].ToString());
                    Details.UnitCode = dr["UnitCode"].ToString();
                    Details.UnitName = dr["UnitName"].ToString();
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

		public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "UnitCode", SortOption SortOrder = SortOption.Ascending)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = "SELECT UnitID, UnitCode, UnitName FROM tblUnit ";

            if (!string.IsNullOrEmpty(SearchKey))
            {
                SQL += "WHERE UnitCode LIKE @SearchKey OR UnitName LIKE @SearchKey ";
                cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
            }
            SQL += "ORDER BY " + SortField + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            
            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
		}

		#endregion
	}
}

