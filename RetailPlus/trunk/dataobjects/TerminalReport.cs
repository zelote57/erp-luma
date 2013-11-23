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
	public struct TerminalReportDetails
	{
        public int BranchID;
		public string  TerminalNo;
		public string  BeginningTransactionNo;
		public string  EndingTransactionNo;
		public Int32   ZReadCount;
		public Int32   XReadCount;
		public decimal GrossSales;
		public decimal TotalDiscount;
		public decimal TotalCharge;
		public decimal DailySales;
		public decimal QuantitySold;
		public decimal GroupSales;
		public decimal OldGrandTotal;
		public decimal NewGrandTotal;
		public decimal VATableAmount;
		public decimal NonVaTableAmount;
		public decimal VAT;
		public decimal EVATableAmount;
		public decimal NonEVaTableAmount;
		public decimal EVAT;
		public decimal LocalTax;
		public decimal CashSales;
		public decimal ChequeSales;
		public decimal CreditCardSales;
		public decimal CreditSales;
		public decimal CreditPayment;
		public decimal DebitPayment;
		public decimal CashInDrawer;
		public decimal TotalDisburse;
		public decimal CashDisburse;
		public decimal ChequeDisburse;
		public decimal CreditCardDisburse;
		public decimal TotalWithHold;
		public decimal CashWithHold;
		public decimal ChequeWithHold;
		public decimal CreditCardWithHold;
		public decimal TotalPaidOut;
		public decimal TotalDeposit;
		public decimal CashDeposit;
		public decimal ChequeDeposit;
		public decimal CreditCardDeposit;
		public decimal BeginningBalance;
		public decimal VoidSales;
		public decimal RefundSales;
		public decimal ItemsDiscount;
		public decimal SubTotalDiscount;
		public Int32   NoOfCashTransactions;
		public Int32   NoOfChequeTransactions;
		public Int32   NoOfCreditCardTransactions;
		public Int32   NoOfCreditTransactions;
		public Int32   NoOfCombinationPaymentTransactions;
		public Int32   NoOfCreditPaymentTransactions;
		public Int32   NoOfDebitPaymentTransactions;
		public Int32   NoOfRefundTransactions;
		public Int32   NoOfClosedTransactions;
		public Int32   NoOfVoidTransactions;
		public Int32   NoOfTotalTransactions;
		public DateTime DateLastInitialized;
        public DateTime DateLastInitializedToDisplay;
        public decimal TrustFund;

        // March 19, 2009
        // Added by Lemuel E. Aceron
        // For the purpose of RLC Accreditation
        public Int32 NoOfDiscountedTransactions;
        public decimal NegativeAdjustments;
        public Int32 NoOfNegativeAdjustmentTransactions;
        public decimal PromotionalItems;
        public decimal CreditSalesTax;
        public Int32 BatchCounter;
        /**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
        public decimal RewardPointsPayment;
        public decimal RewardConvertedPayment;
        public Int32 NoOfRewardPointsPayment;
        /**
		 * Dec 7, 2011
		 * for checking who initialized the zread
		 * */
        public string InitializedBy;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class TerminalReport : POSConnection
	{
		#region Constructors & Destructors

		public TerminalReport()
            : base(null, null)
        {
        }

        public TerminalReport(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
					            "BranchID, " +
                                "TerminalNo, " +
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
					            "TotalWithHold, " +
					            "CashWithHold, " +
					            "ChequeWithHold, " +
					            "CreditCardWithHold, " +
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
                                "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE TerminalNo = tblTerminalReport.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay, " +
                                "NoOfDiscountedTransactions, " +
                                "NegativeAdjustments, " +
                                "NoOfNegativeAdjustmentTransactions, " +
                                "PromotionalItems, " +
                                "CreditSalesTax, " +
                                "BatchCounter " +
					        "FROM tblTerminalReport ";
            return stSQL;
        }

		#region Details
		public TerminalReportDetails Details(string TerminalNo)
		{
			try
			{
				string SQL=	SQLSelect() +  "WHERE TerminalNo = @TerminalNo;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
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
					Details.TotalWithHold = myReader.GetDecimal("TotalWithHold");
					Details.CashWithHold = myReader.GetDecimal("CashWithHold");
					Details.ChequeWithHold = myReader.GetDecimal("ChequeWithHold");
					Details.CreditCardWithHold = myReader.GetDecimal("CreditCardWithHold");
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
                    Details.NoOfDiscountedTransactions = myReader.GetInt32("NoOfDiscountedTransactions");
                    Details.NegativeAdjustments = myReader.GetDecimal("NegativeAdjustments");
                    Details.NoOfNegativeAdjustmentTransactions = myReader.GetInt32("NoOfNegativeAdjustmentTransactions");
                    Details.PromotionalItems = myReader.GetDecimal("PromotionalItems");
                    Details.CreditSalesTax = myReader.GetDecimal("CreditSalesTax");
                    Details.BatchCounter = myReader.GetInt32("BatchCounter");
				}
				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public string EndingTransactioNo(string TerminalNo)
		{
			try
			{
				string SQL=	"SELECT " +
					            "EndingTransactionNo " +
					        "FROM tblTerminalReport " +
					        "WHERE TerminalNo = @TerminalNo;";
        				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				string stEndingTransactioNo = null;

				while (myReader.Read())
				{
					
					stEndingTransactioNo = "" + myReader["EndingTransactionNo"].ToString();
				}
				myReader.Close();

				return stEndingTransactioNo;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public DateTime MAXDateLastInitialized(string TerminalNo)
        {
            try
            {
                string SQL = "SELECT " +
                    "MAX(DateLastInitialized) AS DateLastInitialized " +
                    "FROM tblTerminalReport " +
                    "WHERE TerminalNo = @TerminalNo " +
                    "ORDER BY DateLastInitialized DESC LIMIT 1;";
            
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

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
		public DateTime MAXDateLastInitialized(string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
				string SQL=	"SELECT " +
					"MAX(DateLastInitialized) AS DateLastInitialized " +
					"FROM tblTerminalReport " +
					"WHERE TerminalNo = @TerminalNo " +
					"AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";
				//							"ORDER BY DateLastInitialized DESC ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

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
		#endregion
	
		#region Updates

		public void UpdateBeginningBalance(int BranchID, string TerminalNo, decimal Amount)
		{
			try 
			{
				string SQL=	"UPDATE tblTerminalReport SET " +
					            "CashInDrawer		= CashInDrawer + @CashInDrawer, " +
					            "BeginningBalance	= BeginningBalance + @BeginningBalance " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);			
				prmCashInDrawer.Value = Amount;
				cmd.Parameters.Add(prmCashInDrawer);

				MySqlParameter prmBeginningBalance = new MySqlParameter("@BeginningBalance",MySqlDbType.Decimal);			
				prmBeginningBalance.Value = Amount;
				cmd.Parameters.Add(prmBeginningBalance);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdateZReadCount()
		{
			try 
			{
				string SQL=	"UPDATE tblTerminalReport SET " +
					"ZReadCount = ZReadCount + 1 " +
					"WHERE TerminalNo = @TerminalNo;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = CompanyDetails.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdateXReadCount()
		{
			try 
			{
				string SQL=	"UPDATE tblTerminalReport SET " +
					"XReadCount = XReadCount + 1 " +
					"WHERE TerminalNo = @TerminalNo;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = CompanyDetails.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void IncrementBatchCounter()
        {
            try
            {
                string SQL = "CALL procTerminalReportIncrementBatchCounter(@TerminalNo);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = CompanyDetails.TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void UpdateTransactionSales(TerminalReportDetails Details)
		{
			try 
			{
                string SQL = "CALL procTerminalReportUpdateTransactionSales(@BranchID, @TerminalNo," +
                                    "@GrossSales, " +
                                    "@TotalDiscount, " +
                                    "@TotalCharge, " +
                                    "@DailySales, " +
                                    "@QuantitySold, " +
                                    "@GroupSales, " +
                                    "@OldGrandTotal, " +
                                    "@NewGrandTotal, " +
                                    "@VATableAmount, " +
                                    "@NonVaTableAmount, " +
                                    "@VAT, " +
                                    "@EVATableAmount, " +
                                    "@NonEVaTableAmount, " +
                                    "@EVAT, " +
                                    "@LocalTax, " +
                                    "@CashSales, " +
                                    "@ChequeSales, " +
                                    "@CreditCardSales, " +
                                    "@CreditSales, " +
                                    "@CreditPayment, " +
                                    "@DebitPayment, " +
                                    "@RewardPointsPayment, " +
                                    "@RewardConvertedPayment, " +
                                    "@CashInDrawer, " +
                                    "@VoidSales, " +
                                    "@RefundSales, " +
                                    "@ItemsDiscount, " +
                                    "@SubTotalDiscount, " +
                                    "@NoOfCashTransactions, " +
                                    "@NoOfChequeTransactions, " +
                                    "@NoOfCreditCardTransactions, " +
                                    "@NoOfCreditTransactions, " +
                                    "@NoOfCombinationPaymentTransactions, " +
                                    "@NoOfCreditPaymentTransactions, " +
                                    "@NoOfDebitPaymentTransactions, " +
                                    "@NoOfClosedTransactions, " +
                                    "@NoOfRefundTransactions, " +
                                    "@NoOfVoidTransactions, " +
                                    "@NoOfRewardPointsPayment, " +
                                    "@NoOfTotalTransactions, " +
                                    "@NoOfDiscountedTransactions, " +
                                    "@NegativeAdjustments, " +
                                    "@NoOfNegativeAdjustmentTransactions, " +
                                    "@PromotionalItems, " +
                                    "@CreditSalesTax);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmGrossSales = new MySqlParameter("@GrossSales",MySqlDbType.Decimal);			
				prmGrossSales.Value = Details.GrossSales;
				cmd.Parameters.Add(prmGrossSales);

				MySqlParameter prmTotalDiscount = new MySqlParameter("@TotalDiscount",MySqlDbType.Decimal);			
				prmTotalDiscount.Value = Details.TotalDiscount;
				cmd.Parameters.Add(prmTotalDiscount);

				MySqlParameter prmTotalCharge = new MySqlParameter("@TotalCharge",MySqlDbType.Decimal);			
				prmTotalCharge.Value = Details.TotalCharge;
				cmd.Parameters.Add(prmTotalCharge);

				MySqlParameter prmDailySales = new MySqlParameter("@DailySales",MySqlDbType.Decimal);			
				prmDailySales.Value = Details.DailySales;
				cmd.Parameters.Add(prmDailySales);

				MySqlParameter prmQuantitySold = new MySqlParameter("@QuantitySold",MySqlDbType.Decimal);			
				prmQuantitySold.Value = Details.QuantitySold;
				cmd.Parameters.Add(prmQuantitySold);

				MySqlParameter prmGroupSales = new MySqlParameter("@GroupSales",MySqlDbType.Decimal);			
				prmGroupSales.Value = Details.GroupSales;
				cmd.Parameters.Add(prmGroupSales);

				MySqlParameter prmOldGrandTotal = new MySqlParameter("@OldGrandTotal",MySqlDbType.Decimal);			
				prmOldGrandTotal.Value = Details.OldGrandTotal;
				cmd.Parameters.Add(prmOldGrandTotal);

				MySqlParameter prmNewGrandTotal = new MySqlParameter("@NewGrandTotal",MySqlDbType.Decimal);			
				prmNewGrandTotal.Value = Details.VATableAmount + Details.NonVaTableAmount + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge; //reference from DailySales to accumulate the new grand total c/o Luisa
				cmd.Parameters.Add(prmNewGrandTotal);

				MySqlParameter prmVATableAmount = new MySqlParameter("@VATableAmount",MySqlDbType.Decimal);			
				prmVATableAmount.Value = Details.VATableAmount;
				cmd.Parameters.Add(prmVATableAmount);

				MySqlParameter prmNonVaTableAmount = new MySqlParameter("@NonVaTableAmount",MySqlDbType.Decimal);			
				prmNonVaTableAmount.Value = Details.NonVaTableAmount;
				cmd.Parameters.Add(prmNonVaTableAmount);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmEVATableAmount = new MySqlParameter("@EVATableAmount",MySqlDbType.Decimal);			
				prmEVATableAmount.Value = Details.EVATableAmount;
				cmd.Parameters.Add(prmEVATableAmount);

				MySqlParameter prmNonEVaTableAmount = new MySqlParameter("@NonEVaTableAmount",MySqlDbType.Decimal);			
				prmNonEVaTableAmount.Value = Details.NonEVaTableAmount;
				cmd.Parameters.Add(prmNonEVaTableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);			
				prmCashSales.Value = Details.CashSales;
				cmd.Parameters.Add(prmCashSales);

				MySqlParameter prmChequeSales = new MySqlParameter("@ChequeSales",MySqlDbType.Decimal);			
				prmChequeSales.Value = Details.ChequeSales;
				cmd.Parameters.Add(prmChequeSales);

				MySqlParameter prmCreditCardSales = new MySqlParameter("@CreditCardSales",MySqlDbType.Decimal);			
				prmCreditCardSales.Value = Details.CreditCardSales;
				cmd.Parameters.Add(prmCreditCardSales);

				MySqlParameter prmCreditSales = new MySqlParameter("@CreditSales",MySqlDbType.Decimal);			
				prmCreditSales.Value = Details.CreditSales;
				cmd.Parameters.Add(prmCreditSales);

				MySqlParameter prmCreditPayment = new MySqlParameter("@CreditPayment",MySqlDbType.Decimal);			
				prmCreditPayment.Value = Details.CreditPayment;
				cmd.Parameters.Add(prmCreditPayment);

				MySqlParameter prmDebitPayment = new MySqlParameter("@DebitPayment",MySqlDbType.Decimal);			
				prmDebitPayment.Value = Details.DebitPayment;
				cmd.Parameters.Add(prmDebitPayment);

                MySqlParameter prmRewardPointsPayment = new MySqlParameter("@RewardPointsPayment",MySqlDbType.Decimal);
                prmRewardPointsPayment.Value = Details.RewardPointsPayment;
                cmd.Parameters.Add(prmRewardPointsPayment);

                MySqlParameter prmRewardConvertedPayment = new MySqlParameter("@RewardConvertedPayment",MySqlDbType.Decimal);
                prmRewardConvertedPayment.Value = Details.RewardConvertedPayment;
                cmd.Parameters.Add(prmRewardConvertedPayment);

				MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);			
				prmCashInDrawer.Value = Details.CashSales; //refer to cash sales
				cmd.Parameters.Add(prmCashInDrawer);

				MySqlParameter prmVoidSales = new MySqlParameter("@VoidSales",MySqlDbType.Decimal);			
				prmVoidSales.Value = Details.VoidSales;
				cmd.Parameters.Add(prmVoidSales);

				MySqlParameter prmRefundSales = new MySqlParameter("@RefundSales",MySqlDbType.Decimal);			
				prmRefundSales.Value = Details.RefundSales;
				cmd.Parameters.Add(prmRefundSales);

				MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);			
				prmItemsDiscount.Value = Details.ItemsDiscount;
				cmd.Parameters.Add(prmItemsDiscount);

				MySqlParameter prmSubtotalDiscount = new MySqlParameter("@SubTotalDiscount",MySqlDbType.Decimal);			
				prmSubtotalDiscount.Value = Details.SubTotalDiscount;
				cmd.Parameters.Add(prmSubtotalDiscount);

				MySqlParameter prmNoOfCashTransactions = new MySqlParameter("@NoOfCashTransactions",MySqlDbType.Int32);			
				prmNoOfCashTransactions.Value = Details.NoOfCashTransactions;
				cmd.Parameters.Add(prmNoOfCashTransactions);

				MySqlParameter prmNoOfChequeTransactions = new MySqlParameter("@NoOfChequeTransactions",MySqlDbType.Int32);			
				prmNoOfChequeTransactions.Value = Details.NoOfChequeTransactions;
				cmd.Parameters.Add(prmNoOfChequeTransactions);

				MySqlParameter prmNoOfCreditCardTransactions = new MySqlParameter("@NoOfCreditCardTransactions",MySqlDbType.Int32);			
				prmNoOfCreditCardTransactions.Value = Details.NoOfCreditCardTransactions;
				cmd.Parameters.Add(prmNoOfCreditCardTransactions);

				MySqlParameter prmNoOfCreditTransactions = new MySqlParameter("@NoOfCreditTransactions",MySqlDbType.Int32);			
				prmNoOfCreditTransactions.Value = Details.NoOfCreditTransactions;
				cmd.Parameters.Add(prmNoOfCreditTransactions);

				MySqlParameter prmNoOfCombinationPaymentTransactions = new MySqlParameter("@NoOfCombinationPaymentTransactions",MySqlDbType.Int32);			
				prmNoOfCombinationPaymentTransactions.Value = Details.NoOfCombinationPaymentTransactions;
				cmd.Parameters.Add(prmNoOfCombinationPaymentTransactions);

				MySqlParameter prmNoOfCreditPaymentTransactions = new MySqlParameter("@NoOfCreditPaymentTransactions",MySqlDbType.Int32);			
				prmNoOfCreditPaymentTransactions.Value = Details.NoOfCreditPaymentTransactions;
				cmd.Parameters.Add(prmNoOfCreditPaymentTransactions);

				MySqlParameter prmNoOfDebitPaymentTransactions = new MySqlParameter("@NoOfDebitPaymentTransactions",MySqlDbType.Int32);			
				prmNoOfDebitPaymentTransactions.Value = Details.NoOfDebitPaymentTransactions;
				cmd.Parameters.Add(prmNoOfDebitPaymentTransactions);

				MySqlParameter prmNoOfClosedTransactions = new MySqlParameter("@NoOfClosedTransactions",MySqlDbType.Int32);			
				prmNoOfClosedTransactions.Value = Details.NoOfClosedTransactions;
				cmd.Parameters.Add(prmNoOfClosedTransactions);

				MySqlParameter prmNoOfRefundTransactions = new MySqlParameter("@NoOfRefundTransactions",MySqlDbType.Int32);			
				prmNoOfRefundTransactions.Value = Details.NoOfRefundTransactions;
				cmd.Parameters.Add(prmNoOfRefundTransactions);

				MySqlParameter prmNoOfVoidTransactions = new MySqlParameter("@NoOfVoidTransactions",MySqlDbType.Int32);			
				prmNoOfVoidTransactions.Value = Details.NoOfVoidTransactions;
				cmd.Parameters.Add(prmNoOfVoidTransactions);

                MySqlParameter prmNoOfRewardPointsPayment = new MySqlParameter("@NoOfRewardPointsPayment",MySqlDbType.Int32);
                prmNoOfRewardPointsPayment.Value = Details.NoOfRewardPointsPayment;
                cmd.Parameters.Add(prmNoOfRewardPointsPayment);

				MySqlParameter prmNoOfTotalTransactions = new MySqlParameter("@NoOfTotalTransactions",MySqlDbType.Int32);			
				prmNoOfTotalTransactions.Value = Details.NoOfTotalTransactions;
				cmd.Parameters.Add(prmNoOfTotalTransactions);

                MySqlParameter prmNoOfDiscountedTransactions = new MySqlParameter("@NoOfDiscountedTransactions",MySqlDbType.Int32);
                prmNoOfDiscountedTransactions.Value = Details.NoOfDiscountedTransactions;
                cmd.Parameters.Add(prmNoOfDiscountedTransactions);

                MySqlParameter prmNegativeAdjustments = new MySqlParameter("@NegativeAdjustments",MySqlDbType.Decimal);
                prmNegativeAdjustments.Value = Details.NegativeAdjustments;
                cmd.Parameters.Add(prmNegativeAdjustments);

                MySqlParameter prmNoOfNegativeAdjustmentTransactions = new MySqlParameter("@NoOfNegativeAdjustmentTransactions",MySqlDbType.Int32);
                prmNoOfNegativeAdjustmentTransactions.Value = Details.NoOfNegativeAdjustmentTransactions;
                cmd.Parameters.Add(prmNoOfNegativeAdjustmentTransactions);

                MySqlParameter prmPromotionalItems = new MySqlParameter("@PromotionalItems",MySqlDbType.Decimal);
                prmPromotionalItems.Value = Details.PromotionalItems;
                cmd.Parameters.Add(prmPromotionalItems);

                MySqlParameter prmCreditSalesTax = new MySqlParameter("@CreditSalesTax",MySqlDbType.Decimal);
                prmCreditSalesTax.Value = Details.CreditSalesTax;
                cmd.Parameters.Add(prmCreditSalesTax);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        //public void UpdateTransactionSales(TerminalReportDetails Details)
        //{
        //    try
        //    {
        //        string SQL = "UPDATE tblTerminalReport SET " +
        //            "GrossSales							=  GrossSales							+  @GrossSales, " +
        //            "TotalDiscount						=  TotalDiscount						+  @TotalDiscount, " +
        //            "TotalCharge						=  TotalCharge							+  @TotalCharge, " +
        //            "DailySales							=  DailySales							+  @DailySales, " +
        //            "QuantitySold						=  QuantitySold							+  @QuantitySold, " +
        //            "GroupSales							=  GroupSales							+  @GroupSales, " +
        //            "OldGrandTotal						=  OldGrandTotal						+  @OldGrandTotal, " +
        //            "NewGrandTotal						=  NewGrandTotal						+  @NewGrandTotal, " +
        //            "VATableAmount						=  VATableAmount						+  @VATableAmount, " +
        //            "NonVaTableAmount					=  NonVaTableAmount						+  @NonVaTableAmount, " +
        //            "VAT								=  VAT									+  @VAT, " +
        //            "EVATableAmount						=  EVATableAmount						+  @EVATableAmount, " +
        //            "NonEVaTableAmount					=  NonEVaTableAmount					+  @NonEVaTableAmount, " +
        //            "EVAT								=  EVAT									+  @EVAT, " +
        //            "LocalTax							=  LocalTax								+  @LocalTax, " +
        //            "CashSales							=  CashSales							+  @CashSales, " +
        //            "ChequeSales						=  ChequeSales							+  @ChequeSales, " +
        //            "CreditCardSales					=  CreditCardSales						+  @CreditCardSales, " +
        //            "CreditSales						=  CreditSales							+  @CreditSales, " +
        //            "CreditPayment						=  CreditPayment						+  @CreditPayment, " +
        //            "DebitPayment						=  DebitPayment						    +  @DebitPayment, " +
        //            "CashInDrawer						=  CashInDrawer							+  @CashInDrawer, " +
        //            "VoidSales							=  VoidSales							+  @VoidSales, " +
        //            "RefundSales						=  RefundSales							+  @RefundSales, " +
        //            "ItemsDiscount						=  ItemsDiscount						+  @ItemsDiscount, " +
        //            "SubTotalDiscount					=  SubTotalDiscount						+  @SubTotalDiscount, " +
        //            "NoOfCashTransactions				=  NoOfCashTransactions					+  @NoOfCashTransactions, " +
        //            "NoOfChequeTransactions				=  NoOfChequeTransactions				+  @NoOfChequeTransactions, " +
        //            "NoOfCreditCardTransactions			=  NoOfCreditCardTransactions			+  @NoOfCreditCardTransactions, " +
        //            "NoOfCreditTransactions				=  NoOfCreditTransactions				+  @NoOfCreditTransactions, " +
        //            "NoOfCombinationPaymentTransactions	=  NoOfCombinationPaymentTransactions	+  @NoOfCombinationPaymentTransactions, " +
        //            "NoOfCreditPaymentTransactions		=  NoOfCreditPaymentTransactions		+  @NoOfCreditPaymentTransactions, " +
        //            "NoOfDebitPaymentTransactions		=  NoOfDebitPaymentTransactions			+  @NoOfDebitPaymentTransactions, " +
        //            "NoOfClosedTransactions				=  NoOfClosedTransactions				+  @NoOfClosedTransactions, " +
        //            "NoOfRefundTransactions				=  NoOfRefundTransactions				+  @NoOfRefundTransactions, " +
        //            "NoOfVoidTransactions				=  NoOfVoidTransactions					+  @NoOfVoidTransactions, " +
        //            "NoOfTotalTransactions				=  NoOfTotalTransactions				+  @NoOfTotalTransactions " +
        //            "WHERE TerminalNo = @TerminalNo;";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmGrossSales = new MySqlParameter("@GrossSales",MySqlDbType.Decimal);
        //        prmGrossSales.Value = Details.GrossSales;
        //        cmd.Parameters.Add(prmGrossSales);

        //        MySqlParameter prmTotalDiscount = new MySqlParameter("@TotalDiscount",MySqlDbType.Decimal);
        //        prmTotalDiscount.Value = Details.TotalDiscount;
        //        cmd.Parameters.Add(prmTotalDiscount);

        //        MySqlParameter prmTotalCharge = new MySqlParameter("@TotalCharge",MySqlDbType.Decimal);
        //        prmTotalCharge.Value = Details.TotalCharge;
        //        cmd.Parameters.Add(prmTotalCharge);

        //        MySqlParameter prmDailySales = new MySqlParameter("@DailySales",MySqlDbType.Decimal);
        //        prmDailySales.Value = Details.DailySales;
        //        cmd.Parameters.Add(prmDailySales);

        //        MySqlParameter prmQuantitySold = new MySqlParameter("@QuantitySold",MySqlDbType.Decimal);
        //        prmQuantitySold.Value = Details.QuantitySold;
        //        cmd.Parameters.Add(prmQuantitySold);

        //        MySqlParameter prmGroupSales = new MySqlParameter("@GroupSales",MySqlDbType.Decimal);
        //        prmGroupSales.Value = Details.GroupSales;
        //        cmd.Parameters.Add(prmGroupSales);

        //        MySqlParameter prmOldGrandTotal = new MySqlParameter("@OldGrandTotal",MySqlDbType.Decimal);
        //        prmOldGrandTotal.Value = Details.OldGrandTotal;
        //        cmd.Parameters.Add(prmOldGrandTotal);

        //        MySqlParameter prmNewGrandTotal = new MySqlParameter("@NewGrandTotal",MySqlDbType.Decimal);
        //        prmNewGrandTotal.Value = Details.VATableAmount + Details.NonVaTableAmount + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge; //reference from DailySales to accumulate the new grand total c/o Luisa
        //        cmd.Parameters.Add(prmNewGrandTotal);

        //        MySqlParameter prmVATableAmount = new MySqlParameter("@VATableAmount",MySqlDbType.Decimal);
        //        prmVATableAmount.Value = Details.VATableAmount;
        //        cmd.Parameters.Add(prmVATableAmount);

        //        MySqlParameter prmNonVaTableAmount = new MySqlParameter("@NonVaTableAmount",MySqlDbType.Decimal);
        //        prmNonVaTableAmount.Value = Details.NonVaTableAmount;
        //        cmd.Parameters.Add(prmNonVaTableAmount);

        //        MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
        //        prmVAT.Value = Details.VAT;
        //        cmd.Parameters.Add(prmVAT);

        //        MySqlParameter prmEVATableAmount = new MySqlParameter("@EVATableAmount",MySqlDbType.Decimal);
        //        prmEVATableAmount.Value = Details.EVATableAmount;
        //        cmd.Parameters.Add(prmEVATableAmount);

        //        MySqlParameter prmNonEVaTableAmount = new MySqlParameter("@NonEVaTableAmount",MySqlDbType.Decimal);
        //        prmNonEVaTableAmount.Value = Details.NonEVaTableAmount;
        //        cmd.Parameters.Add(prmNonEVaTableAmount);

        //        MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
        //        prmEVAT.Value = Details.EVAT;
        //        cmd.Parameters.Add(prmEVAT);

        //        MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
        //        prmLocalTax.Value = Details.LocalTax;
        //        cmd.Parameters.Add(prmLocalTax);

        //        MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);
        //        prmCashSales.Value = Details.CashSales;
        //        cmd.Parameters.Add(prmCashSales);

        //        MySqlParameter prmChequeSales = new MySqlParameter("@ChequeSales",MySqlDbType.Decimal);
        //        prmChequeSales.Value = Details.ChequeSales;
        //        cmd.Parameters.Add(prmChequeSales);

        //        MySqlParameter prmCreditCardSales = new MySqlParameter("@CreditCardSales",MySqlDbType.Decimal);
        //        prmCreditCardSales.Value = Details.CreditCardSales;
        //        cmd.Parameters.Add(prmCreditCardSales);

        //        MySqlParameter prmCreditSales = new MySqlParameter("@CreditSales",MySqlDbType.Decimal);
        //        prmCreditSales.Value = Details.CreditSales;
        //        cmd.Parameters.Add(prmCreditSales);

        //        MySqlParameter prmCreditPayment = new MySqlParameter("@CreditPayment",MySqlDbType.Decimal);
        //        prmCreditPayment.Value = Details.CreditPayment;
        //        cmd.Parameters.Add(prmCreditPayment);

        //        MySqlParameter prmDebitPayment = new MySqlParameter("@DebitPayment",MySqlDbType.Decimal);
        //        prmDebitPayment.Value = Details.DebitPayment;
        //        cmd.Parameters.Add(prmDebitPayment);

        //        MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
        //        prmCashInDrawer.Value = Details.CashSales; //refer to cash sales
        //        cmd.Parameters.Add(prmCashInDrawer);

        //        MySqlParameter prmVoidSales = new MySqlParameter("@VoidSales",MySqlDbType.Decimal);
        //        prmVoidSales.Value = Details.VoidSales;
        //        cmd.Parameters.Add(prmVoidSales);

        //        MySqlParameter prmRefundSales = new MySqlParameter("@RefundSales",MySqlDbType.Decimal);
        //        prmRefundSales.Value = Details.RefundSales;
        //        cmd.Parameters.Add(prmRefundSales);

        //        MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);
        //        prmItemsDiscount.Value = Details.ItemsDiscount;
        //        cmd.Parameters.Add(prmItemsDiscount);

        //        MySqlParameter prmSubtotalDiscount = new MySqlParameter("@SubTotalDiscount",MySqlDbType.Decimal);
        //        prmSubtotalDiscount.Value = Details.SubTotalDiscount;
        //        cmd.Parameters.Add(prmSubtotalDiscount);

        //        MySqlParameter prmNoOfCashTransactions = new MySqlParameter("@NoOfCashTransactions",MySqlDbType.Int32);
        //        prmNoOfCashTransactions.Value = Details.NoOfCashTransactions;
        //        cmd.Parameters.Add(prmNoOfCashTransactions);

        //        MySqlParameter prmNoOfChequeTransactions = new MySqlParameter("@NoOfChequeTransactions",MySqlDbType.Int32);
        //        prmNoOfChequeTransactions.Value = Details.NoOfChequeTransactions;
        //        cmd.Parameters.Add(prmNoOfChequeTransactions);

        //        MySqlParameter prmNoOfCreditCardTransactions = new MySqlParameter("@NoOfCreditCardTransactions",MySqlDbType.Int32);
        //        prmNoOfCreditCardTransactions.Value = Details.NoOfCreditCardTransactions;
        //        cmd.Parameters.Add(prmNoOfCreditCardTransactions);

        //        MySqlParameter prmNoOfCreditTransactions = new MySqlParameter("@NoOfCreditTransactions",MySqlDbType.Int32);
        //        prmNoOfCreditTransactions.Value = Details.NoOfCreditTransactions;
        //        cmd.Parameters.Add(prmNoOfCreditTransactions);

        //        MySqlParameter prmNoOfCombinationPaymentTransactions = new MySqlParameter("@NoOfCombinationPaymentTransactions",MySqlDbType.Int32);
        //        prmNoOfCombinationPaymentTransactions.Value = Details.NoOfCombinationPaymentTransactions;
        //        cmd.Parameters.Add(prmNoOfCombinationPaymentTransactions);

        //        MySqlParameter prmNoOfCreditPaymentTransactions = new MySqlParameter("@NoOfCreditPaymentTransactions",MySqlDbType.Int32);
        //        prmNoOfCreditPaymentTransactions.Value = Details.NoOfCreditPaymentTransactions;
        //        cmd.Parameters.Add(prmNoOfCreditPaymentTransactions);

        //        MySqlParameter prmNoOfDebitPaymentTransactions = new MySqlParameter("@NoOfDebitPaymentTransactions",MySqlDbType.Int32);
        //        prmNoOfDebitPaymentTransactions.Value = Details.NoOfDebitPaymentTransactions;
        //        cmd.Parameters.Add(prmNoOfDebitPaymentTransactions);

        //        MySqlParameter prmNoOfClosedTransactions = new MySqlParameter("@NoOfClosedTransactions",MySqlDbType.Int32);
        //        prmNoOfClosedTransactions.Value = Details.NoOfClosedTransactions;
        //        cmd.Parameters.Add(prmNoOfClosedTransactions);

        //        MySqlParameter prmNoOfRefundTransactions = new MySqlParameter("@NoOfRefundTransactions",MySqlDbType.Int32);
        //        prmNoOfRefundTransactions.Value = Details.NoOfRefundTransactions;
        //        cmd.Parameters.Add(prmNoOfRefundTransactions);

        //        MySqlParameter prmNoOfVoidTransactions = new MySqlParameter("@NoOfVoidTransactions",MySqlDbType.Int32);
        //        prmNoOfVoidTransactions.Value = Details.NoOfVoidTransactions;
        //        cmd.Parameters.Add(prmNoOfVoidTransactions);

        //        MySqlParameter prmNoOfTotalTransactions = new MySqlParameter("@NoOfTotalTransactions",MySqlDbType.Int32);
        //        prmNoOfTotalTransactions.Value = Details.NoOfTotalTransactions;
        //        cmd.Parameters.Add(prmNoOfTotalTransactions);

        //        MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
        //        prmTerminalNo.Value = Details.TerminalNo;
        //        cmd.Parameters.Add(prmTerminalNo);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }
        //}

		public void UpdateWithHold(WithHoldDetails Details)
		{
			try 
			{
				string SQL = "";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmTotalWithHold = new MySqlParameter("@TotalWithHold",MySqlDbType.Decimal);
				prmTotalWithHold.Value = Details.Amount;
				cmd.Parameters.Add(prmTotalWithHold);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CashWithHold						= CashWithHold + @CashWithHold, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCashWithHold = new MySqlParameter("@CashWithHold",MySqlDbType.Decimal);
					prmCashWithHold.Value = Details.Amount;
					cmd.Parameters.Add(prmCashWithHold);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);

					MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);
					prmCashSales.Value = Details.Amount;
					cmd.Parameters.Add(prmCashSales);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "ChequeWithHold						= ChequeWithHold + @ChequeWithHold " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmChequeWithHold = new MySqlParameter("@ChequeWithHold",MySqlDbType.Decimal);	
					prmChequeWithHold.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeWithHold);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CreditCardWithHold					= CreditCardWithHold + @CreditCardWithHold " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCreditCardWithHold = new MySqlParameter("@CreditCardWithHold",MySqlDbType.Decimal);	
					prmCreditCardWithHold.Value = Details.Amount;
					cmd.Parameters.Add(prmCreditCardWithHold);
				}

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdateDisburse(DisburseDetails Details)
		{
			try 
			{
				string SQL = "";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmTotalDisburse = new MySqlParameter("@TotalDisburse",MySqlDbType.Decimal);
				prmTotalDisburse.Value = Details.Amount;
				cmd.Parameters.Add(prmTotalDisburse);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CashDisburse						= CashDisburse + @CashDisburse, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCashDisburse = new MySqlParameter("@CashDisburse",MySqlDbType.Decimal);
					prmCashDisburse.Value = Details.Amount;
					cmd.Parameters.Add(prmCashDisburse);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);

					MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);
					prmCashSales.Value = Details.Amount;
					cmd.Parameters.Add(prmCashSales);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "ChequeDisburse						= ChequeDisburse + @ChequeDisburse " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmChequeDisburse = new MySqlParameter("@ChequeDisburse",MySqlDbType.Decimal);	
					prmChequeDisburse.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeDisburse);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CreditCardDisburse					= CreditCardDisburse + @CreditCardDisburse " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCreditCardDisburse = new MySqlParameter("@CreditCardDisburse",MySqlDbType.Decimal);	
					prmCreditCardDisburse.Value = Details.Amount;
					cmd.Parameters.Add(prmCreditCardDisburse);
				}

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdatePaidOut(PaidOutDetails Details)
		{
			try 
			{
				string SQL = "";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmTotalPaidOut = new MySqlParameter("@TotalPaidOut",MySqlDbType.Decimal);
				prmTotalPaidOut.Value = Details.Amount;
				cmd.Parameters.Add(prmTotalPaidOut);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CashPaidOut						= CashPaidOut + @CashPaidOut, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCashPaidOut = new MySqlParameter("@CashPaidOut",MySqlDbType.Decimal);
					prmCashPaidOut.Value = Details.Amount;
					cmd.Parameters.Add(prmCashPaidOut);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);

					MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);
					prmCashSales.Value = Details.Amount;
					cmd.Parameters.Add(prmCashSales);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						"TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						"ChequePaidOut						= ChequePaidOut + @ChequePaidOut " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmChequePaidOut = new MySqlParameter("@ChequePaidOut",MySqlDbType.Decimal);	
					prmChequePaidOut.Value = Details.Amount;
					cmd.Parameters.Add(prmChequePaidOut);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CreditCardPaidOut					= CreditCardPaidOut + @CreditCardPaidOut " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCreditCardPaidOut = new MySqlParameter("@CreditCardPaidOut",MySqlDbType.Decimal);	
					prmCreditCardPaidOut.Value = Details.Amount;
					cmd.Parameters.Add(prmCreditCardPaidOut);
				}

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public void UpdateDeposit(DepositDetails Details)
		{
			try 
			{
				string SQL = "";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				MySqlParameter prmTotalDeposit = new MySqlParameter("@TotalDeposit",MySqlDbType.Decimal);
				prmTotalDeposit.Value = Details.Amount;
				cmd.Parameters.Add(prmTotalDeposit);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CashDeposit						= CashDeposit + @CashDeposit, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCashDeposit = new MySqlParameter("@CashDeposit",MySqlDbType.Decimal);
					prmCashDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmCashDeposit);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);

					MySqlParameter prmCashSales = new MySqlParameter("@CashSales",MySqlDbType.Decimal);
					prmCashSales.Value = Details.Amount;
					cmd.Parameters.Add(prmCashSales);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						    "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						    "ChequeDeposit						= ChequeDeposit + @ChequeDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmChequeDeposit = new MySqlParameter("@ChequeDeposit",MySqlDbType.Decimal);	
					prmChequeDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeDeposit);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CreditCardDeposit					= CreditCardDeposit + @CreditCardDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCreditCardDeposit = new MySqlParameter("@CreditCardDeposit",MySqlDbType.Decimal);	
					prmCreditCardDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmCreditCardDeposit);
				}
                else if (Details.PaymentType == PaymentTypes.Debit)
                {
                    SQL = "UPDATE tblTerminalReport SET " +
                            "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
                            "DebitDeposit					    = DebitDeposit + @DebitDeposit " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    MySqlParameter prmDebitDeposit = new MySqlParameter("@DebitDeposit",MySqlDbType.Decimal);
                    prmDebitDeposit.Value = Details.Amount;
                    cmd.Parameters.Add(prmDebitDeposit);
                }

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Insert and Update

		public void Insert(int BranchID, Int16 TerminalID, string TerminalNo)
		{
			try
			{
                string SQL = "INSERT INTO tblTerminalReport (BranchID, TerminalID, TerminalNo, DateLastInitialized) " +
                    "VALUES (@BranchID, @TerminalID, @TerminalNo, @DateLastInitialized);";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalID = new MySqlParameter("@TerminalNo",MySqlDbType.Int16);			
				prmTerminalID.Value = TerminalID;
				cmd.Parameters.Add(prmTerminalID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);			
				prmDateLastInitialized.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateLastInitialized);

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdateTerminalNo(Int32 TerminalID, string TerminalNo)
		{
			try
			{
				string SQL="UPDATE tblTerminalReport SET "  +
					    "TerminalNo		=	@TerminalNo " +
					    "WHERE TerminalID	=	@TerminalID;";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTerminalID = new MySqlParameter("@TerminalNo",MySqlDbType.Int32);			
				prmTerminalID.Value = TerminalID;
				cmd.Parameters.Add(prmTerminalID);

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Public Methods

        public void InitializeZRead(int BranchID, string pvtTerminalNo, string pvtInitializedBy, bool pvtWithOutTF)
		{
			try 
			{
                string SQL = "CALL procTerminalReportInitializeZRead(@BranchID, @TerminalNo, @DateLastInitialized, @InitializedBy, @WithOutTF);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = pvtTerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);
                prmDateLastInitialized.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDateLastInitialized);

                MySqlParameter prmInitializedBy = new MySqlParameter("@InitializedBy",MySqlDbType.String);
                prmInitializedBy.Value = pvtInitializedBy;
                cmd.Parameters.Add(prmInitializedBy);

                cmd.Parameters.AddWithValue("@WithOutTF", pvtWithOutTF);

				base.ExecuteNonQuery(cmd);

				UpdateZReadCount();
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void InitializeZRead(int BranchID, string pvtTerminalNo, DateTime pvtDateLastInitialized, string pvtInitializedBy)
        {
            try
            {
                string SQL = "CALL procTerminalReportInitializeZRead(@BranchID, @TerminalNo, @DateLastInitialized, @InitializedBy);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = pvtTerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);
                prmDateLastInitialized.Value = pvtDateLastInitialized;
                cmd.Parameters.Add(prmDateLastInitialized);

                MySqlParameter prmInitializedBy = new MySqlParameter("@InitializedBy",MySqlDbType.String);
                prmInitializedBy.Value = pvtInitializedBy;
                cmd.Parameters.Add(prmInitializedBy);

                base.ExecuteNonQuery(cmd);

                UpdateZReadCount();
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public bool IsAllTerminalInitialize(DateTime CuttOfDateTime)
		{
			try
			{
				string SQL=	"SELECT " + 
					"COUNT(TerminalNo) AS RecordCount " +
					"FROM tblTerminalReport " +
					"WHERE DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') < DATE_FORMAT(@CuttOfDateTime, '%Y-%m-%d %H:%i') ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCuttOfDateTime = new MySqlParameter("@CuttOfDateTime",MySqlDbType.DateTime);
				prmCuttOfDateTime.Value = CuttOfDateTime.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmCuttOfDateTime);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                bool boRetValue = true;

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					if (Int64.Parse(dr["RecordCount"].ToString()) > 0)
						boRetValue = false;
				}

				return boRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	

		}


		#endregion

		#region Streams

		public System.Data.DataTable List(string TerminalNo)
		{
			try
			{
				string SQL=	SQLSelect();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				
				if (TerminalNo != null)
				{
					SQL += "WHERE TerminalNo = @TerminalNo;";

					MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
					prmTerminalNo.Value = TerminalNo;
					cmd.Parameters.Add(prmTerminalNo);
				}

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

		public System.Data.DataTable HourlyReport(int BranchID, string TerminalNo = Constants.ALL)
		{
            MySqlCommand cmd = HourlyReportPrivate(DateTime.MinValue, DateTime.MinValue, BranchID, TerminalNo);
			
			string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}

        public System.Data.DataTable HourlyReport(DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, int BranchID = 0, string TerminalNo = Constants.ALL)
        {
            MySqlCommand cmd = HourlyReportPrivate(StartDateTimeOfTransaction, UptoDateTimeOfTransaction, BranchID, TerminalNo);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

		public System.Data.DataTable GroupReport(int BranchID, string TerminalNo)
		{
            MySqlCommand cmd = GroupReportPrivate(TerminalNo, BranchID);
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            decimal TotalAmount = 0;
			foreach(System.Data.DataRow dr in dt.Rows)
			{
				TotalAmount += decimal.Parse(dr["Amount"].ToString());
			}

            if (TotalAmount != 0)
            {
                foreach (System.Data.DataRow dr in dt.Rows)
			    {
				    decimal percent = (decimal.Parse(dr["Amount"].ToString()) / TotalAmount) * 100;
				    dr["Percentage"] = percent.ToString("#,##0.#0") + "%";
			    }
            }

			return dt;
		}

        private MySqlCommand HourlyReportPrivate(DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, int BranchID = 0, string TerminalNo = Constants.ALL)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = "SELECT " +
                                    "DATE(TransactionDate) 'TransactionDate', " +
                                    "HOUR(TransactionDate) 'Time', " +
                                    "COUNT(SubTotal) 'TranCount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, SubTotal - Discount)) 'Amount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, Discount)) 'Discount' " +
                                "FROM  tblTransactions " +
                                "WHERE 1=1 ";
                if (TerminalNo != Constants.ALL)
                {
                    SQL += "AND TerminalNo = @TerminalNo ";
                    MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                    prmTerminalNo.Value = TerminalNo;
                    cmd.Parameters.Add(prmTerminalNo);
                }
                if (BranchID != 0)
                {
                    SQL += "AND BranchID = @BranchID ";
                    MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                    prmBranchID.Value = BranchID;
                    cmd.Parameters.Add(prmBranchID);
                }
                if (StartDateTimeOfTransaction.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND TransactionDate >= @StartDateTimeOfTransaction ";
                    MySqlParameter prmStartDateTimeOfTransaction = new MySqlParameter("@StartDateTimeOfTransaction",MySqlDbType.DateTime);
                    prmStartDateTimeOfTransaction.Value = StartDateTimeOfTransaction.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add(prmStartDateTimeOfTransaction);
                }
                if (UptoDateTimeOfTransaction.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND TransactionDate <= @UptoDateTimeOfTransaction ";
                    MySqlParameter prmUptoDateTimeOfTransaction = new MySqlParameter("@UptoDateTimeOfTransaction",MySqlDbType.DateTime);
                    prmUptoDateTimeOfTransaction.Value = UptoDateTimeOfTransaction.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add(prmUptoDateTimeOfTransaction);
                }
                SQL +=    "AND (TransactionStatus = @TransactionStatusClosed " +
                            "OR TransactionStatus = @TransactionStatusVoid " +
                            "OR TransactionStatus = @TransactionStatusReprinted " +
                            "OR TransactionStatus = @TransactionStatusRefund " +
                            "OR TransactionStatus = @TransactionStatusCreditPayment) " +
                        "GROUP BY DATE(TransactionDate), HOUR(TransactionDate) " +
                        "ORDER BY TransactionDate";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionStatusClosed = new MySqlParameter("@TransactionStatusClosed",MySqlDbType.Int16);
                prmTransactionStatusClosed.Value = (Int16)TransactionStatus.Closed;
                cmd.Parameters.Add(prmTransactionStatusClosed);

                MySqlParameter prmTransactionStatusVoid = new MySqlParameter("@TransactionStatusVoid",MySqlDbType.Int16);
                prmTransactionStatusVoid.Value = (Int16)TransactionStatus.Void;
                cmd.Parameters.Add(prmTransactionStatusVoid);

                MySqlParameter prmTransactionStatusReprinted = new MySqlParameter("@TransactionStatusReprinted",MySqlDbType.Int16);
                prmTransactionStatusReprinted.Value = (Int16)TransactionStatus.Reprinted;
                cmd.Parameters.Add(prmTransactionStatusReprinted);

                MySqlParameter prmTransactionStatusRefund = new MySqlParameter("@TransactionStatusRefund",MySqlDbType.Int16);
                prmTransactionStatusRefund.Value = (Int16)TransactionStatus.Refund;
                cmd.Parameters.Add(prmTransactionStatusRefund);

                MySqlParameter prmTransactionStatusCreditPayment = new MySqlParameter("@TransactionStatusCreditPayment",MySqlDbType.Int16);
                prmTransactionStatusCreditPayment.Value = (Int16)TransactionStatus.CreditPayment;
                cmd.Parameters.Add(prmTransactionStatusCreditPayment);

                return cmd;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        private MySqlCommand GroupReportPrivate(string TerminalNo, int BranchID)
		{
			try
			{
                string SQL = "SELECT " +
                                    "a.ProductGroup, " +
                                    "TransactionItemStatus, " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Quantity, a.Quantity))) 'TranCount', " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Amount, a.Amount))) 'Amount', " +
                                    "'0%' AS Percentage " +
                            "FROM  tblTransactionItems a " +
                                    "INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID " +
                                    "WHERE 1=1 " +
                                    "AND TerminalNo = @TerminalNo " +
                                    "AND BranchID = @BranchID " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                    "OR TransactionStatus = @TransactionStatusVoid " +
                                    "OR TransactionStatus = @TransactionStatusReprinted " +
                                    "OR TransactionStatus = @TransactionStatusRefund " +
                                    "OR TransactionStatus = @TransactionStatusCreditPayment) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID) " +
                            "GROUP BY ProductGroup";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmVoidStatus = new MySqlParameter("@VoidStatus",MySqlDbType.Int16);			
				prmVoidStatus.Value = TransactionItemStatus.Void.ToString("d");
				cmd.Parameters.Add(prmVoidStatus);

				MySqlParameter prmReturnStatus = new MySqlParameter("@ReturnStatus",MySqlDbType.Int16);			
				prmReturnStatus.Value = TransactionItemStatus.Return.ToString("d");
				cmd.Parameters.Add(prmReturnStatus);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

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

				return cmd;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public SalesTransactionDetails[] EJournalReport(int BranchID, string CashierName, string TerminalNo)
		{
			try
			{
                string SQL = "SELECT " +
                                    "TransactionID, " +
                                    "TransactionNo, " +
                                    "CustomerID, " +
                                    "CustomerName, " +
                                    "CashierID, " +
                                    "CashierName, " +
                                    "TerminalNo, " +
                                    "TransactionDate, " +
                                    "DateSuspended, " +
                                    "DateResumed, " +
                                    "TransactionStatus, " +
                                    "SubTotal, " +
                                    "ItemsDiscount, " +
                                    "Discount, " +
                                    "TransDiscount, " +
                                    "TransDiscountType, " +
                                    "VAT, " +
                                    "VatableAmount, " +
                                    "EVAT, " +
                                    "EVatableAmount, " +
                                    "LocalTax, " +
                                    "AmountPaid, " +
                                    "CashPayment, " +
                                    "ChequePayment, " +
                                    "CreditCardPayment, " +
                                    "CreditPayment, " +
                                    "DebitPayment, " +
                                    "RewardPointsPayment, " +
                                    "RewardConvertedPayment, " +
                                    "BalanceAmount, " +
                                    "ChangeAmount, " +
                                    "DateClosed, " +
                                    "PaymentType, " +
                                    "DiscountCode, " +
                                    "DiscountRemarks, " +
                                    "WaiterID, " +
                                    "WaiterName, " +
                                    "Charge, ChargeAmount, ChargeCode, ChargeRemarks " +
                                "FROM  tblTransactions " +
                                "WHERE TerminalNo = @TerminalNo " +
                                    "AND BranchID = @BranchID " +
                                    "AND CashierName = @CashierName " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                    "OR TransactionStatus = @TransactionStatusVoid " +
                                    "OR TransactionStatus = @TransactionStatusReprinted " +
                                    "OR TransactionStatus = @TransactionStatusRefund " +
                                    "OR TransactionStatus = @TransactionStatusCreditPayment) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID)";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);			
				prmCashierName.Value = CashierName;
				cmd.Parameters.Add(prmCashierName);

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

                MySqlDataReader myReader = base.ExecuteReader(cmd);

				ArrayList items = new ArrayList();
                
				while (myReader.Read())
				{
                    Data.SalesTransactionDetails Details = new Data.SalesTransactionDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.TransactionNo = "" + myReader["TransactionNo"].ToString();
					Details.CustomerID = myReader.GetInt64("CustomerID");
					Details.CustomerName = "" + myReader["CustomerName"].ToString();
					Details.CashierID = myReader.GetInt64("CashierID");
					Details.CashierName = "" + myReader["CashierName"].ToString();
					Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
					Details.TransactionDate = myReader.GetDateTime("TransactionDate");
					Details.DateSuspended = myReader.GetDateTime("DateSuspended");
					Details.DateResumed = myReader.GetDateTime("DateResumed");
					Details.TransactionStatus = (TransactionStatus) Enum.Parse(typeof(TransactionStatus), myReader.GetString("TransactionStatus"));
					Details.SubTotal = myReader.GetDecimal("SubTotal");
					Details.ItemsDiscount = myReader.GetDecimal("ItemsDiscount");
					Details.Discount = myReader.GetDecimal("Discount");
					Details.TransDiscount = myReader.GetDecimal("TransDiscount");
					Details.TransDiscountType = (DiscountTypes) Enum.Parse(typeof(DiscountTypes), myReader.GetString("TransDiscountType"));
					Details.VAT = myReader.GetDecimal("VAT");
					Details.VatableAmount = myReader.GetDecimal("VatableAmount");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
					Details.AmountPaid = myReader.GetDecimal("AmountPaid");
					Details.CashPayment = myReader.GetDecimal("CashPayment");
					Details.ChequePayment = myReader.GetDecimal("ChequePayment");
					Details.CreditCardPayment = myReader.GetDecimal("CreditCardPayment");
					Details.CreditPayment = myReader.GetDecimal("CreditPayment");
					Details.DebitPayment = myReader.GetDecimal("DebitPayment");
                    Details.RewardPointsPayment = myReader.GetDecimal("RewardPointsPayment");
                    Details.RewardConvertedPayment = myReader.GetDecimal("RewardConvertedPayment");
					Details.BalanceAmount = myReader.GetDecimal("BalanceAmount");
					Details.ChangeAmount = myReader.GetDecimal("ChangeAmount");
					Details.DateClosed = myReader.GetDateTime("DateClosed");
                    Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), myReader.GetString("PaymentType"));
					Details.WaiterID = myReader.GetInt64("WaiterID");
					Details.WaiterName = "" + myReader["WaiterName"].ToString();
					Details.Charge = myReader.GetDecimal("Charge");
					Details.ChargeAmount = myReader.GetDecimal("ChargeAmount");
					Details.ChargeCode = "" + myReader["ChargeCode"].ToString();
					Details.ChargeRemarks = "" + myReader["ChargeRemarks"].ToString();
					Details.isExist = true;

					items.Add(Details);
				}

				myReader.Close();

				SalesTransactionDetails[] Transactions = new SalesTransactionDetails[0];

				if (items != null)
				{
					Transactions = new SalesTransactionDetails[items.Count];
					items.CopyTo(Transactions);

                    for (int iCtr = 0; iCtr < Transactions.Length;iCtr++)
                    {
                        SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
                        Transactions[iCtr].TransactionItems = clsSalesTransactionItems.Details(Transactions[iCtr].TransactionID, Transactions[iCtr].TransactionDate);
                    }
				}

				return Transactions;		
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	}
}

