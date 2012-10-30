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
    