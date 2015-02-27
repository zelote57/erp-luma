-- =============================================
-- New Saving procedures
-- =============================================



/********************************************
	procSaveSysConfig
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysConfig
GO

create procedure procSaveSysConfig(	
	IN strConfigName VARCHAR(100),
	IN strConfigValue VARCHAR(150),
	IN strCategory VARCHAR(100),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ConfigName FROM sysConfig WHERE ConfigName = strConfigName) THEN 
		UPDATE sysConfig SET
			ConfigValue			= strConfigValue,
			Category			= strCategory,
			LastModified		= dteLastModified
		WHERE ConfigName		= strConfigName;
	ELSE
		INSERT INTO sysConfig(Configname, ConfigValue, Category, CreatedOn, LastModified)
			VALUES(strConfigname, strConfigValue, strCategory, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysCreditConfig
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysCreditConfig
GO

create procedure procSaveSysCreditConfig(	
	IN strConfigName VARCHAR(30),
	IN strConfigValue VARCHAR(100),
	IN strRemarks VARCHAR(150),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ConfigName FROM SysCreditConfig WHERE ConfigName = strConfigName) THEN 
		UPDATE SysCreditConfig SET
			ConfigValue			= strConfigValue,
			Remarks				= strRemarks,
			LastModified		= dteLastModified
		WHERE ConfigName		= strConfigName;
	ELSE
		INSERT INTO SysCreditConfig(Configname, ConfigValue, Remarks, CreatedOn, LastModified)
			VALUES(strConfigname, strConfigValue, strRemarks, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysTerminalKey
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysTerminalKey
GO

create procedure procSaveSysTerminalKey(	
	IN strHDSerialNo VARCHAR(30),
	IN strRegistrationKey VARCHAR(255),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT HDSerialNo FROM SysTerminalKey WHERE HDSerialNo = strHDSerialNo) THEN 
		UPDATE SysTerminalKey SET
			HDSerialNo			= strHDSerialNo,
			RegistrationKey		= strRegistrationKey,
			LastModified		= dteLastModified
		WHERE HDSerialNo		= strHDSerialNo;
	ELSE
		INSERT INTO SysTerminalKey(HDSerialNo, RegistrationKey, CreatedOn, LastModified)
			VALUES(strHDSerialNo, strRegistrationKey, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysAccessTypes
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessTypes
GO

create procedure procSaveSysAccessTypes(	
	IN intTypeID INT(10),
	IN strTypeName VARCHAR(80),
	IN strRemarks VARCHAR(120),
	IN boEnabled TINYINT(1),
	IN intSequenceNo INT(10),
	IN strCategory VARCHAR(50),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT TypeID FROM SysAccessTypes WHERE TypeID = intTypeID) THEN 
		UPDATE SysAccessTypes SET
			TypeName			= strTypeName,
			Remarks				= strRemarks,
			Enabled			= boEnabled,
			SequenceNo			= intSequenceNo,
			Category			= strCategory,
			LastModified		= dteLastModified
		WHERE TypeID			= intTypeID;
	ELSE
		INSERT INTO SysAccessTypes(TypeID, TypeName, Remarks, Enabled, SequenceNo, Category, CreatedOn, LastModified)
			VALUES(intTypeID, strTypeName, strRemarks, boEnabled, intSequenceNo, strCategory, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysAccessGroups
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessGroups
GO

create procedure procSaveSysAccessGroups(	
	IN intGroupID INT(10),
	IN strGroupName VARCHAR(20),
	IN strRemarks VARCHAR(200),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT GroupID FROM SysAccessGroups WHERE GroupID = intGroupID) THEN 
		UPDATE SysAccessGroups SET
			GroupName			= strGroupName,
			Remarks				= strRemarks,
			LastModified		= dteLastModified
		WHERE GroupID			= intGroupID;
	ELSE
		INSERT INTO SysAccessGroups(GroupID, GroupName, Remarks, CreatedOn, LastModified)
			VALUES(intGroupID, strGroupName, strRemarks, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysAccessUsers
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessUsers
GO

create procedure procSaveSysAccessUsers(	
	IN intUID BIGINT(20),
	IN strUserName VARCHAR(25),
	IN strPassword VARCHAR(25),
	IN dteDateCreated DATETIME,
	IN boDeleted TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT UID FROM SysAccessUsers WHERE UID = intUID) THEN 
		UPDATE SysAccessUsers SET
			UserName			= strUserName,
			Password			= strPassword,
			DateCreated			= dteDateCreated,
			Deleted				= boDeleted,
			LastModified		= dteLastModified
		WHERE UID				= intUID;
	ELSE
		INSERT INTO SysAccessUsers(UID, UserName, Password, DateCreated, Deleted, CreatedOn, LastModified)
			VALUES(intUID, strUserName, strPassword, dteDateCreated, boDeleted, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysAccessUserDetails
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessUserDetails
GO

create procedure procSaveSysAccessUserDetails(	
	IN intUID BIGINT(20),
	IN strName VARCHAR(25),
	IN strAddress1 VARCHAR(25),
	IN strAddress2 VARCHAR(25),
	IN strCity VARCHAR(30),
	IN strState VARCHAR(30),
	IN strZip VARCHAR(15),
	IN intCountryID TINYINT(4),
	IN strOfficePhone VARCHAR(150),
	IN strDirectPhone VARCHAR(150),
	IN strHomePhone VARCHAR(150),
	IN strFaxPhone VARCHAR(150),
	IN strMobilePhone VARCHAR(150),
	IN strEmailAddress VARCHAR(150),
	IN intGroupID INT(10),
	IN intPageSize INT(5),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT UID FROM SysAccessUserDetails WHERE UID = intUID) THEN 
		UPDATE SysAccessUserDetails SET
			Name				= strName,
			Address1			= strAddress1,
			Address2			= strAddress2,
			City				= strCity,
			State				= strState,
			Zip					= strZip,
			CountryID			= intCountryID,
			OfficePhone			= strOfficePhone,
			DirectPhone			= strDirectPhone,
			HomePhone			= strHomePhone,
			FaxPhone			= strFaxPhone,
			MobilePhone			= strMobilePhone,
			EmailAddress		= strEmailAddress,
			GroupID				= intGroupID,
			PageSize			= intPageSize,
			LastModified		= dteLastModified
		WHERE UID				= intUID;
	ELSE
		INSERT INTO SysAccessUserDetails(UID, Name, Address1, Address2, City, State, Zip, CountryID,
										 OfficePhone, DirectPhone, HomePhone, FaxPhone, MobilePhone,
										 EmailAddress, GroupID, PageSize, CreatedOn, LastModified)
			VALUES(intUID, strName, strAddress1, strAddress2, strCity, strState, strZip, intCountryID,
										strOfficePhone, strDirectPhone, strHomePhone, strFaxPhone, strMobilePhone,
										strEmailAddress, intGroupID, intPageSize, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;




/********************************************
	procSaveSysAccessGroupRights
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessGroupRights
GO

create procedure procSaveSysAccessGroupRights(	
	IN intGroupID INT(10),
	IN intTranTypeID INT(10),
	IN boAllowRead TINYINT(1),
	IN boAllowWrite TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT GroupID FROM SysAccessGroupRights WHERE GroupID = intGroupID AND TranTypeID = intTranTypeID) THEN 
		UPDATE SysAccessGroupRights SET
			AllowRead			= boAllowRead,
			AllowWrite			= boAllowWrite,
			LastModified		= dteLastModified
		WHERE intGroupID		= intGroupID
			AND TranTypeID		= intTranTypeID;
	ELSE
		INSERT INTO SysAccessGroupRights(GroupID, TranTypeID, AllowRead, AllowWrite, CreatedOn, LastModified)
			VALUES(intGroupID, intTranTypeID, boAllowRead, boAllowWrite, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSysAccessRights
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSysAccessRights
GO

create procedure procSaveSysAccessRights(	
	IN intUID BIGINT(20),
	IN intTranTypeID INT(10),
	IN boAllowRead TINYINT(1),
	IN boAllowWrite TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT UID FROM SysAccessRights WHERE UID = intUID AND TranTypeID = intTranTypeID) THEN 
		UPDATE SysAccessRights SET
			AllowRead			= boAllowRead,
			AllowWrite			= boAllowWrite,
			LastModified		= dteLastModified
		WHERE UID				= intUID
			AND TranTypeID		= intTranTypeID;
	ELSE
		INSERT INTO SysAccessRights(UID, TranTypeID, AllowRead, AllowWrite, CreatedOn, LastModified)
			VALUES(intUID, intTranTypeID, boAllowRead, boAllowWrite, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveBranch
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveBranch
GO

create procedure procSaveBranch(	
	IN intBranchID INT(4),
	IN strBranchCode VARCHAR(30),
	IN strBranchName VARCHAR(50),
	IN strDBIP VARCHAR(20),
	IN strDBPort VARCHAR(4),
	IN strAddress VARCHAR(120),
	IN strRemarks VARCHAR(120),
	IN intIncludeIneSales TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT BranchID FROM tblBranch WHERE BranchID = intBranchID) THEN 
		UPDATE tblBranch SET
			BranchCode			= strBranchCode,
			BranchName			= strBranchName,
			DBIP				= strDBIP,
			DBPort				= strDBPort,
			Address				= strAddress,
			Remarks				= strRemarks,
			IncludeIneSales		= intIncludeIneSales,
			LastModified		= dteLastModified
		WHERE BranchID			= intBranchID;
	ELSE
		INSERT INTO tblBranch(BranchID, BranchCode, Branchname, DBIP, DBPort, Address, Remarks, IncludeIneSales, CreatedOn, LastModified)
			VALUES(intBranchID, strBranchCode, strBranchname, strDBIP, strDBPort, strAddress, strRemarks, intIncludeIneSales, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCalDate
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCalDate
GO

create procedure procSaveCalDate(	
	IN intCalDateID BIGINT(20),
	IN dteCalDate DATETIME,
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT CalDateID FROM tblCalDate WHERE CalDateID = intCalDateID) THEN 
		UPDATE tblCalDate SET
			CalDate				= dteCaldate,
			LastModified		= dteLastModified
		WHERE CalDateID			= intCalDateID;
	ELSE
		INSERT INTO tblCalDate(CalDateID, CalDate, CreatedOn, LastModified)
			VALUES(intCalDateID, dteCaldate, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveERPConfig
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveERPConfig
GO

create procedure procSaveERPConfig(	
	IN intERPConfigID BIGINT(20),
	IN strLastPONo VARCHAR(10),
	IN strLastPOReturnNo VARCHAR(10),
	IN strLastDebitMemoNo VARCHAR(10),
	IN strLastSONo VARCHAR(10),
	IN strLastSOReturnNo VARCHAR(10),
	IN strLastCreditMemoNo VARCHAR(10),
	IN strLastTransferInNo VARCHAR(10),
	IN strLastTransferOutNo VARCHAR(10),
	IN strLastInvAdjustmentNo VARCHAR(10),
	IN strLastClosingNo VARCHAR(10),
	IN dtePostingDateFrom DATETIME,
	IN dtePostingDateTo DATETIME,
	IN intChartOfAccountIDAPTracking INT(4),
	IN intChartOfAccountIDAPBills INT(4),
	IN intChartOfAccountIDAPFreight INT(4),
	IN intChartOfAccountIDAPVDeposit INT(4),
	IN intChartOfAccountIDAPContra INT(4),
	IN intChartOfAccountIDAPLatePayment INT(4),

	IN intChartOfAccountIDARTracking INT(4),
	IN intChartOfAccountIDARBills INT(4),
	IN intChartOfAccountIDARFreight INT(4),
	IN intChartOfAccountIDARVDeposit INT(4),
	IN intChartOfAccountIDARContra INT(4),
	IN intChartOfAccountIDARLatePayment INT(4),

	IN strLastCreditCardNo VARCHAR(11),
	IN strLastRewardCardNo VARCHAR(11),
	IN strDBVersion VARCHAR(10),
	IN strDBVersionSales VARCHAR(10),
	IN strLastBranchTransferNo VARCHAR(10),
	IN strLastCustomerCode VARCHAR(15),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ERPConfigID FROM tblERPConfig WHERE ERPConfigID = intERPConfigID) THEN 
		UPDATE tblERPConfig SET
			LastPONo				= strLastPONo,
			LastPOReturnNo			= strLastPOReturnNo,
			LastDebitMemoNo			= strLastDebitMemoNo,
			LastSONo				= strLastSONo,
			LastSOReturnNo			= strLastSOReturnNo,
			LastCreditMemoNo		= strLastCreditMemoNo,
			LastTransferInNo		= strLastTransferInNo,
			LastTransferOutNo		= strLastTransferOutNo,
			LastInvAdjustmentNo		= strLastInvAdjustmentNo,
			LastClosingNo			= strLastClosingNo,
			PostingDateFrom			= dtePostingDateFrom,
			PostingDateTo			= dtePostingDateTo,
			ChartOfAccountIDAPTracking		= intChartOfAccountIDAPTracking,
			ChartOfAccountIDAPBills			= intChartOfAccountIDAPBills,
			ChartOfAccountIDAPFreight		= intChartOfAccountIDAPFreight,
			ChartOfAccountIDAPVDeposit		= intChartOfAccountIDAPVDeposit,
			ChartOfAccountIDAPContra		= intChartOfAccountIDAPContra,
			ChartOfAccountIDAPLatePayment	= intChartOfAccountIDAPLatePayment,
			ChartOfAccountIDARTracking		= intChartOfAccountIDARTracking,
			ChartOfAccountIDARBills			= intChartOfAccountIDARBills,
			ChartOfAccountIDARFreight		= intChartOfAccountIDARFreight,
			ChartOfAccountIDARVDeposit		= intChartOfAccountIDARVDeposit,
			ChartOfAccountIDARContra		= intChartOfAccountIDARContra,
			ChartOfAccountIDARLatePayment	= intChartOfAccountIDARLatePayment,
			LastCreditCardNo		= strLastCreditCardNo,
			LastRewardCardNo		= strLastRewardCardNo,
			DBVersion				= strDBVersion,
			DBVersionSales			= strDBVersionSales,
			LastBranchTransferNo	= strLastBranchTransferNo,
			LastCustomerCode		= strLastCustomerCode,
			LastModified			= dteLastModified
		WHERE ERPConfigID			= intERPConfigID;
	ELSE
		INSERT INTO tblERPConfig(ERPConfigID, LastPONo, LastPOReturnNo, LastDebitMemoNo, LastSONo,
								LastSOReturnNo, LastCreditMemoNo, LastTransferInNo, LastTransferOutNo,
								LastInvAdjustmentNo, LastClosingNo, PostingDateFrom, PostingDateTo,
								ChartOfAccountIDAPTracking, ChartOfAccountIDAPBills, ChartOfAccountIDAPFreight,
								ChartOfAccountIDAPVDeposit, ChartOfAccountIDAPContra, ChartOfAccountIDAPLatePayment,
								ChartOfAccountIDARTracking, ChartOfAccountIDARBills, ChartOfAccountIDARFreight,
								ChartOfAccountIDARVDeposit, ChartOfAccountIDARContra, ChartOfAccountIDARLatePayment,
								LastCreditCardNo, LastRewardCardNo, DBVersion, DBVersionSales, LastBranchTransferNo, 
								LastCustomerCode, CreatedOn, LastModified)
			VALUES(intERPConfigID, strLastPONo, strLastPOReturnNo, strLastDebitMemoNo, strLastSONo,
							strLastSOReturnNo, strLastCreditMemoNo, strLastTransferInNo, strLastTransferOutNo,
							strLastInvAdjustmentNo, strLastClosingNo, dtePostingDateFrom, dtePostingDateTo,
							intChartOfAccountIDAPTracking, intChartOfAccountIDAPBills, intChartOfAccountIDAPFreight,
							intChartOfAccountIDAPVDeposit, intChartOfAccountIDAPContra, intChartOfAccountIDAPLatePayment,
							intChartOfAccountIDARTracking, intChartOfAccountIDARBills, intChartOfAccountIDARFreight,
							intChartOfAccountIDARVDeposit, intChartOfAccountIDARContra, intChartOfAccountIDARLatePayment,
							strLastCreditCardNo, strLastRewardCardNo, strDBVersion, strDBVersionSales, strLastBranchTransferNo,
							strLastCustomerCode, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveTerminal
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveTerminal
GO

create procedure procSaveTerminal(	
	IN intTerminalID BIGINT(20),
	IN strTerminalNo VARCHAR(10),
	IN strTerminalCode VARCHAR(20),
	IN strTerminalName VARCHAR(50),
	IN intStatus TINYINT(1),
	IN dteDateCreated DATETIME,
	IN boIsPrinterAutoCutter TINYINT(1),
	IN intMaxReceiptWidth int(10),
	IN intTransactionNoLength int(2),
	IN intAutoPrint int(1),
	IN strPrinterName varchar(20),
	IN strTurretName varchar(20),
	IN strCashDrawerName varchar(20),
	IN strMachineSerialNo varchar(20),
	IN strAccreditationNo varchar(25),
	IN boItemVoidConfirmation tinyint(1),
	IN boEnableEVAT tinyint(1),
	IN strFORM_Behavior varchar(20),
	IN strMarqueeMessage varchar(255),
	IN decTrustFund decimal(5,2),
	IN boIsVATInclusive tinyint(1),
	IN intVAT int(2),
	IN intEVAT int(2),
	IN intLocalTax int(2),
	IN boShowItemMoreThanZeroQty tinyint(1),
	IN boShowOneTerminalSuspendedTransactions tinyint(1),
	IN boShowOnlyPackedTransactions tinyint(1),
	IN intTerminalReceiptType tinyint(1),
	IN strSalesInvoicePrinterName varchar(30),
	IN boCashCountBeforeReport tinyint(1),
	IN boPreviewTerminalReport tinyint(1),
	IN intOrderSlipPrinter tinyint(1),
	IN strDBVersion varchar(15),
	IN strFEVersion varchar(15),
	IN strBEVersion varchar(15),
	IN boIsPrinterDotmatrix tinyint(1),
	IN boIsChargeEditable tinyint(1),
	IN boIsDiscountEditable tinyint(1),
	IN boCheckCutOffTime tinyint(1),
	IN strStartCutOffTime varchar(5),
	IN strEndCutOffTime varchar(5),
	IN boWithRestaurantFeatures tinyint(1),
	IN strSeniorCitizenDiscountCode varchar(50),
	IN boIsTouchScreen tinyint(1),
	IN boWillContinueSelectionVariation tinyint(1),
	IN boWillContinueSelectionProduct tinyint(1),
	IN decRETPriceMarkUp decimal(18,2),
	IN decWSPriceMarkUp decimal(18,2),
	IN boWillPrintGrandTotal tinyint(1),
	IN boReservedAndCommit tinyint(1),
	IN boShowCustomerSelection tinyint(1),
	IN boEnableRewardPoints tinyint(1),
	IN decRewardPointsMinimum decimal(18,3),
	IN decRewardPointsEvery decimal(18,3),
	IN decRewardPoints decimal(18,3),
	IN boRoundDownRewardPoints tinyint(1),
	IN boAutoGenerateRewardCardNo tinyint(1),
	IN boEnableRewardPointsAsPayment tinyint(1),
	IN decRewardPointsMaxPercentageForPayment decimal(5,2),
	IN decRewardPointsPaymentValue decimal(18,3),
	IN decRewardPointsPaymentCashEquivalent decimal(18,3),
	IN strRewardsPermitNo varchar(30),
	IN boIsFineDining tinyint(1),
	IN intPersonalChargeTypeID int(10),
	IN intGroupChargeTypeID int(10),
	IN intBranchID int(4),
	IN intProductSearchType int(1),
	IN boIncludeCreditChargeAgreement tinyint(1),
	IN boIsParkingTerminal tinyint(1),
	IN boWillPrintChargeSlip tinyint(1),
	IN boWillPrintVoidItem tinyint(1),
	IN boIncludeTermsAndConditions tinyint(1),
	IN strPWDDiscountCode varchar(100),
	IN strDefaultTransactionChargeCode varchar(60),
	IN strDineInChargeCode varchar(60),
	IN strTakeOutChargeCode varchar(60),
	IN strDeliveryChargeCode varchar(60),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT TerminalID FROM tblTerminal WHERE TerminalID = intTerminalID) THEN 
		UPDATE tblTerminal SET
			TerminalNo				= strTerminalNo,
			TerminalCode			= strTerminalCode,
			TerminalName			= strTerminalName,
			Status					= intStatus,
			DateCreated				= dteDateCreated,
			IsPrinterAutoCutter		= boIsPrinterAutoCutter,
			MaxReceiptWidth			= intMaxReceiptWidth,
			TransactionNoLength		= intTransactionNoLength,
			AutoPrint				= intAutoPrint,
			PrinterName				= strPrinterName,
			TurretName				= strTurretName,
			CashDrawerName			= strCashDrawerName,
			MachineSerialNo			= strMachineSerialNo,
			AccreditationNo			= strAccreditationNo,
			ItemVoidConfirmation	= boItemVoidConfirmation,
			EnableEVAT				= boEnableEVAT,
			FORM_Behavior			= strFORM_Behavior,
			MarqueeMessage			= strMarqueeMessage,
			TrustFund				= decTrustFund,
			IsVATInclusive			= boIsVATInclusive,
			VAT						= intVAT,
			EVAT					= intEVAT,
			LocalTax				= intLocalTax,
			ShowItemMoreThanZeroQty	= boShowItemMoreThanZeroQty,
			ShowOneTerminalSuspendedTransactions	= boShowOneTerminalSuspendedTransactions,
			ShowOnlyPackedTransactions			= boShowOnlyPackedTransactions,
			TerminalReceiptType		= intTerminalReceiptType,
			SalesInvoicePrinterName	= strSalesInvoicePrinterName,
			CashCountBeforeReport	= boCashCountBeforeReport,
			PreviewTerminalReport	= boPreviewTerminalReport,
			OrderSlipPrinter		= intOrderSlipPrinter,
			DBVersion				= strDBVersion,
			FEVersion				= strFEVersion,
			BEVersion				= strBEVersion,
			IsPrinterDotmatrix		= boIsPrinterDotmatrix,
			IsChargeEditable		= boIsChargeEditable,
			IsDiscountEditable		= boIsDiscountEditable,
			CheckCutOffTime			= boCheckCutOffTime,
			StartCutOffTime			= strStartCutOffTime,
			EndCutOffTime			= strEndCutOffTime,
			WithRestaurantFeatures	= boWithRestaurantFeatures,
			SeniorCitizenDiscountCode	= strSeniorCitizenDiscountCode,
			IsTouchScreen			= boIsTouchScreen,
			WillContinueSelectionVariation	= boWillContinueSelectionVariation,
			WillContinueSelectionProduct	= boWillContinueSelectionProduct,
			RETPriceMarkUp			= decRETPriceMarkUp,
			WSPriceMarkUp			= decWSPriceMarkUp,
			WillPrintGrandTotal		= boWillPrintGrandTotal,
			ReservedAndCommit		= boReservedAndCommit,
			ShowCustomerSelection	= boShowCustomerSelection,
			EnableRewardPoints		= boEnableRewardPoints,
			RewardPointsMinimum		= decRewardPointsMinimum,
			RewardPointsEvery		= decRewardPointsEvery,
			RewardPoints			= decRewardPoints,
			RoundDownRewardPoints	= boRoundDownRewardPoints,
			AutoGenerateRewardCardNo			= boAutoGenerateRewardCardNo,
			EnableRewardPointsAsPayment			= boEnableRewardPointsAsPayment,
			RewardPointsMaxPercentageForPayment	= decRewardPointsMaxPercentageForPayment,
			RewardPointsPaymentValue			= decRewardPointsPaymentValue,
			RewardPointsPaymentCashEquivalent	= decRewardPointsPaymentCashEquivalent,
			RewardsPermitNo						= strRewardsPermitNo,
			IsFineDining						= boIsFineDining,
			PersonalChargeTypeID				= intPersonalChargeTypeID,
			GroupChargeTypeID					= intGroupChargeTypeID,
			BranchID							= intBranchID,
			ProductSearchType					= intProductSearchType,
			IncludeCreditChargeAgreement		= boIncludeCreditChargeAgreement,
			IsParkingTerminal					= boIsParkingTerminal,
			WillPrintChargeSlip					= boWillPrintChargeSlip,
			WillPrintVoidItem					= boWillPrintVoidItem,
			IncludeTermsAndConditions			= boIncludeTermsAndConditions,
			PWDDiscountCode						= strPWDDiscountCode,
			DefaultTransactionChargeCode		= strDefaultTransactionChargeCode,
			DineInChargeCode					= strDineInChargeCode,
			TakeOutChargeCode					= strTakeOutChargeCode,
			DeliveryChargeCode					= strDeliveryChargeCode,
			LastModified						= dteLastModified
		WHERE TerminalID						= intTerminalID;
	ELSE
		INSERT INTO tblTerminal(TerminalID, TerminalNo, TerminalCode, TerminalName, Status, DateCreated,
								IsPrinterAutoCutter, MaxReceiptWidth, TransactionNoLength, AutoPrint, PrinterName,
								TurretName, CashDrawerName, MachineSerialNo, AccreditationNo, ItemVoidConfirmation,
								EnableEVAT, FORM_Behavior, MarqueeMessage, TrustFund, IsVATInclusive, VAT, EVAT,
								LocalTax, ShowItemMoreThanZeroQty, ShowOneTerminalSuspendedTransactions,
								ShowOnlyPackedTransactions, TerminalReceiptType, SalesInvoicePrinterName,
								CashCountBeforeReport, PreviewTerminalReport, OrderSlipPrinter, DBVersion, FEVersion,
								BEVersion, IsPrinterDotmatrix, IsChargeEditable, IsDiscountEditable, CheckCutOffTime,
								StartCutOffTime, EndCutOffTime, WithRestaurantFeatures, SeniorCitizenDiscountCode,
								IsTouchScreen, WillContinueSelectionVariation, WillContinueSelectionProduct,
								RETPriceMarkUp, WSPriceMarkUp, WillPrintGrandTotal, ReservedAndCommit,
								ShowCustomerSelection, EnableRewardPoints, RewardPointsMinimum, RewardPointsEvery,
								RewardPoints, RoundDownRewardPoints, AutoGenerateRewardCardNo, 
								EnableRewardPointsAsPayment, RewardPointsMaxPercentageForPayment, 
								RewardPointsPaymentValue, RewardPointsPaymentCashEquivalent, RewardsPermitNo,
								IsFineDining,
								PersonalChargeTypeID, GroupChargeTypeID, BranchID, ProductSearchType,
								IncludeCreditChargeAgreement, IsParkingTerminal, WillPrintChargeSlip, WillPrintVoidItem,
								IncludeTermsAndConditions,PWDDiscountCode, DefaultTransactionChargeCode, 
								DineInChargeCode, TakeOutChargeCode, DeliveryChargeCode, CreatedOn, LastModified)
			VALUES(intTerminalID, strTerminalNo, strTerminalCode, strTerminalName, intStatus, dteDateCreated,
								boIsPrinterAutoCutter, intMaxReceiptWidth, intTransactionNoLength, intAutoPrint, strPrinterName,
								strTurretName, strCashDrawerName, strMachineSerialNo, strAccreditationNo, boItemVoidConfirmation,
								boEnableEVAT, strFORM_Behavior, strMarqueeMessage, decTrustFund, boIsVATInclusive, intVAT, intEVAT,
								intLocalTax, boShowItemMoreThanZeroQty, boShowOneTerminalSuspendedTransactions,
								boShowOnlyPackedTransactions, intTerminalReceiptType, strSalesInvoicePrinterName,
								boCashCountBeforeReport, boPreviewTerminalReport, intOrderSlipPrinter, strDBVersion, strFEVersion,
								strBEVersion, boIsPrinterDotmatrix, boIsChargeEditable, boIsDiscountEditable, boCheckCutOffTime,
								strStartCutOffTime, strEndCutOffTime, boWithRestaurantFeatures, strSeniorCitizenDiscountCode,
								boIsTouchScreen, boWillContinueSelectionVariation, boWillContinueSelectionProduct,
								decRETPriceMarkUp, decWSPriceMarkUp, boWillPrintGrandTotal, boReservedAndCommit,
								boShowCustomerSelection, boEnableRewardPoints, decRewardPointsMinimum, decRewardPointsEvery,
								decRewardPoints, boRoundDownRewardPoints, boAutoGenerateRewardCardNo, 
								boEnableRewardPointsAsPayment, decRewardPointsMaxPercentageForPayment, 
								decRewardPointsPaymentValue, decRewardPointsPaymentCashEquivalent, strRewardsPermitNo,
								boIsFineDining,
								intPersonalChargeTypeID, intGroupChargeTypeID, intBranchID, intProductSearchType,
								boIncludeCreditChargeAgreement, boIsParkingTerminal, boWillPrintChargeSlip, boWillPrintVoidItem,
								boIncludeTermsAndConditions, strPWDDiscountCode, strDefaultTransactionChargeCode, 
								strDineInChargeCode, strTakeOutChargeCode, strDeliveryChargeCode, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveChargeType
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveChargeType
GO

create procedure procSaveChargeType(	
	IN intChargeTypeID INT(10),
	IN strChargeTypeCode VARCHAR(60),
	IN strChargeType VARCHAR(20),
	IN decChargeAmount decimal(18,2),
	IN boInPercent tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ChargeTypeID FROM tblChargeType WHERE ChargeTypeID = intChargeTypeID) THEN 
		UPDATE tblChargeType SET
			ChargeTypeCode			= strChargeTypeCode,
			ChargeType				= strChargeType,
			ChargeAmount			= decChargeAmount,
			InPercent				= boInPercent,
			LastModified			= dteLastModified
		WHERE ChargeTypeID			= intChargeTypeID;
	ELSE
		INSERT INTO tblChargeType(ChargeTypeID, ChargeTypeCode, ChargeType, ChargeAmount, InPercent, CreatedOn, LastModified)
			VALUES(intChargeTypeID, strChargeTypeCode, strChargeType, decChargeAmount, boInPercent, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveDiscount
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveDiscount
GO

create procedure procSaveDiscount(	
	IN intDiscountID INT(10),
	IN strDiscountCode VARCHAR(5),
	IN strDiscountType VARCHAR(60),
	IN decDiscountPrice decimal(18,2),
	IN boInPercent tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT DiscountID FROM tblDiscount WHERE DiscountID = intDiscountID) THEN 
		UPDATE tblDiscount SET
			DiscountCode			= strDiscountCode,
			DiscountType			= strDiscountType,
			DiscountPrice			= decDiscountPrice,
			InPercent				= boInPercent,
			LastModified			= dteLastModified
		WHERE DiscountID			= intDiscountID;
	ELSE
		INSERT INTO tblDiscount(DiscountID, DiscountCode, DiscountType, DiscountPrice, InPercent, CreatedOn, LastModified)
			VALUES(intDiscountID, strDiscountCode, strDiscountType, decDiscountPrice, boInPercent, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procCountry
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCountry
GO

create procedure procSaveCountry(	
	IN intCountryID INT(10),
	IN strCountryName VARCHAR(120),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT CountryID FROM tblCountry WHERE CountryID = intCountryID) THEN 
		UPDATE tblCountry SET
			CountryName				= strCountryName,
			LastModified			= dteLastModified
		WHERE CountryID			= intCountryID;
	ELSE
		INSERT INTO tblCountry(CountryID, CountryName, CreatedOn, LastModified)
			VALUES(intCountryID, strCountryName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procDenomination
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveDenomination
GO

create procedure procSaveDenomination(	
	IN intDenominationID BIGINT(20),
	IN strDenominationCode VARCHAR(100),
	IN decDenominationValue DECIMAL(18,2),
	IN strImagePath VARCHAR(100),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT DenominationID FROM tblDenomination WHERE DenominationID = intDenominationID) THEN 
		UPDATE tblDenomination SET
			DenominationCode		= strDenominationCode,
			DenominationValue		= decDenominationValue,
			ImagePath				= strImagePath,
			LastModified			= dteLastModified
		WHERE DenominationID		= intDenominationID;
	ELSE
		INSERT INTO tblDenomination(DenominationID, DenominationCode, DenominationValue, ImagePath, CreatedOn, LastModified)
			VALUES(intDenominationID, strDenominationCode, decDenominationValue, strImagePath, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveSalutation
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveSalutation
GO

create procedure procSaveSalutation(	
	IN intSalutationID INT(10),
	IN strSalutationCode VARCHAR(30),
	IN strSalutationName VARCHAR(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT SalutationID FROM tblSalutations WHERE SalutationID = intSalutationID) THEN 
		UPDATE tblSalutations SET
			SalutationCode			= strSalutationCode,
			SalutationName			= strSalutationName,
			LastModified			= dteLastModified
		WHERE SalutationID			= intSalutationID;
	ELSE
		INSERT INTO tblSalutations(SalutationID, SalutationCode, SalutationName, CreatedOn, LastModified)
			VALUES(intSalutationID, strSalutationCode, strSalutationName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveDepartment
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveDepartment
GO

create procedure procSaveDepartment(	
	IN intDepartmentID INT(10),
	IN strDepartmentCode VARCHAR(30),
	IN strDepartmentName VARCHAR(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT DepartmentID FROM tblDepartments WHERE DepartmentID = intDepartmentID) THEN 
		UPDATE tblDepartments SET
			DepartmentCode			= strDepartmentCode,
			DepartmentName			= strDepartmentName,
			LastModified			= dteLastModified
		WHERE DepartmentID			= intDepartmentID;
	ELSE
		INSERT INTO tblDepartments(DepartmentID, DepartmentCode, DepartmentName, CreatedOn, LastModified)
			VALUES(intDepartmentID, strDepartmentCode, strDepartmentName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCardType
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCardType
GO

create procedure procSaveCardType(	
	IN intCardTypeID INT(10),
	IN strCardTypeCode VARCHAR(30),
	IN strCardTypeName VARCHAR(30),
	IN decCreditFinanceCharge DECIMAL(18,3),
	IN decCreditLatePenaltyCharge DECIMAL(18,3),
	IN decCreditMinimumAmountDue DECIMAL(18,3),
	IN decCreditMinimumPercentageDue DECIMAL(18,3),
	IN decCreditFinanceCharge15th DECIMAL(18,3),
	IN decCreditLatePenaltyCharge15th DECIMAL(18,3),
	IN decCreditMinimumAmountDue15th DECIMAL(18,3),
	IN decCreditMinimumPercentageDue15th DECIMAL(18,3),
	IN intCreditCardType TINYINT(2),
	IN intWithGuarantor TINYINT(1),
	IN intExemptInTerminalCharge TINYINT(1),
	IN dteBillingDate DATETIME,
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT CardTypeID FROM tblCardTypes WHERE CardTypeID = intCardTypeID) THEN 
		UPDATE tblCardTypes SET
			CardTypeCode			= strCardTypeCode,
			CardTypeName			= strCardTypeName,
			CreditFinanceCharge		= decCreditFinanceCharge,
			CreditLatePenaltyCharge	= decCreditLatePenaltyCharge,
			CreditMinimumAmountDue	= decCreditMinimumAmountDue,
			CreditMinimumPercentageDue	= decCreditMinimumPercentageDue,
			CreditFinanceCharge15th		= decCreditFinanceCharge15th,
			CreditLatePenaltyCharge15th	= decCreditLatePenaltyCharge15th,
			CreditMinimumAmountDue15th	= decCreditMinimumAmountDue15th,
			CreditMinimumPercentageDue15th	= decCreditMinimumPercentageDue15th,
			WithGuarantor			= intWithGuarantor,
			ExemptInTerminalCharge	= intExemptInTerminalCharge,
			BillingDate				= dteBillingDate,
			LastModified			= dteLastModified
		WHERE CardTypeID			= intCardTypeID;
	ELSE
		INSERT INTO tblCardTypes(CardTypeID, CardTypeCode, CardTypeName, 
								 CreditFinanceCharge, CreditLatePenaltyCharge, CreditMinimumAmountDue, CreditMinimumPercentageDue,
								 CreditFinanceCharge15th, CreditLatePenaltyCharge15th, CreditMinimumAmountDue15th, CreditMinimumPercentageDue15th,
								 CreditCardType, WithGuarantor, ExemptInTerminalCharge, BillingDate, CreatedOn, LastModified)
			VALUES(intCardTypeID, strCardTypeCode, strCardTypeName, 
								 decCreditFinanceCharge, decCreditLatePenaltyCharge, decCreditMinimumAmountDue, decCreditMinimumPercentageDue,
								 decCreditFinanceCharge15th, decCreditLatePenaltyCharge15th, decCreditMinimumAmountDue15th, decCreditMinimumPercentageDue15th,
								 intCreditCardType, intWithGuarantor, intExemptInTerminalCharge, dteBillingDate, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveReceipt
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveReceipt
GO

create procedure procSaveReceipt(	
	IN intReceiptID INT(10),
	IN strModule VARCHAR(20),
	IN strText VARCHAR(40),
	IN strValue VARCHAR(40),
	IN intOrientation TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ReceiptID FROM tblReceipt WHERE ReceiptID = intReceiptID) THEN 
		UPDATE tblReceipt SET
			Module					= strModule,
			Text					= strText,
			Value					= strValue,
			Orientation				= intOrientation,
			LastModified			= dteLastModified
		WHERE ReceiptID			= intReceiptID;
	ELSEIF EXISTS(SELECT ReceiptID FROM tblReceipt WHERE Module = strModule) THEN 
		UPDATE tblReceipt SET
			Text					= strText,
			Value					= strValue,
			Orientation				= intOrientation,
			LastModified			= dteLastModified
		WHERE Module				= strModule;
	ELSE
		INSERT INTO tblReceipt(ReceiptID, Module, Text, Value, Orientation, CreatedOn, LastModified)
			VALUES(intReceiptID, strModule, strText, strValue, intOrientation, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveUnit
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveUnit
GO

create procedure procSaveUnit(	
	IN intUnitID INT(3),
	IN strUnitCode VARCHAR(5),
	IN strUnitName VARCHAR(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT UnitID FROM tblUnit WHERE UnitID = intUnitID) THEN 
		UPDATE tblUnit SET
			UnitCode				= strUnitCode,
			UnitName				= strUnitName,
			LastModified			= dteLastModified
		WHERE UnitID				= intUnitID;
	ELSE
		INSERT INTO tblUnit(UnitID, Unitcode, UnitName, CreatedOn, LastModified)
			VALUES(intUnitID, strUnitcode, strUnitName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveVariation
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveVariation
GO

create procedure procSaveVariation(	
	IN intVariationID INT(10),
	IN strVariationCode VARCHAR(3),
	IN strVariationType VARCHAR(20),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT VariationID FROM tblVariations WHERE VariationID = intVariationID) THEN 
		UPDATE tblVariations SET
			VariationCode			= strVariationCode,
			VariationType			= strVariationType,
			LastModified			= dteLastModified
		WHERE VariationID			= intVariationID;
	ELSE
		INSERT INTO tblVariations(VariationID, Variationcode, VariationType, CreatedOn, LastModified)
			VALUES(intVariationID, strVariationcode, strVariationType, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePromoType
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePromoType
GO

create procedure procSavePromoType(	
	IN intPromoTypeID INT(10),
	IN strPromoTypeCode VARCHAR(60),
	IN strPromoTypeName VARCHAR(75),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT PromoTypeID FROM tblPromoType WHERE PromoTypeID = intPromoTypeID) THEN 
		UPDATE tblPromoType SET
			PromoTypeCode			= strPromoTypeCode,
			PromoTypeName			= strPromoTypeName,
			LastModified			= dteLastModified
		WHERE PromoTypeID			= intPromoTypeID;
	ELSE
		INSERT INTO tblPromoType(PromoTypeID, PromoTypecode, PromoTypeName, CreatedOn, LastModified)
			VALUES(intPromoTypeID, strPromoTypecode, strPromoTypeName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePromo
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePromo
GO

create procedure procSavePromo(	
	IN intPromoID BIGINT(20),
	IN strPromoCode VARCHAR(60),
	IN strPromoName VARCHAR(75),
	IN dteStartDate DATETIME,
	IN dteEndDate DATETIME,
	IN intPromoTypeID INT(10),
	IN intStatus TINYINT(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT PromoID FROM tblPromo WHERE PromoID = intPromoID) THEN 
		UPDATE tblPromo SET
			PromoCode				= strPromoCode,
			PromoName				= strPromoName,
			StartDate				= dteStartDate,
			EndDate					= dteEndDate,
			PromoTypeID				= intPromoTypeID,
			Status					= intStatus,
			LastModified			= dteLastModified
		WHERE PromoID				= intPromoID;
	ELSE
		INSERT INTO tblPromo(PromoID, Promocode, PromoName, StartDate, EndDate, PromoTypeID, Status, CreatedOn, LastModified)
			VALUES(intPromoID, strPromocode, strPromoName, dteStartDate, dteEndDate, intPromoTypeID, intStatus, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/******************************************** product group *****************************************/

