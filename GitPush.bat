@echo off
SET /P CommitName=Enter Commit Name:
git add -A
git commit -m "%CommitName%"
git push origin master