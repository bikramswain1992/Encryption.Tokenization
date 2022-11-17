using EncryptionTokenization;
using EncryptionTokenization.Contract;
using NUnit.Framework;

namespace EncodersDecoders.Tests;

public class SymmetricKeyEncoderDecoderTests
{
    ISymmetricKeyEncoderDecoder _keyEncoderDecoder;

    [OneTimeSetUp]
    public void Setup()
    {
        _keyEncoderDecoder = new SymmetricKeyEncoderDecoder();
    }

    [Test]
    public void GenerateKey_ShouldReturnKey()
    {
        var result = _keyEncoderDecoder.GenerateKey();

        Assert.IsNotNull(result);
    }

    [TestCase("Small text")]
    [TestCase("Long text that will also be hashed and generated")]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.")]
    public void EncryptDecrypt_ShouldProperlyEncryptAndDecrypt(string data)
    {
        var key = _keyEncoderDecoder.GenerateKey();

        var encryptionResult = _keyEncoderDecoder.Encrypt<string>(data, key);
        var decryptResult = _keyEncoderDecoder.Decrypt<string>(encryptionResult, key);

        Assert.That(decryptResult, Is.EqualTo(data));
    }
}
