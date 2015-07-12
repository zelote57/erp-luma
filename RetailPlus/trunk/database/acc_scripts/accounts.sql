 /******************************************************************************
**		File: accounts.sql
**		Name: accounts
**		Desc: accounts
**
**		Auth: Lemuel E. Aceron
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/ 

DROP TABLE IF EXISTS tblPaymentPODetails;
DROP TABLE IF EXISTS tblPaymentDebit;
DROP TABLE IF EXISTS tblPaymentCredit;
DROP TABLE IF EXISTS tblPayment;
DROP TABLE IF EXISTS tblChartOfAccount;
DROP TABLE IF EXISTS tblAccountCategory;
DROP TABLE IF EXISTS tblAccountSummary;
/*****************************
**	tblAccountClassification
**
**	ClassTypes:
**		0 = Balance Sheet
**		1 = Income Statement
*****************************/
DROP TABLE IF EXISTS tblAccountClassification;
CREATE TABLE tblAccountClassification (
	`AccountClassificationID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountClassificationCode` VARCHAR(15) NOT NULL,
	`AccountClassificationName` VARCHAR(15) NOT NULL,
	`AccountClassificationType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`AccountClassificationID`),
	INDEX `IX_tblAccountClassification`(`AccountClassificationID`),
	INDEX `IX1_tblAccountClassification`(`AccountClassificationCode`),
	UNIQUE `PK_tblAccountClassification`(`AccountClassificationCode`),
	INDEX `IX2_tblAccountClassification`(`AccountClassificationType`)
);

/*!40000 ALTER TABLE `sysAccessTypes` DISABLE KEYS */; 
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(1,'1-0000','ASSET',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(2,'2-0000','LIABILITY',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(3,'3-0000','EQUITY',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(4,'4-0000','INCOME',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(5,'5-0000','COST OF SALES',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(6,'6-0000','EXPENSE',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(7,'7-0000','OTHER INCOME',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(8,'8-0000','OTHER EXPENSE',1);
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

/*****************************
**	tblAccountSummaries
*****************************/
DROP TABLE IF EXISTS tblAccountSummary;
CREATE TABLE tblAccountSummary (
	`AccountSummaryID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountClassificationID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblAccountClassification(`AccountClassificationID`),
	`AccountSummaryCode` VARCHAR(30) NOT NULL,
	`AccountSummaryName` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`AccountSummaryID`),
	INDEX `IX_tblAccountSummary`(`AccountSummaryID`),
	INDEX `IX1_tblAccountSummary`(`AccountSummaryID`, `AccountSummaryName`),
	UNIQUE `PK_tblAccountSummary`(`AccountSummaryName`),
	INDEX `IX2_tblAccountSummary`(`AccountClassificationID`)
);

