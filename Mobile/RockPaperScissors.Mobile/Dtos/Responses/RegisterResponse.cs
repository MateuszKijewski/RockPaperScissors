using System.Collections.Generic;

namespace RockPaperScissors.Mobile.Dtos.Responses
{
    public class RegisterResponse
    {
        public string Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}