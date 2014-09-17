﻿UPDATE tblTerminalReport SET 
					BeginningTransactionNo				=  '00000000000000', 
					EndingTransactionNo					=  '00000000000001', 
					BeginningORNo						=  '00000000000000', 
					EndingORNo							=  '00000000000001',
					OldGrandTotal						=  0,
					NewGrandTotal						=  0,
					ActualOldGrandTotal					=  0,
					ActualNewGrandTotal					=  0,
					ZReadCount							=  1,
					NetSales							=  0, 
					GrossSales							=  0, 
					TotalDiscount						=  0, 
					SNRDiscount					  		=  0, 
					PWDDiscount					  		=  0, 
					OtherDiscount					  	=  0, 
					TotalCharge							=  0, 
					DailySales							=  0, 
					QuantitySold						=  0, 
					GroupSales							=  0, 
					VATExempt   						=  0, 
					NonVATableAmount					=  0, 
					VATableAmount						=  0, 
					VAT									=  0, 
					EVATableAmount						=  0, 
					NonEVATableAmount					=  0, 
					EVAT								=  0, 
					LocalTax							=  0, 
					CashSales							=  0, 
					ChequeSales							=  0, 
					CreditCardSales						=  0, 
					CreditSales							=  0, 
					CreditPayment						=  0, 
					CreditPaymentCash					=  0, 
					CreditPaymentCheque					=  0, 
					CreditPaymentCreditCard				=  0, 
					CreditPaymentDebit					=  0, 
					DebitPayment						=  0, 
					RewardPointsPayment					=  0,
					RewardConvertedPayment				=  0,
					CashInDrawer						=  0, 
					TotalDisburse						=  0, 
					CashDisburse						=  0, 
					ChequeDisburse						=  0, 
					CreditCardDisburse					=  0, 
					TotalWithhold						=  0, 
					CashWithhold						=  0, 
					ChequeWithhold						=  0, 
					CreditCardWithhold					=  0, 
					TotalPaidOut						=  0, 
					CashPaidOut							=  0,
					ChequePaidOut						=  0,
					CreditCardPaidOut					=  0,
					TotalDeposit						=  0, 
					CashDeposit							=  0, 
					ChequeDeposit						=  0, 
					CreditCardDeposit					=  0, 
					DebitDeposit						=  0, 
					BeginningBalance					=  0, 
					VoidSales							=  0, 
					RefundSales							=  0, 
					ItemsDiscount						=  0, 
					SubTotalDiscount					=  0, 
					NoOfCashTransactions				=  0, 
					NoOfChequeTransactions				=  0, 
					NoOfCreditCardTransactions			=  0, 
					NoOfCreditTransactions				=  0, 
					NoOfCombinationPaymentTransactions	=  0, 
					NoOfCreditPaymentTransactions		=  0, 
					NoOfDebitPaymentTransactions		=  0, 
					NoOfClosedTransactions				=  0, 
					NoOfRefundTransactions				=  0, 
					NoOfVoidTransactions				=  0, 
					NoOfRewardPointsPayment				=  0,
					NoOfTotalTransactions				=  0, 
					NoOfDiscountedTransactions			=  0,
					NegativeAdjustments					=  0,
					NoOfNegativeAdjustmentTransactions	=  0,
					PromotionalItems					=  0,
					CreditSalesTax						=  0,
					BatchCounter						=  1,
					NoOfReprintedTransaction			=  0,
					TotalReprintedTransaction			=  0, 
					DateLastInitialized					=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 DAY), '%Y-%m-%d 00:00:01'),
					CreatedOn							=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 DAY), '%Y-%m-%d 00:00:01'),
					LastModified						=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 DAY), '%Y-%m-%d 00:00:01');

UPDATE tblCashierReport SET 
			NetSales							=  0, 
			GrossSales							=  0, 
			TotalDiscount						=  0,
			SNRDiscount					  		=  0, 
			PWDDiscount					  		=  0, 
			OtherDiscount					  	=  0, 
			TotalCharge							=  0, 
			DailySales							=  0, 
			QuantitySold						=  0, 
			GroupSales							=  0, 
			VATExempt   						=  0, 
			NonVATableAmount					=  0, 
			VATableAmount						=  0, 
			VAT									=  0, 
			EVATableAmount						=  0, 
			NonEVATableAmount					=  0, 
			EVAT								=  0, 
			LocalTax							=  0, 
			CashSales							=  0, 
			ChequeSales							=  0, 
			CreditCardSales						=  0, 
			CreditSales							=  0, 
			CreditPayment						=  0, 
			CreditPaymentCash					=  0, 
			CreditPaymentCheque					=  0, 
			CreditPaymentCreditCard				=  0, 
			CreditPaymentDebit					=  0, 
			DebitPayment						=  0, 
			RewardPointsPayment					=  0,
			RewardConvertedPayment				=  0,
			CashInDrawer						=  0, 
			TotalDisburse						=  0, 
			CashDisburse						=  0, 
			ChequeDisburse						=  0, 
			CreditCardDisburse					=  0, 
			TotalWithhold						=  0, 
			CashWithhold						=  0, 
			ChequeWithhold						=  0, 
			CreditCardWithhold					=  0, 
			TotalPaidOut						=  0, 
			CashPaidOut							=  0,
			ChequePaidOut						=  0,
			CreditCardPaidOut					=  0,
			TotalDeposit						=  0, 
			CashDeposit							=  0, 
			ChequeDeposit						=  0, 
			CreditCardDeposit					=  0, 
			DebitDeposit						=  0, 
			BeginningBalance					=  0, 
			VoidSales							=  0, 
			RefundSales							=  0, 
			ItemsDiscount						=  0, 
			SubTotalDiscount					=  0, 
			NoOfCashTransactions				=  0, 
			NoOfChequeTransactions				=  0, 
			NoOfCreditCardTransactions			=  0, 
			NoOfCreditTransactions				=  0, 
			NoOfCombinationPaymentTransactions	=  0, 
			NoOfCreditPaymentTransactions		=  0, 
			NoOfDebitPaymentTransactions		=  0, 
			NoOfClosedTransactions				=  0, 
			NoOfRefundTransactions				=  0, 
			NoOfVoidTransactions				=  0, 
			NoOfRewardPointsPayment				=  0,
			NoOfTotalTransactions				=  0, 
			NoOfDiscountedTransactions			=  0,
			NegativeAdjustments					=  0,
			NoOfNegativeAdjustmentTransactions	=  0,
			PromotionalItems					=  0,
			CreditSalesTax						=  0,
			CashCount							=  0 ,
			CreatedOn							=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 DAY), '%Y-%m-%d 00:00:01'),
			LastModified						=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 DAY), '%Y-%m-%d 00:00:01');

TRUNCATE TABLE tblTerminalReportHistory;
TRUNCATE TABLE tblCashierReportHistory;
TRUNCATE TABLE sysAuditTrail;
TRUNCATE TABLE tblTransactionItems;
TRUNCATE TABLE tblTransactions;
