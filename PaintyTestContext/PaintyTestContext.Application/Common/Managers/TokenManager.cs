﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PaintyTestContext.Application.Common.Options;
using PaintyTestContext.Application.DTOs;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Application.Common.Managers
{
    /// <inheritdoc/>
    public class TokenManager : ITokenManager
    {
        private readonly JwtOptions _options;

        public TokenManager(JwtOptionsDto optionsDto)
        {
            _options = new JwtOptions(optionsDto);
        }

        public Task<JwtSecurityToken> CreateAccessTokenAsync(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.ISSUER,
                audience: _options.AUDIENCE,
                claims: claims,
                expires: _options.EXPIRES,
                signingCredentials: new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Task.FromResult(jwt);
        }

        public Task<JwtSecurityToken> CreateRefreshTokenAsync(Guid userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };

            var jwt = new JwtSecurityToken(
                issuer: _options.ISSUER,
                audience: _options.AUDIENCE,
                claims: claims,
                expires: _options.RefreshTokenExpires,
                signingCredentials: new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Task.FromResult(jwt);
        }

        public Task<ClaimsPrincipal> GetPrincipalFromRefreshTokenAsync(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _options.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Недействительный токен");

            return Task.FromResult(principal);
        }
    }
}
