using Microsoft.Extensions.DependencyInjection;

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
