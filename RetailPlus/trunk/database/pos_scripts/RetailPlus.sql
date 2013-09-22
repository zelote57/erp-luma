/******************************************************************************
**		File: RetailPlus.sql
**		Name: RetailPlus
**		Desc: RetailPlus
**
**		Auth: Lem E. Aceron
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/ 

USE mysql;


DROP DATABASE IF EXISTS pos;

CREATE DATABASE pos;

GRANT ALL PRIVILEGES ON pos.* TO POSUser IDENTIFIED BY 'pospwd' WITH GRANT OPTION;
FLUSH PRIVILEGES;
DELETE FROM user WHERE user = '' OR user = null;
UPDATE user SET password = OLD_PASSWORD('pospwd') WHERE user = 'POSUser';
FLUSH PRIVILEGES;

USE pos;

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
`Debit` decimal(18,3) NOT NULL DEFAULT 0,
`Credit` decimal(18,3) NOT NULL DEFAULT 0,
`CreditLimit` decimal(18,3) NOT NULL DEFAULT 0,
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
`Status` TINYINT(1) NOT NULL DEFAULT 0,
`DateCreated` DATETIME NOT NULL,
`IsPrinterAutoCutter` TINYINT(1) NOT NULL DEFAULT 0,
`MaxReceiptWidth` INT(10) NOT NULL DEFAULT 40,
`TransactionNoLength` INT(2) NOT NULL DEFAULT 15,
`AutoPrint` TINYINT(1) NOT NULL DEFAULT 0,
`PrinterName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlus',
`TurretName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlusTurret',
`CashDrawerName` VARCHAR(20) NOT NULL DEFAULT 'RetailPlusDrawer',
`MachineSerialNo` VARCHAR(20) NOT NULL,
`AccreditationNo` VARCHAR(20) NOT NULL,
`ItemVoidConfirmation` TINYINT (1) NOT NULL DEFAULT 1,
`EnableEVAT` TINYINT(1) NOT NULL DEFAULT 0,
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


INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('One Thousand Pesos', 1000.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Five Hundred Pesos', 500.00, '');
INSERT INTO tblDenomination (DenominationCode, `DenominationValue`, ImagePath) VALUES ('Two Hundred Pesos', 200.00, '');
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

INSERT INTO sysAccessUserDetails (UID ,Name, CountryID, GroupID) VALUES (1, 'Lem E. Aceron', 1, 1);


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
INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (1, 'EXP', 'EXPIRATION');
INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (2, 'SZE', 'SIZE');
INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (3, 'COL', 'COLOR');
INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (4, 'LEN', 'LENGTH');
INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (5, 'WID', 'WIDTH');

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
**	Lem E. Aceron
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

/*****************************
**	Added on April 25, 2007
**	Lem E. Aceron
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
**	Lem E. Aceron
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
**	Lem E. Aceron
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
**	Lem E. Aceron
**	Parameterized the display of items in Front-End if items with Quantity 
**	to display only Items with more than zero quantity
**		0 - means false [display all items]
**		1 - means true  [display only more than zero items]
*****************************/ 
alter table tblTerminal add `ShowItemMoreThanZeroQty` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE sysAccessUserDetails ADD PageSize INT(5) NOT NULL DEFAULT 10; 

/*****************************
**	Added on Feb 07, 2008
**	Lem E. Aceron
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
`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
`BranchID` INT(4) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblBranch(`BranchID`),
PRIMARY KEY (BranchInventoryID),
INDEX `IX_tblRemoteBranchInventory`(`ProductCode`, `BranchID`),
INDEX `IX1_tblRemoteBranchInventory`(`BranchID`)
); 

/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lem E. Aceron
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
**	Lem E. Aceron
*****************************/

ALTER TABLE tblCashierReport ADD `IsCashCountInitialized` TINYINT(1) NOT NULL DEFAULT 0; 


/******************************************************************
**	Added on Dec 09, 2008 for OrderSlipPrinter
**	Lem E. Aceron
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
**	Lem E. Aceron
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
**	Lem E. Aceron
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
** Lem E. Aceron
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
** Lem E. Aceron
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
Lem E. Aceron

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
** Lem E. Aceron
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
Lem E. Aceron
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
Lem E. Aceron
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
	`PurchasePriceBefore` DECIMAL(18,3),
	`PurchasePriceNow` DECIMAL(18,3),
	`SellingPriceBefore` DECIMAL(18,3),
	`SellingPriceNow` DECIMAL(18,3),
	`VATBefore` DECIMAL(18,3),
	`VATNow` DECIMAL(18,3),
	`EVATBefore` DECIMAL(18,3),
	`EVATNow` DECIMAL(18,3),
	`LocalTaxBefore` DECIMAL(18,3),
	`LocalTaxNow` DECIMAL(18,3),
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
	`PurchasePriceBefore` DECIMAL(18,3),
	`PurchasePriceNow` DECIMAL(18,3),
	`SellingPriceBefore` DECIMAL(18,3),
	`SellingPriceNow` DECIMAL(18,3),
	`VATBefore` DECIMAL(18,3),
	`VATNow` DECIMAL(18,3),
	`EVATBefore` DECIMAL(18,3),
	`EVATNow` DECIMAL(18,3),
	`LocalTaxBefore` DECIMAL(18,3),
	`LocalTaxNow` DECIMAL(18,3),
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
	`Quantity` DECIMAL(18,3),
	`UnitCode` VARCHAR(10) NOT NULL,
	`UnitName` VARCHAR(30) NOT NULL,
	`PurchasePrice` DECIMAL(18,3),
	`Price` DECIMAL(18,3),
	`VAT` DECIMAL(18,3),
	`EVAT` DECIMAL(18,3),
	`LocalTax` DECIMAL(18,3),
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
** Lem E. Aceron
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
** Lem E. Aceron
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
** Lem E. Aceron
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
**	Lem E. Aceron
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
**	Lem E. Aceron
**
**  Start supporting the new MySQL 5.5 and higher version
**  Remove the unsupported 'TYPE=INNODB COMMENT = '
**  
*****************************/

/*****************************
**	For Releasing of Items
**	Added: April 7, 2011 
**	Lem E. Aceron
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
**	Lem E. Aceron
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
** Lem E. Aceron
**
**************************************************************/

