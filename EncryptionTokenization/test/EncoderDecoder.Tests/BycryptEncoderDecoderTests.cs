using Encryption.Tokenization;
using Encryption.Tokenization.Contract;
using Encryption.Tokenization.Response;
using NUnit.Framework;

namespace EncodersDecoders.Tests;

public class BycryptEncoderDecoderTests
{
    IEncoderDecoder _encoderDecoder;

    [OneTimeSetUp]
    public void Setup()
    {
        _encoderDecoder = new EncoderDecoder();
    }

    [TestCase("Small text")]
    [TestCase("Long text that will also be hashed and generated")]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.")]
    public void GenerateHash_ShouldGenerateHashedDataAndSalt(string data)
    {
        var result = _encoderDecoder.GenerateHash(data);
        
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Hash);
        Assert.IsNotNull(result.Salt);
        Assert.That(result.Hash, Is.Not.EqualTo(data));
    }

    [TestCase("Small text", 8)]
    [TestCase("Long text that will also be hashed and generated", 10)]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", 12)]
    public void GenerateHash_WithIterations_ShouldGenerateHashedDataAndSalt(string data, int iterations)
    {
        var result = _encoderDecoder.GenerateHash(data, iterations);

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Hash);
        Assert.IsNotNull(result.Salt);
        Assert.That(result.Hash, Is.Not.EqualTo(data));
    }

    [TestCase("Small text", "$2a$12$w2VO7zfipXXFUjGhp1vuUu")]
    [TestCase("Long text that will also be hashed and generated", "$2a$12$zNb.7K.IMzFBZdwbFmGmT.")]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "$2a$12$uFoH/Ga..Z4QhSwdD1VkX.")]
    public void GenerateHash_WithSalt_ShouldGenerateHashedDataAndSalt(string data, string salt)
    {
        var result = _encoderDecoder.GenerateHash(data, salt);

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Hash);
        Assert.IsNotNull(result.Salt);
        Assert.That(result.Hash, Is.Not.EqualTo(data));
        Assert.That(result.Salt, Is.EqualTo(salt));
    }

    [TestCase("Small text", "$2a$12$LdH7d0vEyGBFTu5lU57Xk.", "$2a$12$LdH7d0vEyGBFTu5lU57Xk.EliXT2RTe2VVIK7X9t8aDGmCIt59KMe", true)]
    [TestCase("Long text that will also be hashed and generated", "$2a$12$COGDO8r28JfbJQ6I7l/m8.", "$2a$12$COGDO8r28JfbJQ6I7l/m8.x238MHi66qe3NxoVNm3n4ut6z3xZEXz", false)]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "$2a$12$sh9/66rj3BRxdUCS2zpl0u", "$2a$12$sh9/66rj3BRxdUCS2zpl0uD3SsfIKFHdgX3waDWji6jklNYVN5lkS", true)]
    public void MatchHash_ShouldCorrectlyVerifyMatch(string data, string salt, string hash, bool isValid)
    {
        var hashedData = new HashedData
        {
            Hash = hash,
            Salt = salt
        };

        var result = _encoderDecoder.MatchHash(hashedData, data);

        if (isValid)
        {
            Assert.IsTrue(result);
        }
        else
        {
            Assert.IsFalse(result);
        }
    }
}
