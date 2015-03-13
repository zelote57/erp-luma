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
        public string ConfigName;
        public string ConfigValue;
        public string Category;
        public DateTime CreatedOn;
        public DateTime LastModified;

        public string BACKEND_VARIATION_TYPE;
        public string BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO;
        public string BECompanyCode;
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
        public string CreditVerificationSlipHeaderLabel;

        public bool WillPrintCreditPaymentHeader;
        public bool WillWriteSystemLog;

        public bool WillDeductTFInXRead;
        public bool WillDeductTFInZRead;
        public bool WillDeductTFInTerminalReport;

        public bool WillAskDoNotPrintTransactionDate;

        public bool WillShowProductTotalQuantityInItemSelect;
        public bool WillNotPrintReprintMessage;

        public bool WillDepositChangeOfCreditPayment;
        public CreditPaymentType CreditPaymentType;

        public bool AllowDebitPayment;
        public bool AllowRewardPointsPayment;
        public bool AllowDiscountGreaterThanAmount;

        public bool isDefaultButtonYesInPrintTransaction;
        public bool AllowZeroAmountTransaction;
        public bool AllowMoreThan1ItemIfConsignment;

        public bool WillProcessCreditBillerInProgram;
        public bool WillConvertWeightMeasurementTo1InQtySold;
        public string WeightMeasurement;

        public string OutOfStockCustomerCode;
        public string WalkInCustomerCode;

        public Int32 ChequePaymentAcceptableNoOfDays;

        public bool EnablePriceLevel;

        public string ORHeader;

        
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

        /// <summary>
        /// obsolete: this is always true
        /// </summary>
        /// <returns></returns>
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

        public bool get_WillShowProductTotalQuantityInItemSelect()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_SHOW_PRODUCT_TOTAL_QUANTITY_IN_ITEMSELECT));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillNotPrintReprintMessage()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_NOT_PRINT_REPRINT_MESSAGE));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillDepositChangeOfCreditPayment()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_DEPOSIT_CHANGE_OF_CREDIT_PAYMENT));
            }
            catch { }
            return boRetValue;
        }

        public bool get_AllowDebitPayment()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ALLOW_DEBIT_PAYMENT));
            }
            catch { }
            return boRetValue;
        }
        
        public bool get_AllowRewardPointsPayment()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ALLOW_REWARD_POINTS_PAYMENT));
            }
            catch { }
            return boRetValue;
        }

        public bool get_AllowDiscountGreaterThanAmount()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ALLOW_DISCOUNT_GREATER_THAN_AMOUNT));
            }
            catch { }
            return boRetValue;
        }
        
        public bool get_AllowZeroAmountTransaction()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ALLOW_ZERO_AMOUNT_TRANSACTION));
            }
            catch { }
            return boRetValue;
        }

        public bool get_AllowMoreThan1ItemIfConsignment()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ALLOW_MORE_THAN_1ITEM_IF_CONSIGNMENT));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillProcessCreditBillerInProgram()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_PROCESS_CREDIT_BILLER_IN_PROGRAM));
            }
            catch { }
            return boRetValue;
        }

        public bool get_WillConvertWeightMeasurementTo1InQtySold()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_WILL_CONVERT_WEIGHT_MEASUREMENT_TO1_IN_QTY_SOLD));
            }
            catch { }
            return boRetValue;
        }

        public string get_WeightMeasurement()
        {
            string strRetValue = "GAL,GALLON,KL,KILO,KG,KILOGRAM,GRM,GRAM,GRAMS";
            try
            {
                strRetValue = get_Sysconfig(Constants.SYS_CONFIG_WILL_CONVERT_WEIGHT_MEASUREMENT);
            }
            catch { }
            return strRetValue;
        }
        

        public bool get_isDefaultButtonYesInPrintTransaction()
        {
            bool boRetValue = true;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_IS_DEFAULT_BUTTON_YES_INPRINTTRANSACTION));
            }
            catch { }
            return boRetValue;
        }

        public CreditPaymentType get_CreditPaymentType()
        {
            CreditPaymentType clsCreditPaymentType = CreditPaymentType.Normal;
            try
            {
                clsCreditPaymentType = (CreditPaymentType) Enum.Parse(typeof(CreditPaymentType), get_Sysconfig(Constants.SYS_CONFIG_CREDIT_PAYMENT_TYPE));
            }
            catch { }
            return clsCreditPaymentType;
        }

        public string get_OutOfStockCustomerCode()
        {
            string strRetValue = "OUT OF STOCK";
            try
            {
                strRetValue = get_Sysconfig(Constants.SYS_CONFIG_OUT_OF_STOCK_CUSTOMER_CODE);
            }
            catch { }
            return strRetValue;
        }

        public string get_WalkInCustomerCode()
        {
            string strRetValue = "WALK-IN";
            try
            {
                strRetValue = get_Sysconfig(Constants.SYS_CONFIG_WALKIN_CUSTOMER_CODE);
            }
            catch { }
            return strRetValue;
        }

        public Int32 get_ChequePaymentAcceptableNoOfDays()
        {
            Int32 iRetValue = 180;
            try
            {
                iRetValue = Int32.TryParse(get_Sysconfig(Constants.SYS_CONFIG_CHEQUE_PAYMENT_ACCEPTABLE_NO_OF_DAYS), out iRetValue) ? iRetValue : 180;
            }
            catch { }
            return iRetValue;
        }

        public bool get_EnablePriceLevel()
        {
            bool boRetValue = false;
            try
            {
                boRetValue = bool.Parse(get_Sysconfig(Constants.SYS_CONFIG_ENABLE_PRICE_LEVEL));
            }
            catch { }
            return boRetValue;
        }

        public string get_ORHeader()
        {
            string strRetValue = "SALES INVOICE";
            try
            {
                strRetValue = get_Sysconfig(Constants.SYS_CONFIG_OR_HEADER);
            }
            catch { }
            return strRetValue;
        }

        public string get_BECompanyCode()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_BE_COMPANY_CODE);
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
        public string get_CreditVerificationSlipHeaderLabel()
        {
            return get_Sysconfig(Constants.SYS_CONFIG_CHARGE_SLIP_HEADER_LABEL);
        }
        

        public SysConfigDetails get_SysConfigDetails()
        {
            SysConfigDetails clsSysConfigDetails = new SysConfigDetails();
            
            clsSysConfigDetails.BACKEND_VARIATION_TYPE = get_BackendVariationType();
            clsSysConfigDetails.BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO = get_BackendVariationTypeExpirationLotNo();
            clsSysConfigDetails.BECompanyCode = get_BECompanyCode();
            clsSysConfigDetails.CompanyCode = get_CompanyCode();
            clsSysConfigDetails.CompanyName = get_CompanyName();
            
            clsSysConfigDetails.Currency = get_Currency();
            clsSysConfigDetails.TIN = get_TIN();
            clsSysConfigDetails.VersionFTPAddress = get_VersionFTPIPAddress();

            clsSysConfigDetails.CheckOutBillHeaderLabel = get_CheckOutBillHeaderLabel();
            clsSysConfigDetails.ChargeSlipHeaderLabel = get_ChargeSlipHeaderLabel();
            clsSysConfigDetails.CreditVerificationSlipHeaderLabel = get_CreditVerificationSlipHeaderLabel();
            clsSysConfigDetails.WillPrintCreditPaymentHeader = get_WillPrintCreditPaymentHeader();
            clsSysConfigDetails.WillWriteSystemLog = get_WillWriteSystemLog();

            clsSysConfigDetails.WillDeductTFInXRead = get_WillDeductTFInXRead();
            clsSysConfigDetails.WillDeductTFInZRead = get_WillDeductTFInZRead();
            clsSysConfigDetails.WillDeductTFInTerminalReport = get_WillDeductTFInTerminalReport();
            clsSysConfigDetails.WillAskDoNotPrintTransactionDate = get_WillAskDoNotPrintTransactionDate();
            clsSysConfigDetails.WillShowProductTotalQuantityInItemSelect = get_WillShowProductTotalQuantityInItemSelect();
            clsSysConfigDetails.WillNotPrintReprintMessage = get_WillNotPrintReprintMessage();
            clsSysConfigDetails.WillDepositChangeOfCreditPayment = get_WillDepositChangeOfCreditPayment();
            clsSysConfigDetails.CreditPaymentType = get_CreditPaymentType();
            clsSysConfigDetails.AllowDebitPayment = get_AllowDebitPayment();
            clsSysConfigDetails.AllowRewardPointsPayment = get_AllowRewardPointsPayment();
            clsSysConfigDetails.AllowDiscountGreaterThanAmount= get_AllowDiscountGreaterThanAmount();
            clsSysConfigDetails.AllowZeroAmountTransaction = get_AllowZeroAmountTransaction();
            clsSysConfigDetails.AllowMoreThan1ItemIfConsignment = get_AllowMoreThan1ItemIfConsignment();
            clsSysConfigDetails.WillProcessCreditBillerInProgram = get_WillProcessCreditBillerInProgram();
            clsSysConfigDetails.WillConvertWeightMeasurementTo1InQtySold = get_WillConvertWeightMeasurementTo1InQtySold();
            clsSysConfigDetails.WeightMeasurement = get_WeightMeasurement();
            clsSysConfigDetails.isDefaultButtonYesInPrintTransaction = get_isDefaultButtonYesInPrintTransaction();
            clsSysConfigDetails.OutOfStockCustomerCode = get_OutOfStockCustomerCode();
            clsSysConfigDetails.WalkInCustomerCode = get_WalkInCustomerCode();
            clsSysConfigDetails.ChequePaymentAcceptableNoOfDays = get_ChequePaymentAcceptableNoOfDays();
            clsSysConfigDetails.EnablePriceLevel = get_EnablePriceLevel();
            clsSysConfigDetails.ORHeader = get_ORHeader();

            return clsSysConfigDetails;
        }

        private string get_Sysconfig(string Configname)
        {
            string stRetValue = String.Empty;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSysConfigSelectByName(@Configname)";

                cmd.Parameters.AddWithValue("@Configname", Configname);

                cmd.CommandText = SQL;
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

        public Int32 Save(SysConfigDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveSysConfig(@ConfigName, @ConfigValue, @Category, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("Configname", Details.ConfigName);
                cmd.Parameters.AddWithValue("ConfigValue", Details.ConfigValue);
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
    }
}
