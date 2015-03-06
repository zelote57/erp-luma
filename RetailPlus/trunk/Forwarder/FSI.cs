using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.FtpClient;
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
    #region Struct

    public struct FSIDetails
    {
        public string TenantCode;
        public string TenantName;
        public string OutputDirectory;
        public string SalesTypeCode;
        public string FTPIPAddress;
        public string FTPUsername;
        public string FTPPassword;
        public string FTPDirectory;
    }


    #endregion

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class FSI
    {
        private FSIDetails mclsFSIDetails;
        private System.IO.StreamWriter writer;

        public FSIDetails FSIDetails
        {
            set { mclsFSIDetails = value; }
        }

        #region Constructors and Destructors

        public FSI()
        {

        }
        ~FSI()
        {
            
        }

        #endregion

        #region Public Methods

        private string CreateDailySales(DateTime pvtProcessDate, Data.TerminalReportDetails pvtTerminalReportDetails, decimal decSeniorCitizenDiscount, long lngSeniorCitizenDiscountCount)
        {
            string strRetValue = "";
            try
            {
                string stDailyTableName = mclsFSIDetails.OutputDirectory + "\\S" + mclsFSIDetails.TenantName.Substring(0, 4) + pvtTerminalReportDetails.TerminalNo + pvtTerminalReportDetails.BatchCounter.ToString() + "." + pvtTerminalReportDetails.DateLastInitializedToDisplay.ToString("MM").Replace("10", "A").Replace("11", "B").Replace("12", "C").Replace("0", "") + pvtTerminalReportDetails.DateLastInitializedToDisplay.ToString("dd");
                if (File.Exists(stDailyTableName)) File.Delete(stDailyTableName);

                long lngDiscountCountNetOfSeniorCitizen = pvtTerminalReportDetails.NoOfDiscountedTransactions - lngSeniorCitizenDiscountCount;
                decimal decDiscountNetOfSeniorCitizen = pvtTerminalReportDetails.TotalDiscount - decSeniorCitizenDiscount;

                writer = File.AppendText(stDailyTableName);
                writer.WriteLine("01{0}", mclsFSIDetails.TenantCode);
                writer.WriteLine("02{0}", pvtTerminalReportDetails.TerminalNo);
                writer.WriteLine("03{0}", pvtTerminalReportDetails.DateLastInitializedToDisplay.ToString("MMddyyyy"));
                writer.WriteLine("04{0}", pvtTerminalReportDetails.OldGrandTotal.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("05{0}", pvtTerminalReportDetails.NewGrandTotal.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("06{0}", Convert.ToDecimal(pvtTerminalReportDetails.DailySales + pvtTerminalReportDetails.VAT).ToString("####.#0").Replace(".", ""));
                writer.WriteLine("07{0}", pvtTerminalReportDetails.NonVATableAmount.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("08{0}", decSeniorCitizenDiscount.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("09{0}", "0");
                writer.WriteLine("10{0}", decDiscountNetOfSeniorCitizen.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("11{0}", pvtTerminalReportDetails.RefundSales.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("12{0}", pvtTerminalReportDetails.VoidSales.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("13{0}", pvtTerminalReportDetails.ZReadCount.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("14{0}", pvtTerminalReportDetails.NoOfClosedTransactions.ToString("####"));
                writer.WriteLine("15{0}", pvtTerminalReportDetails.NoOfTotalTransactions.ToString("####"));
                writer.WriteLine("16{0}", mclsFSIDetails.SalesTypeCode);
                writer.WriteLine("17{0}", pvtTerminalReportDetails.DailySales.ToString("####.#0").Replace(".", ""));
                decimal decVAT = pvtTerminalReportDetails.DailySales * decimal.Parse("0.12");
                writer.WriteLine("18{0}", decVAT.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("19{0}", pvtTerminalReportDetails.TotalCharge.ToString("####.#0").Replace(".", ""));
                writer.WriteLine("20{0}", "0"); //adjustment

                writer.Flush();
                writer.Close();

                strRetValue = stDailyTableName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strRetValue;
        }
        private string CreateHourlySales(Int32 BranchID, string TerminalNo, DateTime ProcessDate, Data.TerminalReportDetails TerminalReportDetails)
        {
            string strRetValue = "";

            DateTime dteDateFrom = ProcessDate;

            Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
            DateTime dteDateTo = DateTime.MinValue;
            try
            {
                dteDateTo = clsTerminalReportHistory.NEXTDateLastInitialized(BranchID, TerminalNo, ProcessDate);
            }
            catch { }

            if (dteDateTo == DateTime.MinValue)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("HourlySales: Did not found MAXDateLastInitialized from Terminal Report History. Using the MAXDateLastInitialized from terminal report", true);
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(clsTerminalReportHistory.Connection, clsTerminalReportHistory.Transaction);
                dteDateTo = clsTerminalReport.MAXDateLastInitialized(BranchID, TerminalNo, ProcessDate);
            }

            System.Data.DataTable dtHourlyReport = clsTerminalReportHistory.HourlyReport(BranchID, TerminalNo, dteDateFrom, dteDateTo);
            clsTerminalReportHistory.CommitAndDispose();

            string stHourlyTableName = mclsFSIDetails.OutputDirectory + "\\H" + mclsFSIDetails.TenantName.Substring(0, 4) + TerminalReportDetails.TerminalNo + TerminalReportDetails.BatchCounter.ToString() + "." + TerminalReportDetails.DateLastInitializedToDisplay.ToString("MM").Replace("10", "A").Replace("11", "B").Replace("12", "C").Replace("0", "") + TerminalReportDetails.DateLastInitializedToDisplay.ToString("dd");
            if (File.Exists(stHourlyTableName)) File.Delete(stHourlyTableName);

            writer = File.AppendText(stHourlyTableName);
            writer.WriteLine("01{0}", mclsFSIDetails.TenantCode);
            writer.WriteLine("02{0}", TerminalNo);

            DateTime dtePreviousTransactionDate = DateTime.MinValue;
            foreach (System.Data.DataRow dr in dtHourlyReport.Rows)
            {
                if (dtePreviousTransactionDate != Convert.ToDateTime(dr["TransactionDate"]))
                {
                    writer.WriteLine("03{0}", Convert.ToDateTime(dr["TransactionDate"]).ToString("MMddyyyy"));
                    dtePreviousTransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                }
                writer.WriteLine("04{0}", dr["Time"].ToString().Substring(0,2));
                writer.WriteLine("05{0}", Convert.ToDecimal(Convert.ToDecimal(dr["Amount"]) - Convert.ToDecimal(dr["VAT"])).ToString("####.#0").Replace(".", ""));
                writer.WriteLine("06{0}", Convert.ToInt64(dr["TranCount"]).ToString("####"));
            }
            writer.WriteLine("07{0}", TerminalReportDetails.DailySales.ToString("####.#0").Replace(".", ""));
            writer.WriteLine("08{0}", TerminalReportDetails.NoOfClosedTransactions.ToString("####"));

            writer.Flush();
            writer.Close();

            strRetValue = stHourlyTableName;

            return strRetValue;
        }

        #endregion

        public void CreateAndTransferFile(int BranchID, string TerminalNo, DateTime pvtDateInitialized)
        {
            DateTime dteDateToprocess = pvtDateInitialized;

            Event clsEvent = new Event();
            try
            {
                clsEvent.AddEventLn("Starting FSI FILE CREATOR.", true);
                clsEvent.AddEventLn("==================================================",true);
                clsEvent.AddEventLn("=                FSI FILE CREATOR                =", true);
                clsEvent.AddEventLn("==================================================", true);

                /***********************************************************************
                 * Check the destination dir if existing.
                 * ********************************************************************/
                clsEvent.AddEventLn("Checking directory settings.", true);
                string dir = mclsFSIDetails.OutputDirectory;
                dir = dir.Replace("{YYYY}", DateTime.Now.ToString("yyyy"));
                dir = dir.Replace("{MM}", DateTime.Now.ToString("MM"));
                dir = dir.Replace("{MMM}", DateTime.Now.ToString("MMM"));
                dir = dir.Replace("{MMMM}", DateTime.Now.ToString("MMMM"));

                if (!System.IO.Directory.Exists(dir))
                {
                    clsEvent.AddEventLn("Directory [" + dir + "] does not exist.", true);
                    System.IO.Directory.CreateDirectory(dir);
                }
                else
                { clsEvent.AddEventLn("Directory [" + dir + "] exist.", true); }
                mclsFSIDetails.OutputDirectory = dir;

                /***********************************************************************
                 * GET The report of Current Terminal using Specified InitializationDate
                 * ********************************************************************/
                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
                Data.TerminalReportDetails clsTerminalReportDetail = clsTerminalReportHistory.Details(BranchID, TerminalNo, dteDateToprocess);

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsTerminalReportHistory.Connection, clsTerminalReportHistory.Transaction);

                long lngSeniorSitizenDiscountCount = 0;
                decimal decSeniorSitizenDiscount = clsSalesTransactions.SeniorCitizenDiscounts(clsTerminalReportDetail.BranchID, clsTerminalReportDetail.TerminalNo, clsTerminalReportDetail.BeginningTransactionNo, clsTerminalReportDetail.EndingTransactionNo, out lngSeniorSitizenDiscountCount);

                string stDailyTableName = CreateDailySales(dteDateToprocess, clsTerminalReportDetail, decSeniorSitizenDiscount, lngSeniorSitizenDiscountCount);
                string stHourlyTableName = CreateHourlySales(BranchID, TerminalNo, dteDateToprocess, clsTerminalReportDetail);
                clsTerminalReportHistory.UpdateTerminalReportBatchCounter(TerminalNo, dteDateToprocess);
                clsTerminalReportHistory.CommitAndDispose();
                clsEvent.AddEventLn("Record for [" + dteDateToprocess.ToString("yyyy-MM-dd HH:mm:ss") + "] BacthCounter:" + clsTerminalReportDetail.BatchCounter.ToString() + " has been created for FSI.", true);

                TransferFile(stDailyTableName);
                TransferFile(stHourlyTableName);

            }
            catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
                throw ex;
            }
            clsEvent.AddEventLn("FSI FILE CREATOR exited.", true);
        }
        private void TransferFile(string pvtFileName)
        {
            Event clsEvent = new Event();
            try
            {
                if (!string.IsNullOrEmpty(pvtFileName))
                {
                    if (string.IsNullOrEmpty(mclsFSIDetails.FTPIPAddress))
                    {   clsEvent.AddEventLn("Cannot transfer file " + pvtFileName + ". FTP IPAddress is empty Automatic File transfer is disabled.", true); 
                        return; 
                    }
                    else
                        clsEvent.AddEventLn("Transferring " + pvtFileName + " to " + mclsFSIDetails.FTPIPAddress, true);
                }
                else
                {
                    clsEvent.AddEventLn("Cannot transfer an blank file.", true); return;
                }

                int intPort = 21;
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                {
                    try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                    catch { }
                }

                FtpClient ftpClient = new FtpClient();
                ftpClient.Host = mclsFSIDetails.FTPIPAddress;
                ftpClient.Port = intPort;
                ftpClient.Credentials = new NetworkCredential(mclsFSIDetails.FTPUsername, mclsFSIDetails.FTPPassword);

                IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(mclsFSIDetails.FTPDirectory, FtpListOption.Modify | FtpListOption.Size);

                bool bolIsFileExist = false;
                clsEvent.AddEventLn("Checking file if already exist...", true);
                try
                {
                    foreach (FtpListItem ftpListItem in lstFtpListItem)
                    {
                        if (ftpListItem.Name.ToUpper() == Path.GetFileName(pvtFileName).ToUpper())
                        { bolIsFileExist = true; break; }
                    }
                }
                catch (Exception excheck)
                {
                    clsEvent.AddEventLn("checking error..." + excheck.Message, true);
                }

                if (bolIsFileExist)
                {
                    clsEvent.AddEventLn("exist...", true);
                    clsEvent.AddEventLn("uploading now...", true);
                }
                else
                {
                    clsEvent.AddEventLn("does not exist...", true);
                    clsEvent.AddEventLn("uploading now...", true);
                }

                using (var fileStream = File.OpenRead(pvtFileName))
                using (var ftpStream = ftpClient.OpenWrite(string.Format("{0}/{1}", Path.GetFileName(pvtFileName), Path.GetFileName(pvtFileName))))
                {
                    var buffer = new byte[8 * 1024];
                    int count;
                    while ((count = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ftpStream.Write(buffer, 0, count);
                    }
                    clsEvent.AddEventLn("Upload Complete: TotalBytes: " + buffer.ToString(), true);
                }

                ftpClient.Disconnect();
                ftpClient.Dispose();
                ftpClient = null;

                clsEvent.AddEventLn("Done.", true);
            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("Error encountered: " + ex.Message, true);
                throw new IOException("Sales file is not sent to RLC server. Please contact your POS vendor");
            }
        }
    }
}

