using Avalonia.Controls;
using CCode.UI.ViewModels;

namespace CCode.UI.Views;

public class ViewBase : UserControl
{
    protected override void OnDataContextChanged(EventArgs e)
    {
        if (this.DataContext is ViewModelBase viewModelBase)
            viewModelBase.View = this;
        base.OnDataContextChanged(e);
    }
    

    protected override void OnDataContextBeginUpdate()
    {
        base.OnDataContextBeginUpdate();
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
    }
}
