using Encryption.Tokenization;
using Encryption.Tokenization.Request;
using System.Security.Claims;

namespace TestApp;
public class TestTokenization
{
    public TestTokenization()
    {
        GenerateTokenRequest tokenRequest = new GenerateTokenRequest()
        {
            Issuer = "Bikram Swain",
            Audience = "Bikram",
            Expires = 1,
            Claims = new Dictionary<string, object>()
            {
                {"userId","BikramSwain" },
                { "role",new string[]{"Admin","Manager"} }
            }
        };

        JWTTokenizer tokenizer = new JWTTokenizer("");
        var jwt = tokenizer.GenerateToken(tokenRequest);

        var validationResp = tokenizer.ValidateToken(new ValidateTokenRequest{
            Issuer = "Bikram Swain",
            Audience = "Bikram",
            JWT = jwt
        });
    }
}
