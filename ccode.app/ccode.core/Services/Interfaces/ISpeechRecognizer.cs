namespace ccode.core.Services.Interfaces;

public interface ISpeechRecognizer
{
    IEnumerable<RecognizedSpeech> RecognizeSpeech();
}

public class RecognizedSpeech
{
    public String Text { get; set; }
    public Boolean HasText { get; set; }
}
