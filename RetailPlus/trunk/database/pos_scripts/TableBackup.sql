
ALTER TABLE tblTransactionsBackup ADD `isConsignment` tinyint(1) unsigned NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD DataSource VARCHAR(30) NULL;
ALTER TABLE tblTransactionsBackup MODIFY DiscountCode VARCHAR(60);
ALTER TABLE tblTransactionsBackup MODIFY ChargeCode VARCHAR(60);
ALTER TABLE tblTransactionsBackup ADD CustomerGroupName VARCHAR(60) NULL;
ALTER TABLE tblTransactionsBackup ADD `ORNo` VARCHAR(30);
ALTER TABLE tblTransactionsBackup ADD `SyncID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD `ZeroRatedVAT` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for ZeroRated';
ALTER TABLE tblTransactionsBackup ADD `NonVATableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for NonVAT';
ALTER TABLE tblTransactionsBackup ADD `VATExempt` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for SNR';
ALTER TABLE tblTransactionsBackup ADD `NonEVATableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD `SNRDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD `PWDDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD `OtherDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionsBackup ADD `NetSales` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Net Sales = Amount Due = VAT Exempt - SNRDisc = Subtotal - Not SNRDisc';
ALTER TABLE tblTransactionsBackup ADD `ChargeType` INT(1) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactionItemsBackup ADD `TransactionDiscount` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'This is an applied discount computed evenly from transaction discount';

ALTER TABLE tblTransactionItemsBackup ADD DataSource VARCHAR(30) NULL;
ALTER TABLE tblTransactionItemsBackup ADD `SyncID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItemsBackup ADD `ZeroRatedVAT` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for ZeroRated';
ALTER TABLE tblTransactionItemsBackup ADD `NonVATableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for NonVAT';
ALTER TABLE tblTransactionItemsBackup ADD `VATExempt` DECIMAL(18,3) NOT NULL DEFAULT 0 COMMENT 'Use for SNR';
ALTER TABLE tblTransactionItemsBackup ADD `NonEVATableAmount` DECIMAL(18,3) NOT NULL DEFAULT 0;

