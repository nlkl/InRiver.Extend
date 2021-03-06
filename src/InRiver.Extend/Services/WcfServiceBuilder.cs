﻿using inRiver.Server.Service;
using InRiver.Extend.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InRiver.Extend.Services
{
    public class WcfServiceBuilder<TContract, TService>
        where TService : TContract
    {
        private readonly string relativeUrl;

        public WcfServiceBuilder(string relativeUrl)
        {
            if (relativeUrl == null) throw new ArgumentNullException(nameof(relativeUrl));
            this.relativeUrl = relativeUrl;
        }

        public ServiceHost CreateAndOpenServiceHost()
        {
            var serviceHost = CreateServiceHost();
            serviceHost.Open();
            return serviceHost;
        }

        public ServiceHost CreateServiceHost()
        {
            var uri = new Uri(ServerUri, relativeUrl);
            var serviceHost = new ServiceHost(typeof(TService), new[] { uri });

            var binding = CreateBinding();
            var serviceEndpoint = serviceHost.AddServiceEndpoint(typeof(TContract), binding, string.Empty);
            serviceEndpoint.Behaviors.Add(new AuthenticationInspector());

            var metadataBehavior = CreateMetadataBehavior();
            serviceHost.Description.Behaviors.Add(metadataBehavior);

            return serviceHost;
        }

        private WSHttpBinding CreateBinding()
        {
            WSHttpBinding binding;

            if (IsSSL())
            {
                binding = new WSHttpBinding(SecurityMode.Transport);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            }
            else
            {
                binding = new WSHttpBinding(SecurityMode.None);
            }

            binding.SendTimeout = new TimeSpan(0, 10, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas = new XmlDictionaryReaderQuotas()
            {
                MaxDepth = int.MaxValue,
                MaxStringContentLength = int.MaxValue,
                MaxArrayLength = int.MaxValue,
                MaxBytesPerRead = int.MaxValue,
                MaxNameTableCharCount = int.MaxValue
            };

            return binding;
        }

        private ServiceMetadataBehavior CreateMetadataBehavior()
        {
            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            if (!IsSSL())
            {
                metadataBehavior.HttpGetEnabled = true;
            }
            return metadataBehavior;
        }

        private bool IsSSL() => ServerUri.Scheme == "https";
        private Uri ServerUri => new Uri(ConfigurationManager.AppSettings["ServerUrl"]);
    }
}
