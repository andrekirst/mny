using ConsoleApp.Database;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConsoleApp.Commands.App;

public class UpgradeAppCommand(MoneyDbContext dbContext) : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.Status()
            .Start("Upgrade", ctx =>
            {
                ctx.SpinnerStyle(Style.Parse("green"));
                dbContext.Database.Migrate();
            });

        return 0;
    }
}