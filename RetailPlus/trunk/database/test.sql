--/**************************************************************

--	procProductPriceHistorySelect
--	Lemuel E. Aceron
--	March 22, 2013
	
--	Desc: This will get the main product list

--	CALL procProductPriceHistorySelect(null, null, 3057, null, null);
	
--**************************************************************/
--delimiter GO
--DROP PROCEDURE IF EXISTS procProductPriceHistorySelect
--GO

--create procedure procProductPriceHistorySelect(
--			 IN StartChangeDate datetime,
--			 IN EndChangeDate datetime,
--			 IN ProductID bigint,
--			 IN SortField varchar(60),
--			 IN SortOrder varchar(4))
--BEGIN
--	SET @SQL = CONCAT('	SELECT 
--							 hst.PackageID
--							,pkg.ProductID
--							,pkg.MatrixID
--							,prd.ProductCode
--							,IF(ISNULL(mtrx.Description), prd.ProductDesc, CONCAT(prd.ProductDesc, '':'' , mtrx.Description)) AS Description 
--							,pkg.UnitID
--							,unt.UnitCode
--							,unt.UnitName
--							,hst.ChangeDate
--							,pkg.Quantity
--							,hst.PurchasePriceBefore
--							,hst.PurchasePriceNow
--							,hst.SellingPriceBefore
--							,hst.SellingPriceNow
--							,hst.VATBefore
--							,hst.VATNow
--							,hst.EVATBefore
--							,hst.EVATNow
--							,hst.LocalTaxBefore
--							,hst.LocalTaxNow
--							,hst.Remarks
--							,usr.name
--						FROM tblProductPackagePriceHistory hst
--						INNER JOIN tblProductPackage pkg ON hst.PackageID = pkg.PackageID
--						INNER JOIN tblProducts prd ON prd.ProductID = pkg.ProductID
--						INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
--						INNER JOIN sysAccessUserDetails usr ON usr.UID = hst.UID
--						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.MatrixID = mtrx.MatrixID AND pkg.ProductID = mtrx.ProductID 
--						WHERE 1=1 ');

--	IF IFNULL(StartChangeDate,'') <> '' THEN
--		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate >= ''',StartChangeDate,''' ');
--	END IF;

--	IF IFNULL(EndChangeDate,'') <> '' THEN
--		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate <= ''',EndChangeDate,''' ');
--	END IF;

--	IF ProductID <> 0 THEN
--		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
--	END IF;

--	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','hst.ChangeDate',SortField),' ',IFNULL(SortOrder,'DESC'),' ');

--	PREPARE cmd FROM @SQL;
--	EXECUTE cmd;
--	DEALLOCATE PREPARE cmd;

--END;
--GO
--delimiter ;