SET FOREIGN_KEY_CHECKS = 0;

/*!40000 ALTER TABLE `tblProducts` DISABLE KEYS */;

INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES ('CUS', 'Default Customer Group', 1);
INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES ('SUP', 'Default Supplier Group', 2);

INSERT INTO tblContacts (ContactCode, ContactName, ContactGroupID, ModeOfTerms, Address, BusinessName, TelephoneNo, Remarks, DateCreated) VALUES
('RC', 'RetailPlus Customer ™', 1, 0, 'RBS', 'RBS', '', '', NOW());
INSERT INTO tblContacts (ContactCode, ContactName, ContactGroupID, ModeOfTerms, Address, BusinessName, TelephoneNo, Remarks, DateCreated) VALUES
('RS', 'RetailPlus Supplier ™', 2, 0, 'RBS', 'RBS', '', '', NOW());

DELETE FROM tblContactRewards WHERE CustomerID = 1;
INSERT INTO tblContactRewards VALUES(1, '', 1, 0, NOW());

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

DELETE FROM tblProductInventory WHERE ProductID = 1;  
DELETE FROM tblProductPackage WHERE PackageID = 1 or (ProductID = 1 AND UnitID=1);
DELETE FROM tblProducts WHERE ProductID = 1;  
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (1, 'CREDIT PAYMENT','CREDIT PAYMENT',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 1, 'CREDIT PAYMENT', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT';
INSERT INTO tblProductInventory(BranchID, ProductID, Quantity)VALUES(1, 1,999999999);
UPDATE tblProductInventory SET Quantity = 999999999 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT');


DELETE FROM tblProductPackage WHERE PackageID = 2;
DELETE FROM tblProducts WHERE ProductID = 2;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (2, 'ADVNTGE CARD - MEMBERSHIP FEE','ADVNTGE CARD - MEMBERSHIP FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 2, 'ADVNTGE CARD - MEMBERSHIP FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 3;
DELETE FROM tblProducts WHERE ProductID = 3;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (3, 'ADVNTGE CARD - RENEWAL FEE','ADVNTGE CARD - RENEWAL FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 3, 'ADVNTGE CARD - RENEWAL FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 4;
DELETE FROM tblProducts WHERE ProductID = 4;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (4, 'ADVNTGE CARD - REPLACEMENT FEE','ADVNTGE CARD - REPLACEMENT FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 4, 'ADVNTGE CARD - REPLACEMENT FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE';

DELETE FROM tblProductPackage WHERE PackageID = 5;
DELETE FROM tblProducts WHERE ProductID = 5;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (5, 'CREDIT CARD - MEMBERSHIP FEE','CREDIT CARD - MEMBERSHIP FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 5, 'CREDIT CARD - MEMBERSHIP FEE', ProductID, BaseUnitID, 300, 0, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 6;
DELETE FROM tblProducts WHERE ProductID = 6;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (6, 'CREDIT CARD - RENEWAL FEE','CREDIT CARD - RENEWAL FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 6, 'CREDIT CARD - RENEWAL FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 7;
DELETE FROM tblProducts WHERE ProductID = 7;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (7, 'CREDIT CARD - REPLACEMENT FEE','CREDIT CARD - REPLACEMENT FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 7, 'CREDIT CARD - REPLACEMENT FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE';

DELETE FROM tblProductPackage WHERE PackageID = 8;
DELETE FROM tblProducts WHERE ProductID = 8;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (8, 'SUPER CARD - MEMBERSHIP FEE','SUPER CARD - MEMBERSHIP FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 8, 'SUPER CARD - MEMBERSHIP FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE';

DELETE FROM tblProductPackage WHERE PackageID = 9;
DELETE FROM tblProducts WHERE ProductID = 9;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (9, 'SUPER CARD - RENEWAL FEE','SUPER CARD - RENEWAL FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 9, 'SUPER CARD - RENEWAL FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE';

DELETE FROM tblProductPackage WHERE PackageID = 10;
DELETE FROM tblProducts WHERE ProductID = 10;
INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES (10, 'SUPER CARD - REPLACEMENT FEE','SUPER CARD - REPLACEMENT FEE',1,1,2,1);
INSERT INTO tblProductPackage (PackageID, Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 10, 'SUPER CARD - REPLACEMENT FEE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE';

DELETE FROM tblContactRewards WHERE CustomerID = 1;
INSERT INTO tblContactRewards (CustomerID, RewardCardNo, RewardActive, RewardCardStatus, RewardAwardDate, ExpiryDate, BirthDate) 
							VALUES(1, '', 1, 0, NOW(), DATE_ADD(NOW(), INTERVAL 200 YEAR), NOW());

-- make sure that this is only 1
INSERT INTO tblProducts( ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES ('CCI LATE PAYMENT CHARGE','CCI LATE PAYMENT CHARGE',1,1,2,1);
INSERT INTO tblProductPackage (Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 'CCI LATE PAYMENT CHARGE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'CCI LATE PAYMENT CHARGE';
INSERT INTO tblProductInventory(BranchID, ProductID, Quantity) SELECT 1, ProductID, 999999999 FROM tblProducts WHERE ProductCode = 'CCI LATE PAYMENT CHARGE';
SELECT * FROM tblProducts WHERE ProductCode = 'CCI LATE PAYMENT CHARGE';

-- make sure that this is only 1
INSERT INTO tblProducts( ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, SupplierID, Deleted) VALUES ('CCI FINANCE CHARGE','CCI FINANCE CHARGE',1,1,2,1);
INSERT INTO tblProductPackage (Barcode1, ProductID, UnitID, Price, PurchasePrice, Quantity)   SELECT 'CCI FINANCE CHARGE', ProductID, BaseUnitID, 0, 0, 1 FROM tblProducts WHERE ProductCode = 'CCI FINANCE CHARGE';
INSERT INTO tblProductInventory(BranchID, ProductID, Quantity) SELECT 1, ProductID, 999999999 FROM tblProducts WHERE ProductCode = 'CCI FINANCE CHARGE';
SELECT * FROM tblProducts WHERE ProductCode = 'CCI FINANCE CHARGE';

/*!40000 ALTER TABLE `tblProducts` ENABLE KEYS */;
SET FOREIGN_KEY_CHECKS = 1;