@echo off
rem SET /P CommitName=Enter Commit Name:

	for /F "tokens=1,2,3 DELIMS=. " %%i IN ('date /t') DO (
 	set DT_DAY=%%i
	set DT_MON=%%j
	set DT_YEAR=%%k)

	for /F "tokens=1,2 DELIMS=: " %%i IN ('time/t') DO (
 	set DT_HOUR=%%i
	set DT_MIN=%%j)

	set dat=%DT_YEAR%-%DT_MON%-%DT_DAY% %DT_HOUR%%DT_MIN%

git add -A
rem  "%CommitName%"
git commit -m "%dat%"
git push origin master
pause