using ccode.core.ValueObjects;

namespace ccode.core.Services.Interfaces;

public interface ISpeechRecognizer
{
    IAsyncEnumerable<Transcription> RecognizeSpeech(
        CancellationToken cancellationToken);
}
