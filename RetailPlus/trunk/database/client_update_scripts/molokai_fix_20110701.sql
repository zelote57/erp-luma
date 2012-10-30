
 
-- Sept Problem 
-- Sep 24 jump to Oct 24
/**************************** For Nov which is for Oct sana ****************************/
UPDATE tbltransactions11 SET TransactionDate = ADDDATE(TransactionDate,INTERVAL -30 DAY),
							 DateSuspended = ADDDATE(DateSuspended,INTERVAL -30 DAY),
							 DateResumed = ADDDATE(DateResumed,INTERVAL -30 DAY),
							 DateClosed = ADDDATE(DateClosed,INTERVAL -30 DAY)
WHERE TransactionDate > '2011-10-24 00:00:00';

INSERT INTO tblTransactions10 (TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName) 
SELECT TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName        
FROM tbltransactions11 WHERE TransactionDate > '2011-10-24 00:00:00';

ALTER TABLE tblTransactionItems11 add transactionno varchar(15);

UPDATE tblTransactionItems11 SET transactionno = (SELECT TransactionNo FROM tbltransactions11 a WHERE a.TransactionID = tblTransactionItems11.TransactionID);

INSERT INTO tblTransactionItems10 (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision)
SELECT b.TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, 
		Quantity, Price, SellingPrice, a.Discount, a.ItemDiscount, a.ItemDiscountType, a.Amount, a.VAT, 
		a.VatableAmount, a.EVAT, a.EVatableAmount, a.LocalTax, a.VariationsMatrixID, a.MatrixDescription, 
		a.ProductGroup, a.ProductSubGroup, a.TransactionItemStatus, a.DiscountCode, a.DiscountRemarks, 
		a.ProductPackageID, a.MatrixPackageID, a.PackageQuantity, a.PromoQuantity, a.PromoValue, a.PromoInPercent, 
		a.PromoType, a.PromoApplied, a.PurchasePrice, a.PurchaseAmount, a.IncludeInSubtotalDiscount, a.OrderSlipPrinter, 
		a.OrderSlipPrinted, a.PercentageCommision, a.Commision 
FROM tblTransactionItems11 a INNER JOIN tblTransactions10 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-10-24 00:00:00';

DELETE a.* FROM tblTransactionItems11 a INNER JOIN tbltransactions11 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-10-24 00:00:00';

DELETE FROM tbltransactions11 WHERE TransactionDate > '2011-10-24 00:00:00';
ALTER TABLE tblTransactionItems11 DROP TransactionNo;


/**************************** For Oct which is for Sep sana ****************************/
UPDATE tbltransactions10 SET TransactionDate = ADDDATE(TransactionDate,INTERVAL -30 DAY),
							 DateSuspended = ADDDATE(DateSuspended,INTERVAL -30 DAY),
							 DateResumed = ADDDATE(DateResumed,INTERVAL -30 DAY),
							 DateClosed = ADDDATE(DateClosed,INTERVAL -30 DAY)
WHERE TransactionDate > '2011-10-24 00:00:00';

INSERT INTO tblTransactions09 (TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName) 
SELECT TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName        
FROM tbltransactions10 WHERE TransactionDate > '2011-10-24 00:00:00';

ALTER TABLE tblTransactionItems10 add transactionno varchar(15);

UPDATE tblTransactionItems10 SET transactionno = (SELECT TransactionNo FROM tbltransactions10 a WHERE a.TransactionID = tblTransactionItems10.TransactionID);

INSERT INTO tblTransactionItems09 (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision)
SELECT b.TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, 
		Quantity, Price, SellingPrice, a.Discount, a.ItemDiscount, a.ItemDiscountType, a.Amount, a.VAT, 
		a.VatableAmount, a.EVAT, a.EVatableAmount, a.LocalTax, a.VariationsMatrixID, a.MatrixDescription, 
		a.ProductGroup, a.ProductSubGroup, a.TransactionItemStatus, a.DiscountCode, a.DiscountRemarks, 
		a.ProductPackageID, a.MatrixPackageID, a.PackageQuantity, a.PromoQuantity, a.PromoValue, a.PromoInPercent, 
		a.PromoType, a.PromoApplied, a.PurchasePrice, a.PurchaseAmount, a.IncludeInSubtotalDiscount, a.OrderSlipPrinter, 
		a.OrderSlipPrinted, a.PercentageCommision, a.Commision 
