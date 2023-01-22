using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord.WebSocket;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Commands
{
    class CmdNick : ICommandMessage
    {
        readonly Random rand = new Random(DateTime.Now.Millisecond);

        public string GetCommand()
        {
            return "!nick";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            await socketMessage.Channel.SendFileAsync(GetRandomImage(), RandomQuote(), true);
        }


        public string GetRandomImage()
        {
            var files = Directory.GetFiles(Environment.GetEnvironmentVariable("NICKLOC"), "*", SearchOption.AllDirectories);
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
                "If I were to send you flowers where would I... no, let me rephrase that. If I were to let you suck my tongue, would you be grateful?",
                "I did a bare ass 360 triple back flip in front of twenty two thousand people. It's kind of funny, it's on Youtube, check it out. But when my dad got sick, I did something way crazier than that.",
                 "Did I ever tell ya that this here jacket represents a symbol of my individuality, and my belief in personal freedom?",
                  "Sorry boss, but there's only two men I trust. One of them's me. The other's not you.",
                  "What's in the bag? A shark or something?",
                  "What do you think I'm gonna do? I'm gonna save the fuckin' day!",
                   "You\'ll be seeing a lot of changes around here. Papa\'s got a brand new bag.",
                    "Tool up, honey bunny. It\'s time to get bad guys.",
                    "Honey? Uh... You wanna know who really killed JFK?",
                    "How, in the name of Zeus's butthole, did you get out of your cell?",
                    "If I were to send you flowers where would I... no, let me rephrase that. If I were to let you suck my tongue, would you be grateful?"
            };
            return quotes[rand.Next(quotes.Count)];
        }
    }
}