DROP TABLE IF EXISTS tblProductMovement;
CREATE TABLE tblProductMovement (
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`ProductCode` VARCHAR(30) NOT NULL,
	`ProductDescription` VARCHAR(50) NOT NULL,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`MatrixDescription` VARCHAR(100),
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

ALTER TABLE tblProducts ADD RIDMinThreshold DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblProducts ADD RIDMaxThreshold DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD RIDMinThreshold DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD RIDMaxThreshold DECIMAL(18,3) DEFAULT 0;

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

ALTER TABLE tblTerminal ADD ShowCustomerSelection TINYINT (1) NOT NULL DEFAULT 1;
update tblTerminal set ShowCustomerSelection = 0;

ALTER TABLE tblTerminal ADD EnableRewardPoints TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD RewardPointsMinimum DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsEvery DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPoints DECIMAL(18,3) DEFAULT 0;

ALTER TABLE tblProducts ADD RewardPoints DECIMAL(18,3) DEFAULT 0;

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
ALTER TABLE tblProductBaseVariationsMatrix ADD ActualQuantity DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblProductBaseVariationsMatrix ADD Deleted TINYINT (1) NOT NULL DEFAULT 0;

/*********************************  v_3.0.3.5.sql END  *******************************************************/ 

/*********************************  v_3.0.3.6.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.6';


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
`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
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
	`TotalPurchases` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`CreditPaid` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`CreditCardStatus` TINYINT(1) NOT NULL DEFAULT 0,
	`ExpiryDate` DATE NOT NULL DEFAULT '0001-01-01',
INDEX `IX_tblContactCreditCardInfo`(`CustomerID`),
INDEX `IX_tblContactCreditCardInfo1`(`CreditCardNo`),
INDEX `IX_tblContactCreditCardInfo2`(`CustomerID`, `CreditCardNo`),
UNIQUE `PK_tblContactCreditCardInfo1`(`CustomerID`),
UNIQUE `PK_tblContactCreditCardInfo2`(`CreditCardNo`)
);

DROP TABLE IF EXISTS tblContactCreditCardMovement;
-- CREATE TABLE tblContactCreditCardMovement (
--	`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
--	`GuarantorID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
--	`CreditType` TINYINT(1) NOT NULL DEFAULT 0,
--	`CreditDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
--	`CreditBefore` DECIMAL(18,3) NOT NULL DEFAULT 0,
--	`Credit` DECIMAL(18,3) NOT NULL DEFAULT 0,
--	`CreditAfter` DECIMAL(18,3) NOT NULL DEFAULT 0,
--	`CreditExpiryDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
--	`CreditReason` VARCHAR(150) NOT NULL,
--	`TerminalNo` VARCHAR(10) NOT NULL,
--	`CashierName` VARCHAR(150) NOT NULL,
--	`TransactionNo` VARCHAR(15) NOT NULL,
-- INDEX `IX_tblContactCreditCardMovement`(`CustomerID`),
-- INDEX `IX_tblContactCreditCardMovement1`(`CreditDate`),
-- INDEX `IX_tblContactCreditCardMovement2`(`CustomerID`, `CreditDate`)
-- );

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

ALTER TABLE tblTerminal ADD AutoGenerateRewardCardNo TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD EnableRewardPointsAsPayment TINYINT (1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD RewardPointsMaxPercentageForPayment DECIMAL(5,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsPaymentValue DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardPointsPaymentCashEquivalent DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD RewardsPermitNo VARCHAR(30);
ALTER TABLE tblTerminal ADD InHouseIndividualCreditPermitNo VARCHAR(30);
ALTER TABLE tblTerminal ADD InHouseGroupCreditPermitNo VARCHAR(30);

ALTER TABLE tblTransactions01 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions01 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReport ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD NoOfRewardPointsPayment INT(10) NOT NULL DEFAULT 0;

/*********************************  v_3.0.3.6.sql END  *******************************************************/ 

/*********************************  v_3.0.3.7.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.3.7';

ALTER TABLE tblTerminal ADD `IsFineDining` INT(1) NOT NULL DEFAULT 1;

ALTER TABLE tblProductGroup ADD `SequenceNo` BIGINT NOT NULL DEFAULT 1;
ALTER TABLE tblProductSubGroup ADD `SequenceNo` BIGINT NOT NULL DEFAULT 1;
ALTER TABLE tblProducts ADD `SequenceNo` BIGINT NOT NULL DEFAULT 1;

UPDATE tblProductGroup SET SequenceNo = ProductGroupID;
UPDATE tblProductSubGroup SET SequenceNo = ProductSubGroupID;
UPDATE tblProducts SET SequenceNo = ProductID;

ALTER TABLE tblTransactions01 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions02 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions03 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions04 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions05 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions06 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions07 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions08 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions09 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions10 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions11 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions12 ADD PaxNo INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblTransactionItems01 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems02 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems03 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems04 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems05 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems06 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems07 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems08 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems09 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems10 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems11 ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactionItems12 ADD PaxNo INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblProducts ADD `Barcode2` VARCHAR(30);
ALTER TABLE tblProducts ADD `Barcode3` VARCHAR(30);

CREATE INDEX IX_tblProducts_Barcode2 ON tblProducts (Barcode2);
CREATE INDEX IX_tblProducts_Barcode3 ON tblProducts (Barcode3);

ALTER TABLE tblUnit MODIFY `UnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems01 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems02 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems03 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems04 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems05 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems06 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems07 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems08 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems09 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems10 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems11 MODIFY `ProductUnitCode` VARCHAR(5);
ALTER TABLE tblTransactionItems12 MODIFY `ProductUnitCode` VARCHAR(5);


ALTER TABLE tblProducts MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems01 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 MODIFY `Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0;

UPDATE tblTerminal SET DBVersion = '3.0.3.71';


ALTER TABLE tblTerminal ADD `PersonalChargeTypeID` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD `GroupChargeTypeID` INT(10) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactions01 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;

UPDATE tblTerminal SET DBVersion = '3.0.3.72';

ALTER TABLE tblContactRewards ADD EmbossedCardNo VARCHAR(15);
ALTER TABLE tblContactCreditCardInfo ADD EmbossedCardNo VARCHAR(15);

UPDATE tblContactRewards SET EmbossedCardNo = NULL;

UPDATE tblContactRewards, tblContacts SET 
	EmbossedCardNo = REPLACE(ContactCode,'ADV-','')
WHERE tblContactRewards.CustomerID = tblContacts.ContactID AND ContactCode LIKE 'ADV-%';

UPDATE tblTerminal SET DBVersion = '3.0.3.73';

/****************************Dec 01, 2011****************************************************/

ALTER TABLE tblProductPackage ADD `Barcode1` VARCHAR(30);
ALTER TABLE tblProductPackage ADD `Barcode2` VARCHAR(30);
ALTER TABLE tblProductPackage ADD `Barcode3` VARCHAR(30);

CREATE INDEX IX_tblProductPackage_Barcode1 ON tblProductPackage (Barcode1);
CREATE INDEX IX_tblProductPackage_Barcode2 ON tblProductPackage (Barcode2);
CREATE INDEX IX_tblProductPackage_Barcode3 ON tblProductPackage (Barcode3);

CREATE INDEX IX_tblProductPackage_Barcode ON tblProductPackage (Barcode1, Barcode2, Barcode3);

-- UPDATE tblProducts SET BaseUnitID = 1 where BaseUnitID <> 1;
-- UPDATE tblProductUnitMatrix SET bottomunitid = 48 where BottomUnitID = 1;

UPDATE tblProductPackage, tblProducts SET 
	tblProductPackage.BarCode1 = tblProducts.BarCode,
	tblProductPackage.BarCode2 = tblProducts.BarCode2,
	tblProductPackage.BarCode3 = tblProducts.BarCode3
WHERE tblProductPackage.ProductID = tblProducts.ProductID 
	AND tblProductPackage.UnitID = tblProducts.BaseUnitID 
	AND tblProductPackage.Quantity = 1;
	
INSERT INTO tblProductPackage (ProductID, UnitID, Price, WSPrice, PurchasePrice, Quantity, VAT, EVAT, LocalTax, BarCode1, BarCode2, BarCode3) SELECT a.ProductID, b.BottomUnitID, a.Price * b.BaseUnitValue, a.WSPrice * b.BaseUnitValue, a.PurchasePrice * b.BaseUnitValue, 1, a.VAT, a.EVAT, a.LocalTax, '', '', '' FROM tblProducts a INNER JOIN tblProductUnitMatrix b ON a.ProductID = b.ProductID;

UPDATE tblTerminal SET DBVersion = '3.0.3.74';


/*********************************  v_3.0.3.7.sql END  *******************************************************/ 

/*********************************  v_3.0.4.0.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.4.0';

ALTER TABLE tblTerminalReportHistory ADD `InitializedBy` VARCHAR(150);

UPDATE tblTerminalReportHistory SET InitializedBy = (SELECT User FROM sysAuditTrail where Activity = 'InitializeZRead' AND tblTerminalReportHistory.DateLastInitialized < sysAuditTrail.ActivityDate ORDER BY ActivityDate DESC limit 1);


/*********************************  v_3.0.4.0.sql END  *******************************************************/ 

/*********************************  v_3.0.5.0.sql START  *******************************************************/ 

UPDATE tblTerminal SET DBVersion = '3.0.5.0';


/*********************************  v_3.0.5.0.sql END  *******************************************************/ 


UPDATE tblTerminal SET DBVersion = '4.0.0.0';

ALTER TABLE tblTransactions01 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions02 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions03 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions04 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions05 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions06 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions07 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions08 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions09 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions10 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions11 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions12 ADD `BranchID` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblTerminal ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminalReport ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminalReportHistory ADD `BranchID` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblTerminalReport DROP PRIMARY KEY;
ALTER TABLE tblTerminalReport ADD PRIMARY KEY (TerminalNo, BranchID);
ALTER TABLE tblTerminalReport DROP KEY PK_tblTerminalReport;
ALTER TABLE tblTerminalReport ADD UNIQUE KEY PK_tblTerminalReport (TerminalNo, BranchID);


ALTER TABLE tblCashierReport ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblCashierReportHistory ADD `BranchID` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblCashierReport DROP PRIMARY KEY;
ALTER TABLE tblCashierReport ADD PRIMARY KEY (CashierID, TerminalNo, BranchID);
ALTER TABLE tblCashierReport DROP KEY PK_tblCashierReport;
ALTER TABLE tblCashierReport ADD UNIQUE KEY PK_tblCashierReport (CashierID, TerminalNo, BranchID);


ALTER TABLE tblCashierLogs ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblCashierLogs ADD `BranchCode` VARCHAR(30);
UPDATE tblCashierLogs, tblBranch SET tblCashierLogs.BranchCode = tblBranch.BranchCode WHERE tblCashierLogs.BranchID = tblBranch.BranchID AND tblCashierLogs.BranchCode IS NULL;
ALTER TABLE tblCashCount ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblCashCount ADD `BranchCode` VARCHAR(30);
UPDATE tblCashCount, tblBranch SET tblCashCount.BranchCode = tblBranch.BranchCode WHERE tblCashCount.BranchID = tblBranch.BranchID AND tblCashCount.BranchCode IS NULL;
ALTER TABLE tblWithHold ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblWithHold ADD `BranchCode` VARCHAR(30);
UPDATE tblWithHold, tblBranch SET tblWithHold.BranchCode = tblBranch.BranchCode WHERE tblWithHold.BranchID = tblBranch.BranchID AND tblWithHold.BranchCode IS NULL;
ALTER TABLE tblDeposit ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblDeposit ADD `BranchCode` VARCHAR(30);
UPDATE tblDeposit, tblBranch SET tblDeposit.BranchCode = tblBranch.BranchCode WHERE tblDeposit.BranchID = tblBranch.BranchID AND tblDeposit.BranchCode IS NULL;
ALTER TABLE tblDisburse ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblDisburse ADD `BranchCode` VARCHAR(30);
UPDATE tblDisburse, tblBranch SET tblDisburse.BranchCode = tblBranch.BranchCode WHERE tblDisburse.BranchID = tblBranch.BranchID AND tblDisburse.BranchCode IS NULL;
ALTER TABLE tblPaidOut ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblPaidOut ADD `BranchCode` VARCHAR(30);
UPDATE tblPaidOut, tblBranch SET tblPaidOut.BranchCode = tblBranch.BranchCode WHERE tblPaidOut.BranchID = tblBranch.BranchID AND tblPaidOut.BranchCode IS NULL;



/*****************************
**	tblTransactions
*****************************/
DROP TABLE IF EXISTS tblTransactions;
CREATE TABLE tblTransactions (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL,
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
INDEX `IXU_tblTransactions`(`TransactionDate`),
INDEX `IX0_tblTransactions`(`TransactionID`),
INDEX `IX1_tblTransactions`(`TransactionNo`),
INDEX `IX2_tblTransactions`(`CustomerID`),
INDEX `IX3_tblTransactions`(`CashierID`)
);


ALTER TABLE tblTransactions ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions ADD `CreatedByID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD `CreatedByName` VARCHAR(100);
ALTER TABLE tblTransactions ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions ADD `ReleaserID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD `ReleaserName` VARCHAR(100);
ALTER TABLE tblTransactions ADD `ReleasedDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblTransactions ADD RewardPointsPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD RewardConvertedPayment DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD PaxNo INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions ADD `CreditChargeAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions ADD `BranchID` INT(4) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions ADD `BranchCode` VARCHAR(30);
UPDATE tblTransactions, tblBranch SET tblTransactions.BranchCode = tblBranch.BranchCode WHERE tblTransactions.BranchID = tblBranch.BranchID AND tblTransactions.BranchCode IS NULL;

ALTER TABLE tblTransactions PARTITION BY RANGE( YEAR(TransactionDate) )
	SUBPARTITION BY HASH( MONTH(TransactionDate) ) (
		PARTITION p2010 VALUES LESS THAN (2010) (
			SUBPARTITION sJan2010,
			SUBPARTITION sFeb2010,
			SUBPARTITION sMar2010,
			SUBPARTITION sApr2010,
			SUBPARTITION sMay2010,
			SUBPARTITION sJun2010,
			SUBPARTITION sJul2010,
			SUBPARTITION sAug2010,
			SUBPARTITION sSep2010,
			SUBPARTITION sOct2010,
			SUBPARTITION sNov2010,
			SUBPARTITION sDec2010
		),
		PARTITION p2011 VALUES LESS THAN (2011) (
			SUBPARTITION sJan2011,
			SUBPARTITION sFeb2011,
			SUBPARTITION sMar2011,
			SUBPARTITION sApr2011,
			SUBPARTITION sMay2011,
			SUBPARTITION sJun2011,
			SUBPARTITION sJul2011,
			SUBPARTITION sAug2011,
			SUBPARTITION sSep2011,
			SUBPARTITION sOct2011,
			SUBPARTITION sNov2011,
			SUBPARTITION sDec2011
		) );


ALTER TABLE tblTransactions ADD PARTITION (
		PARTITION p2012 VALUES LESS THAN (2012) (
			SUBPARTITION sJan2012,
			SUBPARTITION sFeb2012,
			SUBPARTITION sMar2012,
			SUBPARTITION sApr2012,
			SUBPARTITION sMay2012,
			SUBPARTITION sJun2012,
			SUBPARTITION sJul2012,
			SUBPARTITION sAug2012,
			SUBPARTITION sSep2012,
			SUBPARTITION sOct2012,
			SUBPARTITION sNov2012,
			SUBPARTITION sDec2012) ) ;

ALTER TABLE tblTransactions ADD PARTITION (
		PARTITION p2013 VALUES LESS THAN (2013) (
			SUBPARTITION sJan2013,
			SUBPARTITION sFeb2013,
			SUBPARTITION sMar2013,
			SUBPARTITION sApr2013,
			SUBPARTITION sMay2013,
			SUBPARTITION sJun2013,
			SUBPARTITION sJul2013,
			SUBPARTITION sAug2013,
			SUBPARTITION sSep2013,
			SUBPARTITION sOct2013,
			SUBPARTITION sNov2013,
			SUBPARTITION sDec2013) ) ;

INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions01;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions02;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions03;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions04;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions05;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions06;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions07;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions08;
							
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions09;
							   
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions10;
									 
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions11;
										
INSERT INTO tblTransactions (TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID)
					SELECT	TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, TransactionDate, 
							DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount, TransDiscount, TransDiscountType, 
							VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, AmountPaid, CashPayment, ChequePayment, 
							CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount, DateClosed, PaymentType, DiscountCode, 
							DiscountRemarks, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, 
							WaiterID, WaiterName, Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName, 
							AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate, RewardPointsPayment, 
							RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID FROM tblTransactions12;


ALTER TABLE tblTransactionItems01 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems02 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems03 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems04 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems05 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems06 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems07 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems08 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems09 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems10 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems11 ADD TransactionNo VARCHAR(30);
ALTER TABLE tblTransactionItems12 ADD TransactionNo VARCHAR(30);

UPDATE tblTransactionItems01, tblTransactions01 SET tblTransactionItems01.TransactionNo = tblTransactions01.TransactionNo WHERE tblTransactionItems01.TransactionID = tblTransactions01.TransactionID;
UPDATE tblTransactionItems02, tblTransactions02 SET tblTransactionItems02.TransactionNo = tblTransactions02.TransactionNo WHERE tblTransactionItems02.TransactionID = tblTransactions02.TransactionID;
UPDATE tblTransactionItems03, tblTransactions03 SET tblTransactionItems03.TransactionNo = tblTransactions03.TransactionNo WHERE tblTransactionItems03.TransactionID = tblTransactions03.TransactionID;
UPDATE tblTransactionItems04, tblTransactions04 SET tblTransactionItems04.TransactionNo = tblTransactions04.TransactionNo WHERE tblTransactionItems04.TransactionID = tblTransactions04.TransactionID;
UPDATE tblTransactionItems05, tblTransactions05 SET tblTransactionItems05.TransactionNo = tblTransactions05.TransactionNo WHERE tblTransactionItems05.TransactionID = tblTransactions05.TransactionID;
UPDATE tblTransactionItems06, tblTransactions06 SET tblTransactionItems06.TransactionNo = tblTransactions06.TransactionNo WHERE tblTransactionItems06.TransactionID = tblTransactions06.TransactionID;
UPDATE tblTransactionItems07, tblTransactions07 SET tblTransactionItems07.TransactionNo = tblTransactions07.TransactionNo WHERE tblTransactionItems07.TransactionID = tblTransactions07.TransactionID;
UPDATE tblTransactionItems08, tblTransactions08 SET tblTransactionItems08.TransactionNo = tblTransactions08.TransactionNo WHERE tblTransactionItems08.TransactionID = tblTransactions08.TransactionID;
UPDATE tblTransactionItems09, tblTransactions09 SET tblTransactionItems09.TransactionNo = tblTransactions09.TransactionNo WHERE tblTransactionItems09.TransactionID = tblTransactions09.TransactionID;
UPDATE tblTransactionItems10, tblTransactions10 SET tblTransactionItems10.TransactionNo = tblTransactions10.TransactionNo WHERE tblTransactionItems10.TransactionID = tblTransactions10.TransactionID;
UPDATE tblTransactionItems11, tblTransactions11 SET tblTransactionItems11.TransactionNo = tblTransactions11.TransactionNo WHERE tblTransactionItems11.TransactionID = tblTransactions11.TransactionID;
UPDATE tblTransactionItems12, tblTransactions12 SET tblTransactionItems12.TransactionNo = tblTransactions12.TransactionNo WHERE tblTransactionItems12.TransactionID = tblTransactions12.TransactionID;


/*****************************
**	tblTransactionItems
*****************************/
DROP TABLE IF EXISTS tblTransactionItems;
CREATE TABLE `tblTransactionItems` (
  `TransactionItemsID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `TransactionID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `ProductID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `ProductCode` varchar(30) NOT NULL,
  `BarCode` varchar(30) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `ProductUnitID` int(3) unsigned NOT NULL DEFAULT '0',
  `ProductUnitCode` varchar(5) DEFAULT NULL,
  `Quantity` decimal(12,3) NOT NULL DEFAULT '0.000',
  `Price` decimal(18,2) NOT NULL DEFAULT '0.00',
  `SellingPrice` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Discount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ItemDiscount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ItemDiscountType` tinyint(1) NOT NULL DEFAULT '0',
  `Amount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `LocalTax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VariationsMatrixID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `MatrixDescription` varchar(150) DEFAULT NULL,
  `ProductGroup` varchar(20) DEFAULT NULL,
  `ProductSubGroup` varchar(20) DEFAULT NULL,
  `TransactionItemStatus` smallint(1) unsigned NOT NULL DEFAULT '0',
  `DiscountCode` varchar(5) DEFAULT NULL,
  `DiscountRemarks` varchar(255) DEFAULT NULL,
  `ProductPackageID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `MatrixPackageID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `PackageQuantity` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoQuantity` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoValue` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoInPercent` tinyint(1) NOT NULL DEFAULT '0',
  `PromoType` tinyint(1) NOT NULL DEFAULT '0',
  `PromoApplied` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PurchasePrice` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PurchaseAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `IncludeInSubtotalDiscount` tinyint(1) NOT NULL DEFAULT '1',
  `OrderSlipPrinter` tinyint(1) NOT NULL DEFAULT '0',
  `OrderSlipPrinted` tinyint(1) NOT NULL DEFAULT '0',
  `PercentageCommision` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Commision` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PaxNo` int(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`TransactionItemsID`),
  KEY `IX_tblTransactionItems01` (`TransactionItemsID`),
  KEY `IX0_tblTransactionItems01` (`TransactionID`,`ProductID`),
  KEY `IX1_tblTransactionItems01` (`TransactionID`,`ProductID`,`VariationsMatrixID`),
  KEY `IX2_tblTransactionItems01` (`ProductCode`),
  KEY `IX3_tblTransactionItems01` (`TransactionID`),
  KEY `IX4_tblTransactionItems01` (`ProductUnitID`)
);

ALTER TABLE tblTransactionItems01 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 ADD NewTransactionID bigint(20) unsigned NOT NULL DEFAULT 0;


UPDATE tblTransactionItems01, tblTransactions SET tblTransactionItems01.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems01.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems02, tblTransactions SET tblTransactionItems02.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems02.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems03, tblTransactions SET tblTransactionItems03.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems03.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems04, tblTransactions SET tblTransactionItems04.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems04.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems05, tblTransactions SET tblTransactionItems05.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems05.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems06, tblTransactions SET tblTransactionItems06.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems06.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems07, tblTransactions SET tblTransactionItems07.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems07.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems08, tblTransactions SET tblTransactionItems08.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems08.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems09, tblTransactions SET tblTransactionItems09.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems09.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems10, tblTransactions SET tblTransactionItems10.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems10.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems11, tblTransactions SET tblTransactionItems11.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems11.TransactionNo = tblTransactions.TransactionNo;
UPDATE tblTransactionItems12, tblTransactions SET tblTransactionItems12.NewTransactionID = tblTransactions.TransactionID WHERE tblTransactionItems12.TransactionNo = tblTransactions.TransactionNo;


INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems01;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems02;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems03;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems04;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems05;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems06;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems07;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems08;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems09;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems10;
								
INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems11;

INSERT INTO tblTransactionItems (TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo)
						SELECT	NewTransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, Quantity, 
								Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, EVatableAmount, 
								LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus, 
								DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity, PromoQuantity, PromoValue, 
								PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount, IncludeInSubtotalDiscount, 
								OrderSlipPrinter, OrderSlipPrinted, PercentageCommision, Commision, PaxNo FROM tblTransactionItems12;
													
DROP TABLE tblTransactions01;
DROP TABLE tblTransactions02;
DROP TABLE tblTransactions03;
DROP TABLE tblTransactions04;
DROP TABLE tblTransactions05;
DROP TABLE tblTransactions06;
DROP TABLE tblTransactions07;
DROP TABLE tblTransactions08;
DROP TABLE tblTransactions09;
DROP TABLE tblTransactions10;
DROP TABLE tblTransactions11;
DROP TABLE tblTransactions12;

DROP TABLE tblTransactionItems01;
DROP TABLE tblTransactionItems02;
DROP TABLE tblTransactionItems03;
DROP TABLE tblTransactionItems04;
DROP TABLE tblTransactionItems05;
DROP TABLE tblTransactionItems06;
DROP TABLE tblTransactionItems07;
DROP TABLE tblTransactionItems08;
DROP TABLE tblTransactionItems09;
DROP TABLE tblTransactionItems10;
DROP TABLE tblTransactionItems11;
DROP TABLE tblTransactionItems12;


ALTER TABLE tblProducts DROP PRIMARY KEY;
DROP INDEX PK_tblProducts ON tblProducts;
DROP INDEX PK1_tblProducts ON tblProducts;
DROP INDEX IX2_tblProducts ON tblProducts;
DROP INDEX IX3_tblProducts ON tblProducts;
DROP INDEX IX1_tblProducts ON tblProducts;
DROP INDEX IX_tblProducts_Barcode2 ON tblProducts;
DROP INDEX IX_tblProducts_Barcode3 ON tblProducts;
DROP INDEX IX4_tblProducts ON tblProducts;

ALTER TABLE tblProducts ADD INDEX IX_tblProducts_ProductID (ProductID);
ALTER TABLE tblProducts ADD INDEX IX_tblProducts_BarCode1 (BarCode);
ALTER TABLE tblProducts ADD INDEX IX_tblProducts_BarCode2 (BarCode2);
ALTER TABLE tblProducts ADD INDEX IX_tblProducts_BarCode3 (BarCode3);
ALTER TABLE tblProducts ADD INDEX IX_tblProducts_ProductCode (ProductCode);

ALTER TABLE tblProducts ENGINE = InnoDB;
ALTER TABLE tblProducts PARTITION BY HASH ( ProductID ) PARTITIONS 5 ;

/*********************************  v_4.0.0.0.sql END  *******************************************************/ 

/*********************************  v_4.0.0.1.sql START  *******************************************************/ 


ALTER TABLE tblProductSubGroup MODIFY ProductSubGroupCode VARCHAR(50);
ALTER TABLE tblTransactionItems MODIFY ProductSubGroup VARCHAR(50);

ALTER TABLE tblProductBaseVariationsMatrix MODIFY ActualQuantity DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblProducts MODIFY ActualQuantity DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblBranchInventory MODIFY ActualQuantity DECIMAL(18,3) DEFAULT 0;
ALTER TABLE tblBranchInventoryMatrix MODIFY ActualQuantity DECIMAL(18,3) DEFAULT 0;

-- [02/28/2012] For checking who did save the new purchase price
ALTER TABLE tblProductPurchasePriceHistory ADD `PurchaserName` VARCHAR(100);
ALTER TABLE tblProductPurchasePriceHistory ADD `DateCreated` DATETIME NOT NULL;
UPDATE tblProductPurchasePriceHistory SET 
	PurchaserName = (SELECT PurchaserName FROM tblPO WHERE tblPO.PurchaserName = tblProductPurchasePriceHistory.PurchaserName);

-- [03/02/2012] HP request to include the purchase price in Stocks to know how much is the cost of bad order.
ALTER TABLE tblStockItems ADD `PurchasePrice` DECIMAL(18,3) NOT NULL DEFAULT 0;
UPDATE tblStockItems a,  tblProductPackage b SET a.PurchasePrice = IFNULL(b.PurchasePrice,0) WHERE b.ProductID = a.ProductID AND b.UnitID  = a.ProductUnitID;

-- [03/18/2012] Include How the terminal will search item during search code.
--		0 = Search by Barcode1, Barcode2, Barcode 3 Only
--		1 = Search by Barcode1, Barcode2, Barcode 3, ProductCode Only
--		2 = Search by Barcode1, Barcode2, Barcode 3, ProductCode, ProductDesc Only
--		3 = Search by ProductCode Only
--		4 = Search by ProductDesc Only
--		5 = Search by ProductCode, ProductDesc Only
ALTER TABLE tblTerminal ADD `ProductSearchType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
-- 
/*********************************  v_4.0.0.1.sql END  *******************************************************/ 

/*********************************  v_4.0.0.2.sql  *******************************************************/ 

-- [03/31/2012] Include a Flag wether the Movement is in the Main Quantity or Branch Quantity
--		0 = Movement in Main Quantity
--		1 = Movement in Branch Quantity
ALTER TABLE tblProductMovement ADD `QuantityMovementType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductMovement MODIFY `TransactionNo` varchar(30) NOT NULL;


-- [04/02/2012] Remove the tblContactCreditCardMovement and put all info inside the tblCreditPayment
--
DROP TABLE IF EXISTS tblContactCreditCardMovement;

ALTER TABLE tblCreditPayment ADD `GuarantorID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`);
ALTER TABLE tblCreditPayment ADD `CreditType` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblCreditPayment ADD `CreditDate` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblCreditPayment ADD `CreditBefore` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCreditPayment ADD `CreditAfter` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblCreditPayment ADD `CreditExpiryDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00';
ALTER TABLE tblCreditPayment ADD `CreditReason` VARCHAR(150) NOT NULL;
ALTER TABLE tblCreditPayment ADD `TerminalNo` VARCHAR(10) NOT NULL;
ALTER TABLE tblCreditPayment ADD `CashierName` VARCHAR(150) NOT NULL;
ALTER TABLE tblCreditPayment ADD `AmountPaidCuttOffMonth` DECIMAL(18,3) NOT NULL DEFAULT 0;

-- CREATE INDEX `IX_tblContactCreditCardMovement`(`CustomerID`) ON 
-- INDEX `IX_tblContactCreditCardMovement1`(`CreditDate`),
-- INDEX `IX_tblContactCreditCardMovement2`(`CustomerID`, `CreditDate`)


/*****************************
**	sysConfig
*****************************/
DROP TABLE IF EXISTS sysConfig;
CREATE TABLE sysConfig (
	`ConfigName` VARCHAR(30) NOT NULL,
	`ConfigValue` VARCHAR(150) NOT NULL,
	PRIMARY KEY (ConfigName),
	INDEX `IX_sysConfig`(`ConfigName`),
	UNIQUE `PK_sysConfig`(`ConfigName`)
);

INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('CompanyCode',						'RBS');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('CompanyName',						'RETAILPLUS BUSINESS SOLUTIONS');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('TIN',								'104-384-077-000');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('Currency',							'PHP');
INSERT INTO sysConfig (ConfigName, ConfigValue) VALUES ('VersionFTPIPAddress',			    'Localhost');


ALTER TABLE tblTerminal MODIFY `Status` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `AutoPrint` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `TerminalReceiptType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `ProductSearchType` INT (1) NOT NULL DEFAULT 0;


