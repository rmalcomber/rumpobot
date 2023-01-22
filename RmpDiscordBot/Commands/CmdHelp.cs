using Discord.WebSocket;
using RmpDiscordBot.Dtos;
using RmpDiscordBot.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Commands
{
    class CmdHelp : ICommandMessage
    {
        private readonly ICommanndsProvider commanndsProvider;

        public CmdHelp(ICommanndsProvider commanndsProvider)
        {
            this.commanndsProvider = commanndsProvider;
        }

        public string GetCommand()
        {
            return "!help";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            var cmds = await commanndsProvider.Get();

    

            var sb = new StringBuilder();
            sb.Append("All available commands from Static Command library and dynamic library are: ");

            foreach (var item in cmds)
            {
                sb.AppendLine(item);
            }

            await socketMessage.Channel.SendMessageAsync(sb.ToString());
        }
    }
}
