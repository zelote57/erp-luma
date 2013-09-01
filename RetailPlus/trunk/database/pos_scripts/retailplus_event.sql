
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
		STARTS '2012-08-24 23:50:00'
    DO 
	BEGIN
		CALL procContactRewardExpire();
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
		STARTS '2012-08-24 23:50:00'
    DO 
	BEGIN
		CALL procContactCreditCardExpire();
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
		STARTS '2013-08-24 23:50:00'
    DO 
	BEGIN
		CALL sysProductInventorySnapshot();
	END;
GO
delimiter ;



/********************************************
	eventUpdatetblInventorySG
	-- Update the Closing Inventory group details and/or supplier details.
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventUpdatetblInventorySG
GO

CREATE EVENT eventUpdatetblInventorySG
    ON SCHEDULE
		EVERY 1 DAY
		STARTS '2012-08-24 23:50:00'
    DO 
	BEGIN
		CALL sysUpdatetblInventorySG();
	END;
GO
delimiter ;
