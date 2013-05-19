
SELECT tblProducts.ProductCode, tblProductPackage.BarCode4 AS BarCode, tblProductPackage.BarCode1, tblProducts.ProductDesc, tblProductSubGroup.ProductSubGroupCode, tblProductSubGroup.ProductGroupID, tblProductGroup.ProductGroupCode, tblProducts.BaseUnitID, tblUnit.UnitCode 'BaseUnitCode', tblProducts.Active, tblProductPackage.Price, tblProducts.PurchasePrice, tblProducts.IncludeInSubtotalDiscount, tblProducts.VAT, tblProducts.EVAT, tblProducts.LocalTax, tblProductInventory.Quantity AS Quantity, fnProductQuantityConvert(tblProducts.ProductID, tblProductInventory.Quantity, tblProducts.BaseUnitID) AS ConvertedQuantity, tblProducts.SupplierID, tblContacts.ContactName AS SupplierName, tblProducts.ProductID 
FROM tblProducts 
INNER JOIN tblProductSubGroup ON tblProducts.ProductSubGroupID = tblProductSubGroup.ProductSubGroupID 
INNER JOIN tblProductGroup ON tblProductSubGroup.ProductGroupID = tblProductGroup.ProductGroupID 
INNER JOIN tblUnit ON tblProducts.BaseUnitID = tblUnit.UnitID 
INNER JOIN tblProductInventory ON tblProducts.ProductID = tblProductInventory.ProductID 
INNER JOIN tblProductPackage ON tblProducts.ProductID = tblProductPackage.ProductID 
INNER JOIN tblUnit tblUnitPackage ON tblProductPackage.UnitID = tblUnitPackage.UnitID 
INNER JOIN tblContacts ON tblProducts.SupplierID = tblContacts.ContactID 
LIMIT 10;