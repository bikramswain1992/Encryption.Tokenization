namespace EncryptionTokenization.Contract;

public interface ISymmetricKeyEncoderDecoder
{
    string GenerateKey();
    byte[] Encrypt<T>(T data, string key);
    T Decrypt<T>(byte[] data, string key);
}
