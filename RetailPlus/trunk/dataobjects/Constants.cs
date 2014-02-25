using System;
using System.Collections.Generic;
using System.Text;

namespace AceSoft.RetailPlus
{
    public class DataConstants
    {
        public const long DEFAULT_PRODUCT_GROUP = 1;

        public const long MAX_PURCHASE_PRICE_SUPPLIER = 5;

    }

    public struct Constants
    {
        public const Int32 CONTACT_GROUP_CUSTOMER = 1;
        public const Int32 CONTACT_GROUP_SUPPLIER = 2;
        public const Int32 CONTACT_GROUP_BOTH = 3;
        public const Int32 CONTACT_GROUP_AGENT = 4;

        public const string C_RETAILPLUS_CUSTOMER = "RetailPlus Customer ™";
        public const Int64 C_RETAILPLUS_CUSTOMERID = 1;

        public const string C_RETAILPLUS_SUPPLIER = "RetailPlus Supplier ™";
        public const Int64 C_RETAILPLUS_SUPPLIERID = 2;

        public const string C_RETAILPLUS_WAITER = "RetailPlus Default";
        public const Int64 C_RETAILPLUS_WAITERID = 2;

        public const string C_RETAILPLUS_AGENT = "RetailPlus Agent ™";
        public const Int64 C_RETAILPLUS_AGENTID = 1;

        public const string C_RETAILPLUS_ORDER_SLIP_CUSTOMER = "ORDER SLIP";

        public const int C_RETAILPLUS_AGENT_DEPARTMENTID = 1;
        public const int C_RETAILPLUS_AGENT_POSITIONID = 1;
        public const string C_RETAILPLUS_AGENT_POSITIONNAME = "System Default Position";
        public const string C_RETAILPLUS_AGENT_DEPARTMENT_NAME = "System Default Department";

        public const int C_DEFAULT_TERMINAL_ID_01 = 1;
        public const string C_DEFAULT_TERMINAL_01 = "01";

        public const int C_DEFAULT_LIMIT_OF_RECORD_TO_SHOW = 100;

