using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Exceptions.Core;
using Serilog.Sinks.Elasticsearch;
using Serilog.Formatting.Compact;
using Nest;

namespace BlogHung.Infrastructure.Logging
{
    /// <summary>
    /// Serilog
    /// </summary>
    public static class SerilogExtentions
    {
        public static void CreateLoggerConfiguration(this IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            var config = serviceProvider.GetService<IConfiguration>();
            string exprInclude = "SourceType = 'USER-LOG'";

            var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers().WithRootName("Exception"))
            .Enrich.FromLogContext()
            .Filter.ByIncludingOnly(exprInclude);

            if (env.IsDevelopment())
            {
                loggerConfig.WriteTo.Async(a => a.Console());
            }

             if (1 == 1)
            {
                loggerConfig.WriteTo.File(
                    new CompactJsonFormatter(),
                    $"{Environment.CurrentDirectory}\\ErrorLogs\\log.txt",
                    rollingInterval: RollingInterval.Infinite); // Set rollingInterval to Infinite for a single file

                /*  loggerConfig.WriteTo.Elasticsearch(
                             new ElasticsearchSinkOptions(new Uri("http://103.130.215.219:9200"))
                             {
                                 IndexFormat = "pubd15",
                                 ModifyConnectionSettings = conn =>
                                 conn.BasicAuthentication("elastic", "57e3KYLIpVyh61S18KGs")
                             });*/
            }

            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
