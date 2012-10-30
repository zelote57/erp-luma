 /**** Just change the contact code
 to the contact you want to check the credits
 
 
 then run source filenameofthisfile.sql
 
  UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 UPDATE tblcontacts SET CREDIT =  326.00 WHERE ContactCode = 'Rose';
 UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 UPDATE tblcontacts SET CREDIT = 5941.75 WHERE ContactCode = 'Lynette Co';
 
 *****/
 

 
 SET @ContactCode = 'Rose';
 SET @ContactID = (Select ContactID from tblContacts WHERE ContactCode like CONCAT('%',@ContactCode,'%'));
 
 
 SELECT TransactionID,
		TransactionNo,
		CustomerID,
		CustomerName,
		TransactionDate,
		SubTotal,
		ItemsDiscount,
		Discount,
		AmountPaid,
		Credit,
		CreditPaid,
		Balance
FROM ((SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions01 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions02 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions03 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions04 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions05 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions06 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions07 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions08 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions09 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions10 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions11 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC) UNION (SELECT
		a.TransactionID,
		a.TransactionNo,
		a.CustomerID,
		a.CustomerName,
		a.TransactionDate,
		a.SubTotal,
		a.ItemsDiscount,
		a.Discount,
		a.AmountPaid - b.Amount 'AmountPaid',
		b.Amount 'Credit',
		b.AmountPaid 'CreditPaid',
		b.Amount - b.AmountPaid 'Balance'
	FROM  tblTransactions12 a
	INNER JOIN tblCreditPayment b ON a.TransactionID = b.TransactionID
	WHERE 1=1
	AND CustomerID = @ContactID
	AND ContactID = @ContactID
--	AND b.Amount > b.AmountPaid
	ORDER BY TransactionNo ASC)) AS tblCreditPayment
	ORDER BY TransactionNo ASC;
	
	
select credit, tblcontacts.* from tblcontacts where contactid = @ContactID;

