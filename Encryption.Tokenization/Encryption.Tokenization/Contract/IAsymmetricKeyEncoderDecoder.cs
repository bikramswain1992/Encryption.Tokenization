namespace Encryption.Tokenization.Contract;

public interface IAsymmetricKeyEncoderDecoder
{
    (string publicKey, string privateKey) GenerateKeys();
    byte[] Encrypt<T>(T data, string publicKey);
    T Decrypt<T>(byte[] data, string privateKey);
}
