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