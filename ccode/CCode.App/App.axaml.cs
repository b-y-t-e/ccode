using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CCode.App.ViewModels;
using CCode.App.Views;
using CCode.Infrastructure;
using CCode.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ccode.app;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BindingPlugins.DataValidators.RemoveAt(0);

        var collection = new ServiceCollection();
        collection.AddApplication();
        collection.AddInfrastructure();

        var services = collection.BuildServiceProvider();

        var vm = services.GetRequiredService<VMMain>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new VMain
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
