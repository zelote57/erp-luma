@ECHO OFF
For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set date=%%a%%b%%c)

For /f "tokens=1-3 delims=: " %%a in ('time /t') do (set time=%%a%%b%%c)

IF NOT EXIST c:\database (
	mkdir c:\database
)

IF EXIST c:\database\pos_%date%.sql (
	MOVE "c:\database\pos_%date%.sql" "c:\database\pos_%date%.bak"
)

c:\mysql\bin\mysqldump -u POSUser pos -ppospwd -R > c:\database\pos_%date%.sql
echo Database has been backup @ c:\database\pos_%date%.sql
echo.

IF EXIST d:\database\pos_%date%.bak (
	del c:\database\pos_%date%.bak /f /q		
)

@ECHO ON