/********************************************
	procSaveProductGroup
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroup
GO

create procedure procSaveProductGroup(	
	IN intProductGroupID BIGINT(20),
	IN strProductGroupCode varchar(20),
	IN strProductGroupName varchar(50),
	IN intBaseUnitID int(10),
	IN decPrice decimal(18,2),
	IN decPurchasePrice decimal(18,2),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decVAT decimal(18,2),
	IN decEVAT decimal(18,2),
	IN decLocalTax decimal(18,2),
	IN intChartOfAccountIDPurchase int(4),
	IN intChartOfAccountIDTaxPurchase int(4),
	IN intChartOfAccountIDSold int(4),
	IN intChartOfAccountIDTaxSold int(4),
	IN intChartOfAccountIDInventory int(4),
	IN intSequenceNo bigint(20),
	IN boisLock tinyint(1),
	IN intChartOfAccountIDTransferIn int(4),
	IN intChartOfAccountIDTaxTransferIn int(4),
	IN intChartOfAccountIDTransferOut int(4),
	IN intChartOfAccountIDTaxTransferOut int(4),
	IN intChartOfAccountIDInvAdjustment int(4),
	IN intChartOfAccountIDTaxInvAdjustment int(4),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductGroupID FROM tblProductGroup WHERE ProductGroupID = intProductGroupID) THEN 
		UPDATE tblProductGroup SET
			ProductGroupCode		= strProductGroupCode,
			ProductGroupName		= strProductGroupName,
			BaseUnitID				= intBaseUnitID,
			Price					= decPrice,
			PurchasePrice			= decPurchasePrice,
			IncludeInSubtotalDiscount	= boIncludeInSubtotalDiscount,
			VAT						= decVAT,
			EVAT					= decEVAT,
			LocalTax				= decLocalTax,
			ChartOfAccountIDPurchase	= intChartOfAccountIDPurchase,
			ChartOfAccountIDTaxPurchase = intChartOfAccountIDTaxPurchase,
			ChartOfAccountIDSold		= intChartOfAccountIDSold,
			ChartOfAccountIDTaxSold		= intChartOfAccountIDTaxSold,
			ChartOfAccountIDInventory	= intChartOfAccountIDInventory,
			SequenceNo				= intSequenceNo,
			isLock					= boisLock,
			ChartOfAccountIDTransferIn		= intChartOfAccountIDTransferIn,
			ChartOfAccountIDTaxTransferIn	= intChartOfAccountIDTaxTransferIn,
			ChartOfAccountIDTransferOut		= intChartOfAccountIDTransferOut,
			ChartOfAccountIDTaxTransferOut  = intChartOfAccountIDTaxTransferOut,
			ChartOfAccountIDInvAdjustment	= intChartOfAccountIDInvAdjustment,
			ChartOfAccountIDTaxInvAdjustment= intChartOfAccountIDTaxInvAdjustment,
			LastModified			= dteLastModified
		WHERE ProductGroupID		= intProductGroupID;
	ELSE
		INSERT INTO tblProductGroup(ProductGroupID, ProductGroupCode, ProductGroupName, BaseUnitID,
								Price, PurchasePrice, IncludeInSubtotalDiscount, VAT, EVAT,
								LocalTax, ChartOfAccountIDPurchase, ChartOfAccountIDTaxPurchase,
								ChartOfAccountIDSold, ChartOfAccountIDTaxSold, ChartOfAccountIDInventory,
								SequenceNo, isLock, ChartOfAccountIDTransferIn, ChartOfAccountIDTaxTransferIn,
								ChartOfAccountIDTransferOut, ChartOfAccountIDTaxTransferOut, 
								ChartOfAccountIDInvAdjustment, ChartOfAccountIDTaxInvAdjustment, 
								CreatedOn, LastModified)
			VALUES(intProductGroupID, strProductGroupCode, strProductGroupName, intBaseUnitID,
								decPrice, decPurchasePrice, boIncludeInSubtotalDiscount, decVAT, decEVAT,
								decLocalTax, intChartOfAccountIDPurchase, intChartOfAccountIDTaxPurchase,
								intChartOfAccountIDSold, intChartOfAccountIDTaxSold, intChartOfAccountIDInventory,
								intSequenceNo, boisLock, intChartOfAccountIDTransferIn, intChartOfAccountIDTaxTransferIn,
								intChartOfAccountIDTransferOut, intChartOfAccountIDTaxTransferOut, 
								intChartOfAccountIDInvAdjustment, intChartOfAccountIDTaxInvAdjustment, 
								dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductGroupBaseVariationsMatrix
	Aug 2, 2014

	call procSaveProductGroupBaseVariationsMatrix(1,2,3,4,5,6,7,8,9, 10, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroupBaseVariationsMatrix
GO

create procedure procSaveProductGroupBaseVariationsMatrix(	
	IN intMatrixID int(10),
	IN intGroupID bigint(20),
	IN strDescription varchar(255),
	IN intUnitID int(3),
	IN decPrice decimal(18,2),
	IN decPurchasePrice decimal(18,2),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decVAT decimal(18,2),
	IN decEVAT decimal(18,2),
	IN decLocalTax decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductGroupBaseVariationsMatrix WHERE MatrixID = intMatrixID AND GroupID = intGroupID) THEN 
		UPDATE tblProductGroupBaseVariationsMatrix SET
			Description				= strDescription,
			UnitID					= intUnitID,
			Price					= decPrice,
			PurchasePrice			= decPurchasePrice,
			IncludeInSubtotalDiscount	= boIncludeInSubtotalDiscount,
			VAT						= decVAT,
			EVAT					= decEVAT,
			LocalTax				= decLocalTax,
			LastModified			= dteLastModified
		WHERE MatrixID = intMatrixID AND GroupID = intGroupID;
	ELSE
		INSERT INTO tblProductGroupBaseVariationsMatrix(MatrixID, GroupID, Description, UnitID,
								Price, PurchasePrice, IncludeInSubtotalDiscount,
								VAT, EVAT, LocalTax,CreatedOn, LastModified)
			VALUES(intMatrixID, intGroupID, strDescription, intUnitID,
								decPrice, decPurchasePrice, boIncludeInSubtotalDiscount,
								decVAT, decEVAT, decLocalTax, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductGroupVariations
	Aug 9, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroupVariations
GO

create procedure procSaveProductGroupVariations(
	IN intProductGroupVariationID bigint(20),
	IN intGroupID bigint(20),
	IN intVariationID int(10),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductGroupVariationID FROM tblProductGroupVariations WHERE ProductGroupVariationID = intProductGroupVariationID) THEN 
		UPDATE tblProductGroupVariations SET
			GroupID					= intGroupID,
			VariationID				= intVariationID,
			LastModified			= dteLastModified
		WHERE ProductGroupVariationID = intProductGroupVariationID;
	ELSE
		INSERT INTO tblProductGroupVariations(ProductGroupVariationID, GroupID, VariationID, CreatedOn, LastModified)
			VALUES(intProductGroupVariationID, intGroupID, intVariationID, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductGroupVariationsMatrix
	Aug 9, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroupVariationsMatrix
GO

create procedure procSaveProductGroupVariationsMatrix(
	IN intProductGroupVariationsMatrixID bigint(20),
	IN intMatrixID bigint(20),
	IN intVariationID int(10),
	IN strDescription varchar(150),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductGroupVariationsMatrixID FROM tblProductGroupVariationsMatrix WHERE ProductGroupVariationsMatrixID = intProductGroupVariationsMatrixID) THEN 
		UPDATE tblProductGroupVariationsMatrix SET
			MatrixID				= intMatrixID,
			VariationID				= intVariationID,
			Description				= strDescription,
			LastModified			= dteLastModified
		WHERE ProductGroupVariationsMatrixID = intProductGroupVariationsMatrixID;
	ELSE
		INSERT INTO tblProductGroupVariationsMatrix(ProductGroupVariationsMatrixID, MatrixID, VariationID, Description, CreatedOn, LastModified)
			VALUES(intProductGroupVariationsMatrixID, intMatrixID, intVariationID, strDescription, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductGroupUnitMatrix
	Aug 9, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroupUnitMatrix
GO

create procedure procSaveProductGroupUnitMatrix(
	IN intMatrixID bigint(20),
	IN intGroupID bigint(10),
	IN intBaseUnitID int(10),
	IN decBaseUnitValue decimal(18,2),
	IN intBottomUnitID int(10),
	IN decBottomUnitValue decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductGroupUnitMatrix WHERE MatrixID = intMatrixID) THEN 
		UPDATE tblProductGroupUnitMatrix SET
			GroupID					= intGroupID,
			BaseUnitID				= intBaseUnitID,
			BaseUnitValue			= decBaseUnitValue,
			BottomUnitID			= intBottomUnitID,
			BottomUnitValue			= decBottomUnitValue,
			LastModified			= dteLastModified
		WHERE MatrixID				= intMatrixID;
	ELSE
		INSERT INTO tblProductGroupUnitMatrix(MatrixID, GroupID, BaseUnitID, BaseUnitValue, 
										BottomUnitID, BottomUnitValue, CreatedOn, LastModified)
			VALUES(intMatrixID, intGroupID, intBaseUnitID, decBaseUnitValue, 
										intBottomUnitID, decBottomUnitValue, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductGroupCharges
	Aug 9, 2014
	call procSaveProductGroupCharges(1,1,1,1,1,now(),now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductGroupCharges
GO

create procedure procSaveProductGroupCharges(
	IN intChargeID bigint(20),
	IN intGroupID bigint(20),
	IN intChargeTypeID int(10),
	IN decChargeAmount decimal(18,2),
	IN boInPercent tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ChargeID FROM tblProductGroupCharges WHERE ChargeID = intChargeID) THEN 
		UPDATE tblProductGroupCharges SET
			GroupID					= intGroupID,
			ChargeTypeID			= intChargeTypeID,
			ChargeAmount			= decChargeAmount,
			InPercent				= boInPercent,
			LastModified			= dteLastModified
		WHERE ChargeID				= intChargeID;
	ELSE
		INSERT INTO tblProductGroupCharges(ChargeID, GroupID, ChargeTypeID, ChargeAmount, InPercent, CreatedOn, LastModified)
			VALUES(intChargeID, intGroupID, intChargeTypeID, decChargeAmount, boInPercent, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;

/******************************************** product subgroup *****************************************/

