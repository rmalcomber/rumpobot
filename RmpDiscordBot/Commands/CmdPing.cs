using System.Threading.Tasks;
using Discord.WebSocket;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Commands
{
    public class CmdPing : ICommandMessage
    {
        public string GetCommand()
        {
            return "!Ping";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            await socketMessage.Channel.SendMessageAsync("Pong!");
        }
    }
}
