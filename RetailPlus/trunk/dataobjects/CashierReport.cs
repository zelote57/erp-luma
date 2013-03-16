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
		public Int64   CashierID;
        public int BranchID;
		public string  TerminalNo;
		public decimal GrossSales;
		public decimal TotalDiscount;
		public decimal TotalCharge;
		public decimal DailySales;
		public decimal QuantitySold;
		public decimal GroupSales;
		public decimal VAT;
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

        /**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
        public decimal RewardPointsPayment;
        public decimal RewardConvertedPayment;
        public Int32 NoOfRewardPointsPayment;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CashierReport : POSConnection
	{
		
		#region Constructors & Destructors

		public CashierReport()
            : base(null, null)
        {
        }

        public CashierReport(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Details

		public CashierReportDetails Details(Int64 CashierID, int BranchID, string TerminalNo)
		{
			try
			{
				string SQL=	"SELECT " +
								"GrossSales, " +
								"TotalDiscount, " +
								"TotalCharge, " +
								"DailySales, " +
								"QuantitySold, " +
								"GroupSales, " +
								"VAT, " +
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
								"CashCount, " +
								"LastLoginDate " +
							"FROM tblCashierReport " +
							"WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				CashierReportDetails Details = new CashierReportDetails();

				while (myReader.Read())
				{
                    Details.BranchID = BranchID;
					Details.TerminalNo = TerminalNo;
					Details.CashierID = CashierID;
					Details.GrossSales = myReader.GetDecimal("GrossSales");
					Details.TotalDiscount = myReader.GetDecimal("TotalDiscount");
					Details.TotalCharge = myReader.GetDecimal("TotalCharge");
					Details.DailySales = myReader.GetDecimal("DailySales");
					Details.QuantitySold = myReader.GetDecimal("QuantitySold");
					Details.GroupSales = myReader.GetDecimal("GroupSales");
					Details.VAT = myReader.GetDecimal("VAT");
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
					Details.CashCount = myReader.GetDecimal("CashCount");
					Details.LastLoginDate = myReader.GetDateTime("LastLoginDate");
				}
                myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	
		#region Updates

		public void UpdateBeginningBalance(int BranchID, decimal Amount, Int64 CashierID)
		{
			try 
			{
				string SQL=	"";
				if (isExist(BranchID, CashierID) == false)
				{
					SQL ="INSERT INTO tblCashierReport (" +
						        "CashInDrawer, " +
						        "BeginningBalance, " +
                                "BranchID, " +
                                "TerminalID, " +
                                "TerminalNo, " +
						        "CashierID, " +
						        "LastLoginDate " +
						    " ) VALUES ( " + 
						        "@CashInDrawer, " +
						        "@BeginningBalance, " +
                                "@BranchID, " +
                                "(SELECT TerminalID FROM tblTerminal WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), " +
						        "@TerminalNo, " +
						        "@CashierID, " +
						        "@LastLoginDate " +
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
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);			
				prmCashInDrawer.Value = Amount;
				cmd.Parameters.Add(prmCashInDrawer);

				MySqlParameter prmBeginningBalance = new MySqlParameter("@BeginningBalance",MySqlDbType.Decimal);			
				prmBeginningBalance.Value = Amount;
				cmd.Parameters.Add(prmBeginningBalance);

				MySqlParameter prmLastLoginDate = new MySqlParameter("@LastLoginDate",MySqlDbType.DateTime);			
				prmLastLoginDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmLastLoginDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = CompanyDetails.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

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
				string SQL = "CALL procCashierReportUpdateTransactionSales(@BranchID, @TerminalNo,@CashierID," +
                                    "@GrossSales, " +
                                    "@TotalDiscount, " +
                                    "@TotalCharge, " +
                                    "@DailySales, " +
                                    "@QuantitySold, " +
                                    "@GroupSales, " +
                                    "@VAT, " +
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

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);

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

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        //public void UpdateTransactionSales(CashierReportDetails Details)
        //{
        //    try
        //    {
        //        string SQL = "UPDATE tblCashierReport SET " +
        //            "GrossSales							=  GrossSales							+  @GrossSales, " +
        //            "TotalDiscount						=  TotalDiscount						+  @TotalDiscount, " +
        //            "TotalCharge						=  TotalCharge							+  @TotalCharge, " +
        //            "DailySales							=  DailySales							+  @DailySales, " +
        //            "QuantitySold						=  QuantitySold							+  @QuantitySold, " +
        //            "GroupSales							=  GroupSales							+  @GroupSales, " +
        //            "VAT								=  VAT									+  @VAT, " +
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
        //            "WHERE TerminalNo = @TerminalNo AND CashierID = @CashierID;";

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

        //        MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
        //        prmVAT.Value = Details.VAT;
        //        cmd.Parameters.Add(prmVAT);

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

        //        MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
        //        prmCashierID.Value = Details.CashierID;
        //        cmd.Parameters.Add(prmCashierID);

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
        // }

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CashWithHold						= CashWithHold + @CashWithHold, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmCashWithHold = new MySqlParameter("@CashWithHold",MySqlDbType.Decimal);
					prmCashWithHold.Value = Details.Amount;
					cmd.Parameters.Add(prmCashWithHold);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "ChequeWithHold						= ChequeWithHold + @ChequeWithHold " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmChequeWithHold = new MySqlParameter("@ChequeWithHold",MySqlDbType.Decimal);	
					prmChequeWithHold.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeWithHold);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalWithHold						= TotalWithHold + @TotalWithHold, " +
						        "CreditCardWithHold					= CreditCardWithHold + @CreditCardWithHold " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

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

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CashDisburse						= CashDisburse + @CashDisburse, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmCashDisburse = new MySqlParameter("@CashDisburse",MySqlDbType.Decimal);
					prmCashDisburse.Value = Details.Amount;
					cmd.Parameters.Add(prmCashDisburse);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "ChequeDisburse						= ChequeDisburse + @ChequeDisburse " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmChequeDisburse = new MySqlParameter("@ChequeDisburse",MySqlDbType.Decimal);	
					prmChequeDisburse.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeDisburse);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDisburse						= TotalDisburse + @TotalDisburse, " +
						        "CreditCardDisburse					= CreditCardDisburse + @CreditCardDisburse " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

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

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CashPaidOut						= CashPaidOut + @CashPaidOut, " +
						        "CashInDrawer						= CashInDrawer - @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmCashPaidOut = new MySqlParameter("@CashPaidOut",MySqlDbType.Decimal);
					prmCashPaidOut.Value = Details.Amount;
					cmd.Parameters.Add(prmCashPaidOut);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "ChequePaidOut						= ChequePaidOut + @ChequePaidOut " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmChequePaidOut = new MySqlParameter("@ChequePaidOut",MySqlDbType.Decimal);	
					prmChequePaidOut.Value = Details.Amount;
					cmd.Parameters.Add(prmChequePaidOut);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalPaidOut						= TotalPaidOut + @TotalPaidOut, " +
						        "CreditCardPaidOut					= CreditCardPaidOut + @CreditCardPaidOut " +
						    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

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

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdateCashCount(int BranchID, Int64 CashierID, string TerminalNo, decimal Amount)
		{
			try 
			{
				string SQL=	"UPDATE tblCashierReport SET " +
					            "CashCount	= @CashCount " +
					        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashCount = new MySqlParameter("@CashCount",MySqlDbType.Decimal);
				prmCashCount.Value = Amount;
				cmd.Parameters.Add(prmCashCount);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

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
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CashDeposit						= CashDeposit + @CashDeposit, " +
						        "CashInDrawer						= CashInDrawer + @CashInDrawer " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmCashDeposit = new MySqlParameter("@CashDeposit",MySqlDbType.Decimal);
					prmCashDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmCashDeposit);

					MySqlParameter prmCashInDrawer = new MySqlParameter("@CashInDrawer",MySqlDbType.Decimal);
					prmCashInDrawer.Value = Details.Amount;
					cmd.Parameters.Add(prmCashInDrawer);

				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL=	"UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "ChequeDeposit						= ChequeDeposit + @ChequeDeposit " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmChequeDeposit = new MySqlParameter("@ChequeDeposit",MySqlDbType.Decimal);	
					prmChequeDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmChequeDeposit);
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL= "UPDATE tblCashierReport SET " +
						        "TotalDeposit						= TotalDeposit + @TotalDeposit, " +
						        "CreditCardDeposit					= CreditCardDeposit + @CreditCardDeposit " +
                        "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

					MySqlParameter prmCreditCardDeposit = new MySqlParameter("@CreditCardDeposit",MySqlDbType.Decimal);	
					prmCreditCardDeposit.Value = Details.Amount;
					cmd.Parameters.Add(prmCreditCardDeposit);
				}
                else if (Details.PaymentType == PaymentTypes.Debit)
                {
                    SQL = "UPDATE tblCashierReport SET " +
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

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Private Methods

		private bool isExist(int BranchID, Int64 CashierID)
		{
			try
			{
				string SQL="SELECT CashierID FROM tblCashierReport " +
                    "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = CompanyDetails.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);			
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				bool boRetValue = false;

				while (myReader.Read())
				{
					boRetValue = true;
				}
                myReader.Close();

				return boRetValue;

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Public Methods

		public bool IsBeginningBalanceInitialized(int BranchID, Int64 CashierID, string TerminalNo)
		{
			try
			{
				bool boRetValue = false;

				string SQL=	"SELECT " +
					            "BeginningBalance " +
					        "FROM tblCashierReport " +
					        "WHERE CashierID = @CashierID " +
                                "AND BranchID = @BranchID " +
					            "AND TerminalNo = @TerminalNo " +
					            "AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT((SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo), '%Y-%m-%d %H:%i') " +
					        "ORDER BY LastLoginDate DESC LIMIT 1 ";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				while (myReader.Read()) 
				{
					boRetValue = true;
				}

				myReader.Close();

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
				bool boRetValue = false;

				string SQL=	"";

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL	=		"SELECT " +
						"CashInDrawer " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND CashInDrawer >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL	=		"SELECT " +
						"ChequeWithHold " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND ChequeWithHold >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL	=		"SELECT " +
						"CreditCardWithHold " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND CreditCardWithHold >= @Amount;";
				}
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				while (myReader.Read()) 
				{
					boRetValue = true;
				}

				myReader.Close();

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
				bool boRetValue = false;

				string SQL=	"";

				if (Details.PaymentType == PaymentTypes.Cash)
				{
					SQL	=		"SELECT " +
						"CashInDrawer " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND CashInDrawer >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.Cheque)
				{
					SQL	=		"SELECT " +
						"ChequeWithHold " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND ChequeWithHold >= @Amount;";
				}
				else if (Details.PaymentType == PaymentTypes.CreditCard)
				{
					SQL	=		"SELECT " +
						"CreditCardWithHold " +
						"FROM tblCashierReport " +
						"WHERE CashierID = @CashierID " +
                        "AND BranchID = @BranchID " +
						"AND TerminalNo = @TerminalNo " +
						"AND CreditCardWithHold >= @Amount;";
				}
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				while (myReader.Read()) 
				{
					boRetValue = true;
				}

				myReader.Close();

				return boRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

		public System.Data.DataTable PLUReport(int BranchID, string CashierName, string TerminalNo)
		{
			try
			{
                string SQL = "SELECT " +
                                    "a.ProductID, IFNULL(CONCAT(ProductCode, '-',NULLIF(MatrixDescription,'')), ProductCode) AS ProductCode, OrderSlipPrinter, " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Quantity, a.Quantity))) 'Quantity', " +
                                    "SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Amount, a.Amount))) 'Amount' " +
                                    "FROM tblTransactionItems a " +
                                    "INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID " +
                                "WHERE BranchID = @BranchID " +
                                    "AND TerminalNo = @TerminalNo " +
                                    "AND CashierName = @CashierName " +
                                    "AND (TransactionStatus = @TransactionStatusClosed " +
                                    "OR TransactionStatus = @TransactionStatusReprinted " +
                                    "OR TransactionStatus = @TransactionStatusRefund " +
                                    "OR TransactionStatus = @TransactionStatusCreditPayment) " +
                                    "AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport " +
                                    "WHERE TerminalNo = @TerminalNo) " +
                                    "GROUP BY OrderSlipPrinter, IFNULL(CONCAT(ProductCode, '-',NULLIF(MatrixDescription,'')), ProductCode) ORDER BY OrderSlipPrinter, ProductCode ASC ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;

				MySqlParameter prmTransactionItemStatusVoid = new MySqlParameter("@VoidStatus",MySqlDbType.Int16);			
				prmTransactionItemStatusVoid.Value = (Int16) TransactionItemStatus.Void;
				cmd.Parameters.Add(prmTransactionItemStatusVoid);

				MySqlParameter prmReturnStatus = new MySqlParameter("@ReturnStatus",MySqlDbType.Int16);			
				prmReturnStatus.Value = TransactionItemStatus.Return.ToString("d");
				cmd.Parameters.Add(prmReturnStatus);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

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

				cmd.CommandText = SQL;

                //System.Data.DataTable dt = new System.Data.DataTable("tblPLUReport");

                //dt.Columns.Add("OrderSlipPrinter");
                //dt.Columns.Add("ProductID");
                //dt.Columns.Add("ProductCode");
                //dt.Columns.Add("Quantity");
                //dt.Columns.Add("Amount");

                //MySqlDataReader myReader = base.ExecuteReader(cmd);
                //while (myReader.Read())
                //{
                //    System.Data.DataRow dr = dt.NewRow();
                //    dr["OrderSlipPrinter"] = myReader["OrderSlipPrinter"].ToString();
                //    dr["ProductID"] = "" + myReader["ProductID"].ToString();
                //    dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
                //    dr["Quantity"] = myReader.GetDecimal("Quantity").ToString("#,##0.#0");
                //    dr["Amount"] = myReader.GetDecimal("Amount").ToString("#,##0.#0");
                //    dt.Rows.Add(dr);
                //}

                //myReader.Close();

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				return dt;		
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		public void GeneratePLUReport(int BranchID, string CashierName, string TerminalNo)
		{
			try
			{
				System.Data.DataTable dtPLUReport = this.PLUReport(BranchID, CashierName, TerminalNo);
				
				Data.PLUReport clsPLUReport = new Data.PLUReport(base.Connection, base.Transaction);

				clsPLUReport.Delete(TerminalNo);

				PLUReportDetails clsPLUReportDetails;

                ProductComposition clsProductComposition = new ProductComposition(base.Connection, base.Transaction);

				foreach(System.Data.DataRow dr in dtPLUReport.Rows)
				{
					long lProductID = Convert.ToInt64(dr["ProductID"]);
					string stProductCode = dr["ProductCode"].ToString();
					decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
					decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                    OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    clsPLUReportDetails = new PLUReportDetails();
                    clsPLUReportDetails.TerminalNo = TerminalNo;
                    clsPLUReportDetails.ProductID = lProductID;
                    clsPLUReportDetails.ProductCode = stProductCode;
                    clsPLUReportDetails.Quantity = decQuantity;
                    clsPLUReportDetails.Amount = decAmount;
                    clsPLUReportDetails.OrderSlipPrinter = locOrderSlipPrinter;

                    clsPLUReport.Insert(clsPLUReportDetails);

                    clsProductComposition.GeneratePLUReport(TerminalNo, lProductID, stProductCode, decQuantity);
				}		

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

	}
}