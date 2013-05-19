

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

--- check if all products are already in the inventory, should all be zero result
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
ALTER TABLE tblProductBaseVariationsMatrix DROP MinThreshold;
ALTER TABLE tblProductBaseVariationsMatrix DROP MaxThreshold;

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

ALTER TABLE tblTransactions ADD TransactionType INT(1) NOT NULL DEFAULT 0;










