 
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (122, 'ItemSetupFinancial');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (123, 'APLinkConfig');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
 
 ALTER TABLE tblProducts ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblProducts ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblProducts ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblProducts ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblProducts ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;
 
 
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPTracking INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPBills INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPFreight INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPVDeposit INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPContra INT(4) UNSIGNED NOT NULL DEFAULT 0;
 ALTER TABLE tblERPConfig add ChartOfAccountIDAPLatePayment INT(4) UNSIGNED NOT NULL DEFAULT 0;
 
 
UPDATE tblTransactions02 SET TransactionDate = DATE_ADD(TransactionDate, INTERVAL -6 DAY) WHERE TransactionDate >= '2009-02-13 00:00:00';
UPDATE tblTransactions02 SET DateSuspended = DATE_ADD(DateSuspended, INTERVAL -6 DAY) WHERE DateSuspended >= '2009-02-13 00:00:00';
UPDATE tblTransactions02 SET DateResumed = DATE_ADD(DateResumed, INTERVAL -6 DAY) WHERE DateResumed >= '2009-02-13 00:00:00';

select transactiondate, concat('2009-02-13 ',TIME(transactiondate)), DateSuspended from tbltransactions02 order by transactiondate desc limit 10;

select * from tbltransactions02 where transactiondate >= (select datelastinitialized from tblterminalreport);
select * from tbltransactions02 where transactiondate >= (select lastlogindate from tblcashierreport where cashierid = 26);
select datelastinitialized from tblterminalreport;

select datelastinitialized, a.* from tblterminalreport a order by datelastinitialized desc limit 10 ;
select datelastinitialized, a.* from tblterminalreporthistory a order by datelastinitialized desc limit 10 ;

select lastlogindate, a.* from tblcashierreporthistory a order by lastlogindate desc limit 10 ;


--update in the afternoon
UPDATE tblTransactions02 SET TransactionDate = concat('2009-02-13 ',TIME(transactiondate)) WHERE TransactionNo >= 36853 and date(transactiondate) = '2009-02-07';
UPDATE tblTransactions02 SET DateSuspended = concat('2009-02-13 ',TIME(DateSuspended)) WHERE DateSuspended <> '0001-01-01 00:00:00' AND TransactionNo >= 36853 and date(transactiondate) = '2009-02-07';
UPDATE tblTransactions02 SET DateResumed = concat('2009-02-13 ',TIME(DateResumed)) WHERE DateResumed <> '0001-01-01 00:00:00' AND TransactionNo >= 36853 and date(transactiondate) = '2009-02-07';

UPDATE tblTransactionItems01 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems02 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems03 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems04 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems05 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems06 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems07 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems08 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems09 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems10 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems11 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactionItems12 SET vatableamount = amount/(1+(VAT/100));
UPDATE tblTransactions02 SET cashiername = (select name from sysaccessuserdetails where tblTransactions02.cashierid=sysaccessuserdetails.uid) where cashiername <> (select name from sysaccessuserdetails where tblTransactions02.cashierid=sysaccessuserdetails.uid);
SELECT cashierid, cashiername, uid, name from tblTransactions02 a inner join sysaccessuserdetails where cashierid = uid limit 10;



