
/**************************************************************
	procProductQuantityConvert
	Lemuel E. Aceron
	CALL procProcessCreditBillsClose();
	01-Feb-2013	Create this procedure
				Separate this procedure from existing procProcessCreditBills
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBillsClose
GO

create procedure procProcessCreditBillsClose(
	 ) 
BEGIN
	
	DECLARE dteCreditPurcStartDateToProcess DATE;
	DECLARE dteCreditPurcEndDateToProcess DATE;
	DECLARE dteNextCreditCutOffDate DATE;	
	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;	
	DECLARE dteCreditCutOffDate DATE;

	SET bolCreditUseLastDayCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditUseLastDayCutOffDate');
	SET dteCreditCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditCutOffDate');
	SET dteNextCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL 1 MONTH));
	
	-- IF DAY(dteCreditCutOffDate) >= 28 then
	IF bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate) THEN
		SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
		SET dteNextCreditCutOffDate = (SELECT LAST_DAY(dteNextCreditCutOffDate));
	END IF;

	/** Put the credit paramateres to be process *****/
	SELECT CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess
			INTO  dteCreditPurcStartDateToProcess ,dteCreditPurcEndDateToProcess
	FROM tblCreditBills WHERE CreditCutOffDate = dteCreditCutOffDate;
	/** end-Put the credit paramateres to be process *****/


	/** end-Update the header with the details *****/
	-- IF dteCurrDate >= dteCreditCutOffDate THEN
		UPDATE sysCreditConfig SET ConfigValue = DATE_ADD(dteCreditPurcStartDateToProcess, INTERVAL 1 MONTH) WHERE ConfigName = 'CreditPurcStartDateToProcess';
		UPDATE sysCreditConfig SET ConfigValue = DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL 1 MONTH) WHERE ConfigName = 'CreditPurcEndDateToProcess';
		UPDATE sysCreditConfig SET ConfigValue = dteNextCreditCutOffDate WHERE ConfigName = 'CreditCutOffDate';
	-- END IF;


END;
GO
delimiter ;
