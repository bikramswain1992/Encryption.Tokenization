namespace Encryption.Tokenization.Request;
public class ValidateTokenRequest
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string JWT { get; set; }
}
