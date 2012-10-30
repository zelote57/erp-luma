
/*****************************
**	tblInvAdjustment
*****************************/
DROP TABLE IF EXISTS tblInvAdjustmentItems;
DROP TABLE IF EXISTS tblInvAdjustment;
CREATE TABLE tblInvAdjustment (
	`InvAdjustmentID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`InvAdjustmentNo` VARCHAR(30) NOT NULL,
	`InvAdjustmentDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`SupplierCode` VARCHAR(25) NOT NULL,
	`SupplierContact` VARCHAR(75) NOT NULL,
	`SupplierAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`SupplierTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`SupplierModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`SupplierTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`TransferredByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`InvAdjustmentSubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`InvAdjustmentRemarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (InvAdjustmentID),
	INDEX `IX_tblInvAdjustment`(`InvAdjustmentID`),
	UNIQUE `PK_tblInvAdjustment`(`InvAdjustmentNo`),
	INDEX `IX1_tblInvAdjustment`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblInvAdjustment`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblInvAdjustment`(`TransferredByID`),
	FOREIGN KEY (`TransferredByID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Purchase Orders';

/*****************************
**	tblInvAdjustmentItems
*****************************/
DROP TABLE IF EXISTS tblInvAdjustmentItems;
CREATE TABLE tblInvAdjustmentItems (
	`InvAdjustmentItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`InvAdjustmentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblInvAdjustment(`InvAdjustmentID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
    `ProductCode` VARCHAR(30) NOT NULL,
    `BarCode` VARCHAR(30) NOT NULL,
    `Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
    `ProductUnitCode` VARCHAR(30) NOT NULL,
   	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InPercent` TINYINT(1) NOT NULL DEFAULT 1,
	`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`InvAdjustmentItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`InvAdjustmentItemID`),
	INDEX `IX_tblInvAdjustmentItems`(`InvAdjustmentItemID`),
	INDEX `IX0_tblInvAdjustmentItems`(`InvAdjustmentID`, `ProductID`),
	INDEX `IX1_tblInvAdjustmentItems`(`InvAdjustmentID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblInvAdjustmentItems`(`ProductCode`),
	INDEX `IX3_tblInvAdjustmentItems`(`InvAdjustmentID`),
	INDEX `IX4_tblInvAdjustmentItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'InvAdjustment Items'; 

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

