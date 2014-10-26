

UPDATE tblProductPackage SET Price = 150 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET Price = 150 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE');
UPDATE tblProductPackage SET Price = 50  WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE');
UPDATE tblProductPackage SET Price = 150 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET Price = 150 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE');
UPDATE tblProductPackage SET Price = 50  WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE');
UPDATE tblProductPackage SET Price = 250 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET Price = 200 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE');
UPDATE tblProductPackage SET Price = 200 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE');


-- always run this anytime an update is done to reset tax for all products
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - RENEWAL FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'ADVNTGE CARD - REPLACEMENT FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - RENEWAL FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT CARD - REPLACEMENT FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - MEMBERSHIP FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - RENEWAL FEE');
UPDATE tblProductPackage SET VAT = 0, EVAT = 0, LocalTax = 0 WHERE ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'SUPER CARD - REPLACEMENT FEE');

