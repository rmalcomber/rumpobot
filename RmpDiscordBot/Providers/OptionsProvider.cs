using System;
using System.Collections.Generic;
using System.Text;
using RmpDiscordBot.CommandlineArgs;

namespace RmpDiscordBot.Providers
{
    public interface IOptionsProvider
    {
        Options Get();
    }

    public class OptionsProvider : IOptionsProvider
    {
        private Options _options;

        public OptionsProvider(Options options)
        {
            _options = options;
        }

        public Options Get()
        {
            return _options;
        }
    }
}
