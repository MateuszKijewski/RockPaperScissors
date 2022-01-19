using RockPaperScissors.Domain.Common;

namespace RockPaperScissors.Domain.Entities
{
    public class User : EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public byte[]? Salt { get; set; }
        public string? HashedPassword { get; set; }
    }
}