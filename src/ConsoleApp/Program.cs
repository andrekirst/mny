using ConsoleApp.Commands.Account;
using ConsoleApp.Commands.App;
using ConsoleApp.Database;
using ConsoleApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ConsoleApp;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<MoneyDbContext>(options =>
        {
            options.UseSqlite(MoneyDbContext.ConnectionString);
        });

        var registrar = new TypeRegistrar(services);
        
        var commandApp = new CommandApp(registrar);
        commandApp.Configure(config =>
        {
            config.SetApplicationName("mny");
            
            config.AddBranch("account", account =>
            {
                account.AddCommand<AddAccountCommand>("add");
                account.AddCommand<ListAccountsCommand>("list");
                account.AddCommand<SelectActiveAccountCommand>("select");
            });

            config.AddBranch("app", app =>
            {
                app.AddCommand<UpgradeAppCommand>("upgrade");
            });
        });

        return await commandApp.RunAsync(args);
    }
}