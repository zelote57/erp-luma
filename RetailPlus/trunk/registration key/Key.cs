using System;
using System.Management;
using System.Diagnostics;

namespace AceSoft.RetailPlus
{
	/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: May 21, 2006
	*******************************************************************************
	**		Change History
	*******************************************************************************
	**		Date:		Author:				Description:
	**		--------		--------				-------------------------------------------
	**    
	*******************************************************************************/

	/// <summary>
	/// Summary description for Key.
	/// </summary>
	public class Key
	{
		public Key()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public RegistrationType IsDemoExpired(out string SerialNumber)
		{
			SerialNumber = null;
			try
			{
				RegistrationType regIsDemoExpired = RegistrationType.DEMO_Unexpired;

                SerialNumber = GetHDSerialNo();

                if (SerialNumber == "W50SJSHQ" || SerialNumber == "K834T9A2BJNB" || SerialNumber == "K10HT77258WN" || SerialNumber == "587OCI98T" || SerialNumber == "MRG3W9KKH9S45H" || SerialNumber == "9546305796063968058" || SerialNumber == "9VP7QL84")
                {
                    // K10HT77258WN - Lemuel
                    // 587OCI98T - Darius
                    // MRG3W9KKH9S45H - Rico
                    // 9546305796063968058 - Grace
                    // 9VP7QL84 - Lemuel
                    // WD-WXTY08TPJ153
                    return RegistrationType.Registered;
                }

				try
				{
                    Data.Terminal clsTerminal = new Data.Terminal();
                    string cipherText = clsTerminal.getTerminalKey(SerialNumber);
                    clsTerminal.CommitAndDispose();

                    if (cipherText != string.Empty)
                    {
                        string plainText = CompanyDetails.CompanyCode + SerialNumber.ToString().Trim();    // original plaintext
                        //string  cipherText = System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"].ToString();	// encrypted text
                        string passPhrase = CompanyDetails.TIN; // can be any string
                        string initVector = "%@skmelaT3rsh1t!"; // must be 16 bytes

                        // Before encrypting data, we will append plain text to a random
                        // salt value, which will be between 4 and 8 bytes long (implicitly
                        // used defaults).
                        AceSoft.Cryptor clsCryptor = new AceSoft.Cryptor(passPhrase, initVector);

                        if (plainText == clsCryptor.Decrypt(cipherText))
                        {
                            return RegistrationType.Registered;
                        }
                    }
				}
                catch { }

				Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
				string EndingTransactionNo = clsTerminalReport.EndingTransactioNo(CompanyDetails.TerminalNo);
				clsTerminalReport.CommitAndDispose();

				if (Convert.ToInt64(EndingTransactionNo) > 1000)
					regIsDemoExpired = RegistrationType.DEMO_Expired;

				return regIsDemoExpired;
			}
			catch (Exception ex)	
			{	
				SerialNumber = ex.ToString();
				return RegistrationType.Error;	}
		}

        public static string GetHDSerialNo()
        {
            string stRetValue = Constants.ERROR;
            try
            {
                OSVersion osVersion = OSInformation.getOSVersion();
                switch (osVersion)
                {
                    case OSVersion.Windows95:
                    case OSVersion.WindowsMe:
                    case OSVersion.Windows98:
                    case OSVersion.Windows98SecondEdition:
                    case OSVersion.WindowsNT351:
                    case OSVersion.WindowsNT4:
                    case OSVersion.Windows2000:
                    case OSVersion.WindowsXP:
                        const string RegCode = "NW5KF-49VU2-CW1VD-EH32P-UFEL2";
                        DiskInfo diskInfo = new DiskInfo();

                        HDiskInfo.GetIdeDiskInfo(0, ref diskInfo, RegCode);
                        stRetValue = diskInfo.pSerialNumber;
                        break;
                    case OSVersion.WindowsVista:
                    case OSVersion.Windows7:
                        ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
                        ManagementObjectCollection moc = mc.GetInstances();
                        foreach (ManagementObject mo in moc)
                        {
                            if (mo.ToString().ToUpper().IndexOf("PHYSICALDRIVE0") != -1)
                            {
                                stRetValue = mo["SerialNumber"].ToString().Trim();
                                mo.Dispose();
                                break;
                            }
                            mo.Dispose();
                        }
                        break;
                    case OSVersion.Unknown:
                        break;
                }

                return stRetValue;
            }
            catch (Exception ex)
            {
                return stRetValue + ":" + ex.Message;
            }
        }
	}
}
