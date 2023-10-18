using Microsoft.Extensions.DependencyInjection;

namespace PaintyTestContext.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, string connectionString)
    {
        
        
        return serviceCollection;
    }
}