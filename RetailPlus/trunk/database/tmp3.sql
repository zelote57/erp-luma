
/**************************************************************

	procProductMainDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductMainDetails(1, 57, 168 '','', false);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMainDetails
GO

create procedure procProductMainDetails(
			 IN BranchID int,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(60),
			 IN ProductCode varchar(60),
			 IN isQuantityGreaterThanZERO TINYINT(1))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	
	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 = ''',BarCode,''') ');
	ELSEIF IFNULL(ProductCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode = ''',ProductCode,''' ');
	END IF;
	IF MatrixID <> 0 AND MatrixID <> -1 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND pkg.MatrixID = ',MatrixID,' ');
	END IF;

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.PackageID
							,IFNULL(prd.BarCode1,prd.BarCode4) BarCode
							,prd.BarCode1
							,prd.BarCode2
							,prd.BarCode3
							,prd.BarCode4
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
							,prd.Price
							,prd.WSPrice
							,prd.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,prd.VAT
							,prd.EVAT
							,prd.LocalTax
							,prd.RewardPoints

							,SUM(IFNULL(inv.Quantity,0)) Quantity
                            ,fnProductQuantityConvert(prd.ProductID, SUM(IFNULL(inv.Quantity,0)), prd.BaseUnitID)  ConvertedQuantity
                            ,SUM(IFNULL(inv.ReservedQuantity,0)) ReservedQuantity
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
                            ,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
                            ,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
							,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold ');
	IF MatrixID <> -1 THEN
		SET @SQL = CONCAT(@SQL, '	
							,IFNULL(mtrx.MatrixID,0) MatrixID
							,IFNULL(CONCAT(prd.ProductDesc, '':'' , mtrx.Description),'''') AS VariationDesc ');
	ELSE
		SET @SQL = CONCAT(@SQL, '	
							,0 MatrixID
							,MAX(IFNULL(CONCAT(prd.ProductDesc, '':'' , mtrx.Description),'''')) AS VariationDesc ');
	END IF;
	SET @SQL = CONCAT(@SQL, '	
							,IFNULL(mtrx.Description,'''') MatrixDescription
						FROM (SELECT prd.*
									,pkg.PackageID
									,pkg.MatrixID
									,pkg.BarCode1
									,pkg.BarCode2
									,pkg.BarCode3
									,pkg.BarCode4
									
									,pkg.Price
									,pkg.WSPrice
									,pkg.PurchasePrice
									,pkg.VAT
									,pkg.EVAT
									,pkg.LocalTax
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID
														-- AND pkg.Quantity = 1 ');
	IF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, '
														AND prd.BaseUnitID = pkg.UnitID
						');
	END IF;

	-- put the exempted products
	IF  IFNULL(BarCode,'') = 'CREDIT PAYMENT' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'ADVNTGE CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'CREDIT CARD - REPLACEMENT FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - MEMBERSHIP FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - RENEWAL FEE' OR 
		IFNULL(BarCode,'') = 'SUPER CARD - REPLACEMENT FEE' THEN
		SET @SQL = CONCAT(@SQL, ' WHERE 1=1 ');	
	ELSE
		SET @SQL = CONCAT(@SQL, ' WHERE deleted=0 ');	
	END IF;

	SET @SQL = CONCAT(@SQL, '
							  ',SQLWhere,' ORDER BY Quantity ASC LIMIT 1) prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON prd.productID = mtrx.ProductID AND prd.MatrixID =  mtrx.MatrixID
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID =  inv.MatrixID ',IF(BranchID=0,'',Concat('AND inv.BranchID=',BranchID)),' ', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						');

	SET @SQL = CONCAT(@SQL, '
						GROUP BY 
							prd.ProductID
						   ,prd.PackageID
						   ,prd.BarCode1
						   ,prd.BarCode2
						   ,prd.BarCode3
						   ,prd.BarCode4
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
						   ,unt.UnitCode
						   ,unt.UnitName
						   ,prd.BaseUnitID
						   ,unt.UnitCode
						   ,unt.UnitName

						   ,prd.DateCreated
						   ,prd.Active
						   ,prd.Deleted

						   ,prd.SupplierID
						   ,supp.ContactCode
						   ,supp.ContactName

						   ,prd.IsItemSold
						   ,prd.Price
						   ,prd.WSPrice
						   ,prd.PurchasePrice
						   ,prd.PercentageCommision
						   ,prd.IncludeInSubtotalDiscount
						   ,prd.VAT
						   ,prd.EVAT
						   ,prd.LocalTax
						   ,prd.RewardPoints

						   ,prd.WillPrintProductComposition

						   ,prd.MinThreshold
						   ,prd.MaxThreshold
						   ,prd.RID

						   ,prd.MaxThreshold
						   ,prd.RIDMinThreshold
						   ,prd.RIDMaxThreshold
						   ,prd.RIDMaxThreshold

						   ,prd.ChartOfAccountIDPurchase
						   ,prd.ChartOfAccountIDSold
						   ,prd.ChartOfAccountIDInventory
						   ,prd.ChartOfAccountIDTaxPurchase
						   ,prd.ChartOfAccountIDTaxSold ');

	IF MatrixID <> -1 THEN
		SET @SQL = CONCAT(@SQL, '	
						   ,mtrx.MatrixID
						   ,mtrx.Description ');
	END IF;

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;
	
END;
GO
delimiter ;
