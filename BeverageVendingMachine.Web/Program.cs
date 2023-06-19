using BeverageVendingMachine.Infrastructure.Data;
using BeverageVendingMachine.Infrastructure.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();//.Run();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = scope.ServiceProvider.GetService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<BeverageVendingMachineContext>();
                    await context.Database.MigrateAsync();
                    await BeverageVendingMachineContextSeed.SeedAsync(context, loggerfactory);
                }
                catch (Exception ex)
                {
                    var loger = loggerfactory.CreateLogger<BeverageVendingMachineContext>();
                    loger.LogError(ex, "Something went wrong during migration");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
