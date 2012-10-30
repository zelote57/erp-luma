
SET @VoidStatus = 1;
SET @ReturnStatus = 2;

(SELECT
	a.ProductGroup,
	TransactionItemStatus,
	SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Quantity, a.Quantity))) 'TranCount',
	SUM(IF(TransactionItemStatus = @VoidStatus, 0, IF(TransactionItemStatus = @ReturnStatus, -a.Amount, a.Amount))) 'Amount'
FROM  tblTransactions01 
INNER JOIN tblTransactions01 ON a.TransactionID = b.TransactionID
WHERE 1=1
AND TerminalNo = @TerminalNo
AND (TransactionStatus = @TransactionStatusClosed
OR TransactionStatus = @TransactionStatusVoid
OR TransactionStatus = @TransactionStatusReprinted
OR TransactionStatus = @TransactionStatusRefund
OR TransactionStatus = @TransactionStatusCreditPayment)
AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport
WHERE TerminalNo = @TerminalNo)
GROUP BY ProductGroup)