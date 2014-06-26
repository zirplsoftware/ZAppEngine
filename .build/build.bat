@echo Off
set configuration=%1
set version=%2
REM set version=1.0.0.0
if "%configuration%" == "" (
   set configuration=Release
)

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\Zirpl.AppEngine.sln /p:Configuration=%configuration% /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:true /p:BuildInParallel=true /p:RestorePackages=true /t:Clean,Rebuild
if not "%errorlevel%"=="0" goto failure

..\.nuget\package.cmd

:success
REM use github status API to indicate commit compile success
exit 0

:failure
REM use github status API to indicate commit compile success
exit -1