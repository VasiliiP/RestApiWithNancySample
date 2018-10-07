using Nancy.Hosting.Self;
using System;
using Topshelf;
using Topshelf.Logging;

namespace Persons.Service
{
    class HostService 
    {
        private readonly NancyHost _NancyHost;
        private static readonly LogWriter _Log = HostLogger.Get<HostService>();

        public HostService()
        {
            var url = "http://127.0.0.1:9000";
            _NancyHost = new NancyHost(new Uri(url));
        }

        public void Start()
        {
            _Log.Info("Starting");
            _NancyHost.Start();
        }


        public void Stop()
        {
            _Log.Info("Stopping");
            _NancyHost.Stop();
            _NancyHost.Dispose();
        }

    }
}
