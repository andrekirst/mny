using Spectre.Console.Cli;

namespace ConsoleApp.Infrastructure;

public class TypeResolver(IServiceProvider serviceProvider) : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public object? Resolve(Type? type)
    {
        return type == null ? null : _serviceProvider.GetService(type);
    }

    public void Dispose()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}