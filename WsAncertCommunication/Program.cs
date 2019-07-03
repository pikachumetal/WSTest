using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Xml;
using WsAncertCommunication.Bindings.CustomTextMessage;
using WsAncertCommunication.Helpers;
using WsAncertCommunication.Services.DispatcherV2Signed;
using WsAncertCommunication.Services.DispatcherV2Signed.Concrete;
using WsAncertCommunication.Services.DispatcherV2Signed.Models;

namespace WsAncertCommunication
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Creating the WS-client ...");
            var client = GetService(GetEndpoint());

            Console.WriteLine("Invoke the WS-client ...");
            var response = client.process(GetHeader(), GetBodyRequest());

            Console.WriteLine($"WS-Response: {response.OuterXml}");

            Console.WriteLine("Press any key to close ...");
            Console.ReadLine();
        }

        private static EndpointAddress GetEndpoint()
        {
            var identity = EndpointIdentity.CreateDnsIdentity(WebServiceData.DnsIdentity);
            var address = new EndpointAddress(new Uri(WebServiceData.Soap11Endpoint), identity);
            return address;
        }

        private static DispatcherV2SignedClient GetService(EndpointAddress address)
        {
            var client = new DispatcherV2SignedClient(GetCustomBinding(), address);

            if (client.ClientCredentials == null) throw new ArgumentNullException(nameof(client));

            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = GetServerCertificate();
            client.ClientCredentials.ClientCertificate.Certificate = GetClientCertificate();

            client.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.Sign;

            return client;
        }

        private static X509Certificate2 GetClientCertificate()
        {
            return new X509Certificate2(CertificateData.ClientCertificatePath, CertificateData.ClientCertificatePassword);
        }

        private static X509Certificate2 GetServerCertificate()
        {
            return new X509Certificate2(CertificateData.ServiceCertificatePath);
        }

        private static XmlElement GetBodyRequest()
        {
            var xDoc = new XmlDocument();
            var body = xDoc.CreateElement("TEST");
            return body;
        }


        private static SERVICE_DISPATCHER GetHeader()
        {
            return new SERVICE_DISPATCHER()
            {
                TIPO_MSJ = (int)WsTipoMensaje.Request,
                EMISOR = WebServiceData.EmisorPruebas,
                RECEP = WebServiceData.Receptor,
                SERVICIO = WebServiceAction.Prueba,
                TIMESTAMP = DateTime.Now
            };
        }

        private static CustomBinding GetCustomBinding()
        {
            var binding = new CustomBinding();

            binding.Elements.Add(GetSecurityBinding());
            binding.Elements.Add(GetMessageBinding());
            binding.Elements.Add(GetTransportBinding());

            return binding;
        }

        private static AsymmetricSecurityBindingElement GetSecurityBinding()
        {
            var sec = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10);

            sec.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
            sec.IncludeTimestamp = true;
            sec.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
            sec.AllowSerializedSigningTokenOnReply = true;

            sec.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;
            sec.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            sec.EnableUnsecuredResponse = true;

            return sec;
        }

        private static CustomTextMessageBindingElement GetMessageBinding()
        {
            return new CustomTextMessageBindingElement
            {
                MessageVersion = MessageVersion.Soap11,
                MediaType = "text/xml"
            };
        }

        private static HttpsTransportBindingElement GetTransportBinding()
        {
            return new HttpsTransportBindingElement
            {
                AllowCookies = false,
                BypassProxyOnLocal = false,
                HostNameComparisonMode = HostNameComparisonMode.WeakWildcard,
                MaxBufferPoolSize = int.MaxValue - 1,
                MaxBufferSize = int.MaxValue - 1,
                MaxReceivedMessageSize = int.MaxValue - 1,
                RequireClientCertificate = true,
                UseDefaultWebProxy = true
            };
        }
    }
}