FROM tblTransactionItems10 a INNER JOIN tblTransactions09 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-10-24 00:00:00';

DELETE a.* FROM tblTransactionItems10 a INNER JOIN tbltransactions09 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-10-24 00:00:00';

DELETE FROM tbltransactions10 WHERE TransactionDate > '2011-10-24 00:00:00';
ALTER TABLE tblTransactionItems10 DROP TransactionNo;

/**************************** End Oct which is for Sep sana ****************************/

-- SELECT * FROM tblTerminalReportHistory WHERE DatelastInitialized > '2011-10-24 00:00:00';
UPDATE tblTerminalReportHistory SET DatelastInitialized = ADDDATE(DatelastInitialized,INTERVAL -30 DAY) WHERE DatelastInitialized > '2011-10-24 00:00:00';
UPDATE tblTerminalReport SET DatelastInitialized = ADDDATE(DatelastInitialized,INTERVAL -30 DAY) WHERE DatelastInitialized > '2011-10-24 00:00:00';
-- SELECT * FROM tblTerminalReportHistory WHERE DatelastInitialized > '2011-10-24 00:00:00';

-- SELECT * FROM tblCashierReportHistory WHERE LastLoginDate > '2011-10-24 00:00:00';
UPDATE tblCashierReportHistory SET LastLoginDate = ADDDATE(LastLoginDate,INTERVAL -30 DAY) WHERE LastLoginDate > '2011-10-24 00:00:00';
UPDATE tblCashierReport SET LastLoginDate = ADDDATE(LastLoginDate,INTERVAL -30 DAY) WHERE LastLoginDate > '2011-10-24 00:00:00';
-- SELECT * FROM tblCashierReportHistory WHERE LastLoginDate > '2011-10-24 00:00:00';











-- August Problem
-- July 24 Jump to 
/**************************** For August which is for July dapat ****************************/

UPDATE tbltransactions08 SET TransactionDate = ADDDATE(TransactionDate,INTERVAL -25 DAY),
							 DateSuspended = ADDDATE(DateSuspended,INTERVAL -25 DAY),
							 DateResumed = ADDDATE(DateResumed,INTERVAL -25 DAY),
							 DateClosed = ADDDATE(DateClosed,INTERVAL -25 DAY)
WHERE TransactionDate > '2011-07-24 00:00:00';

ALTER TABLE tblTransactionItems08 add transactionno varchar(15);

UPDATE tblTransactionItems08 SET transactionno = (SELECT TransactionNo FROM tblTransactions08 a WHERE a.TransactionID = tblTransactionItems08.TransactionID);

INSERT INTO tblTransactions07 (TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName) 
SELECT TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName        
FROM tblTransactions08 WHERE TransactionDate > '2011-07-24 00:00:00';

INSERT INTO tblTransactionItems07 (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision)
SELECT b.TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, 
		Quantity, Price, SellingPrice, a.Discount, a.ItemDiscount, a.ItemDiscountType, a.Amount, a.VAT, 
		a.VatableAmount, a.EVAT, a.EVatableAmount, a.LocalTax, a.VariationsMatrixID, a.MatrixDescription, 
		a.ProductGroup, a.ProductSubGroup, a.TransactionItemStatus, a.DiscountCode, a.DiscountRemarks, 
		a.ProductPackageID, a.MatrixPackageID, a.PackageQuantity, a.PromoQuantity, a.PromoValue, a.PromoInPercent, 
		a.PromoType, a.PromoApplied, a.PurchasePrice, a.PurchaseAmount, a.IncludeInSubtotalDiscount, a.OrderSlipPrinter, 
		a.OrderSlipPrinted, a.PercentageCommision, a.Commision 
FROM tblTransactionItems08 a INNER JOIN tblTransactions07 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-07-24 00:00:00';

DELETE FROM tblTransactionItems08 WHERE transactionno IN (SELECT TransactionNo FROM tblTransactions08 WHERE TransactionDate > '2011-07-24 00:00:00');