-- select CONCAT('INSERT INTO tblAccountSummary(AccountSummaryCode, AccountSummaryName)VALUES(''',AccountSummaryCode,''',','''', AccountSummaryName,''')') from tblAccountSummary;
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(100-199)','CURRENT ASSET');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(200-299)','LONG TERM ASSET');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(300-399)','OTHER ASSETS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(2,'(400-499)','CURRENT LIABILITY');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(2,'(500-599)','LONG-TERM LIABILITY');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(3,'(600-619)','CAPITAL');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(3,'(610-699)','PROFIT AND LOSS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(4,'(700-799)','NET INCOME/NET LOSS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(4,'(800-899)','PROVISION FOR INCOME TAX');


/*****************************
**	tblAccountCategory
*****************************/
DROP TABLE IF EXISTS tblAccountCategory;
CREATE TABLE tblAccountCategory (
	`AccountCategoryID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountSummaryID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblAccountSummary(`AccountSummaryID`),
	`AccountCategoryCode` VARCHAR(30) NOT NULL,
	`AccountCategoryName` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`AccountCategoryID`),
	INDEX `IX_tblAccountCategory`(`AccountCategoryID`),
	INDEX `IX1_tblAccountCategory`(`AccountCategoryID`, `AccountCategoryName`),
	UNIQUE `PK_tblAccountCategory`(`AccountCategoryName`),
	INDEX `IX2_tblAccountCategory`(`AccountSummaryID`),
	FOREIGN KEY (`AccountSummaryID`) REFERENCES tblAccountSummary(`AccountSummaryID`) ON DELETE RESTRICT
);
-- select CONCAT('INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(',AccountSummaryID,',''',AccountCategoryCode,''',','''', AccountCategoryName,''')') from tblAccountCategory;
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(1,'(100-101)','PETTY CASH');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(1,'(150-151)','CASH IN BANK - {BANK ACCOUNT 1}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(2,'(200-249)','TRADE RECIEVABLE - {PUT CUSTOMER ACCOUNT}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(2,'(250-251)','LOAN RECIVABLE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(300-301)','BUILDING');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(302-303)','EQUIPMENTS');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(304-305)','FURNITURES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(400-449)','PRE-PAYMENTS - {ACC NO}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(450-453)','INPUT TAX');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(454-455)','TAX REFUND');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(5,'(500-501)','TRADE-PAYABLE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(6,'(600-609)','CAPITAL A');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(6,'(610-619)','CAPITAL B');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(700-749)','REVENUE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(750-751)','SALES RETURN & ALLOWANCES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(752-753)','BEG. INVENTORY');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(754-755)','PURCHASES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(756-757)','PURCHASE RETURN AND ALLOWANCES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(758-759)','ENDING INVENTORY');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(760-761)','INCOME AND EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(762-789)','ADMINISTRATIVE AND OPERATING EXPENCE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(790-791)','OTHER OPERATING INCOME & EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(792-799)','NON OPERATING INCOME AND EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(8,'(800-849)','PROVISION FOR INCOME TAX');

/*****************************
**	tblChartOfAccount
*****************************/
DROP TABLE IF EXISTS tblChartOfAccount;
CREATE TABLE tblChartOfAccount (
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountCategoryID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblAccountCategory(`AccountCategoryID`),
	`ChartOfAccountCode` VARCHAR(30) NOT NULL,
	`ChartOfAccountName` VARCHAR(50) NOT NULL,
	`Debit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Credit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`ChartOfAccountID`),
	INDEX `IX_tblChartOfAccount`(`ChartOfAccountID`),
	UNIQUE `PK_tblChartOfAccount`(`ChartOfAccountCode`),
	INDEX `IX1_tblChartOfAccount`(`ChartOfAccountCode`),
	INDEX `IX2_tblChartOfAccount`(`ChartOfAccountName`),
	INDEX `IX3_tblChartOfAccount`(`AccountCategoryID`),
	FOREIGN KEY (`AccountCategoryID`) REFERENCES tblAccountCategory(`AccountCategoryID`) ON DELETE RESTRICT
);
-- select CONCAT('INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(',AccountCategoryID,',''',ChartOfAccountCode,''',','''', ChartOfAccountName,''')') from tblChartOfAccount;
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(1,'100','PETTY CASH');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(2,'150','CASH IN BANK - LANDBANK');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(3,'200','TRADE RECIEVABLE - {ACCOUNT}');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(4,'250','LOAN RECIVABLE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(5,'300','BUILDING');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(6,'302','EQUIPMENTS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(7,'304','FURNITURES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(8,'400','PRE-PAYMENTS - {ACC NO}');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(9,'450','INPUT TAX');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(10,'454','TAX REFUND');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(11,'500','TRADE-PAYABLE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(12,'600','CAPITAL A');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(13,'610','CAPITAL B');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(14,'700','SALES - BRANCH 1');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(14,'701','SALES - BRANCH 2');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(15,'750','SALES RETURN & ALLOWANCES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(16,'752','BEG. INVENTORY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(17,'754','PURCHASES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(18,'756','PURCHASE RETURN AND ALLOWANCES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(19,'758','ENDING INVENTORY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(20,'760','INCOME AND EXPENSE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'762','ADVERTISMENT');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'763','COMMISSION EXPENSES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'764','PACKAGING');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'765','SALES PROMOTION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'766','STORES SUPPLIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'767','DIRECTORS FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'768','BONUS ');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'769','CPF& PAYROLL TAX');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'770','MEDICAL');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'771','SALARIES & WAGES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'772','STAFF AMENITIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'773','MANAGEMENT & ADMIN FEES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'774','SECRETARIAL FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'775','AUDIT FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'776','ENTERTAINMENT & REPRESENTATION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'777','INSURANCE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'778','LEGAL & STAMP DUTIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'779','REPAIR & MAINTENANCE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'780','STORE & OFFICE SUPPLIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'781','PRINTING ');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'782','PROFESSIONAL FESS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'783','RENT EXPENSE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'784','SUBSCRIPTION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'785','SECURITY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'786','TRANSPORTATION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'787','TELEX/TELEPHONE/POSTAGE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'788','MISCELLANEOUS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(22,'790','OTHER OPERATING INCOME');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(23,'792','INTEREST INCOME');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(23,'793','BANK CHARGES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(24,'800','PROVISION FOR INCOME TAX');

