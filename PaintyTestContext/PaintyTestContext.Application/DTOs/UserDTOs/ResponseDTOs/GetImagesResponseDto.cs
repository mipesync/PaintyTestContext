namespace PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;

/// <summary>
/// DTO, возвращаемое из метода получения изображений пользователя
/// </summary>
public class GetImagesResponseDto
{
    /// <summary>
    /// Список ссылок на изображения
    /// </summary>
    public List<ImageLookup>? Images { get; set; }
}