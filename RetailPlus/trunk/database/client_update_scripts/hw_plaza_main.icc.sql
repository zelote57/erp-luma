
DROP TABLE IC_GUA;
CREATE TABLE IC_GUA(
	GURKEY VARCHAR(255),
	GUDESC VARCHAR(255),
	GUADR1 VARCHAR(255),
	GUADR2 VARCHAR(255),
	GUPONE VARCHAR(255),
	GUACCT VARCHAR(255),
	GUTYPE VARCHAR(255),
	GUINST VARCHAR(255),
	GUADON VARCHAR(255),
	GUDISC VARCHAR(255),
	GUPENA VARCHAR(255),
	USERID VARCHAR(255),
	USRDAT VARCHAR(255),
	USRFUN VARCHAR(255),
	STATUS VARCHAR(255)
);
CREATE INDEX IX_IC_GUA ON IC_GUA (GURKEY);
CREATE INDEX IX1_IC_GUA ON IC_GUA (GUDESC);

DROP TABLE IC_ICC;
CREATE TABLE IC_ICC(
	ICRKEY VARCHAR(255),
	ICDESC VARCHAR(255),
	GURKEY VARCHAR(255),
	ICADR1 VARCHAR(255),
	ICADR2 VARCHAR(255),
	ICPONE VARCHAR(255),
	ICLINE VARCHAR(255),
	ICUSED VARCHAR(255),
	ICSTAT VARCHAR(255),
	STATUS VARCHAR(255),
	ICBEGB VARCHAR(255),
	ICPURC VARCHAR(255),
	ICAF15 VARCHAR(255),
	ICAF30 VARCHAR(255),
	ICAF45 VARCHAR(255),
	ICAF60 VARCHAR(255),
	ICCBAL VARCHAR(255),
	ICCHRG VARCHAR(255),
	ICCDUE VARCHAR(255),
	ICPAYM VARCHAR(255),
	ICPAYC VARCHAR(255),
	ICENDB VARCHAR(255),
	ICSALE VARCHAR(255)
);
CREATE INDEX IX_IC_ICC ON IC_ICC (ICRKEY);
CREATE INDEX IX1_IC_ICC ON IC_ICC (ICDESC);
CREATE INDEX IX2_IC_ICC ON IC_ICC (GURKEY);

DROP TABLE IC_ITN;
CREATE TABLE IC_ITN(
	IC_ITN_ID BIGINT(20) NOT NULL AUTO_INCREMENT,
	TRACER VARCHAR(255),
	DIRKEY VARCHAR(255),
	GURKEY VARCHAR(255),
	ICRKEY VARCHAR(255),
	ICDESC VARCHAR(255),
	TRDATE VARCHAR(255),
	TREFNO VARCHAR(255),
	TRAMNT VARCHAR(255),
	STATUS VARCHAR(255),
	PRIMARY KEY (IC_ITN_ID)
);
ALTER TABLE IC_ITN ADD isProcessed TINYINT(1) NOT NULL DEFAULT 0;
CREATE INDEX IX_IC_ITN ON IC_ITN (ICRKEY);
CREATE INDEX IX1_IC_ITN ON IC_ITN (TRDATE);
CREATE INDEX IX2_IC_ITN ON IC_ITN (GURKEY);


DROP TABLE IC_IPY;
CREATE TABLE IC_IPY(
	IC_IPY_ID BIGINT(20) NOT NULL AUTO_INCREMENT,
	TRACER VARCHAR(255),
	GURKEY VARCHAR(255),
	ICRKEY VARCHAR(255),
	ICDESC VARCHAR(255),
	TRDATE VARCHAR(255),
	TREFNO VARCHAR(255),
	TRAMNT VARCHAR(255),
	PAID_P VARCHAR(255),
	STATUS VARCHAR(255),
	PRIMARY KEY (IC_IPY_ID)
);
ALTER TABLE IC_IPY ADD isProcessed TINYINT(1) NOT NULL DEFAULT 0;
CREATE INDEX IX_IC_IPY ON IC_IPY (ICRKEY);
CREATE INDEX IX1_IC_IPY ON IC_IPY (TRDATE);
CREATE INDEX IX2_IC_IPY ON IC_IPY (GURKEY);



