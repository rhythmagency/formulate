@echo off

REM This file allows you to update the frontend assets without interrupting the current Visual Studio build.

cd ../src/Website/

START /WAIT /B ../CustomBuildActions/bin/debug/net6.0/CustomBuildActions.exe -everything