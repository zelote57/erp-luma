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
	DECLARE strProcessRefNo VARCHAR(20);

	SET dteProcessDate = NOW();

	/**** do not include daily snapshot is database is too big
	CALL procsysAuditInsert(dteProcessDate, 'SYSTEM', 'sysProductInventorySnapshot: DAILY INV START', 'localhost', 'Start processing daily inventory');

	INSERT INTO tblProductInventoryDaily (
					 BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DateCreated)
			SELECT   BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock , dteProcessDate FROM tblProductInventory;


	UPDATE tblProductInventoryDaily, tblProducts, tblProductPackage
	SET
		tblProductInventoryDaily.PurchasePrice = tblProductPackage.PurchasePrice,
		tblProductInventoryDaily.Price = tblProductPackage.Price
	WHERE tblProductInventoryDaily.ProductID = tblProducts.ProductID
		AND tblProductInventoryDaily.ProductID = tblProductPackage.ProductID
		AND tblProducts.BaseUnitID = tblProductPackage.UnitID
		AND tblProductInventoryDaily.MatrixID = tblProductPackage.MatrixID
		AND tblProductPackage.Quantity = 1
		AND DATE_FORMAT(tblProductInventoryDaily.DateCreated, '%Y-%m-%d') = DATE_FORMAT(dteProcessDate, '%Y-%m-%d');
	
	CALL procsysAuditInsert(NOW(), 'SYSTEM', 'sysProductInventorySnapshot: DAILY INV FINISH', 'localhost', 'Finish processing daily inventory');
	****/

	SET dteProcessDate = NOW();
	CALL procsysAuditInsert(dteProcessDate, 'SYSTEM', 'sysProductInventorySnapshot: MONTHLY INV START', 'localhost', 'Start processing monthly inventory');

	DELETE FROM tblProductInventoryMonthly WHERE DateMonth = DATE_FORMAT(dteProcessDate, '%Y-%m');

	INSERT INTO tblProductInventoryMonthly (
				BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DateMonth ,DateCreated)
		SELECT  BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DATE_FORMAT(dteProcessDate, '%Y-%m') ,dteProcessDate FROM tblProductInventory;
	
	
	UPDATE tblProductInventoryMonthly, tblProducts, tblProductPackage
		SET
			tblProductInventoryMonthly.PurchasePrice = tblProductPackage.PurchasePrice,
			tblProductInventoryMonthly.Price = tblProductPackage.Price
	WHERE tblProductInventoryMonthly.ProductID = tblProducts.ProductID
		AND tblProductInventoryMonthly.ProductID = tblProductPackage.ProductID
		AND tblProducts.BaseUnitID = tblProductPackage.UnitID
		AND tblProductInventoryMonthly.MatrixID = tblProductPackage.MatrixID
		AND tblProductPackage.Quantity = 1
		AND DATE_FORMAT(tblProductInventoryMonthly.DateCreated, '%Y-%m-%d') = DATE_FORMAT(dteProcessDate, '%Y-%m-%d');

	CALL procsysAuditInsert(NOW(), 'SYSTEM', 'sysProductInventorySnapshot: MONTHLY INV FINISH', 'localhost', 'Finish processing monthly inventory');	
END;
GO
delimiter ;


