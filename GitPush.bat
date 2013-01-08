@echo off
SET /P CommitName=Enter Commit Name:
echo git commit -m "%CommitName%"
;git add -A
;git commit -m "%CommitName%"
;git push origin master