/********************************************
	procHPInsertIC
	
	CALL procHPInsertIC;

********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procHPInsertIC
GO

create procedure procHPInsertIC()
BEGIN

	TRUNCATE TABLE tblContactCreditCardInfo;
	

	-- insert the gua
	DELETE FROM tblContactCreditCardInfo WHERE CustomerID IN (SELECT ContactID FROM IC_GUA INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('GUA:',GURKEY)) ;
	DELETE FROM tblContactCreditCardInfo WHERE CustomerID IN (SELECT ContactID FROM IC_GUA INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) ;
	DELETE FROM tblContacts WHERE ContactCode IN (SELECT DISTINCT CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) FROM IC_GUA) AND ContactGroupID = 3;
	DELETE FROM tblContacts WHERE ContactName IN (SELECT DISTINCT GUDESC FROM IC_GUA) AND ContactGroupID = 3;

	INSERT INTO tblContactGroup(ContactGroupID, ContactGroupCode, ContactGroupName, ContactGroupCategory, CreatedOn, LastModified)VALUES(3, 'IC GUA', 'IC Guarantors', 1, NOW(), NOW());
	
	INSERT INTO tblContacts (ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms 
			,Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit 
			,CreditLimit ,IsCreditAllowed ,DateCreated ,DepartmentID ,PositionID)
	SELECT DISTINCT CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) AS ContactCode, GUDESC AS ContactName, 3 ContactGroupID, 0, 0
			,CONCAT(GUADR1,GUADR2) AS Address, CASE 
													WHEN IFNULL(GUADR1,'') <> '' THEN GUADR1 
													ELSE GUADR2 
											   END AS BusinessName, GUPONE AS TelephoneNo, CONCAT('gupena:',GUPENA,' status:',STATUS) AS Remarks, 0, 0
			,0 ,1 ,NOW(), 1, 1 
	FROM IC_GUA
	WHERE CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) NOT IN (SELECT DISTINCT ContactCode FROM tblContacts WHERE ContactGroupID = 3);

	

	INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate) 
	SELECT ContactID AS CustomerID, ContactID GuarantorID, CASE WHEN CONVERT(GUPENA, DECIMAL(18,2)) = 0 THEN 6 -- 'HP SUPERCARD - 30'
															    ELSE 8 -- HP SUPER CARD - 15/30
														   END CreditCardTypeID, 
															CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) CreditCardNo, NOW() CreditAwardDate, 10 SystemActivatedCreditCardStatus, DATE_ADD(NOW(), INTERVAL 2 YEAR) ExpiryDate
	FROM IC_GUA
	INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0))
	WHERE cntct.ContactID NOT IN (SELECT DISTINCT CustomerID FROM tblContactCreditCardInfo);
								  
	-- insert the icc members
	DELETE FROM tblContactCreditCardInfo WHERE CustomerID IN (SELECT ContactID FROM IC_ICC INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('ICC:',ICRKEY)) ;
	DELETE FROM tblContacts WHERE ContactCode IN (SELECT DISTINCT CONCAT('ICC:',ICRKEY) FROM IC_ICC) AND ContactGroupID = 4;

	DELETE FROM tblContactCreditCardInfo WHERE CustomerID IN (SELECT ContactID FROM IC_ICC INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0));
	DELETE FROM tblContacts WHERE ContactCode IN (SELECT DISTINCT CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0) FROM IC_ICC) AND ContactGroupID = 4;
	DELETE FROM tblContacts WHERE ContactName IN (SELECT DISTINCT ICDESC FROM IC_ICC) AND ContactGroupID = 4;
	
	INSERT INTO tblContactGroup(ContactGroupID, ContactGroupCode, ContactGroupName, ContactGroupCategory, CreatedOn, LastModified)VALUES(4, 'IC Members', 'IC Members', 1, NOW(), NOW());

	INSERT INTO tblContacts (ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms 
			,Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit 
			,CreditLimit ,IsCreditAllowed ,DateCreated ,DepartmentID ,PositionID)
	SELECT DISTINCT CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) AS ContactCode, ICDESC AS ContactName, 4 ContactGroupID, 0, 0
			,CONCAT(ICADR1,ICADR2) AS Address, ICLINE AS BusinessName, ICPONE AS TelephoneNo, CONCAT('gurkey:',GURKEY,'; status:',ICSTAT,'; ICBEGB:',ICBEGB,'; ICPURC',ICPURC) AS Remarks, 0, 0
			,ICLINE ,1 ,NOW(), 1, 1 
	FROM IC_ICC
	WHERE CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) NOT IN (SELECT DISTINCT ContactCode FROM tblContacts WHERE ContactGroupID = 4);

	DELETE FROM tblContactCreditCardInfo WHERE CustomerID IN (SELECT ContactID FROM IC_ICC INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0))) ;

	INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate) 
	SELECT cntct.ContactID AS CustomerID, gua.ContactID GuarantorID, 6 CreditCardTypeID, CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) CreditCardNo, NOW() CreditAwardDate, 
							CASE WHEN ICSTAT = 'A' THEN 10
								WHEN ICSTAT = 'C' THEN 1
								WHEN ICSTAT = 'D' THEN 8
								WHEN ICSTAT = 'L' THEN 1
								WHEN ICSTAT = 'S' THEN 11
								ELSE 11
							END CreditCardStatus, DATE_ADD(NOW(), INTERVAL 2 YEAR) ExpiryDate
	-- SELECT IC_ICC.*
	FROM IC_ICC
	INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0))
	INNER JOIN tblContacts gua ON gua.ContactCode = CONCAT('800000', LPAD(IC_ICC.GURKEY,7,0)) 
	WHERE cntct.ContactID NOT IN (SELECT DISTINCT CustomerID FROM tblContactCreditCardInfo);

	-- insert the purchases

	SELECT * FROM IC_ITN LIMIT 10;
	
	DELETE FROM tblCreditPayment WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN');
	DELETE FROM tblCreditCardPayment WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN');
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN');
	DELETE FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN'; 

	DROP TRIGGER trgtblTransactionsCreatedOn;
	INSERT INTO tblTransactions(TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, BranchID, BranchCode, TransactionDate,
								DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType,
								VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
								CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, 
								DiscountCode, DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, 
								ChargeRemarks, WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName,
								AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment,
								RewardConvertedPayment, PaxNo, CreditChargeAmount, TransactionType, isConsignment,
								DataSource, CustomerGroupName, CreatedOn, ORNo, ZeroRatedVAT, NonVATableAmount, VATExempt,
								NonEVATableAmount, SNRDiscount, PWDDiscount, OtherDiscount, NetSales, ChargeType, ItemSold, QuantitySold, 
								ContactCheckInDate, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, GrossSales)
	SELECT LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') TransactionNo, cntct.ContactID, cntct.ContactName, 1 CashierID, 'ICC SysUser' CashierName, '9999' TerminalNo, 1 BranchID, 'Main' BranchCode, STR_TO_DATE(TRDATE, '%d/%m/%Y') TransactionDate,
								'1900-01-01' DateSuspended, '1900-01-01' DateResumed, 1 TransactionStatus, CONVERT(TRAMNT, DECIMAL(18,3)) SubTotal, 0 Discount, 0 TransDiscount, 0 TransDiscountType,
								0 VAT, 0 VatableAmount, 0 EVAT, 0 EVatableAmount, 0 LocalTax, CONVERT(TRAMNT, DECIMAL(18,3)) AmountPaid, 0 CashPayment, 0 ChequePayment, 
								CONVERT(TRAMNT, DECIMAL(18,3)) CreditCardPayment, 0 CreditPayment, 0 BalanceAmount, 0 ChangeAmount, STR_TO_DATE(TRDATE, '%d/%m/%Y') DateClosed, 2 PaymentType, 
								'' DiscountCode, '' DiscountRemarks, 0 DebitPayment, 0 ItemsDiscount, 0 Charge, 0 ChargeAmount, '' ChargeCode, 
								'' ChargeRemarks, 2 WaiterID, 'RetailPlus Default' WaiterName, 0 Packed, 0 OrderType, 1 AgentID, 'RetailPlus Agent ™' AgentName, 1 CreatedByID, 'ICC SysUser' CreatedByName,
								'System Default Department' AgentDepartmentName, 'System Default Position' AgentPositionName, 0 ReleaserID, '' ReleaserName, '1900-01-01' ReleasedDate, 0 RewardPointsPayment,
								0 RewardConvertedPayment, 1 PaxNo, 0 CreditChargeAmount, 0 TransactionType, 0 isConsignment,
								'IC_ITN' DataSource, 'IC Members' CustomerGroupName, STR_TO_DATE(TRDATE, '%d/%m/%Y') CreatedOn, LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') ORNo, 0 ZeroRatedVAT, 0 NonVATableAmount, 0 VATExempt,
								0 NonEVATableAmount, 0 SNRDiscount, 0 PWDDiscount, 0 OtherDiscount, CONVERT(TRAMNT, DECIMAL(18,3)) NetSales, 0 ChargeType, 1 ItemSold, 1 QuantitySold, 
								STR_TO_DATE(TRDATE, '%d/%m/%Y') ContactCheckInDate, 0 SNRItemsDiscount, 0 PWDItemsDiscount, 0 OtherItemsDiscount, CONVERT(TRAMNT, DECIMAL(18,3)) GrossSales
	FROM IC_ITN 
	INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_ITN.ICRKEY,7,0));
	-- WHERE LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') NOT IN (SELECT TransactionNo FROM tblTransactions WHERE TerminalNo = '9999');

	CREATE TRIGGER trgtblTransactionsCreatedOn BEFORE INSERT ON tblTransactions FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

	UPDATE tblTransactions SET SyncID = TransactionID WHERE SyncID = 0;

	DROP TRIGGER trgtblTransactionItemsCreatedOn;
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN');
	INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode,
									Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, 
									VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription,
									ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID,
									MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied,
									PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted,
									PercentageCommision, Commision, PaxNo, TransactionDiscount, DataSource, CreatedOn, ZeroRatedVAT,
									NonVATableAmount, VATExempt, NonEVATableAmount, IsCreditChargeExcluded, GrossSales)
	SELECT TransactionID, prd.ProductID, prd.ProductCode, '99019' Barcode, CONCAT('ICC purchase tracer:',TRACER,' trefno:',TREFNO) Description, 1, 'PC',
									1 Quantity, CONVERT(TRAMNT, DECIMAL(18,3)) Price, CONVERT(TRAMNT, DECIMAL(18,3)) SellingPrice, 0 Discount, 0 ItemDiscount, 0 ItemDiscountType, CONVERT(TRAMNT, DECIMAL(18,3)) Amount, 
									0 VAT, 0 VatableAmount, 0 EVAT, 0 EVatableAmount, 0 LocalTax, 0 VariationsMatrixID, '' MatrixDescription,
									'SYSTEM DEFAULT' ProductGroup, 'SYSTEM DEFAULT' ProductSubGroup, 0 TransactionItemStatus, '' DiscountCode, '' DiscountRemarks, pkg.PackageID ProductPackageID,
									0 MatrixPackageID, 1 PackageQuantity, 0 PromoQuantity, 0 PromoValue, 0 PromoInPercent, 0 PromoType, 0 PromoApplied,
									0 PurchasePrice, 0 PurchaseAmount, 0 IncludeInSubtotalDiscount, 0 OrderSlipPrinter, 0 OrderSlipPrinted,
									0 PercentageCommision, 0 Commision, 0 PaxNo, 0 TransactionDiscount, 'IC_ITN' DataSource, STR_TO_DATE(TRDATE, '%d/%m/%Y') CreatedOn, 0 ZeroRatedVAT,
									0 NonVATableAmount, 0 VATExempt, 0 NonEVATableAmount, 0 IsCreditChargeExcluded, CONVERT(TRAMNT, DECIMAL(18,3)) GrossSales
	-- SELECT count(*)
	FROM IC_ITN 
	INNER JOIN tblTransactions trx ON trx.TransactionNo = LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') AND trx.TerminalNo = '9999' AND DataSource = 'IC_ITN'
	INNER JOIN tblProducts prd ON prd.ProductCode = 'IC IMPORTED TRX'
	INNER JOIN tblProductPackage pkg ON prd.ProductID = pkg.ProductID;
	UPDATE tblTransactionItems SET SyncID = TransactionItemsID WHERE SyncID = 0;

	INSERT INTO tblCreditCardPayment(TransactionID, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, 
									Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID, AdditionalCharge, 
									ContactID, GuarantorID, TransactionDate, CashierName)
	SELECT trx.TransactionID, CONVERT(TRAMNT, DECIMAL(18,3)) Amount, CreditCardTypeID CardTypeID, ctypes.CardTypeCode, ctypes.CardTypeName, cci.CreditCardNo, trx.CustomerName CardHolder, 
									STR_TO_DATE(TRDATE, '%d/%m/%Y') ValidityDates, '' Remarks, trx.TransactionNo, trx.TransactionDate CreatedOn, trx.TerminalNo, trx.BranchID, 
									0 AdditionalCharge, trx.CustomerID ContactID, cci.GuarantorID, trx.TransactionDate, trx.CashierName
	-- SELECT count(*)
	FROM IC_ITN 
	INNER JOIN tblTransactions trx ON trx.TransactionNo = LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') AND trx.TerminalNo = '9999' AND DataSource = 'IC_ITN'
	INNER JOIN tblContactCreditCardInfo cci ON trx.CustomerID = cci.CustomerID 
	INNER JOIN tblCardTypes ctypes ON ctypes.CardTypeID = cci.CreditCardTypeID;
	CREATE TRIGGER trgtblTransactionItemsCreatedOn BEFORE INSERT ON tblTransactionItems FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

	UPDATE tblCreditCardPayment SET SyncID = CreditCardPaymentID WHERE SyncID = 0;

	DELETE FROM tblCreditPayment WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_ITN');
	INSERT INTO tblCreditPayment(TransactionID, Amount, ContactID, Remarks, AmountPaid,
									TransactionNo, CreditDate, CreditBefore, CreditAfter,
									CreditReason, TerminalNo, CashierName, AmountPaidCuttOffMonth, 
									CreatedOn, BranchID, CreditCardPaymentID, CreditCardTypeID, CreditReasonID)
	SELECT trx.TransactionID, CONVERT(TRAMNT, DECIMAL(18,3)) Amount, trx.CustomerID ContactID, CONCAT('ICC payment tracer:',TRACER,' trefno:',TREFNO) Remarks, CASE WHEN IFNULL(Status,'') = 'P' THEN CONVERT(TRAMNT, DECIMAL(18,3)) ELSE 0 END AmountPaid, 
									trx.TransactionNo, trx.TransactionDate CreditDate, 0 CreditBefore, CONVERT(TRAMNT, DECIMAL(18,3)) CreditAfter, 
									CONCAT('ICC purchase tracer:',TRACER,' trefno:',TREFNO) CreditReason, trx.TerminalNo, trx.CashierName, 0 AmountPaidCuttOffMonth,
									trx.TransactionDate CreatedOn, 1 BranchID, ccp.CreditCardPaymentID, ccp.CardTypeID CreditCardTypeID, 0 CreditReasonID
	-- SELECT count(*)
	FROM IC_ITN 
	INNER JOIN tblTransactions trx ON trx.TransactionNo = LPAD(CONCAT(IC_ITN_ID,TRACER),14,'0') AND trx.TerminalNo = '9999' AND DataSource = 'IC_ITN'
	INNER JOIN tblCreditCardPayment ccp ON trx.TransactionID = ccp.TransactionID;
	UPDATE tblCreditPayment SET SyncID = CreditPaymentID WHERE SyncID = 0;


	DELETE FROM tblCreditPaymentCash WHERE TerminalNo = '9999' AND TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND TransactionStatus = 7 AND DataSource = 'IC_IPY');
	DELETE FROM tblCashPayment WHERE TerminalNo = '9999' AND TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND TransactionStatus = 7 AND DataSource = 'IC_IPY');
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND TransactionStatus = 7 AND DataSource = 'IC_IPY');
	DELETE FROM tblTransactions WHERE TerminalNo = '9999' AND TransactionStatus = 7 AND DataSource = 'IC_IPY';

	DROP TRIGGER trgtblTransactionsCreatedOn;
	INSERT INTO tblTransactions(TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, BranchID, BranchCode, TransactionDate,
								DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType,
								VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
								CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, 
								DiscountCode, DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, 
								ChargeRemarks, WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName,
								AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment,
								RewardConvertedPayment, PaxNo, CreditChargeAmount, TransactionType, isConsignment,
								DataSource, CustomerGroupName, CreatedOn, ORNo, ZeroRatedVAT, NonVATableAmount, VATExempt,
								NonEVATableAmount, SNRDiscount, PWDDiscount, OtherDiscount, NetSales, ChargeType, ItemSold, QuantitySold, 
								ContactCheckInDate, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, GrossSales)
	SELECT LPAD(CONCAT(IC_IPY_ID,TRACER),14,'0') TransactionNo, cntct.ContactID CustomerID, cntct.ContactName CustomerName, 1 CashierID, 'ICC SysUser' CashierName, '9999' TerminalNo, 1 BranchID, 'Main' BranchCode, STR_TO_DATE(TRDATE, '%d/%m/%Y') TransactionDate,
								'1900-01-01' DateSuspended, '1900-01-01' DateResumed, 7 TransactionStatus, CONVERT(TRAMNT, DECIMAL(18,3)) SubTotal, 0 Discount, 0 TransDiscount, 0 TransDiscountType,
								0 VAT, 0 VatableAmount, 0 EVAT, 0 EVatableAmount, 0 LocalTax, CONVERT(TRAMNT, DECIMAL(18,3)) AmountPaid, CONVERT(TRAMNT, DECIMAL(18,3)) CashPayment, 0 ChequePayment, 
								0 CreditCardPayment, 0 CreditPayment, 0 BalanceAmount, 0 ChangeAmount, STR_TO_DATE(TRDATE, '%d/%m/%Y') DateClosed, 4 PaymentType, 
								'' DiscountCode, '' DiscountRemarks, 0 DebitPayment, 0 ItemsDiscount, 0 Charge, 0 ChargeAmount, '' ChargeCode, 
								'' ChargeRemarks, 2 WaiterID, 'RetailPlus Default' WaiterName, 0 Packed, 0 OrderType, 1 AgentID, 'RetailPlus Agent ™' AgentName, 1 CreatedByID, 'ICC SysUser' CreatedByName,
								'System Default Department' AgentDepartmentName, 'System Default Position' AgentPositionName, 0 ReleaserID, '' ReleaserName, '1900-01-01' ReleasedDate, 0 RewardPointsPayment,
								0 RewardConvertedPayment, 1 PaxNo, 0 CreditChargeAmount, 0 TransactionType, 0 isConsignment,
								'IC_IPY' DataSource, 'IC Members' CustomerGroupName, STR_TO_DATE(TRDATE, '%d/%m/%Y') CreatedOn, LPAD(CONCAT(IC_IPY_ID,TRACER),14,'0') ORNo, 0 ZeroRatedVAT, 0 NonVATableAmount, 0 VATExempt,
								0 NonEVATableAmount, 0 SNRDiscount, 0 PWDDiscount, 0 OtherDiscount, CONVERT(TRAMNT, DECIMAL(18,3)) NetSales, 0 ChargeType, 1 ItemSold, 1 QuantitySold, 
								STR_TO_DATE(TRDATE, '%d/%m/%Y') ContactCheckInDate, 0 SNRItemsDiscount, 0 PWDItemsDiscount, 0 OtherItemsDiscount, CONVERT(TRAMNT, DECIMAL(18,3)) GrossSales
	FROM IC_IPY
	INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_IPY.ICRKEY,7,0))
	WHERE INSTR(IC_IPY.TRDATE,'/') > 0 AND STR_TO_DATE(IC_IPY.TRDATE, '%d/%m/%Y') >= '2014-01-01';
	-- WHERE LPAD(CONCAT(IC_IPY_ID,TRACER),14,'0') NOT IN (SELECT TransactionNo FROM tblTransactions WHERE TerminalNo = '9999');

	CREATE TRIGGER trgtblTransactionsCreatedOn BEFORE INSERT ON tblTransactions FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

	UPDATE tblTransactions SET SyncID = TransactionID WHERE SyncID = 0;
	
	DROP TRIGGER trgtblTransactionItemsCreatedOn;
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND DataSource = 'IC_IPY');
	INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode,
									Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, 
									VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, VariationsMatrixID, MatrixDescription,
									ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID,
									MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied,
									PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, OrderSlipPrinter, OrderSlipPrinted,
									PercentageCommision, Commision, PaxNo, TransactionDiscount, DataSource, CreatedOn, ZeroRatedVAT,
									NonVATableAmount, VATExempt, NonEVATableAmount, IsCreditChargeExcluded, GrossSales)
	SELECT TransactionID, prd.ProductID, prd.ProductCode, 'CREDIT PAYMENT' Barcode, CONCAT('ICC payment tracer:',TRACER,' trefno:',TREFNO) Description, 1, 'PC',
									1 Quantity, CONVERT(TRAMNT, DECIMAL(18,3)) Price, CONVERT(TRAMNT, DECIMAL(18,3)) SellingPrice, 0 Discount, 0 ItemDiscount, 0 ItemDiscountType, CONVERT(TRAMNT, DECIMAL(18,3)) Amount, 
									0 VAT, 0 VatableAmount, 0 EVAT, 0 EVatableAmount, 0 LocalTax, 0 VariationsMatrixID, '' MatrixDescription,
									'SYSTEM DEFAULT' ProductGroup, 'SYSTEM DEFAULT' ProductSubGroup, 0 TransactionItemStatus, '' DiscountCode, '' DiscountRemarks, pkg.PackageID ProductPackageID,
									0 MatrixPackageID, 1 PackageQuantity, 0 PromoQuantity, 0 PromoValue, 0 PromoInPercent, 0 PromoType, 0 PromoApplied,
									0 PurchasePrice, 0 PurchaseAmount, 0 IncludeInSubtotalDiscount, 0 OrderSlipPrinter, 0 OrderSlipPrinted,
									0 PercentageCommision, 0 Commision, 0 PaxNo, 0 TransactionDiscount, 'IC_IPY' DataSource, STR_TO_DATE(TRDATE, '%d/%m/%Y') CreatedOn, 0 ZeroRatedVAT,
									0 NonVATableAmount, 0 VATExempt, 0 NonEVATableAmount, 0 IsCreditChargeExcluded, CONVERT(TRAMNT, DECIMAL(18,3)) GrossSales
	-- SELECT count(*)
	FROM IC_IPY
	INNER JOIN tblTransactions trx ON trx.TransactionNo = LPAD(CONCAT(IC_IPY_ID,TRACER),14,'0') AND trx.TerminalNo = '9999' AND DataSource = 'IC_IPY'
	INNER JOIN tblProducts prd ON prd.ProductCode = 'CREDIT PAYMENT'
	INNER JOIN tblProductPackage pkg ON prd.ProductID = pkg.ProductID;
	
	CREATE TRIGGER trgtblTransactionItemsCreatedOn BEFORE INSERT ON tblTransactionItems FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

	UPDATE tblTransactionItems SET SyncID = TransactionItemsID WHERE SyncID = 0;

	DELETE FROM tblCashPayment WHERE TerminalNo = '9999' AND TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '9999' AND TransactionStatus = 7 AND DataSource = 'IC_IPY');

	INSERT INTO tblCashPayment(TransactionID, Amount, Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID)
	SELECT trx.TransactionID, CONVERT(TRAMNT, DECIMAL(18,3)) Amount, CONCAT('ICC payment tracer:',TRACER,' trefno:',TREFNO) Remarks, trx.TransactionNo, trx.TransactionDate CreatedOn, trx.TerminalNo, trx.BranchID
	FROM IC_IPY 
	INNER JOIN tblTransactions trx ON trx.TransactionNo = LPAD(CONCAT(IC_IPY_ID,TRACER),14,'0') AND trx.TerminalNo = '9999';

	UPDATE tblCashPayment SET SyncID = CashPaymentID WHERE SyncID = 0;

	CALL procSyncContactCredit();


	SELECT STR_TO_DATE(TRDATE, '%d/%m/%Y'), COUNT(*) FROM IC_ipy
	GROUP BY STR_TO_DATE(TRDATE, '%d/%m/%Y')
	ORDER BY STR_TO_DATE(TRDATE, '%d/%m/%Y');

END;
GO
delimiter ;

DELETE FROM tblCashPayment WHERE TransactionID = (SELECT TransactionID FROM tblTransactions WHERE TransactionNo = '00034470238027');
DELETE FROM tblTransactionItems WHERE TransactionID = (SELECT TransactionID FROM tblTransactions WHERE TransactionNo = '00034470238027');
DELETE FROM tblTransactions WHERE TransactionNo = '00034470238027';

DELETE FROM tblCreditPaymentCash WHERE TransactionID = (SELECT TransactionID FROM tblTransactions WHERE TransactionNo = '00000000001379');
DELETE FROM tblCashPayment WHERE TransactionID = (SELECT TransactionID FROM tblTransactions WHERE TransactionNo = '00000000001379');
DELETE FROM tblTransactionItems WHERE TransactionID = (SELECT TransactionID FROM tblTransactions WHERE TransactionNo = '00000000001379');
DELETE FROM tblTransactions WHERE TransactionNo = '00000000001379';

-- creditajd.exe 8000001038356   0.05
