using System;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RmpDiscordBot.CommandlineArgs;
using RmpDiscordBot.Commands;
using RmpDiscordBot.Dtos;
using RmpDiscordBot.Providers;
using RmpDiscordBot.StaticStringInjectors;
using RmpDiscordBot.Watchers;

namespace RmpDiscordBot
{
    internal class Program
    {

        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private static ILogger<Program> _logger;
        private static IServiceProvider _serviceProvider;
        private static Options _options;
        static async Task Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    _options = o;
                });

            Console.CancelKeyPress += Console_CancelKeyPress;

            try
            {

                var serviceCollection = new ServiceCollection();

                ConfigureServiceCollection(serviceCollection);

                serviceCollection.AddSingleton<IOptionsProvider>((s) =>
                {
                    return new OptionsProvider(_options);
                });

                _serviceProvider = serviceCollection.BuildServiceProvider();


                ConfigureLocalLogging(_serviceProvider);

                _logger.LogInformation("Starting Program....");

                var taskManager = _serviceProvider.GetService<ITaskManager>();
                var mainApplication = _serviceProvider.GetService<IApplication>();

                await mainApplication.Main(args, _cancellationTokenSource.Token);

            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Good Bye!");
            }
        }

        private static void ConfigureLocalLogging(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            _logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        }

        private static void ConfigureServiceCollection(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<IApplication, Application>();
            services.AddSingleton<IDiscordService, DiscordService>();
            services.AddTransient<IStaticCommandProvider, JsonStaticCommandProvider>();
            

            AddAllCommands(services);
            AddAllWatchers(services);
            AddAllStaticStringInjectors(services);

            services.AddTransient<ICommanndsProvider, CommandsProvider>();

        }

        private static void AddAllCommands(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICommandMessage, CmdPing>()
                .AddTransient<ICommandMessage, CmdNick>()
                .AddTransient<ICommandMessage, CmdFourChan>()
                .AddTransient<ICommandMessage, CmdQuote>()
                .AddTransient<ICommandMessage, CmdPonce>()
                .AddTransient<ICommandMessage, CmdCatApi>()
                .AddTransient<ICommandMessage, CmdAdvice>()
                .AddTransient<ICommandMessage, CmdHelp>();
                
        }

        private static void AddAllStaticStringInjectors(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IStaticStringInjector, SsiDateTime>();
        }
        private static IServiceCollection AddAllWatchers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWatchResponse, WatchCunt>();
            return serviceCollection;
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _cancellationTokenSource.Cancel();
        }
    }
}
