using Discord.WebSocket;
using Newtonsoft.Json;
using RmpDiscordBot.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Commands
{
    class CmdAdvice : ICommandMessage
    {
        public string GetCommand()
        {
            return "!advice";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            var advice = await GetAdviceResponse();

            socketMessage.Channel.SendMessageAsync(advice.slip.advice, true);
        }

        private async Task<AdviceResponse> GetAdviceResponse()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync("https://api.adviceslip.com/advice");
                return JsonConvert.DeserializeObject<AdviceResponse>(json);
            }
        }
    }
}
