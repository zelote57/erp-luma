USE POS

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
    DO 
	BEGIN
		CALL pos.procContactRewardExpire();
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
    DO 
	BEGIN
		CALL pos.procContactCreditCardExpire();
	END;
GO
delimiter ;




