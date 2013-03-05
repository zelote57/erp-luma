
/**************************************************************
	procGenerateProductHistoryToProductMovement
	Lemuel E. Aceron
	CALL procGenerateProductHistoryToProductMovement();
	
	Mar 6, 2013 - Save all previous history of products to tblProductMovement
**************************************************************/

ALTER TABLE tblProductMovement MODIFY MatrixDescription VARCHAR(100);
ALTER TABLE tblProductMovement MODIFY Remarks VARCHAR(150);

delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistoryToProductMovement
GO

create procedure procGenerateProductHistoryToProductMovement()
BEGIN
	DECLARE dteStartTransactionDate DateTime;
	DECLARE dteEndTransactionDate DateTime;

	SET dteStartTransactionDate = '0001-01-01';
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, (SELECT DATE_ADD(MIN(transactiondate), INTERVAL -1 MINUTE) FROM tblProductMovement));
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, dteStartTransactionDate);

	SELECT MIN(transactiondate) AS 'tblProductMovement END Date', 
			dteEndTransactionDate AS EndTransactionDateToProcess 
	FROM tblProductMovement;

	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, b.ProductCode, COALESCE(c.Description,''), a.VariationMatrixID, IFNULL(c.Description, b.ProductCode) 'MatrixDescription', 0, CASE StockDirection
																																						WHEN 0 THEN a.Quantity
																																						WHEN 1 THEN -a.Quantity
																																					END AS Quantity, 0, 0,
									d.UnitCode, a.Remarks, a.StockDate, TransactionNo, 'SYSTEM AUTO-G', BranchID, BranchID, 1 'QuantityMovementType'
	FROM (((tblStockItems a
		INNER JOIN tblStock f ON a.StockID = f.StockID
		LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
		LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
		LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
		LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT ProductID, ProductCode, COALESCE(Description,''), VariationsMatrixID, MatrixDescription, 0, CASE TransactionItemStatus
																											WHEN 0 THEN -Quantity
																											WHEN 1 THEN 0
																											WHEN 2 THEN 0
																											WHEN 3 THEN Quantity
																											WHEN 4 THEN -Quantity
																										END AS Quantity, 0 'QuantityTo', 0 'MatrixQuantity',  
									ProductUnitCode, CASE TransactionItemStatus
														WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
														WHEN 1 THEN 'Void'
														WHEN 2 THEN 'Trash'
														WHEN 3 THEN 'Return'
														WHEN 4 THEN 'Refund'
													END AS Remarks, TransactionDate, TransactionNo,
									CashierName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Purchase Order from ',SupplierCode) AS Remarks, b.PODate AS TransactionDate, b.PONo AS TransactionNo,
									PurchaserName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, ProductCode, COALESCE(Description,''), a.VariationMatrixID, MatrixDescription, QuantityBefore, (QuantityNow - QuantityBefore) AS Quantity, QuantityNow, 0 'MatrixQuantity',
									a.UnitCode, CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks, InvAdjustmentDate AS TransactionDate,
									CONCAT('InvAdjID:' , a.InvAdjustmentID) AS TransactionNo, b.Name, 1, 1, 1 'QuantityMovementType'
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer In from ',SupplierCode) AS Remarks, b.TransferInDate AS TransactionDate, b.TransferInNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer out to ',SupplierCode) AS Remarks, b.TransferOutDate AS TransactionDate, b.TransferOutNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

END;
GO
delimiter ;