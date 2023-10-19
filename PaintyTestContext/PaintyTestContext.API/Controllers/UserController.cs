using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintyTestContext.Application.DTOs.AuthDTOs;
using PaintyTestContext.Application.DTOs.AuthDTOs.ResponseDTOs;
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
        var result = await _userRepository.GetById(userId, UrlRaw);
            
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
}