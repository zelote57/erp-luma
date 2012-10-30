
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