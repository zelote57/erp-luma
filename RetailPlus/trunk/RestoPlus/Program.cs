using System;
using System.Management;
using System.Windows.Forms;
using System.Xml;
using AceSoft.RetailPlus.Client.UI;

namespace AceSoft.RetailPlus.Client
{
	public class MainModule
	{
		#region Application Main

		[STAThread]
		static void Main() 
		{
			Event clsEvent = new Event();
			clsEvent.AddEvent("starting.... loading splash screen...");
			SplashWnd appsplash = new SplashWnd();
			clsEvent.AddEventLn("Done!");

			appsplash.prgBar.Maximum = 100;
			appsplash.lblStatus.Text = "Checking OS Version... ";
			appsplash.prgBar.Value = 10;
			appsplash.Show();
			appsplash.Refresh();

			// get the windows version
			clsEvent.AddEvent("Checking windows current version");
			OSVersion osVersion = OSInformation.getOSVersion();
			switch (osVersion)
			{
				case OSVersion.Windows95:
				case OSVersion.WindowsMe:
					clsEvent.AddEventLn(": " + osVersion.ToString("G"));
					clsEvent.AddEventLn("This OS platform is not supported. Application will now close.");
					MessageBox.Show("FATAL ERROR Level 1.!!! The Operating System : '" + osVersion.ToString("G") + "' is not supported. Application will now close.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					Application.Exit(); return;
				case OSVersion.Windows98:
				case OSVersion.Windows98SecondEdition:
				case OSVersion.WindowsNT351:
				case OSVersion.WindowsNT4:
				case OSVersion.Windows2000:
				case OSVersion.WindowsXP:
				case OSVersion.WindowsVista:
				case OSVersion.Windows7:
					clsEvent.AddEventLn(": " + osVersion.ToString("G"));
					break;
				case OSVersion.Unknown:
					System.OperatingSystem osInfo = System.Environment.OSVersion;
					clsEvent.AddEventLn(": Unidentified: Platform=" + osInfo.Platform.ToString() + " Major Version=" + osInfo.VersionString);
					clsEvent.AddEventLn("This OS platform is not supported. Application will now close.");
					MessageBox.Show("FATAL ERROR Level 1.!!! The Operating System is unidentified by Retailplus Application. System will now close. Platform=" + osInfo.Platform.ToString() + " Version=" + osInfo.VersionString, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					Application.Exit(); return;
			}
			
			
			// get the running version
			appsplash.lblStatus.Text = "Checking application Version... ";
			appsplash.prgBar.Value = 20;
			appsplash.Show();
			appsplash.Refresh();
			clsEvent.AddEvent("Checking application current version");
			Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			clsEvent.AddEventLn(":" + curVersion.ToString());

			Version verNewVersion = GetLatestVersion();
			clsEvent.AddEventLn("latest version is " + verNewVersion.ToString(), true);
			// compare the versions
			if (curVersion.CompareTo(verNewVersion) < 0)
			{
				clsEvent.AddEventLn("system will now exit then download the latest version.", true);
				System.Diagnostics.Process.Start("RetailPlus.VersionChecker.exe");
				Application.Exit();
				return;
			}
			clsEvent.AddEventLn("This application version is updated.");

			appsplash.lblStatus.Text = "Checking connections to database... ";
			appsplash.prgBar.Value = 30;
			appsplash.Refresh();
			if (!IsDBAlive())
			{
				MessageBox.Show("FATAL ERROR Level 1.!!! Cannot connect to database. Please consult your system administrator immediately...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}
			appsplash.lblStatus.Text += "DONE!";
			appsplash.Refresh();

			appsplash.lblStatus.Text = "Checking Last Initialization of ZREAD... ";
			appsplash.prgBar.Value = 50;
			appsplash.Show();
			appsplash.Refresh();
			if (!IsDateLastInitializationOK())
			{
				MessageBox.Show("FATAL ERROR Level 2.!!! System date is behind ZREAD last initialization date. Please adjust SYSTEM DATE!!!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}
			appsplash.lblStatus.Text += "DONE!";
			appsplash.Refresh();

			appsplash.prgBar.Value = 70;
			appsplash.lblStatus.Text = "Checking terminal if exist in the database... ";
			appsplash.Refresh();
			string ErrorMessage;
			if (!IsTerminalExist(out ErrorMessage))
			{
				MessageBox.Show(ErrorMessage, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}
			appsplash.lblStatus.Text += "DONE!";
			appsplash.Refresh();
			appsplash.prgBar.Value = 100;
			appsplash.lblStatus.Text = "Checking terminal if demo is expired... ";
			appsplash.Refresh();
			if (IsDemoExpired())
			{
				string stHDSeriano = Key.GetHDSerialNo();
				MessageBox.Show(
					"This copy has been expired. Please contact your nearest software distributor" + Environment.NewLine + 
					"Or call RBS Sales @: "+  Environment.NewLine +
					"          Philippines: +63918.939.0926" + Environment.NewLine +
					"          Philippines: +63920.946.8513" + Environment.NewLine + 
					"          Singapore: +658213.5368" + Environment.NewLine + 
					"Or email sales@myretailplus.com" + Environment.NewLine + 
					"Your HD Serial No. is: " + stHDSeriano, "RetailPlus™ Demo Version", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				Application.Exit();
				return;
			}

			appsplash.lblStatus.Text += "DONE!";
			appsplash.Refresh();
			appsplash.Dispose();
			appsplash.Close();
			
			Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();
			clsAuditDetails.ActivityDate = DateTime.Now;
			clsAuditDetails.User = "N/A";
			clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
			clsAuditDetails.Activity = "Open Terminal";
			clsAuditDetails.Remarks = "FE:" + "Open terminal no.:'" + CompanyDetails.TerminalNo + "'.";

			Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
			clsAuditTrail.Insert(clsAuditDetails);

			Data.Terminal clsTerminal = new Data.Terminal(clsAuditTrail.Connection, clsAuditTrail.Transaction);
			Data.TerminalDetails clsTerminalDetails = clsTerminal.Details(Constants.TerminalBranchID, CompanyDetails.TerminalNo);

			clsAuditTrail.CommitAndDispose();

			try 
			{
				clsEvent.AddEventLn("Running Main Window.", true);
				if (clsTerminalDetails.IsFineDIning)
				{
					MainRestoWnd appmainresto = new MainRestoWnd();
					appmainresto.Text = " RestoPlus ™";
					/********************************
					 * Added December 21, 2008
					 * Single instance per computer
					 * *****************************/
					try
					{ SingleInstance.Run(appmainresto); }
					catch { }
				}
				else
				{
					MessageBox.Show("FATAL ERROR Level 1.!!! Sorry the database is not configured for fine dining. Please consult your system administrator immediately...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}

				clsEvent.AddEventLn("System has been exited!", true);

				clsAuditDetails = new Security.AuditTrailDetails();
				clsAuditDetails.ActivityDate = DateTime.Now;
				clsAuditDetails.User = "N/A";
				clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
				clsAuditDetails.Activity = "Close Terminal";
				clsAuditDetails.Remarks = "FE:" + "Close terminal no.:'" + CompanyDetails.TerminalNo + "'.";

				clsAuditTrail = new Security.AuditTrail();
				clsAuditTrail.Insert(clsAuditDetails);
				clsAuditTrail.CommitAndDispose();
				
			}
			catch (Exception ex)
			{
				clsEvent.AddErrorEventLn(ex);

				MessageBox.Show("FATAL ERROR Level 1.!!! An internal error has occurred in the system. Please consult your system administrator immediately...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}
		#endregion

		#region Private Modifiers

		private static bool IsDBAlive()
		{
			Event clsEvent = new Event();
			try
			{
				clsEvent.AddEvent("Checking connections to server.");
				if (IPAddress.IsOpen(AceSoft.RetailPlus.DBConnection.ServerIP(), DBConnection.DBPort()) == false)
				{
					clsEvent.AddEvent("Cannot connect to server please check.");
					return false; 
				}

				clsEvent.AddEvent("Checking connections to database.");

				Data.Database clsDatabase = new Data.Database();
				bool boIsDBAlive = clsDatabase.IsAlive();
				clsEvent.AddEventLn("Done! Connected to " + clsDatabase.Connection.ConnectionString, false);

				clsEvent.AddEventLn("Updating version to " + Application.ProductVersion, true);
				Data.Terminal clsTerminal = new Data.Terminal(clsDatabase.Connection, clsDatabase.Transaction);
				clsTerminal.UpdateFEVersion(Constants.TerminalBranchID, CompanyDetails.TerminalNo, Application.ProductVersion);

				clsDatabase.CommitAndDispose();

				return boIsDBAlive;
			}
			catch(Exception ex)
			{
				
				clsEvent.AddErrorEventLn(ex);
				return false;
			}
		}
		private static bool IsDateLastInitializationOK()
		{
			Event clsEvent = new Event();
			try
			{
				clsEvent.AddEvent("Checking last initialization date");

				Data.Database clsDatabase = new Data.Database();
				DateTime dtDateLastInitialized = clsDatabase.DateLastInitialized();
				clsDatabase.CommitAndDispose();

				bool boReturn = false;

				if (DateTime.Now > dtDateLastInitialized)
				{
					boReturn = true;
					clsEvent.AddEventLn("OK: Last initialization is smaller than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
				}
				else
				{
					clsEvent.AddEventLn("Error: Last initialization is greater than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
				}

				return boReturn;
			}
			catch (Exception ex)
			{

				clsEvent.AddErrorEventLn(ex);
				return false;
			}
		}	
		private static bool IsTerminalExist(out string ErrorMessage)
		{
			ErrorMessage = "";
			Event clsEvent = new Event();
			try
			{
				clsEvent.AddEvent("Checking teminal if exist in the database. [" + CompanyDetails.TerminalNo + "]");

				Data.Terminal clsTerminal = new Data.Terminal();
				Data.TerminalDetails clsDetails = new Data.TerminalDetails();

				bool boIsTerminalExist = clsTerminal.IsExist(Constants.TerminalBranchID, CompanyDetails.TerminalNo, out clsDetails);
				clsTerminal.CommitAndDispose();

				if (!boIsTerminalExist)
				{
					ErrorMessage = "FATAL ERROR Level 1.!!! Terminal No. [" + CompanyDetails.TerminalNo + "] does not exist in the database." +  Environment.NewLine + "Please consult your system administrator immediately...";
					clsEvent.AddEventLn("FATAL ERROR!!! Terminal No. [" + CompanyDetails.TerminalNo + "] does not exist in the database. TRACE: Procedure IsTerminalExist returns false.");
				}
				else 
				{
					clsEvent.AddEventLn("Done!");

					clsEvent.AddEvent("Product Version suited for DB Version [" + clsDetails.DBVersion + "] =? Product Version[" + Application.ProductVersion + "]");
					if (clsDetails.DBVersion != Application.ProductVersion)
					{
						ErrorMessage = "FATAL ERROR Level 1.!!! The Product Version [" + Application.ProductVersion + "] is not compatible with Database Version [" + clsDetails.DBVersion + "]." + Environment.NewLine + "Please consult your system administrator immediately...";
						clsEvent.AddEventLn("FATAL ERROR!!! The Product Version is not compatible with database version. TRACE: Product Version returns false.");
						return false;
					}
					clsEvent.AddEventLn("Done!");

					clsEvent.AddEvent("Checking Machine Serial No. [" + CONFIG.MachineSerialNo + "]");
					if (clsDetails.MachineSerialNo != CONFIG.MachineSerialNo)
					{
						ErrorMessage = "FATAL ERROR Level 1.!!! The Machine Serial No. in the database and configuration file does not match." +  Environment.NewLine + "Please consult your system administrator immediately...";
						clsEvent.AddEventLn("FATAL ERROR!!! The Machine Serial No. in the database and configuration file does not match. TRACE: Procedure IsTerminalExist returns false.");
						return false;
					}
					clsEvent.AddEventLn("Done!");

					clsEvent.AddEvent("Checking accreditation no. [" + CONFIG.AccreditationNo + "]");
					if (clsDetails.AccreditationNo != CONFIG.AccreditationNo)
					{
						ErrorMessage = "FATAL ERROR Level 1.!!! The Accreditation No. in the database and configuration file does not match." +  Environment.NewLine + "Please consult your system administrator immediately...";
						clsEvent.AddEventLn("FATAL ERROR!!! The Accreditation No. in the database and configuration file does not match. TRACE: Procedure IsTerminalExist returns false.");
						return false;
					}
					clsEvent.AddEventLn("Done!");

					
				}

				return boIsTerminalExist;
			}
			catch(Exception ex)
			{
				clsEvent.AddEventLn("FATAL ERROR!!! The Terminal No. does not exist in the database. TRACE:" + ex.Message);
				return false;
			}
		}
		private static bool IsDemoExpired()
		{
			Event clsEvent = new Event();
			try
			{
				clsEvent.AddEvent("Checking if product is demo and has expired. ");
				AceSoft.RetailPlus.Key clsKey = new AceSoft.RetailPlus.Key();
				string SerialNumber = null;
				RegistrationType regIsDemoExpired = clsKey.IsDemoExpired(out SerialNumber);

				bool boIsDemoExpired = false;

				switch (regIsDemoExpired)
				{
					case RegistrationType.Error:
						boIsDemoExpired = true;	break;
					case RegistrationType.DEMO_Unexpired:
						boIsDemoExpired = false; break;
					case RegistrationType.DEMO_Expired:
						boIsDemoExpired = true; break;
					case RegistrationType.Registered:
						clsEvent.AddEventLn("Done! Product is registered to HD Serial No: " + SerialNumber);
						return false;
					default:
						boIsDemoExpired = false; break;
				}

				clsEvent.AddEventLn("Done! return code: " + regIsDemoExpired.ToString("G") + " for HDD: " + SerialNumber);
				return boIsDemoExpired;
			}
			catch(Exception ex)
			{
				clsEvent.AddErrorEventLn(ex);
				return false;
			}

		}
		//private static string GetHDSerialNo()
		//{
		//    string stRetValue = Constants.ERROR;
		//    try
		//    {
		//        OSVersion osVersion = OSInformation.getOSVersion();
		//        switch (osVersion)
		//        {
		//            case OSVersion.Windows95:
		//            case OSVersion.WindowsMe:
		//            case OSVersion.Windows98:
		//            case OSVersion.Windows98SecondEdition:
		//            case OSVersion.WindowsNT351:
		//            case OSVersion.WindowsNT4:
		//            case OSVersion.Windows2000:
		//            case OSVersion.WindowsXP:
		//                const string RegCode = "NW5KF-49VU2-CW1VD-EH32P-UFEL2";
		//                DiskInfo diskInfo = new DiskInfo();

		//                HDiskInfo.GetIdeDiskInfo(0, ref diskInfo, RegCode);
		//                stRetValue = diskInfo.pSerialNumber;
		//                break;
		//            case OSVersion.WindowsVista:
		//            case OSVersion.Windows7:
		//                ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
		//                ManagementObjectCollection moc = mc.GetInstances();
		//                foreach (ManagementObject mo in moc)
		//                {
		//                    if (mo.ToString().ToUpper().IndexOf("PHYSICALDRIVE0") != -1)
		//                    {
		//                        stRetValue = mo["SerialNumber"].ToString().Trim();
		//                        mo.Dispose();
		//                        break;
		//                    }
		//                    mo.Dispose();
		//                }
		//                break;
		//            case OSVersion.Unknown:
		//                break;
		//        }
				
		//        return stRetValue;
		//    }
		//    catch (Exception ex) 
		//    {
		//        return stRetValue + ":" + ex.Message;
		//    }
		//}
		private static Version GetLatestVersion()
		{
			Version clsVersion = new Version("0.0.0.0");
			try
			{
				string strServer = "127.0.0.1";
				try { strServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
				catch { }

				int intPort= 21;
				try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
				catch { }

				string strUserName = "ftprbsuser";
				try { strUserName = System.Configuration.ConfigurationManager.AppSettings["VersionFTPUserName"].ToString(); }
				catch { }

				string strPassword = "ftprbspwd";
				try { strPassword = System.Configuration.ConfigurationManager.AppSettings["VersionFTPUserName"].ToString(); }
				catch { }

				string strFTPDirectory = "RetailPlusClient";
				try { strFTPDirectory = System.Configuration.ConfigurationManager.AppSettings["VersionFTPDirectory"].ToString(); }
				catch { }

				FTP clsFTP = new FTP();

				string strConstantRemarks = "Please contact your system administrator immediately.";

				try { clsFTP.Connect(strServer, strUserName, strPassword); }
				catch { MessageBox.Show("Sorry cannot connect to Version FTP Server: " + strServer + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); return clsVersion; }

				try { clsFTP.ChangeDirectory(strFTPDirectory); }
				catch { MessageBox.Show("Sorry cannot navigate to folder: " + strFTPDirectory + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); clsFTP.Disconnect(); return clsVersion; }

				string strXMLFile = Application.StartupPath + "\\version.xml";
				try 
				{
					if (System.IO.File.Exists(strXMLFile))
						System.IO.File.Delete(strXMLFile);

					clsFTP.Files.Download("version.xml");
					while (clsFTP.Files.DownloadComplete == false)
					{ System.Threading.Thread.Sleep(1000); }
				}
				catch { MessageBox.Show("Sorry cannot find the VERSION.XML file which contain the latest version of RetailPlus FE System." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); clsFTP.Disconnect(); return clsVersion; }
				clsFTP.Disconnect();

				string strVersion = string.Empty;
				#region Assign the Version from XML File to strVersion
				XmlReader xmlReader = new XmlTextReader(strXMLFile);
				xmlReader.MoveToContent();
				string strElementName = string.Empty;

				if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "RetailPlus"))
				{
					while (xmlReader.Read())
					{
						// we remember its name  
						if (xmlReader.NodeType == XmlNodeType.Element)
							strElementName = xmlReader.Name;
						else
						{
							// for text nodes...  
							if ((xmlReader.NodeType == XmlNodeType.Text) && (xmlReader.HasValue))
							{
								// we check what the name of the node was  
								switch (strElementName)
								{
									case "version":
										strVersion = xmlReader.Value;
										break;
								}
							}
						}
					}
				}
				if (xmlReader != null) xmlReader.Close();

				#endregion
				
				clsVersion = new Version(strVersion);
			}
			catch (Exception ex)
			{  }

			return clsVersion;
		}

		#endregion
	}
}
