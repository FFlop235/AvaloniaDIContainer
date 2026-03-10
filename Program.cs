using Avalonia;
using System;
using AvaloniaDIContainer.DB;
using AvaloniaDIContainer.ViewModels;
using AvaloniaDIContainer.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AvaloniaDIContainer;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            /*.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").AddEnvironmentVariables();
            })*/
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();
                services.AddSingleton<IPersonRepository, PersonRepository>();
                // При Transient создавался бы каждый раз новый Persons
                services.AddSingleton<StatisticsService>();
            }).Build();
        
        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(IServiceProvider hostServices)
        => AppBuilder.Configure(() => new App(hostServices))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}