using System.Windows;
using WpfApplication = System.Windows.Application;
using Microsoft.Extensions.DependencyInjection;

namespace TaskPlanner.WPF;

public partial class App : WpfApplication
{
    protected override void OnStartup(StartupEventArgs e)
    {
        
        IServiceCollection services = new ServiceCollection();
        
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();
        
        var provider = services.BuildServiceProvider();
        
        var mainWindow = provider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();
        
        mainWindow.Show();
        
        base.OnStartup(e);
    }
}

