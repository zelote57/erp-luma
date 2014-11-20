
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
		STARTS '2012-08-24 8:30:00'
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
		STARTS '2012-08-24 8:30:00'
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
		STARTS '2013-08-24 8:30:00'
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
		STARTS '2012-08-24 8:30:00'
    DO 
	BEGIN
		CALL sysHoldCustomerCreditWithG();
	END;
GO
delimiter ;

SHOW EVENTS\G
