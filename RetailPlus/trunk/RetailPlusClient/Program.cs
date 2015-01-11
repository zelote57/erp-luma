using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.FtpClient;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using AceSoft.RetailPlus.Client.UI;

namespace AceSoft.RetailPlus.Client
{
	public class MainModule
	{
		#region Application Main

        // Handle the UI exceptions by showing a dialog box, and asking the user whether 
        // or not they wish to abort execution. 
        private static void MainWnd_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                Event clsEvent = new Event();
                clsEvent.AddErrorEventLn(t.Exception);

                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error", "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort. 
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                
                Event clsEvent = new Event();
                clsEvent.AddErrorEventLn(ex);

                string errorMsg = "RetailPlus FE Error. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log. 
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "RetailPlus");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
                
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it. 
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

		[STAThread]
		static void Main() 
		{
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(MainWnd_UIThreadException);

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
                System.Diagnostics.Process.Start("RetailPlus.VersionChecker.exe", "RetailPlus.exe");
				Application.Exit();
				return;
			}
			clsEvent.AddEventLn("This application version is updated.",true);

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
                    "Or call RBS Sales @: " + Environment.NewLine +
                    "          Philippines: +63.947.3215979" + Environment.NewLine +
                    "          Philippines: +63.918.9390926" + Environment.NewLine +
                    "          Philippines: +632.998.7722" + Environment.NewLine +
                    "          Singapore: +658.6519601" + Environment.NewLine +
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
            clsAuditDetails.BranchID = Constants.TerminalBranchID;
            clsAuditDetails.TerminalNo = CompanyDetails.TerminalNo;
			clsAuditDetails.ActivityDate = DateTime.Now;
			clsAuditDetails.User = "N/A";
			clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
			clsAuditDetails.Activity = "Open Terminal";
			clsAuditDetails.Remarks = "FE:" + "Open terminal no.:'" + CompanyDetails.TerminalNo + "' @ Branch:" + Constants.TerminalBranchID.ToString();

			Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
			clsAuditTrail.Insert(clsAuditDetails);

			Data.Terminal clsTerminal = new Data.Terminal(clsAuditTrail.Connection, clsAuditTrail.Transaction);
			Data.TerminalDetails clsTerminalDetails = clsTerminal.Details(Constants.TerminalBranchID, CompanyDetails.TerminalNo);

            //overwrite the companydetails from the database
            Data.SysConfig clsSysConfig = new Data.SysConfig(clsAuditTrail.Connection, clsAuditTrail.Transaction);
            Data.SysConfigDetails clsSysConfigDetails = clsSysConfig.get_SysConfigDetails();
            clsAuditTrail.CommitAndDispose();

            CompanyDetails.CompanyCode = string.IsNullOrEmpty(clsSysConfigDetails.CompanyCode) ? CompanyDetails.CompanyCode : clsSysConfigDetails.CompanyCode;
            CompanyDetails.CompanyName = string.IsNullOrEmpty(clsSysConfigDetails.CompanyName) ? CompanyDetails.CompanyName : clsSysConfigDetails.CompanyName;
            CompanyDetails.Currency = string.IsNullOrEmpty(clsSysConfigDetails.Currency) ? CompanyDetails.Currency : clsSysConfigDetails.Currency;
            CompanyDetails.TIN = string.IsNullOrEmpty(clsSysConfigDetails.TIN) ? CompanyDetails.TIN : clsSysConfigDetails.TIN;

            CompanyDetails.Address1 = string.IsNullOrEmpty(clsSysConfigDetails.Address1) ? CompanyDetails.Address1 : clsSysConfigDetails.Address1;
            CompanyDetails.Address2 = string.IsNullOrEmpty(clsSysConfigDetails.Address2) ? CompanyDetails.Address2 : clsSysConfigDetails.Address2;
            CompanyDetails.City = string.IsNullOrEmpty(clsSysConfigDetails.City) ? CompanyDetails.City : clsSysConfigDetails.City;
            CompanyDetails.State = string.IsNullOrEmpty(clsSysConfigDetails.State) ? CompanyDetails.State : clsSysConfigDetails.State;
            CompanyDetails.Zip = string.IsNullOrEmpty(clsSysConfigDetails.Zip) ? CompanyDetails.Zip : clsSysConfigDetails.Zip;
            CompanyDetails.Country = string.IsNullOrEmpty(clsSysConfigDetails.Country) ? CompanyDetails.Country : clsSysConfigDetails.Country;
            CompanyDetails.OfficePhone = string.IsNullOrEmpty(clsSysConfigDetails.OfficePhone) ? CompanyDetails.OfficePhone : clsSysConfigDetails.OfficePhone;
            CompanyDetails.DirectPhone = string.IsNullOrEmpty(clsSysConfigDetails.DirectPhone) ? CompanyDetails.DirectPhone : clsSysConfigDetails.DirectPhone;
            CompanyDetails.FaxPhone = string.IsNullOrEmpty(clsSysConfigDetails.FaxPhone) ? CompanyDetails.FaxPhone : clsSysConfigDetails.FaxPhone;
            CompanyDetails.MobilePhone = string.IsNullOrEmpty(clsSysConfigDetails.MobilePhone) ? CompanyDetails.MobilePhone : clsSysConfigDetails.MobilePhone;
            CompanyDetails.EmailAddress = string.IsNullOrEmpty(clsSysConfigDetails.EmailAddress) ? CompanyDetails.EmailAddress : clsSysConfigDetails.EmailAddress;
            CompanyDetails.WebSite = string.IsNullOrEmpty(clsSysConfigDetails.WebSite) ? CompanyDetails.WebSite : clsSysConfigDetails.WebSite;

