using Domain;
using Domain.Mom;
using Domain.Services;
using Domain.Tools;
using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestMain
{
    public class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(
                builder => builder
                .AddConsole()
                .AddDebug()
                .SetMinimumLevel(LogLevel.Debug)
            );

            Console.WriteLine("Hello, World!");

            ServiceCollection services = new ServiceCollection();
            AsmLoader.LoadPlugins(@"c:\plugins", services);

            MomListener listener = new MomListener(loggerFactory.CreateLogger<Domain.Mom.MomListener>());
            listener.Initialize();
            listener.Run();

            NotificationService notifSrv = new NotificationService(listener, loggerFactory.CreateLogger<Domain.Services.NotificationService>());
            notifSrv.Run();
        }
    }
}
