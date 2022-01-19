using Dawn;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RockPaperScissors.Application.Auth.Providers
{
    public class TokenValidationOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? IssuerSigningKey { get; set; }

        public int ExpirationTimeOfAuthTokenInHours { get; set; }
        public int ExpirationTimeOfRefreshTokenInDays { get; set; }
    }

    public class TokenProvider : ITokenProvider
    {
        private readonly TokenValidationOptions _tokenValidationOptions;

        public TokenProvider(IOptions<TokenValidationOptions> tokenValidationOptions)
        {
            _tokenValidationOptions = Guard.Argument(tokenValidationOptions.Value, nameof(tokenValidationOptions.Value)).NotNull().Value;
        }

        public async Task<JwtToken> GetTokenAsync(User user)
        {
            var authToken = await CreateSecurityToken(user);

            return new JwtToken
            {
                AuthToken = new Common.Interfaces.SecurityToken
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(authToken),
                    ExpiryDate = authToken.ValidTo,
                }
            };
        }

        private async Task<JwtSecurityToken> CreateSecurityToken(User user)
        {
            IEnumerable<Claim> claims = await GetUserClaims(user);
            return CreateTokenBasedOnClaims(claims);
        }

        private JwtSecurityToken CreateTokenBasedOnClaims(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenValidationOptions.IssuerSigningKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _tokenValidationOptions.Issuer,
                audience: _tokenValidationOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_tokenValidationOptions.ExpirationTimeOfAuthTokenInHours),
                signingCredentials: credentials
            );
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Email));

            return claims;
        }
    }
}