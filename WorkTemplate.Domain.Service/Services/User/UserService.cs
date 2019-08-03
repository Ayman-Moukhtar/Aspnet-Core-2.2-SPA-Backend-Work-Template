namespace WorkTemplate.Domain.Service.Services.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using WorkTemplate.Croscutting.Contracts.EntityValidator;
    using WorkTemplate.Domain.Entity.Entities;
    using WorkTemplate.Domain.Entity.ViewModels;

    public class UserService : IUserService
    {
        private readonly Lazy<UserManager<User>> _userManager;
        private readonly Lazy<IEntityValidator> _entityValidator;
        private readonly Lazy<IConfiguration> _configurations;

        public UserService(
            Lazy<UserManager<User>> userManager,
            Lazy<IEntityValidator> entityValidator,
            Lazy<IConfiguration> configurations
            )
        {
            _userManager = userManager;
            _entityValidator = entityValidator;
            _configurations = configurations;
        }

        public int Dummy()
        {
            return _userManager.Value.GetHashCode();
        }

        public async Task<UserTicketViewModel> LoginAsync(LoginViewModel credentials)
        {
            _entityValidator.Value.ValidateAndThrow(credentials);

            var user = await _userManager.Value.FindByEmailAsync(credentials.Email);

            if (user == null)
            {
                throw new InvalidOperationException("Invalid Grant");
            }

            var isValid = await _userManager.Value.CheckPasswordAsync(user, credentials.Password);

            if (!isValid)
            {
                throw new InvalidOperationException("Invalid Grant");
            }

            var roles = await _userManager.Value.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user@user.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(roles.Select(_ => new Claim("roles", _)));

            var accessToken = GenerateAccessToken(claims);

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_configurations.Value["Tokens:RefreshTokenDurationInDays"]));

            await _userManager.Value.UpdateAsync(user);

            return new UserTicketViewModel
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken
            };
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Value["Tokens:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configurations.Value["Tokens:Issuer"],
                audience: _configurations.Value["Tokens:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Value["Tokens:Key"])),
                ValidateLifetime = false,
                ValidIssuer = _configurations.Value["Tokens:Issuer"],
                ValidAudience = _configurations.Value["Tokens:Audience"]
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<UserTicketViewModel> RefreshAsync(RefreshTicketViewModel credentials)
        {
            _entityValidator.Value.ValidateAndThrow(credentials);

            var principal = GetPrincipalFromExpiredToken(credentials.AccessToken);
            var user = await _userManager.Value.FindByEmailAsync(principal.Claims.FirstOrDefault().Value);

            if (user == null)
            {
                throw new SecurityTokenException("Invalid Token");
            }

            if (user.RefreshToken != credentials.RefreshToken || user.RefreshTokenExpiryDate < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newJwtToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_configurations.Value["Tokens:RefreshTokenDurationInDays"]));

            await _userManager.Value.UpdateAsync(user);

            return new UserTicketViewModel
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
