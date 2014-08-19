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
    public class DBSync : POSConnection
    {
		#region Constructors and Destructors

        public DBSync()
            : base(null, null)
        {
        }

        public DBSync(MySqlConnection Connection, MySqlTransaction Transaction) 
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

                string strDataTableName = TableName; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 CountOfRecords(string TableName)
        {
            try
            {
                string SQL = "CALL procTableCountAll(@TableName)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TableName", TableName);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return Int32.Parse(dt.Rows[0]["RecordCount"].ToString());
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public string getAllIDs(string TableName, string KeyColName, bool IsKeyColNumeric = false)
        {
            try
            {
                string stRetvalue = "";

                string SQL = "CALL procTableSelectAllKeys(@TableName, @KeyColName, @IsNumeric)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@KeyColName", KeyColName);
                cmd.Parameters.AddWithValue("@IsNumeric", IsKeyColNumeric);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    stRetvalue = dr[0].ToString();
                }
                return stRetvalue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public int Delete(string TableName, string KeyColName, string KeysNotToDelete)
        {
            try
            {
                string SQL = "CALL procTableDeleteWithKeys(@TableName, @KeyColName, @KeysNotToDelete)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TableName", TableName);
                cmd.Parameters.AddWithValue("@KeyColName", KeyColName);
                cmd.Parameters.AddWithValue("@KeysNotToDelete", KeysNotToDelete);

                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public bool SetForeignKey(bool Enable = true)
        {
            try
            {
                Data.Database clsDatabase = new Data.Database(this.Connection, this.Transaction);
                return clsDatabase.SetForeignKey(Enable);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion
    }
}

