namespace ConsoleApp.Errors;

public class AccountAlreadyExistsByNameError(string name) : Error
{
    public override Exception Exception => new($"Account with name \"{name}\" already exists");
    public override int Code => 1;
}