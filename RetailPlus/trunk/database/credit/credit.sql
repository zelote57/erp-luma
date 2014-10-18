
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

DELETE FROM sysAccessRights WHERE TranTypeID = 145; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 145;
DELETE FROM sysAccessTypes WHERE TypeID = 145;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (145, 'Credit Card Issuance');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 145, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 145, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '07: Backend - Credits' WHERE TypeID = 145;

DELETE FROM sysAccessRights WHERE TranTypeID = 146; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 146;
DELETE FROM sysAccessTypes WHERE TypeID = 146;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (146, 'Credit Card Replacement');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 146, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 146, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '07: Backend - Credits' WHERE TypeID = 146;

DELETE FROM sysAccessRights WHERE TranTypeID = 147; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 147;
DELETE FROM sysAccessTypes WHERE TypeID = 147;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (147, 'Customer Management Feature');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 147, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 147, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '08: Backend - Customer Rewards' WHERE TypeID = 147;


DELETE FROM sysAccessRights WHERE TranTypeID = 153; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 153;
DELETE FROM sysAccessTypes WHERE TypeID = 153;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (153, 'Internal Credit Card Setup', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '01: System Configurations' WHERE TypeID = 153;

DELETE FROM sysAccessRights WHERE TranTypeID = 153; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 153;
DELETE FROM sysAccessTypes WHERE TypeID = 153;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (153, 'Internal Credit Card Setup', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 153, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 153;

DELETE FROM sysAccessRights WHERE TranTypeID = 154; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 154;
DELETE FROM sysAccessTypes WHERE TypeID = 154;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (154, 'Creditors Without Guarantor Setup', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 154, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 154, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 154;

DELETE FROM sysAccessRights WHERE TranTypeID = 155; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 155;
DELETE FROM sysAccessTypes WHERE TypeID = 155;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (155, 'Creditors Without Guarantor Purchases', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 155, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 155, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 155;

DELETE FROM sysAccessRights WHERE TranTypeID = 156; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 156;
DELETE FROM sysAccessTypes WHERE TypeID = 156;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (156, 'Creditors Without Guarantor Payments', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 156, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 156, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 156;

DELETE FROM sysAccessRights WHERE TranTypeID = 161; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 161;
DELETE FROM sysAccessTypes WHERE TypeID = 161;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (161, 'Creditors With Guarantor Setup', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 161, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 161, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 161;

DELETE FROM sysAccessRights WHERE TranTypeID = 162; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 162;
DELETE FROM sysAccessTypes WHERE TypeID = 162;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (162, 'Creditors With Guarantor Purchases', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 162, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 162, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 162;

DELETE FROM sysAccessRights WHERE TranTypeID = 163; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 163;
DELETE FROM sysAccessTypes WHERE TypeID = 163;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (163, 'Creditors With Guarantor Payments', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 163, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 163, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 163;

DELETE FROM sysAccessRights WHERE TranTypeID = 164; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 164;
DELETE FROM sysAccessTypes WHERE TypeID = 164;
INSERT INTO sysAccessTypes (TypeID, TypeName, Enabled) VALUES (164, 'Guarantor''s Ledger Summary', 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 164, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 164, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Credits' WHERE TypeID = 164;


TRUNCATE TABLE sysCreditConfig;
INSERT INTO sysCreditConfig (ConfigName, ConfigValue, Remarks) VALUES ('IndividualCardTypeCode',	'HP CREDIT CARD',			'Individual Credit Card Name for HP');
INSERT INTO sysCreditConfig (ConfigName, ConfigValue, Remarks) VALUES ('GroupCardTypeCode',		    'HP SUPER CARD',			'Major Credit Card Name for HP');

ALTER TABLE tblCreditBillDetail ADD TerminalNoRefID VARCHAR(30);
ALTER TABLE tblCreditBillDetail ADD BranchIDRefID INT(4);



-- disable all credits access
-- UPDATE sysAccessTypes SET enabled = 0 WHERE Category = '07: Backend - Credits';
