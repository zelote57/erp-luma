
ALTER TABLE tblContactCreditCardInfo ADD LastBillingDate DATE NOT NULL DEFAULT '0001-01-1';

DROP TABLE IF EXISTS sysCreditConfig;
CREATE TABLE sysCreditConfig (
	`ConfigName` VARCHAR(30) NOT NULL,
	`ConfigValue` VARCHAR(100) NOT NULL,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (ConfigName),
	INDEX `IX_sysCreditConfig`(`ConfigName`),
	UNIQUE `PK_sysCreditConfig`(`ConfigName`)
);

INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditPurcStartDateToProcess',	'2012-06-10');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditPurcEndDateToProcess',		'2012-07-09');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditCutOffDate',				'2012-06-28');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditUseLastDayCutOffDate',		'1');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditFinanceCharge',			'0.030');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditMinimumPercentageDue',		'0.100');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditMinimumAmountDue',			'500.00');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('CreditLatePenaltyCharge',		'0.050');


/*****************************
**	tblCreditBills
*****************************/
DROP TABLE IF EXISTS tblCreditBills;
CREATE TABLE tblCreditBills (
	`CreditBillID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditPurcStartDateToProcess` DATE NOT NULL,
	`CreditPurcEndDateToProcess` DATE NOT NULL,
	`CreditCutOffDate` DATE NOT NULL,
	`CreditFinanceCharge` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CreditMinimumPercentageDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CreditMinimumAmountDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CreditLatePenaltyCharge` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CreatedOn` DATETIME NOT NULL,
	`CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`CreatedByName` VARCHAR(100),
	PRIMARY KEY (CreditBillID),
	INDEX `IX_tblCreditBills`(`CreditBillID`),
	UNIQUE `PK_tblCreditBills`(`CreditBillID`)
);

/*****************************
**	tblCreditBillHeader
*****************************/
DROP TABLE IF EXISTS tblCreditBillHeader;
CREATE TABLE tblCreditBillHeader (
	`CreditBillHeaderID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditBillID` BIGINT NOT NULL DEFAULT 0 REFERENCES tblCreditBills(`CreditBillID`),
	`ContactID` BIGINT NOT NULL DEFAULT 0,
	`CreditLimit` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`RunningCreditAmt` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CurrMonthCreditAmt` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CurrMonthAmountPaid` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`TotalBillCharges` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`MinimumAmountDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoCurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoMinimumAmountDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoCurrMonthAmountPaid` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev2MoCurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`BillingDate` DATE NOT NULL,
	`CreatedOn` DATETIME NOT NULL,
	`CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`CreatedByName` VARCHAR(100) NOT NULL,
	`IsBillPrinted` TINYINT(1) NOT NULL DEFAULT 0,
	PRIMARY KEY (CreditBillHeaderID),
	INDEX `IX_tblCreditBillHeader`(`CreditBillHeaderID`),
	UNIQUE `PK_tblCreditBillHeader`(`CreditBillHeaderID`)
);

