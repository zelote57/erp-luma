


-- check all indexes in a certain table in case mabagal
SELECT DISTINCT 
	TABLE_NAME, INDEX_NAME, COLUMN_NAME, Cardinality
FROM INFORMATION_SCHEMA.STATISTICS
	WHERE TABLE_SCHEMA = 'poshp' AND TABLE_NAME = 'tblTransactionItems';

-- drop table index in case not needed
ALTER TABLE tblTransactionItems DROP INDEX IX_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX0_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX1_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX2_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX3_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX4_tblTransactionItems01;
ALTER TABLE tblTransactionItems DROP INDEX IX_tblTransactionItems_IXSync;

-- optimize or repair a table is the same as rebuild index. do this after adding or deleting any index
OPTIMIZE TABLE tblTransactionItems;
REPAIR TABLE tblTransactionItems;

-- check the table names that contains column names. Use this if me mga momodify na column names
SELECT DISTINCT 
	TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA='poshp' AND COLUMN_NAME IN ('ProductDesc','ProductDescription');
		
