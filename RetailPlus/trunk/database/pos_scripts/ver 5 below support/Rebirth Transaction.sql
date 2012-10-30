DROP TABLE IF EXISTS tblTransactionItems01;
DROP TABLE IF EXISTS tblTransactionItems02;
DROP TABLE IF EXISTS tblTransactionItems03;
DROP TABLE IF EXISTS tblTransactionItems04;
DROP TABLE IF EXISTS tblTransactionItems05;
DROP TABLE IF EXISTS tblTransactionItems06;
DROP TABLE IF EXISTS tblTransactionItems07;
DROP TABLE IF EXISTS tblTransactionItems08;
DROP TABLE IF EXISTS tblTransactionItems09;
DROP TABLE IF EXISTS tblTransactionItems10;
DROP TABLE IF EXISTS tblTransactionItems11;
DROP TABLE IF EXISTS tblTransactionItems12;

DROP TABLE IF EXISTS tblTransactions01;
DROP TABLE IF EXISTS tblTransactions02;
DROP TABLE IF EXISTS tblTransactions03;
DROP TABLE IF EXISTS tblTransactions04;
DROP TABLE IF EXISTS tblTransactions05;
DROP TABLE IF EXISTS tblTransactions06;
DROP TABLE IF EXISTS tblTransactions07;
DROP TABLE IF EXISTS tblTransactions08;
DROP TABLE IF EXISTS tblTransactions09;
DROP TABLE IF EXISTS tblTransactions10;
DROP TABLE IF EXISTS tblTransactions11;
DROP TABLE IF EXISTS tblTransactions12;

/*****************************
**	tblTransactionNos
*****************************/
DROP TABLE IF EXISTS tblTransactionNos;
CREATE TABLE tblTransactionNos (
`TransactionNo` VARCHAR(30) NOT NULL,
PRIMARY KEY (`TransactionNo`),
INDEX `IX_tblTransactionNos`(`TransactionNo`),
UNIQUE `PK_tblTransactionNos`(`TransactionNo`)
)
TYPE=INNODB COMMENT = 'Transaction Nos';

/*****************************
**	tblTransactions01
*****************************/
DROP TABLE IF EXISTS tblTransactions01;
CREATE TABLE tblTransactions01 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions01`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions01`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions01`(`TransactionNo`),
INDEX `IX2_tblTransactions01`(`CustomerID`),
INDEX `IX3_tblTransactions01`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 01';

/*****************************
**	tblTransactions02
*****************************/
DROP TABLE IF EXISTS tblTransactions02;
CREATE TABLE tblTransactions02 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions02`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions02`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions02`(`TransactionNo`),
INDEX `IX2_tblTransactions02`(`CustomerID`),
INDEX `IX3_tblTransactions02`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 02';

/*****************************
**	tblTransactions03
*****************************/
DROP TABLE IF EXISTS tblTransactions03;
CREATE TABLE tblTransactions03 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions03`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions03`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions03`(`TransactionNo`),
INDEX `IX2_tblTransactions03`(`CustomerID`),
INDEX `IX3_tblTransactions03`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 03';

/*****************************
**	tblTransactions04
*****************************/
DROP TABLE IF EXISTS tblTransactions04;
CREATE TABLE tblTransactions04 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions04`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions04`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions04`(`TransactionNo`),
INDEX `IX2_tblTransactions04`(`CustomerID`),
INDEX `IX3_tblTransactions04`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 04';

/*****************************
**	tblTransactions05
*****************************/
DROP TABLE IF EXISTS tblTransactions05;
CREATE TABLE tblTransactions05 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions05`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions05`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions05`(`TransactionNo`),
INDEX `IX2_tblTransactions05`(`CustomerID`),
INDEX `IX3_tblTransactions05`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 05';


/*****************************
**	tblTransactions06
*****************************/
DROP TABLE IF EXISTS tblTransactions06;
CREATE TABLE tblTransactions06 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions06`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions06`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions06`(`TransactionNo`),
INDEX `IX2_tblTransactions06`(`CustomerID`),
INDEX `IX3_tblTransactions06`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 06';

