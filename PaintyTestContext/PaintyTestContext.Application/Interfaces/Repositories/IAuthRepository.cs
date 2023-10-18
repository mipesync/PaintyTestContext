using PaintyTestContext.Application.DTOs.AuthDTOs;
using PaintyTestContext.Application.DTOs.AuthDTOs.ResponseDTOs;

namespace PaintyTestContext.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория авторизации
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="dto">DTO входных авторизационных данных</param>
        /// <returns><see cref="SignInResponseDto"/></returns>
        Task<SignInResponseDto> SignIn(SignInDto dto);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto">DTO входных регистрационных данных</param>
        /// <returns><see cref="SignUpResponseDto"/>, а так же письмо на почту
        /// с кодом подтверждения регистрации</returns>
        Task<SignUpResponseDto> SignUp(SignUpDto dto);

        /// <summary>
        /// Обновление токена обновления
        /// </summary>
        /// <param name="refresh">Старый токен обновления</param>
        /// <returns></returns>
        Task<RefreshTokenResponseDto> RefreshToken(string refresh);
    }
}
