using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RmpDiscordBot {

    public interface IApplication {
        Task Main(string[] args, CancellationToken cancellationToken);
    }

    public class Application : IApplication {
        private readonly ILogger<Application> _logger;
        private readonly IDiscordService _discordService;
        private readonly ITaskManager _taskManager;

        public Application(ILogger<Application> logger, IDiscordService discordService) {
            this._logger = logger;
            _discordService = discordService;
            ;
        }

        public Task Main(string[] args, CancellationToken cancellationToken) {
            _logger.LogInformation("Main Application Starting....");

            Task.WaitAll(
                _discordService.RunService(cancellationToken)
                );

            return Task.CompletedTask;
        }
    }
}