/*****************************
**	tblBank
*****************************/
DROP TABLE IF EXISTS tblBank;
CREATE TABLE tblBank (
	`BankID` INT(5) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BankCode` VARCHAR(10) NOT NULL,
	`BankName` VARCHAR(50) NOT NULL,
	`ChequeCode` VARCHAR(5) NOT NULL,
	`ChequeCounter` VARCHAR(20) NOT NULL,
	PRIMARY KEY (`BankID`),
	INDEX `IX_tblBanks`(`BankID`, `BankCode`, `BankName`),
	UNIQUE `PK_tblBanks`(`BankCode`),
	INDEX `IX1_tblBanks`(`BankID`),
	INDEX `IX2_tblBanks`(`BankCode`),
	INDEX `IX3_tblBanks`(`BankName`)
);

/*!40000 ALTER TABLE `tblBank` DISABLE KEYS */; 
INSERT INTO tblBank (BankID, BankCode, BankName, ChequeCode, ChequeCounter) VALUES 
					(1, 'DEFAULT', 'DEFAULT BANK', '', '00000000000000');
/*!40000 ALTER TABLE `tblBank` ENABLE KEYS */; 


/*****************************
**	tblPayment
*****************************/
DROP TABLE IF EXISTS tblPayment;
CREATE TABLE tblPayment (
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BankID` INT(5) UNSIGNED NOT NULL DEFAULT 1 REFERENCES tblBank(`BankID`),
	`BankCode` VARCHAR(10) NOT NULL,
	`ChequeDate` DATE NOT NULL DEFAULT '0001-01-01',
	`ChequeNo` VARCHAR(30) NOT NULL,
	`PayeeID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`PayeeCode` VARCHAR(30) NOT NULL,
	`PayeeName` VARCHAR(50) NOT NULL,
	`TotalDebitAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalCreditAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Particulars` VARCHAR(150) NOT NULL,
	`Status` SMALLINT NOT NULL DEFAULT 0,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',	
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',		
	PRIMARY KEY (`PaymentID`),
	INDEX `IX_tblPayment`(`PaymentID`),
	UNIQUE `PK_tblPayment`(`ChequeNo`),
	INDEX `IX1_tblPayment`(`ChequeDate`),
	INDEX `IX2_tblPayment`(`ChequeNo`),
	INDEX `IX3_tblPayment`(`PayeeID`),
	INDEX `IX4_tblPayment`(`PayeeName`)
);

/*****************************
**	tblPaymentPODetails
*****************************/
DROP TABLE IF EXISTS tblPaymentPODetails;
CREATE TABLE tblPaymentPODetails (
	`PaymentDetailID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`POID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentDetailID`),
	INDEX `IX_tblPaymentPODetails`(`PaymentDetailID`),
	UNIQUE `PK_tblPaymentPODetails`(`PaymentDetailID`),
	INDEX `IX1_tblPaymentPODetails`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentPODetails`(`POID`)
);

/*****************************
**	tblPaymentDebit
*****************************/
DROP TABLE IF EXISTS tblPaymentDebit;
CREATE TABLE tblPaymentDebit (
	`PaymentDebitID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentDebitID`),
	INDEX `IX_tblPaymentDebit`(`PaymentDebitID`),
	UNIQUE `PK_tblPaymentDebit`(`PaymentDebitID`),
	INDEX `IX1_tblPaymentDebit`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentDebit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);

/*****************************
**	tblPaymentCredit
*****************************/
DROP TABLE IF EXISTS tblPaymentCredit;
CREATE TABLE tblPaymentCredit (
	`PaymentCreditID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentCreditID`),
	INDEX `IX_tblPaymentCredit`(`PaymentCreditID`),
	UNIQUE `PK_tblPaymentCredit`(`PaymentCreditID`),
	INDEX `IX1_tblPaymentCredit`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentCredit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);




/*****************************
**	Added October 9, 2007
*****************************/
/*****************************
**	tblSO
*****************************/
DROP TABLE IF EXISTS tblSOItems;
DROP TABLE IF EXISTS tblSO;
/*****************************
**	tblSO
*****************************/
DROP TABLE IF EXISTS tblSOItems;
DROP TABLE IF EXISTS tblSO;
CREATE TABLE tblSO (
	`SOID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`SONo` VARCHAR(30) NOT NULL,
	`SODate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`CustomerCode` VARCHAR(25) NOT NULL,
	`CustomerContact` VARCHAR(75) NOT NULL,
	`CustomerAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`CustomerTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`CustomerModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`CustomerTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`SellerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`SOSubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SODiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SOVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SOVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SOStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`SORemarks` VARCHAR(150),
	`CustomerDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (SOID),
	INDEX `IX_tblSO`(`SOID`),
	UNIQUE `PK_tblSO`(`SONo`),
	INDEX `IX1_tblSO`(`CustomerID`),
	INDEX `IX2_tblSO`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSO`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
);

/*****************************
**	tblSOItems
*****************************/
DROP TABLE IF EXISTS tblSOItems;
CREATE TABLE tblSOItems (
	`SOItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`SOID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblSO(`SOID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`BarCode` VARCHAR(30) NOT NULL,
	`Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
	`ProductUnitCode` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`InPercent` TINYINT(1) NOT NULL DEFAULT 1,
	`TotalDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`SOItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`SOItemID`),
	INDEX `IX_tblSOItems`(`SOItemID`),
	INDEX `IX0_tblSOItems`(`SOID`, `ProductID`),
	INDEX `IX1_tblSOItems`(`SOID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOItems`(`ProductCode`),
	INDEX `IX3_tblSOItems`(`SOID`),
	INDEX `IX4_tblSOItems`(`ProductUnitID`)
);

/*****************************
**	tblSOCreditMemo
*****************************/
DROP TABLE IF EXISTS tblSOCreditMemo;
CREATE TABLE tblSOCreditMemo (
	`CreditMemoID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CNNo` VARCHAR(30) NOT NULL,
	`CNDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`CustomerCode` VARCHAR(25) NOT NULL,
	`CustomerContact` VARCHAR(75) NOT NULL,
	`CustomerAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`CustomerTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`CustomerModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`CustomerTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredPostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`SellerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SOReturnStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`CreditMemoStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`CustomerDocNo` VARCHAR(30) NOT NULL,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`CreditMemoID`),
	INDEX `IX_tblSOCreditMemo`(`CreditMemoID`),
	UNIQUE `PK_tblSOCreditMemo`(`CNNo`),
	INDEX `IX1_tblSOCreditMemo`(`CustomerID`),
	INDEX `IX2_tblSOCreditMemo`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSOCreditMemo`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
);

/*****************************
**	tblSOCreditMemoItems
*****************************/
DROP TABLE IF EXISTS tblSOCreditMemoItems;
CREATE TABLE tblSOCreditMemoItems (
	`CreditMemoItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditMemoID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblSOCreditMemo(`CreditMemoID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`BarCode` VARCHAR(30) NOT NULL,
	`Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
	`ProductUnitCode` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`InPercent` TINYINT(1) NOT NULL DEFAULT 1,
	`TotalDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`ItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`CreditMemoItemID`),
	INDEX `IX_tblSOCreditMemoItems`(`CreditMemoItemID`),
	INDEX `IX0_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`),
	INDEX `IX1_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOCreditMemoItems`(`ProductCode`),
	INDEX `IX3_tblSOCreditMemoItems`(`CreditMemoID`),
	INDEX `IX4_tblSOCreditMemoItems`(`ProductUnitID`)
);


 /*****************************
**	tblBranchInventory
*****************************/
DROP TABLE IF EXISTS tblBranchInventory;
CREATE TABLE tblBranchInventory (
	`BranchInventoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchID`),
	PRIMARY KEY (BranchInventoryID),
	INDEX `IX_tblBranchInventory`(`ProductCode`, `BranchID`),
	INDEX `IX1_tblBranchInventory`(`BranchID`)
); 


