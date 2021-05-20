using JokeGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JokeGenerator
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            
            services.AddLogging(l => l.AddConsole());
            services.AddTransient<EntryPoint>();
            services.AddHttpClient<IChuckNorrisService, ChuckNorrisService>();
            services.AddHttpClient<PersonService>();
            services.AddSingleton<IPrinter, ConsolePrinter>();
            services.AddMemoryCache();
            

            return services;
        }
    }
}
