namespace ConsoleApp.Errors;

public abstract class Error
{
    public abstract Exception Exception { get; }
    public abstract int Code { get; }
}