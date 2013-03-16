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

        private string SQLSelect()
        {
            string SQL = "SELECT BranchID, TerminalNo, " +
                                "BeginningTransactionNo, " +
                                "EndingTransactionNo, " +
                                "ZReadCount, " +
                                "XReadCount, " +
                                "GrossSales, " +
                                "TotalDiscount, " +
                                "TotalCharge, " +
                                "DailySales, " +
                                "QuantitySold, " +
                                "GroupSales, " +
                                "OldGrandTotal, " +
                                "NewGrandTotal, " +
                                "VATableAmount, " +
                                "NonVaTableAmount, " +
                                "VAT, " +
                                "EVATableAmount, " +
                                "NonEVaTableAmount, " +
                                "EVAT, " +
                                "LocalTax, " +
                                "CashSales, " +
                                "ChequeSales, " +
                                "CreditCardSales, " +
                                "CreditSales, " +
                                "CreditPayment, " +
                                "DebitPayment, " +
                                "RewardPointsPayment, " +
                                "RewardConvertedPayment, " +
                                "CashInDrawer, " +
                                "TotalDisburse, " +
                                "CashDisburse, " +
                                "ChequeDisburse, " +
                                "CreditCardDisburse, " +
                                "TotalWithhold, " +
                                "CashWithhold, " +
                                "ChequeWithhold, " +
                                "CreditCardWithhold, " +
                                "TotalPaidOut, " +
                                "TotalDeposit, " +
                                "CashDeposit, " +
                                "ChequeDeposit, " +
                                "CreditCardDeposit, " +
                                "BeginningBalance, " +
                                "VoidSales, " +
                                "RefundSales, " +
                                "ItemsDiscount, " +
                                "SubTotalDiscount, " +
                                "NoOfCashTransactions, " +
                                "NoOfChequeTransactions, " +
                                "NoOfCreditCardTransactions, " +
                                "NoOfCreditTransactions, " +
                                "NoOfCombinationPaymentTransactions, " +
                                "NoOfCreditPaymentTransactions, " +
                                "NoOfDebitPaymentTransactions, " +
                                "NoOfClosedTransactions, " +
                                "NoOfRefundTransactions, " +
                                "NoOfVoidTransactions, " +
                                "NoOfRewardPointsPayment, " +
                                "NoOfTotalTransactions, " +
                                "DateLastInitialized, " +
                                "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay, " +
                                "TrustFund, " +
                                "NoOfDiscountedTransactions, " +
                                "NegativeAdjustments, " +
                                "NoOfNegativeAdjustmentTransactions, " +
                                "PromotionalItems, " +
                                "CreditSalesTax, " +
                                "BatchCounter, " +
                                "InitializedBy " +
                            "FROM tblTerminalReportHistory ";
            return SQL;
        }

		#region Details

        private TerminalReportDetails SetDetails(MySqlDataReader myReader)
        {
            TerminalReportDetails Details = new TerminalReportDetails();

            while (myReader.Read())
            {
                Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
                Details.BeginningTransactionNo = "" + myReader["BeginningTransactionNo"].ToString();
                Details.EndingTransactionNo = "" + myReader["EndingTransactionNo"].ToString();
                Details.ZReadCount = myReader.GetInt32("ZReadCount");
                Details.XReadCount = myReader.GetInt32("XReadCount");
                Details.GrossSales = myReader.GetDecimal("GrossSales");
                Details.TotalDiscount = myReader.GetDecimal("TotalDiscount");
                Details.TotalCharge = myReader.GetDecimal("TotalCharge");
                Details.DailySales = myReader.GetDecimal("DailySales");
                Details.QuantitySold = myReader.GetDecimal("QuantitySold");
                Details.GroupSales = myReader.GetDecimal("GroupSales");
                Details.OldGrandTotal = myReader.GetDecimal("OldGrandTotal");
                Details.NewGrandTotal = myReader.GetDecimal("NewGrandTotal");
                Details.VATableAmount = myReader.GetDecimal("VATableAmount");
                Details.NonVaTableAmount = myReader.GetDecimal("NonVaTableAmount");
                Details.VAT = myReader.GetDecimal("VAT");
                Details.EVATableAmount = myReader.GetDecimal("EVATableAmount");
                Details.NonEVaTableAmount = myReader.GetDecimal("NonEVaTableAmount");
                Details.EVAT = myReader.GetDecimal("EVAT");
                Details.LocalTax = myReader.GetDecimal("LocalTax");
                Details.CashSales = myReader.GetDecimal("CashSales");
                Details.ChequeSales = myReader.GetDecimal("ChequeSales");
                Details.CreditCardSales = myReader.GetDecimal("CreditCardSales");
                Details.CreditSales = myReader.GetDecimal("CreditSales");
                Details.CreditPayment = myReader.GetDecimal("CreditPayment");
                Details.DebitPayment = myReader.GetDecimal("DebitPayment");
                Details.RewardPointsPayment = myReader.GetDecimal("RewardPointsPayment");
                Details.RewardConvertedPayment = myReader.GetDecimal("RewardConvertedPayment");
                Details.CashInDrawer = myReader.GetDecimal("CashInDrawer");
                Details.TotalDisburse = myReader.GetDecimal("TotalDisburse");
                Details.CashDisburse = myReader.GetDecimal("CashDisburse");
                Details.ChequeDisburse = myReader.GetDecimal("ChequeDisburse");
                Details.CreditCardDisburse = myReader.GetDecimal("CreditCardDisburse");
                Details.TotalWithHold = myReader.GetDecimal("TotalWithhold");
                Details.CashWithHold = myReader.GetDecimal("CashWithhold");
                Details.ChequeWithHold = myReader.GetDecimal("ChequeWithhold");
                Details.CreditCardWithHold = myReader.GetDecimal("CreditCardWithhold");
                Details.TotalPaidOut = myReader.GetDecimal("TotalPaidOut");
                Details.TotalDeposit = myReader.GetDecimal("TotalDeposit");
                Details.CashDeposit = myReader.GetDecimal("CashDeposit");
                Details.ChequeDeposit = myReader.GetDecimal("ChequeDeposit");
                Details.CreditCardDeposit = myReader.GetDecimal("CreditCardDeposit");
                Details.BeginningBalance = myReader.GetDecimal("BeginningBalance");
                Details.VoidSales = myReader.GetDecimal("VoidSales");
                Details.RefundSales = myReader.GetDecimal("RefundSales");
                Details.ItemsDiscount = myReader.GetDecimal("ItemsDiscount");
                Details.SubTotalDiscount = myReader.GetDecimal("SubTotalDiscount");
                Details.NoOfCashTransactions = myReader.GetInt32("NoOfCashTransactions");
                Details.NoOfChequeTransactions = myReader.GetInt32("NoOfChequeTransactions");
                Details.NoOfCreditCardTransactions = myReader.GetInt32("NoOfCreditCardTransactions");
                Details.NoOfCreditTransactions = myReader.GetInt32("NoOfCreditTransactions");
                Details.NoOfCombinationPaymentTransactions = myReader.GetInt32("NoOfCombinationPaymentTransactions");
                Details.NoOfCreditPaymentTransactions = myReader.GetInt32("NoOfCreditPaymentTransactions");
                Details.NoOfDebitPaymentTransactions = myReader.GetInt32("NoOfDebitPaymentTransactions");
                Details.NoOfClosedTransactions = myReader.GetInt32("NoOfTotalTransactions");
                Details.NoOfRefundTransactions = myReader.GetInt32("NoOfRefundTransactions");
                Details.NoOfVoidTransactions = myReader.GetInt32("NoOfVoidTransactions");
                Details.NoOfRewardPointsPayment = myReader.GetInt32("NoOfRewardPointsPayment");
                Details.NoOfTotalTransactions = myReader.GetInt32("NoOfTotalTransactions");
                Details.DateLastInitialized = myReader.GetDateTime("DateLastInitialized");
                Details.DateLastInitializedToDisplay = myReader.GetDateTime("DateLastInitializedToDisplay");
                Details.TrustFund = myReader.GetDecimal("TrustFund");
                Details.NoOfDiscountedTransactions = myReader.GetInt32("NoOfDiscountedTransactions");
                Details.NegativeAdjustments = myReader.GetDecimal("NegativeAdjustments");
                Details.NoOfNegativeAdjustmentTransactions = myReader.GetInt32("NoOfNegativeAdjustmentTransactions");
                Details.PromotionalItems = myReader.GetDecimal("PromotionalItems");
                Details.CreditSalesTax = myReader.GetDecimal("CreditSalesTax");
                Details.BatchCounter = myReader.GetInt32("BatchCounter");
                Details.InitializedBy = "" + myReader["InitializedBy"].ToString();
            }

            return Details;
        }
		public TerminalReportDetails LastInitializationDetails(string TerminalNo)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE TerminalNo = @TerminalNo " +
					        "ORDER BY DateLastInitialized DESC " +
					        "LIMIT 1 ";
							
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalReportDetails Details = SetDetails(myReader);

                myReader.Close();

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public TerminalReportDetails LastInitializationDetails(string TerminalNo, DateTime DateFrom)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE TerminalNo = @TerminalNo " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
					        "ORDER BY DateLastInitialized DESC " +
					        "LIMIT 1 ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
				prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateFrom);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalReportDetails Details = SetDetails(myReader);

                myReader.Close();

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public TerminalReportDetails Details(string TerminalNo, DateTime DateLastInitialized)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') = DATE_FORMAT(@DateLastInitialized, '%Y-%m-%d %H:%i') ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);
                prmDateLastInitialized.Value = DateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateLastInitialized);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalReportDetails Details = SetDetails(myReader);

                myReader.Close();

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public TerminalReportDetails Details(string TerminalNo, DateTime DateFrom, DateTime DateTo)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE TerminalNo = @TerminalNo " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i');";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
				prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateFrom);

				MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
				prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateTo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalReportDetails Details = SetDetails(myReader);

                myReader.Close();

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public TerminalReportDetails NextDetails(string TerminalNo, DateTime DateLastInitialized)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') > DATE_FORMAT(@DateLastInitialized, '%Y-%m-%d %H:%i') LIMIT 1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);
                prmDateLastInitialized.Value = DateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateLastInitialized);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalReportDetails Details = SetDetails(myReader);

                myReader.Close();

                return Details;
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
				string SQL=	SQLSelect() + "WHERE DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
					"AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i');";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
				prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateFrom);

				MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
				prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateTo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();

				while (myReader.Read())
				{
                    TerminalReportDetails Details = SetDetails(myReader);

					items.Add(Details);
				}
				
				myReader.Close();

				TerminalReportDetails[] ReportDetails = new TerminalReportDetails[0];

				if (items != null)
				{
					ReportDetails = new TerminalReportDetails[items.Count];
					items.CopyTo(ReportDetails);
				}

				return ReportDetails;

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	
		#region Streams : Report

        public System.Data.DataTable SummarizedDailySalesReport(int BranchID = 0, bool WithTF = false, string TerminalNo = "", DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = string.Empty;
                if (WithTF)
                    #region SQL With TF
                    SQL = "SELECT " +
                                "QuantitySold, " +
                                "GrossSales - (GrossSales * TrustFund/100) 'GrossSales', " +
                                "TotalDiscount - (TotalDiscount * TrustFund/100) 'TotalDiscount', " +
                                "TotalCharge - (TotalCharge * TrustFund/100) 'TotalCharge', " +
                                "DailySales - (DailySales * TrustFund/100) 'DailySales', " +
                                "VAT - (VAT * TrustFund/100) 'VAT', " +
                                "LocalTax - (LocalTax * TrustFund/100) 'LocalTax', " +
                                "TotalCharge - (TotalCharge * TrustFund/100) AS 'ServiceCharge', " +
                                "DateLastInitialized, " +
                                "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE " + (BranchID != 0? "BranchID = @BranchID AND ":"") + "TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay, " +
                                "TerminalNo " +
                            "FROM  tblTerminalReportHistory " +
                            "WHERE 1=1 ";
                    #endregion
                else
                    #region SQL Without TF
                    SQL = "SELECT " +
					            "QuantitySold, " +
					            "GrossSales, " +
					            "TotalDiscount, " +
					            "TotalCharge, " +
					            "DailySales, " +
					            "VAT, " +
					            "LocalTax, " +
					            "TotalCharge AS 'ServiceCharge', " +
					            "(DateLastInitialized) 'DateLastInitialized', " +
                                "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE " + (BranchID != 0 ? "BranchID = @BranchID AND " : "") + "TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay, " +
					            "TerminalNo " +
					        "FROM  tblTerminalReportHistory " + 
					        "WHERE 1=1 ";
                    #endregion

                if (BranchID != 0)
                {
                    SQL += "AND BranchID = @BranchID";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                }

                if (TerminalNo != null && TerminalNo != "" && TerminalNo != Constants.ALL)
                {
                    SQL += "AND TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                }

                if (DateFrom.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') ";
                    MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
                    prmDateFrom.Value = DateFrom.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add(prmDateFrom);
                }
                if (DateTo.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') ";
                    MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
                    prmDateTo.Value = DateTo.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add(prmDateTo);
                }

                SQL += "ORDER BY TerminalNo, DateLastInitialized ";

                cmd.CommandType = System.Data.CommandType.Text;
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
		private MySqlDataReader HourlyReportPrivate(string TerminalNo, DateTime DateFrom, DateTime DateTo, int BranchID)
		{
			try
			{
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

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@TerminalNo",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTransactionStatusClosed = new MySqlParameter("@TransactionStatusClosed",MySqlDbType.Int16);			
				prmTransactionStatusClosed.Value = (Int16) TransactionStatus.Closed;
				cmd.Parameters.Add(prmTransactionStatusClosed);

				MySqlParameter prmTransactionStatusVoid = new MySqlParameter("@TransactionStatusVoid",MySqlDbType.Int16);			
				prmTransactionStatusVoid.Value = (Int16) TransactionStatus.Void;
				cmd.Parameters.Add(prmTransactionStatusVoid);

				MySqlParameter prmTransactionStatusReprinted = new MySqlParameter("@TransactionStatusReprinted",MySqlDbType.Int16);			
				prmTransactionStatusReprinted.Value = (Int16) TransactionStatus.Reprinted;
				cmd.Parameters.Add(prmTransactionStatusReprinted);

				MySqlParameter prmTransactionStatusRefund = new MySqlParameter("@TransactionStatusRefund",MySqlDbType.Int16);			
				prmTransactionStatusRefund.Value = (Int16) TransactionStatus.Refund;
				cmd.Parameters.Add(prmTransactionStatusRefund);

				MySqlParameter prmTransactionStatusCreditPayment = new MySqlParameter("@TransactionStatusCreditPayment",MySqlDbType.Int16);			
				prmTransactionStatusCreditPayment.Value = (Int16) TransactionStatus.CreditPayment;
				cmd.Parameters.Add(prmTransactionStatusCreditPayment);

				MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
				prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateFrom);

				MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
				prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateTo);
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public System.Data.DataTable HourlyReport(int BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
		{
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

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            MySqlParameter prmBranchID = new MySqlParameter("@TerminalNo", MySqlDbType.Int32);
            prmBranchID.Value = BranchID;
            cmd.Parameters.Add(prmBranchID);

            MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo", MySqlDbType.String);
            prmTerminalNo.Value = TerminalNo;
            cmd.Parameters.Add(prmTerminalNo);

            MySqlParameter prmTransactionStatusClosed = new MySqlParameter("@TransactionStatusClosed", MySqlDbType.Int16);
            prmTransactionStatusClosed.Value = (Int16)TransactionStatus.Closed;
            cmd.Parameters.Add(prmTransactionStatusClosed);

            MySqlParameter prmTransactionStatusVoid = new MySqlParameter("@TransactionStatusVoid", MySqlDbType.Int16);
            prmTransactionStatusVoid.Value = (Int16)TransactionStatus.Void;
            cmd.Parameters.Add(prmTransactionStatusVoid);

            MySqlParameter prmTransactionStatusReprinted = new MySqlParameter("@TransactionStatusReprinted", MySqlDbType.Int16);
            prmTransactionStatusReprinted.Value = (Int16)TransactionStatus.Reprinted;
            cmd.Parameters.Add(prmTransactionStatusReprinted);

            MySqlParameter prmTransactionStatusRefund = new MySqlParameter("@TransactionStatusRefund", MySqlDbType.Int16);
            prmTransactionStatusRefund.Value = (Int16)TransactionStatus.Refund;
            cmd.Parameters.Add(prmTransactionStatusRefund);

            MySqlParameter prmTransactionStatusCreditPayment = new MySqlParameter("@TransactionStatusCreditPayment", MySqlDbType.Int16);
            prmTransactionStatusCreditPayment.Value = (Int16)TransactionStatus.CreditPayment;
            cmd.Parameters.Add(prmTransactionStatusCreditPayment);

            MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom", MySqlDbType.DateTime);
            prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add(prmDateFrom);

            MySqlParameter prmDateTo = new MySqlParameter("@DateTo", MySqlDbType.DateTime);
            prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add(prmDateTo);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}

		#endregion

		#region Public Modifiers

        public DateTime MINDateLastInitialized(int BranchID, string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
				string SQL=	"SELECT " +
					            "MAX(DateLastInitialized) AS DateLastInitialized " +
					        "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmProcessingDate = new MySqlParameter("@ProcessingDate",MySqlDbType.DateTime);
				prmProcessingDate.Value = ProcessingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmProcessingDate);

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

		public DateTime MAXDateLastInitialized(int BranchID, string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
				string SQL=	"SELECT " +
					            "MIN(DateLastInitialized) AS DateLastInitialized " +
					        "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
					        "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmProcessingDate = new MySqlParameter("@ProcessingDate",MySqlDbType.DateTime);
				prmProcessingDate.Value = ProcessingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmProcessingDate);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;

                foreach (System.Data.DataRow dr in dt.Rows)
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

        public DateTime NEXTDateLastInitialized(int BranchID, string TerminalNo, DateTime ProcessingDate)
        {
            try
            {
                string SQL = "SELECT " +
                                "MIN(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') > DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmProcessingDate = new MySqlParameter("@ProcessingDate",MySqlDbType.DateTime);
                prmProcessingDate.Value = ProcessingDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmProcessingDate);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;

                foreach (System.Data.DataRow dr in dt.Rows)
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

        public DateTime getRLCDateLastInitialized(string TerminalNo)
        {
            try
            {
                string SQL = "SELECT " +
                                "MIN(DateLastInitialized) AS DateLastInitialized " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE TerminalNo = @TerminalNo " +
                            "AND IsMallFileUploadComplete = 0 ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;

                foreach (System.Data.DataRow dr in dt.Rows)
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

        public System.Data.DataTable DatesLastInitialized(int BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                string SQL = "SELECT DateLastInitialized, " +
                             "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay " +
                             "FROM tblTerminalReportHistory " +
                             "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') " +
                             "ORDER BY DateLastInitialized DESC ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
                prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateFrom);

                MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
                prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateTo);

                System.Data.DataTable dt = new System.Data.DataTable("DatesLastInitialized");
                base.MySqlDataAdapterFill(cmd, dt);
                
                return dt;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable DatesLastInitializedForRLC(int BranchID, string TerminalNo, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                string SQL = "SELECT DateLastInitialized, DATE_FORMAT(IF(HOUR(DateLastInitialized)>5, DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedDisplay FROM tblTerminalReportHistory " +
                             "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@DateFrom, '%Y-%m-%d %H:%i') " +
                             "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@DateTo, '%Y-%m-%d %H:%i') " +
                             "ORDER BY DateLastInitialized DESC ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateFrom = new MySqlParameter("@DateFrom",MySqlDbType.DateTime);
                prmDateFrom.Value = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateFrom);

                MySqlParameter prmDateTo = new MySqlParameter("@DateTo",MySqlDbType.DateTime);
                prmDateTo.Value = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateTo);

                System.Data.DataTable dt = new System.Data.DataTable("DatesLastInitialized");
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