/********************************************
	procSaveProductSubGroup
	Aug 2, 2014

	call procSaveProductSubGroup(1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,now(),now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroup
GO

create procedure procSaveProductSubGroup(	
	IN intProductSubGroupID BIGINT(20),
	IN intProductGroupID BIGINT(20),
	IN strProductSubGroupCode varchar(20),
	IN strProductSubGroupName varchar(50),
	IN intBaseUnitID int(10),
	IN decPrice decimal(18,2),
	IN decPurchasePrice decimal(18,2),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decVAT decimal(18,2),
	IN decEVAT decimal(18,2),
	IN decLocalTax decimal(18,2),
	IN intChartOfAccountIDPurchase int(4),
	IN intChartOfAccountIDTaxPurchase int(4),
	IN intChartOfAccountIDSold int(4),
	IN intChartOfAccountIDTaxSold int(4),
	IN intChartOfAccountIDInventory int(4),
	IN intSequenceNo bigint(20),
	IN strImagePath varchar(500),
	IN intChartOfAccountIDTransferIn int(4),
	IN intChartOfAccountIDTaxTransferIn int(4),
	IN intChartOfAccountIDTransferOut int(4),
	IN intChartOfAccountIDTaxTransferOut int(4),
	IN intChartOfAccountIDInvAdjustment int(4),
	IN intChartOfAccountIDTaxInvAdjustment int(4),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductSubGroupID = intProductSubGroupID) THEN 
		UPDATE tblProductSubGroup SET
			ProductGroupID			= intProductGroupID,
			ProductSubGroupCode		= strProductSubGroupCode,
			ProductSubGroupName		= strProductSubGroupName,
			BaseUnitID				= intBaseUnitID,
			Price					= decPrice,
			PurchasePrice			= decPurchasePrice,
			IncludeInSubtotalDiscount	= boIncludeInSubtotalDiscount,
			VAT						= decVAT,
			EVAT					= decEVAT,
			LocalTax				= decLocalTax,
			ChartOfAccountIDPurchase	= intChartOfAccountIDPurchase,
			ChartOfAccountIDTaxPurchase = intChartOfAccountIDTaxPurchase,
			ChartOfAccountIDSold		= intChartOfAccountIDSold,
			ChartOfAccountIDTaxSold		= intChartOfAccountIDTaxSold,
			ChartOfAccountIDInventory	= intChartOfAccountIDInventory,
			SequenceNo				= intSequenceNo,
			ImagePath				= strImagePath,
			ChartOfAccountIDTransferIn		= intChartOfAccountIDTransferIn,
			ChartOfAccountIDTaxTransferIn	= intChartOfAccountIDTaxTransferIn,
			ChartOfAccountIDTransferOut		= intChartOfAccountIDTransferOut,
			ChartOfAccountIDTaxTransferOut  = intChartOfAccountIDTaxTransferOut,
			ChartOfAccountIDInvAdjustment	= intChartOfAccountIDInvAdjustment,
			ChartOfAccountIDTaxInvAdjustment= intChartOfAccountIDTaxInvAdjustment,
			LastModified			= dteLastModified
		WHERE ProductSubGroupID		= intProductSubGroupID;
	ELSE
		INSERT INTO tblProductSubGroup(ProductSubGroupID, ProductGroupID, ProductSubGroupCode, ProductSubGroupName, BaseUnitID,
								Price, PurchasePrice, IncludeInSubtotalDiscount, VAT, EVAT,
								LocalTax, ChartOfAccountIDPurchase, ChartOfAccountIDTaxPurchase,
								ChartOfAccountIDSold, ChartOfAccountIDTaxSold, ChartOfAccountIDInventory,
								SequenceNo, ImagePath, ChartOfAccountIDTransferIn, ChartOfAccountIDTaxTransferIn,
								ChartOfAccountIDTransferOut, ChartOfAccountIDTaxTransferOut, 
								ChartOfAccountIDInvAdjustment, ChartOfAccountIDTaxInvAdjustment, 
								CreatedOn, LastModified)
			VALUES(intProductSubGroupID, intProductGroupID, strProductSubGroupCode, strProductSubGroupName, intBaseUnitID,
								decPrice, decPurchasePrice, boIncludeInSubtotalDiscount, decVAT, decEVAT,
								decLocalTax, intChartOfAccountIDPurchase, intChartOfAccountIDTaxPurchase,
								intChartOfAccountIDSold, intChartOfAccountIDTaxSold, intChartOfAccountIDInventory,
								intSequenceNo, strImagePath, intChartOfAccountIDTransferIn, intChartOfAccountIDTaxTransferIn,
								intChartOfAccountIDTransferOut, intChartOfAccountIDTaxTransferOut, 
								intChartOfAccountIDInvAdjustment, intChartOfAccountIDTaxInvAdjustment, 
								dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;

/********************************************
	procSaveProductSubGroupBaseVariationsMatrix
	Aug 2, 2014

	call procSaveProductSubGroupBaseVariationsMatrix(1,2,3,4,5,6,7,8,9, 10, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroupBaseVariationsMatrix
GO

create procedure procSaveProductSubGroupBaseVariationsMatrix(	
	IN intMatrixID int(10),
	IN intSubGroupID bigint(20),
	IN strDescription varchar(255),
	IN intUnitID int(3),
	IN decPrice decimal(18,2),
	IN decPurchasePrice decimal(18,2),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decVAT decimal(18,2),
	IN decEVAT decimal(18,2),
	IN decLocalTax decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductSubGroupBaseVariationsMatrix WHERE MatrixID = intMatrixID AND SubGroupID = intSubGroupID) THEN 
		UPDATE tblProductSubGroupBaseVariationsMatrix SET
			Description				= strDescription,
			UnitID					= intUnitID,
			Price					= decPrice,
			PurchasePrice			= decPurchasePrice,
			IncludeInSubtotalDiscount	= boIncludeInSubtotalDiscount,
			VAT						= decVAT,
			EVAT					= decEVAT,
			LocalTax				= decLocalTax,
			LastModified			= dteLastModified
		WHERE MatrixID = intMatrixID AND SubGroupID = intSubGroupID;
	ELSE
		INSERT INTO tblProductSubGroupBaseVariationsMatrix(MatrixID, SubGroupID, Description, UnitID,
								Price, PurchasePrice, IncludeInSubtotalDiscount,
								VAT, EVAT, LocalTax,CreatedOn, LastModified)
			VALUES(intMatrixID, intSubGroupID, strDescription, intUnitID,
								decPrice, decPurchasePrice, boIncludeInSubtotalDiscount,
								decVAT, decEVAT, decLocalTax, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductSubGroupVariations
	Aug 9, 2014
	call procSaveProductSubGroupVariations(1,1,1, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroupVariations
GO

create procedure procSaveProductSubGroupVariations(
	IN intProductSubGroupVariationID bigint(20),
	IN intSubGroupID bigint(20),
	IN intVariationID int(10),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductSubGroupVariationID FROM tblProductSubGroupVariations WHERE ProductSubGroupVariationID = intProductSubGroupVariationID) THEN 
		UPDATE tblProductSubGroupVariations SET
			SubGroupID					= intSubGroupID,
			VariationID				= intVariationID,
			LastModified			= dteLastModified
		WHERE ProductSubGroupVariationID = intProductSubGroupVariationID;
	ELSE
		INSERT INTO tblProductSubGroupVariations(ProductSubGroupVariationID, SubGroupID, VariationID, CreatedOn, LastModified)
			VALUES(intProductSubGroupVariationID, intSubGroupID, intVariationID, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductSubGroupVariationsMatrix
	Aug 9, 2014

	call procSaveProductSubGroupVariationsMatrix(1,1,1,1,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroupVariationsMatrix
GO

create procedure procSaveProductSubGroupVariationsMatrix(
	IN intProductSubGroupVariationsMatrixID bigint(20),
	IN intMatrixID bigint(20),
	IN intVariationID int(10),
	IN strDescription varchar(150),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductSubGroupVariationsMatrixID FROM tblProductSubGroupVariationsMatrix WHERE ProductSubGroupVariationsMatrixID = intProductSubGroupVariationsMatrixID) THEN 
		UPDATE tblProductSubGroupVariationsMatrix SET
			MatrixID				= intMatrixID,
			VariationID				= intVariationID,
			Description				= strDescription,
			LastModified			= dteLastModified
		WHERE ProductSubGroupVariationsMatrixID = intProductSubGroupVariationsMatrixID;
	ELSE
		INSERT INTO tblProductSubGroupVariationsMatrix(ProductSubGroupVariationsMatrixID, MatrixID, VariationID, Description, CreatedOn, LastModified)
			VALUES(intProductSubGroupVariationsMatrixID, intMatrixID, intVariationID, strDescription, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductSubGroupUnitMatrix
	Aug 9, 2014
	call procSaveProductSubGroupUnitMatrix(1,1,1,1,1,1,now(),now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroupUnitMatrix
GO

create procedure procSaveProductSubGroupUnitMatrix(
	IN intMatrixID bigint(20),
	IN intSubGroupID bigint(10),
	IN intBaseUnitID int(10),
	IN decBaseUnitValue decimal(18,2),
	IN intBottomUnitID int(10),
	IN decBottomUnitValue decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductSubGroupUnitMatrix WHERE MatrixID = intMatrixID) THEN 
		UPDATE tblProductSubGroupUnitMatrix SET
			SubGroupID				= intSubGroupID,
			BaseUnitID				= intBaseUnitID,
			BaseUnitValue			= decBaseUnitValue,
			BottomUnitID			= intBottomUnitID,
			BottomUnitValue			= decBottomUnitValue,
			LastModified			= dteLastModified
		WHERE MatrixID				= intMatrixID;
	ELSE
		INSERT INTO tblProductSubGroupUnitMatrix(MatrixID, SubGroupID, BaseUnitID, BaseUnitValue, 
										BottomUnitID, BottomUnitValue, CreatedOn, LastModified)
			VALUES(intMatrixID, intSubGroupID, intBaseUnitID, decBaseUnitValue, 
										intBottomUnitID, decBottomUnitValue, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;

/********************************************
	procSaveProductSubGroupCharges
	Aug 9, 2014
	call procSaveProductSubGroupCharges(1,1,1,1,1,now(),now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductSubGroupCharges
GO

create procedure procSaveProductSubGroupCharges(
	IN intChargeID bigint(20),
	IN intSubGroupID bigint(20),
	IN intChargeTypeID int(10),
	IN decChargeAmount decimal(18,2),
	IN boInPercent tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ChargeID FROM tblProductSubGroupCharges WHERE ChargeID = intChargeID) THEN 
		UPDATE tblProductSubGroupCharges SET
			SubGroupID				= intSubGroupID,
			ChargeTypeID			= intChargeTypeID,
			ChargeAmount			= decChargeAmount,
			InPercent				= boInPercent,
			LastModified			= dteLastModified
		WHERE ChargeID				= intChargeID;
	ELSE
		INSERT INTO tblProductSubGroupCharges(ChargeID, SubGroupID, ChargeTypeID, ChargeAmount, InPercent, CreatedOn, LastModified)
			VALUES(intChargeID, intSubGroupID, intChargeTypeID, decChargeAmount, boInPercent, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;

/******************************************** product *****************************************/


