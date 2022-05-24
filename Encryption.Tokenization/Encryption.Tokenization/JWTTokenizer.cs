using Encryption.Tokenization.Contract;
using Encryption.Tokenization.Request;
using Encryption.Tokenization.Response;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Encryption.Tokenization;
public class JWTTokenizer : IJWTTokenizer
{
    private readonly string _secret;
    private readonly RsaSecurityKey _key;
    private readonly JsonWebTokenHandler _handler;

    public JWTTokenizer(string secret)
    {
        _secret = secret!=null ? secret : @"-----BEGIN RSA PRIVATE KEY-----
                    MIIEowIBAAKCAQEAvIa52dDFB34fCJJeak177U/4hnBVuRmnWiuJb2X+ZgrVksnr
                    sp9ff40FmndYZWSnsxlOPAEy56+nJnwQL4UUCja+oKUD0R3TkFyU0pZpevta2zUW
                    Pfbz/T55JQYoV2t1f0haeCA7PmakoDcipHBEzeKvBqPrVnDpDsTzg4zaUFHLv4S5
                    1g8NdyO9o/pCJ5Gl5QmISTdNlnSnLe6smPiX+LQdAHKX8uklmNdzRqwG/TZO6Pyq
                    Oq/Q08P8jXtWDyFpNaSO6M91DJJkdw9DpZA00iVsrPYOAlL3n8fGZQlGe84J0U3B
                    fCsro6pWPU8UeKz1SbEEA/qiAuxCbhJaobyRTQIDAQABAoIBAD5hQdudpaQeCjy2
                    2cDI1KmoXW52exbdMy+12irfD7dJ/HMuluuqqlm1GtaKiNg73vV2+RkHuIVK7L7i
                    LCTdHs1mYdsb3tBx0xAgYinwFQTZaK3BuhNUxFTWOBWVHQIYD2/HTBAVciTqp6xP
                    sgnBEDMjv5At6u/WOndlmcG8eHw8kwqkhBIhm2k4or3UivqHoMV905Kdi82xvYEr
                    +ci5bdYpnQPiTibh8yaNnKY2KTj6DCIO/wEqnisgY28e/TYcf1wbDrJ8z38C0hJU
                    rovQSbtrWsBV3Uy3EnUDkyC0dzET23PjPZPdV644m/1AzWhg0nUel8Rkvs/EuvuO
                    wPOwVI0CgYEA39Ngu3aA9/VolsTRpfTqyZ7tcO1nm38YVFR8yHQ2rMpogVTjU3Bo
                    QkN+2ZHOWmUrROAXZg15TQbLKJn2BKfUk9sdzyKy1uw1zycYN8Jfm4FQ+E76PT/+
                    +ue4u+VOyeIGExAQiPM754c6x/JQC7O60KesOotUyy+eLUG4zA2cC0MCgYEA16Ba
                    7XLQPCvkuxtHuDkXI1RB4Sqp/wAlEnCwoCXmbReco2KQJxCdmS2BdN0r+Wj1w7t8
                    bwQ3nt+bK0sZmgRu8giakpyfI2lTum0+zROwOHe9TVqcogPC0G+Yy+dHRGyZ7aru
                    ObbVv4ejG4XEFQVsmjceyKgokdclrP/AjZHngC8CgYAkAq4dewNk4WbiWugf+zeL
                    GLa65Hc23UvcxXsOBSSGyEnoBPBODFe50YInHv5ELOK5QhSBpslNSzqEXcDnHtlk
                    sGwrVznOulIt8exDjFI1gqi0SoCYOiOb2owuLsZuVJ8FkiAW6ItKxMcAREv8lf4I
                    c/GRubj6t76LKXfB3K6uYQKBgGpvrzKSgBOTnx51Atv+4lsAFlztUGidS69kjOsg
                    ijPDegB0gK+n1gNsoQBsxG4iz13EyFGMhWAZBGFEeui504IJmTNRuIQkU74setmE
                    JHJbOMhcOAEJVjrJO2U7TsSJzxmwTFOU2sHmVC8bwoSV3tvo5Xsq9ou25dQVkpjP
                    MrTlAoGBANDhEg8uWmcnkkb50S7OivmORr7uwKYwwzdklSpyw4PpfKyZhbdj7CQL
                    X6P8BEduFVD/TBUUAWYcWjOBafAoUM4jfWMTPZnh7g/F5AsHryQrN3YjqaVu3/GD
                    snrKPOe6NTNo27AoIf72VDuIUcwhjuv2ce3WR/K3nUSmW9u8koNQ
                    -----END RSA PRIVATE KEY-----";

        var rsa = RSA.Create();
        rsa.ImportFromPem(_secret.ToCharArray());
        _key = new RsaSecurityKey(rsa);

        _handler = new JsonWebTokenHandler();
    }

    public string GenerateToken(GenerateTokenRequest identityInfo)
    {
        var now = DateTime.UtcNow;

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = identityInfo.Issuer,
            Audience = identityInfo.Audience,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(identityInfo.Expires),
            Claims = identityInfo.Claims,
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.RsaSsaPssSha256)
        };

        string jwt = _handler.CreateToken(descriptor);

        return jwt;
    }

    public TokenValidationResponse ValidateToken(ValidateTokenRequest jwtInfo)
    {
        TokenValidationResult result = _handler.ValidateToken(jwtInfo.JWT,
                                        new TokenValidationParameters
                                        {
                                            ValidIssuer = jwtInfo.Issuer,
                                            ValidAudience = jwtInfo.Audience,
                                            ValidateLifetime = true,
                                            LifetimeValidator = ValidateJwtExpiry,
                                            RequireExpirationTime = true,
                                            IssuerSigningKey = new RsaSecurityKey(_key.Rsa.ExportParameters(true))
                                        });

        return new TokenValidationResponse
        {
            IsValid = result.IsValid,
            Claims = result.Claims
        };
    }

    private bool ValidateJwtExpiry(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
    {
        if (expires != null)
        {
            return expires > DateTime.UtcNow;
        }
        return false;
    }
}
