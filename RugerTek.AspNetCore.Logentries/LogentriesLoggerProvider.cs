using System;
using System.Collections.Concurrent;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace RugerTek.AspNetCore.Logentries
{
    public class LogentriesLoggerProvider : ILoggerProvider
    {
        private readonly LogentriesLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, LogentriesLogger> _loggers = new ConcurrentDictionary<string, LogentriesLogger>();
        private readonly IServiceProvider _provider;

        public LogentriesLoggerProvider(IServiceProvider provider, LogentriesLoggerConfiguration config)
        {
            _config = config;
            _provider = provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new LogentriesLogger(name, _config, _provider.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
