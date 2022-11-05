using Encryption.Tokenization;
using Encryption.Tokenization.Contract;
using NUnit.Framework;

namespace EncodersDecoders.Tests;

public class AsymmetricKeyEncoderDecoderTests
{
    IAsymmetricKeyEncoderDecoder _keyEncoderDecoder;

    [OneTimeSetUp]
    public void Setup()
    {
        _keyEncoderDecoder = new AsymmetricKeyEncoderDecoder();
    }

    [Test]
    public void GenerateKeys_ShouldReturnPublicAndPrivateKeys()
    {
        var result = _keyEncoderDecoder.GenerateKeys();

        Assert.IsNotNull(result.publicKey);
        Assert.IsNotNull(result.privateKey);
    }

    [TestCase("Small text")]
    [TestCase("Long text that will also be hashed and generated")]
    [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen boo")]
    public void EncryptDecrypt_ShouldProperlyEncryptAndDecrypt(string data)
    {
        var keys = _keyEncoderDecoder.GenerateKeys();

        var encryptionResult = _keyEncoderDecoder.Encrypt<string>(data, keys.publicKey);
        var decryptResult = _keyEncoderDecoder.Decrypt<string>(encryptionResult, keys.privateKey);

        Assert.That(decryptResult, Is.EqualTo(data));
    }
}
