
/********************************************
	eventProcessCurrentBill12th
	-- every 10th of the month as per HP
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventProcessCurrentBill12th
GO

CREATE EVENT eventProcessCurrentBill12th
    ON SCHEDULE
		AT '2015-05-12 01:00:00'
    DO 
	BEGIN
		DECLARE strWillProcessCreditBillerInProgram VARCHAR(10) DEFAULT 'true';

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Starting process');

		SELECT ConfigValue
		INTO strWillProcessCreditBillerInProgram
		FROM sysConfig WHERE ConfigName = 'WillProcessCreditBillerInProgram';

		SET strWillProcessCreditBillerInProgram = (SELECT IFNULL(strWillProcessCreditBillerInProgram, 'true'));
		
		IF (strWillProcessCreditBillerInProgram = 'false') THEN
			
			IF (DAY(NOW()) = 12) THEN
				-- process those without guarantor 
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP CREDIT CARD', 'localhost', 'Starting process');
				CALL procProcessCreditBills(0, 'HP CREDIT CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP CREDIT CARD', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP CREDIT CARD', 'localhost', 'Closing process');
				CALL procProcessCreditBillsClose('HP CREDIT CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP CREDIT CARD', 'localhost', 'Finish');
			END IF;
		END IF;

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Finish');

	END;
GO
delimiter ;
