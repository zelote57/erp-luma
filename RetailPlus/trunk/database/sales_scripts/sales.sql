  /******************************************************************************
**		File: sales.sql
**		Name: sales
**		Desc: sales
**
**		Auth: Lemuel E. Aceron
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**		12-10-09	Lemuel				created this sql script.    
**
*******************************************************************************/ 

/*****************************
**	Added October 9, 2007
*****************************/

ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARBills` INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARContra` INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig ADD `ChartOfAccountIDARLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0;
	
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
	`SellerName` VARCHAR(100),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`CustomerDRNo` VARCHAR(30) NOT NULL,
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
	`ChartOfAccountIDARTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARBills` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARContra` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0,	
	PRIMARY KEY (SOID),
	INDEX `IX_tblSO`(`SOID`),
	UNIQUE `PK_tblSO`(`SONo`),
	INDEX `IX1_tblSO`(`CustomerID`),
	INDEX `IX2_tblSO`(`BranchID`),
	INDEX `IX3_tblSO`(`SellerID`)
);

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
	`SOItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`ChartOfAccountIDSold` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxSold` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`SOItemID`),
	INDEX `IX_tblSOItems`(`SOItemID`),
	INDEX `IX0_tblSOItems`(`SOID`, `ProductID`),
	INDEX `IX1_tblSOItems`(`SOID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOItems`(`ProductCode`),
	INDEX `IX3_tblSOItems`(`SOID`),
	INDEX `IX4_tblSOItems`(`ProductUnitID`)
);

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
	`SellerName` VARCHAR(100),
	`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`DiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SOReturnStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`CreditMemoStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`Remarks` VARCHAR(150),
	`CustomerDocNo` VARCHAR(30) NOT NULL,
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
	`ChartOfAccountIDARTracking` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARBills` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARFreight` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARVDeposit` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARContra` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDARLatePayment` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`CreditMemoID`),
	INDEX `IX_tblSOCreditMemo`(`CreditMemoID`),
	UNIQUE `PK_tblSOCreditMemo`(`CNNo`),
	INDEX `IX1_tblSOCreditMemo`(`CustomerID`),
	INDEX `IX2_tblSOCreditMemo`(`BranchID`),
	INDEX `IX3_tblSOCreditMemo`(`SellerID`)
);

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
	`PrevUnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0,
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
	`ChartOfAccountIDSold` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDTaxSold` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	`ChartOfAccountIDInventory` INT(4) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`CreditMemoItemID`),
	INDEX `IX_tblSOCreditMemoItems`(`CreditMemoItemID`),
	INDEX `IX0_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`),
	INDEX `IX1_tblSOCreditMemoItems`(`CreditMemoID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblSOCreditMemoItems`(`ProductCode`),
	INDEX `IX3_tblSOCreditMemoItems`(`CreditMemoID`),
	INDEX `IX4_tblSOCreditMemoItems`(`ProductUnitID`)
);

ALTER TABLE tblERPConfig ADD DBVersionSales varchar(10);
UPDATE tblERPConfig SET DBVersionSales = 'v_1.0.0.0';

/*********************************  v_3.0.0.0.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersionSales = 'v_3.0.0.0';


ALTER TABLE tblSO ALTER `CustomerDRNo` SET DEFAULT '';
ALTER TABLE tblSOCreditMemo ALTER `CustomerDocNo` SET DEFAULT '';



/*********************************  v_3.0.0.0.sql END  *******************************************************/
