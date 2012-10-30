@ECHO OFF
For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set date=%%a%%b%%c)

For /f "tokens=1-3 delims=: " %%a in ('time /t') do (set time=%%a%%b%%c)

IF NOT EXIST c:\database (
	mkdir c:\database
)
	
IF EXIST c:\database\pos_%date%%time%.sql (
	del c:\database\pos_%date%%time%.sql /f /q		
)

c:\mysql\bin\mysqldump -u root pos -R > c:\database\pos_%date%%time%.sql
echo Database has been backup @ c:\database\pos_%date%%time%.sql
echo.

@ECHO ON