/**************************************************************

	procProductIsExist

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procProductIsExist(4, 'ADVNTGE CARD - REPLACEMENT FEE');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductIsExist
GO

create procedure procProductIsExist(
									IN lngProductID BIGINT, 
									IN strBarCode VARCHAR(30))
BEGIN
	SET @strSQL := '';
	
	SET @strSQL := 'SELECT
						Count(1) ProductCount
					FROM tblProductPackage ';
	
	IF (lngProductID <> 0) THEN
		SET @strSQL = CONCAT(@strSQL,' WHERE ProductID <> ', lngProductID,' AND ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	ELSEIF (lngProductID = 0) THEN
		SET @strSQL = CONCAT(@strSQL,' WHERE ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	END IF;
	
	PREPARE stmt FROM @strSQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;