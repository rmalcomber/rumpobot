using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordLib
{
    public static class ServiceBuilder
    {
        public static IServiceCollection BuildService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISmartCommandProvider, SmartCommandProvider>();
            serviceCollection.AddTransient<ISmartCommandManager, SmartCommandManager>();
            return serviceCollection;
        }
    }
}