ALTER TABLE tblTerminal MODIFY `IsPrinterAutoCutter` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `EnableEVAT` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `ItemVoidConfirmation` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `ShowCustomerSelection` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `ShowItemMoreThanZeroQty` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `RoundDownRewardPoints` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `EnableRewardPoints` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `IsFineDining` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `AutoGenerateRewardCardNo` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `EnableRewardPointsAsPayment` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `WillPrintGrandTotal` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminal MODIFY `IsVATInclusive` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminal MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactionItems MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `ItemDiscountType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `TransactionItemStatus` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `PromoType` INT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblContactRewards MODIFY `RewardCardStatus` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditCardStatus` INT (1) NOT NULL DEFAULT 0;

CREATE TABLE tblCalDate(
	`CalDate` Date,
    INDEX `IX_tblCalDate`(`CalDate`)
);


-- 06Jan2013 LEAceron : fixed data types due to Boolean and enum conflict

ALTER TABLE tblContactGroup MODIFY `ContactGroupCategory` TINYINT(1) UNSIGNED NOT NULL DEFAULT 3;
ALTER TABLE tblStockType MODIFY `StockDirection` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblPromo MODIFY `Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `Status` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `TerminalReceiptType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `OrderSlipPrinter` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup MODIFY `OrderSlipPrinter` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPLUReport MODIFY `OrderSlipPrinter` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblContactRewards MODIFY `RewardCardStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditCardStatus` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `ItemDiscountType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `PromoType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `OrderSlipPrinter` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblCreditPayment MODIFY `CreditType` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblTransactions ADD PARTITION (
		PARTITION p2014 VALUES LESS THAN (2014) (
			SUBPARTITION sJan2014,
			SUBPARTITION sFeb2014,
			SUBPARTITION sMar2014,
			SUBPARTITION sApr2014,
			SUBPARTITION sMay2014,
			SUBPARTITION sJun2014,
			SUBPARTITION sJul2014,
			SUBPARTITION sAug2014,
			SUBPARTITION sSep2014,
			SUBPARTITION sOct2014,
			SUBPARTITION sNov2014,
			SUBPARTITION sDec2014) ) ;

