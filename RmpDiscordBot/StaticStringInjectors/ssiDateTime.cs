using RmpDiscordBot.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.StaticStringInjectors
{
    class SsiDateTime : IStaticStringInjector
    {
        public string GetKey()
        {
            return "date";
        }

        public Task<string> ProcessCommand(string message, params string[] args)
        {
            return Task.FromResult(DateTime.Now.ToShortDateString());
        }
    }
}
