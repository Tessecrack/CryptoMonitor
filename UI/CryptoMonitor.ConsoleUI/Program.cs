using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using CryptoMonitor.WebAPIClients.Repositories;
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
            //https://localhost:7108/

            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(
                client =>
                {
                    client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/"); 
                    // "/" слэш в конце адреса обязателен
                });
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            var dataSources = Services.GetRequiredService<IRepository<DataSource>>();

            var count = await dataSources.GetCountAsync();
            Console.WriteLine($"Count: {count}");

            //var addedElement = await dataSources.AddAsync(
            //    new DataSource() 
            //    { 
            //        Name = "Test ADDED",
            //        Description = "Test add command"
            //    });
            //count = await dataSources.GetCountAsync();
            //Console.WriteLine($"Count: {count}");
            //var someSources = await dataSources.GetAsync(3, 5);
            //var pages = await dataSources.GetPageAsync(4, 3);
            //var editedItem = await dataSources.UpdateAsync(
            //    new DataSource()
            //    { 
            //        Id = 6,
            //        Name = $"Source - {DateTime.Now:HH-mm-ss}",
            //        Description = $"Edited source at {DateTime.Now}"
            //    });

            var item = await dataSources.GetByIdAsync(14);
            var deletedItem = await dataSources.DeleteAsync(item);

            var sources = await dataSources.GetAllAsync();
            foreach (var source in sources)
            {
                Console.WriteLine($"{source.Id} {source.Name} {source.Description}");
            }
            Console.WriteLine("Done");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}