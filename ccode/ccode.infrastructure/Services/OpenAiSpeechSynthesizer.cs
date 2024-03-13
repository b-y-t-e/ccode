using ccode.core.Services.Interfaces;
using ccode.core.ValueObjects;
using ccode.infrastructure.Options;
using OpenAI;
using OpenAI.Audio;

namespace ccode.infrastructure.Services;

public class OpenAiSpeechSynthesizer : ISpeechSynthesizer
{
    private AiCompletionOptions _aiCompletionOptions;

    public OpenAiSpeechSynthesizer(AiCompletionOptions aiCompletionOptions)
    {
        _aiCompletionOptions = aiCompletionOptions;
    }

    public async IAsyncEnumerable<Audio> SynthesizeSpeech(Transcription transcription)
    {
        using var api = new OpenAIClient(_aiCompletionOptions.Key);
        var request = new SpeechRequest(transcription.ToString());

        async Task chunkCallback(ReadOnlyMemory<byte> chunkCallback)
        {
            await Task.CompletedTask;
        }

        var response = await api
            .AudioEndpoint
            .CreateSpeechAsync(request, chunkCallback);

        yield return Audio.From(response.ToArray());
    }
}
