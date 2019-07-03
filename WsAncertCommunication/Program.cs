using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;
using WsAncertCommunication.Bindings.CustomTextMessage;
using WsAncertCommunication.DispatcherV2Signed;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Creating the WS-client ...");
            var client = GetService(GetEndpoint());

            Console.WriteLine($"Invoke the WS-client ...");
            var response = client.process(GetHeader(), GetBodyRequest());

            Console.WriteLine($"WS-Response: {response.OuterXml}");

            Console.WriteLine($"Press any key to close ...");
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
            var client = new DispatcherV2SignedClient(getCustomBinding(), address);
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
                TIPO_MSJ = 1,
                EMISOR = "test-entidad",
                RECEP = "CGN",
                SERVICIO = "TEST00002",
                TIMESTAMP = DateTime.Now
            };
        }

        private static CustomBinding getCustomBinding()
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


        private static Binding GetCustomBinding()
        {
            var httpsBindingElement = new HttpsTransportBindingElement
            {
                AllowCookies = false,
                BypassProxyOnLocal = false,
                HostNameComparisonMode = HostNameComparisonMode.WeakWildcard,
                MaxBufferPoolSize = 524288,
                MaxBufferSize = 65536,
                MaxReceivedMessageSize = 65536,
                RequireClientCertificate = true,
                UseDefaultWebProxy = true
            };

            var asymmetricSecurityBindingElement = new AsymmetricSecurityBindingElement
            {
                AllowSerializedSigningTokenOnReply = true,

                InitiatorTokenParameters = new X509SecurityTokenParameters()
                {
                    X509ReferenceStyle = X509KeyIdentifierClauseType.Any,
                    InclusionMode = SecurityTokenInclusionMode.AlwaysToInitiator
                },
                RecipientTokenParameters = new X509SecurityTokenParameters()
                {
                    X509ReferenceStyle = X509KeyIdentifierClauseType.Any,
                    InclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient
                },
                DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15,
                SecurityHeaderLayout = SecurityHeaderLayout.Lax,
                IncludeTimestamp = true,
                EnableUnsecuredResponse = true,
                MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10

            };

            asymmetricSecurityBindingElement.EndpointSupportingTokenParameters.Signed.Add(new X509SecurityTokenParameters());
            asymmetricSecurityBindingElement.EndpointSupportingTokenParameters.SetKeyDerivation(false);

            var myBinding = new CustomBinding();

            myBinding.Elements.Add(asymmetricSecurityBindingElement);
            myBinding.Elements.Add(new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8));
            myBinding.Elements.Add(httpsBindingElement);

            return myBinding;
        }
    }
}
