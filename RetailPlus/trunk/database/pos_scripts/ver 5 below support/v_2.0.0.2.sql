/*********************************  v_2.0.0.2.sql START  *******************************************************/

-- ShowItemMoreThanZeroQty is implemented when selecting variations.
-- 

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.2';

ALTER TABLE tblReceipt MODIFY `TEXT` varchar(20);
	
/*********************************
Lemuel E. Aceron
May 1, 2009

OrderType values:
	0 = DINEIN
	1 = TAKE OUT
	2 = DELIVERY
*********************************/
ALTER TABLE tblTransactions01 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions02 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions03 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions04 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions05 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions06 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions07 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions08 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions09 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions10 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions11 ADD OrderType smallint NOT NULL DEFAULT 0; 
ALTER TABLE tblTransactions12 ADD OrderType smallint NOT NULL DEFAULT 0; 


/*********************************
Create table for discount
*********************************/
ALTER TABLE tblDiscount MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactions12 MODIFY DiscountCode varchar(5);

ALTER TABLE tblTransactionItems01 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems02 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems03 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems04 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems05 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems06 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems07 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems08 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems09 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems10 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems11 MODIFY DiscountCode varchar(5);
ALTER TABLE tblTransactionItems12 MODIFY DiscountCode varchar(5);

-- This fields are from .config file, moved to database.
ALTER TABLE tblTerminal ADD IsPrinterDotmatrix TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE tblTerminal ADD IsChargeEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD IsDiscountEditable TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD CheckCutOffTime TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal ADD StartCutOffTime VARCHAR(5) NOT NULL DEFAULT '00:00';
ALTER TABLE tblTerminal ADD EndCutOffTime VARCHAR(5) NOT NULL DEFAULT '23:59';
ALTER TABLE tblTerminal ADD WithRestaurantFeatures TINYINT(1) NOT NULL DEFAULT 0;
        
--Make a default DiscountCode for Senior Citizen Discount
ALTER TABLE tblTerminal ADD SeniorCitizenDiscountCode varchar(5);
UPDATE tblTerminal SET SeniorCitizenDiscountCode = 'SENCZ';

DROP TABLE IF EXISTS tblDiscountHistory;
CREATE TABLE tblDiscountHistory (
	`SessionID` VARCHAR(30) NOT NULL,
	`TerminalNo` VARCHAR(30) NOT NULL,
	`DiscountCode` VARCHAR(5) NOT NULL,
	`DiscountCount` BIGINT(20) NOT NULL DEFAULT 0,
	`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
	INDEX `IX_tblDiscount`(`SessionID`),
	INDEX `IX_tblDiscount1`(`DiscountCode`)
)
TYPE=INNODB COMMENT = 'Product Discount Report';

/*********************************  v_2.0.0.2.sql END  *******************************************************/


