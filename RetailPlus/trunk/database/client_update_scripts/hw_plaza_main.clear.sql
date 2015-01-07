SET FOREIGN_KEY_CHECKS = 0;

DELETE FROM tblCashPayment WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblChequePayment WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblCreditCardPayment WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblCreditPayment WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblDebitPayment WHERE TerminalNo >= 2 AND TerminalNo <= 100;

DELETE FROM tblCreditPaymentCash WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblCreditPaymentCheque WHERE TerminalNo >= 2 AND TerminalNo <= 100;

DELETE FROM tblCashierLogs WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblPaidOut WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblDisburse WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblWithHold WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblDeposit WHERE TerminalNo >= 2 AND TerminalNo <= 100;

DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo >= 2 AND TerminalNo <= 100);
DELETE FROM tblTransactions WHERE TerminalNo >= 2 AND TerminalNo <= 100;

DELETE FROM tblTerminalReportHistory WHERE TerminalNo >= 2 AND TerminalNo <= 100;
DELETE FROM tblCashierReportHistory WHERE TerminalNo >= 2 AND TerminalNo <= 100;

UPDATE tblTerminalReport SET 
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
					SNRItemsDiscount					=  0, 
					PWDItemsDiscount					=  0, 
					OtherItemsDiscount					=  0, 
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
					DateLastInitialized					=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL 0 DAY), '%Y-%m-%d 00:00:01'),
					CreatedOn							=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL 0 DAY), '%Y-%m-%d 00:00:01'),
					LastModified						=  DATE_FORMAT(DATE_ADD(NOW(), INTERVAL 0 DAY), '%Y-%m-%d 00:00:01')
WHERE TerminalNo >= 2 AND TerminalNo <= 100;

DELETE FROM sysAuditTrail WHERE TerminalNo >= 2 AND TerminalNo <= 100;

SET FOREIGN_KEY_CHECKS = 1;
