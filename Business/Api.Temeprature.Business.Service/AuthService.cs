using Api.Temeprature.Business.Service.Contract;
using Api.Temeprature.Common;
using Api.Temperature.Data.Entity;
using Api.Temperature.Data.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Temeprature.Business.Service
{
    public class AuthService : IAuthService
    {
        private readonly JwtSetting _jwtSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IOptions<JwtSetting> jwtSettings, UserManager<IdentityUser> userManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user is null)
                throw new Exception("Echec de génération de token");

            var accessToken = await GenerateAccessTokenAsync(user);
            var refreshToken = await GenerateRefreshTokenAsync();

            var refreshTokenModel = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenModel);

            return (accessToken, refreshToken);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(token);

            if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.Used || refreshToken.Invalidated)
            {
                throw new Exception("Echec : Token invalide");
            }

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                throw new Exception("Echec : User not found");
            }

            var (accessToken, newRefreshToken) = await GenerateTokensAsync(user.UserName);

            refreshToken.Used = true;
            await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);

            return  (accessToken, newRefreshToken);
        }

        public async Task<string> GenerateAccessTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            /* var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role))); */

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }

        public async Task<string> GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
