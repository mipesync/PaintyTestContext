namespace PaintyTestContext.Application.DTOs.UserDTOs;

/// <summary>
/// DTO для обновления имени пользователя
/// </summary>
public class UpdateNameDto
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
}