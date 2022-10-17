using Encryption.Tokenization.Contract;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Encryption.Tokenization;

public sealed class AsymmetricKeyEncoderDecoder : IAsymmetricKeyEncoderDecoder
{
    public (string publicKey, string privateKey) GenerateKeys()
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string publicKey = rsa.ToXmlString(false);
        string privateKey = rsa.ToXmlString(true);

        return (publicKey, privateKey);
    }

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
