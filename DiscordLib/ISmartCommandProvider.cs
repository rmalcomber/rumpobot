using System;
using System.Collections.Generic;
using System.Text;
using DiscordLib.SmartCommands;

namespace DiscordLib
{
    public interface ISmartCommandProvider
    {
        IDictionary<string, ISmartCommand> SmartCommands();
        ISmartCommand GetCommand(string command);
    }

    public class SmartCommandProvider : ISmartCommandProvider
    {
        private readonly IDictionary<string, ISmartCommand> _smartCommands = new Dictionary<string, ISmartCommand>();

        public SmartCommandProvider()
        {
            BuildDictionaryOfCommands();
        }

        public IDictionary<string, ISmartCommand> SmartCommands()
        {
            return _smartCommands;
        }

        public ISmartCommand GetCommand(string command)
        {
            return _smartCommands.ContainsKey(command) ? _smartCommands[command] : null;
        }

        private void BuildDictionaryOfCommands()
        {
            _smartCommands.Add("!ponce", new CmdPonce());
            _smartCommands.Add("!nick", new CmdNick());
        }
    }
}
