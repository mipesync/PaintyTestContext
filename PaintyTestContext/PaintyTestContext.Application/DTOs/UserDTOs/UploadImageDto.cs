using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PaintyTestContext.Application.Common.Attributes;

namespace PaintyTestContext.Application.DTOs.UserDTOs;

/// <summary>
/// DTO для загрузки изображения
/// </summary>
public class UploadImageDto
{
    /// <summary>
    /// Изображение, которое необходимо загрузить
    /// </summary>
    [Required(ErrorMessage = "Изображение обязательно!")]
    [ExtensionValidator(Extensions = "png,jpg,jpeg", ErrorMessage = "Поддерживаются только следующие форматы: .png, .jpg, .jpeg")]
    public IFormFile Image { get; set; } = null!;
}