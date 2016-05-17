@echo off
reg add HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\InRiverServerWindowsService6 /t REG_EXPAND_SZ /v ImagePath /d "\"C:\Program Files\inRiver AB\inRiver Server\inRiver.Server.exe\"" /f
pause
