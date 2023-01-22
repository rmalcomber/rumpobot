using System.Threading.Tasks;
using Discord.WebSocket;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Commands
{
    public class CmdPonce : ICommandMessage    {
        public string GetCommand()
        {
            return "!ponce";
        }

        public async Task RunCommand(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("You'll be pleased to hear Monte's invited us for drinks.", true);
            await Task.Delay(3000);
            await message.Channel.SendMessageAsync("Balls to Monty we're getting out.", true);
            await Task.Delay(3000);
            await message.Channel.SendMessageAsync("Balls to Monty!? I've just spent an hour flattering the bugger.", true);            
            await Task.Delay(5000);
            await message.Channel.SendMessageAsync("There\'s a man over there doesn\'t like the perfume. The big one. Don\'t look, don\'t look. We\'re in danger, we\'ve got to get out.", true);
            await Task.Delay(8000);
            await message.Channel.SendMessageAsync("What are you talking about?", true);            
            await Task.Delay(3000);
            await message.Channel.SendMessageAsync("I've been called a ponce.", true);           
            await Task.Delay(3000);
            await message.Channel.SendMessageAsync("What fucker said that!?", true);
            await Task.Delay(5000);
            await message.Channel.SendFileAsync("C:\\Users\\royst\\Pictures\\withnail.jpg");
        }
    }
}
