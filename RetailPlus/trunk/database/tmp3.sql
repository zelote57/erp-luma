/**************************************************************

	procZeroOutInventory
	Lemuel E. Aceron
	March 14, 2009

	CALL procZeroOutInventory(1, 1, '2015-01-11', '00010', 0, '');

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutInventory
GO

create procedure procZeroOutInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngContactID bigint,
									IN strContactCode varchar(150))
BEGIN
	
	DECLARE lngProductID, lngMatrixID, lngSupplierID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(18,3) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strSupplierCode VARCHAR(150) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(250) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT prd.ProductID, prd.SupplierID, cntct.ContactCode, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								INNER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;
								
								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
									
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								INNER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.Deleted = 0 AND prd.Active = 1;

								-- AND IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
	SET strRemarks = CONCAT('SYSTEM AUTO ADJUSTMENT-ZERO OUT INVENTORY BranchID:', intBranchID);

	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO lngProductID, lngSupplierID, strSupplierCode, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, lngMatrixID, strMatrixDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: Insert to tblInventory
		INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngSupplierID, strSupplierCode, 
									lngProductID, strProductCode, lngMatrixID, strMatrixDescription,
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
					
		-- STEP 2: Insert to product movement history
		CALL procProductMovementInsert(lngProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);
		
		-- STEP 3: Insert to inventory adjustment
		INSERT INTO tblInvAdjustment(UID, InvAdjustmentDate, ProductID, ProductCode, Description, 
							VariationMatrixID, MatrixDescription, UnitID, UnitCode, 
							QuantityBefore, QuantityNow, MinThresholdBefore, MinThresholdNow, MaxThresholdBefore, MaxThresholdNow, Remarks)VALUES
								(pvtUID, pvtInvAdjustmentDate, pvtProductID, pvtProductCode, pvtDescription,
							pvtVariationMatrixID, pvtMatrixDescription, pvtUnitID, pvtUnitCode, 
							pvtQuantityBefore, pvtQuantityNow, pvtMinThresholdBefore, pvtMinThresholdNow, pvtMaxThresholdBefore, pvtMaxThresholdNow, pvtRemarks);
		SELECT lngUID, dteClosingDate, prd.ProductID, prd.ProductCode, prd.ProductDesc, 
							IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, 
							IFNULL(inv.Quantity,0) Quantity, 0, prd.MinThreshold, prd.MinThreshold AS MinThresholdNow, prd.MaxThreshold prd.MaxThresholdNow, CONCAT(strRemarks, ' ', strReferenceNo)
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								INNER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, 0 ActualQuantity FROM tblProductInventory WHERE BranchID=intBranchID GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;

		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, lngProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 4: auto adjust the quantity based on actual quantity
		UPDATE tblProductInventory SET Quantity = decProductActualQuantity WHERE BranchID = intBranchID AND ProductID = lngProductID AND MatrixID = lngMatrixID;
		
		
		UPDATE tblProductInventory SET QuantityIN = 0 WHERE BranchID = intBranchID AND ProductID = lngProductID AND MatrixID = lngMatrixID;
		UPDATE tblProductInventory SET QuantityOUT = 0 WHERE BranchID = intBranchID AND ProductID = lngProductID AND MatrixID = lngMatrixID;

		SET lngProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	

END;
GO
delimiter ;