/********************************************
	procSaveProduct
	Aug 2, 2014
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProduct
GO

create procedure procSaveProduct(	
	IN intProductID BIGINT(20),
	IN strProductCode VARCHAR(30),
	IN strProductDesc VARCHAR(50),
	IN intProductSubGroupID BIGINT(20),
	IN intBaseUnitID int(10),
	IN dteDateCreated datetime,
	IN boDeleted tinyint(1),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decMinThreshold decimal(18,2),
	IN decMaxThreshold decimal(18,2),
	IN intSupplierID bigint(20),
	IN intChartOfAccountIDPurchase int(4),
	IN intChartOfAccountIDTaxPurchase int(4),
	IN intChartOfAccountIDSold int(4),
	IN intChartOfAccountIDTaxSold int(4),
	IN intChartOfAccountIDInventory int(4),
	IN boIsItemSold tinyint(1),
	IN boWillPrintProductComposition tinyint(1),
	IN intVariationCount bigint(20),
	IN boActive tinyint(1),
	IN decPercentageCommision decimal(18,2),
	IN intRID bigint(20),
	IN decRIDMinThreshold decimal(18,3),
	IN decRIDMaxThreshold decimal(18,3),
	IN decRewardPoints decimal(18,3),
	IN intSequenceNo bigint(20),
	IN intChartOfAccountIDTransferIn int(4),
	IN intChartOfAccountIDTaxTransferIn int(4),
	IN intChartOfAccountIDTransferOut int(4),
	IN intChartOfAccountIDTaxTransferOut int(4),
	IN intChartOfAccountIDInvAdjustment int(4),
	IN intChartOfAccountIDTaxInvAdjustment int(4),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT ProductID FROM tblProducts WHERE ProductID = intProductID) THEN 
		UPDATE tblProducts SET
			ProductCode				= strProductCode,
			ProductDesc				= strProductDesc,
			ProductSubGroupID		= intProductSubGroupID,
			BaseUnitID				= intBaseUnitID,
			DateCreated				= dteDateCreated,
			Deleted					= boDeleted,
			IncludeInSubtotalDiscount		= boIncludeInSubtotalDiscount,
			MinThreshold			= decMinThreshold,
			MaxThreshold			= decMaxThreshold,
			SupplierID				= intSupplierID,
			ChartOfAccountIDPurchase		= intChartOfAccountIDPurchase,
			ChartOfAccountIDTaxPurchase		= intChartOfAccountIDTaxPurchase,
			ChartOfAccountIDSold			= intChartOfAccountIDSold,
			ChartOfAccountIDTaxSold			= intChartOfAccountIDTaxSold,
			ChartOfAccountIDInventory		= intChartOfAccountIDInventory,
			IsItemSold						= boIsItemSold,
			WillPrintProductComposition		= boWillPrintProductComposition,
			VariationCount			= intVariationCount,
			Active					= boActive,
			PercentageCommision		= decPercentageCommision,
			RID						= intRID,
			RIDMinThreshold			= decRIDMinThreshold,
			RIDMaxThreshold			= decRIDMaxThreshold,
			RewardPoints			= decRewardPoints,
			SequenceNo				= intSequenceNo,
			ChartOfAccountIDTransferIn		= intChartOfAccountIDTransferIn,
			ChartOfAccountIDTaxTransferIn	= intChartOfAccountIDTaxTransferIn,
			ChartOfAccountIDTransferOut		= intChartOfAccountIDTransferOut,
			ChartOfAccountIDTaxTransferOut	= intChartOfAccountIDTaxTransferOut,
			ChartOfAccountIDInvAdjustment	= intChartOfAccountIDInvAdjustment,
			ChartOfAccountIDTaxInvAdjustment= intChartOfAccountIDTaxInvAdjustment,
			LastModified			= dteLastModified
		WHERE ProductID				= intProductID;
	ELSE
		INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID,
								BaseUnitID, DateCreated, Deleted, IncludeInSubtotalDiscount,
								MinThreshold, MaxThreshold, SupplierID, ChartOfAccountIDPurchase,
								ChartOfAccountIDTaxPurchase, ChartOfAccountIDSold, ChartOfAccountIDTaxSold,
								ChartOfAccountIDInventory, IsItemSold, WillPrintProductComposition,
								VariationCount, Active, PercentageCommision, RID, RIDMinThreshold,
								RIDMaxThreshold, RewardPoints, SequenceNo, ChartOfAccountIDTransferIn,
								ChartOfAccountIDTaxTransferIn, ChartOfAccountIDTransferOut,
								ChartOfAccountIDTaxTransferOut, ChartOfAccountIDInvAdjustment,
								ChartOfAccountIDTaxInvAdjustment, CreatedOn, LastModified)
			VALUES(intProductID, strProductcode, strProductDesc, intProductSubGroupID,
								intBaseUnitID, dteDateCreated, boDeleted, boIncludeInSubtotalDiscount,
								decMinThreshold, decMaxThreshold, intSupplierID, intChartOfAccountIDPurchase,
								intChartOfAccountIDTaxPurchase, intChartOfAccountIDSold, intChartOfAccountIDTaxSold,
								intChartOfAccountIDInventory, boIsItemSold, boWillPrintProductComposition,
								intVariationCount, boActive, decPercentageCommision, intRID, decRIDMinThreshold,
								decRIDMaxThreshold, decRewardPoints, intSequenceNo, intChartOfAccountIDTransferIn,
								intChartOfAccountIDTaxTransferIn, intChartOfAccountIDTransferOut,
								intChartOfAccountIDTaxTransferOut, intChartOfAccountIDInvAdjustment,
								intChartOfAccountIDTaxInvAdjustment, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductBaseVariationsMatrix
	Aug 2, 2014

	call procSaveProductBaseVariationsMatrix(1,2,3,4,5,6,7,8,9, 10, 1, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductBaseVariationsMatrix
GO

create procedure procSaveProductBaseVariationsMatrix(	
	IN intMatrixID int(10),
	IN intProductID bigint(20),
	IN strDescription varchar(255),
	IN intUnitID int(3),
	IN boIncludeInSubtotalDiscount tinyint(1),
	IN decMinThreshold decimal(18,2),
	IN decMaxThreshold decimal(18,2),
	IN intSupplierID bigint(20),
	IN decRIDMinThreshold decimal(18,3),
	IN decRIDMaxThreshold decimal(18,3),
	IN boDeleted tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE MatrixID = intMatrixID AND ProductID = intProductID) THEN 
		UPDATE tblProductBaseVariationsMatrix SET
			Description				= strDescription,
			UnitID					= intUnitID,
			IncludeInSubtotalDiscount	= boIncludeInSubtotalDiscount,
			MinThreshold			= decMinThreshold,
			MaxThreshold			= decMaxThreshold,
			SupplierID				= intSupplierID,
			RIDMinThreshold			= decRIDMinThreshold,
			RIDMaxThreshold			= decRIDMaxThreshold,
			Deleted					= boDeleted,
			LastModified			= dteLastModified
		WHERE MatrixID = intMatrixID AND ProductID = intProductID;
	ELSE
		INSERT INTO tblProductBaseVariationsMatrix(MatrixID, ProductID, Description, UnitID,
						IncludeInSubtotalDiscount, MinThreshold, MaxThreshold, SupplierID, 
						RIDMinThreshold, RIDMaxThreshold, Deleted,CreatedOn, LastModified)
			VALUES(intMatrixID, intProductID, strDescription, intUnitID,
						boIncludeInSubtotalDiscount, decMinThreshold, decMaxThreshold, intSupplierID, 
						decRIDMinThreshold, decRIDMaxThreshold, boDeleted, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductVariations
	Aug 9, 2014
	call procSaveProductVariations(1,1,1, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductVariations
GO

create procedure procSaveProductVariations(	
	IN intProductVariationID bigint(20),
	IN intProductID bigint(20),
	IN intVariationID int(10),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductVariationID FROM tblProductVariations WHERE ProductVariationID = intProductVariationID) THEN 
		UPDATE tblProductVariations SET
			ProductID				= intProductID,
			VariationID				= intVariationID,
			LastModified			= dteLastModified
		WHERE ProductVariationID = intProductVariationID;
	ELSE
		INSERT INTO tblProductVariations(ProductVariationID, ProductID, VariationID, CreatedOn, LastModified)
			VALUES(intProductVariationID, intProductID, intVariationID, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductVariationsMatrix
GO

create procedure procSaveProductVariationsMatrix(
	IN intProductVariationsMatrixID bigint(20),
	IN intMatrixID bigint(20),
	IN intVariationID int(10),
	IN strDescription varchar(150),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ProductVariationsMatrixID FROM tblProductVariationsMatrix WHERE ProductVariationsMatrixID = intProductVariationsMatrixID) THEN 
		UPDATE tblProductVariationsMatrix SET
			MatrixID				= intMatrixID,
			VariationID				= intVariationID,
			Description				= strDescription,
			LastModified			= dteLastModified
		WHERE ProductVariationsMatrixID = intProductVariationsMatrixID;
	ELSE
		INSERT INTO tblProductVariationsMatrix(ProductVariationsMatrixID, MatrixID, VariationID, Description, CreatedOn, LastModified)
			VALUES(intProductVariationsMatrixID, intMatrixID, intVariationID, strDescription, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/********************************************
	procSaveProductUnitMatrix
	Aug 9, 2014
	call procSaveProductUnitMatrix(1,1,1,1,1,1,now(),now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductUnitMatrix
GO

create procedure procSaveProductUnitMatrix(
	IN intMatrixID bigint(20),
	IN intProductID bigint(10),
	IN intBaseUnitID int(10),
	IN decBaseUnitValue decimal(18,2),
	IN intBottomUnitID int(10),
	IN decBottomUnitValue decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT MatrixID FROM tblProductUnitMatrix WHERE MatrixID = intMatrixID) THEN 
		UPDATE tblProductUnitMatrix SET
			ProductID				= intProductID,
			BaseUnitID				= intBaseUnitID,
			BaseUnitValue			= decBaseUnitValue,
			BottomUnitID			= intBottomUnitID,
			BottomUnitValue			= decBottomUnitValue,
			LastModified			= dteLastModified
		WHERE MatrixID				= intMatrixID;
	ELSE
		INSERT INTO tblProductUnitMatrix(MatrixID, ProductID, BaseUnitID, BaseUnitValue, 
										BottomUnitID, BottomUnitValue, CreatedOn, LastModified)
			VALUES(intMatrixID, intProductID, intBaseUnitID, decBaseUnitValue, 
										intBottomUnitID, decBottomUnitValue, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductPackage
	Aug 2, 2014

	CALL procSaveProductPackage(1,2,3,4,5,6,7,8,9, 10, 11, 12, 13, 14, 15, 16, now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductPackage
GO

create procedure procSaveProductPackage(	
	IN intPackageID bigint(20),
	IN intProductID bigint(20),
	IN intMatrixID bigint(20),
	IN intUnitID int(3),
	IN decPrice decimal(18,2),
	IN decPurchasePrice decimal(18,2),
	IN decQuantity decimal(18,2),
	IN decVAT decimal(18,2),
	IN decEVAT decimal(18,2),
	IN decLocalTax decimal(18,2),
	IN decWSPrice decimal(18,2),
	IN strBarcode1 varchar(30),
	IN strBarcode2 varchar(30),
	IN strBarcode3 varchar(30),
	IN strBarCode4 varchar(60),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT PackageID FROM tblProductPackage WHERE PackageID = intPackageID) THEN 
		UPDATE tblProductPackage SET
			ProductID				= intProductID,
			MatrixID				= intMatrixID,
			UnitID					= intUnitID,
			Price					= decPrice,
			PurchasePrice			= decPurchasePrice,
			Quantity				= decQuantity,
			VAT						= decVAT,
			EVAT					= decEVAT,
			LocalTax				= decLocalTax,
			WSPrice					= decWSPrice,
			Barcode1				= strBarcode1,
			Barcode2				= strBarcode2,
			Barcode3				= strBarcode3,
			BarCode4				= strBarCode4,
			LastModified			= dteLastModified
		WHERE PackageID = intPackageID;
	ELSE
		INSERT INTO tblProductPackage(PackageID, ProductID, MatrixID, UnitID, Price, PurchasePrice,
									Quantity, VAT, EVAT, LocalTax, WSPrice, Barcode1, 
									Barcode2, Barcode3, BarCode4, CreatedOn, LastModified)
			VALUES(intPackageID, intProductID, intMatrixID, intUnitID, decPrice, decPurchasePrice,
									decQuantity, decVAT, decEVAT, decLocalTax, decWSPrice, strBarcode1, 
									strBarcode2, strBarcode3, strBarCode4, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveProductComposition
	Aug 2, 2014

	CALL procSaveProductComposition(1,2,3,4,5,6,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveProductComposition
GO

create procedure procSaveProductComposition(	
	IN intCompositionID bigint(20),
	IN intMainProductID bigint(20),
	IN intProductID bigint(20),
	IN intVariationMatrixID bigint(20),
	IN intUnitID int(3),
	IN decQuantity decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT CompositionID FROM tblProductComposition WHERE CompositionID = intCompositionID) THEN 
		UPDATE tblProductComposition SET
			MainProductID			= intMainProductID,
			ProductID				= intProductID,
			VariationMatrixID		= intVariationMatrixID,
			UnitID					= intUnitID,
			Quantity				= decQuantity,
			LastModified			= dteLastModified
		WHERE CompositionID			= intCompositionID;
	ELSE
		INSERT INTO tblProductComposition(CompositionID, MainProductID, ProductID, VariationMatrixID,
										UnitID, Quantity, CreatedOn, LastModified)
			VALUES(intCompositionID, intMainProductID, intProductID, intVariationMatrixID,
										intUnitID, decQuantity, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePromoItems
	Aug 2, 2014

	CALL procSavePromoItems(1,2,3,4,5,6,7,8,9,1,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePromoItems
GO

create procedure procSavePromoItems(	
	IN intPromoItemsID bigint(20),
	IN intPromoID bigint(20),
	IN intContactID bigint(20),
	IN intProductGroupID bigint(20),
	IN intProductSubGroupID bigint(20),
	IN intProductID bigint(20),
	IN intVariationMatrixID bigint(20),
	IN decQuantity decimal(18,2),
	IN decPromoValue decimal(18,2),
	IN boInPercent tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT PromoItemsID FROM tblPromoItems WHERE PromoItemsID = intPromoItemsID) THEN 
		UPDATE tblPromoItems SET
			PromoID					= intPromoID,
			ContactID				= intContactID,
			ProductGroupID			= intProductGroupID,
			ProductSubGroupID		= intProductSubGroupID,
			ProductID				= intProductID,
			VariationMatrixID		= intVariationMatrixID,
			Quantity				= decQuantity,
			PromoValue				= decPromoValue,
			InPercent				= boInPercent,
			LastModified			= dteLastModified
		WHERE PromoItemsID			= intPromoItemsID;
	ELSE
		INSERT INTO tblPromoItems(PromoItemsID, PromoID, ContactID, ProductGroupID,
							ProductSubGroupID, ProductID, VariationMatrixID, Quantity,
							PromoValue, InPercent, CreatedOn, LastModified)
			VALUES(intPromoItemsID, intPromoID, intContactID, intProductGroupID,
							intProductSubGroupID, intProductID, intVariationMatrixID, decQuantity,
							decPromoValue, boInPercent, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveStockType
	Aug 2, 2014

	CALL procSaveStockType(1,2,3,4,5,6,7,8,9,1,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveStockType
GO

create procedure procSaveStockType(	
	IN intStockTypeID bigint(20),
	IN strStockTypeCode varchar(30),
	IN strDescription varchar(150),
	IN intStockDirection tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT StockTypeID FROM tblStockType WHERE StockTypeID = intStockTypeID) THEN 
		UPDATE tblStockType SET
			StockTypeCode			= strStockTypeCode,
			Description				= strDescription,
			StockDirection			= intStockDirection,
			LastModified			= dteLastModified
		WHERE StockTypeID			= intStockTypeID;
	ELSE
		INSERT INTO tblStockType(StockTypeID, StockTypeCode, Description, StockDirection, CreatedOn, LastModified)
			VALUES(intStockTypeID, strStockTypeCode, strDescription, intStockDirection, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveRewardItems
	Aug 2, 2014

	CALL procSaveRewardItems(1,2,3,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveRewardItems
GO

create procedure procSaveRewardItems(	
	IN intRewardItemsID bigint(20),
	IN intProductID  bigint(20),
	IN decRewardPoints decimal(18,2),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT RewardItemsID FROM tblRewardItems WHERE ProductID = intProductID) THEN 
		UPDATE tblRewardItems SET
			ProductID				= intProductID,
			RewardPoints			= decRewardPoints,
			LastModified			= dteLastModified
		WHERE ProductID = intProductID;
	ELSE
		INSERT INTO tblRewardItems(RewardItemsID, ProductID, RewardPoints, CreatedOn, LastModified)
			VALUES(intRewardItemsID, intProductID, decRewardPoints, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveParkingRates
	Aug 2, 2014

	CALL procSaveParkingRates(1,2,3,4,5,6,7,8,9,10,11, now(), now(), now());

	CALL procSaveParkingRates(0, 5448, 'Monday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Tuesday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Wednesday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Thursday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Friday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Saturday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());
	CALL procSaveParkingRates(0, 5448, 'Sunday', '00:00', '23:59', 60, 10, 360, 100, 'CreatedBy', 'LastUpdatedBy', now(), now());

********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveParkingRates
GO

create procedure procSaveParkingRates(	
	IN intParkingRateID     bigint(20),
	IN intProductID         bigint(20),
	IN strDayOfWeek         varchar(9),
	IN strStartTime         varchar(5),
	IN strEndtime           varchar(5),
	IN intNoOfUnitPerMin    int(11),
	IN decPerUnitPrice      decimal(18,3),
	IN intMinimumStayInMin  int(11),
	IN decMinimumStayPrice  decimal(18,3),
	IN strCreatedByName     varchar(100),
	IN strLastUpdatedByName varchar(100),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ParkingRateID FROM tblParkingRates WHERE ParkingRateID = intParkingRateID) THEN 
		UPDATE tblParkingRates SET
			ProductID				= intProductID,
			DayOfWeek				= strDayOfWeek,
			StartTime				= strStartTime,
			Endtime					= strEndtime,
			NoOfUnitPerMin			= intNoOfUnitPerMin,
			PerUnitPrice			= decPerUnitPrice,
			MinimumStayInMin		= intMinimumStayInMin,
			MinimumStayPrice		= decMinimumStayPrice,
			CreatedByName			= strCreatedByName,
			LastUpdatedByName		= strLastUpdatedByName,
			LastModified			= dteLastModified
		WHERE ParkingRateID			= intParkingRateID;
	ELSE
		INSERT INTO tblParkingRates(ParkingRateID, ProductID, DayOfWeek, StartTime, Endtime, NoOfUnitPerMin, PerUnitPrice, MinimumStayInMin, 
								MinimumStayPrice, CreatedByName, LastUpdatedByName, CreatedOn, LastModified)
			VALUES(intParkingRateID, intProductID, strDayOfWeek, strStartTime, strEndtime, intNoOfUnitPerMin, decPerUnitPrice, intMinimumStayInMin, 
								decMinimumStayPrice, strCreatedByName, strLastUpdatedByName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePositions
	Aug 2, 2014

	CALL procSavePositions(1,2,3,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePositions
GO

create procedure procSavePositions(	
	IN intPositionID int(10),
	IN strPositionCode varchar(30),
	IN strPositionName varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT PositionID FROM tblPositions WHERE PositionID = intPositionID) THEN 
		UPDATE tblPositions SET
			PositionCode			= strPositionCode,
			PositionName			= strPositionName,
			LastModified			= dteLastModified
		WHERE PositionID			= intPositionID;
	ELSE
		INSERT INTO tblPositions(PositionID, PositionCode, PositionName, CreatedOn, LastModified)
			VALUES(intPositionID, strPositionCode, strPositionName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/*****************************Contacs**************************/


