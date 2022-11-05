using Encryption.Tokenization.Contract;
using Encryption.Tokenization.Response;

namespace Encryption.Tokenization;

public sealed class EncoderDecoder : IEncoderDecoder
{
    /// <summary>
    /// Hash data with default 12 iterations.
    /// </summary>
    /// <param name="data">Data to be hashed</param>
    /// <returns>Returns the hashed data and salt</returns>
    public HashedData GenerateHash(string data)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(data, salt);

        return new HashedData
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    /// <summary>
    /// Hash data with custom number if iterations
    /// </summary>
    /// <param name="data">Data to be hashed</param>
    /// <param name="iterations">Number of iterations</param>
    /// <returns>Returns the hashed data and salt</returns>
    public HashedData GenerateHash(string data, int iterations)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(iterations);
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(data, salt);

        return new HashedData
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    /// <summary>
    /// Hash data with custom salt
    /// </summary>
    /// <param name="data">Data to be hashed</param>
    /// <param name="salt">Salt to be used for hashing</param>
    /// <returns></returns>
    public HashedData GenerateHash(string data, string salt)
    {
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(data, salt);

        return new HashedData
        {
            Salt = salt,
            Hash = generatedHash
        };
    }

    /// <summary>
    /// Matches hashed data with the un hashed data
    /// </summary>
    /// <param name="hashedData">Hashed data</param>
    /// <param name="data">Unhashed data</param>
    /// <returns></returns>
    public bool MatchHash(HashedData hashedData, string data)
    {
        var salt = hashedData.Salt;
        var generatedHash = BCrypt.Net.BCrypt.HashPassword(data, salt);
        var verify = generatedHash == hashedData.Hash;
        return verify;
    }
}
