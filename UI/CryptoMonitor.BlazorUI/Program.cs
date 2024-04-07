using CryptoMonitor.BlazorUI;
using CryptoMonitor.BlazorUI.Infrastructure.Extensions;
using CryptoMonitor.Domain.Base;
using CryptoMonitor.Interfaces.Base.Repositories;
using CryptoMonitor.WebAPIClients.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

services.AddApi<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>("api/SourcesRepository");

await builder.Build().RunAsync();
