using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordLib.SmartCommands
{
    class CmdNick : ISmartCommand
    {
        readonly Random rand = new Random(DateTime.Now.Millisecond);

        public async Task RunCommand(SocketMessage socketMessage)
        {
            await socketMessage.Channel.SendFileAsync(GetRandomImage(), RandomQuote(), true);
        }


        public string GetRandomImage()
        {
            var files = Directory.GetFiles("C:\\Users\\royst\\source\\repos\\RmpDiscordBot\\NickImages\\","*", SearchOption.AllDirectories);
            return files[rand.Next(files.Length)];
        }

        public string RandomQuote()
        {
            var quotes = new List<string>
            {
                "Well... gosh, kind of a lot's happened since then.",
                "That's funny, my name's Roger. Two Rogers don't make a right!",
                "Well, I'm one of those fortunate people who like my job, sir. Got my first chemistry set when I was seven, blew my eyebrows off, we never saw the cat again, been into it ever since.",
                "Put... the bunny... back... in the box.",
                "Hey! My mama lives in a trailer!",
                "Killing me wont bring back your god damn honey!",
                "If I were to send you flowers where would I... no, let me rephrase that. If I were to let you suck my tongue, would you be grateful?"
            };
            return quotes[rand.Next(quotes.Count)];
        }
    }
}
