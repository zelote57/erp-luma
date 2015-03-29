
-- login as root to enable the event
SET GLOBAL event_scheduler = ON;

-- login as POSUser to access create the events

/********************************************
	eventExpireContactRewardCard
	-- Expires all advantage card(s) that is less than the current date.
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventExpireContactRewardCard
GO

CREATE EVENT eventExpireContactRewardCard
    ON SCHEDULE
		EVERY 1 DAY
		STARTS '2012-08-24 22:30:00'
    DO 
	BEGIN
		CALL sysContactRewardExpire();
	END;
GO
delimiter ;


/********************************************
	eventExpireCustomerCreditCard
	-- Expires all customer credit card(s) that is less than the current date.
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventExpireCustomerCreditCard
GO

CREATE EVENT eventExpireCustomerCreditCard
    ON SCHEDULE
		EVERY 1 DAY
		STARTS '2012-08-24 22:30:00'
    DO 
	BEGIN
		CALL sysContactCreditCardExpire();
	END;
GO
delimiter ;


/********************************************
	eventProductInventorySnapshot
	-- Take a daily and monthly snaphot of the inventory
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventProductInventorySnapshot
GO

CREATE EVENT eventProductInventorySnapshot
    ON SCHEDULE
		EVERY 1 DAY
		STARTS '2013-08-24 22:30:00'
    DO 
	BEGIN
		CALL sysProductInventorySnapshot();
	END;
GO
delimiter ;



/********************************************
	eventHoldCustomerCreditWithG
	-- Suspend all customers credit withG without payment after 3days
	-- schedule every 3 and 18 of the month
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventHoldCustomerCreditWithG
GO

CREATE EVENT eventHoldCustomerCreditWithG
    ON SCHEDULE
		EVERY 1 DAY
		STARTS '2012-08-24 22:50:00'
    DO 
	BEGIN
		CALL sysHoldCustomerCreditWithG();
	END;
GO
delimiter ;

/********************************************
	eventProcessCurrentBill10th
	-- every 10th of the month as per HP
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventProcessCurrentBill10th
GO

CREATE EVENT eventProcessCurrentBill10th
    ON SCHEDULE
		EVERY 1 MONTH
		STARTS '2014-12-10 01:00:00'
    DO 
	BEGIN
		DECLARE strWillProcessCreditBillerInProgram VARCHAR(10) DEFAULT 'true';

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Starting process');

		SELECT ConfigValue
		INTO strWillProcessCreditBillerInProgram
		FROM sysConfig WHERE ConfigName = 'WillProcessCreditBillerInProgram';

		SET strWillProcessCreditBillerInProgram = (SELECT IFNULL(strWillProcessCreditBillerInProgram, 'true'));
		
		IF (strWillProcessCreditBillerInProgram = 'false') THEN
			
			IF (DAY(NOW()) = 10) THEN
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


/********************************************
	eventProcessCurrentBill6th
	-- every 6th of the month as per HP
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventProcessCurrentBill6th
GO

CREATE EVENT eventProcessCurrentBill6th
    ON SCHEDULE
		EVERY 1 MONTH
		STARTS '2014-12-6 01:00:00'
    DO 
	BEGIN
		DECLARE strWillProcessCreditBillerInProgram VARCHAR(10) DEFAULT 'true';

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Starting process');

		SELECT ConfigValue
		INTO strWillProcessCreditBillerInProgram
		FROM sysConfig WHERE ConfigName = 'WillProcessCreditBillerInProgram';

		SET strWillProcessCreditBillerInProgram = (SELECT IFNULL(strWillProcessCreditBillerInProgram, 'true'));
		
		IF (strWillProcessCreditBillerInProgram = 'false') THEN
			IF (DAY(NOW()) = 6) THEN

				-- process those with guarantor 
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'EMP HP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('EMP HP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPERCARD - 30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPERCARD - 30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Finish');


				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPER CARD - 15');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPER CARD - 15');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPER CARD - 15/30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPER CARD - 15/30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'CN EMP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('CN EMP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'DELINQUENT ACC');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('DELINQUENT ACC');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'DORMANT ACC - CN');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('DORMANT ACC - CN');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Finish');
			END IF;
		END IF;

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Finish');

	END;
GO
delimiter ;


/********************************************
	eventProcessCurrentBill20th
	-- every 20th of the month as per HP
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventProcessCurrentBill20th
GO

CREATE EVENT eventProcessCurrentBill20th
    ON SCHEDULE
		EVERY 1 MONTH
		STARTS '2014-12-20 01:00:00'
    DO 
	BEGIN
		DECLARE strWillProcessCreditBillerInProgram VARCHAR(10) DEFAULT 'true';

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Starting process');

		SELECT ConfigValue
		INTO strWillProcessCreditBillerInProgram
		FROM sysConfig WHERE ConfigName = 'WillProcessCreditBillerInProgram';

		SET strWillProcessCreditBillerInProgram = (SELECT IFNULL(strWillProcessCreditBillerInProgram, 'true'));
		
		IF (strWillProcessCreditBillerInProgram = 'false') THEN
			IF (DAY(NOW()) = 20) THEN

				-- process those with guarantor 
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'EMP HP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('EMP HP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: EMP HP SUPER CARD', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPERCARD - 30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPERCARD - 30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPERCARD - 30', 'localhost', 'Finish');


				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPER CARD - 15');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPER CARD - 15');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'HP SUPER CARD - 15/30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('HP SUPER CARD - 15/30');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: HP SUPER CARD - 15/30', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'CN EMP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('CN EMP SUPER CARD');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: CN EMP SUPER CARD', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'DELINQUENT ACC');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('DELINQUENT ACC');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DELINQUENT ACC', 'localhost', 'Finish');



				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Starting process');
				CALL procProcessCreditBillsWG(0, 'DORMANT ACC - CN');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Finish');

				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Closing process');
				CALL procProcessCreditBillsWGClose('DORMANT ACC - CN');
				CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: DORMANT ACC - CN', 'localhost', 'Finish');
			END IF;
		END IF;

		CALL procsysAuditInsert(NOW(), 'CreditBiller Admin', 'CreditBiller: ALL', 'localhost', 'Finish');

	END;
GO
delimiter ;

SHOW EVENTS\G
