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
 
 ALTER TABLE tblProductGroup ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;
 
 ALTER TABLE tblTerminal ADD `OrderSlipPrinter` TINYINT(1) NOT NULL DEFAULT 0;

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