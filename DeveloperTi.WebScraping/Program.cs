using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeveloperTi.WebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);
            services.BuildServiceProvider()
                    .GetService<ConsoleApp>()
                    .Run();
        }
    }
}
