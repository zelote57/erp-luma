
/**************************************************************

	procContactSelectForBilling
	Lemuel E. Aceron
	Sep 15, 2013
	
	Desc: This will get the all information of a contact

	CALL procContactSelectForBilling(0, null, null, null, null, null, 0, 0, 'ContactID','');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSelectForBilling
GO

create procedure procContactSelectForBilling(
			IN CreditCardTypeID int,
			IN ContactID  bigint,
			IN ContactCode varchar(50),
			IN ContactName varchar(75),
			IN ContactGroupCode varchar(30),
			IN CreditCardNo varchar(30),
			IN Name varchar(30),
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
							,CreditCardNo ,cci.GuarantorID ,cci.CreditAwardDate ,cci.TotalPurchases ,cci.CreditPaid
							,cci.CreditCardStatus ,cci.ExpiryDate ,cci.CreditCardTypeID ,cci.CreditBeginningBalance ,cci.LastBillingDate
							,LastCheckInDate
						FROM tblContacts cntct
							INNER JOIN tblContactGroup grp ON cntct.ContactGroupID = grp.ContactGroupID
							INNER JOIN tblDepartments dept ON cntct.DepartmentID = dept.DepartmentID
							INNER JOIN tblPositions pos ON cntct.PositionID = pos.PositionID
							INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = cntct.ContactID
							LEFT OUTER JOIN tblContactAddOn addon ON addon.ContactID = cntct.ContactID
							LEFT OUTER JOIN tblCountry cntry ON addon.CountryID = cntry.CountryID
						WHERE (ContactGroupCategory = 1 OR ContactGroupCategory = 3) ');

	IF CreditCardTypeID <> 0 THEN -- CreditCardTypeID
		SET @SQL = CONCAT(@SQL, 'AND cci.CreditCardTypeID = ',CreditCardTypeID,' ');
	END IF;

	IF intDeleted <> -1 THEN -- Customer Group
		SET @SQL = CONCAT(@SQL, 'AND cntct.deleted = ',intDeleted,' ');
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

	IF IFNULL(CreditCardNo,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND cci.CreditCardNo = ''%',CreditCardNo,'%'' ');
	END IF;

	IF IFNULL(ContactGroupCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (ContactGroupCode LIKE ''%',ContactGroupCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '	  OR ContactGroupName LIKE ''%',ContactGroupCode,'%'') ');
	END IF;


	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','ContactCode, ContactName, LastName',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL,IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

SELECT trx.BranchID, trx.TerminalNo, trx.SyncID, 0 CreditPaymentCashID, trx.TransactionID, trx.TransactionNo, 
				trx.TransactionDate TransactionDate,
							0 LatePenaltyAmount,
							0 FinanceChargeAmount,
							SubTotal PrincipalAmount,
							'' Remarks, trx.CreatedOn, trx.LastModified,
							'Cash' AS PaymentSource,
							trx.BranchID AS CPRefBranchID, trx.TerminalNo AS CPRefTerminalNo, trx.TransactionNo AS CPRefTransactionNo,
							0 CreditReasonID, CONCAT('Payment @ Ter#:', trx.TerminalNo,' Br#:',trx.BranchID) CreditReason,
							cci.CreditCardNo, cntct.ContactName, trx.SubTotal Amount,
							IFNULL(gci.CreditCardNo, '') GuarantorCreditCardNo, IFNULL(gua.ContactName,'') GuarantorName
						FROM tblTransactions trx
						INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = trx.CustomerID
						INNER JOIN tblContacts cntct ON cntct.ContactID =  trx.CustomerID
						LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
						LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
						WHERE TransactionStatus = 7 
						and trx.CustomerID = 2379



update tblterminalreport set isprocessed = 0 where datelastinitialized >= '2014-11-30';
update tblterminalreporthistory set isprocessed = 0 where datelastinitialized >= '2014-11-30';

UPDATE tblTerminalReportHistory SET BeginningTransactionNo = '00000000000000', EndingTransactionNo = '00000000000000' 
WHERE BeginningTransactionNo = '00000000000001' AND EndingTransactionNo = '00000000000001';

UPDATE tblTerminalReportHistory SET BeginningTransactionNo = '00000000000000', EndingTransactionNo = '00000000000000' 
WHERE BeginningTransactionNo = '00000000000000' AND EndingTransactionNo = '00000000000001';

CALL procTerminalReportHistorySyncTransactionSales(1, '01', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '02', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '03', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '04', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '05', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '06', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '07', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '08', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '09', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '10', '2014-11-30 00:00');

CALL procTerminalReportHistorySyncTransactionSales(1, '11', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '12', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '13', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '14', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '15', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '16', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '17', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '18', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '19', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '20', '2014-11-30 00:00');

CALL procTerminalReportHistorySyncTransactionSales(1, '21', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '22', '2014-11-30 00:00');

CALL procTerminalReportHistorySyncTransactionSales(1, '80', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '81', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '82', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '83', '2014-11-30 00:00');
CALL procTerminalReportHistorySyncTransactionSales(1, '84', '2014-11-30 00:00');

CALL procTerminalReportHistorySyncTransactionSales(1, '90', '2014-11-30 00:00');

/***
UPDATE tbltransactions SET CreatedOn = TransactionDate WHERE CreatedOn <> TransactionDate;

UPDATE tbltransactionItems, tblTransactions
SET 
	tbltransactionItems.CreatedOn = tblTransactions.TransactionDate 
WHERE tblTransactionItems.CreatedOn <> tblTransactions.TransactionDate
AND tblTransactions.TransactionID = tblTransactionItems.TransactionID;

UPDATE tblTransactions SET SyncID = TransactionID WHERE SyncID=0;
UPDATE tblTransactionItems SET SyncID = TransactionItemsID WHERE SyncID=0;

CREATE TRIGGER trgtblTransactionsCreatedOn BEFORE INSERT ON tblTransactions FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;
CREATE TRIGGER trgtblTransactionItemsCreatedOn BEFORE INSERT ON tblTransactionItems FOR EACH ROW SET NEW.CreatedOn = CURRENT_TIMESTAMP;



select terminalno, VatableAmount, round((SubTotal - VATExempt - NonVATableAmount) / 1.12, 2) newvatalbleamount 
	,SubTotal ,VATExempt ,NonVATableAmount
from tblTransactions where terminalno >= 3 
and VatableAmount <> round((SubTotal - VATExempt - NonVATableAmount) / 1.12, 2)
order by terminalno
limit 10;


