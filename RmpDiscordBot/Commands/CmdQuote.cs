using System.Net.Http;
using System.Threading.Tasks;
using Discord.WebSocket;
using Newtonsoft.Json;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Commands {
    class CmdQuote : ICommandMessage {




        public async Task<string> GetQuote() {
            using (var client = new HttpClient()) {
                HttpResponseMessage response =
                    await client.GetAsync("https://api.forismatic.com/api/jsonp?&lang=en");

                if (response.IsSuccessStatusCode) {
                    var result = await response.Content.ReadAsStringAsync();
                    var quote = JsonConvert.DeserializeObject<Quote>(result);
                    return $"{quote.quoteText} - {quote.quoteAuthor}";
                }
            }

            return "No quote at the moment";
        }


        public class Quote {
            public string quoteText { get; set; }
            public string quoteAuthor { get; set; }
            public string senderName { get; set; }
            public string senderLink { get; set; }
            public string quoteLink { get; set; }
        }

        public string GetCommand()
        {
            return "!quote";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            var quote = await GetQuote();
            await socketMessage.Channel.SendMessageAsync(quote);
        }
    }
}
