DROP TABLE SM_USR;

CREATE TABLE SM_USR(
	USRKEY VARCHAR(255),
	PASSWORD VARCHAR(255),
	NAME VARCHAR(255),
	INIT VARCHAR(255),
	USTYPE VARCHAR(255),
	UEAN13 VARCHAR(255),
	S0 VARCHAR(255),
	S1 VARCHAR(255),
	S2 VARCHAR(255),
	S3 VARCHAR(255),
	S4 VARCHAR(255),
	S5 VARCHAR(255),
	S6 VARCHAR(255),
	S7 VARCHAR(255),
	S8 VARCHAR(255),
	S9 VARCHAR(255),
	S10 VARCHAR(255),
	S11 VARCHAR(255),
	S12 VARCHAR(255),
	PINNUM VARCHAR(255),
	TREGNO VARCHAR(255),
	CMTREG VARCHAR(255),
	CMAMNT VARCHAR(255),
	CMCOUN VARCHAR(255),
	CMVLAN VARCHAR(255),
	CMVLCT VARCHAR(255),
	CMVTAM VARCHAR(255),
	CMVTCT VARCHAR(255),
	PRCENT VARCHAR(255),
	USERID VARCHAR(255),
	USRDAT VARCHAR(255),
	USRFUN VARCHAR(255),
	STATUS VARCHAR(255)
);

CREATE INDEX IX_SM_USR ON SM_USR(USRKEY);
CREATE INDEX IX1_SM_USR ON SM_USR(NAME);
CREATE INDEX IX2_SM_USR ON SM_USR(USTYPE);
CREATE INDEX IX3_SM_USR ON SM_USR(UEAN13);

DROP TABLE MP_SUP ;

CREATE TABLE MP_SUP (
	SURKEY VARCHAR(255),
	SUDESC VARCHAR(255),
	SUADR1 VARCHAR(255),
	SUADR2 VARCHAR(255),
	SUREP1 VARCHAR(255),
	SUREP2 VARCHAR(255),
	SUTYPE VARCHAR(255),
	SUPONE VARCHAR(255),
	SUFACS VARCHAR(255),
	SUCELL VARCHAR(255),
	SUTERM VARCHAR(255),
	SUTRM2 VARCHAR(255),
	SURENT VARCHAR(255),
	STATUS VARCHAR(255)
);
CREATE INDEX IX_MP_SUP ON MP_SUP (SURKEY);


DROP TABLE MP_PAC ;

CREATE TABLE MP_PAC (
	PARKEY VARCHAR(255),
	PADESC VARCHAR(255)
);
CREATE INDEX IX_MP_PAC ON MP_PAC (PARKEY);

DROP TABLE MP_CLS ;

CREATE TABLE MP_CLS (
	CLRKEY VARCHAR(255),
	CLDESC VARCHAR(255)
);
CREATE INDEX IX_MP_CLS ON MP_CLS (CLRKEY);

DROP TABLE MP_MER;

