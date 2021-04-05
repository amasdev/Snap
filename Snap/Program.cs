using Microsoft.Extensions.DependencyInjection;
using Snap.Services;

namespace Snap
{
    class Program
    {
        static void Main()
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<IAppService>().Start();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IAppService, AppService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IDisplayService, DisplayService>();
            return services;
        }
    }
}