/*****************************
**	tblTransactions07
*****************************/
DROP TABLE IF EXISTS tblTransactions07;
CREATE TABLE tblTransactions07 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions07`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions07`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions07`(`TransactionNo`),
INDEX `IX2_tblTransactions07`(`CustomerID`),
INDEX `IX3_tblTransactions07`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 07';

/*****************************
**	tblTransactions08
*****************************/
DROP TABLE IF EXISTS tblTransactions08;
CREATE TABLE tblTransactions08 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions08`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions08`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions08`(`TransactionNo`),
INDEX `IX2_tblTransactions08`(`CustomerID`),
INDEX `IX3_tblTransactions08`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 08';

/*****************************
**	tblTransactions09
*****************************/
DROP TABLE IF EXISTS tblTransactions09;
CREATE TABLE tblTransactions09 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions09`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions09`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions09`(`TransactionNo`),
INDEX `IX2_tblTransactions09`(`CustomerID`),
INDEX `IX3_tblTransactions09`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 09';

/*****************************
**	tblTransactions10
*****************************/
DROP TABLE IF EXISTS tblTransactions10;
CREATE TABLE tblTransactions10 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions10`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions10`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions10`(`TransactionNo`),
INDEX `IX2_tblTransactions10`(`CustomerID`),
INDEX `IX3_tblTransactions10`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 10';

/*****************************
**	tblTransactions11
*****************************/
DROP TABLE IF EXISTS tblTransactions11;
CREATE TABLE tblTransactions11 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions11`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions11`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions11`(`TransactionNo`),
INDEX `IX2_tblTransactions11`(`CustomerID`),
INDEX `IX3_tblTransactions11`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 11';

/*****************************
**	tblTransactions12
*****************************/
DROP TABLE IF EXISTS tblTransactions12;
CREATE TABLE tblTransactions12 (
`TransactionID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionNo` VARCHAR(30) NOT NULL,
`CustomerID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblContacts(`ContactID`),
`CustomerName` VARCHAR(100) NOT NULL,
`CashierID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`CashierName` VARCHAR(100) NOT NULL,
`TerminalNo` VARCHAR(30) NOT NULL,
`TransactionDate`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00' ,
`DateSuspended`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`DateResumed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`TransactionStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`SubTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TransDiscountType` INT(10) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`AmountPaid` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BalanceAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChangeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DateClosed`	DATETIME NOT NULL DEFAULT '0001-01-01 12:00:00',
`PaymentType` INT(10) UNSIGNED NOT NULL DEFAULT 4,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
PRIMARY KEY (TransactionID),
INDEX `IX_tblTransactions12`(`TransactionID`, `TransactionNo`),
UNIQUE `PK_tblTransactions12`(`TransactionID`, `TransactionNo`),
INDEX `IX1_tblTransactions12`(`TransactionNo`),
INDEX `IX2_tblTransactions12`(`CustomerID`),
INDEX `IX3_tblTransactions12`(`CashierID`)
)
TYPE=INNODB COMMENT = 'Transactions 12';