DELETE FROM tblTransactions08 WHERE TransactionDate > '2011-07-24 00:00:00';

ALTER TABLE tblTransactionItems08 DROP TransactionNo;


/**************************** For Sept which is for August dapat ****************************/

UPDATE tbltransactions09 SET TransactionDate = ADDDATE(TransactionDate,INTERVAL -31 DAY),
							 DateSuspended = ADDDATE(DateSuspended,INTERVAL -31 DAY),
							 DateResumed = ADDDATE(DateResumed,INTERVAL -31 DAY),
							 DateClosed = ADDDATE(DateClosed,INTERVAL -31 DAY)
WHERE TransactionDate > '2011-07-24 00:00:00';

INSERT INTO tblTransactions08 (TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName) 
SELECT TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName        
FROM tbltransactions09 WHERE TransactionDate > '2011-07-24 00:00:00';

ALTER TABLE tblTransactionItems09 add transactionno varchar(15);

UPDATE tblTransactionItems09 SET transactionno = (SELECT TransactionNo FROM tbltransactions09 a WHERE a.TransactionID = tblTransactionItems09.TransactionID);

INSERT INTO tblTransactionItems08 (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision)
SELECT b.TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, 
		Quantity, Price, SellingPrice, a.Discount, a.ItemDiscount, a.ItemDiscountType, a.Amount, a.VAT, 
		a.VatableAmount, a.EVAT, a.EVatableAmount, a.LocalTax, a.VariationsMatrixID, a.MatrixDescription, 
		a.ProductGroup, a.ProductSubGroup, a.TransactionItemStatus, a.DiscountCode, a.DiscountRemarks, 
		a.ProductPackageID, a.MatrixPackageID, a.PackageQuantity, a.PromoQuantity, a.PromoValue, a.PromoInPercent, 
		a.PromoType, a.PromoApplied, a.PurchasePrice, a.PurchaseAmount, a.IncludeInSubtotalDiscount, a.OrderSlipPrinter, 
		a.OrderSlipPrinted, a.PercentageCommision, a.Commision 
FROM tblTransactionItems09 a INNER JOIN tblTransactions08 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-07-24 00:00:00';

DELETE a.* FROM tblTransactionItems09 a INNER JOIN tbltransactions09 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-07-24 00:00:00';

DELETE FROM tbltransactions09 WHERE TransactionDate > '2011-07-24 00:00:00';
ALTER TABLE tblTransactionItems09 DROP TransactionNo;

/**************************** For October which is for Sept datapat ****************************/

UPDATE tbltransactions10 SET TransactionDate = ADDDATE(TransactionDate,INTERVAL -29 DAY),
							 DateSuspended = ADDDATE(DateSuspended,INTERVAL -29 DAY),
							 DateResumed = ADDDATE(DateResumed,INTERVAL -29 DAY),
							 DateClosed = ADDDATE(DateClosed,INTERVAL -29 DAY)
WHERE TransactionDate > '2011-07-24 00:00:00';

INSERT INTO tblTransactions09 (TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName) 
SELECT TransactionNo  , CustomerID , CustomerName , CashierID , CashierName       , TerminalNo , TransactionDate     , DateSuspended       , DateResumed         , TransactionStatus , SubTotal , Discount , TransDiscount , TransDiscountType , VAT    , VatableAmount , EVAT , EVatableAmount , LocalTax , AmountPaid , CashPayment , ChequePayment , CreditCardPayment , CreditPayment , BalanceAmount , ChangeAmount , DateClosed          , PaymentType , DiscountCode , DiscountRemarks , DebitPayment , WaiterID , WaiterName      , ItemsDiscount , Charge , ChargeAmount , ChargeCode , ChargeRemarks , Packed , OrderType , AgentID , AgentName        
FROM tbltransactions10 WHERE TransactionDate > '2011-07-24 00:00:00';

ALTER TABLE tblTransactionItems10 add transactionno varchar(15);

UPDATE tblTransactionItems10 SET transactionno = (SELECT TransactionNo FROM tbltransactions10 a WHERE a.TransactionID = tblTransactionItems10.TransactionID);

