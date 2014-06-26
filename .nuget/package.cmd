@echo Off
set configuration=%1
set version=%2
REM set version=1.0.0.0
if "%configuration%" == "" (
   set configuration=Release
)


rd ..\Download /s /q  REM delete the old stuff

if not exist ..\Download mkdir ..\Download

REM copy LICENSE.txt ..\Download\
REM copy readme.txt ..\Download\package\

..\.nuget\NuGet.exe update -self

..\.nuget\NuGet.exe pack "..\.nuget\Zirpl.AppEngine.nuspec" -Output ..\Download -Version "%version%" -Prop configuration=%configuration%
if not "%errorlevel%"=="0" goto failure
..\.nuget\NuGet.exe pack "..\.nuget\Zirpl.AppEngine.Web.nuspec" -Output ..\Download -Version "%version%" -Prop configuration=%configuration%
if not "%errorlevel%"=="0" goto failure
..\.nuget\NuGet.exe pack "..\.nuget\Zirpl.AppEngine.EntityFramework.nuspec" -Output ..\Download -Version "%version%" -Prop configuration=%configuration%
if not "%errorlevel%"=="0" goto failure
..\.nuget\NuGet.exe pack "..\.nuget\Zirpl.AppEngine.Testing.nuspec" -Output ..\Download -Version "%version%" -Prop configuration=%configuration%
if not "%errorlevel%"=="0" goto failure

:success
REM use github status API to indicate commit compile success
exit 0

:failure
REM use github status API to indicate commit compile success
exit -1