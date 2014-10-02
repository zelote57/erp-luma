/***
select 
	subtotal, netsales, vatableamount, nonvatableamount,
	vat, discount
from tbltransactions
where transactionno >= 930874
and transactionno <= 930541
and netsales <> SubTotal - (VATExempt * 0.12) - Discount;
**/

SELECT 
        a.ProductGroup, 
        TransactionItemStatus, 
        SUM(IF(TransactionItemStatus = 1, 0, IF(TransactionItemStatus = 3, -a.Quantity, a.Quantity))) 'TranCount', 
        SUM(IF(TransactionItemStatus = 1, 0, IF(TransactionItemStatus = 3, -a.Amount, a.Amount))) 'Amount', 
        '0%' AS Percentage 
FROM  tblTransactionItems a 
        INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID 
        WHERE 1=1 
        AND TerminalNo = '01' AND BranchID = 1
		AND TransactionItemStatus in  (0, 3, 4, 5) 
        AND TransactionStatus in  (1, 4, 5, 7, 8, 9, 10, 11) 
        AND transactionno >= (select beginningtransactionno from tblterminalreport WHERE TerminalNo = '01' AND BranchID = 1 limit 1) 
		AND transactionno <= (select endingtransactionno from tblterminalreport WHERE TerminalNo = '01' AND BranchID = 1 limit 1) 
		-- and transactionno >= 930703 and transactionno <= 930874
GROUP BY ProductGroup;

select 
	SUM(subtotal) subtotal,
	SUM(netsales) netsales,
	SUM(vatableamount) vatableamount,
	SUM(nonvatableamount) nonvatableamount,
	SUM(vat) vat,
	SUM(cashpayment) cashpayment,
	sum(discount) discount,
	max(cashiername), max(cashierid)
from tbltransactions
where  transactionno >= (select beginningtransactionno from tblterminalreport WHERE TerminalNo = '01' AND BranchID = 1 limit 1) 
   AND transactionno <= (select endingtransactionno from tblterminalreport WHERE TerminalNo = '01' AND BranchID = 1 limit 1) 
and transactionstatus in (1,5,11);

/*
select grosssales, netsales, vatableamount, nonvatableamount, vat,
	cashsales, totaldiscount, groupsales, cashierreporthistoryid -- same as netsales
from tblcashierreporthistory
where cashierid = 33 and cashierreporthistoryid = 82166;
-- order by lastlogindate desc limit 10; -- 
*/
-- update tblcashierreporthistory set groupsales=5037.450, grosssales = 5038.00, netsales= 5037.450, vatableamount=4426.33, vat=531.12, cashsales=5020.00 where cashierid = 33 and cashierreporthistoryid = 82166;

select grosssales, netsales, vatableamount, nonvatableamount, vat,
	cashsales, totaldiscount, datelastinitialized
from tblterminalreport;


-- update tblterminalreporthistory set grosssales = 5038.00, netsales= 5037.450, vatableamount=4426.33, vat=531.12, cashsales=5020.00, trustfund=0 where beginningorno = 930703;