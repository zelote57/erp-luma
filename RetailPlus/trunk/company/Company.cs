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
		public static string CompanyCode		=	System.Configuration.ConfigurationManager.AppSettings["CompanyCode"].ToString();
		public static string CompanyName		=	System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString();
		public static string Address1			=	System.Configuration.ConfigurationManager.AppSettings["Address1"].ToString();
		public static string Address2			=	System.Configuration.ConfigurationManager.AppSettings["Address2"].ToString();
		public static string City				=	System.Configuration.ConfigurationManager.AppSettings["City"].ToString();
		public static string State				=	System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
		public static string Zip				=	System.Configuration.ConfigurationManager.AppSettings["Zip"].ToString();
		public static string Country			=	System.Configuration.ConfigurationManager.AppSettings["Country"].ToString();
		public static string OfficePhone		=	System.Configuration.ConfigurationManager.AppSettings["OfficePhone"].ToString();
		public static string DirectPhone		=	System.Configuration.ConfigurationManager.AppSettings["DirectPhone"].ToString();
		public static string FaxPhone			=	System.Configuration.ConfigurationManager.AppSettings["FaxPhone"].ToString();
		public static string MobilePhone		=	System.Configuration.ConfigurationManager.AppSettings["MobilePhone"].ToString();
		public static string EmailAddress		=	System.Configuration.ConfigurationManager.AppSettings["EmailAddress"].ToString();
		public static string WebSite			=	System.Configuration.ConfigurationManager.AppSettings["WebSite"].ToString();
		public static string TIN				=	System.Configuration.ConfigurationManager.AppSettings["TIN"].ToString();
		public const string DateCreated			=	"15/08/2006";
		public static string TerminalNo			=	System.Configuration.ConfigurationManager.AppSettings["TerminalNo"].ToString();
		public static string Currency			=	System.Configuration.ConfigurationManager.AppSettings["Currency"].ToString();
	}
}

