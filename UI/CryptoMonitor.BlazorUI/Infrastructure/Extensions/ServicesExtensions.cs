using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CryptoMonitor.BlazorUI.Infrastructure.Extensions
{
	internal static class ServicesExtensions
	{
		public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address)
			where IInterface : class where IClient : class, IInterface
		{
			return services
				.AddHttpClient<IInterface, IClient>((host, client) =>
				{
					client.BaseAddress
						= new($"{host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress}{address}");
				});
		}
	}
}
