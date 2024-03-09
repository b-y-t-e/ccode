using ccode.core.ValueObjects;

namespace ccode.core.Services.Interfaces;

public interface ISpeechRecognizer
{
    IAsyncEnumerable<RecognizedSpeech> RecognizeSpeech();
}

public record RecognizedSpeech(
    TranscribedSpeech Transcription);