/**************************************************************
	sysContactRewardExpire
	Lemuel E. Aceron
	CALL sysContactRewardExpire();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysContactRewardExpire
GO

create procedure sysContactRewardExpire()
BEGIN
	DECLARE intRewardActive TINYINT(1) DEFAULT 0;
	DECLARE intRewardCardStatusExpired TINYINT(1) DEFAULT 2;
	DECLARE strProcessRefNo VARCHAR(20);

	SET strProcessRefNo = DATE_FORMAT(NOW(), '%Y%m%d%H%i');

	-- SysPUser = System Process User
	CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysContactRewardExpire', 'localhost', CONCAT(strProcessRefNo,':Running process'));

	INSERT INTO tblContactRewardsMovement (
		CustomerID, RewardDate, RewardPointsBefore, RewardPointsAdjustment, RewardPointsAfter,
		RewardExpiryDate, RewardReason, TerminalNo, CashierName, TransactionNo)
	SELECT CustomerID, RewardAwardDate, RewardPoints, 0, RewardPoints, 
		ExpiryDate, 'SYSTEM AUTO EXPIRE', '01', 'SYSTEM', DATE_FORMAT(NOW(), '%Y%m%d%H%i') 
		FROM tblContactRewards
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND RewardCardStatus <> intRewardCardStatusExpired
			AND RewardActive <> intRewardActive;
	
	UPDATE tblContactRewards SET 
			RewardActive = intRewardActive,
			RewardCardStatus = intRewardCardStatusExpired
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND RewardCardStatus <> intRewardCardStatusExpired
			AND RewardActive <> intRewardActive;
	
	CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysContactRewardExpire', 'localhost', CONCAT(strProcessRefNo,':done'));

	/*******************************
		CALL sysContactRewardExpire();
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	sysContactCreditCardExpire
	Lemuel E. Aceron
	CALL sysContactCreditCardExpire();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysContactCreditCardExpire
GO

create procedure sysContactCreditCardExpire()
BEGIN
	DECLARE intCreditInActive TINYINT(1) DEFAULT 0;
	DECLARE intCreditCardStatusExpired TINYINT(1) DEFAULT 2;
	DECLARE strProcessRefNo VARCHAR(20);

	SET strProcessRefNo = DATE_FORMAT(NOW(), '%Y%m%d%H%i');

	-- SysPUser = System Process User
	CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysContactCreditCardExpire', 'localhost', CONCAT(strProcessRefNo,':Running process'));

	UPDATE tblContacts SET
		IsCreditAllowed = intCreditInActive
	WHERE ContactID IN (SELECT DISTINCT(CustomerID) FROM tblContactCreditCardInfo 
													WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
													AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
													AND CreditCardStatus <> intCreditCardStatusExpired);
	
	UPDATE tblContactCreditCardInfo SET 
			CreditCardStatus = intCreditCardStatusExpired
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND CreditCardStatus <> intCreditCardStatusExpired;
	
	CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysContactCreditCardExpire', 'localhost', CONCAT(strProcessRefNo,':done'));

	/*******************************
		CALL sysContactCreditCardExpire();
	*******************************/
END;
GO
delimiter ;



