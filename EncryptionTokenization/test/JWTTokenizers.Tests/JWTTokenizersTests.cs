using Encryption.Tokenization;
using Encryption.Tokenization.Contract;
using Encryption.Tokenization.Request;
using NUnit.Framework;
using System.Security.Claims;

namespace JWTTokenizers.Tests;

public class JWTTokenizersTests
{
    IJWTTokenizer _jWTTokenizer;
    GenerateTokenRequest _identityInfo;

    private string _issuer = "Test issuer";
    private string _audience = "Test audience";
    private string _email = "Test email";

    [OneTimeSetUp]
    public void Setup()
    {
        _jWTTokenizer = new JWTTokenizer();

        SetIdentityInfo();
    }

    [Test]
    public void GenerateToken_ShouldGenerateJWTToken()
    {
        var result = _jWTTokenizer.GenerateToken(_identityInfo);

        Assert.IsNotNull(result);
    }

    [Test]
    public void ValidateToken_ShouldReturnTrue_WhenValidTokenIsProvided()
    {
        var request = new ValidateTokenRequest()
        {
            Issuer = _issuer,
            Audience = _audience,
            JWT = GenerateNewToken()
        };

        var result = _jWTTokenizer.ValidateToken(request);

        Assert.IsTrue(result.IsValid);
        Assert.That(Convert.ToString(result.Claims["Email"]), Is.EqualTo(_email));
    }

    [Test]
    public void ValidateToken_ShouldReturnFalse_WhenInValidTokenIsProvided()
    {
        var request = new ValidateTokenRequest()
        {
            Issuer = _issuer+"invalid",
            Audience = _audience,
            JWT = GenerateNewToken()
        };

        var result = _jWTTokenizer.ValidateToken(request);

        Assert.IsFalse(result.IsValid);
    }

    private string GenerateNewToken()
    {
        return _jWTTokenizer.GenerateToken(_identityInfo);
    }

    private void SetIdentityInfo()
    {
        _identityInfo = new GenerateTokenRequest()
        {
            Issuer = _issuer,
            Audience = _audience,
            Expires = 1,
            Claims = new Dictionary<string, object>
            {
                { "Email", _email }
            }
        };
    }
}
