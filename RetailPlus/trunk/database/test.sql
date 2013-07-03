SELECT a.ProductID, a.ProductCode, pkg.PackageID, IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode, pkg.BarCode1, pkg.BarCode2, pkg.BarCode3, a.ProductDesc, a.ProductSubGroupID, b.ProductSubGroupCode, b.ProductSubGroupName, b.ProductGroupID, c.ProductGroupCode, c.ProductGroupName, a.BaseUnitID, d.UnitID, d.UnitCode 'BaseUnitCode', d.UnitName 'BaseUnitName', d.UnitCode, d.UnitName, a.DateCreated, a.Deleted, a.Active, pkg.Price, pkg.WSPrice, pkg.PurchasePrice, a.PercentageCommision, a.IncludeInSubtotalDiscount, pkg.VAT, pkg.EVAT, pkg.LocalTax, IFNULL(inv.Quantity,0) Quantity, fnProductQuantityConvert(a.ProductID, IFNULL(inv.Quantity,0), a.BaseUnitID) AS ConvertedQuantity, a.MinThreshold, a.MaxThreshold, a.RID, a.SupplierID, e.ContactCode AS SupplierCode, e.ContactName AS SupplierName, c.OrderSlipPrinter, a.ChartOfAccountIDPurchase, a.ChartOfAccountIDSold, a.ChartOfAccountIDInventory, a.ChartOfAccountIDTaxPurchase, a.ChartOfAccountIDTaxSold, a.IsItemSold, a.WillPrintProductComposition, a.VariationCount, IFNULL(inv.QuantityIN,0) QuantityIN, IFNULL(inv.QuantityOUT,0) QuantityOUT, IFNULL(inv.ActualQuantity,0) ActualQuantity, a.MaxThreshold - IFNULL(inv.Quantity,0) AS ReorderQty, a.RIDMinThreshold, a.RIDMaxThreshold, a.RIDMaxThreshold - IFNULL(inv.Quantity,0) AS RIDReorderQty, a.RewardPoints, IFNULL(inv.MatrixID,0) MatrixID, IFNULL(mtrx.Description,'') MatrixDescription FROM tblProducts a    INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID    INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID    INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID    INNER JOIN tblContacts e ON a.SupplierID = e.ContactID    INNER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID    
LEFT OUTER JOIN tblProductInventory inv ON a.ProductID =  inv.ProductID AND pkg.MatrixID = inv.MatrixID    
LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON a.ProductID = mtrx.ProductID AND mtrx.MatrixID = inv.MatrixID 
WHERE 1=1 AND IFNULL(inv.BranchID,1) = 1 AND a.ProductId = 5445 AND IFNULL(inv.MatrixID,0) = 0

/***

SELECT barcode1, IFNULL(inv.BranchID,1), a.*
FROM tblProducts a    INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID    
INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID    INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID    
INNER JOIN tblContacts e ON a.SupplierID = e.ContactID    INNER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID    
LEFT OUTER JOIN tblProductInventory inv ON a.ProductID =  inv.ProductID AND pkg.MatrixID = inv.MatrixID    
LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON a.ProductID = mtrx.ProductID AND mtrx.MatrixID = inv.MatrixID 
WHERE 1=1 AND IFNULL(inv.BranchID,1) = 1 AND (BarCode1 = 'LOST TICKET''''S' OR BarCode2 = 'LOST TICKET''''S') 
AND IFNULL(inv.Quantity,0) >= 0 ORDER BY mtrx.Description ASC LIMIT 1


FROM tblProducts a    INNER JOIN tblProductSubGroup b ON a.ProductSubGroupID = b.ProductSubGroupID    
INNER JOIN tblProductGroup c ON b.ProductGroupID = c.ProductGroupID    INNER JOIN tblUnit d ON a.BaseUnitID = d.UnitID    
INNER JOIN tblContacts e ON a.SupplierID = e.ContactID    INNER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID    
LEFT OUTER JOIN tblProductInventory inv ON a.ProductID =  inv.ProductID AND pkg.MatrixID = inv.MatrixID    
LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON a.ProductID = mtrx.ProductID AND mtrx.MatrixID = inv.MatrixID 
WHERE 1=1 AND IFNULL(inv.BranchID,1) = 1 AND (BarCode1 = 'LOST TICKET''''S')
AND IFNULL(inv.Quantity,0) >= 0
ORDER BY mtrx.Description ASC LIMIT 4


**/