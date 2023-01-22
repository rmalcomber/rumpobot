using System;
using System.Collections.Generic;
using System.Text;

namespace RmpDiscordBot.Dtos
{
    public class StaticCommand
    {
        public string Command { get; set; }
        public string Response { get; set; }
    }

    public class StaticCommands
    {
        public IEnumerable<StaticCommand> Commands { get; set; }
    }
}
