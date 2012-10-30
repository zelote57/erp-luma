 /*********************************  v_2.0.1.2.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.1.2';

DROP TABLE IF EXISTS tblProductPurchasePriceHistory;
CREATE TABLE tblProductPurchasePriceHistory (
`ProductPurchasePriceHistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) NOT NULL DEFAULT 1,
`MatrixID` BIGINT(20) NOT NULL DEFAULT 0,
`SupplierID` BIGINT(20) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseDate` DATETIME NOT NULL,
`Remarks` VARCHAR(50),
PRIMARY KEY (ProductPurchasePriceHistoryID),
INDEX `IX_tblProductPurchasePriceHistory`(`ProductID`),
INDEX `IX_tblProductPurchasePriceHistory1`(`SupplierID`)
)
TYPE=INNODB COMMENT = 'Product Purchase Price History';

ALTER TABLE tblStock ADD `Active` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************  v_2.0.1.2.sql END  *******************************************************/    