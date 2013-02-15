select quantity from tblproducts where productid = 13;
select quantity from tblproductbasevariationsmatrix where productid = 13;
select quantity, tblbranchInventory.* from tblbranchInventory where productid = 13;
select quantity, tblbranchInventoryMatrix.* from tblbranchInventoryMatrix where productid = 13;

delimiter GO
DROP PROCEDURE IF EXISTS procProductAddQuantity
GO

create procedure procProductAddQuantity(
	IN intBranchID INT(4),
	IN lngProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity, decMatrixQuantity DECIMAL(18,3) DEFAULT 0;
	
	/*********** add to main ***********/	
	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT Description, Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID = lngMatrixID AND ProductID = lngProductID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, Quantity, UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 0);
										
	-- Add the quantity to Product table
	UPDATE tblProducts SET 
		Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity
	WHERE ProductID = lngProductID;
	
	-- Add the quantity to Matrix table
	UPDATE tblProductBaseVariationsMatrix SET 
		Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity 
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = lngProductID;
	/*********** end add to main ***********/	

	-- Set the value of strMatrixDescription, decMatrixQuantity
	SELECT a.Description, b.Quantity INTO strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID WHERE Deleted = 0 AND a.MatrixID = lngMatrixID AND a.ProductID = lngProductID AND BranchID = intBranchID;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT a.ProductCode, a.ProductDesc, c.Quantity, b.UnitCode INTO strProductCode, strProductDesc, decProductQuantity, strUnitCode FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN tblBranchInventory c ON a.ProductID = c.ProductID WHERE a.ProductID = lngProductID AND BranchID = intBranchID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, decMatrixQuantity, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 1);
									
	-- Add the quantity to BranchInventory table
	IF EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = lngProductID AND BranchID = intBranchID) THEN
		UPDATE tblBranchInventory SET 
			Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity
		WHERE ProductID = lngProductID 
			AND BranchID = intBranchID;
	ELSE
		INSERT INTO tblBranchInventory (BranchID , ProductID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, decQuantity, decQuantity);
	END IF;
	
	-- Add the quantity to Matrix table
	IF EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID ) THEN
		UPDATE tblBranchInventoryMatrix SET 
			Quantity	= Quantity + decQuantity, QuantityIN	= QuantityIN + decQuantity 
		WHERE MatrixID	= lngMatrixID  
			AND ProductID = lngProductID 
			AND BranchID = intBranchID;
	ELSEIF lngMatrixID <> 0 THEN
		INSERT INTO tblBranchInventoryMatrix (BranchID , ProductID, MatrixID, Quantity, QuantityIN) VALUES (intBranchID, lngProductID, lngMatrixID, decQuantity, decQuantity);
	END IF;
	
	-- Tag product as Active if quantity > 0
	IF (SELECT Quantity FROM tblProducts WHERE ProductID = lngProductID) > 0 THEN
		CALL procProductTagActiveInactive(lngProductID, 1);
	END IF;
	
	-- Process sync of product that are sold without matrix but with existing matrix now
	CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
END;
GO
delimiter ;