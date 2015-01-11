using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

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
	public struct CashierReportDetails
	{
        public Int32 BranchID;
        public string TerminalNo;
        public Int64 SyncID;
        public Int64 CashierReportID;
        public Int64 CashierReportHistoryID; //for historyonly
		public Int64  CashierID;
        public Int32 TerminalID;
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
        public decimal VATExempt;
        public decimal NonVATableAmount;
        public decimal VATableAmount;
        public decimal ZeroRatedSales;             
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
        public decimal CashPaidOut;
        public decimal ChequePaidOut;
        public decimal CreditCardPaidOut;
		public decimal TotalDeposit;
		public decimal CashDeposit;
		public decimal ChequeDeposit;
		public decimal CreditCardDeposit;
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
		public Int32   NoOfClosedTransactions;
		public Int32   NoOfVoidTransactions;
		public Int32   NoOfRefundTransactions;
		public Int32   NoOfTotalTransactions;
		public decimal CashCount;
		public DateTime LastLoginDate;

        // March 19, 2009
        // Added by Lemuel E. Aceron
        // For the purpose of RLC Accreditation
        public Int32 NoOfDiscountedTransactions;
        public decimal NegativeAdjustments;
        public Int32 NoOfNegativeAdjustmentTransactions;
        public decimal PromotionalItems;
        public decimal CreditSalesTax;

        public decimal DebitDeposit;

        /**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
        public decimal RewardPointsPayment;
        public decimal RewardConvertedPayment;
        public Int32 NoOfRewardPointsPayment;

        public bool IsCashCountInitialized;
        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CashierReports : POSConnection
	{
		
		#region Constructors & Destructors

		public CashierReports()
            : base(null, null)
        {
        }

        public CashierReports(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Details

		public CashierReportDetails Details(Int64 CashierID, Int32 BranchID, string TerminalNo)
		{
			try
			{
                // Sep 22, 2014 update the figure using synchorize
                new TerminalReport(base.Connection, base.Transaction).SyncTransactionSales(BranchID, TerminalNo);

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
                                "BranchID, " +
                                "TerminalNo, " +
                                "CashierID, " +
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
								"VATExempt, " +
                                "NonVATableAmount, " +
                                "VAT, " +
                                "VATableAmount, " +
                                "ZeroRatedSales, " +
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
								"CashCount, " +
								"LastLoginDate " +
							"FROM tblCashierReport " +
							"WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				  
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return SetDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public static CashierReportDetails SetDetails(System.Data.DataTable dt)
        {
            CashierReportDetails Details = new CashierReportDetails();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                Details.TerminalNo = dr["TerminalNo"].ToString();
                Details.CashierID = Int64.Parse(dr["CashierID"].ToString());
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
                Details.VATExempt = decimal.Parse(dr["VATExempt"].ToString());
                Details.NonVATableAmount = decimal.Parse(dr["NonVATableAmount"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.VATableAmount = decimal.Parse(dr["VATableAmount"].ToString());
                Details.ZeroRatedSales = decimal.Parse(dr["ZeroRatedSales"].ToString());
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
                Details.CashCount = decimal.Parse(dr["CashCount"].ToString());
                Details.LastLoginDate = DateTime.Parse(dr["LastLoginDate"].ToString());
            }

            return Details;
        }
		#endregion
	
		#region Insert and Updates

		public void UpdateBeginningBalance(Int32 BranchID, string TerminalNo, Int64 CashierID, decimal Amount)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"";
				if (!isExist(BranchID, TerminalNo, CashierID))
				{
					SQL ="INSERT INTO tblCashierReport (" +
						        "CashInDrawer, " +
						        "BeginningBalance, " +
                                "BranchID, " +
                                "TerminalID, " +
                                "TerminalNo, " +
						        "CashierID, " +
						        "LastLoginDate, " +
                                "BeginningTransactionNo, " +
                                "EndingTransactionNo, " +
                                "BeginningORNo, " +
                                "EndingORNo " +
						    " ) VALUES ( " + 
						        "@CashInDrawer, " +
						        "@BeginningBalance, " +
                                "@BranchID, " +
                                "(SELECT TerminalID FROM tblTerminal WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), " +
						        "@TerminalNo, " +
						        "@CashierID, " +
						        "@LastLoginDate, " +
                                "(SELECT BeginningTransactionNo FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), " +
                                "'', " +
                                "(SELECT BeginningORNo FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), " +
                                "'' " +
						    ");";
				}
				else
				{
					SQL ="UPDATE tblCashierReport SET " +
						    //							"GrossSales			= GrossSales + @GrossSales, " +
						    "CashInDrawer		= CashInDrawer + @CashInDrawer, " +
						    "BeginningBalance	= BeginningBalance + @BeginningBalance, " +
						    "LastLoginDate		= @LastLoginDate " +
						"WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				}
				
				cmd.Parameters.AddWithValue("CashInDrawer", Amount);
                cmd.Parameters.AddWithValue("BeginningBalance", Amount);
                cmd.Parameters.AddWithValue("LastLoginDate", DateTime.Now);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                SQL = "UPDATE tblCashierReport SET SyncID = CashierReportID " +
                      "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID AND SyncID = 0;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void UpdateTransactionSales(CashierReportDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL procCashierReportUpdateTransactionSales(@BranchID, @TerminalNo,@CashierID," +
                                    "@NetSales, " +
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
                                    "@VATExempt, " +
                                    "@NonVATableAmount, " +
                                    "@VATableAmount, " +
                                    "@ZeroRatedSales, " +
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

                cmd.Parameters.AddWithValue("NetSales", Details.NetSales);
				cmd.Parameters.AddWithValue("GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("TotalDiscount", Details.TotalDiscount);
                cmd.Parameters.AddWithValue("SNRDiscount", Details.SNRDiscount);
                cmd.Parameters.AddWithValue("PWDDiscount", Details.PWDDiscount);
                cmd.Parameters.AddWithValue("OtherDiscount", Details.OtherDiscount);
                cmd.Parameters.AddWithValue("TotalCharge", Details.TotalCharge);
                cmd.Parameters.AddWithValue("DailySales", Details.DailySales);
                cmd.Parameters.AddWithValue("ItemSold", Details.ItemSold);
                cmd.Parameters.AddWithValue("QuantitySold", Details.QuantitySold);
                cmd.Parameters.AddWithValue("GroupSales", Details.GroupSales);
                
                cmd.Parameters.AddWithValue("VATExempt", Details.VATExempt);
                cmd.Parameters.AddWithValue("NonVATableAmount", Details.NonVATableAmount);
                cmd.Parameters.AddWithValue("VATableAmount", Details.VATableAmount);
                cmd.Parameters.AddWithValue("ZeroRatedSales", Details.ZeroRatedSales);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVATableAmount", Details.EVATableAmount);
                cmd.Parameters.AddWithValue("NonEVATableAmount", Details.NonEVATableAmount);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                
                cmd.Parameters.AddWithValue("CashSales", Details.CashSales);
				cmd.Parameters.AddWithValue("ChequeSales", Details.ChequeSales);
				cmd.Parameters.AddWithValue("CreditCardSales", Details.CreditCardSales);
                cmd.Parameters.AddWithValue("CreditSales", Details.CreditSales);
                cmd.Parameters.AddWithValue("CreditPayment", Details.CreditPayment);
                cmd.Parameters.AddWithValue("CreditPaymentCash", Details.CreditPaymentCash);
                cmd.Parameters.AddWithValue("CreditPaymentCheque", Details.CreditPaymentCheque);
                cmd.Parameters.AddWithValue("CreditPaymentCreditCard", Details.CreditPaymentCreditCard);
                cmd.Parameters.AddWithValue("CreditPaymentDebit", Details.CreditPaymentDebit);
                cmd.Parameters.AddWithValue("DebitPayment", Details.DebitPayment);
                cmd.Parameters.AddWithValue("RewardPointsPayment", Details.RewardPointsPayment);
                cmd.Parameters.AddWithValue("RewardConvertedPayment", Details.RewardConvertedPayment);
                cmd.Parameters.AddWithValue("CashInDrawer", Details.CashInDrawer == 0 ? Details.CashSales : Details.CashInDrawer);
                cmd.Parameters.AddWithValue("VoidSales", Details.VoidSales);
                cmd.Parameters.AddWithValue("RefundSales", Details.RefundSales);
                cmd.Parameters.AddWithValue("ItemsDiscount", Details.ItemsDiscount);
                cmd.Parameters.AddWithValue("SNRItemsDiscount", Details.SNRItemsDiscount);
                cmd.Parameters.AddWithValue("PWDItemsDiscount", Details.PWDItemsDiscount);
                cmd.Parameters.AddWithValue("OtherItemsDiscount", Details.OtherItemsDiscount);
                cmd.Parameters.AddWithValue("SubTotalDiscount", Details.SubTotalDiscount);
                cmd.Parameters.AddWithValue("NoOfCashTransactions", Details.NoOfCashTransactions);
                cmd.Parameters.AddWithValue("NoOfChequeTransactions", Details.NoOfChequeTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditCardTransactions", Details.NoOfCreditCardTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditTransactions", Details.NoOfCreditTransactions);
                cmd.Parameters.AddWithValue("NoOfCombinationPaymentTransactions", Details.NoOfCombinationPaymentTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditPaymentTransactions", Details.NoOfCreditPaymentTransactions);
                cmd.Parameters.AddWithValue("NoOfDebitPaymentTransactions", Details.NoOfDebitPaymentTransactions);
                cmd.Parameters.AddWithValue("NoOfClosedTransactions", Details.NoOfClosedTransactions);
                cmd.Parameters.AddWithValue("NoOfRefundTransactions", Details.NoOfRefundTransactions);
                cmd.Parameters.AddWithValue("NoOfVoidTransactions", Details.NoOfVoidTransactions);
                cmd.Parameters.AddWithValue("NoOfRewardPointsPayment", Details.NoOfRewardPointsPayment);
                cmd.Parameters.AddWithValue("NoOfTotalTransactions", Details.NoOfTotalTransactions);
                cmd.Parameters.AddWithValue("NoOfDiscountedTransactions", Details.NoOfDiscountedTransactions);
                cmd.Parameters.AddWithValue("NegativeAdjustments", Details.NegativeAdjustments);
                cmd.Parameters.AddWithValue("NoOfNegativeAdjustmentTransactions", Details.NoOfNegativeAdjustmentTransactions);
                cmd.Parameters.AddWithValue("PromotionalItems", Details.PromotionalItems);
                cmd.Parameters.AddWithValue("CreditSalesTax", Details.CreditSalesTax);
                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CashWithHold						= CashWithHold + @CashWithHold, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					cmd.Parameters.AddWithValue("CashWithHold", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "ChequeWithHold						= ChequeWithHold + @ChequeWithHold " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("ChequeWithHold", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CreditCardWithHold					= CreditCardWithHold + @CreditCardWithHold " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("CreditCardWithHold", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CashDisburse						= CashDisburse + @CashDisburse, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("CashDisburse", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "ChequeDisburse						= ChequeDisburse + @ChequeDisburse " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("ChequeDisburse", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CreditCardDisburse					= CreditCardDisburse + @CreditCardDisburse " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("CreditCardDisburse", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CashPaidOut						= CashPaidOut + @CashPaidOut, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("CashPaidOut", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "ChequePaidOut						= ChequePaidOut + @ChequePaidOut " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("ChequePaidOut", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CreditCardPaidOut					= CreditCardPaidOut + @CreditCardPaidOut " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                    cmd.Parameters.AddWithValue("CreditCardPaidOut", Details.Amount);
				}

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdateCashCount(Int32 BranchID, Int64 CashierID, string TerminalNo, decimal Amount)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblCashierReport SET " +
					            "CashCount	= @CashCount " +
					        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                cmd.Parameters.AddWithValue("CashCount", Amount);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CashDeposit						= CashDeposit + @CashDeposit, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					cmd.Parameters.AddWithValue("CashDeposit", Details.Amount);
                    cmd.Parameters.AddWithValue("CashInDrawer", Details.Amount);

				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "ChequeDeposit						= ChequeDeposit + @ChequeDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					cmd.Parameters.AddWithValue("ChequeDeposit", Details.Amount);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL= "UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CreditCardDeposit					= CreditCardDeposit + @CreditCardDeposit " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					cmd.Parameters.AddWithValue("CreditCardDeposit", Details.Amount);
				}
                else if (Details.PaymentType == PaymentTypes.Debit)
                {
                    SQL = "UPDATE tblCashierReport SET " +
                            "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
                            "DebitDeposit					    = DebitDeposit + @DebitDeposit " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                    cmd.Parameters.AddWithValue("DebitDeposit", Details.Amount);
                }

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(CashierReportDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveCashierReport(@BranchID, @TerminalNo, @SyncID, @CashierReportID, @CashierID, @TerminalID, @NetSales, @GrossSales, " +
                                        "@TotalDiscount, @SNRDiscount, @PWDDiscount, @OtherDiscount, @DailySales," +
                                        "@ItemSold, @QuantitySold, @GroupSales, @VAT, @EVAT, @LocalTax, @CashSales, @ChequeSales, @CreditCardSales," +
                                        "@CreditSales, @CreditPayment, @CashInDrawer, @TotalDisburse, @CashDisburse, @ChequeDisburse," +
                                        "@CreditCardDisburse, @TotalWithhold, @CashWithhold, @ChequeWithhold, @CreditCardWithhold," +
                                        "@TotalPaidOut, @CashPaidOut, @ChequePaidOut, @CreditCardPaidOut, @BeginningBalance," +
                                        "@VoidSales, @RefundSales, @ItemsDiscount, @SNRItemsDiscount, @PWDItemsDiscount, @OtherItemsDiscount, @SubtotalDiscount, @NoOfCashTransactions," +
                                        "@NoOfChequeTransactions, @NoOfCreditCardTransactions, @NoOfCreditTransactions, " +
                                        "@NoOfCombinationPaymentTransactions, @NoOfCreditPaymentTransactions, @NoOfClosedTransactions," +
                                        "@NoOfRefundTransactions, @NoOfVoidTransactions, @NoOfTotalTransactions, @CashCount, " +
                                        "@LastLoginDate, @TotalDeposit, @CashDeposit, @ChequeDeposit, @CreditCardDeposit, " +
                                        "@DebitPayment, @NoOfDebitPaymentTransactions, @TotalCharge, @IsCashCountInitialized," +
                                        "@NoOfDiscountedTransactions, @NegativeAdjustments, @NoOfNegativeAdjustmentTransactions," +
                                        "@PromotionalItems, @CreditSalesTax, @DebitDeposit, @RewardPointsPayment, @RewardConvertedPayment," +
                                        "@NoOfRewardPointsPayment, @CreditPaymentCash, @CreditPaymentCheque," +
                                        "@CreditPaymentCreditCard, @CreditPaymentDebit, " +
                                        "@RefundCash, @RefundCheque, @RefundCreditCard, @RefundCredit, @RefundDebit, " +
                                        "@CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("CashierReportID", Details.CashierReportID);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("TerminalID", Details.TerminalID);
                cmd.Parameters.AddWithValue("NetSales", Details.NetSales);
                cmd.Parameters.AddWithValue("GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("TotalDiscount", Details.TotalDiscount);
                cmd.Parameters.AddWithValue("SNRDiscount", Details.SNRDiscount);
                cmd.Parameters.AddWithValue("PWDDiscount", Details.PWDDiscount);
                cmd.Parameters.AddWithValue("OtherDiscount", Details.OtherDiscount);
                cmd.Parameters.AddWithValue("DailySales", Details.DailySales);
                cmd.Parameters.AddWithValue("ItemSold", Details.ItemSold);
                cmd.Parameters.AddWithValue("QuantitySold", Details.QuantitySold);
                cmd.Parameters.AddWithValue("GroupSales", Details.GroupSales);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("CashSales", Details.CashSales);
                cmd.Parameters.AddWithValue("ChequeSales", Details.ChequeSales);
                cmd.Parameters.AddWithValue("CreditCardSales", Details.CreditCardSales);
                cmd.Parameters.AddWithValue("CreditSales", Details.CreditSales);
                cmd.Parameters.AddWithValue("CreditPayment", Details.CreditPayment);
                cmd.Parameters.AddWithValue("CashInDrawer", Details.CashInDrawer);
                cmd.Parameters.AddWithValue("TotalDisburse", Details.TotalDisburse);
                cmd.Parameters.AddWithValue("CashDisburse", Details.CashDisburse);
                cmd.Parameters.AddWithValue("ChequeDisburse", Details.ChequeDisburse);
                cmd.Parameters.AddWithValue("CreditCardDisburse", Details.CreditCardDisburse);
                cmd.Parameters.AddWithValue("TotalWithhold", Details.TotalWithHold);
                cmd.Parameters.AddWithValue("CashWithhold", Details.CashWithHold);
                cmd.Parameters.AddWithValue("ChequeWithhold", Details.ChequeWithHold);
                cmd.Parameters.AddWithValue("CreditCardWithhold", Details.CreditCardWithHold);
                cmd.Parameters.AddWithValue("TotalPaidOut", Details.TotalPaidOut);
                cmd.Parameters.AddWithValue("CashPaidOut", Details.CashPaidOut);
                cmd.Parameters.AddWithValue("ChequePaidOut", Details.ChequePaidOut);
                cmd.Parameters.AddWithValue("CreditCardPaidOut", Details.CreditCardPaidOut);
                cmd.Parameters.AddWithValue("BeginningBalance", Details.BeginningBalance);
                cmd.Parameters.AddWithValue("VoidSales", Details.VoidSales);
                cmd.Parameters.AddWithValue("RefundSales", Details.RefundSales);
                cmd.Parameters.AddWithValue("ItemsDiscount", Details.ItemsDiscount);
                cmd.Parameters.AddWithValue("SNRItemsDiscount", Details.SNRItemsDiscount);
                cmd.Parameters.AddWithValue("PWDItemsDiscount", Details.PWDItemsDiscount);
                cmd.Parameters.AddWithValue("OtherItemsDiscount", Details.OtherItemsDiscount);
                cmd.Parameters.AddWithValue("SubtotalDiscount", Details.SubTotalDiscount);
                cmd.Parameters.AddWithValue("NoOfCashTransactions", Details.NoOfCashTransactions);
                cmd.Parameters.AddWithValue("NoOfChequeTransactions", Details.NoOfChequeTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditCardTransactions", Details.NoOfCreditCardTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditTransactions", Details.NoOfCreditTransactions);
                cmd.Parameters.AddWithValue("NoOfCombinationPaymentTransactions", Details.NoOfCombinationPaymentTransactions);
                cmd.Parameters.AddWithValue("NoOfCreditPaymentTransactions", Details.NoOfCreditPaymentTransactions);
                cmd.Parameters.AddWithValue("NoOfClosedTransactions", Details.NoOfClosedTransactions);
                cmd.Parameters.AddWithValue("NoOfRefundTransactions", Details.NoOfRefundTransactions);
                cmd.Parameters.AddWithValue("NoOfVoidTransactions", Details.NoOfVoidTransactions);
                cmd.Parameters.AddWithValue("NoOfTotalTransactions", Details.NoOfTotalTransactions);
                cmd.Parameters.AddWithValue("CashCount", Details.CashCount);
                cmd.Parameters.AddWithValue("LastLoginDate", Details.LastLoginDate);
                cmd.Parameters.AddWithValue("TotalDeposit", Details.TotalDeposit);
                cmd.Parameters.AddWithValue("CashDeposit", Details.CashDeposit);
                cmd.Parameters.AddWithValue("ChequeDeposit", Details.ChequeDeposit);
                cmd.Parameters.AddWithValue("CreditCardDeposit", Details.CreditCardDeposit);
                cmd.Parameters.AddWithValue("DebitPayment", Details.DebitPayment);
                cmd.Parameters.AddWithValue("NoOfDebitPaymentTransactions", Details.NoOfDebitPaymentTransactions);
                cmd.Parameters.AddWithValue("TotalCharge", Details.TotalCharge);
                cmd.Parameters.AddWithValue("IsCashCountInitialized", Details.IsCashCountInitialized);
                cmd.Parameters.AddWithValue("NoOfDiscountedTransactions", Details.NoOfDiscountedTransactions);
                cmd.Parameters.AddWithValue("NegativeAdjustments", Details.NegativeAdjustments);
                cmd.Parameters.AddWithValue("NoOfNegativeAdjustmentTransactions", Details.NoOfNegativeAdjustmentTransactions);
                cmd.Parameters.AddWithValue("PromotionalItems", Details.PromotionalItems);
                cmd.Parameters.AddWithValue("CreditSalesTax", Details.CreditSalesTax);
                cmd.Parameters.AddWithValue("DebitDeposit", Details.DebitDeposit);
                cmd.Parameters.AddWithValue("RewardPointsPayment", Details.RewardPointsPayment);
                cmd.Parameters.AddWithValue("RewardConvertedPayment", Details.RewardConvertedPayment);
                cmd.Parameters.AddWithValue("NoOfRewardPointsPayment", Details.NoOfRewardPointsPayment);
                cmd.Parameters.AddWithValue("CreditPaymentCash", Details.CreditPaymentCash);
                cmd.Parameters.AddWithValue("CreditPaymentCheque", Details.CreditPaymentCheque);
                cmd.Parameters.AddWithValue("CreditPaymentCreditCard", Details.CreditPaymentCreditCard);
                cmd.Parameters.AddWithValue("CreditPaymentDebit", Details.CreditPaymentDebit);
                cmd.Parameters.AddWithValue("RefundCash", Details.RefundCash);
                cmd.Parameters.AddWithValue("RefundCheque", Details.RefundCheque);
                cmd.Parameters.AddWithValue("RefundCreditCard", Details.RefundCreditCard);
                cmd.Parameters.AddWithValue("RefundCredit", Details.RefundCredit);
                cmd.Parameters.AddWithValue("RefundDebit", Details.RefundDebit);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Private Methods

		private bool isExist(Int32 BranchID, string TerminalNo, Int64 CashierID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL="SELECT CashierID FROM tblCashierReport " +
                           "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				bool boRetValue = false;

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					boRetValue = true;
				}

				return boRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Public Methods

		public bool IsBeginningBalanceInitialized(Int32 BranchID, string TerminalNo, Int64 CashierID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"SELECT " +
					            "BeginningBalance " +
					        "FROM tblCashierReport " +
					        "WHERE CashierID = @CashierID " +
                                "AND BranchID = @BranchID " +
					            "AND TerminalNo = @TerminalNo " +
					            "AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT((SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), '%Y-%m-%d %H:%i') " +
					        "ORDER BY LastLoginDate DESC LIMIT 1 ";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                bool boRetValue = false;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boRetValue = true;
                }

                return boRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public bool IsDisburseAmountValid(DisburseDetails Details)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"";

				if (Details.PaymentType == PaymentTypes.Cash)
				{
                    SQL = "SELECT CashInDrawer FROM tblCashierReport " +
                          "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND CashInDrawer >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
                    SQL = "SELECT ChequeWithHold FROM tblCashierReport " +
                          "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND ChequeWithHold >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL	= "SELECT CreditCardWithHold FROM tblCashierReport " +
						  "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND CreditCardWithHold >= @Amount;";
				}

                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                bool boRetValue = false;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boRetValue = true;
                }

				return boRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public bool IsPaidOutAmountValid(PaidOutDetails Details)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"";

				if (Details.PaymentType == PaymentTypes.Cash)
				{
                    SQL = "SELECT CashInDrawer FROM tblCashierReport " +
                          "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND CashInDrawer >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
                    SQL = "SELECT ChequeWithHold FROM tblCashierReport " +
                          "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND ChequeWithHold >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
                    SQL = "SELECT CreditCardWithHold FROM tblCashierReport " +
                          "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID " +
                          "AND CreditCardWithHold >= @Amount;";
				}

                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                bool boRetValue = false;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boRetValue = true;
                }

                return boRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

        private System.Data.DataTable PLUReport(Int32 BranchID, string TerminalNo, string CashierName, bool isPerGroup = false)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                    "a.ProductID, IFNULL(CONCAT(ProductCode, '-',NULLIF(MatrixDescription,'')), ProductCode) AS ProductCode, OrderSlipPrinter, ProductGroup, " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Quantity, a.Quantity))) 'Quantity', " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Amount, a.Amount))) 'Amount' " +
                                "FROM tblTransactionItems a " +
                                "INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID " +
                                "WHERE BranchID = @BranchID " +
                                    "AND TerminalNo = @TerminalNo " +
                                    "AND CashierName = @CashierName " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                        "OR TransactionStatus = @TransactionStatusReprinted " +
                                        "OR TransactionStatus = @TransactionStatusRefund) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo) " +
                                    "GROUP BY OrderSlipPrinter, IFNULL(CONCAT(ProductCode, '-',NULLIF(MatrixDescription,'')), ProductCode) ORDER BY OrderSlipPrinter, ProductCode ASC, ProductGroup";

                if (isPerGroup)
                {
                    SQL = "SELECT " +
                                    "0 AS ProductID, '' AS ProductCode, ProductGroup, OrderSlipPrinter, " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Quantity, a.Quantity))) 'Quantity', " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Amount, a.Amount))) 'Amount' " +
                                "FROM tblTransactionItems a " +
                                "INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID " +
                                "WHERE BranchID = @BranchID " +
                                    "AND TerminalNo = @TerminalNo " +
                                    "AND CashierName = @CashierName " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                        "OR TransactionStatus = @TransactionStatusReprinted " +
                                        "OR TransactionStatus = @TransactionStatusRefund) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo) " +
                                    "GROUP BY OrderSlipPrinter, ProductGroup ORDER BY OrderSlipPrinter, ProductGroup ASC ";
                }

				cmd.Parameters.AddWithValue("@VoidStatus", (Int16) TransactionItemStatus.Void);
                cmd.Parameters.AddWithValue("@ReturnStatus", TransactionItemStatus.Return.ToString("d"));
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CashierName", CashierName);
                cmd.Parameters.AddWithValue("@TransactionStatusClosed", (Int16)TransactionStatus.Closed);
                cmd.Parameters.AddWithValue("@TransactionStatusVoid", (Int16)TransactionStatus.Void);
                cmd.Parameters.AddWithValue("@TransactionStatusReprinted", (Int16)TransactionStatus.Reprinted);
                cmd.Parameters.AddWithValue("@TransactionStatusRefund", (Int16)TransactionStatus.Refund);

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

        public void GeneratePLUReport(Int32 BranchID, string TerminalNo, bool isPerGroup, string CashierName)
		{
			try
			{
                System.Data.DataTable dtPLUReport = this.PLUReport(BranchID, TerminalNo, CashierName, isPerGroup);
				
				Data.PLUReport clsPLUReport = new Data.PLUReport(base.Connection, base.Transaction);

				clsPLUReport.Delete(BranchID, TerminalNo);

				PLUReportDetails clsPLUReportDetails;

                ProductComposition clsProductComposition = new ProductComposition(base.Connection, base.Transaction);

				foreach(System.Data.DataRow dr in dtPLUReport.Rows)
				{
					long lProductID = Convert.ToInt64(dr["ProductID"]);
					string stProductCode = dr["ProductCode"].ToString();
                    string stProductGroup = dr["ProductGroup"].ToString();
					decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
					decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                    OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    clsPLUReportDetails = new PLUReportDetails();
                    clsPLUReportDetails.BranchDetails = new BranchDetails {
                        BranchID = BranchID
                    };
                    clsPLUReportDetails.TerminalNo = TerminalNo;
                    clsPLUReportDetails.ProductID = lProductID;
                    clsPLUReportDetails.ProductCode = stProductCode;
                    clsPLUReportDetails.ProductGroup = stProductGroup;
                    clsPLUReportDetails.Quantity = decQuantity;
                    clsPLUReportDetails.Amount = decAmount;
                    clsPLUReportDetails.OrderSlipPrinter = locOrderSlipPrinter;

                    clsPLUReport.Insert(clsPLUReportDetails);

                    // generate if per item
                    if (!isPerGroup) clsProductComposition.GeneratePLUReport(BranchID, TerminalNo, lProductID, stProductCode, decQuantity);
				}		

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

	}
}