using System.Windows;
using WpfApplication = System.Windows.Application;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.WPF.Services;

namespace TaskPlanner.WPF;

public partial class App : WpfApplication
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<TaskJsonManager>()
            .BuildServiceProvider();

        var mainWindow = services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = services.GetRequiredService<MainViewModel>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}

