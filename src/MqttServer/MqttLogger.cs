using Serilog;
using Serilog.Events;

namespace MqttServer
{
    public class MqttLoggerConf
    {
        public string File { get; set; }
        public RollingInterval RollingInterval { get; set; }
    }

    public class MqttLogger : ILogger
    {
        public readonly ILogger Logger;
        public MqttLogger(MqttLoggerConf conf)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(conf.File, rollingInterval: conf.RollingInterval)
                .CreateLogger();
            Logger = Log.Logger;
        }

        public MqttLogger(string logFile)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Logger = Log.Logger;
        }

        public void Write(LogEvent logEvent)
        {
            Logger.Write(logEvent);
        }
    }
}