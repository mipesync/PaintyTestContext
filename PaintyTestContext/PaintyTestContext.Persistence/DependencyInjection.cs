using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaintyTestContext.Application.Interfaces;

namespace PaintyTestContext.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<DBContext>(u => u.UseNpgsql(connectionString));
        serviceCollection.AddScoped<IDBContext>(provider => provider.GetService<DBContext>()!);
        
        return serviceCollection;
    }
}