/********************************************
	procSaveContactGroup
	Aug 2, 2014

	CALL procSaveContactGroup(10,2,3,1,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveContactGroup
GO

create procedure procSaveContactGroup(	
	IN intContactGroupID int(10),
	IN strContactGroupCode varchar(10),
	IN strContactGroupName varchar(30),
	IN intContactGroupCategory tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ContactGroupID FROM tblContactGroup WHERE ContactGroupID = intContactGroupID) THEN 
		UPDATE tblContactGroup SET
			ContactGroupCode		= strContactGroupCode,
			ContactGroupName		= strContactGroupName,
			ContactGroupCategory	= intContactGroupCategory,
			LastModified			= dteLastModified
		WHERE ContactGroupID		= intContactGroupID;
	ELSE
		INSERT INTO tblContactGroup(ContactGroupID, ContactGroupCode, ContactGroupName, ContactGroupCategory, CreatedOn, LastModified)
			VALUES(intContactGroupID, strContactGroupCode, strContactGroupName, intContactGroupCategory, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveContact
	Aug 2, 2014

	CALL procSaveContact(1,2,3,4,5,6,7,8,9,10,11,12,13,14,now(),16,17,18,19,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveContact
GO

create procedure procSaveContact(	
	IN intContactID bigint(20),
	IN intSequenceNo int(20),
	IN strContactCode varchar(25),
	IN strContactName varchar(75),
	IN intContactGroupID int(10),
	IN intModeOfTerms int(10),
	IN intTerms int(10),
	IN strAddress varchar(150),
	IN strBusinessName varchar(75),
	IN strTelephoneNo varchar(75),
	IN strRemarks varchar(150),
	IN decDebit decimal(18,3),
	IN decCredit decimal(18,3),
	IN decCreditLimit decimal(18,3),
	IN boIsCreditAllowed tinyint(1),
	IN dteDateCreated datetime,
	IN boDeleted tinyint(1),
	IN intDepartmentID int(10),
	IN intPositionID int(10),
	IN boisLock tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ContactID FROM tblContacts WHERE ContactID = intContactID) THEN 
		UPDATE tblContacts SET
			SequenceNo				= intSequenceNo,
			ContactCode				= strContactCode,
			ContactName				= strContactName,
			ContactGroupID			= intContactGroupID,
			ModeOfTerms				= intModeOfTerms,
			Terms					= intTerms,
			Address					= strAddress,
			BusinessName			= strBusinessName,
			TelephoneNo				= strTelephoneNo,
			Remarks					= strRemarks,
			Debit					= decDebit,
			Credit					= decCredit,
			CreditLimit				= decCreditLimit,
			IsCreditAllowed			= boIsCreditAllowed,
			DateCreated				= dteDateCreated,
			Deleted					= boDeleted,
			DepartmentID			= intDepartmentID,
			PositionID				= intPositionID,
			isLock					= boisLock,
			LastModified			= dteLastModified
		WHERE ContactID				= intContactID;
	ELSE
		INSERT INTO tblContacts(ContactID, SequenceNo, ContactCode, ContactName, ContactGroupID, ModeOfTerms,
							Terms, Address, BusinessName, TelephoneNo, Remarks, Debit, Credit,
							CreditLimit, IsCreditAllowed, DateCreated, Deleted, DepartmentID,
							PositionID, isLock, CreatedOn, LastModified)
			VALUES(intContactID, intSequenceNo, strContactCode, strContactName, intContactGroupID, intModeOfTerms,
							intTerms, strAddress, strBusinessName, strTelephoneNo, strRemarks, decDebit, decCredit,
							decCreditLimit, boIsCreditAllowed, dteDateCreated, boDeleted, intDepartmentID,
							intPositionID, boisLock, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveContactAddOn
	Aug 2, 2014

	CALL procSaveContactAddOn(1,2,3,4,5,6,7,now(),now(),now(),8,9,10,11,12,13,14,15,16,17,18,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveContactAddOn
GO

create procedure procSaveContactAddOn(	
	IN intContactDetailID bigint(20),
	IN intContactID       bigint(20),
	IN strSalutation      varchar(30),
	IN strFirstName       varchar(85),
	IN strMiddleName      varchar(85),
	IN strLastName        varchar(85),
	IN strSpouseName      varchar(85),
	IN dteBirthDate       date,
	IN dteSpouseBirthDate date,
	IN dteAnniversaryDate date,
	IN strAddress1        varchar(150),
	IN strAddress2        varchar(150),
	IN strCity            varchar(30),
	IN strState           varchar(30),
	IN strZipCode         varchar(15),
	IN intCountryID       tinyint(4),
	IN strBusinessphoneNo varchar(75),
	IN strHomephoneNo     varchar(75),
	IN strMobileNo        varchar(75),
	IN strFaxNo           varchar(75),
	IN strEmailAddress    varchar(85),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT ContactID FROM tblContactAddOn WHERE ContactID = intContactID) THEN 
		UPDATE tblContactAddOn SET
			ContactID				= intContactID,
			Salutation				= strSalutation,
			FirstName				= strFirstName,
			MiddleName				= strMiddleName,
			LastName				= strLastName,
			SpouseName				= strSpouseName,
			BirthDate				= dteBirthDate,
			SpouseBirthDate			= dteSpouseBirthDate,
			AnniversaryDate			= dteAnniversaryDate,
			Address1				= strAddress1,
			Address2				= strAddress2,
			City					= strCity,
			State					= strState,
			ZipCode					= strZipCode,
			CountryID				= intCountryID,
			BusinessphoneNo			= strBusinessphoneNo,
			HomephoneNo				= strHomephoneNo,
			MobileNo				= strMobileNo,
			FaxNo					= strFaxNo,
			LastModified			= dteLastModified
		WHERE ContactID				= intContactID;
	ELSE
		INSERT INTO tblContactAddOn(ContactDetailID, ContactID, Salutation, FirstName, MiddleName, LastName, 
								SpouseName, BirthDate, SpouseBirthDate, AnniversaryDate, Address1, Address2,
								City, State, ZipCode, CountryID, BusinessphoneNo, HomephoneNo, MobileNo,
								FaxNo, EmailAddress, CreatedOn, LastModified)
			VALUES(intContactDetailID, intContactID, strSalutation, strFirstName, strMiddleName, strLastName, 
								strSpouseName, dteBirthDate, dteSpouseBirthDate, dteAnniversaryDate, strAddress1, strAddress2,
								strCity, strState, strZipCode, intCountryID, strBusinessphoneNo, strHomephoneNo, strMobileNo,
								strFaxNo, strEmailAddress, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveContactCreditCardInfo
	Aug 2, 2014

	CALL procSaveContactCreditCardInfo(1,2,3,4,now(),5,6,7,now(),8,now(),now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveContactCreditCardInfo
GO

create procedure procSaveContactCreditCardInfo(	
	IN intCustomerID       bigint(20),
	IN intGuarantorID      bigint(20),
	IN intCreditCardTypeID int(10),
	IN strCreditCardNo     varchar(15),
	IN dteCreditAwardDate  datetime,
	IN decTotalPurchases   decimal(18,3),
	IN decCreditPaid       decimal(18,3),
	IN intCreditCardStatus  tinyint(1),
	IN dteExpiryDate       date,
	IN strEmbossedCardNo   varchar(15),
	IN dteLastBillingDate  date,
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF EXISTS(SELECT CustomerID FROM tblContactCreditCardInfo WHERE CustomerID = intCustomerID) THEN 
		UPDATE tblContactCreditCardInfo SET
			CustomerID				= intCustomerID,
			GuarantorID				= intGuarantorID,
			CreditCardTypeID		= intCreditCardTypeID,
			CreditCardNo			= strCreditCardNo,
			CreditAwardDate			= dteCreditAwardDate,
			TotalPurchases			= decTotalPurchases,
			CreditPaid				= decCreditPaid,
			CreditCardStatus		= intCreditCardStatus,
			ExpiryDate				= dteExpiryDate,
			EmbossedCardNo			= strEmbossedCardNo,
			LastBillingDate			= dteLastBillingDate,
			LastModified			= dteLastModified
		WHERE CustomerID			= intCustomerID;
	ELSE
		INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, CreditCardNo, CreditAwardDate, 
								TotalPurchases, CreditPaid, CreditCardStatus, ExpiryDate, EmbossedCardNo, 
								LastBillingDate, CreatedOn, LastModified)
			VALUES(intCustomerID, intGuarantorID, strCardTypeCode, intCreditCardTypeID, dteCreditAwardDate, 
								decTotalPurchases, decCreditPaid, intCreditCardStatus, dteExpiryDate, strEmbossedCardNo, 
								dteLastBillingDate, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;



/******************************************** transactions *****************************************/



