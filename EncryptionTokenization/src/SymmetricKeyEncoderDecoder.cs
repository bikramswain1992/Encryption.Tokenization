using EncryptionTokenization.Contract;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTokenization;

public class SymmetricKeyEncoderDecoder : ISymmetricKeyEncoderDecoder
{
    /// <summary>
    /// Generates key
    /// </summary>
    /// <returns></returns>
    public string GenerateKey()
    {
        string key = Guid.NewGuid().ToString().Replace("-","");

        return key;
    }

    /// <summary>
    /// Encrypts provided data using the provided key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public byte[] Encrypt<T>(T data, string key)
    {
        string dataToEncrypt = JsonConvert.SerializeObject(data);

        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(dataToEncrypt);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return array;
    }

    /// <summary>
    /// Decrypts provided encrypted data using the provided key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Decrypt<T>(byte[] data, string key)
    {
        byte[] iv = new byte[16];

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        var decryptedData = streamReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(decryptedData);
                    }
                }
            }
        }
    }
}
