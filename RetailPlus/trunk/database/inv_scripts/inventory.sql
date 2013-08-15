 /******************************************************************************
**		File: inventory.sql
**		Name: inventory
**		Desc: inventory
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
SET FOREIGN_KEY_CHECKS = 0;

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
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
	INDEX `IX2_tblPO`(`BranchID`),
	INDEX `IX3_tblPO`(`PurchaserID`)
);

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
-- ALTER TABLE tblPO ADD `UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
--	update tblPO SET UnpaidAmount = POSubTotal;
	
-- ALTER TABLE tblPO ADD `PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
-- ALTER TABLE tblPO ADD `PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0;

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
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
);

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
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`POReturnStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`DebitMemoStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDocNo` VARCHAR(30) NOT NULL,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
	INDEX `IX2_tblPODebitMemo`(`BranchID`),
	INDEX `IX3_tblPODebitMemo`(`PurchaserID`)
);

-- alter table tblPODebitMemo add `CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
-- alter table tblPODebitMemo add `CancelledRemarks` VARCHAR(150);
-- alter table tblPODebitMemo add `CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;

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
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
);

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
	`PurchaseQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PurchaseCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PurchaseVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PInvoiceQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PInvoiceCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PInvoiceVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PReturnQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PReturnCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PReturnVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PDebitQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PDebitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PDebitVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferInQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferInCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferInVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferOutQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferOutCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TransferOutVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SoldQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SoldCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SoldVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SReturnQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SReturnCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SReturnVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`InvAdjustmentQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`InvAdjustmentCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`InvAdjustmentVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ClosingQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ClosingCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ClosingVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`InventoryID`),
	INDEX `IX_tblInventory`(`InventoryID`),
	INDEX `IX1_tblInventory`(`PostingDateFrom`, `PostingDateTo`),
	INDEX `IX2_tblInventory`(`ReferenceNo`),
	INDEX `IX3_tblInventory`(`ContactID`),
	INDEX `IX4_tblInventory`(`ProductCode`)
);

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
);

INSERT INTO tblERPConfig (LastPONo, LastPOReturnNo, LastDebitMemoNo, LastSONo, LastSOReturnNo, LastCreditMemoNo, LastTransferInNo, LastTransferOutNo, LastInvAdjustmentNo, LastClosingNo, PostingDateFrom, PostingDateTo) VALUES ('0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '0000000001', '2007-08-01 12:00:00', '2020-08-31 12:00:00');
ALTER TABLE tblERPConfig ADD LastCreditCardNo VARCHAR(11) NOT NULL DEFAULT '00000001';
ALTER TABLE tblERPConfig ADD LastRewardCardNo VARCHAR(11) NOT NULL DEFAULT '00000001';

-- alter table tblERPConfig add `LastDebitMemoNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastDebitMemoNo` = '0000000001';
-- alter table tblERPConfig add `LastSOReturnNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastSOReturnNo` = '0000000001';
-- alter table tblERPConfig add `LastCreditMemoNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastCreditMemoNo` = '0000000001';
-- alter table tblERPConfig add `LastTransferInNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastTransferInNo` = '0000000001';
-- alter table tblERPConfig add `LastTransferOutNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastTransferOutNo` = '0000000001';
-- alter table tblERPConfig add `LastInvAdjustmentNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastInvAdjustmentNo` = '0000000001';
-- alter table tblERPConfig add `LastClosingNo` VARCHAR(10) NOT NULL;
-- update tblERPConfig set `LastClosingNo` = '0000000001';


/*********************************  v_1.0.0.1.sql START  *******************************************************/

ALTER TABLE tblERPConfig ADD DBVersion varchar(10);
UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.1';

ALTER TABLE tblInventory ADD `SCreditQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `SCreditCost` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `SCreditVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;

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
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
	INDEX `IX2_tblTransferIn`(`BranchID`),
	INDEX `IX3_tblTransferIn`(`TransferrerID`)
);

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
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
);

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
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`SupplierDRNo` VARCHAR(30) NOT NULL,
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
	INDEX `IX2_tblTransferOut`(`BranchID`),
	INDEX `IX3_tblTransferOut`(`TransferrerID`)
);

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
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
);


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
	`QuantityBefore` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityNow` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`MinThresholdBefore` DECIMAL(18,3),
	`MinThresholdNow` DECIMAL(18,3),
	`MaxThresholdBefore` DECIMAL(18,3),
	`MaxThresholdNow` DECIMAL(18,3),
	`Remarks` VARCHAR(100),
PRIMARY KEY (`InvAdjustmentID`),
INDEX `IX_tblInvAdjustment`(`InvAdjustmentID`),
INDEX `IX1_tblInvAdjustment`(`UID`),
INDEX `IX2_tblInvAdjustment`(`InvAdjustmentDate`)
);



/*********************************  v_1.0.0.1.sql END  *******************************************************/

/*********************************  v_1.0.0.2.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.2';

ALTER TABLE tblPODebitMemoItems ADD `PrevUnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0;


