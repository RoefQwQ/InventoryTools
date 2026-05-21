@echo off
setlocal

set CONFIGURATION=Release
set INTERNAL_NAME=InventoryTools

echo === Building %INTERNAL_NAME% (%CONFIGURATION%) ===

dotnet build -c %CONFIGURATION% %INTERNAL_NAME%
if %ERRORLEVEL% neq 0 (
    echo Build failed!
    exit /b 1
)

echo === Packaging to output\latest.zip ===

if not exist output mkdir output
if exist output\latest.zip del output\latest.zip

powershell -Command "Compress-Archive -Path '%INTERNAL_NAME%\bin\%CONFIGURATION%\%INTERNAL_NAME%\*' -DestinationPath 'output\latest.zip' -Force"

echo === Done! Output: output\latest.zip ===
