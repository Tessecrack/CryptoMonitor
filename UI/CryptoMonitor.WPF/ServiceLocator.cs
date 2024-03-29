using CryptoMonitor.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoMonitor.WPF
{
    internal class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
