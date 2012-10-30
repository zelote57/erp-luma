/*********************************  v_1.0.0.1.sql START  *******************************************************/

ALTER TABLE tblERPConfig ADD DBVersion varchar(10);
UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.1';

ALTER TABLE tblProducts ADD ChartOfAccountIDTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxTransferIn INT(4) UNSIGNED NOT NULL DEFAULT 0;

/*****************************
**	tblTransferIn
*****************************/
DROP TABLE IF EXISTS tblTransferInItems;
DROP TABLE IF EXISTS tblTransferIn;
CREATE TABLE tblTransferIn (
	`TransferInID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransferInNo` VARCHAR(30) NOT NULL,
	`TransferInDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`SupplierCode` VARCHAR(25) NOT NULL,
	`SupplierContact` VARCHAR(75) NOT NULL,
	`SupplierAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`SupplierTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`SupplierModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`SupplierTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`TransferrerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`TransferrerName` VARCHAR(100),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPBills` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPContra` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (TransferInID),
	INDEX `IX_tblTransferIn`(`TransferInID`),
	UNIQUE `PK_tblTransferIn`(`TransferInNo`),
	INDEX `IX1_tblTransferIn`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblTransferIn`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblTransferIn`(`TransferrerID`),
	FOREIGN KEY (`TransferrerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Transfer In';

/*****************************
**	tblTransferInItems
*****************************/
DROP TABLE IF EXISTS tblTransferInItems;
CREATE TABLE tblTransferInItems (
	`TransferInItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransferInID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransferIn(`TransferInID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
    `ProductCode` VARCHAR(30) NOT NULL,
    `BarCode` VARCHAR(30) NOT NULL,
    `Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
    `ProductUnitCode` VARCHAR(30) NOT NULL,
   	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`isVatInclusive` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`TransferInItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`ChartOfAccountIDTransferIn` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxTransferIn` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`TransferInItemID`),
	INDEX `IX_tblTransferInItems`(`TransferInItemID`),
	INDEX `IX0_tblTransferInItems`(`TransferInID`, `ProductID`),
	INDEX `IX1_tblTransferInItems`(`TransferInID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblTransferInItems`(`ProductCode`),
	INDEX `IX3_tblTransferInItems`(`TransferInID`),
	INDEX `IX4_tblTransferInItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'TransferIn Items';

ALTER TABLE tblProducts ADD ChartOfAccountIDTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxTransferOut INT(4) UNSIGNED NOT NULL DEFAULT 0;

/*****************************
**	tblTransferOut
*****************************/
DROP TABLE IF EXISTS tblTransferOutItems;
DROP TABLE IF EXISTS tblTransferOut;
CREATE TABLE tblTransferOut (
	`TransferOutID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransferOutNo` VARCHAR(30) NOT NULL,
	`TransferOutDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`SupplierCode` VARCHAR(25) NOT NULL,
	`SupplierContact` VARCHAR(75) NOT NULL,
	`SupplierAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`SupplierTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`SupplierModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`SupplierTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`TransferrerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`TransferrerName` VARCHAR(100),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPBills` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPContra` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (TransferOutID),
	INDEX `IX_tblTransferOut`(`TransferOutID`),
	UNIQUE `PK_tblTransferOut`(`TransferOutNo`),
	INDEX `IX1_tblTransferOut`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblTransferOut`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblTransferOut`(`TransferrerID`),
	FOREIGN KEY (`TransferrerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Transfer In';

/*****************************
**	tblTransferOutItems
*****************************/
DROP TABLE IF EXISTS tblTransferOutItems;
CREATE TABLE tblTransferOutItems (
	`TransferOutItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransferOutID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransferOut(`TransferOutID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
    `ProductCode` VARCHAR(30) NOT NULL,
    `BarCode` VARCHAR(30) NOT NULL,
    `Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
    `ProductUnitCode` VARCHAR(30) NOT NULL,
   	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`isVatInclusive` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`TransferOutItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`ChartOfAccountIDTransferOut` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxTransferOut` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`TransferOutItemID`),
	INDEX `IX_tblTransferOutItems`(`TransferOutItemID`),
	INDEX `IX0_tblTransferOutItems`(`TransferOutID`, `ProductID`),
	INDEX `IX1_tblTransferOutItems`(`TransferOutID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblTransferOutItems`(`ProductCode`),
	INDEX `IX3_tblTransferOutItems`(`TransferOutID`),
	INDEX `IX4_tblTransferOutItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'TransferOut Items';


ALTER TABLE tblProducts ADD ChartOfAccountIDInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxInvAdjustment INT(4) UNSIGNED NOT NULL DEFAULT 0;


/*****************************
**	tblInvAdjustment
*****************************/
DROP TABLE IF EXISTS tblInvAdjustment;
CREATE TABLE tblInvAdjustment (
	`InvAdjustmentID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`InvAdjustmentDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
    `Description` VARCHAR(100) NOT NULL,
    `VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
    `UnitCode` VARCHAR(30) NOT NULL,
   	`QuantityBefore` DECIMAL(18,2) NOT NULL DEFAULT 0,
   	`QuantityNow` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`MinThresholdBefore` DECIMAL(10,2),
	`MinThresholdNow` DECIMAL(10,2),
	`MaxThresholdBefore` DECIMAL(10,2),
	`MaxThresholdNow` DECIMAL(10,2),
	`Remarks` VARCHAR(100),
PRIMARY KEY (`InvAdjustmentID`),
INDEX `IX_tblInvAdjustment`(`InvAdjustmentID`),
INDEX `IX1_tblInvAdjustment`(`UID`),
INDEX `IX2_tblInvAdjustment`(`InvAdjustmentDate`)
)
TYPE=INNODB COMMENT = 'Product Inventory Adjustment Logs';



/*********************************  v_1.0.0.1.sql END  *******************************************************/
 