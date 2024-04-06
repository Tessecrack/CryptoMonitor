using CryptoMonitor.API.Data;
using CryptoMonitor.DAL.Context;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.DAL.Repositories;
using CryptoMonitor.Interfaces.Base.Repositories;
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

            services.AddTransient<DataDBInitializer>();

            //services.AddScoped<IRepository<DataSource>, DbRepository<DataSource>>();
            //services.AddScoped<IRepository<DataValue>, DbRepository<DataValue>>();

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoMonitor.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDBInitializer dbInit)
        {
            dbInit.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoMonitor.API v1"));
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
