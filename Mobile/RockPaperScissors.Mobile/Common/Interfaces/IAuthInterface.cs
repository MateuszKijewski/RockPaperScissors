using RockPaperScissors.Mobile.Dtos.Contracts;
using RockPaperScissors.Mobile.Dtos.Responses;
using System.Threading.Tasks;

namespace RockPaperScissors.Mobile.Common.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginContract loginContract);

        Task<RegisterResponse> Register(RegisterContract registerContract);
    }
}