using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        Task<SecurityToken> GetTokenAsync(User user);
    }

    public class SecurityToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}