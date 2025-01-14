using ConsoleApp.Database;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.Account;

public sealed class ListAccountsCommand(MoneyDbContext dbContext) : Command<ListAccountsCommand.ListAccountsSettings>
{
    public sealed class ListAccountsSettings : CommandSettings;

    public override int Execute(CommandContext context, ListAccountsSettings settings)
    {
        var accounts = dbContext.Accounts
            .Select(a => new
            {
                Id = a.Id.ToString(),
                a.Name,
                a.IsSelected
            })
            .ToList();

        var table = new Table
        {
            Title = new TableTitle("Accounts", new Style(foreground: Color.Aquamarine3))
        };

        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var account in accounts)
        {
            if (account.IsSelected)
            {
                table.AddRow($"[bold green]{account.Id}[/]", $"[bold green]{account.Name}[/]");
            }
            else
            {
                table.AddRow(account.Id, account.Name);
            }
        }
        
        AnsiConsole.Write(table);

        return 0;
    }
}