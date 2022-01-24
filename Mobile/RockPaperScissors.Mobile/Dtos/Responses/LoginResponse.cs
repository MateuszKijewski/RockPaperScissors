using System;

namespace RockPaperScissors.Mobile.Dtos.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}