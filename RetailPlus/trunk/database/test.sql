


/**************************************************************

	procProductMainDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductMainDetails();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMainDetails
GO

create procedure procProductMainDetails(
			 IN ProductID bigint,
			 IN BarCode varchar(60))
BEGIN
	SET @SQL = '		SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prdg.OrderSlipPrinter

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,prd.RewardPoints

							,IFNULL(inv.Quantity,0) Quantity
							,IF(ISNULL(inv.Quantity),0, fnProductQuantityConvert(prd.ProductID, inv.Quantity, prd.BaseUnitID))  ConvertedQuantity
							,IFNULL(inv.QuantityIN,0) QuantityIN
							,IFNULL(inv.QuantityOUT,0) QuantityOUT
							,IFNULL(inv.ActualQuantity,0) ActualQuantity

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID

							,prd.MaxThreshold - IFNULL(inv.Quantity,0) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  IFNULL(inv.Quantity,0) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,0 MatrixID
							,NULL MatrixDescription
						FROM tblProducts prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 AND pkg.MatrixID = 0
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN (
							SELECT ProductID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory GROUP BY ProductID
						) inv ON inv.ProductID = prd.ProductID 
						WHERE 1=1 ';
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 = ''',BarCode,''') ');
	END IF;
	
	SET @SQL = CONCAT(@SQL, 'LIMIT 1 ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;