/*********************************  v_4.0.0.2.sql END  *******************************************************/ 

/*********************************  v_4.0.0.3.sql START
	Change the inventory to look into tblProductInventory
 *******************************************************/ 
ALTER TABLE tblCreditPayment MODIFY `Remarks` VARCHAR(8000); -- 09Mar2013 put payments log in remarks
ALTER TABLE tblProductMovement MODIFY `MatrixDescription` VARCHAR(100) DEFAULT '';

-- Update the correct transactionid. Found out in BOOZE that incorrect transactionid was saved for old transactions.
UPDATE tblCreditPayment SET
	TransactionID = (SELECT transactionid FROM tblTransactions 
				WHERE tblTransactions.transactionno = tblCreditPayment.transactionno
					and tblTransactions.customerid = tblCreditPayment.Contactid);

-- Added Apr 29, 2013
INSERT INTO tblReceipt(Module, Text, Value, Orientation) VALUES ('GroupCreditChargeHeader', '', 'GROUP CREDIT CHARGE SLIP',1);
INSERT INTO tblReceipt(Module, Text, Value, Orientation) VALUES ('IndividualCreditChargeHeader', '', 'CHARGE SLIP',1);
-- For HP values should be
--		GroupCreditChargeHeader:		HP CREDIT CHARGE SLIP
--		IndividualCreditChargeHeader:	SUPER CREDIT CHARGE SLIP

