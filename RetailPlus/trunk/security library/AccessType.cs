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
	public struct AccessTypeDetails
	{
		public int TypeID;
		public string TypeName;
		public string Remarks;
		public bool Enabled;
		public int SequenceNo;
		public string Category;

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
	public class AccessType : POSConnection
	{
		#region Constructors and Destructors

		public AccessType()
            : base(null, null)
        {
        }

        public AccessType(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        public Int32 Save(AccessTypeDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveSysAccessTypes(@TypeID, @TypeName, @Remarks, @Enabled, @SequenceNo, @Category, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("TypeID", Details.TypeID);
                cmd.Parameters.AddWithValue("TypeName", Details.TypeName);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("Enabled", Details.Enabled);
                cmd.Parameters.AddWithValue("SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("Category", Details.Category);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#region Streams

		private string SQLSelect()
		{
            string stSQL = "SELECT " +
                                "TypeID, " +
                                "TypeName, " +
                                "Remarks, " +
                                "Enabled, " +
                                "SequenceNo, " +
                                "Category " +
                            "FROM sysAccessTypes ";
			return stSQL;
		}

        public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC;";
            else
                SQL += " DESC;";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable(this.GetType().FullName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;

        }
        public System.Data.DataTable DataList(string Category, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

            string SQL = SQLSelect() + "WHERE Category = @Category ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC;";
            else
                SQL += " DESC;";
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Category", Category);

            System.Data.DataTable dt = new System.Data.DataTable(this.GetType().FullName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;

        }
        public System.Data.DataTable Categories(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

                string SQL = "SELECT DISTINCT(Category) FROM sysAccessTypes ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC;";
                else
                    SQL += " DESC;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable(this.GetType().FullName);
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

