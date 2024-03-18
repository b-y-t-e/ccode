using System;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using ccode.core.Services.Interfaces;
using CCode.UI.ViewModels;
using ReactiveUI;

namespace CCode.App.ViewModels;

public class VMMain : ViewModelBase
{
    private IClipboardService _clipboardService;
    private ISpeechRecognizer _speechRecognizer;
    private CancellationTokenSource _cancellationTokenSource;

    ////////////////////////////////////////

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

    public ReactiveCommand<Unit, Unit> RecordCommand { get; }
    public ReactiveCommand<Unit, Unit> PauseCommand { get; }

    ////////////////////////////////////////

    public VMMain(ISpeechRecognizer speechRecognizer, IClipboardService clipboardService)
    {
        _speechRecognizer = speechRecognizer;
        _clipboardService = clipboardService;
        _cancellationTokenSource = new CancellationTokenSource();

        Transcription = "";
        RecordCommand = ReactiveCommand.CreateFromTask(Record);
        PauseCommand = ReactiveCommand.CreateFromTask(Pause);
    }

    private async Task Record()
    {
        IsListening = true;

        Task.Run(async () =>
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await foreach (var transcription in _speechRecognizer.RecognizeSpeech(_cancellationTokenSource.Token))
            {
                var clipboardText = TopLevel.GetTopLevel(this.View)?.Clipboard;

                StringBuilder text = new StringBuilder();
                if (!Transcription.EndsWith(" "))
                    text.Append(" ");
                text.Append(transcription);
                _clipboardService.PasteFromClipboard();
                Transcription += text.ToString();
            }
        });
    }

    private async Task Pause()
    {
        IsListening = false;
        _cancellationTokenSource.Cancel();
    }
}
