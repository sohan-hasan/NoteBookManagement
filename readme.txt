////////////////////////////////////////
************INSTALLATION***************

##STEP - 1
Extract WebApiNoteManagement and webNoteManagementByAngular file.

##STEP - 2
Run WebApiNoteManagement on Visual Studio

##STEP - 3
Open Package Manager Console
Run the command : Update-Database
note: if occur any error please check connection string form appsettings.json

##STEP - 4
Run WebApiNoteManagement form IIS Express

For Angular Part:

##STEP - 1
Open webNoteManagementByAngular

##STEP - 2
Open CMD form this folder

##STEP - 3
Run the command : npm install

##STEP - 4
After finish npm install Run Command : ng serve --open

Register a new user and signin.


Basic Features:
1)User can signup with name, email, date of birth, password.
2)User can login using email & password.
3)User can create four types of note.
	i)Regular Note: User can take a text note
	i)Reminder Note : User can set a reminder note and it will notified / reminder user.
	iii) Task : user can take a text note, set a due date (date+time) and update whether or not this task is complete.
	iv)Bookmark:user can bookmark any valid web URL
	V) All types of note you can insert, update and delete;
4)max length of a note is 100 characters.
5) There has a user dashboard, from dashboard he/she will see all the notes (all four types)
6) User will be redirected to dashboard after login
7) From dashboard, user can see which todo or reminder is for today, this week and this month
8) Allow user to logout

Technologies:
1).net 5 core web api
2)Angular 13 for frontend development.
3)Asp.net Identity Authentication for Authenticaton and Authorization,
4)I also use JWT TOKEN for REST Api and frontend secure commiunication,
5)SignalR for send real time notification.
6)SqlServer 2019
7)Microsoft.EntityFrameworkCore