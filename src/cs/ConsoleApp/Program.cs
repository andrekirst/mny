using ConsoleApp.Commands.Account;
using ConsoleApp.Commands.App;
using ConsoleApp.Database;
using ConsoleApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ConsoleApp;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<AccountRepository>();
        services.AddDataAccess();

        var registrar = new TypeRegistrar(services);
        
        var commandApp = new CommandApp(registrar);
        commandApp.Configure(config =>
        {
            config.SetApplicationName("mny");
            config.PropagateExceptions();

            config.AddBranch("list", list =>
            {
                list.AddCommand<ListAccountsCommand>("accounts");
            });
            
            config.AddBranch("add", add =>
            {
                add.AddCommand<AddAccountCommand>("account");
            });

            config.AddBranch("select", select =>
            {
                select.AddCommand<SelectAccountCommand>("account");
            });

            config.AddBranch("upgrade", upgrade =>
            {
                upgrade.AddCommand<UpgradeAppCommand>("app");
            });
        });

        return await commandApp.RunAsync(args);
    }
}