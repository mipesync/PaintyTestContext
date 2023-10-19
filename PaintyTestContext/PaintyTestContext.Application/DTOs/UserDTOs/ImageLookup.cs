namespace PaintyTestContext.Application.DTOs.UserDTOs;

/// <summary>
/// Класс вывода изображений на клиент
/// </summary>
public class ImageLookup
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; } = null!;
    /// <summary>
    /// Ссылка на файл
    /// </summary>
    public string Url { get; set; } = null!;
}