using System;
using System.Security.Permissions;

namespace AceSoft
{
	
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class DateClass
	{
		public static string MonthName(int Month, bool Abbreviate)
		{
			string Name = "";
			switch (Month) 
			{
				case 1: Name = "January"; break;
				case 2: Name =  "February"; break;
				case 3: Name =  "March"; break;
				case 4: Name =  "April"; break;
				case 5: Name =  "May"; break;
				case 6: Name =  "June"; break;
				case 7: Name =  "July"; break;
				case 8: Name =  "August"; break;
				case 9: Name =  "September"; break;
				case 10: Name =  "October"; break;
				case 11: Name =  "November"; break;
				case 12: Name =  "December"; break;
				default: Name = "Not applicable"; break;
			}
			if (Abbreviate==true)
			{
				Name = Name.Substring(0,3);
			}

			return Name;
		}
	}
}
