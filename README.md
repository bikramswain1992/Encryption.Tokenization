Encryption.Tokenization
========================
Provides three functionalities BCrypt based hashing, Asymmetric Encoding and Decoding and JWT token generation.

Usage - EncoderDecoder
----------------------
This module provides BCrypt based hashing using the package **BCrypt.Net-Next(4.0.3)**.

Use IEncoderDecoder to inject dependency for EncoderDecoder class.

> IEncoderDecoder en = new EncoderDecoder();  
en.GenerateHash(string) //Takes a string parameter and generates a hashed value and a salt. The default workFactor is 12.  
en.GenerateHash(string, int) //Overload with string parameter for data to be hashed and int parameter for workFactor.  
en.GenerateHash(string, string) //Overload with first string paramater for data to be hashed and second string paramater for the salt to be used for hashing.  
  
> IEncoderDecoder en = new EncoderDecoder();
en.MatchHash(HashedData, string) //Takes first paramater as a HashedData type object and second paramater as string. Hashes the string parameter using the salt in HashedData object and compares the final hashed values.  
  
**Note: increasing the workFactor will increase the complexity of encryption as well as increasing the time required to encrypt. Ideal workFactor is between 10 to 13.**  

Usage - AsymmetricKeyEncoderDecoder
------------------------------------
This module provides RSA based asymmetric encryption and decryption functionality.

Use IAsymmetricKeyEncoderDecoder to inject dependency for AsymmetricKeyEncoderDecoder class.

> IAsymmetricKeyEncoderDecoder en = new AsymmetricKeyEncoderDecoder();  
en.GenerateKeys() //Returns a tuple where first value is public key and second value is private key.  
en.Encrypt<T>(T, string) //Takes T as generic first parameter for object to be encrypted and second string parameter for public key to be used for encryption.  
en.Decrypt<T>(byte[], string) //Takes byte[] as first parameter for encrypted object and second string parameter for private key to be used for decryption.
  
**Note: This uses a 2048 bit key encryption. Hence maximum 245 bytes data can be encrypted.**  

Usage - JWTTokenizer
---------------------
This module provides JWT token generation functionality.

Use IJWTTokenizer to inject dependency for JWTTokenizer class.  

> IJWTTokenizer tn = new JWTTokenizer(string?) //Optional parameter can be used to provide the RSA private key.  
tn.GenerateToken(GenerateTokenRequest) //Takes GenerateTokenRequest as paramater which contains the identity information and claims that will be tokenized.  
tn.ValidateToken(ValidateTokenRequest) //Takes ValidateTokenRequest as parameter which contains issuer, audience and JWT to be verified. Returns TokenValidationResponse object which contains the claims.  
  
