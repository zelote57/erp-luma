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
