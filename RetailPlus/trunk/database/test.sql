

/**************************************************************

	procContactSelect
	Lemuel E. Aceron
	Sep 15, 2013
	
	Desc: This will get the all information of a contact

	CALL procContactSelect(1, 0, null, null, null, null, null, 0, 0, '1900-01-01', '1900-01-01', '1900-01-01', '1900-01-01', 0, 0, 0, 'ContactID','');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSelect
GO

create procedure procContactSelect(
			IN ContactGroupCategory  int,
			IN ContactID  bigint,
			IN ContactCode varchar(50),
			IN ContactName varchar(75),
			IN ContactGroupCode varchar(30),
			IN RewardCardNo varchar(30),
			IN Name varchar(30),
			IN BirthMonth int,
			IN AnniversaryMonth int,
			IN BirthDateFrom varchar(30),
			IN BirthDateTo varchar(30),
			IN AnniversaryDateFrom datetime,
			IN AnniversaryDateTo datetime,
			IN hasCreditOnly tinyint(1),
			IN intDeleted int,
			IN lngLimit int,
			IN SortField varchar(60),
			IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 cntct.ContactID
							,cntct.ContactCode ,cntct.ContactName ,cntct.BusinessName
							,grp.ContactGroupID ,grp.ContactGroupName
							,ModeOfTerms ,cntct.Terms ,cntct.Address
							,TelephoneNo ,cntct.Remarks ,cntct.Debit ,cntct.Credit ,cntct.CreditLimit ,cntct.IsCreditAllowed
							,DateCreated ,cntct.Deleted ,dept.DepartmentID ,dept.DepartmentName
							,pos.PositionID ,pos.PositionName ,cntct.isLock
							,IFNULL(LastName,'''') LastName ,IFNULL(Middlename,'''') Middlename ,IFNULL(FirstName,'''') FirstName ,IFNULL(Spousename,'''') Spousename
							,SpouseBirthDate ,AnniversaryDate
							,Address1 ,Address2 ,City ,State ,ZipCode ,IFNULL(cntry.CountryID,0) CountryID ,CountryName
							,BusinessPhoneNo ,HomePhoneNo ,MobileNo ,FaxNo ,EmailAddress 
							,RewardCardNo ,RewardActive, RewardPoints, RewardAwardDate, TotalPurchases, RedeemedPoints, RewardCardStatus, ExpiryDate
							,IFNULL(addon.BirthDate,rwrd.BirthDate) BirthDate 
						FROM tblContacts cntct
							INNER JOIN tblContactGroup grp ON cntct.ContactGroupID = grp.ContactGroupID
							INNER JOIN tblDepartments dept ON cntct.DepartmentID = dept.DepartmentID
							INNER JOIN tblPositions pos ON cntct.PositionID = pos.PositionID
							LEFT OUTER JOIN tblContactAddOn addon ON addon.ContactID = cntct.ContactID
							LEFT OUTER JOIN tblContactRewards rwrd ON rwrd.CustomerID = cntct.ContactID
							LEFT OUTER JOIN tblCountry cntry ON addon.CountryID = cntry.CountryID
						WHERE 1=1 ');

	IF intDeleted <> -1 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.deleted = ',intDeleted,' ');
	END IF;
	IF hasCreditOnly = true THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.Credit > 0 ');
	END IF;

	IF ContactGroupCategory = 1 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 1 OR ContactGroupCategory = 3) ');
	ELSEIF ContactGroupCategory = 2 THEN -- Supplier Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 2 OR ContactGroupCategory = 3) ');
	ELSEIF ContactGroupCategory = 4 THEN -- Agent Group
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCategory = 4) ');
	END IF;

	IF ContactID <> 0 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.ContactID = ',ContactID,' ');
	ELSEIF IFNULL(ContactCode,'') <> '' AND IFNULL(ContactName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (ContactCode LIKE ''%',ContactCode,'%'' OR ContactName LIKE ''%',ContactName,'%'') ');
	ELSEIF IFNULL(ContactCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND ContactCode LIKE ''%',ContactCode,'%'' ');
	ELSEIF IFNULL(ContactName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND ContactName LIKE ''%',ContactName,'%'' ');
	ELSEIF IFNULL(Name,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (LastName LIKE ''%',Name,'%'' OR MiddleName LIKE ''%',Name,'%'' OR FirstName LIKE ''%',Name,'%'') ');
	END IF;

	IF IFNULL(ContactGroupCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCode LIKE ''%',ContactGroupCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '	  OR ContactGroupName LIKE ''%',ContactGroupCode,'%'') ');
	END IF;

	-- Added 10Oct2013 
	IF IFNULL(BirthMonth,0) <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND (DATE_FORMAT(IFNULL(IFNULL(addon.BirthDate,rwrd.BirthDate),0),''%m'') = ''',BirthMonth,''' ');
		SET @SQL = CONCAT(@SQL, '	  OR DATE_FORMAT(IFNULL(addon.SpouseBirthDate,0),''%m'') = ''',BirthMonth,''') ');
	END IF;

	IF IFNULL(AnniversaryMonth,0) <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(IFNULL(addon.AnniversaryDate,0),''%m'') = ''',AnniversaryMonth,''' ');
	END IF;

	
	SET BirthDateFrom = IF(NOT ISNULL(BirthDateFrom), BirthDateFrom, '1900-01-01');
	IF DATE_FORMAT(BirthDateFrom, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND (IFNULL(addon.BirthDate,rwrd.BirthDate) >= ''',DATE_FORMAT(BirthDateFrom, '%Y-%m-%d'),''' ');
		SET @SQL = CONCAT(@SQL, '	  OR SpouseBirthDate >= ''',DATE_FORMAT(BirthDateFrom, '%Y-%m-%d'),''') ');
	END IF;

	SET BirthDateTo = IF(NOT ISNULL(BirthDateTo), BirthDateTo, '1900-01-01');
	IF DATE_FORMAT(BirthDateTo, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND (IFNULL(addon.BirthDate,rwrd.BirthDate) <= ''',DATE_FORMAT(BirthDateTo, '%Y-%m-%d'),''' ');
		SET @SQL = CONCAT(@SQL, '	  OR SpouseBirthDate <= ''',DATE_FORMAT(BirthDateTo, '%Y-%m-%d'),''') ');
	END IF;
	
	SET AnniversaryDateFrom = IF(NOT ISNULL(AnniversaryDateFrom), AnniversaryDateFrom, '1900-01-01');
	IF DATE_FORMAT(AnniversaryDateFrom, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND addon.AnniversaryDate >= ''',DATE_FORMAT(AnniversaryDateFrom, '%Y-%m-%d'),''' ');
	END IF;

	SET AnniversaryDateTo = IF(NOT ISNULL(AnniversaryDateTo), AnniversaryDateTo, '1900-01-01');
	IF DATE_FORMAT(AnniversaryDateTo, '%Y-%m-%d') <> '1900-01-01' THEN
		SET @SQL = CONCAT(@SQL, 'AND addon.AnniversaryDate <= ''',DATE_FORMAT(AnniversaryDateTo, '%Y-%m-%d'),''' ');
	END IF;
	
		
	

	SET @SortOrder = IF(IFNULL(SortOrder,'')='','ASC ',SortOrder);
	SELECT @SortOrder;
	
	-- SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','ContactCode, ContactName, LastName',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SELECT @SQL;
	SET @SQL = CONCAT(@SQL,IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));


	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;
