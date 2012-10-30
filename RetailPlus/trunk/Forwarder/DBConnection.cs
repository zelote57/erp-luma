using System;
using System.Configuration;
using System.Security.Permissions;

namespace AceSoft.RetailPlus.Forwarder
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class DBConnection
	{
		static DBConnection()
		{

		}

		public static string DBFConnectionString(string stDataSource)
		{
			return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + stDataSource + ";Extended Properties=dBASE 5.0;User ID=Admin;Password=";
		}
        public static string TEXTConnectionString(string stDataSource)
        {
            return @"DRIVER=Microsoft Text Driver (*.txt; *.csv);UID=admin;UserCommitSync=Yes;Threads=3;SafeTransactions=0;PageTimeout=5;MaxScanRows=8;MaxBufferSize=2048;FIL=text;DriverId=27;DefaultDir=" + stDataSource;
        } 
	}
}
