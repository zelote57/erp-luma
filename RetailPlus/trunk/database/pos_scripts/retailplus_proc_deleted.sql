-- =============================================
-- Script Template
-- =============================================

/*********************************
	procDepartmentInsert
	Lemuel E. Aceron
	CALL procDepartmentInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentInsert
GO

create procedure procDepartmentInsert(
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	INSERT INTO tblDepartments(DepartmentCode, DepartmentName)
	VALUES (pvtDepartmentCode, pvtDepartmentName);
		
END;
GO
delimiter ;


/*********************************
	procDepartmentUpdate
	Lemuel E. Aceron
	CALL procDepartmentUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentUpdate
GO

create procedure procDepartmentUpdate(
	IN pvtDepartmentID INT(10),
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	UPDATE tblDepartments SET 
		DepartmentCode	= pvtDepartmentCode, 
		DepartmentName	= pvtDepartmentName
	WHERE DepartmentID	= pvtDepartmentID;
		
END;
GO
delimiter ;


/********************************************
	procSaveParkingRate
	
	CALL procSaveParkingRate(0, 5448, 'Monday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Tuesday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Wednesday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Thursday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Friday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Saturday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Sunday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');

	Jul 21, 2013 : Lem
	- create this procedure
	
	Aug 17, 2014 : Lem
	- replaced with procSaveParkingRate
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSaveParkingRate
GO
delimiter ;


create procedure procSaveParkingRate(
	IN lngParkingRateID BIGINT,
	IN lngProductID BIGINT,
	IN strDayOfWeek VARCHAR(9),
	IN strStartTime VARCHAR(5),
	IN strEndtime VARCHAR(5),
	IN intNoOfUnitPerMin INT,
	IN decPerUnitPrice DECIMAL(18,3),
	IN intMinimumStayInMin INT,
	IN decMinimumStayPrice DECIMAL(18,3),
	IN strCreatedByName VARCHAR(100)
	)
BEGIN
	
	IF EXISTS(SELECT ProductID FROM tblParkingRates WHERE ParkingRateID = lngParkingRateID) THEN 
		UPDATE tblParkingRates SET
			DayOfWeek			= strDayOfWeek,
			StartTime			= strStartTime,
			EndTime				= strEndTime,
			NoofUnitPerMin		= intNoOfUnitPerMin,
			PerUnitPrice		= decPerUnitPrice,
			MinimumStayInMin	= intMinimumStayInMin,
			MinimumStayPrice	= decMinimumStayPrice,
			LastUpdatedByName	= strCreatedByName,
			LastUpdatedOn		= NOW()
		WHERE ParkingRateID		= lngParkingRateID;
	ELSE
		INSERT INTO tblParkingRates(ProductID, DayOfWeek, StartTime, EndTime, NoOfUnitperMin, PerUnitPrice, MinimumStayInMin, MinimumStayPrice, CreatedByName, CreatedOn)
			VALUES(lngProductID, strDayOfWeek, strStartTime, strEndTime, intNoOfUnitperMin, decPerUnitPrice, intMinimumStayInMin, decMinimumStayPrice, strCreatedByName, NOW());
	END IF;
				
END;
GO
delimiter ;
