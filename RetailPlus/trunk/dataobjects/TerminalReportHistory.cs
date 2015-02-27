using System;
using System.Security.Permissions;
using System.Collections;
using MySql.Data.MySqlClient;

// March 29, 2006 - 
//		Remove the "OR TransactionStatus = @TransactionStatusVoid " + from the PLUReport function
//		Remove the "OR TransactionStatus = @TransactionStatusVoid " + from the GroupReportPrivate function
//		Remove the "OR TransactionStatus = @TransactionStatusVoid " + from the HourlyReportPrivate function
//		Remove the "OR TransactionStatus = @TransactionStatusVoid " + from the EJournalReport function

namespace AceSoft.RetailPlus.Data
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]

	public class TerminalReportHistory : POSConnection
	{
		#region Constructors & Destructors

		public TerminalReportHistory()
            : base(null, null)
        {
        }

        public TerminalReportHistory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public void UpdateTerminalReportBatchCounter(string pvtTerminalNo, DateTime pvtDateLastInitialized)
        {
            try
            {
                string SQL = "CALL procUpdateTerminalReportBatchCounter(@TerminalNo, @DateLastInitialized);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", pvtTerminalNo);
                cmd.Parameters.AddWithValue("@DateLastInitialized", pvtDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateTerminalReportMallForwarderFileName(string pvtTerminalNo, DateTime pvtDateLastInitialized, string pvtMallFileName)
        {
            try
            {
                string SQL = "CALL procUpdateTerminalReportMallForwarderFileName(@TerminalNo, @DateLastInitialized, @MallFileName);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", pvtTerminalNo);
                cmd.Parameters.AddWithValue("@DateLastInitialized", pvtDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@MallFileName", pvtMallFileName);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateTerminalReportIsMallFileUploadComplete(string pvtTerminalNo, DateTime pvtDateLastInitialized, bool pvtIsMallFileUploadComplete)
        {
            try
            {
                string SQL = "CALL procUpdateTerminalReportIsMallFileUploadComplete(@TerminalNo, @DateLastInitialized, @IsMallFileUploadComplete);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", pvtTerminalNo);
                cmd.Parameters.AddWithValue("@DateLastInitialized", pvtDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@IsMallFileUploadComplete", Convert.ToInt16(pvtIsMallFileUploadComplete));

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

		#region Details

        private TerminalReportDetails getDetails(Int32 BranchID, string TerminalNo, DateTime? DateFrom, DateTime? DateTo, DateTime? DateLastInitialized, bool WithTF = false, bool LastInitializationDetails = false, bool NextDetails = false)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(BranchID, TerminalNo, false, DateFrom, DateTo, DateLastInitialized, WithTF, LastInitializationDetails, NextDetails);

                return new TerminalReport(base.Connection, base.Transaction).SetDetails(dt);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public TerminalReportDetails LastInitializationDetails(Int32 BranchID, string TerminalNo, DateTime? DateFrom, DateTime? DateTo, DateTime? DateLastInitialized)
		{
			try
			{
                return getDetails(BranchID, TerminalNo, Constants.C_DATE_MIN_VALUE, Constants.C_DATE_MIN_VALUE, DateLastInitialized, false, true, false);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public TerminalReportDetails Details(Int32 BranchID, string TerminalNo, DateTime DateLastInitialized, bool WithTF = false)
        {
            try
            {
                return getDetails(BranchID, TerminalNo, Constants.C_DATE_MIN_VALUE, Constants.C_DATE_MIN_VALUE, DateLastInitialized, WithTF, true, false);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public TerminalReportDetails Details(Int32 BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
		{
			try
			{
                return getDetails(BranchID, TerminalNo, DateFrom, DateTo, Constants.C_DATE_MIN_VALUE, false, true, false);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public TerminalReportDetails NextDetails(Int32 BranchID, string TerminalNo, DateTime DateLastInitialized)
        {
            try
            {
                return getDetails(BranchID, TerminalNo, Constants.C_DATE_MIN_VALUE, Constants.C_DATE_MIN_VALUE, DateLastInitialized, false, false, true);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Streams

		public TerminalReportDetails[] List(DateTime DateFrom, DateTime DateTo)
		{
			try
			{
                System.Data.DataTable dt = ListAsDataTable(0, string.Empty, false, DateFrom, DateTo, Constants.C_DATE_MIN_VALUE, false, false, false);

                return new TerminalReport(base.Connection, base.Transaction).SetDetailsList(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	
		#region Streams : Report

        public System.Data.DataTable ListAsDataTable(Int32 BranchID, string TerminalNo = "", bool OnlyIncludeIneSales = false, DateTime? DateFrom = null, DateTime? DateTo = null, DateTime? DateLastInitialized = null, bool WithTF = false, bool LastInitializationDetails = false, bool NextDetails = false)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportHistorySelect(@BranchID, @TerminalNo, @OnlyIncludeIneSales, @DateFrom, @DateTo, @DateLastInitialized, @WithTF, @LastInitializationDetails, @NextDetails);";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("OnlyIncludeIneSales", OnlyIncludeIneSales);
                cmd.Parameters.AddWithValue("DateFrom", DateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : DateFrom);
                cmd.Parameters.AddWithValue("DateTo", DateTo.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : DateTo);
                cmd.Parameters.AddWithValue("DateLastInitialized", DateLastInitialized.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : DateLastInitialized);
                cmd.Parameters.AddWithValue("WithTF", WithTF);
                cmd.Parameters.AddWithValue("LastInitializationDetails", LastInitializationDetails);
                cmd.Parameters.AddWithValue("NextDetails", NextDetails);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable SummarizedDailySalesReport(Int32 BranchID = 0, string TerminalNo = "", bool OnlyIncludeIneSales = false, DateTime? DateFrom = null, DateTime? DateTo = null, bool WithTF = false)
        {
            try
            {
                return ListAsDataTable(BranchID, TerminalNo, OnlyIncludeIneSales, DateFrom, DateTo, Constants.C_DATE_MIN_VALUE, WithTF, false, false);

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public System.Data.DataTable HourlyReport(Int32 BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = "SELECT BranchID" +
                                    "TerminalNo, " +
                                    "DATE(TransactionDate) 'TransactionDate', " +
                                    "HOUR(TransactionDate) 'Time', " +
                                    "COUNT(SubTotal) 'TranCount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, SubTotal - Discount)) 'Amount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, VAT)) 'VAT', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, Discount)) 'Discount' " +
                            "FROM  tblTransactions " +
                            "WHERE BranchID = @BranchID " +
                                    "AND TerminalNo = @TerminalNo " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                    "OR TransactionStatus = @TransactionStatusVoid " +
                                    "OR TransactionStatus = @TransactionStatusReprinted " +
                                    "OR TransactionStatus = @TransactionStatusRefund " +
                                    "OR TransactionStatus = @TransactionStatusCreditPayment) " +
                                    "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
                                    "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') " +
                            "GROUP BY DATE(TransactionDate), HOUR(TransactionDate) " +
                            "ORDER BY TerminalNo, TransactionDate";

            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
            cmd.Parameters.AddWithValue("@TransactionStatusClosed", (Int16)TransactionStatus.Closed);
            cmd.Parameters.AddWithValue("@TransactionStatusVoid", (Int16)TransactionStatus.Void);
            cmd.Parameters.AddWithValue("@TransactionStatusReprinted", (Int16)TransactionStatus.Reprinted);
            cmd.Parameters.AddWithValue("@TransactionStatusRefund", (Int16)TransactionStatus.Refund);
            cmd.Parameters.AddWithValue("@TransactionStatusCreditPayment", (Int16)TransactionStatus.Closed);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);

            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}

		#endregion

		#region Public Modifiers

        public DateTime MINDateLastInitialized(Int32 BranchID, string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
                // Uses MAX coz the DateLastInitialized is >= ProcessingDate 
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
					            "MAX(DateLastInitialized) AS DateLastInitialized " +
					        "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";
  
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@ProcessingDate", ProcessingDate);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    dteRetValue = DateTime.Parse(dr["DateLastInitialized"].ToString());
                }
				
				return dteRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public DateTime MAXDateLastInitialized(Int32 BranchID, string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
                // Uses MAX coz the DateLastInitialized is <= ProcessingDate 
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "MIN(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@ProcessingDate", ProcessingDate);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["DateLastInitialized"].ToString()))
                        dteRetValue = DateTime.Parse(dr["DateLastInitialized"].ToString());
                }

                return dteRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public DateTime NEXTDateLastInitialized(Int32 BranchID, string TerminalNo, DateTime ProcessingDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "MIN(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') > DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@ProcessingDate", ProcessingDate);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = Constants.C_DATE_MIN_VALUE; // always use constants DateTime.MinValue;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["DateLastInitialized"].ToString()))
                        dteRetValue = DateTime.Parse(dr["DateLastInitialized"].ToString());
                }

                return dteRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DateTime PREVDateLastInitialized(Int32 BranchID, string TerminalNo, DateTime ProcessingDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "MAX(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') < DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@ProcessingDate", ProcessingDate);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = Constants.C_DATE_MIN_VALUE; // always use constants DateTime.MinValue;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["DateLastInitialized"].ToString()))
                        dteRetValue = DateTime.Parse(dr["DateLastInitialized"].ToString());
                }

                return dteRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DateTime getRLCDateLastInitialized(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "MIN(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND IsMallFileUploadComplete = 0 ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["DateLastInitialized"].ToString()))
                        dteRetValue = DateTime.Parse(dr["DateLastInitialized"].ToString());
                }

                return dteRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable DatesLastInitialized(Int32 BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DateLastInitialized, " +
                             "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE " + (BranchID != 0 ? "BranchID = @BranchID AND " : "") + "TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay " +
                             "FROM tblTerminalReportHistory " +
                             "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') " +
                             "ORDER BY DateLastInitialized DESC ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable DatesLastInitializedForRLC(Int32 BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DateLastInitialized, DATE_FORMAT(IF(HOUR(DateLastInitialized)>5, DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedDisplay FROM tblTerminalReportHistory " +
                             "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                                 "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
                                 "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') " +
                             "ORDER BY DateLastInitialized DESC ";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", DateTo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

	}
}