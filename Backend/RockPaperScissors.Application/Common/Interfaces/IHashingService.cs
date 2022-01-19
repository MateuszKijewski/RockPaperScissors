namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IHashingService
    {
        byte[] GetSalt();

        string Hash(string input, byte[] salt);
    }
}