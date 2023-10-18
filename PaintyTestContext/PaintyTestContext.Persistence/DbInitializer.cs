namespace PaintyTestContext.Persistence;

/// <summary>
/// Класс для инициализации базы данных
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Инициализировать базу данных
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    public static void Initialize(DBContext dbContext)
    {
        dbContext.Database.EnsureCreated();
    }
}