using Encryption.Tokenization.Request;
using Encryption.Tokenization.Response;

namespace Encryption.Tokenization.Contract;
public interface IJWTTokenizer
{
    string GenerateToken(GenerateTokenRequest identityInfo);
    TokenValidationResponse ValidateToken(ValidateTokenRequest jwtInfo);
}
