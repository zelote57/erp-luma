
/*****************************Chart Of Accounts**************************/


/********************************************
	procSaveBank
	Aug 2, 2014

	CALL procSaveBank(5,2,3,4,5,now(), now());

********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveBank
GO

create procedure procSaveBank(	
	IN intBankID        int(5),
	IN strBankCode      varchar(10),
	IN strBankName      varchar(50),
	IN strChequeCode    varchar(5),
	IN strChequeCounter varchar(20),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT BankID FROM tblBank WHERE BankID = intBankID) THEN 
		UPDATE tblBank SET
			BankCode				= strBankCode,
			BankName				= strBankName,
			ChequeCode				= strChequeCode,
			ChequeCounter			= strChequeCounter,
			CreatedOn				= dteCreatedOn,
			LastModified			= dteLastModified
		WHERE BankID				= intBankID;
	ELSE
		INSERT INTO tblBank(BankID, BankCode, BankName, ChequeCode, ChequeCounter, CreatedOn, LastModified)
			VALUES(intBankID, strBankCode, strBankName, strChequeCode, strChequeCounter, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveAccountClassification
	Aug 2, 2014

	CALL procSaveAccountClassification(1000,2,3,1,now(), now());

********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveAccountClassification
GO

create procedure procSaveAccountClassification(	
	IN intAccountClassificationID        int(4),
	IN strAccountClassificationCode      varchar(30),
	IN strAccountClassificationName      varchar(50),
	IN intAccountClassificationType		 tinyint(1),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT AccountClassificationID FROM tblAccountClassification WHERE AccountClassificationID = intAccountClassificationID) THEN 
		UPDATE tblAccountClassification SET
			AccountClassificationCode	= strAccountClassificationCode,
			AccountClassificationName	= strAccountClassificationName,
			AccountClassificationType	= intAccountClassificationType,
			CreatedOn					= dteCreatedOn,
			LastModified				= dteLastModified
		WHERE AccountClassificationID	= intAccountClassificationID;
	ELSE
		INSERT INTO tblAccountClassification(AccountClassificationID, AccountClassificationCode, AccountClassificationName, AccountClassificationType, CreatedOn, LastModified)
			VALUES(intAccountClassificationID, strAccountClassificationCode, strAccountClassificationName, intAccountClassificationType, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveAccountSummary
	Aug 2, 2014

	CALL procSaveAccountSummary(1000,2,3,4,now(), now());

********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveAccountSummary
GO

create procedure procSaveAccountSummary(	
	IN intAccountSummaryID        int(4),
	IN intAccountClassificationID int(4),
	IN strAccountSummaryCode      varchar(30),
	IN strAccountSummaryName      varchar(50),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT AccountSummaryID FROM tblAccountSummary WHERE AccountSummaryID = intAccountSummaryID) THEN 
		UPDATE tblAccountSummary SET
			AccountClassificationID = intAccountClassificationID,
			AccountSummaryCode		= strAccountSummaryCode,
			AccountSummaryName		= strAccountSummaryName,
			CreatedOn				= dteCreatedOn,
			LastModified			= dteLastModified
		WHERE AccountSummaryID		= intAccountSummaryID;
	ELSE
		INSERT INTO tblAccountSummary(AccountSummaryID, AccountClassificationID, AccountSummaryCode, AccountSummaryName, CreatedOn, LastModified)
			VALUES(intAccountSummaryID, intAccountClassificationID, strAccountSummaryCode, strAccountSummaryName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;

/********************************************
	procSaveAccountCategory
	Aug 2, 2014

	CALL procSaveAccountCategory(1000,2,3,4,now(), now());

********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveAccountCategory
GO

create procedure procSaveAccountCategory(	
	IN intAccountCategoryID        int(4),
	IN intAccountSummaryID		   int(4),
	IN strAccountCategoryCode      varchar(30),
	IN strAccountCategoryName      varchar(50),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT AccountCategoryID FROM tblAccountCategory WHERE AccountCategoryID = intAccountCategoryID) THEN 
		UPDATE tblAccountCategory SET
			AccountSummaryID		= intAccountSummaryID,
			AccountCategoryCode		= strAccountCategoryCode,
			AccountCategoryName		= strAccountCategoryName,
			CreatedOn				= dteCreatedOn,
			LastModified			= dteLastModified
		WHERE AccountCategoryID			= intAccountCategoryID;
	ELSE
		INSERT INTO tblAccountCategory(AccountCategoryID, AccountSummaryID, AccountCategoryCode, AccountCategoryName, CreatedOn, LastModified)
			VALUES(intAccountCategoryID, intAccountSummaryID, strAccountCategoryCode, strAccountCategoryName, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;


/********************************************
	procSaveChartOfAccount
	Aug 2, 2014

	CALL procSaveChartOfAccount(1,2,3,4,5,6,now(), now());
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procSaveChartOfAccount
GO

create procedure procSaveChartOfAccount(	
	IN intChartOfAccountID   int(4),
	IN intAccountCategoryID  int(4),
	IN strChartOfAccountCode varchar(30),
	IN strChartOfAccountName varchar(50),
	IN decDebit              decimal(18,3),
	IN decCredit             decimal(18,3),
	IN dteCreatedOn DATETIME,
	IN dteLastModified DATETIME
	)
BEGIN
	
	IF (DATE_FORMAT(dteCreatedOn, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN SET dteCreatedOn = NOW(); END IF;
	IF (DATE_FORMAT(dteLastModified, '%Y-%m-%d') = DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN  SET dteLastModified = NOW(); END IF;
	
	IF EXISTS(SELECT ChartOfAccountID FROM tblChartOfAccount WHERE ChartOfAccountID = intChartOfAccountID) THEN 
		UPDATE tblChartOfAccount SET
			AccountCategoryID		= intAccountCategoryID,
			ChartOfAccountCode		= strChartOfAccountCode,
			ChartOfAccountName		= strChartOfAccountName,
			Debit					= decDebit,
			Credit					= decCredit,
			CreatedOn				= dteCreatedOn,
			LastModified			= dteLastModified
		WHERE ChartOfAccountID		= intChartOfAccountID;
	ELSE
		INSERT INTO tblChartOfAccount(ChartOfAccountID, AccountCategoryID, ChartOfAccountCode, ChartOfAccountName, 
								Debit, Credit, CreatedOn, LastModified)
			VALUES(intChartOfAccountID, intAccountCategoryID, strChartOfAccountCode, strChartOfAccountName, 
								decDebit, decCredit, dteCreatedOn, dteLastModified);
	END IF;
				
END;
GO
delimiter ;
