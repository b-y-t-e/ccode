using Avalonia.Controls;
using ReactiveUI;

namespace CCode.UI.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public ContentControl View { get; set; }
}
