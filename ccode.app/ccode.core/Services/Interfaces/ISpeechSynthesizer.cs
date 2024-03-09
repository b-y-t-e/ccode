using ccode.core.ValueObjects;

namespace ccode.core.Services.Interfaces;

public interface ISpeechSynthesizer
{
    IAsyncEnumerable<SynthesizedSpeech> SynthesizeSpeech(
        TranscribedSpeech transcribedSpeech);
}

public record SynthesizedSpeech(
    Speech Speech);
