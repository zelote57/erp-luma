/******************************************************************************
**		File: RetailPlus.sql
**		Name: RetailPlus
**		Desc: RetailPlus
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

USE mysql;

DROP DATABASE IF EXISTS posresto;

CREATE DATABASE posresto;

USE posresto;

	/*****************************
	**	tblCountry
	*****************************/
	DROP TABLE IF EXISTS tblCountry;
	CREATE TABLE tblCountry (
	`CountryID` TINYINT(1) UNSIGNED NOT NULL AUTO_INCREMENT,
	`CountryName` VARCHAR(120) NOT NULL,
	PRIMARY KEY (CountryID),
	INDEX `IX_tblCountry`(`CountryID`, `CountryName`),
	UNIQUE `PK_tblCountry`(`CountryName`)
	);

/*****************************
**	sysAccessUser
*****************************/
DROP TABLE IF EXISTS sysAccessUsers;
CREATE TABLE sysAccessUsers (
`UID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`UserName` VARCHAR(25) NOT NULL UNIQUE,
`Password` VARCHAR(25) NOT NULL,
`DateCreated` DATETIME NOT NULL,
`Deleted` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (UID),
INDEX `IX_sysAccessUser`(`UID`, `UserName`),
UNIQUE `PK_sysAccessUser`(`UserName`)
);

/*****************************
**	sysAccessGroups
*****************************/
DROP TABLE IF EXISTS sysAccessGroups;
CREATE TABLE sysAccessGroups (
`GroupID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`GroupName` VARCHAR(20) NOT NULL,
`Remarks` VARCHAR(200),
PRIMARY KEY (GroupID),
INDEX `IX_sysAccessGroups`(`GroupID`, `GroupName`),
UNIQUE `PK_sysAccessGroups`(`GroupName`)
);

