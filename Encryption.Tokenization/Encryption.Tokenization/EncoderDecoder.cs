using Encryption.Tokenization.Contract;
using Encryption.Tokenization.Response;

namespace Encryption.Tokenization;
public class EncoderDecoder : IEncoderDecoder
{
    public PasswordHash HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return new PasswordHash
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    public PasswordHash HashPassword(string password, int iterations)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(iterations);
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return new PasswordHash
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    public PasswordHash HashPassword(string password, string salt)
    {
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return new PasswordHash
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    public bool MatchPassword(PasswordHash passwordHash, string password)
    {
        var salt = passwordHash.Salt;
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        var verify = generatedHash == passwordHash.Hash;
        return verify;
    }
}
