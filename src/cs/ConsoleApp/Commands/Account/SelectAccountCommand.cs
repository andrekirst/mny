using ConsoleApp.Database;
using ConsoleApp.Errors;
using ConsoleApp.Infrastructure.Extensions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.Account;

public class SelectAccountCommand(AccountRepository accountRepository) : Command<SelectAccountCommand.SelectActiveAccountCommandSettings>
{
    public class SelectActiveAccountCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[Id]")]
        public required int Id { get; init; }
    }

    public override int Execute(CommandContext context, SelectActiveAccountCommandSettings settings)
    {
        var id = settings.Id;
        
        if (accountRepository.ExistsById(id))
        {
            return accountRepository.SelectAccount(1);
        }
        
        var error = new AccountWithIdNotFoundError(id);
        AnsiConsole.Console.WriteError(error);
        return error.Code;
    }
}