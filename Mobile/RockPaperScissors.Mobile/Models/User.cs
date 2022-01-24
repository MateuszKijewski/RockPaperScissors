using System;

namespace RockPaperScissors.Mobile.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Salt { get; set; }
        public string HashedPassword { get; set; }
    }
}