using ConsoleApp.Errors;
using Spectre.Console;

namespace ConsoleApp.Infrastructure.Extensions;

public static class AnsiConsoleExtensions
{
    public static void WriteError(this IAnsiConsole console, Error error)
    {
        console.WriteException(error.Exception);
    }
}