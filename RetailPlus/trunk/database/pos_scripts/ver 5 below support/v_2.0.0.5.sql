/*********************************  v_2.0.0.5.sql START  *****************************************************/
-- SELECT IFNULL(AllowRead,0) as 'Read', IFNULL(AllowWrite,0) as 'Write'
-- FROM sysAccessRights a INNER JOIN sysAccessTypes b ON a.TranTypeID = b.TypeID
-- WHERE UID = 1 AND TranTypeID = 3 AND Enabled=1


UPDATE tblTerminal SET DBVersion = 'v_2.0.0.5';

ALTER TABLE sysAccessTypes ADD Enabled smallint NOT NULL DEFAULT 1; 

-- Added August 2, 2009 to monitor if product still has/have variations
ALTER TABLE tblProducts ADD VariationCount BIGINT NOT NULL DEFAULT 0;

UPDATE tblProducts SET VariationCount = (SELECT COUNT(MatrixID) FROM tblProductBaseVariationsMatrix z WHERE tblProducts.ProductID = z.ProductID);

/*****************************
**	tblProductPrices
*****************************/
DROP TABLE IF EXISTS tblProductPrices;
CREATE TABLE tblProductPrices (
	`SessionID` VARCHAR(30) NOT NULL,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductCode` VARCHAR(30) NOT NULL,
	`ProductDescription` VARCHAR(30) NOT NULL,
	`MatrixDescription` VARCHAR(100) NOT NULL,
	`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductGroupName` VARCHAR(30) NOT NULL,
	`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductSubGroupName` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(10,2),
	`UnitCode` VARCHAR(10) NOT NULL,
	`UnitName` VARCHAR(30) NOT NULL,
	`PurchasePrice` DECIMAL(10,2),
	`Price` DECIMAL(10,2),
	`VAT` DECIMAL(10,2),
	`EVAT` DECIMAL(10,2),
	`LocalTax` DECIMAL(10,2),
INDEX `IX_tblProductPrices`(`SessionID`),
INDEX `IX1_tblProductPrices`(`ProductID`),
INDEX `IX2_tblProductPrices`(`ProductGroupID`),
INDEX `IX3_tblProductPrices`(`ProductSubGroupID`)
)
TYPE=INNODB COMMENT = 'Product Prices Logs';

/*********************************  v_2.0.0.5.sql END  *******************************************************/