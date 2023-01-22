using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordLib
{
    public interface ISmartCommandManager
    {

        Task RunCommand(string command, SocketMessage SocketMessage);
    }

    public class SmartCommandManager : ISmartCommandManager
    {
        private readonly ISmartCommandProvider _smartCommandProvider;

        public SmartCommandManager(ISmartCommandProvider smartCommandProvider)
        {
            this._smartCommandProvider = smartCommandProvider;
        }

        public async Task RunCommand(string command, SocketMessage socketMessage)
        {
            var cmd = _smartCommandProvider.GetCommand(command);
            await cmd.RunCommand(socketMessage);
        }
    }
}
