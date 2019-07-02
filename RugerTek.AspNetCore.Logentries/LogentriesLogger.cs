using System;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;

namespace RugerTek.AspNetCore.Logentries
{
    public class LogentriesLogger : ILogger
    {
        private readonly string _name;
        private readonly LogentriesLoggerConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public LogentriesLogger(string name, LogentriesLoggerConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _name = name;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://webhook.logentries.com/noformat/logs/{_config.Token}")
            {
                Content = new StringContent(message, Encoding.UTF8, "text/plain")
            };
            httpClient.SendAsync(request);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _config.LogLevel <= logLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
