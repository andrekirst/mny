using System.ComponentModel;
using ConsoleApp.Database;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.Account;

public class AddAccountCommand(MoneyDbContext dbContext) : Command<AddAccountCommand.AddAccountCommandSettings>
{
    public sealed class AddAccountCommandSettings : CommandSettings
    {
        [Description("The name of the account")]
        [CommandOption("-p|--name")]
        public string Name { get; init; } = null!;
    }

    public override int Execute(CommandContext context, AddAccountCommandSettings settings)
    {
        var name = settings.Name
            .Trim();
        
        if (dbContext.Accounts.Any(a => a.Name.ToUpper() == name.ToUpper()))
        {
            AnsiConsole.Console.WriteException(new Exception($"Account with name {settings.Name} already exist"));
            return 1;
        }

        dbContext.Accounts.Add(new Database.Model.Account
        {
            Name = name
        });

        var affectedRows = dbContext.SaveChanges();

        return affectedRows == 1 ? 0 : 1;
    }
}