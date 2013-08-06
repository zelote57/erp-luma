 SELECT prd.ProductID, prd.SupplierID, cntct.ContactCode, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubgroupID = prd.ProductSubgroupID
								INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=1 GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE prdsg.ProductGroupID = 7 AND prd.Deleted = 0 AND prd.Active = 1
								ORDER BY prd.ProductCode, MatrixDescription;