INSERT INTO tblTerminal (`TerminalNo`, `TerminalCode`, `TerminalName`, DateCreated, MachineSerialNo, AccreditationNo )
			VALUES		('02', 'Terminal No. 02', 'Terminal No. 02', NOW(), '000000', '0000-000-00000-000'); 
			
			
INSERT INTO tblTerminalReport (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
			VALUES		('00000000000001', '00000000000001', 1, 1, 2, '02', DATE_SUB(DATE(NOW()), INTERVAL 1 DAY));
			

INSERT INTO tblTerminalReportHistory (`BeginningTransactionNo`, `EndingTransactionNo`, `ZReadCount`, `XReadCount`, `TerminalID`, `TerminalNo`, `DateLastInitialized`)
			VALUES		('00000000000000', '00000000000000', 0, 0, 2, '02', DATE_SUB(DATE(NOW()), INTERVAL 2 DAY));
			

INSERT INTO tblCashierReport (`CashierID`, `TerminalID`, `TerminalNo`, `LastLoginDate`)
			VALUES		(1, 2, '02', "0001-01-01 00:00");


