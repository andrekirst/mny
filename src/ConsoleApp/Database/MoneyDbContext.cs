using ConsoleApp.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Database;

public class MoneyDbContext(DbContextOptions<MoneyDbContext> options) : DbContext(options)
{
    public const string ConnectionString = "Data Source=local.db";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }

    public DbSet<Account> Accounts => Set<Account>();
}