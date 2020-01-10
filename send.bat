@echo off

:main
echo.
echo Got command: "%1"

if "%1" == "" goto end

for /f "delims=" %%A in ('python -c "import getpass; print(getpass.getpass('Password: '));"') do @set pass=%%A
set login=-u ftp99691459-0 -p %pass% access812755966.webspace-data.io

if "%1" == "all" (
	set source=.\LanguageLearner\bin\Release\netcoreapp2.2\publish
	set dest=~
	goto sendfolder
)

if "%1" == "wwwroot" (
	set source=.\LanguageLearner\bin\Release\netcoreapp2.2\publish\wwwroot
	set dest=publish\
	goto sendfolder
)

if "%1" == "runtimes" (
	set source=.\LanguageLearner\bin\Release\netcoreapp2.2\publish\runtimes
	set dest=publish\
	goto sendfolder
)

if "%1" == "json" (
	set source=.\LanguageLearner\bin\Release\netcoreapp2.2\publish\*.json
	set dest=publish\
	goto sendfiles
)

if "%1" == "config" (
	set source=.\LanguageLearner\bin\Release\netcoreapp2.2\publish\*.config
	set dest=publish\
	goto sendfiles
)

:sendfolder
NcFTP\ncftpput -R -m %login% %dest% %source%
goto exit

:sendfiles
NcFTP\ncftpput -m %login% %dest% %source%
goto exit


:end
echo 1. all		to send full publish folder
echo 2. wwwroot	to send only wwwroot
echo 3. runtimes	to send only compiled and json files in publish folder
echo 4. json		to send only compiled and json files in publish folder
echo 5. config	to send only compiled and json files in publish folder
goto exit

:: Old way of sending something
:sendfolder
echo user ftp99691459-0> ftpcmd.dat
echo FtpPassword2+>> ftpcmd.dat
echo binary>> ftpcmd.dat
echo send %folderName%>> ftpcmd.dat
echo disconnect>> ftpcmd.dat
echo quit>> ftpcmd.dat
ftp -n -s:ftpcmd.dat access812755966.webspace-data.io
del ftpcmd.dat
goto exit

:exit
echo.
