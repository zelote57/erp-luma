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
	public class Common
	{
		public static string Encrypt(string Value, string Encryptor)
		{
			string newValue = "";				
			if (Value==null || Value==string.Empty)
			{
				newValue = Encryptor;
			}
			else
			{
				Value = Value.Trim();
				for (int i=0; i<Value.Length; i++)
				{
					if (Encryptor.Length>i)
					{
						newValue +=  Encryptor.Substring(i,1) + Value.Substring(i,1);
					}
					else
					{
						Encryptor += Encryptor;
						newValue +=  Encryptor.Substring(i,1) + Value.Substring(i,1);
						//newValue +=  Value.Substring(i,1);
					}
					
				}
			}
			return newValue;
		}

		public static string Decrypt(string Value, string Encryptor)
		{
			string newValue = "";			
			if (Value==Encryptor)
			{
				newValue = null;
			}
			else
			{
				Value = Value.Trim();
				for (int i=0; i < Value.Length; i++)
				{
					if (Encryptor.Length>i)
					{
						if ((i%2) == 1)
						{
							newValue += Value.Substring(i,1);
						}
					}
					else
					{
						Encryptor += Encryptor;
						if ((i%2) == 1)
						{
							newValue += Value.Substring(i,1);	
						}
					}
				}
			}
			return newValue;
		}

        [System.Runtime.InteropServices.DllImport("kernel32 ", SetLastError = true)]
        public static extern bool GetSystemTime(out SYSTEMTIME systemTime);
        [System.Runtime.InteropServices.DllImport("kernel32 ", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME systemTime);

        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        public static SYSTEMTIME ConvertToSystemTime(DateTime DateTimeToConvert)
        {
            SYSTEMTIME st = new SYSTEMTIME();

            st.wYear = (short) DateTimeToConvert.Year;
            st.wMonth = (short) DateTimeToConvert.Month;
            st.wDay = (short) DateTimeToConvert.Day;
            st.wHour = (short) DateTimeToConvert.Hour;
            st.wMinute = (short) DateTimeToConvert.Minute;
            st.wSecond = (short) DateTimeToConvert.Second;
            st.wMilliseconds = (short) DateTimeToConvert.Millisecond;

            return st;
        }

        public static string ToShortTimeString(DateTime DateToFormat)
        {
            string strRetValue = string.Empty;
            strRetValue = DateToFormat.Hour.ToString() + "-" + DateToFormat.Minute.ToString();
            return strRetValue;
        }

        public static string ToLongTimeString(DateTime DateToFormat)
        {
            string strRetValue = string.Empty;
            strRetValue = DateToFormat.Hour.ToString() + ":" + DateToFormat.Minute.ToString() + ":" + DateToFormat.Second.ToString(); ;
            return strRetValue;
        }

        public static string ToShortDateString(DateTime DateToFormat)
        {
            string strRetValue = string.Empty;
            strRetValue = DateToFormat.Year.ToString() + "-" + DateToFormat.Month.ToString() + "-" + DateToFormat.Day.ToString();
            return strRetValue;
        }

        public static string ToLongDateString(DateTime DateToFormat)
        {
            string strRetValue = string.Empty;
            strRetValue = DateToFormat.Year.ToString() + "-" + DateToFormat.Month.ToString() + "-" + DateToFormat.Day.ToString() + " " + DateToFormat.Hour.ToString() + ":" + DateToFormat.Minute.ToString();
            return strRetValue;
        }
	}
}
