using ccode.core.Services.Interfaces;
using ccode.core.ValueObjects;
using ccode.infrastructure.Options;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace ccode.infrastructure.Services;

public class AzureSpeechRecognizer : ISpeechRecognizer
{
    private SpeechOptions _speechOptions;

    public AzureSpeechRecognizer(SpeechOptions speechOptions)
    {
        _speechOptions = speechOptions;
    }

    public async IAsyncEnumerable<Transcription> RecognizeSpeech(
        CancellationToken cancellationToken)
    {
        var speechConfig = SpeechConfig
            .FromSubscription(_speechOptions.Key, _speechOptions.Region);
        speechConfig.SpeechRecognitionLanguage = _speechOptions.DefaultLanguage;

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
        var conversationEnded = false;

        while (!conversationEnded)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    yield return Transcription.From(speechRecognitionResult.Text);
                    break;

                case ResultReason.NoMatch:
                    break;

                case ResultReason.Canceled:
                    var cancellationDetails = CancellationDetails.FromResult(speechRecognitionResult);
                    Console.WriteLine($"Speech Recognition canceled: {cancellationDetails.Reason}");
                    if (cancellationDetails.Reason == CancellationReason.Error)
                        Console.WriteLine($"Error details={cancellationDetails.ErrorDetails}");
                    break;
            }
        }
    }
}
