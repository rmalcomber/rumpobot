using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmpDiscordBot.Dtos
{
    public interface IStaticStringInjector
    {
        string GetKey();
        Task<string> ProcessCommand(string message, params string[] args);
    }
}