/*********************************  v_1.0.0.1.sql START  *******************************************************/

/******************************
 * For AccountingSystem
 * Added: March 17,2010 Lemuel E. Aceron
 * ******************************/
ALTER TABLE tblERPConfig ADD DBVersionAccounts varchar(10);
UPDATE tblERPConfig SET DBVersionAccounts = 'v_1.0.0.1';

 /*****************************
**	tblBankDeposit
*****************************/
DROP TABLE IF EXISTS tblBankDeposit;
CREATE TABLE tblBankDeposit (
	`BankDepositID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransactionDate` DATETIME NOT NULL,
	`DepositStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`DepositInAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`DepositMemo` VARCHAR(100) NULL,
	`DepositItemDate` DATETIME NOT NULL,
	`DepositItemType` TINYINT(1) NOT NULL DEFAULT 0,
	`DepositItemAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`DepositItemReference` VARCHAR(100) NULL,
	`DepositItemAmount` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CashBackAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`CashBackAmount` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CashBackMemo` VARCHAR(100) NULL,
	`CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`BankDepositID`),
	INDEX `IX_tblBankDeposit`(`TransactionDate`, `DepositInAccountID`),
	INDEX `IX1_tblBankDeposit`(`DepositInAccountID`)
); 

/*********************************  v_1.0.0.1.sql END  *******************************************************/

  /*********************************  v_1.0.1.2.sql START  *****************************************************/

 /******************************
 * For AccountingSystem
 * Added: April 10,2010 Lemuel E. Aceron
 * ******************************/
UPDATE tblERPConfig SET DBVersionAccounts = 'v_1.0.0.2';

/*****************************
**	tblGJournal
*****************************/
DROP TABLE IF EXISTS tblGJournal;
CREATE TABLE tblGJournal (
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TotalDebitAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalCreditAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Particulars` VARCHAR(150) NOT NULL,
	`Status` SMALLINT NOT NULL DEFAULT 0,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',	
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',		
	PRIMARY KEY (`GJournalID`),
	INDEX `IX_tblGJournal`(`GJournalID`)
);