ALTER TABLE tblTerminal ADD `IncludeCreditChargeAgreement` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
-- If HP the value should be 1 which will include the following
--		I hereby agree  to pay the total  amount
--		stated herein including any charges  due
--		thereon  subject   to    the   pertinent
--		contract   governing  the use of    this


-- Added May 14, 2013. 
--	0  means product can be sold and not lock for closing inventory
--	1  means product can be sold and lock for closing inventory
ALTER TABLE tblProductGroup ADD `isLock` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblContacts ADD `isLock` TINYINT(1) UNSIGNED NOT NULL DEFAULT 0;





DROP TABLE IF EXISTS deleted_tblProducts;
CREATE TABLE deleted_tblProducts(SELECT * FROM tblProducts);

DROP TABLE IF EXISTS deleted_tblProductBaseVariationsMatrix;
CREATE TABLE deleted_tblProductBaseVariationsMatrix(SELECT * FROM tblProductBaseVariationsMatrix);

/*****************************
**	tblProductInventory
*****************************/
DROP TABLE IF EXISTS tblProductInventory;
CREATE TABLE tblProductInventory (
	`BranchID` INT(4) UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`IsLock` TINYINT(1) NOT NULL DEFAULT 0,
	INDEX `IX_tblProductInventory`(`BranchID`, `ProductID`, `MatrixID`),
	UNIQUE `PK_tblProductInventory`(`BranchID`, `ProductID`, `MatrixID`)
);


