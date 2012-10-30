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
	`TotalDebitAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TotalCreditAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Particulars` VARCHAR(150) NOT NULL,
	`Status` SMALLINT NOT NULL DEFAULT 0,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',	
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',		
	PRIMARY KEY (`GJournalID`),
	INDEX `IX_tblGJournal`(`GJournalID`)
)
TYPE=INNODB COMMENT = 'Financial GJournals';

/*****************************
**	tblGJournalDebit
*****************************/
DROP TABLE IF EXISTS tblGJournalDebit;
CREATE TABLE tblGJournalDebit (
	`GJournalDebitID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalDebitID`),
	INDEX `IX_tblGJournalDebit`(`GJournalDebitID`),
	UNIQUE `PK_tblGJournalDebit`(`GJournalDebitID`),
	INDEX `IX1_tblGJournalDebit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalDebit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Financial Debit GJournals';

/*****************************
**	tblGJournalCredit
*****************************/
DROP TABLE IF EXISTS tblGJournalCredit;
CREATE TABLE tblGJournalCredit (
	`GJournalCreditID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalCreditID`),
	INDEX `IX_tblGJournalCredit`(`GJournalCreditID`),
	UNIQUE `PK_tblGJournalCredit`(`GJournalCreditID`),
	INDEX `IX1_tblGJournalCredit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalCredit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Financial Credit GJournals';

/*********************************  v_1.0.1.2.sql END  *******************************************************/    