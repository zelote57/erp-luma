-- =============================================
-- Script Template
--	System automatic procedures.
-- =============================================



/**************************************************************
	10Aug2013: LEAceron		sysUpdatetblInventorySG
	Desc: Auto update the pproductsubgroupids in tblInventory
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysUpdatetblInventorySG
GO

create procedure sysUpdatetblInventorySG()
BEGIN

	UPDATE tblInventory
    INNER JOIN tblProducts ON tblProducts.ProductID = tblInventory.ProductID
    INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID
	INNER JOIN tblProductGroup ON tblProductSubGroup.ProductGroupID = tblProductGroup.ProductGroupID
    SET  tblInventory.ProductGroupID = tblProductGroup.ProductGroupID
		,tblInventory.ProductGroupCode = tblProductGroup.ProductGroupCode
		,tblInventory.ProductGroupName = tblProductGroup.ProductGroupName
	WHERE tblInventory.ProductGroupID = 0;

END;
GO
delimiter ;

CALL sysUpdatetblInventorySG();



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


