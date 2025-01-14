using ConsoleApp.Database;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.Account;

public class SelectActiveAccountCommand(MoneyDbContext dbContext) : Command<SelectActiveAccountCommand.SelectActiveAccountCommandSettings>
{
    public class SelectActiveAccountCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[Id]")]
        public int Id { get; set; }
    }

    public override int Execute(CommandContext context, SelectActiveAccountCommandSettings settings)
    {
        if (!dbContext.Accounts.Any(a => a.Id == settings.Id))
        {
            AnsiConsole.Console.WriteException(new Exception($"Account with Id {settings.Id} not found"));
            return ErrorCodes.Account.IdNotFound;
        }

        dbContext.Accounts
            .Where(a => a.IsSelected)
            .ExecuteUpdate(calls => calls.SetProperty(p => p.IsSelected, false));

        dbContext.Accounts
            .Where(a => a.Id == settings.Id)
            .ExecuteUpdate(calls => calls.SetProperty(p => p.IsSelected, true));

        return 0;
    }
}