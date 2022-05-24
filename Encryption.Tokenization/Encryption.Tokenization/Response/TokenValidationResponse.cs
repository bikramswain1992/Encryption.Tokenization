using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Encryption.Tokenization.Response;
public class TokenValidationResponse
{
    public bool IsValid { get; set; }
    public IDictionary<string, object> Claims { get; set; }
}
