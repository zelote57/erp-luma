using System;
using System.Security.Permissions;

namespace AceSoft.RetailPlus
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
    public struct CompanyDetails
	{
        private static string _BECompanyCode;
        public static string BECompanyCode
        {
            get
            {
                if (string.IsNullOrEmpty(_BECompanyCode))
                    if (System.Configuration.ConfigurationManager.AppSettings["BECompanyCode"] != null)
                        try { BECompanyCode = System.Configuration.ConfigurationManager.AppSettings["BECompanyCode"].ToString(); }
                        catch { }

                return _BECompanyCode;
            }
            set { _BECompanyCode = value; }
        }

        private static string _CompanyCode;
        public static string CompanyCode
        {
            get
            {
                if (string.IsNullOrEmpty(_CompanyCode))
                    if (System.Configuration.ConfigurationManager.AppSettings["CompanyCode"] != null)
                        try { CompanyCode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"].ToString(); }
                        catch { }

                return _CompanyCode;
            }
            set { _CompanyCode = value; }
        }

        private static string _CompanyName;
        public static string CompanyName
        {
            get
            {
                if (string.IsNullOrEmpty(_CompanyName))
                    if (System.Configuration.ConfigurationManager.AppSettings["CompanyCode"] != null)
                        try { _CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString(); }
                        catch { }

                return _CompanyName;
            }
            set { _CompanyName = value; }
        }

        private static string _Address1;
        public static string Address1
        {
            get
            {
                if (string.IsNullOrEmpty(_Address1))
                    try { _Address1 = System.Configuration.ConfigurationManager.AppSettings["Address1"].ToString(); }
                    catch { }

                return _Address1;
            }
            set { _Address1 = value; }
        }

        private static string _Address2;
        public static string Address2
        {
            get
            {
                if (string.IsNullOrEmpty(_Address2))
                    try { _Address2 = System.Configuration.ConfigurationManager.AppSettings["Address2"].ToString(); }
                    catch { }

                return _Address2;
            }
            set { _Address2 = value; }
        }

        private static string _City;
        public static string City
        {
            get
            {
                if (string.IsNullOrEmpty(_City))
                    try { _City = System.Configuration.ConfigurationManager.AppSettings["City"].ToString(); }
                    catch { }

                return _City;
            }
            set { _City = value; }
        }

        private static string _State;
        public static string State
        {
            get
            {
                if (string.IsNullOrEmpty(_State))
                    try { _State = System.Configuration.ConfigurationManager.AppSettings["State"].ToString(); }
                    catch { }

                return _State;
            }
            set { _State = value; }
        }

        private static string _Zip;
        public static string Zip
        {
            get
            {
                if (string.IsNullOrEmpty(_Zip))
                    try { _Zip = System.Configuration.ConfigurationManager.AppSettings["Zip"].ToString(); }
                    catch { }

                return _Zip;
            }
            set { _Zip = value; }
        }

        private static string _Country;
        public static string Country
        {
            get
            {
                if (string.IsNullOrEmpty(_Country))
                    try { _Country = System.Configuration.ConfigurationManager.AppSettings["Country"].ToString(); }
                    catch { }

                return _Country;
            }
            set { _Country = value; }
        }

        private static string _OfficePhone;
        public static string OfficePhone
        {
            get
            {
                if (string.IsNullOrEmpty(_OfficePhone))
                    try { _OfficePhone = System.Configuration.ConfigurationManager.AppSettings["OfficePhone"].ToString(); }
                    catch { }

                return _OfficePhone;
            }
            set { _OfficePhone = value; }
        }

        private static string _DirectPhone;
        public static string DirectPhone
        {
            get
            {
                if (string.IsNullOrEmpty(_DirectPhone))
                    try { _DirectPhone = System.Configuration.ConfigurationManager.AppSettings["DirectPhone"].ToString(); }
                    catch { }

                return _DirectPhone;
            }
            set { _DirectPhone = value; }
        }

        private static string _FaxPhone;
        public static string FaxPhone
        {
            get
            {
                if (string.IsNullOrEmpty(_FaxPhone))
                    try { _FaxPhone = System.Configuration.ConfigurationManager.AppSettings["FaxPhone"].ToString(); }
                    catch { }

                return _FaxPhone;
            }
            set { _FaxPhone = value; }
        }

        private static string _MobilePhone;
        public static string MobilePhone
        {
            get
            {
                if (string.IsNullOrEmpty(_MobilePhone))
                    try { _MobilePhone = System.Configuration.ConfigurationManager.AppSettings["MobilePhone"].ToString(); }
                    catch { }

                return _MobilePhone;
            }
            set { _MobilePhone = value; }
        }

        private static string _EmailAddress;
        public static string EmailAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_EmailAddress))
                    try { _EmailAddress = System.Configuration.ConfigurationManager.AppSettings["EmailAddress"].ToString(); }
                    catch { }

                return _EmailAddress;
            }
            set { _EmailAddress = value; }
        }

        private static string _WebSite;
        public static string WebSite
        {
            get
            {
                if (string.IsNullOrEmpty(_WebSite))
                    try { _WebSite = System.Configuration.ConfigurationManager.AppSettings["WebSite"].ToString(); }
                    catch { }

                return _WebSite;
            }
            set { _WebSite = value; }
        }

        private static string _TIN;
        public static string TIN
        {
            get
            {
                if (string.IsNullOrEmpty(_TIN))
                    try { _TIN = System.Configuration.ConfigurationManager.AppSettings["TIN"].ToString(); }
                    catch { }

                return _TIN;
            }
            set { _TIN = value; }
        }
        
		public const string DateCreated			=	"15/08/2006";

        private static string _TerminalNo;
        public static string TerminalNo
        {
            get
            {
                if (string.IsNullOrEmpty(_TerminalNo))
                    try { _TerminalNo = System.Configuration.ConfigurationManager.AppSettings["TerminalNo"].ToString(); }
                    catch { }

                return _TerminalNo;
            }
            set { _TerminalNo = value; }
        }

        private static string _Currency;
        public static string Currency
        {
            get
            {
                if (string.IsNullOrEmpty(_Currency))
                    try { _Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"].ToString(); }
                    catch { }

                return _Currency;
            }
            set { _Currency = value; }
        }

	}
}

