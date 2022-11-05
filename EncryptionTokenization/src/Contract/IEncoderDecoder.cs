using Encryption.Tokenization.Response;

namespace Encryption.Tokenization.Contract;
public interface IEncoderDecoder
{
    HashedData GenerateHash(string data);
    HashedData GenerateHash(string data, int iterations);
    HashedData GenerateHash(string data, string salt);
    bool MatchHash(HashedData hashedData, string data);
}
