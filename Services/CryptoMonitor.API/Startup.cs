using CryptoMonitor.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CryptoMonitor.API
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDB>(
                opt => opt
                   .UseSqlServer(
                        Configuration.GetConnectionString("Data"),
                        o => o.MigrationsAssembly("CryptoMonitor.DAL.SqlServer")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoMonitor.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoMonitor.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
