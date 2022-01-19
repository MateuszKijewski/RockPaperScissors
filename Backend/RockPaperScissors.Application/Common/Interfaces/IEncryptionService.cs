namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string input);

        string Decrypt(string encryptedData);
    }
}