using RockPaperScissors.Domain.Dtos.Auth;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterNewUser(RegisterDto userDto);

        Task<JwtToken> HandleLogin(LoginDto loginDto);

        UserDto GetCurrentUser();
    }
}