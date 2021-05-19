using JokeGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<EntryPoint>();
            services.AddHttpClient<ChuckNorrisService>();
            services.AddHttpClient<PersonService>();
            services.AddSingleton<IPrinter, ConsolePrinter>();

            return services;
        }
    }
}
