using Microsoft.AspNetCore.Mvc;
using PaintyTestContext.Application.DTOs.AuthDTOs;
using PaintyTestContext.Application.DTOs.AuthDTOs.ResponseDTOs;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaintyTestContext.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        /// <summary>
        /// Инициализация начальных параметров
        /// </summary>
        /// <param name="authRepository">Репозиторий авторизации</param>
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="dto">Входные авторизационные данные</param>
        /// <returns><see cref="SignInResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Неверный пароль</response>
        /// <response code="400">Неудачная попытка входа</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("sign-in")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(SignInResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var result = await _authRepository.SignIn(dto);
            
            return Ok(new SignInResponseDto
            {
                userId = result.userId,
                access_token = result.access_token,
                access_token_expires = result.access_token_expires,
                refresh_token = result.refresh_token,
                refresh_token_expires = result.refresh_token_expires
            });
        }

        /// <summary>
        /// Обновление токена доступа
        /// </summary>
        /// <returns><see cref="RefreshTokenResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Недействительный токен</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("token-refresh")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(RefreshTokenResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> TokenRefresh([FromQuery] string refreshToken)
        {
            var result = await _authRepository.RefreshToken(refreshToken);
            
            return Ok(new RefreshTokenResponseDto
            {
                userId = result.userId,
                access_token = result.access_token,
                access_token_expires = result.access_token_expires,
                refresh_token = result.refresh_token,
                refresh_token_expires = result.refresh_token_expires
            });
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <remarks>
        /// Пароль должен состоять из верхнего и нижнего регистра, содержать специальные символы и цифры <br/><br/>
        /// </remarks>
        /// <param name="dto">Входные регистрационные данные</param>
        /// <returns><see cref="SignUpResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Пользователь с такой почтой уже существует</response>
        /// <response code="404">Что-то пошло не так</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("sign-up")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(SignUpResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            var result = await _authRepository.SignUp(dto);

            return Ok(result);
        }
    }
}
