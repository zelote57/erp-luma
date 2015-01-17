
SELECT CreditBillHeaderID, CreditBillID, ContactID, GuarantorID,
	CreditLimit, RunningCreditAmt, CurrMonthCreditAmt, CurrMonthAmountPaid,
	BillingDate, BillingFile, TotalBillCharges, CurrentDueAmount, MinimumAmountDue,
	Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue, Prev1MoCurrMonthAmountPaid, 
	Prev2MoCurrentDueAmount, CurrentPurchaseAmt, BeginningBalance, EndingBalance,
	CreatedOn, CreatedByID, CreatedByName, IsBillPrinted
FROM tblCreditBillHeader
WHERE BillingDate = '2014-12-20' 
LIMIT 10;

SELECT CreditBillHeaderID, CreditBillID, ContactID, GuarantorID,
	CreditLimit, RunningCreditAmt, CurrMonthCreditAmt, CurrMonthAmountPaid,
	BillingDate, BillingFile, TotalBillCharges, CurrentDueAmount, MinimumAmountDue,
	Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue, Prev1MoCurrMonthAmountPaid, 
	Prev2MoCurrentDueAmount, CurrentPurchaseAmt, BeginningBalance, EndingBalance,
	CreatedOn, CreatedByID, CreatedByName, IsBillPrinted
FROM tblCreditBillHeader
WHERE BillingDate = '2015-01-06' 
LIMIT 10;

update tblterminal set TerminalName = 'SOS: 22' where terminalno = '18';
update tblterminal set branchid=9 where terminalno = '22';
update tblterminalreport set branchid=9 where terminalno = '22';
update tblterminalreporthistory set branchid=9 where terminalno = '22';
update tblcashierreport set branchid=9 where terminalno = '22';
update tblcashierreporthistory set branchid=9 where terminalno = '22';
update tbltransactions set branchid=9 where terminalno = '22';
update tblcreditpaymentcash set branchid=9 where terminalno = '22';
update tblcreditcardpayment set branchid=9 where terminalno = '22';
update tblchequepayment set branchid=9 where terminalno = '22';
update tblcashpayment set branchid=9 where terminalno = '22';
update tblcashierlogs set branchid=9 where terminalno = '22';
update tblcashcount set branchid=9 where terminalno = '22';
update tblwithhold set branchid=9 where terminalno = '22';
update tblDisburse set branchid=9 where terminalno = '22';
update tblDeposit set branchid=9 where terminalno = '22';
update tblPaidOut set branchid=9 where terminalno = '22';
update tblplureport set branchid=9 where terminalno = '22';
update tblCreditPaymentCash set branchid=9 where terminalno = '22';
update tblCreditPaymentCheque set branchid=9 where terminalno = '22';
update tblDebitPayment set branchid=9 where terminalno = '22';
update tblCreditPayment set branchid=9 where terminalno = '22';
select * from tblbranch;

update tblCreditPayment set branchid=7 where terminalno = '80';
update tblCreditPayment set branchid=7 where terminalno = '81';
update tblCreditPayment set branchid=7 where terminalno = '82';
update tblCreditPayment set branchid=7 where terminalno = '83';
update tblCreditPayment set branchid=7 where terminalno = '84';

-- sa gabi ito
-- update sysaudittrail set branchid=5 where terminalno = '02';
-- update sysaudittrail set branchid=4 where terminalno = '01';
-- update sysaudittrail set branchid=6 where terminalno = '90';

