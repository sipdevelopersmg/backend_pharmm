using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Pharmm.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext().MinimumLevel.Debug()
                        .WriteTo.Logger(lc => lc.WriteTo.Console())

                        //.Filter.ByIncludingOnly("@Level in [''Debug,'Info','Warning', 'Error', 'Fatal']")
                        //.Filter.ByExcluding("StartsWith(SourceContext, 'Microsoft')")
                        .Filter.ByExcluding("RequestPath in ['/health']")
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                })
                
                //mengarahkan log ke datalust seq 
                //dan mengecualikan /health untuk healthcheck docker swarm
                .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                        .WriteTo.Logger(lc =>
                            lc.WriteTo.Seq("http://seq:5341")
                            // lc.WriteTo.Seq("http://174.138.22.139:5341")
                        )
                        //.Filter.ByIncludingOnly("@Level in ['Debug','Info','Warning', 'Error', 'Fatal']")
                        //.Filter.ByExcluding("StartsWith(, 'Microsoft')")
                        .Filter.ByExcluding("RequestPath in ['/health']")
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
