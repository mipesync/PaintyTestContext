namespace PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;

/// <summary>
/// DTO, возвращаемое из метода получения изображений пользователя
/// </summary>
public class GetImagesResponseDto
{
    /// <summary>
    /// Список ссылок на изображения
    /// </summary>
    private List<string> ImagesUrlsList { get; set; } = new();
}