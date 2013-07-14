SELECT prd.SupplierID, prd.ProductID, IFNULL(inv.Quantity,0) Quantity, IFNULL(inv.ActualQuantity,0) ActualQuantity, prd.ProductCode, prd.ProductDesc, IFNULL(mtrx.Description,'') AS MatrixDescription, prd.BaseUnitID, unt.UnitCode, prd.MinThreshold, prd.MaxThreshold, pkg.PurchasePrice 
								FROM tblProducts prd
								INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
																AND prd.BaseUnitID = pkg.UnitID
																AND pkg.Quantity = 1 
								LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
								LEFT OUTER JOIN (
									SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE BranchID=1 GROUP BY ProductID, MatrixID
								) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
								WHERE IFNULL(inv.Quantity,0) <> IFNULL(inv.ActualQuantity,0)
									AND prd.SupplierID = 2565; 