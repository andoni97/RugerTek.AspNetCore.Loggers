using Microsoft.Extensions.Logging;

namespace RugerTek.AspNetCore.Logentries
{
    public class LogentriesLoggerConfiguration
    {
        public string Token { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