/*****************************
**	sysAccessDetails
*****************************/
DROP TABLE IF EXISTS sysAccessUserDetails;
CREATE TABLE sysAccessUserDetails (
`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`Name` VARCHAR (160) NULL ,
`Address1` VARCHAR (150) NULL ,
`Address2` VARCHAR (150) NULL ,
`City` VARCHAR (30) NULL ,
`State` VARCHAR (30) NULL ,
`Zip` VARCHAR (15) NULL ,
`CountryID` TINYINT NOT NULL DEFAULT 0,
`OfficePhone` VARCHAR (150) NOT NULL DEFAULT '---',
`DirectPhone` VARCHAR (150) NOT NULL DEFAULT '---',
`HomePhone` VARCHAR (150) NOT NULL DEFAULT '---',
`FaxPhone` VARCHAR (150) NOT NULL DEFAULT '---',
`MobilePhone` VARCHAR (150) NOT NULL DEFAULT '---',
`EmailAddress` VARCHAR (150) NOT NULL DEFAULT '',
`GroupID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessGroups(`GroupID`),
INDEX `IX_sysAccessUserDetails`(`UID`, `Name`),
UNIQUE `PK_sysAccessUserDetails`(`Name`),
INDEX `IX1_sysAccessUserDetails`(`UID`),
FOREIGN KEY (`UID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT,
INDEX `IX2_sysAccessUserDetails`(`GroupID`),
FOREIGN KEY (`GroupID`)REFERENCES sysAccessGroups(`GroupID`) ON DELETE RESTRICT
);

/*****************************
**	sysAccessTypes
*****************************/
DROP TABLE IF EXISTS sysAccessTypes;
CREATE TABLE sysAccessTypes (
`TypeID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`TypeName` VARCHAR(80) NOT NULL,
`Remarks` VARCHAR(120),
PRIMARY KEY (TypeID),
INDEX `IX_sysAccessTypes`(`TypeID`, `TypeName`),
UNIQUE `PK_sysAccessTypes`(`TypeName`)
);

/*****************************
**	sysAccessRights
*****************************/
DROP TABLE IF EXISTS sysAccessRights;
CREATE TABLE sysAccessRights (
`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`TranTypeID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessTypes(`TypeID`),
`AllowRead` TINYINT(1) NOT NULL DEFAULT 0,
`AllowWrite` TINYINT(1) NOT NULL DEFAULT 0,
INDEX `IX_sysAccessTypes`(`UID`, `TranTypeID`),
UNIQUE `PK_sysAccessTypes`(`UID`, `TranTypeID`),
INDEX `IX1_sysAccessTypes`(`UID`),
FOREIGN KEY (`UID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT,
INDEX `IX2_sysAccessTypes`(`TranTypeID`),
FOREIGN KEY (`TranTypeID`) REFERENCES sysAccessTypes(`TypeID`) ON DELETE RESTRICT
);

/*****************************
**	sysAccessGroupRights
*****************************/
DROP TABLE IF EXISTS sysAccessGroupRights;
CREATE TABLE sysAccessGroupRights (
`GroupID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessGroups(`GroupID`),
`TranTypeID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessTypes(`TypeID`),
`AllowRead` TINYINT(1) NOT NULL DEFAULT 0,
`AllowWrite` TINYINT(1) NOT NULL DEFAULT 0,
INDEX `IX_sysAccessGroupRights`(`GroupID`, `TranTypeID`),
UNIQUE `PK_ssysAccessGroupRights`(`GroupID`, `TranTypeID`),
INDEX `IX1_sysAccessGroupRights`(`GroupID`),
FOREIGN KEY (`GroupID`) REFERENCES sysAccessGroups(`GroupID`) ON DELETE RESTRICT,
INDEX `IX2_sysAccessGroupRights`(`TranTypeID`),
FOREIGN KEY (`TranTypeID`) REFERENCES sysAccessTypes(`TypeID`) ON DELETE RESTRICT
);

/*****************************
**	sysAuditTrail
*****************************/
DROP TABLE IF EXISTS sysAuditTrail;
CREATE TABLE sysAuditTrail (
`ActivityDate` DATETIME NOT NULL,
`User` VARCHAR(80) NOT NULL,
`Activity` VARCHAR(120),
`IPAddress` VARCHAR(15),
`Remarks` VARCHAR (8000),
INDEX `IX_sysAuditTrail`(`ActivityDate`, `User`)
);

/*****************************
**	tblCashierLogs
*****************************/
DROP TABLE IF EXISTS tblCashierLogs;
CREATE TABLE tblCashierLogs (
`CashierLogsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`LoginDate` DATETIME NOT NULL,
`TerminalNo` VARCHAR(5),
`IPAddress` VARCHAR(15),
`LogoutDate` DATETIME,
`Status` TINYINT(3),
PRIMARY KEY (`CashierLogsID`),
INDEX `IX_tblCashierLogs`(`CashierLogsID`),
UNIQUE `PK_tblCashierLogs`(`CashierLogsID`),
INDEX `IX1_tblCashierLogs`(`UID`),
FOREIGN KEY (`UID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT,
INDEX `IX2_tblCashierLogs`(`UID`, `LoginDate`),
INDEX `IX3_tblCashierLogs`(`UID`, `LoginDate`, `TerminalNo`),
INDEX `IX4_tblCashierLogs`(`UID`, `TerminalNo`)
);

/*****************************
**	tblContactGroup
*****************************/
DROP TABLE IF EXISTS tblContactGroup;
CREATE TABLE tblContactGroup (
`ContactGroupID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`ContactGroupCode` VARCHAR(3) NOT NULL,
`ContactGroupName` VARCHAR(30) NOT NULL,
`ContactGroupCategory` TINYINT(1) NOT NULL DEFAULT 3,
PRIMARY KEY (`ContactGroupID`),
INDEX `IX_tblContactGroup`(`ContactGroupID`, `ContactGroupCode`, `ContactGroupName`),
UNIQUE `PK_tblContactGroup`(`ContactGroupCode`)
);

INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES ('CUS', 'Default Customer Group', 1);
INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES ('SUP', 'Default Supplier Group', 2);

/*****************************
**	tblContacts
*****************************/
DROP TABLE IF EXISTS tblContacts;
CREATE TABLE tblContacts (
`ContactID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ContactCode` VARCHAR(25) NOT NULL,
`ContactName` VARCHAR(75) NOT NULL,
`ContactGroupID` INT(10) UNSIGNED DEFAULT 0 REFERENCES tblContactGroup(`ContactGroupID`),
`ModeOfTerms` INT(10) NOT NULL DEFAULT 0,
`Terms` INT(10) NOT NULL DEFAULT 0,
`Address` VARCHAR(150) NOT NULL DEFAULT '',
`BusinessName` VARCHAR(75) NOT NULL DEFAULT '',
`TelephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
`Remarks` VARCHAR(150) NOT NULL DEFAULT '',
`Debit` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Credit` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditLimit` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IsCreditAllowed` TINYINT(1) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`Deleted` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (ContactID),
INDEX `IX_tblContacts`(`ContactID`, `ContactCode`, `ContactName`),
UNIQUE `PK_tblContacts`(`ContactCode`),
INDEX `IX1_tblContacts`(`ContactGroupID`),
FOREIGN KEY (`ContactGroupID`) REFERENCES tblContactGroup(`ContactGroupID`) ON DELETE RESTRICT
);

INSERT INTO tblContacts (ContactCode, ContactName, ContactGroupID, ModeOfTerms, Address, BusinessName, TelephoneNo, Remarks, DateCreated) VALUES
('RC', 'RetailPlus Customer ™', 1, 0, 'RBS', 'RBS', '', '', NOW());
INSERT INTO tblContacts (ContactCode, ContactName, ContactGroupID, ModeOfTerms, Address, BusinessName, TelephoneNo, Remarks, DateCreated) VALUES
('RS', 'RetailPlus Supplier ™', 2, 0, 'RBS', 'RBS', '', '', NOW());

/*****************************
**	tblUnit
*****************************/
DROP TABLE IF EXISTS tblUnit;
CREATE TABLE tblUnit (
`UnitID` INT(3) UNSIGNED NOT NULL AUTO_INCREMENT,
`UnitCode` VARCHAR(3) NOT NULL,
`UnitName` VARCHAR(30) NOT NULL,
PRIMARY KEY (UnitID),
INDEX `IX_tblUnit`(`UnitID`, `UnitCode`, `UnitName`),
UNIQUE `PK_tblUnit`(`UnitCode`)
);

/*****************************
**	tblVariations
*****************************/
DROP TABLE IF EXISTS tblVariations;
CREATE TABLE tblVariations (
`VariationID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`VariationCode` VARCHAR(3) NOT NULL,
`VariationType` VARCHAR(20) NOT NULL,
PRIMARY KEY (VariationID),
INDEX `IX_tblVariations`(`VariationID`, `VariationCode`, `VariationType`),
UNIQUE `PK_tblVariations`(`VariationCode`)
);

/*****************************
**	tblChargeType
*****************************/
DROP TABLE IF EXISTS tblChargeType;
CREATE TABLE tblChargeType (
`ChargeTypeID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`ChargeTypeCode` VARCHAR(3) NOT NULL,
`ChargeType` VARCHAR(20) NOT NULL,
`ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`InPercent` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (ChargeTypeID),
INDEX `IX_tblChargeType`(`ChargeTypeID`, `ChargeTypeCode`, `ChargeType`),
UNIQUE `PK_tblChargeType`(`ChargeTypeCode`),
INDEX `IX1_tblChargeType`(`ChargeTypeID`),
INDEX `IX2_tblChargeType`(`ChargeTypeCode`),
INDEX `IX3_tblChargeType`(`ChargeType`),
INDEX `IX4_tblChargeType`(`ChargeTypeCode`, `ChargeType`),
INDEX `IX5_tblChargeType`(`InPercent`)
);

/*****************************
**	tblProductGroup
*****************************/
DROP TABLE IF EXISTS tblProductGroup;
CREATE TABLE tblProductGroup (
`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductGroupCode` VARCHAR(20) NOT NULL,
`ProductGroupName` VARCHAR(50) NOT NULL,
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
PRIMARY KEY (ProductGroupID),
INDEX `IX_tblProductGroup`(`ProductGroupID`, `ProductGroupCode`, `ProductGroupName`),
UNIQUE `PK_tblProductGroup`(`ProductGroupCode`),
INDEX `IX1_tblProductGroup`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT 
);

/*****************************
**	tblProductGroupVariations
*****************************/
DROP TABLE IF EXISTS tblProductGroupVariations;
CREATE TABLE tblProductGroupVariations (
`GroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroup(`ProductGroupID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblVariations(`VariationID`),
INDEX `IX_tblProductGroupVariations`(`GroupID`, `VariationID`),
UNIQUE `PK_tblProductGroupVariations`(`GroupID`, `VariationID`),
INDEX `IX1_tblProductGroupVariations`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblVariations(`VariationID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductGroupVariations`(`GroupID`),
FOREIGN KEY (`GroupID`) REFERENCES tblProductGroup(`ProductGroupID`) ON DELETE RESTRICT
);


/*****************************
**	tblProductGroupBaseVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductGroupBaseVariationsMatrix;
CREATE TABLE tblProductGroupBaseVariationsMatrix (
`MatrixID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`GroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroup(`ProductGroupID`),
`Description` VARCHAR(255) NOT NULL,
`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblProductGroupBaseVariationsMatrix`(`MatrixID`,`GroupID`),
UNIQUE `PK_tblProductGroupBaseVariationsMatrix`(`GroupID`,`Description`),
INDEX `IX1_tblProductGroupBaseVariationsMatrix`(`GroupID`),
FOREIGN KEY (`GroupID`) REFERENCES tblProductGroup(`ProductGroupID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductGroupBaseVariationsMatrix`(`UnitID`),
FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
);


/*****************************
**	tblProductGroupVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductGroupVariationsMatrix;
CREATE TABLE tblProductGroupVariationsMatrix (
`MatrixID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroupBaseVariationsMatrix(`MatrixID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroupVariations(`VariationID`),
`Description` VARCHAR(150) NOT NULL,
INDEX `IX_tblProductGroupVariationsMatrix`(`MatrixID`,`VariationID`),
UNIQUE `PK_tblProductGroupVariationsMatrix`(`MatrixID`, `VariationID`, `Description`),
INDEX `IX1_tblProductGroupVariationsMatrix`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblProductGroupVariations(`VariationID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductGroupVariationsMatrix`(`MatrixID`),
FOREIGN KEY (`MatrixID`) REFERENCES tblProductGroupBaseVariationsMatrix(`MatrixID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductGroupUnitMatrix
** Product Group Unit Matrix for inventoy purpose
*****************************/
DROP TABLE IF EXISTS tblProductGroupUnitMatrix;
CREATE TABLE tblProductGroupUnitMatrix (
`MatrixID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`GroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroup(`ProductGroupID`),
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BaseUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
`BottomUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BottomUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
INDEX `IX_tblProductGroupUnitMatrix`(`MatrixID`, `GroupID`, `BaseUnitID`, `BottomUnitID`),
UNIQUE `PX_tblProductGroupUnitMatrix`(`GroupID`, `BaseUnitID`, `BottomUnitID`),
INDEX `IX1_tblProductGroupUnitMatrix`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductGroupUnitMatrix`(`BottomUnitID`),
FOREIGN KEY (`BottomUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductGroupUnitMatrix`(`GroupID`),
FOREIGN KEY (`GroupID`) REFERENCES tblProductGroup(`ProductGroupID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductGroupCharges
*****************************/
DROP TABLE IF EXISTS tblProductGroupCharges;
CREATE TABLE tblProductGroupCharges (
`ChargeID`		BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`GroupID`		BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroup(`ProductGroupID`),
`ChargeTypeID`	INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChargeType(`ChargeTypeID`),
`ChargeAmount`	DECIMAL(18,2) NOT NULL DEFAULT 0,
`InPercent`		TINYINT(1) NOT NULL DEFAULT 0,
INDEX `IX_tblProductGroupCharges`(`ChargeID`),
UNIQUE `PK_tblProductGroupCharges`(`ChargeID`),
INDEX `IX1_tblProductGroupCharges`(`GroupID`, `ChargeTypeID`),
UNIQUE `PK1_tblProductGroupCharges`(`GroupID`, `ChargeTypeID`),
INDEX `IX2_tblProductGroupCharges`(`ChargeTypeID`),
FOREIGN KEY (`ChargeTypeID`) REFERENCES tblChargeType(`ChargeTypeID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductGroupCharges`(`GroupID`),
FOREIGN KEY (`GroupID`) REFERENCES tblProductGroup(`ProductGroupID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductSubGroup
*****************************/
DROP TABLE IF EXISTS tblProductSubGroup;
CREATE TABLE tblProductSubGroup (
`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductGroupID` INT(10) UNSIGNED NOT NULL,
`ProductSubGroupCode` VARCHAR(20) NOT NULL,
`ProductSubGroupName` VARCHAR(50) NOT NULL,
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`) ,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
PRIMARY KEY (ProductSubGroupID),
INDEX `IX_tblProductSubGroup`(`ProductSubGroupID`, `ProductSubGroupCode`, `ProductSubGroupName`),
UNIQUE `PK_tblProductSubGroup`(`ProductGroupID`, `ProductSubGroupCode`),
INDEX `IX1_tblProductSubGroup`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT 
);

/*****************************
**	tblProductSubGroupVariations
*****************************/
DROP TABLE IF EXISTS tblProductSubGroupVariations;
CREATE TABLE tblProductSubGroupVariations (
`SubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroup(`ProductSubGroupID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblVariations(`VariationID`),
INDEX `IX_tblProductSubGroupVariations`(`SubGroupID`, `VariationID`),
UNIQUE `PK_tblProductSubGroupVariations`(`SubGroupID`, `VariationID`),
INDEX `IX1_tblProductSubGroupVariations`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblVariations(`VariationID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductSubGroupVariations`(`SubGroupID`),
FOREIGN KEY (`SubGroupID`) REFERENCES tblProductSubGroup(`ProductSubGroupID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductSubGroupBaseVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductSubGroupBaseVariationsMatrix;
CREATE TABLE tblProductSubGroupBaseVariationsMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`SubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroup(`ProductSubGroupID`),
`Description` VARCHAR(255) NOT NULL,
`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblProductSubGroupBaseVariationsMatrix`(`MatrixID`,`SubGroupID`),
UNIQUE `PK_tblProductSubGroupBaseVariationsMatrix`(`SubGroupID`,`Description`),
INDEX `IX1_tblProductSubGroupBaseVariationsMatrix`(`SubGroupID`),
FOREIGN KEY (`SubGroupID`) REFERENCES tblProductSubGroup(`ProductSubGroupID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductSubGroupBaseVariationsMatrix`(`UnitID`),
FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductSubGroupVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductSubGroupVariationsMatrix;
CREATE TABLE tblProductSubGroupVariationsMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroupBaseVariationsMatrix(`MatrixID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductGroupVariations(`VariationID`),
`Description` VARCHAR(150) NOT NULL,
INDEX `IX_tblProductSubGroupVariationsMatrix`(`MatrixID`,`VariationID`),
UNIQUE `PK_tblProductSubGroupVariationsMatrix`(`MatrixID`, `VariationID`, `Description`),
INDEX `IX1_tblProductSubGroupVariationsMatrix`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblProductGroupVariations(`VariationID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductSubGroupVariationsMatrix`(`MatrixID`),
FOREIGN KEY (`MatrixID`) REFERENCES tblProductSubGroupBaseVariationsMatrix(`MatrixID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductSubGroupUnitMatrix
*****************************/
DROP TABLE IF EXISTS tblProductSubGroupUnitMatrix;
CREATE TABLE tblProductSubGroupUnitMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`SubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroup(`ProductSubGroupID`),
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BaseUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
`BottomUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BottomUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
INDEX `IX_tblProductSubGroupUnitMatrix`(`MatrixID`, `SubGroupID`, `BaseUnitID`, `BottomUnitID`),
UNIQUE `PX_tblProductUnitMatrix`(`SubGroupID`, `BaseUnitID`, `BottomUnitID`),
INDEX `IX1_tblProductSubGroupUnitMatrix`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductSubGroupUnitMatrix`(`BottomUnitID`),
FOREIGN KEY (`BottomUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductSubGroupUnitMatrix`(`SubGroupID`),
FOREIGN KEY (`SubGroupID`) REFERENCES tblProductSubGroup(`ProductSubGroupID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductSubGroupCharges
*****************************/
DROP TABLE IF EXISTS tblProductSubGroupCharges;
CREATE TABLE tblProductSubGroupCharges (
`ChargeID`		BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`SubGroupID`	BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroup(`ProductSubGroupID`),
`ChargeTypeID`	INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChargeType(`ChargeTypeID`),
`ChargeAmount`	DECIMAL(18,2) NOT NULL DEFAULT 0,
`InPercent`		TINYINT(1) NOT NULL DEFAULT 0,
INDEX `IX_tblProductSubGroupCharges`(`ChargeID`),
UNIQUE `PK_tblProductSubGroupCharges`(`ChargeID`),
INDEX `IX1_tblProductSubGroupCharges`(`SubGroupID`, `ChargeTypeID`),
UNIQUE `PK1_tblProductSubGroupCharges`(`SubGroupID`, `ChargeTypeID`),
INDEX `IX2_tblProductSubGroupCharges`(`ChargeTypeID`),
FOREIGN KEY (`ChargeTypeID`) REFERENCES tblChargeType(`ChargeTypeID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductSubGroupCharges`(`SubGroupID`),
FOREIGN KEY (`SubGroupID`) REFERENCES tblProductSubGroup(`ProductSubGroupID`) ON DELETE RESTRICT
);

/*****************************
**	tblProducts
*****************************/
DROP TABLE IF EXISTS tblProducts;
CREATE TABLE tblProducts (
`ProductID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`ProductDesc` VARCHAR(50) NOT NULL,
`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductSubGroup(`ProductSubGroupID`),
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`DateCreated` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`Deleted` TINYINT(1) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`MinThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`MaxThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
PRIMARY KEY (ProductID),
INDEX `IX_tblProducts`(`ProductID`, `ProductCode`),
UNIQUE `PK_tblProducts`(`ProductCode`),
INDEX `IX1_tblProducts`(`BarCode`),
UNIQUE `PK1_tblProducts`(`BarCode`),
INDEX `IX2_tblProducts`(`ProductSubGroupID`),
FOREIGN KEY (`ProductSubGroupID`) REFERENCES tblProductSubGroup(`ProductSubGroupID`),
INDEX `IX3_tblProducts`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX4_tblProducts`(`SupplierID`),
FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductVariations
*****************************/
DROP TABLE IF EXISTS tblProductVariations;
CREATE TABLE tblProductVariations (
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblVariations(`VariationID`),
INDEX `IX_tblProductVariations`(`ProductID`, `VariationID`),
UNIQUE `PK_tblProductVariations`(`ProductID`, `VariationID`),
INDEX `IX1_tblProductVariations`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblVariations(`VariationID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductBaseVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductBaseVariationsMatrix;
CREATE TABLE tblProductBaseVariationsMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`Description` VARCHAR(255) NOT NULL,
`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`MinThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`MaxThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
INDEX `IX_tblProductBaseVariationsMatrix`(`MatrixID`,`ProductID`),
UNIQUE `PK_tblProductBaseVariationsMatrix`(`ProductID`,`Description`),
INDEX `IX1_tblProductBaseVariationsMatrix`(`ProductID`),
FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductBaseVariationsMatrix`(`UnitID`),
FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductBaseVariationsMatrix`(`SupplierID`),
FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductVariationsMatrix
*****************************/
DROP TABLE IF EXISTS tblProductVariationsMatrix;
CREATE TABLE tblProductVariationsMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductBaseVariationsMatrix(`MatrixID`),
`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductVariations(`VariationID`),
`Description` VARCHAR(150) NOT NULL,
INDEX `IX_tblProductVariationsMatrix`(`MatrixID`,`VariationID`),
UNIQUE `PK_tblProductVariationsMatrix`(`MatrixID`, `VariationID`, `Description`),
INDEX `IX1_tblProductVariationsMatrix`(`MatrixID`),
FOREIGN KEY (`MatrixID`) REFERENCES tblProductBaseVariationsMatrix(`MatrixID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductVariationsMatrix`(`VariationID`),
FOREIGN KEY (`VariationID`) REFERENCES tblProductVariations(`VariationID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductUnitMatrix
*****************************/
DROP TABLE IF EXISTS tblProductUnitMatrix;
CREATE TABLE tblProductUnitMatrix (
`MatrixID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`BaseUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BaseUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
`BottomUnitID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`BottomUnitValue` DECIMAL(18,2) NOT NULL DEFAULT 1,
INDEX `IX_tblProductUnitMatrix`(`MatrixID`, `ProductID`, `BaseUnitID`, `BottomUnitID`),
UNIQUE `PX_tblProductUnitMatrix`(`ProductID`, `BaseUnitID`, `BottomUnitID`),
INDEX `IX1_tblProductUnitMatrix`(`ProductID`),
FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductUnitMatrix`(`BaseUnitID`),
FOREIGN KEY (`BaseUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX3_tblProductUnitMatrix`(`BottomUnitID`),
FOREIGN KEY (`BottomUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
);

/*****************************
**	tblProductPackage
*****************************/
DROP TABLE IF EXISTS tblProductPackage;
CREATE TABLE tblProductPackage (
`PackageID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblProductPackage`(`PackageID`,`ProductID`),
UNIQUE `PK_tblProductPackage`(`ProductID`,`UnitID`,`Quantity`),
INDEX `IX1_tblProductPackage`(`ProductID`),
FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
INDEX `IX2_tblProductPackage`(`UnitID`),
FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
);

/*****************************
**	tblMatrixPackage
*****************************/
DROP TABLE IF EXISTS tblMatrixPackage;
CREATE TABLE tblMatrixPackage (
`PackageID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`MatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductBaseVariationsMatrix(`MatrixID`),
`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblMatrixPackage`(`PackageID`,`MatrixID`),
UNIQUE `PK_tblMatrixPackage`(`MatrixID`,`UnitID`,`Quantity`),
INDEX `IX1_tblMatrixPackage`(`MatrixID`),
INDEX `IX2_tblMatrixPackage`(`UnitID`),
FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
);

/*****************************
**	tblDiscount
*****************************/
DROP TABLE IF EXISTS tblDiscount;
CREATE TABLE tblDiscount (
`DiscountID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`DiscountCode` VARCHAR(3) NOT NULL,
`DiscountType` VARCHAR(30) NOT NULL,
`DiscountPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`InPercent` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (DiscountID),
INDEX `IX_tblDiscount`(`DiscountID`, `DiscountCode`, `DiscountType`),
UNIQUE `PK_tblDiscount`(`DiscountCode`)
);

INSERT INTO tblDiscount(`DiscountCode`, `DiscountType`, `DiscountPrice`, `InPercent`)
		VALUES ('DEF', 'DEFAULT', 0, 0);

/*****************************
**	tblReceiptFormat
*****************************/
DROP TABLE IF EXISTS tblReceiptFormat;
CREATE TABLE tblReceiptFormat (
`ReportHeaderSpacer` INT(10) NOT NULL DEFAULT 0,
`ReportHeader1` VARCHAR(70) NOT NULL,
`ReportHeader2` VARCHAR(70) NOT NULL,
`ReportHeader3` VARCHAR(70) NOT NULL,
`ReportHeader4` VARCHAR(70) NOT NULL,
`PageHeader1` VARCHAR(70) NOT NULL,
`PageHeader2` VARCHAR(70) NOT NULL,
`PageHeader3` VARCHAR(70) NOT NULL,
`PageFooter1` VARCHAR(70) NOT NULL,
`PageFooter2` VARCHAR(70) NOT NULL,
`PageFooter3` VARCHAR(70) NOT NULL,
`ReportFooter1` VARCHAR(70) NOT NULL,
`ReportFooter2` VARCHAR(70) NOT NULL,
`ReportFooter3` VARCHAR(70) NOT NULL,
`ReportFooterSpacer` INT(10) NOT NULL DEFAULT 1,
INDEX `IX_tblReceiptFormat`(`ReportHeader1`, `ReportHeader2`, `ReportHeader3`, `ReportHeader4`)
);

/*****************************
**	insert the default values
*****************************/
INSERT INTO tblReceiptFormat VALUES (0, '6937 Rosal Street Guadalupe Viejo', 'Makati City, Phil', 
'{DateNow}', '-/-', 
'{InvoiceNo}', '',
'', 'Thank you for shopping.',
'Please Come Again.', '{AccreditationNo}',
'{Cashier}', '{TerminalNo}',
'{MachineSerialNo}',1);

/*****************************
**	tblStockType
*****************************/
DROP TABLE IF EXISTS tblStockType;
CREATE TABLE tblStockType (
`StockTypeID` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
`StockTypeCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(150) NOT NULL,
`StockDirection` TINYINT(1) NOT NULL DEFAULT 1,
INDEX `IX_tblStockType`(`StockTypeID`, `Description`),
UNIQUE `PX_tblStockType`(`StockTypeCode`),
INDEX `IX1_tblStockType`(`StockTypeCode`),
INDEX `IX2_tblStockType`(`Description`)
);

INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Stock In', 'Stock In', 0);
INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Stock Out', 'Stock Out', 1);
INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Incremental Adjustment', 'Incremental Adjustment', 0);
INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Decremental Adjustment', 'Decremental Adjustment', 1);

/*****************************
**	tblStock
*****************************/
DROP TABLE IF EXISTS tblStock;
CREATE TABLE tblStock (
`StockID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`StockTypeID` TINYINT UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblStockType(`StockTypeID`),
`StockDate` DATETIME NOT NULL,
`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
`Remarks` VARCHAR(150) NOT NULL,
INDEX `IX_tblStock`(`StockID`),
INDEX `IX1_tblStock`(`TransactionNo`),
UNIQUE `PX_tblStock`(`TransactionNo`),
INDEX `IX2_tblStock`(`StockDate`),
INDEX `IX3_tblStock`(`SupplierID`),
FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT
);


/*****************************
**	tblStockItems
*****************************/
DROP TABLE IF EXISTS tblStockItems;
CREATE TABLE tblStockItems (
`StockItemID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`StockID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`VariationMatrixID` INT(10) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`StockTypeID` TINYINT UNSIGNED NOT NULL DEFAULT 0,
`StockDate` DATETIME NOT NULL,
`Quantity` DECIMAL(18,2) UNSIGNED NOT NULL DEFAULT 0,
`Remarks` VARCHAR(150) NOT NULL,
INDEX `IX_tblStockItems`(`StockItemID`),
FOREIGN KEY (`StockID`) REFERENCES tblStock(`StockID`) ON DELETE RESTRICT,
INDEX `IX1_tblStockItems`(`StockID`, `ProductID`, `ProductUnitID`, `VariationMatrixID`),
UNIQUE `PX_tblStockItems`(`StockItemID`),
INDEX `IX2_tblStockItems`(`ProductID`),
FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
INDEX `IX3_tblStockItems`(`VariationMatrixID`),
INDEX `IX4_tblStockItems`(`ProductID`, `VariationMatrixID`),
INDEX `IX5_tblStockItems`(`ProductUnitID`),
FOREIGN KEY (`ProductUnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
INDEX `IX6_tblStockItems`(`StockID`)
);

/*****************************
**	tblPromoType
*****************************/
DROP TABLE IF EXISTS tblPromoType;
CREATE TABLE tblPromoType (
`PromoTypeID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`PromoTypeCode` VARCHAR(60) NOT NULL,
`PromoTypeName` VARCHAR(75) NOT NULL,
PRIMARY KEY (`PromoTypeID`),
INDEX `IX_tblPromoType`(`PromoTypeID`, `PromoTypeCode`, `PromoTypeName`),
UNIQUE `PK_tblPromoType`(`PromoTypeCode`)
);

INSERT INTO tblPromoType VALUES(1, 'Value-Off after quantity reached.', 'Value-Off after quantity reached.');
INSERT INTO tblPromoType VALUES(2, 'Percent-Off after quantity reached.', 'Percent-Off after quantity reached.');


/*****************************
**	tblPromo
*****************************/
DROP TABLE IF EXISTS tblPromo;
CREATE TABLE tblPromo (
`PromoID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`PromoCode` VARCHAR(60) NOT NULL,
`PromoName` VARCHAR(75) NOT NULL,
`StartDate` DATETIME NOT NULL,
`EndDate` DATETIME NOT NULL,
`PromoTypeID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPromoType(`PromoTypeID`),
`Status` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (`PromoID`),
INDEX `IX_tblPromo`(`PromoID`, `PromoCode`, `PromoName`, `PromoTypeID`),
UNIQUE `PK_tblPromo`(`PromoCode`),
INDEX `IX_tblPromo1`(`PromoTypeID`),
FOREIGN KEY (`PromoTypeID`) REFERENCES tblPromoType(`PromoTypeID`) ON DELETE RESTRICT 
);

/*****************************
**	tblPromoItems
*****************************/
DROP TABLE IF EXISTS tblPromoItems;
CREATE TABLE tblPromoItems (
`PromoItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`PromoID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblPromo(`PromoID`),
`ContactID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`VariationMatrixID` INT(10) UNSIGNED NOT NULL DEFAULT 0,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`InPercent` TINYINT(1) NOT NULL DEFAULT 0,
PRIMARY KEY (`PromoItemsID`),
INDEX `IX_tblPromoItems`(`PromoID`),
UNIQUE `PK_tblPromoItems`(`PromoItemsID`),
UNIQUE `PK_tblPromoItems1`(`PromoID`, `ContactID`, `ProductGroupID`, `ProductSubGroupID`, `ProductID`, `VariationMatrixID`),
INDEX `IX_tblPromoItems1`(`VariationMatrixID`),
FOREIGN KEY (`PromoID`) REFERENCES tblPromo(`PromoID`) ON DELETE RESTRICT ,
INDEX `IX_tblPromoItems2`(`ProductGroupID`),
INDEX `IX_tblPromoItems3`(`ProductSubGroupID`),
INDEX `IX_tblPromoItems4`(`ProductID`),
INDEX `IX_tblPromoItems5`(`VariationMatrixID`),
INDEX `IX_tblPromoItems6`(`ContactID`)
);

/*****************************
**	tblTerminal
*****************************/
DROP TABLE IF EXISTS tblTerminal;
CREATE TABLE tblTerminal (
`TerminalID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TerminalNo` VARCHAR(10) NOT NULL,
`TerminalCode` VARCHAR(20) NOT NULL,
`TerminalName` VARCHAR(50) NOT NULL,
`Status`  TINYINT (1) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`IsPrinterAutoCutter` TINYINT (1) NOT NULL DEFAULT 0,
`MaxReceiptWidth` INT(10) NOT NULL DEFAULT 40,
`TransactionNoLength` INT(2) NOT NULL DEFAULT 15,
`AutoPrint` TINYINT (1) NOT NULL DEFAULT 0,
`PrinterName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlus',
`TurretName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlusTurret',
`CashDrawerName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlusDrawer',
`MachineSerialNo` VARCHAR(20) NOT NULL,
`AccreditationNo` VARCHAR(20) NOT NULL,
`ItemVoidConfirmation` TINYINT (1) NOT NULL DEFAULT 0,
`EnableEVAT` TINYINT (1) NOT NULL DEFAULT 0,
`FORM_Behavior` VARCHAR(20) NOT NULL DEFAULT 'NON_MODAL',
`MarqueeMessage` VARCHAR(255) NOT NULL DEFAULT ' Your suggestive selling message and/or description.',
`TrustFund` DECIMAL(5,2) NOT NULL DEFAULT 0.00,
PRIMARY KEY (TerminalID),
INDEX `IX_tblTerminal`(`TerminalID`),
UNIQUE `PK_tblTerminal`(`TerminalNo`, `MachineSerialNo`),
INDEX `IX1_tblTerminal`(`TerminalNo`, `TerminalCode`),
INDEX `IX2_tblTerminal`(`TerminalCode`, `TerminalName`),
INDEX `IX3_tblTerminal`(`TerminalID`, `TerminalCode`, `TerminalName`)
);

INSERT INTO tblTerminal (`TerminalNo`, `TerminalCode`, `TerminalName`, DateCreated, MachineSerialNo, AccreditationNo )
		VALUES		('01', 'Terminal No. 01', 'Terminal No. 01', NOW(), 'AR-00000001', 'AC-00000001');

/*****************************
**	tblWithHold
*****************************/
DROP TABLE IF EXISTS tblWithHold;
CREATE TABLE tblWithHold (
`WithholdID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PaymentType` INT(10) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
PRIMARY KEY (`WithholdID`),
INDEX `IX_tblWithHold`(`WithholdID`),
INDEX `IX1_tblWithHold`(`DateCreated`),
INDEX `IX2_tblWithHold`(`TerminalNo`),
INDEX `IX3_tblWithHold`(`DateCreated`, `TerminalNo`),
INDEX `IX4_tblWithHold`(`CashierID`),
FOREIGN KEY (`CashierID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
);

/*****************************
**	tblDisburse
*****************************/
DROP TABLE IF EXISTS tblDisburse;
CREATE TABLE tblDisburse (
`DisburseID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PaymentType` INT(10) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
PRIMARY KEY (`DisburseID`),
INDEX `IX_tblDisburse`(`DisburseID`),
INDEX `IX1_tblDisburse`(`DateCreated`),
INDEX `IX2_tblDisburse`(`TerminalNo`),
INDEX `IX3_tblDisburse`(`DateCreated`, `TerminalNo`),
INDEX `IX4_tblDisburse`(`CashierID`),
FOREIGN KEY (`CashierID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
);

/*****************************
**	tblPaidOut
*****************************/
DROP TABLE IF EXISTS tblPaidOut;
CREATE TABLE tblPaidOut (
`PaidOutID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PaymentType` INT(10) NOT NULL DEFAULT 0,
`Remarks` VARCHAR(150) NOT NULL,
`DateCreated` DATETIME NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
PRIMARY KEY (`PaidOutID`),
INDEX `IX_tblPaidOut`(`PaidOutID`),
INDEX `IX1_tblPaidOut`(`DateCreated`),
INDEX `IX2_tblPaidOut`(`TerminalNo`),
INDEX `IX3_tblPaidOut`(`DateCreated`, `TerminalNo`),
INDEX `IX4_tblPaidOut`(`CashierID`),
FOREIGN KEY (`CashierID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
);

/*****************************
**	tblCardTypes
*****************************/
DROP TABLE IF EXISTS tblCardTypes;
CREATE TABLE tblCardTypes (
`CardTypeID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`CardTypeCode` VARCHAR(30) NOT NULL,
`CardTypeName` VARCHAR(30) NOT NULL,
PRIMARY KEY (`CardTypeID`),
INDEX `IX_tblCardTypes`(`CardTypeID`, `CardTypeCode`, `CardTypeName`),
UNIQUE `PK_tblCardTypes`(`CardTypeCode`),
INDEX `IX1_tblCardTypes`(`CardTypeID`),
INDEX `IX2_tblCardTypes`(`CardTypeCode`),
INDEX `IX3_tblCardTypes`(`CardTypeName`)
);

/*****************************
**	tblCashPayment
*****************************/
DROP TABLE IF EXISTS tblCashPayment;
CREATE TABLE tblCashPayment (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`Amount`  DECIMAL(18,2) NOT NULL DEFAULT 0,
`Remarks` VARCHAR(255) NOT NULL,
INDEX `IX_tblCashPayment`(`TransactionID`),
INDEX `IX1_tblCashPayment`(`Remarks`)
);

/*****************************
**	tblChequePayment
*****************************/
DROP TABLE IF EXISTS tblChequePayment;
CREATE TABLE tblChequePayment (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`ChequeNo` VARCHAR(30) NOT NULL,
`Amount`  DECIMAL(18,2) NOT NULL DEFAULT 0,
`ValidityDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`Remarks` VARCHAR(255) NOT NULL,
INDEX `IX_tblChequePayment`(`TransactionID`),
INDEX `IX1_tblChequePayment`(`ChequeNo`),
INDEX `IX2_tblChequePayment`(`ValidityDate`),
INDEX `IX3_tblChequePayment`(`Remarks`)
);

/*****************************
**	tblCreditCardPayment
*****************************/
DROP TABLE IF EXISTS tblCreditCardPayment;
CREATE TABLE tblCreditCardPayment (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`Amount`  DECIMAL(18,2) NOT NULL DEFAULT 0,
`CardTypeID` INT(10) UNSIGNED NOT NULL DEFAULT 0,
`CardTypeCode` VARCHAR(30) NOT NULL,
`CardTypeName` VARCHAR(30) NOT NULL,
`CardNo` VARCHAR(30) NOT NULL,
`CardHolder` VARCHAR(150) NOT NULL,
`ValidityDates` VARCHAR(14) NOT NULL,
`Remarks` VARCHAR(255) NOT NULL,
INDEX `IX_tblCreditCardPayment`(`TransactionID`),
INDEX `IX1_tblCreditCardPayment`(`CardTypeID`),
INDEX `IX2_tblCreditCardPayment`(`CardTypeCode`),
INDEX `IX3_tblCreditCardPayment`(`CardTypeName`),
INDEX `IX4_tblCreditCardPayment`(`CardNo`),
INDEX `IX5_tblCreditCardPayment`(`CardHolder`),
INDEX `IX6_tblCreditCardPayment`(`Remarks`)
);

/*****************************
**	tblCreditPayment
*****************************/
DROP TABLE IF EXISTS tblCreditPayment;
CREATE TABLE tblCreditPayment (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`Amount`  DECIMAL(18,2) NOT NULL DEFAULT 0,
`ContactID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`Remarks` VARCHAR(255) NOT NULL,
`AmountPaid`  DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblCreditPayment`(`TransactionID`),
INDEX `IX1_tblCreditPayment`(`ContactID`),
FOREIGN KEY (`ContactID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
INDEX `IX2_tblCreditPayment`(`Remarks`)
);

/*****************************
**	tblDenomination
*****************************/
DROP TABLE IF EXISTS tblDenomination;
CREATE TABLE tblDenomination (
`DenominationID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`DenominationCode` VARCHAR(100) NOT NULL,
`DenominationValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ImagePath` VARCHAR(100) NOT NULL,
PRIMARY KEY (`DenominationID`),
INDEX `IX_tblDenomination`(`DenominationID`),
UNIQUE `PX_tblDenomination`(`DenominationCode`),
INDEX `IX1_tblDenomination`(`DenominationCode`)
);

/*****************************
**	tblCashCount
*****************************/
DROP TABLE IF EXISTS tblCashCount;
CREATE TABLE tblCashCount (
`CashCountID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`DateCreated` DATETIME NOT NULL,
`DenominationID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblDenomination(`DenominationID`),
`DenominationCount` INT(10) NOT NULL DEFAULT 0,
`DenominationAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
PRIMARY KEY (`CashCountID`),
INDEX `IX_tblCashCount`(`CashCountID`),
UNIQUE `PK_tblCashCount`(`CashCountID`),
INDEX `IX1_tblCashCount`(`DenominationID`),
FOREIGN KEY (`DenominationID`) REFERENCES tblDenomination(`DenominationID`) ON DELETE RESTRICT
);

/*****************************
**	tblTransactionNos
*****************************/
DROP TABLE IF EXISTS tblTransactionNos;
CREATE TABLE tblTransactionNos (
`TransactionNo` VARCHAR(30) NOT NULL,
PRIMARY KEY (`TransactionNo`),
INDEX `IX_tblTransactionNos`(`TransactionNo`),
UNIQUE `PK_tblTransactionNos`(`TransactionNo`)
);

/*****************************
**	tblTransactions01
*****************************/
DROP TABLE IF EXISTS tblTransactions01;
CREATE TABLE tblTransactions01 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions01`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions01`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions01`(`TransactionNo`),
INDEX `IX2_tblTransactions01`(`CustomerID`),
INDEX `IX3_tblTransactions01`(`CashierID`)
);

/*****************************
**	tblTransactions02
*****************************/
DROP TABLE IF EXISTS tblTransactions02;
CREATE TABLE tblTransactions02 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions02`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions02`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions02`(`TransactionNo`),
INDEX `IX2_tblTransactions02`(`CustomerID`),
INDEX `IX3_tblTransactions02`(`CashierID`)
);

/*****************************
**	tblTransactions03
*****************************/
DROP TABLE IF EXISTS tblTransactions03;
CREATE TABLE tblTransactions03 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions03`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions03`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions03`(`TransactionNo`),
INDEX `IX2_tblTransactions03`(`CustomerID`),
INDEX `IX3_tblTransactions03`(`CashierID`)
);

/*****************************
**	tblTransactions04
*****************************/
DROP TABLE IF EXISTS tblTransactions04;
CREATE TABLE tblTransactions04 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions04`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions04`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions04`(`TransactionNo`),
INDEX `IX2_tblTransactions04`(`CustomerID`),
INDEX `IX3_tblTransactions04`(`CashierID`)
);

/*****************************
**	tblTransactions05
*****************************/
DROP TABLE IF EXISTS tblTransactions05;
CREATE TABLE tblTransactions05 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions05`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions05`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions05`(`TransactionNo`),
INDEX `IX2_tblTransactions05`(`CustomerID`),
INDEX `IX3_tblTransactions05`(`CashierID`)
);


/*****************************
**	tblTransactions06
*****************************/
DROP TABLE IF EXISTS tblTransactions06;
CREATE TABLE tblTransactions06 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions06`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions06`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions06`(`TransactionNo`),
INDEX `IX2_tblTransactions06`(`CustomerID`),
INDEX `IX3_tblTransactions06`(`CashierID`)
);

/*****************************
**	tblTransactions07
*****************************/
DROP TABLE IF EXISTS tblTransactions07;
CREATE TABLE tblTransactions07 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions07`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions07`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions07`(`TransactionNo`),
INDEX `IX2_tblTransactions07`(`CustomerID`),
INDEX `IX3_tblTransactions07`(`CashierID`)
);

/*****************************
**	tblTransactions08
*****************************/
DROP TABLE IF EXISTS tblTransactions08;
CREATE TABLE tblTransactions08 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions08`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions08`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions08`(`TransactionNo`),
INDEX `IX2_tblTransactions08`(`CustomerID`),
INDEX `IX3_tblTransactions08`(`CashierID`)
);

/*****************************
**	tblTransactions09
*****************************/
DROP TABLE IF EXISTS tblTransactions09;
CREATE TABLE tblTransactions09 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions09`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions09`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions09`(`TransactionNo`),
INDEX `IX2_tblTransactions09`(`CustomerID`),
INDEX `IX3_tblTransactions09`(`CashierID`)
);

/*****************************
**	tblTransactions10
*****************************/
DROP TABLE IF EXISTS tblTransactions10;
CREATE TABLE tblTransactions10 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions10`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions10`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions10`(`TransactionNo`),
INDEX `IX2_tblTransactions10`(`CustomerID`),
INDEX `IX3_tblTransactions10`(`CashierID`)
);

/*****************************
**	tblTransactions11
*****************************/
DROP TABLE IF EXISTS tblTransactions11;
CREATE TABLE tblTransactions11 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions11`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions11`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions11`(`TransactionNo`),
INDEX `IX2_tblTransactions11`(`CustomerID`),
INDEX `IX3_tblTransactions11`(`CashierID`)
);

/*****************************
**	tblTransactions12
*****************************/
DROP TABLE IF EXISTS tblTransactions12;
CREATE TABLE tblTransactions12 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions12`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions12`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions12`(`TransactionNo`),
INDEX `IX2_tblTransactions12`(`CustomerID`),
INDEX `IX3_tblTransactions12`(`CashierID`)
);

/*****************************
**	tblTransactionItems01
*****************************/
DROP TABLE IF EXISTS tblTransactionItems01;
CREATE TABLE tblTransactionItems01 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions01(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems01`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems01`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems01`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems01`(`ProductCode`),
INDEX `IX3_tblTransactionItems01`(`TransactionID`),
INDEX `IX4_tblTransactionItems01`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems02
*****************************/
DROP TABLE IF EXISTS tblTransactionItems02;
CREATE TABLE tblTransactionItems02 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions02(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems02`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems02`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems02`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems02`(`ProductCode`),
INDEX `IX3_tblTransactionItems02`(`TransactionID`),
INDEX `IX4_tblTransactionItems02`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems03
*****************************/
DROP TABLE IF EXISTS tblTransactionItems03;
CREATE TABLE tblTransactionItems03 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions03(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems03`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems03`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems03`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems03`(`ProductCode`),
INDEX `IX3_tblTransactionItems03`(`TransactionID`),
INDEX `IX4_tblTransactionItems03`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems04
*****************************/
DROP TABLE IF EXISTS tblTransactionItems04;
CREATE TABLE tblTransactionItems04 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions04(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems04`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems04`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems04`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems04`(`ProductCode`),
INDEX `IX3_tblTransactionItems04`(`TransactionID`),
INDEX `IX4_tblTransactionItems04`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems05
*****************************/
DROP TABLE IF EXISTS tblTransactionItems05;
CREATE TABLE tblTransactionItems05 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions05(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems05`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems05`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems05`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems05`(`ProductCode`),
INDEX `IX3_tblTransactionItems05`(`TransactionID`),
INDEX `IX4_tblTransactionItems05`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems06
*****************************/
DROP TABLE IF EXISTS tblTransactionItems06;
CREATE TABLE tblTransactionItems06 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions06(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems06`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems06`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems06`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems06`(`ProductCode`),
INDEX `IX3_tblTransactionItems06`(`TransactionID`),
INDEX `IX4_tblTransactionItems06`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems07
*****************************/
DROP TABLE IF EXISTS tblTransactionItems07;
CREATE TABLE tblTransactionItems07 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions07(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems07`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems07`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems07`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems07`(`ProductCode`),
INDEX `IX3_tblTransactionItems07`(`TransactionID`),
INDEX `IX4_tblTransactionItems07`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems08
*****************************/
DROP TABLE IF EXISTS tblTransactionItems08;
CREATE TABLE tblTransactionItems08 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions08(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems08`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems08`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems08`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems08`(`ProductCode`),
INDEX `IX3_tblTransactionItems08`(`TransactionID`),
INDEX `IX4_tblTransactionItems08`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems09
*****************************/
DROP TABLE IF EXISTS tblTransactionItems09;
CREATE TABLE tblTransactionItems09 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions09(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems09`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems09`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems09`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems09`(`ProductCode`),
INDEX `IX3_tblTransactionItems09`(`TransactionID`),
INDEX `IX4_tblTransactionItems09`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems10
*****************************/
DROP TABLE IF EXISTS tblTransactionItems10;
CREATE TABLE tblTransactionItems10 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions10(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems10`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems10`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems10`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems10`(`ProductCode`),
INDEX `IX3_tblTransactionItems10`(`TransactionID`),
INDEX `IX4_tblTransactionItems10`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems11
*****************************/
DROP TABLE IF EXISTS tblTransactionItems11;
CREATE TABLE tblTransactionItems11 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions11(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems11`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems11`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems11`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems11`(`ProductCode`),
INDEX `IX3_tblTransactionItems11`(`TransactionID`),
INDEX `IX4_tblTransactionItems11`(`ProductUnitID`)
);

/*****************************
**	tblTransactionItems12
*****************************/
DROP TABLE IF EXISTS tblTransactionItems12;
CREATE TABLE tblTransactionItems12 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions12(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems12`(`TransactionItemsID`),
INDEX `IX9_tblTransactionItems12`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems12`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems12`(`ProductCode`),
INDEX `IX3_tblTransactionItems12`(`TransactionID`),
INDEX `IX4_tblTransactionItems12`(`ProductUnitID`)
);

/*****************************
**	tblTerminalReport
*****************************/
DROP TABLE IF EXISTS tblTerminalReport;
CREATE TABLE tblTerminalReport (
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`BeginningTransactionNo` VARCHAR(30) NOT NULL,
`EndingTransactionNo` VARCHAR(30) NOT NULL,
`ZReadCount` INT(10) NOT NULL DEFAULT 0,
`XReadCount` INT(10) NOT NULL DEFAULT 0,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`OldGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NewGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonEVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`DateLastInitialized` DATETIME NOT NULL,
PRIMARY KEY (TerminalNo),
INDEX `IX_tblTerminalReport`(`TerminalNo`),
UNIQUE `PK_tblTerminalReport`(`TerminalNo`),
INDEX `IX1_tblTerminalReport`(`ZReadCount`),
INDEX `IX2_tblTerminalReport`(`XReadCount`)
);

INSERT INTO tblTerminalReport (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
		VALUES		('00000000000001', '00000000000001', 1, 1, 1, '01', DATE_SUB(DATE(NOW()), INTERVAL 1 DAY));

/*****************************
**	tblTerminalReportHistory
*****************************/
DROP TABLE IF EXISTS tblTerminalReportHistory;
CREATE TABLE tblTerminalReportHistory (
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`BeginningTransactionNo` VARCHAR(30) NOT NULL,
`EndingTransactionNo` VARCHAR(30) NOT NULL,
`ZReadCount` INT(10) NOT NULL DEFAULT 0,
`XReadCount` INT(10) NOT NULL DEFAULT 0,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`OldGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NewGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonEVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`DateLastInitialized` DATETIME NOT NULL,
INDEX `IX_tblTerminalReport`(`TerminalNo`),
INDEX `IX1_tblTerminalReport`(`ZReadCount`),
INDEX `IX2_tblTerminalReport`(`XReadCount`)
);

INSERT INTO tblTerminalReportHistory (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
		VALUES		('00000000000000', '00000000000000', 0, 0, 1, '01', DATE_SUB(DATE(NOW()), INTERVAL 2 DAY));
		
/*****************************
**	tblCashierReport
*****************************/
DROP TABLE IF EXISTS tblCashierReport;
CREATE TABLE tblCashierReport (
`CashierID` BIGINT(20) NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`CashCount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LastLoginDate` DATETIME NOT NULL,
PRIMARY KEY (`CashierID`, `TerminalNo`),
INDEX `IX_tblCashierReport`(`CashierID`, `TerminalNo`),
UNIQUE `PK_tblCashierReport`(`CashierID`, `TerminalNo`),
INDEX `IX1_tblCashierReport`(`CashierID`),
INDEX `IX2_tblCashierReport`(`TerminalNo`),
INDEX `IX3_tblCashierReport`(`TerminalID`)
);

/*****************************
**	tblCashierReportHistory
*****************************/
DROP TABLE IF EXISTS tblCashierReportHistory;
CREATE TABLE tblCashierReportHistory (
`CashierID` BIGINT(20) NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`CashCount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LastLoginDate` DATETIME NOT NULL,
INDEX `IX_tblCashierReport`(`CashierID`, `TerminalNo`),
INDEX `IX1_tblCashierReport`(`CashierID`),
INDEX `IX2_tblCashierReport`(`TerminalNo`),
INDEX `IX3_tblCashierReport`(`TerminalID`)
);

INSERT INTO tblCashierReport (`CashierID`, `TerminalID`, `TerminalNo`, `LastLoginDate`)
		VALUES		(1, 1, '01', "0001-01-01 00:00");

USE mysql;
GRANT ALL PRIVILEGES ON posresto.* TO POSUser IDENTIFIED BY 'pospwd' WITH GRANT OPTION;
FLUSH PRIVILEGES;
DELETE FROM user WHERE user = '' OR user = null;
UPDATE user SET password = OLD_PASSWORD('pospwd') WHERE user = 'POSUser';
FLUSH PRIVILEGES;

USE posresto;

INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('One Thousand Pesos', 1000.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Five Hundred Pesos', 500.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Two Hundred Pesos', 100.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('One Hundred Pesos', 100.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Fifty Pesos', 50.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Twenty Pesos', 20.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Ten Pesos', 10.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Five Pesos', 5.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('One Peso', 1.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Fifty Cents', 0.5, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('25 Cents', 0.25, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('10 Cents', 0.10, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('5 Cents', 0.05, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('1 Cent', 0.01, '');

INSERT INTO tblCountry(countryname) values ('Philippines');
INSERT INTO tblCountry(countryname) values ('USA');

INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Administrators', 'Default group for administrators. Has access on all rights.');
INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Managers', 'Default group for managers.');
INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Supervisors', 'Default group for supervisors.');
INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Cashiers', 'Default group for cashiers.');

INSERT INTO sysAccessUsers (Username, Password, DateCreated ) VALUES ('admin', 'admin', '2004-10-17 13:37:45.293');

INSERT INTO sysAccessUserDetails (UID ,Name, CountryID, GroupID) VALUES (1, 'Lemuel E. Aceron', 1, 1);


INSERT INTO sysAccessTypes (TypeName) VALUES ('LoginBE');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Home');
INSERT INTO sysAccessTypes (TypeName) VALUES ('MasterFilesMenu');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CardType');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ChargeType');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Variations');
INSERT INTO sysAccessTypes (TypeName) VALUES ('UnitMeasurement');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductGroups');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductGroupVariations');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductSubGroups');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductSubGroupVariations');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Products');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductVariations');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductPackage');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductVariationsPackage');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Discounts');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Promos');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Contacts');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ContactGroups');
INSERT INTO sysAccessTypes (TypeName) VALUES ('InventoryMenu');
INSERT INTO sysAccessTypes (TypeName) VALUES ('InventoryList');
INSERT INTO sysAccessTypes (TypeName) VALUES ('StockTypes');
INSERT INTO sysAccessTypes (TypeName) VALUES ('StockTransactions');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReportsMenu');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ProductsListReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('InventoryReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReorderReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('OverStockReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PricesReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('SalesTransactionReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ContactsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CustomerCreditReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CustomersWithCreditReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('MostSalableItemsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('WorstSalableItemsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('DiscountsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintReceiptsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReturnedItemsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('VoidedItemsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('RefundedItemsReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('SalesReports');
INSERT INTO sysAccessTypes (TypeName) VALUES ('AdministrationFilesMenu');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CompanyInfo');
INSERT INTO sysAccessTypes (TypeName) VALUES ('AccessGroups');
INSERT INTO sysAccessTypes (TypeName) VALUES ('AccessUsers');
INSERT INTO sysAccessTypes (TypeName) VALUES ('AccessRights');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReportFormat');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Terminal');

INSERT INTO sysAccessTypes (TypeName) VALUES ('LoginFE');
INSERT INTO sysAccessTypes (TypeName) VALUES ('InitializeXRead');
INSERT INTO sysAccessTypes (TypeName) VALUES ('InitializeZRead');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CreateTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CloseTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('SuspendTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ResumeTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('VoidTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('RefundTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReprintTransaction');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Withhold');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Disburse');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PaidOut');
INSERT INTO sysAccessTypes (TypeName) VALUES ('Denomination');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CashCount');
INSERT INTO sysAccessTypes (TypeName) VALUES ('CreditPayment');
INSERT INTO sysAccessTypes (TypeName) VALUES ('OpenDrawer');
INSERT INTO sysAccessTypes (TypeName) VALUES ('VoidItem');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ChangePrice');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ChangeQuantity');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintXRead');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintZRead');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintTerminalReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintCashierReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintHourlyReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintGroupReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintPLUReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintElectronicJournal');
INSERT INTO sysAccessTypes (TypeName) VALUES ('LogoutFE');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ApplyItemDiscount');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ApplyTransDiscount');
INSERT INTO sysAccessTypes (TypeName) VALUES ('EnterCreditPayment');
INSERT INTO sysAccessTypes (TypeName) VALUES ('PrintTransactionHeader');
INSERT INTO sysAccessTypes (TypeName) VALUES ('AyalaInfo');
INSERT INTO sysAccessTypes (TypeName) VALUES ('LockTerminal');
INSERT INTO sysAccessTypes (TypeName) VALUES ('UnlockTerminal');
INSERT INTO sysAccessTypes (TypeName) VALUES ('EnterFloat');
INSERT INTO sysAccessTypes (TypeName) VALUES ('LoginLogoutReport');
INSERT INTO sysAccessTypes (TypeName) VALUES ('ReturnItem');

/*****************************
**	Administrator Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 87, 1, 1);

/*****************************
**	Managers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 87, 1, 1);

/*****************************
**	Supervisors Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 87, 1, 1);

/*****************************
**	Cashiers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 87, 1, 1);


INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 1,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 2,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 3,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 4,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 5,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 6,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 7,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 8,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 9,  1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 10, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 11, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 12, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 13, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 14, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 15, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 16, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 17, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 18, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 19, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 20, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 21, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 22, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 23, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 24, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 25, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 26, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 27, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 28, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 29, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 30, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 31, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 32, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 33, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 34, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 35, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 36, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 37, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 38, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 39, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 40, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 41, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 42, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 43, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 44, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 45, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 46, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 47, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 48, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 49, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 50, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 51, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 52, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 53, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 54, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 55, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 56, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 57, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 58, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 59, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 60, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 61, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 62, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 63, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 64, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 65, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 66, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 67, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 68, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 69, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 70, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 71, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 72, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 73, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 74, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 75, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 76, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 77, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 78, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 79, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 80, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 81, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 82, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 83, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 84, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 85, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 86, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 87, 1, 1);


/*****************************
**	Products Default Definition
*****************************/
INSERT INTO tblVariations (VariationCode, VariationType) VALUES ('EXP', 'EXPIRATION');
INSERT INTO tblVariations (VariationCode, VariationType) VALUES ('SZE', 'SIZE');
INSERT INTO tblVariations (VariationCode, VariationType) VALUES ('COL', 'COLOR');
INSERT INTO tblVariations (VariationCode, VariationType) VALUES ('LEN', 'LENGTH');
INSERT INTO tblVariations (VariationCode, VariationType) VALUES ('WID', 'WIDTH');

INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('PC', 'PIECE (S)');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('CTN', 'CARTON');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('BAG', 'BAG');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('BTL', 'BOTTLE');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('BOX', 'BOX');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('CAN', 'CAN');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('CTR', 'CONTAINER');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('CSE', 'CASE');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('DRM', 'DRUM');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('GAL', 'GALLON');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('KL', 'KILO');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('LTR', 'LITER');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('ROL', 'ROLL');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('SCK', 'SACK');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('YRD', 'YARD');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('DOZ', 'DOZEN');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('QRT', 'QUART');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('BDL', 'BUNDLE');
INSERT INTO tblUnit (UnitCode, UnitName) VALUES ('MTR', 'METER');

/*****************************
**	Added on March 8, 2007
**	Lemuel E. Aceron
*****************************/

INSERT INTO sysAccessTypes (TypeName) VALUES ('Branch');
INSERT INTO sysAccessTypes (TypeName) VALUES ('BranchTransfer');
INSERT INTO sysAccessTypes (TypeName) VALUES ('BranchUpload');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 90, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 88, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 89, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 90, 1, 1);

/*****************************
**	tblBranch
*****************************/
DROP TABLE IF EXISTS tblBranch;
CREATE TABLE tblBranch (
`BranchID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
`BranchCode` VARCHAR(30) NOT NULL,
`BranchName` VARCHAR(50) NOT NULL,
`DBIP` VARCHAR(20),
`DBPort` VARCHAR(4),
`Address` VARCHAR(120),
`Remarks` VARCHAR(120),
PRIMARY KEY (BranchID),
INDEX `IX_tblBranch`(`BranchID`),
INDEX `IX_tblBranch1`(`BranchID`, `BranchCode`),
UNIQUE `PK_tblBranch`(`BranchCode`)
);

INSERT INTO tblBranch(BranchID, BranchCode, BranchName, DBIP, DBPort, Address, Remarks)VALUES(0, 'Main','Main Branch','localhost','3306','Main branch','Main branch wherein all stocks comes in');

INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Transfer To Branch', 'Transfer To Branch', 0);
INSERT INTO tblStockType(`StockTypeCode`, `Description`, `StockDirection`)VALUES('Transfer From Branch', 'Transfer From Branch', 1);

UPDATE tblTerminal SET AccreditationNo = '0000-000-00000-000', MachineSerialNo = '000000';

/*****************************
**	tblDeposit
*****************************/
DROP TABLE IF EXISTS tblDeposit;
CREATE TABLE tblDeposit (
`DepositID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PaymentType` INT(10) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`ContactID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
PRIMARY KEY (`DepositID`),
INDEX `IX_tblDeposit`(`DepositID`),
INDEX `IX1_tblDeposit`(`DateCreated`),
INDEX `IX2_tblDeposit`(`TerminalNo`),
INDEX `IX3_tblDeposit`(`DateCreated`, `TerminalNo`),
INDEX `IX4_tblDeposit`(`CashierID`),
FOREIGN KEY (`CashierID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT,
FOREIGN KEY (`ContactID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT
);


ALTER TABLE tblTerminalReport ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReportHistory ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReport ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReportHistory ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

INSERT INTO sysAccessTypes (TypeName) VALUES ('Deposit');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 91, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 91, 1, 1);

/*****************************
**	tblDebitPayment
*****************************/
DROP TABLE IF EXISTS tblDebitPayment;
CREATE TABLE tblDebitPayment (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`Amount`  DECIMAL(18,2) NOT NULL DEFAULT 0,
`ContactID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`Remarks` VARCHAR(255) NOT NULL,
`AmountPaid`  DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblDebitPayment`(`TransactionID`),
INDEX `IX1_tblDebitPayment`(`ContactID`),
FOREIGN KEY (`ContactID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
INDEX `IX2_tblDebitPayment`(`Remarks`)
);

ALTER TABLE tblTransactions01 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;


use posresto;

/*****************************
**	Added on April 25, 2007
**	Lemuel E. Aceron
*****************************/ 

ALTER TABLE tblTransactions01 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;


/*****************************
**	Added on April 30, 2007
**	Lemuel E. Aceron
*****************************/ 

ALTER TABLE tblTransactions01 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions02 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions03 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions04 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions05 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions06 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions07 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions08 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions09 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions10 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions11 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions12 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);

ALTER TABLE tblTerminalReport ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;

alter table tblTerminal add `IsVATInclusive` TINYINT (1) NOT NULL DEFAULT 1;
alter table tblTerminal add `VAT` INT (2) NOT NULL DEFAULT 12;
alter table tblTerminal add `EVAT` INT (2) NOT NULL DEFAULT 0;
alter table tblTerminal add `LocalTax` INT (2) NOT NULL DEFAULT 0;

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
);

/*!40000 ALTER TABLE `sysAccessTypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `sysAccessGroupRights` DISABLE KEYS */;
/*!40000 ALTER TABLE `sysAccessRights` DISABLE KEYS */;


INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (92, 'Product Composition');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 92, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 92, 1, 1);

/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 
/*!40000 ALTER TABLE `sysAccessGroupRights` ENABLE KEYS */; 
/*!40000 ALTER TABLE `sysAccessRights` ENABLE KEYS */; 

ALTER TABLE tblTerminalReportHistory ADD `TrustFund` DECIMAL(5,2) NOT NULL DEFAULT 0.00;

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
);


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
);

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
); 


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
);

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
); 

/*****************************
**	tblPLUReport
**  PLU Report Extraction
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
);


/*****************************
**	tblPLUReport
**  PLU Report Extraction
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
);

/*****************************
**	tblRemoteBranchInventory
*****************************/
DROP TABLE IF EXISTS tblRemoteBranchInventory;
CREATE TABLE tblRemoteBranchInventory (
`BranchInventoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`VariationMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchID`),
PRIMARY KEY (BranchInventoryID),
INDEX `IX_tblRemoteBranchInventory`(`ProductCode`, `BranchID`),
INDEX `IX1_tblRemoteBranchInventory`(`BranchID`)
); 

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
);


/********************************************
Lemuel E. Aceron

Cater the requirement of RLC

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
);

ALTER TABLE tblProducts ADD `WillPrintProductComposition` TINYINT(1) NOT NULL DEFAULT 1;

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
        
-- Make a default DiscountCode for Senior Citizen Discount.
ALTER TABLE tblTerminal ADD SeniorCitizenDiscountCode VARCHAR(5);

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
);

/*********************************  v_2.0.0.2.sql END  *******************************************************/

/*********************************  v_2.0.0.3.sql START  *****************************************************/

-- Added procGenerateSalesPerItemByGroup for P&L per item., run retailplus_proc.sql

DELETE FROM tblSalesPerItem;

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.3';

ALTER TABLE tblTerminal ADD `IsTouchScreen` TINYINT(1) NOT NULL DEFAULT 0; 

ALTER TABLE tblTerminalReportHistory ADD `MallFileName` VARCHAR(30); 
ALTER TABLE tblTerminalReportHistory ADD `IsMallFileUploadComplete` TINYINT(1) NOT NULL DEFAULT 0; 

UPDATE tblTerminalReportHistory SET IsMallFileUploadComplete = 1 WHERE MallFileName IS NOT NULL;

ALTER TABLE tblDeposit ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblWithHold ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblDisburse ADD `Remarks` VARCHAR(255); 

/*********************************  v_2.0.0.3.sql END  *******************************************************/

/*********************************  v_2.0.0.4.sql START  *****************************************************/

-- Added procProductPackageUpdate for P&L per item., run retailplus_proc.sql

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.4';

/*****************************
**	tblProductPackagePriceHistory
*****************************/
DROP TABLE IF EXISTS tblProductPackagePriceHistory;
CREATE TABLE tblProductPackagePriceHistory (
	`HistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ChangeDate` DATETIME NOT NULL,
	`PurchasePriceBefore` DECIMAL(10,2),
	`PurchasePriceNow` DECIMAL(10,2),
	`SellingPriceBefore` DECIMAL(10,2),
	`SellingPriceNow` DECIMAL(10,2),
	`VATBefore` DECIMAL(10,2),
	`VATNow` DECIMAL(10,2),
	`EVATBefore` DECIMAL(10,2),
	`EVATNow` DECIMAL(10,2),
	`LocalTaxBefore` DECIMAL(10,2),
	`LocalTaxNow` DECIMAL(10,2),
PRIMARY KEY (`HistoryID`),
INDEX `IX_tblProductPackagePriceHistory`(`HistoryID`),
INDEX `IX1_tblProductPackagePriceHistory`(`UID`),
INDEX `IX2_tblProductPackagePriceHistory`(`ChangeDate`)
);

/*****************************
**	tblMatrixPackagePriceHistory
*****************************/
DROP TABLE IF EXISTS tblMatrixPackagePriceHistory;
CREATE TABLE tblMatrixPackagePriceHistory (
	`HistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`UID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
	`PackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ChangeDate` DATETIME NOT NULL,
	`PurchasePriceBefore` DECIMAL(10,2),
	`PurchasePriceNow` DECIMAL(10,2),
	`SellingPriceBefore` DECIMAL(10,2),
	`SellingPriceNow` DECIMAL(10,2),
	`VATBefore` DECIMAL(10,2),
	`VATNow` DECIMAL(10,2),
	`EVATBefore` DECIMAL(10,2),
	`EVATNow` DECIMAL(10,2),
	`LocalTaxBefore` DECIMAL(10,2),
	`LocalTaxNow` DECIMAL(10,2),
PRIMARY KEY (`HistoryID`),
INDEX `IX_tblMatrixPackagePriceHistory`(`HistoryID`),
INDEX `IX1_tblMatrixPackagePriceHistory`(`UID`),
INDEX `IX2_tblMatrixPackagePriceHistory`(`ChangeDate`)
);

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (127, 'Change Product Price');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 127, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 127, 1, 1);

/*********************************  v_2.0.0.4.sql END  *******************************************************/

/*********************************  v_2.0.0.5.sql START  *****************************************************/
-- SELECT IFNULL(AllowRead,0) as 'Read', IFNULL(AllowWrite,0) as 'Write'
-- FROM sysAccessRights a INNER JOIN sysAccessTypes b ON a.TranTypeID = b.TypeID
-- WHERE UID = 1 AND TranTypeID = 3 AND Enabled=1


UPDATE tblTerminal SET DBVersion = 'v_2.0.0.5';

ALTER TABLE sysAccessTypes ADD Enabled smallint NOT NULL DEFAULT 1; 

-- Added August 2, 2009 to monitor if product still has/have variations
ALTER TABLE tblProducts ADD VariationCount BIGINT NOT NULL DEFAULT 0;

UPDATE tblProducts SET VariationCount = (SELECT COUNT(MatrixID) FROM tblProductBaseVariationsMatrix z WHERE tblProducts.ProductID = z.ProductID);

/*****************************
**	tblProductPrices
*****************************/
DROP TABLE IF EXISTS tblProductPrices;
CREATE TABLE tblProductPrices (
	`SessionID` VARCHAR(30) NOT NULL,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductCode` VARCHAR(30) NOT NULL,
	`ProductDescription` VARCHAR(30) NOT NULL,
	`MatrixDescription` VARCHAR(100) NOT NULL,
	`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductGroupName` VARCHAR(30) NOT NULL,
	`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	`ProductSubGroupName` VARCHAR(30) NOT NULL,
	`Quantity` DECIMAL(10,2),
	`UnitCode` VARCHAR(10) NOT NULL,
	`UnitName` VARCHAR(30) NOT NULL,
	`PurchasePrice` DECIMAL(10,2),
	`Price` DECIMAL(10,2),
	`VAT` DECIMAL(10,2),
	`EVAT` DECIMAL(10,2),
	`LocalTax` DECIMAL(10,2),
INDEX `IX_tblProductPrices`(`SessionID`),
INDEX `IX1_tblProductPrices`(`ProductID`),
INDEX `IX2_tblProductPrices`(`ProductGroupID`),
INDEX `IX3_tblProductPrices`(`ProductSubGroupID`)
);

/*********************************  v_2.0.0.5.sql END  *******************************************************/

/*********************************  v_2.0.0.6.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.6';

/*********************************
**
** May 29, 2011
** Lemuel E. Aceron
** Remove the 2 codes below to support the mysql version 5 and above
** No warning should be displayed from fresh install

ALTER TABLE sysAccessTypes DROP SequenceNo; 
ALTER TABLE sysAccessTypes DROP Category;
*********************************/

ALTER TABLE sysAccessTypes ADD SequenceNo INT NOT NULL DEFAULT 0; 
ALTER TABLE sysAccessTypes ADD Category VARCHAR(50) NOT NULL DEFAULT 'System Configurations'; 

UPDATE sysAccessTypes SET SequenceNo = 1, Category = '01: System Configurations' WHERE TypeID = 1;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '01: System Configurations' WHERE TypeID = 43;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '01: System Configurations' WHERE TypeID = 48;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '01: System Configurations' WHERE TypeID = 123;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '01: System Configurations' WHERE TypeID = 47;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '01: System Configurations' WHERE TypeID = 82;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '01: System Configurations' WHERE TypeID = 49;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '01: System Configurations' WHERE TypeID = 77;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '02: Backend - Administration' WHERE TypeID = 46;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '02: Backend - Administration' WHERE TypeID = 44;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '02: Backend - Administration' WHERE TypeID = 45;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '03: Backend - Menu' WHERE TypeID = 2;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '03: Backend - Menu' WHERE TypeID = 3;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '03: Backend - Menu' WHERE TypeID = 93;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '03: Backend - Menu' WHERE TypeID = 104;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '03: Backend - Menu' WHERE TypeID = 111;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '03: Backend - Menu' WHERE TypeID = 103;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '03: Backend - Menu' WHERE TypeID = 20;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '03: Backend - Menu' WHERE TypeID = 24;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '03: Backend - Menu' WHERE TypeID = 42;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '04: Backend - MasterFiles' WHERE TypeID = 4;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '04: Backend - MasterFiles' WHERE TypeID = 5;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '04: Backend - MasterFiles' WHERE TypeID = 16;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '04: Backend - MasterFiles' WHERE TypeID = 17;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '04: Backend - MasterFiles' WHERE TypeID = 18;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '04: Backend - MasterFiles' WHERE TypeID = 19;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '04: Backend - MasterFiles' WHERE TypeID = 88;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '04: Backend - MasterFiles' WHERE TypeID = 119;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 12;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 13;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 14;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 15;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 10;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 11;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 8;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 9;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 7;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 6;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 92;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 122;
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '05: Backend - MasterFiles - Products' WHERE TypeID = 127;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 94;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 105;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 106;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 107;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 109;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 108;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '07: Backend - Sales And Receivables' WHERE TypeID = 110;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '08: Backend - Inventory' WHERE TypeID = 21;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '08: Backend - Inventory' WHERE TypeID = 22;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '08: Backend - Inventory' WHERE TypeID = 23;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '08: Backend - Inventory' WHERE TypeID = 89;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '08: Backend - Inventory' WHERE TypeID = 90;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '08: Backend - Inventory' WHERE TypeID = 112;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '08: Backend - Inventory' WHERE TypeID = 113;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '08: Backend - Inventory' WHERE TypeID = 114;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '08: Backend - Inventory' WHERE TypeID = 115;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '08: Backend - Inventory' WHERE TypeID = 116;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '08: Backend - Inventory' WHERE TypeID = 117;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '08: Backend - Inventory' WHERE TypeID = 118;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '09: Backend - General Ledger' WHERE TypeID = 99;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '09: Backend - General Ledger' WHERE TypeID = 100;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '09: Backend - General Ledger' WHERE TypeID = 101;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '09: Backend - General Ledger' WHERE TypeID = 97;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '09: Backend - General Ledger' WHERE TypeID = 102;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '09: Backend - General Ledger' WHERE TypeID = 98;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '09: Backend - General Ledger' WHERE TypeID = 96;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '09: Backend - General Ledger' WHERE TypeID = 95;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '10: Backend - Inventory Reports' WHERE TypeID = 25;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '10: Backend - Inventory Reports' WHERE TypeID = 26;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '10: Backend - Inventory Reports' WHERE TypeID = 27;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '10: Backend - Inventory Reports' WHERE TypeID = 28;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '10: Backend - Inventory Reports' WHERE TypeID = 29;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '10: Backend - Inventory Reports' WHERE TypeID = 34;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '10: Backend - Inventory Reports' WHERE TypeID = 35;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '10: Backend - Inventory Reports' WHERE TypeID = 121;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '10: Backend - Inventory Reports' WHERE TypeID = 38;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '10: Backend - Inventory Reports' WHERE TypeID = 39;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '10: Backend - Inventory Reports' WHERE TypeID = 40;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '11: Backend - Sales Reports' WHERE TypeID = 41;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '11: Backend - Sales Reports' WHERE TypeID = 30;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '11: Backend - Sales Reports' WHERE TypeID = 36;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '12: Backend - Admin Reports' WHERE TypeID = 31;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '12: Backend - Admin Reports' WHERE TypeID = 32;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '12: Backend - Admin Reports' WHERE TypeID = 33;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '12: Backend - Admin Reports' WHERE TypeID = 86;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '13: Frontend - Administration' WHERE TypeID = 50;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '13: Frontend - Administration' WHERE TypeID = 51;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '13: Frontend - Administration' WHERE TypeID = 78;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '13: Frontend - Administration' WHERE TypeID = 79;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '13: Frontend - Administration' WHERE TypeID = 67;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '13: Frontend - Administration' WHERE TypeID = 126;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '13: Frontend - Administration' WHERE TypeID = 81;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '14: Frontend - Cashiering' WHERE TypeID = 52;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '14: Frontend - Cashiering' WHERE TypeID = 53;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '14: Frontend - Cashiering' WHERE TypeID = 54;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '14: Frontend - Cashiering' WHERE TypeID = 55;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '14: Frontend - Cashiering' WHERE TypeID = 56;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '14: Frontend - Cashiering' WHERE TypeID = 57;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '14: Frontend - Cashiering' WHERE TypeID = 58;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '14: Frontend - Cashiering' WHERE TypeID = 59;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '14: Frontend - Cashiering' WHERE TypeID = 60;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '14: Frontend - Cashiering' WHERE TypeID = 61;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '14: Frontend - Cashiering' WHERE TypeID = 62;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '14: Frontend - Cashiering' WHERE TypeID = 63;
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '14: Frontend - Cashiering' WHERE TypeID = 64;
UPDATE sysAccessTypes SET SequenceNo = 14, Category = '14: Frontend - Cashiering' WHERE TypeID = 65;
UPDATE sysAccessTypes SET SequenceNo = 15, Category = '14: Frontend - Cashiering' WHERE TypeID = 66;
UPDATE sysAccessTypes SET SequenceNo = 16, Category = '14: Frontend - Cashiering' WHERE TypeID = 68;
UPDATE sysAccessTypes SET SequenceNo = 17, Category = '14: Frontend - Cashiering' WHERE TypeID = 80;
UPDATE sysAccessTypes SET SequenceNo = 18, Category = '14: Frontend - Cashiering' WHERE TypeID = 83;
UPDATE sysAccessTypes SET SequenceNo = 19, Category = '14: Frontend - Cashiering' WHERE TypeID = 84;
UPDATE sysAccessTypes SET SequenceNo = 20, Category = '14: Frontend - Cashiering' WHERE TypeID = 85;
UPDATE sysAccessTypes SET SequenceNo = 21, Category = '14: Frontend - Cashiering' WHERE TypeID = 87;
UPDATE sysAccessTypes SET SequenceNo = 22, Category = '14: Frontend - Cashiering' WHERE TypeID = 91;
UPDATE sysAccessTypes SET SequenceNo = 23, Category = '14: Frontend - Cashiering' WHERE TypeID = 120;
UPDATE sysAccessTypes SET SequenceNo = 1, Category = '15: Frontend - Reports' WHERE TypeID = 69;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '15: Frontend - Reports' WHERE TypeID = 70;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '15: Frontend - Reports' WHERE TypeID = 71;
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '15: Frontend - Reports' WHERE TypeID = 72;
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '15: Frontend - Reports' WHERE TypeID = 73;
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '15: Frontend - Reports' WHERE TypeID = 74;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '15: Frontend - Reports' WHERE TypeID = 75;
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '15: Frontend - Reports' WHERE TypeID = 76;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '15: Frontend - Reports' WHERE TypeID = 124;
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '15: Frontend - Reports' WHERE TypeID = 125;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '15: Frontend - Reports' WHERE TypeID = 37;

        
/*********************************  v_2.0.0.6.sql END  *******************************************************/ 

/*********************************  v_2.0.0.7.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.7';


ALTER TABLE tblProductPackagePriceHistory ADD Remarks VARCHAR(75) NOT NULL DEFAULT 'Change Price Module';
ALTER TABLE tblMatrixPackagePriceHistory ADD Remarks VARCHAR(75) NOT NULL DEFAULT 'Change Price Module';

UPDATE tblProductPackagePriceHistory SET Remarks = 'Change Price Module';
UPDATE tblMatrixPackagePriceHistory SET Remarks = 'Change Price Module';
        
/*********************************  v_2.0.0.7.sql END  *******************************************************/ 

 /*********************************  v_2.0.0.8.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.8';


ALTER TABLE tblCashPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblChequePayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblCreditCardPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblCreditPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblDebitPayment ADD TransactionNo VARCHAR(30);

ALTER TABLE tblCashPayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblChequePayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblCreditPayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblDebitPayment MODIFY Remarks VARCHAR(255);
       
/*********************************
**
** May 29, 2011
** Lemuel E. Aceron
** Remove the codes below to support the mysql version 5 and above
** No warning should be displayed from fresh install
** Below code should be run if upgraded from lower version.

CALL procCreditPaymentSyncTransactionNo();
*********************************/

/*********************************  v_2.0.0.8.sql END  *******************************************************/  

 /*********************************  v_2.0.0.81.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.81';

ALTER TABLE tblSalesPerItem ADD `ProductGroup` VARCHAR(100) NOT NULL;
ALTER TABLE tblSalesPerItem ADD `ProductUnitCode` VARCHAR(30) NOT NULL;

/*********************************  v_2.0.0.81.sql END  *******************************************************/  

 /*********************************  v_2.0.0.9.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.9';

ALTER TABLE tblProducts DROP INDEX `IX_tblProducts`;
ALTER TABLE tblProducts DROP INDEX `PK_tblProducts`;

ALTER TABLE tblProducts ADD INDEX `IX_tblProducts` (`ProductID`, `ProductCode`,`ProductDesc`);
ALTER TABLE tblProducts ADD UNIQUE INDEX `PK_tblProducts` (`ProductCode`,`ProductDesc`);

ALTER TABLE tblProducts ADD `Active` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************  v_2.0.0.9.sql END  *******************************************************/  

 /*********************************  v_2.0.1.1.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.1.1';

ALTER TABLE tblProducts ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
UPDATE tblProducts SET `PercentageCommision` = 2 WHERE ProductID > 1;

ALTER TABLE tblTransactions01 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions01 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions02 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions02 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions03 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions03 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions04 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions04 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions05 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions05 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions06 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions06 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions07 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions07 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions08 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions08 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions09 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions09 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions10 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions10 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions11 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions11 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions12 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions12 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';

ALTER TABLE tblTransactionItems01 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems01 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;

DROP TABLE IF EXISTS tblAgentsCommision;
CREATE TABLE tblAgentsCommision (
`SessionID` VARCHAR(30) NOT NULL,
`TransactionNo` VARCHAR(30) NOT NULL,
`TransactionDate` DATETIME NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Commision` DECIMAL(18,2) NOT NULL DEFAULT 0,
INDEX `IX_tblAgentsCommision`(`SessionID`),
INDEX `IX_tblAgentsCommision1`(`Description`)
);

/*********************************
**
** May 29, 2011 
** Lemuel E. Aceron
** Remove the drop command since this is a new update version to mysql 5
** 

ALTER TABLE tblTransactions01 DROP `Commision`;
ALTER TABLE tblTransactions02 DROP `Commision`;
ALTER TABLE tblTransactions03 DROP `Commision`;
ALTER TABLE tblTransactions04 DROP `Commision`;
ALTER TABLE tblTransactions05 DROP `Commision`;
ALTER TABLE tblTransactions06 DROP `Commision`;
ALTER TABLE tblTransactions07 DROP `Commision`;
ALTER TABLE tblTransactions08 DROP `Commision`;
ALTER TABLE tblTransactions09 DROP `Commision`;
ALTER TABLE tblTransactions10 DROP `Commision`;
ALTER TABLE tblTransactions11 DROP `Commision`;
ALTER TABLE tblTransactions12 DROP `Commision`;
*********************************/

/*********************************  v_2.0.1.1.sql END  *******************************************************/    

 /*********************************  v_2.0.1.2.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.1.2';

DROP TABLE IF EXISTS tblProductPurchasePriceHistory;
CREATE TABLE tblProductPurchasePriceHistory (
`ProductPurchasePriceHistoryID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT(20) NOT NULL DEFAULT 1,
`MatrixID` BIGINT(20) NOT NULL DEFAULT 0,
`SupplierID` BIGINT(20) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseDate` DATETIME NOT NULL,
`Remarks` VARCHAR(50),
PRIMARY KEY (ProductPurchasePriceHistoryID),
INDEX `IX_tblProductPurchasePriceHistory`(`ProductID`),
INDEX `IX_tblProductPurchasePriceHistory1`(`SupplierID`)
);

ALTER TABLE tblStock ADD `Active` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************  v_2.0.1.2.sql END  *******************************************************/    

/*********************************  v_2.0.1.3.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.1.3';

ALTER TABLE tblTransactions01 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions01 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions02 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions03 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions04 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions05 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions06 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions07 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions08 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions09 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions10 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions11 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions12 ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `CreatedByName` VARCHAR(100);

UPDATE tblTransactions01 SET `CreatedByID` = CashierID	WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions01 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions02 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions02 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions03 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions03 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions04 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions04 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions05 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions05 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions06 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions06 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions07 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions07 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions08 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions08 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions09 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions09 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions10 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions10 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions11 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions11 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';
UPDATE tblTransactions12 SET `CreatedByID` = CashierID WHERE CashierName = NULL OR CashierName = ''; 
UPDATE tblTransactions12 SET `CreatedByName` = CashierName WHERE CashierName = NULL OR CashierName = '';

ALTER TABLE tblProducts ADD `QuantityIN` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD `QuantityOUT` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblProductBaseVariationsMatrix ADD `QuantityIN` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD `QuantityOUT` DECIMAL(18,2) NOT NULL DEFAULT 0;

UPDATE tblProducts SET `QuantityIN` = `Quantity`;
UPDATE tblProductBaseVariationsMatrix SET `QuantityIN` = `Quantity`;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (133, 'Synchronize Branch Products');

ALTER TABLE tblTerminal add `WillContinueSelectionVariation` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal add `WillContinueSelectionProduct` TINYINT (1) NOT NULL DEFAULT 0;
/*********************************  v_2.0.1.3.sql END  *******************************************************/    


/*********************************  v_2.0.1.4.sql START  *******************************************************/


UPDATE tblTerminal SET DBVersion = 'v_2.0.1.4';

ALTER TABLE tblTerminal ADD `RETPriceMarkUp` DECIMAL(18,2) NOT NULL DEFAULT 5;
ALTER TABLE tblTerminal ADD `WSPriceMarkUp` DECIMAL(18,2) NOT NULL DEFAULT 2;

ALTER TABLE tblProducts ADD `WSPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
UPDATE tblProducts SET `WSPrice` = PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100));

ALTER TABLE tblProductPackage ADD `WSPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
UPDATE tblProductPackage SET `WSPrice` = PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100));

ALTER TABLE tblProductBaseVariationsMatrix ADD `WSPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
UPDATE tblProductBaseVariationsMatrix SET `WSPrice` = PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100));

ALTER TABLE tblMatrixPackage ADD `WSPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
UPDATE tblMatrixPackage SET `WSPrice` = PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100));


/*********************************  v_2.0.1.4.sql END  *******************************************************/
    

/*********************************  v_2.0.1.5.sql START  *******************************************************/

ALTER TABLE tblAgentsCommision ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblAgentsCommision ADD `AgentName` VARCHAR(100);

/*****************************
**	Added on September 21, 2010 for Agent Commision Access
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (134, 'Agents Commision Report');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 134, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 134, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '11: Backend - Sales Reports' WHERE TypeID = 134;

ALTER TABLE tblTerminal ADD `WillPrintGrandTotal` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (135, 'Position');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 135, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 135, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '04: Backend - MasterFiles' WHERE TypeID = 135;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (136, 'Department');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 136, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 136, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '04: Backend - MasterFiles' WHERE TypeID = 136;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (137, 'Agents Sales Report');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 137, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 137, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '11: Backend - Sales Reports' WHERE TypeID = 137;

/*****************************
**	tblPositions
*****************************/
DROP TABLE IF EXISTS tblPositions;
CREATE TABLE tblPositions (
`PositionID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`PositionCode` VARCHAR(30) NOT NULL,
`PositionName` VARCHAR(30) NOT NULL,
PRIMARY KEY (`PositionID`),
INDEX `IX_tblPositions`(`PositionID`, `PositionCode`, `PositionName`),
UNIQUE `PK_tblPositions`(`PositionCode`),
INDEX `IX1_tblPositions`(`PositionID`),
INDEX `IX2_tblPositions`(`PositionCode`),
INDEX `IX3_tblPositions`(`PositionName`)
);

INSERT INTO tblPositions VALUES(1, 'System Default Position', 'System Default Position');



/*****************************
**	tblDepartments
*****************************/
DROP TABLE IF EXISTS tblDepartments;
CREATE TABLE tblDepartments (
`DepartmentID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`DepartmentCode` VARCHAR(30) NOT NULL,
`DepartmentName` VARCHAR(30) NOT NULL,
PRIMARY KEY (`DepartmentID`),
INDEX `IX_tblDepartments`(`DepartmentID`, `DepartmentCode`, `DepartmentName`),
UNIQUE `PK_tblDepartments`(`DepartmentCode`),
INDEX `IX1_tblDepartments`(`DepartmentID`),
INDEX `IX2_tblDepartments`(`DepartmentCode`),
INDEX `IX3_tblDepartments`(`DepartmentName`)
);

INSERT INTO tblDepartments VALUES(1, 'System Default Department', 'System Default Department');

ALTER TABLE tblContacts ADD `DepartmentID` INT(10) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblContacts ADD `PositionID` INT(10) UNSIGNED NOT NULL DEFAULT 1;

ALTER TABLE tblAgentsCommision ADD `DepartmentName` VARCHAR(30) NOT NULL;
ALTER TABLE tblAgentsCommision ADD `PositionName` VARCHAR(30) NOT NULL;

ALTER TABLE tblTransactions01 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions01 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions02 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions02 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions03 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions03 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions04 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions04 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions05 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions05 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions06 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions06 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions07 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions07 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions08 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions08 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions09 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions09 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions10 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions10 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions11 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions11 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions12 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions12 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';

UPDATE tblTransactions01 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions01 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions02 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions02 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions03 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions03 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions04 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions04 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions05 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions05 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions06 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions06 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions07 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions07 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions08 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions08 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions09 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions09 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions10 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions10 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions11 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions11 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions12 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions12 SET AgentPositionName = 'System Default Position';


/*********************************  v_2.0.1.5.sql END  *******************************************************/

/*********************************  v_3.0.0.0.sql START  *******************************************************/
/*****************************
**	  
**	Added: May 29, 2011 
**	Lemuel E. Aceron
**
**  Start supporting the new MySQL 5.5 and higher version
**  Remove the unsupported 'TYPE=INNODB COMMENT = '
**  
*****************************/

/*****************************
**	For Releasing of Items
**	Added: April 7, 2011 
**	Lemuel E. Aceron
*****************************/

UPDATE tblTerminal SET DBVersion = 'v_3.0.0.0';

DELETE FROM sysAccessGroupRights WHERE TranTypeID = 138;
DELETE FROM sysAccessRights WHERE TranTypeID = 138;
DELETE FROM sysAccessTypes WHERE TypeID = 138;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (138, 'Releasing of Items');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 138, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 138, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 24, Category = '14: Frontend - Cashiering' WHERE TypeID = 138;


/*****************************
**	Added on May 3, 2011 for releasing
**	Lemuel E. Aceron
*****************************/

ALTER TABLE tblTransactions01 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactions01 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions02 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions03 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions04 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions05 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions06 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions07 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions08 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions09 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions10 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions11 ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions12 ADD `ReleaserName` VARCHAR(100);

ALTER TABLE tblTransactions01 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions02 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions03 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions04 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions05 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions06 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions07 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions08 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions09 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions10 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions11 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions12 ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';

UPDATE tblTransactions01 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions02 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions03 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions04 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions05 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions06 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions07 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions08 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions09 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions10 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions11 SET `ReleasedDate` = '0001-01-01 12:00:00';
UPDATE tblTransactions12 SET `ReleasedDate` = '0001-01-01 12:00:00';

ALTER TABLE tblTerminal ADD `ReservedAndCommit` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `DebitDeposit` DECIMAL NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `DebitDeposit` DECIMAL NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReport ADD `DebitDeposit` DECIMAL NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `DebitDeposit` DECIMAL NOT NULL DEFAULT 0;

ALTER TABLE tblProducts ADD `ActualQuantity` DECIMAL NOT NULL DEFAULT 0;

ALTER TABLE tblProducts ADD `LastModified` TIMESTAMP DEFAULT NOW() ON UPDATE CURRENT_TIMESTAMP;

UPDATE tblTransactions01 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions02 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions03 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions04 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions05 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions06 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions07 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions08 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions09 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions10 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions11 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);
UPDATE tblTransactions12 SET TransactionStatus = 9 WHERE TransactionStatus IN (1, 5, 7, 8);

/*********************************  v_3.0.0.0.sql END  *******************************************************/
    
/*********************************  v_3.0.0.1.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = 'v_3.0.0.1';

/**************************************************************
** July 26, 2011
** Lemuel E. Aceron
**
**************************************************************/

DROP TABLE IF EXISTS tblProductMovement;
CREATE TABLE tblProductMovement (
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`ProductCode` VARCHAR(30) NOT NULL,
	`ProductDescription` VARCHAR(50) NOT NULL,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(100) NOT NULL,
	`QuantityFrom` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`QuantityTo` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`MatrixQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`UnitCode` VARCHAR(100) NOT NULL,
	`Remarks` VARCHAR(100) NOT NULL,
	`TransactionDate` DateTime NOT NULL,
	`TransactionNo` VARCHAR(100) NOT NULL,
INDEX `IX_tblProductMovement`(`ProductID`),
INDEX `IX_tblProductMovement1`(`MatrixDescription`)
);

ALTER TABLE tblProductMovement ADD `CreatedBy` VARCHAR(100) NOT NULL DEFAULT '';

DROP TABLE IF EXISTS sysTerminalkey;
CREATE TABLE sysTerminalkey (
	`HDSerialNo` VARCHAR(30) NOT NULL,
	`RegistrationKey` VARCHAR(255) NOT NULL,
INDEX `IX_sysTerminalkey`(`HDSerialNo`),
INDEX `IX_sysTerminalkey1`(`HDSerialNo`, `RegistrationKey`)
);

/*********************************  v_3.0.0.1.sql END  *******************************************************/

/*********************************  v_3.0.0.2.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.0.2';

ALTER TABLE tblProductMovement ADD `CreatedBy` VARCHAR(100) NOT NULL DEFAULT '';

DROP TABLE IF EXISTS sysTerminalkey;
CREATE TABLE sysTerminalkey (
	`HDSerialNo` VARCHAR(30) NOT NULL,
	`RegistrationKey` VARCHAR(255) NOT NULL,
INDEX `IX_sysTerminalkey`(`HDSerialNo`),
INDEX `IX_sysTerminalkey1`(`HDSerialNo`, `RegistrationKey`)
);

INSERT INTO sysTerminalkey VALUE ('K10HT77258WN', '3ZQXU3PxyN3/z1IiNNKIF41lzvqlIQ/YANXPgddDv5NjhvHLOMIvK6sggTT3dKE8');
INSERT INTO sysTerminalkey VALUE ('K834T9A2BJNB', 'VWHt9ZteRBNQZb9gBbnOGZDpjD70UL19Dzv6dZzEy5LmUJFI7i7zP4wQZ/G07hHs');
INSERT INTO sysTerminalkey VALUE ('WD-WXEY08TPJ153', 'XzdDZGO4tkW2IfafQSh7kXs8JEeCK0r2Qqc7yI9arTtPtcLG5r874HU4uaX/pEAq');
INSERT INTO sysTerminalkey VALUE ('587OCI98T', 'lLJPu/BhLTcF0XpSVZ/p3JH3Hp/zbCwy+5rUF/kj/YpVaZwDBjOxgzaT15jJY8Qu');
INSERT INTO sysTerminalkey VALUE ('9VP7QL84', 'VuQyYqBleUyCjuWIonIDCnxS3dBaZOsV0/0mn3znbktrXn4EfKsOTpxVOAxVw+Jw');

ALTER TABLE tblProducts ADD `RID` BIGINT NOT NULL DEFAULT 0;

/*********************************  v_3.0.0.2.sql END  *********************************************************/ 

/*********************************  v_3.0.0.3.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.0.3';

DROP TABLE IF EXISTS tblCountingRef;
CREATE TABLE tblCountingRef (
	`SessionID` VARCHAR(15) NOT NULL,
	`Counter` BIGINT NOT NULL DEFAULT 0,
	`ReferenceDate` DATE,
INDEX `IX_tblCountingRef`(`Counter`),
INDEX `IX_tblCountingRef1`(`SessionID`),
INDEX `IX_tblCountingRef2`(`ReferenceDate`)
);

/*********************************  v_3.0.0.3.sql END  *******************************************************/ 

/*********************************  v_3.0.3.4.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.4';

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (139, 'Inventory Analyst');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 139, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 139, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '08: Backend - Inventory' WHERE TypeID = 139;

ALTER TABLE tblProducts ADD RIDMinThreshold DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblProducts ADD RIDMaxThreshold DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD RIDMinThreshold DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD RIDMaxThreshold DECIMAL(10,2) DEFAULT 0;

/*********************************  v_3.0.3.4.sql END  *******************************************************/ 

/*********************************  v_3.0.3.5.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.5';

DROP TABLE IF EXISTS tblContactRewards;
CREATE TABLE tblContactRewards (
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
	`RewardCardNo` VARCHAR(15) DEFAULT '',
	`RewardActive` TINYINT(1) NOT NULL DEFAULT 0,
	`RewardPoints` DECIMAL (10,2) NOT NULL DEFAULT 0,
	`RewardAwardDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
INDEX `IX_tblContactRewards`(`CustomerID`),
INDEX `IX_tblContactRewards1`(`RewardCardNo`),
INDEX `IX_tblContactRewards2`(`CustomerID`, `RewardCardNo`),
UNIQUE `PK_tblContactRewards1`(`CustomerID`),
UNIQUE `PK_tblContactRewards2`(`RewardCardNo`)
);

DELETE FROM tblContactRewards WHERE CustomerID = 1;
INSERT INTO tblContactRewards VALUES(1, '', 1, 0, NOW());

ALTER TABLE tblTerminal DROP ShowCustomerSelection;
ALTER TABLE tblTerminal ADD ShowCustomerSelection TINYINT (1) NOT NULL DEFAULT 1;
update tblTerminal set ShowCustomerSelection = 0;

ALTER TABLE tblTerminal DROP EnableRewardPoints;
ALTER TABLE tblTerminal DROP RewardPointsEvery;
ALTER TABLE tblTerminal DROP RewardPointsMinimum;
ALTER TABLE tblTerminal DROP RewardPointsEvery;
ALTER TABLE tblTerminal DROP RewardPoints;
ALTER TABLE tblTerminal ADD EnableRewardPoints TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD RewardPointsMinimum DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsEvery DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPoints DECIMAL(10,2) DEFAULT 0;

ALTER TABLE tblProducts DROP RewardPoints;
ALTER TABLE tblProducts ADD RewardPoints DECIMAL(10,2) DEFAULT 0;

DROP TABLE IF EXISTS tblContactRewardsMovement;
CREATE TABLE tblContactRewardsMovement (
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 1 REFERENCES tblContacts(`ContactID`),
	`RewardDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`RewardPointsBefore` BIGINT NOT NULL DEFAULT 0,
	`RewardPointsAdjustment` BIGINT NOT NULL DEFAULT 0,
	`RewardPointsAfter` BIGINT NOT NULL DEFAULT 0,
	`RewardExpiryDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`RewardReason` VARCHAR(150) NOT NULL,
	`TerminalNo` VARCHAR(10) NOT NULL,
	`CashierName` VARCHAR(150) NOT NULL,
	`TransactionNo` VARCHAR(15) NOT NULL,
INDEX `IX_tblContactRewardsMovement`(`CustomerID`),
INDEX `IX_tblContactRewardsMovement1`(`RewardDate`),
INDEX `IX_tblContactRewardsMovement2`(`CustomerID`, `RewardDate`)
);


DROP TABLE IF EXISTS tblRewardItems;
CREATE TABLE tblRewardItems (
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 1 REFERENCES tblProducts(`ProductID`),
	`RewardPoints` DECIMAL (10,2) NOT NULL DEFAULT 0,
INDEX `IX_tblRewardItems`(`ProductID`)
);

DELETE FROM sysAccessRights WHERE TranTypeID = 140; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 140;
DELETE FROM sysAccessTypes WHERE TypeID = 140;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (140, 'Reward Points Setup');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 140, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 140, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 6, Category = '7: Backend - MasterFiles' WHERE TypeID = 140;

DELETE FROM sysAccessRights WHERE TranTypeID = 141; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 141;
DELETE FROM sysAccessTypes WHERE TypeID = 141;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (141, 'Reward Cards Issuance');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 141, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 141, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '7: Backend - MasterFiles' WHERE TypeID = 141;

DELETE FROM sysAccessRights WHERE TranTypeID = 142; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 142;
DELETE FROM sysAccessTypes WHERE TypeID = 142;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (142, 'Reward Cards Change');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 142, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 142, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '7: Backend - MasterFiles' WHERE TypeID = 142;

DELETE FROM sysAccessRights WHERE TranTypeID = 143; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 143; 
DELETE FROM sysAccessTypes WHERE TypeID = 143;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (143, 'Reward Points Redeemption');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 143, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 143, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '7: Backend - MasterFiles' WHERE TypeID = 143;

DELETE FROM sysAccessRights WHERE TranTypeID = 144; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 144;
DELETE FROM sysAccessTypes WHERE TypeID = 144;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (144, 'Reward Items Setup');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 144, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 144, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '7: Backend - MasterFiles' WHERE TypeID = 144;


-- For ClosingInventory
ALTER TABLE tblProductBaseVariationsMatrix ADD ActualQuantity DECIMAL(10,2) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD Deleted TINYINT (1) NOT NULL DEFAULT 0;

/*********************************  v_3.0.3.5.sql END  *******************************************************/ 

/*********************************  v_3.0.3.6.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.6';

ALTER TABLE tblInventory ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblStock ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblProductMovement ADD `BranchIDFrom` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblProductMovement ADD `BranchIDTo` INT(4) NOT NULL DEFAULT 1;

/*****************************
**	tblBranchInventory
*****************************/
DROP TABLE IF EXISTS tblBranchInventory;
CREATE TABLE tblBranchInventory (
`BranchID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT NOT NULL DEFAULT 0,
`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
`QuantityIn` DECIMAL(10,2) NOT NULL DEFAULT 0,
`QuantityOut` DECIMAL(10,2) NOT NULL DEFAULT 0,
`ActualQuantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
INDEX `IX_tblBranchInventory`(`BranchID`, `ProductID`),
UNIQUE `PK_tblBranchInventory`(`BranchID`, `ProductID`)
);

/*****************************
**	tblBranchInventoryMatrix
*****************************/
DROP TABLE IF EXISTS tblBranchInventoryMatrix;
CREATE TABLE tblBranchInventoryMatrix (
`BranchID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
`ProductID` BIGINT NOT NULL DEFAULT 0,
`MatrixID` BIGINT NOT NULL DEFAULT 0,
`Quantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
`QuantityIn` DECIMAL(10,2) NOT NULL DEFAULT 0,
`QuantityOut` DECIMAL(10,2) NOT NULL DEFAULT 0,
`ActualQuantity` DECIMAL(10,2) NOT NULL DEFAULT 0,
INDEX `IX_tblBranchInventoryMatrix`(`BranchID`, `ProductID`),
UNIQUE `PK_tblBranchInventoryMatrix`(`BranchID`, `ProductID`, `MatrixID`)
);

ALTER TABLE tblTerminal ADD RoundDownRewardPoints TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactRewards ADD TotalPurchases DECIMAL (10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblContactRewards ADD RedeemedPoints DECIMAL (10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblContactRewards ADD `RewardCardStatus` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactRewards ADD `ExpiryDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblContactRewards ADD `BirthDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00';


DROP TABLE IF EXISTS tblContactRewardsMovement;
CREATE TABLE tblContactRewardsMovement (
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 1 REFERENCES tblContacts(`ContactID`),
	`RewardDate` DATE NOT NULL DEFAULT '0001-01-01',
	`RewardPointsBefore` BIGINT NOT NULL DEFAULT 0,
	`RewardPointsAdjustment` BIGINT NOT NULL DEFAULT 0,
	`RewardPointsAfter` BIGINT NOT NULL DEFAULT 0,
	`RewardExpiryDate` DATE NOT NULL DEFAULT '0001-01-01',
	`RewardReason` VARCHAR(150) NOT NULL,
	`TerminalNo` VARCHAR(10) NOT NULL,
	`CashierName` VARCHAR(150) NOT NULL,
	`TransactionNo` VARCHAR(15) NOT NULL,
INDEX `IX_tblContactRewardsMovement`(`CustomerID`),
INDEX `IX_tblContactRewardsMovement1`(`RewardDate`),
INDEX `IX_tblContactRewardsMovement2`(`CustomerID`, `RewardDate`)
);


DROP TABLE IF EXISTS tblContactCreditCardInfo;
CREATE TABLE tblContactCreditCardInfo (
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
	`GuarantorID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
	`CreditType` TINYINT(1) NOT NULL DEFAULT 0,
	`CreditCardNo` VARCHAR(15) DEFAULT '',
	`CreditAwardDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`TotalPurchases` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CreditPaid` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CreditCardStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`ExpiryDate` DATE NOT NULL DEFAULT '0001-01-01',
INDEX `IX_tblContactCreditCardInfo`(`CustomerID`),
INDEX `IX_tblContactCreditCardInfo1`(`CreditCardNo`),
INDEX `IX_tblContactCreditCardInfo2`(`CustomerID`, `CreditCardNo`),
UNIQUE `PK_tblContactCreditCardInfo1`(`CustomerID`),
UNIQUE `PK_tblContactCreditCardInfo2`(`CreditCardNo`)
);

DROP TABLE IF EXISTS tblContactCreditCardMovement;
CREATE TABLE tblContactCreditCardMovement (
	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
	`GuarantorID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
	`CreditType` TINYINT(1) NOT NULL DEFAULT 0,
	`CreditDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CreditBefore` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`Credit` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CreditAfter` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CreditExpiryDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
	`CreditReason` VARCHAR(150) NOT NULL,
	`TerminalNo` VARCHAR(10) NOT NULL,
	`CashierName` VARCHAR(150) NOT NULL,
	`TransactionNo` VARCHAR(15) NOT NULL,
INDEX `IX_tblContactCreditCardMovement`(`CustomerID`),
INDEX `IX_tblContactCreditCardMovement1`(`CreditDate`),
INDEX `IX_tblContactCreditCardMovement2`(`CustomerID`, `CreditDate`)
);

DELETE FROM sysAccessRights WHERE TranTypeID = 145; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 145;
DELETE FROM sysAccessTypes WHERE TypeID = 145;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (145, 'Credit Card Issuance');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 145, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 145, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 8, Category = '11: Backend - MasterFiles' WHERE TypeID = 145;

DELETE FROM sysAccessRights WHERE TranTypeID = 146; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 146;
DELETE FROM sysAccessTypes WHERE TypeID = 146;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (146, 'Credit Card Replacement');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 146, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 146, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '11: Backend - MasterFiles' WHERE TypeID = 146;

ALTER TABLE tblERPConfig DROP LastCreditCardNo;
ALTER TABLE tblERPConfig DROP LastRewardCardNo;
ALTER TABLE tblERPConfig ADD LastCreditCardNo VARCHAR(11) NOT NULL DEFAULT '00000001';
ALTER TABLE tblERPConfig ADD LastRewardCardNo VARCHAR(11) NOT NULL DEFAULT '00000001';

ALTER TABLE tblTerminal ADD AutoGenerateRewardCardNo TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD EnableRewardPointsAsPayment TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD RewardPointsMaxPercentageForPayment DECIMAL(5,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsPaymentValue DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsPaymentCashEquivalent DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardsPermitNo VARCHAR(30);
ALTER TABLE tblTerminal ADD InHouseIndividualCreditPermitNo VARCHAR(30);
ALTER TABLE tblTerminal ADD InHouseGroupCreditPermitNo VARCHAR(30);

ALTER TABLE tblTransactions01 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions01 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReport ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD RewardPointsPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD RewardConvertedPayment DECIMAL(10,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;

/*********************************  v_3.0.3.6.sql END  *******************************************************/ 

/*********************************  v_3.0.3.7.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.7';

ALTER TABLE tblTerminal ADD `IsFineDining` INT(1) NOT NULL DEFAULT 0;

/*********************************  v_3.0.3.7.sql END  *******************************************************/ 


/*!40000 ALTER TABLE `tblProducts` DISABLE KEYS */;

DELETE FROM tblChargeType WHERE `ChargeTypeCode` = 'PC';
INSERT INTO tblChargeType (`ChargeTypeCode`, `ChargeType`, `ChargeAmount`, `InPercent`)
	VALUES ('PC', 'Personal Charge', 0, 1);

DELETE FROM tblCardTypes WHERE CardTypeCode = 'CITI';
DELETE FROM tblCardTypes WHERE CardTypeCode = 'HSBC';
DELETE FROM tblCardTypes WHERE CardTypeCode = 'AIG';
INSERT INTO tblCardTypes (CardTypeCode, CardTypeName) VALUES ('CITI', 'CITIBANK');
INSERT INTO tblCardTypes (CardTypeCode, CardTypeName) VALUES ('HSBC', 'HSBC');
INSERT INTO tblCardTypes (CardTypeCode, CardTypeName) VALUES ('AIG', 'AIG');


DELETE FROM tblProductGroup WHERE ProductGroupID = 1;
INSERT INTO tblProductGroup(ProductGroupID, ProductGroupCode, ProductGroupName, BaseUnitID) VALUES (1, 'DEFAULT','SYSTEM DEFAULT',1);

DELETE FROM tblProductSubGroup WHERE ProductSubGroupID = 1;  
INSERT INTO tblProductSubGroup(ProductSubGroupID, ProductGroupID, ProductSubGroupCode, ProductSubGroupName, BaseUnitID) VALUES (1, 1,'DEFAULT','DEFAULT',1); 

DELETE FROM tblProductPackage WHERE PackageID = 1;
DELETE FROM tblProducts WHERE ProductID = 1;  
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (1, 'CREDIT PAYMENT','CREDIT PAYMENT','CREDIT PAYMENT',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 1, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT';

DELETE FROM tblProductPackage WHERE PackageID = 2;
DELETE FROM tblProducts WHERE ProductID = 2;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (2, 'ADVNTGE CARD - MEMBERSHIP FEE','ADVNTGE CARD - MEMBERSHIP FEE','ADVNTGE CARD - MEMBERSHIP FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 2, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 3;
DELETE FROM tblProducts WHERE ProductID = 3;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (3, 'ADVNTGE CARD - RENEWAL FEE','ADVNTGE CARD - RENEWAL FEE','ADVNTGE CARD - RENEWAL FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 3, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 4;
DELETE FROM tblProducts WHERE ProductID = 4;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (4, 'ADVNTGE CARD - REPLACEMENT FEE','ADVNTGE CARD - REPLACEMENT FEE','ADVNTGE CARD - REPLACEMENT FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 4, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE';

DELETE FROM tblProductPackage WHERE PackageID = 5;
DELETE FROM tblProducts WHERE ProductID = 5;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (5, 'CREDIT CARD - MEMBERSHIP FEE','CREDIT CARD - MEMBERSHIP FEE','CREDIT CARD - MEMBERSHIP FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 5, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 6;
DELETE FROM tblProducts WHERE ProductID = 6;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (6, 'CREDIT CARD - RENEWAL FEE','CREDIT CARD - RENEWAL FEE','CREDIT CARD - RENEWAL FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 6, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 7;
DELETE FROM tblProducts WHERE ProductID = 7;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (7, 'CREDIT CARD - REPLACEMENT FEE','CREDIT CARD - REPLACEMENT FEE','CREDIT CARD - REPLACEMENT FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 7, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE';

DELETE FROM tblProductPackage WHERE PackageID = 8;
DELETE FROM tblProducts WHERE ProductID = 8;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (8, 'SUPER CARD - MEMBERSHIP FEE','SUPER CARD - MEMBERSHIP FEE','SUPER CARD - MEMBERSHIP FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 8, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 9;
DELETE FROM tblProducts WHERE ProductID = 9;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (9, 'SUPER CARD - RENEWAL FEE','SUPER CARD - RENEWAL FEE','SUPER CARD - RENEWAL FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 9, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 10;
DELETE FROM tblProducts WHERE ProductID = 10;
INSERT INTO tblProducts(ProductID, ProductCode, BarCode, ProductDesc, ProductSubGroupID, BaseUnitID, Price, PurchasePrice, SupplierID, Deleted, Quantity) VALUES (10, 'SUPER CARD - REPLACEMENT FEE','SUPER CARD - REPLACEMENT FEE','SUPER CARD - REPLACEMENT FEE',1,1,0,0,2,1,9999999999);
INSERT INTO tblProductPackage (PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 10, ProductID, BaseUnitID, Price, PurchasePrice, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE';

DELETE FROM tblContactRewards WHERE CustomerID = 1;
INSERT INTO tblContactRewards (CustomerID, RewardCardNo, RewardActive, RewardCardStatus, RewardAwardDate, ExpiryDate, BirthDate) 
							VALUES(1, '', 1, 0, NOW(), DATE_ADD(NOW(), INTERVAL 200 YEAR), NOW());

/*!40000 ALTER TABLE `tblProducts` ENABLE KEYS */;


/*****************************
**	Added on September 1, 2007 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (93, 'PurchasesAndPayablesMenu');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (94, 'Purchase Orders');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (95, 'GRN');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (96, 'Accounts Payable');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (97, 'PostingDates');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (98, 'PurchaseAnalysis');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (99, 'AccountSummary');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (100, 'AccountCategory');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (101, 'ChartOfAccounts');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (102, 'Payment Journals');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (103, 'General Ledger Menu');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (104, 'Sales & Receivable Menu');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (105, 'Purchase Returns');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (106, 'Purchase Debit Memo');

/*****************************
**	Added on October 9, 2007 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (107, 'Sales Orders');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (108, 'Sales Journals');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (109, 'Sales Returns');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (110, 'SalesCreditMemos');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (111, 'Sales And Receivables Menu');

/*****************************
**	Added on Feb 17, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (112, 'Transfer In');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (113, 'Transfer Out');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

/*****************************
**	Added on Feb 21, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (114, 'InvAdjustment');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (115, 'CloseInventory');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

/*****************************
**	Added on Feb 26, 2008 for Branch Inventory Count Sync
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (116, 'Export Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (117, 'Import Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (118, 'Synchronize Inventory Count');

/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 99, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 100, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 101, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 102, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 103, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 104, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 107, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 108, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 109, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 110, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 111, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Managers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 99, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 100, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 101, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 102, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 103, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 104, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 107, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 108, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 109, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 110, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 111, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 118, 1, 1);

/*****************************
**	Supervisors Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 87, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 99, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 100, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 101, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 102, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 103, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 104, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 107, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 108, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 109, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 110, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 111, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 118, 1, 1);


/*****************************
**	Cashiers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 99, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 100, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 101, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 102, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 103, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 104, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 107, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 108, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 109, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 110, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 111, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 115, 1, 1);



INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 99, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 100, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 101, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 102, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 103, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 104, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 107, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 108, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 109, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 110, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 111, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Added on July 28, 2008 for Bank - payment
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (119, 'Banks');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 119, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 119, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);

/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (120, 'PackUnpackTransaction');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 120, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);


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
-- ALTER TABLE tblPO ADD `UnpaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0;
--	update tblPO SET UnpaidAmount = POSubTotal;
	
-- ALTER TABLE tblPO ADD `PaidAmount` DECIMAL(18,2) NOT NULL DEFAULT 0;
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
);



/*********************************  v_1.0.0.1.sql END  *******************************************************/

/*********************************  v_1.0.0.2.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.2';

ALTER TABLE tblPODebitMemoItems ADD `PrevUnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0;


/*********************************  v_1.0.0.2.sql END  *******************************************************/

/*********************************  v_1.0.0.3.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.3';

ALTER TABLE tblPOItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.3.sql END  *******************************************************/
  
/*********************************  v_1.0.0.4.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.4';

ALTER TABLE tblMatrixPackagePriceHistory MODIFY COLUMN Remarks VARCHAR(150);

ALTER TABLE tblPOItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOutItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferInItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.4.sql END  *******************************************************/
   
SET FOREIGN_KEY_CHECKS = 1;

/*********************************  v_3.0.0.0.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_3.0.0.0';

ALTER TABLE tblPO ALTER `SupplierDRNo` SET DEFAULT '';
ALTER TABLE tblPODebitMemo ALTER `SupplierDocNo` SET DEFAULT '';
ALTER TABLE tblTransferIn ALTER `SupplierDRNo` SET DEFAULT '';
ALTER TABLE tblTransferOut ALTER `SupplierDRNo` SET DEFAULT '';


ALTER TABLE tblInventory ADD `ClosingActualQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_3.0.0.0.sql END  *******************************************************/


/*********************************  v_3.0.3.4.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersion = 'v_3.0.3.4';

ALTER TABLE tblPO ADD `RID` BIGINT NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `RID` BIGINT NOT NULL DEFAULT 0; 

/*********************************  v_3.0.3.4.sql END  *******************************************************/

/*********************************  v_3.0.3.5.sql SKIP  *******************************************************/
/*********************************  v_3.0.3.6.sql SKIP  *******************************************************/

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
	`RequestedBy` VARCHAR(150) NOT NULL DEFAULT '',
	`ReceivedBy` VARCHAR(150) NOT NULL DEFAULT '',
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
	PRIMARY KEY (BranchTransferID),
	INDEX `IX_tblBranchTransfer`(`BranchTransferID`),
	UNIQUE `PK_tblBranchTransfer`(`BranchTransferNo`),
	INDEX `IX2_tblBranchTransfer`(`BranchIDFrom`),
	FOREIGN KEY (`BranchIDFrom`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblBranchTransfer`(`BranchIDTo`),
	FOREIGN KEY (`BranchIDTo`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX4_tblBranchTransfer`(`TransferrerID`),
	FOREIGN KEY (`TransferrerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
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
	`BranchTransferItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
	`IsVatable` TINYINT(1) NOT NULL DEFAULT 1,
	`Remarks` VARCHAR(150),
	`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`BranchTransferItemID`),
	INDEX `IX_tblBranchTransferItems`(`BranchTransferItemID`),
	INDEX `IX0_tblBranchTransferItems`(`BranchTransferID`, `ProductID`),
	INDEX `IX1_tblBranchTransferItems`(`BranchTransferID`, `ProductID`,`VariationMatrixID`),
	INDEX `IX2_tblBranchTransferItems`(`ProductCode`),
	INDEX `IX3_tblBranchTransferItems`(`BranchTransferID`),
	INDEX `IX4_tblBranchTransferItems`(`ProductUnitID`)
);

ALTER TABLE tblERPConfig ADD `LastBranchTransferNo` VARCHAR(10) NOT NULL DEFAULT '0000000001';

/*********************************  v_3.0.3.7.sql END  *******************************************************/

/******************************************************************************
**		File: access.sql
**		Name: inventory access
**		Desc: inventory access
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
**	Added on September 1, 2007 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (93, 'PurchasesAndPayablesMenu');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (94, 'Purchase Orders');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (95, 'GRN');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (96, 'Accounts Payable');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (97, 'PostingDates');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (98, 'PurchaseAnalysis');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (105, 'Purchase Returns');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (106, 'Purchase Debit Memo');

UPDATE sysAccessTypes SET SequenceNo = 1, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 94;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 105;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 106;
/*****************************
**	Added on Feb 17, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (112, 'Transfer In');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (113, 'Transfer Out');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 6, Category = '08: Backend - Inventory' WHERE TypeID = 112;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '08: Backend - Inventory' WHERE TypeID = 113;

/*****************************
**	Added on Feb 21, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (114, 'InvAdjustment');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (115, 'CloseInventory');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 8, Category = '08: Backend - Inventory' WHERE TypeID = 114;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '08: Backend - Inventory' WHERE TypeID = 115;

/*****************************
**	Added on Feb 26, 2008 for Branch Inventory Count Sync
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (116, 'Export Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (117, 'Import Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (118, 'Synchronize Inventory Count');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 10, Category = '08: Backend - Inventory' WHERE TypeID = 116;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '08: Backend - Inventory' WHERE TypeID = 117;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '08: Backend - Inventory' WHERE TypeID = 118;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Managers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 118, 1, 1);

/*****************************
**	Supervisors Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 87, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 118, 1, 1);


/*****************************
**	Cashiers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 115, 1, 1);



INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Added on July 28, 2008 for Bank - payment
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (119, 'Banks');
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '08: Backend - Inventory' WHERE TypeID = 119;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);

/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (120, 'PackUnpackTransaction');

UPDATE sysAccessTypes SET SequenceNo = 14, Category = '08: Backend - Inventory' WHERE TypeID = 120;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 120, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);


/*****************************
**	Added on May 21, 2010 for Synchronizing Products
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (133, '');

UPDATE sysAccessTypes SET SequenceNo = 14, Category = '05: Backend - MasterFiles - Product' WHERE TypeID = 133;
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 133, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 133, 1, 1);

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
	FOREIGN KEY (`CustomerID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblSO`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSO`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
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
	FOREIGN KEY (`CustomerID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT,
	INDEX `IX2_tblSOCreditMemo`(`BranchID`),
	FOREIGN KEY (`BranchID`) REFERENCES tblBranch(`BranchID`) ON DELETE RESTRICT,
	INDEX `IX3_tblSOCreditMemo`(`SellerID`),
	FOREIGN KEY (`SellerID`) REFERENCES sysAccessUsers(`UID`) ON DELETE RESTRICT
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

ALTER TABLE tblInventory ADD `SCreditQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `SCreditCost` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblInventory ADD `SCreditVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_3.0.0.0.sql START  *******************************************************/

UPDATE tblERPConfig SET DBVersionSales = 'v_3.0.0.0';


ALTER TABLE tblSO ALTER `CustomerDRNo` SET DEFAULT '';
ALTER TABLE tblSOCreditMemo ALTER `CustomerDocNo` SET DEFAULT '';



/*********************************  v_3.0.0.0.sql END  *******************************************************/
 /*************************************************************************************************************

procSOSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where SOID = lngSOID;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decSODiscount, decSODiscountApplied, intSODiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procSOSynchronizeAmount;
delimiter GO

create procedure procSOSynchronizeAmount(IN lngSOID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intSODiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSODiscount, decSODiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblSOItems WHERE SOID = lngSOID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblSOItems WHERE SOID = lngSOID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblSOItems WHERE SOID = lngSOID AND Discount <> 0);
	
	SET decSODiscountApplied = (SELECT DiscountApplied FROM tblSO WHERE SOID = lngSOID);
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
	
	SET decSODiscountApplied = (SELECT DiscountApplied FROM tblSO WHERE SOID = lngSOID);
	SET intSODiscountType = (SELECT DiscountType FROM tblSO WHERE SOID = lngSOID);

	IF intSODiscountType = 1 and decTotalItemAmount >= decSODiscountApplied THEN
		SET decSODiscount = (SELECT decSODiscountApplied);
	ELSEIF intSODiscountType = 2 THEN
		SET decSODiscount = (SELECT (decSubTotalDiscountableAmount * (decSODiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decSODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decSODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblSO SET
		SubTotal = decTotalItemAmount - decSODiscount + Freight - Deposit,
		Discount = decSODiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decSODiscount + Freight - Deposit
	WHERE SOID = lngSOID;
	
END;
GO
delimiter ;

 /*************************************************************************************************************

procSOUpdateFreight
Lemuel E. Aceron

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procSOUpdateFreight;
delimiter GO

create procedure procSOUpdateFreight(IN lngSOID bigint(20), IN decFreight DECIMAL(10,2))
BEGIN
	
	UPDATE tblSO SET
		Freight	= decFreight 
	WHERE SOID	= lngSOID;
	
END;
GO
delimiter ;

/*************************************************************************************************************

procSOCreditMemoSynchronizeAmount
Lemuel E. Aceron

*************************************************************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSOCreditMemoSynchronizeAmount
GO

create procedure procSOCreditMemoSynchronizeAmount(IN lngCreditMemoID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intSOCreditMemoDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSOCreditMemoDiscount, decSOCreditMemoDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID AND Discount <> 0);
	
	SET decSOCreditMemoDiscountApplied = (SELECT DiscountApplied FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);
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
	
	SET decSOCreditMemoDiscountApplied = (SELECT DiscountApplied FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);
	SET intSOCreditMemoDiscountType = (SELECT DiscountType FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);

	IF intSOCreditMemoDiscountType = 1 and decTotalItemAmount >= decSOCreditMemoDiscountApplied THEN
		SET decSOCreditMemoDiscount = (SELECT decSOCreditMemoDiscountApplied);
	ELSEIF intSOCreditMemoDiscountType = 2 THEN
		SET decSOCreditMemoDiscount = (SELECT (decSubTotalDiscountableAmount * (decSOCreditMemoDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decSOCreditMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decSOCreditMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblSOCreditMemo SET
		SubTotal = decTotalItemAmount - decSOCreditMemoDiscount + Freight - Deposit,
		Discount = decSOCreditMemoDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decSOCreditMemoDiscount + Freight - Deposit
	WHERE CreditMemoID = lngCreditMemoID;
	
END;
GO
delimiter ;

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

/*******************************************************************

procTransferInSynchronizeAmount
Lemuel E. Aceron
April 29, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procTransferInSynchronizeAmount;
delimiter GO

create procedure procTransferInSynchronizeAmount(IN lngTransferInID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intTransferInDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decTransferInDiscount, decTransferInDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblTransferInItems WHERE TransferInID = lngTransferInID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblTransferInItems WHERE TransferInID = lngTransferInID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblTransferInItems WHERE TransferInID = lngTransferInID AND Discount <> 0);
	
	SET decTransferInDiscountApplied = (SELECT DiscountApplied FROM tblTransferIn WHERE TransferInID = lngTransferInID);
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
	
	SET decTransferInDiscountApplied = (SELECT DiscountApplied FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	SET intTransferInDiscountType = (SELECT DiscountType FROM tblTransferIn WHERE TransferInID = lngTransferInID);

	IF intTransferInDiscountType = 1 and decTotalItemAmount >= decTransferInDiscountApplied THEN
		SET decTransferInDiscount = (SELECT decTransferInDiscountApplied);
	ELSEIF intTransferInDiscountType = 2 THEN
		SET decTransferInDiscount = (SELECT (decSubTotalDiscountableAmount * (decTransferInDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decTransferInDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decTransferInDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblTransferIn SET
		SubTotal = decTotalItemAmount - decTransferInDiscount + Freight - Deposit,
		Discount = decTransferInDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decTransferInDiscount + Freight - Deposit
	WHERE TransferInID = lngTransferInID;
	
END;
GO
delimiter ;

/*******************************************************************

procTransferOutSynchronizeAmount
Lemuel E. Aceron
April 29, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procTransferOutSynchronizeAmount;
delimiter GO

create procedure procTransferOutSynchronizeAmount(IN lngTransferOutID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intTransferOutDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decTransferOutDiscount, decTransferOutDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblTransferOutItems WHERE TransferOutID = lngTransferOutID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblTransferOutItems WHERE TransferOutID = lngTransferOutID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblTransferOutItems WHERE TransferOutID = lngTransferOutID AND Discount <> 0);
	
	SET decTransferOutDiscountApplied = (SELECT DiscountApplied FROM tblTransferOut WHERE TransferOutID = lngTransferOutID);
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
	
	SET decTransferOutDiscountApplied = (SELECT DiscountApplied FROM tblTransferOut WHERE TransferOutID = lngTransferOutID);
	SET intTransferOutDiscountType = (SELECT DiscountType FROM tblTransferOut WHERE TransferOutID = lngTransferOutID);

	IF intTransferOutDiscountType = 1 and decTotalItemAmount >= decTransferOutDiscountApplied THEN
		SET decTransferOutDiscount = (SELECT decTransferOutDiscountApplied);
	ELSEIF intTransferOutDiscountType = 2 THEN
		SET decTransferOutDiscount = (SELECT (decSubTotalDiscountableAmount * (decTransferOutDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decTransferOutDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decTransferOutDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblTransferOut SET
		SubTotal = decTotalItemAmount - decTransferOutDiscount + Freight - Deposit,
		Discount = decTransferOutDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decTransferOutDiscount + Freight - Deposit
	WHERE TransferOutID = lngTransferOutID;
	
END;
GO
delimiter ;

/*******************************************************************

procInvAdjustmentSynchronizeAmount
Lemuel E. Aceron
June 28, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procInvAdjustmentSynchronizeAmount;
delimiter GO

create procedure procInvAdjustmentSynchronizeAmount(IN lngInvAdjustmentID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intInvAdjustmentDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decInvAdjustmentDiscount, decInvAdjustmentDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID AND Discount <> 0);
	
	SET decInvAdjustmentDiscountApplied = (SELECT DiscountApplied FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);
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
	
	SET decInvAdjustmentDiscountApplied = (SELECT DiscountApplied FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);
	SET intInvAdjustmentDiscountType = (SELECT DiscountType FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);

	IF intInvAdjustmentDiscountType = 1 and decTotalItemAmount >= decInvAdjustmentDiscountApplied THEN
		SET decInvAdjustmentDiscount = (SELECT decInvAdjustmentDiscountApplied);
	ELSEIF intInvAdjustmentDiscountType = 2 THEN
		SET decInvAdjustmentDiscount = (SELECT (decSubTotalDiscountableAmount * (decInvAdjustmentDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decInvAdjustmentDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decInvAdjustmentDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblInvAdjustment SET
		SubTotal = decTotalItemAmount - decInvAdjustmentDiscount + Freight - Deposit,
		Discount = decInvAdjustmentDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decInvAdjustmentDiscount + Freight - Deposit
	WHERE InvAdjustmentID = lngInvAdjustmentID;
	
END;
GO
delimiter ;

/*********************************
	procInvAdjustmentInsert
	Lemuel E. Aceron
	CALL procInvAdjustmentInsert();
	
	July 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procInvAdjustmentInsert
GO

create procedure procInvAdjustmentInsert(
	IN pvtUID BIGINT(20),
	IN pvtInvAdjustmentDate DATETIME,
	IN pvtProductID BIGINT(20),
	IN pvtProductCode VARCHAR(30),
	IN pvtDescription VARCHAR(100),
	IN pvtVariationMatrixID BIGINT(20),
	IN pvtMatrixDescription VARCHAR(150),
	IN pvtUnitID BIGINT(20),
	IN pvtUnitCode VARCHAR(30),
	IN pvtQuantityBefore DECIMAL(10,2), 
	IN pvtQuantityNow DECIMAL(10,2),
	IN pvtMinThresholdBefore DECIMAL(10,2), 
	IN pvtMinThresholdNow DECIMAL(10,2), 
	IN pvtMaxThresholdBefore DECIMAL(10,2), 
	IN pvtMaxThresholdNow DECIMAL(10,2),
	IN pvtRemarks VARCHAR(100))
BEGIN

	INSERT INTO tblInvAdjustment(UID, InvAdjustmentDate, ProductID, ProductCode, Description, 
							VariationMatrixID, MatrixDescription, UnitID, UnitCode, 
							QuantityBefore, QuantityNow, MinThresholdBefore, MinThresholdNow, MaxThresholdBefore, MaxThresholdNow, Remarks)VALUES
								(pvtUID, pvtInvAdjustmentDate, pvtProductID, pvtProductCode, pvtDescription,
							pvtVariationMatrixID, pvtMatrixDescription, pvtUnitID, pvtUnitCode, 
							pvtQuantityBefore, pvtQuantityNow, pvtMinThresholdBefore, pvtMinThresholdNow, pvtMaxThresholdBefore, pvtMaxThresholdNow, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procProductUpdateInvDetails
	Lemuel E. Aceron
	CALL procProductUpdateInvDetails();
	
	July 9, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateInvDetails
GO

create procedure procProductUpdateInvDetails(
	IN pvtProductID BIGINT(20),
	IN pvtQuantityNow DECIMAL(10,2), 
	IN pvtMinThresholdNow DECIMAL(10,2), 
	IN pvtMaxThresholdNow DECIMAL(10,2))
BEGIN

	UPDATE tblProducts SET
		Quantity	= pvtQuantityNow,
		MinThreshold= pvtMinThresholdNow,
		MaxThreshold= pvtmaxThresholdNow
	WHERE ProductID = pvtproductID;
	
END;
GO
delimiter ;

/*********************************
	procProductBaseVariationUpdateInvDetails
	Lemuel E. Aceron
	CALL procProductBaseVariationUpdateInvDetails
	
	July 9, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationUpdateInvDetails
GO

create procedure procProductBaseVariationUpdateInvDetails(
	IN pvtMatrixID BIGINT(20),
	IN pvtQuantityNow DECIMAL(10,2), 
	IN pvtMinThresholdNow DECIMAL(10,2), 
	IN pvtMaxThresholdNow DECIMAL(10,2))
BEGIN

	UPDATE tblProductBaseVariationsMatrix SET
		Quantity	= pvtQuantityNow,
		MinThreshold= pvtMinThresholdNow,
		MaxThreshold= pvtmaxThresholdNow
	WHERE MatrixID = pvtMatrixID;
	
END;
GO
delimiter ;


/*************************************************************************************************************

procBranchTransferSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decBranchTransferDiscount, decBranchTransferDiscountApplied, intBranchTransferDiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procBranchTransferSynchronizeAmount;
delimiter GO

create procedure procBranchTransferSynchronizeAmount(IN lngBranchTransferID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intBranchTransferDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decBranchTransferDiscount, decBranchTransferDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID AND Discount <> 0);
	
	SET decBranchTransferDiscountApplied = (SELECT DiscountApplied FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);
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
	
	SET decBranchTransferDiscountApplied = (SELECT DiscountApplied FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);
	SET intBranchTransferDiscountType = (SELECT DiscountType FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);

	IF intBranchTransferDiscountType = 1 and decTotalItemAmount >= decBranchTransferDiscountApplied THEN
		SET decBranchTransferDiscount = (SELECT decBranchTransferDiscountApplied);
	ELSEIF intBranchTransferDiscountType = 2 THEN
		SET decBranchTransferDiscount = (SELECT (decSubTotalDiscountableAmount * (decBranchTransferDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decBranchTransferDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decBranchTransferDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblBranchTransfer SET
		SubTotal = decTotalItemAmount - decBranchTransferDiscount + Freight - Deposit,
		Discount = decBranchTransferDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decBranchTransferDiscount + Freight - Deposit
	WHERE BranchTransferID = lngBranchTransferID;
	
END;
GO
delimiter ;

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

DROP TABLE IF EXISTS tblPaymentPODetails;
DROP TABLE IF EXISTS tblPaymentDebit;
DROP TABLE IF EXISTS tblPaymentCredit;
DROP TABLE IF EXISTS tblPayment;
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
);

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
);

-- select CONCAT('INSERT INTO tblAccountSummary(AccountSummaryCode, AccountSummaryName)VALUES(''',AccountSummaryCode,''',','''', AccountSummaryName,''')') from tblAccountSummary;
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
);
-- select CONCAT('INSERT INTO tblAccountCategory(AccountSummaryID, AccountCategoryCode, AccountCategoryName)VALUES(',AccountSummaryID,',''',AccountCategoryCode,''',','''', AccountCategoryName,''')') from tblAccountCategory;
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
);
-- select CONCAT('INSERT INTO tblChartOfAccount(AccountCategoryID, ChartOfAccountCode, ChartOfAccountName)VALUES(',AccountCategoryID,',''',ChartOfAccountCode,''',','''', ChartOfAccountName,''')') from tblChartOfAccount;
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
);

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
);

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
);

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
);

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
);




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
);


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
); 


/*********************************  v_1.0.0.1.sql START  *******************************************************/

/******************************
 * For AccountingSystem
 * Added: March 17,2010 Lemuel E. Aceron
 * ******************************/
ALTER TABLE tblERPConfig ADD DBVersionAccounts varchar(10);
UPDATE tblERPConfig SET DBVersionAccounts = 'v_1.0.0.1';

 /*****************************
**	tblBankDeposit
*****************************/
DROP TABLE IF EXISTS tblBankDeposit;
CREATE TABLE tblBankDeposit (
	`BankDepositID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TransactionDate` DATETIME NOT NULL,
	`DepositStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`DepositInAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`DepositMemo` VARCHAR(100) NULL,
	`DepositItemDate` DATETIME NOT NULL,
	`DepositItemType` TINYINT(1) NOT NULL DEFAULT 0,
	`DepositItemAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`DepositItemReference` VARCHAR(100) NULL,
	`DepositItemAmount` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CashBackAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`CashBackAmount` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`CashBackMemo` VARCHAR(100) NULL,
	`CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY (`BankDepositID`),
	INDEX `IX_tblBankDeposit`(`TransactionDate`, `DepositInAccountID`),
	INDEX `IX1_tblBankDeposit`(`DepositInAccountID`)
); 

/*********************************  v_1.0.0.1.sql END  *******************************************************/

  /*********************************  v_1.0.1.2.sql START  *****************************************************/

 /******************************
 * For AccountingSystem
 * Added: April 10,2010 Lemuel E. Aceron
 * ******************************/
UPDATE tblERPConfig SET DBVersionAccounts = 'v_1.0.0.2';

/*****************************
**	tblGJournal
*****************************/
DROP TABLE IF EXISTS tblGJournal;
CREATE TABLE tblGJournal (
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`TotalDebitAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`TotalCreditAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Particulars` VARCHAR(150) NOT NULL,
	`Status` SMALLINT NOT NULL DEFAULT 0,
	`PostingDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',	
	`CancelledDate` DATETIME NOT NULL DEFAULT '0001-01-01 00:00:00',		
	PRIMARY KEY (`GJournalID`),
	INDEX `IX_tblGJournal`(`GJournalID`)
);

/*****************************
**	tblGJournalDebit
*****************************/
DROP TABLE IF EXISTS tblGJournalDebit;
CREATE TABLE tblGJournalDebit (
	`GJournalDebitID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalDebitID`),
	INDEX `IX_tblGJournalDebit`(`GJournalDebitID`),
	UNIQUE `PK_tblGJournalDebit`(`GJournalDebitID`),
	INDEX `IX1_tblGJournalDebit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalDebit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);

/*****************************
**	tblGJournalCredit
*****************************/
DROP TABLE IF EXISTS tblGJournalCredit;
CREATE TABLE tblGJournalCredit (
	`GJournalCreditID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`GJournalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblGJournal(`GJournalID`),
	`ChartOfAccountID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblChartOfAccount(`ChartOfAccountID`),
	`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	PRIMARY KEY (`GJournalCreditID`),
	INDEX `IX_tblGJournalCredit`(`GJournalCreditID`),
	UNIQUE `PK_tblGJournalCredit`(`GJournalCreditID`),
	INDEX `IX1_tblGJournalCredit`(`GJournalID`),
	FOREIGN KEY (`GJournalID`) REFERENCES tblGJournal(`GJournalID`) ON DELETE RESTRICT,
	INDEX `IX2_tblGJournalCredit`(`ChartOfAccountID`),
	FOREIGN KEY (`ChartOfAccountID`) REFERENCES tblChartOfAccount(`ChartOfAccountID`) ON DELETE RESTRICT
);

/*********************************  v_1.0.1.2.sql END  *******************************************************/    





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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
														IN decRewardPointsPayment decimal(10,2),
														IN decRewardConvertedPayment decimal(10,2),
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
														IN intNoOfRewardPointsPayment int(10),
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
					RewardPointsPayment					=  RewardPointsPayment					+  decRewardPointsPayment, 
					RewardConvertedPayment				=  RewardConvertedPayment			    +  decRewardConvertedPayment, 
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
					NoOfRewardPointsPayment				=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment, 
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
														IN decRewardPointsPayment decimal(10,2),
														IN decRewardConvertedPayment decimal(10,2),
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
														IN intNoOfRewardPointsPayment int(10),
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
		RewardPointsPayment						=  RewardPointsPayment					+  decRewardPointsPayment, 
		RewardConvertedPayment					=  RewardConvertedPayment				+  decRewardConvertedPayment, 
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
		NoOfRewardPointsPayment					=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment,
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

	procSyncQuantityProductHistory
	Lemuel E. Aceron
    March 14, 2009

	CALL procSyncQuantityProductHistory();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistory
GO

create procedure procSyncQuantityProductHistory()
BEGIN
	DECLARE strSessionID varchar(30);
	set strSessionID = '1';
	
	DROP TABLE IF EXISTS tblProductHistoryAll;
	
	CREATE TABLE tblProductHistoryAll (
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
	);

	INSERT INTO tblProductHistoryAll
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
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID;
	
	SELECT 'Inserting StockItems';
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems01 a INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems02 a INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems03 a INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems04 a INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems05 a INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems06 a INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems07 a INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems08 a INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems09 a INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems10 a INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems11 a INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
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
	FROM  tblTransactionItems12 a INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID;
	
	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Purchase Order' AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		'Inventory Adjustment' AS Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer In' AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer Out' AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE b.Status = 1;
	
	UPDATE tblProductBaseVariationsMatrix SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.MatrixID = tblProductBaseVariationsMatrix.MatrixID);
		
	UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.ProductID = tblProducts.ProductID);

	SELECT N'DONE level1 Synching';
	
	
END;
GO
delimiter ;

/**************************************************************

	procSyncQuantityProductHistoryAdjust
	Lemuel E. Aceron
    July 20, 2009

	CALL procSyncQuantityProductHistoryAdjust();

	Description: make an automatic adjustments for products Not in the current Variations List;
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistoryAdjust
GO

create procedure procSyncQuantityProductHistoryAdjust()
BEGIN	
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decMatrixQuantity, decMinThreshold, decMaxThreshold DECIMAL(10,2);
	DECLARE strProductCode, strUnitCode VARCHAR(30);
	DECLARE strDescription VARCHAR(150);
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, Quantity, ProductCode, ProductDesc, a.BaseUnitID, UnitCode, MinThreshold, MaxThreshold FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID
									WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO lngProductID, decProductQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold;
		
		IF NOT done THEN
			SET decMatrixQuantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID);
			
			CALL procInvAdjustmentInsert (1, now(), lngProductID, strProductCode, strDescription, 0, 0, intUnitID, strUnitCode, 
											decProductQuantity, decMatrixQuantity, decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, 'System added during auto-sync.');
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SELECT 'DONE level2 Synching';
	
END;
GO
delimiter ;

/**************************************************************

	--UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE tblProductBaseVariationsMatrix.ProductID = tblProducts.ProductID);

	procGenerateProductHistory
	Lemuel E. Aceron
    2009-03-14 : created procedure
    2009-07-10 : included Purchase Orders
    2009-07-13 : included inventory adjustment in product history.
    2009-07-20 : included transferin in product history.
    2009-07-20 : included transferout in product history.
    
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
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems01 a INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems02 a INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems03 a INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems04 a INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems05 a INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems06 a INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems07 a INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems08 a INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems09 a INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems10 a INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems11 a INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
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
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems12 a INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
	
	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Purchase Order from ',SupplierCode) AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer In from ',SupplierCode) AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer out to ',SupplierCode) AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	

END;
GO
delimiter ;

/*********************************
	procTerminalVersionUpdate
	Lemuel E. Aceron
	
	April 22, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procTerminalVersionUpdate;
delimiter GO

create procedure procTerminalVersionUpdate(IN strTerminalNo varchar(10), IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET
		FEVersion = strVersion
	WHERE TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportBatchCounter
	Lemuel E. Aceron
	CALL procUpdateTerminalReportBatchCounter();
	
	April 19, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportBatchCounter
GO

create procedure procUpdateTerminalReportBatchCounter(IN pvtTerminalNo varchar(10), IN pvtDateLastInitialized DATETIME)
BEGIN
	
	UPDATE tblTerminalReportHistory SET BatchCounter = BatchCounter + 1 WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;
 
/*********************************
	procTransactionOrderTypeUpdate
	Lemuel E. Aceron
	
	May 1, 2009 - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionOrderTypeUpdate;
delimiter GO

create procedure procTransactionOrderTypeUpdate(IN lngTransactionID bigint(20), IN intMonth smallint, IN intOrderType smallint)
BEGIN

	IF intMonth = 1 THEN
		UPDATE tblTransactions01 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 2 THEN
		UPDATE tblTransactions02 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 3 THEN
		UPDATE tblTransactions03 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 4 THEN
		UPDATE tblTransactions04 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 5 THEN
		UPDATE tblTransactions05 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 6 THEN
		UPDATE tblTransactions06 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 7 THEN
		UPDATE tblTransactions07 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 8 THEN
		UPDATE tblTransactions08 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 9 THEN
		UPDATE tblTransactions09 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 10 THEN
		UPDATE tblTransactions10 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 11 THEN
		UPDATE tblTransactions11 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 12 THEN
		UPDATE tblTransactions12 SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	END IF;
	
END;
GO
delimiter ;

/*********************************
	procTransactionWaiterUpdate
	Lemuel E. Aceron
	May 1, 2009

	SET @SQL := CONCAT('UPDATE tblTransactions',strMonth,' SET WaiterID = ',lngWaiterID,', WaiterName = ''',strWaiterName,''' WHERE TransactionID = ',lngTransactionID,';');
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionWaiterUpdate;
delimiter GO

create procedure procTransactionWaiterUpdate(IN lngTransactionID bigint(20), IN intMonth smallint, IN lngWaiterID BIGINT(20), IN strWaiterName varchar(100))
BEGIN
	
	IF intMonth = 1 THEN
		UPDATE tblTransactions01 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 2 THEN
		UPDATE tblTransactions02 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 3 THEN
		UPDATE tblTransactions03 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 4 THEN
		UPDATE tblTransactions04 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 5 THEN
		UPDATE tblTransactions05 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 6 THEN
		UPDATE tblTransactions06 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 7 THEN
		UPDATE tblTransactions07 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 8 THEN
		UPDATE tblTransactions08 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 9 THEN
		UPDATE tblTransactions09 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 10 THEN
		UPDATE tblTransactions10 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 11 THEN
		UPDATE tblTransactions11 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 12 THEN
		UPDATE tblTransactions12 SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;
	END IF;

END;
GO
delimiter ;

/*********************************
	procTransactionContactUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionContactUpdate;
delimiter GO

create procedure procTransactionContactUpdate(IN lngTransactionID bigint(20), IN intMonth smallint, IN lngCustomerID BIGINT(20), IN strCustomerName varchar(100))
BEGIN
	
	IF intMonth = 1 THEN
		UPDATE tblTransactions01 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 2 THEN
		UPDATE tblTransactions02 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 3 THEN
		UPDATE tblTransactions03 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 4 THEN
		UPDATE tblTransactions04 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 5 THEN
		UPDATE tblTransactions05 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 6 THEN
		UPDATE tblTransactions06 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 7 THEN
		UPDATE tblTransactions07 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 8 THEN
		UPDATE tblTransactions08 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 9 THEN
		UPDATE tblTransactions09 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 10 THEN
		UPDATE tblTransactions10 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 11 THEN
		UPDATE tblTransactions11 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 12 THEN
		UPDATE tblTransactions12 SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;
	END IF;

END;
GO
delimiter ;

/*********************************
	procTransactionTerminalNoUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionTerminalNoUpdate;
delimiter GO

create procedure procTransactionTerminalNoUpdate(IN lngTransactionID bigint(20), IN intMonth smallint, IN strTerminalNo varchar(30))
BEGIN
	
	IF intMonth = 1 THEN
		UPDATE tblTransactions01 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 2 THEN
		UPDATE tblTransactions02 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 3 THEN
		UPDATE tblTransactions03 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 4 THEN
		UPDATE tblTransactions04 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 5 THEN
		UPDATE tblTransactions05 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 6 THEN
		UPDATE tblTransactions06 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 7 THEN
		UPDATE tblTransactions07 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 8 THEN
		UPDATE tblTransactions08 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 9 THEN
		UPDATE tblTransactions09 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 10 THEN
		UPDATE tblTransactions10 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 11 THEN
		UPDATE tblTransactions11 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 12 THEN
		UPDATE tblTransactions12 SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;
	END IF;

END;
GO
delimiter ;


/*********************************
	procGenerateDiscountByTerminalNo
	Lemuel E. Aceron
	May 5, 2009
	CALL procGenerateDiscountByTerminalNo('1', '01', '00000000000001, '00000000000006' );
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNo
GO

create procedure procGenerateDiscountByTerminalNo(
	IN strSessionID varchar(30),
	IN strTerminalNo varchar(30),
	IN strTransactionNoFrom varchar(30),
	IN strTransactionNoTo varchar(30)
	)
BEGIN
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions01 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions02 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions03 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions04 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions05 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions06 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions07 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions08 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions09 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions10 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions11 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions12 WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
END;
GO
delimiter ;

/*********************************
	procDeleteDiscountHistory
	Lemuel E. Aceron
	CALL procDeleteDiscountHistory('1');
	
	May 5, 2009 - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDeleteDiscountHistory
GO

create procedure procDeleteDiscountHistory(
	IN strSessionID varchar(30) 
	)
BEGIN
	
	DELETE FROM tblDiscountHistory WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/*********************************
	procGenerateDiscountByTerminalNoByCashier
	Lemuel E. Aceron
	CALL procGenerateDiscountByTerminalNoByCashier('1', '01', 1, '00000000000001, '00000000000006' );
	
	May 5, 2009 - create this procedure
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNoByCashier
GO

create procedure procGenerateDiscountByTerminalNoByCashier(
	IN strSessionID varchar(30),
	IN strTerminalNo varchar(30),
	IN lngCashierID bigint(20),
	IN strTransactionNoFrom varchar(30),
	IN strTransactionNoTo varchar(30)
	)
BEGIN
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions01 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions02 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions03 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions04 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions05 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions06 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions07 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions08 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions09 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions10 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions11 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions12 WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
END;
GO
delimiter ;


/*********************************
	procTerminalReportInitializeZRead
	Lemuel E. Aceron
	
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter
					
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax

*********************************/
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
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, TrustFund, 
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter) 
				(SELECT 
					TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, ZReadCount, 
					XReadCount, GrossSales, TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, VATableAmount, 
					NonVATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, decTrustFund,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter FROM tblTerminalReport WHERE TerminalNo = strTerminalNo);
					
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
					RewardPointsPayment					=  0,
					RewardConvertedPayment				=  0,
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
					DebitDeposit						=  0, 
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
					NoOfRewardPointsPayment				=  0,
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
					CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit, 
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax )
				(SELECT 
					CashierID, TerminalID, TerminalNo, GrossSales, 
					TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, VAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax FROM tblCashierReport WHERE TerminalNo = strTerminalNo);
					
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
			RewardPointsPayment					=  0,
			RewardConvertedPayment				=  0,
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
			DebitDeposit						=  0, 
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
			NoOfRewardPointsPayment				=  0,
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
	procTerminalUpdate
	Lemuel E. Aceron
	
	May 06, 2009 : - Created this procedure
	Oct 17, 2011 : - Added ShowCustomerSelection, RewardPointsMinimum,
					 RewardPointsEvery, RewardPoints
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdate
GO

create procedure procTerminalUpdate(
	IN lngTerminalID BIGINT(20), 
	IN bolIsPrinterAutoCutter TINYINT(1),
	IN bolAutoPrint TINYINT(1),
	IN bolIsVATInclusive TINYINT(1),
	IN strPrinterName VARCHAR(20),
	IN strTurretName VARCHAR(20),
	IN strCashDrawerName VARCHAR(20),
	IN bolItemVoidConfirmation TINYINT(1),
	IN bolEnableEVAT TINYINT(1),
	IN intMaxReceiptWidth INT(10),
	IN strFORM_Behavior VARCHAR(20),
	IN strMarqueeMessage VARCHAR(255),
	IN strMachineSerialNo VARCHAR(20),
	IN strAccreditationNo VARCHAR(20),
	IN decVAT DECIMAL(10,2),
	IN decEVAT DECIMAL(10,2),
	IN decLocalTax DECIMAL(10,2),
	IN bolShowItemMoreThanZeroQty TINYINT(1),
	IN bolShowOnlyPackedTransactions TINYINT(1),
	IN bolShowOneTerminalSuspendedTransactions TINYINT(1),
	IN intTerminalReceiptType TINYINT(1),
	IN strSalesInvoicePrinterName VARCHAR(30),
	IN bolCashCountBeforeReport TINYINT(1),
	IN bolPreviewTerminalReport TINYINT(1),
	IN bolIsPrinterDotMatrix TINYINT(1),
	IN bolIsChargeEditable TINYINT(1),
	IN bolIsDiscountEditable TINYINT(1),
	IN bolCheckCutOffTime TINYINT(1),
	IN strStartCutOffTime VARCHAR(5),
	IN strEndCutOffTime VARCHAR(5),
	IN bolWithRestaurantFeatures TINYINT(1),
	IN strSeniorCitizenDiscountCode varchar(5),
	IN bolIsTouchScreen TINYINT(1),
	IN bolWillContinueSelectionVariation TINYINT(1),
	IN bolWillContinueSelectionProduct TINYINT(1),
	IN bolWillPrintGrandTotal TINYINT(1),
	IN bolReservedAndCommit TINYINT(1),
	IN bolShowCustomerSelection TINYINT(1)
	)
BEGIN

	UPDATE tblTerminal SET 
				IsPrinterAutoCutter		= bolIsPrinterAutoCutter,
				AutoPrint				= bolAutoPrint,
				IsVATInclusive			= bolIsVATInclusive,
				PrinterName				= strPrinterName,
				TurretName				= strTurretName,
				CashDrawerName			= strCashDrawerName,
				ItemVoidConfirmation	= bolItemVoidConfirmation,
				EnableEVAT				= bolEnableEVAT,
				MaxReceiptWidth			= intMaxReceiptWidth,
				FORM_Behavior			= strFORM_Behavior,
				MarqueeMessage			= strMarqueeMessage,
				MachineSerialNo			= strMachineSerialNo,
				AccreditationNo			= strAccreditationNo,
				VAT						= decVAT,
				EVAT					= decEVAT,
				LocalTax				= decLocalTax,
				ShowItemMoreThanZeroQty = bolShowItemMoreThanZeroQty,
				ShowOnlyPackedTransactions = bolShowOnlyPackedTransactions,
				ShowOneTerminalSuspendedTransactions = bolShowOneTerminalSuspendedTransactions,
				TerminalReceiptType		= intTerminalReceiptType,
				SalesInvoicePrinterName = strSalesInvoicePrinterName,
				CashCountBeforeReport	= bolCashCountBeforeReport,
				PreviewTerminalReport	= bolPreviewTerminalReport,
				IsPrinterDotMatrix		= bolIsPrinterDotMatrix,
				IsChargeEditable		= bolIsChargeEditable,
				IsDiscountEditable		= bolIsDiscountEditable,
				CheckCutOffTime			= bolCheckCutOffTime,
				StartCutOffTime			= strStartCutOffTime,
				EndCutOffTime			= strEndCutOffTime,
				WithRestaurantFeatures	= bolWithRestaurantFeatures,
				SeniorCitizenDiscountCode = strSeniorCitizenDiscountCode,
				IsTouchScreen			= bolIsTouchScreen,
				WillContinueSelectionVariation	= bolWillContinueSelectionVariation,
				WillContinueSelectionProduct	= bolWillContinueSelectionProduct,
				WillPrintGrandTotal		= bolWillPrintGrandTotal,
				ReservedAndCommit		= bolReservedAndCommit,
				ShowCustomerSelection	= bolShowCustomerSelection
	WHERE TerminalID = lngTerminalID;
	
	
END;
GO
delimiter ;


/********************************************
	procTerminalUpdateRewardPointSystem
	Lemuel E. Aceron
	
	Nov 4, 2011 : - Created this procedure
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdateRewardPointSystem
GO

create procedure procTerminalUpdateRewardPointSystem(
	IN bolEnableRewardPoints TINYINT(1),
	IN bolRoundDownRewardPoints TINYINT(1),
	IN decRewardPointsMinimum DECIMAL(10,2),
	IN decRewardPointsEvery DECIMAL(10,2),
	IN decRewardPoints DECIMAL(10,2),
	IN bolEnableRewardPointsAsPayment DECIMAL(10,2),
	IN decRewardPointsMaxPercentageForPayment DECIMAL(5,2),
	IN decRewardPointsPaymentValue DECIMAL(10,2),
	IN decRewardPointsPaymentCashEquivalent DECIMAL(10,2)
	)
BEGIN

	UPDATE tblTerminal SET 
		EnableRewardPoints		= bolEnableRewardPoints,
		RoundDownRewardPoints	= bolRoundDownRewardPoints,
		RewardPointsMinimum		= decRewardPointsMinimum,
		RewardPointsEvery		= decRewardPointsEvery,
		RewardPoints			= decRewardPoints,
		EnableRewardPointsAsPayment			= bolEnableRewardPointsAsPayment,
		RewardPointsMaxPercentageForPayment	= decRewardPointsMaxPercentageForPayment,
		RewardPointsPaymentValue			= decRewardPointsPaymentValue,
		RewardPointsPaymentCashEquivalent	= decRewardPointsPaymentCashEquivalent;
		
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateSalesPerItemByGroup
	Lemuel E. Aceron
	CALL procGenerateSalesPerItemByGroup('1', null, null, null, null, null, null, null);
	
	May 15, 2009 - as requested by Malou of Baguio
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemByGroup
GO

create procedure procGenerateSalesPerItemByGroup(
	IN strSessionID varchar(30),
	IN strProductGroup varchar(20),
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
	SET strProductGroup = IF(NOT ISNULL(strProductGroup), CONCAT('%',strProductGroup,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
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
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);
	
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportMallForwarderFileName
	Lemuel E. Aceron
	CALL procUpdateTerminalReportMallForwarderFileName
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportMallForwarderFileName
GO

create procedure procUpdateTerminalReportMallForwarderFileName(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtMallFilename varchar(30)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		MallFilename = pvtMallFilename
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportIsMallFileUploadComplete
	Lemuel E. Aceron
	CALL procUpdateTerminalReportIsMallFileUploadComplete
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportIsMallFileUploadComplete
GO

create procedure procUpdateTerminalReportIsMallFileUploadComplete(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtIsMallFileUploadComplete TINYINT(1)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		IsMallFileUploadComplete = pvtIsMallFileUploadComplete
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procBEVersionUpdate
	Lemuel E. Aceron
	
	May 26, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procBEVersionUpdate;
delimiter GO

create procedure procBEVersionUpdate(IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET BEVersion = strVersion;
	
END;
GO
delimiter ;

/*********************************
	procProductPackageUpdate
	Lemuel E. Aceron
	CALL procProductPackageUpdate();
	
	June 3, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageUpdate
GO

create procedure procProductPackageUpdate(
	IN pvtPackageID BIGINT(20),
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT(10),
	IN pvtSellingPrice DECIMAL(10,2),
	IN pvtWSPrice DECIMAL(10,2),
	IN pvtPurchasePrice DECIMAL(10,2), 
	IN pvtQuantity DECIMAL(10,2), 
	IN pvtVAT DECIMAL(10,2), 
	IN pvtEVAT DECIMAL(10,2), 
	IN pvtLocalTax DECIMAL(10,2))
BEGIN
	UPDATE tblProductPackage SET
		ProductID		=	pvtProductID,
		UnitID			=	pvtUnitID,
		Price			=	pvtSellingPrice,
		WSPrice			=	pvtWSPrice,
		PurchasePrice	=	pvtPurchasePrice,
		Quantity		=	pvtQuantity,
		VAT				=	pvtVAT,
		EVAT			=	pvtEVAT,
		LocalTax		=	pvtLocalTax
	WHERE PackageID		=	pvtPackageID;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdate
	Lemuel E. Aceron
	CALL procMatrixPackageUpdate();
	
	June 4, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdate
GO

create procedure procMatrixPackageUpdate(
	IN pvtPackageID BIGINT(20),
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT(10),
	IN pvtSellingPrice DECIMAL(10,2),
	IN pvtWSPrice DECIMAL(10,2),
	IN pvtPurchasePrice DECIMAL(10,2), 
	IN pvtQuantity DECIMAL(10,2), 
	IN pvtVAT DECIMAL(10,2), 
	IN pvtEVAT DECIMAL(10,2), 
	IN pvtLocalTax DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		MatrixID		=	pvtMatrixID,
		UnitID			=	pvtUnitID,
		Price			=	pvtSellingPrice,
		WSPrice			=	pvtWSPrice,
		PurchasePrice	=	pvtPurchasePrice,
		Quantity		=	pvtQuantity,
		VAT				=	pvtVAT,
		EVAT			=	pvtEVAT,
		LocalTax		=	pvtLocalTax
	WHERE PackageID		=	pvtPackageID;
							
		
END;
GO
delimiter ;


/*********************************
	procProductPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procProductPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackagePriceHistoryInsert
GO

create procedure procProductPackagePriceHistoryInsert(
	IN pvtUID BIGINT(20),
	IN pvtPackageID BIGINT(20),
	IN pvtChangeDate DATETIME,
	IN pvtPurchasePriceNow DECIMAL(10,2), 
	IN pvtSellingPriceNow DECIMAL(10,2),
	IN pvtVATNow DECIMAL(10,2), 
	IN pvtEVATNow DECIMAL(10,2), 
	IN pvtLocalTaxNow DECIMAL(10,2),
	IN pvtRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblProductPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		VATBefore, VATNow, EVATBefore, EVATNow, LocalTaxBefore, LocalTaxNow, Remarks)
						   SELECT pvtUID, pvtPackageID, pvtChangeDate, 
		PurchasePrice, IF(pvtPurchasePriceNow=-1,PurchasePrice,pvtPurchasePriceNow), 
		Price, IF(pvtSellingPriceNow=-1,Price,pvtSellingPriceNow), 
		VAT, IF(pvtVATNow=-1,VAT,pvtVATNow), EVAT, IF(pvtEVATNow=-1,EVAT,pvtEVATNow), 
		LocalTax, IF(pvtLocalTaxNow=-1,LocalTax,pvtLocalTaxNow), pvtRemarks FROM tblProductPackage WHERE PackageID = pvtPackageID;
		
END;
GO
delimiter ;


/*********************************
	procMatrixPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procMatrixPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackagePriceHistoryInsert
GO

create procedure procMatrixPackagePriceHistoryInsert(
	IN pvtUID BIGINT(20),
	IN pvtPackageID BIGINT(20),
	IN pvtChangeDate DATETIME,
	IN pvtPurchasePriceNow DECIMAL(10,2), 
	IN pvtSellingPriceNow DECIMAL(10,2),
	IN pvtVATNow DECIMAL(10,2), 
	IN pvtEVATNow DECIMAL(10,2), 
	IN pvtLocalTaxNow DECIMAL(10,2),
	IN pvtRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblMatrixPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		VATBefore, VATNow, EVATBefore, EVATNow, LocalTaxBefore, LocalTaxNow, Remarks)
						   SELECT pvtUID, pvtPackageID, pvtChangeDate, 
		PurchasePrice, IF(pvtPurchasePriceNow=-1,PurchasePrice,pvtPurchasePriceNow), 
		Price, IF(pvtSellingPriceNow=-1,Price,pvtSellingPriceNow), 
		VAT, IF(pvtVATNow=-1,VAT,pvtVATNow), EVAT, IF(pvtEVATNow=-1,EVAT,pvtEVATNow), 
		LocalTax, IF(pvtLocalTaxNow=-1,LocalTax,pvtLocalTaxNow), pvtRemarks FROM tblMatrixPackage WHERE PackageID = pvtPackageID;
		
END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantity
	Lemuel E. Aceron
	CALL procZeroOutProductQuantity();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantity
GO

create procedure procZeroOutProductQuantity()
BEGIN

	UPDATE tblProducts SET Quantity = 0;

	UPDATE tblProductBasevariationsMatrix SET Quantity = 0;

END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantityAndDropVariations
	Lemuel E. Aceron
	CALL procZeroOutProductQuantityAndDropVariations();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantityAndDropVariations
GO

create procedure procZeroOutProductQuantityAndDropVariations()
BEGIN

	UPDATE tblProducts SET Quantity = 0;

	DROP TABLE IF EXISTS tblProductVariationsMatrix;

	/*****************************
	**	tblMatrixPackage
	*****************************/
	DROP TABLE IF EXISTS tblMatrixPackage;
	CREATE TABLE tblMatrixPackage (
	`PackageID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`MatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductBaseVariationsMatrix(`MatrixID`),
	`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
	`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblMatrixPackage`(`PackageID`,`MatrixID`),
	UNIQUE `PK_tblMatrixPackage`(`MatrixID`,`UnitID`,`Quantity`),
	INDEX `IX1_tblMatrixPackage`(`MatrixID`),
	INDEX `IX2_tblMatrixPackage`(`UnitID`),
	FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT
	);

	/*****************************
	**	tblProductBaseVariationsMatrix
	*****************************/
	DROP TABLE IF EXISTS tblProductBaseVariationsMatrix;
	CREATE TABLE tblProductBaseVariationsMatrix (
	`MatrixID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
	`Description` VARCHAR(255) NOT NULL,
	`UnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblUnit(`UnitID`),
	`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
	`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`ActualQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`MinThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`MaxThreshold` DECIMAL(18,2) NOT NULL DEFAULT 0,
	`SupplierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2 REFERENCES tblContacts(`ContactID`),
	`Deleted` TINYINT(1) NOT NULL DEFAULT 0,
	INDEX `IX_tblProductBaseVariationsMatrix`(`MatrixID`,`ProductID`),
	UNIQUE `PK_tblProductBaseVariationsMatrix`(`ProductID`,`Description`),
	INDEX `IX1_tblProductBaseVariationsMatrix`(`ProductID`),
	FOREIGN KEY (`ProductID`) REFERENCES tblProducts(`ProductID`) ON DELETE RESTRICT,
	INDEX `IX2_tblProductBaseVariationsMatrix`(`UnitID`),
	FOREIGN KEY (`UnitID`) REFERENCES tblUnit(`UnitID`) ON DELETE RESTRICT,
	INDEX `IX3_tblProductBaseVariationsMatrix`(`SupplierID`),
	FOREIGN KEY (`SupplierID`) REFERENCES tblContacts(`ContactID`) ON DELETE RESTRICT
	);

	/*****************************
	**	tblProductVariationsMatrix
	*****************************/
	DROP TABLE IF EXISTS tblProductVariationsMatrix;
	CREATE TABLE tblProductVariationsMatrix (
	`MatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductBaseVariationsMatrix(`MatrixID`),
	`VariationID` INT(10) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProductVariations(`VariationID`),
	`Description` VARCHAR(150) NOT NULL,
	INDEX `IX_tblProductVariationsMatrix`(`MatrixID`,`VariationID`),
	UNIQUE `PK_tblProductVariationsMatrix`(`MatrixID`, `VariationID`, `Description`),
	INDEX `IX1_tblProductVariationsMatrix`(`MatrixID`),
	FOREIGN KEY (`MatrixID`) REFERENCES tblProductBaseVariationsMatrix(`MatrixID`) ON DELETE RESTRICT,
	INDEX `IX2_tblProductVariationsMatrix`(`VariationID`),
	FOREIGN KEY (`VariationID`) REFERENCES tblProductVariations(`VariationID`) ON DELETE RESTRICT
	);
	
END;
GO
delimiter ;

/*********************************
	procProductVariationCountUpdate
	Lemuel E. Aceron
	CALL procProductVariationCountUpdate
	
	July 8 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductVariationCountUpdate
GO

create procedure procProductVariationCountUpdate(IN lngProductID BIGINT)
BEGIN
	
	UPDATE tblProducts SET VariationCount = (SELECT COUNT(MatrixID) FROM tblProductBaseVariationsMatrix z WHERE z.Deleted = 0 AND tblProducts.ProductID = z.ProductID) WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/*********************************
	procProductSynchronizeQuantity
	Lemuel E. Aceron
	CALL procProductSynchronizeQuantity(1);
	
	Oct 01, 2009 : Lemu
	Create this procedure
	
	Jul 26, 2011 : Lemu
	Added Insert to product movement history
	Added Insert to inventory adjustment
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSynchronizeQuantity
GO

create procedure procProductSynchronizeQuantity(
	IN lngProductID BIGINT
)
BEGIN
	DECLARE strTransactionNo VARCHAR(30) DEFAULT '';
	DECLARE lngMatrixVariationCount BIGINT DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(3) DEFAULT '';
	DECLARE decProductQuantity, decProductActualQuantity, decMinThreshold, decMaxThreshold DECIMAL(10,2) DEFAULT 0;
	DECLARE decMatrixTotalQuantity DECIMAL(10,2) DEFAULT 0;
	DECLARE strRemarks VARCHAR(100);
	DECLARE intBranchID INT(4) DEFAULT 1;
		
	-- STEP 1: check if there is an existing variation

	-- STEP 1.a: Set the value of lngMatrixVariationCount
	SELECT COUNT(MatrixID) INTO lngMatrixVariationCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID;

	IF (ISNULL(lngMatrixVariationCount)) THEN SET lngMatrixVariationCount = 0; END IF; 
	
	-- STEP 1.b: compare if there is a count
	IF (lngMatrixVariationCount <> 0) THEN 
		
		-- STEP 2: get the total Quantity of all Matrix
		SET decMatrixTotalQuantity = 0;
		SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; 
	
		-- STEP 3: Set the value of strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		SELECT ProductCode, ProductDesc, Quantity, ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
			
		-- STEP 4: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the product quantity based on total of quantities of all variations
		IF (decMatrixTotalQuantity <> decProductQuantity) THEN
			
			-- set the value of stRemarks, see the administrator for the list of constant remarks
			SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM SUM OF MATRIX QTY AS BASIS';
			
			-- STEP 4.a: Insert to product movement history
			CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, 0, '', 
											decProductQuantity, decMatrixTotalQuantity - decProductQuantity, decMatrixTotalQuantity, 0, 
											strUnitCode, strRemarks, now(), strTransactionNo, 'SYSTEM', intBranchID, intBranchID);
			
			-- STEP 4.b: Insert to inventory adjustment
			CALL procInvAdjustmentInsert(1, now(), lngProductID, strProductCode, strProductDesc, 0,
														'', intUnitID, strUnitCode, decProductQuantity, decMatrixTotalQuantity, 
														decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strTransactionNo));
			
			-- STEP 4.c: Do the actual adjustment
			UPDATE tblProducts a SET 
				Quantity			= (SELECT IFNULL(SUM(Quantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
			WHERE ProductID = lngProductID;
		
		END IF;
		
		-- STEP 5: Update the Actual Quantity
		UPDATE tblProducts a SET 
			ActualQuantity		= (SELECT IFNULL(SUM(ActualQuantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
		WHERE ProductID = lngProductID;
	
	END IF;
	
	
	
END;
GO
delimiter ;

/*********************************
	procProductBaseVariationsMatrixDelete
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDelete();
	
	Jul 02 2009 : Lemu
	- Create this procedure
	
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDelete
GO

create procedure procProductBaseVariationsMatrixDelete(
	IN pvtIDs varchar(100)
)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curProductIDs CURSOR FOR SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID IN (pvtIDs);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
		
	DELETE FROM tblMatrixPackage WHERE MatrixID IN (pvtIDs);

	DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (pvtIDs);
	
	OPEN curProductIDs;
	
	UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID IN (pvtIDs);
	
	REPEAT
		FETCH curProductIDs INTO lngProductID;
		
		IF NOT done THEN
			
			-- Synchronize the Product Quantity	
			CALL procProductSynchronizeQuantity(lngProductID);
			
			-- Update the variation count in table Product
			CALL procProductVariationCountUpdate(lngProductID);
			
		END IF;
		
	UNTIL done END REPEAT;
	CLOSE curProductIDs;
			
END;
GO
delimiter ;


/*********************************
	procProductBaseVariationsMatrixDeleteByID
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDeleteByID
	
	Jul 02, 2009 : Lemu
	- create this procedure
	
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDeleteByID
GO

create procedure procProductBaseVariationsMatrixDeleteByID(
	IN pvtID BIGINT
)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	
	SET lngProductID = (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = pvtID LIMIT 1);
	
	DELETE FROM tblMatrixPackage WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductVariationsMatrix WHERE MatrixID = pvtID;
	
	UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID = pvtID;
	
	-- Synchronize the Product Quantity	
	CALL procProductSynchronizeQuantity(lngProductID);
	
	-- Update the variation count in table Product
	CALL procProductVariationCountUpdate(lngProductID);
	
	/********
	** Jul 26, 2011 
	** Lemu E. Aceron
	** Only 1 ProductId is afftected and no need to use cursor.
	DECLARE curProductIDs CURSOR FOR SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = pvtID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
		
	DELETE FROM tblMatrixPackage WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductVariationsMatrix WHERE MatrixID = pvtID;
	
	OPEN curProductIDs;
	
	DELETE FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = pvtID;
	
	REPEAT
		FETCH curProductIDs INTO lngProductID;
		
		IF NOT done THEN
			CALL procProductVariationCountUpdate(lngProductID);
		END IF;
		
	UNTIL done END REPEAT;
	CLOSE curProductIDs;
			
	*****/
END;
GO
delimiter ;

/**************************************************************
	procGenerateProductPrices
	Lemuel E. Aceron
	CALL procGenerateProductPrices('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductPrices
GO

create procedure procGenerateProductPrices(
	IN strSessionID varchar(30),
	IN strProductGroupName varchar(30),
	IN strProductSubGroupName varchar(30)
	)
BEGIN

	/*****************************
	**	tblProductPrices
	*****************************/
	DROP TABLE IF EXISTS tblProductPrices;
	CREATE TABLE tblProductPrices (
		`SessionID` VARCHAR(30) NOT NULL,
		`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductCode` VARCHAR(30) NOT NULL,
		`ProductDescription` VARCHAR(30) NOT NULL,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductGroupName` VARCHAR(30) NOT NULL,
		`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductSubGroupName` VARCHAR(30) NOT NULL,
		`Quantity` DECIMAL(10,2),
		`UnitCode` VARCHAR(10) NOT NULL,
		`UnitName` VARCHAR(30) NOT NULL,
		`PurchasePrice` DECIMAL(10,2),
		`Price` DECIMAL(10,2),
		`VAT` DECIMAL(10,2),
		`EVAT` DECIMAL(10,2),
		`LocalTax` DECIMAL(10,2),
	INDEX `IX_tblProductPrices`(`SessionID`),
	INDEX `IX1_tblProductPrices`(`ProductID`),
	INDEX `IX2_tblProductPrices`(`ProductGroupID`),
	INDEX `IX3_tblProductPrices`(`ProductSubGroupID`)
	);

	/*** Select the package prices for matrix ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		d.ProductCode, 
		d.ProductDesc AS ProductDescription, 
		b.Description AS MatrixDescription,
		f.ProductGroupID, 
		f.ProductGroupName,
		e.ProductSubGroupID,
		e.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblMatrixPackage a 
		INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID 
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProducts d ON b.ProductID = d. ProductID
		INNER JOIN tblProductSubGroup e ON d.ProductSubGroupID = e.ProductSubGroupID
		INNER JOIN tblProductGroup f ON e.ProductGroupID = f.ProductGroupID
	WHERE d.Deleted = 0;
	
	/*** Select the packages for products without variations ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		b.ProductCode, 
		b.ProductDesc AS ProductDescription, 
		null AS MatrixDescription,
		e.ProductGroupID, 
		e.ProductGroupName,
		d.ProductSubGroupID,
		d.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblProductPackage a 
		INNER JOIN tblProducts b ON a.ProductID = b.ProductID
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID
		INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID
	WHERE b.ProductID NOT IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0);
	
END;
GO
delimiter ;

/**************************************************************
	procProductPackageCopyToMatrixPackage
	Lemuel E. Aceron
	CALL procProductPackageCopyToMatrixPackage(2885);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageCopyToMatrixPackage
GO

create procedure procProductPackageCopyToMatrixPackage(IN pvtProductID BIGINT)
BEGIN
	
	-- update all matrix packages that have the same unit and quantity in productpackage
	UPDATE tblMatrixPackage, tblProductPackage SET
		tblMatrixPackage.Price = tblProductPackage.Price,
		tblMatrixPackage.WSPrice = tblProductPackage.WSPrice,
		tblMatrixPackage.PurchasePrice = tblProductPackage.PurchasePrice,
		tblMatrixPackage.VAT = tblProductPackage.VAT,
		tblMatrixPackage.EVAT = tblProductPackage.EVAT,
		tblMatrixPackage.LocalTax = tblProductPackage.LocalTax
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix
							WHERE tblProductBaseVariationsMatrix.Deleted = 0 AND tblProductBaseVariationsMatrix.ProductID = pvtProductID)
		AND tblMatrixPackage.Quantity = tblProductPackage.Quantity
		AND tblMatrixPackage.UnitID = tblProductPackage.UnitID
		AND tblProductPackage.ProductID = pvtProductID;
	
	-- insert all the product packages in matrix packages that are not in matrix packages
	INSERT INTO tblMatrixPackage (
		MatrixID,
		UnitID,
		Price, 
		WSPrice, 
		PurchasePrice, 
		Quantity, 
		VAT,
		EVAT, 
		LocalTax)
	SELECT 
		b.MatrixID,
		a.UnitID,
		a.Price, 
		a.WSPrice, 
		a.PurchasePrice, 
		a.Quantity, 
		a.VAT,
		a.EVAT, 
		a.LocalTax
	FROM tblProductPackage a
	LEFT JOIN tblProductBaseVariationsMatrix b ON a.ProductID = b.ProductID
	WHERE b.Deleted = 0 AND b.ProductID = pvtProductID 
		AND NOT EXISTS (SELECT UnitID, Quantity FROM tblMatrixPackage 
							WHERE MatrixID IN (select matrixid from tblProductBaseVariationsMatrix where tblProductBaseVariationsMatrix.Deleted = 0 
												AND ProductID = pvtProductID)
							AND tblMatrixPackage.UnitID = a.UnitID AND tblMatrixPackage.Quantity = a.Quantity);
	
	-- update all tblProductBaseVariationsMatrix pricing
	UPDATE tblProductBaseVariationsMatrix, tblProducts SET
		tblProductBaseVariationsMatrix.UnitID = tblProducts.BaseUnitID,
		tblProductBaseVariationsMatrix.Price = tblProducts.Price,
		tblProductBaseVariationsMatrix.WSPrice = tblProducts.WSPrice,
		tblProductBaseVariationsMatrix.PurchasePrice = tblProducts.PurchasePrice,
		tblProductBaseVariationsMatrix.IncludeInSubtotalDiscount = tblProducts.IncludeInSubtotalDiscount,
		tblProductBaseVariationsMatrix.VAT = tblProducts.VAT,
		tblProductBaseVariationsMatrix.EVAT = tblProducts.EVAT,
		tblProductBaseVariationsMatrix.LocalTax = tblProducts.LocalTax,
		tblProductBaseVariationsMatrix.SupplierID = tblProducts.SupplierID
	WHERE tblProductBaseVariationsMatrix.ProductID = tblProducts.ProductID
		AND tblProducts.ProductID = pvtProductID;
	
END;
GO
delimiter ;

/**************************************************************
	AccessuserSynchronizeAccessRights
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRights(1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRights
GO

create procedure AccessuserSynchronizeAccessRights(IN pvtUID BIGINT)
BEGIN
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = (SELECT GroupID FROM sysAccessUserDetails WHERE UID = pvtUID);
	
END;
GO
delimiter ;


/**************************************************************
	AccessuserSynchronizeAccessRightsFromGroup
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRightsFromGroup(1,1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRightsFromGroup
GO

create procedure AccessuserSynchronizeAccessRightsFromGroup(IN pvtUID BIGINT, IN pvtGroupID INT)
BEGIN
	
	DELETE FROM sysAccessRights WHERE UID = pvtUID;
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = pvtGroupID;
	
END;
GO
delimiter ;


/*********************************
	procCashPaymentInsert
	Lemuel E. Aceron
	CALL procCashPaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCashPaymentInsert
GO

create procedure procCashPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2), 
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblCashPayment(TransactionID, TransactionNo, Amount, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtRemarks);
		
END;
GO
delimiter ;


/*********************************
	procChequePaymentInsert
	Lemuel E. Aceron
	CALL procChequePaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procChequePaymentInsert
GO

create procedure procChequePaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtChequeNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2), 
	IN pvtValidityDate DATETIME,
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblChequePayment(TransactionID, TransactionNo, ChequeNo, Amount, ValidityDate, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtChequeNo, pvtAmount, pvtValidityDate, pvtRemarks);
		
END;
GO
delimiter ;


/*********************************
	procCreditCardPaymentInsert
	Lemuel E. Aceron
	CALL procCreditCardPaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditCardPaymentInsert
GO

create procedure procCreditCardPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2), 
	IN pvtCardTypeID INT,
	IN pvtCardTypeCode VARCHAR(30),
	IN pvtCardTypeName VARCHAR(30),
	IN pvtCardNo VARCHAR(30),
	in pvtCardHolder VARCHAR(150),
	IN pvtValidityDates VARCHAR(14),
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblCreditCardPayment(TransactionID, TransactionNo, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtCardTypeID, pvtCardTypeCode, pvtCardTypeName, pvtCardNo, pvtCardHolder, pvtValidityDates, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procCreditPaymentInsert
	Lemuel E. Aceron
	CALL procCreditPaymentInsert();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentInsert
GO

create procedure procCreditPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2),
	IN pvtContactID BIGINT,
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblCreditPayment(TransactionID, TransactionNo, Amount, ContactID, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtContactID, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procDebitPaymentInsert
	Lemuel E. Aceron
	CALL procDebitPaymentInsert();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentInsert
GO

create procedure procDebitPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2),
	IN pvtContactID BIGINT,
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblDebitPayment(TransactionID, TransactionNo, Amount, ContactID, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtContactID, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procContactAddCredit
	Lemuel E. Aceron
	CALL procContactAddCredit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddCredit
GO

create procedure procContactAddCredit(
	IN pvtContactID BIGINT(20),
	IN pvtCredit DECIMAL(10,2))
BEGIN

	UPDATE tblContacts SET Credit =	Credit + pvtCredit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactAddDebit
	Lemuel E. Aceron
	CALL procContactAddDebit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddDebit
GO

create procedure procContactAddDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(10,2))
BEGIN

	UPDATE tblContacts SET Debit =	Debit + pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactSubtractCredit
	Lemuel E. Aceron
	CALL procContactSubtractCredit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractCredit
GO

create procedure procContactSubtractCredit(
	IN pvtContactID BIGINT(20),
	IN pvtCredit DECIMAL(10,2))
BEGIN

	UPDATE tblContacts SET Credit =	Credit - pvtCredit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactSubtractDebit
	Lemuel E. Aceron
	CALL procContactSubtractDebit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractDebit
GO

create procedure procContactSubtractDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(10,2))
BEGIN

	UPDATE tblContacts SET Debit =	Debit - pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;


/*********************************
	procCreditPaymentUpdateCredit
	Lemuel E. Aceron
	CALL procCreditPaymentUpdateCredit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentUpdateCredit
GO

create procedure procCreditPaymentUpdateCredit(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2),
	IN pvtRemarks VARCHAR(255))
BEGIN

	UPDATE tblCreditPayment SET 
		AmountPaid = AmountPaid + pvtAmount,
		Remarks = CONCAT(Remarks,';',pvtRemarks)
	WHERE TransactionID = pvtTransactionID 
		AND TransactionNo = pvtTransactionNo;
		
END;
GO
delimiter ;

/*********************************
	procDebitPaymentUpdateDebit
	Lemuel E. Aceron
	CALL procDebitPaymentUpdateDebit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentUpdateDebit
GO

create procedure procDebitPaymentUpdateDebit(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(10,2),
	IN pvtRemarks VARCHAR(255))
BEGIN

	UPDATE tblDebitPayment SET 
		AmountPaid = AmountPaid + pvtAmount,
		Remarks = CONCAT(Remarks,';',pvtRemarks)
	WHERE TransactionID = pvtTransactionID 
		AND TransactionNo = pvtTransactionNo;
		
END;
GO
delimiter ;

/*********************************
	procCreditPaymentSyncTransactionNo
	Lemuel E. Aceron
	CALL procCreditPaymentSyncTransactionNo();
	
	September 02, 2009 - create this procedure
	Update Credit Payment TransactionNo with the Correct TransactionNo
	THIS IS ONLY APPLICABLE IF DB Version is lower than v.2.0.0.8
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentSyncTransactionNo
GO

create procedure procCreditPaymentSyncTransactionNo()
BEGIN

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions01) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions02) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions03) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions04) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions05) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions06) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions07) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions08) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions09) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions10) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions11) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions12) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;
		
END;
GO
delimiter ;

/**************************************************************
	procGenerateSalesPerItemWithZeroSales
	Lemuel E. Aceron
	Sep 07, 2009
	CALL procGenerateSalesPerItemWithZeroSales('1', null, null, null, null, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemWithZeroSales
GO

create procedure procGenerateSalesPerItemWithZeroSales(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN

	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	CALL procGenerateSalesPerItem(strSessionID, strTransactionNo, strCustomerName, strCashierName, strTerminalNo, dteStartTransactionDate, dteEndTransactionDate);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID,
		ProductGroupCode,
		a.ProductCode 'ProductCode',
		UnitCode 'ProductUnitCode',
		0 'Quantity', 0 Amount, 
		0 PurchaseAmount
	FROM tblProducts a 
		INNER JOIN tblProductSubGroup b ON b.ProductSubGroupID = a.ProductSubGroupID
		INNER JOIN tblProductGroup c ON c.ProductGroupID = b.ProductGroupID
		INNER JOIN tblUnit d ON d.UnitID = a.BaseUnitID
	WHERE ProductCode NOT IN (SELECT ProductCode FROM tblSalesPerItem WHERE ProductCode NOT LIKE '%-%');
	
	-- exclude the ProductCode with '-' coz it's sure that's is with sales [ProductCode NOT LIKE '%-%']
	
END;
GO
delimiter ;


/*********************************
	procBaseVariationEasyInsert
	Lemuel E. Aceron
	CALL procBaseVariationEasyInsert(1,'test');
	
	Oct 1, 2009 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- Added inventory per branch
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procBaseVariationEasyInsert
GO

create procedure procBaseVariationEasyInsert(
	IN lngProductID BIGINT,
	IN strDescription VARCHAR(30)
)
BEGIN
	DECLARE lngMatrixID, lngVariationID, lngExpirationVariationID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch WHERE BranchID <> 1; 
	
	/*********************************Check The Expiration Variation ********************************/
	SET lngExpirationVariationID = 1;
	SET lngVariationID = (SELECT VariationID FROM tblProductVariations WHERE ProductID = lngProductID AND VariationID = 1 LIMIT 1);
	SET lngVariationID = IF(NOT ISNULL(lngVariationID), lngVariationID, 0);
	IF lngVariationID = 0 THEN
		INSERT INTO tblProductVariations(ProductID, VariationID) VALUES(lngProductID, lngExpirationVariationID);
	END IF;
	
	/*********************************Check The Matrix Variation ********************************/
	INSERT INTO tblProductBaseVariationsMatrix (ProductID, Description, UnitID, Price, PurchasePrice, 
								IncludeInSubtotalDiscount, VAT, EVAT, LocalTax, 
								Quantity, MinThreshold, MaxThreshold  
							)SELECT ProductID, strDescription, BaseUnitID, Price, PurchasePrice, 
								IncludeInSubtotalDiscount, VAT, EVAT, LocalTax, 
								0, MinThreshold, MaxThreshold 
							FROM tblProducts WHERE ProductID = lngProductID;
	
	SET lngMatrixID = (SELECT LAST_INSERT_ID());
	
	INSERT INTO tblProductVariationsMatrix (MatrixID, VariationID, Description) VALUES (lngMatrixID, 1, strDescription);
	
	INSERT INTO tblMatrixPackage (MatrixID, UnitID, Price, PurchasePrice, 
								Quantity, VAT, EVAT, LocalTax
							) SELECT lngMatrixID, UnitID, Price, PurchasePrice, 
								Quantity, VAT, EVAT, LocalTax
							FROM tblProductPackage WHERE ProductID = lngProductID;
	
	-- Put the count of variation in Product
	CALL procProductVariationCountUpdate(lngProductID);
	
	-- create inventory item per branch
	CALL procProductBranchInventoryMatrixInsert(lngProductID, lngMatrixID);
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdatePurchasing
	Lemuel E. Aceron
	CALL procMatrixPackageUpdatePurchasing();
	
	October 25, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdatePurchasing
GO

create procedure procMatrixPackageUpdatePurchasing(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(10,2),
	IN pvtPurchasePrice DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		PurchasePrice	=	pvtPurchasePrice
	WHERE MatrixID		=	pvtMatrixID
	    AND UnitID		=	pvtUnitID
	    AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procProductTagActiveInactive
	Lemuel E. Aceron
	CALL procProductTagActiveInactive
	
	October 28 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductTagActiveInactive
GO

create procedure procProductTagActiveInactive(IN lngProductID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblProducts SET Active = intStatus WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/*********************************
	procSyncContactCredit
	Lemuel E. Aceron
	CALL procSyncContactCredit();
	
	December 18, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncContactCredit
GO

create procedure procSyncContactCredit()
BEGIN
	
	UPDATE tblContacts SET Credit = (SELECT SUM(Amount - AmountPaid) FROM tblCreditPayment WHERE tblCreditPayment.ContactID = tblContacts.ContactID AND Amount > AmountPaid);

END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSelling
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSelling();
	
	Jan 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSelling
GO

create procedure procMatrixPackageUpdateSelling(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(10,2),
	IN pvtPrice DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID		=	pvtMatrixID
	    AND UnitID		=	pvtUnitID
	    AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingWSPrice();
	
	Jan 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingWSPrice
GO

create procedure procMatrixPackageUpdateSellingWSPrice(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(10,2),
	IN pvtWSPrice DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID		=	pvtMatrixID
	    AND UnitID		=	pvtUnitID
	    AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductID
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductID();
	
	Jan 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductID
GO

create procedure procMatrixPackageUpdateSellingUsingProductID(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(10,2),
	IN pvtPrice DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
	    AND UnitID		=	pvtUnitID
	    AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductIDWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductIDWSPrice();
	
	Jan 13, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductIDWSPrice
GO

create procedure procMatrixPackageUpdateSellingUsingProductIDWSPrice(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(10,2),
	IN pvtPrice DECIMAL(10,2))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
	    AND UnitID		=	pvtUnitID
	    AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procTransactionAgentUpdate
	Lemuel E. Aceron
	Feb 26, 2010
	
	Oct 8, 2010 - Added AgentPositionName and AgentDepartmentName
*********************************/
DROP PROCEDURE IF EXISTS procTransactionAgentUpdate;
delimiter GO

create procedure procTransactionAgentUpdate(IN lngTransactionID bigint(20), IN intMonth smallint, IN lngAgentID BIGINT(20), IN strAgentName varchar(100), IN strAgentPositionName varchar(30), IN strAgentDepartmentName varchar(30))
BEGIN
	
	IF intMonth = 1 THEN
		UPDATE tblTransactions01 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 2 THEN
		UPDATE tblTransactions02 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 3 THEN
		UPDATE tblTransactions03 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 4 THEN
		UPDATE tblTransactions04 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 5 THEN
		UPDATE tblTransactions05 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 6 THEN
		UPDATE tblTransactions06 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 7 THEN
		UPDATE tblTransactions07 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 8 THEN
		UPDATE tblTransactions08 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 9 THEN
		UPDATE tblTransactions09 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 10 THEN
		UPDATE tblTransactions10 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 11 THEN
		UPDATE tblTransactions11 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	ELSEIF intMonth = 12 THEN
		UPDATE tblTransactions12 SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;
	END IF;

END;
GO
delimiter ;


/*********************************
	procProductCommisionUpdate
	Lemuel E. Aceron
	CALL procProductCommisionUpdate
	
	March 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCommisionUpdate
GO

create procedure procProductCommisionUpdate(IN lngProductID BIGINT, IN decPercentageCommision DECIMAL(3,2))
BEGIN
	
	UPDATE tblProducts SET PercentageCommision = decPercentageCommision WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/**************************************************************
	procGenerateAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAgentsCommision('1', 1, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAgentsCommision
GO

create procedure procGenerateAgentsCommision(
	IN strSessionID varchar(30),
	IN lngAgentID BIGINT(2),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems01 a 
	INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems02 a 
	INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems03 a 
	INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems04 a 
	INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems05 a 
	INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems06 a 
	INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems07 a 
	INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems08 a 
	INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems09 a 
	INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems10 a 
	INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems11 a 
	INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems12 a 
	INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateAllAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAllAgentsCommision('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAllAgentsCommision
GO

create procedure procGenerateAllAgentsCommision(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems01 a 
	INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems02 a 
	INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems03 a 
	INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems04 a 
	INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems05 a 
	INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems06 a 
	INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems07 a 
	INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems08 a 
	INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems09 a 
	INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems10 a 
	INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems11 a 
	INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems12 a 
	INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;
	
END;
GO
delimiter ;

/*********************************
	procStockTagActiveInactive
	Lemuel E. Aceron
	CALL procStockTagActiveInactive
	
	March 10,2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procStockTagActiveInactive
GO

create procedure procStockTagActiveInactive(IN lngStockID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblStock SET Active = intStatus WHERE StockID = lngStockID;

END;
GO
delimiter ;

/*********************************
	procCheckTerminalLastDateInitialized
	Lemuel E. Aceron
	CALL procCheckTerminalLastDateInitialized
	
	This can be use to get the last initialization of zread 
	and previous initialization of zread.
	
	March 10,2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCheckTerminalLastDateInitialized
GO

create procedure procCheckTerminalLastDateInitialized()
BEGIN
	
	SELECT DateLastInitialized 'DateLastInitialized' FROM tblTerminalReport;
	
	SELECT DateLastInitialized 'PreviousDateLastInitialized' FROM tblTerminalReportHistory ORDER BY DateLastInitialized DESC LIMIT 1;

END;
GO
delimiter ;

/*********************************
	procFixItemsPurchaseAmount
	Lemuel E. Aceron
	CALL procFixItemsPurchaseAmount();
	
	This can be use to fix the item purchase amount if purchase amount is not consistent 
	with purchaseprice * quantity.
	
	April 8,2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procFixItemsPurchaseAmount
GO

create procedure procFixItemsPurchaseAmount()
BEGIN
	
	UPDATE tblTransactionItems01 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems02 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems03 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems04 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems05 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems06 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems07 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems08 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems09 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems10 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems11 SET PurchaseAmount = Purchaseprice * Quantity;
	UPDATE tblTransactionItems12 SET PurchaseAmount = Purchaseprice * Quantity;

	UPDATE tblTransactionItems01 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems02 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems03 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems04 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems05 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems06 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems07 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems08 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems09 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems10 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems11 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	UPDATE tblTransactionItems12 SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	
END;
GO
delimiter ;


/*********************************
	procPositionInsert
	Lemuel E. Aceron
	CALL procPositionInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionInsert
GO

create procedure procPositionInsert(
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	INSERT INTO tblPositions(PositionCode, PositionName)
	VALUES (pvtPositionCode, pvtPositionName);
		
END;
GO
delimiter ;


/*********************************
	procPositionUpdate
	Lemuel E. Aceron
	CALL procPositionUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionUpdate
GO

create procedure procPositionUpdate(
	IN pvtPositionID INT(10),
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	UPDATE tblPositions SET 
		PositionCode	= pvtPositionCode, 
		PositionName	= pvtPositionName
	WHERE PositionID	= pvtPositionID;
		
END;
GO
delimiter ;

/*********************************
	procDepartmentInsert
	Lemuel E. Aceron
	CALL procDepartmentInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentInsert
GO

create procedure procDepartmentInsert(
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	INSERT INTO tblDepartments(DepartmentCode, DepartmentName)
	VALUES (pvtDepartmentCode, pvtDepartmentName);
		
END;
GO
delimiter ;


/*********************************
	procDepartmentUpdate
	Lemuel E. Aceron
	CALL procDepartmentUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentUpdate
GO

create procedure procDepartmentUpdate(
	IN pvtDepartmentID INT(10),
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	UPDATE tblDepartments SET 
		DepartmentCode	= pvtDepartmentCode, 
		DepartmentName	= pvtDepartmentName
	WHERE DepartmentID	= pvtDepartmentID;
		
END;
GO
delimiter ;

/*********************************
	procTransactionRelease
	Lemuel E. Aceron
	May 3, 2011
	For releasing of closed transaction.
*********************************/

DROP PROCEDURE IF EXISTS procTransactionRelease;
delimiter GO

create procedure procTransactionRelease(IN lngTransactionID BIGINT(20), 
										IN intMonth SMALLINT(2) UNSIGNED ZEROFILL, 
										IN intTransactionStatus SMALLINT,
										IN lngReleasedByID BIGINT(20),
										IN strReleasedByName VARCHAR(100))
BEGIN
	SET @strSQL = CONCAT('UPDATE tblTransactions', intMonth,' SET ');
	SET @strSQL = CONCAT(@strSQL,'	TransactionStatus=', intTransactionStatus,', ');
	SET @strSQL = CONCAT(@strSQL,'	ReleaserID=', lngReleasedByID,', ');
	SET @strSQL = CONCAT(@strSQL,'	ReleaserName=''', strReleasedByName,''', ');
	SET @strSQL = CONCAT(@strSQL,'	ReleasedDate=NOW() ');
	SET @strSQL = CONCAT(@strSQL,'WHERE TransactionID=',lngTransactionID,'; ');
		
	PREPARE strCmd FROM @strSQL;
	EXECUTE strCmd;
	DEALLOCATE PREPARE strCmd;
	
	
END;
GO
delimiter ;

/*********************************
call procTransactionRelease(1,1,9,1,'Administrator');
*********************************/

/**************************************************************

	procProductUpdateActualQuantity
	Lemuel E. Aceron
    March 14, 2009

	CALL procProductUpdateActualQuantity();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateActualQuantity
GO

create procedure procProductUpdateActualQuantity(
						IN intBranchID INT(4),
						IN lngProductID bigint,
						IN decQuantity numeric)
BEGIN
	IF (lngProductID = 0) THEN
		UPDATE tblBranchInventory SET ActualQuantity = decQuantity AND BranchID = intBranchID;
	ELSE
		UPDATE tblBranchInventory SET ActualQuantity = decQuantity WHERE ProductID = lngProductID AND BranchID = intBranchID;
	END IF;
	
	IF (lngProductID = 0) THEN
		UPDATE tblProducts SET ActualQuantity = (SELECT SUM(ActualQuantity) FROM tblBranchInventory z WHERE tblProducts.ProductID = z.ProductID);
	ELSE
		UPDATE tblProducts SET ActualQuantity = (SELECT SUM(ActualQuantity) FROM tblBranchInventory z WHERE tblProducts.ProductID = z.ProductID) WHERE ProductID = lngProductID;
	END IF;
END;
GO
delimiter ;

/**************************************************************

	procCloseInventory
	Lemuel E. Aceron
    March 14, 2009

	CALL procCloseInventory(1, '2011-07-26', '00001', 1, 'RetailPlus', true);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCloseInventory
GO

create procedure procCloseInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngContactID bigint,
									IN strContactCode varchar(150),
									IN bolUseVariationAsReference TINYINT(1))
BEGIN
	
	DECLARE lngProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(10,2) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(10,2) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(100) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(3) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, Quantity, ActualQuantity, ProductCode, ProductDesc, a.BaseUnitID, UnitCode, a.MinThreshold, a.MaxThreshold, PurchasePrice FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID
									WHERE Quantity <> ActualQuantity ; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE Quantity <> ActualQuantity;
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice)  
									SELECT intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
										strReferenceNo, lngContactID, strContactCode, 
										ProductID, ProductCode, 0, '',
										Quantity, ActualQuantity, 
										PurchasePrice * ActualQuantity * 0.12, 
										PurchasePrice * ActualQuantity, PurchasePrice
									FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
										WHERE a.Deleted = 0 AND Active = 1 AND Quantity = ActualQuantity;
									
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: get the last Matrix to be udpated
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		
		SELECT MatrixID, Description INTO lngMatrixID, strMatrixDescription FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID ORDER BY MatrixID DESC LIMIT 1;
		
		IF (ISNULL(lngMatrixID)) THEN SET lngMatrixID = 0; END IF; 
		IF (ISNULL(strMatrixDescription)) THEN SET strMatrixDescription = ''; END IF;
		
		-- STEP 2: get the total Quantity of all Matrix
		SET decMatrixTotalQuantity = 0;
		IF (lngMatrixID <> 0) THEN SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; END IF;
		
		-- STEP 3: Overwrite the PurchasePrice from tblProductBaseVariationsMatrix if with Matrix
		IF (lngMatrixID <> 0) THEN SELECT AVG(PurchasePrice) INTO decPurchasePrice FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; END IF;
		
		-- For testing: SELECT lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 4: Insert to tblInventory
		INSERT INTO tblInventory (intBranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngContactID, strContactCode, 
									lngProductID, strProductCode, 0, '',
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
		
		-- STEP 6: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the last Matrix (using matrixid) quantity of the correct quantity
		IF (bolUseVariationAsReference = 0) THEN
			IF (decMatrixTotalQuantity <> decProductActualQuantity) THEN
				UPDATE tblProductBaseVariationsMatrix SET Quantity = decProductActualQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID ;
			END IF;
		ELSE
			UPDATE tblProductBaseVariationsMatrix SET Quantity = ActualQuantity WHERE ProductID = lngProductID;
		END IF;
		
		-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
		SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM INVENTORY CLOSING-ACTUAL PRODUCT QTY AS BASIS';
			
		-- STEP 8: Insert to product movement history
		CALL procProductMovementInsert(lngProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID);
		
		-- STEP 9: Insert to inventory adjustment
		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, lngProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 10: auto adjust the quantity based on actual quantity
		UPDATE tblProducts SET Quantity = decProductActualQuantity WHERE ProductID = lngProductID;
		
		SET lngProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
	UPDATE tblProducts SET QuantityIN = 0;
	UPDATE tblProducts SET QuantityOUT = 0;
	
	UPDATE tblProductBaseVariationsMatrix SET QuantityIN = 0;
	UPDATE tblProductBaseVariationsMatrix SET QuantityOUT = 0;
	
	
	IF (bolUseVariationAsReference = 0) THEN
		CALL procSyncProductVariationFromQuantityAllItem();
	END IF;
END;
GO
delimiter ;


/**************************************************************

	procSyncProductVariationFromQuantity
	Lemuel E. Aceron
    March 14, 2009

	CALL procSyncProductVariationFromQuantityPerItem(9, 1);

	Jul 26, 2011 : Lemu
	- Added Insert to product movement history
	- Added Insert to inventory adjustment
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItem
GO

create procedure procSyncProductVariationFromQuantityPerItem(IN lngProductID BIGINT, IN intBranchID INT)
BEGIN
	DECLARE strTransactionNo VARCHAR(30) DEFAULT '';
	DECLARE lngMatrixID BIGINT DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(3) DEFAULT '';
	DECLARE decProductQuantity, decProductActualQuantity, decMinThreshold, decMaxThreshold DECIMAL(10,2) DEFAULT 0;
	DECLARE decMatrixQuantity, decMatrixTotalQuantity DECIMAL(10,2) DEFAULT 0;
	DECLARE strRemarks VARCHAR(100) DEFAULT '';
	
	-- STEP 1: get the last Matrix to be udpated
	SET lngMatrixID = 0; SET strMatrixDescription = '';
	
	-- Set the value of strMatrixDescription, decMatrixQuantity
	/********** update main product **********/
	SELECT MatrixID, Description, Quantity INTO lngMatrixID, strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID ORDER BY MatrixID DESC LIMIT 1;
	/********** end update main product **********/
	
	SELECT a.MatrixID, Description, b.Quantity INTO lngMatrixID, strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID WHERE a.Deleted = 0 AND a.ProductID = lngProductID AND BranchID = intBranchID ORDER BY MatrixID DESC LIMIT 1;
	
	IF (ISNULL(lngMatrixID)) THEN SET lngMatrixID = 0; END IF; 
	IF (ISNULL(strMatrixDescription)) THEN SET strMatrixDescription = ''; END IF;
	
	SET decMatrixTotalQuantity = 0;
	IF (lngMatrixID <> 0) THEN
			
		/********** update main product **********/
		-- STEP 2.a: get the total Quantity of all Matrix
		SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; 
		
		-- STEP 2.b: get the Quantity of product
		--			 Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
		SELECT ProductCode, ProductDesc, Quantity, ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
		
		/********** end update main product **********/
		
		-- STEP 2.a: get the total Quantity of all Matrix
		SELECT SUM(b.Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID AND a.ProductID = lngProductID AND BranchID = intBranchID; 
		
		-- STEP 2.b: get the Quantity of product
		--			 Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
		SELECT ProductCode, ProductDesc, c.Quantity, c.ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN tblBranchInventory c ON a.ProductID = c.ProductID WHERE a.ProductID = lngProductID AND BranchID = intBranchID;
		
		
		-- STEP 3: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the last Matrix (using matrixid) quantity of the correct quantity
		IF (decMatrixTotalQuantity <> decProductQuantity) THEN
			
			-- STEP 3.a: set values for tblProductMovement history
			-- Set the value of strTransactionNo
			SET strTransactionNo = (SELECT CONCAT('SAADJ-', EndingTransactionNo) AS strTransactionNo FROM tblTerminalReport LIMIT 1);
			
			-- set the value of stRemarks, see the administrator for the list of constant remarks
			SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF MATRIX QTY FROM PRODUCT QTY AS BASIS';
				
			-- STEP 3.b: Insert to product movement history
			CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
											decProductQuantity, 0, decProductQuantity, decProductQuantity - decMatrixTotalQuantity + decMatrixQuantity, 
											strUnitCode, strRemarks, now(), strTransactionNo, 'SYSTEM', intBranchID, intBranchID);
			
			-- STEP 3.c: Insert to inventory adjustment
			CALL procInvAdjustmentInsert(1, now(), lngProductID, strProductCode, strProductDesc, lngMatrixID,
														strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductQuantity, 
														decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strTransactionNo));
														
			-- STEP 3.c: Do the actual adjustment
			/********** update main product **********/
			UPDATE tblProductBaseVariationsMatrix SET Quantity = decProductQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID ;
			/********** end update main product **********/
			
			UPDATE tblBranchInventoryMatrix SET Quantity = decProductQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID AND BranchID = intBranchID;

		END IF;
	END IF;
END;
GO
delimiter ; 

/**************************************************************

	procSyncProductVariationFromQuantityAllItem
	Lemuel E. Aceron
    March 14, 2009

	CALL procSyncProductVariationFromQuantityAllItem(1);

	Mar 14, 2011 : Lemu
	- create this procedure
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityAllItem
GO

create procedure procSyncProductVariationFromQuantityAllItem(IN intBranchID INT(4))
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts a
									WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID) ; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID) ; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the actual sync of product
		CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;


/********************************************
	procProductMovementInsert
	
	CALL procProductMovementInsert(9, 'test', 'test desc', 0, 'test matrix desc', 100, 30, 130, 100, 'PC', 'remarks', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel', 1, 1);
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include BranchIDFrom, BranchIDTo to include inventory per branch.
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementInsert
GO

create procedure procProductMovementInsert(
	IN lngProductID BIGINT,
	IN strProductCode VARCHAR(30),
	IN strProductDesc VARCHAR(50),
	IN lngMatrixID BIGINT,
	IN strMatrixDescription VARCHAR(100),
	IN decQuantityFrom DECIMAL(18,2),
	IN decQuantity DECIMAL(18,2),
	IN decQuantityTo DECIMAL(18,2),
	IN decMatrixQuantity DECIMAL(18,2),
	IN strUnitCode VARCHAR(3),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100),
	IN intBranchIDFrom INT(4),
	IN intBranchIDTo INT(4)
	)
BEGIN

	INSERT INTO tblProductMovement (ProductID,
									ProductCode,
									ProductDescription,
									MatrixID,
									MatrixDescription,
									QuantityFrom,
									Quantity,
									QuantityTo,
									MatrixQuantity,
									UnitCode,
									Remarks,
									TransactionDate,
									TransactionNo,
									CreatedBy,
									BranchIDFrom,
									BranchIDTo)
							VALUES( lngProductID,
									strProductCode,
									strProductDesc,
									lngMatrixID,
									strMatrixDescription,
									decQuantityFrom,
									decQuantity,									
									decQuantityTo,
									decMatrixQuantity,
									strUnitCode,
									strRemarks,
									dteTransactionDate,
									strTransactionNo,
									strCreatedBy,
									intBranchIDFrom,
									intBranchIDTo);
									
END;
GO
delimiter ;

/********************************************
	procProductAddQuantity
	
	CALL procProductAddQuantity(2, 2715, 2581, 10, 'purchase', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductAddQuantity
GO

create procedure procProductAddQuantity(
	IN intBranchID INT(4),
	IN lngProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(3) DEFAULT '';
	DECLARE decProductQuantity, decMatrixQuantity DECIMAL(10,2) DEFAULT 0;
	
	/*********** add to main ***********/	
	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT Description, Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = lngMatrixID AND ProductID = lngProductID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, Quantity, UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID);
										
	-- Add the quantity to Product table
	UPDATE tblProducts SET 
		Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity
	WHERE ProductID = lngProductID;
	
	-- Add the quantity to Matrix table
	UPDATE tblProductBaseVariationsMatrix SET 
		Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity 
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = lngProductID;
	/*********** end add to main ***********/	

	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT a.Description, b.Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID WHERE Deleted = 0 AND a.MatrixID = lngMatrixID AND a.ProductID = lngProductID AND BranchID = intBranchID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT a.ProductCode, a.ProductDesc, c.Quantity, b.UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN tblBranchInventory c ON a.ProductID = c.ProductID WHERE a.ProductID = lngProductID AND BranchID = intBranchID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID);
									
	-- Add the quantity to BranchInventory table
	IF EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = lngProductID AND BranchID = intBranchID) THEN
		UPDATE tblBranchInventory SET 
			Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity
		WHERE ProductID = lngProductID 
			AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblBranchInventory (BranchID , ProductID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, decQuantity, decQuantity);
	END IF;
	
	-- Add the quantity to Matrix table
	IF EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID ) THEN
		UPDATE tblBranchInventoryMatrix SET 
			Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity 
		WHERE MatrixID	= lngMatrixID  
			AND ProductID = lngProductID 
			AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblBranchInventoryMatrix (BranchID , ProductID, MatrixID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, lngMatrixID, decQuantity, decQuantity);
	END IF;
	
	-- Tag product as Active if quantity > 0
	IF (SELECT Quantity FROM tblProducts WHERE ProductID = lngProductID) > 0 THEN
		CALL procProductTagActiveInactive(lngProductID, 1);
	END IF;
	
	-- Process sync of product that are sold without matrix but with existing matrix now
	CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
END;
GO
delimiter ;


/********************************************
	procProductSubtractQuantity
	
	CALL procProductSubtractQuantity(2, 2715, 484, 3, 'SALES', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSubtractQuantity
GO

create procedure procProductSubtractQuantity(
	IN intBranchID INT(4),
	IN lngProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(3) DEFAULT '';
	DECLARE decProductQuantity, decMatrixQuantity DECIMAL(10,2) DEFAULT 0;
	
	/*********** subtract from main ***********/
	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT Description, Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = lngMatrixID AND ProductID = lngProductID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, Quantity, UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, -1 * decQuantity, decProductQuantity - decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID);
	
	-- Subtract the quantity from Product table
	UPDATE tblProducts SET 
		Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity, ActualQuantity = ActualQuantity - decQuantity
	WHERE ProductID = lngProductID;
	
	-- Subtract the quantity from Matrix table
	UPDATE tblProductBaseVariationsMatrix SET 
		Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity, ActualQuantity = ActualQuantity - decQuantity
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = lngProductID;
		
	/*********** end subtract from main ***********/

	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT a.Description, b.Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID WHERE Deleted = 0 AND a.MatrixID = lngMatrixID AND a.ProductID = lngProductID AND BranchID = intBranchID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT a.ProductCode, a.ProductDesc, c.Quantity, b.UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN tblBranchInventory c ON a.ProductID = c.ProductID WHERE a.ProductID = lngProductID AND BranchID = intBranchID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, -1 * decQuantity, decProductQuantity - decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID);
	
	-- Subtract the quantity from Product table
	-- Add the quantity to BranchInventory table
	IF EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = lngProductID AND BranchID = intBranchID) THEN
		UPDATE tblBranchInventory SET 
			Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity, ActualQuantity = ActualQuantity - decQuantity
		WHERE ProductID = lngProductID
			AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblBranchInventory (BranchID , ProductID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, -decQuantity, -decQuantity);
	END IF;
	
	-- Subtract the quantity from Matrix table
	IF EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID ) THEN
		UPDATE tblBranchInventoryMatrix SET 
			Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity, ActualQuantity = ActualQuantity - decQuantity
		WHERE MatrixID	= lngMatrixID 
			AND ProductID = lngProductID
			AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblBranchInventoryMatrix (BranchID , ProductID, MatrixID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, lngMatrixID, -decQuantity, -decQuantity);
	END IF;
		
	
	-- Tag product as InActive if quantity <= 0
	IF (SELECT Quantity FROM tblProducts WHERE ProductID = lngProductID) = 0 THEN
		CALL procProductTagActiveInactive(lngProductID, 0);
	END IF;

	-- Process sync of product that are returned without matrix but with existing matrix now
	CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
									
END;
GO
delimiter ;


/**************************************************************

	procProductMovementSelect

    Jul 26, 2011 : Lemu
    - create this procedure

	CALL procProductMovementSelect(3924, '', '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementSelect
GO

create procedure procProductMovementSelect(
									IN lngProductID BIGINT, 
									IN dteStartTransactionDate DATETIME,
									in dteEndTransactionDate DATETIME)
BEGIN
	SET @strSQL := '';
	
	SET @strSQL := 'SELECT
						ProductID,
						ProductCode, 
						ProductDescription,
						MatrixID,
						MatrixDescription, 
						QuantityFrom,
						Quantity,
						QuantityTo,
						matrixQuantity,
						UnitCode,
						Remarks,
						TransactionDate,
						TransactionNo
					FROM tblProductMovement
					WHERE 1=1 ';
	
	IF (lngProductID <> 0) THEN
		SET @strSQL = CONCAT(@strSQL,'AND ProductID = ', lngProductID,' ');
	END IF;
	
	IF (dteStartTransactionDate <> '') THEN
		SET @strSQL = CONCAT(@strSQL,'AND TransactionDate >= ''', dteStartTransactionDate,''' ');
	END IF;
	
	IF (dteEndTransactionDate <> '') THEN
		SET @strSQL = CONCAT(@strSQL,'AND TransactionDate <= ''', dteEndTransactionDate,''' ');
	END IF;
	
	PREPARE stmt FROM @strSQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;


/**************************************************************

	procProductUpdateRIDByPO

    Aug 26, 2011 : Lemu
    - create this procedure

	CALL procProductUpdateRIDByPO(10);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRIDByPO
GO

create procedure procProductUpdateRIDByPO(IN lngPOID BIGINT)
BEGIN
	DECLARE lngProductID, lngRID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, RID FROM tblPOItems WHERE POID = lngPOID; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblPOItems WHERE POID = lngPOID; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID, lngRID to be processed 
		FETCH curItems INTO lngProductID, lngRID;
		
		-- Process the actual update of product RID
		CALL procProductUpdateRID(lngProductID, lngRID);
		
		-- reset the ProductID, lngRID to be processed
		SET lngProductID = 0; SET lngRID = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;

/**************************************************************

	procProductUpdateRID

    Aug 26, 2011 : Lemu
    - create this procedure

	CALL procProductUpdateRID(3924, 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRID
GO

create procedure procProductUpdateRID(
									IN lngProductID BIGINT, 
									IN lngRID BIGINT)
BEGIN
	-- Update the RID to Products table
	UPDATE tblProducts SET 
		RID	= lngRID 
	WHERE ProductID	= lngProductID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerProduct

    Aug 26, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerProduct(3139, '2011-09-28 00:00:00', '2011-09-28 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProduct
GO

create procedure procUpdateProductReorderOverStockPerProduct(IN lngProductID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
	
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerProductID

    Aug 26, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerProductID(3011, 1, '2011-09-25 00:00:00', '2011-09-27 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProductID
GO

create procedure procUpdateProductReorderOverStockPerProductID(IN lngProductID BIGINT, IN strSessionID VARCHAR(15), IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngRID BIGINT DEFAULT 0;
	DECLARE intAvgCounter INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE decQuantity, decTotalQuantity, decAverageSales, decTotalAverageSales, decIDC DECIMAL(10,2) DEFAULT 0;
	DECLARE intValidTransactionItemStatus INTEGER DEFAULT 0;
	DECLARE intOrderSlipItemStatus INTEGER DEFAULT 5;
	
	SELECT Quantity, RID INTO decQuantity, lngRID FROM tblProducts WHERE ProductID = lngProductID;
	
	SET intValidTransactionItemStatus = 0; SET intOrderSlipItemStatus = 5;
	SET intAvgCounter = 0; SET decTotalAverageSales = 0;
	
	-- SELECT * FROM tblCountingRef;
	-- SELECT lngProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Get the average sales
	IF (MONTH(dteStartDate) <= 1) AND (MONTH(dteEndDate) >= 1) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions01 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems01 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 2) AND (MONTH(dteEndDate) >= 2) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions02 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems02 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 3) AND (MONTH(dteEndDate) >= 3) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions03 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems03 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 4) AND (MONTH(dteEndDate) >= 4) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions04 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems04 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 5) AND (MONTH(dteEndDate) >= 5) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions05 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems05 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 6) AND (MONTH(dteEndDate) >= 6) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions06 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems06 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 7) AND (MONTH(dteEndDate) >= 7) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions07 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems07 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 8) AND (MONTH(dteEndDate) >= 8) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions08 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems08 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 9) AND (MONTH(dteEndDate) >= 9) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions09 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems09 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 10) AND (MONTH(dteEndDate) >= 10) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions10 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems10 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 11) AND (MONTH(dteEndDate) >= 11) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions11 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems11 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	IF (MONTH(dteStartDate) <= 12) AND (MONTH(dteEndDate) >= 12) THEN
		SELECT AVG(Quantity) INTO decAverageSales FROM 
				( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
										LEFT JOIN tblTransactions12 b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
										LEFT JOIN tblTransactionItems12 c ON b.TransactionID = c.TransactionID
														AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
														AND ProductID = lngProductID
														AND TransactionDate BETWEEN dteStartDate AND dteEndDate
										WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
		SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	END IF;
	
	SET decAverageSales = decTotalAverageSales / intAvgCounter;
	
	-- Get the Inventory Days Covered of the existing Quantity
	-- SET decIDC = decQuantity / decAverageSales;
	IF decQuantity = 0 THEN
		SET decIDC = 0;
	ELSEIF decAverageSales = 0 THEN
		SET decIDC = decQuantity;
	ELSE
		SET decIDC = decQuantity / decAverageSales;
	END IF;
	  
	-- Get the daily average sales will be used as RIDMinThreshold
	IF decIDC <> 0 THEN
		IF (lngRID > decIDC) THEN
			SET decTotalQuantity = (lngRID * cdbl(decIDC)) - cdbl(decQuantity);
		ELSE
			SET decTotalQuantity = 0;
		END IF; 
	ELSE
		SET decTotalQuantity = 0;
	END IF; 
	    
	-- IF (decIDC > lngRID) THEN 
	-- 	SET decTotalQuantity = decQuantity;
	-- ELSE
	-- 	SET decTotalQuantity = round(lngRID - decIDC) * decAverageSales;
	-- END IF; 
	
	-- For checking purposes uncomment this
	-- SELECT * FROM tblCountingRef;
	-- SELECT lngProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Set the RIDMinThreshold and RIDMaxThreshold
	UPDATE tblProducts SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = lngProductID;
	UPDATE tblProductBaseVariationsMatrix SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = lngProductID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerRID

    Aug 26, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerRID(1019, 10, '2011-09-10', '2011-09-16');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerRID
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerRID(IN lngSupplierID BIGINT, IN lngRID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0; 
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplier

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerSupplier(1019, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplier
GO

create procedure procUpdateProductReorderOverStockPerSupplier(IN lngSupplierID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerGroup

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerGroup(IN lngSupplierID BIGINT, IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerSubGroup

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerSubGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerSubGroup(IN lngSupplierID BIGINT, IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;																		
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerGroup

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerGroup
GO

create procedure procUpdateProductReorderOverStockPerGroup(IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSubGroup

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStockPerSubGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSubGroup(IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStock

    Sep 14, 2011 : Lemu
    - create this procedure

	CALL procUpdateProductReorderOverStock('2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStock
GO

create procedure procUpdateProductReorderOverStock(IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsAddPoint
	Lemuel E. Aceron
	CALL procContactRewardsAddPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPoint
GO

create procedure procContactRewardsAddPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(10,2))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsDeductPoint
	Lemuel E. Aceron
	CALL procContactRewardsDeductPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsDeductPoint
GO

create procedure procContactRewardsDeductPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(10,2))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints - pvtRewardPoint WHERE CustomerID = pvtContactID;
	
	UPDATE tblContactRewards SET RedeemedPoints =	RedeemedPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardsAddPurchase
	Lemuel E. Aceron
	CALL procContactRewardsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPurchase
GO

create procedure procContactRewardsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(10,2))
BEGIN

	UPDATE tblContactRewards SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardModify
	Lemuel E. Aceron
	CALL procContactRewardModify(2885);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardModify
GO

create procedure procContactRewardModify(IN lngCustomerID BIGINT, 
										IN strRewardCardNo VARCHAR(15), 
										IN intRewardActive TINYINT(1),
										IN decPoints DECIMAL(10,2),
										IN dteRewardAwardDate DATETIME,
										IN intRewardCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN dteBirthDate DATE)
BEGIN
	
	IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE RewardCardNo = strRewardCardNo)) THEN
		IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactRewards(CustomerID, RewardCardNo, RewardActive, RewardPoints, RewardAwardDate, RewardCardStatus, ExpiryDate, BirthDate) 
								  VALUES(lngCustomerID, strRewardCardNo, intRewardActive, decPoints, dteRewardAwardDate, intRewardCardStatus, dteExpiryDate, dteBirthDate);
		ELSE
			UPDATE tblContactRewards SET
				RewardCardNo = strRewardCardNo,
				RewardActive = intRewardActive,
				RewardAwardDate = dteRewardAwardDate,
				RewardCardStatus = intRewardCardStatus,
				ExpiryDate = dteExpiryDate,
				BirthDate = dteBirthDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactRewards SET
			RewardCardNo = strRewardCardNo,
			RewardActive = intRewardActive,
			RewardAwardDate = dteRewardAwardDate,
			RewardCardStatus = intRewardCardStatus,
			ExpiryDate = dteExpiryDate,
			BirthDate = dteBirthDate
		WHERE
			RewardCardNo = strRewardCardNo;
	END IF;
	
	/*******************************
		CALL procContactRewardModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactRewardsMovementInsert
	Lemuel E. Aceron
	CALL procContactRewardsMovementInsert();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsMovementInsert
GO

create procedure procContactRewardsMovementInsert(
	IN lngCustomerID BIGINT(20),
	IN dteRewardDate DATETIME,
	IN decRewardPointsBefore DECIMAL(10,2),
	IN decRewardPointsAdjustment DECIMAL(10,2),
	IN decRewardPointsAfter DECIMAL(10,2),
	IN dteRewardExpiryDate DATE,
	IN strRewardReason VARCHAR(150),
	IN strTerminalNo VARCHAR(10),
	IN strCashierName VARCHAR(150),
	IN strTransactionNo VARCHAR(15))
BEGIN

	INSERT INTO tblContactRewardsMovement (
		CustomerID, RewardDate, RewardPointsBefore, RewardPointsAdjustment, RewardPointsAfter,
		RewardExpiryDate, RewardReason, TerminalNo, CashierName, TransactionNo
	)VALUES(
		lngCustomerID, dteRewardDate, decRewardPointsBefore, decRewardPointsAdjustment, decRewardPointsAfter,
		dteRewardExpiryDate, strRewardReason, strTerminalNo, strCashierName, strTransactionNo
	);
	
	
END;
GO
delimiter ;

/**************************************************************

	procProductUpdateRewardPoints
	Lemuel E. Aceron
    March 14, 2009

	CALL procProductUpdateRewardPoints(0,0,0,2);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRewardPoints
GO

create procedure procProductUpdateRewardPoints(
						IN lngProductGroupID BIGINT,
						IN lngProductSubGroupID BIGINT,
						IN lngProductID BIGINT,
						IN decRewardPoints NUMERIC)
BEGIN
	IF (lngProductID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductID = lngProductID;
	ELSEIF (lngProductSubGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID = lngProductSubGroupID;
	ELSEIF (lngProductGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngProductGroupID);
	ELSE
		UPDATE tblProducts SET RewardPoints = decRewardPoints;
	END IF;
	
END;
GO
delimiter ;

/**************************************************************

	procProductBaseVariationUpdateActualQuantity
	Lemuel E. Aceron
    Oct 24, 2011

	CALL procProductBaseVariationUpdateActualQuantity();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationUpdateActualQuantity
GO

create procedure procProductBaseVariationUpdateActualQuantity(
						IN lngProductID bigint,
						IN lngMatrixID bigint,
						IN decQuantity numeric)
BEGIN
	IF (lngMatrixID != 0) THEN
		UPDATE tblProductBaseVariationsMatrix SET ActualQuantity = decQuantity WHERE MatrixID = lngMatrixID;
	END IF;
	
	UPDATE tblProducts SET 
		ActualQuantity = (SELECT IFNULL(SUM(ActualQuantity),0) FROM tblProductBaseVariationsMatrix a WHERE a.Deleted = 0 AND a.ProductID = tblProducts.ProductID) 
	WHERE ProductID = lngProductID;
	
END;
GO
delimiter ;

/**************************************************************
	procProductBranchInventoryInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryInsert(1);
	
	Oct 6, 2009 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryInsert
GO

create procedure procProductBranchInventoryInsert(IN lngProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = lngProductID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID)VALUES (intBranchID, lngProductID);
		END IF;
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procSyncProductVariationFromQuantityPerItemAllBranch
	Lemuel E. Aceron
	CALL procSyncProductVariationFromQuantityPerItemAllBranch(1);
	
	Oct 28, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItemAllBranch
GO

create procedure procSyncProductVariationFromQuantityPerItemAllBranch(IN lngProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		-- Put the correct quantity of the newly created variation based on Product Quantity
		CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procProductBranchInventoryMatrixInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixInsert(1);
	
	Oct 29, 2011 : Lemu - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixInsert
GO

create procedure procProductBranchInventoryMatrixInsert(IN lngProductID BIGINT, IN lngMatrixID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch WHERE BranchID <> 1; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch WHERE BranchID <> 1; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID, MatrixID)VALUES (intBranchID, lngProductID, MatrixID);
		END IF;
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryMatrixCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixCopyAllItems
GO

create procedure procProductBranchInventoryMatrixCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE lngProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	SELECT COUNT(MatrixID) INTO lngCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO lngProductID, lngMatrixID;
		
		CALL procProductBranchInventoryMatrixInsert(lngProductID, lngMatrixID);
		
		SET lngProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryCopyAllItems
GO

create procedure procProductBranchInventoryCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE Deleted = 0; 
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO lngProductID;
		
		CALL procProductBranchInventoryInsert(lngProductID);
		
		SET lngProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

/**************************************************************
	procContactCreditModify
	Lemuel E. Aceron
	CALL procContactCreditModify(5, 5, 0, 'Egay', '2011-11-01 01:00:00', 0, '2012-11-01 01:00:00', 1, 1000);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditModify
GO

create procedure procContactCreditModify(IN lngCustomerID BIGINT, 
										IN lngGuarantorID BIGINT, 
										IN intCreditType TINYINT(1),
										IN strCreditCardNo VARCHAR(15), 
										IN dteCreditAwardDate DATETIME,
										IN intCreditCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN intCreditActive TINYINT(1),
										IN decCreditLimit DECIMAL(10,2))
BEGIN
	
	UPDATE tblContacts SET
		IsCreditAllowed = intCreditActive,
		CreditLimit = decCreditLimit
	WHERE ContactID = lngCustomerID;
	
	IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CreditCardNo = strCreditCardNo)) THEN
		IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditType, CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate) 
								  VALUES(lngCustomerID, lngGuarantorID, intCreditType, strCreditCardNo, dteCreditAwardDate, intCreditCardStatus, dteExpiryDate);
		ELSE
			UPDATE tblContactCreditCardInfo SET
				CreditCardNo = strCreditCardNo,
				CreditAwardDate = dteCreditAwardDate,
				CreditCardStatus = intCreditCardStatus,
				ExpiryDate = dteExpiryDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactCreditCardInfo SET
			CreditCardNo = strCreditCardNo,
			CreditAwardDate = dteCreditAwardDate,
			CreditCardStatus = intCreditCardStatus,
			ExpiryDate = dteExpiryDate
		WHERE
			CreditCardNo = strCreditCardNo;
	END IF;
	
	/*******************************
		CALL procContactCreditModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactCreditsAddPurchase
	Lemuel E. Aceron
	CALL procContactCreditsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditsAddPurchase
GO

create procedure procContactCreditsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(10,2))
BEGIN

	UPDATE tblContactCreditCardInfo SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;
