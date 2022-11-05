using Encryption.Tokenization.Contract;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Encryption.Tokenization;

public sealed class AsymmetricKeyEncoderDecoder : IAsymmetricKeyEncoderDecoder
{
    /// <summary>
    /// Generates Public and Private keys
    /// </summary>
    /// <returns></returns>
    public (string publicKey, string privateKey) GenerateKeys()
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        string publicKey = rsa.ToXmlString(false);
        string privateKey = rsa.ToXmlString(true);

        return (publicKey, privateKey);
    }

    /// <summary>
    /// Encrypts provided data using the provided public key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="publicKey"></param>
    /// <returns></returns>
    public byte[] Encrypt<T>(T data, string publicKey)
    {
        byte[] dataToEncrypt = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

        byte[] encryptedData;
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        { 
            rsa.FromXmlString(publicKey);
  
            encryptedData = rsa.Encrypt(dataToEncrypt, false);
        }
        return encryptedData;
    }

    /// <summary>
    /// Decrypts provided encrypted data using the provided private key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    public T Decrypt<T>(byte[] data, string privateKey)
    {
        byte[] decryptedData;
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        { 
            rsa.FromXmlString(privateKey);
            decryptedData = rsa.Decrypt(data, false);
        }

        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(decryptedData));
    }
}
