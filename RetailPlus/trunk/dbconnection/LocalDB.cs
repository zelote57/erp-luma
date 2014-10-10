using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
namespace AceSoft.RetailPlus.Client
{

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class LocalDB : POSConnection
    {
		#region Constructors and Destructors

		public LocalDB()
            : base(null, null)
        {
        }

        public LocalDB(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Streams

        public System.Data.DataTable ListAsDataTable(string TableName, DateTime StartSyncDateTime, DateTime EndSyncDateTime)
        {
            try
            {
                string SQL = "CALL procTableSelectAll(@TableName, @StartSyncDateTime, @EndSyncDateTime)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@StartSyncDateTime", StartSyncDateTime);
                cmd.Parameters.AddWithValue("@EndSyncDateTime", EndSyncDateTime);

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

