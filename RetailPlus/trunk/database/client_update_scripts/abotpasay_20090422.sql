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
					DateLastInitialized, decTrustFund FROM tblTerminalReport WHERE TerminalNo = strTerminalNo);
					
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
 
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (126, 'MallForwarder');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);


/********************************************
procTerminalReportInitializeZRead
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportInitializeZRead
GO

create procedure procTerminalReportInitializeZRead(IN strTerminalNo varchar(10), IN dteDateLastInitialized DateTime)
BEGIN
	DECLARE decTrustFund DECIMAL(10,2) DEFAULT 0;
	
	SET decTrustFund = (SELECT TRUSTFUND FROM tblTerminal WHERE TerminalNo = strTerminalNo);
	
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
					DateLastInitialized, decTrustFund FROM tblTerminalReport WHERE TerminalNo = strTerminalNo);
					
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
					DateLastInitialized					=  dteDateLastInitialized
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

/**************************************************************

procUpdateTerminalReportBatchCounter
Lemuel E. Aceron
April 19, 2009

CALL procUpdateTerminalReportBatchCounter();
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportBatchCounter
GO

create procedure procUpdateTerminalReportBatchCounter(IN pvtTerminalNo varchar(10), IN pvtDateLastInitialized DATETIME)
BEGIN
	
	UPDATE tblTerminalReportHistory SET BatchCounter = BatchCounter + 1 WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*************************************************************************************************************
Lemuel E. Aceron
April 22, 2009
*************************************************************************************************************/
ALTER TABLE tblTerminal ADD DBVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD FEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD BEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 

/*************************************************************************************************************
procTerminalVersionUpdate
Lemuel E. Aceron
April 22, 2009
*************************************************************************************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalVersionUpdate
GO

create procedure procTerminalVersionUpdate(IN strTerminalNo varchar(10), IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET
		FEVersion = strVersion
	WHERE TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;
