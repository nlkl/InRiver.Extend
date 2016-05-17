using inRiver.Common.Log;
using inRiver.Remoting.Log;
using inRiver.Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Logging
{
    public static class Log
    {
        public static void Verbose(string message) =>
            Logger.Instance.Write(LogLevel.Verbose, message, CurrentUser, CurrentSessionId);

        public static void Debug(string message) =>
            Logger.Instance.Write(LogLevel.Debug, message, CurrentUser, CurrentSessionId);

        public static void Info(string message) =>
            Logger.Instance.Write(LogLevel.Information, message, CurrentUser, CurrentSessionId);

        public static void Warning(string message) =>
            Logger.Instance.Write(LogLevel.Warning, message, CurrentUser, CurrentSessionId);

        public static void Error(Exception ex, string message) =>
            Logger.Instance.Write(LogLevel.Error, message, ex, CurrentUser, CurrentSessionId);

        private static string CurrentUser => inRiverContext.Default.CurrentUser;

        private static string CurrentSessionId
        {
            get
            {
                if (!inRiverOperationContext.Current.Items.ContainsKey("CurrentSessionId"))
                {
                    return string.Empty;
                }

                return inRiverOperationContext.Current.Items["CurrentSessionId"].ToString();
            }
        }
    }
}
