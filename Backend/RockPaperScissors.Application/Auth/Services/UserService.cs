using Dawn;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Dtos.Auth;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IHashingService _hashingService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBaseRepositoryProvider _baseRepositoryProvider;
        private readonly ITokenProvider _tokenProvider;

        public UserService(IHashingService hashingService, IBaseRepositoryProvider baseRepositoryProvider, ITokenProvider tokenProvider, ICurrentUserService currentUserService)
        {
            _hashingService = Guard.Argument(hashingService, nameof(hashingService)).NotNull().Value;
            _baseRepositoryProvider = Guard.Argument(baseRepositoryProvider, nameof(baseRepositoryProvider)).NotNull().Value;
            _tokenProvider = Guard.Argument(tokenProvider, nameof(tokenProvider)).NotNull().Value;
            _currentUserService = Guard.Argument(currentUserService, nameof(currentUserService)).NotNull().Value;
        }

        public UserDto GetCurrentUser()
        {
            return new UserDto
            {
                Id = _currentUserService.Id,
                FirstName = _currentUserService.FirstName,
                LastName = _currentUserService.LastName,
                Email = _currentUserService.Email
            };
        }

        public async Task<SecurityToken> HandleLogin(LoginDto loginDto)
        {
            var userRepository = _baseRepositoryProvider.GetRepository<User>();

            var user = (await userRepository.FindAsync(x => x.Email == loginDto.Email)).SingleOrDefault();
            if (user is null)
                throw new Exception("User for this email was not found or password was incorrect");

            string hashedPassword = _hashingService.Hash(loginDto.Password, user.Salt);
            if (hashedPassword != user.HashedPassword)
                throw new Exception("User for this email was not found or password was incorrect");

            return await _tokenProvider.GetTokenAsync(user);
        }

        public async Task<UserDto> RegisterNewUser(RegisterDto userDto)
        {
            var userRepository = _baseRepositoryProvider.GetRepository<User>();
            var userExists = (await userRepository.FindAsync(x => x.Email == userDto.Email)).Count() > 0;
            if (userExists)
                throw new Exception("User with this email already exists");

            byte[] salt = _hashingService.GetSalt();
            string hashedPassword = _hashingService.Hash(userDto.Password, salt);

            var user = await userRepository.AddAsync(new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                HashedPassword = hashedPassword,
                Salt = salt,
            });

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}
