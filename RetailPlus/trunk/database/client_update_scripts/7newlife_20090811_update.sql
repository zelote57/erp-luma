 /*****************************
**	Added on December 10, 2007
**	Lemuel E. Aceron
**	Parameterized the display of items in Front-End if items with Quantity 
**	to display only Items with more than zero quantity
**		0 - means false [display all items]
**		1 - means true  [display only more than zero items]
*****************************/ 
alter table tblTerminal add `ShowItemMoreThanZeroQty` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE sysAccessUserDetails ADD PageSize INT(5) NOT NULL DEFAULT 10; 

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

/**
SELECT 
    CONCAT('INSERT INTO tblBranchInventory ( ProductID, ProductCode, VariationMatrixID, MatrixDescription, Quantity, BranchID', 
             ') VALUES ( '
             , ProductID, ',''', ProductCode,''',0,'''',', Quantity, ',1);') AS INSERTStatement
FROM tblProducts limit 10;




SELECT 
                                BranchInventoryID, 
                                a.ProductID, 
                                a.ProductCode, 
                                a.VariationMatrixID, 
                                MatrixDescription, 
                                a.Quantity, 
                                c.BaseUnitID AS UnitID, 
                                UnitCode, 
                                a.BranchID, 
                                BranchCode 
                            FROM tblBranchInventory a 
                                INNER JOIN tblBranch b ON a.BranchID = b.BranchID 
                                INNER JOIN tblProducts c ON a.ProductID = c.ProductID 
                                LEFT JOIN tblProductSubGroup d ON c.ProductSubGroupID = d.ProductSubGroupID 
                                LEFT JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID 
                                LEFT JOIN tblUnit f ON c.BaseUnitID = f.UnitID
                               LIMIT 10;
                               
**/

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
 
/*********************************  v_1.0.3.28.sql END  *******************************************************/

ALTER TABLE tblProducts ADD `WillPrintProductComposition` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************   v_1.0.3.28.sql END  ******************************************************/

/*********************************  v_2.0.0.0.sql START  *******************************************************/

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (126, 'MallForwarder');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 126, 1, 1);


/*********************************  v_2.0.0.0.sql END  *******************************************************/

/*********************************  v_2.0.0.1.sql START  *******************************************************/
	
/*********************************
Lemuel E. Aceron
April 22, 2009
*********************************/
ALTER TABLE tblTerminal ADD DBVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD FEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 
ALTER TABLE tblTerminal ADD BEVersion varchar(15) NOT NULL DEFAULT 'v_2.0.0.1'; 

/*********************************  v_2.0.0.1.sql END  *******************************************************/

/*********************************  v_2.0.0.2.sql START  *******************************************************/

-- ShowItemMoreThanZeroQty is implemented when selecting variations.
-- 

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.2';

ALTER TABLE tblReceipt MODIFY `TEXT` varchar(20);
	
/*********************************
Lemuel E. Aceron
May 1, 2009

OrderType values:
	0 = DINEIN
	1 = TAKE OUT
	2 = DELIVERY
*********************************/
ALTER TABLE tblTransactions01 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions02 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions03 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions04 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions05 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions06 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions07 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions08 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions09 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions10 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions11 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions12 ADD OrderType smallint NOT NULL DEFAULT 0; 


/*********************************
Create table for discount
*********************************/
ALTER TABLE tblDiscount MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions12 MODIFY DiscountCode varchar(5);

ALTER TABLE tblTransactionItems01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems12 MODIFY DiscountCode varchar(5);

-- This fields are from .config file, moved to database.
ALTER TABLE tblTerminal ADD IsPrinterDotmatrix TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD IsChargeEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD IsDiscountEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD CheckCutOffTime TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD StartCutOffTime VARCHAR(5) NOT NULL DEFAULT '00:00';
ALTER TABLE tblTerminal ADD EndCutOffTime VARCHAR(5) NOT NULL DEFAULT '23:59';
ALTER TABLE tblTerminal ADD WithRestaurantFeatures TINYINT(1) NOT NULL DEFAULT 0;
        
--Make a default DiscountCode for Senior Citizen Discount
ALTER TABLE tblTerminal DROP SeniorCitizenDiscountCode;
ALTER TABLE tblTerminal ADD SeniorCitizenDiscountCode varchar(5);
UPDATE tblTerminal SET SeniorCitizenDiscountCode = 'SENCZ';

DROP TABLE IF EXISTS tblDiscountHistory;
CREATE TABLE tblDiscountHistory (
	`SessionID` VARCHAR(30) NOT NULL,
	`TerminalNo` VARCHAR(30) NOT NULL,
	`DiscountCode` VARCHAR(5) NOT NULL,
	`DiscountCount` BIGINT(20) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblDiscount`(`SessionID`),
	INDEX `IX_tblDiscount1`(`DiscountCode`)
)
TYPE=INNODB COMMENT = 'Product Discount Report';

/*********************************  v_2.0.0.2.sql END  *******************************************************/