/*****************************
**	tblGJournalDebit
*****************************/
DROP TABLE IF EXISTS tblGJournalDebit;
CREATE TABLE tblGJournalDebit (
	`GJournalDebitID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalDebitID`),
	INDEX `IX_tblGJournalDebit`(`GJournalDebitID`),
	UNIQUE `PK_tblGJournalDebit`(`GJournalDebitID`),
	INDEX `IX1_tblGJournalDebit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalDebit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);

/*****************************
**	tblGJournalCredit
*****************************/
DROP TABLE IF EXISTS tblGJournalCredit;
CREATE TABLE tblGJournalCredit (
	`GJournalCreditID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalCreditID`),
	INDEX `IX_tblGJournalCredit`(`GJournalCreditID`),
	UNIQUE `PK_tblGJournalCredit`(`GJournalCreditID`),
	INDEX `IX1_tblGJournalCredit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalCredit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);

/*********************************  v_1.0.1.2.sql END  *******************************************************/    

ALTER TABLE tblChartOfAccount MODIFY `Debit` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblChartOfAccount MODIFY `Credit` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblPayment DROP CreatedOn;
ALTER TABLE tblPaymentCredit DROP CreatedOn;
ALTER TABLE tblPaymentDebit DROP CreatedOn;
ALTER TABLE tblPaymentPODetails DROP CreatedOn;

ALTER TABLE tblPayment ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblPaymentCredit ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblPaymentDebit ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblPaymentPODetails ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';

ALTER TABLE tblPayment DROP LastModified;
ALTER TABLE tblPaymentCredit DROP LastModified;
ALTER TABLE tblPaymentDebit DROP LastModified;
ALTER TABLE tblPaymentPODetails DROP LastModified;

ALTER TABLE tblPayment ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblPaymentCredit ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblPaymentDebit ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblPaymentPODetails ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

DROP TRIGGER IF EXISTS trgtblPaymentCreatedOn;
DROP TRIGGER IF EXISTS trgtblPaymentCreditCreatedOn;
DROP TRIGGER IF EXISTS trgtblPaymentDebitCreatedOn;
DROP TRIGGER IF EXISTS trgtblPaymentPODetailsCreatedOn;

CREATE TRIGGER trgtblPaymentCreatedOn BEFORE INSERT ON tblPayment FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblPaymentCreditCreatedOn BEFORE INSERT ON tblPaymentCredit FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblPaymentDebitCreatedOn BEFORE INSERT ON tblPaymentDebit FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblPaymentPODetailsCreatedOn BEFORE INSERT ON tblPaymentPODetails FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

