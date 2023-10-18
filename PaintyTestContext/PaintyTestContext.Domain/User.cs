using Microsoft.AspNetCore.Identity;

namespace PaintyTestContext.Domain;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User: IdentityUser<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Полное имя (ФИО) пользователя
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// Отображаемое имя пользователя.
    /// Может быть либо полным имем, либо адресом электронной почты
    /// </summary>
    public string DisplayedName { get; set; } = null!;
    /// <summary>
    /// Список ссылок на изображения
    /// </summary>
    public List<string> UrlList { get; set; } = new();
    /// <summary>
    /// Список идентификаторов друзей пользователя
    /// </summary>
    public List<Guid> FriendsIdList { get; set; } = new();
}