RENAME TABLE tblBranchInventory TO deleted_tblBranchInventory;
RENAME TABLE tblBranchInventoryMatrix TO deleted_tblBranchInventoryMatrix;

INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIn, QuantityOut, ActualQuantity)
SELECT BranchID, ProductID, 0, Quantity, QuantityIn, QuantityOut, ActualQuantity 
FROM deleted_tblBranchInventory
WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM deleted_tblBranchInventoryMatrix);

INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIn, QuantityOut, ActualQuantity)
SELECT BranchID, ProductID, MatrixID, Quantity, QuantityIn, QuantityOut, ActualQuantity FROM deleted_tblBranchInventoryMatrix;

-- insert all products that are not in the inventory
INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIn, QuantityOut, ActualQuantity)
SELECT brnch.BranchID, prd.ProductID, 0, prd.Quantity, prd.QuantityIn, prd.QuantityOut, prd.ActualQuantity
FROM deleted_tblProducts prd
LEFT OUTER JOIN tblBranch brnch ON 1=1
LEFT OUTER JOIN tblProductInventory inv ON prd.ProductID = inv.ProductID AND MatrixID = 0 AND brnch.BranchID = inv.BranchID
WHERE inv.MatrixID IS NULL;

-- insert all variations that are not in the inventory
INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIn, QuantityOut, ActualQuantity)
SELECT brnch.BranchID, prd.ProductID, mtrx.MatrixID, mtrx.Quantity, mtrx.QuantityIn, mtrx.QuantityOut, mtrx.ActualQuantity
FROM deleted_tblProducts prd
INNER JOIN deleted_tblProductBaseVariationsMatrix mtrx ON prd.ProductID = mtrx.ProductID
LEFT OUTER JOIN tblBranch brnch ON 1=1
LEFT OUTER JOIN tblProductInventory inv ON prd.ProductID = inv.ProductID AND inv.MatrixID = mtrx.MatrixID AND brnch.BranchID = inv.BranchID
WHERE inv.MatrixID IS NULL;

-- remove all variations where productid not in tblproducts
DELETE FROM tblProductBaseVariationsMatrix WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM tblProducts);

-- remove invalid variations from the inventory.
DELETE FROM tblProductInventory WHERE MatrixID <> 0 AND MatrixID NOT IN (SELECT DISTINCT MatrixID FROM tblProductBaseVariationsMatrix);

-- check if all variations are already in the inventory, should all be zero result
SELECT * FROM tblProductInventory WHERE MatrixID <> 0 AND MatrixID NOT IN (SELECT DISTINCT MatrixID FROM tblProductBaseVariationsMatrix);
SELECT * FROM tblProductBaseVariationsMatrix WHERE MatrixID <> 0 AND MatrixID NOT IN (SELECT DISTINCT MatrixID FROM tblProductInventory);

-- remove invalid products from the inventory. this is cause by the branchinventorymatrix
DELETE FROM tblProductInventory WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM tblProducts);

-- check if all products are already in the inventory, should all be zero result
SELECT * FROM tblProducts WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM tblProductInventory);
SELECT * FROM tblProductInventory WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM tblProducts);

-- update the quantity of constant products
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE');
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE');

ALTER TABLE tblProductBaseVariationsMatrix DROP Quantity;
ALTER TABLE tblProductBaseVariationsMatrix DROP QuantityIN;
ALTER TABLE tblProductBaseVariationsMatrix DROP QuantityOut;
ALTER TABLE tblProductBaseVariationsMatrix DROP ActualQuantity;

ALTER TABLE tblProducts DROP Quantity;
ALTER TABLE tblProducts DROP QuantityIN;
ALTER TABLE tblProducts DROP QuantityOut;
ALTER TABLE tblProducts DROP ActualQuantity;


/******************************** do the package insertion ***********************************/
ALTER TABLE tblProductPackage ADD MatrixID bigint NOT NULL DEFAULT 0;
ALTER TABLE tblProductPackage ADD PRIMARY KEY(PackageID);

ALTER TABLE tblProductPackage DROP INDEX `PK_tblProductPackage`;
ALTER TABLE tblProductPackage ADD UNIQUE INDEX `PK_tblProductPackage`(`ProductID`,`MatrixID`,`UnitID`,`Quantity`);
ALTER TABLE tblProductPackage DROP INDEX `IX_tblProductPackage`;
ALTER TABLE tblProductPackage ADD INDEX `IX_tblProductPackage`(`PackageID`,`ProductID`,`MatrixID`);

ALTER TABLE tblProductPackage ADD BarCode4 VARCHAR(60);
ALTER TABLE tblProductPackage ADD INDEX `IX3_tblProductPackage`(`BarCode1`,`BarCode2`,`BarCode3`,`BarCode4`);

INSERT INTO tblProductPackage(ProductID, MatrixID, UnitID, Price, Quantity, VAT, EVAT, LocalTax, WSPrice, Barcode1, Barcode2, Barcode3)
SELECT ProductID, 0, BaseUnitID, Price, 1, VAT, EVAT, LocalTax, WSPrice, BarCode, BarCode2, BarCode3
FROM tblProducts prd WHERE ProductID NOT IN (SELECT DISTINCT PRODUCTID FROM tblProductPackage WHERE MatrixID = 0);

DROP TABLE IF EXISTS deleted_tblmatrixpackage;
RENAME TABLE tblmatrixpackage TO deleted_tblmatrixpackage;

INSERT INTO tblProductPackage(ProductID, MatrixID, UnitID, Price, Quantity, VAT, EVAT, LocalTax, WSPrice, Barcode1, Barcode2, Barcode3)
SELECT ProductID, mtrxpkg.MatrixID, mtrxpkg.UnitID, mtrxpkg.Price, mtrxpkg.Quantity, mtrxpkg.VAT, mtrxpkg.EVAT, mtrxpkg.LocalTax, mtrxpkg.WSPrice, '', '', '' 
FROM deleted_tblmatrixpackage mtrxpkg INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrxpkg.MatrixID = mtrx.MatrixID;

-- update the barcode4 as a unique system barcode
UPDATE tblProductPackage SET 
	BarCode4 = REPLACE(CONCAT(IFNULL(BarCode1,''), Quantity, ProductID, MatrixID),'.','')
WHERE MatrixID = 0;	

-- update the barcode4 as a unique system barcode for each variations
UPDATE tblProductPackage prd 
INNER JOIN tblProductPackage mtrx ON
	prd.ProductID = mtrx.ProductID AND mtrx.MatrixID <> 0
SET mtrx.BarCode4 = REPLACE(CONCAT(IFNULL(prd.BarCode1,''), mtrx.Quantity, prd.ProductID, mtrx.MatrixID),'.','');

-- update the purchase price
UPDATE tblProductPackage pkg
INNER JOIN tblProducts prd ON pkg.ProductID = prd.ProductID AND pkg.MatrixID = 0 AND pkg.Quantity = 1 AND pkg.UnitID = prd.BaseUnitID 
SET pkg.purchaseprice = prd.PurchasePrice;

UPDATE tblProductPackage pkg
INNER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.ProductID = mtrx.ProductID AND pkg.MatrixID = mtrx.MatrixID AND pkg.Quantity = 1 AND pkg.UnitID = mtrx.UnitID 
SET pkg.purchaseprice = mtrx.PurchasePrice;

ALTER TABLE tblProducts DROP Barcode;
ALTER TABLE tblProducts DROP Barcode2;
ALTER TABLE tblProducts DROP Barcode3;

ALTER TABLE tblProducts DROP PurchasePrice;
ALTER TABLE tblProducts DROP Price;
ALTER TABLE tblProducts DROP WSPrice;
ALTER TABLE tblProducts DROP VAT;
ALTER TABLE tblProducts DROP EVAT;
ALTER TABLE tblProducts DROP LocalTax;

ALTER TABLE tblProductBaseVariationsMatrix DROP PurchasePrice;
ALTER TABLE tblProductBaseVariationsMatrix DROP Price;
ALTER TABLE tblProductBaseVariationsMatrix DROP WSPrice;
ALTER TABLE tblProductBaseVariationsMatrix DROP VAT;
ALTER TABLE tblProductBaseVariationsMatrix DROP EVAT;
ALTER TABLE tblProductBaseVariationsMatrix DROP LocalTax;

