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