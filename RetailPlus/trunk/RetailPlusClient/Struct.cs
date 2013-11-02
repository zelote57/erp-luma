using System;

namespace AceSoft.RetailPlus.Client
{
	public struct DenominationDetails
	{
		public int DenominationID;
		public int CashierID;
		public string CashierName;
		public string TerminalNo;
		public DateTime DenominationDate;
		public int OneThousandPesos;
		public int FiveHundredPesos;
		public int OneHundredPesos;
		public int FiftyPesos;
		public int TwentyPesos;
		public int TenPesos;
		public int FivePesos;
		public int OnePeso;
		public int TwentyFiveCents;
		public decimal Others;
	}

	public enum ReportTypes
	{
		TerminalReport = 1,
		CashierReport = 2,
		XReadReport = 3,
		CashierHourlyReport = 4,
		DeptSalesReport = 5,
		Receipts = 6
	}

    public struct MallCodes
    {
        public const string NA = "NA";
        public const string AYALA = "AYALA";
        public const string RLC = "RLC";
        public const string FSI = "FSI";
    }

	public struct FORM_Behavior
	{
		public static string MODAL	=	"MODAL";
		public static string NON_MODAL = "NON_MODAL";
	}
	public struct CONFIG
	{
        public static string VersionXML
        {
            get
            {
                string strRetValue = "http://" + AceSoft.RetailPlus.DBConnection.ServerIP() + "/RetailPlus/version.xml";;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["VersionXML"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

        public static string MachineSerialNo
        {
            get
            {
                string strRetValue = "000000";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["MachineSerialNo"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string AccreditationNo
        {
            get
            {
                string strRetValue = "00000000000000000";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["AccreditationNo"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

        public static string MallCode
        {
            get
            {
                string strRetValue = "NA";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["MallCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

        public static int FTPAutoResendInterval
        {
            get
            {
                int intRetValue = 1000 * 30;
                try { intRetValue = 1000 * int.Parse(System.Configuration.ConfigurationManager.AppSettings["FTPAutoResendInterval"]); }
                catch { }
                if (intRetValue == 0) intRetValue = 1000 * 30;
                return intRetValue;
            }
        }

        public static string FTPIPAddress
        {
            get
            {
                string strRetValue = null;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["FTPIPAddress"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string FTPUsername
        {
            get
            {
                string strRetValue = null;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["FTPUsername"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string FTPPassword
        {
            get
            {
                string strRetValue = null;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["FTPPassword"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string FTPDirectory
        {
            get
            {
                string strRetValue = null;
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["FTPDirectory"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

        public static bool ShowBarcodeNotProductCodeItemSelect
        {
            get
            {
                bool bolRetValue = true;
                try { bolRetValue = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ShowBarcodeNotProductCodeItemSelect"])); }
                catch {
                    try { bolRetValue = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ShowBarcodeNotProductCodeItemSelect"]); }
                    catch { }
                }
                return bolRetValue;
            }
        }

        public static bool ShowDescriptionDuringItemSelect
        {
            get
            {
                bool bolRetValue = false;
                try { bolRetValue = Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ShowDescriptionDuringItemSelect"])); }
                catch {
                    try { bolRetValue = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ShowDescriptionDuringItemSelect"]); }
                    catch { }
                }
                return bolRetValue;
            }
        }

	}

    public struct AYALA_CONFIG
    {
        public static string TenantCode
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string TenantName
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantName"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string OutputDirectory
        {
            get
            {
                string strRetValue = "/RLC/{YYYY}";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["OutputDirectory"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

    }
    public struct RLC_CONFIG
    {
        public static string TenantCode
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string TenantName
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantName"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string OutputDirectory
        {
            get
            {
                string strRetValue = "/RLC/{YYYY}";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["OutputDirectory"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        
    }
    public struct FSI_CONFIG
    {
        public static string TenantCode
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string TenantName
        {
            get
            {
                string strRetValue = "";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["TenantName"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string OutputDirectory
        {
            get
            {
                string strRetValue = "/FSI/{YYYY}";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["OutputDirectory"].ToString(); }
                catch { }
                return strRetValue;
            }
        }
        public static string SalesTypeCode
        {
            get
            {
                string strRetValue = "FO";
                try { strRetValue = System.Configuration.ConfigurationManager.AppSettings["SalesTypeCode"].ToString(); }
                catch { }
                return strRetValue;
            }
        }

    }
}
