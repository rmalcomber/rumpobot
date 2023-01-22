using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using RmpDiscordBot.Dtos;
using RmpDiscordBot.Providers;

namespace RmpDiscordBot
{
    public interface IDiscordService
    {
        Task RunService(CancellationToken cancellationToken);
    }

    public class DiscordService : IDiscordService
    {
        private readonly ILogger<DiscordService> _logger;
        private readonly IEnumerable<ICommandMessage> _commandMessages;
        private readonly IEnumerable<IWatchResponse> _watchResponses;
        private readonly IEnumerable<IStaticStringInjector> _staticStringInjectors;
        private readonly IStaticCommandProvider _staticCommandProvider;
        private readonly string _botToken = Environment.GetEnvironmentVariable("BOTKEY");
        private DiscordSocketClient _client;

        public DiscordService(ILogger<DiscordService> logger, IEnumerable<ICommandMessage> commandMessages, IEnumerable<IWatchResponse> watchResponses, IStaticCommandProvider staticCommandProvider, IEnumerable<IStaticStringInjector> staticStringInjectors)
        {
            this._logger = logger;
            _commandMessages = commandMessages;
            _watchResponses = watchResponses;
            _staticCommandProvider = staticCommandProvider;
            _staticStringInjectors = staticStringInjectors;
        }

        public async Task RunService(CancellationToken cancellationToken)
        {
            await SetUpClient();

            _client.MessageReceived += _client_MessageReceived;

            while (!cancellationToken.IsCancellationRequested) { }

            await TearDown();
        }


        private async Task _client_MessageReceived(SocketMessage message)
        {
            if (message.Source != MessageSource.Bot)
            {

                var s = message.Content;
                var args = s.Split(' ');
                var firstWord = args[0].ToLower();

                if (firstWord.StartsWith("!"))
                {
                    await CommandRegister(firstWord, message);
                }

                await WatcherRegister(message);
            }
        }

        private async Task TearDown()
        {
            await _client.LogoutAsync();
        }

        private async Task CommandRegister(string command, SocketMessage socketMessage)
        {
            foreach (var commandMessage in _commandMessages)
            {
                var c = commandMessage.GetCommand();
                var matched = command.ToLower() == c.ToLower();
                if (matched)
                {
                    await commandMessage.RunCommand(socketMessage);
                }

            }

            var staticCommands = await _staticCommandProvider.Get();

            foreach (var c in staticCommands.Commands)
            {
                var matched = command.ToLower() == c.Command.ToLower();

                if (matched)
                {
                    var response = await CheckAndInjectStaticMessage(c);
                    await socketMessage.Channel.SendMessageAsync(response, true);
                }
            }


        }

        private async Task<string> CheckAndInjectStaticMessage(StaticCommand staticCommand)
        {
            var regex = new Regex(@"{(\w*)\|?(.*)}");

            var match = regex.Match(staticCommand.Response);

            if (match.Success)
            {
                var keyword = match.Groups[1].Value;
                List<string> args = new List<string>();

                if (match.Groups[2] != null)
                {
                    args = match.Groups[2].Value.Split(';').ToList();
                }

                var injector = _staticStringInjectors.FirstOrDefault(a => a.GetKey() == keyword);
                var injectorResp = await injector.ProcessCommand(keyword, args.ToArray());
                var replaced = regex.Replace(staticCommand.Response, injectorResp);
                return replaced;
            }

            return staticCommand.Response;
        }

        private async Task WatcherRegister(SocketMessage socketMessage)
        {
            foreach (var response in _watchResponses)
            {
                if (response.KeysToWatch().Any(s => socketMessage.Content.Contains(s)))
                {
                    await response.RunCommand(socketMessage);

                }
            }
        }

        private async Task SetUpClient()
        {
            _client = new DiscordSocketClient();
            _client.Log += Client_Log;

            await _client.LoginAsync(TokenType.Bot, _botToken);
            await _client.StartAsync();
        }

        private async Task Client_Log(Discord.LogMessage arg)
        {
            _logger.LogInformation(arg.Message);
            await Task.CompletedTask;
        }
    }
}
