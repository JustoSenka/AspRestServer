@echo off

:main
echo.
echo Got command: "%1"

if "%1" == "" goto end

for /f "delims=" %%A in ('python -c "import getpass; print(getpass.getpass('Password: '));"') do @set pass=%%A
set login=-u ftp99691459-0 -p %pass% access812755966.webspace-data.io

if "%1" == "all" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish
	set dest=~
	goto sendfolder
)

if "%1" == "proj" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish\*.*
	set dest=publish\
	goto sendfiles
)

if "%1" == "wwwroot" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish\wwwroot
	set dest=publish\
	goto sendfolder
)

if "%1" == "runtimes" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish\runtimes
	set dest=publish\
	goto sendfolder
)

if "%1" == "json" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish\*.json
	set dest=publish\
	goto sendfiles
)

if "%1" == "config" (
	set source=.\LanguageLearner\bin\Release\netcoreapp3.1\publish\*.config
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
echo 2. proj		to send all files in publish, but not folders, runtimes or libraries
echo 3. wwwroot	to send only wwwroot
echo 4. runtimes	to send only compiled and json files in publish folder
echo 5. json		to send only compiled and json files in publish folder
echo 6. config	to send only compiled and json files in publish folder
goto exit


:exit
echo.
