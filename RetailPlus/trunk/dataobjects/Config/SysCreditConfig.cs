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

    public struct SysCreditConfigDetails
    {
        public string ConfigName;
        public string ConfigValue;
        public string Remarks;
        public DateTime CreatedOn;
        public DateTime LastModified;

        public string BILLING_DATE;
        public string CREDIT_CUTOFF_DATE;
        public string CREDIT_FINANCE_CHARGE;
        public string CREDIT_LATE_PENALTY_CHARGE;
        
        public string CRED_MIN_AMOUNT_DUE;
        public string CRED_MIN_PERCENTAGE_DUE;
        public string CREDIT_USE_LASTDAY_CUTT_OFF_DATE;


    }
    public class SysCreditConfig : POSConnection
    {
        #region Constructors and Destructors

        public SysCreditConfig()
            : base(null, null)
        {
        }

        public SysCreditConfig(MySqlConnection Connection, MySqlTransaction Transaction)
            : base(Connection, Transaction)
        {

        }

        #endregion

        public string get_BILLING_DATE()
        {
            return get_SysCreditConfig(Constants.SYS_BILLING_DATE);
        }

        public string get_CREDIT_CUTOFF_DATE()
        {
            return get_SysCreditConfig(Constants.SYS_CREDIT_CUTOFF_DATE);
        }

        public string get_CREDIT_FINANCE_CHARGE()
        {
            return get_SysCreditConfig(Constants.SYS_CREDIT_FINANCE_CHARGE);
        }

        public string get_CREDIT_LATE_PENALTY_CHARGE()
        {
            return get_SysCreditConfig(Constants.SYS_CREDIT_LATE_PENALTY_CHARGE);
        }

        public string get_CRED_MIN_AMOUNT_DUE()
        {
            return get_SysCreditConfig(Constants.SYS_CONFIG_CRED_MIN_AMOUNT_DUE);
        }

        public string get_CRED_MIN_PERCENTAGE_DUE()
        {
            return get_SysCreditConfig(Constants.SYS_CONFIG_CRED_MIN_PERCENTAGE_DUE);
        }

        public string get_CREDIT_USE_LASTDAY_CUTT_OFF_DATE()
        {
            return get_SysCreditConfig(Constants.SYS_CREDIT_USE_LASTDAY_CUTT_OFF_DATE);
        }

        public SysCreditConfigDetails get_SysCreditConfigDetails()
        {
            SysCreditConfigDetails clsSysCreditConfigDetails = new SysCreditConfigDetails();

            clsSysCreditConfigDetails.BILLING_DATE = get_BILLING_DATE();
            clsSysCreditConfigDetails.CREDIT_CUTOFF_DATE = get_CREDIT_CUTOFF_DATE();
            clsSysCreditConfigDetails.CREDIT_FINANCE_CHARGE = get_CREDIT_FINANCE_CHARGE();
            clsSysCreditConfigDetails.CREDIT_LATE_PENALTY_CHARGE = get_CREDIT_LATE_PENALTY_CHARGE();
            
            clsSysCreditConfigDetails.CRED_MIN_AMOUNT_DUE = get_CRED_MIN_AMOUNT_DUE();
            clsSysCreditConfigDetails.CRED_MIN_PERCENTAGE_DUE = get_CRED_MIN_PERCENTAGE_DUE();
            clsSysCreditConfigDetails.CREDIT_USE_LASTDAY_CUTT_OFF_DATE = get_CREDIT_USE_LASTDAY_CUTT_OFF_DATE();

            return clsSysCreditConfigDetails;
        }

        private string get_SysCreditConfig(string Configname)
        {
            string stRetValue = String.Empty;

            try
            {
                string SQL = "CALL procSysCreditConfigSelectByName(@Configname)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Configname", Configname);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["ConfigValue"].ToString()))
                    {
                        stRetValue = "" + dr["ConfigValue"].ToString();
                    }
                    break;
                }

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
            return stRetValue;
        }

        public Int32 Save(SysCreditConfigDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveSysCreditConfig(@ConfigName, @ConfigValue, @Remarks, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("Configname", Details.ConfigName);
                cmd.Parameters.AddWithValue("ConfigValue", Details.ConfigValue);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }	
        }
    }
}
