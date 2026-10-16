# Hospital Management System

A Windows Forms desktop application for managing hospital-related operations. This educational project was developed with C#, .NET Framework, and Microsoft SQL Server.

## Features

- Patient registration and login
- Doctor login and profile management
- Secretary login
- Doctor and department management
- Appointment management
- Patient and doctor information screens
- Announcements

## Technologies

- C#
- Windows Forms
- .NET Framework 4.7.2
- Microsoft SQL Server / SQL Server Express
- ADO.NET

## Requirements

- Windows
- Visual Studio with the **.NET desktop development** workload
- .NET Framework 4.7.2 Developer/Targeting Pack
- SQL Server Express or another compatible local SQL Server instance
- SQL Server Management Studio (SSMS), to restore the demo database backup

## Repository structure

```text
MvY-hastane-yonetim-sistemi/
├── HastaneProje.sln
├── HastaneProje.bak
├── README.md
├── .gitignore
└── HastaneProje/
    ├── HastaneProje.csproj
    ├── App.config
    ├── Program.cs
    ├── sqlBaglantisi.cs
    ├── Properties/
    └── ... forms, resources, and DataSet files
```

## Setup and running

### 1. Download the project

Download or clone this repository and open `HastaneProje.sln` in Visual Studio.

### 2. Restore the demo database

The repository includes the backup file `HastaneProje.bak` in the repository root.

1. Open SQL Server Management Studio (SSMS) and connect to your local SQL Server instance.
2. In Object Explorer, right-click **Databases** and select **Restore Database...**.
3. Under **Source**, choose **Device**, click the browse (`...`) button, and select the `HastaneProje.bak` file downloaded with this repository.
4. Set the destination database name to `HastaneProje`.
5. Open the **Files** page and check the restore paths. If necessary, change them to folders where the SQL Server service has permission to write.
6. Select **OK** and confirm that `HastaneProje` appears under **Databases**.

If a database named `HastaneProje` already exists, do not overwrite it unless you are sure it is safe to replace. Back up any database you need to keep first.

### 3. Configure the SQL Server connection

Open `HastaneProje/App.config` in Visual Studio. In the `<connectionStrings>` section, find the connection string with this name:

`HastaneProje.Properties.Settings.HastaneProjeConnectionString`

The default example is:

```xml
<add name="HastaneProje.Properties.Settings.HastaneProjeConnectionString"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;Encrypt=False"
     providerName="System.Data.SqlClient" />
```

`Data Source=.\SQLEXPRESS` means a SQL Server Express instance named `SQLEXPRESS` on the same computer. If your SQL Server instance has a different name, change only the `Data Source` value to match the server/instance you connect to in SSMS. Common examples are:

- SQL Server Express: `Data Source=.\SQLEXPRESS`
- Default local SQL Server instance: `Data Source=.`
- LocalDB: `Data Source=(localdb)\MSSQLLocalDB`

Keep `Initial Catalog=HastaneProje` consistent with the database name used when restoring the backup. With `Integrated Security=True`, the application uses the current Windows account. That account must have permission to access the SQL Server instance and the `HastaneProje` database.

The connection helper reads this named setting from `App.config`; no personal computer name needs to be hard-coded in `sqlBaglantisi.cs`. After changing `App.config`, save the file and rebuild the solution. For a built executable, the corresponding configuration file is `HastaneProje.exe.config` beside the `.exe` file.

### 4. Build and run

1. In Visual Studio, select the appropriate build configuration, usually **Debug**.
2. Select **Build > Build Solution** (or press `Ctrl+Shift+B`).
3. Press `F5` to run the application.

## Troubleshooting

- **Cannot connect to SQL Server:** Make sure the SQL Server service is running and `Data Source` matches the instance name shown in SSMS.
- **Database not found:** Restore `HastaneProje.bak` with the destination name `HastaneProje`, or update `Initial Catalog` to match your database name.
- **Login failed / access denied:** Confirm the current Windows account has permission to connect to the instance and access the database.
- **Restore path or file-access error:** On the restore dialog's **Files** page, choose data and log paths that the SQL Server service can access.
- **`ConfigurationManager` compile error:** In Visual Studio, check that the project has a reference to `System.Configuration`.

## Security and project status

This is an educational project, not a production-ready hospital information system. Use synthetic demonstration data only. Do not add real patient information or production credentials to this public repository. Authentication, password storage, authorization, input validation, database permissions, and error handling should be reviewed before any real-world use.

The included database backup is provided for demonstration. Database restoration and application execution should be verified on the target computer because the SQL Server instance name, permissions, and file paths can vary between environments.