/********************************************
	procSaveCashierLogs
	Aug 2, 2014

	CALL procSaveCashierLogs(2,'01',0,0,5,NOW(),7,now(),1,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCashierLogs
GO

create procedure procSaveCashierLogs(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID        bigint(20),
	IN intCashierLogsID bigint(20),
	IN intUID           bigint(20),
	IN dteLoginDate     datetime,
	IN strIPAddress     varchar(15),
	IN dteLogoutDate    datetime,
	IN intStatus        tinyint(3),
	IN strBranchCode    varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCashierLogsID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;
	
	IF EXISTS(SELECT CashierLogsID FROM tblCashierLogs WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCashierLogs SET
			UID						= intUID,
			LoginDate				= dteLoginDate,
			IPAddress				= strIPAddress,
			LogoutDate				= dteLogoutDate,
			Status					= intStatus,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCashierLogs(BranchID, TerminalNo, UID, LoginDate, IPAddress, LogoutDate, Status, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intUID, dteLoginDate, strIPAddress, dteLogoutDate, intStatus, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblCashierLogs SET SyncID = CashierLogsID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCashierReport
	Aug 2, 2014

	CALL procSaveCashierReport(2,'01',0,0,5,NOW(),7,now(),1,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCashierReport
GO

create procedure procSaveCashierReport(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID							 bigint(20),
	IN intCashierReportID                    bigint(20),
	IN intCashierID							 bigint(20),
	IN intTerminalID						 bigint(20),
	IN decNetSales							 decimal(18,3),
	IN decGrossSales                         decimal(18,3),
	IN decTotalDiscount                      decimal(18,3),
	IN decSNRDiscount						 decimal(18,3),
	IN decPWDDiscount						 decimal(18,3),
	IN decOtherDiscount                      decimal(18,3),
	IN decDailySales                         decimal(18,3),
	IN decItemSold							 decimal(18,3),
	IN decQuantitySold                       decimal(18,3),
	IN decGroupSales                         decimal(18,3),
	IN decVAT                                decimal(18,3),
	IN decEVAT                               decimal(18,3),
	IN decLocalTax                           decimal(18,3),
	IN decCashSales                          decimal(18,3),
	IN decChequeSales                        decimal(18,3),
	IN decCreditCardSales                    decimal(18,3),
	IN decCreditSales                        decimal(18,3),
	IN decCreditPayment                      decimal(18,3),
	IN decCashInDrawer                       decimal(18,3),
	IN decTotalDisburse                      decimal(18,3),
	IN decCashDisburse                       decimal(18,3),
	IN decChequeDisburse                     decimal(18,3),
	IN decCreditCardDisburse                 decimal(18,3),
	IN decTotalWithhold                      decimal(18,3),
	IN decCashWithhold                       decimal(18,3),
	IN decChequeWithhold                     decimal(18,3),
	IN decCreditCardWithhold                 decimal(18,3),
	IN decTotalPaidOut                       decimal(18,3),
	IN decCashPaidOut                        decimal(18,3),
	IN decChequePaidOut                      decimal(18,3),
	IN decCreditCardPaidOut                  decimal(18,3),
	IN decBeginningBalance                   decimal(18,3),
	IN decVoidSales                          decimal(18,3),
	IN decRefundSales                        decimal(18,3),
	IN decItemsDiscount                      decimal(18,3),
	IN decSNRItemsDiscount                   decimal(18,3),
	IN decPWDItemsDiscount                   decimal(18,3),
	IN decOtherItemsDiscount                 decimal(18,3),
	IN decSubtotalDiscount                   decimal(18,3),
	IN intNoOfCashTransactions               int(10),
	IN intNoOfChequeTransactions             int(10),
	IN intNoOfCreditCardTransactions         int(10),
	IN intNoOfCreditTransactions             int(10),
	IN intNoOfCombinationPaymentTransactions int(10),
	IN intNoOfCreditPaymentTransactions      int(10),
	IN intNoOfClosedTransactions             int(10),
	IN intNoOfRefundTransactions             int(10),
	IN intNoOfVoidTransactions               int(10),
	IN intNoOfTotalTransactions              int(10),
	IN decCashCount                          decimal(18,3),
	IN dteLastLoginDate                      datetime,
	IN decTotalDeposit                       decimal(18,3),
	IN decCashDeposit                        decimal(18,3),
	IN decChequeDeposit                      decimal(18,3),
	IN decCreditCardDeposit                  decimal(18,3),
	IN decDebitPayment                       decimal(18,3),
	IN intNoOfDebitPaymentTransactions       int(10),
	IN decTotalCharge                        decimal(18,3),
	IN intIsCashCountInitialized             tinyint(1),
	IN intNoOfDiscountedTransactions         int(4),
	IN decNegativeAdjustments                decimal(18,3),
	IN intNoOfNegativeAdjustmentTransactions int(4),
	IN decPromotionalItems                   decimal(18,3),
	IN decCreditSalesTax                     decimal(18,3),
	IN decDebitDeposit                       decimal(18,3),
	IN decRewardPointsPayment                decimal(18,3),
	IN decRewardConvertedPayment             decimal(18,3),
	IN intNoOfRewardPointsPayment            int(10),
	IN decCreditPaymentCash                  decimal(18,3),
	IN decCreditPaymentCheque                decimal(18,3),
	IN decCreditPaymentCreditCard            decimal(18,3),
	IN decCreditPaymentDebit                 decimal(18,3),
	IN decRefundCash						 decimal(18,3),
	IN decRefundCheque						 decimal(18,3),
	IN decRefundCreditCard				 	 decimal(18,3),
	IN decRefundCredit						 decimal(18,3),
	IN decRefundDebit						 decimal(18,3),

	IN intNoOfConsignmentTransactions  int(10),
	IN intNoOfConsignmentRefundTransactions  int(10),
	IN intNoOfWalkInTransactions  int(10),
	IN intNoOfWalkInRefundTransactions  int(10),
	IN intNoOfOutOfStockTransactions  int(10),
	IN intNoOfOutOfStockRefundTransactions  int(10),

	IN decConsignmentSales  decimal(18,3),
	IN decConsignmentRefundSales  decimal(18,3),
	IN decWalkInSales  decimal(18,3),
	IN decWalkInRefundSales  decimal(18,3),
	IN decOutOfStockSales  decimal(18,3),
	IN decOutOfStockRefundSales  decimal(18,3),

	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCashierReportID; END IF;
	
	IF EXISTS(SELECT CashierReportID FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCashierReport SET
			CashierID							= intCashierID,
			TerminalID							= decNetSales,
			NetSales							= decSubTotal,
			GrossSales							= decGrossSales,
			TotalDiscount						= decTotalDiscount,
			SNRDiscount							= decSNRDiscount,
			PWDDiscount							= decPWDDiscount,
			OtherDiscount						= decOtherDiscount,
			DailySales							= decDailySales,
			ItemSold							= decItemSold,
			QuantitySold						= decQuantitySold,
			GroupSales							= decGroupSales,
			VAT									= decVAT,
			EVAT								= decEVAT,
			LocalTax							= decLocalTax,
			CashSales							= decCashSales,
			ChequeSales							= decChequeSales,
			CreditCardSales						= decCreditCardSales,
			CreditSales							= decCreditSales,
			CreditPayment						= decCreditPayment,
			CashInDrawer						= decCashInDrawer,
			TotalDisburse						= decTotalDisburse,
			CashDisburse						= decCashDisburse,
			ChequeDisburse						= decChequeDisburse,
			CreditCardDisburse					= decCreditCardDisburse,
			TotalWithhold						= decTotalWithhold,
			CashWithhold						= decCashWithhold,
			ChequeWithhold						= decChequeWithhold,
			CreditCardWithhold					= decCreditCardWithhold,
			TotalPaidOut						= decTotalPaidOut,
			CashPaidOut							= decCashPaidOut,
			ChequePaidOut						= decChequePaidOut,
			CreditCardPaidOut					= decCreditCardPaidOut,
			BeginningBalance					= decBeginningBalance,
			VoidSales							= decVoidSales,
			RefundSales							= decRefundSales,
			ItemsDiscount						= decItemsDiscount,
			SNRItemsDiscount					= decSNRItemsDiscount,
			PWDItemsDiscount					= decPWDItemsDiscount,
			OtherItemsDiscount					= decOtherItemsDiscount,
			SubtotalDiscount					= decSubtotalDiscount,
			NoOfCashTransactions				= intNoOfCashTransactions,
			NoOfChequeTransactions				= intNoOfChequeTransactions,
			NoOfCreditCardTransactions			= intNoOfCreditCardTransactions,
			NoOfCreditTransactions				= intNoOfCreditTransactions,
			NoOfCombinationPaymentTransactions  = intNoOfCombinationPaymentTransactions,
			NoOfCreditPaymentTransactions		= intNoOfCreditPaymentTransactions,
			NoOfClosedTransactions  = intNoOfClosedTransactions,
			NoOfRefundTransactions  = intNoOfRefundTransactions,
			NoOfVoidTransactions	= intNoOfVoidTransactions,
			NoOfTotalTransactions	= intNoOfTotalTransactions,
			CashCount				= decCashCount,
			LastLoginDate			= dteLastLoginDate,
			TotalDeposit			= decTotalDeposit,
			CashDeposit				= decCashDeposit,
			ChequeDeposit			= decChequeDeposit,
			CreditCardDeposit		= decCreditCardDeposit,
			DebitPayment			= decDebitPayment,
			NoOfDebitPaymentTransactions		= intNoOfDebitPaymentTransactions,
			TotalCharge				= decTotalCharge,
			IsCashCountInitialized  = intIsCashCountInitialized,
			NoOfDiscountedTransactions			= intNoOfDiscountedTransactions,
			NegativeAdjustments		= decNegativeAdjustments,
			NoOfNegativeAdjustmentTransactions  = intNoOfNegativeAdjustmentTransactions,
			PromotionalItems		= decPromotionalItems,
			CreditSalesTax			= decCreditSalesTax,
			DebitDeposit			= decDebitDeposit,
			RewardPointsPayment		= decRewardPointsPayment,
			RewardConvertedPayment  = decRewardConvertedPayment,
			NoOfRewardPointsPayment = intNoOfRewardPointsPayment,
			CreditPaymentCash		= decCreditPaymentCash,
			CreditPaymentCheque		= decCreditPaymentCheque,
			CreditPaymentCreditCard = decCreditPaymentCreditCard,
			CreditPaymentDebit		= decCreditPaymentDebit,
			RefundCash				= decRefundCash,
			RefundCheque			= decRefundCheque,
			RefundCreditCard		= decRefundCreditCard,
			RefundCredit			= decRefundCredit,
			RefundDebit				= decRefundDebit,

			NoOfConsignmentTransactions			=	intNoOfConsignmentTransactions,
			NoOfConsignmentRefundTransactions	=	intNoOfConsignmentRefundTransactions,
			NoOfWalkInTransactions				=	intNoOfWalkInTransactions,
			NoOfWalkInRefundTransactions		=	intNoOfWalkInRefundTransactions,
			NoOfOutOfStockTransactions			=	intNoOfOutOfStockTransactions,
			NoOfOutOfStockRefundTransactions	=	intNoOfOutOfStockRefundTransactions,

			ConsignmentSales				=	decConsignmentSales,
			ConsignmentRefundSales		=	decConsignmentRefundSales,
			WalkInSales					=	decWalkInSales,
			WalkInRefundSales			=	decWalkInRefundSales,
			OutOfStockSales				=	decOutOfStockSales,
			OutOfStockRefundSales		=	decOutOfStockRefundSales,

			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCashierReport(BranchID, TerminalNo, SyncID, CashierID, TerminalID, NetSales, GrossSales, 
							TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, DailySales,
							ItemSold, QuantitySold, GroupSales, VAT, EVAT, LocalTax, CashSales, ChequeSales, CreditCardSales,
							CreditSales, CreditPayment, CashInDrawer, TotalDisburse, CashDisburse, ChequeDisburse,
							CreditCardDisburse, TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold,
							TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, BeginningBalance,
							VoidSales, RefundSales, ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, SubtotalDiscount, NoOfCashTransactions,
							NoOfChequeTransactions, NoOfCreditCardTransactions, NoOfCreditTransactions, 
							NoOfCombinationPaymentTransactions, NoOfCreditPaymentTransactions, NoOfClosedTransactions,
							NoOfRefundTransactions, NoOfVoidTransactions, NoOfTotalTransactions, CashCount, 
							LastLoginDate, TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
							DebitPayment, NoOfDebitPaymentTransactions, TotalCharge, IsCashCountInitialized,
							NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
							PromotionalItems, CreditSalesTax, DebitDeposit, RewardPointsPayment, RewardConvertedPayment,
							NoOfRewardPointsPayment, CreditPaymentCash, CreditPaymentCheque,
							CreditPaymentCreditCard, CreditPaymentDebit, 
							RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit, 
							NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
							NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
							ConsignmentSales, ConsignmentRefundSales, WalkInSales,
							WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales,
							CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intCashierID, intTerminalID, decNetSales, decGrossSales, 
							decTotalDiscount, decSNRDiscount, decPWDDiscount, decOtherDiscount, decDailySales,
							decItemSold, decQuantitySold, decGroupSales, decVAT, decEVAT, decLocalTax, decCashSales, decChequeSales, decCreditCardSales,
							decCreditSales, decCreditPayment, decCashInDrawer, decTotalDisburse, decCashDisburse, decChequeDisburse,
							decCreditCardDisburse, decTotalWithhold, decCashWithhold, decChequeWithhold, decCreditCardWithhold,
							decTotalPaidOut, decCashPaidOut, decChequePaidOut, decCreditCardPaidOut, decBeginningBalance,
							decVoidSales, decRefundSales, decItemsDiscount, decSNRItemsDiscount, decPWDItemsDiscount, decOtherItemsDiscount, decSubtotalDiscount, intNoOfCashTransactions,
							intNoOfChequeTransactions, intNoOfCreditCardTransactions, intNoOfCreditTransactions, 
							intNoOfCombinationPaymentTransactions, intNoOfCreditPaymentTransactions, intNoOfClosedTransactions,
							intNoOfRefundTransactions, intNoOfVoidTransactions, intNoOfTotalTransactions, decCashCount, 
							dteLastLoginDate, decTotalDeposit, decCashDeposit, decChequeDeposit, decCreditCardDeposit, 
							decDebitPayment, intNoOfDebitPaymentTransactions, decTotalCharge, intIsCashCountInitialized,
							intNoOfDiscountedTransactions, decNegativeAdjustments, intNoOfNegativeAdjustmentTransactions,
							decPromotionalItems, decCreditSalesTax, decDebitDeposit, decRewardPointsPayment, decRewardConvertedPayment,
							intNoOfRewardPointsPayment, decCreditPaymentCash, decCreditPaymentCheque,
							decCreditPaymentCreditCard, decCreditPaymentDebit, 
							decRefundCash, decRefundCheque, decRefundCreditCard, decRefundCredit, decRefundDebit, 
							intNoOfConsignmentTransactions, intNoOfConsignmentRefundTransactions, intNoOfWalkInTransactions,
							intNoOfWalkInRefundTransactions, intNoOfOutOfStockTransactions, intNoOfOutOfStockRefundTransactions,
							decConsignmentSales, decConsignmentRefundSales, decWalkInSales,
							decWalkInRefundSales, decOutOfStockSales, decOutOfStockRefundSales,
							dteCreatedOn, dteLastModified);

		UPDATE tblCashierReport SET SyncID = CashierReportID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCashierReportHistory
	Aug 2, 2014

	CALL procSaveCashierReportHistory(2,'01',0,0,5,NOW(),7,now(),1,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCashierReportHistory
GO

create procedure procSaveCashierReportHistory(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID							 bigint(20),
	IN intCashierReportHistoryID             bigint(20),
	IN intCashierID							 bigint(20),
	IN intTerminalID						 bigint(20),
	IN decNetSales							 decimal(18,3),
	IN decGrossSales                         decimal(18,3),
	IN decTotalDiscount                      decimal(18,3),
	IN decDailySales                         decimal(18,3),
	IN decQuantitySold                       decimal(18,3),
	IN decGroupSales                         decimal(18,3),
	IN decVAT                                decimal(18,3),
	IN decEVAT                               decimal(18,3),
	IN decLocalTax                           decimal(18,3),
	IN decCashSales                          decimal(18,3),
	IN decChequeSales                        decimal(18,3),
	IN decCreditCardSales                    decimal(18,3),
	IN decCreditSales                        decimal(18,3),
	IN decCreditPayment                      decimal(18,3),
	IN decCashInDrawer                       decimal(18,3),
	IN decTotalDisburse                      decimal(18,3),
	IN decCashDisburse                       decimal(18,3),
	IN decChequeDisburse                     decimal(18,3),
	IN decCreditCardDisburse                 decimal(18,3),
	IN decTotalWithhold                      decimal(18,3),
	IN decCashWithhold                       decimal(18,3),
	IN decChequeWithhold                     decimal(18,3),
	IN decCreditCardWithhold                 decimal(18,3),
	IN decTotalPaidOut                       decimal(18,3),
	IN decCashPaidOut                        decimal(18,3),
	IN decChequePaidOut                      decimal(18,3),
	IN decCreditCardPaidOut                  decimal(18,3),
	IN decBeginningBalance                   decimal(18,3),
	IN decVoidSales                          decimal(18,3),
	IN decRefundSales                        decimal(18,3),
	IN decItemsDiscount                      decimal(18,3),
	IN decSNRItemsDiscount                   decimal(18,3),
	IN decPWDItemsDiscount                   decimal(18,3),
	IN decOtherItemsDiscount                 decimal(18,3),
	IN decSubtotalDiscount                   decimal(18,3),
	IN intNoOfCashTransactions               int(10),
	IN intNoOfChequeTransactions             int(10),
	IN intNoOfCreditCardTransactions         int(10),
	IN intNoOfCreditTransactions             int(10),
	IN intNoOfCombinationPaymentTransactions int(10),
	IN intNoOfCreditPaymentTransactions      int(10),
	IN intNoOfClosedTransactions             int(10),
	IN intNoOfRefundTransactions             int(10),
	IN intNoOfVoidTransactions               int(10),
	IN intNoOfTotalTransactions              int(10),
	IN decCashCount                          decimal(18,3),
	IN dteLastLoginDate                      datetime,
	IN decTotalDeposit                       decimal(18,3),
	IN decCashDeposit                        decimal(18,3),
	IN decChequeDeposit                      decimal(18,3),
	IN decCreditCardDeposit                  decimal(18,3),
	IN decDebitPayment                       decimal(18,3),
	IN intNoOfDebitPaymentTransactions       int(10),
	IN decTotalCharge                        decimal(18,3),
	IN intIsCashCountInitialized             tinyint(1),
	IN intNoOfDiscountedTransactions         int(4),
	IN decNegativeAdjustments                decimal(18,3),
	IN intNoOfNegativeAdjustmentTransactions int(4),
	IN decPromotionalItems                   decimal(18,3),
	IN decCreditSalesTax                     decimal(18,3),
	IN decDebitDeposit                       decimal(18,3),
	IN decRewardPointsPayment                decimal(18,3),
	IN decRewardConvertedPayment             decimal(18,3),
	IN intNoOfRewardPointsPayment            int(10),
	IN decCreditPaymentCash                  decimal(18,3),
	IN decCreditPaymentCheque                decimal(18,3),
	IN decCreditPaymentCreditCard            decimal(18,3),
	IN decCreditPaymentDebit                 decimal(18,3),
	IN decRefundCash						decimal(18,3),
	IN decRefundCheque						decimal(18,3),
	IN decRefundCreditCard					decimal(18,3),
	IN decRefundCredit						decimal(18,3),
	IN decRefundDebit						decimal(18,3),

	IN intNoOfConsignmentTransactions  int(10),
	IN intNoOfConsignmentRefundTransactions  int(10),
	IN intNoOfWalkInTransactions  int(10),
	IN intNoOfWalkInRefundTransactions  int(10),
	IN intNoOfOutOfStockTransactions  int(10),
	IN intNoOfOutOfStockRefundTransactions  int(10),

	IN decConsignmentSales  decimal(18,3),
	IN decConsignmentRefundSales  decimal(18,3),
	IN decWalkInSales  decimal(18,3),
	IN decWalkInRefundSales  decimal(18,3),
	IN decOutOfStockSales  decimal(18,3),
	IN decOutOfStockRefundSales  decimal(18,3),

	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCashierReportHistoryID; END IF;
	
	IF EXISTS(SELECT CashierReportHistoryID FROM tblCashierReportHistory WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCashierReportHistory SET
			CashierID							= intCashierID,
			TerminalID							= intTerminalID,
			NetSales							= decNetSales,
			GrossSales							= decGrossSales,
			TotalDiscount						= decTotalDiscount,
			DailySales							= decDailySales,
			QuantitySold						= decQuantitySold,
			GroupSales							= decGroupSales,
			VAT									= decVAT,
			EVAT								= decEVAT,
			LocalTax							= decLocalTax,
			CashSales							= decCashSales,
			ChequeSales							= decChequeSales,
			CreditCardSales						= decCreditCardSales,
			CreditSales							= decCreditSales,
			CreditPayment						= decCreditPayment,
			CashInDrawer						= decCashInDrawer,
			TotalDisburse						= decTotalDisburse,
			CashDisburse						= decCashDisburse,
			ChequeDisburse						= decChequeDisburse,
			CreditCardDisburse					= decCreditCardDisburse,
			TotalWithhold						= decTotalWithhold,
			CashWithhold						= decCashWithhold,
			ChequeWithhold						= decChequeWithhold,
			CreditCardWithhold					= decCreditCardWithhold,
			TotalPaidOut						= decTotalPaidOut,
			CashPaidOut							= decCashPaidOut,
			ChequePaidOut						= decChequePaidOut,
			CreditCardPaidOut					= decCreditCardPaidOut,
			BeginningBalance					= decBeginningBalance,
			VoidSales							= decVoidSales,
			RefundSales							= decRefundSales,
			ItemsDiscount						= decItemsDiscount,
			SNRItemsDiscount					= decSNRItemsDiscount,
			PWDItemsDiscount					= decPWDItemsDiscount,
			OtherItemsDiscount					= decOtherItemsDiscount,
			SubtotalDiscount					= decSubtotalDiscount,
			NoOfCashTransactions				= intNoOfCashTransactions,
			NoOfChequeTransactions				= intNoOfChequeTransactions,
			NoOfCreditCardTransactions			= intNoOfCreditCardTransactions,
			NoOfCreditTransactions				= intNoOfCreditTransactions,
			NoOfCombinationPaymentTransactions  = intNoOfCombinationPaymentTransactions,
			NoOfCreditPaymentTransactions		= intNoOfCreditPaymentTransactions,
			NoOfClosedTransactions  = intNoOfClosedTransactions,
			NoOfRefundTransactions  = intNoOfRefundTransactions,
			NoOfVoidTransactions	= intNoOfVoidTransactions,
			NoOfTotalTransactions	= intNoOfTotalTransactions,
			CashCount				= decCashCount,
			LastLoginDate			= dteLastLoginDate,
			TotalDeposit			= decTotalDeposit,
			CashDeposit				= decCashDeposit,
			ChequeDeposit			= decChequeDeposit,
			CreditCardDeposit		= decCreditCardDeposit,
			DebitPayment			= decDebitPayment,
			NoOfDebitPaymentTransactions		= intNoOfDebitPaymentTransactions,
			TotalCharge				= decTotalCharge,
			IsCashCountInitialized  = intIsCashCountInitialized,
			NoOfDiscountedTransactions			= intNoOfDiscountedTransactions,
			NegativeAdjustments		= decNegativeAdjustments,
			NoOfNegativeAdjustmentTransactions  = intNoOfNegativeAdjustmentTransactions,
			PromotionalItems		= decPromotionalItems,
			CreditSalesTax			= decCreditSalesTax,
			DebitDeposit			= decDebitDeposit,
			RewardPointsPayment		= decRewardPointsPayment,
			RewardConvertedPayment  = decRewardConvertedPayment,
			NoOfRewardPointsPayment = intNoOfRewardPointsPayment,
			CreditPaymentCash		= decCreditPaymentCash,
			CreditPaymentCheque		= decCreditPaymentCheque,
			CreditPaymentCreditCard = decCreditPaymentCreditCard,
			CreditPaymentDebit		= decCreditPaymentDebit,
			RefundCash				= decRefundCash,
			RefundCheque			= decRefundCheque,
			RefundCreditCard		= decRefundCreditCard,
			RefundCredit			= decRefundCredit,
			RefundDebit				= decRefundDebit,

			NoOfConsignmentTransactions			=	intNoOfConsignmentTransactions,
			NoOfConsignmentRefundTransactions	=	intNoOfConsignmentRefundTransactions,
			NoOfWalkInTransactions				=	intNoOfWalkInTransactions,
			NoOfWalkInRefundTransactions		=	intNoOfWalkInRefundTransactions,
			NoOfOutOfStockTransactions			=	intNoOfOutOfStockTransactions,
			NoOfOutOfStockRefundTransactions	=	intNoOfOutOfStockRefundTransactions,

			ConsignmentSales					=	decConsignmentSales,
			ConsignmentRefundSales				=	decConsignmentRefundSales,
			WalkInSales							=	decWalkInSales,
			WalkInRefundSales					=	decWalkInRefundSales,
			OutOfStockSales						=	decOutOfStockSales,

			OutOfStockRefundTransactions		=	decOutOfStockRefundTransactions,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCashierReportHistory(BranchID, TerminalNo, SyncID, CashierID, TerminalID, NetSales, GrossSales, TotalDiscount, DailySales,
							QuantitySold, GroupSales, VAT, EVAT, LocalTax, CashSales, ChequeSales, CreditCardSales,
							CreditSales, CreditPayment, CashInDrawer, TotalDisburse, CashDisburse, ChequeDisburse,
							CreditCardDisburse, TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold,
							TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, BeginningBalance,
							VoidSales, RefundSales, ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, SubtotalDiscount, NoOfCashTransactions,
							NoOfChequeTransactions, NoOfCreditCardTransactions, NoOfCreditTransactions, 
							NoOfCombinationPaymentTransactions, NoOfCreditPaymentTransactions, NoOfClosedTransactions,
							NoOfRefundTransactions, NoOfVoidTransactions, NoOfTotalTransactions, CashCount, 
							LastLoginDate, TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, 
							DebitPayment, NoOfDebitPaymentTransactions, TotalCharge, IsCashCountInitialized,
							NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
							PromotionalItems, CreditSalesTax, DebitDeposit, RewardPointsPayment, RewardConvertedPayment,
							NoOfRewardPointsPayment, CreditPaymentCash, CreditPaymentCheque,
							CreditPaymentCreditCard, CreditPaymentDebit, 
							RefundCash, RefundCheque, RefundCreditCard, RefundCredit, RefundDebit, 
							NoOfConsignmentTransactions, NoOfConsignmentRefundTransactions, NoOfWalkInTransactions,
							NoOfWalkInRefundTransactions, NoOfOutOfStockTransactions, NoOfOutOfStockRefundTransactions,
							ConsignmentSales, ConsignmentRefundSales, WalkInSales,
							WalkInRefundSales, OutOfStockSales, OutOfStockRefundSales,
							CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intCashierID, intTerminalID, decNetSales, decGrossSales, decTotalDiscount, decDailySales,
							decQuantitySold, decGroupSales, decVAT, decEVAT, decLocalTax, decCashSales, decChequeSales, decCreditCardSales,
							decCreditSales, decCreditPayment, decCashInDrawer, decTotalDisburse, decCashDisburse, decChequeDisburse,
							decCreditCardDisburse, decTotalWithhold, decCashWithhold, decChequeWithhold, decCreditCardWithhold,
							decTotalPaidOut, decCashPaidOut, decChequePaidOut, decCreditCardPaidOut, decBeginningBalance,
							decVoidSales, decRefundSales, decItemsDiscount, decSNRItemsDiscount, decPWDItemsDiscount, decOtherItemsDiscount, decSubtotalDiscount, intNoOfCashTransactions,
							intNoOfChequeTransactions, intNoOfCreditCardTransactions, intNoOfCreditTransactions, 
							intNoOfCombinationPaymentTransactions, intNoOfCreditPaymentTransactions, intNoOfClosedTransactions,
							intNoOfRefundTransactions, intNoOfVoidTransactions, intNoOfTotalTransactions, decCashCount, 
							dteLastLoginDate, decTotalDeposit, decCashDeposit, decChequeDeposit, decCreditCardDeposit, 
							decDebitPayment, intNoOfDebitPaymentTransactions, decTotalCharge, intIsCashCountInitialized,
							intNoOfDiscountedTransactions, decNegativeAdjustments, intNoOfNegativeAdjustmentTransactions,
							decPromotionalItems, decCreditSalesTax, decDebitDeposit, decRewardPointsPayment, decRewardConvertedPayment,
							intNoOfRewardPointsPayment, decCreditPaymentCash, decCreditPaymentCheque,
							decCreditPaymentCreditCard, decCreditPaymentDebit, 
							decRefundCash, decRefundCheque, decRefundCreditCard, decRefundCredit, decRefundDebit, 
							intNoOfConsignmentTransactions, intNoOfConsignmentRefundTransactions, intNoOfWalkInTransactions,
							intNoOfWalkInRefundTransactions, intNoOfOutOfStockTransactions, intNoOfOutOfStockRefundTransactions,
							decConsignmentSales, decConsignmentRefundSales, decWalkInSales,
							decWalkInRefundSales, decOutOfStockSales, decOutOfStockRefundSales,
							dteCreatedOn, dteLastModified);

		UPDATE tblCashierReportHistory SET SyncID = CashierReportHistoryID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveDeposit
	Aug 2, 2014

	CALL procSaveDeposit(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveDeposit
GO

create procedure procSaveDeposit(	
	IN intBranchID     int(4),
	IN strTerminalNo   varchar(5),
	IN intSyncID	   bigint(20),
	IN intDepositID    bigint(20),
	In decAmount       decimal(18,2),
	IN intPaymentType  int(10),
	IN dteDateCreated  datetime,
	IN intCashierID    bigint(20),
	IN intContactID    bigint(20),
	IN strRemarks      varchar(255),
	IN strBranchCode   varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intDepositID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;

	IF EXISTS(SELECT DepositID FROM tblDeposit WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblDeposit SET
			Amount					= decAmount,
			PaymentType				= intPaymentType,
			DateCreated				= dteDateCreated,
			CashierID				= intCashierID,
			ContactID				= intContactID,
			Remarks					= strRemarks,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblDeposit(BranchID, TerminalNo, SyncID, Amount, PaymentType, DateCreated, 
							CashierID, ContactID, Remarks, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, decAmount, intPaymentType, dteDateCreated, 
							intCashierID, intContactID, strRemarks, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblDeposit SET SyncID = DepositID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveDisburse
	Aug 2, 2014

	CALL procSaveDisburse(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveDisburse
GO

create procedure procSaveDisburse(	
	IN intBranchID     int(4),
	IN strTerminalNo   varchar(5),
	IN intSyncID	   bigint(20),
	IN intDisburseID    bigint(20),
	In decAmount       decimal(18,2),
	IN intPaymentType  int(10),
	IN dteDateCreated  datetime,
	IN intCashierID    bigint(20),
	IN strRemarks      varchar(255),
	IN strBranchCode   varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intDisburseID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;

	IF EXISTS(SELECT DisburseID FROM tblDisburse WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblDisburse SET
			Amount					= decAmount,
			PaymentType				= intPaymentType,
			DateCreated				= dteDateCreated,
			CashierID				= intCashierID,
			Remarks					= strRemarks,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblDisburse(BranchID, TerminalNo, SyncID, Amount, PaymentType, DateCreated, 
							CashierID, Remarks, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, decAmount, intPaymentType, dteDateCreated, 
							intCashierID, strRemarks, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblDisburse SET SyncID = DisburseID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePaidOut
	Aug 2, 2014

	CALL procSavePaidOut(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePaidOut
GO

create procedure procSavePaidOut(	
	IN intBranchID     int(4),
	IN strTerminalNo   varchar(5),
	IN intSyncID	   bigint(20),
	IN intPaidOutID    bigint(20),
	In decAmount       decimal(18,2),
	IN intPaymentType  int(10),
	IN dteDateCreated  datetime,
	IN intCashierID    bigint(20),
	IN strRemarks      varchar(255),
	IN strBranchCode   varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intPaidOutID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;

	IF EXISTS(SELECT PaidOutID FROM tblPaidOut WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblPaidOut SET
			Amount					= decAmount,
			PaymentType				= intPaymentType,
			DateCreated				= dteDateCreated,
			CashierID				= intCashierID,
			Remarks					= strRemarks,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblPaidOut(BranchID, TerminalNo, SyncID, Amount, PaymentType, DateCreated, 
							CashierID, Remarks, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, decAmount, intPaymentType, dteDateCreated, 
							intCashierID, strRemarks, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblPaidOut SET SyncID = PaidOutID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;

/********************************************
	procSaveWithhold
	Aug 2, 2014

	CALL procSaveWithhold(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveWithhold
GO

create procedure procSaveWithhold(	
	IN intBranchID     int(4),
	IN strTerminalNo   varchar(5),
	IN intSyncID	   bigint(20),
	IN intWithholdID    bigint(20),
	In decAmount       decimal(18,2),
	IN intPaymentType  int(10),
	IN dteDateCreated  datetime,
	IN intCashierID    bigint(20),
	IN strRemarks      varchar(255),
	IN strBranchCode   varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intWithholdID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;

	IF EXISTS(SELECT WithholdID FROM tblWithhold WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblWithhold SET
			Amount					= decAmount,
			PaymentType				= intPaymentType,
			DateCreated				= dteDateCreated,
			CashierID				= intCashierID,
			Remarks					= strRemarks,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblWithhold(BranchID, TerminalNo, SyncID, Amount, PaymentType, DateCreated, 
							CashierID, Remarks, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, decAmount, intPaymentType, dteDateCreated, 
							intCashierID, strRemarks, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblWithhold SET SyncID = WithholdID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCashPayment
	Aug 2, 2014

	CALL procSaveCashPayment(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCashPayment
GO

create procedure procSaveCashPayment(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID	    bigint(20),
	IN intCashPaymentID bigint(20),
	IN intTransactionID bigint(20),
	IN decAmount        decimal(18,2),
	IN strRemarks       varchar(255),
	IN strTransactionNo varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCashPaymentID; END IF;
	
	IF EXISTS(SELECT CashPaymentID FROM tblCashPayment WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCashPayment SET
			Amount					= decAmount,
			Remarks					= strRemarks,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCashPayment(BranchID, TerminalNo, SyncID, TransactionID, Amount, Remarks, TransactionNo, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, CashPaymentID, intTransactionID, decAmount, strRemarks, strTransactionNo, dteCreatedOn, dteLastModified);

		UPDATE tblCashPayment SET SyncID = CashPaymentID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveChequePayment
	Aug 2, 2014

	CALL procSaveChequePayment(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveChequePayment
GO

create procedure procSaveChequePayment(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID	    bigint(20),
	IN intChequePaymentID bigint(20),
	IN intTransactionID bigint(20),
	IN strChequeNo		varchar(30),
	IN decAmount        decimal(18,2),
	IN dteValidityDate  datetime,
	IN strRemarks       varchar(255),
	IN strTransactionNo varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intChequePaymentID; END IF;
	
	IF EXISTS(SELECT ChequePaymentID FROM tblChequePayment WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblChequePayment SET
			ChequeNo				= ChequeNo,
			Amount					= decAmount,
			ValidityDate			= dteValidityDate,
			Remarks					= strRemarks,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblChequePayment(BranchID, TerminalNo, SyncID, TransactionID, ChequeNo, Amount, ValidityDate, Remarks, TransactionNo, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intTransactionID, strChequeNo, decAmount, dteValidityDate, strRemarks, strTransactionNo, dteCreatedOn, dteLastModified);

		UPDATE tblChequePayment SET SyncID = ChequePaymentID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCreditCardPayment
	Aug 2, 2014

	CALL procSaveCreditCardPayment(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCreditCardPayment
GO

create procedure procSaveCreditCardPayment(	
	IN intBranchID			  int(4),
	IN strTerminalNo		  varchar(5),
	IN intSyncID			  bigint(20),
	IN intCreditCardPaymentID bigint(20),
	IN intTransactionID		  bigint(20),
	IN dteTransactionDate	  datetime,
	IN strCashierName         varchar(150),
	IN decAmount			  decimal(18,3),
	IN decAdditionalCharge	  decimal(18,3),
	IN intContactID			  bigint(20),
	IN intGuarantorID		  bigint(20),
	IN intCardTypeID          int(10),
	IN strCardTypeCode        varchar(30),
	IN strCardTypeName        varchar(30),
	IN strCardNo              varchar(30),
	IN strCardHolder          varchar(150),
	IN strValidityDates       varchar(14),
	IN strRemarks			  varchar(255),
	IN strTransactionNo		  varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCreditCardPaymentID; END IF;
	
	IF EXISTS(SELECT CreditCardPaymentID FROM tblCreditCardPayment WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCreditCardPayment SET
			Amount					= decAmount,
			AdditionalCharge		= decAdditionalCharge,
			ContactID				= intContactID,
			GuarantorID				= intGuarantorID,
			CardTypeID				= intCardTypeID,
			CardTypeCode			= strCardTypeCode,
			CardTypeName			= strCardTypeName,
			CardNo					= strCardNo,
			CardHolder				= strCardHolder,
			ValidityDates			= strValidityDates,
			Remarks					= strRemarks,
			TransactionNo			= strTransactionNo,
			TransactionDate			= dteTransactionDate,
			CashierName				= strCashierName,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCreditCardPayment(BranchID, TerminalNo, SyncID, TransactionID, TransactionDate, CashierName, Amount, AdditionalCharge, ContactID, GuarantorID, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, Remarks, TransactionNo, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intTransactionID, dteTransactionDate, strCashierName, decAmount, decAdditionalCharge, intContactID, intGuarantorID, intCardTypeID, strCardTypeCode, strCardTypeName, strCardNo, strCardHolder, strValidityDates, strRemarks, strTransactionNo, dteCreatedOn, dteLastModified);

		UPDATE tblCreditCardPayment SET SyncID = CreditCardPaymentID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCreditPaymentCash
	Oct 28, 2014

	CALL procSaveCreditPaymentCash(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCreditPaymentCash
GO

create procedure procSaveCreditPaymentCash(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID	    bigint(20),
	IN intCreditPaymentCashID bigint(20),
	IN intCreditPaymentID	bigint(20),
	IN intCPRefBranchID      int(4),
	IN strCPRefTerminalNo    varchar(5),
	IN intTransactionID bigint(20),
	IN decAmount        decimal(18,2),
	IN strRemarks       varchar(255),
	IN strTransactionNo varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCreditPaymentCashID; END IF;
	
	IF EXISTS(SELECT CreditPaymentCashID FROM tblCreditPaymentCash WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCreditPaymentCash SET
			Amount					= decAmount,
			Remarks					= strRemarks,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCreditPaymentCash(BranchID, TerminalNo, SyncID, CreditPaymentID, CPRefBranchID, CPRefTerminalNo, TransactionID, Amount, Remarks, TransactionNo, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, CreditPaymentCashID, intCreditPaymentID, intCPRefBranchID, strCPRefTerminalNo, intTransactionID, decAmount, strRemarks, strTransactionNo, dteCreatedOn, dteLastModified);

		UPDATE tblCreditPaymentCash SET SyncID = CreditPaymentCashID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCreditPaymentCheque
	Oct 26, 2014

	CALL procSaveCreditPaymentCheque(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Desc: save the cheques use to pay creditpayment [utang]
	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCreditPaymentCheque
GO

create procedure procSaveCreditPaymentCheque(	
	IN intBranchID      int(4),
	IN strTerminalNo    varchar(5),
	IN intSyncID	    bigint(20),
	IN intCreditPaymentChequeID bigint(20),
	IN intCreditPaymentID	bigint(20),
	IN intCPRefBranchID      int(4),
	IN strCPRefTerminalNo    varchar(5),
	IN intTransactionID bigint(20),
	IN strChequeNo		varchar(30),
	IN decAmount        decimal(18,2),
	IN dteValidityDate  datetime,
	IN strRemarks       varchar(255),
	IN strTransactionNo varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCreditPaymentChequeID; END IF;
	
	IF EXISTS(SELECT CreditPaymentChequeID FROM tblCreditPaymentCheque WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCreditPaymentCheque SET
			ChequeNo				= ChequeNo,
			Amount					= decAmount,
			ValidityDate			= dteValidityDate,
			Remarks					= strRemarks,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCreditPaymentCheque(BranchID, TerminalNo, SyncID, CreditPaymentID, CPRefBranchID, CPRefTerminalNo, TransactionID, ChequeNo, Amount, ValidityDate, Remarks, TransactionNo, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intCreditPaymentID, intCPRefBranchID, strCPRefTerminalNo, intTransactionID, strChequeNo, decAmount, dteValidityDate, strRemarks, strTransactionNo, dteCreatedOn, dteLastModified);

		UPDATE tblCreditPaymentCheque SET SyncID = CreditPaymentChequeID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveCashCount
	Aug 2, 2014

	CALL procSaveCashCount(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: SyncID is the same as the auto_increment ID in the local table.
	      It is only used to copy local db transactions to master db.
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveCashCount
GO

create procedure procSaveCashCount(	
	IN intBranchID			 int(4),
	IN strTerminalNo		 varchar(5),
	IN intSyncID			 bigint(20),
	IN intCashCountID		 bigint(20),
	IN intCashierID          bigint(20),
	IN strCashierName        varchar(100),
	IN dteDateCreated        datetime,
	IN intDenominationID     bigint(20),
	IN decDenominationValue	 decimal(18,2),
	IN intDenominationCount  int(10),
	IN decDenominationAmount decimal(18,2),
	IN strBranchCode         varchar(30),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	IF (intSyncID = 0) THEN SET intSyncID = intCashCountID; END IF;
	IF (strBranchCode = '') THEN  SET strBranchCode = (SELECT BranchCode FROM tblBranch WHERE BranchID = intBranchID LIMIT 1); END IF;

	IF EXISTS(SELECT CashCountID FROM tblCashCount WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID) THEN 
		UPDATE tblCashCount SET
			CashierID				= intCashierID,
			CashierName				= strCashierName,
			DenominationID			= intDenominationID,
			DenominationValue		= decDenominationValue,
			DenominationCount		= intDenominationCount,
			DenominationAmount		= decDenominationAmount,
			BranchCode				= strBranchCode,
			LastModified			= dteLastModified
		WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = intSyncID;

	ELSE
		INSERT INTO tblCashCount(BranchID, TerminalNo, SyncID, CashierID, CashierName, DateCreated, DenominationID, DenominationValue, DenominationCount, DenominationAmount, BranchCode, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intSyncID, intCashierID, strCashierName, dteDateCreated, intDenominationID, decDenominationValue, intDenominationCount, decDenominationAmount, strBranchCode, dteCreatedOn, dteLastModified);

		UPDATE tblCashCount SET SyncID = CashCountID WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND SyncID = 0;
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSavePLUReport
	Sep 2, 2014

	CALL procSavePLUReport(2,'01',1,0,1,NOW(),7,8,9,10,now(), now());

	Note: This is a temp table use for generation of PLU Report
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSavePLUReport
GO

create procedure procSavePLUReport(	
	IN intBranchID		   int(4),
	IN strTerminalNo	   varchar(5),
	IN intPLUReportID	   bigint(20),
	IN intProductID        bigint(20),
	IN strProductCode	   varchar(500),
	IN strProductGroup	   varchar(50),
	IN decQuantity         decimal(18,3),
	IN decAmount           decimal(18,3),
	IN intOrderSlipPrinter tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('0001-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;

	INSERT INTO tblPLUReport(BranchID, TerminalNo, ProductID, ProductCode, ProductGroup, Quantity, Amount, OrderSlipPrinter, CreatedOn, LastModified)
			VALUES(intBranchID, strTerminalNo, intProductID, strProductCode, strProductGroup, decQuantity, decAmount, intOrderSlipPrinter, dteCreatedOn, dteLastModified);
				
END;
GO
delimiter ;

