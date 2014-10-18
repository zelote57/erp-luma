
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

/*****************************
**	tblCreditBills
*****************************/
DROP TABLE IF EXISTS tblCreditBills;
CREATE TABLE tblCreditBills (
	`CreditBillID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditPurcStartDateToProcess` DATE NOT NULL,
	`CreditPurcEndDateToProcess` DATE NOT NULL,
	`BillingDate` DATE NOT NULL,
	`CreditCutOffDate` DATE NOT NULL,
	`CreditPaymentDueDate` DATE NOT NULL,
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
	`BillingDate` DATE NOT NULL,
	`BillingFile` VARCHAR(120),
	`TotalBillCharges` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`CurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`MinimumAmountDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoCurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoMinimumAmountDue` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev1MoCurrMonthAmountPaid` DECIMAL(10,3) NOT NULL DEFAULT 0,
	`Prev2MoCurrentDueAmount` DECIMAL(10,3) NOT NULL DEFAULT 0,
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

DELETE FROM sysAccessRights WHERE TranTypeID = 153; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 153;
DELETE FROM sysAccessTypes WHERE TypeID = 153;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (153, 'Internal Credit Card Setup', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '01: System Configurations' WHERE TypeID = 153;


INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('IndividualCardTypeCode',		'HP CARD');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue) VALUES ('GroupCardTypeCode',		    'HP SUPERCARD');
