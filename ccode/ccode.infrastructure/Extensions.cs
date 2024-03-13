using ccode.core.Services.Interfaces;
using ccode.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ccode.infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddTransient<ISpeechRecognizer, AzureSpeechRecognizer>();
        services.AddTransient<IChatBotService, OpenAiChatBotService>();
        services.AddTransient<ISpeechSynthesizer, OpenAiSpeechSynthesizer>();

        return services;
    }
}
