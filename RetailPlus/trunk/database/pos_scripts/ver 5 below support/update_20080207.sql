/*****************************
**	Added on Feb 07, 2008
**	Lemuel E. Aceron
**	Include Waiter in all installations
*****************************/
INSERT INTO sysAccessGroups (GroupName, Remarks) VALUES ('Waiters', 'Default group for waiters.');

INSERT INTO sysAccessUsers (UID, Username, Password, DateCreated ) VALUES (2, 'waiter', 'waiter', now());

INSERT INTO sysAccessUserDetails (UID ,Name, CountryID, GroupID) VALUES (2, 'RetailPlus Waiter', 1, 5);

ALTER TABLE tblTransactions01 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions01 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions02 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions02 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions03 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions03 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions04 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions04 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions05 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions05 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions06 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions06 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions07 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions07 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions08 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions08 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions09 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions09 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions10 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions10 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions11 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions11 ADD `WaiterName` VARCHAR(100) NOT NULL;
ALTER TABLE tblTransactions12 ADD `WaiterID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 2;
ALTER TABLE tblTransactions12 ADD `WaiterName` VARCHAR(100) NOT NULL;
 