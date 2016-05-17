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
            ServiceBase.Run(new ExtendableServerWindowsService());
        }
    }
}
