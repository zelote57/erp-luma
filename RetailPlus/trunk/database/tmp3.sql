



-- check how many rows per partition
SELECT PARTITION_NAME, SUBPARTITION_NAME, TABLE_ROWS FROM INFORMATION_SCHEMA.PARTITIONS WHERE TABLE_NAME = 'tblTransactions' AND TABLE_SCHEMA = 'posbooze';

-- check create tabke 
SHOW CREATE TABLE tblTransactions;

-- create a table with the same schema as the original
CREATE TABLE e2 LIKE tblTransactions;


-- check indexes
SHOW INDEX FROM tblTransactions;


-- backup table
CREATE DATABASE posback;
CREATE TABLE IF NOT EXISTS posback.bktblTransactions LIKE tblTransactions;
CREATE TABLE IF NOT EXISTS posback.bktblTransactionItems LIKE tblTransactionItems;

INSERT INTO posback.bktblTransactions SELECT * FROM tblTransactions WHERE YEAR(TransactionDate) <= '2013';
INSERT INTO posback.bktblTransactionItems SELECT a.* FROM tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID WHERE YEAR(TransactionDate) <= '2013';

SELECT COUNT(*) FROM tblTransactions WHERE YEAR(TransactionDate) <= '2013';
SELECT COUNT(*) FROM posback.bktblTransactions WHERE YEAR(TransactionDate) <= '2013';

DELETE a FROM tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID WHERE YEAR(TransactionDate) <= '2013';
DELETE FROM tblTransactions WHERE YEAR(TransactionDate) <= '2013';

SELECT PARTITION_NAME, SUBPARTITION_NAME, TABLE_ROWS FROM INFORMATION_SCHEMA.PARTITIONS WHERE TABLE_NAME = 'tblTransactions' AND TABLE_SCHEMA = 'pos';
SELECT PARTITION_NAME, SUBPARTITION_NAME, TABLE_ROWS FROM INFORMATION_SCHEMA.PARTITIONS WHERE TABLE_NAME = 'bktblTransactions' AND TABLE_SCHEMA = 'posback';

