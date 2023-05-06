# KUSYS-Demo

## Initial

First edit connection string on appsetting.json in KUSYS-Demo.UI Project like below:
```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Initial Catalog=KUSYSDB;Integrated Security=True;"
}
```

Go Packager Manager Console \
Select Default Project as **"KUSYS-Demo.Infastructure"** \
Run  **"update-database"**

After doing these, your local or remote db will be initialized. \

Run the "KUSYS-Demo.UI" project. \
Account/Login screen will be apperead.

You can login with admin user credentials: \
**UserName**: admin@gmail.com \
**Password**: mypassword_?  

As Admin User,
You can list students, create update or delete any student \
Assign courses to students, \
List matched course and students.

When you create a new student, a new user will be created together.
And default user password will be **"Password_123"**
You can also change it from AppSetting.json file

So, you can login with email specified and default password above.

## Technologies, Utilities and Infastructure:

- Dotnet 6 Core Mvc Razor
- Ef Core 6 Code First
- Asp.net Core Identity
- Generic Repository
- Unit of Work
- Admin LTE 3 Boostrap Admin UI Templates

## Screenshots

![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/1.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/2.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/3.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/4.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/5.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/6.PNG)
![alt text](https://github.com/mustafaalkan64/KUSYS-Demo/blob/master/KUSYS-Demo.UI/ScreenShots/7.PNG)