/*********************************  v_1.0.0.2.sql END  *******************************************************/

/*********************************  v_1.0.0.3.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.3';

ALTER TABLE tblPOItems ADD `SellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingEVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingLocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.3.sql END  *******************************************************/
  
/*********************************  v_1.0.0.4.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.4';

ALTER TABLE tblMatrixPackagePriceHistory MODIFY COLUMN Remarks VARCHAR(150);

ALTER TABLE tblPOItems ADD `OldSellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOutItems ADD `SellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingEVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingLocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `OldSellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferInItems ADD `SellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingEVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingLocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `OldSellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.4.sql END  *******************************************************/
   
SET FOREIGN_KEY_CHECKS = 1;

/*********************************  v_3.0.0.0.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_3.0.0.0';

ALTER TABLE tblPO ALTER `SupplierDRNo` SET DEFAULT '';
ALTER TABLE tblPODebitMemo ALTER `SupplierDocNo` SET DEFAULT '';
ALTER TABLE tblTransferIn ALTER `SupplierDRNo` SET DEFAULT '';
ALTER TABLE tblTransferOut ALTER `SupplierDRNo` SET DEFAULT '';


ALTER TABLE tblInventory ADD `ClosingActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `PurchasePrice` DECIMAL(18,3) NOT NULL DEFAULT 0;

/*********************************  v_3.0.0.0.sql END  *******************************************************/


/*********************************  v_3.0.3.4.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_3.0.3.4';

ALTER TABLE tblPO ADD `RID` BIGINT NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `RID` BIGINT NOT NULL DEFAULT 0; 

/*********************************  v_3.0.3.4.sql END  *******************************************************/

/*********************************  v_3.0.3.5.sql SKIP  *******************************************************/
/*********************************  v_3.0.3.6.sql START  *******************************************************/

ALTER TABLE tblInventory ADD `BranchID` INT(4) NOT NULL DEFAULT 1;

/*********************************  v_3.0.3.6.sql END  *******************************************************/

/*********************************  v_3.0.3.7.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_3.0.3.7';

/*****************************
**	tblBranchTransfer
*****************************/
DROP TABLE IF EXISTS tblBranchTransferItems;
DROP TABLE IF EXISTS tblBranchTransfer;
CREATE TABLE tblBranchTransfer (
	`BranchTransferID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BranchTransferNo` VARCHAR(30) NOT NULL,
	`BranchTransferDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`RequiredDeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`BranchIDFrom` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchIDFrom`),
	`BranchIDTo` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchIDFrom`),
	`TransferrerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`TransferrerName` VARCHAR(100),
	`SubTotal` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`RequestedBy` VARCHAR(150) NOT NULL DEFAULT '',
	`ReceivedBy` VARCHAR(150) NOT NULL DEFAULT '',
	`DeliveryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CancelledRemarks` VARCHAR(150),
	`CancelledByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`UnpaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaidAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PaymentStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`Freight` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Deposit` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`TotalItemDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (BranchTransferID),
	INDEX `IX_tblBranchTransfer`(`BranchTransferID`),
	UNIQUE `PK_tblBranchTransfer`(`BranchTransferNo`),
	INDEX `IX2_tblBranchTransfer`(`BranchIDFrom`),
	INDEX `IX3_tblBranchTransfer`(`BranchIDTo`),
	INDEX `IX4_tblBranchTransfer`(`TransferrerID`)
);


/*****************************
**	tblBranchTransferItems
*****************************/
DROP TABLE IF EXISTS tblBranchTransferItems;
CREATE TABLE tblBranchTransferItems (
	`BranchTransferItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`BranchTransferID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranchTransfer(`BranchTransferID`),
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`ProductCode` VARCHAR(30) NOT NULL,
	`BarCode` VARCHAR(30) NOT NULL,
	`Description` VARCHAR(100) NOT NULL,
	`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
	`ProductUnitCode` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`isVatInclusive` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1,
	`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(150) NULL,
	`ProductGroup` VARCHAR(20) NULL,
	`ProductSubGroup` VARCHAR(20) NULL,
	`BranchTransferItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`SellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SellingVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SellingEVAT` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`SellingLocalTax` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`OldSellingPrice` DECIMAL(18,3) NOT NULL DEFAULT 0,
	PRIMARY KEY (`BranchTransferItemID`),
	INDEX `IX_tblBranchTransferItems`(`BranchTransferItemID`),
	INDEX `IX0_tblBranchTransferItems`(`BranchTransferID`, `ProductID`),
	INDEX `IX1_tblBranchTransferItems`(`BranchTransferID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblBranchTransferItems`(`ProductCode`),
	INDEX `IX3_tblBranchTransferItems`(`BranchTransferID`),
	INDEX `IX4_tblBranchTransferItems`(`ProductUnitID`)
);

