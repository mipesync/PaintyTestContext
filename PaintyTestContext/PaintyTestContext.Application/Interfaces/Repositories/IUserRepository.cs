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
    /// <returns><see cref="GetUserByIdResponseDto"/></returns>
    Task<GetUserByIdResponseDto> GetById(Guid userId);
    
    /// <summary>
    /// Обновить имя пользователя
    /// </summary>
    /// <param name="dto">Входные данные</param>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    Task UpdateName(UpdateNameDto dto, string currentUserId);
    
    /// <summary>
    /// Получить список ссылок на изображения пользователя
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="ownerId">Идентификатор владельца изображений</param>
    /// <returns><see cref="GetImagesResponseDto"/></returns>
    Task<GetImagesResponseDto> GetImages(Guid currentUserId, Guid ownerId);
    
    /// <summary>
    /// Загрузить новое изображение
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="image">Файл изображения</param>
    /// <returns>Ссылка на загруженное изображение</returns>
    Task<string> UploadImage(Guid currentUserId, IFormFile image);
    
    /// <summary>
    /// Удалить изображение
    /// </summary>
    /// <param name="currentUserId">Идентификатор текущего пользователя</param>
    /// <param name="fileName">Название файла, который необходимо удалить</param>
    Task RemoveImage(Guid currentUserId, string fileName);
    
    /// <summary>
    /// Отправить запрос на добавление нового друга
    /// </summary>
    /// <param name="targetId">Идентификатор пользователя, которому нужно отправить запрос на добавление в друзья</param>
    Task SendFriendRequest(Guid targetId);
    
    /// <summary>
    /// Добавить нового друга
    /// </summary>
    /// <param name="targetId">Идентификатор пользователя, которого нужно добавить в друзья</param>
    /// <returns></returns>
    Task AddFriend(Guid targetId);
    
    /// <summary>
    /// Удалить пользователя из друзей
    /// </summary>
    /// <param name="friendId">Идентификатор пользователя, которого нужно удалить из друзей</param>
    Task RemoveFriend(Guid friendId);
}