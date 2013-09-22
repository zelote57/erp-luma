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
    public class SysConfig : POSConnection
    {
        #region Constructors and Destructors

        public SysConfig()
            : base(null, null)
        {
        }

        public SysConfig(MySqlConnection Connection, MySqlTransaction Transaction)
            : base(Connection, Transaction)
        {

        }

        #endregion
        

        #region get_BackendVariationType

        public string get_BackendVariationType()
        {
            string stRetValue = String.Empty;

            try
            {
                string SQL = "CALL procSysConfigSelectByName(@Configname)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Configname", Constants.SYS_CONFIG_BACKEND_VARIATION_TYPE);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["ConfigValue"].ToString()))
                        stRetValue = "" + dr["ConfigValue"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
            return stRetValue;
        }

        #endregion
    }
}
