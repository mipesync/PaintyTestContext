using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintyTestContext.Application.DTOs.UserDTOs;
using PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaintyTestContext.Controllers;

/// <summary>
/// Контроллер пользователя
/// </summary>
[Route("api/user")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    
    private readonly IWebHostEnvironment _environment;
    private string UrlRaw => $"{Request.Scheme}://{Request.Host}";
    
    private Guid CurrentUserId
    {
        get
        {
            var claimNameId = Request.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            if (claimNameId is null)
                return Guid.Empty;
            
            return Guid.Parse(claimNameId.Value);
        }
    }

    /// <summary>
    /// Инициализация начальных параметров
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователя</param>
    /// <param name="environment">Текущее окружение</param>
    public UserController(IUserRepository userRepository, IWebHostEnvironment environment)
    {
        _userRepository = userRepository;
        _environment = environment;
    }
    
    /// <summary>
    /// Получить информацию о пользователе по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns><see cref="GetUserByIdResponseDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [AllowAnonymous]
    [HttpGet("details/{userId:guid}")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetUserByIdResponseDto))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await _userRepository.GetById(userId, hostUrl: UrlRaw);
            
        return Ok(result);
    }
    
    /// <summary>
    /// Обновить имя пользователя
    /// </summary>
    /// <param name="dto">Входные данные</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut("details")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    public async Task<IActionResult> UpdateName([FromBody] UpdateNameDto dto)
    {
        await _userRepository.UpdateName(dto, CurrentUserId);
            
        return Ok();
    }
    
    /// <summary>
    /// Получить изображения
    /// </summary>
    /// <param name="userId">Идентификатор владельца изображений</param>
    /// <returns><see cref="GetImagesResponseDto"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="403">Отображение изображений запрещено</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{userId:guid}/images")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetImagesResponseDto))]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    public async Task<IActionResult> GetImages([FromRoute] Guid userId)
    {
        var result = await _userRepository.GetImages(CurrentUserId, ownerId: userId, hostUrl: UrlRaw);
            
        return Ok(result);
    }

    /// <summary>
    /// Загрузить новое изображение
    /// </summary>
    /// <param name="dto">Входные данные</param>
    /// <returns>Ссылка на загруженное изображение</returns>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost("/images")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetImagesResponseDto))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageDto dto)
    {
        var result =
            await _userRepository.UploadImage(CurrentUserId, dto.Image, _environment.WebRootPath, hostUrl: UrlRaw);

        return Ok(result);
    }

    /// <summary>
    /// Удалить изображение
    /// </summary>
    /// <param name="fileName">Название удаляемоего изображения</param>
    /// <returns>Ссылка на загруженное изображение</returns>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpDelete("/images/{fileName}")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetImagesResponseDto))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
    public async Task<IActionResult> RemoveImage([FromRoute] string fileName)
    {
        await _userRepository.RemoveImage(CurrentUserId, fileName, _environment.WebRootPath);

        return Ok();
    }
}