DROP TABLE IF EXISTS tblCreditBillDetail;
CREATE TABLE tblCreditBillDetail (
	`CreditBillDetailID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditBillHeaderID` BIGINT NOT NULL DEFAULT 0 REFERENCES tblCreditBillHeader(`CreditBillHeaderID`),
	`TransactionDate` DATETIME NOT NULL,
	`Description` VARCHAR(120),
	`Amount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`TransactionTypeID` TINYINT(1) NOT NULL DEFAULT 0,
	`TransactionRefID` BIGINT(20) NOT NULL DEFAULT 0,
	PRIMARY KEY (CreditBillDetailID),
	INDEX `IX_tblCreditBillDetail`(`CreditBillDetailID`),
	UNIQUE `PK_tblCreditBillDetail`(`CreditBillDetailID`)
);

/**


		SELECT 
			1 CreditBillID,
			tblContacts.ContactID,
			CreditLimit,
			CurrMonthCreditAmt,
			CurrMonthAmountPaid,
			0 TotalBillCharges,
			(CurrMonthCreditAmt - CurrMonthAmountPaid + TotalBillCharges) CurrentDueAmount,
			LastBillingDate,
			now(),
			1 CreatedByID,
			'Lemuel' CreatedByName,
			
			
			(IFNULL(Amount60Days,0) - IFNULL(AmountPaid60Days,0) + IFNULL(AmountPaidCuttOffMonth60Days,0)) CreditAmount60Days,
			(IFNULL(Amount30Days,0) - IFNULL(AmountPaid60Days,0) + IFNULL(AmountPaidCuttOffMonth30Days,0)) CreditAmount30Days,
			IFNULL(CurrentDueAmount,0) CurrentDueAmount,
			CurrentAmountPaid + IFNULL(AmountPaidCuttOffMonth30Days,0) + IFNULL(AmountPaidCuttOffMonth60Days,0) TotalAmountPaid,
			IFNULL(CreditFinanceCharge,0) CreditFinanceCharge,
			(((IFNULL(Amount60Days,0) - IFNULL(AmountPaid60Days,0) + IFNULL(AmountPaidCuttOffMonth60Days,0)) + 
			  (IFNULL(Amount30Days,0) - IFNULL(AmountPaid60Days,0) + IFNULL(AmountPaidCuttOffMonth30Days,0))) * IFNULL(CreditFinanceCharge,0)) CreditFinanceChargeAmount,
			'End'
			-- (IFNULL(AmountCurrentDue,0) - IFNULL(AmountPaidCurrentDue,0)) CurrentDueAmount,
			-- IFNULL(Amount30Days,0) Amount30Days,
			-- IFNULL(AmountPaid30Days,0) AmountPaid30Days,
			-- IFNULL(Amount60Days,0) Amount60Days,
			-- IFNULL(AmountPaid60Days,0) AmountPaid60Days,
			-- (IFNULL(Amount60Days,0) - IFNULL(AmountPaid60Days,0) + IFNULL(AmountPaidCuttOffMonth60Days,0)) CreditAmount60Days,
			-- IFNULL(TrxAmountPaid,0) TrxAmountPaid
		FROM
			(SELECT 
				CUS.ContactID,
				CUS.Credit,
				CUS.CreditLimit,
				CCI.LastBillingDate
			FROM tblContacts CUS
			INNER JOIN tblContactCreditCardInfo CCI ON CUS.ContactID = CCI.CustomerID
			WHERE Credit > 0 
				AND LastBillingDate <= (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditBillLastProcessDate'))
			tblContacts
		LEFT OUTER JOIN
			(SELECT 
				ContactID,
				SUM(Amount) CurrMonthCreditAmt,
				SUM(AmountPaid) CurrentAmountPaid
			FROM tblCreditPayment
			WHERE CreditDate > (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditPurcEndDateToProcess')
			GROUP BY ContactID) 
			CurrentDue ON CurrentDue.ContactID = tblContacts.ContactID
		LEFT OUTER JOIN
			(SELECT 
				ContactID,
				SUM(Amount) Amount30Days,
				SUM(AmountPaid) AmountPaid30Days,
				SUM(AmountPaidCuttOffMonth) AmountPaidCuttOffMonth30Days
			FROM tblCreditPayment
			WHERE CreditDate BETWEEN (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditPurcStartDateToProcess')
								 AND (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditPurcEndDateToProcess')
			GROUP BY ContactID) 
			Credit30Days ON Credit30Days.ContactID = tblContacts.ContactID
		LEFT OUTER JOIN 
			(SELECT 
				ContactID,
				SUM(Amount) Amount60Days,
				SUM(AmountPaid) AmountPaid60Days,
				SUM(AmountPaidCuttOffMonth) AmountPaidCuttOffMonth60Days
			FROM tblCreditPayment
			WHERE CreditDate < (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditPurcStartDateToProcess')
			GROUP BY ContactID) 
			Credit60Days ON Credit60Days.ContactID = tblContacts.ContactID
		LEFT OUTER JOIN
			(SELECT 
				ConfigValue CreditFinanceCharge
			FROM sysCreditConfig
			WHERE ConfigName = 'CreditFinanceCharge')
			tblCreditFinanceCharge ON 1=1
--        LEFT OUTER JOIN 
--            (SELECT 
--                CustomerID,
--                IFNULL(SUM(Subtotal),0) TrxAmountPaid
--            FROM tblTransactions trx
--                INNER JOIN tblTransactionItems trxItem ON trx.TransactionID = trxItem.TransactionID 
--            WHERE TransactionDate >= (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditCutOffDate')
--                AND ProductID = 1 -- Means credit payment
--                AND TransactionStatus = 1 -- Means closed or paid
--            GROUP BY CustomerID) 
--            CreditPayments ON tblContacts.ContactID = CreditPayments.CustomerID;

** /
