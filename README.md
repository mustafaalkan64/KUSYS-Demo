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









