using System.ComponentModel.DataAnnotations;

namespace PaintyTestContext.Application.DTOs.UserDTOs;

/// <summary>
/// DTO для обновления имени пользователя
/// </summary>
public class UpdateNameDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required(ErrorMessage = "Имя обязательно")]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required(ErrorMessage = "Фамилия обязательна")]
    public string LastName { get; set; } = null!;
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? MiddleName { get; set; }
}