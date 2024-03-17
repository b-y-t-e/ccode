using ccode.core.Services.Interfaces;
using CCode.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CCode.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddTransient<ISpeechRecognizer, AzureSpeechRecognizer>();
        services.AddTransient<IChatBotService, OpenAiChatBotService>();
        services.AddTransient<ISpeechSynthesizer, OpenAiSpeechSynthesizer>();
        services.AddTransient<IClipboardService, OsWindowsClipboardService>();

        return services;
    }
}
