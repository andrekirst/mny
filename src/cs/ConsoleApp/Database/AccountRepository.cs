using System.Data.SQLite;
using ConsoleApp.Database.Model;
using Dapper;
using Spectre.Console;

namespace ConsoleApp.Database;

public class AccountRepository(SQLiteConnection connection)
{
    public int Add(string name)
    {
        const string sql = "INSERT INTO Accounts(Name) VALUES (@name)";

        return connection.Execute(sql, new { name });
    }

    public bool ExistsByName(string name)
    {
        const string sql = "SELECT COUNT(1) FROM Accounts a WHERE lower(a.Name) == @name";
        var count = connection.QueryFirst<int>(sql, new { name });

        return count == 1;
    }

    public bool ExistsById(int id)
    {
        const string sql = "SELECT COUNT(1) FROM Accounts a WHERE Id = @id";
        var count = connection.QueryFirst<int>(sql, new { id });

        return count == 1;
    }

    public List<Account> ListAccounts()
    {
        const string sql = "SELECT * FROM Accounts";
        return connection
            .Query<Account>(sql)
            .ToList();
    }

    public int SelectAccount(int id)
    {
        using var transaction = connection.BeginTransaction();

        try
        {
            connection.Execute("UPDATE Accounts SET IsSelected = 0 WHERE IsSelected = 1");
            var affectedRows = connection.Execute("UPDATE Accounts SET IsSelected = 1 WHERE Id = @id", new { id });
        
            transaction.Commit();

            return affectedRows;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            AnsiConsole.Console.WriteException(ex);
        }

        return 0;
    }
}