CREATE TABLE MP_MER (
	OLDEAN NVARCHAR(255),
	MERKEY NVARCHAR(255),
	MEANCS NVARCHAR(255),
	MEANBX NVARCHAR(255),
	MEAN13 NVARCHAR(255),
	MEDESC NVARCHAR(255),
	MECLAS NVARCHAR(255),
	MEADOC NVARCHAR(255),
	MEBRAC NVARCHAR(255),
	MEVATA NVARCHAR(255),
	MENVAT NVARCHAR(255),
	MEMSEQ NVARCHAR(255),
	MEPOSD NVARCHAR(255),
	MEPCK1 NVARCHAR(255),
	MEQTY1 NVARCHAR(255),
	MECOS0 NVARCHAR(255),
	MEPACK NVARCHAR(255),
	MEMUCH NVARCHAR(255),
	MEPCK3 NVARCHAR(255),
	MECOS1 NVARCHAR(255),
	MECOS2 NVARCHAR(255),
	MEDIS0 NVARCHAR(255),
	MEDIS1 NVARCHAR(255),
	MEDIS2 NVARCHAR(255),
	MEDIS3 NVARCHAR(255),
	MEEVAT NVARCHAR(255),
	MEOCHG NVARCHAR(255),
	MEXPEN NVARCHAR(255),
	MEMK12 NVARCHAR(255),
	MEMK22 NVARCHAR(255),
	MERET2 NVARCHAR(255),
	ME2DRT NVARCHAR(255),
	MERDI2 NVARCHAR(255),
	MEMK1R NVARCHAR(255),
	MEMK2R NVARCHAR(255),
	MERETP NVARCHAR(255),
	MERDRT NVARCHAR(255),
	MERDIS NVARCHAR(255),
	MERETU NVARCHAR(255),
	MERETD NVARCHAR(255),
	MEMK1W NVARCHAR(255),
	MEMK2W NVARCHAR(255),
	MEWHOP NVARCHAR(255),
	MEWDRT NVARCHAR(255),
	MEWDIS NVARCHAR(255),
	MEWHOU NVARCHAR(255),
	MEWHOD NVARCHAR(255),
	MEMINI NVARCHAR(255),
	MEMAKR NVARCHAR(255),
	MEMAKW NVARCHAR(255),
	CLRKEY NVARCHAR(255),
	SURKEY NVARCHAR(255),
	SUSTOK NVARCHAR(255),
	SUSTK1 NVARCHAR(255),
	SUSTK2 NVARCHAR(255),
	SURKY2 NVARCHAR(255),
	USDLAR NVARCHAR(255),
	USPESO NVARCHAR(255),
	OTHERS NVARCHAR(255),
	MOPCK1 NVARCHAR(255),
	MOQTY1 NVARCHAR(255),
	MOCOS0 NVARCHAR(255),
	MOXPEN NVARCHAR(255),
	MOPACK NVARCHAR(255),
	MOMUCH NVARCHAR(255),
	MOCOS1 NVARCHAR(255),
	MOPCK3 NVARCHAR(255),
	MEPRIM NVARCHAR(255),
	MOCOS2 NVARCHAR(255),
	BARCD1 NVARCHAR(255),
	BARCD2 NVARCHAR(255),
	BARCD3 NVARCHAR(255),
	BARCD4 NVARCHAR(255),
	GNETPT NVARCHAR(255),
	BARCD5 NVARCHAR(255),
	USERID NVARCHAR(255),
	USRDAT NVARCHAR(255),
	USRFUN NVARCHAR(255),
	STATUS NVARCHAR(255)
);

ALTER TABLE MP_MER ADD isProcessed TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE MP_MER ADD MP_MER_ID BIGINT(20) NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY (MP_MER_ID);
CREATE INDEX IX_MP_MER ON MP_MER (MEDESC);
CREATE INDEX IX1_MP_MER ON MP_MER (MEPCK3);
CREATE INDEX IX2_MP_MER ON MP_MER (MEPACK);
CREATE INDEX IX3_MP_MER ON MP_MER (MEPCK1);
CREATE INDEX IX4_MP_MER ON MP_MER (CLRKEY);
CREATE INDEX IX5_MP_MER ON MP_MER (SURKEY);


