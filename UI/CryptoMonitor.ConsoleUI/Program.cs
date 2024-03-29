using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoMonitor.ConsoleUI
{
    class Program
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            Console.WriteLine("Done");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}