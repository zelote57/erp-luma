UPDATE sysCreditConfig SET ConfigValue = '2013-01-10' WHERE ConfigName = 'CreditPurcStartDateToProcess';
UPDATE sysCreditConfig SET ConfigValue = '2013-02-09' WHERE ConfigName = 'CreditPurcEndDateToProcess';
UPDATE sysCreditConfig SET ConfigValue = '2013-01-31' WHERE ConfigName = 'CreditCutOffDate';

SELECT CreditBillHeaderID ,CreditDate ,CONCAT('Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
			,SUM(Amount) CurrMonthCreditAmt ,1 tblCreditPaymentTransactionTypeID ,TransactionID TransactionRefID
	FROM tblCreditBillHeader CBH
	INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
	WHERE CBH.CreditBillID = 1 
		AND CONVERT(CreditDate, DATE) BETWEEN '2013-01-10' AND '2013-02-09';

SELECT CreditBillHeaderID, TransactionDate
					,CONCAT('Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
					,-SUM(Subtotal) CurrMonthCreditAmt ,2 tblCreditPaymentTransactionTypeID ,Trx.TransactionID TransactionRefID
			FROM tblTransactions Trx 
				INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
				INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID
			WHERE TrxD.ProductID = 1 
				AND TransactionStatus = 1
				AND CONVERT(Trx.TransactionDate, DATE) BETWEEN '2013-01-10' AND '2013-02-09'