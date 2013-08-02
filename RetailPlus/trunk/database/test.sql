
/**************************************************************

	sysPurgeTransactions
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL sysPurgeTransactions();
	
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