SELECT spi.ProductGroup,spi.ProductID,spi.ProductCode,cntct.ContactCode SupplierCode,SUM(spi.Quantity) 'Quantity'
			,SUM(spi.Amount) 'Amount',SUM(spi.PurchaseAmount) 'PurchaseAmount', SUM(spi.Discount) 'Discount'
			, MIN(spi.PurchasePrice) 'PurchasePrice', MAX(spi.InvQuantity) 'InvQuantity'
			, IFNULL(MIN(ppph.PurchasePrice),0) 'PurchasePrice2', IFNULL(cntct2.ContactCode,'''') SupplierCode2 
			FROM tblSalesPerItem spi INNER JOIN tblProducts prd ON spi.ProductID = prd.ProductID 
			INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID 
			LEFT OUTER JOIN tblProductPurchasePriceHistory ppph ON prd.ProductID = ppph.ProductID 
							AND ppph.SupplierID <> prd.SupplierID AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) 
			LEFT OUTER JOIN tblContacts cntct2 ON ppph.SupplierID = cntct2.ContactID 
			WHERE spi.SessionID = 1
			GROUP BY spi.ProductGroup, spi.ProductID, spi.ProductCode, cntct.ContactCode, IFNULL(cntct2.ContactCode,'''') ORDER BY ProductCode;

			/**

delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemByGroup
GO

create procedure procGenerateSalesPerItemByGroup(
	IN strSessionID varchar(30),
	IN strProductGroup varchar(50),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET strProductGroup = IF(NOT ISNULL(strProductGroup), CONCAT('%',strProductGroup,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductID, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount, Discount, PurchasePrice, InvQuantity)
	SELECT strSessionID, ProductGroup,
		a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode) ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount,
		SUM(IF(TransactionItemStatus=3,-a.Discount + -a.TransactionDiscount,IF(TransactionItemStatus=4,-a.Discount + -a.TransactionDiscount,a.Discount + a.TransactionDiscount))) Discount,
		IFNULL(MIN(pkg.PurchasePrice), a.PurchasePrice) PurchasePrice,
		IFNULL(MAX(inv.InvQuantity),0) InvQuantity
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	LEFT OUTER JOIN tblProductPackage pkg ON a.ProductID = pkg.ProductID AND pkg.Quantity = 1 AND a.ProductPackageID = pkg.PackageID
	LEFT OUTER JOIN (
		SELECT ProductID, SUM(Quantity) InvQuantity FROM tblProductInventory
		GROUP BY ProductID
	) inv ON a.ProductID = inv.ProductID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY ProductGroup, a.ProductID, IF(MatrixDescription <> NULL, MatrixDescription, ProductCode), ProductUnitCode;

END;
GO
delimiter ;

/*****
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '09', '2015-01-31 19:00');

CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2015-01-31 19:00');

CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(2, '02', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(3, '03', '2015-01-31 19:00');


CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(4, '04', '2015-01-31 19:00');


CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(5, '05', '2015-01-31 19:00');



CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(6, '06', '2015-01-31 19:00');



CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(7, '07', '2015-01-31 19:00');



CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(8, '08', '2015-01-31 19:00');



CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');
CALL procTerminalReportHistorySyncTransactionSales(9, '09', '2015-01-31 19:00');


