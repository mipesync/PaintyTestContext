using System.Reflection;
using lightning_shop.Application.Interfaces;
using lightning_shop.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using PaintyTestContext.Application.Common.Managers;
using PaintyTestContext.Application.DTOs;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Application.Repositories;

namespace PaintyTestContext.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceCollection,
        JwtOptionsDto jwtOptions)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        serviceCollection.AddTransient<IAuthRepository, AuthRepository>();
        serviceCollection.AddTransient<IUserRepository, UserRepository>();

        serviceCollection.AddSingleton<IFileUploader, FileUploader>();
        
        var dbContext = serviceCollection.BuildServiceProvider().GetService<IDBContext>();

        if (dbContext is not null)
            serviceCollection.AddTransient<ITokenManager>(x => new TokenManager(jwtOptions));
    }
}