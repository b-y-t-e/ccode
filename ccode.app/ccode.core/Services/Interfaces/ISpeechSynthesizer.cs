using ccode.core.ValueObjects;

namespace ccode.core.Services.Interfaces;

public interface ISpeechSynthesizer
{
    IAsyncEnumerable<Audio> SynthesizeSpeech(
        Transcription transcription);
}
