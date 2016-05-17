using InRiver.Extend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Sample
{
    public class CustomServiceStartupExtension : IStartupExtension
    {
        private ServiceHost serviceHost;

        public void OnServerStart()
        {
            // Creates a custom InRiver service.
            var serviceBuilder = new WcfServiceBuilder<ICustomService, CustomService>("CustomService/");
            serviceHost = serviceBuilder.CreateAndOpenServiceHost();

            // In order to authenticate a WCF client, authenticate using the remote manager,
            // and add the InRiver AuthenticationBehavior to the client.
            // Example:
            // RemoteManager.CreateInstance("http://my-inriver-instance:8080", "username", "password");
            // var client = new CustomServiceClient();
            // client.Endpoint.EndpointBehaviors.Add(new AutenticationBehavior());
        }

        public void OnServerStop()
        {
            serviceHost?.Close();
            serviceHost = null;
        }
    }
}
