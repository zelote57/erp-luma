
/**************************************************************

	procProductInventorySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the all product package list

	CALL procProductInventorySelect(1, 0,0,'','MEFUROX 125MG/5ML',4,0,0,2,0,0,1,'1900-01-01',null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductInventorySelect
GO

create procedure procProductInventorySelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN ProductGroupID bigint,
			 IN ProductSubGroupID bigint,
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN isSummary int,
			 IN dteExpiration datetime,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	DECLARE SQLWhere VARCHAR(8000) DEFAULT '';

	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	IF ProductID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET SQLWhere = CONCAT(SQLWhere, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET SQLWhere = CONCAT(SQLWhere, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	/***
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET SQLWhere = CONCAT(SQLWhere,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
	END IF;
	***/

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
							,SUM(IFNULL(inv.QuantityIN,0)) QuantityIN
							,SUM(IFNULL(inv.QuantityOUT,0)) QuantityOUT
							,SUM(IFNULL(inv.ActualQuantity,0)) ActualQuantity
                            ,IFNULL(MAX(inv.IsLock),0) IsLock

							,prd.WillPrintProductComposition

							,IFNULL(mtrx.MinThreshold, prd.MinThreshold) MinThreshold
							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) MaxThreshold
							,prd.RID

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - SUM(IFNULL(inv.Quantity,0)) ReorderQty
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold
                            ,prd.RIDMaxThreshold -  SUM(IFNULL(inv.Quantity,0)) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,IFNULL(mtrx.MatrixID,0) MatrixID
							,CONCAT(prd.ProductDesc, '':'' , IFNULL(mtrx.Description,'''')) AS VariationDesc
							,IFNULL(mtrx.Description,'''') AS MatrixDescription
							,',IF(isSummary=1,'0','IFNULL(brnch.BranchID,0)'),' AS BranchID
							,',IF(isSummary=1,'''All''','IFNULL(brnch.BranchCode,''All'')'),' AS BranchCode
						FROM (SELECT prd.* ,pkg.PackageID, pkg.MatrixID
									,pkg.BarCode1 ,pkg.BarCode2 ,pkg.BarCode3 ,pkg.BarCode4
									,pkg.Price ,pkg.WSPrice ,pkg.PurchasePrice ,pkg.VAT ,pkg.EVAT ,pkg.LocalTax
							  FROM tblProducts prd 
							  INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1
							  WHERE prd.deleted = 0 ',SQLWhere,' ',IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')),') prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID ', IF(ProductSubGroupID=0,'',CONCAT('AND prdsg.ProductSubGroupID =',ProductSubGroupID)),'
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID ', IF(ProductGroupID=0,'',CONCAT('AND prdg.ProductGroupID =',ProductGroupID)),'
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND prd.MatrixID = mtrx.MatrixID
						LEFT OUTER JOIN tblBranch brnch ON ',IF(BranchID=0,'1=1',Concat('brnch.BranchID=',BranchID)),'						
						LEFT OUTER JOIN tblProductInventory inv ON inv.ProductID = prd.ProductID AND prd.MatrixID = inv.MatrixID 
														',IF(BranchID=0,'AND brnch.BranchID = INV.BranchID ',Concat('AND INV.BranchID=',BranchID)),' 
														', IF(isQuantityGreaterThanZERO=0,'','AND inv.Quantity > 0 '),'
						WHERE IFNULL(mtrx.deleted, 0) = 0 ');
	
	IF isSummary = 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(mtrx.MatrixID,0) = ',MatrixID,' ');
	END IF;

	-- check only those with Quantity
	IF (DATE_FORMAT(dteExpiration, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND prd.ProductID IN (SELECT DISTINCT mtrx.ProductID FROM tblProductVariationsMatrix prdmtrx INNER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.MatrixID = prdmtrx.MatrixID AND prdmtrx.VariationID = 1 WHERE DATE_FORMAT(prdmtrx.Description, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteExpiration,''', ''%Y-%m-%d'')) ');
		SET @SQL = CONCAT(@SQL,'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;
	
	SET @SQL = CONCAT(@SQL, '
						GROUP BY prd.ProductID
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

                            ,mtrx.MinThreshold, prd.MinThreshold
                            ,mtrx.MaxThreshold, prd.MaxThreshold
                            ,prd.RID
                            ,prd.RIDMinThreshold
                            ,prd.RIDMaxThreshold

                            ,prd.ChartOfAccountIDPurchase
                            ,prd.ChartOfAccountIDSold
                            ,prd.ChartOfAccountIDInventory
                            ,prd.ChartOfAccountIDTaxPurchase
                            ,prd.ChartOfAccountIDTaxSold

                            ,mtrx.MatrixID
                            ,mtrx.Description
                            ',IF(isSummary=1,'',',IFNULL(brnch.BranchID,0)'),'
							',IF(isSummary=1,'',',IFNULL(brnch.BranchCode,''All'')'),' ');

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode, MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;
