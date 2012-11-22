drop table tblCalDate;

CREATE TABLE tblCalDate(
	`CalDate` Date,
    INDEX `IX_tblCalDate`(`CalDate`)
);


DROP PROCEDURE procSetupCalendarDate;

delimiter GO
CREATE PROCEDURE procSetupCalendarDate(IN strYear VARCHAR(4))
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DELETE FROM tblCalDate WHERE YEAR(CalDate) = strYear;
	REPEAT 
		insert into tblCalDate(CalDate)values(DATE_ADD(DATE_FORMAT(CONCAT(strYear,'-01-01'), '%Y-%m-%d'), INTERVAL lngCtr DAY));
		SET lngCtr = lngCtr + 1; 
		UNTIL lngCtr = 366
	END REPEAT;
END;
GO
delimiter ;

call procSetupCalendarDate('2012');
