@echo off

REM This file allows you to update the frontend assets without interrupting the current Visual Studio build.

cd ../src/Website/

START /WAIT /B ../CustomBuildActions/bin/debug/net6.0/CustomBuildActions.exe -generate-css-for-svg-icons -generate-package-manifest -copy-static-assets-to-website -watch