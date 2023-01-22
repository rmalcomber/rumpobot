using RmpDiscordBot.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Providers
{

    public interface IStaticCommandProvider
    {
        Task<StaticCommands> Get();
    }

    class TestStaticCommandProvider : IStaticCommandProvider
    {
        public Task<StaticCommands> Get()
        {

            var commands = new List<StaticCommand>
            {
                new StaticCommand() { Command = "!Testing", Response = "Response from test" }
            };

            var staticCommands = new StaticCommands()
            {
                Commands = commands
            };


            return Task.FromResult(staticCommands);
        }
    }

    class JsonStaticCommandProvider : IStaticCommandProvider
    {
        private IOptionsProvider _optionsProvider;

        public JsonStaticCommandProvider(IOptionsProvider optionsProvider)
        {
            _optionsProvider = optionsProvider;
        }

        private Task<StaticCommands> ParseStaticCommands()
        {
            using (var r = new StreamReader(_optionsProvider.Get().StaticCommands))
            {
                var json = r.ReadToEnd();
                var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticCommands>(json);
                return Task.FromResult(parsed);
            }

        }


        public Task<StaticCommands> Get()
        {
            return ParseStaticCommands();
        }
    }
}