ALTER TABLE tblERPConfig ADD `LastBranchTransferNo` VARCHAR(10) NOT NULL DEFAULT '0000000001';

ALTER TABLE tblPO ADD `Discount2` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD `Discount2Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD `Discount2Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPO ADD `Discount3` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD `Discount3Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD `Discount3Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPOItems MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `Discount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblPOItems MODIFY `Amount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `VAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems MODIFY `EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
	
/*********************************  v_3.0.3.7.sql END  *******************************************************/

ALTER TABLE tblPO ADD `IsVatInclusive` TINYINT(1) NOT NULL DEFAULT 1;
UPDATE tblPO SET IsVatInClusive = 1;

/*********************************  v_4.0.0.0.sql  *******************************************************/ 

ALTER TABLE tblPOItems ADD `OriginalQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `POItemReceivedStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPO MODIFY SupplierCode VARCHAR(75);
ALTER TABLE tblPODebitMemo MODIFY SupplierCode VARCHAR(75);
ALTER TABLE tblInventory MODIFY ContactCode VARCHAR(75);

ALTER TABLE tblTransferIn MODIFY SupplierCode VARCHAR(75);
ALTER TABLE tblTransferOut MODIFY SupplierCode VARCHAR(75);

ALTER TABLE tblTransferIn ADD `IsVatInclusive` TINYINT(1) NOT NULL DEFAULT 1;
UPDATE tblTransferIn SET IsVatInClusive = 1;

ALTER TABLE tblTransferIn ADD `Discount2` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferIn ADD `Discount2Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferIn ADD `Discount2Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblTransferIn ADD `Discount3` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferIn ADD `Discount3Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferIn ADD `Discount3Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblTransferInItems MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `Discount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblTransferInItems MODIFY `Amount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `VAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems MODIFY `EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferInItems ADD `OriginalQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `TransferInItemReceivedStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0;


ALTER TABLE tblTransferOut ADD `IsVatInclusive` TINYINT(1) NOT NULL DEFAULT 1;
UPDATE tblTransferOut SET IsVatInClusive = 1;

ALTER TABLE tblTransferOut ADD `Discount2` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOut ADD `Discount2Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOut ADD `Discount2Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOut ADD `Discount3` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOut ADD `Discount3Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOut ADD `Discount3Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOutItems MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `Discount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblTransferOutItems MODIFY `Amount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `VAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems MODIFY `EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOutItems ADD `OriginalQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `TransferOutItemReceivedStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0;

/*********************************  v_4.0.0.0.sql END  *******************************************************/ 

ALTER TABLE tblPODebitMemo ADD `IsVatInclusive` TINYINT(1) NOT NULL DEFAULT 1;
UPDATE tblPODebitMemo SET IsVatInClusive = 1;

ALTER TABLE tblPODebitMemo ADD `Discount2` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemo ADD `Discount2Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemo ADD `Discount2Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPODebitMemo ADD `Discount3` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemo ADD `Discount3Applied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemo ADD `Discount3Type` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPODebitMemoItems MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `UnitCost` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `Discount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `DiscountApplied` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblPODebitMemoItems MODIFY `Amount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `VAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `VatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `EVAT` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems MODIFY `EVatableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblPODebitMemoItems ADD `OriginalQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblPODebitMemoItems ADD `DebitMemoItemReceivedStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0;


/*********************************  v_4.0.1.0.sql START  *******************************************************/ 
-- 08Aug2013 Added for closing inventory by group

ALTER TABLE tblInventory ADD `ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `ProductGroupCode` VARCHAR(20);
ALTER TABLE tblInventory ADD `ProductGroupName` VARCHAR(50);

/*********************************  v_4.0.1.0.sql END  *******************************************************/ 


