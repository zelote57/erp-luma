delimiter GO
DROP PROCEDURE IF EXISTS procProductVaritionMatrixSelect
GO

create procedure procProductVaritionMatrixSelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN MatrixDescription varchar(60),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
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

							,mtrx.MatrixID
							,CONCAT(prd.ProductDesc, '':'' , mtrx.Description) AS VariationDesc
							,mtrx.Description AS MatrixDescription
						FROM tblProductBaseVariationsMatrix mtrx 
						INNER JOIN tblProducts prd ON mtrx.ProductID = prd.ProductID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 AND pkg.MatrixID = mtrx.MatrixID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN (
							SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID, MatrixID
						) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = mtrx.MatrixID
						WHERE mtrx.deleted = 0 ');
	
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF IFNULL(MatrixDescription,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(mtrx.Description,'''') LIKE ''%',MatrixDescription,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF isQuantityGreaterThanZERO <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;