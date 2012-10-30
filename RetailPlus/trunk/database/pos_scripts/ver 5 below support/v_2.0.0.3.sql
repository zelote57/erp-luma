/*********************************  v_2.0.0.3.sql START  *****************************************************/

-- Added procGenerateSalesPerItemByGroup for P&L per item., run retailplus_proc.sql

DELETE FROM tblSalesPerItem;

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.3';

ALTER TABLE tblTerminal ADD `IsTouchScreen` TINYINT(1) NOT NULL DEFAULT 0; 

ALTER TABLE tblTerminalReportHistory ADD `MallFileName` VARCHAR(30); 
ALTER TABLE tblTerminalReportHistory ADD `IsMallFileUploadComplete` TINYINT(1) NOT NULL DEFAULT 0; 

UPDATE tblTerminalReportHistory SET IsMallFileUploadComplete = 1 WHERE MallFileName IS NOT NULL;

ALTER TABLE tblDeposit ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblWithHold ADD `Remarks` VARCHAR(255); 
ALTER TABLE tblDisburse ADD `Remarks` VARCHAR(255); 

/*********************************  v_2.0.0.3.sql END  *******************************************************/
