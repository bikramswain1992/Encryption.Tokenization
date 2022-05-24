using Encryption.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp;
public class TestEncryption
{
    public TestEncryption()
    {
        EncoderDecoder _encoderDecoder = new EncoderDecoder();
        var passwordHash = _encoderDecoder.HashPassword("ABC1234");
        Console.WriteLine(passwordHash.Salt);
        Console.WriteLine(passwordHash.Hash);
        var verifyPassword = _encoderDecoder.MatchPassword(passwordHash, "ABC1234");
        Console.WriteLine(verifyPassword);
    }
}
