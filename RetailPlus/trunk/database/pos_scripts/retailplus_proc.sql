-- COLLATE ILLEGAL MIX ISSUE
ALTER DATABASE pos CHARACTER SET utf8;
ALTER DATABASE pos DEFAULT COLLATE utf8_general_ci;

SELECT default_character_set_name FROM information_schema.SCHEMATA S WHERE schema_name = 'pos';

/********************************************
	trgr_tblContacts_Insert

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblContacts_Insert
GO

CREATE TRIGGER trgr_tblContacts_Insert AFTER INSERT ON tblContacts
FOR EACH ROW 
BEGIN
	CALL procContactAuditInsert(NEW.ContactID ,NEW.ContactCode ,NEW.ContactName ,NEW.ContactGroupID ,NEW.ModeOfTerms ,NEW.Terms 
								,NEW.Address ,NEW.BusinessName ,NEW.TelephoneNo ,NEW.Remarks ,NEW.Debit ,NEW.Credit 
								,NEW.CreditLimit ,NEW.IsCreditAllowed ,NEW.DateCreated ,NEW.Deleted ,NEW.DepartmentID ,NEW.PositionID , NEW.isLock);
END;
GO

delimiter ;

********************************************/

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblContacts_Insert
GO
delimiter ;

/********************************************
	trgr_tblContacts_Update
********************************************/
delimiter GO
DROP TRIGGER IF EXISTS trgr_tblContacts_Update
GO

CREATE TRIGGER trgr_tblContacts_Update AFTER UPDATE ON tblContacts
FOR EACH ROW 
BEGIN
	CALL procContactAuditInsert(NEW.ContactID ,NEW.ContactCode ,NEW.ContactName ,NEW.ContactGroupID ,NEW.ModeOfTerms ,NEW.Terms 
								,NEW.Address ,NEW.BusinessName ,NEW.TelephoneNo ,NEW.Remarks ,NEW.Debit ,NEW.Credit 
								,NEW.CreditLimit ,NEW.IsCreditAllowed ,NEW.DateCreated ,NEW.Deleted ,NEW.DepartmentID ,NEW.PositionID , NEW.isLock);

END;
GO

delimiter ;


/********************************************
	procContactAuditInsert
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAuditInsert
GO

create procedure procContactAuditInsert(
	IN lngContactID BIGINT(20),
	IN strContactCode VARCHAR(25),
	IN strContactName VARCHAR(75),
	IN intContactGroupID INT(10),
	IN intModeOfTerms INT(10),
	IN intTerms INT(10),
	IN strAddress VARCHAR(150),
	IN strBusinessName varchar(75),
	IN strTelephoneNo varchar(75),
	IN strRemarks varchar(150),
	IN decDebit decimal(18,2),
	IN decCredit decimal(18,2),
	IN decCreditLimit decimal(18,2),
	IN decIsCreditAllowed tinyint(1),
	IN dteDateCreated datetime,
	IN intDeleted tinyint(1),
	IN intDepartmentID int(10),
	IN intPositionID int(10),
	IN intisLock tinyint(1)
)
BEGIN
	INSERT INTO tblContactsAudit (ContactID ,ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms 
			,Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit 
			,CreditLimit ,IsCreditAllowed ,DateCreated ,Deleted ,DepartmentID ,PositionID , isLock
			,AuditDateCreated)
	VALUES (lngContactID ,strContactCode ,strContactName ,intContactGroupID ,intModeOfTerms ,intTerms 
			,strAddress ,strBusinessName ,strTelephoneNo ,strRemarks ,decDebit ,decCredit 
			,decCreditLimit ,decIsCreditAllowed ,dteDateCreated ,intDeleted ,intDepartmentID ,intPositionID , intisLock
			 ,NOW());

END;
GO
delimiter ;


/********************************************
	trgr_tblProductInventory_Insert
********************************************/
delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProductInventory_Insert
GO

CREATE TRIGGER trgr_tblProductInventory_Insert AFTER INSERT ON tblProductInventory
FOR EACH ROW 
BEGIN
	CALL procProductInventoryAuditInsert(NEW.BranchID ,NEW.ProductID ,NEW.MatrixID ,NEW.Quantity ,NEW.QuantityIn 
										,NEW.QuantityOut ,NEW.ActualQuantity ,NEW.ReservedQuantity ,NEW.IsLock );
END;
GO

delimiter ;


/********************************************
	trgr_tblProductInventory_Update
********************************************/
delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProductInventory_Update
GO

CREATE TRIGGER trgr_tblProductInventory_Update AFTER UPDATE ON tblProductInventory
FOR EACH ROW 
BEGIN
	CALL procProductInventoryAuditInsert(NEW.BranchID ,NEW.ProductID ,NEW.MatrixID ,NEW.Quantity ,NEW.QuantityIn 
										,NEW.QuantityOut ,NEW.ActualQuantity ,NEW.ReservedQuantity ,NEW.IsLock );

END;
GO

delimiter ;



delimiter GO
DROP PROCEDURE IF EXISTS procProductInventoryAuditInsert
GO

create procedure procProductInventoryAuditInsert(
	IN intBranchID INT(4),
	IN intProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,3),
	IN decQuantityIn DECIMAL(18,3),
	IN decQuantityOut DECIMAL(18,3),
	IN decActualQuantity DECIMAL(18,3),
	IN decReservedQuantity DECIMAL(18,3),
	IN intIsLock TINYINT(1)
	)
BEGIN
	INSERT INTO tblProductInventoryAudit (
			 BranchID ,ProductID ,MatrixID ,Quantity ,QuantityIn ,QuantityOut ,ActualQuantity ,ReservedQuantity ,IsLock ,DateCreated)
	VALUES (intBranchID ,intProductID ,lngMatrixID ,decQuantity ,decQuantityIn ,decQuantityOut 
			,decActualQuantity ,decReservedQuantity ,intIsLock ,NOW());

END;
GO
delimiter ;




/******************************************************** END OF TRIGGERS *****************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procsysAuditInsert
GO

create procedure procsysAuditInsert(
	IN dteActivityDate DATETIME,
	IN strUser VARCHAR(80),
	IN strActivity VARCHAR(120),
	IN strIPAddress VARCHAR(15),
	IN strRemarks VARCHAR(8000)
	)
BEGIN
	
	INSERT INTO sysAuditTrail(ActivityDate, User, Activity, IPAddress, Remarks, BranchID, TerminalNo)
					   VALUES(dteActivityDate, strUser, strActivity, strIPAddress, strRemarks, 0, '00');

END;
GO
delimiter ;

/**************************************************************
	procGenerateSalesPerItem
	Lemuel E. Aceron
	CALL procGenerateSalesPerItem (1, '', '', '', '01', '2014-10-5 00:00', '2014-10-06 23:59');
	
	May 15, 2008 - 
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItem
GO

create procedure procGenerateSalesPerItem(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	

	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductID, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount, Discount, PurchasePrice, InvQuantity)
	SELECT strSessionID, ProductGroup,
		a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode) ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount,
		SUM(IF(TransactionItemStatus=3,-a.Discount + -a.TransactionDiscount,IF(TransactionItemStatus=4,-a.Discount + -a.TransactionDiscount,a.Discount + a.TransactionDiscount))) Discount,
		IFNULL(MIN(pkg.PurchasePrice), a.PurchasePrice) PurchasePrice,
		IFNULL(MAX(inv.InvQuantity),0) InvQuantity
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	LEFT OUTER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID AND pkg.Quantity = 1 AND a.ProductPackageID = pkg.PackageID
	LEFT OUTER JOIN (
		SELECT ProductID, SUM(Quantity) InvQuantity FROM tblProductInventory
		GROUP BY ProductID
	) inv ON a.ProductID = inv.ProductID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY ProductGroup, a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode), ProductUnitCode;

END;
GO
delimiter ;


/********************************************
	procCashierReportSyncTransactionSales

	CALL procCashierReportSyncTransactionSales( 1, '01', '2015-01-28 19:05:45', '2015-01-29 18:58:42');
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashierReportSyncTransactionSales
GO

create procedure procCashierReportSyncTransactionSales(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10),
	IN dteActualZReadDate DATETIME,
	IN dteNextZReadDate DATETIME
)
BEGIN
	
	UPDATE tblCashierReport 
	LEFT JOIN (
			select BranchID, TerminalNo, CashierID,
					SUM(CASE TransactionStatus 
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) NetSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN GrossSales WHEN 4 THEN GrossSales WHEN 5 THEN GrossSales WHEN 9 THEN GrossSales WHEN 11 THEN GrossSales ELSE 0
						END) GrossSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Subtotal WHEN 4 THEN Subtotal WHEN 5 THEN Subtotal WHEN 9 THEN Subtotal WHEN 11 THEN Subtotal ELSE 0
						END) SubTotal, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Discount WHEN 4 THEN Discount WHEN 5 THEN Discount WHEN 9 THEN Discount WHEN 11 THEN Discount ELSE 0
						END) Discount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemsDiscount WHEN 4 THEN ItemsDiscount WHEN 5 THEN ItemsDiscount WHEN 9 THEN ItemsDiscount WHEN 11 THEN ItemsDiscount ELSE 0
						END) ItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRItemsDiscount WHEN 4 THEN SNRItemsDiscount WHEN 5 THEN SNRItemsDiscount WHEN 9 THEN SNRItemsDiscount WHEN 11 THEN SNRItemsDiscount ELSE 0
						END) SNRItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDItemsDiscount WHEN 4 THEN PWDItemsDiscount WHEN 5 THEN PWDItemsDiscount WHEN 9 THEN PWDItemsDiscount WHEN 11 THEN PWDItemsDiscount ELSE 0
						END) PWDItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherItemsDiscount WHEN 4 THEN OtherItemsDiscount WHEN 5 THEN OtherItemsDiscount WHEN 9 THEN OtherItemsDiscount WHEN 11 THEN OtherItemsDiscount ELSE 0
						END) OtherItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRDiscount WHEN 4 THEN SNRDiscount WHEN 5 THEN SNRDiscount WHEN 9 THEN SNRDiscount WHEN 11 THEN SNRDiscount ELSE 0
						END) SNRDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDDiscount WHEN 4 THEN PWDDiscount WHEN 5 THEN PWDDiscount WHEN 9 THEN PWDDiscount WHEN 11 THEN PWDDiscount ELSE 0
						END) PWDDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherDiscount WHEN 4 THEN OtherDiscount WHEN 5 THEN OtherDiscount WHEN 9 THEN OtherDiscount WHEN 11 THEN OtherDiscount ELSE 0
						END) OtherDiscount,
					SUM(CASE TransactionStatus
							WHEN 1 THEN Charge WHEN 4 THEN Charge WHEN 5 THEN Charge WHEN 9 THEN Charge WHEN 11 THEN Charge ELSE 0
						END) TotalCharge, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) DailySales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemSold WHEN 4 THEN ItemSold WHEN 5 THEN ItemSold WHEN 9 THEN ItemSold WHEN 11 THEN ItemSold ELSE 0
						END) ItemSold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN QuantitySold WHEN 4 THEN QuantitySold WHEN 5 THEN QuantitySold WHEN 9 THEN QuantitySold WHEN 11 THEN QuantitySold ELSE 0
						END) QuantitySold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATExempt WHEN 4 THEN VATExempt WHEN 5 THEN VATExempt WHEN 9 THEN VATExempt WHEN 11 THEN VATExempt ELSE 0
						END) VATExempt, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonVATableAmount WHEN 4 THEN NonVATableAmount WHEN 5 THEN NonVATableAmount WHEN 9 THEN NonVATableAmount WHEN 11 THEN NonVATableAmount ELSE 0
						END) NonVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATableAmount WHEN 4 THEN VATableAmount WHEN 5 THEN VATableAmount WHEN 9 THEN VATableAmount WHEN 11 THEN VATableAmount ELSE 0
						END) VATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ZeroRatedSales WHEN 4 THEN ZeroRatedSales WHEN 5 THEN ZeroRatedSales WHEN 9 THEN ZeroRatedSales WHEN 11 THEN ZeroRatedSales ELSE 0
						END) ZeroRatedSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VAT WHEN 4 THEN VAT WHEN 5 THEN VAT WHEN 9 THEN VAT WHEN 11 THEN VAT ELSE 0
						END) VAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVATableAmount WHEN 4 THEN EVATableAmount WHEN 5 THEN EVATableAmount WHEN 9 THEN EVATableAmount WHEN 11 THEN EVATableAmount ELSE 0
						END) EVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonEVATableAmount WHEN 4 THEN NonEVATableAmount WHEN 5 THEN NonEVATableAmount WHEN 9 THEN NonEVATableAmount WHEN 11 THEN NonEVATableAmount ELSE 0
						END) NonEVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVAT WHEN 4 THEN EVAT WHEN 5 THEN EVAT WHEN 9 THEN EVAT WHEN 11 THEN EVAT ELSE 0
						END) EVAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN LocalTax WHEN 4 THEN LocalTax WHEN 5 THEN LocalTax WHEN 9 THEN LocalTax WHEN 11 THEN LocalTax ELSE 0
						END) LocalTax,
					SUM(CASE TransactionStatus
							WHEN 1 THEN CashPayment WHEN 4 THEN CashPayment WHEN 9 THEN CashPayment WHEN 11 THEN CashPayment ELSE 0
						END) CashSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ChequePayment WHEN 4 THEN ChequePayment WHEN 9 THEN ChequePayment WHEN 11 THEN ChequePayment ELSE 0
						END) ChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditCardPayment WHEN 4 THEN CreditCardPayment WHEN 9 THEN CreditCardPayment WHEN 11 THEN CreditCardPayment ELSE 0
						END) CreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditPayment WHEN 4 THEN CreditPayment WHEN 9 THEN CreditPayment WHEN 11 THEN CreditPayment ELSE 0
						END) CreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 1 THEN DebitPayment WHEN 4 THEN DebitPayment WHEN 9 THEN DebitPayment WHEN 11 THEN DebitPayment ELSE 0
						END) DebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN CashPayment ELSE 0
						END) RefundCashSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN ChequePayment ELSE 0
						END) RefundChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditCardPayment ELSE 0
						END) RefundCreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditPayment ELSE 0
						END) RefundCreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN DebitPayment ELSE 0
						END) RefundDebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment + ChequePayment + CreditCardPayment + DebitPayment ELSE 0
						END) CreditPayment,
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment ELSE 0
						END) CreditPaymentCash, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN ChequePayment ELSE 0
						END) CreditPaymentCheque, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN CreditCardPayment ELSE 0
						END) CreditPaymentCreditCard, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN DebitPayment ELSE 0
						END) CreditPaymentDebit, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardPointsPayment WHEN 4 THEN RewardPointsPayment WHEN 5 THEN RewardPointsPayment WHEN 9 THEN RewardPointsPayment WHEN 11 THEN RewardPointsPayment ELSE 0
						END) RewardPointsPayment,
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardConvertedPayment WHEN 4 THEN RewardConvertedPayment WHEN 5 THEN RewardConvertedPayment WHEN 9 THEN RewardConvertedPayment WHEN 11 THEN RewardConvertedPayment ELSE 0
						END) RewardConvertedPayment,
					SUM(CASE TransactionStatus
							WHEN 3 THEN Subtotal ELSE 0
						END) VoidSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN Subtotal ELSE 0
						END) RefundSales,
					SUM(CASE TransactionStatus
							WHEN 14 THEN Subtotal ELSE 0
						END) WalkInSales,	-- CashPayment lang ito, cannot pay using other payment types.
					SUM(CASE TransactionStatus
							WHEN 15 THEN Subtotal ELSE 0
						END) OutOfStockSales,
					SUM(CASE TransactionStatus
							WHEN 16 THEN Subtotal ELSE 0
						END) ConsignmentSales,
					SUM(CASE TransactionStatus
							WHEN 17 THEN Subtotal ELSE 0
						END) WalkInRefundSales,
					SUM(CASE TransactionStatus
							WHEN 18 THEN Subtotal ELSE 0
						END) OutOfStockRefundSales,
					SUM(CASE TransactionStatus
							WHEN 19 THEN Subtotal ELSE 0
						END) ConsignmentRefundSales
			FROM  tblTransactions
					WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
						AND TransactionStatus NOT IN (0,2) -- remove the open, suspended transactions
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s')
			GROUP BY BranchID, TerminalNo, CashierID
		) Trx ON tblCashierReport.BranchID = Trx.BranchID AND tblCashierReport.TerminalNo = Trx.TerminalNo AND tblCashierReport.CashierID = Trx.CashierID
	SET					
						tblCashierReport.NetSales							=  IFNULL(Trx.NetSales,0), 
						tblCashierReport.GrossSales							=  IFNULL(Trx.GrossSales,0), 
						tblCashierReport.TotalDiscount						=  IFNULL(Trx.Discount,0) + IFNULL(Trx.ItemsDiscount,0), 
						tblCashierReport.SNRDiscount					  	=  IFNULL(Trx.SNRDiscount,0), 
						tblCashierReport.PWDDiscount					  	=  IFNULL(Trx.PWDDiscount,0), 
						tblCashierReport.OtherDiscount					  	=  IFNULL(Trx.OtherDiscount,0),
						tblCashierReport.TotalCharge						=  IFNULL(Trx.TotalCharge,0), 
						tblCashierReport.DailySales							=  IFNULL(Trx.DailySales,0), 
						tblCashierReport.ItemSold							=  IFNULL(Trx.ItemSold,0), 
						tblCashierReport.QuantitySold						=  IFNULL(Trx.QuantitySold,0), 
						tblCashierReport.GroupSales							=  IFNULL(Trx.SubTotal,0), 
						tblCashierReport.VATExempt   						=  IFNULL(Trx.VATExempt,0), 
						tblCashierReport.NonVATableAmount					=  IFNULL(Trx.NonVATableAmount,0), 
						tblCashierReport.VATableAmount						=  IFNULL(Trx.VATableAmount,0), 
						tblCashierReport.ZeroRatedSales						=  IFNULL(Trx.ZeroRatedSales,0), 
						tblCashierReport.VAT								=  IFNULL(Trx.VAT,0), 
						tblCashierReport.EVATableAmount						=  IFNULL(Trx.EVATableAmount,0), 
						tblCashierReport.NonEVATableAmount					=  IFNULL(Trx.NonEVATableAmount,0), 
						tblCashierReport.EVAT								=  IFNULL(Trx.EVAT,0), 
						tblCashierReport.LocalTax							=  IFNULL(Trx.LocalTax,0), 
						tblCashierReport.CashSales							=  IFNULL(Trx.CashSales,0), 
						tblCashierReport.ChequeSales						=  IFNULL(Trx.ChequeSales,0), 
						tblCashierReport.CreditCardSales					=  IFNULL(Trx.CreditCardSales,0), 
						tblCashierReport.CreditSales						=  IFNULL(Trx.CreditSales,0), 
						tblCashierReport.DebitPayment						=  IFNULL(Trx.DebitPayment,0), 
						tblCashierReport.RefundCash							=  IFNULL(Trx.RefundCashSales,0), 
						tblCashierReport.RefundCheque						=  IFNULL(Trx.RefundChequeSales,0), 
						tblCashierReport.RefundCreditCard					=  IFNULL(Trx.RefundCreditCardSales,0), 
						tblCashierReport.RefundCredit						=  IFNULL(Trx.RefundCreditSales,0), 
						tblCashierReport.RefundDebit						=  IFNULL(Trx.RefundDebitPayment,0), 
						tblCashierReport.CreditPayment						=  IFNULL(Trx.CreditPayment,0), 
						tblCashierReport.CreditPaymentCash					=  IFNULL(Trx.CreditPaymentCash,0), 
						tblCashierReport.CreditPaymentCheque				=  IFNULL(Trx.CreditPaymentCheque,0), 
						tblCashierReport.CreditPaymentCreditCard			=  IFNULL(Trx.CreditPaymentCreditCard,0), 
						tblCashierReport.CreditPaymentDebit					=  IFNULL(Trx.CreditPaymentDebit,0), 
					
						tblCashierReport.RewardPointsPayment				=  IFNULL(Trx.RewardPointsPayment,0),
						tblCashierReport.RewardConvertedPayment				=  IFNULL(Trx.RewardConvertedPayment,0),
						tblCashierReport.CashInDrawer						=  IFNULL(Trx.CashSales,0) - (-IFNULL(Trx.RefundCashSales,0)) + IFNULL(Trx.CreditPaymentCash,0) + IFNULL(Trx.WalkInSales,0) + IFNULL(Trx.WalkInRefundSales,0) + tblCashierReport.BeginningBalance + tblCashierReport.TotalWithHold + tblCashierReport.TotalDeposit - tblCashierReport.TotalPaidOut - tblCashierReport.TotalDisburse, 
						tblCashierReport.VoidSales							=  IFNULL(Trx.VoidSales,0), 
						tblCashierReport.RefundSales						=  IFNULL(Trx.RefundSales,0), 
						tblCashierReport.ItemsDiscount						=  IFNULL(Trx.ItemsDiscount,0), 
						tblCashierReport.SNRItemsDiscount					=  IFNULL(Trx.SNRItemsDiscount,0), 
						tblCashierReport.PWDItemsDiscount					=  IFNULL(Trx.PWDItemsDiscount,0), 
						tblCashierReport.OtherItemsDiscount					=  IFNULL(Trx.OtherItemsDiscount,0), 
						tblCashierReport.SubTotalDiscount					=  IFNULL(Trx.Discount,0),

						tblCashierReport.ConsignmentSales					=  IFNULL(Trx.ConsignmentSales,0),
						tblCashierReport.ConsignmentRefundSales				=  IFNULL(Trx.ConsignmentRefundSales,0),
						tblCashierReport.WalkInSales						=  IFNULL(Trx.WalkInSales,0),
						tblCashierReport.WalkInRefundSales					=  IFNULL(Trx.WalkInRefundSales,0),
						tblCashierReport.OutOfStockSales					=  IFNULL(Trx.OutOfStockSales,0),
						tblCashierReport.OutOfStockRefundSales				=  IFNULL(Trx.OutOfStockRefundSales,0),

						tblCashierReport.IsProcessed						=  1	-- this must be set to 0 during salestransaction update
	WHERE tblCashierReport.BranchID = intBranchID AND tblCashierReport.TerminalNo = strTerminalNo
		AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
		AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s');
	
END;
GO
delimiter ;

/********************************************
	procTerminalReportSyncTransactionSales

	CALL procTerminalReportSyncTransactionSales( 1, '01');
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportSyncTransactionSales
GO

create procedure procTerminalReportSyncTransactionSales(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10)
)
BEGIN
	DECLARE dteActualZReadDate DATETIME DEFAULT NULL;
	DECLARE dteNextZReadDate DATETIME DEFAULT NULL;
	DECLARE boIsProcessed TINYINT(1) DEFAULT 0;
	DECLARE decSNRPercent DECIMAL(5,2) DEFAULT 0.20;
	DECLARE strSNRDiscountCode, strPWDDiscountCode VARCHAR(10) DEFAULT 'SNR';

	SET boIsProcessed = (SELECT IsProcessed FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

	IF (boIsProcessed = 0) THEN
		
		SET strSNRDiscountCode = (IFNULL((SELECT SeniorCitizenDiscountCode FROM tblTerminal WHERE TerminalNo='01' LIMIT 1), 'SNR'));
		SET strPWDDiscountCode = (IFNULL((SELECT PWDDiscountCode FROM tblTerminal WHERE TerminalNo='01' LIMIT 1), 'PWD'));
		SET decSNRPercent = (IFNULL((SELECT DiscountPrice FROM tblDiscount WHERE DiscountCode = strSNRDiscountCode), 20) / 100);

		SET dteActualZReadDate = (SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
		SET dteNextZReadDate = NOW();

		-- update the vat to make sure everything is correct
		UPDATE tblTransactions SET
			VATExempt = CASE DiscountCode WHEN strSNRDiscountCode THEN Discount / decSNRPercent ELSE 0 END,
			SNRDiscount = CASE DiscountCode WHEN strSNRDiscountCode THEN Discount ELSE 0 END,
			PWDDiscount = CASE DiscountCode WHEN strPWDDiscountCode THEN Discount ELSE 0 END,
			OtherDiscount = CASE DiscountCode WHEN strSNRDiscountCode THEN 0 WHEN strPWDDiscountCode THEN 0 ELSE Discount END
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s');

		UPDATE tblTransactions SET
			GrossSales = SubTotal + itemsdiscount,
			VatableAmount = (SubTotal - Discount - VATExempt - (VATExempt * 0.12) - NonVATableAmount) / 1.12,
			VAT = (SubTotal - Discount - VATExempt - (VATExempt * 0.12) - NonVATableAmount) / 1.12 * 0.12,
			NetSales = SubTotal - (VATExempt * 0.12) - Discount
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s');

		CALL procCashierReportSyncTransactionSales(intBranchID, strTerminalNo, dteActualZReadDate, dteNextZReadDate);

		UPDATE tblTerminalReport
		LEFT JOIN (
				select BranchID, TerminalNo, 
						SUM(CASE TransactionStatus 
								WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
							END) NetSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN GrossSales WHEN 4 THEN GrossSales WHEN 5 THEN GrossSales WHEN 9 THEN GrossSales WHEN 11 THEN GrossSales ELSE 0
							END) GrossSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN Subtotal WHEN 4 THEN Subtotal WHEN 5 THEN Subtotal WHEN 9 THEN Subtotal WHEN 11 THEN Subtotal ELSE 0
							END) SubTotal, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN Discount WHEN 4 THEN Discount WHEN 5 THEN Discount WHEN 9 THEN Discount WHEN 11 THEN Discount ELSE 0
							END) Discount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN ItemsDiscount WHEN 4 THEN ItemsDiscount WHEN 5 THEN ItemsDiscount WHEN 9 THEN ItemsDiscount WHEN 11 THEN ItemsDiscount ELSE 0
							END) ItemsDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN SNRItemsDiscount WHEN 4 THEN SNRItemsDiscount WHEN 5 THEN SNRItemsDiscount WHEN 9 THEN SNRItemsDiscount WHEN 11 THEN SNRItemsDiscount ELSE 0
							END) SNRItemsDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN PWDItemsDiscount WHEN 4 THEN PWDItemsDiscount WHEN 5 THEN PWDItemsDiscount WHEN 9 THEN PWDItemsDiscount WHEN 11 THEN PWDItemsDiscount ELSE 0
							END) PWDItemsDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN OtherItemsDiscount WHEN 4 THEN OtherItemsDiscount WHEN 5 THEN OtherItemsDiscount WHEN 9 THEN OtherItemsDiscount WHEN 11 THEN OtherItemsDiscount ELSE 0
							END) OtherItemsDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN SNRDiscount WHEN 4 THEN SNRDiscount WHEN 5 THEN SNRDiscount WHEN 9 THEN SNRDiscount WHEN 11 THEN SNRDiscount ELSE 0
							END) SNRDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN PWDDiscount WHEN 4 THEN PWDDiscount WHEN 5 THEN PWDDiscount WHEN 9 THEN PWDDiscount WHEN 11 THEN PWDDiscount ELSE 0
							END) PWDDiscount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN OtherDiscount WHEN 4 THEN OtherDiscount WHEN 5 THEN OtherDiscount WHEN 9 THEN OtherDiscount WHEN 11 THEN OtherDiscount ELSE 0
							END) OtherDiscount,
						SUM(CASE TransactionStatus
								WHEN 1 THEN Charge WHEN 4 THEN Charge WHEN 5 THEN Charge WHEN 9 THEN Charge WHEN 11 THEN Charge ELSE 0
							END) TotalCharge, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
							END) DailySales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN ItemSold WHEN 4 THEN ItemSold WHEN 5 THEN ItemSold WHEN 9 THEN ItemSold WHEN 11 THEN ItemSold ELSE 0
							END) ItemSold,
						SUM(CASE TransactionStatus
								WHEN 1 THEN QuantitySold WHEN 4 THEN QuantitySold WHEN 5 THEN QuantitySold WHEN 9 THEN QuantitySold WHEN 11 THEN QuantitySold ELSE 0
							END) QuantitySold,
						SUM(CASE TransactionStatus
								WHEN 1 THEN VATExempt WHEN 4 THEN VATExempt WHEN 5 THEN VATExempt WHEN 9 THEN VATExempt WHEN 11 THEN VATExempt ELSE 0
							END) VATExempt, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN NonVATableAmount WHEN 4 THEN NonVATableAmount WHEN 5 THEN NonVATableAmount WHEN 9 THEN NonVATableAmount WHEN 11 THEN NonVATableAmount ELSE 0
							END) NonVATableAmount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN VATableAmount WHEN 4 THEN VATableAmount WHEN 5 THEN VATableAmount WHEN 9 THEN VATableAmount WHEN 11 THEN VATableAmount ELSE 0
							END) VATableAmount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN ZeroRatedSales WHEN 4 THEN ZeroRatedSales WHEN 5 THEN ZeroRatedSales WHEN 9 THEN ZeroRatedSales WHEN 11 THEN ZeroRatedSales ELSE 0
							END) ZeroRatedSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN VAT WHEN 4 THEN VAT WHEN 5 THEN VAT WHEN 9 THEN VAT WHEN 11 THEN VAT ELSE 0
							END) VAT, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN EVATableAmount WHEN 4 THEN EVATableAmount WHEN 5 THEN EVATableAmount WHEN 9 THEN EVATableAmount WHEN 11 THEN EVATableAmount ELSE 0
							END) EVATableAmount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN NonEVATableAmount WHEN 4 THEN NonEVATableAmount WHEN 5 THEN NonEVATableAmount WHEN 9 THEN NonEVATableAmount WHEN 11 THEN NonEVATableAmount ELSE 0
							END) NonEVATableAmount, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN EVAT WHEN 4 THEN EVAT WHEN 5 THEN EVAT WHEN 9 THEN EVAT WHEN 11 THEN EVAT ELSE 0
							END) EVAT, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN LocalTax WHEN 4 THEN LocalTax WHEN 5 THEN LocalTax WHEN 9 THEN LocalTax WHEN 11 THEN LocalTax ELSE 0
							END) LocalTax,
						SUM(CASE TransactionStatus
								WHEN 1 THEN CashPayment WHEN 4 THEN CashPayment WHEN 9 THEN CashPayment WHEN 11 THEN CashPayment ELSE 0
							END) CashSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN ChequePayment WHEN 4 THEN ChequePayment WHEN 9 THEN ChequePayment WHEN 11 THEN ChequePayment ELSE 0
							END) ChequeSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN CreditCardPayment WHEN 4 THEN CreditCardPayment WHEN 9 THEN CreditCardPayment WHEN 11 THEN CreditCardPayment ELSE 0
							END) CreditCardSales, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN CreditPayment WHEN 4 THEN CreditPayment WHEN 9 THEN CreditPayment WHEN 11 THEN CreditPayment ELSE 0
							END) CreditSales, -- creditpayment for normal transactions
						SUM(CASE TransactionStatus
								WHEN 1 THEN DebitPayment WHEN 4 THEN DebitPayment WHEN 9 THEN DebitPayment WHEN 11 THEN DebitPayment ELSE 0
							END) DebitPayment, -- debit for normal transactions
						SUM(CASE TransactionStatus
								WHEN 5 THEN CashPayment ELSE 0
							END) RefundCashSales, 
						SUM(CASE TransactionStatus
								WHEN 5 THEN ChequePayment ELSE 0
							END) RefundChequeSales, 
						SUM(CASE TransactionStatus
								WHEN 5 THEN CreditCardPayment ELSE 0
							END) RefundCreditCardSales, 
						SUM(CASE TransactionStatus
								WHEN 5 THEN CreditPayment ELSE 0
							END) RefundCreditSales, -- creditpayment for normal transactions
						SUM(CASE TransactionStatus
								WHEN 5 THEN DebitPayment ELSE 0
							END) RefundDebitPayment, -- debit for normal transactions
						SUM(CASE TransactionStatus
								WHEN 7 THEN CashPayment + ChequePayment + CreditCardPayment + DebitPayment ELSE 0
							END) CreditPayment,
						SUM(CASE TransactionStatus
								WHEN 7 THEN CashPayment ELSE 0
							END) CreditPaymentCash, 
						SUM(CASE TransactionStatus
								WHEN 7 THEN ChequePayment ELSE 0
							END) CreditPaymentCheque, 
						SUM(CASE TransactionStatus
								WHEN 7 THEN CreditCardPayment ELSE 0
							END) CreditPaymentCreditCard, 
						SUM(CASE TransactionStatus
								WHEN 7 THEN DebitPayment ELSE 0
							END) CreditPaymentDebit, 
						SUM(CASE TransactionStatus
								WHEN 1 THEN RewardPointsPayment WHEN 4 THEN RewardPointsPayment WHEN 5 THEN RewardPointsPayment WHEN 9 THEN RewardPointsPayment WHEN 11 THEN RewardPointsPayment ELSE 0
							END) RewardPointsPayment,
						SUM(CASE TransactionStatus
								WHEN 1 THEN RewardConvertedPayment WHEN 4 THEN RewardConvertedPayment WHEN 5 THEN RewardConvertedPayment WHEN 9 THEN RewardConvertedPayment WHEN 11 THEN RewardConvertedPayment ELSE 0
							END) RewardConvertedPayment,
						SUM(CASE TransactionStatus
								WHEN 3 THEN Subtotal ELSE 0
							END) VoidSales, 
						SUM(CASE TransactionStatus
								WHEN 5 THEN Subtotal ELSE 0
							END) RefundSales,
						SUM(CASE TransactionStatus
								WHEN 14 THEN Subtotal ELSE 0
							END) WalkInSales,
						SUM(CASE TransactionStatus
								WHEN 15 THEN Subtotal ELSE 0
							END) OutOfStockSales,
						SUM(CASE TransactionStatus
								WHEN 16 THEN Subtotal ELSE 0
							END) ConsignmentSales,
						SUM(CASE TransactionStatus
								WHEN 17 THEN Subtotal ELSE 0
							END) WalkInRefundSales,
						SUM(CASE TransactionStatus
								WHEN 18 THEN Subtotal ELSE 0
							END) OutOfStockRefundSales,
						SUM(CASE TransactionStatus
								WHEN 19 THEN Subtotal ELSE 0
							END) ConsignmentRefundSales
				FROM  tblTransactions
						WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
							AND TransactionStatus NOT IN (0,2) -- remove the open, suspended transactions
							AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
							AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s')
				GROUP BY BranchID, TerminalNo
			) Trx ON tblTerminalReport.BranchID = Trx.BranchID AND tblTerminalReport.TerminalNo = Trx.TerminalNo
		SET					
							tblTerminalReport.ActualNewGrandTotal				=  tblTerminalReport.ActualOldGrandTotal + IFNULL(Trx.GrossSales,0),
							tblTerminalReport.NewGrandTotal						=  tblTerminalReport.OldGrandTotal + (IFNULL(Trx.GrossSales,0) * ((100-TrustFund)/100)),
							tblTerminalReport.NetSales							=  IFNULL(Trx.NetSales,0), 
							tblTerminalReport.GrossSales						=  IFNULL(Trx.GrossSales,0),
							tblTerminalReport.TotalDiscount						=  IFNULL(Trx.Discount,0) + IFNULL(Trx.ItemsDiscount,0), 
							tblTerminalReport.SNRDiscount					  	=  IFNULL(Trx.SNRDiscount,0), 
							tblTerminalReport.PWDDiscount					  	=  IFNULL(Trx.PWDDiscount,0), 
							tblTerminalReport.OtherDiscount					  	=  IFNULL(Trx.OtherDiscount,0),
							tblTerminalReport.TotalCharge						=  IFNULL(Trx.TotalCharge,0), 
							tblTerminalReport.DailySales						=  IFNULL(Trx.DailySales,0), 
							tblTerminalReport.ItemSold							=  IFNULL(Trx.ItemSold,0), 
							tblTerminalReport.QuantitySold						=  IFNULL(Trx.QuantitySold,0), 
							tblTerminalReport.GroupSales						=  IFNULL(Trx.NetSales,0), -- IFNULL(Trx.SubTotal,0), 
							tblTerminalReport.VATExempt   						=  IFNULL(Trx.VATExempt,0), 
							tblTerminalReport.NonVATableAmount					=  IFNULL(Trx.NonVATableAmount,0), 
							tblTerminalReport.VATableAmount						=  IFNULL(Trx.VATableAmount,0), 
							tblTerminalReport.ZeroRatedSales					=  IFNULL(Trx.ZeroRatedSales,0), 
							tblTerminalReport.VAT								=  IFNULL(Trx.VAT,0), 
							tblTerminalReport.EVATableAmount					=  IFNULL(Trx.EVATableAmount,0), 
							tblTerminalReport.NonEVATableAmount					=  IFNULL(Trx.NonEVATableAmount,0), 
							tblTerminalReport.EVAT								=  IFNULL(Trx.EVAT,0), 
							tblTerminalReport.LocalTax							=  IFNULL(Trx.LocalTax,0), 
							tblTerminalReport.CashSales							=  IFNULL(Trx.CashSales,0), 
							tblTerminalReport.ChequeSales						=  IFNULL(Trx.ChequeSales,0), 
							tblTerminalReport.CreditCardSales					=  IFNULL(Trx.CreditCardSales,0), 
							tblTerminalReport.CreditSales						=  IFNULL(Trx.CreditSales,0), 
							tblTerminalReport.DebitPayment						=  IFNULL(Trx.DebitPayment,0),
							tblTerminalReport.RefundCash						=  IFNULL(Trx.RefundCashSales,0), 
							tblTerminalReport.RefundCheque						=  IFNULL(Trx.RefundChequeSales,0), 
							tblTerminalReport.RefundCreditCard					=  IFNULL(Trx.RefundCreditCardSales,0), 
							tblTerminalReport.RefundCredit						=  IFNULL(Trx.RefundCreditSales,0), 
							tblTerminalReport.RefundDebit						=  IFNULL(Trx.RefundDebitPayment,0),
							tblTerminalReport.CreditPayment						=  IFNULL(Trx.CreditPayment,0), 
							tblTerminalReport.CreditPaymentCash					=  IFNULL(Trx.CreditPaymentCash,0), 
							tblTerminalReport.CreditPaymentCheque				=  IFNULL(Trx.CreditPaymentCheque,0), 
							tblTerminalReport.CreditPaymentCreditCard			=  IFNULL(Trx.CreditPaymentCreditCard,0), 
							tblTerminalReport.CreditPaymentDebit				=  IFNULL(Trx.CreditPaymentDebit,0), 
					
							tblTerminalReport.RewardPointsPayment				=  IFNULL(Trx.RewardPointsPayment,0),
							tblTerminalReport.RewardConvertedPayment			=  IFNULL(Trx.RewardConvertedPayment,0),
							tblTerminalReport.CashInDrawer						=  IFNULL(Trx.CashSales,0) - (-IFNULL(Trx.RefundCashSales,0)) + IFNULL(Trx.CreditPaymentCash,0) + IFNULL(Trx.WalkInSales,0) + IFNULL(Trx.WalkInRefundSales,0) + tblTerminalReport.BeginningBalance + tblTerminalReport.TotalWithHold + tblTerminalReport.TotalDeposit - tblTerminalReport.TotalPaidOut - tblTerminalReport.TotalDisburse, 
							tblTerminalReport.VoidSales							=  IFNULL(Trx.VoidSales,0), 
							tblTerminalReport.RefundSales						=  IFNULL(Trx.RefundSales,0), 
							tblTerminalReport.ItemsDiscount						=  IFNULL(Trx.ItemsDiscount,0), 
							tblTerminalReport.SNRItemsDiscount					=  IFNULL(Trx.SNRItemsDiscount,0), 
							tblTerminalReport.PWDItemsDiscount					=  IFNULL(Trx.PWDItemsDiscount,0), 
							tblTerminalReport.OtherItemsDiscount				=  IFNULL(Trx.OtherItemsDiscount,0), 
							tblTerminalReport.SubTotalDiscount					=  IFNULL(Trx.Discount,0),

							tblTerminalReport.ConsignmentSales					=  IFNULL(Trx.ConsignmentSales,0),
							tblTerminalReport.ConsignmentRefundSales			=  IFNULL(Trx.ConsignmentRefundSales,0),
							tblTerminalReport.WalkInSales						=  IFNULL(Trx.WalkInSales,0),
							tblTerminalReport.WalkInRefundSales					=  IFNULL(Trx.WalkInRefundSales,0),
							tblTerminalReport.OutOfStockSales					=  IFNULL(Trx.OutOfStockSales,0),
							tblTerminalReport.OutOfStockRefundSales				=  IFNULL(Trx.OutOfStockRefundSales,0),

							tblTerminalReport.IsProcessed						=  1	-- this must be set to 0 during salestransaction update
		WHERE tblTerminalReport.BranchID = intBranchID AND tblTerminalReport.TerminalNo = strTerminalNo
			AND DATE_FORMAT(tblTerminalReport.DateLastInitialized, '%Y-%m-%d %H:%i:%s') = DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s');

		CALL procsysAuditInsert(NOW(), 'RetailPlus Admin', 'RESYNC TERMINAL REPORT', 'localhost', CONCAT('TR has been re-run @ BranchID:', intBranchID, ' TerminalNo:', strTerminalNo,'.'));

	END IF;
	
END;
GO
delimiter ;

/********************************************
	procTerminalReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportUpdateTransactionSales
GO

create procedure procTerminalReportUpdateTransactionSales(IN intBranchID int(4), IN strTerminalNo varchar(10), 
														IN decNetSales decimal(18,2),
														IN decGrossSales decimal(18,2),
														IN decTotalDiscount decimal(18,2),
														IN decSNRDiscount decimal(18,2),
														IN decPWDDiscount decimal(18,2),
														IN decOtherDiscount decimal(18,2),
														IN decTotalCharge decimal(18,2),
														IN decDailySales decimal(18,2),
														IN decItemSold decimal(18,2),
														IN decQuantitySold decimal(18,2),
														IN decGroupSales decimal(18,2),
														IN decOldGrandTotal decimal(18,2),
														IN decNewGrandTotal decimal(18,2),
														IN decVATExempt decimal(18,2),
														IN decNonVATableAmount decimal(18,2),
														IN decVATableAmount decimal(18,2),
														IN decZeroRatedSales decimal(18,2),
														IN decVAT decimal(18,2),
														IN decEVATableAmount decimal(18,2),
														IN decNonEVATableAmount decimal(18,2),
														IN decEVAT decimal(18,2),
														IN decLocalTax decimal(18,2),
														IN decCashSales decimal(18,2),
														IN decChequeSales decimal(18,2),
														IN decCreditCardSales decimal(18,2),
														IN decCreditSales decimal(18,2),
														IN decCreditPayment decimal(18,2),
														IN decCreditPaymentCash decimal(18,2),
														IN decCreditPaymentCheque decimal(18,2),
														IN decCreditPaymentCreditCard decimal(18,2),
														IN decCreditPaymentDebit decimal(18,2),
														IN decDebitPayment decimal(18,2),
														IN decRewardPointsPayment decimal(18,2),
														IN decRewardConvertedPayment decimal(18,2),
														IN decCashInDrawer decimal(18,2),
														IN decVoidSales decimal(18,2),
														IN decRefundSales decimal(18,2),
														IN decItemsDiscount decimal(18,2),
														IN decSNRItemsDiscount decimal(18,2),
														IN decPWDItemsDiscount decimal(18,2),
														IN decOtherItemsDiscount decimal(18,2),
														IN decSubTotalDiscount decimal(18,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfRewardPointsPayment int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(10),
														IN intNoOfNegativeAdjustmentTransactions  int(10),
														IN intNoOfConsignmentTransactions  int(10),
														IN intNoOfConsignmentRefundTransactions  int(10),
														IN intNoOfWalkInTransactions  int(10),
														IN intNoOfWalkInRefundTransactions  int(10),
														IN intNoOfOutOfStockTransactions  int(10),
														IN intNoOfOutOfStockRefundTransactions  int(10),
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN

	UPDATE tblTerminalReport SET 
					NetSales							=  NetSales								+  decNetSales, 
					GrossSales							=  GrossSales							+  decGrossSales, 
					TotalDiscount						=  TotalDiscount						+  decTotalDiscount, 
					SNRDiscount							=  SNRDiscount							+  decSNRDiscount, 
					PWDDiscount							=  PWDDiscount							+  decPWDDiscount, 
					OtherDiscount						=  OtherDiscount						+  decOtherDiscount, 
					TotalCharge							=  TotalCharge							+  decTotalCharge, 
					DailySales							=  DailySales							+  decDailySales, 
					ItemSold							=  ItemSold								+  decItemSold, 
					QuantitySold						=  QuantitySold							+  decQuantitySold, 
					GroupSales							=  GroupSales							+  decGroupSales, 
					OldGrandTotal						=  OldGrandTotal						+  decOldGrandTotal, 
					NewGrandTotal						=  NewGrandTotal						+  decNewGrandTotal,
					ActualNewGrandTotal					=  ActualNewGrandTotal					+  decNewGrandTotal, 
					VATExempt							=  VATExempt							+  decVATExempt, 
					NonVATableAmount					=  NonVATableAmount						+  decNonVATableAmount, 
					VATableAmount						=  VATableAmount						+  decVATableAmount, 
					ZeroRatedSales						=  ZeroRatedSales						+  decZeroRatedSales, 
					VAT									=  VAT									+  decVAT, 
					EVATableAmount						=  EVATableAmount						+  decEVATableAmount, 
					NonEVATableAmount					=  NonEVATableAmount					+  decNonEVATableAmount, 
					EVAT								=  EVAT									+  decEVAT, 
					LocalTax							=  LocalTax								+  decLocalTax, 
					CashSales							=  CashSales							+  decCashSales, 
					ChequeSales							=  ChequeSales							+  decChequeSales, 
					CreditCardSales						=  CreditCardSales						+  decCreditCardSales, 
					CreditSales							=  CreditSales							+  decCreditSales, 
					CreditPayment						=  CreditPayment						+  decCreditPayment, 
					
					-- need to insert refund breakdown

					CreditPaymentCash					=  CreditPaymentCash					+  decCreditPaymentCash, 
					CreditPaymentCheque					=  CreditPaymentCheque					+  decCreditPaymentCheque, 
					CreditPaymentCreditCard				=  CreditPaymentCreditCard				+  decCreditPaymentCreditCard, 
					CreditPaymentDebit					=  CreditPaymentDebit					+  decCreditPaymentDebit, 

					DebitPayment						=  DebitPayment						    +  decDebitPayment, 
					RewardPointsPayment					=  RewardPointsPayment					+  decRewardPointsPayment, 
					RewardConvertedPayment				=  RewardConvertedPayment			    +  decRewardConvertedPayment, 
					CashInDrawer						=  CashInDrawer							+  decCashInDrawer, 
					VoidSales							=  VoidSales							+  decVoidSales, 
					RefundSales							=  RefundSales							+  decRefundSales, 
					ItemsDiscount						=  ItemsDiscount						+  decItemsDiscount, 
					SNRItemsDiscount					=  SNRItemsDiscount						+  decSNRItemsDiscount, 
					PWDItemsDiscount					=  PWDItemsDiscount						+  decPWDItemsDiscount, 
					OtherItemsDiscount					=  OtherItemsDiscount					+  decOtherItemsDiscount, 
					SubTotalDiscount					=  SubTotalDiscount						+  decSubTotalDiscount, 

					NoOfCashTransactions				=  NoOfCashTransactions					+  intNoOfCashTransactions, 
					NoOfChequeTransactions				=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
					NoOfCreditCardTransactions			=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
					NoOfCreditTransactions				=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions	=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions		=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
					NoOfDebitPaymentTransactions		=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
					NoOfClosedTransactions				=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
					NoOfRefundTransactions				=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 
					NoOfVoidTransactions				=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
					NoOfRewardPointsPayment				=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment, 
					NoOfTotalTransactions				=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
					NoOfDiscountedTransactions			=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
					NegativeAdjustments					=  NegativeAdjustments					+  decNegativeAdjustments,
					NoOfNegativeAdjustmentTransactions	=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,
					NoOfConsignmentTransactions			=  NoOfConsignmentTransactions			+  intNoOfConsignmentTransactions,
					NoOfConsignmentRefundTransactions	=  NoOfConsignmentRefundTransactions	+  intNoOfConsignmentRefundTransactions,
					NoOfWalkInTransactions				=  NoOfWalkInTransactions				+  intNoOfWalkInTransactions,
					NoOfWalkInRefundTransactions		=  NoOfWalkInRefundTransactions			+  intNoOfWalkInRefundTransactions,
					NoOfOutOfStockTransactions			=  NoOfOutOfStockTransactions			+  intNoOfOutOfStockTransactions,
					NoOfOutOfStockRefundTransactions	=  NoOfOutOfStockRefundTransactions		+  intNoOfOutOfStockRefundTransactions,

					-- 25Feb2015 : no need for this, already recomputed in procCashierReportSyncTransactionSales after zread
					-- ConsignmentTransactions					=  ConsignmentTransactions			+  decConsignmentTransactions,
					-- ConsignmentRefundTransactions			=  ConsignmentRefundTransactions	+  decConsignmentRefundTransactions,
					-- WalkInTransactions						=  WalkInTransactions				+  decWalkInTransactions,
					-- WalkInRefundTransactions					=  WalkInRefundTransactions			+  decWalkInRefundTransactions,
					-- OutOfStockTransactions					=  OutOfStockTransactions			+  decOutOfStockTransactions,
					-- OutOfStockRefundTransactions				=  OutOfStockRefundTransactions		+  decOutOfStockRefundTransactions,

					PromotionalItems					=  PromotionalItems						+  decPromotionalItems,
					CreditSalesTax						=  CreditSalesTax						+  decCreditSalesTax,
					IsProcessed							=  0	-- this must be set to 0 during transaction update
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/********************************************
	procTerminalReportIncrementBatchCounter
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportIncrementBatchCounter
GO

create procedure procTerminalReportIncrementBatchCounter(
	IN intBranchID int(4),
	IN strTerminalNo varchar(10)
)
BEGIN

	UPDATE tblTerminalReport SET 
				BatchCounter	=  BatchCounter	+  1
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;


/********************************************
	procTerminalReportUpdateTrustFund
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportUpdateTrustFund
GO

create procedure procTerminalReportUpdateTrustFund(
	IN intBranchID int(4),
	IN strTerminalNo varchar(10),
	IN decTrustFund decimal(18,3),
	IN strUpdatedBy varchar(150),
	IN strReason varchar(4000)
)
BEGIN
	DECLARE decOldTrustFund DECIMAL(18,3) DEFAULT 0;

	SET decOldTrustFund = (SELECT TrustFund FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

	UPDATE tblTerminalReport SET TrustFund = decTrustFund;

	CALL procsysAuditInsert(NOW(), strUpdatedBy, 'TRUSTFUND OVERRIDE', 'localhost', CONCAT('TrustFund was overwritten from ',decOldTrustFund,' to ',decTrustFund,' due to ',strReason,' @ BranchID:', intBranchID, ' TerminalNo:', strTerminalNo));

END;
GO
delimiter ;

/********************************************
	procCashierReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashierReportUpdateTransactionSales
GO

create procedure procCashierReportUpdateTransactionSales(IN intBranchID INT(4), IN strTerminalNo varchar(10), IN lngCashierID int(10),
														IN decNetSales decimal(18,2),
														IN decGrossSales decimal(18,2),
														IN decTotalDiscount decimal(18,2),
														IN decSNRDiscount decimal(18,2),
														IN decPWDDiscount decimal(18,2),
														IN decOtherDiscount decimal(18,2),
														IN decTotalCharge decimal(18,2),
														IN decDailySales decimal(18,2),
														IN decItemSold decimal(18,2),
														IN decQuantitySold decimal(18,2),
														IN decGroupSales decimal(18,2),
														IN decVATExempt decimal(18,2),
														IN decNonVATableAmount decimal(18,2),
														IN decVATableAmount decimal(18,2),
														IN decZeroRatedSales decimal(18,2),
														IN decVAT decimal(18,2),
														IN decEVATableAmount decimal(18,2),
														IN decNonEVATableAmount decimal(18,2),
														IN decEVAT decimal(18,2),
														IN decLocalTax decimal(18,2),
														IN decCashSales decimal(18,2),
														IN decChequeSales decimal(18,2),
														IN decCreditCardSales decimal(18,2),
														IN decCreditSales decimal(18,2),
														IN decCreditPayment decimal(18,2),
														IN decCreditPaymentCash decimal(18,2),
														IN decCreditPaymentCheque decimal(18,2),
														IN decCreditPaymentCreditCard decimal(18,2),
														IN decCreditPaymentDebit decimal(18,2),
														IN decDebitPayment decimal(18,2),
														IN decRewardPointsPayment decimal(18,2),
														IN decRewardConvertedPayment decimal(18,2),
														IN decCashInDrawer decimal(18,2),
														IN decVoidSales decimal(18,2),
														IN decRefundSales decimal(18,2),
														IN decItemsDiscount decimal(18,2),
														IN decSNRItemsDiscount decimal(18,2),
														IN decPWDItemsDiscount decimal(18,2),
														IN decOtherItemsDiscount decimal(18,2),
														IN decSubTotalDiscount decimal(18,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfRewardPointsPayment int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(18,3),
														IN intNoOfNegativeAdjustmentTransactions  int(10),

														IN intNoOfConsignmentTransactions  int(10),
														IN intNoOfConsignmentRefundTransactions  int(10),
														IN intNoOfWalkInTransactions  int(10),
														IN intNoOfWalkInRefundTransactions  int(10),
														IN intNoOfOutOfStockTransactions  int(10),
														IN intNoOfOutOfStockRefundTransactions  int(10),
														
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN
	UPDATE tblCashierReport SET 
		NetSales								=  NetSales								+  decNetSales, 
		GrossSales								=  GrossSales							+  decGrossSales, 
		TotalDiscount							=  TotalDiscount						+  decTotalDiscount, 
		SNRDiscount								=  SNRDiscount							+  decSNRDiscount, 
		PWDDiscount								=  PWDDiscount							+  decPWDDiscount, 
		OtherDiscount							=  OtherDiscount						+  decOtherDiscount, 
		TotalCharge								=  TotalCharge							+  decTotalCharge, 
		DailySales								=  DailySales							+  decDailySales, 
		ItemSold								=  ItemSold								+  decItemSold, 
		QuantitySold							=  QuantitySold							+  decQuantitySold, 
		GroupSales								=  GroupSales							+  decGroupSales, 
		
		VATExempt								=  VATExempt							+  decVATExempt, 
		NonVATableAmount						=  NonVATableAmount						+  decNonVATableAmount, 
		VATableAmount							=  VATableAmount						+  decVATableAmount, 
		ZeroRatedSales							=  ZeroRatedSales						+  decZeroRatedSales, 
		VAT										=  VAT									+  decVAT, 
		EVATableAmount							=  EVATableAmount						+  decEVATableAmount, 
		NonEVATableAmount						=  NonEVATableAmount					+  decNonEVATableAmount, 
		EVAT									=  EVAT									+  decEVAT, 
		LocalTax								=  LocalTax								+  decLocalTax, 

		CashSales								=  CashSales							+  decCashSales, 
		ChequeSales								=  ChequeSales							+  decChequeSales, 
		CreditCardSales							=  CreditCardSales						+  decCreditCardSales, 
		CreditSales								=  CreditSales							+  decCreditSales, 
		CreditPayment							=  CreditPayment						+  decCreditPayment, 

		CreditPaymentCash						=  CreditPaymentCash					+  decCreditPaymentCash, 
		CreditPaymentCheque						=  CreditPaymentCheque					+  decCreditPaymentCheque, 
		CreditPaymentCreditCard					=  CreditPaymentCreditCard				+  decCreditPaymentCreditCard, 
		CreditPaymentDebit						=  CreditPaymentDebit					+  decCreditPaymentDebit, 

		DebitPayment							=  DebitPayment						   	+  decDebitPayment, 
		RewardPointsPayment						=  RewardPointsPayment					+  decRewardPointsPayment, 
		RewardConvertedPayment					=  RewardConvertedPayment				+  decRewardConvertedPayment, 
		CashInDrawer							=  CashInDrawer							+  decCashInDrawer, 
		VoidSales								=  VoidSales							+  decVoidSales, 
		RefundSales								=  RefundSales							+  decRefundSales, 
		ItemsDiscount							=  ItemsDiscount						+  decItemsDiscount, 
		SNRItemsDiscount						=  SNRItemsDiscount						+  decSNRItemsDiscount, 
		PWDItemsDiscount						=  PWDItemsDiscount						+  decPWDItemsDiscount, 
		OtherItemsDiscount						=  OtherItemsDiscount					+  decOtherItemsDiscount, 
		SubTotalDiscount						=  SubTotalDiscount						+  decSubTotalDiscount, 
		NoOfCashTransactions					=  NoOfCashTransactions					+  intNoOfCashTransactions, 
		NoOfChequeTransactions					=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
		NoOfCreditCardTransactions				=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
		NoOfCreditTransactions					=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
		NoOfCombinationPaymentTransactions		=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
		NoOfCreditPaymentTransactions			=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
		NoOfDebitPaymentTransactions			=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
		NoOfClosedTransactions					=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
		NoOfRefundTransactions					=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 			
		NoOfVoidTransactions					=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
		NoOfRewardPointsPayment					=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment,
		NoOfTotalTransactions					=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
		NoOfDiscountedTransactions				=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
		NegativeAdjustments						=  NegativeAdjustments					+  decNegativeAdjustments,
		NoOfNegativeAdjustmentTransactions		=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,

		NoOfConsignmentTransactions				=  NoOfConsignmentTransactions			+  intNoOfConsignmentTransactions,
		NoOfConsignmentRefundTransactions		=  NoOfConsignmentRefundTransactions	+  intNoOfConsignmentRefundTransactions,
		NoOfWalkInTransactions					=  NoOfWalkInTransactions				+  intNoOfWalkInTransactions,
		NoOfWalkInRefundTransactions			=  NoOfWalkInRefundTransactions			+  intNoOfWalkInRefundTransactions,
		NoOfOutOfStockTransactions				=  NoOfOutOfStockTransactions			+  intNoOfOutOfStockTransactions,
		NoOfOutOfStockRefundTransactions		=  NoOfOutOfStockRefundTransactions		+  intNoOfOutOfStockRefundTransactions,

		-- 25Feb2015 : no need for this, already recomputed in procCashierReportSyncTransactionSales after zread
		-- ConsignmentTransactions					=  ConsignmentTransactions			+  decConsignmentTransactions,
		-- ConsignmentRefundTransactions			=  ConsignmentRefundTransactions	+  decConsignmentRefundTransactions,
		-- WalkInTransactions						=  WalkInTransactions				+  decWalkInTransactions,
		-- WalkInRefundTransactions					=  WalkInRefundTransactions			+  decWalkInRefundTransactions,
		-- OutOfStockTransactions					=  OutOfStockTransactions			+  decOutOfStockTransactions,
		-- OutOfStockRefundTransactions				=  OutOfStockRefundTransactions		+  decOutOfStockRefundTransactions,

		PromotionalItems						=  PromotionalItems						+  decPromotionalItems,
		CreditSalesTax							=  CreditSalesTax						+  decCreditSalesTax,
		IsProcessed								=  0	-- this must be set to 0 during transaction update
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND CashierID = lngCashierID;
	
	
END;
GO
delimiter ;

/**************************************************************

	procSyncQuantityProductHistory
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncQuantityProductHistory();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistory
GO

create procedure procSyncQuantityProductHistory()
BEGIN
	DECLARE strSessionID varchar(30);
	set strSessionID = '1';
	
	DROP table IF EXISTS tblProductHistoryAll;
	
	CREATE table tblProductHistoryAll (
		`SessionID` VARCHAR(30) NOT NULL,
		`HistoryID` BIGINT NOT NULL DEFAULT 0,
		`ProductID` BIGINT NOT NULL DEFAULT 0,
		`MatrixID` BIGINT NOT NULL DEFAULT 0,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
		`UnitCode` VARCHAR(100) NOT NULL,
		`Remarks` VARCHAR(100) NOT NULL,
		`TransactionDate` DateTime NOT NULL,
		`TransactionNo` VARCHAR(100) NOT NULL,
		INDEX `IX_tblProductHistory`(`SessionID`),
		INDEX `IX_tblProductHistory1`(`MatrixDescription`)
	);

	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID;
	
	SELECT 'Inserting StockItems';
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
		IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID;

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Purchase Order' AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		'Inventory Adjustment' AS Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer In' AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer Out' AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE b.Status = 1;
	
	UPDATE tblProductBaseVariationsMatrix SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.MatrixID = tblProductBaseVariationsMatrix.MatrixID);

	UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.ProductID = tblProducts.ProductID);

	SELECT N'DONE level1 Synching';
	
	
END;
GO
delimiter ;

/**************************************************************

	procSyncQuantityProductHistoryAdjust
	Lemuel E. Aceron
	July 20, 2009

	CALL procSyncQuantityProductHistoryAdjust();

	Description: make an automatic adjustments for products Not in the current Variations List;
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistoryAdjust
GO

create procedure procSyncQuantityProductHistoryAdjust()
BEGIN	
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decMatrixQuantity, decMinThreshold, decMaxThreshold DECIMAL(18,3);
	DECLARE strProductCode, strUnitCode VARCHAR(30);
	DECLARE strDescription VARCHAR(150);
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, Quantity, ProductCode, ProductDesc, a.BaseUnitID, UnitCode, MinThreshold, MaxThreshold FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID
									WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO intProductID, decProductQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold;
		
		IF NOT done THEN
			SET decMatrixQuantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = intProductID);
			
			-- CALL procInvAdjustmentInsert (1, now(), intProductID, strProductCode, strDescription, 0, 0, intUnitID, strUnitCode, 
			--								decProductQuantity, decMatrixQuantity, decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, 'System added during auto-sync.');
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SELECT 'DONE level2 Synching';
	
END;
GO
delimiter ;

/**************************************************************

	--UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE tblProductBaseVariationsMatrix.ProductID = tblProducts.ProductID);

	procGenerateProductHistory
	Lemuel E. Aceron
	2009-03-14 : created procedure
	2009-07-10 : included Purchase Orders
	2009-07-13 : included inventory adjustment in product history.
	2009-07-20 : included transferin in product history.
	2009-07-20 : included transferout in product history.
	
	CALL procGenerateProductHistory('1', null, null, 2 );
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistory
GO

create procedure procGenerateProductHistory(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime,
	IN intProductID BIGINT
	)
BEGIN
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE a.ProductID = intProductID
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
		IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = intProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Purchase Order from ',SupplierCode) AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE a.ProductID = intProductID
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE a.ProductID = intProductID
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer In from ',SupplierCode) AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE a.ProductID = intProductID
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer out to ',SupplierCode) AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE a.ProductID = intProductID
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	

END;
GO
delimiter ;

/*********************************
	procTerminalVersionUpdate
	Lemuel E. Aceron
	
	April 22, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procTerminalVersionUpdate;
delimiter GO

create procedure procTerminalVersionUpdate(IN intBranchID INT(4), IN strTerminalNo varchar(10), IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET
		FEVersion = strVersion
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportBatchCounter
	Lemuel E. Aceron
	CALL procUpdateTerminalReportBatchCounter();
	
	April 19, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportBatchCounter
GO

create procedure procUpdateTerminalReportBatchCounter(IN pvtTerminalNo varchar(10), IN pvtDateLastInitialized DATETIME)
BEGIN
	
	UPDATE tblTerminalReportHistory SET BatchCounter = BatchCounter + 1 WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procTransactionOrderTypeUpdate
	Lemuel E. Aceron
	
	May 1, 2009 - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionOrderTypeUpdate;
delimiter GO

create procedure procTransactionOrderTypeUpdate(IN intTransactionID bigint(20), IN intOrderType smallint)
BEGIN

	UPDATE tblTransactions SET OrderType = intOrderType WHERE TransactionID = intTransactionID;
	
END;
GO
delimiter ;

/*********************************
	procTransactionWaiterUpdate
	Lemuel E. Aceron
	May 1, 2009

	SET @SQL := CONCAT('UPDATE tblTransactions',strMonth,' SET WaiterID = ',intWaiterID,', WaiterName = ''',strWaiterName,''' WHERE TransactionID = ',intTransactionID,';');
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionWaiterUpdate;
delimiter GO

create procedure procTransactionWaiterUpdate(IN intTransactionID bigint(20), IN intWaiterID BIGINT(20), IN strWaiterName varchar(100))
BEGIN
	
	UPDATE tblTransactions SET WaiterID = intWaiterID, WaiterName = strWaiterName WHERE TransactionID = intTransactionID;

END;
GO
delimiter ;


/*********************************
	procTransactionRewardsContactUpdate
	Lemuel E. Aceron
	Dec 1, 2014
*********************************/
DROP PROCEDURE IF EXISTS procTransactionRewardsContactUpdate;
delimiter GO

create procedure procTransactionRewardsContactUpdate(
		IN intBranchID int(10), 
		IN strTerminalNo VARCHAR(5), 
		IN intTransactionID bigint(20), 
		IN intRewardsCustomerID BIGINT(20), 
		IN strRewardsCustomerName VARCHAR(100))
BEGIN
	
	UPDATE tblTransactions SET 
		RewardsCustomerID = intRewardsCustomerID,
		RewardsCustomerName = strRewardsCustomerName
	WHERE BranchID=intBranchID AND TerminalNo=strTerminalNo AND TransactionID = intTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionContactUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionContactUpdate;
delimiter GO

create procedure procTransactionContactUpdate(
	IN intTransactionID bigint(20), 
	IN intCustomerID BIGINT(20), 
	IN strCustomerName varchar(100), 
	IN strCustomerGroupName varchar(100),
	IN intModeOfTerms int(10),
	IN intTerms int(10))
BEGIN
	
	UPDATE tblTransactions SET 
		CustomerID = intCustomerID, CustomerName = strCustomerName, CustomerGroupName = strCustomerGroupName,
		ModeOfTerms = intModeOfTerms, Terms = intTerms
	WHERE TransactionID = intTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionTerminalNoUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionTerminalNoUpdate;
delimiter GO

create procedure procTransactionTerminalNoUpdate(
	IN intTransactionID bigint(20), 
	IN strTerminalNo varchar(30))
BEGIN
	
	UPDATE tblTransactions SET TerminalNo = strTerminalNo WHERE TransactionID = intTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionDateClosedUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionDateClosedUpdate;
delimiter GO

create procedure procTransactionDateClosedUpdate(IN intTransactionID bigint(20), IN dteDateClosed DateTime)
BEGIN
	
	UPDATE tblTransactions SET TransactionDate = DATE_FORMAT(dteDateClosed, '%Y-%m-%d %H:%i') WHERE TransactionID = intTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionDeleteByDataSource
	Lemuel E. Aceron
	Oct 15, 2013
*********************************/
DROP PROCEDURE IF EXISTS procTransactionDeleteByDataSource;
delimiter GO

create procedure procTransactionDeleteByDataSource(IN strDataSource varchar(30))
BEGIN
	
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT DISTINCT TransactionID FROM tblTransactions WHERE DataSource = strDataSource);

	DELETE FROM tblTransactions WHERE DataSource = strDataSource;

END;
GO
delimiter ;

/*********************************
	procGenerateDiscountByTerminalNo
	Lemuel E. Aceron
	May 5, 2009
	CALL procGenerateDiscountByTerminalNo('1', 1, '01', 1, '00000000000001', '00000000000010' );
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNo
GO

create procedure procGenerateDiscountByTerminalNo(
	IN strSessionID varchar(30),
	IN intBranchID int(4),
	IN strTerminalNo varchar(30),
	IN intCashierID BIGINT(20),
	IN strTransactionNoFrom varchar(30),
	IN strTransactionNoTo varchar(30)
	)
BEGIN
	
	IF (intCashierID = 0) THEN
		INSERT INTO tblDiscountHistory(SessionID, TerminalNo, DiscountCode, DiscountCount, Discount, CreatedOn, LastModified)
		SELECT strSessionID, strTerminalNo,
				IFNULL(DiscountCode,''), COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount, NOW(), NOW()
		FROM  tblTransactions 
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
			AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo AND IFNULL(DiscountCode,'') <> ''
		GROUP BY DiscountCode;
	ELSE
		INSERT INTO tblDiscountHistory(SessionID, TerminalNo, DiscountCode, DiscountCount, Discount, CreatedOn, LastModified)
		SELECT strSessionID, strTerminalNo,
				IFNULL(DiscountCode,''), COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount, NOW(), NOW()
		FROM  tblTransactions 
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND CashierID = intCashierID
			AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo AND IFNULL(DiscountCode,'') <> ''
		GROUP BY DiscountCode;
	END IF;
END;
GO
delimiter ;

/*********************************
	procDeleteDiscountHistory
	Lemuel E. Aceron
	CALL procDeleteDiscountHistory('1');
	
	May 5, 2009 - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDeleteDiscountHistory
GO

create procedure procDeleteDiscountHistory(
	IN strSessionID varchar(30) 
	)
BEGIN
	
	DELETE FROM tblDiscountHistory WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/*********************************
	procGenerateDiscountByTerminalNoByCashier
	Lemuel E. Aceron
	CALL procGenerateDiscountByTerminalNoByCashier('1', '01', 1, '00000000000001, '00000000000006' );
	
	May 5, 2009 - create this procedure
	Sep 4, 2014 - replaced with procGenerateDiscountByTerminalNo
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNoByCashier
GO

delimiter ;



/*********************************
	procTerminalReportInitializeZRead
	Lemuel E. Aceron
	
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter
					
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax

	Sep 29, 2014 - fix the newgrandtotal to always include trustfund

	CALL procTerminalReportInitializeZRead(1, '01', now(),'Lem', 0);
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportInitializeZRead
GO

create procedure procTerminalReportInitializeZRead(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10), 
	IN dteDateLastInitialized DateTime, 
	IN strInitializedBy varchar(150),
	IN intWithOutTF tinyint(1)
)
BEGIN
	DECLARE decOldTrustFund DECIMAL(18,3) DEFAULT 0;
	DECLARE strEndingTransactionNo, strEndingORNo VARCHAR(30);

	-- overwrite the TrustFund for whatever reason
	-- should be zero out if
	--		X-Read
	--		Manual overwride
	IF (intWithOutTF = 1 OR intWithOutTF = -1) THEN
		SET decOldTrustFund = (SELECT TrustFund FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

		UPDATE tblTerminalReport SET TrustFund = 0;

		CALL procsysAuditInsert(NOW(), strInitializedBy, 'TRUSTFUND OVERRIDE', 'localhost', CONCAT('TrustFund was overwritten from ',decOldTrustFund,' to 0 due to ALT+DEL @ BranchID:', intBranchID, ' TerminalNo:', strTerminalNo));
	END IF;

	-- get this to update the EndingTransactionNo, EndingORNo in tblCashierReport
	SET strEndingTransactionNo = (SELECT EndingTransactionNo FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	SET strEndingORNo = (SELECT EndingORNo FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

	-- update the tblTerminalReport GrandTotal depending on the TrustFund
	-- must be overwritten coz procTerminalReportSyncTransactionSales is the straight computation
	UPDATE tblTerminalReport SET 
		NewGrandTotal =  OldGrandTotal + (GrossSales * (100-TrustFund)/100)
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;

	-- 05Mar2015 Automatically Void the suspended or suspended open transaction
	UPDATE tblTransactions SET TransactionStatus = 20 WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND TransactionStatus IN (2,13);

	INSERT INTO tblTerminalReportHistory (
					BranchID, TerminalID, TerminalNo, IncludeIneSales, BeginningTransactionNo, EndingTransactionNo, BeginningORNo, EndingORNo, 
					ZReadCount, XReadCount, NetSales, GrossSales, TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, ActualOldGrandTotal, ActualNewGrandTotal, 
					VATExempt, NonVATableAmount, VATableAmount, ZeroRatedSales, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, 
					RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit,
					CreditPaymentCash, CreditPaymentCheque,
					CreditPaymentCreditCard, CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, SubtotalDiscount,
					ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount,					
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, TrustFund, 
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter, 
					NoOfReprintedTransaction, TotalReprintedTransaction, 
					NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
					NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
					ConsignmentSales, ConsignmentRefundSales, WalkInSales,
					WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales, ORSeriesBranchID, ORSeriesTerminalNo,
					IsProcessed, InitializedBy) 
				(SELECT 
					BranchID, TerminalID, TerminalNo, IncludeIneSales, BeginningTransactionNo, EndingTransactionNo, BeginningORNo, EndingORNo, 
					ZReadCount, XReadCount, NetSales, GrossSales, TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, ActualOldGrandTotal, ActualNewGrandTotal, 
					VATExempt, NonVATableAmount, VATableAmount, ZeroRatedSales, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, 
					RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit,
					CreditPaymentCash, CreditPaymentCheque,
					CreditPaymentCreditCard, CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, SubtotalDiscount, 
					ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount,
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, TrustFund,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter, 
					NoOfReprintedTransaction, TotalReprintedTransaction, 
					NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
					NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
					ConsignmentSales, ConsignmentRefundSales, WalkInSales,
					WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales, ORSeriesBranchID, ORSeriesTerminalNo,
					IsProcessed, strInitializedBy 
				FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	
	UPDATE tblTerminalReport SET 
					BeginningTransactionNo				=  EndingTransactionNo, 
					BeginningORNo						=  EndingORNo, 
					GrossSales							=  0, 
					TotalDiscount						=  0, 
					SNRDiscount							=  0, 
					PWDDiscount							=  0, 
					OtherDiscount						=  0,
					TotalCharge							=  0, 
					DailySales							=  0, 
					ItemSold							=  0, 
					QuantitySold						=  0, 
					NetSales							=  0, 
					GroupSales							=  0, 
					OldGrandTotal						=  NewGrandTotal,
					ActualOldGrandTotal					=  ActualNewGrandTotal,
					VATExempt   						=  0, 
					NonVATableAmount					=  0, 
					VATableAmount						=  0, 
					ZeroRatedSales						=  0,
					VAT									=  0, 
					EVATableAmount						=  0, 
					NonEVATableAmount					=  0, 
					EVAT								=  0, 
					LocalTax							=  0, 
					CashSales							=  0, 
					ChequeSales							=  0, 
					CreditCardSales						=  0, 
					CreditSales							=  0, 
					CreditPayment						=  0, 
					RefundCash							=  0, 
					RefundCheque						=  0, 
					RefundCreditCard					=  0, 
					RefundCredit						=  0, 
					RefundDebit							=  0, 
					CreditPaymentCash					=  0, 
					CreditPaymentCheque					=  0, 
					CreditPaymentCreditCard				=  0, 
					CreditPaymentDebit					=  0, 
					DebitPayment						=  0, 
					RewardPointsPayment					=  0,
					RewardConvertedPayment				=  0,
					CashInDrawer						=  0, 
					TotalDisburse						=  0, 
					CashDisburse						=  0, 
					ChequeDisburse						=  0, 
					CreditCardDisburse					=  0, 
					TotalWithhold						=  0, 
					CashWithhold						=  0, 
					ChequeWithhold						=  0, 
					CreditCardWithhold					=  0, 
					TotalPaidOut						=  0, 
					CashPaidOut							=  0,
					ChequePaidOut						=  0,
					CreditCardPaidOut					=  0,
					TotalDeposit						=  0, 
					CashDeposit							=  0, 
					ChequeDeposit						=  0, 
					CreditCardDeposit					=  0, 
					DebitDeposit						=  0, 
					BeginningBalance					=  0, 
					VoidSales							=  0, 
					RefundSales							=  0, 
					SubTotalDiscount					=  0, 
					ItemsDiscount						=  0, 
					SNRItemsDiscount					=  0, 
					PWDItemsDiscount					=  0, 
					OtherItemsDiscount					=  0, 
					NoOfCashTransactions				=  0, 
					NoOfChequeTransactions				=  0, 
					NoOfCreditCardTransactions			=  0, 
					NoOfCreditTransactions				=  0, 
					NoOfCombinationPaymentTransactions	=  0, 
					NoOfCreditPaymentTransactions		=  0, 
					NoOfDebitPaymentTransactions		=  0, 
					NoOfClosedTransactions				=  0, 
					NoOfRefundTransactions				=  0, 
					NoOfVoidTransactions				=  0, 
					NoOfRewardPointsPayment				=  0,
					NoOfTotalTransactions				=  0, 
					NoOfDiscountedTransactions			=  0,
					NegativeAdjustments					=  0,
					NoOfNegativeAdjustmentTransactions	=  0,
					PromotionalItems					=  0,
					CreditSalesTax						=  0,
					BatchCounter						=  1,
					NoOfReprintedTransaction			=  0,
					TotalReprintedTransaction			=  0, 
					NoOfConsignmentTransactions			=  0, 
					NoOfConsignmentRefundTransactions	=  0,  
					NoOfWalkInTransactions				=  0, 
					NoOfWalkInRefundTransactions		=  0, 
					NoOfOutOfStockTransactions			=  0,  
					NoOfOutOfStockRefundTransactions	=  0, 
					ConsignmentSales					=  0, 
					ConsignmentRefundSales				=  0,  
					WalkInSales							=  0, 
					WalkInRefundSales					=  0,  
					OutOfStockSales						=  0,  
					OutOfStockRefundSales				=  0, 
					IsProcessed							=  0,
					Trustfund							=  (SELECT TrustFund FROM tblTerminal WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo),
					IncludeIneSales						=  (SELECT IncludeIneSales FROM tblTerminal WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo),
					DateLastInitialized					=  dteDateLastInitialized
			WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
			
	
	INSERT INTO tblCashierReportHistory (
					CashierID, BranchID, TerminalID, TerminalNo, BeginningTransactionNo, BeginningORNo, 
					EndingTransactionNo, EndingORNo, NetSales, GrossSales, 
					TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, VATExempt, NonVATableAmount, VATableAmount, ZeroRatedSales, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit,
					CreditPayment, CreditPaymentCash, CreditPaymentCheque, CreditPaymentCreditCard, 
					CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit, 
					BeginningBalance, VoidSales, RefundSales, SubtotalDiscount, 
					ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
					NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
					ConsignmentSales, ConsignmentRefundSales, WalkInSales,
					WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales,
					PromotionalItems, CreditSalesTax )
				(SELECT 
					CashierID, BranchID, TerminalID, TerminalNo, BeginningTransactionNo, BeginningORNo, 
					strEndingTransactionNo, strEndingORNo, NetSales, GrossSales, 
					TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, VATExempt, NonVATableAmount, VATableAmount, ZeroRatedSales, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit,
					CreditPayment, CreditPaymentCash, CreditPaymentCheque, CreditPaymentCreditCard, 
					CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, SubtotalDiscount, 
					ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
					NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
					ConsignmentSales, ConsignmentRefundSales, WalkInSales,
					WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales,
					PromotionalItems, CreditSalesTax FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	
	-- Just delete the cashier's this will be recreated anyway.
	-- also the syncid does update and the cashiers logs will be cleaned up
	DELETE FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/********************************************
	procTerminalUpdate
	Lemuel E. Aceron
	
	May 06, 2009 : - Created this procedure
	Oct 17, 2011 : - Added ShowCustomerSelection, RewardPointsMinimum,
					 RewardPointsEvery, RewardPoints
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdate
GO

create procedure procTerminalUpdate(
	IN intBranchID INT(4),
	IN lngTerminalID BIGINT(20), 
	IN bolIsPrinterAutoCutter TINYINT(1),
	IN bolAutoPrint TINYINT(1),
	IN bolIsVATInclusive TINYINT(1),
	IN strPrinterName VARCHAR(20),
	IN strTurretName VARCHAR(20),
	IN strCashDrawerName VARCHAR(20),
	IN bolItemVoidConfirmation TINYINT(1),
	IN bolEnableEVAT TINYINT(1),
	IN intMaxReceiptWidth INT(10),
	IN strFORM_Behavior VARCHAR(20),
	IN strMarqueeMessage VARCHAR(255),
	IN strMachineSerialNo VARCHAR(20),
	IN strAccreditationNo VARCHAR(25),
	IN decVAT DECIMAL(18,3),
	IN decEVAT DECIMAL(18,3),
	IN decLocalTax DECIMAL(18,3),
	IN bolShowItemMoreThanZeroQty TINYINT(1),
	IN bolShowOnlyPackedTransactions TINYINT(1),
	IN bolShowOneTerminalSuspendedTransactions TINYINT(1),
	IN intTerminalReceiptType TINYINT(1),
	IN strSalesInvoicePrinterName VARCHAR(30),
	IN bolCashCountBeforeReport TINYINT(1),
	IN bolPreviewTerminalReport TINYINT(1),
	IN bolIsPrinterDotMatrix TINYINT(1),
	IN bolIsChargeEditable TINYINT(1),
	IN bolIsDiscountEditable TINYINT(1),
	IN bolCheckCutOffTime TINYINT(1),
	IN strStartCutOffTime VARCHAR(5),
	IN strEndCutOffTime VARCHAR(5),
	IN bolWithRestaurantFeatures TINYINT(1),
	IN strSeniorCitizenDiscountCode varchar(5),
	IN strPWDDiscountCode varchar(5),
	IN intGroupChargeTypeID INT(10),
	IN intPersonalChargeTypeID INT(10),
	IN strDefaultTransactionChargeCode varchar(60),
	IN strDineInChargeCode varchar(60),
	IN strTakeOutChargeCode varchar(60),
	IN strDeliveryChargeCode varchar(60),
	IN bolIsTouchScreen TINYINT(1),
	IN bolWillContinueSelectionVariation TINYINT(1),
	IN bolWillContinueSelectionProduct TINYINT(1),
	IN bolWillPrintGrandTotal TINYINT(1),
	IN bolReservedAndCommit TINYINT(1),
	IN bolShowCustomerSelection TINYINT(1)
	)
BEGIN

	UPDATE tblTerminal SET 
				BranchID				= intBranchID,
				IsPrinterAutoCutter		= bolIsPrinterAutoCutter,
				AutoPrint				= bolAutoPrint,
				IsVATInclusive			= bolIsVATInclusive,
				PrinterName				= strPrinterName,
				TurretName				= strTurretName,
				CashDrawerName			= strCashDrawerName,
				ItemVoidConfirmation	= bolItemVoidConfirmation,
				EnableEVAT				= bolEnableEVAT,
				MaxReceiptWidth			= intMaxReceiptWidth,
				FORM_Behavior			= strFORM_Behavior,
				MarqueeMessage			= strMarqueeMessage,
				MachineSerialNo			= strMachineSerialNo,
				AccreditationNo			= strAccreditationNo,
				VAT						= decVAT,
				EVAT					= decEVAT,
				LocalTax				= decLocalTax,
				ShowItemMoreThanZeroQty = bolShowItemMoreThanZeroQty,
				ShowOnlyPackedTransactions = bolShowOnlyPackedTransactions,
				ShowOneTerminalSuspendedTransactions = bolShowOneTerminalSuspendedTransactions,
				TerminalReceiptType		= intTerminalReceiptType,
				SalesInvoicePrinterName = strSalesInvoicePrinterName,
				CashCountBeforeReport	= bolCashCountBeforeReport,
				PreviewTerminalReport	= bolPreviewTerminalReport,
				IsPrinterDotMatrix		= bolIsPrinterDotMatrix,
				IsChargeEditable		= bolIsChargeEditable,
				IsDiscountEditable		= bolIsDiscountEditable,
				CheckCutOffTime			= bolCheckCutOffTime,
				StartCutOffTime			= strStartCutOffTime,
				EndCutOffTime			= strEndCutOffTime,
				WithRestaurantFeatures	= bolWithRestaurantFeatures,
				SeniorCitizenDiscountCode = strSeniorCitizenDiscountCode,
				PWDDiscountCode						= strPWDDiscountCode,
				GroupChargeTypeID					= intGroupChargeTypeID,
				PersonalChargeTypeID				= intPersonalChargeTypeID,
				DefaultTransactionChargeCode		= strDefaultTransactionChargeCode,
				DineInChargeCode					= strDineInChargeCode,
				TakeOutChargeCode					= strTakeOutChargeCode,
				DeliveryChargeCode					= strDeliveryChargeCode,
				IsTouchScreen						= bolIsTouchScreen,
				WillContinueSelectionVariation		= bolWillContinueSelectionVariation,
				WillContinueSelectionProduct		= bolWillContinueSelectionProduct,
				WillPrintGrandTotal					= bolWillPrintGrandTotal,
				ReservedAndCommit					= bolReservedAndCommit,
				ShowCustomerSelection				= bolShowCustomerSelection
	WHERE TerminalID = lngTerminalID;
	
	
END;
GO
delimiter ;


/********************************************
	procTerminalUpdateRewardPointSystem
	Lemuel E. Aceron
	
	Nov 4, 2011 : - Created this procedure
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdateRewardPointSystem
GO

create procedure procTerminalUpdateRewardPointSystem(
	IN bolEnableRewardPoints TINYINT(1),
	IN bolRoundDownRewardPoints TINYINT(1),
	IN decRewardPointsMinimum DECIMAL(18,3),
	IN decRewardPointsEvery DECIMAL(18,3),
	IN decRewardPoints DECIMAL(18,3),
	IN bolEnableRewardPointsAsPayment DECIMAL(18,3),
	IN decRewardPointsMaxPercentageForPayment DECIMAL(5,2),
	IN decRewardPointsPaymentValue DECIMAL(18,3),
	IN decRewardPointsPaymentCashEquivalent DECIMAL(18,3)
	)
BEGIN

	UPDATE tblTerminal SET 
		EnableRewardPoints		= bolEnableRewardPoints,
		RoundDownRewardPoints	= bolRoundDownRewardPoints,
		RewardPointsMinimum		= decRewardPointsMinimum,
		RewardPointsEvery		= decRewardPointsEvery,
		RewardPoints			= decRewardPoints,
		EnableRewardPointsAsPayment			= bolEnableRewardPointsAsPayment,
		RewardPointsMaxPercentageForPayment	= decRewardPointsMaxPercentageForPayment,
		RewardPointsPaymentValue			= decRewardPointsPaymentValue,
		RewardPointsPaymentCashEquivalent	= decRewardPointsPaymentCashEquivalent;
		
	
END;
GO
delimiter ;


/**************************************************************
	procGenerateSalesPerItemByGroup
	Lemuel E. Aceron
	CALL procGenerateSalesPerItemByGroup('1', null, null, null, null, null, '2013-12-12 00:00', '2013-12-14 23:59');
	
	May 15, 2009 - as requested by Malou of Baguio
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemByGroup
GO

create procedure procGenerateSalesPerItemByGroup(
	IN strSessionID varchar(30),
	IN strProductGroup varchar(50),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET strProductGroup = IF(NOT ISNULL(strProductGroup), CONCAT('%',strProductGroup,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductID, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount, Discount, PurchasePrice, InvQuantity)
	SELECT strSessionID, ProductGroup,
		a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode) ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount,
		SUM(IF(TransactionItemStatus=3,-a.Discount + -a.TransactionDiscount,IF(TransactionItemStatus=4,-a.Discount + -a.TransactionDiscount,a.Discount + a.TransactionDiscount))) Discount,
		IFNULL(MIN(pkg.PurchasePrice), a.PurchasePrice) PurchasePrice,
		IFNULL(MAX(inv.InvQuantity),0) InvQuantity
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	LEFT OUTER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID AND pkg.Quantity = 1 AND a.ProductPackageID = pkg.PackageID
	LEFT OUTER JOIN (
		SELECT ProductID, SUM(Quantity) InvQuantity FROM tblProductInventory
		GROUP BY ProductID
	) inv ON a.ProductID = inv.ProductID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY ProductGroup, a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode), ProductUnitCode;

END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportMallForwarderFileName
	Lemuel E. Aceron
	CALL procUpdateTerminalReportMallForwarderFileName
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportMallForwarderFileName
GO

create procedure procUpdateTerminalReportMallForwarderFileName(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtMallFilename varchar(30)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		MallFilename = pvtMallFilename
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportIsMallFileUploadComplete
	Lemuel E. Aceron
	CALL procUpdateTerminalReportIsMallFileUploadComplete
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportIsMallFileUploadComplete
GO

create procedure procUpdateTerminalReportIsMallFileUploadComplete(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtIsMallFileUploadComplete TINYINT(1)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		IsMallFileUploadComplete = pvtIsMallFileUploadComplete
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procBEVersionUpdate
	Lemuel E. Aceron
	
	May 26, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procBEVersionUpdate;
delimiter GO

create procedure procBEVersionUpdate(IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET BEVersion = strVersion;
	
END;
GO
delimiter ;

/*********************************
	procProductPackageSave
	Lemuel E. Aceron
	May 30, 2013
	

	30May2013 Combine procProductPackageInsert and procProductPackageUpdate
	CALL procProductPackageSave(0, 3, 0, 5, 0, 388800, 388800, 0, 12, 0, 0, 'JVS100000451', NULL, NULL);
*********************************/

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageInsert
GO

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageUpdate
GO

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageSave
GO

create procedure procProductPackageSave(
	IN intPackageID BIGINT(20),
	IN intProductID BIGINT(20),
	IN intMatrixID BIGINT(20),
	IN intUnitID INT(10),
	IN decPurchasePrice DECIMAL(18,3),
	IN decSellingPrice DECIMAL(18,3),
	IN decPrice1 DECIMAL(18,3),
	IN decPrice2 DECIMAL(18,3),
	IN decPrice3 DECIMAL(18,3),
	IN decPrice4 DECIMAL(18,3),
	IN decPrice5 DECIMAL(18,3),
	IN decWSPrice DECIMAL(18,3),
	IN decQuantity DECIMAL(18,3), 
	IN decVAT DECIMAL(18,3), 
	IN decEVAT DECIMAL(18,3), 
	IN decLocalTax DECIMAL(18,3),
	IN strBarCode1 VARCHAR(30),
	IN strBarCode2 VARCHAR(30),
	IN strBarCode3 VARCHAR(30))
BEGIN
	IF intPackageID = 0 THEN
		IF intUnitID = 0 THEN
			SET intPackageID = IFNULL((SELECT PackageID FROM tblProductPackage WHERE ProductID = intProductID AND MatrixID = intMatrixID AND Quantity = 1),0);
		ELSE
			SET intPackageID = IFNULL((SELECT PackageID FROM tblProductPackage WHERE ProductID = intProductID AND MatrixID = intMatrixID AND UnitID = intUnitID AND Quantity = decQuantity),0);
		END IF;
	END IF;

	IF intPackageID = 0 THEN
		INSERT INTO tblProductPackage(
			ProductID, MatrixID, UnitID, PurchasePrice, 
			Price, Price1, Price2, Price3, Price4, Price5, 
			WSPrice, Quantity,
			VAT, EVAT, LocalTax, BarCode1, BarCode2, BarCode3)
		VALUES(
			intProductID, intMatrixID, intUnitID, decPurchasePrice, 
			decSellingPrice, decPrice1, decPrice2, decPrice3, decPrice4, decPrice5, 
			decWSPrice, decQuantity,
			decVAT, decEVAT, decLocalTax, strBarCode1, strBarCode2, strBarCode3);
	ELSE
		UPDATE tblProductPackage SET
			UnitID			=	intUnitID,
			PurchasePrice	=	decPurchasePrice,
			Price			=	decSellingPrice,
			Price1			=	decPrice1,
			Price2			=	decPrice2,
			Price3			=	decPrice3,
			Price4			=	decPrice4,
			Price5			=	decPrice5,
			WSPrice			=	decWSPrice,
			Quantity		=	decQuantity,
			VAT				=	decVAT,
			EVAT			=	decEVAT,
			LocalTax		=	decLocalTax,
			BarCode1		=	strBarCode1,
			BarCode2		=	strBarCode2,
			BarCode3		=	strBarCode3
		WHERE PackageID		=	intPackageID;
	END IF;

	UPDATE tblProductPackage SET 
		BarCode4 = REPLACE(CONCAT(IFNULL(BarCode1,''), Quantity, ProductID, MatrixID),'.','')
	WHERE MatrixID = 0 AND PackageID=intPackageID;

	IF decQuantity = 1 THEN
		UPDATE tblProductPackage prd 
		INNER JOIN tblProductPackage mtrx ON prd.ProductID = mtrx.ProductID AND mtrx.MatrixID <> 0 SET 
			mtrx.BarCode4 = REPLACE(CONCAT(IFNULL(prd.BarCode1,''), mtrx.Quantity, prd.ProductID, mtrx.MatrixID),'.','')
		WHERE prd.ProductID	=intProductID;
	END IF;
	
END;
GO
delimiter ;


/*********************************
	procProductPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procProductPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackagePriceHistoryInsert
GO

create procedure procProductPackagePriceHistoryInsert(
	IN intUID BIGINT(20),
	IN intPackageID BIGINT(20),
	IN dteChangeDate DATETIME,
	IN decPurchasePriceNow DECIMAL(18,3), 
	IN decSellingPriceNow DECIMAL(18,3),
	IN decPrice1Now DECIMAL(18,3),
	IN decPrice2Now DECIMAL(18,3),
	IN decPrice3Now DECIMAL(18,3),
	IN decPrice4Now DECIMAL(18,3),
	IN decPrice5Now DECIMAL(18,3),
	IN decVATNow DECIMAL(18,3), 
	IN decEVATNow DECIMAL(18,3), 
	IN decLocalTaxNow DECIMAL(18,3),
	IN strRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblProductPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		Price1Now, Price2Now, Price3Now, Price4Now, Price5Now,
		VATBefore, VATNow, EVATBefore, EVATNow, 
		LocalTaxBefore, LocalTaxNow, Remarks)
	SELECT intUID, intPackageID, dteChangeDate, 
		PurchasePrice, IF(decPurchasePriceNow=-1,PurchasePrice,decPurchasePriceNow), 
		Price, IF(decSellingPriceNow=-1,Price,decSellingPriceNow), 
		IF(decPrice1Now=-1,Price1,decPrice1Now) Price1Now, 
		IF(decPrice2Now=-1,Price2,decPrice2Now) Price2Now, 
		IF(decPrice3Now=-1,Price3,decPrice3Now) Price3Now, 
		IF(decPrice4Now=-1,Price4,decPrice4Now) Price4Now, 
		IF(decPrice5Now=-1,Price5,decPrice5Now) Price5Now, 
		VAT, IF(decVATNow=-1,VAT,decVATNow), EVAT, IF(decEVATNow=-1,EVAT,decEVATNow), 
		LocalTax, IF(decLocalTaxNow=-1,LocalTax,decLocalTaxNow), strRemarks 
	FROM tblProductPackage pkg
	WHERE PackageID = intPackageID;
		
END;
GO
delimiter ;


/*********************************
	procMatrixPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procMatrixPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackagePriceHistoryInsert
GO

create procedure procMatrixPackagePriceHistoryInsert(
	IN pvtUID BIGINT(20),
	IN pvtPackageID BIGINT(20),
	IN pvtChangeDate DATETIME,
	IN pvtPurchasePriceNow DECIMAL(18,3), 
	IN pvtSellingPriceNow DECIMAL(18,3),
	IN pvtVATNow DECIMAL(18,3), 
	IN pvtEVATNow DECIMAL(18,3), 
	IN pvtLocalTaxNow DECIMAL(18,3),
	IN pvtRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblMatrixPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		VATBefore, VATNow, EVATBefore, EVATNow, LocalTaxBefore, LocalTaxNow, Remarks)
						   SELECT pvtUID, pvtPackageID, pvtChangeDate, 
		PurchasePrice, IF(pvtPurchasePriceNow=-1,PurchasePrice,pvtPurchasePriceNow), 
		Price, IF(pvtSellingPriceNow=-1,Price,pvtSellingPriceNow), 
		VAT, IF(pvtVATNow=-1,VAT,pvtVATNow), EVAT, IF(pvtEVATNow=-1,EVAT,pvtEVATNow), 
		LocalTax, IF(pvtLocalTaxNow=-1,LocalTax,pvtLocalTaxNow), pvtRemarks FROM tblMatrixPackage WHERE PackageID = pvtPackageID;
		
END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantity
	Lemuel E. Aceron
	CALL procZeroOutProductQuantity();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantity
GO

create procedure procZeroOutProductQuantity()
BEGIN

	UPDATE tblProducts SET Quantity = 0;

	UPDATE tblProductBasevariationsMatrix SET Quantity = 0;

	UPDATE tblBranchInventory SET Quantity = 0;

	UPDATE tblBranchInventoryMatrix SET Quantity = 0;

END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantityAndDropVariations
	Lemuel E. Aceron
	CALL procZeroOutProductQuantityAndDropVariations();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantityAndDropVariations
GO

create procedure procZeroOutProductQuantityAndDropVariations()
BEGIN

	SET FOREIGN_KEY_CHECKS = 0;

	TRUNCATE table tblProductInventory;
	TRUNCATE table tblProductVariationsMatrix;
	TRUNCATE table tblProductBaseVariationsMatrix;

	DELETE FROM tblProductPackage WHERE MatrixID <> 0;
	
	SET FOREIGN_KEY_CHECKS = 1;
END;
GO
delimiter ;

/*********************************
	procProductVariationCountUpdate
	Lemuel E. Aceron
	CALL procProductVariationCountUpdate
	
	July 8 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductVariationCountUpdate
GO

create procedure procProductVariationCountUpdate(IN intProductID BIGINT)
BEGIN
	
	UPDATE tblProducts SET VariationCount = (SELECT COUNT(MatrixID) FROM tblProductBaseVariationsMatrix z WHERE z.Deleted = 0 AND tblProducts.ProductID = z.ProductID) WHERE ProductID = intProductID ;

END;
GO
delimiter ;

/*********************************
	procProductSynchronizeQuantity
	Lemuel E. Aceron
	CALL procProductSynchronizeQuantity(11);
	
	Oct 01, 2009 : Lemu
	Create this procedure
	
	Jul 26, 2011 : Lemu
	Added Insert to product movement history
	Added Insert to inventory adjustment
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSynchronizeQuantity
GO

create procedure procProductSynchronizeQuantity(
	IN intProductID BIGINT
)
BEGIN
	DECLARE strTransactionNo VARCHAR(30) DEFAULT '';
	DECLARE lngMatrixVariationCount BIGINT DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity, decProductActualQuantity, decMinThreshold, decMaxThreshold DECIMAL(18,3) DEFAULT 0;
	DECLARE decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE lngMatrixID BIGINT DEFAULT 0;
	DECLARE strRemarks VARCHAR(100);
	DECLARE intBranchID INT(4) DEFAULT 1;
	
	-- STEP 1: check if there is an existing variation

	-- STEP 1.a: Set the value of lngMatrixVariationCount
	SELECT COUNT(MatrixID) INTO lngMatrixVariationCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = intProductID;

	IF (ISNULL(lngMatrixVariationCount)) THEN SET lngMatrixVariationCount = 0; END IF; 
	
	-- STEP 1.b: compare if there is a count
	IF (lngMatrixVariationCount <> 0) THEN 

		-- STEP 2: get the total Quantity of all Matrix
		SET decMatrixTotalQuantity = 0;
		SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = intProductID;
	
		-- STEP 3: Set the value of strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		SELECT ProductCode, ProductDesc, Quantity, ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = intProductID;
			
		-- STEP 4: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the product quantity based on total of quantities of all variations
		IF (decMatrixTotalQuantity <> decProductQuantity) THEN
			
			-- set the value of stRemarks, see the administrator for the list of constant remarks
			SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM SUM OF MATRIX QTY AS BASIS';
			
			-- STEP 4.a: Insert to product movement history
			CALL procProductMovementInsert(intProductID, strProductCode, strProductDesc, 0, '', 
											decProductQuantity, decMatrixTotalQuantity - decProductQuantity, decMatrixTotalQuantity, 0, 
											strUnitCode, strRemarks, now(), strTransactionNo, 'SYSTEM', intBranchID, intBranchID, 0);
			
			-- STEP 4.b: Insert to inventory adjustment
			CALL procInvAdjustmentInsert(1, now(), intProductID, strProductCode, strProductDesc, 0,
														'', intUnitID, strUnitCode, decProductQuantity, decMatrixTotalQuantity, 
														decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strTransactionNo));
			
			-- STEP 4.c: Do the actual adjustment
			UPDATE tblProducts a SET 
				Quantity			= (SELECT IFNULL(SUM(Quantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
			WHERE ProductID = intProductID;
		
		END IF;
		
		-- STEP 5: Update the Actual Quantity
		UPDATE tblProducts a SET 
			ActualQuantity		= (SELECT IFNULL(SUM(ActualQuantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
		WHERE ProductID = intProductID;
	
	END IF;
	
	
END;
GO
delimiter ;

/*********************************
	procProductBaseVariationsMatrixDelete
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDelete();
	
	Jul 02 2009 : Lemu
	- Create this procedure
	
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDelete
GO

create procedure procProductBaseVariationsMatrixDelete(
	IN pvtIDs varchar(100)
)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curProductIDs CURSOR FOR SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID IN (pvtIDs);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
		
	DELETE FROM tblMatrixPackage WHERE MatrixID IN (pvtIDs);

	DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (pvtIDs);
	
	OPEN curProductIDs;
	
	UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID IN (pvtIDs);
	
	REPEAT
		FETCH curProductIDs INTO intProductID;
		
		IF NOT done THEN
			
			-- Synchronize the Product Quantity	
			CALL procProductSynchronizeQuantity(intProductID);
			
			-- Update the variation count in table Product
			CALL procProductVariationCountUpdate(intProductID);
			
		END IF;
		
	UNTIL done END REPEAT;
	CLOSE curProductIDs;
			
END;
GO
delimiter ;


/*********************************
	procProductBaseVariationsMatrixDeleteByID
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDeleteByID
	
	Jul 02, 2009 : Lemu
	- create this procedure
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
	May 23, 2013 : Lemu
	- Point to tblProductInventory
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDeleteByID
GO

create procedure procProductBaseVariationsMatrixDeleteByID(
	IN pvtID BIGINT
)
BEGIN
	
	DELETE FROM tblProductPackage WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductVariationsMatrix WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductInventory WHERE MatrixID = pvtID;

	DELETE FROM tblProductBaseVariationsMatrix WHERE MatrixID = pvtID;

	-- UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID = pvtID;
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateProductPrices
	Lemuel E. Aceron
	CALL procGenerateProductPrices('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductPrices
GO

create procedure procGenerateProductPrices(
	IN strSessionID varchar(30),
	IN strProductGroupName varchar(30),
	IN strProductSubGroupName varchar(30)
	)
BEGIN

	/*****************************
	**	tblProductPrices
	*****************************/
	DROP table IF EXISTS tblProductPrices;
	CREATE table tblProductPrices (
		`SessionID` VARCHAR(30) NOT NULL,
		`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductCode` VARCHAR(30) NOT NULL,
		`ProductDescription` VARCHAR(30) NOT NULL,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductGroupName` VARCHAR(30) NOT NULL,
		`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductSubGroupName` VARCHAR(30) NOT NULL,
		`Quantity` DECIMAL(18,3),
		`UnitCode` VARCHAR(10) NOT NULL,
		`UnitName` VARCHAR(30) NOT NULL,
		`PurchasePrice` DECIMAL(18,3),
		`Price` DECIMAL(18,3),
		`VAT` DECIMAL(18,3),
		`EVAT` DECIMAL(18,3),
		`LocalTax` DECIMAL(18,3),
	INDEX `IX_tblProductPrices`(`SessionID`),
	INDEX `IX1_tblProductPrices`(`ProductID`),
	INDEX `IX2_tblProductPrices`(`ProductGroupID`),
	INDEX `IX3_tblProductPrices`(`ProductSubGroupID`)
	);

	/*** Select the package prices for matrix ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		d.ProductCode, 
		d.ProductDesc AS ProductDescription, 
		b.Description AS MatrixDescription,
		f.ProductGroupID, 
		f.ProductGroupName,
		e.ProductSubGroupID,
		e.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblMatrixPackage a 
		INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID 
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProducts d ON b.ProductID = d. ProductID
		INNER JOIN tblProductSubGroup e ON d.ProductSubGroupID = e.ProductSubGroupID
		INNER JOIN tblProductGroup f ON e.ProductGroupID = f.ProductGroupID
	WHERE d.Deleted = 0;
	
	/*** Select the packages for products without variations ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		b.ProductCode, 
		b.ProductDesc AS ProductDescription, 
		null AS MatrixDescription,
		e.ProductGroupID, 
		e.ProductGroupName,
		d.ProductSubGroupID,
		d.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblProductPackage a 
		INNER JOIN tblProducts b ON a.ProductID = b.ProductID
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID
		INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID
	WHERE b.ProductID NOT IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0);
	
END;
GO
delimiter ;


/**************************************************************
	AccessuserSynchronizeAccessRights
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRights(1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRights
GO

create procedure AccessuserSynchronizeAccessRights(IN pvtUID BIGINT)
BEGIN
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = (SELECT GroupID FROM sysAccessUserDetails WHERE UID = pvtUID);
	
END;
GO
delimiter ;


/**************************************************************
	AccessuserSynchronizeAccessRightsFromGroup
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRightsFromGroup(1,1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRightsFromGroup
GO

create procedure AccessuserSynchronizeAccessRightsFromGroup(IN pvtUID BIGINT, IN pvtGroupID INT)
BEGIN
	
	DELETE FROM sysAccessRights WHERE UID = pvtUID;
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = pvtGroupID;
	
END;
GO
delimiter ;


/*********************************
	procCashPaymentInsert
	Lemuel E. Aceron
	CALL procCashPaymentInsert();
	
	Sep 01, 2009 - create this procedure
	Aug 29, 2014 - replaced with procCashPaymentSave
*********************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashPaymentInsert
GO
delimiter ;

/*********************************
	procChequePaymentInsert
	Lemuel E. Aceron
	CALL procChequePaymentInsert();
	
	Sep 01, 2009 - create this procedure
	Aug 29, 2014 - replaced with procChequePaymentSave
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procChequePaymentInsert
GO
delimiter ;
/*********************************
	procCreditCardPaymentInsert
	Lemuel E. Aceron
	CALL procCreditCardPaymentInsert();
	
	Sep 01, 2009 - create this procedure
	Aug 29, 2014 - replaced with procCreditCardPaymentSave
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditCardPaymentInsert
GO
delimiter ;

/*********************************
	procCreditPaymentInsert
	Lemuel E. Aceron
	CALL procCreditPaymentInsert();
	
	[09/01/2009] - create this procedure
	[04/05/2012] - include additional fields in saving to get the values of creditcard info

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentInsert
GO

create procedure procCreditPaymentInsert(
	IN intBranchID			  int(4),
	IN strTerminalNo		  varchar(5),
	IN intTransactionID BIGINT(20),
	IN intCustomerID BIGINT(20),
	IN intCreditCardPaymentID BIGINT(20),
	IN intCreditCardTypeID INT(10),
	IN decCurrentCredit DECIMAL(18,3),
	IN decAmount DECIMAL(18,3),
	IN dteTransactionDate DATETIME,
	IN strTransactionNo VARCHAR(30),
	IN strCashierName VARCHAR(150),
	IN strRemarks VARCHAR(255),
	IN strCreditReason VARCHAR(150),
	IN intCreditReasonID int(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
)
BEGIN

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	INSERT INTO tblCreditPayment(BranchID, TerminalNo, TransactionID, ContactID, CreditCardPaymentID, CreditCardTypeID,
								CreditBefore, Amount, CreditAfter, 
								CreditDate, CashierName, TransactionNo, Remarks, CreditReason, CreditReasonID, CreatedOn, LastModified)
					VALUES (intBranchID, strTerminalNo, intTransactionID, intCustomerID, intCreditCardPaymentID, intCreditCardTypeID,
								decCurrentCredit, decAmount, decCurrentCredit + decAmount, 
								dteTransactionDate, strCashierName, strTransactionNo, strRemarks, strCreditReason, intCreditReasonID, dteCreatedOn, dteLastModified);

	UPDATE tblCreditPayment SET SyncID = CreditPaymentID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
END;
GO
delimiter ;

/*********************************
	procDebitPaymentInsert
	Lemuel E. Aceron
	CALL procDebitPaymentInsert();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentInsert
GO


/*********************************
	procContactAddCredit
	Lemuel E. Aceron
	CALL procContactAddCredit();
	
	[09/01/2009] - create this procedure
	[04/03/2012] - include saving of credit as totalpuchase in creditcardinfo
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddCredit
GO

create procedure procContactAddCredit(
	IN intContactID BIGINT(20),
	IN decCredit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Credit =	Credit + decCredit WHERE ContactID = intContactID;
	
	UPDATE tblContactCreditCardInfo SET TotalPurchases = TotalPurchases + decCredit WHERE CustomerID = intContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactAddDebit
	Lemuel E. Aceron
	CALL procContactAddDebit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddDebit
GO

create procedure procContactAddDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Debit =	Debit + pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactSubtractCredit
	Lemuel E. Aceron
	CALL procContactSubtractCredit();
	
	[01Sep2009] - create this procedure
	[03Nov2014] - include updating of isCreditAllowed = 1 if payment is done
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractCredit
GO

create procedure procContactSubtractCredit(
	IN intContactID BIGINT(20),
	IN decCredit DECIMAL(18,3),
	IN boActivateSuspendedAccount TINYINT(1))
BEGIN
	UPDATE tblContacts SET Credit =	Credit - decCredit WHERE ContactID = intContactID;
	
	-- [03Nov2014] - include updating of isCreditAllowed = 1 if payment is done
	IF (boActivateSuspendedAccount = 1) THEN
		UPDATE tblContacts SET IsCreditAllowed = 1 WHERE ContactID = intContactID;
		UPDATE tblContactCreditCardInfo SET CreditCardStatus = 10 WHERE CustomerID = intContactID;
	END IF;
END;
GO
delimiter ;

/*********************************
	procContactSubtractDebit
	Lemuel E. Aceron
	CALL procContactSubtractDebit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractDebit
GO

create procedure procContactSubtractDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Debit =	Debit - pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;


/*********************************
	procCreditPaymentUpdateCredit
	Lemuel E. Aceron
	CALL procCreditPaymentUpdateCredit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentUpdateCredit
GO

create procedure procCreditPaymentUpdateCredit(
	IN intBranchID BIGINT, 
	IN strTerminalNo VARCHAR(20),
	IN intCreditPaymentID BIGINT(20),
	IN pvtAmount DECIMAL(18,3),
	IN pvtRemarks VARCHAR(255))
BEGIN

	UPDATE tblCreditPayment SET 
		AmountPaid = AmountPaid + pvtAmount,
		AmountPaidCuttOffMonth = AmountPaidCuttOffMonth + pvtAmount,
		Remarks = CONCAT(Remarks,';',pvtRemarks)
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
		AND CreditPaymentID = intCreditPaymentID;
		
END;
GO
delimiter ;

/*********************************
	procDebitPaymentUpdateDebit
	Lemuel E. Aceron
	CALL procDebitPaymentUpdateDebit();
	
	01Sep2009 : create this procedure
	02Mar2015 : Deleted coz it's redundant. Changed with procSaveDebitPayment()

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentUpdateDebit
GO


/*********************************
	procCreditPaymentSyncTransactionNo
	Lemuel E. Aceron
	CALL procCreditPaymentSyncTransactionNo();
	
	[09/02/2009] - create this procedure
	Update Credit Payment TransactionNo with the Correct TransactionNo
	THIS IS ONLY APPLICABLE IF DB Version is lower than v.2.0.0.8

	[09/14/2014] - deleted	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentSyncTransactionNo
GO

delimiter ;

/**************************************************************
	procGenerateSalesPerItemWithZeroSales
	Lemuel E. Aceron
	
	CALL procGenerateSalesPerItemWithZeroSales('1', null, null, null, null, null, null);
	
	[09/07/2009] created this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemWithZeroSales
GO

create procedure procGenerateSalesPerItemWithZeroSales(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN

	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	CALL procGenerateSalesPerItem(strSessionID, strTransactionNo, strCustomerName, strCashierName, strTerminalNo, dteStartTransactionDate, dteEndTransactionDate);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductID, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount, Discount, PurchasePrice, InvQuantity)
	SELECT strSessionID,
		ProductGroupCode,
		a.ProductID,
		a.ProductCode 'ProductCode',
		UnitCode 'ProductUnitCode',
		0 'Quantity', 0 Amount, 
		0 PurchaseAmount, 0 Discount,
		IFNULL(pkg.PurchasePrice, 0) PurchasePrice,
		IFNULL(inv.InvQuantity,0) InvQuantity
	FROM tblProducts a 
		INNER JOIN tblProductSubGroup b ON b.ProductSubGroupID = a.ProductSubGroupID
		INNER JOIN tblProductGroup c ON c.ProductGroupID = b.ProductGroupID
		INNER JOIN tblUnit d ON d.UnitID = a.BaseUnitID
		LEFT OUTER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID AND pkg.Quantity = 1 AND a.BaseUnitID = pkg.UnitID 
		LEFT OUTER JOIN (
			SELECT ProductID, SUM(Quantity) InvQuantity FROM tblProductInventory
			GROUP BY ProductID
		) inv ON a.ProductID = inv.ProductID
	WHERE ProductCode NOT IN (SELECT ProductCode FROM tblSalesPerItem WHERE ProductCode NOT LIKE '%-%');
	
	-- exclude the ProductCode with '-' coz it's sure that's is with sales [ProductCode NOT LIKE '%-%']
	
END;
GO
delimiter ;


/*********************************
	procMatrixPackageUpdatePurchasing
	Lemuel E. Aceron
	CALL procMatrixPackageUpdatePurchasing();
	
	[10/25/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdatePurchasing
GO

create procedure procMatrixPackageUpdatePurchasing(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPurchasePrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		PurchasePrice	=	pvtPurchasePrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procProductTagActiveInactive
	Lemuel E. Aceron
	CALL procProductTagActiveInactive
	
	[10/28/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductTagActiveInactive
GO

create procedure procProductTagActiveInactive(IN intProductID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblProducts SET Active = intStatus WHERE ProductID = intProductID ;

END;
GO
delimiter ;

/*********************************
	procSyncContactCredit
	Lemuel E. Aceron
	CALL procSyncContactCredit();
	
	[12/18/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncContactCredit
GO

create procedure procSyncContactCredit()
BEGIN
	
	UPDATE tblContacts SET Credit = 0;

	UPDATE tblContacts, (SELECT ContactID, SUM(Amount) - SUM(AmountPaid) Credit 
						 FROM tblCreditPayment 
						 WHERE CreditReasonID <> 8 AND TerminalNo <> '00'
						 GROUP BY ContactID 
						) tblCreditPayment
	SET 
		tblContacts.Credit = IFNULL(tblCreditPayment.Credit,0)
	WHERE tblContacts.ContactID = tblCreditPayment.ContactID
		AND tblContacts.Credit <> IFNULL(tblCreditPayment.Credit,0);

END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSelling
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSelling();
	
	[01/01/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSelling
GO

create procedure procMatrixPackageUpdateSelling(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingWSPrice();
	
	[01/01/2010] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingWSPrice
GO

create procedure procMatrixPackageUpdateSellingWSPrice(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtWSPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductID
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductID();
	
	[01/01/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductID
GO

create procedure procMatrixPackageUpdateSellingUsingProductID(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductIDWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductIDWSPrice();
	
	[01/13/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductIDWSPrice
GO

create procedure procMatrixPackageUpdateSellingUsingProductIDWSPrice(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procTransactionAgentUpdate
	Lemuel E. Aceron
	
	[02/26/2010] - created this procedure

	[10/08/2010] - Added AgentPositionName and AgentDepartmentName

*********************************/
DROP PROCEDURE IF EXISTS procTransactionAgentUpdate;
delimiter GO

create procedure procTransactionAgentUpdate(IN intTransactionID bigint(20), IN lngAgentID BIGINT(20), IN strAgentName varchar(100), IN strAgentPositionName varchar(30), IN strAgentDepartmentName varchar(30))
BEGIN
	
	UPDATE tblTransactions SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = intTransactionID;

END;
GO
delimiter ;


/*********************************
	procProductCommisionUpdate
	Lemuel E. Aceron
	CALL procProductCommisionUpdate
	
	March 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCommisionUpdate
GO

create procedure procProductCommisionUpdate(IN intProductID BIGINT, IN decPercentageCommision DECIMAL(3,2))
BEGIN
	
	UPDATE tblProducts SET PercentageCommision = decPercentageCommision WHERE ProductID = intProductID ;

END;
GO
delimiter ;

/**************************************************************
	procGenerateAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAgentsCommision('1', 1, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAgentsCommision
GO

create procedure procGenerateAgentsCommision(
	IN strSessionID varchar(30),
	IN lngAgentID BIGINT(2),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateAllAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAllAgentsCommision('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAllAgentsCommision
GO

create procedure procGenerateAllAgentsCommision(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;

END;
GO
delimiter ;

/*********************************
	procStockTagActiveInactive
	Lemuel E. Aceron
	CALL procStockTagActiveInactive
	
	March 10,2010 - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procStockTagActiveInactive
GO

create procedure procStockTagActiveInactive(IN lngStockID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblStock SET Active = intStatus WHERE StockID = lngStockID;

END;
GO
delimiter ;

/*********************************
	procCheckTerminalLastDateInitialized
	Lemuel E. Aceron
	CALL procCheckTerminalLastDateInitialized
	
	This can be use to get the last initialization of zread 
	and previous initialization of zread.
	
	[03/10/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCheckTerminalLastDateInitialized
GO

create procedure procCheckTerminalLastDateInitialized()
BEGIN
	
	SELECT DateLastInitialized 'DateLastInitialized' FROM tblTerminalReport;
	
	SELECT DateLastInitialized 'PreviousDateLastInitialized' FROM tblTerminalReportHistory ORDER BY DateLastInitialized DESC LIMIT 1;

END;
GO
delimiter ;

/*********************************
	procFixItemsPurchaseAmount
	Lemuel E. Aceron
	CALL procFixItemsPurchaseAmount();
	
	This can be use to fix the item purchase amount if purchase amount is not consistent 
	with purchaseprice * quantity.
	
	April 8,2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procFixItemsPurchaseAmount
GO

create procedure procFixItemsPurchaseAmount()
BEGIN
	
	UPDATE tblTransactionItems SET PurchaseAmount = Purchaseprice * Quantity;

	UPDATE tblTransactionItems SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	
END;
GO
delimiter ;


/*********************************
	procPositionInsert
	Lemuel E. Aceron
	CALL procPositionInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionInsert
GO

create procedure procPositionInsert(
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	INSERT INTO tblPositions(PositionCode, PositionName)
	VALUES (pvtPositionCode, pvtPositionName);
		
END;
GO
delimiter ;


/*********************************
	procPositionUpdate
	Lemuel E. Aceron
	CALL procPositionUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionUpdate
GO

create procedure procPositionUpdate(
	IN pvtPositionID INT(10),
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	UPDATE tblPositions SET 
		PositionCode	= pvtPositionCode, 
		PositionName	= pvtPositionName
	WHERE PositionID	= pvtPositionID;
		
END;
GO
delimiter ;

/*********************************
	procDepartmentInsert
	Lemuel E. Aceron
	CALL procDepartmentInsert();
	
	September 21, 2010 - create this procedure
	August 4, 2014 - deleted and replaced with procSaveDepartment
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentInsert
GO
delimiter ;

/*********************************
	procDepartmentUpdate
	Lemuel E. Aceron
	CALL procDepartmentUpdate();
	
	September 21, 2010 - create this procedure
	August 4, 2014 - deleted and replaced with procSaveDepartment
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentUpdate
GO
delimiter ;

/*********************************
	procTransactionRelease
	Lemuel E. Aceron
	May 3, 2011
	For releasing of closed transaction.
*********************************/

DROP PROCEDURE IF EXISTS procTransactionRelease;
delimiter GO

create procedure procTransactionRelease(IN intTransactionID BIGINT(20), 
										IN intMonth SMALLINT(2) UNSIGNED ZEROFILL, 
										IN intTransactionStatus SMALLINT,
										IN lngReleasedByID BIGINT(20),
										IN strReleasedByName VARCHAR(100))
BEGIN
	SET @SQL = CONCAT('UPDATE tblTransactions', intMonth,' SET ');
	SET @SQL = CONCAT(@SQL,'	TransactionStatus=', intTransactionStatus,', ');
	SET @SQL = CONCAT(@SQL,'	ReleaserID=', lngReleasedByID,', ');
	SET @SQL = CONCAT(@SQL,'	ReleaserName=''', strReleasedByName,''', ');
	SET @SQL = CONCAT(@SQL,'	ReleasedDate=NOW() ');
	SET @SQL = CONCAT(@SQL,'WHERE TransactionID=',intTransactionID,'; ');
		
	PREPARE strCmd FROM @SQL;
	EXECUTE strCmd;
	DEALLOCATE PREPARE strCmd;
	
END;
GO
delimiter ;

/*********************************
call procTransactionRelease(1,1,9,1,'Administrator');
*********************************/

/**************************************************************

	procSyncProductVariationFromQuantity
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncProductVariationFromQuantityPerItem(9, 1);

	Jul 26, 2011 : Lemu
	- Added Insert to product movement history
	- Added Insert to inventory adjustment
	
	Jul 14, 2013 : Lemu
	- Remove this as replacement for the tblProductInventory. No need to synchronize vairation vs. product quantity
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItem
GO
delimiter ; 


/**************************************************************

	procSyncProductVariationFromQuantityAllItem
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncProductVariationFromQuantityAllItem(1);

	Mar 14, 2011 : Lemu
	- create this procedure
	
	July 14, 2013 : Lemu
	- remove this as replaced by tblProductInventory. No need to synchronize product quantity

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityAllItem
GO
delimiter ;

/********************************************
	procProductMovementInsert
	
	CALL procProductMovementInsert(9, 'test', 'test desc', 0, 'test matrix desc', 100, 30, 130, 100, 'PC', 'remarks', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel', 1, 1, 0);
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include BranchIDFrom, BranchIDTo to include inventory per branch.
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementInsert
GO

create procedure procProductMovementInsert(
	IN intProductID BIGINT,
	IN strProductCode VARCHAR(30),
	IN strProductDesc VARCHAR(50),
	IN lngMatrixID BIGINT,
	IN strMatrixDescription VARCHAR(100),
	IN decQuantityFrom DECIMAL(18,2),
	IN decQuantity DECIMAL(18,2),
	IN decQuantityTo DECIMAL(18,2),
	IN decMatrixQuantity DECIMAL(18,2),
	IN strUnitCode VARCHAR(5),
	IN strRemarks VARCHAR(150),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100),
	IN intBranchIDFrom INT(4),
	IN intBranchIDTo INT(4),
	IN intQuantityMovementType INT(1)
	)
BEGIN

	INSERT INTO tblProductMovement (ProductID,
									ProductCode,
									ProductDescription,
									MatrixID,
									MatrixDescription,
									QuantityFrom,
									Quantity,
									QuantityTo,
									MatrixQuantity,
									UnitCode,
									Remarks,
									TransactionDate,
									TransactionNo,
									CreatedBy,
									BranchIDFrom,
									BranchIDTo,
									QuantityMovementType)
							VALUES( intProductID,
									strProductCode,
									strProductDesc,
									lngMatrixID,
									strMatrixDescription,
									decQuantityFrom,
									decQuantity,									
									decQuantityTo,
									decMatrixQuantity,
									strUnitCode,
									strRemarks,
									dteTransactionDate,
									strTransactionNo,
									strCreatedBy,
									intBranchIDFrom,
									intBranchIDTo,
									intQuantityMovementType);
									
END;
GO
delimiter ;

/********************************************
	procProductAddQuantity
	
	CALL procProductAddQuantity(2, 2715, 2581, 10, 'purchase', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductAddQuantity
GO

create procedure procProductAddQuantity(
	IN intBranchID INT(4),
	IN intProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(8000),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decQuantityDiff DECIMAL(18,3) DEFAULT 0;

	/*********** add to main ***********/	
	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = intProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0) INTO decProductQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = intProductID;
	
	SET decQuantityDiff = decQuantity - decProductQuantity;

	-- Insert to product movement history
	CALL procProductMovementInsert(intProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, 0, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 0);
	
	IF EXISTS(SELECT Quantity FROM tblProductInventory WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN 
		IF decQuantity >= 0 THEN
			UPDATE tblProductInventory SET
				Quantity	= decQuantity + Quantity,
				QuantityIN  = decQuantity + QuantityIN
			WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
		ELSE
			UPDATE tblProductInventory SET
				Quantity	= decQuantity + Quantity,
				QuantityOut = decQuantity + QuantityOut
			WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
		END IF;
	ELSE
		IF decQuantity >= 0 THEN
			INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIN, QuantityOut)
			VALUES(intBranchID, intProductID, lngMatrixID, decQuantity, decQuantity, 0);
		ELSE
			INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIN, QuantityOut)
			VALUES(intBranchID, intProductID, lngMatrixID, decQuantity, 0, decQuantity);
		END IF;
	END IF;
									
	/*********** end add to main ***********/	
	
	-- Tag product as Active if quantity > 0
	IF (SELECT SUM(Quantity) FROM tblProductInventory WHERE ProductID = intProductID) > 0 THEN
		CALL procProductTagActiveInactive(intProductID, 1);
	END IF;

	-- Process sync of product that are sold without matrix but with existing matrix now
	-- CALL procSyncProductVariationFromQuantityPerItem(intProductID, intBranchID);
END;
GO
delimiter ;


/********************************************
	procProductSubtractQuantity
	
	CALL procProductSubtractQuantity(2, 2715, 484, 3, 'SALES', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSubtractQuantity
GO

create procedure procProductSubtractQuantity(
	IN intBranchID INT(4),
	IN intProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(8000),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decQuantityDiff DECIMAL(18,3) DEFAULT 0;

	/*********** subtract from main ***********/

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = intProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0) INTO decProductQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = intProductID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(intProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, -1 * decQuantity, decProductQuantity - decQuantity, 0, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 0);
	
	-- Subtract the quantity from Product table
	UPDATE tblProductInventory SET 
		Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = intProductID
		AND BranchID = intBranchID;
		
	/*********** end subtract from main ***********/
	
	-- Tag product as InActive if quantity <= 0
	IF (SELECT ShowItemMoreThanZeroQty FROM tblTerminal WHERE TerminalID = 1) = 1 THEN
		IF (SELECT SUM(Quantity) FROM tblProductInventory WHERE ProductID = intProductID) = 0 THEN
			CALL procProductTagActiveInactive(intProductID, 0);
		END IF;
	END IF;
	
	-- Process sync of product that are returned without matrix but with existing matrix now
	-- CALL procSyncProductVariationFromQuantityPerItem(intProductID, intBranchID);
									
END;
GO
delimiter ;

/**************************************************************

	procProductUpdateActualQuantity
	Lemuel E. Aceron
	Oct 24, 2011

	CALL procProductUpdateActualQuantity();
	
	Jul 14, 2013: replace procProductBaseVariationUpdateActualQuantity

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateActualQuantity
GO

create procedure procProductUpdateActualQuantity(
						IN intBranchID INT(4),
						IN intProductID bigint,
						IN lngMatrixID bigint,
						IN decQuantity numeric)
BEGIN
	
	IF EXISTS(SELECT ActualQuantity FROM tblProductInventory WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN 
		UPDATE tblProductInventory SET
			ActualQuantity	= decQuantity
		WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, ActualQuantity)
			VALUES(intBranchID, intProductID, lngMatrixID, decQuantity);
	END IF;
	
END;
GO
delimiter ;


/**************************************************************

	procProductMovementSelect

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procProductMovementSelect(2535, -1, '2014-10-27', '1900-01-01', 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementSelect
GO

create procedure procProductMovementSelect(
									IN intProductID BIGINT, 
									IN intMatrixID BIGINT, 
									IN dteStartTransactionDate DATETIME,
									IN dteEndTransactionDate DATETIME,
									IN intBranchID INT)
BEGIN
	SET @SQL := '';
	
	SET @SQL := 'SELECT
						ProductID,
						ProductCode, 
						ProductDescription,
						MatrixID,
						MatrixDescription, 
						SUM(QuantityFrom) QuantityFrom,
						SUM(Quantity) Quantity,
						SUM(QuantityTo) QuantityTo,
						SUM(matrixQuantity) matrixQuantity,
						UnitCode,
						Remarks,
						TransactionDate,
						TransactionNo,
						CreatedBy
					FROM tblProductMovement
					WHERE QuantityMovementType = 0 ';
	
	IF (intProductID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND ProductID = ', intProductID,' ');
	END IF;
	
	IF (intMatrixID <> -1) THEN
		SET @SQL = CONCAT(@SQL,'AND MatrixID = ', intMatrixID,' ');
	END IF;

	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND BranchIDTo = ', intBranchID,' ');
	END IF;

	IF (DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d') AND 
	    DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d')  <> DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate >= ''', dteStartTransactionDate,''' ');
	END IF;
	
	IF (DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d') AND 
	    DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d')  <> DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate <= ''', dteEndTransactionDate,''' ');
	END IF;
	
	SET @SQL = CONCAT(@SQL,'GROUP BY ProductID,
									ProductCode, 
									ProductDescription,
									MatrixID,
									MatrixDescription, 
									UnitCode,
									Remarks,
									TransactionDate,
									TransactionNo,
									CreatedBy ');

	SET @SQL = CONCAT(@SQL,'ORDER BY TransactionDate DESC, QuantityTo ASC ');

	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;


/**************************************************************

	procProductUpdateRIDByPO

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procProductUpdateRIDByPO(10);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRIDByPO
GO

create procedure procProductUpdateRIDByPO(IN lngPOID BIGINT)
BEGIN
	DECLARE intProductID, lngRID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, RID FROM tblPOItems WHERE POID = lngPOID; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblPOItems WHERE POID = lngPOID; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID, lngRID to be processed 
		FETCH curItems INTO intProductID, lngRID;
		
		-- Process the actual update of product RID
		CALL procProductUpdateRID(intProductID, lngRID);
		
		-- reset the ProductID, lngRID to be processed
		SET intProductID = 0; SET lngRID = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;

/**************************************************************

	procProductUpdateRID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procProductUpdateRID(3924, 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRID
GO

create procedure procProductUpdateRID(
									IN intProductID BIGINT, 
									IN lngRID BIGINT)
BEGIN
	-- Update the RID to Products table
	UPDATE tblProducts SET 
		RID	= lngRID 
	WHERE ProductID	= intProductID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerProduct

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerProduct(3139, '2011-09-28 00:00:00', '2011-09-28 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProduct
GO

create procedure procUpdateProductReorderOverStockPerProduct(IN intProductID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
	
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerProductID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerProductID(3011, 1, '2013-09-25 00:00:00', '2013-09-27 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProductID
GO

create procedure procUpdateProductReorderOverStockPerProductID(IN intProductID BIGINT, IN strSessionID VARCHAR(15), IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngRID BIGINT DEFAULT 0;
	DECLARE intAvgCounter INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE decQuantity, decTotalQuantity, decAverageSales, decTotalAverageSales, decIDC DECIMAL(18,3) DEFAULT 0;
	DECLARE intValidTransactionItemStatus INTEGER DEFAULT 0;
	DECLARE intOrderSlipItemStatus INTEGER DEFAULT 5;
	
	SELECT IFNULL(inv.Quantity,0) Quantity, prd.RID INTO decQuantity, lngRID 
	FROM (SELECT ProductID, RID FROM tblProducts WHERE ProductID = intProductID) prd
	LEFT OUTER JOIN (SELECT SUM(Quantity) Quantity, ProductID FROM tblProductInventory WHERE ProductID = intProductID AND BranchID = 1) inv ON prd.productID = inv.ProductID;
	
	SET intValidTransactionItemStatus = 0; SET intOrderSlipItemStatus = 5;
	SET intAvgCounter = 0; SET decTotalAverageSales = 0;
	
	-- SELECT * FROM tblCountingRef;
	-- SELECT intProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Get the average sales
	SELECT AVG(Quantity) INTO decAverageSales FROM 
			( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
									LEFT JOIN tblTransactions b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
									LEFT JOIN tblTransactionItems c ON b.TransactionID = c.TransactionID
													AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
													AND ProductID = intProductID
													AND TransactionDate BETWEEN dteStartDate AND dteEndDate
									WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
	SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	
	SET decAverageSales = decTotalAverageSales / intAvgCounter;
	
	-- Get the Inventory Days Covered of the existing Quantity
	-- SET decIDC = decQuantity / decAverageSales;
	IF decQuantity = 0 THEN
		SET decIDC = 0;
	ELSEIF decAverageSales = 0 THEN
		SET decIDC = decQuantity;
	ELSE
		SET decIDC = decQuantity / decAverageSales;
	END IF;
	  
	-- Get the daily average sales will be used as RIDMinThreshold
	IF decIDC <> 0 THEN
		IF (lngRID > decIDC) THEN
			SET decTotalQuantity = (lngRID * cdbl(decIDC)) - cdbl(decQuantity);
		ELSE
			SET decTotalQuantity = 0;
		END IF; 
	ELSE
		SET decTotalQuantity = 0;
	END IF; 
		
	-- IF (decIDC > lngRID) THEN 
	-- 	SET decTotalQuantity = decQuantity;
	-- ELSE
	-- 	SET decTotalQuantity = round(lngRID - decIDC) * decAverageSales;
	-- END IF; 
	
	-- For checking purposes uncomment this
	-- SELECT * FROM tblCountingRef;
	-- SELECT intProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Set the RIDMinThreshold and RIDMaxThreshold
	UPDATE tblProducts SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = intProductID;
	UPDATE tblProductBaseVariationsMatrix SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = intProductID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerRID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerRID(3, 0, '2013-09-10', '2013-10-23');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerRID
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerRID(IN lngSupplierID BIGINT, IN lngRID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0; 
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplier

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplier(1019, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplier
GO

create procedure procUpdateProductReorderOverStockPerSupplier(IN lngSupplierID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerGroup(IN lngSupplierID BIGINT, IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerSubGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerSubGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerSubGroup(IN lngSupplierID BIGINT, IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;																		
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerGroup
GO

create procedure procUpdateProductReorderOverStockPerGroup(IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSubGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSubGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSubGroup(IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStock

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStock('2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStock
GO

create procedure procUpdateProductReorderOverStock(IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO intProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(intProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET intProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsAddPoint
	Lemuel E. Aceron
	CALL procContactRewardsAddPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPoint
GO

create procedure procContactRewardsAddPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsDeductPoint
	Lemuel E. Aceron
	CALL procContactRewardsDeductPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsDeductPoint
GO

create procedure procContactRewardsDeductPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints - pvtRewardPoint WHERE CustomerID = pvtContactID;
	
	UPDATE tblContactRewards SET RedeemedPoints =	RedeemedPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardsAddPurchase
	Lemuel E. Aceron
	CALL procContactRewardsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPurchase
GO

create procedure procContactRewardsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardModify
	Lemuel E. Aceron
	CALL procContactRewardModify(2885);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardModify
GO

create procedure procContactRewardModify(IN lngCustomerID BIGINT, 
										IN strRewardCardNo VARCHAR(15), 
										IN intRewardActive TINYINT(1),
										IN decPoints DECIMAL(18,3),
										IN dteRewardAwardDate DATETIME,
										IN intRewardCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN dteBirthDate DATE)
BEGIN
	
	IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE RewardCardNo = strRewardCardNo)) THEN
		IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactRewards(CustomerID, RewardCardNo, RewardActive, RewardPoints, RewardAwardDate, RewardCardStatus, ExpiryDate, BirthDate) 
								  VALUES(lngCustomerID, strRewardCardNo, intRewardActive, decPoints, dteRewardAwardDate, intRewardCardStatus, dteExpiryDate, dteBirthDate);
		ELSE
			UPDATE tblContactRewards SET
				RewardCardNo = strRewardCardNo,
				RewardActive = intRewardActive,	
				RewardAwardDate = dteRewardAwardDate,
				RewardCardStatus = intRewardCardStatus,
				ExpiryDate = dteExpiryDate,
				BirthDate = dteBirthDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactRewards SET
			RewardCardNo = strRewardCardNo,
			RewardActive = intRewardActive,
			RewardAwardDate = dteRewardAwardDate,
			RewardCardStatus = intRewardCardStatus,
			ExpiryDate = dteExpiryDate,
			BirthDate = dteBirthDate
		WHERE
			RewardCardNo = strRewardCardNo;
	END IF;
	
	/*******************************
		CALL procContactRewardModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsMovementInsert
	Lemuel E. Aceron
	CALL procContactRewardsMovementInsert();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsMovementInsert
GO

create procedure procContactRewardsMovementInsert(
	IN lngCustomerID BIGINT(20),
	IN dteRewardDate DATETIME,
	IN decRewardPointsBefore DECIMAL(18,3),
	IN decRewardPointsAdjustment DECIMAL(18,3),
	IN decRewardPointsAfter DECIMAL(18,3),
	IN dteRewardExpiryDate DATE,
	IN strRewardReason VARCHAR(150),
	IN strTerminalNo VARCHAR(10),
	IN strCashierName VARCHAR(150),
	IN strTransactionNo VARCHAR(15))
BEGIN

	INSERT INTO tblContactRewardsMovement (
		CustomerID, RewardDate, RewardPointsBefore, RewardPointsAdjustment, RewardPointsAfter,
		RewardExpiryDate, RewardReason, TerminalNo, CashierName, TransactionNo
	)VALUES(
		lngCustomerID, dteRewardDate, decRewardPointsBefore, decRewardPointsAdjustment, decRewardPointsAfter,
		dteRewardExpiryDate, strRewardReason, strTerminalNo, strCashierName, strTransactionNo
	);
	
	
END;
GO
delimiter ;


/**************************************************************

	procProductUpdateRewardPoints
	Lemuel E. Aceron
	March 14, 2009

	CALL procProductUpdateRewardPoints(0,0,0,2);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRewardPoints
GO

create procedure procProductUpdateRewardPoints(
						IN lngProductGroupID BIGINT,
						IN lngProductSubGroupID BIGINT,
						IN intProductID BIGINT,
						IN decRewardPoints NUMERIC)
BEGIN
	IF (intProductID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductID = intProductID;
	ELSEIF (lngProductSubGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID = lngProductSubGroupID;
	ELSEIF (lngProductGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngProductGroupID);
	ELSE
		UPDATE tblProducts SET RewardPoints = decRewardPoints;
	END IF;
	
END;
GO
delimiter ;


/**************************************************************
	procProductBranchInventoryInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryInsert(1);
	
	Oct 6, 2009 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryInsert
GO

create procedure procProductBranchInventoryInsert(IN intProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = intProductID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID)VALUES (intBranchID, intProductID);
		END IF;

		INSERT INTO tblBranchInventoryMatrix(BranchID, ProductID, MatrixID, Quantity, QuantityIn)
		SELECT intBranchID, intProductID, MatrixID, Quantity, QuantityIn FROM tblProductBaseVariationsMatrix 
										 WHERE ProductID = intProductID 
												AND MatrixID NOT IN (SELECT DISTINCT MatrixID FROM tblBranchInventoryMatrix WHERE ProductID = intProductID AND BranchID = intBranchID);
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procSyncProductVariationFromQuantityPerItemAllBranch
	Lemuel E. Aceron
	CALL procSyncProductVariationFromQuantityPerItemAllBranch(1);
	
	Oct 28, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItemAllBranch
GO

create procedure procSyncProductVariationFromQuantityPerItemAllBranch(IN intProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		-- Put the correct quantity of the newly created variation based on Product Quantity
		CALL procSyncProductVariationFromQuantityPerItem(intProductID, intBranchID);
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procProductBranchInventoryMatrixInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixInsert(1);
	
	Oct 29, 2011 : Lemu - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixInsert
GO

create procedure procProductBranchInventoryMatrixInsert(IN intProductID BIGINT, IN lngMatrixID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch WHERE BranchID <> 1; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch WHERE BranchID <> 1; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID, MatrixID)VALUES (intBranchID, intProductID, MatrixID);
		END IF;
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryMatrixCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixCopyAllItems
GO

create procedure procProductBranchInventoryMatrixCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	SELECT COUNT(MatrixID) INTO lngCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO intProductID, lngMatrixID;
		
		CALL procProductBranchInventoryMatrixInsert(intProductID, lngMatrixID);
		
		SET intProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryCopyAllItems
GO

create procedure procProductBranchInventoryCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intProductID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE Deleted = 0; 
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO intProductID;
		
		CALL procProductBranchInventoryInsert(intProductID);
		
		SET intProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

-- CALL procProductBranchInventoryCopyAllItems();

/**************************************************************
	procContactCreditModify
	Lemuel E. Aceron
	CALL procContactCreditModify(5, 5, 0, 'Egay', '2011-11-01 01:00:00', 0, '2012-11-01 01:00:00', 1, 1000);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditModify
GO

create procedure procContactCreditModify(IN lngCustomerID BIGINT, 
										IN lngGuarantorID BIGINT, 
										IN intCreditCardTypeID VARCHAR(30),
										IN strCreditCardNo VARCHAR(20), 
										IN dteCreditAwardDate DATETIME,
										IN intCreditCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN intCreditActive TINYINT(1),
										IN decCreditLimit DECIMAL(18,3))
BEGIN
	
	UPDATE tblContacts SET
		IsCreditAllowed = intCreditActive,
		CreditLimit = decCreditLimit
	WHERE ContactID = lngCustomerID;
	
	IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CreditCardNo = strCreditCardNo)) THEN
		IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate) 
								  VALUES(lngCustomerID, lngGuarantorID, intCreditCardTypeID, strCreditCardNo, dteCreditAwardDate, intCreditCardStatus, dteExpiryDate);
		ELSE
			UPDATE tblContactCreditCardInfo SET
				CreditCardNo = strCreditCardNo,
				CreditAwardDate = dteCreditAwardDate,
				CreditCardStatus = intCreditCardStatus,
				ExpiryDate = dteExpiryDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactCreditCardInfo SET
			CreditCardNo = strCreditCardNo,
			CreditAwardDate = dteCreditAwardDate,
			CreditCardStatus = intCreditCardStatus,
			ExpiryDate = dteExpiryDate
		WHERE
			CreditCardNo = strCreditCardNo;
	END IF;
	
	/*******************************
		CALL procContactCreditModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactCreditsAddPurchase
	Lemuel E. Aceron
	CALL procContactCreditsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditsAddPurchase
GO

create procedure procContactCreditsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(18,3))
BEGIN

	UPDATE tblContactCreditCardInfo SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	fnProductQuantityConvert
	[3/19/2012] get the converted string equivalent of the quantity.
	Lemuel E. Aceron
	CALL fnProductQuantityConvert();
	
**************************************************************/
delimiter GO
DROP FUNCTION IF EXISTS fnProductQuantityConvert
GO

create function fnProductQuantityConvert(
	intProductID BIGINT,
	decProductQuantity DECIMAL(18,3),
	intProductUnitID INT
	) RETURNS VARCHAR(200) DETERMINISTIC
BEGIN
	DECLARE strRetValue VARCHAR(200) DEFAULT '';
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE intBaseUnitID, intBottomUnitID, intComparedUnitID INT DEFAULT 0;
	DECLARE decBaseUnitValue, decBottomUnitValue, decConvertedWholeQty, decConvertedRemainderQty DECIMAL(18,3) DEFAULT 0;
	DECLARE strBaseUnitCode, strBottomUnitCode VARCHAR(5);
	DECLARE curItems CURSOR FOR SELECT PUM.BaseUnitID, PUM.BaseUnitValue, PUM.BottomUnitValue, PUM.BottomUnitID, BottU.UnitCode
								FROM tblProductUnitMatrix PUM LEFT JOIN tblUnit BottU ON PUM.BottomUnitID = BottU.UnitID 
								WHERE ProductID = intProductID ORDER BY MatrixID ASC;
	
	SELECT COUNT(*) INTO lngCount FROM tblProductUnitMatrix PUM LEFT JOIN tblUnit BottU ON PUM.BottomUnitID = BottU.UnitID WHERE ProductID = intProductID;
	
	IF (intProductUnitID = 0) THEN
		SELECT P.BaseUnitID, U.UnitCode INTO intComparedUnitID, strBaseUnitCode FROM tblProducts P INNER JOIN tblUnit U ON P.BaseUnitID = U.UnitID WHERE ProductID = intProductID;
	ELSE
		SET intComparedUnitID = intProductUnitID;
		SELECT UnitCode INTO strBaseUnitCode FROM tblUnit WHERE UnitID = intProductUnitID;
	END IF;
	
	IF (lngCount > 0 AND decProductQuantity <> 0) THEN
		OPEN curItems;
		curItems: LOOP
			SET lngCtr = lngCtr + 1; 
			IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
			
			FETCH curItems INTO intBaseUnitID, decBaseUnitValue, decBottomUnitValue, intBottomUnitID, strBottomUnitCode;
			
			IF (intComparedUnitID = intBaseUnitID) THEN
				SET decConvertedRemainderQty = decProductQuantity MOD decBaseUnitValue;
				IF (decConvertedRemainderQty <> 0) THEN
					SET strRetValue = CONCAT(decConvertedRemainderQty, ' ', strBaseUnitCode, '; ', strRetValue);
					-- SET decProductQuantity = decProductQuantity - decConvertedRemainderQty;
				END IF;
				
				SET decProductQuantity = (decProductQuantity DIV decBaseUnitValue) * decBottomUnitValue;
				IF (decProductQuantity <> 0) THEN
					IF (lngCtr = lngCount) THEN
						SET strRetValue = CONCAT(decProductQuantity,' ', strBottomUnitCode,'; ', strRetValue);
					END IF;
				END IF;
				
				SET intComparedUnitID = intBottomUnitID;
				SET strBaseUnitCode = strBottomUnitCode;
			END IF;
			
			SET intBaseUnitID = 0;
			SET decBaseUnitValue = 0;
			SET decBottomUnitValue = 0;
			SET intBottomUnitID = 0;
			SET strBottomUnitCode = '';
		END LOOP curItems;
		CLOSE curItems;
		
		SET strRetValue = TRIM(strRetValue);
		SET strRetValue = LEFT(strRetValue, LENGTH(strRetValue) - 1);
	ELSE
		SET strRetValue = CONCAT(decProductQuantity,' ', strBaseUnitCode);
	END IF;
	
	RETURN strRetValue;
	/*******************************
		CALL fnProductQuantityConvert();
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procProductQuantityConvert
	Lemuel E. Aceron
	CALL procGetRewardPointsReport(0, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGetRewardPointsReport
GO

create procedure procGetRewardPointsReport(
	lngCustomerID BIGINT,
	dteTransactionDateFrom DATETIME,
	dteTransactionDateTo DATETIME
	) 
BEGIN
	
	SET @SQL = CONCAT('SELECT BranchID 
								,TerminalNo 
								,CashierName
								,CustomerID
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') TransactionDate
								,COUNT(TransactionNo) TransactionCount
								,SUM(RewardPointsPayment) AS RewardPointsPayment
								,SUM(RewardConvertedPayment) AS RewardConvertedPayment
							FROM tblTransactions
							WHERE CustomerID <> 1 AND TransactionStatus = 1 ');

	IF (lngCustomerID <> 0) THEN
		SET @SQL = CONCAT(@SQL, 'AND CustomerID = ',lngCustomerID,' ');
	END IF;

	IF (NOT ISNULL(dteTransactionDateFrom) AND DATE_FORMAT(dteTransactionDateFrom, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') >= DATE_FORMAT(''',dteTransactionDateFrom,''', ''%Y-%m-%d'') ');
	END IF;
	
	IF (NOT ISNULL(dteTransactionDateTo) AND DATE_FORMAT(dteTransactionDateTo, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteTransactionDateTo,''', ''%Y-%m-%d'') ');
	END IF;

	SET @SQL = CONCAT(@SQL, '
							GROUP BY BranchID
								,TerminalNo
								,CashierName
								,CustomerID
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'')
							ORDER BY BranchID
								,TerminalNo
								,CashierName
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'')  ');

	PREPARE strCmd FROM @SQL;
	EXECUTE strCmd;
	DEALLOCATE PREPARE strCmd;
	/*******************************
		CALL procGetRewardPointsReport();
	*******************************/
END;
GO
delimiter ;

/**************************************************************

	procProductIsExist

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procProductIsExist(4, 'ADVNTGE CARD - REPLACEMENT FEE');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductIsExist
GO

create procedure procProductIsExist(
									IN intProductID BIGINT, 
									IN strBarCode VARCHAR(30))
BEGIN
	SET @SQL := '';
	
	SET strBarCode = REPLACE(strBarCode, '''', '''''');

	SET @SQL := 'SELECT
						Count(1) ProductCount
					FROM tblProductPackage pkg INNER JOIN tblProducts prd ON pkg.ProductID = prd.ProductID
					WHERE prd.deleted = 0 ';
	
	IF (intProductID <> 0) THEN
		SET @SQL = CONCAT(@SQL,' AND pkg.ProductID <> ', intProductID,' AND ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	ELSEIF (intProductID = 0) THEN
		SET @SQL = CONCAT(@SQL,' AND ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	END IF;
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;

delimiter GO
DROP PROCEDURE IF EXISTS procSetupCalendarDate
GO

delimiter GO
CREATE PROCEDURE procSetupCalendarDate(IN strYear VARCHAR(4))
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DELETE FROM tblCalDate WHERE YEAR(CalDate) = strYear;
	REPEAT 
		insert into tblCalDate(CalDate)values(DATE_ADD(DATE_FORMAT(CONCAT(strYear,'-01-01'), '%Y-%m-%d'), INTERVAL lngCtr DAY));
		SET lngCtr = lngCtr + 1; 
		UNTIL lngCtr = 366
	END REPEAT;
END;
GO
delimiter ;

CALL procSetupCalendarDate('2012');
CALL procSetupCalendarDate('2013');

/**************************************************************
	procGenerateProductHistoryToProductMovement
	Lemuel E. Aceron
	CALL procGenerateProductHistoryToProductMovement();
	
	Mar 6, 2013 - Save all previous history of products to tblProductMovement
**************************************************************/

ALTER table tblProductMovement MODIFY MatrixDescription VARCHAR(100);
ALTER table tblProductMovement MODIFY Remarks VARCHAR(150);

delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistoryToProductMovement
GO

create procedure procGenerateProductHistoryToProductMovement()
BEGIN
	DECLARE dteStartTransactionDate DateTime;
	DECLARE dteEndTransactionDate DateTime;

	SET dteStartTransactionDate = '0001-01-01';
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, (SELECT DATE_ADD(MIN(transactiondate), INTERVAL -1 MINUTE) FROM tblProductMovement));
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, dteStartTransactionDate);

	SELECT MIN(transactiondate) AS 'tblProductMovement END Date', 
			dteEndTransactionDate AS EndTransactionDateToProcess 
	FROM tblProductMovement;

	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, b.ProductCode, COALESCE(c.Description,''), a.VariationMatrixID, IFNULL(c.Description, b.ProductCode) 'MatrixDescription', 0, CASE StockDirection
																																						WHEN 0 THEN a.Quantity
																																						WHEN 1 THEN -a.Quantity
																																					END AS Quantity, 0, 0,
									d.UnitCode, a.Remarks, a.StockDate, TransactionNo, 'SYSTEM AUTO-G', BranchID, BranchID, 1 'QuantityMovementType'
	FROM (((tblStockItems a
		INNER JOIN tblStock f ON a.StockID = f.StockID
		LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
		LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
		LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
		LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT ProductID, ProductCode, COALESCE(Description,''), VariationsMatrixID, MatrixDescription, 0, CASE TransactionItemStatus
																											WHEN 0 THEN -Quantity
																											WHEN 1 THEN 0
																											WHEN 2 THEN 0
																											WHEN 3 THEN Quantity
																											WHEN 4 THEN -Quantity
																										END AS Quantity, 0 'QuantityTo', 0 'MatrixQuantity',  
									ProductUnitCode, CASE TransactionItemStatus
														WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
														WHEN 1 THEN 'Void'
														WHEN 2 THEN 'Trash'
														WHEN 3 THEN 'Return'
														WHEN 4 THEN 'Refund'
													END AS Remarks, TransactionDate, TransactionNo,
									CashierName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Purchase Order from ',SupplierCode) AS Remarks, b.PODate AS TransactionDate, b.PONo AS TransactionNo,
									PurchaserName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, ProductCode, COALESCE(Description,''), a.VariationMatrixID, MatrixDescription, QuantityBefore, (QuantityNow - QuantityBefore) AS Quantity, QuantityNow, 0 'MatrixQuantity',
									a.UnitCode, CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks, InvAdjustmentDate AS TransactionDate,
									CONCAT('InvAdjID:' , a.InvAdjustmentID) AS TransactionNo, b.Name, 1, 1, 1 'QuantityMovementType'
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer In from ',SupplierCode) AS Remarks, b.TransferInDate AS TransactionDate, b.TransferInNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer out to ',SupplierCode) AS Remarks, b.TransferOutDate AS TransactionDate, b.TransferOutNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

END;
GO
delimiter ;


/**************************************************************

	procProductCopyPOSToActualByProductGroup
	Lemuel E. Aceron
	Sep 1, 2013 - Update Actual Quantity by POS Quantity by Group

	CALL procProductCopyPOSToActualByProductGroup();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCopyPOSToActualByProductGroup
GO

create procedure procProductCopyPOSToActualByProductGroup(
						IN intBranchID INT(4),
						IN lngProductGroupID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = Quantity 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID WHERE ProductGroupID = lngProductGroupID));
	
END;
GO
delimiter ;


/**************************************************************

	procProductCopyPOSToActualByProductSubGroup
	Lemuel E. Aceron
	Sep 24, 2014 - Update Actual Quantity by POS Quantity by Group

	CALL procProductCopyPOSToActualByProductSubGroup(1, 2);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCopyPOSToActualByProductSubGroup
GO

create procedure procProductCopyPOSToActualByProductSubGroup(
						IN intBranchID INT(4),
						IN intProductSubGroupID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = Quantity 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = intProductSubGroupID));
	
END;
GO
delimiter ;

/**************************************************************

	procProductCopyPOSToActualBySupplier
	Lemuel E. Aceron
	Sep 1, 2013 - Update Actual Quantity by POS Quantity by Supplier

	CALL procProductCopyPOSToActualBySupplier();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCopyPOSToActualBySupplier
GO

create procedure procProductCopyPOSToActualBySupplier(
						IN intBranchID INT(4),
						IN lngSupplierID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = Quantity 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID)
		OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = lngSupplierID));
END;
GO
delimiter ;

/**************************************************************

	procProductZeroOutActualQuantityByProductGroup
	Lemuel E. Aceron
	May 4, 2013 - Add updating of products by supplier

	CALL procProductZeroOutActualQuantityByProductGroup();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductZeroOutActualQuantityByProductGroup
GO

create procedure procProductZeroOutActualQuantityByProductGroup(
						IN intBranchID INT(4),
						IN lngProductGroupID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = 0 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID WHERE ProductGroupID = lngProductGroupID));
	
END;
GO
delimiter ;


/**************************************************************

	procProductZeroOutActualQuantityByProductSubGroup
	Lemuel E. Aceron
	Sep 24, 2014 - Add updating of products by subgroup

	CALL procProductZeroOutActualQuantityByProductSubGroup(1, 2);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductZeroOutActualQuantityByProductSubGroup
GO

create procedure procProductZeroOutActualQuantityByProductSubGroup(
						IN intBranchID INT(4),
						IN intProductSubGroupID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = 0 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = intProductSubGroupID));
	
END;
GO
delimiter ;


/**************************************************************

	procProductZeroOutActualQuantityBySupplier
	Lemuel E. Aceron
	May 4, 2013 - Add updating of products by supplier

	CALL procProductZeroOutActualQuantityBySupplier();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductZeroOutActualQuantityBySupplier
GO

create procedure procProductZeroOutActualQuantityBySupplier(
						IN intBranchID INT(4),
						IN lngSupplierID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = 0 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID)
		OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = lngSupplierID));
END;
GO
delimiter ;

/**************************************************************

	procLockUnlockProduct
	Lemuel E. Aceron

	CALL LockUnlockProductForSellingByProductGroup(1, 12, 0);

	Aug 4, 2013 - Add updating of products by product group
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS LockUnlockProductForSellingByProductGroup
GO

create procedure LockUnlockProductForSellingByProductGroup(
						IN intBranchID INT(4),
						IN lngProductGroupID bigint,
						IN bolisLock TINYINT(1))
BEGIN

	UPDATE tblProductInventory SET 
		isLock = bolisLock
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID WHERE ProductGroupID = lngProductGroupID));
	
	-- UPDATE tblProductInventory SET 
	--	isLock = bolisLock
	-- WHERE BranchID = intBranchID 
	--	AND ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID WHERE ProductGroupID = lngProductGroupID));
	

	UPDATE tblProductGroup SET
		isLock = bolisLock
	WHERE ProductGroupID = lngProductGroupID;
END;
GO
delimiter ;


/**************************************************************

	procLockUnlockProduct
	Lemuel E. Aceron

	CALL LockUnlockProductForSellingByProductSubGroup(1, 12, 0);

	Sep 24, 2014 - Add updating of products by product group
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS LockUnlockProductForSellingByProductSubGroup
GO

create procedure LockUnlockProductForSellingByProductSubGroup(
						IN intBranchID INT(4),
						IN intProductSubGroupID bigint,
						IN boisLock TINYINT(1))
BEGIN

	UPDATE tblProductInventory SET 
		isLock = boisLock
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = intProductSubGroupID));
	
	UPDATE tblProductSubGroup SET
		isLock = boisLock
	WHERE ProductSubGroupID = intProductSubGroupID;
END;
GO
delimiter ;


/**************************************************************

	procLockUnlockProduct
	Lemuel E. Aceron
	March 14, 2009

	CALL LockUnlockProductForSellingBySupplier();

	May 4, 2013 - Add updating of products by supplier
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS LockUnlockProductForSellingBySupplier
GO

create procedure LockUnlockProductForSellingBySupplier(
						IN intBranchID INT(4),
						IN lngSupplierID bigint,
						IN bolisLock TINYINT(1))
BEGIN

	UPDATE tblProductInventory SET 
		isLock = bolisLock
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID)
		OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = lngSupplierID));
	
	UPDATE tblContacts SET
		isLock = bolisLock
	WHERE ContactID = lngSupplierID;
END;
GO
delimiter ;


/**************************************************************

	procProductInventoryMonthlySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the all product package list

	CALL procProductInventoryMonthlySelect('2013-10', 1, 0,0,'','',0,0,0,2,0,1,0,'1900-01-01',0,1,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductInventoryMonthlySelect
GO

create procedure procProductInventoryMonthlySelect(
			 IN DateMonth varchar(7),
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN ProductGroupID bigint,
			 IN ProductSubGroupID bigint,
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN isSummary int,
			 IN dteExpiration datetime,
			 IN ForReorder int,
			 IN OverStock int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF ProductGroupID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductSubgroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = ',ProductGroupID,') ');
	END IF;

	IF ProductSubGroupID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductSubgroupID = ',ProductSubGroupID,' ');
	END IF;

	/***
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET SQLWhere = CONCAT(SQLWhere,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
	END IF;
	***/

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.PackageID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,prd.Price
							,prd.Price1 ,prd.Price2 ,prd.Price3 ,prd.Price4 ,prd.Price5 
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,SUM(IFNULL(inv.Quantity,0)) Quantity
							,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
							,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
							,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
                            ,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,IFNULL(mtrx.MinThreshold, prd.MinThreshold) MinThreshold
							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) MaxThreshold
							,prd.RID

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold
                            ,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,IFNULL(mtrx.MatrixID,0) MatrixID
							,CONCAT(prd.ProductDesc, '':'' , IFNULL(mtrx.Description,'''')) AS VariationDesc
							,IFNULL(mtrx.Description,'''') AS MatrixDescription
							,',IF(isSummary=1,'0','IFNULL(brnch.BranchID,0)'),' AS BranchID
							,',IF(isSummary=1,'''All''','IFNULL(brnch.BranchCode,''All'')'),' AS BranchCode
						FROM (SELECT prd.* ,pkg.PackageID, pkg.MatrixID
									,pkg.BarCode1 ,pkg.BarCode2 ,pkg.BarCode3 ,pkg.BarCode4
									,pkg.Price ,pkg.Price1 ,pkg.Price2 ,pkg.Price3 ,pkg.Price4 ,pkg.Price5 
									,pkg.WSPrice ,pkg.PurchasePrice ,pkg.VAT ,pkg.EVAT ,pkg.LocalTax
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;
	SET @SQL = CONCAT(@SQL, '
							  WHERE prd.deleted = 0 ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID ', IF(ProductSubGroupID=0,'',CONCAT('AND prdsg.ProductSubGroupID =',ProductSubGroupID)),'
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID ', IF(ProductGroupID=0,'',CONCAT('AND prdg.ProductGroupID =',ProductGroupID)),'
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND prd.MatrixID = mtrx.MatrixID
						INNER JOIN tblBranch brnch ON ',IF(BranchID=0,'1=1',Concat('brnch.BranchID=',BranchID)),'						
						INNER JOIN tblProductInventoryMonthly inv ON inv.ProductID = prd.ProductID AND prd.MatrixID = inv.MatrixID 
														',IF(BranchID=0,'AND brnch.BranchID = INV.BranchID ',Concat('AND INV.BranchID=',BranchID)),' 
														', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						WHERE IFNULL(mtrx.deleted, 0) = 0 AND inv.DateMonth = ''',DateMonth,''' ');
	
	
	IF isSummary = 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(mtrx.MatrixID,0) = ',MatrixID,' ');
	END IF;

	-- check only those with Quantity
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
		SET @SQL = CONCAT(@SQL,'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, '
						GROUP BY prd.ProductID
                            ,prd.PackageID
                            ,prd.BarCode1
                            ,prd.BarCode2
                            ,prd.BarCode3
                            ,prd.BarCode4
                            ,prd.ProductCode
                            ,prd.ProductDesc

                            ,prdsg.ProductGroupID
                            ,prdg.ProductGroupCode
                            ,prdg.ProductGroupName
                            ,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

                            ,prd.ProductSubGroupID
                            ,prdsg.ProductSubGroupCode
                            ,prdsg.ProductSubGroupName

                            ,prd.BaseUnitID
                            ,unt.UnitCode
                            ,unt.UnitName
                            ,prd.BaseUnitID
                            ,unt.UnitCode
                            ,unt.UnitName

                            ,prd.DateCreated
                            ,prd.Active
                            ,prd.Deleted

                            ,prd.SupplierID
                            ,supp.ContactCode
                            ,supp.ContactName

                            ,prd.IsItemSold
                            ,prd.Price
                            ,prd.WSPrice
                            ,prd.PurchasePrice
                            ,prd.PercentageCommision
                            ,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
                            ,prd.VAT
                            ,prd.EVAT
                            ,prd.LocalTax
                            ,prd.RewardPoints

                            ,prd.WillPrintProductComposition

                            ,mtrx.MinThreshold, prd.MinThreshold
                            ,mtrx.MaxThreshold, prd.MaxThreshold
                            ,prd.RID
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold

                            ,prd.ChartOfAccountIDPurchase
                            ,prd.ChartOfAccountIDSold
                            ,prd.ChartOfAccountIDInventory
                            ,prd.ChartOfAccountIDTaxPurchase
                            ,prd.ChartOfAccountIDTaxSold

                            ,mtrx.MatrixID
                            ,mtrx.Description
                            ',IF(isSummary=1,'',',IFNULL(brnch.BranchID,0)'),'
							',IF(isSummary=1,'',',IFNULL(brnch.BranchCode,''All'')'),' ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode, MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	IF ForReorder <> 0 THEN
		SET @SQL = CONCAT('SELECT * FROM (',@SQL,') INV WHERE Quantity <= MinThreshold ');
	ELSEIF OverStock <> 0 THEN
		SET @SQL = CONCAT('SELECT * FROM (',@SQL,') INV WHERE Quantity > MaxThreshold ');
	END IF;

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductInventorySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the all product package list

	CALL procProductInventorySelect(1, 0,0,'TEST','',0,0,0,2,0,1,0,'1900-01-01',1,0,null,null);

	CALL procProductInventorySelect(1, 99983,0,'','',0,0,0,2,0,100,0,'1900-01-01',0,0,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductInventorySelect
GO

create procedure procProductInventorySelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN ProductGroupID bigint,
			 IN ProductSubGroupID bigint,
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN isSummary int,
			 IN dteExpiration datetime,
			 IN ForReorder int,
			 IN OverStock int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',ProductCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF ProductGroupID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductSubgroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = ',ProductGroupID,') ');
	END IF;

	IF ProductSubGroupID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductSubgroupID = ',ProductSubGroupID,' ');
	END IF;

	/***
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET SQLWhere = CONCAT(SQLWhere,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
	END IF;
	***/

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.PackageID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,prd.Price
							,prd.Price1 ,prd.Price2 ,prd.Price3 ,prd.Price4 ,prd.Price5 
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,SUM(IFNULL(inv.Quantity,0)) Quantity
							,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
							,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
							,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
                            ,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,IFNULL(mtrx.MinThreshold, prd.MinThreshold) MinThreshold
							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) MaxThreshold
							,prd.RID

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold
                            ,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,prd.SequenceNo

							,IFNULL(mtrx.MatrixID,0) MatrixID
							,CONCAT(prd.ProductDesc, '':'' , IFNULL(mtrx.Description,'''')) AS VariationDesc
							,IFNULL(mtrx.Description,'''') AS MatrixDescription
							,',IF(isSummary=1,'0','IFNULL(brnch.BranchID,0)'),' AS BranchID
							,',IF(isSummary=1,'''All''','IFNULL(brnch.BranchCode,''All'')'),' AS BranchCode
						FROM (SELECT prd.* ,pkg.PackageID, pkg.MatrixID
									,pkg.BarCode1 ,pkg.BarCode2 ,pkg.BarCode3 ,IFNULL(pkg.BarCode4,'''') BarCode4
									,pkg.Price ,pkg.Price1 ,pkg.Price2 ,pkg.Price3 ,pkg.Price4 ,pkg.Price5 
									,pkg.WSPrice ,pkg.PurchasePrice ,pkg.VAT ,pkg.EVAT ,pkg.LocalTax
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;
	SET @SQL = CONCAT(@SQL, '
							  WHERE prd.deleted = 0 ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID ', IF(ProductSubGroupID=0,'',CONCAT('AND prdsg.ProductSubGroupID =',ProductSubGroupID)),'
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID ', IF(ProductGroupID=0,'',CONCAT('AND prdg.ProductGroupID =',ProductGroupID)),'
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND prd.MatrixID = mtrx.MatrixID
						LEFT OUTER JOIN tblBranch brnch ON ',IF(BranchID=0,'1=1',Concat('brnch.BranchID=',BranchID)),'						
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID = inv.MatrixID 
														',IF(BranchID=0,'AND brnch.BranchID = INV.BranchID ',Concat('AND INV.BranchID=',BranchID)),' 
														', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						WHERE IFNULL(mtrx.deleted, 0) = 0 ');
	
	
	IF isSummary = 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(mtrx.MatrixID,0) = ',MatrixID,' ');
	END IF;

	-- check only those with Quantity
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
		SET @SQL = CONCAT(@SQL,'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, '
						GROUP BY prd.ProductID
                            ,prd.PackageID
                            ,prd.BarCode1
                            ,prd.BarCode2
                            ,prd.BarCode3
                            ,prd.BarCode4
                            ,prd.ProductCode
                            ,prd.ProductDesc

                            ,prdsg.ProductGroupID
                            ,prdg.ProductGroupCode
                            ,prdg.ProductGroupName
                            ,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

                            ,prd.ProductSubGroupID
                            ,prdsg.ProductSubGroupCode
                            ,prdsg.ProductSubGroupName

                            ,prd.BaseUnitID
                            ,unt.UnitCode
                            ,unt.UnitName
                            ,prd.BaseUnitID
                            ,unt.UnitCode
                            ,unt.UnitName

                            ,prd.DateCreated
                            ,prd.Active
                            ,prd.Deleted

                            ,prd.SupplierID
                            ,supp.ContactCode
                            ,supp.ContactName

                            ,prd.IsItemSold
                            ,prd.Price
                            ,prd.WSPrice
                            ,prd.PurchasePrice
                            ,prd.PercentageCommision
                            ,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
                            ,prd.VAT
                            ,prd.EVAT
                            ,prd.LocalTax
                            ,prd.RewardPoints

                            ,prd.WillPrintProductComposition

                            ,mtrx.MinThreshold, prd.MinThreshold
                            ,mtrx.MaxThreshold, prd.MaxThreshold
                            ,prd.RID
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold

                            ,prd.ChartOfAccountIDPurchase
                            ,prd.ChartOfAccountIDSold
                            ,prd.ChartOfAccountIDInventory
                            ,prd.ChartOfAccountIDTaxPurchase
                            ,prd.ChartOfAccountIDTaxSold

							,prd.SequenceNo

                            ,mtrx.MatrixID
                            ,mtrx.Description
                            ',IF(isSummary=1,'',',IFNULL(brnch.BranchID,0)'),'
							',IF(isSummary=1,'',',IFNULL(brnch.BranchCode,''All'')'),' ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode, MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));


	IF ForReorder <> 0 THEN
		SET @SQL = CONCAT('SELECT * FROM (',@SQL,') INV WHERE Quantity <= MinThreshold ');
	ELSEIF OverStock <> 0 THEN
		SET @SQL = CONCAT('SELECT * FROM (',@SQL,') INV WHERE Quantity > MaxThreshold ');
	END IF;

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductSelect(0, '','',0,2,0,100,null,null);

	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSelect
GO

create procedure procProductSelect(
			 IN BranchID int,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN BarcodeFrom varchar(30),
			 IN BarcodeTo varchar(30),
			 IN ProductCodeFrom varchar(30),
			 IN ProductCodeTo varchar(30),
			 IN ProductSubGroupNameFrom varchar(30),
			 IN ProductSubGroupNameTo varchar(30),
			 IN ProductGroupNameFrom varchar(30),
			 IN ProductGroupNameTo varchar(30),
			 IN SupplierNameFrom varchar(30),
			 IN SupplierNameTo varchar(30),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductDesc LIKE ''%',ProductCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',ProductCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	-- additional filter for backend filtering
	IF IFNULL(BarcodeFrom,'') <> '' AND IFNULL(BarcodeTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (');
		SET SQLWhere = CONCAT(SQLWhere, '    (pkg.Barcode1 >= ''',BarcodeFrom,''' AND pkg.Barcode1 <= ''',BarcodeTo,''') ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR (pkg.Barcode2 >= ''',BarcodeFrom,''' AND pkg.Barcode2 <= ''',BarcodeTo,''') ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR (pkg.Barcode3 >= ''',BarcodeFrom,''' AND pkg.Barcode3 <= ''',BarcodeTo,''') ');
		SET SQLWhere = CONCAT(SQLWhere, '    ) ');
	ELSEIF IFNULL(BarcodeFrom,'') <> '' AND IFNULL(BarcodeTo,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.Barcode1 LIKE ''',BarcodeFrom,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR  pkg.Barcode2 LIKE ''',BarcodeFrom,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR  pkg.Barcode3 LIKE ''',BarcodeFrom,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '    ) ');
	ELSEIF IFNULL(BarcodeFrom,'') = '' AND IFNULL(BarcodeTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.Barcode1 LIKE ''',BarcodeTo,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR  pkg.Barcode2 LIKE ''',BarcodeTo,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, ' OR  pkg.Barcode3 LIKE ''',BarcodeTo,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '    ) ');
	END IF;

	IF IFNULL(ProductCodeFrom,'') <> '' AND IFNULL(ProductCodeTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode >= ''',ProductCodeFrom,''' ');
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode <= ''',ProductCodeTo,''' ');
	ELSEIF IFNULL(ProductCodeFrom,'') <> '' AND IFNULL(ProductCodeTo,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''',ProductCodeFrom,'%'' ');
	ELSEIF IFNULL(ProductCodeFrom,'') = '' AND IFNULL(ProductCodeTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''',ProductCodeTo,'%'' ');
	END IF;

	IF IFNULL(ProductSubGroupNameFrom,'') <> '' AND IFNULL(ProductSubGroupNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdsg.ProductSubGroupName >= ''',ProductSubGroupNameFrom,''' ');
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdsg.ProductSubGroupName <= ''',ProductSubGroupNameTo,''' ');
	ELSEIF IFNULL(ProductSubGroupNameFrom,'') <> '' AND IFNULL(ProductSubGroupNameTo,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdsg.ProductSubGroupName LIKE ''',ProductSubGroupNameFrom,'%'' ');
	ELSEIF IFNULL(ProductSubGroupNameFrom,'') = '' AND IFNULL(ProductSubGroupNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdsg.ProductSubGroupName LIKE ''',ProductSubGroupNameTo,'%'' ');
	END IF;

	IF IFNULL(ProductGroupNameFrom,'') <> '' AND IFNULL(ProductGroupNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdg.ProductGroupName >= ''',ProductGroupNameFrom,''' ');
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdg.ProductGroupName <= ''',ProductGroupNameTo,''' ');
	ELSEIF IFNULL(ProductGroupNameFrom,'') <> '' AND IFNULL(ProductGroupNameTo,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdg.ProductGroupName LIKE ''',ProductGroupNameFrom,'%'' ');
	ELSEIF IFNULL(ProductGroupNameFrom,'') = '' AND IFNULL(ProductGroupNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prdg.ProductGroupName LIKE ''',ProductGroupNameTo,'%'' ');
	END IF;

	IF IFNULL(SupplierNameFrom,'') <> '' AND IFNULL(SupplierNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND supp.ContactName >= ''',SupplierNameFrom,''' ');
		SET SQLWhere = CONCAT(SQLWhere, 'AND supp.ContactName <= ''',SupplierNameTo,''' ');
	ELSEIF IFNULL(SupplierNameFrom,'') <> '' AND IFNULL(SupplierNameTo,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND supp.ContactName LIKE ''',SupplierNameFrom,'%'' ');
	ELSEIF IFNULL(SupplierNameFrom,'') = '' AND IFNULL(SupplierNameTo,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND supp.ContactName LIKE ''',SupplierNameTo,'%'' ');
	END IF;

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prd.ProductSubGroupID
							,prd.ProductSubGroupCode
							,prd.ProductSubGroupName
							,prd.ProductGroupID
							,prd.ProductGroupCode
							,prd.ProductGroupName
							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,unt.UnitCode
							,unt.UnitName
							,prd.DateCreated
							,prd.Active

							,prd.SupplierID
							,prd.ContactCode SupplierCode
							,prd.ContactName SupplierName

							,prd.Price
							,prd.Price1 ,prd.Price2 ,prd.Price3 ,prd.Price4 ,prd.Price5 
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,prd.MinThreshold
							,prd.MaxThreshold

							,prd.RID

							,prd.MaxThreshold - SUM(IFNULL(inv.Quantity,0)) ReorderQty
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold
                            ,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,SUM(IFNULL(inv.Quantity,0)) Quantity
                            ,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
                            ,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
							,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
							,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
                            ,IFNULL(MAX(inv.IsLock),0) IsLock
						FROM (SELECT prd.* ,pkg.PackageID, pkg.MatrixID
									,pkg.BarCode1 ,pkg.BarCode2 ,pkg.BarCode3 ,pkg.BarCode4
									,pkg.Price ,pkg.Price1 ,pkg.Price2 ,pkg.Price3 ,pkg.Price4 ,pkg.Price5 
									,pkg.WSPrice ,pkg.PurchasePrice ,pkg.VAT ,pkg.EVAT ,pkg.LocalTax
									,prdsg.ProductSubGroupCode ,prdsg.ProductSubGroupName 
									,prdsg.ProductGroupID ,prdg.ProductGroupCode ,prdg.ProductGroupName
									,supp.ContactCode ,supp.ContactName
							  FROM tblProducts prd 
							  INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
							  INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
							  INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID
														AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;
	SET @SQL = CONCAT(@SQL, '
							  WHERE prd.deleted = 0 ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						-- INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						-- INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						-- INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID = inv.MatrixID',IF(BranchID=0,'',Concat('AND inv.BranchID=',BranchID)),' ', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						');

	SET @SQL = CONCAT(@SQL, ' 
						GROUP BY prd.ProductID
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc

							,prd.ProductSubGroupID
							,prd.ProductSubGroupCode
							,prd.ProductSubGroupName
							,prd.ProductGroupID
							,prd.ProductGroupCode
							,prd.ProductGroupName
							,prd.BaseUnitID
							,unt.UnitCode
							,unt.UnitName
							,prd.DateCreated
							,prd.Active

							,prd.SupplierID
							,prd.ContactCode
							,prd.ContactName

							,prd.Price
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints 
							,prd.MinThreshold
							,prd.MaxThreshold 
							
							,prd.RID

                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductCodeSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductCodeSelect(1,'4000003762208','',0,2,0,10,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCodeSelect
GO

create procedure procProductCodeSelect(
			 IN BranchID int,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',ProductCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 = ''',BarCode,''') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.ProductCode
							-- ,SUM(inv.Quantity) Quantity
							-- ,SUM(inv.ActualQuantity) ActualQuantity
						FROM (SELECT prd.ProductID, prd.ProductCode
								,pkg.MatrixID
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;
	SET @SQL = CONCAT(@SQL, '
							  WHERE prd.deleted = 0 ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						-- INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						-- INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						-- INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						-- INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						-- LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = prd.MatrixID ',IF(BranchID=0,'',Concat('AND inv.BranchID=',BranchID)),' ', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						');

	-- SET @SQL = CONCAT(@SQL, 'GROUP BY prd.ProductID, prd.ProductCode ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductMainDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductMainDetails(1, 48, 0, '','', false);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMainDetails
GO

create procedure procProductMainDetails(
			 IN BranchID int,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(60),
			 IN ProductCode varchar(60),
			 IN isQuantityGreaterThanZERO TINYINT(1))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	
	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''') ');
	ELSEIF IFNULL(ProductCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode = ''',ProductCode,''' ');
	END IF;
	IF MatrixID <> 0 AND MatrixID <> -1 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND pkg.MatrixID = ',MatrixID,' ');
	END IF;

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.PackageID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
														
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,prd.Price
							,prd.Price1 ,prd.Price2 ,prd.Price3 ,prd.Price4 ,prd.Price5 
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,SUM(IFNULL(inv.Quantity,0)) Quantity
                            ,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
                            ,SUM(IFNULL(inv.ReservedQuantity,0)) ReservedQuantity
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
                            ,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
                            ,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
							,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID
							,prd.SequenceNo

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold ');
	IF MatrixID <> -1 THEN
		SET @SQL = CONCAT(@SQL, '	
							,IFNULL(mtrx.MatrixID,0) MatrixID
							,IFNULL(CONCAT(prd.ProductDesc, '':'' , mtrx.Description),'''') AS VariationDesc ');
	ELSE
		SET @SQL = CONCAT(@SQL, '	
							,0 MatrixID
							,MAX(IFNULL(CONCAT(prd.ProductDesc, '':'' , mtrx.Description),'''')) AS VariationDesc ');
	END IF;
	SET @SQL = CONCAT(@SQL, '	
							,IFNULL(mtrx.Description,'''') MatrixDescription
						FROM (SELECT prd.*
									,pkg.PackageID
									,pkg.MatrixID
									,pkg.BarCode1
									,pkg.BarCode2
									,pkg.BarCode3
									,pkg.BarCode4
									
									,pkg.Price
									,pkg.Price1 ,pkg.Price2 ,pkg.Price3 ,pkg.Price4 ,pkg.Price5 
									,pkg.WSPrice
									,pkg.PurchasePrice
									,pkg.VAT
									,pkg.EVAT
									,pkg.LocalTax
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID
														-- AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;

	-- put the exempted products
	IF  IFNULL(BarCode,'') = 'CREDIT PAYMENT' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - REPLACEMENT FEE' THEN
		SET @SQL = CONCAT(@SQL, ' WHERE 1=1 ');	
	ELSE
		SET @SQL = CONCAT(@SQL, ' WHERE deleted=0 ');	
	END IF;

	SET @SQL = CONCAT(@SQL, '
							  ',SQLWhere,' ORDER BY Quantity ASC LIMIT 1) prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON prd.productID = mtrx.ProductID AND prd.MatrixID =  mtrx.MatrixID
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID =  inv.MatrixID ',IF(BranchID=0,'',Concat('AND inv.BranchID=',BranchID)),' ', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						');

	SET @SQL = CONCAT(@SQL, '
						GROUP BY 
							prd.ProductID
						   ,prd.PackageID
						   ,prd.BarCode1
						   ,prd.BarCode2
						   ,prd.BarCode3
						   ,prd.BarCode4
						   ,prd.ProductCode
						   ,prd.ProductDesc
						   
						   ,prdsg.ProductGroupID
						   ,prdg.ProductGroupCode
						   ,prdg.ProductGroupName
						   ,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

						   ,prd.ProductSubGroupID
						   ,prdsg.ProductSubGroupCode
						   ,prdsg.ProductSubGroupName

						   ,prd.BaseUnitID
						   ,unt.UnitCode
						   ,unt.UnitName
						   ,prd.BaseUnitID
						   ,unt.UnitCode
						   ,unt.UnitName

						   ,prd.DateCreated
						   ,prd.Active
						   ,prd.Deleted

						   ,prd.SupplierID
						   ,supp.ContactCode
						   ,supp.ContactName

						   ,prd.IsItemSold
						   ,prd.Price
						   ,prd.WSPrice
						   ,prd.PurchasePrice
						   ,prd.PercentageCommision
						   ,prd.IncludeInSubtotalDiscount
						   ,prd.IsCreditChargeExcluded
						   ,prd.VAT
						   ,prd.EVAT
						   ,prd.LocalTax
						   ,prd.RewardPoints

						   ,prd.WillPrintProductComposition

						   ,prd.MinThreshold
						   ,prd.MaxThreshold
						   ,prd.RID
						   ,prd.SequenceNo

						   ,prd.MaxThreshold
						   ,prd.RIDMinThreshold
						   ,prd.RIDMaxThreshold
						   ,prd.RIDMaxThreshold

						   ,prd.ChartOfAccountIDPurchase
						   ,prd.ChartOfAccountIDSold
						   ,prd.ChartOfAccountIDInventory
						   ,prd.ChartOfAccountIDTaxPurchase
						   ,prd.ChartOfAccountIDTaxSold ');

	IF MatrixID <> -1 THEN
		SET @SQL = CONCAT(@SQL, '	
						   ,mtrx.MatrixID
						   ,mtrx.Description ');
	END IF;

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;
	
END;
GO
delimiter ;


/**************************************************************

	procProductVaritionMatrixSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductVaritionMatrixSelect(0, 4355, '', '', '', 0, 2, 0, 10, null, null);
	
**************************************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procProductVaritionMatrixSelect
GO

create procedure procProductVaritionMatrixSelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN MatrixDescription varchar(60),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');
	SET MatrixDescription = REPLACE(MatrixDescription, '''', '''''');

	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;
	
	IF IFNULL(MatrixDescription,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND IFNULL(mtrx.Description,'''') LIKE ''%',MatrixDescription,'%'' ');
	END IF;

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.PackageID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,prd.Price
							,prd.Price1 ,prd.Price2 ,prd.Price3 ,prd.Price4 ,prd.Price5 
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,SUM(IFNULL(inv.Quantity,0)) Quantity
                            ,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
                            ,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
                            ,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
                            ,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
							,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID
							,prd.SequenceNo

							,IFNULL(mtrxMaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,prd.MatrixID
							,CONCAT(prd.ProductDesc, '':'' , prd.Description) AS VariationDesc
							,prd.Description AS MatrixDescription
						FROM (SELECT prd.* ,pkg.PackageID ,pkg.MatrixID
									,pkg.BarCode1 ,pkg.BarCode2 ,pkg.BarCode3 ,pkg.BarCode4
									,pkg.Price ,pkg.Price1 ,pkg.Price2 ,pkg.Price3 ,pkg.Price4 ,pkg.Price5 
									,pkg.WSPrice ,pkg.PurchasePrice
									,pkg.VAT ,pkg.EVAT ,pkg.LocalTax
									,mtrx.Description ,mtrx.MinThreshold mtrxMinThreshold, mtrx.MaxThreshold mtrxMaxThreshold
							  FROM tblProducts prd 
							  INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND pkg.Quantity = 1
														AND pkg.MatrixID = mtrx.MatrixID ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;

	-- put the exempted products
	IF  IFNULL(BarCode,'') = 'CREDIT PAYMENT' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - REPLACEMENT FEE' THEN
		SET @SQL = CONCAT(@SQL, ' WHERE 1=1 ');	
	ELSE
		SET @SQL = CONCAT(@SQL, ' WHERE prd.deleted=0 ');	
	END IF;

	SET @SQL = CONCAT(@SQL, '
							  ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID = inv.MatrixID ',IF(BranchID=0,'',Concat('AND inv.BranchID=',BranchID)),' ', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						');

	SET @SQL = CONCAT(@SQL, '
						GROUP BY 
							 prd.ProductID
							,prd.PackageID
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.OrderSlipPrinter1 ,prd.OrderSlipPrinter2 ,prd.OrderSlipPrinter3 ,prd.OrderSlipPrinter4 ,prd.OrderSlipPrinter5

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode
							,unt.UnitName
							,prd.BaseUnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode
							,supp.ContactName

							,prd.IsItemSold
							,prd.Price
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.IsCreditChargeExcluded
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID
							,prd.SequenceNo

							,prd.MaxThreshold
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,prd.MatrixID
							,prd.Description ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductVaritionMatrixDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the product and matrix packages

	CALL procProductPackageSelect(0, 0, 22269, '', '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageSelect
GO

create procedure procProductPackageSelect(
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductGroupName varchar(30),
			 IN ProductSubGroupName varchar(30),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductGroupName = REPLACE(ProductGroupName, '''', '''''');
	SET ProductSubGroupName = REPLACE(ProductSubGroupName, '''', '''''');

	SET @SQL = '		SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,IFNULL(mtrx.Description,prd.ProductCode) ProductDesc
							,IF(IFNULL(mtrx.Description,'''') <> '''', CONCAT(prd.ProductDesc, '':'' , mtrx.Description), prd.ProductDesc) Description

							,pkg.UnitID
							,unt.UnitCode
							,unt.UnitName

							,pkg.Price
							,pkg.Price1
							,pkg.Price2
							,pkg.Price3
							,pkg.Price4
							,pkg.Price5
							,pkg.WSPrice
							,pkg.PurchasePrice
							,pkg.Quantity
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,IFNULL(mtrx.MatrixID,0) MatrixID
							,IFNULL(mtrx.Description,'''') MatrixDescription
						FROM tblProductPackage pkg
						INNER JOIN tblProducts prd ON pkg.ProductID = prd.ProductID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.MatrixID = mtrx.MatrixID AND mtrx.ProductID = prd.ProductID
						WHERE prd.deleted = 0 ';
	
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	END IF;

	IF IFNULL(ProductGroupName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prdg.ProductGroupName LIKE ''%',BarCode,'%'' ');
	END IF;

	IF IFNULL(ProductSubGroupName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prdsg.ProductSubGroupName LIKE ''%',BarCode,'%'' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','PackageID',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

/**************************************************************

	procProductPriceHistorySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductPriceHistorySelect(null, null, 3057, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPriceHistorySelect
GO

create procedure procProductPriceHistorySelect(
			 IN StartChangeDate datetime,
			 IN EndChangeDate datetime,
			 IN ProductID bigint,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 hst.PackageID
							,pkg.ProductID
							,pkg.MatrixID
							,prd.ProductCode
							,IF(ISNULL(mtrx.Description), prd.ProductDesc, CONCAT(prd.ProductDesc, '':'' , mtrx.Description)) AS Description 
							,pkg.UnitID
							,unt.UnitCode
							,unt.UnitName
							,hst.ChangeDate
							,pkg.Quantity
							,hst.PurchasePriceBefore
							,hst.PurchasePriceNow
							,hst.SellingPriceBefore
							,hst.SellingPriceNow
							,hst.Price1Now
							,hst.Price2Now
							,hst.Price3Now
							,hst.Price4Now
							,hst.Price5Now
							,hst.VATBefore
							,hst.VATNow
							,hst.EVATBefore
							,hst.EVATNow
							,hst.LocalTaxBefore
							,hst.LocalTaxNow
							,hst.Remarks
							,usr.name
						FROM tblProductPackagePriceHistory hst
						INNER JOIN tblProductPackage pkg ON hst.PackageID = pkg.PackageID
						INNER JOIN tblProducts prd ON prd.ProductID = pkg.ProductID
						INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
						INNER JOIN sysAccessUserDetails usr ON usr.UID = hst.UID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.MatrixID = mtrx.MatrixID AND pkg.ProductID = mtrx.ProductID 
						WHERE 1=1 ');

	IF IFNULL(StartChangeDate,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate >= ''',StartChangeDate,''' ');
	END IF;

	IF IFNULL(EndChangeDate,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate <= ''',EndChangeDate,''' ');
	END IF;

	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','hst.ChangeDate',SortField),' ',IFNULL(SortOrder,'DESC'),' ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/**************************************************************
	10Aug2013: LEAceron		procUpdatetblInventorySG
	Desc: Update the pproductsubgroupids in tblInventory

	This is triggered when Closing Inventory By Group

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdatetblInventorySG
GO

create procedure procUpdatetblInventorySG()
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

CALL procUpdatetblInventorySG();

/**************************************************************

	procZeroOutInventory
	Lemuel E. Aceron
	Jan 13, 2015

	CALL procZeroOutInventory(1, 1, '2015-01-11', '00010');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutInventory
GO

create procedure procZeroOutInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30))
BEGIN
	DECLARE strRemarks VARCHAR(250) DEFAULT '';
	
	-- STEP 1: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-ZERO OUT INVENTORY BranchID:', intBranchID);

		-- STEP 2: Insert to product movement history
		-- CALL procProductMovementInsert(intProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
		-- 								decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
		-- 								strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);

		INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription,
									MatrixID, MatrixDescription, 
									QuantityFrom, Quantity, QuantityTo,
									MatrixQuantity, UnitCode, Remarks, TransactionDate,
									TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
		SELECT prd.ProductID, prd.ProductCode, prd.ProductDesc, 
							IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, 
							IFNULL(inv.Quantity,0) Quantity, IFNULL(-inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, 
							IFNULL(inv.ActualQuantity,0) ActualQuantity, unt.UnitCode, strRemarks, now(),
							strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0
						FROM tblProducts prd
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
						INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
						INNER JOIN (
							SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
						) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
						WHERE prd.Deleted = 0 AND prd.Active = 1
						ORDER BY prd.ProductCode, MatrixDescription;

		
		
		-- STEP 3: Insert to inventory adjustment
		
		-- CALL procInvAdjustmentInsert(lngUID, dteClosingDate, intProductID, strProductCode, strDescription, lngMatrixID,
		--										strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
		--										decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));

		INSERT INTO tblInvAdjustment(UID, InvAdjustmentDate, ProductID, ProductCode, Description, 
							VariationMatrixID, MatrixDescription, UnitID, UnitCode, 
							QuantityBefore, QuantityNow, MinThresholdBefore, MinThresholdNow, 
							MaxThresholdBefore, MaxThresholdNow, Remarks)
		SELECT lngUID, dteClosingDate, prd.ProductID, prd.ProductCode, prd.ProductDesc, 
							IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, 
							IFNULL(inv.Quantity,0) Quantity, 0, prd.MinThreshold, prd.MinThreshold AS MinThresholdNow, 
							prd.MaxThreshold, prd.MaxThreshold AS MaxThresholdNow, CONCAT(strRemarks, ' ', strReferenceNo)
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								INNER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;

		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET QuantityIN = 0, QuantityOUT = 0 WHERE BranchID = intBranchID;
		UPDATE tblProductInventory SET ReservedQuantity = 0 WHERE BranchID = intBranchID;
		UPDATE tblProductInventory SET Quantity = 0, ActualQuantity = 0 WHERE BranchID = intBranchID;
		
END;
GO
delimiter ;

/**************************************************************

	procZeroOutNegativeInventory
	Lemuel E. Aceron
	Jan 13, 2015

	CALL procZeroOutNegativeInventory(1, 1, '2015-01-11', '00010');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutNegativeInventory
GO

create procedure procZeroOutNegativeInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30))
BEGIN
	DECLARE strRemarks VARCHAR(250) DEFAULT '';
	
	-- STEP 1: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-ZERO OUT NEG-INVENTORY BranchID:', intBranchID);

		-- STEP 2: Insert to product movement history
		-- CALL procProductMovementInsert(intProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
		-- 								decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
		-- 								strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);

		INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription,
									MatrixID, MatrixDescription, 
									QuantityFrom, Quantity, QuantityTo,
									MatrixQuantity, UnitCode, Remarks, TransactionDate,
									TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
		SELECT prd.ProductID, prd.ProductCode, prd.ProductDesc, 
							IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, 
							IFNULL(inv.Quantity,0) Quantity, IFNULL(-inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, 
							IFNULL(inv.ActualQuantity,0) ActualQuantity, unt.UnitCode, strRemarks, now(),
							strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0
						FROM tblProducts prd
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
						INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
						INNER JOIN (
							SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=1 GROUP BY ProductID, MatrixID
						) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
						WHERE prd.Deleted = 0 AND prd.Active = 1 AND inv.Quantity < 0
						ORDER BY prd.ProductCode, MatrixDescription;

		
		
		-- STEP 3: Insert to inventory adjustment
		
		-- CALL procInvAdjustmentInsert(lngUID, dteClosingDate, intProductID, strProductCode, strDescription, lngMatrixID,
		--										strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
		--										decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));

		INSERT INTO tblInvAdjustment(UID, InvAdjustmentDate, ProductID, ProductCode, Description, 
							VariationMatrixID, MatrixDescription, UnitID, UnitCode, 
							QuantityBefore, QuantityNow, MinThresholdBefore, MinThresholdNow, 
							MaxThresholdBefore, MaxThresholdNow, Remarks)
		SELECT lngUID, dteClosingDate, prd.ProductID, prd.ProductCode, prd.ProductDesc, 
							IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, 
							IFNULL(inv.Quantity,0) Quantity, 0, prd.MinThreshold, prd.MinThreshold AS MinThresholdNow, 
							prd.MaxThreshold, prd.MaxThreshold AS MaxThresholdNow, CONCAT(strRemarks, ' ', strReferenceNo)
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								INNER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.Deleted = 0 AND prd.Active = 1 AND inv.Quantity < 0
								ORDER BY prd.ProductCode, MatrixDescription;

		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET QuantityIN = 0, QuantityOUT = 0 WHERE BranchID = intBranchID AND Quantity < 0;
		UPDATE tblProductInventory SET ReservedQuantity = 0 WHERE BranchID = intBranchID AND Quantity < 0;
		UPDATE tblProductInventory SET Quantity = 0, ActualQuantity = 0 WHERE BranchID = intBranchID AND Quantity < 0;
		

END;
GO
delimiter ;

/**************************************************************

	procCloseInventory
	Lemuel E. Aceron
	March 14, 2009

	CALL procCloseInventory(1, 1, '2013-07-26', '00001', 2565, 'RetailPlus');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCloseInventory
GO

create procedure procCloseInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngContactID bigint,
									IN strContactCode varchar(150))
BEGIN
	
	DECLARE intProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(18,3) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(100) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT prd.ProductID, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.SupplierID = lngContactID AND prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;

								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
									
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.SupplierID = lngContactID AND prd.Deleted = 0 AND prd.Active = 1;

								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	/*
	INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice)  
									SELECT intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
										strReferenceNo, lngContactID, strContactCode, 
										prd.ProductID, prd.ProductCode, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription,
										IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, 
										pkg.PurchasePrice * IFNULL(inv.ActualQuantity,0) * 0.12, 
										pkg.PurchasePrice * IFNULL(inv.ActualQuantity,0), PurchasePrice
									FROM tblProducts prd
									INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
									INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																	AND prd.BaseUnitID = pkg.UnitID
																	AND pkg.Quantity = 1 
									LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
									LEFT OUTER JOIN (
										SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
									) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
									WHERE IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
										AND prd.SupplierID = lngContactID AND prd.Deleted = 0;
	*/						
	-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-CLOSING INVENTORY BY SUPPLIER:', strContactCode);

	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO intProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT intProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: Insert to tblInventory
		INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngContactID, strContactCode, 
									intProductID, strProductCode, lngMatrixID, strMatrixDescription,
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
					
		-- STEP 2: Insert to product movement history
		CALL procProductMovementInsert(intProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);
		
		-- STEP 3: Insert to inventory adjustment
		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, intProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET Quantity = decProductActualQuantity WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		
		
		UPDATE tblProductInventory SET QuantityIN = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		UPDATE tblProductInventory SET QuantityOUT = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;

		SET intProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
	CALL procUpdatetblInventorySG();

END;
GO
delimiter ;





/**************************************************************

	procCloseInventoryByProductGroup
	Lemuel E. Aceron
	March 14, 2009

	CALL procCloseInventoryByProductGroup(1, 1, '2013-07-26', '00010', 7, 'GALENICALS');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCloseInventoryByProductGroup
GO

create procedure procCloseInventoryByProductGroup(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngProductGroupID bigint,
									IN strProductGroupName varchar(150))
BEGIN
	
	DECLARE intProductID, lngMatrixID, lngSupplierID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(18,3) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strSupplierCode VARCHAR(150) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(250) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT prd.ProductID, prd.SupplierID, cntct.ContactCode, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prdsg.ProductGroupID = lngProductGroupID AND prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;
								
								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
									
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prdsg.ProductGroupID = lngProductGroupID AND prd.Deleted = 0 AND prd.Active = 1;

								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-CLOSING INVENTORY BY GROUP:', strProductGroupName);

	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO intProductID, lngSupplierID, strSupplierCode, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT intProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: Insert to tblInventory
		INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngSupplierID, strSupplierCode, 
									intProductID, strProductCode, lngMatrixID, strMatrixDescription,
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
					
		-- STEP 2: Insert to product movement history
		CALL procProductMovementInsert(intProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);
		
		-- STEP 3: Insert to inventory adjustment
		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, intProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET Quantity = decProductActualQuantity WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		
		
		UPDATE tblProductInventory SET QuantityIN = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		UPDATE tblProductInventory SET QuantityOUT = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;

		SET intProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	

END;
GO
delimiter ;



/**************************************************************

	procCloseInventoryByProductSubGroup
	Lemuel E. Aceron
	March 14, 2009

	CALL procCloseInventoryByProductSubGroup(1, 1, '2013-07-26', '00010', 7, 'GALENICALS');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCloseInventoryByProductSubGroup
GO

create procedure procCloseInventoryByProductSubGroup(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngProductSubGroupID bigint,
									IN strProductSubGroupName varchar(150))
BEGIN
	
	DECLARE intProductID, lngMatrixID, lngSupplierID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(18,3) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strSupplierCode VARCHAR(150) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(250) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT prd.ProductID, prd.SupplierID, cntct.ContactCode, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prdsg.ProductSubGroupID = lngProductSubGroupID AND prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;
								
								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
									
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prdsg.ProductSubGroupID = lngProductSubGroupID AND prd.Deleted = 0 AND prd.Active = 1;

								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-CLOSING INVENTORY BY SUBGROUP:', strProductSubGroupName);

	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO intProductID, lngSupplierID, strSupplierCode, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT intProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: Insert to tblInventory
		INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngSupplierID, strSupplierCode, 
									intProductID, strProductCode, lngMatrixID, strMatrixDescription,
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
					
		-- STEP 2: Insert to product movement history
		CALL procProductMovementInsert(intProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);
		
		-- STEP 3: Insert to inventory adjustment
		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, intProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET Quantity = decProductActualQuantity WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		
		
		UPDATE tblProductInventory SET QuantityIN = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;
		UPDATE tblProductInventory SET QuantityOUT = 0 WHERE BranchID = intBranchID AND ProductID = intProductID AND MatrixID = lngMatrixID;

		SET intProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	

END;
GO
delimiter ;

/********************************************
	procSaveParkingRate
	
	Jul 21, 2013 - create this procedure
	August 17, 2014 - deleted and replaced with procSaveParkingRates
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSaveParkingRate
GO
delimiter ;


/**************************************************************

	procParkingRateSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procParkingRateSelect(0, 5448, NULL, NULL, NULL, NULL, NULL);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procParkingRateSelect
GO

create procedure procParkingRateSelect(
			 	IN lngParkingRateID BIGINT,
				IN intProductID BIGINT,
				IN strDayOfWeek VARCHAR(9),
				IN strStartTime VARCHAR(5),
				IN strEndtime VARCHAR(5),
				IN SortField varchar(60),
				IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 rte.ParkingRateID
							,rte.ProductID
							,rte.DayOfWeek
							,rte.StartTime
							,rte.EndTime
							,rte.NoOfUnitperMin
							,rte.PerUnitPrice
							,rte.MinimumStayInMin
							,rte.MinimumStayPrice
							,rte.CreatedByName
							,rte.LastUpdatedByName
							,rte.CreatedOn
							,rte.LastModified
						FROM tblParkingRates rte
						WHERE 1=1 ');

	IF lngParkingRateID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND rte.ParkingRateID >= ',lngParkingRateID,' ');
	END IF;

	IF intProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND rte.ProductID = ',intProductID,' ');
	END IF;

	IF IFNULL(strDayOfWeek,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND rte.DayOfWeek = ''',strDayOfWeek,''' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','rte.ParkingRateID',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procSalutationSelect
	Lemuel E. Aceron
	Aug 9, 2013
	
	Desc: This will get the all salutation's list

	CALL procSalutationSelect(null, null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSalutationSelect
GO

create procedure procSalutationSelect(
			IN Salutation varchar(30),
			IN SortField varchar(60),
			IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 SalutationCode
							,SalutationName
						FROM tblSalutations cnfg
						WHERE 1=1 ');
	
	IF IFNULL(Salutation,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND SalutationCode LIKE ''%',Salutation,'%'' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','cnfg.SalutationCode',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/**************************************************************

	procSysConfigSelectByName
	Lemuel E. Aceron
	Sep 9, 2013
	
	Desc: This will get any value from sysConfig

	CALL procSysConfigSelectByName('WillDeductTFInZRead');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSysConfigSelectByName
GO

create procedure procSysConfigSelectByName(
			IN ConfigValue varchar(100))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							ConfigValue
						FROM sysConfig cnfg
						WHERE ConfigName = ''',IFNULL(ConfigValue,''),''' ');
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/**************************************************************

	procSysCreditConfigSelectByName
	Lemuel E. Aceron
	Sep 9, 2013
	
	Desc: This will get any value from SysCreditConfig

	CALL procSysCreditConfigSelectByName('BillingDate');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSysCreditConfigSelectByName
GO

create procedure procSysCreditConfigSelectByName(
			IN ConfigValue varchar(100))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							ConfigValue
						FROM SysCreditConfig cnfg
						WHERE ConfigName = ''',IFNULL(ConfigValue,''),''' ');
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/*********************************
	procCheckDuplicateBarcode
	Lemuel E. Aceron
	CALL procCheckDuplicateBarcode();
	
	[08/31/2013] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCheckDuplicateBarcode
GO

create procedure procCheckDuplicateBarcode()
BEGIN
	
	SELECT main.BarCode1, prd.ProductID, pkg.UnitID, prd.baseUnitID, ProductCode, Barcode1Count
	FROM ( 
		SELECT Barcode1, COUNT(Barcode1) Barcode1Count, pkg.UnitID
		FROM tblProducts prd 
		INNER JOIN tblProductPackage pkg ON prd.ProductID = pkg.ProductID
		WHERE IFNULL(BarCode1,'') <> ''
		GROUP BY BarCode1, pkg.UnitID
	) MAIN 
	INNER JOIN tblProductPackage pkg ON MAIN.BarCode1 = pkg.BarCode1
	INNER JOIN tblProducts prd ON pkg.ProductID = prd.ProductID
	WHERE Barcode1Count >= 2;


END;
GO
delimiter ;


/*********************************
	procTransactionIsConsignmentUpdate
	Lemuel E. Aceron
	
	[09/09/2013]  - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionIsConsignmentUpdate;
delimiter GO

create procedure procTransactionIsConsignmentUpdate(IN intTransactionID bigint(20), IN intIsConsignment tinyint(1))
BEGIN

	UPDATE tblTransactions SET isConsignment = intIsConsignment WHERE TransactionID = intTransactionID;
	
END;
GO
delimiter ;


/*********************************
	procTransactionisZeroRatedUpdate
	Lemuel E. Aceron
	
	[01/05/2015]  - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionisZeroRatedUpdate;
delimiter GO

create procedure procTransactionisZeroRatedUpdate(IN intTransactionID bigint(20), IN intisZeroRated tinyint(1))
BEGIN

	UPDATE tblTransactions SET isZeroRated = intisZeroRated WHERE TransactionID = intTransactionID;
	
END;
GO
delimiter ;


/*********************************
	procTransactionSuspendedOpen
	Lemuel E. Aceron
	
	[01/05/2015]  - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionSuspendedOpen;
delimiter GO

create procedure procTransactionSuspendedOpen(IN iTransactionID bigint(20))
BEGIN
	-- TransactionStatus
	--		Open = 0
	--		SuspendedOpen = 2
	UPDATE tblTransactions SET TransactionStatus = 13 WHERE TransactionID = iTransactionID AND (TransactionStatus = 2 OR TransactionStatus = 0);
	
END;
GO
delimiter ;


/********************************************
	procProductAddReservedQuantity
	
	CALL procProductAddReservedQuantity(1, 4338, 0, 1, 'Remarks', NOW(), '1000001', 'Lemuel');
	
	Sep 14, 2013: Created this procedure
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductAddReservedQuantity
GO

create procedure procProductAddReservedQuantity(
	IN intBranchID INT(4),
	IN intProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,3),
	IN strRemarks VARCHAR(8000),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decProductReservedQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE strAuditRemarks VARCHAR(8000) DEFAULT '';

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = intProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0), IFNULL(SUM(ReservedQuantity),0)  INTO decProductQuantity, decProductReservedQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = intProductID;
	
	-- Apr 20, 2014 remove the remarks
	SET strAuditRemarks = CONCAT('Reserved for: ',strTransactionNo,' ; Curr Reserved: ',IFNULL(decProductReservedQuantity,'0'),'; Curr Quantity:',IFNULL(decProductQuantity,'0'),' ; prodid: ',intProductID,' ; matrixid: ',lngMatrixID,' ; ',strProductCode,' ; ',strProductDesc,' ; ',strUnitCode,' ; ',strMatrixDescription,' ; ',strRemarks);
	-- SET strAuditRemarks = CONCAT('Reserved for: ',strTransactionNo,' ; Curr Reserved: ',IFNULL(decProductReservedQuantity,'0'),'; Curr Quantity:',IFNULL(decProductQuantity,'0'),' ; prodid: ',intProductID,' ; matrixid: ',lngMatrixID,' ; ',strProductCode,' ; ',strProductDesc,' ; ',strUnitCode,' ; ',strMatrixDescription);
	
	-- Insert to audit instead of product movement history
	CALL procsysAuditInsert(NOW(), strCreatedBy, 'PRODUCT RESERVED ADD', 'localhost', strAuditRemarks);
	
	IF EXISTS(SELECT ReservedQuantity FROM tblProductInventory WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN 
		UPDATE tblProductInventory SET
			ReservedQuantity	= decQuantity + ReservedQuantity
		WHERE ProductID = intProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, ReservedQuantity)
		VALUES(intBranchID, intProductID, lngMatrixID, decQuantity);
	END IF;
									
	
END;
GO
delimiter ;




/********************************************
	procProductSubtractReservedQuantity
	
	CALL procProductSubtractReservedQuantity(1, 4338, 0, 1, 'Remarks', NOW(), '1000001', 'Lemuel');
	
	Sep 14, 2013: Created this procedure
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSubtractReservedQuantity
GO

create procedure procProductSubtractReservedQuantity(
	IN intBranchID INT(4),
	IN intProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(8000),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decProductReservedQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE strAuditRemarks VARCHAR(8000) DEFAULT '';

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = intProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0), IFNULL(SUM(ReservedQuantity),0)  INTO decProductQuantity, decProductReservedQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = intProductID;
	
	SET strAuditRemarks = CONCAT('Unreserved for: ',strTransactionNo,' ; Curr Reserved: ',IFNULL(decProductReservedQuantity,'0'),'; Curr Quantity:',IFNULL(decProductQuantity,'0'),' ; prodid: ',intProductID,' ; matrixid: ',lngMatrixID,' ; ',strProductCode,' ; ',strProductDesc,' ; ',strUnitCode,' ; ',strMatrixDescription,' ; ',strRemarks);
	
	-- Insert to audit instead of product movement history
	CALL procsysAuditInsert(NOW(), strCreatedBy, 'PRODUCT RESERVED SUBTRACT', 'localhost', strAuditRemarks);
	
		
	-- Subtract the quantity from Product table
	UPDATE tblProductInventory SET 
		ReservedQuantity	= ReservedQuantity - decQuantity
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = intProductID
		AND BranchID = intBranchID;
		
							
END;
GO
delimiter ;




/**************************************************************

	procContactSelect
	Lemuel E. Aceron
	Sep 15, 2013
	
	Desc: This will get the all information of a contact

	CALL procContactSelect(1, 0, null, null, null, null, null, 0, 1, '1900-01-01', '1900-01-01', '1900-01-01', '1900-01-01', 0, 0, 0, 'ContactID','');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSelect
GO

create procedure procContactSelect(
			IN ContactGroupCategory  int,
			IN ContactID  bigint,
			IN ContactCode varchar(50),
			IN ContactName varchar(75),
			IN ContactGroupCode varchar(30),
			IN RewardCardNo varchar(30),
			IN Name varchar(30),
			IN BirthMonth int,
			IN AnniversaryMonth int,
			IN BirthDateFrom varchar(30),
			IN BirthDateTo varchar(30),
			IN AnniversaryDateFrom datetime,
			IN AnniversaryDateTo datetime,
			IN hasCreditOnly tinyint(1),
			IN intDeleted int,
			IN intModeOfTerms int,
			IN lngLimit int,
			IN SortField varchar(60),
			IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 cntct.ContactID ,cntct.SequenceNo
							,cntct.ContactCode ,cntct.ContactName ,cntct.BusinessName
							,grp.ContactGroupID ,grp.ContactGroupName
							,ModeOfTerms ,cntct.Terms ,cntct.Address
							,TelephoneNo ,cntct.Remarks ,cntct.Debit ,cntct.Credit ,cntct.CreditLimit ,cntct.IsCreditAllowed
							,DateCreated ,cntct.Deleted ,dept.DepartmentID ,dept.DepartmentName
							,pos.PositionID ,pos.PositionName ,cntct.isLock
							,IFNULL(LastName,'''') LastName ,IFNULL(Middlename,'''') Middlename ,IFNULL(FirstName,'''') FirstName ,IFNULL(Spousename,'''') Spousename
							,SpouseBirthDate ,AnniversaryDate
							,Address1 ,Address2 ,City ,State ,ZipCode ,IFNULL(cntry.CountryID,0) CountryID ,CountryName
							,BusinessPhoneNo ,HomePhoneNo ,MobileNo ,FaxNo ,EmailAddress 
							,RewardCardNo ,RewardActive, RewardPoints, RewardAwardDate, TotalPurchases, RedeemedPoints, RewardCardStatus, ExpiryDate
							,IFNULL(addon.BirthDate,rwrd.BirthDate) BirthDate 
							,LastCheckInDate ,TINNo ,LTONo ,PriceLevel
						FROM tblContacts cntct
							INNER JOIN tblContactGroup grp ON cntct.ContactGroupID = grp.ContactGroupID
							INNER JOIN tblDepartments dept ON cntct.DepartmentID = dept.DepartmentID
							INNER JOIN tblPositions pos ON cntct.PositionID = pos.PositionID
							LEFT OUTER JOIN tblContactAddOn addon ON addon.ContactID = cntct.ContactID
							LEFT OUTER JOIN tblContactRewards rwrd ON rwrd.CustomerID = cntct.ContactID
							LEFT OUTER JOIN tblCountry cntry ON addon.CountryID = cntry.CountryID
						WHERE 1=1 ');

	IF intDeleted <> -1 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.deleted = ',intDeleted,' ');
	END IF;
	IF hasCreditOnly = true THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.Credit > 0 ');
	END IF;

	IF ContactGroupCategory = 1 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 1 OR ContactGroupCategory = 3) ');
	ELSEIF ContactGroupCategory = 2 THEN -- Supplier Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 2 OR ContactGroupCategory = 3) ');
	ELSEIF ContactGroupCategory = 4 THEN -- Agent Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 4) ');
	END IF;

	IF ContactID <> 0 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.ContactID = ',ContactID,' ');
	ELSEIF IFNULL(ContactCode,'') <> '' AND IFNULL(ContactName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (ContactCode LIKE ''%',ContactCode,'%'' OR ContactName LIKE ''%',ContactName,'%'') ');
	ELSEIF IFNULL(ContactCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND ContactCode LIKE ''%',ContactCode,'%'' ');
	ELSEIF IFNULL(ContactName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND ContactName LIKE ''%',ContactName,'%'' ');
	ELSEIF IFNULL(Name,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (LastName LIKE ''%',Name,'%'' OR MiddleName LIKE ''%',Name,'%'' OR FirstName LIKE ''%',Name,'%'') ');
	END IF;

	IF IFNULL(ContactGroupCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCode LIKE ''%',ContactGroupCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '	  OR ContactGroupName LIKE ''%',ContactGroupCode,'%'') ');
	END IF;

	-- Added 10Oct2013 
	IF IFNULL(BirthMonth,0) <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND (DATE_FORMAT(IFNULL(IFNULL(addon.BirthDate,rwrd.BirthDate),0),''%m'') = ',BirthMonth,' ');
		SET @SQL = CONCAT(@SQL, '	  OR DATE_FORMAT(IFNULL(addon.SpouseBirthDate,0),''%m'') = ',BirthMonth,') ');
	END IF;

	IF IFNULL(AnniversaryMonth,0) <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(IFNULL(addon.AnniversaryDate,0),''%m'') = ',AnniversaryMonth,' ');
	END IF;

	
	SET BirthDateFrom = IF(NOT ISNULL(BirthDateFrom), BirthDateFrom, '1900-01-01');
	IF DATE_FORMAT(BirthDateFrom, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND (IFNULL(addon.BirthDate,rwrd.BirthDate) >= ''',DATE_FORMAT(BirthDateFrom, '%Y-%m-%d'),''' ');
		SET @SQL = CONCAT(@SQL, '	  OR SpouseBirthDate >= ''',DATE_FORMAT(BirthDateFrom, '%Y-%m-%d'),''') ');
	END IF;

	SET BirthDateTo = IF(NOT ISNULL(BirthDateTo), BirthDateTo, '1900-01-01');
	IF DATE_FORMAT(BirthDateTo, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND (IFNULL(addon.BirthDate,rwrd.BirthDate) <= ''',DATE_FORMAT(BirthDateTo, '%Y-%m-%d'),''' ');
		SET @SQL = CONCAT(@SQL, '	  OR SpouseBirthDate <= ''',DATE_FORMAT(BirthDateTo, '%Y-%m-%d'),''') ');
	END IF;
	
	SET AnniversaryDateFrom = IF(NOT ISNULL(AnniversaryDateFrom), AnniversaryDateFrom, '1900-01-01');
	IF DATE_FORMAT(AnniversaryDateFrom, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND addon.AnniversaryDate >= ''',DATE_FORMAT(AnniversaryDateFrom, '%Y-%m-%d'),''' ');
	END IF;

	SET AnniversaryDateTo = IF(NOT ISNULL(AnniversaryDateTo), AnniversaryDateTo, '1900-01-01');
	IF DATE_FORMAT(AnniversaryDateTo, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND addon.AnniversaryDate <= ''',DATE_FORMAT(AnniversaryDateTo, '%Y-%m-%d'),''' ');
	END IF;
	
	IF IFNULL(intModeOfTerms,-1) <> -1 THEN
		SET @SQL = CONCAT(@SQL, 'AND ModeOfTerms = ',intModeOfTerms,' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','ContactCode, ContactName, LastName',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL,IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));


	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/**************************************************************

	procContactAddOnSelect
	Lemuel E. Aceron
	Sep 15, 2013
	
	Desc: This will get the all information of a contact

	CALL procContactAddOnSelect(0, '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddOnSelect
GO

create procedure procContactAddOnSelect(
			IN ContactID  bigint,
			IN Name varchar(50))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 AddOn.*
							,IFNULL(cntry.CountryName,'''') CountryName
						FROM tblContactAddOn addon
						LEFT OUTER JOIN tblCountry cntry ON cntry.CountryID = addon.CountryID
						WHERE 1=1 ');

	IF ContactID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND addon.ContactID = ',ContactID,' ');
	ELSEIF IFNULL(Name,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (addon.LastName LIKE ''%',Name,'%'' OR addon.MiddleName LIKE ''%',Name,'%'' OR addon.FirstName LIKE ''%',Name,'%'') ');
	END IF;

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/*********************************
	procContactLastCheckInDateUpdate
	Lemuel E. Aceron
	Sep 24, 2014 as required by Bellevue to monitor how long the table is occupied without order
*********************************/
DROP PROCEDURE IF EXISTS procContactLastCheckInDateUpdate;
delimiter GO

create procedure procContactLastCheckInDateUpdate(IN intContactID bigint(20), IN dteLastCheckInDate DateTime)
BEGIN
	
	UPDATE tblContacts SET LastCheckInDate = DATE_FORMAT(dteLastCheckInDate, '%Y-%m-%d %H:%i') WHERE ContactID = intContactID;

END;
GO
delimiter ;



/********************************************
	procProductUpdateVAT
	
	CALL procProductUpdateVAT(1, 1, 1, 12, 12, 12, 'Lemuel');
	
	Sep 21, 2013: Created this procedure
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateVAT
GO

create procedure procProductUpdateVAT(
	IN lngProductGroupID BIGINT,
	IN lngProductSubGroupID BIGINT,
	IN intProductID BIGINT,
	IN decNewVAT DECIMAL(18,3),
	IN decNewEVAT DECIMAL(18,3),
	IN decNewLocalTax DECIMAL(18,3),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	SET @SQL = CONCAT('	UPDATE tblProductPackage SET
							 VAT = ',decNewVAT,'
							,EVAT = ',decNewEVAT,'
							,LocalTax = ',decNewLocalTax,' ');

	IF intProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'WHERE ProductID > 10 AND ProductID = ',intProductID,' ');
	ELSEIF lngProductSubGroupID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'WHERE ProductID > 10 AND ProductID IN (SELECT DISTINCT ProductID FROM tblProducts WHERE ProductSubGroupID = ',lngProductSubGroupID,') ');
	ELSEIF lngProductGroupID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'WHERE ProductID > 10 AND ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = ',lngProductGroupID,')) ');
	END IF;
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

	
	-- update also the subgroup and group
	IF intProductID = 0 AND lngProductGroupID = 0 THEN
		SET @SQL = CONCAT('	UPDATE tblProductSubGroup SET
							 VAT = ',decNewVAT,'
							,EVAT = ',decNewEVAT,'
							,LocalTax = ',decNewLocalTax,' ');
		IF lngProductSubGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'WHERE ProductSubGroupID = ',lngProductSubGroupID,' ');
		END IF;
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;
	END IF;

	IF lngProductSubGroupID = 0 AND intProductID = 0 THEN
		SET @SQL = CONCAT('	UPDATE tblProductSubGroup SET
							 VAT = ',decNewVAT,'
							,EVAT = ',decNewEVAT,'
							,LocalTax = ',decNewLocalTax,' ');
		IF lngProductGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'WHERE ProductGroupID = ',lngProductGroupID,' ');
		END IF;
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('	UPDATE tblProductGroupBaseVariationsMatrix SET
							 VAT = ',decNewVAT,'
							,EVAT = ',decNewEVAT,'
							,LocalTax = ',decNewLocalTax,' ');
		IF lngProductGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'WHERE GroupID = ',lngProductGroupID,' ');
		END IF;
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('	UPDATE tblProductGroup SET
							 VAT = ',decNewVAT,'
							,EVAT = ',decNewEVAT,'
							,LocalTax = ',decNewLocalTax,' ');
		IF lngProductGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'WHERE ProductGroupID = ',lngProductGroupID,' ');
		END IF;
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

	END IF;	

	-- always run this anytime an update is done to reset tax for all products
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE');
	UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE');

	CALL procsysAuditInsert(NOW(), strCreatedBy, 'UPDATE VAT', 'localhost', CONCAT('ProductID:',intProductID,' ProductSubGroupID:',lngProductSubGroupID,' ProductGroupID:',lngProductGroupID,' VAT:',decNewVAT,' EVAT:',decNewEVAT,' LocalTax:',decNewLocalTax));

END;
GO
delimiter ;



/********************************************
	procProductDelete
	
	CALL procProductDelete('0', 'Lemuel');
	
	Sep 21, 2013: Created this procedure
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductDelete
GO

create procedure procProductDelete(
	IN IDs VARCHAR(1000),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN

	-- execute only if there are IDs
	IF IFNULL(IDs,'0') <> '0' THEN
		SET FOREIGN_KEY_CHECKS = 0;

		SET @SQL = CONCAT('DELETE FROM tblProductPackage WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('DELETE FROM tblProductInventory WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;
		
		SET @SQL = CONCAT('DELETE FROM tblProductUnitMatrix WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE ProductID IN (',IDs,')) ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('DELETE FROM tblProductBaseVariationsMatrix WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;
		
		SET @SQL = CONCAT('DELETE FROM tblProductVariations WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET @SQL = CONCAT('DELETE FROM tblProducts WHERE ProductID IN (',IDs,') ');
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		SET FOREIGN_KEY_CHECKS = 1;

		CALL procsysAuditInsert(NOW(), strCreatedBy, 'DELETE PRODUCTS', 'localhost', CONCAT('IDS:',IDs));
	END IF;			
END;
GO
delimiter ;




/********************************************
	procProductVariationAddEasy
	
	CALL procProductVariationAddEasy(36, 0, 0, 6, 'Lemuel');
	
	Sep 21, 2013: Created this procedure
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductVariationAddEasy
GO

create procedure procProductVariationAddEasy(
	IN lngProductGroupID BIGINT,
	IN lngProductSubGroupID BIGINT,
	IN intProductID BIGINT,
	IN lngVariationID BIGINT,
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	IF lngVariationID <> 0 THEN

		SET @SQL = 'INSERT tblProductVariations	(ProductID, VariationID)  ';
	
		IF intProductID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'SELECT ProductID, ',lngVariationID,' FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductVariations WHERE VariationID = ',lngVariationID,') 
																								 AND ProductID = ',intProductID,' ');
		ELSEIF lngProductSubGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'SELECT ProductID, ',lngVariationID,' FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductVariations WHERE VariationID = ',lngVariationID,')
																								 AND ProductID IN (SELECT DISTINCT ProductID FROM tblProducts WHERE ProductSubGroupID = ',lngProductSubGroupID,') ');
		
		ELSEIF lngProductGroupID <> 0 THEN
			SET @SQL = CONCAT(@SQL, 'SELECT ProductID, ',lngVariationID,' FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductVariations WHERE VariationID = ',lngVariationID,') 
																								 AND ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = ',lngProductGroupID,')) ');
		ELSE
			SET @SQL = CONCAT(@SQL, 'SELECT ProductID, ',lngVariationID,' FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductVariations WHERE VariationID = ',lngVariationID,') ');
		END IF;
		
		SELECT @SQL;
		PREPARE cmd FROM @SQL;
		EXECUTE cmd;
		DEALLOCATE PREPARE cmd;

		-- update also the subgroup and group
		IF intProductID = 0 AND lngProductGroupID = 0 THEN
			SET @SQL = CONCAT('	INSERT tblProductSubGroupVariations	(SubGroupID, VariationID)
								SELECT ProductSubGroupID, ',lngVariationID,' FROM tblProductSubGroup WHERE ProductSubGroupID NOT IN (SELECT DISTINCT SubGroupID FROM tblProductSubGroupVariations WHERE VariationID = ',lngVariationID,') ');
			SET @SQL = CONCAT(@SQL, IF(lngProductSubGroupID=0,'',CONCAT('AND ProductSubGroupID = ',lngProductSubGroupID,' ')));
		
			PREPARE cmd FROM @SQL;
			EXECUTE cmd;
			DEALLOCATE PREPARE cmd;
		END IF;
		
		IF lngProductSubGroupID = 0 AND intProductID = 0 THEN
			SET @SQL = CONCAT('	INSERT tblProductSubGroupVariations	(SubGroupID, VariationID)
								SELECT ProductSubGroupID, ',lngVariationID,' FROM tblProductSubGroup WHERE ProductSubGroupID NOT IN (SELECT DISTINCT SubGroupID FROM tblProductSubGroupVariations WHERE VariationID = ',lngVariationID,') ');
			SET @SQL = CONCAT(@SQL, IF(lngProductGroupID=0,'',CONCAT('AND ProductSubGroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID=',lngProductGroupID,') ')));

			PREPARE cmd FROM @SQL;
			EXECUTE cmd;
			DEALLOCATE PREPARE cmd;
		
			SET @SQL = CONCAT('	INSERT tblProductGroupVariations (GroupID, VariationID)');
			SET @SQL = CONCAT(@SQL, 'SELECT ProductGroupID, ',lngVariationID,' FROM tblProductGroup WHERE ProductGroupID NOT IN (SELECT DISTINCT GroupID FROM tblProductGroupVariations WHERE VariationID = ',lngVariationID,') ');
			SET @SQL = CONCAT(@SQL, IF(lngProductGroupID=0,'',CONCAT('AND ProductGroupID=',lngProductGroupID,' ')));

			PREPARE cmd FROM @SQL;
			EXECUTE cmd;
			DEALLOCATE PREPARE cmd;
		
		END IF;	
		
		CALL procsysAuditInsert(NOW(), strCreatedBy, 'ADD MULTIPLE VARIATION', 'localhost', CONCAT('ProductID:',intProductID,' ProductSubGroupID:',lngProductSubGroupID,' ProductGroupID:',lngProductGroupID,' VriationID:',lngVariationID));
	END IF;
END;
GO
delimiter ;

-- default the supplier to RetailPlus Default Supplier for those that doesnt have supplierid
UPDATE tblProducts SET SupplierID = 2 WHERE SupplierID NOT IN (SELECT DISTINCT contactid from tblContacts);

/*********************************
	procTerminalReportRePrintedUpdate
	Lemuel E. Aceron
	
	[02/06/2014]  - create this procedure
	
	CALL procTerminalReportRePrintedUpdate(1, '01', '"00000000153656');

*********************************/
DROP PROCEDURE IF EXISTS procTerminalReportRePrintedUpdate;
delimiter GO

create procedure procTerminalReportRePrintedUpdate(IN intBranchID int(4), IN strTerminalNo VARCHAR(30), IN strTransactionNo VARCHAR(30))
BEGIN
	DECLARE decSubTotal DECIMAL;

	SET decSubTotal = (SELECT SUM(SubTotal) FROM tblTransactions WHERE TransactionNo = strTransactionNo AND TerminalNo = strTerminalNo);

	UPDATE tblTerminalReport SET 
		NoOfReprintedTransaction = NoOfReprintedTransaction + 1,
		TotalReprintedTransaction = TotalReprintedTransaction + IFNULL(decSubTotal,0)
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/**************************************************************

	procProcessGLA
	Lemuel E. Aceron
	october 22, 2013
	
	Desc: This will process all the GLA from infogenesis

	CALL procProcessGLA();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessGLA
GO
	
create procedure procProcessGLA(
	IN strBatchID	VARCHAR(60)
)
BEGIN

	DECLARE intINFOGENESISGroupID INT(4) DEFAULT 1;

	-- update the discount using the latest discounts
	INSERT INTO tblDiscount(DiscountID, DiscountCode, DiscountType, DiscountPrice, InPercent)
	SELECT CONCAT(999,Dsc_Number), Dsc_Name, Dsc_Name, 0, 1 FROM tblgla_d_dsc_def
	WHERE CONCAT(999,Dsc_Number) NOT IN (SELECT DiscountID FROM tblDiscount) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Dsc_Number), Dsc_Name, Dsc_Name;

	UPDATE tblDiscount dsc 
	INNER JOIN tblgla_d_dsc_def def ON dsc.DiscountID = CONCAT(999,def.Dsc_Number) AND BatchID = strBatchID SET
		DiscountCode = Dsc_Name,
		DiscountType = Dsc_Name
	WHERE DiscountCode <> Dsc_Name;

	/**** Step 1: pdate the infogenesis users ***/

	SELECT GroupID INTO intINFOGENESISGroupID FROM sysAccessGroups WHERE GroupName = 'INFOGENESIS';

	INSERT INTO sysAccessUsers(UID, UserName, Password, DateCreated)
	SELECT CONCAT(999,Emp_Number), CONCAT('gen_',Emp_Number) UserName, First_Name, NOW() FROM tblgla_d_emp_def
	WHERE CONCAT(999,Emp_Number) NOT IN (SELECT UID FROM sysAccessUsers) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Emp_Number), CONCAT('gen_',Emp_Number), First_Name, NOW()
	ORDER BY Emp_Number;

	INSERT INTO sysAccessUserDetails(UID, Name, EmailAddress, GroupID, CountryID, PageSize)
	SELECT usrs.UID, CONCAT(emp.First_Name,' ',emp.Last_Name, ' ',usrs.UID) Name, '' EmailAddress, intINFOGENESISGroupID GroupID, 1, 10 PageSize
	FROM tblgla_d_emp_def emp 
	INNER JOIN sysAccessUsers usrs ON CONCAT(999,Emp_Number) = usrs.UID
	WHERE UID NOT IN (SELECT UID FROM SysAccessUserDetails) AND BatchID = strBatchID
	GROUP BY usrs.UID, CONCAT(emp.First_Name,' ',emp.Last_Name, ' ',usrs.UID)
	ORDER BY UID;

	-- update the branch
	INSERT INTO tblBranch(BranchID, BranchCode, BranchName, DBIP, DBPort)
	SELECT CONCAT(999,Rvc_Number) BranchID, Rvc_Name BranchCode, Rvc_Name BranchName, '127.0.0.1' DBIP, 3306 DBPort
	FROM tblgla_d_location_def 
	WHERE Rvc_Number > 0 AND CONCAT(999,Rvc_Number)  NOT IN (SELECT BranchID FROM tblBranch) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Rvc_Number), Rvc_Name
	ORDER BY Rvc_Number;

	UPDATE tblBranch brnch
	INNER JOIN tblgla_d_location_def loc ON brnch.BranchID = CONCAT(999,loc.Rvc_Number) AND BatchID = strBatchID SET
		brnch.BranchCode = Rvc_Name,
		brnch.BranchName = Rvc_Name
	WHERE BranchCode <> Rvc_Name;

	-- update the products

	INSERT INTO tblProductGroup(ProductGroupID, ProductGroupCode, ProductGroupName, BaseUnitID, IncludeInSubtotalDiscount, SequenceNo)
	SELECT CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name, Sales_Itemizer_Name, 1, 1, Sales_Itemizer_Number
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Sales_Itemizer_Number) NOT IN (SELECT ProductGroupID FROM tblProductGroup) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name 
	ORDER BY Sales_Itemizer_Number;

	UPDATE tblProductGroup grp
	INNER JOIN (
		SELECT CONCAT(999,Sales_Itemizer_Number) Sales_Itemizer_Number, Sales_Itemizer_Name
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY Sales_Itemizer_Number, Sales_Itemizer_Name 
	) gla_grp ON grp.ProductGroupID = gla_grp.Sales_Itemizer_Number	SET
		grp.ProductGroupCode = gla_grp.Sales_Itemizer_Name,
		grp.ProductGroupName = gla_grp.Sales_Itemizer_Name
	WHERE grp.ProductGroupCode <> gla_grp.Sales_Itemizer_Name;

	INSERT INTO tblProductSubGroup(ProductGroupID, ProductSubGroupID, ProductSubGroupCode, ProductSubGroupName, BaseUnitID, IncludeInSubtotalDiscount, SequenceNo)
	SELECT CONCAT(999,Sales_Itemizer_Number), CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name, Sales_Itemizer_Name, 1, 1, Sales_Itemizer_Number
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Sales_Itemizer_Number) NOT IN (SELECT ProductSubGroupID FROM tblProductSubGroup) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name 
	ORDER BY Sales_Itemizer_Number;

	UPDATE tblProductSubGroup sgrp
	INNER JOIN (
		SELECT CONCAT(999,Sales_Itemizer_Number) Sales_Itemizer_Number, Sales_Itemizer_Name
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY Sales_Itemizer_Number, Sales_Itemizer_Name 
	) gla_grp ON sgrp.ProductSubGroupID = gla_grp.Sales_Itemizer_Number	SET
		sgrp.ProductSubGroupCode = gla_grp.Sales_Itemizer_Name,
		sgrp.ProductSubGroupName = gla_grp.Sales_Itemizer_Name
	WHERE sgrp.ProductSubGroupCode <> gla_grp.Sales_Itemizer_Name;

	-- append 999 to identify that it is Infogenesis products
	INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, IncludeInSubTotalDiscount
	 	,SupplierID ,IsItemSold, Active, SequenceNo)
	SELECT CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name, CONCAT(999,Sales_Itemizer_Number), 1 BaseUnitID, NOW(), 1 IncludeInSubTotalDiscount
		,2 SupplierID, 1 IsItemSold, 1 Active, Def_Seq SequenceNo
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Mi_Number) NOT IN (SELECT ProductID FROM tblProducts) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name, Sales_Itemizer_Name
	ORDER BY Mi_Number;

	UPDATE tblProducts prd
	INNER JOIN (
		SELECT CONCAT(999,Mi_Number) ProductID, LEFT(Mi_Name, 30) ProductCode, Mi_Name ProductDesc, CONCAT(999,Sales_Itemizer_Number) ProductSubGroupID
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name
	) gla_prd ON prd.ProductID = gla_prd.ProductID SET
		prd.ProductCode = gla_prd.ProductCode,
		prd.ProductDesc = gla_prd.ProductDesc,
		prd.ProductSubGroupID = gla_prd.ProductSubGroupID
	WHERE prd.ProductCode <> gla_prd.ProductCode;

	INSERT INTO tblProductPackage(PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity, VAT, EVAT, LocalTax, WSPrice, BarCode1)
	SELECT ProductID, ProductID, BaseUnitID, 0, 0, 1 Quantity, 12, 0, 0, 0, ProductCode
	FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductPackage);

	-- update charge types
	INSERT INTO tblChargeType(ChargeTypeID, ChargeTypeCode, ChargeType, ChargeAmount, InPercent)
	SELECT CONCAT(999,Svc_Number), Svc_Name, Svc_Name, 0, 1 FROM tblgla_d_svc_def
	WHERE CONCAT(999,Svc_Number) NOT IN (SELECT ChargeTypeID FROM tblChargeType) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Svc_Number), Svc_Name;

	UPDATE tblChargeType svc	
	INNER JOIN tblgla_d_svc_def def ON svc.ChargeTypeID = CONCAT(999,def.Svc_Number) AND BatchID = strBatchID SET
		ChargeTypeCode = Svc_Name,
		ChargeType = Svc_Name
	WHERE ChargeTypeCode <> Svc_Name;

	-- update the contacts 
	INSERT INTO tblContacts(ContactCode, ContactName, ContactGroupID
		,ModeOfTerms, Terms, Address, BusinessName, TelephoneNo, Remarks
		,Debit, Credit, CreditLimit, IsCreditAllowed, DateCreated
		,Deleted, DepartmentID, PositionID, isLock
	)
	SELECT RIGHT(dsc.Ref_Info_1, 6) ContactCode, RIGHT(dsc.Ref_Info_1, 6) ContactName, 21 ContactGroupID
		,0 ModeOfTerms, 0 Terms, '' Address, dsc.Ref_Info_1 BusinessName, '' TelephoneNo, '' Remarks
		,0 Debit, 0 Credit, 0 CreditLimit, 0 IsCreditAllowed, NOW() DateCreated
		,0 Deleted, 1 DepartmentID, 1 PositionID, 0 isLock
 	FROM tblgla_f_dtl_chk_dsc dsc
	WHERE BatchID = strBatchID
		AND IFNULL(RIGHT(dsc.Ref_Info_1, 6),'') <> ''
		AND RIGHT(dsc.Ref_Info_1, 6) NOT IN (SELECT ContactCode FROM tblContacts)
	GROUP BY RIGHT(dsc.Ref_Info_1, 6), dsc.Ref_Info_1
	;

	-- update the transactions
	DELETE FROM tblTransactionItems WHERE IFNULL(Datasource,'') = strBatchID;
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE IFNULL(Datasource,'') = strBatchID);
	DELETE FROM tblTransactions WHERE IFNULL(Datasource,'') = strBatchID;

	INSERT INTO tblTransactions(TransactionID, TransactionNo, CustomerID , CustomerName, CustomerGroupName, CashierID, CashierName, TerminalNo, TransactionDate
		,DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount 
		,TransDiscount, TransDiscountType, VAT, VatableAmount, EVAT, EVATableAmount, LocalTax
		,AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount
		,DateClosed, PaymentType, DiscountCode, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, WaiterID, WaiterName
		,Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName
		,AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate
		,RewardPointsPayment, RewardConvertedPayment, PaxNo, ModeOfTerms, Terms, CRNo, CreditChargeAmount, BranchID, BranchCode, TransactionType, isConsignment
		,Datasource)
	SELECT chk_headers_seq_number TransactionID, chk_num TransactionNo
		,IFNULL(cntct.ContactID,1) CustomerID ,IFNULL(cntct.ContactName, 'RetailPlus Default Customer') CustomerName -- ,RIGHT(dsc.Ref_Info_1, 6) ContactCode
		,IFNULL(grp.ContactGroupName,'') CustomerGroupName
		,CONCAT(999,hdr.fk_emp_def) CashierID,  IFNULL(usr.Name, '') CashierName, hdr.fk_location_def TerminalNo, hdr.Chk_Open_Date_Time TransactionDate
		,'1900-01-01' DateSuspended ,'1900-01-01' DateResumed 
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 5
			  when dsc.Status_Flag = 'DSC_VOID' THEN 3
			  else 1
		 end TransactionStatus 
		,(hdr.Sub_Ttl + Tax_Ttl + -hdr.Dsc_Ttl) SubTotal ,-hdr.Dsc_Ttl Discount 
		,0 TransDiscount, IFNULL(dsc.fk_dsc_def,0) TransDiscountType
		,Tax_Ttl VAT ,hdr.Sub_Ttl VatableAmount, 0 EVAT,0 EvatableAmount, 0 LocalTax
		,hdr.Pymnt_Ttl AmountPaid ,hdr.Pymnt_Ttl CashPayment, 0 ChequePayment, 0 CreditCardPayment, 0, 0
		,(hdr.Pymnt_Ttl -((hdr.Sub_Ttl + Tax_Ttl + -hdr.Dsc_Ttl) - -hdr.Dsc_Ttl + (hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) )) ChangeAmount
		,hdr.Chk_Closed_Date_Time DateClosed ,0 PaymentType ,dis.DiscountCode ,0 DebitPayment ,0 ItemsDiscount
		,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) Charge ,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) ChargeAmount
		,MAX(IFNULL(chrg.ChargeTypeCode, '')) ChargeCode, '' ChargeRemarks, 2 WaiterID, 'RetailPlus Default' WaiterName
		,0 Packed, 0 OrderType, 1 AgentID, 'RetailPlus Agent ?' AgentName, CONCAT(999,hdr.fk_emp_def) CreatedByID, IFNULL(usr.Name, '') CreatedByName
		,'System Default Department' AgentDepartmentName, 'System Default Position' AgentPositionName, 0 ReleaserID, '' ReleaserName, hdr.fk_business_date ReleasedDate
		,0 RewardPointsPayment,0 RewardConvertedPayment, hdr.cov_cnt PaxNo, 0 ModeOfTerms, 0 Terms, 0 CRNo, 0 CreditChargeAmount
		,CONCAT(999,hdr.fk_location_def) BranchID, brnch.BranchCode
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 1 else 0 end TransactionType, 0 isConsignment
		,hdr.BatchID
		--  + Auto_Svc_Ttl + Other_Svc_Ttl
	FROM tblgla_f_dtl_chk_headers hdr
	INNER JOIN tblgla_f_dtl_chk_dsc dsc ON dsc.fk_chk_headers = hdr.chk_headers_seq_number
	LEFT OUTER JOIN tblgla_f_dtl_chk_svc svc ON svc.fk_chk_headers = hdr.chk_headers_seq_number
	LEFT OUTER JOIN tblContacts cntct ON RIGHT(dsc.Ref_Info_1, 6) = cntct.ContactCode
	LEFT OUTER JOIN tblContactGroup grp ON cntct.ContactGroupID = grp.ContactGroupID
	LEFT OUTER JOIN sysAccessUserDetails usr ON CONCAT(999,hdr.fk_emp_def) = usr.UID
	LEFT OUTER JOIN tblDiscount dis ON CONCAT(999,dsc.fk_dsc_def) = dis.DiscountID
	LEFT OUTER JOIN tblChargeType chrg ON CONCAT(999,svc.fk_svc_def) = chrg.ChargeTypeID
	LEFT OUTER JOIN tblBranch brnch ON CONCAT(999,hdr.fk_location_def) = brnch.BranchID
	WHERE hdr.BatchID = strBatchID 
		AND IFNULL(hdr.chk_headers_seq_number,0) NOT IN (SELECT TransactionID FROM tblTransactions)
		AND IFNULL(RIGHT(dsc.Ref_Info_1, 6),'') <> ''
	GROUP BY chk_headers_seq_number, chk_num
		,IFNULL(cntct.ContactID,1) ,IFNULL(cntct.ContactName, 'RetailPlus Default Customer') -- ,RIGHT(dsc.Ref_Info_1, 6) ContactCode
		,CONCAT(999,hdr.fk_emp_def),IFNULL(usr.Name, ''), hdr.fk_location_def, hdr.Chk_Open_Date_Time
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 5
			  when dsc.Status_Flag = 'DSC_VOID' THEN 3
			  else 1
		 end 
		,(hdr.Sub_Ttl + -hdr.Dsc_Ttl) ,-hdr.Dsc_Ttl 
		,IFNULL(dsc.fk_dsc_def,0)
		,Tax_Ttl ,hdr.Sub_Ttl
		,hdr.Pymnt_Ttl ,hdr.Pymnt_Ttl
		,hdr.Chk_Closed_Date_Time ,dis.DiscountCode
		,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) ,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl)
		,CONCAT(999,hdr.fk_emp_def), IFNULL(usr.Name, ''), hdr.fk_business_date
		,CONCAT(999,hdr.fk_location_def), brnch.BranchCode
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 1 else 0 end
		,hdr.BatchID;
	

	-- insert to transaciton items table
	INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description
		,ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType
		,Amount, VAT, vatableAmount, EVAT, EVATableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup
		,TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity
		,PromoQuantity, PromoValue, PromoType, PromoApplied, PurchasePrice, PurchaseAmount
		,IncludeInSubtotalDiscount, OrderSlipPrinter1, orderslipprinted, PercentageCommision, Commision, PaxNo, TransactionDiscount, Datasource
	)
	SELECT trx.TransactionID, CONCAT(999,det.fk_mi_def) ProductID, prd.ProductCode, pkg.Barcode1, prd.ProductDesc Description
		,pkg.UnitID ProductUnitID, unt.UnitCode ProductUnitCode
		,det.Item_Count Quantity, (det.Item_Total / (case when det.Item_Count = 0 then 1 else det.Item_Count end)) Price, 0 SellingPrice, 0 Discount, 0 ItemDiscount, 0 ItemDiscountType
		,det.Item_Total Amount, 12 VAT, (det.Item_Total / 1.12) VatableAmount, 0 EVAT, 0 EVATableAmount, 0 LocalTax
		,0 VariationsMatrixID, '' MatrixDescription, grp.ProductGroupCode ProductGroup, sgrp.ProductSubGroupCode ProductSubGroup
		,case when Status_Flag = 'MI_VOID' then 1
			  when Status_Flag = 'MI_RTN' then 4
			  else 0
		 end TransactionItemStatus
		,'' DiscountCode, '' DiscountRemarks, pkg.PackageID ProductPackageID, 0 MatrixPackageID, 1 PackageQuantity
		,0 PromoQuantity, 0 PromoValue, 0 PromoType, 0 PromoApplied, 0 PurchasePrice, 0 PurchaseAmount
		,prd.IncludeInSubtotalDiscount, prd.OrderSlipPrinter1, 0 orderslipprinted, 0 PercentageCommision, 0 Commision, 1 PaxNo, 0 TransactionDiscount
		,det.BatchID
	FROM tblTransactions trx
	INNER JOIN tblgla_f_dtl_chk_mi det ON det.fk_chk_headers = trx.TransactionID
	INNER JOIN tblProducts prd ON CONCAT(999,det.fk_mi_def) = prd.ProductID 
	INNER JOIN tblProductPackage pkg ON pkg.ProductID = prd.ProductID AND pkg.UnitID = prd.BaseUnitID AND pkg.Quantity = 1
	INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
	INNER JOIN tblProductSubGroup sgrp ON sgrp.ProductSubGroupID = prd.ProductSubGroupID
	INNER JOIN tblProductGroup grp ON grp.ProductGroupID = sgrp.ProductGroupID
	WHERE det.BatchID = strBatchID
	GROUP BY trx.TransactionID, CONCAT(999,det.fk_mi_def), prd.ProductCode, pkg.Barcode1, prd.ProductDesc
		,pkg.UnitID, unt.UnitCode
		,det.Item_Count, (det.Item_Total / det.Item_Count)
		,det.Item_Total, (det.Item_Total / 1.12)
		, grp.ProductGroupCode, sgrp.ProductSubGroupCode
		,case when Status_Flag = 'MI_VOID' then 1
			  when Status_Flag = 'MI_RTN' then 4
			  else 0
		 end
		,prd.IncludeInSubtotalDiscount, prd.OrderSlipPrinter1
	ORDER BY det.fk_chk_headers, det.Dtl_Num;

END;
GO
delimiter ;


/**************************************************************

	procTableSelectAll
	Lemuel E. Aceron
	Aug 1, 2014
	
	Desc: This will get all the columns from a table

	CALL procTableSelectAll('sysConfig', '2014-08-01', '1900-01-01');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTableSelectAll
GO

create procedure procTableSelectAll(
			IN strTableName varchar(150),
			IN dteStartSyncDateTime datetime,
			IN dteEndSyncDateTime datetime)
BEGIN
	SET @SQL = CONCAT('	SELECT * FROM ',strTableName, ' WHERE 1=1 ');
		
	IF DATE_FORMAT(dteStartSyncDateTime, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(LastModified, ''%Y-%m-%d %H:%i'') >=''',DATE_FORMAT(dteStartSyncDateTime, '%Y-%m-%d %H:%i'), ''' ');
	END IF;

	IF DATE_FORMAT(dteEndSyncDateTime, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(LastModified, ''%Y-%m-%d %H:%i'') <=''',DATE_FORMAT(dteEndSyncDateTime, '%Y-%m-%d %H:%i'), ''' ');
	END IF;
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procTableSelectAllKeys
	Lemuel E. Aceron
	Aug 1, 2014
	
	Desc: This will get the all columns from a table

	CALL procTableSelectAllKeys('sysConfig', 'ConfigName');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTableSelectAllKeys
GO

create procedure procTableSelectAllKeys(
			IN strTableName varchar(150),
			IN strKeyColName varchar(150),
			IN boIsNumeric TINYINT(1))
BEGIN
	IF (boIsNumeric = 0) THEN
		SET @SQL = CONCAT('	SELECT GROUP_CONCAT(CONCAT('''''''',',strKeyColName,','''''''')) KeyColname FROM ',strTableName, ' ');
	ELSE
		SET @SQL = CONCAT('	SELECT GROUP_CONCAT(',strKeyColName,') KeyColname FROM ',strTableName, ' ');
	END IF;
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procTableDeleteWithKeys
	Lemuel E. Aceron
	Aug 1, 2014
	
	Desc: This will get the all columns from a table

	CALL procTableDeleteWithKeys('sysConfig', 'ConfigName','''BACKEND_VARIATION_TYPE''');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTableDeleteWithKeys
GO

create procedure procTableDeleteWithKeys(
			IN strTableName VARCHAR(150),
			IN strKeyColName VARCHAR(150),
			IN strKeysNotToDelete VARCHAR(21000))
BEGIN
	
	SET @SQL = CONCAT('	DELETE FROM ',strTableName, ' ');

	IF LENGTH(TRIM(strKeysNotToDelete)) > 0 THEN
		SET @SQL = CONCAT(@SQL, ' WHERE ',strKeyColName,' NOT IN (',strKeysNotToDelete,') ');
	END IF;
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;



/**************************************************************

	procTerminalReportHistorySelect

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procTerminalReportHistorySelect(1, '22', '2014-12-03', '2014-12-04', '2014-12-03', 0, 1, 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportHistorySelect
GO

create procedure procTerminalReportHistorySelect(
									IN intBranchID BIGINT, 
									IN strTerminalNo VARCHAR(20), 
									IN intOnlyIncludeIneSales TINYINT(1),
									IN dteDateFrom DATETIME,
									IN dteDateTo DATETIME,
									IN dteDateLastInitialized DATETIME,
									IN boWithTF TINYINT(1),
									IN boLastInitializationDetails TINYINT(1),
									IN boNextDetails TINYINT(1))
BEGIN
	SET @SQL := '';
	
	IF boWithTF = 0 THEN
		-- this is use for backend reporting only
		SET @SQL := 'SELECT BranchID, TerminalNo,
							CASE 
								WHEN BeginningTransactionNo = 0 THEN LPAD(1, LENGTH(EndingTransactionNo), ''0'')
								WHEN BeginningTransactionNo = 1 THEN LPAD(1, LENGTH(EndingTransactionNo), ''0'')
								WHEN BeginningTransactionNo = EndingTransactionNo THEN LPAD(EndingTransactionNo-1, LENGTH(BeginningTransactionNo), ''0'')
								ELSE BeginningTransactionNo
							END BeginningTransactionNo,
							CASE
								WHEN EndingTransactionNo = 0 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								WHEN EndingTransactionNo = 1 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								WHEN EndingTransactionNo = 0 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								ELSE LPAD(EndingTransactionNo-1, LENGTH(BeginningTransactionNo), ''0'') 
							END EndingTransactionNo,
							CASE 
								WHEN BeginningORNo = 0 THEN LPAD(1, LENGTH(EndingORNo), ''0'')
								WHEN BeginningORNo = 1 THEN LPAD(1, LENGTH(EndingORNo), ''0'')
								WHEN BeginningORNo = EndingORNo THEN LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'')
								ELSE BeginningORNo
							END BeginningORNo,
							CASE 
								WHEN EndingORNo = 0 THEN LPAD(1, LENGTH(BeginningORNo), ''0'')
								WHEN EndingORNo = 1 THEN LPAD(1, LENGTH(BeginningORNo), ''0'')
								WHEN BeginningORNo = EndingORNo THEN LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'')
								ELSE LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'') 
							END EndingORNo,
							CASE 
								WHEN BeginningORNo = EndingORNo THEN 0
								WHEN BeginningORNo = 0 THEN EndingORNo - 1 - BeginningORNo 
								ELSE EndingORNo - BeginningORNo 
							END AS NoOfORNo,
							ZReadCount, 
							XReadCount, 
							NetSales, 
							GrossSales, 
							TotalDiscount, 
							SNRDiscount, 
							PWDDiscount, 
							OtherDiscount, 
							TotalCharge, 
							TotalCharge AS ServiceCharge, 
							DailySales, 
							ItemSold, 
							QuantitySold, 
							GroupSales, 
							ActualOldGrandTotal, 
							ActualNewGrandTotal, 
							OldGrandTotal, 
							NewGrandTotal, 
							VATExempt,
							NonVATableAmount, 
							VATableAmount, 
							ZeroRatedSales,
							VAT, 
							EVATableAmount, 
							NonEVATableAmount, 
							EVAT, 
							LocalTax, 
							CashSales, 
							ChequeSales, 
							CreditCardSales, 
							CreditSales, 
							RefundCash,
							RefundCheque,
							RefundCreditCard,
							RefundCredit,
							RefundDebit,
							CreditPayment, 
							CreditPaymentCash, 
							CreditPaymentCheque, 
							CreditPaymentCreditCard, 
							CreditPaymentDebit, 
							DebitPayment, 
							RewardPointsPayment, 
							RewardConvertedPayment, 
							CashInDrawer, 
							TotalDisburse, 
							CashDisburse, 
							ChequeDisburse, 
							CreditCardDisburse, 
							TotalWithhold, 
							CashWithhold, 
							ChequeWithhold, 
							CreditCardWithhold, 
							TotalPaidOut, 
							TotalDeposit, 
							CashDeposit, 
							ChequeDeposit, 
							CreditCardDeposit, 
							DebitDeposit,
							BeginningBalance, 
							VoidSales, 
							RefundSales, 
							SubTotalDiscount, 
							ItemsDiscount, 
							SNRItemsDiscount, 
							PWDItemsDiscount, 
							OtherItemsDiscount, 
							NoOfCashTransactions, 
							NoOfChequeTransactions, 
							NoOfCreditCardTransactions, 
							NoOfCreditTransactions, 
							NoOfCombinationPaymentTransactions, 
							NoOfCreditPaymentTransactions, 
							NoOfDebitPaymentTransactions, 
							NoOfClosedTransactions, 
							NoOfRefundTransactions, 
							NoOfVoidTransactions, 
							NoOfRewardPointsPayment, 
							NoOfTotalTransactions, 
							DateLastInitialized, 
							DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE tblTerminal.BranchID = tblTerminalReportHistory.BranchID AND tblTerminal.TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), ''%Y-%m-%d'') AS DateLastInitializedToDisplay, 
							TrustFund, 
							NoOfDiscountedTransactions, 
							NegativeAdjustments, 
							NoOfNegativeAdjustmentTransactions, 
							PromotionalItems, 
							CreditSalesTax, 
							BatchCounter, 
							NoOfReprintedTransaction, 
							TotalReprintedTransaction, 
							NoOfReprintedTransaction, TotalReprintedTransaction, 
							NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
							NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
							ConsignmentSales, ConsignmentRefundSales, WalkInSales,
							WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales,
							InitializedBy 
						FROM tblTerminalReportHistory
						WHERE 1 = 1 ';
	ELSE
		-- this is use for backend reporting only
		SET @SQL := 'SELECT BranchID, TerminalNo, 
							CASE 
								WHEN BeginningTransactionNo = 0 THEN LPAD(1, LENGTH(EndingTransactionNo), ''0'')
								WHEN BeginningTransactionNo = 1 THEN LPAD(1, LENGTH(EndingTransactionNo), ''0'')
								WHEN BeginningTransactionNo = EndingTransactionNo THEN LPAD(EndingTransactionNo-1, LENGTH(BeginningTransactionNo), ''0'')
								ELSE BeginningTransactionNo
							END BeginningTransactionNo,
							CASE
								WHEN EndingTransactionNo = 0 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								WHEN EndingTransactionNo = 1 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								WHEN EndingTransactionNo = 0 THEN LPAD(1, LENGTH(BeginningTransactionNo), ''0'')
								ELSE LPAD(EndingTransactionNo-1, LENGTH(BeginningTransactionNo), ''0'') 
							END EndingTransactionNo,
							CASE 
								WHEN BeginningORNo = 0 THEN LPAD(1, LENGTH(EndingORNo), ''0'')
								WHEN BeginningORNo = 1 THEN LPAD(1, LENGTH(EndingORNo), ''0'')
								WHEN BeginningORNo = EndingORNo THEN LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'')
								ELSE BeginningORNo
							END BeginningORNo,
							CASE 
								WHEN EndingORNo = 0 THEN LPAD(1, LENGTH(BeginningORNo), ''0'')
								WHEN EndingORNo = 1 THEN LPAD(1, LENGTH(BeginningORNo), ''0'')
								WHEN BeginningORNo = EndingORNo THEN LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'')
								ELSE LPAD(EndingORNo-1, LENGTH(BeginningORNo), ''0'') 
							END EndingORNo,
							CASE 
								WHEN BeginningORNo = EndingORNo THEN 0
								WHEN BeginningORNo = 0 THEN EndingORNo - 1 - BeginningORNo 
								ELSE EndingORNo - BeginningORNo 
							END AS NoOfORNo,
							ZReadCount, 
							XReadCount, 
							NetSales - (NetSales * TrustFund/100) NetSales, 
							GrossSales - (GrossSales * TrustFund/100) GrossSales, 
							TotalDiscount - (TotalDiscount * TrustFund/100) TotalDiscount, 
							SNRDiscount - (SNRDiscount * TrustFund/100) SNRDiscount, 
							PWDDiscount - (PWDDiscount * TrustFund/100) PWDDiscount, 
							OtherDiscount - (OtherDiscount * TrustFund/100) OtherDiscount, 
							TotalCharge - (TotalCharge * TrustFund/100) TotalCharge, 
							TotalCharge - (TotalCharge * TrustFund/100) ServiceCharge, 
							DailySales - (DailySales * TrustFund/100) DailySales, 
							ItemSold, 
							QuantitySold, 
							GroupSales - (GroupSales * TrustFund/100) GroupSales, 
							OldGrandTotal,
							NewGrandTotal,
							ActualOldGrandTotal, 
							ActualNewGrandTotal, 
							VATExempt - (VATExempt * TrustFund/100) VATExempt, 
							NonVATableAmount - (NonVATableAmount * TrustFund/100) NonVATableAmount, 
							VATableAmount - (VATableAmount * TrustFund/100) VATableAmount, 
							ZeroRatedSales - (ZeroRatedSales * TrustFund/100) ZeroRatedSales, 
							VAT - (VAT * TrustFund/100) VAT, 
							EVATableAmount - (EVATableAmount * TrustFund/100) EVATableAmount, 
							NonEVATableAmount - (NonEVATableAmount * TrustFund/100) NonEVATableAmount, 
							EVAT - (EVAT * TrustFund/100) EVAT, 
							LocalTax - (LocalTax * TrustFund/100) LocalTax, 
							CashSales - (CashSales * TrustFund/100) CashSales, 
							ChequeSales - (ChequeSales * TrustFund/100) ChequeSales, 
							CreditCardSales - (CreditCardSales * TrustFund/100) CreditCardSales, 
							CreditSales - (CreditSales * TrustFund/100) CreditSales, 
							RefundCash - (RefundCash * TrustFund/100) RefundCash, 
							RefundCheque - (RefundCheque * TrustFund/100) RefundCheque, 
							RefundCreditCard - (RefundCreditCard * TrustFund/100) RefundCreditCard, 
							RefundCredit - (RefundCredit * TrustFund/100) RefundCredit, 
							RefundDebit - (RefundDebit * TrustFund/100) RefundDebit, 
							CreditPayment - (CreditPayment * TrustFund/100) CreditPayment, 
							CreditPaymentCash - (CreditPaymentCash * TrustFund/100) CreditPaymentCash, 
							CreditPaymentCheque - (CreditPaymentCheque * TrustFund/100) CreditPaymentCheque, 
							CreditPaymentCreditCard - (CreditPaymentCreditCard * TrustFund/100) CreditPaymentCreditCard, 
							CreditPaymentDebit - (CreditPaymentDebit * TrustFund/100) CreditPaymentDebit, 
							DebitPayment - (DebitPayment * TrustFund/100) DebitPayment, 
							RewardPointsPayment - (RewardPointsPayment * TrustFund/100) RewardPointsPayment, 
							RewardConvertedPayment - (RewardConvertedPayment * TrustFund/100) RewardConvertedPayment, 
							CashInDrawer - (CashInDrawer * TrustFund/100) CashInDrawer, 
							TotalDisburse - (TotalDisburse * TrustFund/100) TotalDisburse, 
							CashDisburse - (CashDisburse * TrustFund/100) CashDisburse, 
							ChequeDisburse - (ChequeDisburse * TrustFund/100) ChequeDisburse, 
							CreditCardDisburse - (CreditCardDisburse * TrustFund/100) CreditCardDisburse, 
							TotalWithhold - (TotalWithhold * TrustFund/100) TotalWithhold, 
							CashWithhold - (CashWithhold * TrustFund/100) CashWithhold, 
							ChequeWithhold - (ChequeWithhold * TrustFund/100) ChequeWithhold, 
							CreditCardWithhold - (CreditCardWithhold * TrustFund/100) CreditCardWithhold, 
							TotalPaidOut - (TotalPaidOut * TrustFund/100) TotalPaidOut, 
							TotalDeposit - (Totaldeposit * TrustFund/100) Totaldeposit, 
							CashDeposit - (CashDeposit * TrustFund/100) CashDeposit, 
							ChequeDeposit - (ChequeDeposit * TrustFund/100) ChequeDeposit, 
							CreditCardDeposit - (CreditCardDeposit * TrustFund/100) CreditCardDeposit, 
							DebitDeposit - (DebitDeposit * TrustFund/100) DebitDeposit, 
							BeginningBalance, 
							VoidSales - (VoidSales * TrustFund/100) VoidSales, 
							RefundSales - (RefundSales * TrustFund/100) RefundSales, 
							SubTotalDiscount - (SubTotalDiscount * TrustFund/100) SubTotalDiscount, 
							ItemsDiscount - (ItemsDiscount * TrustFund/100) ItemsDiscount, 
							SNRItemsDiscount - (SNRItemsDiscount * TrustFund/100) SNRItemsDiscount, 
							PWDItemsDiscount - (PWDItemsDiscount * TrustFund/100) PWDItemsDiscount, 
							OtherItemsDiscount - (OtherItemsDiscount * TrustFund/100) OtherItemsDiscount, 
							NoOfCashTransactions, 
							NoOfChequeTransactions, 
							NoOfCreditCardTransactions, 
							NoOfCreditTransactions, 
							NoOfCombinationPaymentTransactions, 
							NoOfCreditPaymentTransactions, 
							NoOfDebitPaymentTransactions, 
							NoOfClosedTransactions, 
							NoOfRefundTransactions, 
							NoOfVoidTransactions, 
							NoOfRewardPointsPayment, 
							NoOfTotalTransactions, 
							DateLastInitialized, 
							DATE_FORMAT(IF(HOUR(DateLastInitialized)>(SELECT SUBSTR(EndCutOffTime,1,2) FROM tblTerminal WHERE tblTerminal.BranchID = tblTerminalReportHistory.BranchID AND tblTerminal.TerminalNo = tblTerminalReportHistory.TerminalNo), DATE_ADD(DateLastInitialized, INTERVAL 1 DAY), DateLastInitialized), ''%Y-%m-%d'') AS DateLastInitializedToDisplay, 
							TrustFund, 
							NoOfDiscountedTransactions, 
							NegativeAdjustments - (NegativeAdjustments * TrustFund/100) NegativeAdjustments, 
							NoOfNegativeAdjustmentTransactions, 
							PromotionalItems - (PromotionalItems * TrustFund/100) PromotionalItems, 
							CreditSalesTax - (CreditSalesTax * TrustFund/100) CreditSalesTax, 
							BatchCounter, 
							NoOfReprintedTransaction, 
							TotalReprintedTransaction - (TotalReprintedTransaction * TrustFund/100) TotalReprintedTransaction, 
							NoOfReprintedTransaction, TotalReprintedTransaction, 
							NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
							NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
							ConsignmentSales - (ConsignmentSales * TrustFund/100) ConsignmentSales, 
							ConsignmentRefundSales - (ConsignmentRefundSales * TrustFund/100) ConsignmentRefundSales, 
							WalkInSales - (WalkInSales * TrustFund/100) WalkInSales,
							WalkInRefundSales - (WalkInRefundSales * TrustFund/100) WalkInRefundSales, 
							OutOfStockSales - (OutOfStockSales * TrustFund/100) OutOfStockSales, 
							OutOfStockRefundSales - (OutOfStockRefundSales * TrustFund/100) OutOfStockRefundSales,
							InitializedBy 
					FROM tblTerminalReportHistory
					WHERE 1 = 1 ';
	END IF;
	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND BranchID = ', intBranchID,' ');
	END IF;
	
	IF (strTerminalNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND TerminalNo = ''', strTerminalNo,''' ');
	END IF;

	IF (intOnlyIncludeIneSales <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND IncludeIneSales = ', intOnlyIncludeIneSales,' ');
	END IF;

	IF (DATE_FORMAT(dteDateFrom, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND DateLastInitialized >= ''', dteDateFrom,''' ');
	END IF;
	
	IF (DATE_FORMAT(dteDateTo, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND DateLastInitialized <= ''', dteDateTo,''' ');
	END IF;

	IF (boNextDetails = 1 AND DATE_FORMAT(dteDateLastInitialized, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND DateLastInitialized > ''', dteDateLastInitialized,''' ');
	ELSEIF (DATE_FORMAT(dteDateLastInitialized, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND DateLastInitialized = ''', dteDateLastInitialized,''' ');
	END IF;
	
	IF (boLastInitializationDetails = 1 OR boNextDetails = 1) THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY BranchID, TerminalNo, DateLastInitialized DESC LIMIT 1 ');
	ELSE
		SET @SQL = CONCAT(@SQL,'ORDER BY BranchID, TerminalNo, DateLastInitialized ');
	END IF;

	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;


/**************************************************************

	procTransactionsSelect

	Sep 15, 2014 : Lemu
	- create this procedure

	Descrition: 
		1. get transaction details for resuming transaction
		2. get transactions list for reports
		3. get list of suspended transactions

	CALL procTransactionsSelect(1, '01', 0, '', '2014-09-20', '2014-10-2', 6, 4, 0, 0, 0, '', 0, '', '', 1, 0, '', '', 10);
	
	CALL procTransactionsSelect(1, '00', 0, '00000000052336', '1900-01-01', '1900-01-01', 6, 4, 0, 0, '', '', 0, '', '', 0, 0, '', '', 10);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTransactionsSelect
GO

create procedure procTransactionsSelect(
									IN intBranchID BIGINT, 
									IN strTerminalNo VARCHAR(20),
									IN intTransactionID BIGINT(20),
									IN strTransactionNo VARCHAR(30),
									IN dteTransactionDateFrom DATETIME,
									IN dteTransactionDateTo DATETIME,
									IN intTransactionStatus TINYINT(1),
									IN intPaymentType TINYINT(1),
									IN intisConsignment TINYINT(1),
									IN intisPacked TINYINT(1),
									IN strCustomerName VARCHAR(100),
									IN strCustomerGroupName VARCHAR(60),
									IN intCashierID BIGINT(20),
									IN strCashierName VARCHAR(100),
									IN strAgentName VARCHAR(100),
									IN boWithTF TINYINT(1),
									IN boShowSuspended TINYINT(1),
									IN strSortField VARCHAR(200), 
									IN strSortOption VARCHAR(4), 
									IN intLimit INT(10))
BEGIN
	SET @SQL := '';
	
	IF boShowSuspended THEN
		SET @SQL := 'SELECT trx.TransactionNo, trx.CustomerName, trx.DateSuspended, 
							IF(trx.isConsignment=0,trx.CreditPayment,0) ''CreditPayment'', 
							IF(trx.isConsignment<>0,trx.CreditPayment,0) ''ConsignmentPayment'', 
							trx.TransactionType, trx.TransactionID, trx.isConsignment, 
							IFNULL(chque.ChequeNo,'''') AS ChequeNo, 
							IFNULL(chque.ValidityDate,''1900-01-01'') AS ValidityDate
						FROM tblTransactions trx
						LEFT OUTER JOIN tblChequePayment chque ON trx.BranchID = trx.BranchID AND trx.TerminalNo = trx.TerminalNo AND trx.TransactionID = chque.TransactionID
						WHERE trx.TransactionStatus = 2 OR trx.TransactionStatus = 13 '; -- 2=SuspendedTransactionStatus		13=SuspendedOpenTransactionStatus
	ELSEIF boWithTF = 0 THEN
		SET @SQL := 'SELECT trx.BranchID, trx.TerminalNo,
							trx.TransactionID,
							trx.TransactionNo,
							trx.RewardsCustomerID,
							trx.RewardsCustomerName,
							trx.CustomerID,
							trx.CustomerName,
							trx.CashierID,
							trx.CashierName,
							trx.TransactionDate,
							trx.DateSuspended,
							trx.DateResumed,
							trx.TransactionStatus,
							CASE TransactionStatus 
                                WHEN 0 THEN ''Open''
                                WHEN 1 THEN ''Closed'' 
                                WHEN 2 THEN ''Suspended''
                                WHEN 3 THEN ''Void'' 
                                WHEN 4 THEN ''Reprinted'' 
                                WHEN 5 THEN ''Refund''
                                WHEN 6 THEN ''NotYetApplied''
                                WHEN 7 THEN ''CreditPayment'' 
                                WHEN 8 THEN ''DebitPayment''
                                WHEN 9 THEN ''Released''
                                WHEN 10 THEN ''OrderSlip''
								WHEN 11 THEN ''ParkingTicket''
                            END TransactionStatusName,
							trx.GrossSales,
							trx.SubTotal,
							trx.Discount,
							trx.TransDiscount,
							trx.TransDiscountType,
							trx.VAT,
							trx.VatableAmount,
							trx.ZeroRatedSales,
							trx.EVAT,
							trx.EVatableAmount,
							trx.LocalTax,
							trx.AmountPaid,
							trx.CashPayment,
							trx.ChequePayment,
							trx.CreditCardPayment,
							trx.CreditPayment,
							IF(trx.isConsignment<>0,trx.CreditPayment,0) ConsignmentPayment,
							trx.BalanceAmount,
							trx.ChangeAmount,
							trx.DateClosed,
							trx.PaymentType,
							trx.DiscountCode,
							trx.DiscountRemarks,
							trx.DebitPayment,
							trx.ItemsDiscount,
							trx.SNRItemsDiscount,
							trx.PWDItemsDiscount,
							trx.OtherItemsDiscount,
							trx.Charge,
							trx.ChargeAmount,
							trx.ChargeCode,
							trx.ChargeRemarks,
							trx.ChargeType,
							trx.WaiterID,
							trx.WaiterName,
							trx.Packed,
							trx.OrderType,
							trx.AgentID,
							trx.AgentName,
							trx.CreatedByID,
							trx.CreatedByName,
							trx.AgentDepartmentName,
							trx.AgentPositionName,
							trx.ReleaserID,
							trx.ReleaserName,
							trx.ReleasedDate,
							trx.RewardPointsPayment,
							trx.RewardConvertedPayment,
							trx.PaxNo,
							trx.ModeOfTerms,
							trx.Terms,
							trx.CRNo,
							trx.CreditChargeAmount,
							trx.BranchCode,
							trx.TransactionType,
							trx.isConsignment,
							trx.isZeroRated,
							trx.DataSource,
							trx.CustomerGroupName,
							trx.CreatedOn,
							trx.LastModified,
							trx.ORNo,
							trx.SyncID,
							trx.NonVATableAmount,
							trx.VATExempt,
							trx.NonEVATableAmount,
							trx.SNRDiscount,
							trx.PWDDiscount,
							trx.OtherDiscount,
							trx.NetSales,
							trx.ItemSold,
							trx.QuantitySold,
							IFNULL(tr.TrustFund, 0) TrustFund,
							IFNULL(chque.ChequeNo,'''') AS ChequeNo, 
							IFNULL(chque.ValidityDate,''1900-01-01'') AS ValidityDate
						FROM tblTransactions trx
						LEFT OUTER JOIN tblTerminalReport tr ON trx.BranchID = tr.BranchID AND trx.TerminalNo = tr.TerminalNo
						LEFT OUTER JOIN tblChequePayment chque ON trx.BranchID = trx.BranchID AND trx.TerminalNo = trx.TerminalNo AND trx.TransactionID = chque.TransactionID
						WHERE 1 = 1 ';
	ELSE
		SET @SQL := 'SELECT trx.BranchID, trx.TerminalNo,
							trx.TransactionID,
							trx.TransactionNo,
							trx.RewardsCustomerID,
							trx.RewardsCustomerName,
							trx.CustomerID,
							trx.CustomerName,
							trx.CashierID,
							trx.CashierName,
							trx.TransactionDate,
							trx.DateSuspended,
							trx.DateResumed,
							trx.TransactionStatus,
							CASE TransactionStatus 
                                WHEN 0 THEN ''Open''
                                WHEN 1 THEN ''Closed'' 
                                WHEN 2 THEN ''Suspended''
                                WHEN 3 THEN ''Void'' 
                                WHEN 4 THEN ''Reprinted'' 
                                WHEN 5 THEN ''Refund''
                                WHEN 6 THEN ''NotYetApplied''
                                WHEN 7 THEN ''CreditPayment'' 
                                WHEN 8 THEN ''DebitPayment''
                                WHEN 9 THEN ''Released''
                                WHEN 10 THEN ''OrderSlip''
								WHEN 11 THEN ''ParkingTicket''
                            END TransactionStatusName,
							trx.GrossSales - (trx.GrossSales * IFNULL(trh.TrustFund, 0)/100) GrossSales,
							trx.SubTotal - (trx.SubTotal * IFNULL(trh.TrustFund, 0)/100) SubTotal,
							trx.Discount - (trx.Discount * IFNULL(trh.TrustFund, 0)/100) Discount,
							trx.TransDiscount,
							trx.TransDiscountType,
							trx.VAT - (trx.VAT * IFNULL(trh.TrustFund, 0)/100) VAT,
							trx.VatableAmount - (trx.VatableAmount * IFNULL(trh.TrustFund, 0)/100) VatableAmount,
							trx.ZeroRatedSales - (trx.ZeroRatedSales * IFNULL(trh.TrustFund, 0)/100) ZeroRatedSales,
							trx.EVAT - (trx.EVAT * IFNULL(trh.TrustFund, 0)/100) EVAT,
							trx.EVatableAmount - (trx.EVatableAmount * IFNULL(trh.TrustFund, 0)/100) EVatableAmount,
							trx.LocalTax - (trx.LocalTax * IFNULL(trh.TrustFund, 0)/100) LocalTax,
							trx.AmountPaid - (trx.AmountPaid * IFNULL(trh.TrustFund, 0)/100) AmountPaid,
							trx.CashPayment - (trx.CashPayment * IFNULL(trh.TrustFund, 0)/100) CashPayment,
							trx.ChequePayment - (trx.ChequePayment * IFNULL(trh.TrustFund, 0)/100) ChequePayment,
							trx.CreditCardPayment - (trx.CreditCardPayment * IFNULL(trh.TrustFund, 0)/100) CreditCardPayment,
							trx.CreditPayment - (trx.CreditPayment * IFNULL(trh.TrustFund, 0)/100) CreditPayment,
							IF(trx.isConsignment<>0,trx.CreditPayment - (trx.CreditPayment * IFNULL(trh.TrustFund, 0)/100),0) ConsignmentPayment,
							trx.BalanceAmount - (trx.BalanceAmount * IFNULL(trh.TrustFund, 0)/100) BalanceAmount,
							trx.ChangeAmount - (trx.ChangeAmount * IFNULL(trh.TrustFund, 0)/100) ChangeAmount,
							trx.DateClosed,
							trx.PaymentType,
							trx.DiscountCode,
							trx.DiscountRemarks,
							trx.DebitPayment,
							trx.ItemsDiscount - (trx.ItemsDiscount * IFNULL(trh.TrustFund, 0)/100) ItemsDiscount,
							trx.SNRItemsDiscount - (trx.SNRItemsDiscount * IFNULL(trh.TrustFund, 0)/100) SNRItemsDiscount,
							trx.PWDItemsDiscount - (trx.PWDItemsDiscount * IFNULL(trh.TrustFund, 0)/100) PWDItemsDiscount,
							trx.OtherItemsDiscount - (trx.OtherItemsDiscount * IFNULL(trh.TrustFund, 0)/100) OtherItemsDiscount,
							trx.Charge - (trx.Charge * IFNULL(trh.TrustFund, 0)/100) Charge,
							trx.ChargeAmount - (trx.ChargeAmount * IFNULL(trh.TrustFund, 0)/100) ChargeAmount,
							trx.ChargeCode,
							trx.ChargeRemarks,
							trx.ChargeType,
							trx.WaiterID,
							trx.WaiterName,
							trx.Packed,
							trx.OrderType,
							trx.AgentID,
							trx.AgentName,
							trx.CreatedByID,
							trx.CreatedByName,
							trx.AgentDepartmentName,
							trx.AgentPositionName,
							trx.ReleaserID,
							trx.ReleaserName,
							trx.ReleasedDate,
							trx.RewardPointsPayment - (trx.RewardPointsPayment * IFNULL(trh.TrustFund, 0)/100) RewardPointsPayment,
							trx.RewardConvertedPayment - (trx.RewardConvertedPayment * IFNULL(trh.TrustFund, 0)/100) RewardConvertedPayment,
							trx.PaxNo,
							trx.ModeOfTerms,
							trx.Terms,
							trx.CRNo,
							trx.CreditChargeAmount - (trx.CreditChargeAmount * IFNULL(trh.TrustFund, 0)/100) CreditChargeAmount,
							trx.BranchCode,
							trx.TransactionType,
							trx.isConsignment,
							trx.isZeroRated,
							trx.DataSource,
							trx.CustomerGroupName,
							trx.CreatedOn,
							trx.LastModified,
							trx.ORNo,
							trx.SyncID,
							trx.NonVATableAmount - (trx.NonVATableAmount * IFNULL(trh.TrustFund, 0)/100) NonVATableAmount,
							trx.VATExempt - (trx.VATExempt * IFNULL(trh.TrustFund, 0)/100) VATExempt,
							trx.NonEVATableAmount - (trx.NonEVATableAmount * IFNULL(trh.TrustFund, 0)/100) NonEVATableAmount,
							trx.SNRDiscount - (trx.SNRDiscount * IFNULL(trh.TrustFund, 0)/100) SNRDiscount,
							trx.PWDDiscount - (trx.PWDDiscount * IFNULL(trh.TrustFund, 0)/100) PWDDiscount,
							trx.OtherDiscount - (trx.OtherDiscount * IFNULL(trh.TrustFund, 0)/100) OtherDiscount,
							trx.NetSales - (trx.NetSales * IFNULL(trh.TrustFund, 0)/100) NetSales,
							trx.ItemSold,
							trx.QuantitySold,
							IFNULL(trh.TrustFund, 0) TrustFund,
							IFNULL(chque.ChequeNo,'''') AS ChequeNo, 
							IFNULL(chque.ValidityDate,''1900-01-01'') AS ValidityDate
					FROM tblTransactions trx 
					LEFT OUTER JOIN tblChequePayment chque ON trx.BranchID = trx.BranchID AND trx.TerminalNo = trx.TerminalNo AND trx.TransactionID = chque.TransactionID
					LEFT OUTER JOIN (SELECT DISTINCT BranchID, TerminalNo, BeginningTransactionNo,
										CASE 
											WHEN EndingTransactionNo = 0 THEN EndingTransactionNo
											ELSE LPAD(EndingTransactionNo-1, LENGTH(BeginningTransactionNo), ''0'') 
										END EndingTransactionNo,
										TrustFund
									 FROM tblTerminalReportHistory)
															 trh ON trx.BranchID = trh.BranchID AND trx.TerminalNo = trh.TerminalNo AND
																	BeginningTransactionNo <= trx.TransactionNo AND EndingTransactionNo >= trx.TransactionNo
					WHERE 1 = 1 ';
	END IF;
	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.BranchID = ', intBranchID,' ');
	END IF;
	
	IF (strTerminalNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.TerminalNo = ''', strTerminalNo,''' ');
	END IF;

	IF (intTransactionID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.TransactionID = ', intTransactionID,' ');
	END IF;

	IF (strTransactionNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.TransactionNo = ''', strTransactionNo,''' ');
	END IF;
	
	IF (DATE_FORMAT(dteTransactionDateFrom, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate >= ''', dteTransactionDateFrom,''' ');
	END IF;

	IF (DATE_FORMAT(dteTransactionDateTo, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate <= ''', dteTransactionDateTo,''' ');
	END IF;

	IF (intTransactionStatus <> 6) THEN -- NotYetAssigned
		SET @SQL = CONCAT(@SQL,'AND trx.TransactionStatus = ', intTransactionStatus,' ');
	END IF;

	IF (intPaymentType <> 4) THEN -- NotYetAssigned
		SET @SQL = CONCAT(@SQL,'AND trx.PaymentType = ', intPaymentType,' ');
	END IF;

	IF (intisConsignment <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.isConsignment = ', intisConsignment,' ');
	END IF;

	IF (intisPacked <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.isPacked = ', intisPacked,' ');
	END IF;

	IF (strCustomerName <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerName LIKE ''%', strCustomerName,'%'' ');
	END IF;

	IF (strCustomerGroupName <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerGroupName LIKE ''%', strCustomerGroupName,'%'' ');
	END IF;

	IF (intCashierID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CashierID = ', intCashierID,' ');
	END IF;

	IF (strCashierName <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CashierName LIKE ''%', strCashierName,'%'' ');
	END IF;

	IF (strAgentName <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.AgentName LIKE ''%', strAgentName,'%'' ');
	END IF;

	IF (strSortField <> '') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ' ');
	ELSE
		SET @SQL = CONCAT(@SQL,'ORDER BY BranchID, TerminalNo, TransactionDate ');
	END IF;

	IF (strSortOption <> '') THEN SET @SQL = CONCAT(@SQL,strSortOption,' '); END IF;
	IF (intLimit <> 0) THEN SET @SQL = CONCAT(@SQL,'LIMIT ', intLimit,' '); END IF;

	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;

	
END;
GO
delimiter ;


/********************************************
	procCashierReportHistorySyncTransactionSales

	CALL procCashierReportHistorySyncTransactionSales( 1, '01', '2015-01-28 19:05:45', '2015-01-29 18:58:42');
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashierReportHistorySyncTransactionSales
GO

create procedure procCashierReportHistorySyncTransactionSales(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10),
	IN dteActualZReadDate DATETIME,
	IN dteNextZReadDate DATETIME
)
BEGIN
	
	UPDATE tblCashierReportHistory 
	LEFT JOIN (
			select BranchID, TerminalNo, CashierID,
					SUM(CASE TransactionStatus 
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) NetSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN GrossSales WHEN 4 THEN GrossSales WHEN 5 THEN GrossSales WHEN 9 THEN GrossSales WHEN 11 THEN GrossSales ELSE 0
						END) GrossSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Subtotal WHEN 4 THEN Subtotal WHEN 5 THEN Subtotal WHEN 9 THEN Subtotal WHEN 11 THEN Subtotal ELSE 0
						END) SubTotal, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Discount WHEN 4 THEN Discount WHEN 5 THEN Discount WHEN 9 THEN Discount WHEN 11 THEN Discount ELSE 0
						END) Discount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemsDiscount WHEN 4 THEN ItemsDiscount WHEN 5 THEN ItemsDiscount WHEN 9 THEN ItemsDiscount WHEN 11 THEN ItemsDiscount ELSE 0
						END) ItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRItemsDiscount WHEN 4 THEN SNRItemsDiscount WHEN 5 THEN SNRItemsDiscount WHEN 9 THEN SNRItemsDiscount WHEN 11 THEN SNRItemsDiscount ELSE 0
						END) SNRItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDItemsDiscount WHEN 4 THEN PWDItemsDiscount WHEN 5 THEN PWDItemsDiscount WHEN 9 THEN PWDItemsDiscount WHEN 11 THEN PWDItemsDiscount ELSE 0
						END) PWDItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherItemsDiscount WHEN 4 THEN OtherItemsDiscount WHEN 5 THEN OtherItemsDiscount WHEN 9 THEN OtherItemsDiscount WHEN 11 THEN OtherItemsDiscount ELSE 0
						END) OtherItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRDiscount WHEN 4 THEN SNRDiscount WHEN 5 THEN SNRDiscount WHEN 9 THEN SNRDiscount WHEN 11 THEN SNRDiscount ELSE 0
						END) SNRDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDDiscount WHEN 4 THEN PWDDiscount WHEN 5 THEN PWDDiscount WHEN 9 THEN PWDDiscount WHEN 11 THEN PWDDiscount ELSE 0
						END) PWDDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherDiscount WHEN 4 THEN OtherDiscount WHEN 5 THEN OtherDiscount WHEN 9 THEN OtherDiscount WHEN 11 THEN OtherDiscount ELSE 0
						END) OtherDiscount,
					SUM(CASE TransactionStatus
							WHEN 1 THEN Charge WHEN 4 THEN Charge WHEN 5 THEN Charge WHEN 9 THEN Charge WHEN 11 THEN Charge ELSE 0
						END) TotalCharge, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) DailySales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemSold WHEN 4 THEN ItemSold WHEN 5 THEN ItemSold WHEN 9 THEN ItemSold WHEN 11 THEN ItemSold ELSE 0
						END) ItemSold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN QuantitySold WHEN 4 THEN QuantitySold WHEN 5 THEN QuantitySold WHEN 9 THEN QuantitySold WHEN 11 THEN QuantitySold ELSE 0
						END) QuantitySold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATExempt WHEN 4 THEN VATExempt WHEN 5 THEN VATExempt WHEN 9 THEN VATExempt WHEN 11 THEN VATExempt ELSE 0
						END) VATExempt, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonVATableAmount WHEN 4 THEN NonVATableAmount WHEN 5 THEN NonVATableAmount WHEN 9 THEN NonVATableAmount WHEN 11 THEN NonVATableAmount ELSE 0
						END) NonVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATableAmount WHEN 4 THEN VATableAmount WHEN 5 THEN VATableAmount WHEN 9 THEN VATableAmount WHEN 11 THEN VATableAmount ELSE 0
						END) VATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ZeroRatedSales WHEN 4 THEN ZeroRatedSales WHEN 5 THEN ZeroRatedSales WHEN 9 THEN ZeroRatedSales WHEN 11 THEN ZeroRatedSales ELSE 0
						END) ZeroRatedSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VAT WHEN 4 THEN VAT WHEN 5 THEN VAT WHEN 9 THEN VAT WHEN 11 THEN VAT ELSE 0
						END) VAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVATableAmount WHEN 4 THEN EVATableAmount WHEN 5 THEN EVATableAmount WHEN 9 THEN EVATableAmount WHEN 11 THEN EVATableAmount ELSE 0
						END) EVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonEVATableAmount WHEN 4 THEN NonEVATableAmount WHEN 5 THEN NonEVATableAmount WHEN 9 THEN NonEVATableAmount WHEN 11 THEN NonEVATableAmount ELSE 0
						END) NonEVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVAT WHEN 4 THEN EVAT WHEN 5 THEN EVAT WHEN 9 THEN EVAT WHEN 11 THEN EVAT ELSE 0
						END) EVAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN LocalTax WHEN 4 THEN LocalTax WHEN 5 THEN LocalTax WHEN 9 THEN LocalTax WHEN 11 THEN LocalTax ELSE 0
						END) LocalTax,
					SUM(CASE TransactionStatus
							WHEN 1 THEN CashPayment WHEN 4 THEN CashPayment WHEN 9 THEN CashPayment WHEN 11 THEN CashPayment ELSE 0
						END) CashSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ChequePayment WHEN 4 THEN ChequePayment WHEN 9 THEN ChequePayment WHEN 11 THEN ChequePayment ELSE 0
						END) ChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditCardPayment WHEN 4 THEN CreditCardPayment WHEN 9 THEN CreditCardPayment WHEN 11 THEN CreditCardPayment ELSE 0
						END) CreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditPayment WHEN 4 THEN CreditPayment WHEN 9 THEN CreditPayment WHEN 11 THEN CreditPayment ELSE 0
						END) CreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 1 THEN DebitPayment WHEN 4 THEN DebitPayment WHEN 9 THEN DebitPayment WHEN 11 THEN DebitPayment ELSE 0
						END) DebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN CashPayment ELSE 0
						END) RefundCashSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN ChequePayment ELSE 0
						END) RefundChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditCardPayment ELSE 0
						END) RefundCreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditPayment ELSE 0
						END) RefundCreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN DebitPayment ELSE 0
						END) RefundDebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment + ChequePayment + CreditCardPayment + DebitPayment ELSE 0
						END) CreditPayment,
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment ELSE 0
						END) CreditPaymentCash, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN ChequePayment ELSE 0
						END) CreditPaymentCheque, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN CreditCardPayment ELSE 0
						END) CreditPaymentCreditCard, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN DebitPayment ELSE 0
						END) CreditPaymentDebit, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardPointsPayment WHEN 4 THEN RewardPointsPayment WHEN 5 THEN RewardPointsPayment WHEN 9 THEN RewardPointsPayment WHEN 11 THEN RewardPointsPayment ELSE 0
						END) RewardPointsPayment,
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardConvertedPayment WHEN 4 THEN RewardConvertedPayment WHEN 5 THEN RewardConvertedPayment WHEN 9 THEN RewardConvertedPayment WHEN 11 THEN RewardConvertedPayment ELSE 0
						END) RewardConvertedPayment,
					SUM(CASE TransactionStatus
							WHEN 3 THEN Subtotal ELSE 0
						END) VoidSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN Subtotal ELSE 0
						END) RefundSales,
					SUM(CASE TransactionStatus
							WHEN 14 THEN Subtotal ELSE 0
						END) WalkInSales,
					SUM(CASE TransactionStatus
							WHEN 15 THEN Subtotal ELSE 0
						END) OutOfStockSales,
					SUM(CASE TransactionStatus
							WHEN 16 THEN Subtotal ELSE 0
						END) ConsignmentSales,
					SUM(CASE TransactionStatus
							WHEN 17 THEN Subtotal ELSE 0
						END) WalkInRefundSales,
					SUM(CASE TransactionStatus
							WHEN 18 THEN Subtotal ELSE 0
						END) OutOfStockRefundSales,
					SUM(CASE TransactionStatus
							WHEN 19 THEN Subtotal ELSE 0
						END) ConsignmentRefundSales
			FROM  tblTransactions
					WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
						AND TransactionStatus NOT IN (0,2) -- remove the open, suspended transactions
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s')
			GROUP BY BranchID, TerminalNo, CashierID
		) Trx ON tblCashierReportHistory.BranchID = Trx.BranchID AND tblCashierReportHistory.TerminalNo = Trx.BranchID AND tblCashierReportHistory.CashierID = Trx.CashierID
	SET					
						tblCashierReportHistory.NetSales						=  IFNULL(Trx.NetSales,0), 
						tblCashierReportHistory.GrossSales						=  IFNULL(Trx.GrossSales,0), 
						tblCashierReportHistory.TotalDiscount					=  IFNULL(Trx.Discount,0) + IFNULL(Trx.ItemsDiscount,0), 
						tblCashierReportHistory.SNRDiscount					  	=  IFNULL(Trx.SNRDiscount,0), 
						tblCashierReportHistory.PWDDiscount					  	=  IFNULL(Trx.PWDDiscount,0), 
						tblCashierReportHistory.OtherDiscount					=  IFNULL(Trx.OtherDiscount,0),
						tblCashierReportHistory.TotalCharge						=  IFNULL(Trx.TotalCharge,0), 
						tblCashierReportHistory.DailySales						=  IFNULL(Trx.DailySales,0), 
						tblCashierReportHistory.ItemSold						=  IFNULL(Trx.ItemSold,0), 
						tblCashierReportHistory.QuantitySold					=  IFNULL(Trx.QuantitySold,0), 
						tblCashierReportHistory.GroupSales						=  IFNULL(Trx.SubTotal,0), 
						tblCashierReportHistory.VATExempt   					=  IFNULL(Trx.VATExempt,0), 
						tblCashierReportHistory.NonVATableAmount				=  IFNULL(Trx.NonVATableAmount,0), 
						tblCashierReportHistory.VATableAmount					=  IFNULL(Trx.VATableAmount,0), 
						tblCashierReportHistory.ZeroRatedSales					=  IFNULL(Trx.ZeroRatedSales,0), 
						tblCashierReportHistory.VAT								=  IFNULL(Trx.VAT,0), 
						tblCashierReportHistory.EVATableAmount					=  IFNULL(Trx.EVATableAmount,0), 
						tblCashierReportHistory.NonEVATableAmount				=  IFNULL(Trx.NonEVATableAmount,0), 
						tblCashierReportHistory.EVAT							=  IFNULL(Trx.EVAT,0), 
						tblCashierReportHistory.LocalTax						=  IFNULL(Trx.LocalTax,0), 
						tblCashierReportHistory.CashSales						=  IFNULL(Trx.CashSales,0), 
						tblCashierReportHistory.ChequeSales						=  IFNULL(Trx.ChequeSales,0), 
						tblCashierReportHistory.CreditCardSales					=  IFNULL(Trx.CreditCardSales,0), 
						tblCashierReportHistory.CreditSales						=  IFNULL(Trx.CreditSales,0), 
						tblCashierReportHistory.DebitPayment					=  IFNULL(Trx.DebitPayment,0), 
						tblCashierReportHistory.RefundCash						=  IFNULL(Trx.RefundCashSales,0), 
						tblCashierReportHistory.RefundCheque					=  IFNULL(Trx.RefundChequeSales,0), 
						tblCashierReportHistory.RefundCreditCard				=  IFNULL(Trx.RefundCreditCardSales,0), 
						tblCashierReportHistory.RefundCredit					=  IFNULL(Trx.RefundCreditSales,0), 
						tblCashierReportHistory.RefundDebit						=  IFNULL(Trx.RefundDebitPayment,0), 
						tblCashierReportHistory.CreditPayment					=  IFNULL(Trx.CreditPayment,0), 
						tblCashierReportHistory.CreditPaymentCash				=  IFNULL(Trx.CreditPaymentCash,0), 
						tblCashierReportHistory.CreditPaymentCheque				=  IFNULL(Trx.CreditPaymentCheque,0), 
						tblCashierReportHistory.CreditPaymentCreditCard			=  IFNULL(Trx.CreditPaymentCreditCard,0), 
						tblCashierReportHistory.CreditPaymentDebit				=  IFNULL(Trx.CreditPaymentDebit,0), 
					
						tblCashierReportHistory.RewardPointsPayment				=  IFNULL(Trx.RewardPointsPayment,0),
						tblCashierReportHistory.RewardConvertedPayment			=  IFNULL(Trx.RewardConvertedPayment,0),
						tblCashierReportHistory.CashInDrawer					=  IFNULL(Trx.CashSales,0) - (-IFNULL(Trx.RefundCashSales,0)) + IFNULL(Trx.CreditPaymentCash,0) + IFNULL(Trx.WalkInSales,0) + IFNULL(Trx.WalkInRefundSales,0) + tblCashierReportHistory.BeginningBalance + tblCashierReportHistory.TotalWithHold + tblCashierReportHistory.TotalDeposit - tblCashierReportHistory.TotalPaidOut - tblCashierReportHistory.TotalDisburse, 
						tblCashierReportHistory.VoidSales						=  IFNULL(Trx.VoidSales,0), 
						tblCashierReportHistory.RefundSales						=  IFNULL(Trx.RefundSales,0), 
						tblCashierReportHistory.ItemsDiscount					=  IFNULL(Trx.ItemsDiscount,0), 
						tblCashierReportHistory.SNRItemsDiscount				=  IFNULL(Trx.SNRItemsDiscount,0), 
						tblCashierReportHistory.PWDItemsDiscount				=  IFNULL(Trx.PWDItemsDiscount,0), 
						tblCashierReportHistory.OtherItemsDiscount				=  IFNULL(Trx.OtherItemsDiscount,0), 
						tblCashierReportHistory.SubTotalDiscount				=  IFNULL(Trx.Discount,0),

						tblCashierReportHistory.ConsignmentSales				=  IFNULL(Trx.ConsignmentSales,0),
						tblCashierReportHistory.ConsignmentRefundSales			=  IFNULL(Trx.ConsignmentRefundSales,0),
						tblCashierReportHistory.WalkInSales						=  IFNULL(Trx.WalkInSales,0),
						tblCashierReportHistory.WalkInRefundSales				=  IFNULL(Trx.WalkInRefundSales,0),
						tblCashierReportHistory.OutOfStockSales					=  IFNULL(Trx.OutOfStockSales,0),
						tblCashierReportHistory.OutOfStockRefundSales			=  IFNULL(Trx.OutOfStockRefundSales,0),

						tblCashierReportHistory.IsProcessed						=  1	-- this must be set to 0 during salestransaction update
	WHERE tblCashierReportHistory.BranchID = intBranchID AND tblCashierReportHistory.TerminalNo = strTerminalNo
		AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
		AND DATE_FORMAT(LastLoginDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s');
	
END;
GO
delimiter ;


/********************************************
	procTerminalReportHistorySyncTransactionSales

	CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2014-12-01 00:00');

	-- this must be run when version 4.0.1.1 is updated
	-- this should be run up to the last zread to correct the grandtotals.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportHistorySyncTransactionSales
GO

create procedure procTerminalReportHistorySyncTransactionSales(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10),
	IN dteZReadDateFrom datetime
)
BEGIN
	DECLARE intCtr, intCount bigint DEFAULT 0;
	DECLARE dteActualZReadDate DATETIME DEFAULT NULL;
	DECLARE dteNextZReadDate DATETIME DEFAULT NULL;
	DECLARE decSNRPercent DECIMAL(5,2) DEFAULT 0.20;
	DECLARE strSNRDiscountCode, strPWDDiscountCode VARCHAR(10) DEFAULT 'SNR';
	
	DECLARE curActualZReadDate CURSOR FOR SELECT DateLastInitialized FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteZReadDateFrom, '%Y-%m-%d %H:%i:%s'); 

	SELECT COUNT(DateLastInitialized) INTO intCount FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteZReadDateFrom, '%Y-%m-%d %H:%i:%s'); 

	SET strSNRDiscountCode = (IFNULL((SELECT SeniorCitizenDiscountCode FROM tblTerminal WHERE TerminalNo='01' LIMIT 1), 'SNR'));
	SET strPWDDiscountCode = (IFNULL((SELECT PWDDiscountCode FROM tblTerminal WHERE TerminalNo='01' LIMIT 1), 'PWD'));
	SET decSNRPercent = (IFNULL((SELECT DiscountPrice FROM tblDiscount WHERE DiscountCode = strSNRDiscountCode), 20) / 100);

	OPEN curActualZReadDate;
	curActualZReadDate: LOOP
		SET intCtr = intCtr + 1; 
		IF (intCtr > intCount) THEN LEAVE curActualZReadDate; END IF;
		
		FETCH curActualZReadDate INTO dteActualZReadDate;

		SET dteNextZReadDate = (SELECT DateLastInitialized FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND DateLastInitialized > dteActualZReadDate ORDER BY DateLastInitialized LIMIT 1);
		IF (IFNULL(dteNextZReadDate,'') = '') THEN
			SET dteNextZReadDate = (SELECT DateLastInitialized FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND DateLastInitialized > dteActualZReadDate ORDER BY DateLastInitialized LIMIT 1);
		END IF;

		-- update the vat to make sure everything is correct
		UPDATE tblTransactions SET
			VATExempt = CASE DiscountCode WHEN strSNRDiscountCode THEN Discount / decSNRPercent ELSE 0 END,
			SNRDiscount = CASE DiscountCode WHEN strSNRDiscountCode THEN Discount ELSE 0 END,
			PWDDiscount = CASE DiscountCode WHEN strPWDDiscountCode THEN Discount ELSE 0 END,
			OtherDiscount = CASE DiscountCode WHEN strSNRDiscountCode THEN 0 WHEN strPWDDiscountCode THEN 0 ELSE Discount END
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s');

		UPDATE tblTransactions SET
			GrossSales = SubTotal + itemsdiscount,
			VatableAmount = (SubTotal - Discount - VATExempt - (VATExempt * 0.12) - NonVATableAmount) / 1.12,
			VAT = (SubTotal - Discount - VATExempt - (VATExempt * 0.12) - NonVATableAmount) / 1.12 * 0.12,
			NetSales = SubTotal - (VATExempt * 0.12) - Discount
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
			AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s');

		CALL procCashierReportHistorySyncTransactionSales(intBranchID, strTerminalNo, dteActualZReadDate, dteNextZReadDate);
			
		UPDATE tblTerminalReportHistory
		LEFT JOIN (
			select BranchID, TerminalNo, 
					SUM(CASE TransactionStatus 
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) NetSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN GrossSales WHEN 4 THEN GrossSales WHEN 5 THEN GrossSales WHEN 9 THEN GrossSales WHEN 11 THEN GrossSales ELSE 0
						END) GrossSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Subtotal WHEN 4 THEN Subtotal WHEN 5 THEN Subtotal WHEN 9 THEN Subtotal WHEN 11 THEN Subtotal ELSE 0
						END) SubTotal, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN Discount WHEN 4 THEN Discount WHEN 5 THEN Discount WHEN 9 THEN Discount WHEN 11 THEN Discount ELSE 0
						END) Discount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemsDiscount WHEN 4 THEN ItemsDiscount WHEN 5 THEN ItemsDiscount WHEN 9 THEN ItemsDiscount WHEN 11 THEN ItemsDiscount ELSE 0
						END) ItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRItemsDiscount WHEN 4 THEN SNRItemsDiscount WHEN 5 THEN SNRItemsDiscount WHEN 9 THEN SNRItemsDiscount WHEN 11 THEN SNRItemsDiscount ELSE 0
						END) SNRItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDItemsDiscount WHEN 4 THEN PWDItemsDiscount WHEN 5 THEN PWDItemsDiscount WHEN 9 THEN PWDItemsDiscount WHEN 11 THEN PWDItemsDiscount ELSE 0
						END) PWDItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherItemsDiscount WHEN 4 THEN OtherItemsDiscount WHEN 5 THEN OtherItemsDiscount WHEN 9 THEN OtherItemsDiscount WHEN 11 THEN OtherItemsDiscount ELSE 0
						END) OtherItemsDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN SNRDiscount WHEN 4 THEN SNRDiscount WHEN 5 THEN SNRDiscount WHEN 9 THEN SNRDiscount WHEN 11 THEN SNRDiscount ELSE 0
						END) SNRDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN PWDDiscount WHEN 4 THEN PWDDiscount WHEN 5 THEN PWDDiscount WHEN 9 THEN PWDDiscount WHEN 11 THEN PWDDiscount ELSE 0
						END) PWDDiscount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN OtherDiscount WHEN 4 THEN OtherDiscount WHEN 5 THEN OtherDiscount WHEN 9 THEN OtherDiscount WHEN 11 THEN OtherDiscount ELSE 0
						END) OtherDiscount,
					SUM(CASE TransactionStatus
							WHEN 1 THEN Charge WHEN 4 THEN Charge WHEN 5 THEN Charge WHEN 9 THEN Charge WHEN 11 THEN Charge ELSE 0
						END) TotalCharge, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NetSales WHEN 4 THEN NetSales WHEN 5 THEN NetSales WHEN 9 THEN NetSales WHEN 11 THEN NetSales ELSE 0
						END) DailySales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ItemSold WHEN 4 THEN ItemSold WHEN 5 THEN ItemSold WHEN 9 THEN ItemSold WHEN 11 THEN ItemSold ELSE 0
						END) ItemSold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN QuantitySold WHEN 4 THEN QuantitySold WHEN 5 THEN QuantitySold WHEN 9 THEN QuantitySold WHEN 11 THEN QuantitySold ELSE 0
						END) QuantitySold,
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATExempt WHEN 4 THEN VATExempt WHEN 5 THEN VATExempt WHEN 9 THEN VATExempt WHEN 11 THEN VATExempt ELSE 0
						END) VATExempt, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonVATableAmount WHEN 4 THEN NonVATableAmount WHEN 5 THEN NonVATableAmount WHEN 9 THEN NonVATableAmount WHEN 11 THEN NonVATableAmount ELSE 0
						END) NonVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VATableAmount WHEN 4 THEN VATableAmount WHEN 5 THEN VATableAmount WHEN 9 THEN VATableAmount WHEN 11 THEN VATableAmount ELSE 0
						END) VATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ZeroRatedSales WHEN 4 THEN ZeroRatedSales WHEN 5 THEN ZeroRatedSales WHEN 9 THEN ZeroRatedSales WHEN 11 THEN ZeroRatedSales ELSE 0
						END) ZeroRatedSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN VAT WHEN 4 THEN VAT WHEN 5 THEN VAT WHEN 9 THEN VAT WHEN 11 THEN VAT ELSE 0
						END) VAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVATableAmount WHEN 4 THEN EVATableAmount WHEN 5 THEN EVATableAmount WHEN 9 THEN EVATableAmount WHEN 11 THEN EVATableAmount ELSE 0
						END) EVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN NonEVATableAmount WHEN 4 THEN NonEVATableAmount WHEN 5 THEN NonEVATableAmount WHEN 9 THEN NonEVATableAmount WHEN 11 THEN NonEVATableAmount ELSE 0
						END) NonEVATableAmount, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN EVAT WHEN 4 THEN EVAT WHEN 5 THEN EVAT WHEN 9 THEN EVAT WHEN 11 THEN EVAT ELSE 0
						END) EVAT, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN LocalTax WHEN 4 THEN LocalTax WHEN 5 THEN LocalTax WHEN 9 THEN LocalTax WHEN 11 THEN LocalTax ELSE 0
						END) LocalTax,
					SUM(CASE TransactionStatus
							WHEN 1 THEN CashPayment WHEN 4 THEN CashPayment WHEN 9 THEN CashPayment WHEN 11 THEN CashPayment ELSE 0
						END) CashSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN ChequePayment WHEN 4 THEN ChequePayment WHEN 9 THEN ChequePayment WHEN 11 THEN ChequePayment ELSE 0
						END) ChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditCardPayment WHEN 4 THEN CreditCardPayment WHEN 9 THEN CreditCardPayment WHEN 11 THEN CreditCardPayment ELSE 0
						END) CreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN CreditPayment WHEN 4 THEN CreditPayment WHEN 9 THEN CreditPayment WHEN 11 THEN CreditPayment ELSE 0
						END) CreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 1 THEN DebitPayment WHEN 4 THEN DebitPayment WHEN 9 THEN DebitPayment WHEN 11 THEN DebitPayment ELSE 0
						END) DebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN CashPayment ELSE 0
						END) RefundCashSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN ChequePayment ELSE 0
						END) RefundChequeSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditCardPayment ELSE 0
						END) RefundCreditCardSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN CreditPayment ELSE 0
						END) RefundCreditSales, -- creditpayment for normal transactions
					SUM(CASE TransactionStatus
							WHEN 5 THEN DebitPayment ELSE 0
						END) RefundDebitPayment, -- debit for normal transactions
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment + ChequePayment + CreditCardPayment + DebitPayment ELSE 0
						END) CreditPayment,
					SUM(CASE TransactionStatus
							WHEN 7 THEN CashPayment ELSE 0
						END) CreditPaymentCash, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN ChequePayment ELSE 0
						END) CreditPaymentCheque, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN CreditCardPayment ELSE 0
						END) CreditPaymentCreditCard, 
					SUM(CASE TransactionStatus
							WHEN 7 THEN DebitPayment ELSE 0
						END) CreditPaymentDebit, 
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardPointsPayment WHEN 4 THEN RewardPointsPayment WHEN 5 THEN RewardPointsPayment WHEN 9 THEN RewardPointsPayment WHEN 11 THEN RewardPointsPayment ELSE 0
						END) RewardPointsPayment,
					SUM(CASE TransactionStatus
							WHEN 1 THEN RewardConvertedPayment WHEN 4 THEN RewardConvertedPayment WHEN 5 THEN RewardConvertedPayment WHEN 9 THEN RewardConvertedPayment WHEN 11 THEN RewardConvertedPayment ELSE 0
						END) RewardConvertedPayment,
					SUM(CASE TransactionStatus
							WHEN 3 THEN Subtotal ELSE 0
						END) VoidSales, 
					SUM(CASE TransactionStatus
							WHEN 5 THEN Subtotal ELSE 0
						END) RefundSales,
					SUM(CASE TransactionStatus
							WHEN 14 THEN Subtotal ELSE 0
						END) WalkInSales,
					SUM(CASE TransactionStatus
							WHEN 15 THEN Subtotal ELSE 0
						END) OutOfStockSales,
					SUM(CASE TransactionStatus
							WHEN 16 THEN Subtotal ELSE 0
						END) ConsignmentSales,
					SUM(CASE TransactionStatus
							WHEN 17 THEN Subtotal ELSE 0
						END) WalkInRefundSales,
					SUM(CASE TransactionStatus
							WHEN 18 THEN Subtotal ELSE 0
						END) OutOfStockRefundSales,
					SUM(CASE TransactionStatus
							WHEN 19 THEN Subtotal ELSE 0
						END) ConsignmentRefundSales
			FROM  tblTransactions
					WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
						AND TransactionStatus NOT IN (0,2) -- remove the open, suspended transactions
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
						AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i:%s') <= DATE_FORMAT(dteNextZReadDate, '%Y-%m-%d %H:%i:%s')
			GROUP BY BranchID, TerminalNo
		) Trx ON tblTerminalReportHistory.BranchID = Trx.BranchID AND tblTerminalReportHistory.TerminalNo = Trx.TerminalNo
		SET					
				-- tblTerminalReportHistory.ActualNewGrandTotal				=  Trx.SubTotal + tblTerminalReportHistory.OldGrandTotal,
				-- tblTerminalReportHistory.NewGrandTotal						=  Trx.SubTotal + tblTerminalReportHistory.OldGrandTotal,
				tblTerminalReportHistory.NetSales							= IFNULL(Trx.NetSales,0), 
				tblTerminalReportHistory.GrossSales							= IFNULL(Trx.GrossSales,0), 
				tblTerminalReportHistory.TotalDiscount						= IFNULL(Trx.Discount,0) + IFNULL(Trx.ItemsDiscount,0), 
				tblTerminalReportHistory.SNRDiscount					  	= IFNULL(Trx.SNRDiscount,0), 
				tblTerminalReportHistory.PWDDiscount					  	= IFNULL(Trx.PWDDiscount,0), 
				tblTerminalReportHistory.OtherDiscount					  	= IFNULL(Trx.OtherDiscount,0),
				tblTerminalReportHistory.TotalCharge						= IFNULL(Trx.TotalCharge,0), 
				tblTerminalReportHistory.DailySales							= IFNULL(Trx.DailySales,0), 
				tblTerminalReportHistory.ItemSold							= IFNULL(Trx.ItemSold,0), 
				tblTerminalReportHistory.QuantitySold						= IFNULL(Trx.QuantitySold,0), 
				tblTerminalReportHistory.GroupSales							= IFNULL(Trx.SubTotal,0), 
				tblTerminalReportHistory.VATExempt   						= IFNULL(Trx.VATExempt,0), 
				tblTerminalReportHistory.NonVATableAmount					= IFNULL(Trx.NonVATableAmount,0), 
				tblTerminalReportHistory.VATableAmount						= IFNULL(Trx.VATableAmount,0), 
				tblTerminalReportHistory.ZeroRatedSales						= IFNULL(Trx.ZeroRatedSales,0), 
				tblTerminalReportHistory.VAT								= IFNULL(Trx.VAT,0), 
				tblTerminalReportHistory.EVATableAmount						= IFNULL(Trx.EVATableAmount,0), 
				tblTerminalReportHistory.NonEVATableAmount					= IFNULL(Trx.NonEVATableAmount,0), 
				tblTerminalReportHistory.EVAT								= IFNULL(Trx.EVAT,0), 
				tblTerminalReportHistory.LocalTax							= IFNULL(Trx.LocalTax,0), 
				tblTerminalReportHistory.CashSales							= IFNULL(Trx.CashSales,0), 
				tblTerminalReportHistory.ChequeSales						= IFNULL(Trx.ChequeSales,0), 
				tblTerminalReportHistory.CreditCardSales					= IFNULL(Trx.CreditCardSales,0), 
				tblTerminalReportHistory.CreditSales						= IFNULL(Trx.CreditSales,0), 
				tblTerminalReportHistory.DebitPayment						= IFNULL(Trx.DebitPayment,0), 
				tblTerminalReportHistory.RefundCash							= IFNULL(Trx.RefundCashSales,0), 
				tblTerminalReportHistory.RefundCheque						= IFNULL(Trx.RefundChequeSales,0), 
				tblTerminalReportHistory.RefundCreditCard					= IFNULL(Trx.RefundCreditCardSales,0), 
				tblTerminalReportHistory.RefundCredit						= IFNULL(Trx.RefundCreditSales,0), 
				tblTerminalReportHistory.RefundDebit						= IFNULL(Trx.RefundDebitPayment,0),
				tblTerminalReportHistory.CreditPayment						= IFNULL(Trx.CreditPayment,0), 
				tblTerminalReportHistory.CreditPaymentCash					= IFNULL(Trx.CreditPaymentCash,0), 
				tblTerminalReportHistory.CreditPaymentCheque				= IFNULL(Trx.CreditPaymentCheque,0), 
				tblTerminalReportHistory.CreditPaymentCreditCard			= IFNULL(Trx.CreditPaymentCreditCard,0), 
				tblTerminalReportHistory.CreditPaymentDebit					= IFNULL(Trx.CreditPaymentDebit,0), 
					
				tblTerminalReportHistory.RewardPointsPayment				= IFNULL(Trx.RewardPointsPayment,0),
				tblTerminalReportHistory.RewardConvertedPayment				= IFNULL(Trx.RewardConvertedPayment,0),
				tblTerminalReportHistory.CashInDrawer						= IFNULL(Trx.CashSales,0) - (-IFNULL(Trx.RefundCashSales,0)) + IFNULL(Trx.CreditPaymentCash,0) + IFNULL(Trx.WalkInSales,0) + IFNULL(Trx.WalkInRefundSales,0) + tblTerminalReportHistory.BeginningBalance + tblTerminalReportHistory.TotalWithHold + tblTerminalReportHistory.TotalDeposit - tblTerminalReportHistory.TotalPaidOut - tblTerminalReportHistory.TotalDisburse, 
				tblTerminalReportHistory.VoidSales							= IFNULL(Trx.VoidSales,0), 
				tblTerminalReportHistory.RefundSales						= IFNULL(Trx.RefundSales,0), 
				tblTerminalReportHistory.ItemsDiscount						= IFNULL(Trx.ItemsDiscount,0), 
				tblTerminalReportHistory.SNRItemsDiscount					= IFNULL(Trx.SNRItemsDiscount,0), 
				tblTerminalReportHistory.PWDItemsDiscount					= IFNULL(Trx.PWDItemsDiscount,0), 
				tblTerminalReportHistory.OtherItemsDiscount					= IFNULL(Trx.OtherItemsDiscount,0), 
				tblTerminalReportHistory.SubTotalDiscount					= IFNULL(Trx.Discount, 0),

				tblTerminalReportHistory.ConsignmentSales					=  IFNULL(Trx.ConsignmentSales,0),
				tblTerminalReportHistory.ConsignmentRefundSales				=  IFNULL(Trx.ConsignmentRefundSales,0),
				tblTerminalReportHistory.WalkInSales						=  IFNULL(Trx.WalkInSales,0),
				tblTerminalReportHistory.WalkInRefundSales					=  IFNULL(Trx.WalkInRefundSales,0),
				tblTerminalReportHistory.OutOfStockSales					=  IFNULL(Trx.OutOfStockSales,0),
				tblTerminalReportHistory.OutOfStockRefundSales				=  IFNULL(Trx.OutOfStockRefundSales,0),

				tblTerminalReportHistory.IsProcessed						=  1	-- this must be set to 0 during salestransaction update
		WHERE tblTerminalReportHistory.BranchID = intBranchID AND tblTerminalReportHistory.TerminalNo = strTerminalNo
			AND DATE_FORMAT(tblTerminalReportHistory.DateLastInitialized, '%Y-%m-%d %H:%i:%s') = DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s');
		
		UPDATE tblTerminalReportHistory,
			(SELECT BranchID, TerminalNo, a.DateLastInitialized, a.NewGrandTotal, a.GrossSales,
				IFNULL((SELECT NewGrandTotal FROM tblTerminalReportHistory b WHERE a.BranchID = b.BranchID AND a.TerminalNo = b.TerminalNo AND b.DateLastInitialized < DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s') ORDER BY b.DateLastInitialized DESC LIMIT 1),0) OldGrandTotal,
				IFNULL((SELECT ActualNewGrandTotal FROM tblTerminalReportHistory b WHERE a.BranchID = b.BranchID AND a.TerminalNo = b.TerminalNo AND b.DateLastInitialized < DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s') ORDER BY b.DateLastInitialized DESC LIMIT 1),0) ActualOldGrandTotal
				FROM tblTerminalReportHistory a 
				WHERE DATE_FORMAT(a.DateLastInitialized, '%Y-%m-%d %H:%i:%s') = DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s')
				) Trx
		SET
			tblTerminalReportHistory.OldGrandTotal						= Trx.OldGrandTotal,
			tblTerminalReportHistory.NewGrandTotal						= Trx.OldGrandTotal + (tblTerminalReportHistory.GrossSales * ((100-TrustFund)/100)),
			
			tblTerminalReportHistory.ActualOldGrandTotal				= Trx.ActualOldGrandTotal,
			tblTerminalReportHistory.ActualNewGrandTotal				= Trx.ActualOldGrandTotal + tblTerminalReportHistory.GrossSales
		WHERE tblTerminalReportHistory.BranchID = Trx.BranchID AND tblTerminalReportHistory.TerminalNo = Trx.TerminalNo 
			AND DATE_FORMAT(tblTerminalReportHistory.DateLastInitialized, '%Y-%m-%d %H:%i:%s') = DATE_FORMAT(Trx.DateLastInitialized, '%Y-%m-%d %H:%i:%s')
			AND tblTerminalReportHistory.BranchID = intBranchID AND tblTerminalReportHistory.TerminalNo = strTerminalNo
			AND DATE_FORMAT(tblTerminalReportHistory.DateLastInitialized, '%Y-%m-%d %H:%i:%s') = DATE_FORMAT(dteActualZReadDate, '%Y-%m-%d %H:%i:%s');

		SET dteActualZReadDate = NULL;
		SET dteNextZReadDate = NULL;

	END LOOP curActualZReadDate;
	CLOSE curActualZReadDate;

	UPDATE tblTerminalReport SET 
		OldGrandTotal = (SELECT NewGrandTotal FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo ORDER BY DateLastInitialized DESC LIMIT 1),
		ActualOldGrandTotal = (SELECT ActualNewGrandTotal FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo ORDER BY DateLastInitialized DESC LIMIT 1)
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo; 

	UPDATE tblTerminalReport SET 
		NewGrandTotal =  OldGrandTotal + (GrossSales * (100-TrustFund)/100),
		ActualNewGrandTotal =  ActualOldGrandTotal + GrossSales
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo; 

	CALL procsysAuditInsert(NOW(), 'RetailPlus Admin', 'RESYNC TERMINAL REPORT HISTORY', 'localhost', CONCAT('TRH has been re-run from ',DATE_FORMAT(dteZReadDateFrom, '%Y-%m-%d %H:%i:%s'),' up to ',DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i:%s'),' @ BranchID:', intBranchID, ' TerminalNo:', strTerminalNo,'.'));

	-- checking
	SELECT 'Please double check the following if correct.';

	SELECT DateLastInitialized, OldGrandTotal, NewGrandTotal, 
								OldGrandTotal + (GrossSales * ((100-TrustFund)/100)) AS CorrectedNewGrandTotal,
								ActualOldGrandTotal, ActualNewGrandTotal, 
								ActualOldGrandTotal + GrossSales AS CorrectedActualNewGrandTotal,
								BeginningTransactionNo, EndingTransactionNo
	FROM tblTerminalReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
		AND DATE_FORMAT(DateLastInitialized, '%Y-%m-%d %H:%i:%s') >= DATE_FORMAT(dteZReadDateFrom, '%Y-%m-%d %H:%i:%s')
	ORDER BY DateLastInitialized DESC; 

	SELECT DateLastInitialized, OldGrandTotal, NewGrandTotal, 
								OldGrandTotal + (GrossSales * ((100-TrustFund)/100)) AS CorrectedNewGrandTotal,
								ActualOldGrandTotal, ActualNewGrandTotal, 
								ActualOldGrandTotal + GrossSales AS CorrectedActualNewGrandTotal,
								BeginningTransactionNo, EndingTransactionNo
	FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo
	ORDER BY DateLastInitialized DESC; 
	
END;
GO
delimiter ;


/*********************************
	procContactChangeCreditCardType
	Lemuel E. Aceron
	CALL procContactChangeCreditCardType();
	
	October 26, 2014 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactChangeCreditCardType
GO

create procedure procContactChangeCreditCardType(
	IN intContactID BIGINT(20),
	IN intCreditCardTypeID INT(10),
	IN strUpdatedBy varchar(150))
BEGIN

	DECLARE intOldCreditCardTypeID INT(1) DEFAULT 0;

	SET intOldCreditCardTypeID = (SELECT CreditCardTypeID FROM tblContactCreditCardInfo WHERE CustomerID = intContactID LIMIT 1);

	-- update the contact only
	UPDATE tblContactCreditCardInfo SET CreditCardTypeID = intCreditCardTypeID WHERE CustomerID = intContactID AND GuarantorID = 0;
	
	-- update all contact with the same guarantor
	UPDATE tblContactCreditCardInfo SET CreditCardTypeID = intCreditCardTypeID WHERE GuarantorID = intContactID;
		
	CALL procsysAuditInsert(NOW(), strUpdatedBy, 'CONTACT CREDITCARD INFO', 'localhost', CONCAT('CreditCardTypeID of customer/guarantor: ',intContactID,' was overwritten from ',intOldCreditCardTypeID,' to ',intCreditCardTypeID,' due to backend update.'));

END;
GO
delimiter ;


/*********************************
	procgetProductSubGroupBarCodeCounter
	Lemuel E. Aceron
	
	Oct 28, 2014 - create this procedure

	CALL procgetProductSubGroupBarCodeCounter(1);

*********************************/
DROP PROCEDURE IF EXISTS procgetProductSubGroupBarCodeCounter;
delimiter GO

CREATE PROCEDURE procgetProductSubGroupBarCodeCounter(IN intProductSubGroupID BIGINT(20))
BEGIN
	DECLARE intBarCodeCounter BIGINT DEFAULT 0;

	SET intBarcodeCounter = (SELECT IFNULL(BarcodeCounter,0) + 1 AS BarcodeCounter FROM tblProductSubGroup WHERE ProductSubGroupID = intProductSubGroupID);

	UPDATE tblProductSubGroup SET BarCodeCounter = intBarcodeCounter WHERE ProductSubGroupID = intProductSubGroupID;

	SELECT intBarcodeCounter AS BarCodeCounter;

END;
GO
delimiter ;

/********************************************
	procSaveMergeTable
	Nov 22, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveMergeTable
GO

create procedure procSaveMergeTable(
	IN strMainTableCode VARCHAR(25),
	IN strChildTableCode VARCHAR(25)
	)
BEGIN
	
	IF NOT EXISTS(SELECT MergeTableID FROM tblMergeTable WHERE MainTableCode = strMainTableCode AND ChildTableCode = strChildTableCode) THEN 
		INSERT INTO tblMergeTable(MainTableCode, ChildTableCode)
			VALUES(strMainTableCode, strChildTableCode);
	END IF;
				
END;
GO
delimiter ;


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

-- remove this for Houseware Plaza	
delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProductInventory_Update
GO

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProductInventory_Insert
GO

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblContacts_Update
GO

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblContacts_Insert
GO

delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProducts_Update
GO

delimiter ;



/**************************************************************

	procRewardsSummarizedStatistics
	Lemuel E. Aceron
	Feb 6, 2015
	
	Desc: This will get the summary of rewards, per status

	CALL procRewardsSummarizedStatistics();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procRewardsSummarizedStatistics
GO

create procedure procRewardsSummarizedStatistics()
BEGIN
	
	SELECT
		RewardActive
		,CASE RewardActive
			WHEN 0 THEN 'InActive'
			WHEN 1 THEN 'Active'
		 END RewardActiveName
		,RewardCardStatus
		,CASE RewardCardStatus 
			WHEN 0 THEN 'New'
			WHEN 1 THEN 'Lost'
			WHEN 2 THEN 'Expired'
			WHEN 3 THEN 'Replaced_Lost'
			WHEN 4 THEN 'Replaced_Expired'
			WHEN 5 THEN 'ReNew'
			WHEN 6 THEN 'Reactivated_Lost'
			WHEN 7 THEN 'ManualDeactivated'
			WHEN 8 THEN 'SystemDeactivated'
			WHEN 9 THEN 'ManualActivated'
			WHEN 10 THEN 'SystemActivated'
			WHEN 11 THEN 'Suspended'
		 END RewardCardStatusName
		,COUNT(crew.CustomerID) NoOfCustomers
		,SUM(RewardPoints) RewardPoints
		,SUM(TotalPurchases) TotalPurchases
		,SUM(RedeemedPoints) RedeemedPoints
	FROM tblContactRewards crew
	GROUP BY 
		RewardActive
		,RewardCardStatus
	ORDER BY RewardActiveName, RewardCardStatusName;

END;
GO
delimiter ;

/**************************************************************

	procIHCreditCardSummarizedStatistics
	Lemuel E. Aceron
	Feb 6, 2015
	
	Desc: This will get the summary of in-house credit card, per status

	CALL procIHCreditCardSummarizedStatistics(0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procIHCreditCardSummarizedStatistics
GO

create procedure procIHCreditCardSummarizedStatistics(
	IN boWithGuarantor TINYINT(1)
)
BEGIN
	
	SELECT
		ctype.CardTypeCode
		,ctype.CardTypeName
		,CASE CreditCardStatus
			WHEN 0 THEN 1
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN 1
			WHEN 4 THEN 1
			WHEN 5 THEN 1
			WHEN 6 THEN 1
			WHEN 7 THEN 0
			WHEN 8 THEN 0
			WHEN 9 THEN 1
			WHEN 10 THEN 1
			WHEN 11 THEN 0
		 END CreditActive
		,CASE CreditCardStatus
			WHEN 0 THEN 'Active'
			WHEN 1 THEN 'InActive'
			WHEN 2 THEN 'InActive'
			WHEN 3 THEN 'Active'
			WHEN 4 THEN 'Active'
			WHEN 5 THEN 'Active'
			WHEN 6 THEN 'Active'
			WHEN 7 THEN 'InActive'
			WHEN 8 THEN 'InActive'
			WHEN 9 THEN 'Active'
			WHEN 10 THEN 'Active'
			WHEN 11 THEN 'InActive'
		 END CreditActiveName
		,CreditCardStatus
		,CASE CreditCardStatus
			WHEN 0 THEN 'New'
			WHEN 1 THEN 'Lost'
			WHEN 2 THEN 'Expired'
			WHEN 3 THEN 'Replaced_Lost'
			WHEN 4 THEN 'Replaced_Expired'
			WHEN 5 THEN 'ReNew'
			WHEN 6 THEN 'Reactivated_Lost'
			WHEN 7 THEN 'ManualDeactivated'
			WHEN 8 THEN 'SystemDeactivated'
			WHEN 9 THEN 'ManualActivated'
			WHEN 10 THEN 'SystemActivated'
			WHEN 11 THEN 'Suspended'
		 END CreditCardStatusName
		,COUNT(cci.CustomerID) NoOfCustomers
		,SUM(cci.TotalPurchases) TotalPurchases
		,SUM(cntct.Credit) Credit
		,SUM(cntct.CreditLimit) CreditLimit
		,SUM(cntct.Debit) Debit
	FROM tblContactCreditCardInfo cci
	INNER JOIN tblCardTypes ctype ON cci.CreditCardTypeID = ctype.CardTypeID
	INNER JOIN tblContacts cntct ON cci.CustomerID = cntct.ContactID
	WHERE ctype.WithGuarantor = boWithGuarantor
	GROUP BY 
		ctype.CardTypeCode
		,ctype.CardTypeName
		,cci.CreditCardStatus
	ORDER BY ctype.CardTypeCode, CreditActiveName, CreditCardStatusName;

END;
GO
delimiter ;


/**************************************************************

	procInventorySummary
	Lemuel E. Aceron
	Feb 6, 2015
	
	Desc: This will get the inventory in summary form

	CALL procInventorySummary(0, 0, 0, 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procInventorySummary
GO

create procedure procInventorySummary(
	IN intInventoryType INT(1),
	IN intBranchID INT(2),
	IN intSupplierID BIGINT(20),
	IN intProductGroupID BIGINT(20)
)
BEGIN
	
	IF (intInventoryType = 2) THEN -- by group
		SELECT 
			BranchCode,
			BranchName,
			'' SupplierName,
			ProductGroupName,
			'' ProductSubGroupName,
			'' ProductCode,
			SUM(inv.Quantity) Quantity,
			MAX(pkg.PurchasePrice) PurchasePrice,
			SUM(inv.Quantity * pkg.PurchasePrice) TotalInventory
		FROM tblProductInventory inv
		INNER JOIN tblBranch brnch ON inv.branchID = brnch.BranchID
		LEFT OUTER JOIN tblProducts prd ON prd.ProductID = inv.ProductID
		LEFT OUTER JOIN tblProductPackage pkg ON pkg.ProductID = prd.ProductID AND prd.BaseUnitID = pkg.UnitID AND pkg.Quantity = 1
		LEFT OUTER JOIN tblProductSubGroup prdsg ON prd.ProductSubGroupID = prdsg.ProductSubGroupID
		LEFT OUTER JOIN tblProductGroup prdg ON prdsg.ProductGroupID = prdg.ProductGroupID
		LEFT OUTER JOIN tblContacts cntct ON cntct.ContactID = prd.SupplierID
		WHERE IF(intBranchID=0,0,inv.BranchID) = intBranchID
			AND IF(intSupplierID=0,0,cntct.ContactID) = intSupplierID
			AND IF(intProductGroupID=0,0,prdg.ProductGroupID) = intProductGroupID
		GROUP BY BranchCode, BranchName, ProductGroupName;
	ELSEIF (intInventoryType = 1) THEN
		SELECT 
			BranchCode,
			BranchName,
			ContactName SupplierName,
			'' ProductGroupName,
			'' ProductSubGroupName,
			'' ProductCode,
			SUM(inv.Quantity) Quantity,
			MAX(pkg.PurchasePrice) PurchasePrice,
			SUM(inv.Quantity * pkg.PurchasePrice) TotalInventory
		FROM tblProductInventory inv
		INNER JOIN tblBranch brnch ON inv.branchID = brnch.BranchID
		LEFT OUTER JOIN tblProducts prd ON prd.ProductID = inv.ProductID
		LEFT OUTER JOIN tblProductPackage pkg ON pkg.ProductID = prd.ProductID AND prd.BaseUnitID = pkg.UnitID AND pkg.Quantity = 1
		LEFT OUTER JOIN tblProductSubGroup prdsg ON prd.ProductSubGroupID = prdsg.ProductSubGroupID
		LEFT OUTER JOIN tblProductGroup prdg ON prdsg.ProductGroupID = prdg.ProductGroupID
		LEFT OUTER JOIN tblContacts cntct ON cntct.ContactID = prd.SupplierID
		WHERE IF(intBranchID=0,0,inv.BranchID) = intBranchID
			AND IF(intSupplierID=0,0,cntct.ContactID) = intSupplierID
			AND IF(intProductGroupID=0,0,prdg.ProductGroupID) = intProductGroupID
		GROUP BY BranchCode, BranchName, ContactName;
	ELSE	-- by branch
		SELECT 
			BranchCode,
			BranchName,
			'' SupplierName,
			'' ProductGroupName,
			'' ProductSubGroupName,
			'' ProductCode,
			SUM(inv.Quantity) Quantity,
			MAX(pkg.PurchasePrice) PurchasePrice,
			SUM(inv.Quantity * pkg.PurchasePrice) TotalInventory
		FROM tblProductInventory inv
		INNER JOIN tblBranch brnch ON inv.branchID = brnch.BranchID
		LEFT OUTER JOIN tblProducts prd ON prd.ProductID = inv.ProductID
		LEFT OUTER JOIN tblProductPackage pkg ON pkg.ProductID = prd.ProductID AND prd.BaseUnitID = pkg.UnitID AND pkg.Quantity = 1
		LEFT OUTER JOIN tblProductSubGroup prdsg ON prd.ProductSubGroupID = prdsg.ProductSubGroupID
		LEFT OUTER JOIN tblProductGroup prdg ON prdsg.ProductGroupID = prdg.ProductGroupID
		LEFT OUTER JOIN tblContacts cntct ON cntct.ContactID = prd.SupplierID
		WHERE IF(intBranchID=0,0,inv.BranchID) = intBranchID
			AND IF(intSupplierID=0,0,cntct.ContactID) = intSupplierID
			AND IF(intProductGroupID=0,0,prdg.ProductGroupID) = intProductGroupID
		GROUP BY BranchCode, BranchName;
	END IF;

END;
GO
delimiter ;



GRANT RELOAD ON *.* TO 'POSUser';
GRANT RELOAD ON *.* TO 'POSAuditUser';

SHOW ENGINE INNODB STATUS\G
