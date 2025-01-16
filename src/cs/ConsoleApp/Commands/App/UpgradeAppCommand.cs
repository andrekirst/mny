using System.Reflection;
using ConsoleApp.Database;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Spectre.Console.Cli;
using SQLitePCL;

namespace ConsoleApp.Commands.App;

public class UpgradeAppCommand : Command
{
    public override int Execute(CommandContext context)
    {
        using var connection = new SqliteConnection(DatabaseModule.ConnectionString);
        raw.SetProvider(new SQLite3Provider_e_sqlite3());
        connection.Open();

        using var database = new DbUp.Sqlite.Helpers.SharedConnection(connection);
        var upgrader = DbUp.DeployChanges
            .To
            .SqliteDatabase(connection.ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        if (!upgrader.IsUpgradeRequired())
        {
            return 0;
        }

        AnsiConsole.Status()
            .Start("Upgrade database", _ =>
            {
                upgrader.PerformUpgrade();
            });

        return 0;
    }
}