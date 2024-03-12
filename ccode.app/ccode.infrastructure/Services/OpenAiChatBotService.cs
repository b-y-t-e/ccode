using System.Text;
using ccode.core.Services.Interfaces;
using ccode.core.ValueObjects;
using ccode.infrastructure.Options;
using OpenAI;
using OpenAI.Audio;
using OpenAI.Chat;

namespace ccode.infrastructure.Services;

public class OpenAiChatBotService : IChatBotService
{
    private AiCompletionOptions _aiCompletionOptions;

    public OpenAiChatBotService(AiCompletionOptions aiCompletionOptions)
    {
        _aiCompletionOptions = aiCompletionOptions;
    }

    public async IAsyncEnumerable<Prompt> ProcessPrompt(Prompt prompt)
    {
        using var api = new OpenAIClient(_aiCompletionOptions.Key);
        var messages = new List<Message>
        {
            new(Role.System,
                $"Answer concisely and briefly. " +
                $"Answer only in the following language: {_aiCompletionOptions.DefaultLanguage}"),
            new(Role.User, prompt.ToString())
        };

        var chatRequest = new ChatRequest(messages);

        await foreach (var completionUpdate in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
        {
            var message = completionUpdate.FirstChoice.Delta.ToString();
            if (string.IsNullOrEmpty(message))
                continue;

            yield return Prompt.From(message);
        }
    }
}
