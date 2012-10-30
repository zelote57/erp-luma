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
)
TYPE=INNODB COMMENT = 'Bank deposits'; 

/*********************************  v_1.0.0.1.sql END  *******************************************************/
