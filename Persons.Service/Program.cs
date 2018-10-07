
using System;
using Serilog;
using Topshelf;

namespace Persons.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            HostFactory.Run(x =>
            {
                x.UseSerilog(logger);
                x.Service<HostService>(s =>
                {
                    s.ConstructUsing(settings => new HostService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("NancyFxService selfhosting Windows Service Example");
                x.SetDisplayName("NancyFxService Example");
                x.SetServiceName("NancyFxService");
                x.StartAutomatically();
            });
        }
    }
}
