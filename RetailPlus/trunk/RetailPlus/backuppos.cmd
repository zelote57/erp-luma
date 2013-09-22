@ECHO OFF
For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set date=%%a%%b%%c)

For /f "tokens=1-3 delims=: " %%a in ('time /t') do (set time=%%a%%b%%c)

IF NOT EXIST d:\database (
	mkdir d:\database
)

IF EXIST d:\database\pos_%date%.sql (
	MOVE "d:\database\pos_%date%.sql" "d:\database\pos_%date%.bak"
)

c:\mysql\bin\mysqldump -u root pos -ppospwd -R > d:\database\pos_%date%.sql
echo Database has been backup @ d:\database\pos_%date%.sql
echo.

IF EXIST d:\database\pos_%date%.bak (
	del d:\database\pos_%date%.bak /f /q		
)

@ECHO ON