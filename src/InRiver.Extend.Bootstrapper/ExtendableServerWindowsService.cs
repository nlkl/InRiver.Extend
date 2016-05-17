using inRiver.Server;
using InRiver.Extend.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Bootstrapper
{
    public class ExtendableServerWindowsService : ServerWindowsService
    {
        private List<IStartupExtension> startupExtensions = new List<IStartupExtension>();

        public void ExecuteOnStartEvent(string[] args) => OnStart(args);
        public void ExecuteOnStopEvent() => OnStop();

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            startupExtensions = LoadStartupExtensions();
            foreach (var extension in startupExtensions)
            {
                try
                {
                    Log.Info($"Starting startup extension: {extension.GetType()}");
                    extension.OnServerStart();
                    Log.Info("Startup extensions was successfully started.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured when trying to start startup extension.");
                }
            }
        }

        protected override void OnStop()
        {
            foreach (var extension in startupExtensions)
            {
                try
                {
                    Log.Info($"Stopping startup extension: {extension.GetType()}");
                    extension.OnServerStop();
                    Log.Info("Startup extensions was successfully stopped.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured when trying to stop startup extension.");
                }
            }

            base.OnStop();
        }

        private List<IStartupExtension> LoadStartupExtensions()
        {
            try
            {
                Log.Info("Loading startup extensions.");

                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var path = Path.Combine(directoryName, "Extensions");

                var extensions = Directory
                    .GetFiles(path, "*.dll")
                    .Select(Assembly.LoadFrom)
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.GetInterfaces().Contains(typeof(IStartupExtension)))
                    .Select(type => (IStartupExtension)Activator.CreateInstance(type))
                    .ToList();

                Log.Info("Finished loading startup extensions.");

                return extensions;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured when loading startup extensions.");
                return new List<IStartupExtension>();
            }
        }
    }
}
