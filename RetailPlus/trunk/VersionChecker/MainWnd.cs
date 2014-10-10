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
        public MainWnd()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
        Back:
            foreach(Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.ToUpper() == "RETAILPLUS")
                { System.Threading.Thread.Sleep(100); goto Back; }
            }

            tmrDownload.Enabled = true;
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
        private void CopyIndividualFile(FTP clsFTP, string strPath, FTP.FileCollection clsFiles)
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
                                    this.lblStatus.Text = "  Downloading " + clsFile.FileName + ": TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString();
                                    prgBar.Value = clsFTP.Files.PercentComplete;
                                }
                                this.lblStatus.Text = "  " + clsFile.FileName + " download complete: TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString();
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
        private void CopyDirectory(FTP clsFTP, string strPath, FTP.DirectoryCollection clsDirs)
        {
            try
            {
                foreach (FTP.Directory clsDir in clsDirs)
                {
                    string strConstantRemarks = "Please contact your system administrator immediately.";
                    try { clsFTP.ChangeDirectory(clsDir.DirectoryName); }
                    catch (Exception exCWD) { MessageBox.Show("Sorry cannot navigate to folder: " + clsDirs.GetWorkingDirectory() + "/" + clsDir.DirectoryName + "." + Environment.NewLine + " Details: " + exCWD.Message + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); throw exCWD; }

                    string strLocalDirectory = strPath + "\\" + clsDir.DirectoryName;
                    if (!System.IO.Directory.Exists(strLocalDirectory))
                        System.IO.Directory.CreateDirectory(strLocalDirectory);

                    CopyIndividualFile(clsFTP, strLocalDirectory, clsFTP.Files);
                    // CopyDirectory(clsFTP, strLocalDirectory, clsFTP.Directories);
                }
            }
            catch (Exception ex) {  }
        }
        private bool CopyFile()
        {
            bool boRetValue = false;
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

                FTP clsFTP = new FTP();

                string strConstantRemarks = "Please contact your system administrator immediately.";

                try { clsFTP.Connect(strServer, strUserName, strPassword); }
                catch { MessageBox.Show("Sorry cannot connect to Version FTP Server: " + strServer + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); return boRetValue; }

                try { clsFTP.ChangeDirectory(strFTPDirectory); }
                catch (Exception exCWD) { MessageBox.Show("Sorry cannot navigate to folder: " + strFTPDirectory + "." + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); return boRetValue; }

                try
                {
                    CopyIndividualFile(clsFTP, Application.StartupPath, clsFTP.Files);
                    CopyDirectory(clsFTP, Application.StartupPath, clsFTP.Directories);
                }
                catch (Exception ex) { MessageBox.Show("Sorry cannot copy the due to an error. Details: " + ex.Message + Environment.NewLine + strConstantRemarks, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error); return boRetValue; }
                clsFTP.Disconnect();

                boRetValue = true;
            }
            catch (Exception ex)
            { }

            return boRetValue;
        }

        private void tmrDownload_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrDownload.Enabled = false;

                if (CopyFile())
                {
                    this.lblStatus.Text = "Done updating all files...";
                    System.Threading.Thread.Sleep(100);
                    System.Diagnostics.Process.Start("RetailPlus.exe");
                    Application.Exit();
                }
                else
                {
                    this.lblStatus.Text = this.Text = "Error: ";
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex) { this.Text = "Error: " + ex.Message; tmrDownload.Enabled = false; }
        }
    }
}