using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Persistence;

/// <inheritdoc/>
public class DBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDBContext
{
    /// <summary>
    /// Конструктор, инициализирующий первоначальные настройки контекста
    /// </summary>
    /// <param name="options">Первоначальные настройки</param>
    public DBContext(DbContextOptions<DBContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}