
/**************************************************************
	procCreditCardReportForZread
	Lemuel E. Aceron
	CALL procCreditCardReportForZread(1, '05', 1, '2014-12-20 00:00', '2014-12-25 23:59');
	
	Jan 31, 2015 - as requested by HP
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditCardReportForZread
GO

create procedure procCreditCardReportForZread(
	IN iBranchID int(4),
	IN strTerminalNo varchar(30),
	IN iCashierID bigint(20),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE iTransactionStatusClosed, iTransactionStatusVoid, iTransactionStatusReprinted, iTransactionStatusRefund INTEGER DEFAULT 0;
	
	SET iTransactionStatusClosed = 1; 
	SET iTransactionStatusVoid = 3;
	SET iTransactionStatusReprinted = 4;
	SET iTransactionStatusRefund = 5;


	IF iCashierID = 0 THEN
		SELECT
			 ccp.CardTypeCode
			,COUNT(IF(TransactionStatus = iTransactionStatusVoid, 0, IF(TransactionStatus = iTransactionStatusRefund, -ccp.CardTypeCode, ccp.CardTypeCode))) TranCount
            ,SUM(IF(TransactionStatus = iTransactionStatusVoid, 0, IF(TransactionStatus = iTransactionStatusRefund, -ccp.Amount, ccp.Amount))) Amount
			,'0%' Percentage
		FROM tblCreditCardPayment ccp
		INNER JOIN tblTransactions trx ON ccp.TransactionID = trx.TransactionID AND ccp.BranchID = trx.BranchID
		WHERE ccp.BranchID = iBranchID AND ccp.TerminalNo = strTerminalNo
			AND DATE_FORMAT(ccp.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
			AND DATE_FORMAT(ccp.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
			AND (TransactionStatus = iTransactionStatusClosed 
                OR TransactionStatus = iTransactionStatusVoid
                OR TransactionStatus = iTransactionStatusReprinted
                OR TransactionStatus = iTransactionStatusRefund)
		GROUP BY CardTypeCode
		ORDER BY CardTypeCode;
	ELSE
		SELECT
			ccp.CardTypeCode
			,COUNT(IF(TransactionStatus = iTransactionStatusVoid, 0, IF(TransactionStatus = iTransactionStatusRefund, -ccp.CardTypeCode, ccp.CardTypeCode))) TranCount
            ,SUM(IF(TransactionStatus = iTransactionStatusVoid, 0, IF(TransactionStatus = iTransactionStatusRefund, -ccp.Amount, ccp.Amount))) Amount
			,'0%' Percentage
		FROM tblCreditCardPayment ccp
		INNER JOIN tblTransactions trx ON ccp.TransactionID = trx.TransactionID AND ccp.BranchID = trx.BranchID
		WHERE ccp.BranchID = iBranchID AND ccp.TerminalNo = strTerminalNo
			AND trx.CreatedByID = iCashierID
			AND DATE_FORMAT(ccp.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
			AND DATE_FORMAT(ccp.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
			AND (TransactionStatus = iTransactionStatusClosed 
                OR TransactionStatus = iTransactionStatusVoid
                OR TransactionStatus = iTransactionStatusReprinted
                OR TransactionStatus = iTransactionStatusRefund)
		GROUP BY CardTypeCode
		ORDER BY CardTypeCode;
	END IF;

	-- shortcut
	-- AND IFNULL(strCashierName, '') = IF(IFNULL(strCashierName,'') = '', '', CashierName) 
END;
GO
delimiter ;