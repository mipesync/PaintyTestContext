using Microsoft.EntityFrameworkCore;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Application.Interfaces;

/// <summary>
/// Интерфейс, описывающий таблицы, используемые в проекте
/// </summary>
public interface IDBContext
{
    /// <summary>
    /// Получить/установить список пользователей
    /// </summary>
    DbSet<User> Users { get; set; }
    /// <summary>
    /// Получить/установить список прав пользователя
    /// </summary>
    DbSet<Permission> Permissions { get; set; }
    
    /// <summary>
    /// Ассинхронно сохраняет сделанные изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}