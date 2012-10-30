/**************************************************************

	procSyncQuantityProductHistory
	Lemuel E. Aceron
    March 14, 2009

	CALL procSyncQuantityProductHistory();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistory
GO

create procedure procSyncQuantityProductHistory()
BEGIN
	declare strSessionID varchar(30);
	set strSessionID = '1';
	
	DROP TABLE IF EXISTS tblProductHistoryAll;
	
	CREATE TABLE tblProductHistoryAll (
		`SessionID` VARCHAR(30) NOT NULL,
		`HistoryID` BIGINT NOT NULL DEFAULT 0,
		`ProductID` BIGINT NOT NULL DEFAULT 0,
		`MatrixID` BIGINT NOT NULL DEFAULT 0,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
		`UnitCode` VARCHAR(100) NOT NULL,
		`Remarks` VARCHAR(100) NOT NULL,
		`TransactionDate` DateTime NOT NULL,
		`TransactionNo` VARCHAR(100) NOT NULL,
		INDEX `IX_tblProductHistory`(`SessionID`),
		INDEX `IX_tblProductHistory1`(`MatrixDescription`)
	)
	TYPE=INNODB COMMENT = 'Product History Report';

	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID;
	
	SELECT 'Inserting StockItems';
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems01 a INNER JOIN tblTransactions01 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems02 a INNER JOIN tblTransactions02 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems03 a INNER JOIN tblTransactions03 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems04 a INNER JOIN tblTransactions04 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems05 a INNER JOIN tblTransactions05 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems06 a INNER JOIN tblTransactions06 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems07 a INNER JOIN tblTransactions07 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems08 a INNER JOIN tblTransactions08 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems09 a INNER JOIN tblTransactions09 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems10 a INNER JOIN tblTransactions10 b ON a.TransactionID = b.TransactionID;
		
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems11 a INNER JOIN tblTransactions11 b ON a.TransactionID = b.TransactionID;
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
        IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems12 a INNER JOIN tblTransactions12 b ON a.TransactionID = b.TransactionID;
	
	UPDATE tblProductBaseVariationsMatrix SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.MatrixID = tblProductBaseVariationsMatrix.MatrixID);
	
	UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.ProductID = tblProducts.ProductID);

	SELECT 'DONE Synching';
		
END;
GO
delimiter ;
 