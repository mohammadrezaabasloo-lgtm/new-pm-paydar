@echo off
chcp 65001 >nul
echo این اسکریپت به Windows و .NET 10 SDK و workload مربوط به MAUI نیاز دارد.
dotnet workload install maui
dotnet restore PMEquipment.csproj
if errorlevel 1 goto failed
dotnet publish PMEquipment.csproj -f net10.0-windows10.0.19041.0 -c Release -r win10-x64 --self-contained true -p:WindowsPackageType=None -p:WindowsAppSDKSelfContained=true -p:Platform=x64 -o publish
if errorlevel 1 goto failed
echo.
echo خروجی در پوشه publish ساخته شد. فایل PMEquipment.exe را اجرا کنید.
pause
exit /b 0
:failed
echo ساخت با خطا متوقف شد. متن خطا را برای بررسی ارسال کنید.
pause
exit /b 1