			try 
			{
				clsEvent.AddEventLn("Running Main Window.", true);

				MainWnd appmain = new MainWnd();
				appmain.Text = " RetailPlus ™";
				/********************************
				 * Added December 21, 2008
				 * Enable 2 windows in one computer
				 * *****************************/
				try
				{
                    if (Common.isTerminalMultiInstanceEnabled())
					{ Application.Run(appmain); }
					else 
					{ SingleInstance.Run(appmain); }
				}
				catch (Exception exMain) 
				{
					clsEvent.AddEventLn("System has excountered an error while loading main screen!" + Environment.NewLine + exMain.ToString(), true);
					clsEvent.AddEventLn("System has been exited!", true);
					throw (exMain);
				}

				clsEvent.AddEventLn("System has been exited!", true);

				clsAuditDetails = new Security.AuditTrailDetails();
                clsAuditDetails.BranchID = Constants.TerminalBranchID;
                clsAuditDetails.TerminalNo = CompanyDetails.TerminalNo;
				clsAuditDetails.ActivityDate = DateTime.Now;
				clsAuditDetails.User = "N/A";
				clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
				clsAuditDetails.Activity = "Close Terminal";
                clsAuditDetails.Remarks = "FE:" + "Close terminal no.:'" + CompanyDetails.TerminalNo + "' @ Branch:" + Constants.TerminalBranchID.ToString() + ".";

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
				clsEvent.AddEvent("Checking terminal if exist in the database. [" + CompanyDetails.TerminalNo + "]");

				Data.Terminal clsTerminal = new Data.Terminal();
				Data.TerminalDetails clsDetails = new Data.TerminalDetails();

				bool boIsTerminalExist = clsTerminal.IsExist(Constants.TerminalBranchID, CompanyDetails.TerminalNo, out clsDetails);
				clsTerminal.CommitAndDispose();

				if (!boIsTerminalExist)
				{
                    ErrorMessage = "FATAL ERROR Level 1.!!! " + Environment.NewLine + "Terminal No:[" + CompanyDetails.TerminalNo + "] @ BranchID:[" + Constants.TerminalBranchID .ToString() + "] does not exist in the database." + Environment.NewLine + "Please consult your system administrator immediately...";
                    clsEvent.AddEventLn("FATAL ERROR!!! " + Environment.NewLine + "Terminal No:[" + CompanyDetails.TerminalNo + "] @ BranchID:[" + Constants.TerminalBranchID.ToString() + "] does not exist in the database. TRACE: Procedure IsTerminalExist returns false.");
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
                ErrorMessage = "FATAL ERROR!!! TRACE: " + ex.Message;
				clsEvent.AddEventLn("FATAL ERROR!!! TRACE:" + ex.Message);
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

        private static Version GetLatestVersion()
		{
			Version clsVersion = new Version("0.0.0.0");
			try
			{
				string strServer = "127.0.0.1";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"] != null)
                {
                    try { strServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
                    catch { }
                }

				int intPort= 21;
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                {
                    try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                    catch { }
                }

				string strUserName = "ftprbsuser";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPUserName"] != null)
                {
                    try { strUserName = System.Configuration.ConfigurationManager.AppSettings["VersionFTPUserName"].ToString(); }
                    catch { }
                }

				string strPassword = "ftprbspwd";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPassword"] != null)
                {
                    try { strPassword = System.Configuration.ConfigurationManager.AppSettings["VersionFTPPassword"].ToString(); }
                    catch { }
                }

				string strFTPDirectory = "RetailPlusClient";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPDirectory"] != null)
                {
                    try { strFTPDirectory = System.Configuration.ConfigurationManager.AppSettings["VersionFTPDirectory"].ToString(); }
                    catch { }
                }

                string strXMLFile = Application.StartupPath + "\\version.xml";
                string destinationDirectory = Application.StartupPath;

                FtpClient ftpClient = new FtpClient();
                ftpClient.Host = strServer;
                ftpClient.Port = intPort;
                ftpClient.Credentials = new NetworkCredential(strUserName, strPassword);

                System.Collections.Generic.IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size);

                // List all files with a .txt extension
                foreach (FtpListItem ftpListItem in lstFtpListItem)
                {
                    if (ftpListItem.Name.ToLower() == "version.xml" ||
                        ftpListItem.Name.ToLower() == "retailplus.versionchecker.exe" ||
                        ftpListItem.Name.ToLower() == "retailplus.versionchecker.exe.config")
                    {

                        var destinationPath = string.Format(@"{0}\{1}", destinationDirectory, ftpListItem.Name);

                        using (var ftpStream = ftpClient.OpenRead(ftpListItem.FullName))
                        using (var fileStream = File.Create(destinationPath, (int)ftpStream.Length))
                        {
                            var buffer = new byte[8 * 1024];
                            int count;
                            while ((count = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fileStream.Write(buffer, 0, count);
                            }
                        }
                    }
                }

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
                        if (!string.IsNullOrEmpty(strVersion)) break;
					}
				}
				if (xmlReader != null) xmlReader.Close();

				#endregion
				
				clsVersion = new Version(strVersion);
			}
			catch
			{  }

			return clsVersion;
		}

		#endregion
	}
}
