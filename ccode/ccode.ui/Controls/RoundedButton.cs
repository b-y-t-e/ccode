using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Newtonsoft.Json;

namespace CCode.UI.Controls;

public class RoundedButton : Button
{
    public RoundedButton()
    {

    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property.Name == nameof(this.Width))
        {
            CornerRadius = new CornerRadius(Width / 2);
            if (Width != Height)
                Height = Width;
        }

        if (change.Property.Name == nameof(this.Height))
        {
            CornerRadius = new CornerRadius(Height / 2);
            if (Width != Height)
                Width = Height;
        }

        base.OnPropertyChanged(change);
    }
}
