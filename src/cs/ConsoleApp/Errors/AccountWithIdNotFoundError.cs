namespace ConsoleApp.Errors;

public class AccountWithIdNotFoundError(int id) : Error
{
    public override Exception Exception => new Exception($"Account with Id {id} not found");
    public override int Code => 2;
}