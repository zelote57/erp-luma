/*****************************
**	tblReceipt
*****************************/
DROP TABLE IF EXISTS tblReceipt;
CREATE TABLE tblReceipt (
	`Module` VARCHAR(20) NOT NULL,
	`Text` VARCHAR(20),
	`Value` VARCHAR(40),
	`Orientation` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	INDEX `IX_tblReceipt`(`Text`),
	INDEX `IX1_tblReceipt`(`Value`)
)
TYPE=INNODB COMMENT = 'Receipt Format';


INSERT INTO tblReceipt VALUES ('ReportHeaderSpacer', '','0',0);

INSERT INTO tblReceipt VALUES ('ReportHeader1', '','6937 Rosal Street Guadalupe Viejo',1);
INSERT INTO tblReceipt VALUES ('ReportHeader2', '','Makati City, Phil',1);
INSERT INTO tblReceipt VALUES ('ReportHeader3', '','{DateNow}',1);
INSERT INTO tblReceipt VALUES ('ReportHeader4', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader5', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader6', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader7', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader8', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader9', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader10', '','',1);



INSERT INTO tblReceipt VALUES ('PageHeader1', '',	'TIN: 007-094-991-000',0);
INSERT INTO tblReceipt VALUES ('PageHeader2', 'OFFICIAL RECEIPT','{InvoiceNo}',1);
INSERT INTO tblReceipt VALUES ('PageHeader3', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader4', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader5', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader6', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader7', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader8', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader9', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader10', '','',0);

INSERT INTO tblReceipt VALUES ('PageFooterA1',	'','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA2',	'','----------------------------------------',1);
INSERT INTO tblReceipt VALUES ('PageFooterA3',	'SUBTOTAL',			'{SUBTOTAL}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA4',	'OTH CHARGE',		'{OTH CHARGE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA5',	'DISCOUNT',			'{DISCOUNT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA6',	'AMOUNT DUE',		'{AMOUNT DUE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA7',	'AMOUNT TENDER',	'{AMOUNT TENDER}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA8',	'CHANGE',			'{CHANGE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA9', '','{NewLine}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA10', 'NON-VAT AMT.',		'{NON-VAT AMT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA11', 'VATABLE AMT.',		'{VATABLE AMT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA12', 'VAT',				'{VAT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA13', '','----------------------------------------',1);
INSERT INTO tblReceipt VALUES ('PageFooterA14', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA15', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA16', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA17', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA18', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA19', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA20', '','',0);

INSERT INTO tblReceipt VALUES ('PageFooterB1', '', '{NewLine}',0);
INSERT INTO tblReceipt VALUES ('PageFooterB2', 'TTL ITEM SOLD','{TTL ITEM SOLD}',0);
INSERT INTO tblReceipt VALUES ('PageFooterB3', 'TTL QTY SOLD','{TTL QTY SOLD}',0);
INSERT INTO tblReceipt VALUES ('PageFooterB4', 'Customer','{Customer Name}',0);
INSERT INTO tblReceipt VALUES ('PageFooterB5', 'Acc. No.:', '{AccreditationNo}',0);


INSERT INTO tblReceipt VALUES ('ReportFooter1', 'Cashier','{Cashier}',1);
INSERT INTO tblReceipt VALUES ('ReportFooter2', 'Terminal #','{TerminalNo}',1);
INSERT INTO tblReceipt VALUES ('ReportFooter3', 'MIN','{MachineSerialNo}',1);
INSERT INTO tblReceipt VALUES ('ReportFooter4', '','',1);
INSERT INTO tblReceipt VALUES ('ReportFooter5', '','',1);

INSERT INTO tblReceipt VALUES ('ReportFooterSpacer', '','4',1);


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

