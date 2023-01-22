using System.Threading.Tasks;
using Discord.WebSocket;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Watchers
{
    public class WatchCunt : IWatchResponse
    {
        public string[] KeysToWatch()
        {
            return new[] {"cunt"};
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            await socketMessage.Channel.SendMessageAsync(
                "Monty! Monty Monty Monty......MONTY you terrible cunt! what are you doing prowling round in the middle of the fucking night?", true);
            await socketMessage.Channel.SendFileAsync("C:\\Users\\royst\\Pictures\\Withnail\\Withnail-and-I.jpg");
            
        }
    }
}
