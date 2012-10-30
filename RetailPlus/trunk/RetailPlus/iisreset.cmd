@ECHO OFF

For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set date=%%a%%b%%c)
For /f "tokens=1-3 delims=: " %%a in ('time /t') do (set time=%%a%%b%%c)

echo "%date%%time%: resetting iis" > c:\iisreset.txt
iisreset
echo "%date%%time%: done resetting iis" > c:\iisreset.txt
echo.

@ECHO ON