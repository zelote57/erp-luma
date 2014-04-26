SET FOREIGN_KEY_CHECKS = 0;



DELETE FROM tblCashierLogs;
DELETE FROM tblPaidOut;
DELETE FROM tblDisburse;
DELETE FROM tblWithHold;
DELETE FROM tblDeposit;

DELETE FROM tblcashpayment;
DELETE FROM tblchequepayment;
DELETE FROM tblcreditcardpayment;
DELETE FROM tblcreditpayment;
DELETE FROM tbldebitpayment;
DELETE FROM tblpaymentcredit;
DELETE FROM tblpaymentdebit;
DELETE FROM tblpaymentpodetails;
DELETE FROM tblplureport;

DELETE FROM tbldiscounthistory;

DELETE FROM tblPO;
DELETE FROM tblpoitems;

DELETE FROM tblpodebitmemo;
DELETE FROM tblpodebitmemoitems;

DELETE FROM tblTransactionItems;
DELETE FROM tblTransactions;

TRUNCATE TABLE tblterminalreporthistory;

TRUNCATE TABLE tblCashierReport;
TRUNCATE TABLE tblCashierReportHistory;

UPDATE tblTerminalReport SET
	BeginningTransactionNo = '00000000000001', 
	EndingTransactionNo = '00000000000001', 
	ZReadCount = 1,
	XReadCount = 1,
	GrossSales = 0,
	TotalDiscount = 0,
	DailySales = 0,
	QuantitySold = 0,
	GroupSales = 0,
	OldGrandTotal = 0,
	NewGrandTotal = 0,
	VATableAmount = 0,
	NonVATableAmount = 0,
	VAT = 0,
	EVATableAmount = 0,
	NonEVATableAmount = 0,
	EVAT = 0,
	LocalTax = 0,
	CashSales = 0,
	ChequeSales = 0,
	CreditCardSales = 0,
	CreditSales = 0,
	CreditPayment = 0,
	CreditPaymentCash = 0,
	CreditPaymentCheque = 0,
	CreditPaymentCreditCard = 0,
	CreditPaymentDebit = 0,
	CashInDrawer = 0,
	TotalDisburse = 0,
	CashDisburse = 0,
	ChequeDisburse = 0,
	CreditCardDisburse = 0,
	TotalWithhold = 0,
	CashWithhold = 0,
	ChequeWithhold = 0,
	CreditCardWithhold = 0,
	TotalPaidOut = 0,
	CashPaidOut = 0,
	ChequePaidOut = 0,
	CreditCardPaidOut = 0,
	BeginningBalance = 0,
	VoidSales = 0,
	RefundSales = 0,
	ItemsDiscount = 0,
	SubtotalDiscount = 0,
	NoOfCashTransactions = 0,
	NoOfChequeTransactions = 0,
	NoOfCreditCardTransactions = 0,
	NoOfCreditTransactions = 0,
	NoOfCombinationPaymentTransactions = 0,
	NoOfCreditPaymentTransactions = 0,
	NoOfClosedTransactions = 0,
	NoOfRefundTransactions = 0,
	NoOfVoidTransactions = 0,
	NoOfTotalTransactions = 0,
	TotalDeposit = 0,
	CashDeposit = 0,
	ChequeDeposit = 0,
	CreditCardDeposit = 0,
	DebitPayment = 0,
	NoOfDebitPaymentTransactions = 0,
	TotalCharge = 0,
	NoOfDiscountedTransactions = 0,
	NegativeAdjustments = 0,
	NoOfNegativeAdjustmentTransactions = 0,
	PromotionalItems = 0,
	CreditSalesTax = 0,
	BatchCounter = 0,
	DebitDeposit = 0,
	RewardPointsPayment = 0,
	RewardConvertedPayment = 0,
	NoOfRewardPointsPayment = 0,
	ActualOldGrandTotal = 0,
	ActualNewGrandTotal = 0,
	NoOfReprintedTransaction = 0,
	TotalReprintedTransaction = 0
WHERE TerminalID = 1;

UPDATE tblTerminal SET  TrustFund = 45;


SET FOREIGN_KEY_CHECKS = 1;
