namespace Encryption.Tokenization.Request;

public class GenerateTokenRequest
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expires { get; set; }
    public IDictionary<string, object> Claims { get; set; }
}
