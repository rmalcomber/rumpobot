using System.Threading.Tasks;
using Discord.WebSocket;

namespace RmpDiscordBot.Dtos
{
    public interface ICommandMessage
    {
        string GetCommand();
        Task RunCommand(SocketMessage socketMessage);
    }
}