UPDATE tblPayment SET LastModified = NOW();
UPDATE tblPaymentCredit SET LastModified = NOW();
UPDATE tblPaymentDebit SET LastModified = NOW();
UPDATE tblPaymentPODetails SET LastModified = NOW();

ALTER TABLE tblGJournal ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblGJournalCredit ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblGJournalDebit ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';

ALTER TABLE tblGJournal DROP LastModified;
ALTER TABLE tblGJournalCredit DROP LastModified;
ALTER TABLE tblGJournalDebit DROP LastModified;

ALTER TABLE tblGJournal ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblGJournalCredit ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblGJournalDebit ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

DROP TRIGGER IF EXISTS trgtblGJournalCreatedOn;
DROP TRIGGER IF EXISTS trgtblGJournalCreditCreatedOn;
DROP TRIGGER IF EXISTS trgtblGJournalDebitCreatedOn;

CREATE TRIGGER trgtblGJournalCreatedOn BEFORE INSERT ON tblGJournal FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblGJournalCreditCreatedOn BEFORE INSERT ON tblGJournalCredit FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblGJournalDebitCreatedOn BEFORE INSERT ON tblGJournalDebit FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;

UPDATE tblGJournal SET LastModified = NOW();
UPDATE tblGJournalCredit SET LastModified = NOW();
UPDATE tblGJournalDebit SET LastModified = NOW();

ALTER TABLE tblGJournal DROP CreatedOn;
ALTER TABLE tblGJournalCredit DROP CreatedOn;
ALTER TABLE tblGJournalDebit DROP CreatedOn;

ALTER TABLE tblChartOfAccount DROP CreatedOn;
ALTER TABLE tblChartOfAccount ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblChartOfAccount DROP LastModified;
ALTER TABLE tblChartOfAccount ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
DROP TRIGGER IF EXISTS trgtblChartOfAccountCreatedOn;
CREATE TRIGGER trgtblChartOfAccountCreatedOn BEFORE INSERT ON tblChartOfAccount FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
UPDATE tblChartOfAccount SET LastModified = NOW();

ALTER TABLE tblAccountCategory DROP CreatedOn;
ALTER TABLE tblAccountClassification DROP CreatedOn;
ALTER TABLE tblAccountSummary DROP CreatedOn;
ALTER TABLE tblAccountCategory ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblAccountClassification ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblAccountSummary ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblAccountCategory DROP LastModified;
ALTER TABLE tblAccountClassification DROP LastModified;
ALTER TABLE tblAccountSummary DROP LastModified;
ALTER TABLE tblAccountCategory ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblAccountClassification ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblAccountSummary ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
DROP TRIGGER IF EXISTS trgtblAccountCategoryCreatedOn;
DROP TRIGGER IF EXISTS trgtblAccountClassificationCreatedOn;
DROP TRIGGER IF EXISTS trgtblAccountSummaryCreatedOn;
CREATE TRIGGER trgtblAccountCategoryCreatedOn BEFORE INSERT ON tblAccountCategory FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblAccountClassificationCreatedOn BEFORE INSERT ON tblAccountClassification FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblAccountSummaryCreatedOn BEFORE INSERT ON tblAccountSummary FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
UPDATE tblAccountCategory SET LastModified = NOW();
UPDATE tblAccountClassification SET LastModified = NOW();
UPDATE tblAccountSummary SET LastModified = NOW();


ALTER TABLE tblBank DROP CreatedOn;
ALTER TABLE tblBankDeposit DROP CreatedOn;
ALTER TABLE tblBank ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblBankDeposit ADD `CreatedOn` DATETIME NOT NULL DEFAULT '1900-01-01 12:00:00';
ALTER TABLE tblBank DROP LastModified;
ALTER TABLE tblBankDeposit DROP LastModified;
ALTER TABLE tblBank ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE tblBankDeposit ADD `LastModified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
DROP TRIGGER IF EXISTS trgtblBankCreatedOn;
DROP TRIGGER IF EXISTS trgtblBankDepositCreatedOn;
CREATE TRIGGER trgtblBankCreatedOn BEFORE INSERT ON tblBank FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblBankDepositCreatedOn BEFORE INSERT ON tblBankDeposit FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
UPDATE tblBank SET LastModified = NOW();
UPDATE tblBankDeposit SET LastModified = NOW();