using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace AceSoft.RetailPlus.VersionChecker
{
    public partial class MainWnd : Form
    {
        private System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        private string mstStatus;

        public string ExecutableSender { get; set; }

        public MainWnd()
        {
            InitializeComponent();

            InitializeBackgroundWorker();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
        Back:
            foreach(Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.ToUpper() == "RETAILPLUS")
                { System.Threading.Thread.Sleep(100); goto Back; }
            }


            //tmrDownload.Enabled = true;
        }

        // Set up the BackgroundWorker object by  
        // attaching event handlers.  
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerReportsProgress = true;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void DeleteIndividualFile(string strPath)
        {
            try
            {
                foreach (string strFile in System.IO.Directory.GetFiles(strPath))
                {
                    string strFileToDelete = Path.GetFileName(strFile).ToLower();

                    if (strFileToDelete != "acesoft.dll" &&
                        strFileToDelete != "version.xml" &&
                        strFileToDelete != "retailplus.versionchecker.exe" &&
                        strFileToDelete != "retailplus.versionchecker.exe.config")
                    {
                        try { System.IO.File.Delete(strFile); }
                        catch (Exception exDel)
                        { }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                string strServer = "127.0.0.1";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"] != null)
                {
                    try { strServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
                    catch { }
                }

                int intPort = 21;
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                {
                    try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                    catch { }
                }

                string strUserName = "ftprbsuser";
                string strPassword = "ftprbspwd";
                string strFTPDirectory = "retailplusclient";

                string destinationDirectory = Application.StartupPath;
                string strConstantRemarks = "Please contact your system administrator immediately.";

                mstStatus = "getting ftp server configuration...";
                worker.ReportProgress(1);

                FtpClient ftpClient = new FtpClient();
                ftpClient.Host = strServer;
                ftpClient.Port = intPort;
                ftpClient.Credentials = new NetworkCredential(strUserName, strPassword);

                mstStatus = "connecting to ftp server " + strServer + "...";
                worker.ReportProgress(2);

                //IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size)
                //        .Where(ftpListItem => string.Equals(Path.GetExtension(ftpListItem.Name), ".dll"));

                IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size);

                mstStatus = "connecting to ftp server " + strServer + "... done.";
                worker.ReportProgress(5);
                System.Threading.Thread.Sleep(10);

                Int32 iCount =  lstFtpListItem.Count();
                Int32 iCtr = 1;

                mstStatus = "copying " + iCount.ToString() + " files from retailplusclient...";
                worker.ReportProgress(10);
                System.Threading.Thread.Sleep(10);

                // List all files with a .txt extension
                foreach (FtpListItem ftpListItem in lstFtpListItem)
                {
                    if (ftpListItem.Name.ToLower() != "version.xml" &&
                        ftpListItem.Name.ToLower() != "retailplus.versionchecker.exe" &
                        ftpListItem.Name.ToLower() != "retailplus.versionchecker.exe.config")
                    {
                        // Report progress as a percentage of the total task. 
                        mstStatus = "copying file: " + ftpListItem.Name + " ...";
                        decimal x = ((decimal.Parse(iCtr.ToString()) / decimal.Parse(iCount.ToString()) * decimal.Parse("100")) - decimal.Parse("1"));
                        iCtr++;

                        Int32 iProgress = Int32.Parse(Math.Round(x, 0).ToString());
                        worker.ReportProgress(iProgress >= 90 ? 90 : iProgress);

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

                mstStatus = "Done copying all files...";

                worker.ReportProgress(100);
                System.Threading.Thread.Sleep(100);
                System.Diagnostics.Process.Start(ExecutableSender);
                Application.Exit();

            }
            catch (Exception ex)
            {

            }
        }

        // This event handler deals with the results of the 
        // background operation. 
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set progress bar to 100% in case it's not already there.
            prgBar.Value = 100;

            // First, handle the case where an exception was thrown. 
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled  
                // the operation. 
                // Note that due to a race condition in  
                // the DoWork event handler, the Cancelled 
                // flag may not have been set, even though 
                // CancelAsync was called.
                lblStatus.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation  
                // succeeded.
                lblStatus.Text = "Done";

                System.Threading.Thread.Sleep(100);
                System.Diagnostics.Process.Start(ExecutableSender);
                Application.Exit();
            }
        }

        // This event handler updates the progress bar. 
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgBar.Value = e.ProgressPercentage;

            //switch (e.ProgressPercentage)
            //{
            //    case 1: lblStatus.Text = "connecting to " + mstrServer; break;
            //    case 2: lblStatus.Text = "changing directory to retailplusclient"; break;
            //    default:
            //        break;
            //}
            this.lblStatus.Text = mstStatus;
        }

        private void tmrDownload_Tick(object sender, EventArgs e)
        {
            tmrDownload.Enabled = false;

            System.Threading.Thread.Sleep(10);
            // Start the download operation in the background.
            this.backgroundWorker1.RunWorkerAsync();

            // Wait for the BackgroundWorker to finish the download.
            while (this.backgroundWorker1.IsBusy)
            {
                //prgBar.Increment(1);
                // Keep UI messages moving, so the form remains 
                // responsive during the asynchronous operation.
                Application.DoEvents();
            }
        }
    }
}