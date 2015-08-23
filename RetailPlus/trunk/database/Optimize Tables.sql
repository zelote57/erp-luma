-- run this scripts to create the script for optimizing
SELECT CONCAT('OPTIMIZE TABLE ', TABLE_NAME, ';') FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA IN ('pos', 'posaudit') AND TABLE_NAME NOT LIKE 'xxx_%' AND TABLE_NAME NOT LIKE 'deleted_%';

-- run this scripts to create the script for repairing
SELECT CONCAT('REPAIR TABLE ', TABLE_NAME, ';') FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA IN ('pos', 'posaudit') AND TABLE_NAME NOT LIKE 'xxx_%' AND TABLE_NAME NOT LIKE 'deleted_%';



-- check all indexes in a certain table in case mabagal
SELECT DISTINCT TABLE_NAME, INDEX_NAME, COLUMN_NAME, Cardinality FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA = 'pos' AND TABLE_NAME = 'tblTransactions';

-- drop table index in case not needed
ALTER TABLE tblTransactionItems DROP INDEX IX_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX0_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX1_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX2_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX3_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX4_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX_tblTransactionItems_IXSync;

-- create index
CREATE INDEX IX0_tblTransactionItems ON tblTransactionItems (TransactionID, ProductID);
CREATE INDEX IX1_tblTransactionItems ON tblTransactionItems (TransactionID, ProductID, VariationsMatrixID);
CREATE INDEX IX2_tblTransactionItems ON tblTransactionItems (ProductCode);
CREATE INDEX IX3_tblTransactionItems ON tblTransactionItems (TransactionID);
CREATE INDEX IX4_tblTransactionItems ON tblTransactionItems (ProductUnitID);

-- optimize or repair a table is the same as rebuild index. do this after adding or deleting any index
OPTIMIZE TABLE tblTransactionItems;
REPAIR TABLE tblTransactionItems;

OPTIMIZE TABLE tblTransactions;

-- check the table names that contains column names. Use this if me mga momodify na column names
SELECT DISTINCT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='poshp' AND COLUMN_NAME IN ('ProductDesc','ProductDescription');
		



