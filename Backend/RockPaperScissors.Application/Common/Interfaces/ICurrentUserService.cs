namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
    }
}