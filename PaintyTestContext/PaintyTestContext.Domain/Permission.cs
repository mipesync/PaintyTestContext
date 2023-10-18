namespace PaintyTestContext.Domain;

/// <summary>
/// Сущность, хранящая в себе права пользователя
/// </summary>
public class Permission
{
    /// <summary>
    /// Идентификатор текущего пользователя
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Идентификатор друга пользователя
    /// </summary>
    public Guid FriendId { get; set; }
    /// <summary>
    /// Права пользователя
    /// </summary>
    public PermissionsEnum Permissions { get; set; }
}