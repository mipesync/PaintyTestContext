using Microsoft.AspNetCore.Http;
using PaintyTestContext.Application.DTOs.UserDTOs;
using PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;

namespace PaintyTestContext.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория пользователя. Описывает методы по взаимодействию с ним
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="hostUrl">Домен API</param>
    /// <returns><see cref="GetUserByIdResponseDto"/></returns>
    Task<GetUserByIdResponseDto> GetById(Guid userId, string hostUrl);
    
    /// <summary>
    /// Обновить имя пользователя
    /// </summary>
    /// <param name="dto">Входные данные</param>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    Task UpdateName(UpdateNameDto dto, Guid currentUserId);

    /// <summary>
    /// Получить список ссылок на изображения пользователя
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="ownerId">Идентификатор владельца изображений</param>
    /// <param name="hostUrl">Домен API</param>
    /// <returns><see cref="GetImagesResponseDto"/></returns>
    Task<GetImagesResponseDto> GetImages(Guid currentUserId, Guid ownerId, string hostUrl);

    /// <summary>
    /// Загрузить новое изображение
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="image">Файл изображения</param>
    /// <param name="webRootPath">Корневой путь проекта</param>
    /// <param name="hostUrl">Домен API</param>
    /// <returns>Ссылка на загруженное изображение</returns>
    Task<string> UploadImage(Guid currentUserId, IFormFile image, string webRootPath, string hostUrl);

    /// <summary>
    /// Удалить изображение
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="fileName">Название файла, который необходимо удалить</param>
    /// <param name="webRootPath">Корневой путь проекта</param>
    Task RemoveImage(Guid currentUserId, string fileName, string webRootPath);
    
    /// <summary>
    /// Отправить запрос на добавление нового друга
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="targetId">Идентификатор пользователя, которому нужно отправить запрос на добавление в друзья</param>
    Task SendFriendRequest(Guid currentUserId, Guid targetId);
    
    /// <summary>
    /// Добавить нового друга
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="targetId">Идентификатор пользователя, которого нужно добавить в друзья</param>
    Task AddFriend(Guid currentUserId, Guid targetId);
    
    /// <summary>
    /// Удалить пользователя из друзей
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="friendId">Идентификатор пользователя, которого нужно удалить из друзей</param>
    Task RemoveFriend(Guid currentUserId, Guid friendId);
}