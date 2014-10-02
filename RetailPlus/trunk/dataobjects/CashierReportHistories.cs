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
	public class CashierReportHistories : POSConnection
	{
		
		#region Constructors & Destructors

		public CashierReportHistories()
            : base(null, null)
        {
        }

        public CashierReportHistories(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Details

        public CashierReportDetails Details(Int64 CashierID, int BranchID, string TerminalNo)
		{
			try
			{
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
								"QuantitySold, " +
								"GroupSales, " +
                                "VATExempt, " +
                                "ZeroRatedVAT, " +
                                "NonVATableAmount, " +
                                "VAT, " +
                                "VATableAmount, " +
								"EVAT, " +
								"LocalTax, " +
								"CashSales, " +
								"ChequeSales, " +
								"CreditCardSales, " +
								"CreditSales, " +
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
							"FROM tblCashierReportHistory " +
							"WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				  
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CashierReportDetails Details = CashierReports.SetDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	
		#region Insert and Updates

        public Int32 Save(CashierReportDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveCashierReportHistory(@BranchID, @TerminalNo, @SyncID, @CashierReportHistoryID, @CashierID, @TerminalID, @NetSales, @GrossSales, @TotalDiscount, @DailySales," +
                                        "@QuantitySold, @GroupSales, @VAT, @EVAT, @LocalTax, @CashSales, @ChequeSales, @CreditCardSales," +
                                        "@CreditSales, @CreditPayment, @CashInDrawer, @TotalDisburse, @CashDisburse, @ChequeDisburse," +
                                        "@CreditCardDisburse, @TotalWithhold, @CashWithhold, @ChequeWithhold, @CreditCardWithhold," +
                                        "@TotalPaidOut, @CashPaidOut, @ChequePaidOut, @CreditCardPaidOut, @BeginningBalance," +
                                        "@VoidSales, @RefundSales, @ItemsDiscount, @SubtotalDiscount, @NoOfCashTransactions," +
                                        "@NoOfChequeTransactions, @NoOfCreditCardTransactions, @NoOfCreditTransactions, " +
                                        "@NoOfCombinationPaymentTransactions, @NoOfCreditPaymentTransactions, @NoOfClosedTransactions," +
                                        "@NoOfRefundTransactions, @NoOfVoidTransactions, @NoOfTotalTransactions, @CashCount, " +
                                        "@LastLoginDate, @TotalDeposit, @CashDeposit, @ChequeDeposit, @CreditCardDeposit, " +
                                        "@DebitPayment, @NoOfDebitPaymentTransactions, @TotalCharge, @IsCashCountInitialized," +
                                        "@NoOfDiscountedTransactions, @NegativeAdjustments, @NoOfNegativeAdjustmentTransactions," +
                                        "@PromotionalItems, @CreditSalesTax, @DebitDeposit, @RewardPointsPayment, @RewardConvertedPayment," +
                                        "@NoOfRewardPointsPayment, @CreditPaymentCash, @CreditPaymentCheque," +
                                        "@CreditPaymentCreditCard, @CreditPaymentDebit, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("CashierReportHistoryID", Details.CashierReportHistoryID);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("TerminalID", Details.TerminalID);
                cmd.Parameters.AddWithValue("NetSales", Details.NetSales);
                cmd.Parameters.AddWithValue("GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("TotalDiscount", Details.TotalDiscount);
                cmd.Parameters.AddWithValue("DailySales", Details.DailySales);
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


		#endregion

		#region Public Methods


		#endregion

	}
}