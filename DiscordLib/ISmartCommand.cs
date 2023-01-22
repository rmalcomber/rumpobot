using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordLib
{
    public interface ISmartCommand
    {
        Task RunCommand(SocketMessage socketMessage);
    }
}