/*****************************
**	tblTransactionItems01
*****************************/
DROP TABLE IF EXISTS tblTransactionItems01;
CREATE TABLE tblTransactionItems01 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions01(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems01`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems01`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems01`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems01`(`ProductCode`),
INDEX `IX3_tblTransactionItems01`(`TransactionID`),
INDEX `IX4_tblTransactionItems01`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 01';

/*****************************
**	tblTransactionItems02
*****************************/
DROP TABLE IF EXISTS tblTransactionItems02;
CREATE TABLE tblTransactionItems02 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions02(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems02`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems02`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems02`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems02`(`ProductCode`),
INDEX `IX3_tblTransactionItems02`(`TransactionID`),
INDEX `IX4_tblTransactionItems02`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 02';

/*****************************
**	tblTransactionItems03
*****************************/
DROP TABLE IF EXISTS tblTransactionItems03;
CREATE TABLE tblTransactionItems03 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions03(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems03`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems03`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems03`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems03`(`ProductCode`),
INDEX `IX3_tblTransactionItems03`(`TransactionID`),
INDEX `IX4_tblTransactionItems03`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 03';

/*****************************
**	tblTransactionItems04
*****************************/
DROP TABLE IF EXISTS tblTransactionItems04;
CREATE TABLE tblTransactionItems04 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions04(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems04`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems04`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems04`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems04`(`ProductCode`),
INDEX `IX3_tblTransactionItems04`(`TransactionID`),
INDEX `IX4_tblTransactionItems04`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 04';

/*****************************
**	tblTransactionItems05
*****************************/
DROP TABLE IF EXISTS tblTransactionItems05;
CREATE TABLE tblTransactionItems05 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions05(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems05`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems05`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems05`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems05`(`ProductCode`),
INDEX `IX3_tblTransactionItems05`(`TransactionID`),
INDEX `IX4_tblTransactionItems05`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 05';

/*****************************
**	tblTransactionItems06
*****************************/
DROP TABLE IF EXISTS tblTransactionItems06;
CREATE TABLE tblTransactionItems06 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions06(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems06`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems06`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems06`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems06`(`ProductCode`),
INDEX `IX3_tblTransactionItems06`(`TransactionID`),
INDEX `IX4_tblTransactionItems06`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 06';

/*****************************
**	tblTransactionItems07
*****************************/
DROP TABLE IF EXISTS tblTransactionItems07;
CREATE TABLE tblTransactionItems07 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions07(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems07`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems07`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems07`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems07`(`ProductCode`),
INDEX `IX3_tblTransactionItems07`(`TransactionID`),
INDEX `IX4_tblTransactionItems07`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 07';

/*****************************
**	tblTransactionItems08
*****************************/
DROP TABLE IF EXISTS tblTransactionItems08;
CREATE TABLE tblTransactionItems08 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions08(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems08`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems08`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems08`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems08`(`ProductCode`),
INDEX `IX3_tblTransactionItems08`(`TransactionID`),
INDEX `IX4_tblTransactionItems08`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 08';

/*****************************
**	tblTransactionItems09
*****************************/
DROP TABLE IF EXISTS tblTransactionItems09;
CREATE TABLE tblTransactionItems09 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions09(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems09`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems09`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems09`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems09`(`ProductCode`),
INDEX `IX3_tblTransactionItems09`(`TransactionID`),
INDEX `IX4_tblTransactionItems09`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 09';

/*****************************
**	tblTransactionItems10
*****************************/
DROP TABLE IF EXISTS tblTransactionItems10;
CREATE TABLE tblTransactionItems10 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions10(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems10`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems10`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems10`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems10`(`ProductCode`),
INDEX `IX3_tblTransactionItems10`(`TransactionID`),
INDEX `IX4_tblTransactionItems10`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 10';

/*****************************
**	tblTransactionItems11
*****************************/
DROP TABLE IF EXISTS tblTransactionItems11;
CREATE TABLE tblTransactionItems11 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions11(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems11`(`TransactionItemsID`),
INDEX `IX0_tblTransactionItems11`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems11`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems11`(`ProductCode`),
INDEX `IX3_tblTransactionItems11`(`TransactionID`),
INDEX `IX4_tblTransactionItems11`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 11';

/*****************************
**	tblTransactionItems12
*****************************/
DROP TABLE IF EXISTS tblTransactionItems12;
CREATE TABLE tblTransactionItems12 (
`TransactionItemsID` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
`TransactionID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTransactions12(`TransactionID`),
`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblProducts(`ProductID`),
`ProductCode` VARCHAR(30) NOT NULL,
`BarCode` VARCHAR(30) NOT NULL,
`Description` VARCHAR(100) NOT NULL,
`ProductUnitID` INT(3) UNSIGNED NOT NULL DEFAULT 0,
`ProductUnitCode` VARCHAR(3) NOT NULL,
`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Price` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`Discount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemDiscountType` TINYINT(1) NOT NULL DEFAULT 0,
`Amount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVatableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VariationsMatrixID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
`MatrixDescription` VARCHAR(150) NULL,
`ProductGroup` VARCHAR(20) NULL,
`ProductSubGroup` VARCHAR(20) NULL,
`TransactionItemStatus` SMALLINT(1) UNSIGNED NOT NULL DEFAULT 0,
`DiscountCode` VARCHAR(30),
`DiscountRemarks` VARCHAR(255),
`ProductPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`MatrixPackageID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 ,
`PackageQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoQuantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoValue` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PromoInPercent` TINYINT(1) NOT NULL DEFAULT 0,
`PromoType` TINYINT(1) NOT NULL DEFAULT 0,
`PromoApplied` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchasePrice` DECIMAL(18,2) NOT NULL DEFAULT 0,
`PurchaseAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`IncludeInSubtotalDiscount` TINYINT(1) NOT NULL DEFAULT 1,
PRIMARY KEY (TransactionItemsID),
INDEX `IX_tblTransactionItems12`(`TransactionItemsID`),
INDEX `IX9_tblTransactionItems12`(`TransactionID`, `ProductID`),
INDEX `IX1_tblTransactionItems12`(`TransactionID`, `ProductID`,`VariationsMatrixID`),
INDEX `IX2_tblTransactionItems12`(`ProductCode`),
INDEX `IX3_tblTransactionItems12`(`TransactionID`),
INDEX `IX4_tblTransactionItems12`(`ProductUnitID`)
)
TYPE=INNODB COMMENT = 'Transaction Items 12';

/*****************************
**	tblTerminalReport
*****************************/
DROP TABLE IF EXISTS tblTerminalReport;
CREATE TABLE tblTerminalReport (
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`BeginningTransactionNo` VARCHAR(30) NOT NULL,
`EndingTransactionNo` VARCHAR(30) NOT NULL,
`ZReadCount` INT(10) NOT NULL DEFAULT 0,
`XReadCount` INT(10) NOT NULL DEFAULT 0,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`OldGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NewGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonEVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`DateLastInitialized` DATETIME NOT NULL,
PRIMARY KEY (TerminalNo),
INDEX `IX_tblTerminalReport`(`TerminalNo`),
UNIQUE `PK_tblTerminalReport`(`TerminalNo`),
INDEX `IX1_tblTerminalReport`(`ZReadCount`),
INDEX `IX2_tblTerminalReport`(`XReadCount`)
)
TYPE=INNODB COMMENT = 'Terminal Report';
INSERT INTO tblTerminalReport (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
		VALUES		('00000000000001', '00000000000001', 1, 1, 1, '01', DATE_SUB(DATE(NOW()), INTERVAL 1 DAY));

/*****************************
**	tblTerminalReportHistory
*****************************/
DROP TABLE IF EXISTS tblTerminalReportHistory;
CREATE TABLE tblTerminalReportHistory (
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`BeginningTransactionNo` VARCHAR(30) NOT NULL,
`EndingTransactionNo` VARCHAR(30) NOT NULL,
`ZReadCount` INT(10) NOT NULL DEFAULT 0,
`XReadCount` INT(10) NOT NULL DEFAULT 0,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`OldGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NewGrandTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NonEVATableAmount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`DateLastInitialized` DATETIME NOT NULL,
INDEX `IX_tblTerminalReport`(`TerminalNo`),
INDEX `IX1_tblTerminalReport`(`ZReadCount`),
INDEX `IX2_tblTerminalReport`(`XReadCount`)
)
TYPE=INNODB COMMENT = 'Terminal Report History';

INSERT INTO tblTerminalReportHistory (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
		VALUES		('00000000000000', '00000000000000', 0, 0, 1, '01', DATE_SUB(DATE(NOW()), INTERVAL 2 DAY));
		
/*****************************
**	tblCashierReport
*****************************/
DROP TABLE IF EXISTS tblCashierReport;
CREATE TABLE tblCashierReport (
`CashierID` BIGINT(20) NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`CashCount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LastLoginDate` DATETIME NOT NULL,
PRIMARY KEY (`CashierID`, `TerminalNo`),
INDEX `IX_tblCashierReport`(`CashierID`, `TerminalNo`),
UNIQUE `PK_tblCashierReport`(`CashierID`, `TerminalNo`),
INDEX `IX1_tblCashierReport`(`CashierID`),
INDEX `IX2_tblCashierReport`(`TerminalNo`),
INDEX `IX3_tblCashierReport`(`TerminalID`)
)
TYPE=INNODB COMMENT = 'Cashier Report';

/*****************************
**	tblCashierReportHistory
*****************************/
DROP TABLE IF EXISTS tblCashierReportHistory;
CREATE TABLE tblCashierReportHistory (
`CashierID` BIGINT(20) NOT NULL DEFAULT 0 REFERENCES sysAccessUsers(`UID`),
`TerminalID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0 REFERENCES tblTerminal(`TerminalID`),
`TerminalNo` VARCHAR(10) NOT NULL,
`GrossSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`DailySales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`QuantitySold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`GroupSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`EVAT` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditPayment` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashInDrawer` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardDisburse` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequeWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardWithhold` DECIMAL(18,2) NOT NULL DEFAULT 0,
`TotalPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CashPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ChequePaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`CreditCardPaidOut` DECIMAL(18,2) NOT NULL DEFAULT 0,
`BeginningBalance` DECIMAL(18,2) NOT NULL DEFAULT 0,
`VoidSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`RefundSales` DECIMAL(18,2) NOT NULL DEFAULT 0,
`ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`SubtotalDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`NoOfCashTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfChequeTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditCardTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCombinationPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfCreditPaymentTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfClosedTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfRefundTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfVoidTransactions` INT(10) NOT NULL DEFAULT 0,
`NoOfTotalTransactions` INT(10) NOT NULL DEFAULT 0,
`CashCount` DECIMAL(18,2) NOT NULL DEFAULT 0,
`LastLoginDate` DATETIME NOT NULL,
INDEX `IX_tblCashierReport`(`CashierID`, `TerminalNo`),
INDEX `IX1_tblCashierReport`(`CashierID`),
INDEX `IX2_tblCashierReport`(`TerminalNo`),
INDEX `IX3_tblCashierReport`(`TerminalID`)
)
TYPE=INNODB COMMENT = 'Cashier Report History';

INSERT INTO tblCashierReport (`CashierID`, `TerminalID`, `TerminalNo`, `LastLoginDate`)
			VALUES		(1, 1, '01', "0001-01-01 00:00");
						
ALTER TABLE tblTransactions01 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReportHistory ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReport ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReportHistory ADD `TotalDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CashDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `ChequeDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CreditCardDeposit` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `DebitPayment` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReport ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NoOfDebitPaymentTransactions` INT(10) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactions01 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `ItemsDiscount` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*****************************
**	Added on April 30, 2007
**	Lemuel E. Aceron
*****************************/ 

ALTER TABLE tblTransactions01 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions02 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions03 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions04 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions05 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions06 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions07 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions08 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions09 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions10 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions11 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);
ALTER TABLE tblTransactions12 ADD `Charge` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeAmount` DECIMAL(18,2) NOT NULL DEFAULT 0, ADD `ChargeCode` VARCHAR(30), ADD `ChargeRemarks` VARCHAR(255);

ALTER TABLE tblTerminalReport ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `TotalCharge` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*****************************
**	Added on Feb 07, 2008
**	Lemuel E. Aceron
**	Include Waiter in all installations.
*****************************/ 

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


/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lemuel E. Aceron
*****************************/

ALTER TABLE tblTransactions01 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions02 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions03 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions04 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions05 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions06 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions07 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions08 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions09 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions10 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions11 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactions12 ADD `Packed` TINYINT(1) NOT NULL DEFAULT 0;

 /******************************************************************
 **	Added on Dec 09, 2008 for OrderSlipPrinter
 **	Lemuel E. Aceron
 **
 ** OrderSlipPrinter Types
 ** 0 = Print to 1st printer PrinterName=RetailPlusOSPrinter1
 ** 1 = Print to 2nd printer PrinterName=RetailPlusOSPrinter2
 ** 2 = Print to 2nd printer PrinterName=RetailPlusOSPrinter3
 ** 3 = print to 4th printer PrinterName=RetailPlusOSPrinter4
 ** 4 = print to 5th printer PrinterName=RetailPlusOSPrinter5
 
 ******************************************************************/
 
 ALTER TABLE tblTransactionItems01 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems02 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems03 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems04 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems05 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems06 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems07 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems08 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems09 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems10 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems11 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems12 ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 
 
 ALTER TABLE tblCashierReport ADD `IsCashCountInitialized` TINYINT(1) NOT NULL DEFAULT 0;

ALTER TABLE tblTerminalReportHistory ADD `TrustFund` DECIMAL(5,2) NOT NULL DEFAULT 0.00;

  /******************************************************************
 **	Added on Dec 18, 2008 for OrderSlipPrinted
 **	Lemuel E. Aceron
 **
 ** OrderSlipPrinted Types
 ** 0 = not yet printed
 ** 1 = printed
 
 ******************************************************************/
 ALTER TABLE tblTransactionItems01 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems02 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems03 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems04 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems05 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems06 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems07 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems08 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems09 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems10 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems11 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0;
 ALTER TABLE tblTransactionItems12 ADD `OrderSlipPrinted` TINYINT(1) NOT NULL DEFAULT 0; 
 
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

ALTER TABLE tblTerminalReport ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReport ADD `BatchCounter` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblTerminalReportHistory ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminalReportHistory ADD `BatchCounter` INT(4) NOT NULL DEFAULT 1;

ALTER TABLE tblCashierReport ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReport ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblCashierReportHistory ADD `NoOfDiscountedTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NegativeAdjustments` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `NoOfNegativeAdjustmentTransactions` INT(4) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `PromotionalItems` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblCashierReportHistory ADD `CreditSalesTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

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

ALTER TABLE tblTerminalReportHistory ADD `MallFileName` VARCHAR(30); 
ALTER TABLE tblTerminalReportHistory ADD `IsMallFileUploadComplete` TINYINT(1) NOT NULL DEFAULT 0; 

UPDATE tblTerminalReportHistory SET IsMallFileUploadComplete = 1 WHERE MallFileName IS NOT NULL;

ALTER TABLE tblTransactions01 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions01 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions02 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions02 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions03 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions03 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions04 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions04 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions05 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions05 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions06 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions06 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions07 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions07 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions08 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions08 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions09 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions09 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions10 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions10 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions11 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions11 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';
ALTER TABLE tblTransactions12 ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 1;
ALTER TABLE tblTransactions12 ADD `AgentName` VARCHAR(100) DEFAULT 'RetailPlus Customer ™';

ALTER TABLE tblTransactionItems01 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems01 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems02 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems03 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems04 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems05 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems06 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems07 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems08 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems09 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems10 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems11 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 ADD `PercentageCommision` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems12 ADD `Commision` DECIMAL(18,2) NOT NULL DEFAULT 0;

CALL procZeroOutProductQuantityAndDropVariations();