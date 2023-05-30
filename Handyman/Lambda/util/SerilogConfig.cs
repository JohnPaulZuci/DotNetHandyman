using Serilog;

public static class SerilogConfig
{
    public static Serilog.Core.Logger? Log = null;
    public static void Configure()
    {
         Log = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
    }
}
