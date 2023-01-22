using RmpDiscordBot.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Providers
{
    public interface ICommanndsProvider
    {
        Task<List<string>> Get();
    }

    class CommandsProvider : ICommanndsProvider
    {

        public async Task<List<string>> Get()
        {
            var cmds = new List<string>();

            cmds.Add("!catme");
            cmds.Add("!advice");
            cmds.Add("!ylyl - (NSFW)");
            cmds.Add("!nick");
            cmds.Add("!ponce");
            cmds.Add("!quote");

            return cmds;
            
        }
    }
}
