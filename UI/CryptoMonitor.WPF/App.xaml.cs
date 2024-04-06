using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using CryptoMonitor.WebAPIClients.Repositories;
using CryptoMonitor.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Windows;

namespace CryptoMonitor.WPF;

public partial class App
{
    public static Window? WindowActive => Current?.Windows?.Cast<Window>()?.FirstOrDefault(w => w.IsActive);

    public static Window? WindowFocused => Current?.Windows?.Cast<Window>()?.FirstOrDefault(w => w.IsFocused);

    public static Window? WindowCurrent => WindowFocused ?? WindowActive; 

    private static IHost __Hosting;

    public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IServiceProvider Services => Hosting.Services;

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddScoped<MainWindowViewModel>();
        services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(
            client =>
            {
                client.BaseAddress = new Uri($"{context.Configuration["WebAPI"]}/api/DataSources");
            });
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var host = Hosting;
        base.OnStartup(e);
        await host.StartAsync().ConfigureAwait(false);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using var host = Hosting;
        base.OnExit(e);
        await host.StopAsync().ConfigureAwait(false);
    }
}

