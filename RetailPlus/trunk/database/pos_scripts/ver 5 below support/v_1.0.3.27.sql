/*********************************   v_1.0.3.27.sql START  ******************************************************/

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


/*********************************   v_1.0.3.27.sql END  ******************************************************/
 