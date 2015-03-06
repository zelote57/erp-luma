using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Linq;
using System.Net;
using System.Net.FtpClient;

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

    public struct RLCDetails
    {
        public string TenantCode;
        public string TenantName;
        public string OutputDirectory;
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
    public class RLC
    {
        private RLCDetails mclsRLCDetails;
        private System.IO.StreamWriter writer;

        public RLCDetails RLCDetails
        {
            set { mclsRLCDetails = value; }
        }

        #region Constructors and Destructors

        public RLC()
        {

        }
        ~RLC()
        {

        }

        #endregion

        #region Public Methods

        private string CreateDailySales(DateTime pvtProcessDate, Data.TerminalReportDetails pvtTerminalReportDetails, decimal decSeniorCitizenDiscount, long lngSeniorCitizenDiscountCount, decimal decPWDDiscount, long lngPWDDiscountCount)
        {
            string strRetValue = "";
            try
            {
                string stDailyTableName = mclsRLCDetails.OutputDirectory + "\\" + mclsRLCDetails.TenantCode.Substring(mclsRLCDetails.TenantCode.Length - 4) + pvtTerminalReportDetails.DateLastInitializedToDisplay.ToString("MMdd") + "." + pvtTerminalReportDetails.TerminalNo + pvtTerminalReportDetails.BatchCounter.ToString();
                if (File.Exists(stDailyTableName)) File.Delete(stDailyTableName);

                // remove the + pvtTerminalReportDetails.TotalCharge in decGrossSales 
                decimal decGrossSales = pvtTerminalReportDetails.DailySales + pvtTerminalReportDetails.VAT + pvtTerminalReportDetails.LocalTax + pvtTerminalReportDetails.TotalDiscount; //+ pvtTerminalReportDetails.TotalCharge
                //decimal decVAT = (pvtTerminalReportDetails.DailySales + pvtTerminalReportDetails.VAT - pvtTerminalReportDetails.NonVaTableAmount) / Convert.ToDecimal(1.12) * Convert.ToDecimal(0.12); //decGrossSales
                
                long lngDiscountCountNetOfSeniorCitizen = pvtTerminalReportDetails.NoOfDiscountedTransactions - lngSeniorCitizenDiscountCount - lngPWDDiscountCount;
                decimal decDiscountNetOfSeniorCitizen = pvtTerminalReportDetails.TotalDiscount - decSeniorCitizenDiscount - decPWDDiscount;
                decimal deCreditCardSalesTax = pvtTerminalReportDetails.CreditCardSales / Convert.ToDecimal(1.12) * Convert.ToDecimal(0.12);

                decimal decVAT = (decGrossSales - decSeniorCitizenDiscount - pvtTerminalReportDetails.NonVATableAmount - decPWDDiscount) / Convert.ToDecimal(1.12) * Convert.ToDecimal(0.12); //decGrossSales

                writer = File.AppendText(stDailyTableName);
                writer.WriteLine("01{0}", mclsRLCDetails.TenantCode.PadLeft(16, '0'));
                writer.WriteLine("02{0}", pvtTerminalReportDetails.TerminalNo.PadLeft(16, '0'));
                writer.WriteLine("03{0}", decGrossSales.ToString("####.#0").PadLeft(16, '0')); //gross of VAT, regular discount, sr citizen, local tax, pwd,  / net of void ,refund and service charge
                writer.WriteLine("04{0}", decVAT.ToString("####.#0").PadLeft(16, '0')); // Line 3 less Line 11, Line 24, Line 27 & Line 28 / 1.12 x 12%
                writer.WriteLine("05{0}", pvtTerminalReportDetails.VoidSales.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("06{0}", pvtTerminalReportDetails.NoOfVoidTransactions.ToString("####").PadLeft(16, '0'));
                writer.WriteLine("07{0}", decDiscountNetOfSeniorCitizen.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("08{0}", lngDiscountCountNetOfSeniorCitizen.ToString("####").PadLeft(16, '0'));
                writer.WriteLine("09{0}", Convert.ToDecimal(pvtTerminalReportDetails.RefundSales * Convert.ToDecimal(1.12)).ToString("####.#0").PadLeft(16, '0')); // with VAT
                writer.WriteLine("10{0}", pvtTerminalReportDetails.NoOfRefundTransactions.ToString("####").PadLeft(16, '0'));
                writer.WriteLine("11{0}", decSeniorCitizenDiscount.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("12{0}", lngSeniorCitizenDiscountCount.ToString("####").PadLeft(16, '0'));
                writer.WriteLine("13{0}", pvtTerminalReportDetails.TotalCharge.ToString("####.#0").PadLeft(16, '0'));
                if (pvtTerminalReportDetails.ZReadCount==0)
                    writer.WriteLine("14{0}", "0".PadLeft(16, '0'));
                else
                    writer.WriteLine("14{0}", Convert.ToInt64(pvtTerminalReportDetails.ZReadCount - 1).ToString("####").PadLeft(16, '0'));
                writer.WriteLine("15{0}", pvtTerminalReportDetails.OldGrandTotal.ToString("####.#0").PadLeft(16, '0')); // line 15
                writer.WriteLine("16{0}", pvtTerminalReportDetails.ZReadCount.ToString("####").PadLeft(16, '0'));
                writer.WriteLine("17{0}", pvtTerminalReportDetails.NewGrandTotal.ToString("####.#0").PadLeft(16, '0')); // line 3 - line 7 - line 11 - line 27 + Line 15
                writer.WriteLine("18{0}", pvtTerminalReportDetails.DateLastInitializedToDisplay.ToString("MM/dd/yyyy").PadLeft(16, '0'));
                writer.WriteLine("19{0}", pvtTerminalReportDetails.PromotionalItems.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("20{0}", "0.00".PadLeft(16, '0'));
                writer.WriteLine("21{0}", pvtTerminalReportDetails.LocalTax.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("22{0}", pvtTerminalReportDetails.CreditCardSales.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("23{0}", deCreditCardSalesTax.ToString("####.#0").PadLeft(16, '0'));
                writer.WriteLine("24{0}", pvtTerminalReportDetails.NonVATableAmount.ToString("####.#0").PadLeft(16, '0'));
                //08Jan2014 added as per new requirement
                writer.WriteLine("25{0}", "0.00".PadLeft(16, '0'));         // Pharma Sales
                writer.WriteLine("26{0}", "0.00".PadLeft(16, '0'));                                                 // Non Pharma Sales
                writer.WriteLine("27{0}", decPWDDiscount.ToString("####.#0").PadLeft(16, '0'));           // PWD - Persons with Disability Discount
                writer.WriteLine("28{0}", "0.00".PadLeft(16, '0'));                                                 // SALES NOT SUBJECT TO PERCENTAGE RENT (ex. sales from fixed rent kiosks)
                writer.WriteLine("29{0}", pvtTerminalReportDetails.TotalReprintedTransaction.ToString("####.#0").PadLeft(16, '0'));     // Total Sales of Reprinted Transaction
                writer.WriteLine("30{0}", pvtTerminalReportDetails.NoOfReprintedTransaction.ToString("####").PadLeft(16, '0'));             // No. of Reprinted Transaction
                
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

        #endregion

        public bool CreateAndTransferFile(Int32 BranchID, string TerminalNo, DateTime pvtDateInitialized)
        {
            bool bolRetValue = false;

            DateTime dteDateToprocess = pvtDateInitialized;

            Event clsEvent = new Event();
            try
            {
                clsEvent.AddEventLn("Starting RLC FILE CREATOR.", true);
                clsEvent.AddEventLn("==================================================", true);
                clsEvent.AddEventLn("=                RLC FILE CREATOR                =", true);
                clsEvent.AddEventLn("==================================================", true);

                /***********************************************************************
                 * Check the destination dir if existing.
                 * ********************************************************************/
                clsEvent.AddEventLn("Checking directory settings.", true);
                string dir = mclsRLCDetails.OutputDirectory;
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
                mclsRLCDetails.OutputDirectory = dir;

                /***********************************************************************
                 * GET The report of Current Terminal using Specified InitializationDate
                 * ********************************************************************/
                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
                Data.TerminalReportDetails clsTerminalReportDetail = clsTerminalReportHistory.Details(BranchID, TerminalNo, dteDateToprocess);

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsTerminalReportHistory.Connection, clsTerminalReportHistory.Transaction);

                long lngSeniorCitizenDiscountCount = 0;
                decimal decSeniorCitizenDiscount = clsSalesTransactions.SeniorCitizenDiscounts(clsTerminalReportDetail.BranchID, clsTerminalReportDetail.TerminalNo, clsTerminalReportDetail.BeginningTransactionNo, clsTerminalReportDetail.EndingTransactionNo, out lngSeniorCitizenDiscountCount);

                long lngPWDDiscountCount = 0;
                decimal decPWDDiscount = clsSalesTransactions.PersonWithDisabilityDiscounts(clsTerminalReportDetail.BranchID, clsTerminalReportDetail.TerminalNo, clsTerminalReportDetail.BeginningTransactionNo, clsTerminalReportDetail.EndingTransactionNo, out lngPWDDiscountCount);

                string stDailyTableName = CreateDailySales(dteDateToprocess, clsTerminalReportDetail, decSeniorCitizenDiscount, lngSeniorCitizenDiscountCount, decPWDDiscount, lngPWDDiscountCount);

                clsTerminalReportHistory.CommitAndDispose();

                bool bolTransferFile = TransferFile(stDailyTableName);
                if (bolTransferFile )
                {
                    clsTerminalReportHistory = new Data.TerminalReportHistory();
                    clsTerminalReportHistory.UpdateTerminalReportBatchCounter(clsTerminalReportDetail.TerminalNo, dteDateToprocess);
                    clsTerminalReportHistory.UpdateTerminalReportIsMallFileUploadComplete(clsTerminalReportDetail.TerminalNo, dteDateToprocess, true);

                    if (clsTerminalReportDetail.BatchCounter == 1)
                        clsTerminalReportHistory.UpdateTerminalReportMallForwarderFileName(clsTerminalReportDetail.TerminalNo, dteDateToprocess, stDailyTableName);
                    
                    clsTerminalReportHistory.CommitAndDispose();

                    bolRetValue = true;
                }

                clsEvent.AddEventLn("Record for [" + dteDateToprocess.ToString("yyyy-MM-dd HH:mm:ss") + "] BacthCounter:" + clsTerminalReportDetail.BatchCounter.ToString() + " has been created for RLC.", true);

            }
            catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
                throw ex;
            }
            clsEvent.AddEventLn("RLC FILE CREATOR exited.", true);

            return bolRetValue;
        }
        private bool TransferFile(string pvtFileName)
        {
            bool bolRetValue = false;

            Event clsEvent = new Event();
            try
            {
                if (!string.IsNullOrEmpty(pvtFileName))
                {
                    if (string.IsNullOrEmpty(mclsRLCDetails.FTPIPAddress))
                    { 
                        clsEvent.AddEventLn("Cannot transfer file " + pvtFileName + ". FTP IPAddress is empty Automatic File transfer is disabled.", true); 
                        return bolRetValue; 
                    }
                    else
                        clsEvent.AddEventLn("Transferring " + pvtFileName + " to " + mclsRLCDetails.FTPIPAddress, true);
                }
                else
                {
                    clsEvent.AddEventLn("Cannot transfer an blank file.", true); return bolRetValue;
                }

                int intPort = 21;
                if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                {
                    try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                    catch { }
                }

                FtpClient ftpClient = new FtpClient();
                ftpClient.Host = mclsRLCDetails.FTPIPAddress;
                ftpClient.Port = intPort;
                ftpClient.Credentials = new NetworkCredential(mclsRLCDetails.FTPUsername, mclsRLCDetails.FTPPassword);

                IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(mclsRLCDetails.FTPDirectory, FtpListOption.Modify | FtpListOption.Size);

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
                catch (Exception excheck) {
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

                bolRetValue = true;
            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("Error encountered: " + ex.Message, true);
                throw new IOException("Sales file is not sent to RLC server. Please contact your POS vendor");
            }

            return bolRetValue;
        }
    }
}