        public const int BRANCH_ID_MAIN = 1;
        public const string BRANCH_MAIN = "MAIN";
        public static int TerminalBranchID
        {
            get
            {
                int intRetValue = BRANCH_ID_MAIN;
                try { intRetValue = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BranchID"]); }
                catch { }
                return intRetValue;
            }
        }

        public static string MaskProductSearch
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["MaskProductSearch"].Replace("*", "%"); }
                catch { }
                return strRetValue;
            }
        }

        public const string ALL = "ALL";
        public const string PLEASE_SELECT = "Please Select";
        public const long ZERO = 0;
        public const string ZERO_STRING = "0";

        public static string C_DISCOUNT_CODE_SENIORCITIZEN
        {
            get
            {
                string strRetValue = "SNR"; ;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["SeniorCitizenDiscountCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public const string C_DISCOUNT_CODE_FREE = "FREE";

        public const string RETAILPLUS_BUSINESS_SOLUTIONS = "RetailPlus™ Business Solutions";
        public const string DEMO_EXPIRED_HEADER = "RetailPlus™ Demo Version";
        public const string DEMO_EXPIRED_MESSAGE = "This copy has been expired. Please contact your nearest software distributor or email <a href='mailto:sales@myretailplus.com'>sales@myretailplus.com</a>";
        public const string STOCK_TYPE_TRANSFER_TO_BRANCH_ID = "5";
        public const string STOCK_TYPE_TRANSFER_FROM_BRANCH_ID = "6";
        public const string ROOT_DIRECTORY = "/RetailPlus";
        public const string PURCHASE_ORDER_CODE = "PO-";
        public const string PURCHASE_RETURN_CODE = "PORET-";
        public const string PURCHASE_DEBITMEMO_CODE = "PODEB-";
        public const string SALES_ORDER_CODE = "SO-";
        public const string SALES_RETURN_CODE = "SORET-";
        public const string SALES_CREDITMEMO_CODE = "SOCRE-";
        public const string TRANSFER_IN_CODE = "TXI-";
        public const string TRANSFER_OUT_CODE = "TXO-";
        public const string INVENTORY_ADJUSTMENT_CODE = "INADJ-";
        public const string BRANCH_TRANSFER_CODE = "BX-";
        public const decimal DEFAULTS_VAT = 12;

        public const string CLOSE_INVENTORY_CODE = "CINV-";
        public const string CLOSE_INVENTORY_FILE_NAME_CODE = "CLOSE-INV";
        public const string CLOSE_INVENTORY_SHEET_NAME = "INVENTORY";
        public const string CLOSE_INVENTORY_SHEET_NAME_QUANTITY_ERROR = "QUANTITY_ERROR";
        public const string CLOSE_INVENTORY_SHEET_NAME_INVALID_PRODUCT = "INVALID_PRODUCT";

        public const string ERROR = "ERROR";

        public const string SWIPE_REWARD_CARD = "SWIPE REWARD or CREDIT CARD:";

        public const int C_RESTOPLUS_MAX_SUB_GROUP = 7;
        public const int C_RESTOPLUS_MAX_PRODUCTS = 20;
        public const int C_RESTOPLUS_MAX_TABLES = 20;
        public const string C_RESTOPLUS = "RestoPlus ™";
        public const string C_RESTOPLUS_CUSTOMER_ORDERS = "CUSTOMER ORDERS";

        public const string C_FE_DEFAULT_DECIMAL_FORMAT = "#,##0.##0";

        public const string C_FE_NOT_VALID_AS_RECEIPT = "NOT VALID AS RECEIPT";

        public enum DateSelectionString
        {
            ALL = 0,
            Today = 1,
            NextMonth = 2,
            CurrentMonth = 3,
            PreviousMonth = 4,
        }

        public const Int32 C_DEFAULT_MYSQL_PROCESS_TIMEOUT = 90;

        public static DateTime C_DATE_MIN_VALUE = Convert.ToDateTime("1900-01-01");
        public const string C_DATE_MIN_VALUE_STRING = "1900-01-01";

        public const string SYS_CONFIG_BACKEND_VARIATION_TYPE = "BACKEND_VARIATION_TYPE";
        public const string SYS_CONFIG_BACKEND_VARIATION_TYPE_EXPIRATION_LOTNO = "EXPIRATION;LOTNO";
        public const string SYS_CONFIG_COMPANY_CODE = "CompanyCode";
        public const string SYS_CONFIG_COMPANY_NAME = "CompanyName";
        public const string SYS_CONFIG_CURRENCY = "Currency";
        public const string SYS_CONFIG_TIN = "TIN";
        public const string SYS_CONFIG_VERSION_FTP_IPADDRESS = "VersionFTPIPAddress";
        public const string SYS_CONFIG_CHECK_OUT_BILL_HEADER_LABEL = "CheckOutBillHeaderLabel";
        public const string SYS_CONFIG_CHARGE_SLIP_HEADER_LABEL = "ChargeSlipHeaderLabel";
        public const string SYS_CONFIG_WILL_PRINT_CREDITPAYMENT_HEADER = "WillPrintCreditPaymentHeader";
        public const string SYS_CONFIG_WILL_WRITE_SYSTEM_LOG = "WillWriteSystemLog";
        public const string SYS_CONFIG_WILL_DEDUCT_TF_IN_XREAD = "WillDeductTFInXRead";
        public const string SYS_CONFIG_WILL_DEDUCT_TF_IN_ZREAD = "WillDeductTFInZRead";
        public const string SYS_CONFIG_WILL_DEDUCT_TF_IN_TERMINAL_REPORT = "WillDeductTFInTerminalReport";
        public const string SYS_CONFIG_WILL_ASK_DO_NOT_PRINT_TRANSACTIONDATE = "WillAskDoNotPrintTransactionDate";
        public const string SYS_CONFIG_WILL_SHOW_PRODUCT_TOTAL_QUANTITY_IN_ITEMSELECT = "WillShowProductTotalQuantityInItemSelect";
        public const string SYS_CONFIG_WILL_NOT_PRINT_REPRINT_MESSAGE = "WillNotPrintReprintMessage";
        public const string SYS_CONFIG_OR_HEADER = "ORHeader";

        /// <summary>
        /// GLA Files
        /// </summary>
        public const string GLA_file_batch_id = "batch_id.dat";
        public const string GLA_file_d_dsc_def = "d_dsc_def.dat";
        public const string GLA_file_d_emp_def = "d_emp_def.dat";
        public const string GLA_file_d_location_def = "d_location_def.dat";
        public const string GLA_file_d_mi_def = "d_mi_def.dat";
        public const string GLA_file_d_svc_def = "d_svc_def.dat";
        public const string GLA_file_d_tmd_def = "d_tmd_def.dat";
        public const string GLA_file_f_dtl_chk_dsc = "f_dtl_chk_dsc.dat";
        public const string GLA_file_f_dtl_chk_headers = "f_dtl_chk_headers.dat";
        public const string GLA_file_f_dtl_chk_mi = "f_dtl_chk_mi.dat";
        public const string GLA_file_f_dtl_chk_svc = "f_dtl_chk_svc.dat";
        public const string GLA_file_f_dtl_chk_tmd = "f_dtl_chk_tmd.dat";
        public const string GLA_file_otntender = "otn_tender.txt";
        
    }
}
