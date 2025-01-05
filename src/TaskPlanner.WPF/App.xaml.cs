using System.Windows;
using WpfApplication = System.Windows.Application;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.WPF.Services;

namespace TaskPlanner.WPF;

public partial class App : WpfApplication
{
    protected override void OnStartup(StartupEventArgs e)
    {
        
        IServiceCollection services = new ServiceCollection();
        
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<TaskJsonManager>();
        
        var provider = services.BuildServiceProvider();
        
        var mainWindow = provider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();
        
        mainWindow.Show();
        
        base.OnStartup(e);
    }
}

