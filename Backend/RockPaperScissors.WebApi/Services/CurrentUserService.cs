using Dawn;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace RockPaperScissors.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<User> _userRepository;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IBaseRepositoryProvider baseRepositoryProvider)
        {
            _httpContextAccessor = Guard.Argument(httpContextAccessor).NotNull().Value;
            _userRepository = baseRepositoryProvider.GetRepository<User>();
        }

        public Guid Id => User.Id;
        public string Email => User.Email;
        public string FirstName => User.FirstName;
        public string LastName => User.LastName;


        public User User => GetCurrentUser().GetAwaiter().GetResult() ?? throw new Exception("User service error");

        private async Task<User?> GetCurrentUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (!context.Items.ContainsKey(nameof(User)))
            {
                var user = context.User;
                if (user == null)
                    return null;

                var email = user.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName);
                if (email == null || string.IsNullOrEmpty(email.Value))
                    return null;

                context.Items[nameof(User)] = (await _userRepository.FindAsync(x => x.Email == email.Value)).SingleOrDefault();
            }

            return context.Items[nameof(User)] as User;
        }
    }
}
