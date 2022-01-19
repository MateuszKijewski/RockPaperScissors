using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        Task<JwtToken> GetTokenAsync(User user);
    }

    public class SecurityToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class JwtToken
    {
        public SecurityToken AuthToken { get; set; }
        public SecurityToken RefreshToken { get; set; }
    }
}