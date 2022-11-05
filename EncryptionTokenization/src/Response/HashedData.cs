using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Tokenization.Response;
public class HashedData
{
    public string Salt { get; set; }
    public string Hash { get; set; }
}
