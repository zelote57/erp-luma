USE POS;

SELECT ProductCode, Quantity, PurchasePrice, PurchaseAmount FROM tblTransactionItems01 WHERE Quantity > 2 order by transactionitemsid desc LIMIT 5 ;

UPDATE tblTransactionItems01 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems02 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems03 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems04 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems05 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems06 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems07 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems08 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems09 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems10 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems11 SET PurchaseAmount = Quantity * PurchasePrice;
UPDATE tblTransactionItems12 SET PurchaseAmount = Quantity * PurchasePrice; 

select * from tblterminalreporthistory where DateLastInitialized > '2009-01-15 10:00'


select * from tblCashierReportHistory where LastLoginDate > '2009-01-15 10:00';

select BeginningTransactionNo,EndingTransactionNo,GrossSales,TotalDiscount,DailySales,GroupSales,CashSales,ChequeSales,CreditCardSales ,CreditSales,CreditPayment,CashInDrawer,DateLastInitialized  from tblterminalreporthistory where DateLastInitialized > '2009-01-15 00:00:00';

select CashierID, GrossSales,TotalDiscount,DailySales,GroupSales,CashSales,ChequeSales,CreditCardSales ,CreditSales,CreditPayment,CashInDrawer,BeginningBalance,cashcount,  LastLoginDate from tblCashierReportHistory where LastLoginDate > '2009-01-15 00:00:00';


update tblterminalreporthistory set trustfund = 35 where DateLastInitialized > '2009-01-15 10:00';
update tblterminal set trustfund = 35;

delete from tblterminalreport;
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-17 11:24:33';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-18 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-19 09:09:09';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-20 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-21 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-22 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-23 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-24 09:14:36';
insert into tblterminalreport select * from tblterminalreporthistory where DateLastInitialized = '2009-01-25 09:14:36';



INSERT INTO tblTerminalReport (TerminalID, 	TerminalNo, 	BeginningTransactionNo, 	EndingTransactionNo, 	ZReadCount, 	XReadCount, 
	GrossSales, 	TotalDiscount, 	TotalCharge, 	DailySales, 	QuantitySold, 	GroupSales, 	OldGrandTotal, 	NewGrandTotal, 	VATableAmount, 
	NonVATableAmount, 	VAT, 	EVATableAmount, 	NonEVATableAmount, 	EVAT, 	LocalTax, 	CashSales, 	ChequeSales, 	CreditCardSales, 
	CreditSales, 	CreditPayment, 	DebitPayment, 	CashInDrawer, 	TotalDisburse, 	CashDisburse, 	ChequeDisburse, 	CreditCardDisburse, 
	TotalWithhold, 	CashWithhold, 	ChequeWithhold, 	CreditCardWithhold, 	TotalPaidOut, 	CashPaidOut, 	ChequePaidOut, 	CreditCardPaidOut, 
	TotalDeposit, 	CashDeposit, 	ChequeDeposit, 	CreditCardDeposit, 	BeginningBalance,	VoidSales, 	RefundSales, 	ItemsDiscount, 
	SubtotalDiscount, 
	NoOfCashTransactions, 
	NoOfChequeTransactions, 
	NoOfCreditCardTransactions, 
	NoOfCreditTransactions, 
	NoOfCombinationPaymentTransactions, 
	NoOfCreditPaymentTransactions, 
	NoOfDebitPaymentTransactions, 
	NoOfClosedTransactions, 
	NoOfRefundTransactions, 
	NoOfVoidTransactions, 
	NoOfTotalTransactions, 
	DateLastInitialized) (SELECT TerminalID, 
	TerminalNo, 
	BeginningTransactionNo, 
	EndingTransactionNo, 
	ZReadCount, 
	XReadCount, 
	GrossSales, 
	TotalDiscount, 
	TotalCharge, 
	DailySales, 
	QuantitySold, 
	GroupSales, 
	OldGrandTotal, 
	NewGrandTotal, 
	VATableAmount, 
	NonVATableAmount, 
	VAT, 
	EVATableAmount, 
	NonEVATableAmount, 
	EVAT, 
	LocalTax, 
	CashSales, 
	ChequeSales, 
	CreditCardSales, 
	CreditSales, 
	CreditPayment, 
	DebitPayment, 
	CashInDrawer, 
	TotalDisburse, 
	CashDisburse, 
	ChequeDisburse, 
	CreditCardDisburse, 
	TotalWithhold, 
	CashWithhold, 
	ChequeWithhold, 
	CreditCardWithhold, 
	TotalPaidOut, 
	CashPaidOut, 
	ChequePaidOut, 
	CreditCardPaidOut, 
	TotalDeposit, 
	CashDeposit, 
	ChequeDeposit, 
	CreditCardDeposit, 
	BeginningBalance, 
	VoidSales, 
	RefundSales, 
	ItemsDiscount, 
	SubtotalDiscount, 
	NoOfCashTransactions, 
	NoOfChequeTransactions, 
	NoOfCreditCardTransactions, 
	NoOfCreditTransactions, 
	NoOfCombinationPaymentTransactions, 
	NoOfCreditPaymentTransactions, 	
	NoOfDebitPaymentTransactions, 	
	NoOfClosedTransactions, 
	NoOfRefundTransactions, 
	NoOfVoidTransactions, 
	NoOfTotalTransactions, 
	DateLastInitialized
FROM tblTerminalReportHistory WHERE DateLastInitialized = '2009-01-17 11:24:33');	