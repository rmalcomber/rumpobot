using Discord.WebSocket;
using Newtonsoft.Json;
using RmpDiscordBot.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Commands
{
    class CmdCatApi : ICommandMessage
    {
        public string GetCommand()
        {
            return "!catme";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            var catresp = await GetCatResponse();
            var filename = await DownloadFile(catresp.FirstOrDefault().url);

            await socketMessage.Channel.SendFileAsync(filename);
        }

        private Task<string> DownloadFile(string url)
        {
            var filename = $"C:\\Users\\royst\\source\\repos\\RmpDiscordBot\\CatImages\\{DateTime.UtcNow.Ticks}.jpg";
            using (var client = new WebClient())
            {
                client.DownloadFile(url, filename);
            }

            return Task.FromResult(filename);
        }

        private async Task<IEnumerable<CatObj>> GetCatResponse()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = await client.GetStringAsync("https://api.thecatapi.com/v1/images/search");

                    return JsonConvert.DeserializeObject<IEnumerable<CatObj>>(json);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
