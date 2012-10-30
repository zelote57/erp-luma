/**************************************************************
** March 14, 2009
** Lemuel E. Aceron
**
** 1. Add table to hold temporary records for sales per item
** 2. Add stored procedure to insert the records
** 3. Add stored procedure to select the records
**
**************************************************************/

DROP TABLE IF EXISTS tblSalesPerItem;
CREATE TABLE tblSalesPerItem (
	`SessionID` VARCHAR(30) NOT NULL,
	`ProductCode` VARCHAR(100) NOT NULL,
	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblSalesPerItem`(`SessionID`),
	INDEX `IX_tblSalesPerItem1`(`ProductCode`)
)
TYPE=INNODB COMMENT = 'Sales Per Item Report';


