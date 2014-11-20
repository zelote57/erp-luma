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
        public Int32 BranchID;
        public string TerminalNo;
		public string BeginningTransactionNo;
		public string EndingTransactionNo;
        public string BeginningORNo;
        public string EndingORNo;
		public Int32   ZReadCount;
		public Int32   XReadCount;
        public decimal NetSales;
		public decimal GrossSales;
		public decimal TotalDiscount;
        public decimal SNRDiscount;
        public decimal PWDDiscount;
        public decimal OtherDiscount;
		public decimal TotalCharge;
		public decimal DailySales;
        public decimal ItemSold;
		public decimal QuantitySold;
		public decimal GroupSales;
		public decimal OldGrandTotal;
		public decimal NewGrandTotal;
        public decimal VATExempt;
        public decimal VATZeroRated;
        public decimal NonVATableAmount;
        public decimal VATableAmount;
		public decimal VAT;
        public decimal EVATableAmount;
		public decimal NonEVATableAmount;
		public decimal EVAT;
		public decimal LocalTax;
		public decimal CashSales;
		public decimal ChequeSales;
		public decimal CreditCardSales;
		public decimal CreditSales;
        public decimal RefundCash;
        public decimal RefundCheque;
        public decimal RefundCreditCard;
        public decimal RefundCredit;
        public decimal RefundDebit;
		public decimal CreditPayment;
        public decimal CreditPaymentCash;
        public decimal CreditPaymentCheque;
        public decimal CreditPaymentCreditCard;
        public decimal CreditPaymentDebit;
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
        public decimal DebitDeposit;
		public decimal BeginningBalance;
		public decimal VoidSales;
		public decimal RefundSales;
		public decimal ItemsDiscount;
        public decimal SNRItemsDiscount;
        public decimal PWDItemsDiscount;
        public decimal OtherItemsDiscount;
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
        /**
		 * Feb 6, 2014
		 * for RLC
		 * */
        public Int32 NoOfReprintedTransaction;
        public decimal TotalReprintedTransaction;
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
                                "BeginningORNo, " +
                                "EndingORNo, " +
					            "ZReadCount, " +
					            "XReadCount, " +
                                "NetSales, " + 
                                "GrossSales, " +
					            "TotalDiscount, " +
                                "SNRDiscount, " +
                                "PWDDiscount, " +
                                "OtherDiscount, " +
					            "TotalCharge, " +
					            "DailySales, " +
                                "ItemSold, " +
					            "QuantitySold, " +
					            "GroupSales, " +
					            "OldGrandTotal, " +
					            "NewGrandTotal, " +
                                "VATExempt, " +
					            "NonVATableAmount, " +
                                "VATableAmount, " +
					            "VAT, " +
					            "EVATableAmount, " +
					            "NonEVATableAmount, " +
					            "EVAT, " +
					            "LocalTax, " +
					            "CashSales, " +
					            "ChequeSales, " +
					            "CreditCardSales, " +
					            "CreditSales, " +
                                "RefundCash, " +
                                "RefundCheque, " +
                                "RefundCreditCard, " +
                                "RefundCredit, " +
                                "RefundDebit, " +
					            "CreditPayment, " +
                                "CreditPaymentCash, " +
                                "CreditPaymentCheque, " +
                                "CreditPaymentCreditCard, " +
                                "CreditPaymentDebit, " +
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
                                "SNRItemsDiscount, " +
                                "PWDItemsDiscount, " +
                                "OtherItemsDiscount, " +
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
                                "DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE tblTerminal.BranchID = tblTerminalReport.BranchID AND tblTerminal.TerminalNo = tblTerminalReport.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), '%Y-%m-%d') AS DateLastInitializedToDisplay, " +
                                "NoOfDiscountedTransactions, " +
                                "NegativeAdjustments, " +
                                "NoOfNegativeAdjustmentTransactions, " +
                                "PromotionalItems, " +
                                "CreditSalesTax, " +
                                "BatchCounter, " +
                                "DebitDeposit, " +
                                "NoOfReprintedTransaction, " +
                                "TotalReprintedTransaction, " +
                                "TrustFund " +
					        "FROM tblTerminalReport ";
            return stSQL;
        }

		#region Details
        public TerminalReportDetails Details(Int32 BranchID, string TerminalNo)
		{
			try
			{
                // Sep 22, 2014 update the figure using synchorize
                SyncTransactionSales(BranchID, TerminalNo);

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() +  "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
				
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                TerminalReportDetails Details = SetDetails(dt);

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public static TerminalReportDetails SetDetails(System.Data.DataTable dt)
        {
            TerminalReportDetails Details = new TerminalReportDetails();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                Details.TerminalNo = dr["TerminalNo"].ToString();
                Details.BeginningTransactionNo = dr["BeginningTransactionNo"].ToString();
                Details.EndingTransactionNo = dr["EndingTransactionNo"].ToString();
                Details.BeginningORNo = dr["BeginningORNo"].ToString();
                Details.EndingORNo = dr["EndingORNo"].ToString();
                Details.ZReadCount = Int32.Parse(dr["ZReadCount"].ToString());
                Details.XReadCount = Int32.Parse(dr["XReadCount"].ToString());
                Details.NetSales = decimal.Parse(dr["NetSales"].ToString());
                Details.GrossSales = decimal.Parse(dr["GrossSales"].ToString());
                Details.TotalDiscount = decimal.Parse(dr["TotalDiscount"].ToString());
                Details.SNRDiscount = decimal.Parse(dr["SNRDiscount"].ToString());
                Details.PWDDiscount = decimal.Parse(dr["PWDDiscount"].ToString());
                Details.OtherDiscount = decimal.Parse(dr["OtherDiscount"].ToString());
                Details.TotalCharge = decimal.Parse(dr["TotalCharge"].ToString());
                Details.DailySales = decimal.Parse(dr["DailySales"].ToString());
                Details.ItemSold = decimal.Parse(dr["ItemSold"].ToString());
                Details.QuantitySold = decimal.Parse(dr["QuantitySold"].ToString());
                Details.GroupSales = decimal.Parse(dr["GroupSales"].ToString());
                Details.OldGrandTotal = decimal.Parse(dr["OldGrandTotal"].ToString());
                Details.NewGrandTotal = decimal.Parse(dr["NewGrandTotal"].ToString());
                Details.VATExempt = decimal.Parse(dr["VATExempt"].ToString());
                Details.NonVATableAmount = decimal.Parse(dr["NonVATableAmount"].ToString());
                Details.VATableAmount = decimal.Parse(dr["VATableAmount"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.EVATableAmount = decimal.Parse(dr["EVATableAmount"].ToString());
                Details.NonEVATableAmount = decimal.Parse(dr["NonEVATableAmount"].ToString());
                Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                Details.CashSales = decimal.Parse(dr["CashSales"].ToString());
                Details.ChequeSales = decimal.Parse(dr["ChequeSales"].ToString());
                Details.CreditCardSales = decimal.Parse(dr["CreditCardSales"].ToString());
                Details.CreditSales = decimal.Parse(dr["CreditSales"].ToString());
                Details.RefundCash = decimal.Parse(dr["RefundCash"].ToString());
                Details.RefundCheque = decimal.Parse(dr["RefundCheque"].ToString());
                Details.RefundCreditCard = decimal.Parse(dr["RefundCreditCard"].ToString());
                Details.RefundCredit = decimal.Parse(dr["RefundCredit"].ToString());
                Details.RefundDebit = decimal.Parse(dr["RefundDebit"].ToString());
                Details.CreditPayment = decimal.Parse(dr["CreditPayment"].ToString());
                Details.CreditPaymentCash = decimal.Parse(dr["CreditPaymentCash"].ToString());
                Details.CreditPaymentCheque = decimal.Parse(dr["CreditPaymentCheque"].ToString());
                Details.CreditPaymentCreditCard = decimal.Parse(dr["CreditPaymentCreditCard"].ToString());
                Details.CreditPaymentDebit = decimal.Parse(dr["CreditPaymentDebit"].ToString());
                Details.DebitPayment = decimal.Parse(dr["DebitPayment"].ToString());
                Details.RewardPointsPayment = decimal.Parse(dr["RewardPointsPayment"].ToString());
                Details.RewardConvertedPayment = decimal.Parse(dr["RewardConvertedPayment"].ToString());
                Details.CashInDrawer = decimal.Parse(dr["CashInDrawer"].ToString());
                Details.TotalDisburse = decimal.Parse(dr["TotalDisburse"].ToString());
                Details.CashDisburse = decimal.Parse(dr["CashDisburse"].ToString());
                Details.ChequeDisburse = decimal.Parse(dr["ChequeDisburse"].ToString());
                Details.CreditCardDisburse = decimal.Parse(dr["CreditCardDisburse"].ToString());
                Details.TotalWithHold = decimal.Parse(dr["TotalWithHold"].ToString());
                Details.CashWithHold = decimal.Parse(dr["CashWithHold"].ToString());
                Details.ChequeWithHold = decimal.Parse(dr["ChequeWithHold"].ToString());
                Details.CreditCardWithHold = decimal.Parse(dr["CreditCardWithHold"].ToString());
                Details.TotalPaidOut = decimal.Parse(dr["TotalPaidOut"].ToString());
                Details.TotalDeposit = decimal.Parse(dr["TotalDeposit"].ToString());
                Details.CashDeposit = decimal.Parse(dr["CashDeposit"].ToString());
                Details.ChequeDeposit = decimal.Parse(dr["ChequeDeposit"].ToString());
                Details.CreditCardDeposit = decimal.Parse(dr["CreditCardDeposit"].ToString());
                Details.BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString());
                Details.VoidSales = decimal.Parse(dr["VoidSales"].ToString());
                Details.RefundSales = decimal.Parse(dr["RefundSales"].ToString());
                Details.ItemsDiscount = decimal.Parse(dr["ItemsDiscount"].ToString());
                Details.SNRItemsDiscount = decimal.Parse(dr["SNRItemsDiscount"].ToString());
                Details.PWDItemsDiscount = decimal.Parse(dr["PWDItemsDiscount"].ToString());
                Details.OtherItemsDiscount = decimal.Parse(dr["OtherItemsDiscount"].ToString());
                Details.SubTotalDiscount = decimal.Parse(dr["SubTotalDiscount"].ToString());
                Details.NoOfCashTransactions = Int32.Parse(dr["NoOfCashTransactions"].ToString());
                Details.NoOfChequeTransactions = Int32.Parse(dr["NoOfChequeTransactions"].ToString());
                Details.NoOfCreditCardTransactions = Int32.Parse(dr["NoOfCreditCardTransactions"].ToString());
                Details.NoOfCreditTransactions = Int32.Parse(dr["NoOfCreditTransactions"].ToString());
                Details.NoOfCombinationPaymentTransactions = Int32.Parse(dr["NoOfCombinationPaymentTransactions"].ToString());
                Details.NoOfCreditPaymentTransactions = Int32.Parse(dr["NoOfCreditPaymentTransactions"].ToString());
                Details.NoOfDebitPaymentTransactions = Int32.Parse(dr["NoOfDebitPaymentTransactions"].ToString());
                Details.NoOfClosedTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString());
                Details.NoOfRefundTransactions = Int32.Parse(dr["NoOfRefundTransactions"].ToString());
                Details.NoOfVoidTransactions = Int32.Parse(dr["NoOfVoidTransactions"].ToString());
                Details.NoOfRewardPointsPayment = Int32.Parse(dr["NoOfRewardPointsPayment"].ToString());
                Details.NoOfTotalTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString());
                Details.DateLastInitialized = DateTime.Parse(dr["DateLastInitialized"].ToString());
                Details.DateLastInitializedToDisplay = DateTime.Parse(dr["DateLastInitializedToDisplay"].ToString());
                Details.NoOfDiscountedTransactions = Int32.Parse(dr["NoOfDiscountedTransactions"].ToString());
                Details.NegativeAdjustments = decimal.Parse(dr["NegativeAdjustments"].ToString());
                Details.NoOfNegativeAdjustmentTransactions = Int32.Parse(dr["NoOfNegativeAdjustmentTransactions"].ToString());
                Details.PromotionalItems = decimal.Parse(dr["PromotionalItems"].ToString());
                Details.CreditSalesTax = decimal.Parse(dr["CreditSalesTax"].ToString());
                Details.BatchCounter = Int32.Parse(dr["BatchCounter"].ToString());
                Details.DebitDeposit = decimal.Parse(dr["DebitDeposit"].ToString());
                Details.NoOfReprintedTransaction = Int32.Parse(dr["NoOfReprintedTransaction"].ToString());
                Details.TotalReprintedTransaction = decimal.Parse(dr["TotalReprintedTransaction"].ToString());

                Details.TrustFund = decimal.Parse(dr["TrustFund"].ToString());
            }

            return Details;
        }
        public static TerminalReportDetails[] SetDetailsList(System.Data.DataTable dt)
        {
            ArrayList items = new ArrayList();
            
            TerminalReportDetails Details = new TerminalReportDetails(); ;
            TerminalReportDetails[] DetailsList = new TerminalReportDetails[0];

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                Details.TerminalNo = dr["TerminalNo"].ToString();
                Details.BeginningTransactionNo = dr["BeginningTransactionNo"].ToString();
                Details.EndingTransactionNo = dr["EndingTransactionNo"].ToString();
                Details.BeginningORNo = dr["BeginningORNo"].ToString();
                Details.EndingORNo = dr["EndingORNo"].ToString();
                Details.ZReadCount = Int32.Parse(dr["ZReadCount"].ToString());
                Details.XReadCount = Int32.Parse(dr["XReadCount"].ToString());
                Details.NetSales = decimal.Parse(dr["NetSales"].ToString());
                Details.GrossSales = decimal.Parse(dr["GrossSales"].ToString());
                Details.TotalDiscount = decimal.Parse(dr["TotalDiscount"].ToString());
                Details.SNRDiscount = decimal.Parse(dr["SNRDiscount"].ToString());
                Details.PWDDiscount = decimal.Parse(dr["PWDDiscount"].ToString());
                Details.OtherDiscount = decimal.Parse(dr["OtherDiscount"].ToString());
                Details.TotalCharge = decimal.Parse(dr["TotalCharge"].ToString());
                Details.DailySales = decimal.Parse(dr["DailySales"].ToString());
                Details.ItemSold = decimal.Parse(dr["ItemSold "].ToString());
                Details.QuantitySold = decimal.Parse(dr["QuantitySold"].ToString());
                Details.GroupSales = decimal.Parse(dr["GroupSales"].ToString());
                Details.OldGrandTotal = decimal.Parse(dr["OldGrandTotal"].ToString());
                Details.NewGrandTotal = decimal.Parse(dr["NewGrandTotal"].ToString());
                Details.VATExempt = decimal.Parse(dr["VATExempt"].ToString());
                Details.NonVATableAmount = decimal.Parse(dr["NonVATableAmount"].ToString());
                Details.VATableAmount = decimal.Parse(dr["VATableAmount"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.EVATableAmount = decimal.Parse(dr["EVATableAmount"].ToString());
                Details.NonEVATableAmount = decimal.Parse(dr["NonEVATableAmount"].ToString());
                Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                Details.CashSales = decimal.Parse(dr["CashSales"].ToString());
                Details.ChequeSales = decimal.Parse(dr["ChequeSales"].ToString());
                Details.CreditCardSales = decimal.Parse(dr["CreditCardSales"].ToString());
                Details.CreditSales = decimal.Parse(dr["CreditSales"].ToString());
                Details.RefundCash = decimal.Parse(dr["RefundCash"].ToString());
                Details.RefundCheque = decimal.Parse(dr["RefundCheque"].ToString());
                Details.RefundCreditCard = decimal.Parse(dr["RefundCreditCard"].ToString());
                Details.RefundCredit = decimal.Parse(dr["RefundCredit"].ToString());
                Details.RefundDebit = decimal.Parse(dr["RefundDebit"].ToString());
                Details.CreditPayment = decimal.Parse(dr["CreditPayment"].ToString());
                Details.CreditPaymentCash = decimal.Parse(dr["CreditPaymentCash"].ToString());
                Details.CreditPaymentCheque = decimal.Parse(dr["CreditPaymentCheque"].ToString());
                Details.CreditPaymentCreditCard = decimal.Parse(dr["CreditPaymentCreditCard"].ToString());
                Details.CreditPaymentDebit = decimal.Parse(dr["CreditPaymentDebit"].ToString());
                Details.DebitPayment = decimal.Parse(dr["DebitPayment"].ToString());
                Details.RewardPointsPayment = decimal.Parse(dr["RewardPointsPayment"].ToString());
                Details.RewardConvertedPayment = decimal.Parse(dr["RewardConvertedPayment"].ToString());
                Details.CashInDrawer = decimal.Parse(dr["CashInDrawer"].ToString());
                Details.TotalDisburse = decimal.Parse(dr["TotalDisburse"].ToString());
                Details.CashDisburse = decimal.Parse(dr["CashDisburse"].ToString());
                Details.ChequeDisburse = decimal.Parse(dr["ChequeDisburse"].ToString());
                Details.CreditCardDisburse = decimal.Parse(dr["CreditCardDisburse"].ToString());
                Details.TotalWithHold = decimal.Parse(dr["TotalWithHold"].ToString());
                Details.CashWithHold = decimal.Parse(dr["CashWithHold"].ToString());
                Details.ChequeWithHold = decimal.Parse(dr["ChequeWithHold"].ToString());
                Details.CreditCardWithHold = decimal.Parse(dr["CreditCardWithHold"].ToString());
                Details.TotalPaidOut = decimal.Parse(dr["TotalPaidOut"].ToString());
                Details.TotalDeposit = decimal.Parse(dr["TotalDeposit"].ToString());
                Details.CashDeposit = decimal.Parse(dr["CashDeposit"].ToString());
                Details.ChequeDeposit = decimal.Parse(dr["ChequeDeposit"].ToString());
                Details.CreditCardDeposit = decimal.Parse(dr["CreditCardDeposit"].ToString());
                Details.BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString());
                Details.VoidSales = decimal.Parse(dr["VoidSales"].ToString());
                Details.RefundSales = decimal.Parse(dr["RefundSales"].ToString());
                Details.ItemsDiscount = decimal.Parse(dr["ItemsDiscount"].ToString());
                Details.SNRItemsDiscount = decimal.Parse(dr["SNRItemsDiscount"].ToString());
                Details.PWDItemsDiscount = decimal.Parse(dr["PWDItemsDiscount"].ToString());
                Details.OtherItemsDiscount = decimal.Parse(dr["OtherItemsDiscount"].ToString());
                Details.SubTotalDiscount = decimal.Parse(dr["SubTotalDiscount"].ToString());
                Details.NoOfCashTransactions = Int32.Parse(dr["NoOfCashTransactions"].ToString());
                Details.NoOfChequeTransactions = Int32.Parse(dr["NoOfChequeTransactions"].ToString());
                Details.NoOfCreditCardTransactions = Int32.Parse(dr["NoOfCreditCardTransactions"].ToString());
                Details.NoOfCreditTransactions = Int32.Parse(dr["NoOfCreditTransactions"].ToString());
                Details.NoOfCombinationPaymentTransactions = Int32.Parse(dr["NoOfCombinationPaymentTransactions"].ToString());
                Details.NoOfCreditPaymentTransactions = Int32.Parse(dr["NoOfCreditPaymentTransactions"].ToString());
                Details.NoOfDebitPaymentTransactions = Int32.Parse(dr["NoOfDebitPaymentTransactions"].ToString());
                Details.NoOfClosedTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString());
                Details.NoOfRefundTransactions = Int32.Parse(dr["NoOfRefundTransactions"].ToString());
                Details.NoOfVoidTransactions = Int32.Parse(dr["NoOfVoidTransactions"].ToString());
                Details.NoOfRewardPointsPayment = Int32.Parse(dr["NoOfRewardPointsPayment"].ToString());
                Details.NoOfTotalTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString());
                Details.DateLastInitialized = DateTime.Parse(dr["DateLastInitialized"].ToString());
                Details.DateLastInitializedToDisplay = DateTime.Parse(dr["DateLastInitializedToDisplay"].ToString());
                Details.NoOfDiscountedTransactions = Int32.Parse(dr["NoOfDiscountedTransactions"].ToString());
                Details.NegativeAdjustments = decimal.Parse(dr["NegativeAdjustments"].ToString());
                Details.NoOfNegativeAdjustmentTransactions = Int32.Parse(dr["NoOfNegativeAdjustmentTransactions"].ToString());
                Details.PromotionalItems = decimal.Parse(dr["PromotionalItems"].ToString());
                Details.CreditSalesTax = decimal.Parse(dr["CreditSalesTax"].ToString());
                Details.BatchCounter = Int32.Parse(dr["BatchCounter"].ToString());
                Details.DebitDeposit = decimal.Parse(dr["DebitDeposit"].ToString());
                Details.NoOfReprintedTransaction = Int32.Parse(dr["NoOfReprintedTransaction"].ToString());
                Details.TotalReprintedTransaction = decimal.Parse(dr["TotalReprintedTransaction"].ToString());

                items.Add(Details);
            }

            if (items != null)
            {
                DetailsList = new TerminalReportDetails[items.Count];
                items.CopyTo(DetailsList);
            }

            return DetailsList;
        }
		public string EndingTransactioNo(Int32 BranchID, string TerminalNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
					            "EndingTransactionNo " +
					        "FROM tblTerminalReport " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				string stEndingTransactioNo = string.Empty;
                foreach(System.Data.DataRow dr in dt.Rows)
				{
					stEndingTransactioNo = dr["EndingTransactionNo"].ToString();
				}

				return stEndingTransactioNo;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        /// <summary>
        /// Get the Last ZReadInitialization Date.
        /// If ProcessingDate is same as Constants.C_DATE_MIN_VALUE then it will get DateLastInitialized DESC
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="ProcessingDate"></param>
        /// <returns></returns>
		public DateTime MAXDateLastInitialized(Int32 BranchID, string TerminalNo, DateTime ProcessingDate)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT MAX(DateLastInitialized) AS DateLastInitialized FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo ";

                if (ProcessingDate == Constants.C_DATE_MIN_VALUE || ProcessingDate == DateTime.MinValue)
                {
                    SQL += "ORDER BY DateLastInitialized DESC LIMIT 1;";
                }
                else
                {
                    SQL += "AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@ProcessingDate, '%Y-%m-%d %H:%i') ";
                    cmd.Parameters.AddWithValue("ProcessingDate", ProcessingDate);
                }

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
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

		public void UpdateBeginningBalance(Int32 BranchID, string TerminalNo, decimal Amount)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblTerminalReport SET " +
					            "CashInDrawer		= CashInDrawer + @CashInDrawer, " +
					            "BeginningBalance	= BeginningBalance + @BeginningBalance " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
	 			
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("BeginningBalance", Amount);
                cmd.Parameters.AddWithValue("CashInDrawer", Amount);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void UpdateZReadCount(Int32 BranchID, string TerminalNo)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblTerminalReport SET " +
					            "ZReadCount = ZReadCount + 1 " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void UpdateXReadCount(Int32 BranchID, string TerminalNo)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblTerminalReport SET " +
					            "XReadCount = XReadCount + 1 " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void IncrementBatchCounter(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportIncrementBatchCounter(@BranchID, @TerminalNo);";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateTrustFund(Int32 BranchID, string TerminalNo, decimal TrustFund, string UpdatedBy, string Reason)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportUpdateTrustFund(@BranchID, @TerminalNo, @TrustFund, @UpdatedBy, @Reason);";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TrustFund", TrustFund);
                cmd.Parameters.AddWithValue("UpdatedBy", UpdatedBy);
                cmd.Parameters.AddWithValue("Reason", Reason);

                cmd.CommandText = SQL;
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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportUpdateTransactionSales(@BranchID, @TerminalNo," +
                                    "@NetSales," +
                                    "@GrossSales, " +
                                    "@TotalDiscount, " +
                                    "@SNRDiscount, " +
                                    "@PWDDiscount, " +
                                    "@OtherDiscount, " +
                                    "@TotalCharge, " +
                                    "@DailySales, " +
                                    "@ItemSold, " +
                                    "@QuantitySold, " +
                                    "@GroupSales, " +
                                    "@OldGrandTotal, " +
                                    "@NewGrandTotal, " +
                                    "@VATExempt, " +
                                    "@NonVATableAmount, " +
                                    "@VATableAmount, " +
                                    "@VAT, " +
                                    "@EVATableAmount, " +
                                    "@NonEVATableAmount, " +
                                    "@EVAT, " +
                                    "@LocalTax, " +
                                    "@CashSales, " +
                                    "@ChequeSales, " +
                                    "@CreditCardSales, " +
                                    "@CreditSales, " +
                                    "@CreditPayment, " +
                                    "@CreditPaymentCash, " +
                                    "@CreditPaymentCheque, " +
                                    "@CreditPaymentCreditCard, " +
                                    "@CreditPaymentDebit, " +
                                    "@DebitPayment, " +
                                    "@RewardPointsPayment, " +
                                    "@RewardConvertedPayment, " +
                                    "@CashInDrawer, " +
                                    "@VoidSales, " +
                                    "@RefundSales, " +
                                    "@ItemsDiscount, " +
                                    "@SNRItemsDiscount, " +
                                    "@PWDItemsDiscount, " +
                                    "@OtherItemsDiscount, " +
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

				cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@NetSales", Details.NetSales);
                cmd.Parameters.AddWithValue("@GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("@TotalDiscount", Details.TotalDiscount);
                cmd.Parameters.AddWithValue("@SNRDiscount", Details.SNRDiscount);
                cmd.Parameters.AddWithValue("@PWDDiscount", Details.PWDDiscount);
                cmd.Parameters.AddWithValue("@OtherDiscount", Details.OtherDiscount);
                cmd.Parameters.AddWithValue("@TotalCharge", Details.TotalCharge);
                cmd.Parameters.AddWithValue("@DailySales", Details.DailySales);
                cmd.Parameters.AddWithValue("@ItemSold", Details.ItemSold);
                cmd.Parameters.AddWithValue("@QuantitySold", Details.QuantitySold);
                cmd.Parameters.AddWithValue("@GroupSales", Details.GroupSales);
                cmd.Parameters.AddWithValue("@OldGrandTotal", 0);
                cmd.Parameters.AddWithValue("@NewGrandTotal", Details.GrossSales);
                
                cmd.Parameters.AddWithValue("@VATExempt", Details.VATExempt);
                cmd.Parameters.AddWithValue("@NonVATableAmount", Details.NonVATableAmount);
                cmd.Parameters.AddWithValue("@VATableAmount", Details.VATableAmount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVATableAmount", Details.EVATableAmount);
                cmd.Parameters.AddWithValue("@NonEVATableAmount", Details.NonEVATableAmount);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                
                cmd.Parameters.AddWithValue("@CashSales", Details.CashSales);
                cmd.Parameters.AddWithValue("@ChequeSales", Details.ChequeSales);
                cmd.Parameters.AddWithValue("@CreditCardSales", Details.CreditCardSales);
                cmd.Parameters.AddWithValue("@CreditSales", Details.CreditSales);

                cmd.Parameters.AddWithValue("@CreditPayment", Details.CreditPayment);
                cmd.Parameters.AddWithValue("@CreditPaymentCash", Details.CreditPaymentCash);
                cmd.Parameters.AddWithValue("@CreditPaymentCheque", Details.CreditPaymentCheque);
                cmd.Parameters.AddWithValue("@CreditPaymentCreditCard", Details.CreditPaymentCreditCard);
                cmd.Parameters.AddWithValue("@CreditPaymentDebit", Details.CreditPaymentDebit);
                cmd.Parameters.AddWithValue("@DebitPayment", Details.DebitPayment);
                cmd.Parameters.AddWithValue("@RewardPointsPayment", Details.RewardPointsPayment);
                cmd.Parameters.AddWithValue("@RewardConvertedPayment", Details.RewardConvertedPayment);
                // prmCashInDrawer.Value = Details.CashSales; //refer to cash sales
                // Apr 12, 2014 make an override use the cash in drawer due to credit payment
                cmd.Parameters.AddWithValue("@CashInDrawer", Details.CashInDrawer == 0 ? Details.CashSales : Details.CashInDrawer);

                cmd.Parameters.AddWithValue("@VoidSales", Details.VoidSales);
                cmd.Parameters.AddWithValue("@RefundSales", Details.RefundSales);
                cmd.Parameters.AddWithValue("@ItemsDiscount", Details.ItemsDiscount);
                cmd.Parameters.AddWithValue("@SNRItemsDiscount", Details.SNRItemsDiscount);
                cmd.Parameters.AddWithValue("@PWDItemsDiscount", Details.PWDItemsDiscount);
                cmd.Parameters.AddWithValue("@OtherItemsDiscount", Details.OtherItemsDiscount);
                cmd.Parameters.AddWithValue("@SubTotalDiscount", Details.SubTotalDiscount);
                cmd.Parameters.AddWithValue("@NoOfCashTransactions", Details.NoOfCashTransactions);
                cmd.Parameters.AddWithValue("@NoOfChequeTransactions", Details.NoOfChequeTransactions);
                cmd.Parameters.AddWithValue("@NoOfCreditCardTransactions", Details.NoOfCreditCardTransactions);
                cmd.Parameters.AddWithValue("@NoOfCreditTransactions", Details.NoOfCreditTransactions);
                cmd.Parameters.AddWithValue("@NoOfCombinationPaymentTransactions", Details.NoOfCombinationPaymentTransactions);
                cmd.Parameters.AddWithValue("@NoOfCreditPaymentTransactions", Details.NoOfCreditPaymentTransactions);
                cmd.Parameters.AddWithValue("@NoOfDebitPaymentTransactions", Details.NoOfDebitPaymentTransactions);
                cmd.Parameters.AddWithValue("@NoOfClosedTransactions", Details.NoOfClosedTransactions);
                cmd.Parameters.AddWithValue("@NoOfRefundTransactions", Details.NoOfRefundTransactions);
                cmd.Parameters.AddWithValue("@NoOfVoidTransactions", Details.NoOfVoidTransactions);
                cmd.Parameters.AddWithValue("@NoOfRewardPointsPayment", Details.NoOfRewardPointsPayment);
                cmd.Parameters.AddWithValue("@NoOfTotalTransactions", Details.NoOfTotalTransactions);
                cmd.Parameters.AddWithValue("@NoOfDiscountedTransactions", Details.NoOfDiscountedTransactions);
                cmd.Parameters.AddWithValue("@NegativeAdjustments", Details.NegativeAdjustments);
                cmd.Parameters.AddWithValue("@NoOfNegativeAdjustmentTransactions", Details.NoOfNegativeAdjustmentTransactions);
                cmd.Parameters.AddWithValue("@PromotionalItems", Details.PromotionalItems);
                cmd.Parameters.AddWithValue("@CreditSalesTax", Details.CreditSalesTax);
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void SyncTransactionSales(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportSyncTransactionSales(@BranchID, @TerminalNo);";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void UpdateWithHold(WithholdDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "";

                cmd.Parameters.AddWithValue("TotalWithHold", Details.Amount);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CashWithHold						= CashWithHold + @CashWithHold, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("CashWithHold", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
                    cmd.Parameters.AddWithValue("CashSales", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "ChequeWithHold						= ChequeWithHold + @ChequeWithHold " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("ChequeWithHold", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CreditCardWithHold					= CreditCardWithHold + @CreditCardWithHold " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					cmd.Parameters.AddWithValue("CreditCardWithHold", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "";
	 			
                cmd.Parameters.AddWithValue("TotalDisburse", Details.Amount);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CashDisburse						= CashDisburse + @CashDisburse, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("CashDisburse", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
                    cmd.Parameters.AddWithValue("CashSales", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "ChequeDisburse						= ChequeDisburse + @ChequeDisburse " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("ChequeDisburse", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CreditCardDisburse					= CreditCardDisburse + @CreditCardDisburse " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					cmd.Parameters.AddWithValue("CreditCardDisburse", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "";
				
                cmd.Parameters.AddWithValue("TotalPaidOut", Details.Amount);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CashPaidOut						= CashPaidOut + @CashPaidOut, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("CashPaidOut", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
                    cmd.Parameters.AddWithValue("CashSales", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "ChequePaidOut						= ChequePaidOut + @ChequePaidOut " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("ChequePaidOut", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CreditCardPaidOut					= CreditCardPaidOut + @CreditCardPaidOut " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

					MySqlParameter prmCreditCardPaidOut = new MySqlParameter("@CreditCardPaidOut",MySqlDbType.Decimal);
                    cmd.Parameters.AddWithValue("CreditCardPaidOut", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "";

                cmd.Parameters.AddWithValue("TotalDeposit", Details.Amount);

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CashDeposit						= CashDeposit + @CashDeposit, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("CashDeposit", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
                    cmd.Parameters.AddWithValue("CashSales", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						    "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						    "ChequeDeposit						= ChequeDeposit + @ChequeDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("ChequeDeposit", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblTerminalReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CreditCardDeposit					= CreditCardDeposit + @CreditCardDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("CreditCardDeposit", Details.Amount);
				}
                else if (Details.PaymentType == PaymentTypes.Debit)
                {
                    SQL = "UPDATE tblTerminalReport SET " +
                            "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
                            "DebitDeposit					    = DebitDeposit + @DebitDeposit " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("DebitDeposit", Details.Amount);
                }

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        // 06Feb2014 RLC Accreditation:
        // Update no of reprinted transaction
        public void UpdateReprintedTransaction(int BranchID, string TerminalNo, string TransactionNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportRePrintedUpdate(@BranchID, @TerminalNo, @TransactionNo);";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);

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

        /// <summary>
        ///  Sep 4: remove this redundant
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalID"></param>
        /// <param name="TerminalNo"></param>
        //public void Insert(int BranchID, Int16 TerminalID, string TerminalNo)
        //{
        //    try
        //    {
        //        string SQL = "INSERT INTO tblTerminalReport (BranchID, TerminalID, TerminalNo, DateLastInitialized) " +
        //            "VALUES (@BranchID, @TerminalID, @TerminalNo, @DateLastInitialized);";
				
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
        //        prmBranchID.Value = BranchID;
        //        cmd.Parameters.Add(prmBranchID);

        //        MySqlParameter prmTerminalID = new MySqlParameter("@TerminalNo",MySqlDbType.Int16);			
        //        prmTerminalID.Value = TerminalID;
        //        cmd.Parameters.Add(prmTerminalID);

        //        MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
        //        prmTerminalNo.Value = TerminalNo;
        //        cmd.Parameters.Add(prmTerminalNo);

        //        MySqlParameter prmDateLastInitialized = new MySqlParameter("@DateLastInitialized",MySqlDbType.DateTime);			
        //        prmDateLastInitialized.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmDateLastInitialized);

        //        base.ExecuteNonQuery(cmd);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}

		public void UpdateTerminalNo(Int32 TerminalID, string TerminalNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL="UPDATE tblTerminalReport SET "  +
					            "TerminalNo		=	@TerminalNo " +
					       "WHERE TerminalID	=	@TerminalID;";

                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@TerminalID", TerminalID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Public Methods

        public void InitializeZRead(Int32 BranchID, string TerminalNo, string InitializedBy, DateTime DateLastInitialized, bool WithOutTF)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTerminalReportInitializeZRead(@BranchID, @TerminalNo, @DateLastInitialized, @InitializedBy, @WithOutTF);";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@DateLastInitialized", DateLastInitialized == Constants.C_DATE_MIN_VALUE || DateLastInitialized == DateTime.MinValue ? DateTime.Now : DateLastInitialized);
                cmd.Parameters.AddWithValue("@InitializedBy", InitializedBy);
                cmd.Parameters.AddWithValue("@WithOutTF", WithOutTF);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                UpdateZReadCount(BranchID, TerminalNo);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public bool IsAllTerminalInitialize(Int32 BranchID, DateTime CuttOfDateTime)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"SELECT " + 
					            "COUNT(TerminalNo) AS RecordCount " +
					        "FROM tblTerminalReport " +
                            "WHERE BranchID = @BranchID AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i') < DATE_FORMAT(@CuttOfDateTime, '%Y-%m-%d %H:%i') ";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("CuttOfDateTime", CuttOfDateTime);

                cmd.CommandText = SQL;
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

        public System.Data.DataTable HourlyReport(string BeginningTransactionNo, string EndingTransactionNo, Int32 BranchID, string TerminalNo = Constants.ALL)
		{
            MySqlCommand cmd = HourlyReportPrivate(BeginningTransactionNo, EndingTransactionNo, DateTime.MinValue, DateTime.MinValue, BranchID, TerminalNo);
			
			string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}

        public System.Data.DataTable HourlyReport(string BeginningTransactionNo, string EndingTransactionNo, DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, int BranchID = 0, string TerminalNo = Constants.ALL)
        {
            MySqlCommand cmd = HourlyReportPrivate(BeginningTransactionNo, EndingTransactionNo, StartDateTimeOfTransaction, UptoDateTimeOfTransaction, BranchID, TerminalNo);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

		public System.Data.DataTable GroupReport(Int32 BranchID, string TerminalNo)
		{
            MySqlCommand cmd = GroupReportPrivate(BranchID, TerminalNo);
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

        private MySqlCommand HourlyReportPrivate(string BeginningTransactionNo, string EndingTransactionNo, DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, Int32 BranchID = 0, string TerminalNo = Constants.ALL)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                    "DATE(TransactionDate) 'TransactionDate', " +
                                    "HOUR(TransactionDate) 'Time', " +
                                    "COUNT(SubTotal) 'TranCount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, SubTotal)) 'Amount', " +
                                    "SUM(IF(TransactionStatus = @TransactionStatusVoid, 0, Discount)) 'Discount' " +
                                "FROM  tblTransactions " +
                                "WHERE 1=1 ";
                if (TerminalNo != Constants.ALL)
                {
                    SQL += "AND TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                }
                if (BranchID != 0)
                {
                    SQL += "AND BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                }
                if (!string.IsNullOrEmpty(BeginningTransactionNo))
                {
                    SQL += "AND TransactionNo >= @BeginningTransactionNo ";
                    cmd.Parameters.AddWithValue("@BeginningTransactionNo", BeginningTransactionNo);
                }
                if (!string.IsNullOrEmpty(EndingTransactionNo))
                {
                    SQL += "AND TransactionNo <= @EndingTransactionNo ";
                    cmd.Parameters.AddWithValue("@EndingTransactionNo", EndingTransactionNo);
                }
                if (StartDateTimeOfTransaction.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND TransactionDate >= @StartDateTimeOfTransaction ";
                    cmd.Parameters.AddWithValue("@StartDateTimeOfTransaction", StartDateTimeOfTransaction.GetValueOrDefault());
                }
                if (UptoDateTimeOfTransaction.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND TransactionDate <= @UptoDateTimeOfTransaction ";
                    cmd.Parameters.AddWithValue("@UptoDateTimeOfTransaction", UptoDateTimeOfTransaction.GetValueOrDefault());
                }
                SQL +=    "AND (TransactionStatus = @TransactionStatusClosed " +
                            "OR TransactionStatus = @TransactionStatusVoid " +
                            "OR TransactionStatus = @TransactionStatusReprinted " +
                            "OR TransactionStatus = @TransactionStatusRefund) " +
                        "GROUP BY DATE(TransactionDate), HOUR(TransactionDate) " +
                        "ORDER BY TransactionDate";

                cmd.Parameters.AddWithValue("@TransactionStatusClosed", TransactionStatus.Closed.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusVoid", TransactionStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusReprinted", TransactionStatus.Reprinted.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusRefund", TransactionStatus.Refund.ToString("d"));

                cmd.CommandText = SQL;
                return cmd;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        private MySqlCommand GroupReportPrivate(Int32 BranchID, string TerminalNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

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
                                        "OR TransactionStatus = @TransactionStatusRefund) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID) " +
                            "GROUP BY ProductGroup";


                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@VoidStatus", TransactionItemStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@ReturnStatus", TransactionItemStatus.Return.ToString("d"));
                
                cmd.Parameters.AddWithValue("@TransactionStatusClosed", TransactionStatus.Closed.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusVoid", TransactionStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusReprinted", TransactionStatus.Reprinted.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusRefund", TransactionStatus.Refund.ToString("d"));
				
                cmd.CommandText = SQL;
				return cmd;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public SalesTransactionDetails[] EJournalReport(Int32 BranchID, string CashierName, string TerminalNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT TransactionNo FROM  tblTransactions " +
                                "WHERE TerminalNo = @TerminalNo " +
                                    "AND BranchID = @BranchID " +
                                    "AND CashierName = @CashierName " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                    "  OR TransactionStatus = @TransactionStatusVoid " +
                                    "  OR TransactionStatus = @TransactionStatusReprinted " +
                                    "  OR TransactionStatus = @TransactionStatusRefund) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID)";
				
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CashierName", CashierName);
                cmd.Parameters.AddWithValue("@TransactionStatusClosed", TransactionStatus.Closed.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusVoid", TransactionStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusReprinted", TransactionStatus.Reprinted.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusRefund", TransactionStatus.Refund.ToString("d"));

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				ArrayList items = new ArrayList();

                SalesTransactionDetails Details = new SalesTransactionDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
				{
                    Details = new Data.SalesTransactions(base.Connection, base.Transaction).Details(dr["TransactionNo"].ToString(), TerminalNo, BranchID);
                    Details.TransactionItems = new SalesTransactionItems(base.Connection, base.Transaction).Details(Details.TransactionID, Details.TransactionDate);
					items.Add(Details);
				}

                SalesTransactionDetails[] arrclsSalesTransactionDetails = new SalesTransactionDetails[0];

				if (items != null)
				{
                    arrclsSalesTransactionDetails = new SalesTransactionDetails[items.Count];
                    items.CopyTo(arrclsSalesTransactionDetails);
				}

                return arrclsSalesTransactionDetails;		
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	}
}

