using Encryption.Tokenization;
using Encryption.Tokenization.Contract;
using EncryptionTokenization.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptionTokenization.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEncryptionTokenizationSingleton(this IServiceCollection services)
    {
        services.AddSingleton<IEncoderDecoder, EncoderDecoder>();
        services.AddSingleton<IAsymmetricKeyEncoderDecoder, AsymmetricKeyEncoderDecoder>();
        services.AddSingleton<ISymmetricKeyEncoderDecoder, SymmetricKeyEncoderDecoder>();
        services.AddSingleton<IJWTTokenizer, JWTTokenizer>();

        return services;
    }

    public static IServiceCollection AddEncryptionTokenizationScoped(this IServiceCollection services)
    {
        services.AddScoped<IEncoderDecoder, EncoderDecoder>();
        services.AddScoped<IAsymmetricKeyEncoderDecoder, AsymmetricKeyEncoderDecoder>();
        services.AddScoped<ISymmetricKeyEncoderDecoder, SymmetricKeyEncoderDecoder>();
        services.AddScoped<IJWTTokenizer, JWTTokenizer>();

        return services;
    }

    public static IServiceCollection AddEncryptionTokenizationTransient(this IServiceCollection services)
    {
        services.AddTransient<IEncoderDecoder, EncoderDecoder>();
        services.AddTransient<IAsymmetricKeyEncoderDecoder, AsymmetricKeyEncoderDecoder>();
        services.AddTransient<ISymmetricKeyEncoderDecoder, SymmetricKeyEncoderDecoder>();
        services.AddTransient<IJWTTokenizer, JWTTokenizer>();

        return services;
    }
}