-- This will be use to determine if Transaction is Refund
--		0 POSNormal
--		1 POSRefund
ALTER TABLE tblTransactions ADD TransactionType INT(1) NOT NULL DEFAULT 0;


/*********************************  v_4.0.0.3.sql END  *******************************************************/ 

/*********************************  v_4.0.0.4.sql START  
	Include terminal as parking and parking rates
*******************************************************/ 

ALTER TABLE tblTerminal add `IsParkingTerminal` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminal add `WillPrintChargeSlip` TINYINT (1) NOT NULL DEFAULT 1;

/*****************************
**	tblParkingRates
		Overrides the base parking rate if the rate is not flat rate. Flat rate is the Price in tblProductPackage. 
		No need to create parking rate if rate is just flat rate but not daily. If daily flat rate then have to create this.
	
		MinimumStayInMin is the no of minutes on the first bucket 
			example: 24HRS everyday rate is 200 pesos value should be 1440 and MinimumStayPrice should be 200 (pesos)
			example: if first 2 hours is 100 pesos value should be 120 and MinimumStayPrice should be 100 (pesos)
			example: if first 1 hour is 50 pesos value should be 60 and MinimumStayPrice should be 50 (pesos)
			example: if first 15 mins is free value should be 15 and MinimumStayPrice should be 0 (pesos)
		NoOfUnitPerMin is the no of units per succeeding after the first charge of MinimumStayInMin
			example: if succeeding hourly rate is 10 pesos value should be 60 and PerUnitPrice should be 10 (pesos)
			example: if succeeding rate is 10 pesos every 30 min value should be 30 and PerUnitPrice should be 10 (pesos)
			example: if succeeding rate is 1 peso per min value should be 1 and PerUnitPrice should be 10 (peso)

*****************************/
DROP TABLE IF EXISTS tblParkingRates;
CREATE TABLE tblParkingRates (
	`ParkingRateID` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`DayOfWeek` VARCHAR(9) NOT NULL,
	`StartTime` VARCHAR(5) NOT NULL,
	`Endtime` VARCHAR(5) NOT NULL,
	`NoOfUnitPerMin` INT NOT NULL DEFAULT 1,
	`PerUnitPrice` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`MinimumStayInMin` INT NOT NULL DEFAULT 60,
	`MinimumStayPrice` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`CreatedByName` VARCHAR(100),
	`CreatedOn` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	`LastUpdatedByName` VARCHAR(100),
	`LastUpdatedOn` DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
	INDEX `IX_tblParkingRates`(`ProductID`, `DayOfWeek`, `StartTime`),
	PRIMARY KEY (ParkingRateID),
	UNIQUE `PK_tblParkingRates`(`ProductID`, `DayOfWeek`, `StartTime`, `Endtime`)
);

/*****************************
**	tblTransactionsBackup
		This table will hold the backup records of tblTransactions.
	
		Purpose of this is to make the reporting of sales items faster specially in groceries.

*****************************/

DROP TABLE IF EXISTS tblTransactionsBackup;
CREATE TABLE `tblTransactionsBackup` (
  `TransactionID` bigint(20) unsigned NOT NULL DEFAULT 0,
  `TransactionNo` varchar(30) NOT NULL,
  `CustomerID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `CustomerName` varchar(100) NOT NULL,
  `CashierID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `CashierName` varchar(100) NOT NULL,
  `TerminalNo` varchar(30) NOT NULL,
  `TransactionDate` datetime NOT NULL,
  `DateSuspended` datetime NOT NULL DEFAULT '0001-01-01 12:00:00',
  `DateResumed` datetime NOT NULL DEFAULT '0001-01-01 12:00:00',
  `TransactionStatus` smallint(1) unsigned NOT NULL DEFAULT '0',
  `SubTotal` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Discount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `TransDiscount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `TransDiscountType` int(10) NOT NULL DEFAULT '0',
  `VAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `LocalTax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `AmountPaid` decimal(18,2) NOT NULL DEFAULT '0.00',
  `CashPayment` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ChequePayment` decimal(18,2) NOT NULL DEFAULT '0.00',
  `CreditCardPayment` decimal(18,2) NOT NULL DEFAULT '0.00',
  `CreditPayment` decimal(18,2) NOT NULL DEFAULT '0.00',
  `BalanceAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ChangeAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `DateClosed` datetime NOT NULL DEFAULT '0001-01-01 12:00:00',
  `PaymentType` int(10) unsigned NOT NULL DEFAULT '4',
  `DiscountCode` varchar(5) DEFAULT NULL,
  `DiscountRemarks` varchar(255) DEFAULT NULL,
  `DebitPayment` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ItemsDiscount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Charge` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ChargeAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ChargeCode` varchar(30) DEFAULT NULL,
  `ChargeRemarks` varchar(255) DEFAULT NULL,
  `WaiterID` bigint(20) unsigned NOT NULL DEFAULT '2',
  `WaiterName` varchar(100) NOT NULL,
  `Packed` tinyint(1) NOT NULL DEFAULT '0',
  `OrderType` smallint(6) NOT NULL DEFAULT '0',
  `AgentID` bigint(20) NOT NULL DEFAULT '1',
  `AgentName` varchar(100) DEFAULT 'RetailPlus Customer T',
  `CreatedByID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `CreatedByName` varchar(100) DEFAULT NULL,
  `AgentDepartmentName` varchar(30) NOT NULL DEFAULT 'System Default Department',
  `AgentPositionName` varchar(30) NOT NULL DEFAULT 'System Default Position',
  `ReleaserID` bigint(20) NOT NULL DEFAULT '0',
  `ReleaserName` varchar(100) DEFAULT NULL,
  `ReleasedDate` datetime NOT NULL DEFAULT '0001-01-01 12:00:00',
  `RewardPointsPayment` decimal(18,3) NOT NULL DEFAULT '0.000',
  `RewardConvertedPayment` decimal(18,3) NOT NULL DEFAULT '0.000',
  `PaxNo` int(4) NOT NULL DEFAULT '1',
  `CreditChargeAmount` decimal(18,3) NOT NULL DEFAULT '0.000',
  `BranchID` int(4) NOT NULL DEFAULT '1',
  `BranchCode` varchar(30) DEFAULT NULL,
  `TransactionType` int(1) NOT NULL DEFAULT '0',
  `BackupDate` datetime,
  KEY `IXU_tblTransactionsBackup` (`TransactionDate`),
  KEY `IX0_tblTransactionsBackup` (`TransactionID`),
  KEY `IX1_tblTransactionsBackup` (`TransactionNo`),
  KEY `IX2_tblTransactionsBackup` (`CustomerID`),
  KEY `IX3_tblTransactionsBackup` (`CashierID`)
);

DROP TABLE IF EXISTS tblTransactionItemsBackup;
CREATE TABLE `tblTransactionItemsBackup` (
  `TransactionItemsID` bigint(20) unsigned NOT NULL DEFAULT 0,
  `TransactionID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `ProductID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `ProductCode` varchar(30) NOT NULL,
  `BarCode` varchar(30) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `ProductUnitID` int(3) unsigned NOT NULL DEFAULT '0',
  `ProductUnitCode` varchar(5) DEFAULT NULL,
  `Quantity` decimal(12,3) NOT NULL DEFAULT '0.000',
  `Price` decimal(18,2) NOT NULL DEFAULT '0.00',
  `SellingPrice` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Discount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ItemDiscount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ItemDiscountType` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `Amount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVAT` decimal(18,2) NOT NULL DEFAULT '0.00',
  `EVatableAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `LocalTax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `VariationsMatrixID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `MatrixDescription` varchar(150) DEFAULT NULL,
  `ProductGroup` varchar(20) DEFAULT NULL,
  `ProductSubGroup` varchar(50) DEFAULT NULL,
  `TransactionItemStatus` int(1) NOT NULL DEFAULT '0',
  `DiscountCode` varchar(5) DEFAULT NULL,
  `DiscountRemarks` varchar(255) DEFAULT NULL,
  `ProductPackageID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `MatrixPackageID` bigint(20) unsigned NOT NULL DEFAULT '0',
  `PackageQuantity` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoQuantity` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoValue` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PromoInPercent` tinyint(1) NOT NULL DEFAULT '0',
  `PromoType` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `PromoApplied` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PurchasePrice` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PurchaseAmount` decimal(18,2) NOT NULL DEFAULT '0.00',
  `IncludeInSubtotalDiscount` tinyint(1) NOT NULL DEFAULT '1',
  `OrderSlipPrinter` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `orderslipprinted` tinyint(1) NOT NULL DEFAULT '0',
  `PercentageCommision` decimal(18,2) NOT NULL DEFAULT '0.00',
  `Commision` decimal(18,2) NOT NULL DEFAULT '0.00',
  `PaxNo` int(4) NOT NULL DEFAULT '1',
  `BackupDate` datetime,
  PRIMARY KEY (`TransactionItemsID`),
  KEY `IX_tblTransactionItemsBackup` (`TransactionItemsID`),
  KEY `IX0_tblTransactionItemsBackup` (`TransactionID`,`ProductID`),
  KEY `IX1_tblTransactionItemsBackup` (`TransactionID`,`ProductID`,`VariationsMatrixID`),
  KEY `IX2_tblTransactionItemsBackup` (`ProductCode`),
  KEY `IX3_tblTransactionItemsBackup` (`TransactionID`),
  KEY `IX4_tblTransactionItemsBackup` (`ProductUnitID`)
);

