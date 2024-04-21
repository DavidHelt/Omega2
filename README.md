#Omega
--
Description:
--
This is window application, which is email-like app, that serves for sending messagges to users.

Its simple, you just register a new user, then login. After you login you will be asked to do my ,,captcha" and you are in the menu, where 
you can choose if you want to send message, look at the history of sent messages and see received messages and use other functions I made.

My app does have another functions like info about your profile, for example which hobbies you like, what gender you are, etc.. but this is not compulsary,
so if you do not wish to fill any of these informations, its okay :) But if you want to reset your password, you should fill in hobby. It is because of verification
reasons. 

Installation:
--
To install, launch MSSQL. login and with this login you use, fill app.config. To be precise copy server name, insert it in DataSource,
which is located at the top of config file, InitialCatalog is identical (messengerApp), IntegratedSecurity is True if you use localhost, or windows auth.
If you use SQL Auth, which is in school you uncomment Username and Password and fill it with login

Import SQL file to MSSQL,create database MessengerApp and execute the SQL files, first SqlImport1, then UserInfo. Then you just start up my project, register and 
do whatever my app offers.

