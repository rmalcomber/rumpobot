using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommonLib;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;


namespace DiscordLib {
    public class DiscordServiceRunner : IServiceRunner {
        private readonly ILogger<DiscordServiceRunner> _logger;
        private readonly ICommandProvider _commandProvider;
        private readonly ICommandParser _commandParser;

        public DiscordServiceRunner(ILogger<DiscordServiceRunner> logger, ICommandProvider commandProvider, ICommandParser commandParser) {
            _logger = logger;
            _commandProvider = commandProvider;
            _commandParser = commandParser;
        }

        public async Task RunService(CancellationToken cancellationToken) {

            _logger.LogInformation("Starting Discord Service");

            var client = new DiscordSocketClient();
            client.Log += Client_Log;
            client.MessageReceived += Client_MessageReceived;

            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORDKEY"));
            await client.StartAsync();

            _logger.LogInformation("Discord Service Started");
            await Task.Delay(-1, cancellationToken);
        }

        private async Task Client_MessageReceived(SocketMessage message) {
            if (message.Source != MessageSource.Bot) {
                await HandleCommands(message); 
            }
        }

        private async Task HandleCommands(SocketMessage message) {
            var cmd = CheckForCommand(message);
            var hasCommand = !string.IsNullOrWhiteSpace(cmd);

            if (hasCommand) {
                var command = _commandProvider.GetCommand(cmd);
                await RunCommand(message, command);
            }
        }

        private async Task RunCommand(SocketMessage message, Command command) {
            if (command != null) {

                var resp = _commandParser.ParseCommand(command);
                await message.Channel.SendMessageAsync(resp, true);
            }
        }

        private static string CheckForCommand(IMessage message)
        {
            const string pattern = @"(\!\w*)";

            var response = Regex.Match(message.Content, pattern);
            return response.Success ? response.Value : string.Empty;
        }

        private async Task Client_Log(LogMessage arg) {
            _logger.LogInformation(arg.Message);
            await Task.Delay(1);
        }
    }
}
