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

    public struct SysConfigDetails
    {
        public string BACKEND_VARIATION_TYPE;
        public string BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO;
        public string CompanyCode;
        public string CompanyName;
        
        public string Currency;
        public string TIN;
        public string VersionFTPAddress;

        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public string Zip;
        public string Country;
        public string OfficePhone;
        public string DirectPhone;
        public string FaxPhone;
        public string MobilePhone;
        public string EmailAddress;
        public string WebSite;

        public string CheckOutBillHeaderLabel;
        public string ChargeSlipHeaderLabel;

        public bool WillPrintCreditPaymentHeader;
        public bool WillWriteSystemLog;

        public bool WillDeductTFInXRead;
        public bool WillDeductTFInZRead;
        public bool WillDeductTFInTerminalReport;

        public bool WillAskDoNotPrintTransactionDate;

    }
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
            return get_Sysconfig(Constants.SYS_CONFIG_BACKEND_VARIATION_TYPE);
        }

        public string get_BackendVariationTypeExpirationLotNo()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO);
        }

        #endregion

        public bool get_WillPrintCreditPaymentHeader()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_PRINT_CREDITPAYMENT_HEADER));
            }
            catch { }
            return boRetValue;
        }
        public bool get_WillWriteSystemLog()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_WRITE_SYSTEM_LOG));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillDeductTFInXRead()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_DEDUCT_TF_IN_XREAD));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillDeductTFInZRead()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_DEDUCT_TF_IN_ZREAD));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillDeductTFInTerminalReport()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_DEDUCT_TF_IN_TERMINAL_REPORT));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillAskDoNotPrintTransactionDate()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_ASK_DO_NOT_PRINT_TRANSACTIONDATE));
            }
            catch { }
            return boRetValue;
        }


        public string get_CompanyCode()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_COMPANY_CODE);
        }

        public string get_CompanyName()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_COMPANY_NAME);
        }

        public string get_Currency()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_CURRENCY);
        }

        public string get_TIN()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_TIN);
        }

        public string get_VersionFTPIPAddress()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_VERSION_FTP_IPADDRESS);
        }

        public string get_CheckOutBillHeaderLabel()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_CHECK_OUT_BILL_HEADER_LABEL);
        }

        public string get_ChargeSlipHeaderLabel()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_CHARGE_SLIP_HEADER_LABEL);
        }

        public SysConfigDetails get_SysConfigDetails()
        {
            SysConfigDetails clsSysConfigDetails = new SysConfigDetails();
            
            clsSysConfigDetails.BACKEND_VARIATION_TYPE = get_BackendVariationType();
            clsSysConfigDetails.BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO = get_BackendVariationTypeExpirationLotNo();
            clsSysConfigDetails.CompanyCode = get_CompanyCode();
            clsSysConfigDetails.CompanyName = get_CompanyName();
            
            clsSysConfigDetails.Currency = get_Currency();
            clsSysConfigDetails.TIN = get_TIN();
            clsSysConfigDetails.VersionFTPAddress = get_VersionFTPIPAddress();

            clsSysConfigDetails.CheckOutBillHeaderLabel = get_CheckOutBillHeaderLabel();
            clsSysConfigDetails.ChargeSlipHeaderLabel = get_ChargeSlipHeaderLabel();
            clsSysConfigDetails.WillPrintCreditPaymentHeader = get_WillPrintCreditPaymentHeader();
            clsSysConfigDetails.WillWriteSystemLog = get_WillWriteSystemLog();

            clsSysConfigDetails.WillDeductTFInXRead = get_WillDeductTFInXRead();
            clsSysConfigDetails.WillDeductTFInZRead = get_WillDeductTFInZRead();
            clsSysConfigDetails.WillDeductTFInTerminalReport = get_WillDeductTFInTerminalReport();
            clsSysConfigDetails.WillAskDoNotPrintTransactionDate = get_WillAskDoNotPrintTransactionDate();

            return clsSysConfigDetails;
        }

        private string get_Sysconfig(string Configname)
        {
            string stRetValue = String.Empty;

            try
            {
                string SQL = "CALL procSysConfigSelectByName(@Configname)";

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

        
    }
}
