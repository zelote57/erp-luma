USE POS;

UPDATE tblTransactionItems01 SET TransactionID = 1 WHERE TransactionItemsID = 1;


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