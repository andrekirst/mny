using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp.Database;

public class MoneyDbContextFactory : IDesignTimeDbContextFactory<MoneyDbContext>
{
    public MoneyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MoneyDbContext>();
        optionsBuilder.UseSqlite(MoneyDbContext.ConnectionString);

        return new MoneyDbContext(optionsBuilder.Options);
    }
}