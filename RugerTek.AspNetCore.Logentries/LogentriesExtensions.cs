using System;
using Microsoft.Extensions.Logging;

namespace RugerTek.AspNetCore.Logentries
{
    public static class LogentriesExtensions
    {
        public static ILoggerFactory AddLogEntries(this ILoggerFactory loggerFactory, IServiceProvider provider, Action<LogentriesLoggerConfiguration> configure)
        {
            var config = new LogentriesLoggerConfiguration();
            configure(config);
            loggerFactory.AddProvider(new LogentriesLoggerProvider(provider, config));
            return loggerFactory;
        }
    }
}
