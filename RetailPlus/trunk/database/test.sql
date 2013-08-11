
/**************************************************************

	procSalutationSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the all salutation's list

	CALL procSalutationSelect(null, null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSalutationSelect
GO

create procedure procSalutationSelect(
			IN Salutation varchar(30),
			IN SortField varchar(60),
			IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 ConfigValue
							,ConfigName
							
						FROM sysConfig cnfg
						WHERE Category = ''Salutation'' ');
	
	IF IFNULL(Salutation,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND ConfigName LIKE ''%',Salutation,'%'' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','cnfg.ConfigValue',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;
