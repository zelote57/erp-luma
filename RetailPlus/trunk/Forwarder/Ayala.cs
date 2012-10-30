using System;
using System.IO;
using System.Security.Permissions;
using System.Data.OleDb;

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

    public struct AyalaDetails
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
    public class Ayala
    {
        private AyalaDetails mclsAyalaDetails;

        OleDbConnection mConnection;
        OleDbTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public AyalaDetails AyalaDetails
        {
            set { mclsAyalaDetails = value; }
        }

        #region Constructors and Destructors

        public Ayala()
        {

        }

        public void CommitAndDispose()
        {
            if (!TransactionFailed)
            {
                if (IsInTransaction)
                {
                    mTransaction.Commit();
                    mConnection.Close();
                    mConnection.Dispose();
                }
            }
        }

        #endregion

        private OleDbConnection GetConnection()
        {
            if (mConnection == null || TransactionFailed == true)
            {
                mConnection = new OleDbConnection(AceSoft.RetailPlus.Forwarder.DBConnection.DBFConnectionString(mclsAyalaDetails.OutputDirectory));
                mConnection.Open();

                mTransaction = (OleDbTransaction)mConnection.BeginTransaction();
                IsInTransaction = true;
            }

            return mConnection;
        }

        #region Create Table

        

        

        #endregion

        #region Public Methods

        private string CreateDailySales(DateTime pvtProcessDate, Data.TerminalReportDetails pvtTerminalReportDetails)
        {
            string strRetValue = "";
            try
            {
                string stDailyTableName = mclsAyalaDetails.TenantCode.Substring(0,3) + pvtProcessDate.ToString("MMdd");
                string stDailyFileName = mclsAyalaDetails.OutputDirectory + "\\" + stDailyTableName + ".dbf";
                if (File.Exists(stDailyFileName)) File.Delete(stDailyFileName);

                CreateDailySalesTable(stDailyTableName);
                InsertDailySales(stDailyTableName, pvtTerminalReportDetails);

                strRetValue = stDailyFileName;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return strRetValue;
        }
        private void CreateDailySalesTable(string stDailyTableName)
        {
            try
            {
                string SQL = "CREATE TABLE " + stDailyTableName + " (" +
                                "TRANDATE		date," +
                                "OLDGT			numeric(15,2)," +
                                "NEWGT			numeric(15,2)," +
                                "DLYSALE		numeric(11,2)," +
                                "TOTDISC		numeric(11,12)," +
                                "TOTREF			numeric(11,2)," +
                                "TOTCAN			numeric(11,2)," +
                                "VAT			numeric(11,2)," +
                                "TENTNAME		char(10)," +
                                "BEGININV		char(6)," +
                                "ENDIND			char(6)," +
                                "BEGOR			char(6)," +
                                "ENDOR			char(6)," +
                                "TRANCNT		numeric(9)," +
                                "LOCALTAX		numeric(11,2)," +
                                "SERVCHARGE		numeric(11,2)," +
                                "NOTAXSALE		numeric(11,2)," +
                                "RAWGROSS		numeric(11,2)," +
                                "DLYLOCTAX		numeric(11,2)," +
                                "OTHERS			numeric(11,2)," +
                                "TERMNUM		char(3)" +
                            ")";

                OleDbConnection cn = GetConnection();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        private void InsertDailySales(string pvtDailyTableName, Data.TerminalReportDetails pvtTerminalReportDetails)
        {
            try
            {
                string SQL = "INSERT INTO " + pvtDailyTableName + "(" +
                                                "TRANDATE," +
                                                "OLDGT," +
                                                "NEWGT," +
                                                "DLYSALE," +
                                                "TOTDISC," +
                                                "TOTREF," +
                                                "TOTCAN," +
                                                "VAT," +
                                                "TENTNAME," +
                                                "BEGININV," +
                                                "ENDIND," +
                                                "BEGOR," +
                                                "ENDOR," +
                                                "TRANCNT," +
                                                "LOCALTAX," +
                                                "SERVCHARGE," +
                                                "NOTAXSALE," +
                                                "RAWGROSS," +
                                                "DLYLOCTAX," +
                                                "OTHERS," +
                                                "TERMNUM" +
                                            ") VALUES (" +
                                                "@TRANDATE," +
                                                "@OLDGT," +
                                                "@NEWGT," +
                                                "@DLYSALE," +
                                                "@TOTDISC," +
                                                "@TOTREF," +
                                                "@TOTCAN," +
                                                "@VAT," +
                                                "@TENTNAME," +
                                                "@BEGININV," +
                                                "@ENDIND," +
                                                "@BEGOR," +
                                                "@ENDOR," +
                                                "@TRANCNT," +
                                                "@LOCALTAX," +
                                                "@SERVCHARGE," +
                                                "@NOTAXSALE," +
                                                "@RAWGROSS," +
                                                "@DLYLOCTAX," +
                                                "@OTHERS," +
                                                "@TERMNUM" +
                                            ");";

                OleDbConnection cn = GetConnection();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                OleDbParameter prmTRANDATE = new OleDbParameter("@TRANDATE", System.Data.OleDb.OleDbType.Date, 8);
                prmTRANDATE.Value = pvtTerminalReportDetails.DateLastInitialized.ToString("MM/dd/yyyy");
                cmd.Parameters.Add(prmTRANDATE);

                OleDbParameter prmOLDGT = new OleDbParameter("@OLDGT", System.Data.OleDb.OleDbType.Numeric, 17);
                prmOLDGT.Value = pvtTerminalReportDetails.OldGrandTotal;
                cmd.Parameters.Add(prmOLDGT);

                OleDbParameter prmNEWGT = new OleDbParameter("@NEWGT", System.Data.OleDb.OleDbType.Numeric, 17);
                prmNEWGT.Value = pvtTerminalReportDetails.NewGrandTotal;
                cmd.Parameters.Add(prmNEWGT);

                OleDbParameter prmDLYSALE = new OleDbParameter("@DLYSALE", System.Data.OleDb.OleDbType.Numeric, 13);
                prmDLYSALE.Value = pvtTerminalReportDetails.DailySales;
                cmd.Parameters.Add(prmDLYSALE);

                OleDbParameter prmTOTDISC = new OleDbParameter("@TOTDISC", System.Data.OleDb.OleDbType.Numeric, 13);
                prmTOTDISC.Value = pvtTerminalReportDetails.TotalDiscount;
                cmd.Parameters.Add(prmTOTDISC);

                OleDbParameter prmTOTREF = new OleDbParameter("@TOTREF", System.Data.OleDb.OleDbType.Numeric, 13);
                prmTOTREF.Value = pvtTerminalReportDetails.RefundSales;
                cmd.Parameters.Add(prmTOTREF);

                OleDbParameter prmTOTCAN = new OleDbParameter("@TOTCAN", System.Data.OleDb.OleDbType.Numeric, 13);
                prmTOTCAN.Value = pvtTerminalReportDetails.VoidSales;
                cmd.Parameters.Add(prmTOTCAN);

                OleDbParameter prmVAT = new OleDbParameter("@VAT", System.Data.OleDb.OleDbType.Numeric, 13);
                prmVAT.Value = pvtTerminalReportDetails.VAT;
                cmd.Parameters.Add(prmVAT);

                OleDbParameter prmTENTNAME = new OleDbParameter("@TENTNAME", System.Data.OleDb.OleDbType.VarChar, 10);
                prmTENTNAME.Value = mclsAyalaDetails.TenantName;
                cmd.Parameters.Add(prmTENTNAME);

                OleDbParameter prmBEGININV = new OleDbParameter("@BEGININV", System.Data.OleDb.OleDbType.VarChar, 6);
                prmBEGININV.Value = Convert.ToInt64(pvtTerminalReportDetails.BeginningTransactionNo).ToString();
                cmd.Parameters.Add(prmBEGININV);

                OleDbParameter prmENDIND = new OleDbParameter("@ENDIND", System.Data.OleDb.OleDbType.VarChar, 6);
                prmENDIND.Value = Convert.ToInt64(pvtTerminalReportDetails.EndingTransactionNo).ToString();
                cmd.Parameters.Add(prmENDIND);

                OleDbParameter prmBEGOR = new OleDbParameter("@BEGOR", System.Data.OleDb.OleDbType.VarChar, 6);
                prmBEGOR.Value = "";
                cmd.Parameters.Add(prmBEGOR);

                OleDbParameter prmENDOR = new OleDbParameter("@ENDOR", System.Data.OleDb.OleDbType.VarChar, 6);
                prmENDOR.Value = "";
                cmd.Parameters.Add(prmENDOR);

                OleDbParameter prmTRANCNT = new OleDbParameter("@TRANCNT", System.Data.OleDb.OleDbType.Numeric, 9);
                prmTRANCNT.Value = pvtTerminalReportDetails.NoOfTotalTransactions;
                cmd.Parameters.Add(prmTRANCNT);

                OleDbParameter prmLOCALTAX = new OleDbParameter("@LOCALTAX", System.Data.OleDb.OleDbType.Numeric, 13);
                prmLOCALTAX.Value = pvtTerminalReportDetails.LocalTax;
                cmd.Parameters.Add(prmLOCALTAX);

                OleDbParameter prmSERVCHARGE = new OleDbParameter("@SERVCHARGE", System.Data.OleDb.OleDbType.Numeric, 13);
                prmSERVCHARGE.Value = 0;
                cmd.Parameters.Add(prmSERVCHARGE);

                OleDbParameter prmNOTAXSALE = new OleDbParameter("@NOTAXSALE", System.Data.OleDb.OleDbType.Numeric, 13);
                prmNOTAXSALE.Value = pvtTerminalReportDetails.NonVaTableAmount;
                cmd.Parameters.Add(prmNOTAXSALE);

                OleDbParameter prmRAWGROSS = new OleDbParameter("@RAWGROSS", System.Data.OleDb.OleDbType.Numeric, 13);
                prmRAWGROSS.Value = pvtTerminalReportDetails.GrossSales;
                cmd.Parameters.Add(prmRAWGROSS);

                OleDbParameter prmDLYLOCTAX = new OleDbParameter("@DLYLOCTAX", System.Data.OleDb.OleDbType.Numeric, 13);
                prmDLYLOCTAX.Value = pvtTerminalReportDetails.LocalTax;	//pvtTerminalReportDetails.DailySales - pvtTerminalReportDetails.LocalTax;
                cmd.Parameters.Add(prmDLYLOCTAX);

                OleDbParameter prmOTHERS = new OleDbParameter("@OTHERS", System.Data.OleDb.OleDbType.Numeric);
                prmOTHERS.Value = 0;
                cmd.Parameters.Add(prmOTHERS);

                OleDbParameter prmTERMNUM = new OleDbParameter("@TERMNUM", System.Data.OleDb.OleDbType.VarChar, 3);
                prmTERMNUM.Value = pvtTerminalReportDetails.TerminalNo;
                cmd.Parameters.Add(prmTERMNUM);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        private string CreateHourlySales(int BranchID, string TerminalNo, DateTime pvtProcessDate)
        {
            string strRetValue = "";
            DateTime dteDateFrom = pvtProcessDate;

            Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
            DateTime dteDateTo = DateTime.MinValue;
            try
            {
                dteDateTo = clsTerminalReportHistory.NEXTDateLastInitialized(BranchID, TerminalNo, pvtProcessDate);
            }
            catch { }

            if (dteDateTo == DateTime.MinValue)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("HourlySales: Did not found MAXDateLastInitialized from Terminal Report History. Using the MAXDateLastInitialized from terminal report", true);
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(clsTerminalReportHistory.Connection, clsTerminalReportHistory.Transaction);
                dteDateTo = clsTerminalReport.MAXDateLastInitialized(TerminalNo, pvtProcessDate);
            }

            System.Data.DataTable dthreport = clsTerminalReportHistory.HourlyReport(BranchID, TerminalNo, dteDateFrom, dteDateTo);
            clsTerminalReportHistory.CommitAndDispose();

            string stHourlyTableName = mclsAyalaDetails.TenantCode.Substring(0, 3) + pvtProcessDate.ToString("MMdd") + "H";
            string stHourlyFileName = mclsAyalaDetails.OutputDirectory + "\\" + stHourlyTableName + ".dbf";
            if (File.Exists(stHourlyFileName)) File.Delete(stHourlyFileName);

            CreateHourlySalesTable(stHourlyTableName);
            InsertHourlySales(stHourlyTableName, dthreport);

            strRetValue = stHourlyFileName;

            return strRetValue;
        }
        private void CreateHourlySalesTable(string stHourlyTableName)
        {
            try
            {
                string SQL = "CREATE TABLE " + stHourlyTableName + " (" +
                                "TRANDATE		date," +
                                "[HOUR]			char(5)," +
                                "SALES			numeric(11,2)," +
                                "TRANCNT		numeric(9)," +
                                "TENTNAME		char(10)," +
                                "TERMNUM		char(3)" +
                            ")";

                OleDbConnection cn = GetConnection();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void InsertHourlySales(string stHourlyTableName, System.Data.DataTable dtHourlyReport)
        {
            try
            {
                Data.HourlyReportDetails clsHourlyReportDetails;

                foreach (System.Data.DataRow dr in dtHourlyReport.Rows)
                {
                    clsHourlyReportDetails = new Data.HourlyReportDetails();
                    clsHourlyReportDetails.TRANDATE = Convert.ToDateTime(dr["TransactionDate"]);
                    clsHourlyReportDetails.HOUR = dr["Time"].ToString();
                    clsHourlyReportDetails.SALES = Convert.ToDecimal(dr["Amount"]);
                    clsHourlyReportDetails.TRANCNT = Convert.ToInt64(dr["TranCount"]);
                    clsHourlyReportDetails.TENTNAME = mclsAyalaDetails.TenantName;
                    clsHourlyReportDetails.TERMNUM = dr["TerminalNo"].ToString();
                    InsertHourlySales(stHourlyTableName, clsHourlyReportDetails);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void InsertHourlySales(string stHourlyTableName, Data.HourlyReportDetails HourlyReportDetails)
        {
            try
            {
                string SQL = "INSERT INTO " + stHourlyTableName + "(" +
                                "TRANDATE," +
                                "[HOUR]," +
                                "SALES," +
                                "TRANCNT," +
                                "TENTNAME," +
                                "TERMNUM" +
                            ") VALUES (" +
                                "@TRANDATE," +
                                "@HOUR," +
                                "@SALES," +
                                "@TRANCNT," +
                                "@TENTNAME," +
                                "@TERMNUM" +
                            ");";

                OleDbConnection cn = GetConnection();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                OleDbParameter prmTRANDATE = new OleDbParameter("@TRANDATE", System.Data.OleDb.OleDbType.Date, 8);
                prmTRANDATE.Value = HourlyReportDetails.TRANDATE.ToString("MM/dd/yyyy");
                cmd.Parameters.Add(prmTRANDATE);

                OleDbParameter prmHOUR = new OleDbParameter("@HOUR", System.Data.OleDb.OleDbType.Char, 5);
                prmHOUR.Value = HourlyReportDetails.HOUR;
                cmd.Parameters.Add(prmHOUR);

                OleDbParameter prmSALES = new OleDbParameter("@SALES", System.Data.OleDb.OleDbType.Numeric, 13);
                prmSALES.Value = HourlyReportDetails.SALES;
                cmd.Parameters.Add(prmSALES);

                OleDbParameter prmTRANCNT = new OleDbParameter("@TRANCNT", System.Data.OleDb.OleDbType.Numeric, 9);
                prmTRANCNT.Value = HourlyReportDetails.TRANCNT;
                cmd.Parameters.Add(prmTRANCNT);

                OleDbParameter prmTENTNAME = new OleDbParameter("@TENTNAME", System.Data.OleDb.OleDbType.Char, 10);
                prmTENTNAME.Value = HourlyReportDetails.TENTNAME;
                cmd.Parameters.Add(prmTENTNAME);

                OleDbParameter prmTERMNUM = new OleDbParameter("@TERMNUM", System.Data.OleDb.OleDbType.VarChar, 3);
                prmTERMNUM.Value = HourlyReportDetails.TERMNUM;
                cmd.Parameters.Add(prmTERMNUM);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        #endregion

        public void CreateAndTransferFile(int BranchID, string TerminalNo, DateTime pvtDateInitialized)
        {
            DateTime dteDateToprocess = pvtDateInitialized;

            Event clsEvent = new Event();
            try
            {
                clsEvent.AddEventLn("Starting Ayala FILE CREATOR.", true);
                clsEvent.AddEventLn("==================================================", true);
                clsEvent.AddEventLn("=                Ayala FILE CREATOR                =", true);
                clsEvent.AddEventLn("==================================================", true);

                /***********************************************************************
                 * Check the destination dir if existing.
                 * ********************************************************************/
                clsEvent.AddEventLn("Checking directory settings.", true);
                string dir = mclsAyalaDetails.OutputDirectory;
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
                mclsAyalaDetails.OutputDirectory = dir;

                /***********************************************************************
                 * GET The report of Current Terminal using Specified InitializationDate
                 * ********************************************************************/
                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
                Data.TerminalReportDetails clsTerminalReportDetail = clsTerminalReportHistory.Details(TerminalNo, dteDateToprocess);
                string stDailyTableName = CreateDailySales(dteDateToprocess, clsTerminalReportDetail);
                string stHourlyTableName = CreateHourlySales(BranchID, TerminalNo, dteDateToprocess);
                clsTerminalReportHistory.UpdateTerminalReportBatchCounter(TerminalNo, dteDateToprocess);
                clsTerminalReportHistory.CommitAndDispose();
                clsEvent.AddEventLn("Record for [" + dteDateToprocess.ToString("yyyy-MM-dd HH:mm:ss") + "] BacthCounter:" + clsTerminalReportDetail.BatchCounter.ToString() + " has been created for Ayala.", true);

                TransferFile(stDailyTableName);
                TransferFile(stHourlyTableName);
            }
            catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
                throw ex;
            }
            clsEvent.AddEventLn("Ayala FILE CREATOR exited.", true);
        }
        private void TransferFile(string pvtFileName)
        {
            Event clsEvent = new Event();
            try
            {


                if (pvtFileName != "")
                {
                    if (mclsAyalaDetails.FTPIPAddress == null || mclsAyalaDetails.FTPIPAddress == string.Empty)
                    { 
                        clsEvent.AddEventLn("Cannot transfer file " + pvtFileName + ". FTP IPAddress is empty Automatic File transfer is disabled.", true); 
                        return; 
                    }
                    else
                        clsEvent.AddEventLn("Transferring " + pvtFileName + " to " + mclsAyalaDetails.FTPIPAddress, true);
                }
                else
                {
                    clsEvent.AddEventLn("Cannot transfer an blank file.", true); return;
                }

                FTP clsFTP = new FTP();
                clsFTP.Connect(mclsAyalaDetails.FTPIPAddress, mclsAyalaDetails.FTPUsername, mclsAyalaDetails.FTPPassword);
                if (mclsAyalaDetails.FTPDirectory != null && mclsAyalaDetails.FTPDirectory != string.Empty)
                    clsFTP.ChangeDirectory(mclsAyalaDetails.FTPDirectory);

                bool bolIsFileExist = false;
                foreach (FTP.File file in clsFTP.Files)
                {
                    if (file.FileName.ToUpper() == Path.GetFileName(pvtFileName).ToUpper())
                    { bolIsFileExist = true; break; }
                }

                if (bolIsFileExist == true)
                    clsFTP.Files.Upload(Path.GetFileName(pvtFileName), pvtFileName, true);
                else
                    clsFTP.Files.Upload(Path.GetFileName(pvtFileName), pvtFileName);

                while (!clsFTP.Files.UploadComplete)
                {
                    clsEvent.AddEventLn("Uploading: TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString(), true);
                }
                clsEvent.AddEventLn("Upload Complete: TotalBytes: " + clsFTP.Files.TotalBytes.ToString() + ", : PercentComplete: " + clsFTP.Files.PercentComplete.ToString(), true);

                clsFTP.Disconnect();
                clsFTP = null;
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

