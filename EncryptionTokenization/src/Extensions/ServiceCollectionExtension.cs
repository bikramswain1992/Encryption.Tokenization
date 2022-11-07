using Encryption.Tokenization;
using Encryption.Tokenization.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptionTokenization.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEncryptionTokenization(IServiceCollection services)
    {
        services.AddSingleton<IEncoderDecoder, EncoderDecoder>();
        services.AddSingleton<IAsymmetricKeyEncoderDecoder, AsymmetricKeyEncoderDecoder>();
        services.AddSingleton<IJWTTokenizer, JWTTokenizer>();

        return services;
    }
}
