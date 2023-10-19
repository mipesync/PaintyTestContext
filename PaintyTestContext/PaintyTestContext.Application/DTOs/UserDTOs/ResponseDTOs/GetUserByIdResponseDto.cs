﻿namespace PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;

/// <summary>
/// DTO, возвращаемое из метода получения информации о пользователе по идентификатору
/// </summary>
public class GetUserByIdResponseDto
{
    /// <summary>
    /// Отображаемое имя пользователя
    /// </summary>
    public string DisplayedName { get; set; } = null!;
    /// <summary>
    /// Количество друзей
    /// </summary>
    public int FriendsCount { get; set; }
    /// <summary>
    /// Список ссылок на изображения
    /// </summary>
    public List<ImageLookup>? ImagesUrlsList { get; set; }
}