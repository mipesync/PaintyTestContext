namespace PaintyTestContext.Application.DTOs.UserDTOs;

/// <summary>
/// Класс для вывода информации о друге
/// </summary>
public class FriendLookup
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Отображаемое имя пользователя
    /// </summary>
    public string DisplayedName { get; set; } = null!;
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = null!;
}