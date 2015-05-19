			
/********************************************
	eventTerminalReportHistorySyncTransactionSales
	-- Once only to resync all
********************************************/
delimiter GO
DROP EVENT IF EXISTS eventTerminalReportHistorySyncTransactionSales
GO

CREATE EVENT eventTerminalReportHistorySyncTransactionSales
    ON SCHEDULE
		AT '2015-04-14 00:01:00'
    DO 
	BEGIN
		
		CALL procTerminalReportHistorySyncTransactionSales(1, '05', '2015-04-11 19:00');
		CALL procTerminalReportHistorySyncTransactionSales(9, '22', '2015-04-11 19:00');
		
	END;
GO
delimiter ;
