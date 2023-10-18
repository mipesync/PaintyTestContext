using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PaintyTestContext.Application.Common.Exceptions;
using PaintyTestContext.Application.DTOs.AuthDTOs;
using PaintyTestContext.Application.DTOs.AuthDTOs.ResponseDTOs;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Application.Repositories
{
    /// <inheritdoc/>
    public class AuthRepository : IAuthRepository
    {
        private readonly ITokenManager _tokenManager;
        private readonly ILogger<AuthRepository> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthRepository(IDBContext dbContext, ILogger<AuthRepository> logger, SignInManager<User> signInManager, 
            ITokenManager tokenManager, UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
            _userManager = userManager;
        }

        public async Task<SignInResponseDto> SignIn(SignInDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }
            if (!user.EmailConfirmed)
            {
                const string message = "Почта не подтверждена";

                _logger.LogWarning($"{user.Id} - {message}");

                throw new ForbiddenException(message);
            }

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordIsValid)
                throw new BadRequestException("Неверный пароль");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);

            if (result.Succeeded)
            {
                const string message = "Пользователь авторизован";

                _logger.LogInformation($"{user.Id} - {message}");

                var accessToken = await _tokenManager.CreateAccessTokenAsync(user);

                JwtSecurityToken? refreshToken = null;

                if (dto.RememberMe)
                    refreshToken = await _tokenManager.CreateRefreshTokenAsync(user.Id);

                return new SignInResponseDto
                {
                    userId = user.Id,
                    access_token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    access_token_expires = DateToMilleseconds(accessToken.ValidTo),
                    refresh_token = refreshToken is null
                        ? null
                        : new JwtSecurityTokenHandler().WriteToken(refreshToken),
                    refresh_token_expires = refreshToken is null
                        ? null
                        : DateToMilleseconds(refreshToken!.ValidTo)
                };
            }
            if (result.IsLockedOut)
            {
                var message = "Аккаунт заблокирован";
                _logger.LogWarning($"{user.Id} - {message}");

                user.AccessFailedCount = 0;
                await _userManager.UpdateAsync(user);

                throw new ForbiddenException(message);
            }
            else
            {
                var message = "Неудачная попытка входа";
                _logger.LogWarning($"{user.Id} - {message}");

                throw new BadRequestException(message);
            }
        }

        public async Task<SignUpResponseDto> SignUp(SignUpDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var getUser = await _userManager.FindByEmailAsync(dto.Email);

            if (getUser is not null)
                throw new BadRequestException("Пользователь с такой почтой уже существует");

            var result = await _userManager.CreateAsync(user, dto.Password);
            var userId = await _userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                var message = "Пользователь был зарегистрирован";
                _logger.LogInformation($"{userId} - {message}");

                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                return new SignUpResponseDto
                {
                    userId = Guid.Parse(userId)
                };
            }

            foreach (var error in result.Errors)
            {
                throw new BadRequestException(error.Description);
            }

            throw new WentWrongException();
        }

        public async Task<RefreshTokenResponseDto> RefreshToken(string refresh)
        {
            var principal = await _tokenManager.GetPrincipalFromRefreshTokenAsync(refresh)!;

            if (principal is null)
                throw new BadRequestException("Недействительный токен");

            var userId = principal.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(user);

            var newAccessToken = await _tokenManager.CreateAccessTokenAsync(user);
            var newRefreshToken = await _tokenManager.CreateRefreshTokenAsync(Guid.Parse(userId));

            await _userManager.UpdateAsync(user);

            return new RefreshTokenResponseDto
            {
                userId = user.Id,
                access_token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                access_token_expires = DateToMilleseconds(newAccessToken.ValidTo),
                refresh_token = new JwtSecurityTokenHandler().WriteToken(newRefreshToken),
                refresh_token_expires = DateToMilleseconds(newRefreshToken.ValidTo)
            };
        }

        private long DateToMilleseconds(DateTime date)
        {
            return (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
