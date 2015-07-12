

SELECT

FROM tblTransactionItems items 
WHERE ProductGroup = ''



SELECT items.ProductCode, items.Description, trx.DateClosed, trx.CustomerName, trxRet.DateClosed ReturnDate, trx.CashierName ReleasedBy, trxret.CashierName ReturnTo, items.ItemRemarks ReleaseRemarks, itemsret.ItemRemarks ReturnRemarks 
FROM tblTransactionItems items 
INNER JOIN tblTransactionItems itemsret ON items.RefReturnTransactionItemsID = itemsret.TransactionItemsID AND itemsret.TransactionItemStatus = 3 
INNER JOIN tblTransactions trx ON items.TransactionID = trx.TransactionID 
INNER JOIN tblTransactions trxret ON itemsret.TransactionID = trxret.TransactionID WHERE items.TransactionItemStatus = 7;

SELECT PromoBySupplierItemsID, a.SupplierID, ContactName, a.ProductGroupID, ProductGroupName, a.ProductSubGroupID, ProductSubGroupName,
a.ProductID, ProductDesc, a.VariationMatrixID, Description, a.PromoBySupplierValue, a.CouponRemarks
FROM tblPromoBySupplierItems a 
LEFT OUTER JOIN tblContacts b ON a.SupplierID = b.ContactID 
LEFT OUTER JOIN tblProductGroup c ON a.ProductGroupID = c.ProductGroupID 
LEFT OUTER JOIN tblProductSubGroup d ON a.ProductSubGroupID = d.ProductSubGroupID 
LEFT OUTER JOIN tblProducts e ON a.ProductID = e.ProductID 
LEFT OUTER JOIN tblProductBaseVariationsMatrix f ON a.VariationMatrixID = f.MatrixID 
WHERE PromoBySupplierID = 1 ORDER BY PromoBySupplierItemsID ASC;
