using System.ComponentModel;
using ConsoleApp.Database;
using ConsoleApp.Errors;
using ConsoleApp.Infrastructure.Extensions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.Account;

public class AddAccountCommand(AccountRepository accountRepository) : Command<AddAccountCommand.AddAccountCommandSettings>
{
    public sealed class AddAccountCommandSettings : CommandSettings
    {
        [Description("The name of the account")]
        [CommandOption("-p|--name")]
        public required string Name { get; init; } = null!;
    }

    public override int Execute(CommandContext context, AddAccountCommandSettings settings)
    {
        var name = settings.Name.Trim();

        if (accountRepository.ExistsByName(name))
        {
            var error = new AccountAlreadyExistsByNameError(name);
            AnsiConsole.Console.WriteError(error);
            return error.Code;
        }
        
        var affectedRows = accountRepository.Add(name);
        return affectedRows;
    }
}