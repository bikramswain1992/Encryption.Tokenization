using Encryption.Tokenization.Response;

namespace Encryption.Tokenization.Contract;
public interface IEncoderDecoder
{
    PasswordHash HashPassword(string password);
    PasswordHash HashPassword(string password, int iterations);
    PasswordHash HashPassword(string password, string salt);
    bool MatchPassword(PasswordHash passwordHash, string password);
}
