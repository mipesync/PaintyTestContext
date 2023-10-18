using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PaintyTestContext.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DBContext>(u => u.UseNpgsql(connectionString));
        
        return serviceCollection;
    }
}