using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Xml;
using WsAncertConnection.NetFramework.Bindings.CustomTextMessage;
using WsAncertConnection.NetFramework.Constants;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Concrete;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models;

namespace WsAncertConnection.NetFramework
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine($"Creating the WS-client ...");
            var client = GetService(GetEndpoint());

            Console.WriteLine($"Invoke the WS-client ...");
            var response = client.SendMessage(GetHeader(), GetBodyRequest());

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

            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                X509CertificateValidationMode.None;
            client.ClientCredentials.ServiceCertificate.DefaultCertificate =
                new X509Certificate2(CertificateData.ServiceCertificatePath);
            client.ClientCredentials.ClientCertificate.Certificate =
                new X509Certificate2(CertificateData.ClientCertificatePath, CertificateData.ClientCertificatePassword);

            client.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.Sign;
            return client;
        }

        private static XmlElement GetBodyRequest()
        {
            var xDoc = new XmlDocument();
            var body = xDoc.CreateElement("TEST");
            return body;
        }


        private static Header GetHeader()
        {
            return new Header()
            {
                TipoMensaje = 1,
                Emisor = "test-entidad",
                Receptor = "CGN",
                Servicio = "TEST00002",
                Timestamp = DateTime.Now
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
            var bindingElement = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement(MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10);

            bindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
            bindingElement.IncludeTimestamp = true;
            bindingElement.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
            bindingElement.AllowSerializedSigningTokenOnReply = true;

            bindingElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;
            bindingElement.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            bindingElement.EnableUnsecuredResponse = true;

            return bindingElement;
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
