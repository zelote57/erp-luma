 /*****************************
**	Added on November 22, 2007
**	Lemuel E. Aceron
**	
**	UPDATES:
**	1. Add product composition to enable inventory of RAW PRODUCTS.
**	2. Add TrustFund in the tblTerminalReport to identify how much trustfund is applied during ZREAD.
*****************************/ 

 /*****************************
**	tblProductComposition
*****************************/
DROP TABLE IF EXISTS tblProductComposition;
CREATE TABLE tblProductComposition (
	`CompositionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`MainProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`VariationMatrixID` INT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblProductComposition`(`CompositionID`,`ProductID`),
    UNIQUE `PK_tblProductComposition`(`MainProductID`, `ProductID`,`UnitID`,`Quantity`),
    INDEX `IX1_tblProductComposition`(`MainProductID`),
    FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
    INDEX `IX2_tblProductComposition`(`UnitID`),
	FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Product Composition';


ALTER TABLE tblTerminalReportHistory ADD `TrustFund` DECIMAL(5,2) NOT NULL DEFAULT 0.00;

/*****************************
**	Added on Feb 07, 2008
**	Lemuel E. Aceron
**	Include Waiter in all installations
*****************************/
INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Waiters', 'Default group for waiters.');

INSERT INTO sysAccessUsers (UID, Username, Password, DateCreated ) VALUES (2, 'waiter', 'waiter', now());

INSERT INTO sysAccessUserDetails (UID ,Name, CountryID, GroupID) VALUES (2, 'RetailPlus Waiter', 1, 5);

ALTER TABLE tblTransactions01 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions01 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions02 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions02 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions03 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions03 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions04 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions04 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions05 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions05 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions06 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions06 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions07 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions07 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions08 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions08 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions09 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions09 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions10 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions10 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions11 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions11 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions12 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions12 ADD `WaiterName` VARCHAR(100) NOT NULL;
 
/*****************************
**	tblReceipt
*****************************/
DROP TABLE IF EXISTS tblReceipt;
CREATE TABLE tblReceipt (
	`Module` VARCHAR(20) NOT NULL,
	`Text` VARCHAR(13),
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
INSERT INTO tblReceipt VALUES ('ReportHeader4', '','-/-',1);
INSERT INTO tblReceipt VALUES ('ReportHeader5', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader6', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader7', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader8', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader9', '','',1);
INSERT INTO tblReceipt VALUES ('ReportHeader10', '','',1);


INSERT INTO tblReceipt VALUES ('PageHeader1', 'Invoice #',	'{InvoiceNo}',0);
INSERT INTO tblReceipt VALUES ('PageHeader2', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader3', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader4', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader5', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader6', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader7', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader8', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader9', '','',0);
INSERT INTO tblReceipt VALUES ('PageHeader10', '','',0);

INSERT INTO tblReceipt VALUES ('PageFooterA1',	'','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA2',	'----------------------------------------','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA3',	'SUBTOTAL',			'{SUBTOTAL}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA4',	'OTH CHARGE',		'{OTH CHARGE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA5',	'DISCOUNT',			'{DISCOUNT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA6',	'AMOUNT DUE',		'{AMOUNT DUE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA7',	'AMOUNT TENDER',	'{AMOUNT TENDER}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA8',	'CHANGE',			'{CHANGE}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA9', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA10', 'NON-VAT AMT.',		'{NON-VAT AMT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA11', 'VATABLE AMT.',		'{VATABLE AMT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA12', 'VAT',				'{VAT}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA13', '','',0);
INSERT INTO tblReceipt VALUES ('PageFooterA14', 'TTL ITEM SOLD',	'{TTL ITEM SOLD}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA15', 'TTL QTY SOLD',		'{TTL QTY SOLD}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA16', 'Customer',			'{Customer Name}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA17', 'Accreditation #',	'{AccreditationNo}',0);
INSERT INTO tblReceipt VALUES ('PageFooterA18', '',					'',0);
INSERT INTO tblReceipt VALUES ('PageFooterA19', '',					'',0);
INSERT INTO tblReceipt VALUES ('PageFooterA20', '',					'{-}',0);

INSERT INTO tblReceipt VALUES ('PageFooterB1', '',					'Thank you for shopping.',1);
INSERT INTO tblReceipt VALUES ('PageFooterB2', '',					'Please Come Again.',1);
INSERT INTO tblReceipt VALUES ('PageFooterB3', '',					'{-}',0);
INSERT INTO tblReceipt VALUES ('PageFooterB4', '', '',0);
INSERT INTO tblReceipt VALUES ('PageFooterB5', '', '',0);

INSERT INTO tblReceipt VALUES ('ReportFooter1', 'Cashier','{Cashier}',1);
INSERT INTO tblReceipt VALUES ('ReportFooter2', 'Terminal #','{TerminalNo}',1);
INSERT INTO tblReceipt VALUES ('ReportFooter3', 'Machine Serial #','{MachineSerialNo}',1);
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

 /*****************************
**	tblBranchInventory
*****************************/
DROP TABLE IF EXISTS tblBranchInventory;
CREATE TABLE tblBranchInventory (
	`BranchInventoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchID`),
	PRIMARY KEY (BranchInventoryID),
	INDEX `IX_tblBranchInventory`(`ProductCode`, `BranchID`),
	INDEX `IX1_tblBranchInventory`(`BranchID`)
)
TYPE=INNODB COMMENT = 'Branch Inventories'; 


/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lemuel E. Aceron
*****************************/

ALTER TABLE tblTransactions01 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminal ADD `ShowOneTerminalSuspendedTransactions` TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD `ShowOnlyPackedTransactions` TINYINT(1) NOT NULL DEFAULT 0;

/************************************
**	TerminalReceiptType
**	0 = POS terminal report
**	1 = Sales Invoice or Cash Invoice
************************************/
ALTER TABLE tblTerminal ADD `TerminalReceiptType` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD `SalesInvoicePrinterName` varchar(30) NOT NULL DEFAULT 'RetailPlus';
ALTER TABLE tblTerminal ADD `CashCountBeforeReport` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD `PreviewTerminalReport` TINYINT(1) NOT NULL DEFAULT 1;


INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (120, 'PackUnpackTransaction');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 120, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);

/*****************************
**	Added on Dec 01, 2008 for cas count
**	Lemuel E. Aceron
*****************************/

ALTER TABLE tblCashierReport ADD `IsCashCountInitialized` TINYINT(1) NOT NULL DEFAULT 0; 

 
 /******************************************************************
 **	Added on Dec 09, 2008 for OrderSlipPrinter
 **	Lemuel E. Aceron
 **
 ** OrderSlipPrinter Types
 ** 0 = Print to 1st printer PrinterName=RetailPlusOSPrinter1
 ** 1 = Print to 2nd printer PrinterName=RetailPlusOSPrinter2
 ** 2 = Print to 2nd printer PrinterName=RetailPlusOSPrinter3
 ** 3 = print to 4th printer PrinterName=RetailPlusOSPrinter4
 ** 4 = print to 5th printer PrinterName=RetailPlusOSPrinter5
 
 ******************************************************************/
 
 ALTER TABLE tblProductGroup ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 
 ALTER TABLE tblTerminal ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;

 ALTER TABLE tblTransactionItems01 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems02 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems03 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems04 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems05 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems06 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems07 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems08 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems09 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems10 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems11 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems12 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 
 /******************************************************************
 **	Added on Dec 18, 2008 for OrderSlipPrinted
 **	Lemuel E. Aceron
 **
 ** OrderSlipPrinted Types
 ** 0 = not yet printed
 ** 1 = printed
 
 ******************************************************************/
 ALTER TABLE tblTransactionItems01 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems02 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems03 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems04 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems05 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems06 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems07 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems08 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems09 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems10 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems11 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems12 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0; 
 
 /*****************************
**	Added on December 26, 2008 Total Stock Report
**	Lemuel E. Aceron
*****************************/ 

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (121, 'TotalStockReport');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 121, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 121, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 121, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 121, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 121, 1, 1);


INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (122, 'ItemSetupFinancial');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (123, 'APLinkConfig');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
 
ALTER TABLE tblProducts ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblProductGroup ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0; 

ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0; 
 
ALTER TABLE tblERPConfig add ChartOfAccountIDAPTracking INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPBills INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPFreight INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPVDeposit INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPContra INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPLatePayment INT(4) UNSIGNED NOT NULL DEFAULT 0;
 
 /**************************************************************
** February 8, 2009
** Lemuel E. Aceron
**
** 1.For accounting entries
**	 
**
**************************************************************/
ALTER TABLE tblPO ADD POFreight DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD PODeposit DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPTracking INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPBills INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPFreight INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPVDeposit INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPContra INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPLatePayment INT(4) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPOItems ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;


  /**************************************************************
  ** February 7. 2009
  ** Lemuel E. Aceron
  **
  ** 1.Add OrderSlipPrinter for printing PLU Report group by
  **   OrderSlipPrinter
  **
  **************************************************************/
  ALTER TABLE tblPLUReport ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
  
  INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (124, 'ReprintZRead');
  INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (125, 'PLUReportPerOrderSlipPrinter');

  INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 124, 1, 1);
  INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 125, 1, 1);
  INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 124, 1, 1);
  INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 125, 1, 1);

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

/**************************************************************

procGenerateSalesPerItem
Lemuel E. Aceron

select productcode, purchaseprice, quantity, PurchaseAmount, Amount
from tbltransactionitems12 
where PurchaseAmount <> (quantity*purchaseprice)
AND transactionitemstatus = 0
LIMIT 10;

UPDATE tbltransactionitems01 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems02 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems03 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems04 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems05 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems06 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems07 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems08 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems09 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems10 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems11 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;
UPDATE tbltransactionitems12 SET PurchaseAmount = Quantity * purchaseprice WHERE TransactionItemStatus = 0;

CALL procGenerateSalesPerItem('1', null, null, null, null, null, null);
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItem
GO

create procedure procGenerateSalesPerItem(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems01 a 
	INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems02 a 
	INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems03 a 
	INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems04 a 
	INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems05 a 
	INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems06 a 
	INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems07 a 
	INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems08 a 
	INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems09 a 
	INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems10 a 
	INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems11 a 
	INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem
	SELECT strSessionID,
		IF(NOT ISNULL(MatrixDescription), CONCAT(ProductCode,'-',MatrixDescription), ProductCode) 'ProductCode',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,PurchaseAmount)) PurchaseAmount
	FROM tblTransactionItems12 a 
	INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
END;
GO
delimiter ;


/********************************************
Lemuel E. Aceron

Cater the requirement of RLC

Added procedure procTerminalReportInitializeZRead - called during initialization of ZREAD.

********************************************/
ALTER TABLE tblProducts ADD `IsItemSold` TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE tblTerminalReport ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `BatchCounter` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblTerminalReportHistory ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `BatchCounter` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblCashierReport ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReportHistory ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

/********************************************
procTerminalReportInitializeZRead
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportInitializeZRead
GO

create procedure procTerminalReportInitializeZRead(IN strTerminalNo varchar(10), IN decTrustFund decimal(10,2))
BEGIN

	INSERT INTO tblTerminalReportHistory (
					TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, ZReadCount, 
					XReadCount, GrossSales, TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, VATableAmount, 
					NonVATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfTotalTransactions, 
					DateLastInitialized, TrustFund ) 
				(SELECT 
					TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, ZReadCount, 
					XReadCount, GrossSales, TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, VATableAmount, 
					NonVATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfTotalTransactions, 
					DateLastInitialized, @TrustFund FROM tblTerminalReport WHERE TerminalNo = strTerminalNo);
					
	UPDATE tblTerminalReport SET OldGrandTotal =  NewGrandTotal WHERE TerminalNo = strTerminalNo;
	
	UPDATE tblTerminalReport SET 
					BeginningTransactionNo				=  EndingTransactionNo, 
					GrossSales							=  0, 
					TotalDiscount						=  0, 
					TotalCharge							=  0, 
					DailySales							=  0, 
					QuantitySold						=  0, 
					GroupSales							=  0, 
					VATableAmount						=  0, 
					NonVaTableAmount					=  0, 
					VAT									=  0, 
					EVATableAmount						=  0, 
					NonEVaTableAmount					=  0, 
					EVAT								=  0, 
					LocalTax							=  0, 
					CashSales							=  0, 
					ChequeSales							=  0, 
					CreditCardSales						=  0, 
					CreditSales							=  0, 
					CreditPayment						=  0, 
					DebitPayment						=  0, 
					CashInDrawer						=  0, 
					TotalDisburse						=  0, 
					CashDisburse						=  0, 
					ChequeDisburse						=  0, 
					CreditCardDisburse					=  0, 
					TotalWithhold						=  0, 
					CashWithhold						=  0, 
					ChequeWithhold						=  0, 
					CreditCardWithhold					=  0, 
					TotalPaidOut						=  0, 
					CashPaidOut							=  0,
					ChequePaidOut						=  0,
					CreditCardPaidOut					=  0,
					TotalDeposit						=  0, 
					CashDeposit							=  0, 
					ChequeDeposit						=  0, 
					CreditCardDeposit					=  0, 
					BeginningBalance					=  0, 
					VoidSales							=  0, 
					RefundSales							=  0, 
					ItemsDiscount						=  0, 
					SubTotalDiscount					=  0, 
					NoOfCashTransactions				=  0, 
					NoOfChequeTransactions				=  0, 
					NoOfCreditCardTransactions			=  0, 
					NoOfCreditTransactions				=  0, 
					NoOfCombinationPaymentTransactions	=  0, 
					NoOfCreditPaymentTransactions		=  0, 
					NoOfDebitPaymentTransactions		=  0, 
					NoOfClosedTransactions				=  0, 
					NoOfRefundTransactions				=  0, 
					NoOfVoidTransactions				=  0, 
					NoOfTotalTransactions				=  0, 
					NoOfDiscountedTransactions			=  0,
					NegativeAdjustments					=  0,
					NoOfNegativeAdjustmentTransactions	=  0,
					PromotionalItems					=  0,
					CreditSalesTax						=  0,
					BatchCounter						=  1,
					DateLastInitialized					=  NOW() 
			WHERE TerminalNo = strTerminalNo;
			
	
	INSERT INTO tblCashierReportHistory (
					CashierID, TerminalID, TerminalNo, GrossSales, 
					TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, VAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, DebitPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfTotalTransactions, 
					CashCount, LastLoginDate )
				(SELECT 
					CashierID, TerminalID, TerminalNo, GrossSales, 
					TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, VAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, DebitPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfTotalTransactions, 
					CashCount, LastLoginDate FROM tblCashierReport WHERE TerminalNo = strTerminalNo);
					
	UPDATE tblCashierReport SET 
			GrossSales							=  0, 
			TotalDiscount						=  0, 
			TotalCharge							=  0, 
			DailySales							=  0, 
			QuantitySold						=  0, 
			GroupSales							=  0, 
			VAT									=  0, 
			LocalTax							=  0, 
			CashSales							=  0, 
			ChequeSales							=  0, 
			CreditCardSales						=  0, 
			CreditSales							=  0, 
			CreditPayment						=  0, 
			DebitPayment						=  0, 
			CashInDrawer						=  0, 
			TotalDisburse						=  0, 
			CashDisburse						=  0, 
			ChequeDisburse						=  0, 
			CreditCardDisburse					=  0, 
			TotalWithhold						=  0, 
			CashWithhold						=  0, 
			ChequeWithhold						=  0, 
			CreditCardWithhold					=  0, 
			TotalPaidOut						=  0, 
			CashPaidOut							=  0,
			ChequePaidOut						=  0,
			CreditCardPaidOut					=  0,
			TotalDeposit						=  0, 
			CashDeposit							=  0, 
			ChequeDeposit						=  0, 
			CreditCardDeposit					=  0, 
			BeginningBalance					=  0, 
			VoidSales							=  0, 
			RefundSales							=  0, 
			ItemsDiscount						=  0, 
			SubTotalDiscount					=  0, 
			NoOfCashTransactions				=  0, 
			NoOfChequeTransactions				=  0, 
			NoOfCreditCardTransactions			=  0, 
			NoOfCreditTransactions				=  0, 
			NoOfCombinationPaymentTransactions	=  0, 
			NoOfCreditPaymentTransactions		=  0, 
			NoOfDebitPaymentTransactions		=  0, 
			NoOfClosedTransactions				=  0, 
			NoOfRefundTransactions				=  0, 
			NoOfVoidTransactions				=  0, 
			NoOfTotalTransactions				=  0, 
			NoOfDiscountedTransactions			=  0,
			NegativeAdjustments					=  0,
			NoOfNegativeAdjustmentTransactions	=  0,
			PromotionalItems					=  0,
			CreditSalesTax						=  0,
			CashCount							=  0 
	WHERE TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
procTerminalReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportUpdateTransactionSales
GO

create procedure procTerminalReportUpdateTransactionSales(IN strTerminalNo varchar(10), 
														IN decGrossSales decimal(10,2),
														IN decTotalDiscount decimal(10,2),
														IN decTotalCharge decimal(10,2),
														IN decDailySales decimal(10,2),
														IN decQuantitySold decimal(10,2),
														IN decGroupSales decimal(10,2),
														IN decOldGrandTotal decimal(10,2),
														IN decNewGrandTotal decimal(10,2),
														IN decVATableAmount decimal(10,2),
														IN decNonVaTableAmount decimal(10,2),
														IN decVAT decimal(10,2),
														IN decEVATableAmount decimal(10,2),
														IN decNonEVaTableAmount decimal(10,2),
														IN decEVAT decimal(10,2),
														IN decLocalTax decimal(10,2),
														IN decCashSales decimal(10,2),
														IN decChequeSales decimal(10,2),
														IN decCreditCardSales decimal(10,2),
														IN decCreditSales decimal(10,2),
														IN decCreditPayment decimal(10,2),
														IN decDebitPayment decimal(10,2),
														IN decCashInDrawer decimal(10,2),
														IN decVoidSales decimal(10,2),
														IN decRefundSales decimal(10,2),
														IN decItemsDiscount decimal(10,2),
														IN decSubTotalDiscount decimal(10,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(10),
														IN intNoOfNegativeAdjustmentTransactions  int(10),
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN

	UPDATE tblTerminalReport SET 
					GrossSales							=  GrossSales							+  decGrossSales, 
					TotalDiscount						=  TotalDiscount						+  decTotalDiscount, 
					TotalCharge							=  TotalCharge							+  decTotalCharge, 
					DailySales							=  DailySales							+  decDailySales, 
					QuantitySold						=  QuantitySold							+  decQuantitySold, 
					GroupSales							=  GroupSales							+  decGroupSales, 
					OldGrandTotal						=  OldGrandTotal						+  decOldGrandTotal, 
					NewGrandTotal						=  NewGrandTotal						+  decNewGrandTotal, 
					VATableAmount						=  VATableAmount						+  decVATableAmount, 
					NonVaTableAmount					=  NonVaTableAmount						+  decNonVaTableAmount, 
					VAT									=  VAT									+  decVAT, 
					EVATableAmount						=  EVATableAmount						+  decEVATableAmount, 
					NonEVaTableAmount					=  NonEVaTableAmount					+  decNonEVaTableAmount, 
					EVAT								=  EVAT									+  decEVAT, 
					LocalTax							=  LocalTax								+  decLocalTax, 
					CashSales							=  CashSales							+  decCashSales, 
					ChequeSales							=  ChequeSales							+  decChequeSales, 
					CreditCardSales						=  CreditCardSales						+  decCreditCardSales, 
					CreditSales							=  CreditSales							+  decCreditSales, 
					CreditPayment						=  CreditPayment						+  decCreditPayment, 
					DebitPayment						=  DebitPayment						    +  decDebitPayment, 
					CashInDrawer						=  CashInDrawer							+  decCashInDrawer, 
					VoidSales							=  VoidSales							+  decVoidSales, 
					RefundSales							=  RefundSales							+  decRefundSales, 
					ItemsDiscount						=  ItemsDiscount						+  decItemsDiscount, 
					SubTotalDiscount					=  SubTotalDiscount						+  decSubTotalDiscount, 
					NoOfCashTransactions				=  NoOfCashTransactions					+  intNoOfCashTransactions, 
					NoOfChequeTransactions				=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
					NoOfCreditCardTransactions			=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
					NoOfCreditTransactions				=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions	=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions		=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
					NoOfDebitPaymentTransactions		=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
					NoOfClosedTransactions				=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
					NoOfRefundTransactions				=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 
					NoOfVoidTransactions				=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
					NoOfTotalTransactions				=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
					NoOfDiscountedTransactions			=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
                    NegativeAdjustments					=  NegativeAdjustments					+  decNegativeAdjustments,
                    NoOfNegativeAdjustmentTransactions	=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,
                    PromotionalItems					=  PromotionalItems						+  decPromotionalItems,
                    CreditSalesTax						=  CreditSalesTax						+  decCreditSalesTax
	WHERE TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
procTerminalReportIncrementBatchCounter
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportIncrementBatchCounter
GO

create procedure procTerminalReportIncrementBatchCounter(IN strTerminalNo varchar(10))
BEGIN

	UPDATE tblTerminalReport SET 
				BatchCounter	=  BatchCounter	+  1
	WHERE TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
procCashierReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashierReportUpdateTransactionSales
GO

create procedure procCashierReportUpdateTransactionSales(IN strTerminalNo varchar(10), IN lngCashierID int(10),
														IN decGrossSales decimal(10,2),
														IN decTotalDiscount decimal(10,2),
														IN decTotalCharge decimal(10,2),
														IN decDailySales decimal(10,2),
														IN decQuantitySold decimal(10,2),
														IN decGroupSales decimal(10,2),
														IN decVAT decimal(10,2),
														IN decLocalTax decimal(10,2),
														IN decCashSales decimal(10,2),
														IN decChequeSales decimal(10,2),
														IN decCreditCardSales decimal(10,2),
														IN decCreditSales decimal(10,2),
														IN decCreditPayment decimal(10,2),
														IN decDebitPayment decimal(10,2),
														IN decCashInDrawer decimal(10,2),
														IN decVoidSales decimal(10,2),
														IN decRefundSales decimal(10,2),
														IN decItemsDiscount decimal(10,2),
														IN decSubTotalDiscount decimal(10,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(10),
														IN intNoOfNegativeAdjustmentTransactions  int(10),
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN
	UPDATE tblCashierReport SET 
		GrossSales								=  GrossSales							+  decGrossSales, 
		TotalDiscount							=  TotalDiscount						+  decTotalDiscount, 
		TotalCharge								=  TotalCharge							+  decTotalCharge, 
		DailySales								=  DailySales							+  decDailySales, 
		QuantitySold							=  QuantitySold							+  decQuantitySold, 
		GroupSales								=  GroupSales							+  decGroupSales, 
		VAT										=  VAT									+  decVAT, 
		LocalTax								=  LocalTax								+  decLocalTax, 
		CashSales								=  CashSales							+  decCashSales, 
		ChequeSales								=  ChequeSales							+  decChequeSales, 
		CreditCardSales							=  CreditCardSales						+  decCreditCardSales, 
		CreditSales								=  CreditSales							+  decCreditSales, 
		CreditPayment							=  CreditPayment						+  decCreditPayment, 
		DebitPayment							=  DebitPayment						   	+  decDebitPayment, 
		CashInDrawer							=  CashInDrawer							+  decCashInDrawer, 
		VoidSales								=  VoidSales							+  decVoidSales, 
		RefundSales								=  RefundSales							+  decRefundSales, 
		ItemsDiscount							=  ItemsDiscount						+  decItemsDiscount, 
		SubTotalDiscount						=  SubTotalDiscount						+  decSubTotalDiscount, 
		NoOfCashTransactions					=  NoOfCashTransactions					+  intNoOfCashTransactions, 
		NoOfChequeTransactions					=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
		NoOfCreditCardTransactions				=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
		NoOfCreditTransactions					=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
		NoOfCombinationPaymentTransactions		=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
		NoOfCreditPaymentTransactions			=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
		NoOfDebitPaymentTransactions			=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
		NoOfClosedTransactions					=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
		NoOfRefundTransactions					=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 			
		NoOfVoidTransactions					=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
		NoOfTotalTransactions					=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
		NoOfDiscountedTransactions				=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
        NegativeAdjustments						=  NegativeAdjustments					+  decNegativeAdjustments,
        NoOfNegativeAdjustmentTransactions		=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,
        PromotionalItems						=  PromotionalItems						+  decPromotionalItems,
        CreditSalesTax							=  CreditSalesTax						+  decCreditSalesTax
	WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID;
	
	
END;
GO
delimiter ;

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

/**************************************************************

procGenerateProductHistory
Lemuel E. Aceron

CALL procGenerateProductHistory('1', null, null, 2 );
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistory
GO

create procedure procGenerateProductHistory(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime,
	IN lngProductID BIGINT
	)
BEGIN
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE 1=1 
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;

	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems01 a INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems02 a INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems03 a INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
		
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems04 a INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
		
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems05 a INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems06 a INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems07 a INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems08 a INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
		
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems09 a INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
		
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems10 a INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
		
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems11 a INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems12 a INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND a.ProductID = lngProductID;
	
	
END;
GO
delimiter ;
 
 ALTER TABLE tblProducts ADD `WillPrintProductComposition` TINYINT(1) NOT NULL DEFAULT 1;
 
 /******************************************************************************
**		File: accounts.sql
**		Name: accounts
**		Desc: accounts
**
**		Auth: Lemuel E. Aceron
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/ 


/*****************************
**	tblPO
*****************************/
DROP TABLE IF EXISTS tblPOItems;
DROP TABLE IF EXISTS tblPO;
CREATE TABLE tblPO (
	`POID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PONo` VARCHAR(30) NOT NULL,
	`PODate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`SupplierCode` VARCHAR(25) NOT NULL,
	`SupplierContact` VARCHAR(75) NOT NULL,
	`SupplierAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`SupplierTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`SupplierModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`SupplierTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`PurchaserID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PurchaserName` VARCHAR(100),
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
	PRIMARY KEY (POID),
	INDEX `IX_tblPO`(`POID`),
	UNIQUE `PK_tblPO`(`PONo`),
	INDEX `IX1_tblPO`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPO`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblPO`(`PurchaserID`),
	FOREIGN KEY (`PurchaserID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Purchase Orders';

/*****************************
** Added July 27, 2008
** Lemuel E. Aceron
** To cater payments
**
** PaymentStatus
**	0 - Unpaid
**	1 - Partially Paid
**	2 - Fully Paid
*****************************/
--ALTER TABLE tblPO ADD `UnpaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0;
--	update tblPO SET UnpaidAmount = POSubTotal;
	
--ALTER TABLE tblPO ADD `PaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0;
--ALTER TABLE tblPO ADD `PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0;

/*****************************
**	tblPOItems
*****************************/
DROP TABLE IF EXISTS tblPOItems;
CREATE TABLE tblPOItems (
	`POItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`POID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPO(`POID`),
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
	`POItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`ChartOfAccountIDPurchase` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxPurchase` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`POItemID`),
	INDEX `IX_tblPOItems`(`POItemID`),
	INDEX `IX0_tblPOItems`(`POID`, `ProductID`),
	INDEX `IX1_tblPOItems`(`POID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblPOItems`(`ProductCode`),
	INDEX `IX3_tblPOItems`(`POID`),
	INDEX `IX4_tblPOItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'PO Items';

/*****************************
**	tblInventory
*****************************/
DROP TABLE IF EXISTS tblInventory;
CREATE TABLE tblInventory (
	`InventoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PostingDateFrom` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`PostingDateTo` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`ReferenceNo` VARCHAR(30) NOT NULL,
	`ContactID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`ContactCode` VARCHAR(25) NOT NULL,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`PurchaseQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PurchaseCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PurchaseVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PInvoiceQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PInvoiceCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PInvoiceVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PReturnQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PReturnCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PReturnVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PDebitQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PDebitCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PDebitVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SoldQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SoldCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SoldVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SReturnQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SReturnCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SReturnVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`InvAdjustmentVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ClosingQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ClosingCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ClosingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`InventoryID`),
	INDEX `IX_tblInventory`(`InventoryID`),
	INDEX `IX1_tblInventory`(`PostingDateFrom`, `PostingDateTo`),
	INDEX `IX2_tblInventory`(`ReferenceNo`),
	INDEX `IX3_tblInventory`(`ContactID`),
	FOREIGN KEY (`ContactID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX4_tblInventory`(`ProductCode`)
)
TYPE=INNODB COMMENT = 'Inventory';

/*****************************
**	tblERPConfig
*****************************/
DROP TABLE IF EXISTS tblERPConfig;
CREATE TABLE tblERPConfig (
	`LastPONo` VARCHAR(10) NOT NULL,
	`LastPOReturnNo` VARCHAR(10) NOT NULL,
	`LastDebitMemoNo` VARCHAR(10) NOT NULL,
	`LastSONo` VARCHAR(10) NOT NULL,
	`LastSOReturnNo` VARCHAR(10) NOT NULL,
	`LastCreditMemoNo` VARCHAR(10) NOT NULL,
	`LastTransferInNo` VARCHAR(10) NOT NULL,
	`LastTransferOutNo` VARCHAR(10) NOT NULL,
	`LastInvAdjustmentNo` VARCHAR(10) NOT NULL,
	`LastClosingNo` VARCHAR(10) NOT NULL,
	`PostingDateFrom` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`PostingDateTo` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`ChartOfAccountIDAPTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPBills` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPContra` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDAPLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	INDEX `IX_tblERPConfig`(`LastPONo`)
)
TYPE=INNODB COMMENT = 'ERP Configurations';

INSERT INTO tblERPConfig (LastPONo, LastPOReturnNo, LastDebitMemoNo, LastSONo, LastSOReturnNo, LastCreditMemoNo, LastTransferInNo, LastTransferOutNo, LastInvAdjustmentNo, LastClosingNo, PostingDateFrom, PostingDateTo) VALUES ('0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '2007-08-01 12:00:00', '2007-08-31 12:00:00');
--alter table tblERPConfig add `LastDebitMemoNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastDebitMemoNo` = '0000000001';
--alter table tblERPConfig add `LastSOReturnNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastSOReturnNo` = '0000000001';
--alter table tblERPConfig add `LastCreditMemoNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastCreditMemoNo` = '0000000001';
--alter table tblERPConfig add `LastTransferInNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastTransferInNo` = '0000000001';
--alter table tblERPConfig add `LastTransferOutNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastTransferOutNo` = '0000000001';
--alter table tblERPConfig add `LastInvAdjustmentNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastInvAdjustmentNo` = '0000000001';
--alter table tblERPConfig add `LastClosingNo` VARCHAR(10) NOT NULL;
--update tblERPConfig set `LastClosingNo` = '0000000001';

DROP TABLE IF EXISTS tblPaymentPODetails;
DROP TABLE IF EXISTS tblPaymentDebit;
DROP TABLE IF EXISTS tblPaymentCredit;
DROP TABLE IF EXISTS tblChartOfAccount;
DROP TABLE IF EXISTS tblAccountCategory;
DROP TABLE IF EXISTS tblAccountSummary;
/*****************************
**	tblAccountClassification
**
**	ClassTypes:
**		0 = Balance Sheet
**		1 = Income Statement
*****************************/
DROP TABLE IF EXISTS tblAccountClassification;
CREATE TABLE tblAccountClassification (
	`AccountClassificationID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountClassificationCode` VARCHAR(15) NOT NULL,
	`AccountClassificationName` VARCHAR(15) NOT NULL,
	`AccountClassificationType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`AccountClassificationID`),
	INDEX `IX_tblAccountClassification`(`AccountClassificationID`),
	INDEX `IX1_tblAccountClassification`(`AccountClassificationCode`),
	UNIQUE `PK_tblAccountClassification`(`AccountClassificationCode`),
	INDEX `IX2_tblAccountClassification`(`AccountClassificationType`)
)
TYPE=INNODB COMMENT = 'Account Classification';

/*!40000 ALTER TABLE `sysAccessTypes` DISABLE KEYS */; 
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(1,'1-0000','ASSET',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(2,'2-0000','LIABILITY',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(3,'3-0000','EQUITY',0);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(4,'4-0000','INCOME',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(5,'5-0000','COST OF SALES',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(6,'6-0000','EXPENSE',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(7,'7-0000','OTHER INCOME',1);
INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType)VALUES(8,'8-0000','OTHER EXPENSE',1);
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

/*****************************
**	tblAccountSummaries
*****************************/
DROP TABLE IF EXISTS tblAccountSummary;
CREATE TABLE tblAccountSummary (
	`AccountSummaryID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountClassificationID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblAccountClassification(`AccountClassificationID`),
	`AccountSummaryCode` VARCHAR(30) NOT NULL,
	`AccountSummaryName` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`AccountSummaryID`),
	INDEX `IX_tblAccountSummary`(`AccountSummaryID`),
	INDEX `IX1_tblAccountSummary`(`AccountSummaryID`, `AccountSummaryName`),
	UNIQUE `PK_tblAccountSummary`(`AccountSummaryName`),
	INDEX `IX2_tblAccountSummary`(`AccountClassificationID`)
)
TYPE=INNODB COMMENT = 'Account Summary';

--select CONCAT('INSERT INTO tblAccountSummary(AccountSummaryCode, AccountSummaryName)VALUES(''',AccountSummaryCode,''',','''', AccountSummaryName,''')') from tblAccountSummary;
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(100-199)','CURRENT ASSET');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(200-299)','LONG TERM ASSET');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(1,'(300-399)','OTHER ASSETS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(2,'(400-499)','CURRENT LIABILITY');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(2,'(500-599)','LONG-TERM LIABILITY');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(3,'(600-619)','CAPITAL');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(3,'(610-699)','PROFIT AND LOSS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(4,'(700-799)','NET INCOME/NET LOSS');
INSERT INTO tblAccountSummary(AccountClassificationID, AccountSummaryCode, AccountSummaryName)VALUES(4,'(800-899)','PROVISION FOR INCOME TAX');


/*****************************
**	tblAccountCategory
*****************************/
DROP TABLE IF EXISTS tblAccountCategory;
CREATE TABLE tblAccountCategory (
	`AccountCategoryID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountSummaryID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblAccountSummary(`AccountSummaryID`),
	`AccountCategoryCode` VARCHAR(30) NOT NULL,
	`AccountCategoryName` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`AccountCategoryID`),
	INDEX `IX_tblAccountCategory`(`AccountCategoryID`),
	INDEX `IX1_tblAccountCategory`(`AccountCategoryID`, `AccountCategoryName`),
	UNIQUE `PK_tblAccountCategory`(`AccountCategoryName`),
	INDEX `IX2_tblAccountCategory`(`AccountSummaryID`),
	FOREIGN KEY (`AccountSummaryID`) REFERENCES tblAccountSummary(`AccountSummaryID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Account Category';
--select CONCAT('INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(',AccountSummaryID,',''',AccountCategoryCode,''',','''', AccountCategoryName,''')') from tblAccountCategory;
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(1,'(100-101)','PETTY CASH');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(1,'(150-151)','CASH IN BANK - {BANK ACCOUNT 1}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(2,'(200-249)','TRADE RECIEVABLE - {PUT CUSTOMER ACCOUNT}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(2,'(250-251)','LOAN RECIVABLE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(300-301)','BUILDING');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(302-303)','EQUIPMENTS');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(3,'(304-305)','FURNITURES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(400-449)','PRE-PAYMENTS - {ACC NO}');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(450-453)','INPUT TAX');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(4,'(454-455)','TAX REFUND');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(5,'(500-501)','TRADE-PAYABLE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(6,'(600-609)','CAPITAL A');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(6,'(610-619)','CAPITAL B');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(700-749)','REVENUE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(750-751)','SALES RETURN & ALLOWANCES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(752-753)','BEG. INVENTORY');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(754-755)','PURCHASES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(756-757)','PURCHASE RETURN AND ALLOWANCES');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(758-759)','ENDING INVENTORY');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(760-761)','INCOME AND EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(762-789)','ADMINISTRATIVE AND OPERATING EXPENCE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(790-791)','OTHER OPERATING INCOME & EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(7,'(792-799)','NON OPERATING INCOME AND EXPENSE');
INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(8,'(800-849)','PROVISION FOR INCOME TAX');

/*****************************
**	tblChartOfAccount
*****************************/
DROP TABLE IF EXISTS tblChartOfAccount;
CREATE TABLE tblChartOfAccount (
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`AccountCategoryID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblAccountCategory(`AccountCategoryID`),
	`ChartOfAccountCode` VARCHAR(30) NOT NULL,
	`ChartOfAccountName` VARCHAR(50) NOT NULL,
	`Debit` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Credit` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`ChartOfAccountID`),
	INDEX `IX_tblChartOfAccount`(`ChartOfAccountID`),
	UNIQUE `PK_tblChartOfAccount`(`ChartOfAccountCode`),
	INDEX `IX1_tblChartOfAccount`(`ChartOfAccountCode`),
	INDEX `IX2_tblChartOfAccount`(`ChartOfAccountName`),
	INDEX `IX3_tblChartOfAccount`(`AccountCategoryID`),
	FOREIGN KEY (`AccountCategoryID`) REFERENCES tblAccountCategory(`AccountCategoryID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Chart Of Account';
--select CONCAT('INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(',AccountCategoryID,',''',ChartOfAccountCode,''',','''', ChartOfAccountName,''')') from tblChartOfAccount;
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(1,'100','PETTY CASH');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(2,'150','CASH IN BANK - LANDBANK');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(3,'200','TRADE RECIEVABLE - {ACCOUNT}');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(4,'250','LOAN RECIVABLE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(5,'300','BUILDING');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(6,'302','EQUIPMENTS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(7,'304','FURNITURES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(8,'400','PRE-PAYMENTS - {ACC NO}');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(9,'450','INPUT TAX');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(10,'454','TAX REFUND');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(11,'500','TRADE-PAYABLE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(12,'600','CAPITAL A');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(13,'610','CAPITAL B');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(14,'700','SALES - BRANCH 1');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(14,'701','SALES - BRANCH 2');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(15,'750','SALES RETURN & ALLOWANCES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(16,'752','BEG. INVENTORY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(17,'754','PURCHASES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(18,'756','PURCHASE RETURN AND ALLOWANCES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(19,'758','ENDING INVENTORY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(20,'760','INCOME AND EXPENSE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'762','ADVERTISMENT');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'763','COMMISSION EXPENSES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'764','PACKAGING');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'765','SALES PROMOTION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'766','STORES SUPPLIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'767','DIRECTORS FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'768','BONUS ');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'769','CPF& PAYROLL TAX');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'770','MEDICAL');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'771','SALARIES & WAGES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'772','STAFF AMENITIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'773','MANAGEMENT & ADMIN FEES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'774','SECRETARIAL FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'775','AUDIT FEE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'776','ENTERTAINMENT & REPRESENTATION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'777','INSURANCE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'778','LEGAL & STAMP DUTIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'779','REPAIR & MAINTENANCE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'780','STORE & OFFICE SUPPLIES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'781','PRINTING ');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'782','PROFESSIONAL FESS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'783','RENT EXPENSE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'784','SUBSCRIPTION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'785','SECURITY');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'786','TRANSPORTATION');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'787','TELEX/TELEPHONE/POSTAGE');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(21,'788','MISCELLANEOUS');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(22,'790','OTHER OPERATING INCOME');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(23,'792','INTEREST INCOME');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(23,'793','BANK CHARGES');
INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(24,'800','PROVISION FOR INCOME TAX');

/*****************************
**	tblBank
*****************************/
DROP TABLE IF EXISTS tblBank;
CREATE TABLE tblBank (
	`BankID` INT(5) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BankCode` VARCHAR(10) NOT NULL,
	`BankName` VARCHAR(50) NOT NULL,
	`ChequeCode` VARCHAR(5) NOT NULL,
	`ChequeCounter` VARCHAR(20) NOT NULL,
	PRIMARY KEY (`BankID`),
	INDEX `IX_tblBanks`(`BankID`, `BankCode`, `BankName`),
	UNIQUE `PK_tblBanks`(`BankCode`),
	INDEX `IX1_tblBanks`(`BankID`),
	INDEX `IX2_tblBanks`(`BankCode`),
	INDEX `IX3_tblBanks`(`BankName`)
)
TYPE=INNODB COMMENT = 'Bank Accounts';

/*!40000 ALTER TABLE `tblBank` DISABLE KEYS */; 
INSERT INTO tblBank (BankID, BankCode, BankName, ChequeCode, ChequeCounter) VALUES 
					(1, 'DEFAULT', 'DEFAULT BANK', '', '00000000000000');
/*!40000 ALTER TABLE `tblBank` ENABLE KEYS */; 


/*****************************
**	tblPayment
*****************************/
DROP TABLE IF EXISTS tblPayment;
CREATE TABLE tblPayment (
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BankID` INT(5) UNSIGNED NOT NULL DEFAULT 1 REFERENCES tblBank(`BankID`),
	`BankCode` VARCHAR(10) NOT NULL,
	`ChequeDate` DATE NOT NULL DEFAULT '0001-01-01',
	`ChequeNo` VARCHAR(30) NOT NULL,
	`PayeeID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`PayeeCode` VARCHAR(30) NOT NULL,
	`PayeeName` VARCHAR(50) NOT NULL,
	`TotalDebitAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TotalCreditAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Particulars` VARCHAR(150) NOT NULL,
	`Status` SMALLINT NOT NULL DEFAULT 0,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',	
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',		
	PRIMARY KEY (`PaymentID`),
	INDEX `IX_tblPayment`(`PaymentID`),
	UNIQUE `PK_tblPayment`(`ChequeNo`),
	INDEX `IX1_tblPayment`(`ChequeDate`),
	INDEX `IX2_tblPayment`(`ChequeNo`),
	INDEX `IX3_tblPayment`(`PayeeID`),
	FOREIGN KEY (`PayeeID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX4_tblPayment`(`PayeeName`)
)
TYPE=INNODB COMMENT = 'Financial Payments';

/*****************************
**	tblPaymentPODetails
*****************************/
DROP TABLE IF EXISTS tblPaymentPODetails;
CREATE TABLE tblPaymentPODetails (
	`PaymentDetailID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`POID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentDetailID`),
	INDEX `IX_tblPaymentPODetails`(`PaymentDetailID`),
	UNIQUE `PK_tblPaymentPODetails`(`PaymentDetailID`),
	INDEX `IX1_tblPaymentPODetails`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentPODetails`(`POID`)
)
TYPE=INNODB COMMENT = 'Financial Purchase Payment Details';

/*****************************
**	tblPaymentDebit
*****************************/
DROP TABLE IF EXISTS tblPaymentDebit;
CREATE TABLE tblPaymentDebit (
	`PaymentDebitID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentDebitID`),
	INDEX `IX_tblPaymentDebit`(`PaymentDebitID`),
	UNIQUE `PK_tblPaymentDebit`(`PaymentDebitID`),
	INDEX `IX1_tblPaymentDebit`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentDebit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Financial Debit Payments';

/*****************************
**	tblPaymentCredit
*****************************/
DROP TABLE IF EXISTS tblPaymentCredit;
CREATE TABLE tblPaymentCredit (
	`PaymentCreditID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`PaymentID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPayment(`PaymentID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`PaymentCreditID`),
	INDEX `IX_tblPaymentCredit`(`PaymentCreditID`),
	UNIQUE `PK_tblPaymentCredit`(`PaymentCreditID`),
	INDEX `IX1_tblPaymentCredit`(`PaymentID`),
	FOREIGN KEY (`PaymentID`) REFERENCES tblPayment(`PaymentID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPaymentCredit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Financial Credit Payments';


/*****************************
**	tblPODebitMemo
*****************************/
DROP TABLE IF EXISTS tblPODebitMemo;
CREATE TABLE tblPODebitMemo (
	`DebitMemoID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`MemoNo` VARCHAR(30) NOT NULL,
	`MemoDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`SupplierCode` VARCHAR(25) NOT NULL,
	`SupplierContact` VARCHAR(75) NOT NULL,
	`SupplierAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`SupplierTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`SupplierModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`SupplierTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredPostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`PurchaserID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PurchaserName` VARCHAR(100),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`POReturnStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`DebitMemoStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDocNo` VARCHAR(30) NOT NULL,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
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
	PRIMARY KEY (`DebitMemoID`),
	INDEX `IX_tblPODebitMemo`(`DebitMemoID`),
	UNIQUE `PK_tblPODebitMemo`(`MemoNo`),
	INDEX `IX1_tblPODebitMemo`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblPODebitMemo`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblPODebitMemo`(`PurchaserID`),
	FOREIGN KEY (`PurchaserID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Purchase Debit Memo';

--alter table tblPODebitMemo add `CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
--alter table tblPODebitMemo add `CancelledRemarks` VARCHAR(150);
--alter table tblPODebitMemo add `CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;

/*****************************
**	tblPODebitMemoItems
*****************************/
DROP TABLE IF EXISTS tblPODebitMemoItems;
CREATE TABLE tblPODebitMemoItems (
	`DebitMemoItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`DebitMemoID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPODebitMemo(`DebitMemoID`),
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
	`ItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`ChartOfAccountIDPurchase` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxPurchase` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`DebitMemoItemID`),
	INDEX `IX_tblPODebitMemoItems`(`DebitMemoItemID`),
	INDEX `IX0_tblPODebitMemoItems`(`DebitMemoID`, `ProductID`),
	INDEX `IX1_tblPODebitMemoItems`(`DebitMemoID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblPODebitMemoItems`(`ProductCode`),
	INDEX `IX3_tblPODebitMemoItems`(`DebitMemoID`),
	INDEX `IX4_tblPODebitMemoItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'PO Debit Memo Items';

/*****************************
**	Added October 9, 2007
*****************************/
/*****************************
**	tblSO
*****************************/
DROP TABLE IF EXISTS tblSOItems;
DROP TABLE IF EXISTS tblSO;
/*****************************
**	tblSO
*****************************/
DROP TABLE IF EXISTS tblSOItems;
DROP TABLE IF EXISTS tblSO;
CREATE TABLE tblSO (
	`SOID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`SONo` VARCHAR(30) NOT NULL,
	`SODate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`CustomerCode` VARCHAR(25) NOT NULL,
	`CustomerContact` VARCHAR(75) NOT NULL,
	`CustomerAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`CustomerTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`CustomerModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`CustomerTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`SellerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`SOSubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SODiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SOVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SOVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SOStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`SORemarks` VARCHAR(150),
	`CustomerDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (SOID),
	INDEX `IX_tblSO`(`SOID`),
	UNIQUE `PK_tblSO`(`SONo`),
	INDEX `IX1_tblSO`(`CustomerID`),
	FOREIGN KEY (`CustomerID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblSO`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSO`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Sales Orders';

/*****************************
**	tblSOItems
*****************************/
DROP TABLE IF EXISTS tblSOItems;
CREATE TABLE tblSOItems (
	`SOItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`SOID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblSO(`SOID`),
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
	`SOItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`SOItemID`),
	INDEX `IX_tblSOItems`(`SOItemID`),
	INDEX `IX0_tblSOItems`(`SOID`, `ProductID`),
	INDEX `IX1_tblSOItems`(`SOID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOItems`(`ProductCode`),
	INDEX `IX3_tblSOItems`(`SOID`),
	INDEX `IX4_tblSOItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'SO Items';

/*****************************
**	tblSOCreditMemo
*****************************/
DROP TABLE IF EXISTS tblSOCreditMemo;
CREATE TABLE tblSOCreditMemo (
	`CreditMemoID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CNNo` VARCHAR(30) NOT NULL,
	`CNDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`CustomerCode` VARCHAR(25) NOT NULL,
	`CustomerContact` VARCHAR(75) NOT NULL,
	`CustomerAddress` VARCHAR(150) NOT NULL DEFAULT '',
	`CustomerTelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`CustomerModeOfTerms` INT(10) NOT NULL DEFAULT 0,
	`CustomerTerms` INT(10) NOT NULL DEFAULT 0,
	`RequiredPostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblBranch(`BranchID`),
	`SellerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SOReturnStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`CreditMemoStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`CustomerDocNo` VARCHAR(30) NOT NULL,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`CreditMemoID`),
	INDEX `IX_tblSOCreditMemo`(`CreditMemoID`),
	UNIQUE `PK_tblSOCreditMemo`(`CNNo`),
	INDEX `IX1_tblSOCreditMemo`(`CustomerID`),
	FOREIGN KEY (`CustomerID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblSOCreditMemo`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSOCreditMemo`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Sales Debit Memo';

/*****************************
**	tblSOCreditMemoItems
*****************************/
DROP TABLE IF EXISTS tblSOCreditMemoItems;
CREATE TABLE tblSOCreditMemoItems (
	`CreditMemoItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CreditMemoID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblSOCreditMemo(`CreditMemoID`),
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
	`ItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`CreditMemoItemID`),
	INDEX `IX_tblSOCreditMemoItems`(`CreditMemoItemID`),
	INDEX `IX0_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`),
	INDEX `IX1_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOCreditMemoItems`(`ProductCode`),
	INDEX `IX3_tblSOCreditMemoItems`(`CreditMemoID`),
	INDEX `IX4_tblSOCreditMemoItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'SO Credit Memo Items';

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
	`TransferredByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`TransferInSubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferInStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`TransferInRemarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (TransferInID),
	INDEX `IX_tblTransferIn`(`TransferInID`),
	UNIQUE `PK_tblTransferIn`(`TransferInNo`),
	INDEX `IX1_tblTransferIn`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblTransferIn`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblTransferIn`(`TransferredByID`),
	FOREIGN KEY (`TransferredByID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Purchase Orders';

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
	`TransferInItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`TransferInItemID`),
	INDEX `IX_tblTransferInItems`(`TransferInItemID`),
	INDEX `IX0_tblTransferInItems`(`TransferInID`, `ProductID`),
	INDEX `IX1_tblTransferInItems`(`TransferInID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblTransferInItems`(`ProductCode`),
	INDEX `IX3_tblTransferInItems`(`TransferInID`),
	INDEX `IX4_tblTransferInItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'TransferIn Items';

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
	`TransferredByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`TransferOutSubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TransferOutStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`TransferOutRemarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (TransferOutID),
	INDEX `IX_tblTransferOut`(`TransferOutID`),
	UNIQUE `PK_tblTransferOut`(`TransferOutNo`),
	INDEX `IX1_tblTransferOut`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblTransferOut`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblTransferOut`(`TransferredByID`),
	FOREIGN KEY (`TransferredByID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
)
TYPE=INNODB COMMENT = 'Purchase Orders';

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
	`TransferOutItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	PRIMARY KEY (`TransferOutItemID`),
	INDEX `IX_tblTransferOutItems`(`TransferOutItemID`),
	INDEX `IX0_tblTransferOutItems`(`TransferOutID`, `ProductID`),
	INDEX `IX1_tblTransferOutItems`(`TransferOutID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblTransferOutItems`(`ProductCode`),
	INDEX `IX3_tblTransferOutItems`(`TransferOutID`),
	INDEX `IX4_tblTransferOutItems`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'TransferOut Items'; 


 /*****************************
**	tblBranchInventory
*****************************/
DROP TABLE IF EXISTS tblBranchInventory;
CREATE TABLE tblBranchInventory (
	`BranchInventoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchID`),
	PRIMARY KEY (BranchInventoryID),
	INDEX `IX_tblBranchInventory`(`ProductCode`, `BranchID`),
	INDEX `IX1_tblBranchInventory`(`BranchID`)
)
TYPE=INNODB COMMENT = 'Branch Inventories'; 

/**************************************************************
** January 9, 2009
** Lemuel E. Aceron
**
** 1.For accounting entries
**	 
**
**************************************************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (122, 'ItemSetupFinancial');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (123, 'APLinkConfig');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);

ALTER TABLE tblProducts ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;

/*************************************************************************************************************

procPOSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decPODiscount, decPODiscountApplied, intPODiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procPOSynchronizeAmount;
delimiter GO

create procedure procPOSynchronizeAmount(IN lngPOID bigint(20))
BEGIN
	/*******************************   
	Just set the value of
		@ProductID to check if Qty is 
		correct base don history
	*******************************/
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intPODiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decPODiscount, decPODiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblPOItems WHERE POID = lngPOID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblPOItems WHERE POID = lngPOID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblPOItems WHERE POID = lngPOID AND Discount <> 0);
	
	SET decPODiscountApplied = (SELECT DiscountApplied FROM tblPO WHERE POID = lngPOID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount=0 THEN
				SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
			ELSE
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decPODiscountApplied = (SELECT DiscountApplied FROM tblPO WHERE POID = lngPOID);
	SET intPODiscountType = (SELECT DiscountType FROM tblPO WHERE POID = lngPOID);

	IF intPODiscountType = 1 and decTotalItemAmount >= decPODiscountApplied THEN
		SET decPODiscount = (SELECT decPODiscountApplied);
	ELSEIF intPODiscountType = 2 THEN
		SET decPODiscount = (SELECT (decSubTotalDiscountableAmount * (decPODiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decPODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decPODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblPO SET
		SubTotal = decTotalItemAmount - decPODiscount + Freight - Deposit,
		Discount = decPODiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decPODiscount + Freight - Deposit
	WHERE POID = lngPOID;
	
END;
GO
delimiter ;
/*************************************************************************************************************

procPODebitMemoSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decPODebitMemoDiscount, decPODebitMemoDiscountApplied, intPODebitMemoDiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPODebitMemoSynchronizeAmount
GO

create procedure procPODebitMemoSynchronizeAmount(IN lngDebitMemoID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intPODebitMemoDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decPODebitMemoDiscount, decPODebitMemoDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblPODebitMemoItems WHERE DebitMemoID = lngDebitMemoID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblPODebitMemoItems WHERE DebitMemoID = lngDebitMemoID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblPODebitMemoItems WHERE DebitMemoID = lngDebitMemoID AND Discount <> 0);
	
	SET decPODebitMemoDiscountApplied = (SELECT DiscountApplied FROM tblPODebitMemo WHERE DebitMemoID = lngDebitMemoID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount=0 THEN
				SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
			ELSE
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decPODebitMemoDiscountApplied = (SELECT DiscountApplied FROM tblPODebitMemo WHERE DebitMemoID = lngDebitMemoID);
	SET intPODebitMemoDiscountType = (SELECT DiscountType FROM tblPODebitMemo WHERE DebitMemoID = lngDebitMemoID);

	IF intPODebitMemoDiscountType = 1 and decTotalItemAmount >= decPODebitMemoDiscountApplied THEN
		SET decPODebitMemoDiscount = (SELECT decPODebitMemoDiscountApplied);
	ELSEIF intPODebitMemoDiscountType = 2 THEN
		SET decPODebitMemoDiscount = (SELECT (decSubTotalDiscountableAmount * (decPODebitMemoDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decPODebitMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decPODebitMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblPODebitMemo SET
		SubTotal = decTotalItemAmount - decPODebitMemoDiscount + Freight - Deposit,
		Discount = decPODebitMemoDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decPODebitMemoDiscount + Freight - Deposit
	WHERE DebitMemoID = lngDebitMemoID;
	
END;
GO
delimiter ;


delete from tblmatrixpackage;

delete from tblproductvariationsmatrix;

delete from tblproductbasevariationsmatrix;

DELETE FROM tblProductPackage WHERE ProductID <> 1;
INSERT INTO tblProductPackage (ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductID <> 1;

UPDATE tblProducts SET Quantity = 0;

UPDATE tblterminal SET MachineSerialNo = '000058';

UPDATE tblterminal SET AccreditationNo = '0409-050-47783-000';










