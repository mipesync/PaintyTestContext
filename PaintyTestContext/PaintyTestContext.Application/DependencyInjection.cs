using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PaintyTestContext.Application.Common.Managers;
using PaintyTestContext.Application.DTOs;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Application.Repositories;

namespace PaintyTestContext.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, 
        JwtOptionsDto jwtOptions)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        serviceCollection.AddTransient<IAuthRepository, AuthRepository>();
        serviceCollection.AddTransient<IUserRepository, UserRepository>();
        
        var dbContext = serviceCollection.BuildServiceProvider().GetService<IDBContext>();

        if (dbContext is not null)
            serviceCollection.AddTransient<ITokenManager>(x => new TokenManager(jwtOptions));
        return serviceCollection;
    }
}