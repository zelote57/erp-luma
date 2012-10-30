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
