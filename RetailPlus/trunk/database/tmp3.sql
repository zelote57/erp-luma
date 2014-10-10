SELECT prd.ProductID, prd.ProductCode, prd.ProductDesc, prd.ProductSubGroupID, prd.BaseUnitID, prd.DateCreated, prd.PercentageCommision, prd.IncludeInSubtotalDiscount                 
	,prd.SupplierID, prd.RewardPoints, prd.MinThreshold, prd.MaxThreshold                 ,pkg.PackageID, pkg.MatrixID ,pkg.BarCode1, pkg.BarCode2, pkg.BarCode3, pkg.BarCode4, pkg.Price, pkg.WSPrice, pkg.PurchasePrice, pkg.VAT, pkg.EVAT, pkg.LocalTax               
	FROM tblProducts prd INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID AND prd.BaseUnitID = pkg.UnitID AND pkg.Quantity = 1  
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID               
	LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0) AND INV.BranchID=1 
	WHERE prd.Active = 1 AND prd.deleted = 0                       
	AND (Barcode1 LIKE '%' OR Barcode2 LIKE '%' OR BarCode3 LIKE '%' OR BarCode4 LIKE '%' OR prd.ProductCode LIKE '%' OR prd.ProductDesc LIKE '%')
	AND IFNULL(mtrx.deleted, prd.deleted) = 0 AND inv.Quantity > 0 
	 LIMIT 100;


	 /****
SELECT prd.PackageID, prd.ProductID, prd.MatrixID, prd.ProductCode, prd.ProductDesc, prd.ProductSubGroupID, prd.BaseUnitID, prd.DateCreated, prd.PercentageCommision, prd.IncludeInSubtotalDiscount, prd.SupplierID, prd.RewardPoints, prd.MinThreshold, prd.MaxThreshold, prd.BarCode1, prd.BarCode2, prd.BarCode3, IFNULL(prd.BarCode1,prd.BarCode4) BarCode, prd.Price, prd.WSPrice, prd.PurchasePrice, prd.VAT, prd.EVAT, prd.LocalTax, SUM(IFNULL(inv.Quantity,0)) - SUM(IFNULL(inv.ReservedQuantity,0)) Quantity, MAX(IFNULL(inv.IsLock,0)) IsLock, SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity, IFNULL(mtrx.Description,'') MatrixDescription         
FROM (
	SELECT prd.ProductID, prd.ProductCode, prd.ProductDesc, prd.ProductSubGroupID, prd.BaseUnitID, prd.DateCreated, prd.PercentageCommision, prd.IncludeInSubtotalDiscount                 ,prd.SupplierID, prd.RewardPoints, prd.MinThreshold, prd.MaxThreshold                 ,pkg.PackageID, pkg.MatrixID ,pkg.BarCode1, pkg.BarCode2, pkg.BarCode3, pkg.BarCode4, pkg.Price, pkg.WSPrice, pkg.PurchasePrice, pkg.VAT, pkg.EVAT, pkg.LocalTax               
	FROM tblProducts prd INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID AND prd.BaseUnitID = pkg.UnitID AND pkg.Quantity = 1                     
	WHERE prd.Active = 1 AND prd.deleted = 0                       
	AND (Barcode1 LIKE '%' OR Barcode2 LIKE '%' OR BarCode3 LIKE '%' OR BarCode4 LIKE '%' OR prd.ProductCode LIKE '%' OR prd.ProductDesc LIKE '%')               LIMIT 100) prd      
LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND prd.MatrixID = mtrx.MatrixID            
LEFT OUTER JOIN tblBranch brnch ON brnch.BranchID=1          
LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0) AND INV.BranchID=1 
-- WHERE IFNULL(mtrx.deleted, 0) = 0 AND inv.Quantity > 0 
GROUP BY prd.PackageID, prd.ProductID, prd.MatrixID, prd.ProductCode, prd.ProductDesc, prd.ProductSubGroupID, prd.BaseUnitID, prd.DateCreated, prd.PercentageCommision, prd.IncludeInSubtotalDiscount, prd.SupplierID, prd.RewardPoints, prd.MinThreshold, prd.MaxThreshold, prd.BarCode1, prd.BarCode2, prd.BarCode3, IFNULL(prd.BarCode1,prd.BarCode4), prd.Price, prd.WSPrice, prd.PurchasePrice, prd.VAT, prd.EVAT, prd.LocalTax,IFNULL(mtrx.Description,'') 
ORDER BY ProductCode ASC LIMIT 100;
