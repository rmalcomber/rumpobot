using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RmpDiscordBot.CommandlineArgs
{


    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Static commands input file")]
        public string StaticCommands { get; set; }
    }
}
