@REM This file will build the code and put the resulting
@REM Umbraco package in the "dist" folder.
cd ../src
call npm install --quiet
call grunt package-full