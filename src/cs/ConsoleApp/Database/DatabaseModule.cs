using System.Data.SQLite;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Database;

public static class DatabaseModule
{
    public const string ConnectionString = "Data Source=local.db";
    
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services
            .AddSingleton<SQLiteConnection>(_ =>
            {
                var connection = new SQLiteConnection(ConnectionString);
                connection.Open();
                return connection;
            });
    }
}