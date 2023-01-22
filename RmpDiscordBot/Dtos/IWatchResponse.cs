using System.Threading.Tasks;
using Discord.WebSocket;

namespace RmpDiscordBot.Dtos
{
    public interface IWatchResponse
    {
        string[] KeysToWatch();
        Task RunCommand(SocketMessage socketMessage);

    }
}
