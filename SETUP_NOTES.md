# Implementation notes

## Chosen setup design

- Use a sanitized SQL Server backup (`database/HastaneProje_demo.bak`) for the demo database.
- Restore that backup once in the recipient's own SQL Server instance.
- Keep the connection string in `App.config`, which is the standard configuration mechanism for a .NET Framework desktop app. `HastaneProje.exe.config` is produced beside the EXE at build time and can be edited after deployment.
- `sqlBaglantisi.cs` reads the named connection string from `App.config`; there is one runtime source of truth, not a hard-coded computer name in C#.

## Apply the files

1. Replace the existing `sqlBaglantisi.cs`.
2. Replace `App.config` with the included template, or update only its connection string using the template as reference.
3. Add the framework reference: in Visual Studio, right-click **References** → **Add Reference** → **Assemblies / Framework** → select **System.Configuration**.
4. In `Properties/Settings.settings`, update its connection string to the same generic value (or remove the unused setting after verifying no code uses it). Do not manually edit `Settings.Designer.cs`; it is generated.
5. Create `database/HastaneProje_demo.bak` from the cleaned demo database in SSMS and add it to the repo. Test restore before publishing.

Sample local Express connection string:

```text
Data Source=.\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;Encrypt=False
```

For a local-only educational app, integrated Windows authentication means the config need not contain a SQL password. `Encrypt=False` is kept for compatibility with the current local SQL Server setup; it is not a template for a remote/production database. For remote connections, configure encryption and validate the server certificate, use least-privilege credentials, and never commit secrets.

References:
- Microsoft: connection strings and configuration files — https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/connection-strings-and-configuration-files
- Microsoft: attach database file permissions — https://learn.microsoft.com/en-us/sql/relational-databases/databases/attach-a-database
- GitHub: browser uploads are limited to 25 MiB per file — https://docs.github.com/en/repositories/working-with-files/managing-files/adding-a-file-to-a-repository
