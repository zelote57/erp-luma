-- =============================================
-- Script Template
--	System automatic procedures.
-- =============================================

/**************************************************************
	10Aug2013: LEAceron		sysUpdatetblInventorySG
	Desc: Auto update the pproductsubgroupids in tblInventory

	01Sep2013 Deleted and put in the Closing inventory by group
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysUpdatetblInventorySG
GO


/**************************************************************
	08Aug2013: LEAceron		sysPurgeTransactions
	Desc: Auto purge the transactions once a month. retain only 1 year of data.
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysPurgeTransactions
GO

create procedure sysPurgeTransactions()
BEGIN
	
	INSERT INTO tblTransactionItemsBackup (TransactionItemsID, 	TransactionID, 	ProductID, 	ProductCode, 	BarCode, 	Description, 	ProductUnitID, 	ProductUnitCode, 	Quantity, 	Price, 	SellingPrice, 	Discount, 	ItemDiscount, 	ItemDiscountType, 	Amount, 	VAT, 	VatableAmount, 	EVAT, 	EVatableAmount, 	LocalTax, 	VariationsMatrixID, 	MatrixDescription, 	ProductGroup, 	ProductSubGroup, 	TransactionItemStatus, 	DiscountCode, 	DiscountRemarks, 	ProductPackageID, 	MatrixPackageID, 	PackageQuantity, 	PromoQuantity, 	PromoValue, 	PromoInPercent, 	PromoType, 	PromoApplied, 	PurchasePrice, 	PurchaseAmount, 	IncludeInSubtotalDiscount, 	OrderSlipPrinter, 	orderslipprinted, 	PercentageCommision, 	Commision, 	PaxNo, BackupDate)
	SELECT TransactionItemsID, 	TransactionID, 	ProductID, 	ProductCode, 	BarCode, 	Description, 	ProductUnitID, 	ProductUnitCode, 	Quantity, 	Price, 	SellingPrice, 	Discount, 	ItemDiscount, 	ItemDiscountType, 	Amount, 	VAT, 	VatableAmount, 	EVAT, 	EVatableAmount, 	LocalTax, 	VariationsMatrixID, 	MatrixDescription, 	ProductGroup, 	ProductSubGroup, 	TransactionItemStatus, 	DiscountCode, 	DiscountRemarks, 	ProductPackageID, 	MatrixPackageID, 	PackageQuantity, 	PromoQuantity, 	PromoValue, 	PromoInPercent, 	PromoType, 	PromoApplied, 	PurchasePrice, 	PurchaseAmount, 	IncludeInSubtotalDiscount, 	OrderSlipPrinter, 	orderslipprinted, 	PercentageCommision, 	Commision, 	PaxNo, NOW()
	FROM tblTransactionItems WHERE TransactionID IN (SELECT DISTINCT TransactionID FROM tblTransactions WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d') <= '2012-05-31');

	INSERT INTO tblTransactionsBackup(TransactionID, 	TransactionNo, 	CustomerID, 	CustomerName, 	CashierID, 	CashierName, 	TerminalNo, 	TransactionDate, 	DateSuspended, 	DateResumed, 	TransactionStatus, 	SubTotal, 	Discount, 	TransDiscount, 	TransDiscountType, 	VAT, 	VatableAmount, 	EVAT, 	EVatableAmount, 	LocalTax, 	AmountPaid, 	CashPayment, 	ChequePayment, 	CreditCardPayment, 	CreditPayment, 	BalanceAmount, 	ChangeAmount, 	DateClosed, 	PaymentType, 	DiscountCode, 	DiscountRemarks, 	DebitPayment, 	ItemsDiscount, 	Charge, 	ChargeAmount, 	ChargeCode, 	ChargeRemarks, 	WaiterID, 	WaiterName, 	Packed, 	OrderType, 	AgentID, 	AgentName, 	CreatedByID, 	CreatedByName, 	AgentDepartmentName, 	AgentPositionName, 	ReleaserID, 	ReleaserName, 	ReleasedDate, 	RewardPointsPayment, 	RewardConvertedPayment, 	PaxNo, 	CreditChargeAmount, 	BranchID, 	BranchCode, TransactionType, BackupDate)
	SELECT TransactionID, 	TransactionNo, 	CustomerID, 	CustomerName, 	CashierID, 	CashierName, 	TerminalNo, 	TransactionDate, 	DateSuspended, 	DateResumed, 	TransactionStatus, 	SubTotal, 	Discount, 	TransDiscount, 	TransDiscountType, 	VAT, 	VatableAmount, 	EVAT, 	EVatableAmount, 	LocalTax, 	AmountPaid, 	CashPayment, 	ChequePayment, 	CreditCardPayment, 	CreditPayment, 	BalanceAmount, 	ChangeAmount, 	DateClosed, 	PaymentType, 	DiscountCode, 	DiscountRemarks, 	DebitPayment, 	ItemsDiscount, 	Charge, 	ChargeAmount, 	ChargeCode, 	ChargeRemarks, 	WaiterID, 	WaiterName, 	Packed, 	OrderType, 	AgentID, 	AgentName, 	CreatedByID, 	CreatedByName, 	AgentDepartmentName, 	AgentPositionName, 	ReleaserID, 	ReleaserName, 	ReleasedDate, 	RewardPointsPayment, 	RewardConvertedPayment, 	PaxNo, 	CreditChargeAmount, 	BranchID, 	BranchCode, TransactionType, NOW()
	FROM tblTransactions WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d') <= '2012-05-31';

	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT DISTINCT TransactionID FROM tblTransactions WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d') <= '2012-05-31');
	DELETE FROM tblTransactions WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d') <= '2012-05-31';

END;
GO
delimiter ;



/**************************************************************
	22Aug2013: LEAceron		sysProductInventorySnapshot
	Desc: Auto save the daily inventory and monthly

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysProductInventorySnapshot
GO

create procedure sysProductInventorySnapshot()
BEGIN
	DECLARE dteProcessDate DATETIME;
	
	SET dteProcessDate = NOW();

	CALL procsysAuditInsert(dteProcessDate, 'SYSTEM', 'sysProductInventorySnapshot: DAILY INV START', 'localhost', 'Start processing daily inventory');

	INSERT INTO tblProductInventoryDaily (
					 BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DateCreated)
			SELECT   BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock , dteProcessDate FROM tblProductInventory;

	CALL procsysAuditInsert(NOW(), 'SYSTEM', 'sysProductInventorySnapshot: DAILY INV FINISH', 'localhost', 'Finish processing daily inventory');

	SET dteProcessDate = NOW();
	CALL procsysAuditInsert(dteProcessDate, 'SYSTEM', 'sysProductInventorySnapshot: MONTHLY INV START', 'localhost', 'Start processing monthly inventory');

	DELETE FROM tblProductInventoryMonthly WHERE DateMonth = DATE_FORMAT(dteProcessDate, '%Y-%m');

	INSERT INTO tblProductInventoryMonthly (
				BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DateMonth ,DateCreated)
		SELECT  BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DATE_FORMAT(dteProcessDate, '%Y-%m') ,dteProcessDate FROM tblProductInventory;
	
	CALL procsysAuditInsert(NOW(), 'SYSTEM', 'sysProductInventorySnapshot: MONTHLY INV FINISH', 'localhost', 'Start processing monthly inventory');	
END;
GO
delimiter ;