/*********************************  v_4.0.0.5.sql END  *******************************************************/ 

/*********************************  v_4.0.1.0.sql START  
	09-Aug-2013 LEAceron
	Purpose: To serve the Customer Management System required by GLA Mandarin

	Requirements:
		Roster Information of members w/ complete information.
*******************************************************/ 

UPDATE tblTerminal SET DBVersion = '4.0.1.0';

DELETE FROM sysAccessRights WHERE TranTypeID = 147; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 147;
DELETE FROM sysAccessTypes WHERE TypeID = 147;
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (147, 'Customer Management Feature');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 147, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 147, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '11: Backend - MasterFiles' WHERE TypeID = 147;

ALTER TABLE sysConfig ADD Category VARCHAR(100);

ALTER TABLE tblContactGroup MODIFY ContactGroupCode VARCHAR(10);





/*****************************
**	tblProductInventoryAudit
**	This will contain all changes in the tblProductInventory
*****************************/
DROP TABLE IF EXISTS tblProductInventoryAudit;
CREATE TABLE tblProductInventoryAudit (
	`BranchID` INT(4) UNSIGNED NOT NULL,
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`IsLock` TINYINT(1) NOT NULL DEFAULT 0,
	`DateCreated` DATETIME NOT NULL DEFAULT NOW(),
	INDEX `IX_tblProductInventory`(`BranchID`, `ProductID`, `MatrixID`)
);

/*****************************
**	tblProductInventoryDaily
**	This will contain all the end-of-day quantity in tblProductInventory
*****************************/
DROP TABLE IF EXISTS tblProductInventoryDaily;
CREATE TABLE tblProductInventoryDaily (
	`BranchID` INT(4) UNSIGNED NOT NULL,
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`IsLock` TINYINT(1) NOT NULL DEFAULT 0,
	`DateCreated` DATETIME NOT NULL DEFAULT NOW(),
	INDEX `IX_tblProductInventory`(`BranchID`, `ProductID`, `MatrixID`)
);


/*****************************
**	tblProductInventoryMonthly
**	This will contain all the end-of-month quantity in tblProductInventory
*****************************/
DROP TABLE IF EXISTS tblProductInventoryMonthly;
CREATE TABLE tblProductInventoryMonthly (
	`BranchID` INT(4) UNSIGNED NOT NULL,
	`ProductID` BIGINT NOT NULL DEFAULT 0,
	`MatrixID` BIGINT NOT NULL DEFAULT 0,
	`Quantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityIn` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`QuantityOut` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`ActualQuantity` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`IsLock` TINYINT(1) NOT NULL DEFAULT 0,
	`DateMonth` VARCHAR(7),
	`DateCreated` DATETIME NOT NULL DEFAULT NOW(),
	INDEX `IX_tblProductInventory`(`BranchID`, `ProductID`, `MatrixID`)
);

CREATE INDEX IX_tblProductInventory_PID ON tblProductInventory (ProductID);
CREATE INDEX IX_tblProductInventory_PBID ON tblProductInventory (ProductID, BranchID);

DROP TABLE IF EXISTS tblContactsAudit;
CREATE TABLE `tblContactsAudit` (
  `ContactID` bigint(20) NOT NULL,
  `ContactCode` varchar(25) NOT NULL,
  `ContactName` varchar(75) NOT NULL,
  `ContactGroupID` int(10) unsigned DEFAULT '0',
  `ModeOfTerms` int(10) NOT NULL DEFAULT '0',
  `Terms` int(10) NOT NULL DEFAULT '0',
  `Address` varchar(150) NOT NULL DEFAULT '',
  `BusinessName` varchar(75) NOT NULL DEFAULT '',
  `TelephoneNo` varchar(75) NOT NULL DEFAULT '',
  `Remarks` varchar(150) NOT NULL DEFAULT '',
  `Debit` decimal(18,3) NOT NULL DEFAULT '0.00',
  `Credit` decimal(18,3) NOT NULL DEFAULT '0.00',
  `CreditLimit` decimal(18,3) NOT NULL DEFAULT '0.00',
  `IsCreditAllowed` tinyint(1) NOT NULL DEFAULT '0',
  `DateCreated` DATETIME NOT NULL,
  `Deleted` tinyint(1) NOT NULL DEFAULT '0',
  `DepartmentID` int(10) unsigned NOT NULL DEFAULT '1',
  `PositionID` int(10) unsigned NOT NULL DEFAULT '1',
  `isLock` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `AuditDateCreated` DATETIME NOT NULL DEFAULT NOW(),
  KEY `IX_tblcontactsAudit` (`ContactID`,`ContactCode`,`ContactName`),
  KEY `IX1_tblcontactsAudit` (`ContactGroupID`)
);

/*********************************  v_4.0.1.0.sql END  *******************************************************/ 

ALTER TABLE tblTransactions ADD `isConsignment` tinyint(1) unsigned NOT NULL DEFAULT 0;

ALTER TABLE tblProductInventory ADD ReservedQuantity DECIMAL(18,3) NOT NULL DEFAULT '0.000';
ALTER TABLE tblProductInventoryAudit ADD ReservedQuantity DECIMAL(18,3) NOT NULL DEFAULT '0.000';
ALTER TABLE tblProductInventoryDaily ADD ReservedQuantity DECIMAL(18,3) NOT NULL DEFAULT '0.000';
ALTER TABLE tblProductInventoryMonthly ADD ReservedQuantity DECIMAL(18,3) NOT NULL DEFAULT '0.000';


/*****************************
**	tblContactAddOn
*****************************/
DROP TABLE IF EXISTS tblContactDetails;
DROP TABLE IF EXISTS tblContactAddOn;
CREATE TABLE tblContactAddOn (
    `ContactDetailID` bigint(20) unsigned NOT NULL DEFAULT 0,
	`ContactID` BIGINT(20) NOT NULL DEFAULT 0,
	`Salutation` VARCHAR(25) NOT NULL,
	`FirstName` VARCHAR(85) NOT NULL,
	`MiddleName` VARCHAR(85) NOT NULL,
	`LastName` VARCHAR(85) NOT NULL,	
	`SpouseName` VARCHAR(85) NOT NULL,
	
	`BirthDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
	`SpouseBirthDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
	`AnniversaryDate` DATE NOT NULL DEFAULT '0001-01-01 12:00:00',
	
	`Address1` VARCHAR (150) NULL ,
	`Address2` VARCHAR (150) NULL ,
	`City` VARCHAR (30) NULL ,
	`State` VARCHAR (30) NULL ,
	`ZipCode` VARCHAR (15) NULL ,
	`CountryID` TINYINT NOT NULL DEFAULT 0,

	`BusinessphoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`HomephoneNo` VARCHAR(75) NOT NULL DEFAULT '',
	`MobileNo` VARCHAR(75) NOT NULL DEFAULT '',
	`FaxNo` VARCHAR(75) NOT NULL DEFAULT '',

	`EmailAddress` VARCHAR(85) NOT NULL,
INDEX `IX_tblContactAddOn`(`ContactID`)
);

DELETE FROM sysConfig WHERE ConfigName = 'BACKEND_VARIATION_TYPE';
INSERT INTO sysConfig (ConfigName, ConfigValue, Category) VALUES ('BACKEND_VARIATION_TYPE',	'EXPIRATION;LOTNO', 'BACKEND_VARIATION_TYPE');


INSERT INTO tblVariations (VariationID, VariationCode, VariationType) VALUES (6, 'LOT', 'LOTNO');
