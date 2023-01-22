using System.Threading;
using CommonLib;
using DiscordLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace RumpoBot {
    public class Program
    {

        private static ServiceManager _serviceManager;
        private static ILogger<Program> _logger;
        static void Main(string[] args) {

            ConfigureLogging();

            Log.Debug("Configuring IOC Container and building dependencies");
            var serviceCollection = ConfigureIocContainer();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _logger = serviceProvider.GetService<ILogger<Program>>();

            RunServiceRunners(serviceProvider);


            _serviceManager.RunServices();
        }

        private static IServiceCollection ConfigureIocContainer() {
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog(dispose: true));

            return ConfigureLocalServices(serviceCollection);
        }

        private static IServiceCollection ConfigureLocalServices(IServiceCollection serviceCollection) {
            serviceCollection.AddSingleton((builder) => new CancellationTokenSource());
            serviceCollection.AddSingleton<ICommandProvider, BasicCommandProvider>();
            serviceCollection.AddSingleton<ICommandParser, CommandParser>();
            return BuildExternalServices(serviceCollection);
        }

        private static IServiceCollection BuildExternalServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IServiceRunner, DiscordServiceRunner>();
            return serviceCollection;
        }

        

        private static void RunServiceRunners(ServiceProvider serviceProvider)
        {
            _logger.LogInformation("Starting Services...");

            var serviceRunners = serviceProvider.GetServices<IServiceRunner>();


            var cancellationTokenSource = serviceProvider.GetService<CancellationTokenSource>();

            _serviceManager = new ServiceManager(serviceRunners, cancellationTokenSource);
            _serviceManager.RunServices();
        }

        private static void ConfigureLogging() {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();


        }
    }
}
