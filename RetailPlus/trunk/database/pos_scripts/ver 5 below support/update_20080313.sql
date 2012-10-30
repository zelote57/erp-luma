 /*****************************
**	tblPLUReport
*****************************/
DROP TABLE IF EXISTS tblPLUReport;
CREATE TABLE tblPLUReport (
	`PLUReportID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TerminalNo` VARCHAR(10) NOT NULL,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`Amount` DECIMAL(10,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (PLUReportID),
	INDEX `IX_tblPLUReport`(`TerminalNo`, `ProductCode`),
	INDEX `IX1_tblPLUReport`(`TerminalNo`)
)
TYPE=INNODB COMMENT = 'PLU Report Extraction';