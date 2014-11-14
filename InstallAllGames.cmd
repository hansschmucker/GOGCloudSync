@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

:NOPATH

for /F "tokens=1,2,* delims= " %%i IN ('reg query HKLM\SOFTWARE\Wow6432Node\GOG.com /v DefaultPackPath^|find "DefaultPackPath"') DO (
	SET GAMEROOTPATH=%%k
)

IF "%GAMEROOTPATH%"=="" for /F "tokens=1,2,* delims= " %%i IN ('reg query HKLM\SOFTWARE\GOG.com /v DefaultPackPath^|find "DefaultPackPath"') DO (
	SET GAMEROOTPATH=%%k
)

IF "%GAMEROOTPATH%"=="" (
	SET /P GAMEROOTPATH="Please enter the root path under which the games will be installed [C:\GOG\] "
	IF "%GAMEROOTPATH%"=="" SET GAMEROOTPATH=C:\GOG\
	MKDIR "%GAMEROOTPATH%"
	IF NOT EXIST "%GAMEROOTPATH%" (
		ECHO Could not create %GAMEROOTPATH%
		GOTO :NOPATH
	)
)

for /F "tokens=4,* delims=\" %%h in ('DIR /S /B setup_*_2.*.*.*.exe') DO @(
	SET GAMEINSTALLER=%%h\%%i
	for /F "tokens=1,* delims=_" %%j in ('ECHO %%i') DO @(
		for /F "tokens=1,* delims=." %%l in ('ECHO %%k') DO @(
			SET GAMENAME_SUFFIXED=%%l
			SET GAMENAME=!GAMENAME_SUFFIXED:~0,-2!
			ECHO Game found. !GAMENAME!

			IF NOT EXIST "%GAMEROOTPATH%\!GAMENAME!" (
				ECHO No installation found. Installing to %GAMEROOTPATH%\!GAMENAME!
				start /w "" "%~dp0\!GAMEINSTALLER!" /DIR="%GAMEROOTPATH%\!GAMENAME!" /SP- /SILENT /VERYSILENT /NORESTART /CLOSEAPPLICATIONS
			)
		)
	)
)
	

ENDLOCAL