INSERT INTO tblTransactionItems09 (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision)
SELECT b.TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, 
		Quantity, Price, SellingPrice, a.Discount, a.ItemDiscount, a.ItemDiscountType, a.Amount, a.VAT, 
		a.VatableAmount, a.EVAT, a.EVatableAmount, a.LocalTax, a.VariationsMatrixID, a.MatrixDescription, 
		a.ProductGroup, a.ProductSubGroup, a.TransactionItemStatus, a.DiscountCode, a.DiscountRemarks, 
		a.ProductPackageID, a.MatrixPackageID, a.PackageQuantity, a.PromoQuantity, a.PromoValue, a.PromoInPercent, 
		a.PromoType, a.PromoApplied, a.PurchasePrice, a.PurchaseAmount, a.IncludeInSubtotalDiscount, a.OrderSlipPrinter, 
		a.OrderSlipPrinted, a.PercentageCommision, a.Commision 
FROM tblTransactionItems10 a INNER JOIN tblTransactions09 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-07-24 00:00:00';

DELETE a.* FROM tblTransactionItems10 a INNER JOIN tbltransactions10 b ON a.TransactionNo = b.TransactionNo
WHERE TransactionDate > '2011-07-24 00:00:00';

DELETE FROM tbltransactions10 WHERE TransactionDate > '2011-07-24 00:00:00';
ALTER TABLE tblTransactionItems10 DROP TransactionNo;

/**************************** End For October which is for Sept datapat ****************************/

-- SELECT * FROM tblTerminalReportHistory WHERE DatelastInitialized > '2011-07-24 00:00:00';
UPDATE tblTerminalReportHistory SET DatelastInitialized = ADDDATE(DatelastInitialized,INTERVAL -31 DAY) WHERE DatelastInitialized > '2011-07-24 00:00:00';
UPDATE tblTerminalReport SET DatelastInitialized = ADDDATE(DatelastInitialized,INTERVAL -31 DAY) WHERE DatelastInitialized > '2011-07-24 00:00:00';
-- SELECT * FROM tblTerminalReportHistory WHERE DatelastInitialized > '2011-07-24 00:00:00';

-- SELECT * FROM tblCashierReportHistory WHERE LastLoginDate > '2011-07-24 00:00:00';
UPDATE tblCashierReportHistory SET LastLoginDate = ADDDATE(LastLoginDate,INTERVAL -31 DAY) WHERE LastLoginDate > '2011-07-24 00:00:00';
UPDATE tblCashierReport SET LastLoginDate = ADDDATE(LastLoginDate,INTERVAL -31 DAY) WHERE LastLoginDate > '2011-07-24 00:00:00';
-- SELECT * FROM tblCashierReportHistory WHERE LastLoginDate > '2011-07-24 00:00:00';

SELECT cast(DateLastInitialized as date) FROM tblterminalReportHistory WHERE DateLastInitialized > '2011-05-24 00:00:00';

SELECT cast(DatelastInitialized as date) FROM tblTerminalReport WHERE DatelastInitialized > '2011-07-24 00:00:00' order by cast(DatelastInitialized as date);
SELECT cast(LastLoginDate as date) FROM tblCashierReport WHERE LastLoginDate > '2011-07-24 00:00:00' order by cast(LastLoginDate as date);

SELECT cast(TransactionDate as date) FROM tblTransactions11 WHERE TransactionDate > '2011-07-24 00:00:00' group by cast(TransactionDate as Date) order by cast(TransactionDate as date);
SELECT cast(TransactionDate as date) FROM tblTransactions10 WHERE TransactionDate > '2011-07-24 00:00:00' group by cast(TransactionDate as Date) order by cast(TransactionDate as date);
SELECT cast(TransactionDate as date) FROM tblTransactions09 WHERE TransactionDate > '2011-07-24 00:00:00' group by cast(TransactionDate as Date) order by cast(TransactionDate as date);
SELECT cast(TransactionDate as date) FROM tblTransactions08 WHERE TransactionDate > '2011-07-24 00:00:00' group by cast(TransactionDate as Date) order by cast(TransactionDate as date);
SELECT cast(TransactionDate as date) FROM tblTransactions07 WHERE TransactionDate > '2011-07-24 00:00:00' group by cast(TransactionDate as Date) order by cast(TransactionDate as date);







