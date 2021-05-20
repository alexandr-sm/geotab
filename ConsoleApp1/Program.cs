using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<EntryPoint>().Run(args).Wait();
        }
    }
}
