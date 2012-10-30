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
)
TYPE=INNODB COMMENT = 'Agents Commision Report';

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

/*********************************  v_2.0.1.1.sql END  *******************************************************/    