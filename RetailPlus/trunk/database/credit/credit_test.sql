-- =============================================
-- Use this for tetsing. 
-- Just set the first 3 configs then run everything.
-- =============================================
use pos;

begin;

UPDATE `pos`.`syscreditconfig` SET `ConfigValue`='2012-12-10' WHERE `ConfigName`='CreditPurcStartDateToProcess';
UPDATE `pos`.`syscreditconfig` SET `ConfigValue`='2013-01-09' WHERE `ConfigName`='CreditPurcEndDateToProcess';
UPDATE `pos`.`syscreditconfig` SET `ConfigValue`='2012-12-31' WHERE `ConfigName`='CreditCutOffDate';
UPDATE `pos`.`syscreditconfig` SET `ConfigValue`='2013-01-09' WHERE `ConfigName`='BillingDate';

SELECT * FROM tblCreditBills;

SET SQL_SAFE_UPDATES=0;
DELETE FROM tblCreditBillDetail WHERE CreditBillHeaderID IN 
										(SELECT DISTINCT CreditBillHeaderID 
										 FROM tblCreditBillHeader 
										 WHERE CreditBillID = (SELECT CreditBillID 
															   FROM tblCreditBills 
															   WHERE CreditCutOffDate = (SELECT ConfigValue 
																						 FROM syscreditconfig 
																						 WHERE `ConfigName`='BillingDate')));
												
DELETE FROM tblCreditBillHeader WHERE CreditBillID = (SELECT CreditBillID 
													  FROM tblCreditBills 
													  WHERE CreditCutOffDate = (SELECT ConfigValue 
																			    FROM syscreditconfig 
																				WHERE `ConfigName`='BillingDate'));

commit;


select * from tblCreditBillHeader;
select * from tblCreditBillDetail;
