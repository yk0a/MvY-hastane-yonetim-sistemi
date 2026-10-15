# Hospital Management System

A Windows Forms desktop application for managing hospital-related operations, developed with C# and Microsoft SQL Server.

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
- Visual Studio with **.NET desktop development** support
- .NET Framework 4.7.2 Developer/Targeting Pack
- SQL Server Express or another compatible local SQL Server instance
- SQL Server Management Studio (SSMS), to restore the demo database

## Setup

### 1. Download the project

Download this repository and open `HastaneProje.sln` in Visual Studio.

### 2. Restore the demo database

The intended public demo backup path is `database/HastaneProje_demo.bak`. Use only synthetic demonstration data; never publish real patient information.

1. Open SQL Server Management Studio (SSMS) and connect to your local SQL Server instance, such as `YOURPC\SQLEXPRESS`.
2. Right-click **Databases** and choose **Restore Database...**.
3. Under **Source**, choose **Device**, click the browse button, and select `database/HastaneProje_demo.bak`.
4. Set the destination database name to **HastaneProje**.
5. On the **Files** page, verify the database and log files will be restored to locations writable by the SQL Server instance. Change the paths if necessary.
6. Click **OK** and confirm that `HastaneProje` appears under **Databases**.

If `database/HastaneProje_demo.bak` is not present in the repository yet, the maintainer still needs to create and upload it; see the maintainer note at the end of this README.

### 3. Configure the connection string

This project uses the standard .NET Framework `App.config` connection-string section instead of a custom `dBaddress.txt` file. Open `HastaneProje/App.config` and edit the connection string to match your local SQL Server instance:

```xml
<add name="HastaneProje.Properties.Settings.HastaneProjeConnectionString"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;Encrypt=False"
     providerName="System.Data.SqlClient" />
```

Examples for `Data Source`:

- SQL Server Express instance named `SQLEXPRESS`: `Data Source=.\SQLEXPRESS`
- Default local SQL Server instance: `Data Source=.`
- LocalDB instance: `Data Source=(localdb)\MSSQLLocalDB`

Keep `Initial Catalog=HastaneProje` consistent with the name used when restoring the backup. With `Integrated Security=True`, the application uses the current Windows user's SQL Server permissions; no SQL username or password is stored in this example.

When building in Visual Studio, edit `App.config` and rebuild. For a prebuilt application, the corresponding editable file is `HastaneProje.exe.config` next to the executable. Do not commit personal computer names or production credentials.

### 4. Run the application

1. Select the appropriate build configuration in Visual Studio.
2. Build the solution.
3. Press **F5** to run the application.

## Database distribution

The preferred distribution method for this SQL Server project is a sanitized `.bak` backup plus clear restore instructions. Do not use an absolute path to your own `.mdf` file as the app's connection setting: the physical database files need to be attached to a SQL Server instance, and the database engine must have appropriate access permissions. The connection string identifies the server instance and database name after restoration.

## Troubleshooting

- **Cannot connect to server:** verify SQL Server is installed and running, and check `Data Source` in `App.config`.
- **Database not found:** make sure the backup was restored as `HastaneProje` or update `Initial Catalog` accordingly.
- **Login failed:** verify that the current Windows account has permission to access the restored database.
- **Restore path/access denied:** choose data and log file destinations that are writable by the SQL Server service account.

## Security note

This is an educational project, not a production-ready hospital system. Review authentication, password storage, authorization, input validation, error handling, and database permissions before any real-world use. Use synthetic demo data only, and never place production credentials in a public repository.

---

## Maintainer note — before publishing

Create a full backup of the cleaned, synthetic `HastaneProje` database in SSMS (**Databases → HastaneProje → Tasks → Back Up...**). Save it as `database/HastaneProje_demo.bak`, then verify that it can be restored to a fresh database/instance before publishing. For browser-based GitHub uploads, each file must be no larger than 25 MiB; if the backup exceeds that, publish it as a release asset or provide schema-and-seed SQL scripts instead.
