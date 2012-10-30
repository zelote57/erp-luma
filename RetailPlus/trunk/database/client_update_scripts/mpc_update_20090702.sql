 /**************************************************************
** March 14, 2009
** Lemuel E. Aceron
**
** 1. Add table to hold temporary records for sales per item
** 2. Add stored procedure to insert the records
** 3. Add stored procedure to select the records
**
**************************************************************/

DROP TABLE IF EXISTS tblProductHistory;
CREATE TABLE tblProductHistory (
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
)
TYPE=INNODB COMMENT = 'Product History Report';

ALTER TABLE tblProducts ADD `WillPrintProductComposition` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************  v_2.0.0.0.sql START  *******************************************************/

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (126, 'MallForwarder');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);


/*********************************  v_2.0.0.0.sql END  *******************************************************/

/*********************************  v_2.0.0.1.sql START  *******************************************************/
	
/*********************************
Lemuel E. Aceron
April 22, 2009
*********************************/
ALTER TABLE tblTerminal ADD DBVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD FEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD BEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 

/*********************************  v_2.0.0.1.sql END  *******************************************************/

/*********************************  v_2.0.0.2.sql START  *******************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.2';

ALTER TABLE tblReceipt MODIFY `TEXT` varchar(20);
	
/*********************************
Lemuel E. Aceron
May 1, 2009

OrderType values:
	0 = DINEIN
	1 = TAKE OUT
	2 = DELIVERY
*********************************/
ALTER TABLE tblTransactions01 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions02 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions03 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions04 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions05 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions06 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions07 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions08 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions09 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions10 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions11 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions12 ADD OrderType smallint NOT NULL DEFAULT 0; 


/*********************************
Create table for discount
*********************************/
ALTER TABLE tblDiscount MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions12 MODIFY DiscountCode varchar(5);

ALTER TABLE tblTransactionItems01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems12 MODIFY DiscountCode varchar(5);

-- This fields are from .config file, moved to database.
ALTER TABLE tblTerminal ADD IsPrinterDotmatrix TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD IsChargeEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD IsDiscountEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD CheckCutOffTime TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD StartCutOffTime VARCHAR(5) NOT NULL DEFAULT '00:00';
ALTER TABLE tblTerminal ADD EndCutOffTime VARCHAR(5) NOT NULL DEFAULT '23:59';
ALTER TABLE tblTerminal ADD WithRestaurantFeatures TINYINT(1) NOT NULL DEFAULT 0;
        
--Make a default DiscountCode for Senior Citizen Discount
ALTER TABLE tblTerminal ADD SeniorCitizenDiscountCode varchar(5);
UPDATE tblTerminal SET SeniorCitizenDiscountCode = 'SENCZ';

DROP TABLE IF EXISTS tblDiscountHistory;
CREATE TABLE tblDiscountHistory (
	`SessionID` VARCHAR(30) NOT NULL,
	`TerminalNo` VARCHAR(30) NOT NULL,
	`DiscountCode` VARCHAR(5) NOT NULL,
	`DiscountCount` BIGINT(20) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblDiscount`(`SessionID`),
	INDEX `IX_tblDiscount1`(`DiscountCode`)
)
TYPE=INNODB COMMENT = 'Product Discount Report';

/*********************************  v_2.0.0.2.sql END  *******************************************************/

/*********************************  v_2.0.0.3.sql START  *****************************************************/

-- Added procGenerateSalesPerItemByGroup for P&L per item., run retailplus_proc.sql

DELETE FROM tblSalesPerItem;

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.3';

ALTER TABLE tblTerminal ADD `IsTouchScreen` TINYINT(1) NOT NULL DEFAULT 0; 

ALTER TABLE tblTerminalReportHistory ADD `MallFileName` VARCHAR(30); 
ALTER TABLE tblTerminalReportHistory ADD `IsMallFileUploadComplete` TINYINT(1) NOT NULL DEFAULT 0; 

UPDATE tblTerminalReportHistory SET IsMallFileUploadComplete = 1 WHERE MallFileName IS NOT NULL;

ALTER TABLE tblDeposit ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblWithHold ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblDisburse ADD `Remarks` VARCHAR(255); 

/*********************************  v_2.0.0.3.sql END  *******************************************************/

/*********************************  v_2.0.0.4.sql START  *****************************************************/

-- Added procProductPackageUpdate for P&L per item., run retailplus_proc.sql

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.4';

/*****************************
**	tblProductPackagePriceHistory
*****************************/
DROP TABLE IF EXISTS tblProductPackagePriceHistory;
CREATE TABLE tblProductPackagePriceHistory (
	`HistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ChangeDate` DATETIME NOT NULL,
	`PurchasePriceBefore` DECIMAL(10,2),
	`PurchasePriceNow` DECIMAL(10,2),
	`SellingPriceBefore` DECIMAL(10,2),
	`SellingPriceNow` DECIMAL(10,2),
	`VATBefore` DECIMAL(10,2),
	`VATNow` DECIMAL(10,2),
	`EVATBefore` DECIMAL(10,2),
	`EVATNow` DECIMAL(10,2),
	`LocalTaxBefore` DECIMAL(10,2),
	`LocalTaxNow` DECIMAL(10,2),
PRIMARY KEY (`HistoryID`),
INDEX `IX_tblProductPackagePriceHistory`(`HistoryID`),
INDEX `IX1_tblProductPackagePriceHistory`(`UID`),
INDEX `IX2_tblProductPackagePriceHistory`(`ChangeDate`)
)
TYPE=INNODB COMMENT = 'Product Package Change Price Logs';

/*****************************
**	tblMatrixPackagePriceHistory
*****************************/
DROP TABLE IF EXISTS tblMatrixPackagePriceHistory;
CREATE TABLE tblMatrixPackagePriceHistory (
	`HistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ChangeDate` DATETIME NOT NULL,
	`PurchasePriceBefore` DECIMAL(10,2),
	`PurchasePriceNow` DECIMAL(10,2),
	`SellingPriceBefore` DECIMAL(10,2),
	`SellingPriceNow` DECIMAL(10,2),
	`VATBefore` DECIMAL(10,2),
	`VATNow` DECIMAL(10,2),
	`EVATBefore` DECIMAL(10,2),
	`EVATNow` DECIMAL(10,2),
	`LocalTaxBefore` DECIMAL(10,2),
	`LocalTaxNow` DECIMAL(10,2),
PRIMARY KEY (`HistoryID`),
INDEX `IX_tblMatrixPackagePriceHistory`(`HistoryID`),
INDEX `IX1_tblMatrixPackagePriceHistory`(`UID`),
INDEX `IX2_tblMatrixPackagePriceHistory`(`ChangeDate`)
)
TYPE=INNODB COMMENT = 'Product Matrix Package Change Price Logs';

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (127, 'Change Product Price');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 127, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 127, 1, 1);

/*********************************  v_2.0.0.4.sql END  *******************************************************/
