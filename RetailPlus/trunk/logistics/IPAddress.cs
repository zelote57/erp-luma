using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;
using System.Net.Sockets;

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
	public struct IPAddressDetails
	{
		public string IPAddress;
		public int IPNumber;
		public string CountryCode;
		public string Country;		
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class IPAddress
	{
		public int ToIPNumber(string ipAddress)
		{
			string[] arrIPAddress = ipAddress.Split('.');
			int ipNumber = 16777216 * Convert.ToInt32(arrIPAddress[0]) + 65536 * Convert.ToInt32(arrIPAddress[1]) + 256 * Convert.ToInt32(arrIPAddress[2]) + Convert.ToInt32(arrIPAddress[3]);
			return ipNumber;
		}

		public string ToIPAddress(int ipNumber)
		{
			string w = Convert.ToString((Convert.ToInt64(ipNumber) / 16777216)  % 256);
			string x = Convert.ToString((Convert.ToInt64(ipNumber) / 65536)  % 256);
			string y = Convert.ToString((Convert.ToInt64(ipNumber) / 256)  % 256);
			string z = Convert.ToString((Convert.ToInt64(ipNumber))  % 256);
			string stIPaddress = w + "." + x + "." + y + "." + z;

			return stIPaddress;
		}

		public IPAddressDetails Details(string IPAddress)
		{
			try
			{
                SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["IPAddressConnectionString"]);
				SqlCommand myCommand = new SqlCommand("usp_IPDetails",myConnection);

				myCommand.CommandType = CommandType.StoredProcedure;
				
				int IPNumber = ToIPNumber(IPAddress);

				SqlParameter prmIPAddress = new SqlParameter("@IPAddress", SqlDbType.Float);
				prmIPAddress.Value =  IPNumber;
				myCommand.Parameters.Add(prmIPAddress);

				SqlParameter prmCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar, 10);
				prmCountryCode.Direction = ParameterDirection.Output;
				myCommand.Parameters.Add(prmCountryCode);

				SqlParameter prmCountry = new SqlParameter("@Country", SqlDbType.VarChar, 15);
				prmCountry.Direction = ParameterDirection.Output;
				myCommand.Parameters.Add(prmCountry);

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();

				IPAddressDetails Details = new IPAddressDetails();

				Details.IPAddress = IPAddress;
				Details.IPNumber = IPNumber;
				Details.CountryCode = "" + prmCountryCode.Value; 
				Details.Country = "" + prmCountry.Value;				

				return Details;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public static bool IsOpen(string IPAddress, int Port)
        {
            bool boRetValue = false;
            Socket sockRemote = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IAsyncResult synResult = sockRemote.BeginConnect(IPAddress, Port, null, null);
                boRetValue = synResult.AsyncWaitHandle.WaitOne(3000, true);
            }
            catch { boRetValue = false;  }
            finally {sockRemote.Close();}

            return boRetValue;
        }
	}
}
