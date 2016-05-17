using inRiver.Common.Log;
using inRiver.Common.Log.Loggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Bootstrapper
{
    public static class Program
    {
        public static void Main()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "inRiver.Server.exe.config");

            // Run service
            var serverWindowsService = new ExtendableServerWindowsService();
            if (Debugger.IsAttached)
            {
                var loggers = Logger.Instance.Loggers;

                var consoleLogger = new ConsoleLogger();
                consoleLogger.Name = "ConsoleLogger";
                loggers.Add(consoleLogger);

                serverWindowsService.ExecuteOnStartEvent(null);
                while (Console.ReadKey().KeyChar != 'q') { }
                serverWindowsService.ExecuteOnStopEvent();
            }
            else
            {
                ServiceBase.Run(serverWindowsService);
            }
        }
    }
}