/********************************************
	procHPInsert
	
	CALL procHPInsert;

********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procHPInsert
GO

create procedure procHPInsert()
BEGIN

	-- insert the users
	DELETE FROM sysAccessUserDetails WHERE UID IN (SELECT DISTINCT UID FROM SM_USR INNER JOIN sysAccessUsers usr ON usr.UserName = SM_USR.UEAN13);
	DELETE FROM sysAccessUsers WHERE UserName IN (SELECT DISTINCT UEAN13 FROM SM_USR);

	INSERT INTO sysAccessUsers(UserName, Password, DateCreated, CreatedOn, LastModified)
	SELECT UEAN13 UserName, UEAN13 Password, NOW(), NOW(), NOW()
	FROM SM_USR
	WHERE UEAN13 NOT IN (SELECT DISTINCT UserName FROM sysAccessUsers);

	INSERT INTO sysAccessUserDetails(UID, Name, Address1, Address2, City, State, Zip, CountryID, EmailAddress,
									 GroupID, PageSize, CreatedOn, LastModified)
	SELECT usr.UID, SM_USR.Name, '' Address1, '' Address2, '' City, '' State, '' Zip, 1 CountryID, '' EmailAddress,
									 CASE 
										WHEN USTYPE = 1 THEN 1
										WHEN USTYPE = 2 THEN 2
										WHEN USTYPE = 3 THEN 3
										WHEN USTYPE = 4 THEN 12
										WHEN USTYPE = 5 THEN 4
										WHEN USTYPE = 6 THEN 13
										WHEN USTYPE = 7 THEN 14
									 END GroupID, 10, NOW(), NOW()
	FROM SM_USR
	INNER JOIN sysAccessUsers usr ON usr.UserName = SM_USR.UEAN13
	WHERE UID NOT IN (SELECT DISTINCT UID FROM sysAccessUserDetails)
		AND NAME NOT IN (SELECT DISTINCT Name FROM sysAccessUserDetails);

	-- insert the suppliers
	-- DELETE FROM tblContacts WHERE ContactCode IN (SELECT DISTINCT CONCAT('SUP:',SURKEY) FROM MP_SUP) AND ContactGroupID = 2;
	-- DELETE FROM tblContacts WHERE ContactName IN (SELECT DISTINCT SUDESC FROM MP_SUP) AND ContactGroupID = 2;
	
	INSERT INTO tblContacts (ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms 
			,Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit 
			,CreditLimit ,IsCreditAllowed ,DateCreated ,DepartmentID ,PositionID)
	SELECT DISTINCT CONCAT('SUP:',SURKEY) AS ContactCode, SUDESC AS ContactName, 2, 0, 0
			,CONCAT(SUADR1, ' ',SUADR2) AS Address, CONCAT(SUREP1, ';',SUREP2) AS BusinessName, SUPONE AS TelephoneNo, CONCAT('fax:',SUFACS,' cell:',SUCELL, ' terms:',SUTERM) AS Remarks, 0, 0
			,0 ,0 ,NOW(), 1, 1
	FROM MP_SUP
	WHERE CONCAT('SUP:',SURKEY) NOT IN (SELECT DISTINCT ContactCode FROM tblContacts WHERE ContactGroupID = 2);

	-- insert the unit
	INSERT INTO tblUnit(UnitCode, UnitName, CreatedOn, LastModified)	
	SELECT DISTINCT PARKEY, PADESC, NOW(), NOW()
	FROM MP_PAC
	WHERE PARKEY NOT IN (SELECT DISTINCT UnitCode FROM tblUnit);

	-- insert the groups
	INSERT INTO tblProductGroup(ProductGroupCode, ProductGroupName, BaseUnitID, VAT, CreatedOn, LastModified)
	SELECT DISTINCT CLRKEY, CLDESC, 1, 12, NOW(), NOW()
	FROM MP_CLS WHERE CLRKEY NOT IN (SELECT DISTINCT ProductGroupCode FROM tblProductGroup);

	-- insert the subgroups
	INSERT INTO tblProductSubGroup(ProductGroupID, ProductSubGroupCode, ProductSubGroupName, BaseUnitID, VAT, CreatedOn, LastModified)
	SELECT DISTINCT grp.ProductGroupID, CLRKEY, CLDESC, 1, 12, NOW(), NOW()
	FROM MP_CLS 
	INNER JOIN tblProductGroup grp ON grp.ProductGroupCode = MP_CLS.CLRKEY 
	WHERE CLRKEY NOT IN (SELECT DISTINCT ProductSubGroupCode FROM tblProductSubGroup);

	-- SUSTOK = barcode1
	-- SUSTK2 = barcode2
	-- SUSTK1 = wholesale Barcode
	-- insert the merchandise 
	-- this is faster than join the units, subgroups and contacts
	SELECT NOW() AS starttime;
	INSERT INTO tblProducts(ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, IncludeInSubtotalDiscount,
							SupplierID, IsItemSold, Active, CreatedOn, LastModified, SequenceNo)
	SELECT MEDESC AS ProductCode, MEDESC AS ProductDesc, sgrp.ProductSubGroupID, unit.UnitID, NOW(), 1,
							cntct.ContactID SupplierID, 1 IsItemSold, 1 Active, NOW(), NOW(), MERKEY AS SequenceNo -- use the SequenceNo as container for MERKEY as reference to old system
	FROM MP_MER 
	INNER JOIN tblUnit unit ON unit.UnitCode = MP_MER.MEPCK3 -- MEPCK3 is the main unit
	INNER JOIN tblProductSubGroup sgrp ON sgrp.ProductSubGroupCode = MP_MER.CLRKEY 
	INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('SUP:',MP_MER.SURKEY )
	LEFT OUTER JOIN tblProducts prd ON prd.ProductCode = MP_MER.MEDESC
	WHERE IFNULL(prd.ProductCode, '') = '';
	-- WHERE MEDESC NOT IN (SELECT DISTINCT ProductCode FROM tblProducts);
	SELECT NOW() AS endtime;
	
	-- DELETE FROM tblProductPackage WHERE ProductID IN (SELECT DISTINCT ProductID 
	--												  FROM MP_MER
	--												  INNER JOIN tblProducts prd ON prd.ProductCode = MP_MER.MEDESC);
													  
	-- DELETE FROM tblProductPackage WHERE ProductID IN (SELECT DISTINCT ProductID FROM tblProducts WHERE SequenceNo > 1);

	DELETE FROM tblProductPackage WHERE Barcode4 IN (SELECT DISTINCT CONCAT('9999',MERKEY) FROM MP_MER);

	INSERT INTO tblProductPackage(ProductID, UnitID, Price, PurchasePrice, Quantity, VAT, 
								  WSPrice, 
								  Barcode1, Barcode2, Barcode3, Barcode4, CreatedOn, LastModified)
	SELECT prd.ProductID, prd.BaseUnitID, MP_MER.MERETP Price, MP_MER.MECOS2 PurchasePrice, 1 AS Quantity, 12 AS VAT,
								  MP_MER.MECOS2 WSPrice, 
								  CASE WHEN IFNULL(MP_MER.SUSTOK,'') <> '' THEN MP_MER.SUSTOK
									   WHEN IFNULL(MP_MER.MEAN13,'') <> '' THEN MP_MER.MEAN13
									   ELSE CONCAT('400000',MERKEY)
								  END Barcode1, MP_MER.SUSTK2 Barcode2, MP_MER.BARCD1 Barcode3, CONCAT('9999',MERKEY) Barcode4, NOW(), NOW()
	FROM MP_MER
	INNER JOIN tblProducts prd ON prd.ProductCode = MP_MER.MEDESC
	WHERE prd.ProductID NOT IN (SELECT DISTINCT ProductID FROM tblProductPackage);

	-- REMARKS!!!!!!!!!!
	-- 
	-- DO NOT FORGET TO update the last digit of barcode1 
	-- 
	UPDATE tblProductPackage,
	(
		SELECT prd.ProductID, prd.BaseUnitID, MP_MER.MERETP Price, MP_MER.MECOS2 PurchasePrice, 1 AS Quantity, 12 AS VAT,
								  MP_MER.MECOS2 WSPrice, 
								  CASE WHEN IFNULL(MP_MER.SUSTOK,'') <> '' THEN MP_MER.SUSTOK
									   WHEN IFNULL(MP_MER.MEAN13,'') <> '' THEN MP_MER.MEAN13
									   ELSE CONCAT('400000',MERKEY)
								  END Barcode1, MP_MER.SUSTK2 Barcode2, MP_MER.BARCD1 Barcode3, CONCAT('9999',MERKEY) Barcode4
		FROM MP_MER
		INNER JOIN tblProducts prd ON prd.ProductCode = MP_MER.MEDESC
	) prd
	SET
		tblProductPackage.Price = prd.Price,
		tblProductPackage.PurchasePrice = prd.PurchasePrice,
		tblProductPackage.WSPrice = prd.WSPrice
	WHERE prd.ProductID = tblProductPackage.ProductID 
		AND prd.Quantity = tblProductPackage.Quantity 
		AND prd.Barcode1 = tblProductPackage.Barcode1 
		AND prd.Barcode2 = tblProductPackage.Barcode2
		AND prd.Barcode3 = tblProductPackage.Barcode3 
		AND prd.Barcode4 = tblProductPackage.Barcode4;
	
	/**** INSERT THE PRODUCTS INTO POS 
	
	INSERT INTO tblProducts(ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, IncludeInSubtotalDiscount,
							SupplierID, IsItemSold, Active, CreatedOn, LastModified, SequenceNo)


	SELECT prd.ProductCode, prd.ProductDesc, prd.ProductSubGroupID, prd.BaseUnitID, prd.DateCreated, prd.IncludeInSubtotalDiscount,
							prd.SupplierID, prd.IsItemSold, prd.Active, prd.CreatedOn, prd.LastModified, prd.SequenceNo
	FROM poshp.tblProducts prd
	INNER JOIN poshp.tblContacts cntct ON cntct.ContactID = prd.SupplierID
	WHERE ProductCode NOT IN (SELECT DISTINCT ProductCode FROM pos.tblProducts)
	AND ContactName = 'KAIZAN TRADING INC.';

	select contactid, contactcode, contactname from poshp.tblcontacts where contactgroupid = 2 and contactid > 10 and length(contactcode) > 4 and contactcode not in (select distinct supplierid from tblproducts) limit 10;

	select contactid, contactcode, contactname from poshp.tblcontacts where contactgroupid = 2 and contactid > 10 and length(contactcode) > 4 and contactcode in (select distinct supplierid from tblproducts) limit 10;
	select * from tblcontacts where contactname = 'FOCUS BRAND SALES & DIST. INC';
	****/


END;
GO
delimiter ;