/**************************************************************
	sysHoldCustomerCreditWithG
	Lemuel E. Aceron
	CALL sysHoldCustomerCreditWithG();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS sysHoldCustomerCreditWithG
GO

create procedure sysHoldCustomerCreditWithG()
BEGIN
	DECLARE intCreditInActive TINYINT(1) DEFAULT 0;
	DECLARE intCreditCardStatusSystemDeactivated TINYINT(1) DEFAULT 8;
	DECLARE dteBillingDate, dteCreditPurcEndDateToProcess DATE;
	DECLARE intCreditBillID BIGINT(20);
	DECLARE strProcessRefNo VARCHAR(20);
	

	-- schedule every 3 and 18 of the month as per Dan of CNTraders
	IF (DATE_FORMAT(NOW(), '%d') = 3 OR DATE_FORMAT(NOW(), '%d') = '03') OR
	   (DATE_FORMAT(NOW(), '%d') = 18 OR DATE_FORMAT(NOW(), '%d') = '18') THEN

		SET strProcessRefNo = DATE_FORMAT(NOW(), '%Y%m%d%H%i');
		
		-- SysPUser = System Process User
		CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysHoldCustomerCreditWithG', 'localhost', CONCAT(strProcessRefNo,':Running process'));

		-- get the next billing date
		SET dteBillingDate = (SELECT BillingDate FROM tblCardTypes WHERE WithGuarantor = 1 LIMIT 1); 
		
		-- get the actual last billing date
		SET dteBillingDate = DATE_ADD(dteBillingDate, INTERVAL -13 DAY);
		IF (DAY(dteBillingDate) <= 15) THEN
			SET dteBillingDate = (SELECT CONCAT(YEAR(dteBillingDate), '-', MONTH(dteBillingDate), '-', '06'));
		ELSE
			SET dteBillingDate = (SELECT CONCAT(YEAR(dteBillingDate), '-', MONTH(dteBillingDate), '-', '20'));
		END IF;

		-- get the next CreditPurcEndDateToProcess
		SET dteCreditPurcEndDateToProcess = (SELECT CreditPurcEndDateToProcess FROM tblCardTypes WHERE WithGuarantor = 1 LIMIT 1);

		-- get the actual last CreditPurcEndDateToProcess
		SET dteCreditPurcEndDateToProcess = DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL -13 DAY);
		IF (DAY(dteCreditPurcEndDateToProcess) <= 15) THEN
			SET dteCreditPurcEndDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess), '-', '05'));
		ELSE
			SET dteCreditPurcEndDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess), '-', '19'));
		END IF;
		
		/**
		SELECT DISTINCT ContactID, CBH.RunningCreditAmt, IFNULL(PaidAmount,0) PaidAmount FROM tblContactCreditCardInfo CCI 
														INNER JOIN tblCreditBillHeader CBH ON CCI.CustomerID = CBH.ContactID
														LEFT OUTER JOIN (
															SELECT CreditBillHeaderID, TransactionDate ,SUM(Subtotal) PaidAmount
															FROM tblTransactions Trx 
																INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
																INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditPurcEndDateToProcess = dteCreditPurcEndDateToProcess)
															WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
																AND TransactionStatus = 7 -- creditPaymentStatus
																AND CONVERT(Trx.TransactionDate, DATE) BETWEEN DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL 1 DAY) AND NOW()
															GROUP BY TransactionNo
														) Payments ON CBH.CreditBillHeaderID = Payments.CreditBillHeaderID
														WHERE CCI.CreditCardStatus <> intCreditCardStatusSystemDeactivated
														AND CBH.BillingDate = dteBillingDate
														AND CBH.RunningCreditAmt > IFNULL(Payments.PaidAmount,0);
		**/

		UPDATE tblContacts SET
			IsCreditAllowed = intCreditInActive
		WHERE ContactID IN (SELECT DISTINCT(ContactID) FROM tblContactCreditCardInfo CCI 
														INNER JOIN tblCreditBillHeader CBH ON CCI.CustomerID = CBH.ContactID
														LEFT OUTER JOIN (
															SELECT CreditBillHeaderID, TransactionDate, SUM(Subtotal) PaidAmount
															FROM tblTransactions Trx 
																INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
																INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditPurcEndDateToProcess = dteCreditPurcEndDateToProcess)
															WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
																AND TransactionStatus = 7 -- creditPaymentStatus
																AND CONVERT(Trx.TransactionDate, DATE) BETWEEN DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL 1 DAY) AND NOW()
															GROUP BY TransactionNo
														) Payments ON CBH.CreditBillHeaderID = Payments.CreditBillHeaderID
														WHERE CCI.CreditCardStatus <> intCreditCardStatusSystemDeactivated
														AND CBH.BillingDate = dteBillingDate
														AND CBH.RunningCreditAmt > IFNULL(Payments.PaidAmount,0));
		

		UPDATE tblContactCreditCardInfo SET
				CreditCardStatus = intCreditCardStatusSystemDeactivated
		WHERE CreditCardStatus <> intCreditCardStatusSystemDeactivated
			AND CustomerID IN (SELECT DISTINCT(ContactID) FROM tblCreditBillHeader CBH
														LEFT OUTER JOIN (
															SELECT CreditBillHeaderID, TransactionDate ,SUM(Subtotal) PaidAmount
															FROM tblTransactions Trx 
																INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
																INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditPurcEndDateToProcess = dteCreditPurcEndDateToProcess)
															WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
																AND TransactionStatus = 7 -- creditPaymentStatus
																AND CONVERT(Trx.TransactionDate, DATE) BETWEEN DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL 1 DAY) AND NOW()
															GROUP BY TransactionNo
														) Payments ON CBH.CreditBillHeaderID = Payments.CreditBillHeaderID
														WHERE CBH.BillingDate = dteBillingDate
														AND CBH.RunningCreditAmt > IFNULL(Payments.PaidAmount,0));

		CALL procsysAuditInsert(NOW(), 'SysPUser', 'sysHoldCustomerCreditWithG', 'localhost', CONCAT(strProcessRefNo,':done'));
	END IF;

	/*******************************
		CALL sysHoldCustomerCreditWithG();
	*******************************/

END;
GO
delimiter ;

