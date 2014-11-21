using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace AceSoft.RetailPlus.VersionChecker
{
    public partial class MainWnd : Form
    {
        private System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        private string mstrServer;
        private string mstStatus;

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
        private void CopyIndividualFile(BackgroundWorker worker, FTP clsFTP, string strPath, FTP.FileCollection clsFiles)
        {
            try
            {
                foreach (FTP.File clsFile in clsFiles)
                {
                    if (clsFile.FileName.ToLower() != "acesoft.dll" &&
                            clsFile.FileName.ToLower() != "version.xml" &&
                            clsFile.FileName.ToLower() != "retailplus.versionchecker.exe" &&
                            clsFile.FileName.ToLower() != "retailplus.versionchecker.exe.config")
                    {
                        try
                        {
                            string strLocalFileName = strPath + "\\" + clsFile.FileName;
                            FileInfo clsFileInfoLocalFile = new System.IO.FileInfo(strLocalFileName);
                            DateTime dteFileDateLocalFile = clsFileInfoLocalFile.CreationTime;
                            DateTime dteFileDateRemoteFile = clsFile.FileDate;

                            if (dteFileDateLocalFile != dteFileDateRemoteFile || clsFile.FileSize != clsFileInfoLocalFile.Length)
                            {
                                if (File.Exists(strLocalFileName))
                                    System.IO.File.Delete(strLocalFileName);

                                clsFiles.Download(clsFile.FileName, strLocalFileName);
                                
                                while (!clsFTP.Files.DownloadComplete)
                                {
                                    mstStatus = "  Downloading " + clsFile.FileName + ": TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString();
                                    // Report progress as a percentage of the total task. 
                                    worker.ReportProgress(clsFTP.Files.PercentComplete);
                                }
                                mstStatus = "  " + clsFile.FileName + " download complete: TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString();
                            }
                        }
                        catch (Exception exAdd)
                        { throw exAdd; }
                    }
                }     
            }
            catch (Exception ex)
            {
 
            }
        }
        private void CopyDirectory(BackgroundWorker worker, FTP clsFTP, string strPath, FTP.DirectoryCollection clsDirs)
        {
            try
            {
                foreach (FTP.Directory clsDir in clsDirs)
                {
                    if (clsDir.DirectoryName != "." && clsDir.DirectoryName != "..")
                    {
                        string strConstantRemarks = "Please contact your system administrator immediately.";

                        mstStatus = "changing directory to " + clsDir.DirectoryName + "...";
                        worker.ReportProgress(60);
                        try { clsFTP.ChangeDirectory(clsDir.DirectoryName); }
                        catch (Exception exCWD) { MessageBox.Show("Sorry cannot navigate to folder: " + clsDirs.GetWorkingDirectory() + "/" + clsDir.DirectoryName + "." + Environment.NewLine + " Details: " + exCWD.Message + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); throw exCWD; }

                        string strLocalDirectory = strPath + "\\" + clsDir.DirectoryName;
                        if (!System.IO.Directory.Exists(strLocalDirectory))
                            System.IO.Directory.CreateDirectory(strLocalDirectory);

                        mstStatus = "copying files @ directory " + clsDir.DirectoryName + "...";
                        worker.ReportProgress(70);
                        CopyIndividualFile(worker, clsFTP, strLocalDirectory, clsFTP.Files);
                        // CopyDirectory(clsFTP, strLocalDirectory, clsFTP.Directories);
                    }
                }
            }
            catch (Exception ex) {  }
        }

        // This event handler is where the actual, 
        // potentially time-consuming work is done. 
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                string mstrServer = "127.0.0.1";
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"] != null)
                {
                    try { mstrServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
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

                FTP clsFTP = new FTP();

                string strConstantRemarks = "Please contact your system administrator immediately.";

                mstStatus = "connecting to ftp server " + mstrServer + "...";
                worker.ReportProgress(1);
                try { clsFTP.Connect(mstrServer, strUserName, strPassword); }
                catch (Exception exConn) { MessageBox.Show("Sorry cannot connect to Version FTP Server: " + mstrServer + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); throw exConn; }

                mstStatus = "changing directory to retailplusclient...";
                worker.ReportProgress(5);
                try { clsFTP.ChangeDirectory(strFTPDirectory); }
                catch (Exception exCWD) { MessageBox.Show("Sorry cannot navigate to folder: " + strFTPDirectory + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); throw exCWD; }

                try
                {
                    // Report progress as a percentage of the total task. 
                    mstStatus = "copying files @ retailplusclient...";
                    worker.ReportProgress(10);
                    CopyIndividualFile(worker, clsFTP, Application.StartupPath, clsFTP.Files);

                    mstStatus = "copying directories @ retailplusclient...";
                    worker.ReportProgress(50);
                    CopyDirectory(worker, clsFTP, Application.StartupPath, clsFTP.Directories);

                    mstStatus = "Done copying all files...";

                    worker.ReportProgress(100);
                    System.Threading.Thread.Sleep(100);
                    System.Diagnostics.Process.Start("RetailPlus.exe");
                    Application.Exit();
                }
                catch (Exception ex) { MessageBox.Show("Sorry cannot copy the due to an error. Details: " + ex.Message + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex; }
                clsFTP.Disconnect();
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