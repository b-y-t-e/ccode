using System.Reactive;
using ReactiveUI;

namespace ccode.app.ViewModels;

public class MainViewModel : ViewModelBase
{
    private bool _isListening;
    public bool IsListening
    {
        get => _isListening;
        set => this.RaiseAndSetIfChanged(ref _isListening, value);
    }

    private string _transcription;
    public string Transcription
    {
        get => _transcription;
        set => this.RaiseAndSetIfChanged(ref _transcription, value);
    }
    ////////////////////////////////////////

    public ReactiveCommand<Unit, Unit> Record { get; }
    public ReactiveCommand<Unit, Unit> Pause { get; }

    ////////////////////////////////////////

    public MainViewModel()
    {
        Record = ReactiveCommand.Create(() =>
        {
            IsListening = true;
        });
        Pause = ReactiveCommand.Create(() =>
        {
            IsListening = false;
        });

        Transcription = "";
    }
}
