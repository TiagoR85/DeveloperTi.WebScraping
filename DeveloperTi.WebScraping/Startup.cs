using DeveloperTi.WebScraping.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DeveloperTi.WebScraping
{
    internal static class Startup
    {
        internal static void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory()
                                                .Replace(@"bin\Debug\netcoreapp2.1", ""))
                          .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DataBase"));
            //services.AddTransient<IRepositoryGeneric, RepositoryGeneric>();
            services.AddTransient<IUnityOfWork, UnityOfWork>();
            services.AddTransient<ConsoleApp>();
        }
    }
}