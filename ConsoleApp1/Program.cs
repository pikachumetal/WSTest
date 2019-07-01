using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;
using ConsoleApp1.DispatcherV2Signed;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var host = new ServiceHost(typeof(ServiceDispatcher));

            //X509Certificate2 cert = new X509Certificate2();
            //cert.Import("soapui_CGN.jks", "<the password>", X509KeyStorageFlags.DefaultKeySet);

            //var certAuth = host.Credentials.ClientCertificate.Authentication;

            //certAuth.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            //certAuth.IncludeWindowsGroups = true;
            //certAuth.MapClientCertificateToWindowsAccount = false;
            //certAuth.RevocationMode = X509RevocationMode.Online;
            //certAuth.TrustedStoreLocation = StoreLocation.LocalMachine;

            var endpoint = new EndpointAddress("https://test.e-notario.notariado.org/dispatcher-web/DispatcherV2Signed");
            var client = new DispatcherV2SignedClient(GetCustomBinding(), endpoint);

            if(client == null) throw new ArgumentNullException(nameof(client));
            client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(@"cert.cer");
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = new X509Certificate2(@"cert.cer");

            var header = new SERVICE_DISPATCHER();
            var xDoc = new XmlDocument();
            var body = xDoc.CreateElement("TEST");

            var response = client.process(header, body);
        }

        private static Binding GetCustomBinding()
        {
            var httpsBindingElement = new HttpsTransportBindingElement
            {
                AllowCookies = false,
                BypassProxyOnLocal = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                MaxBufferPoolSize = 524288,
                MaxBufferSize = 65536,
                MaxReceivedMessageSize = 65536,
                RequireClientCertificate = true,
                UseDefaultWebProxy = true
            };

            var asymmetricSecurityBindingElement = new AsymmetricSecurityBindingElement
            {
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
                MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10
            };

            asymmetricSecurityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted.Add(new X509SecurityTokenParameters());
            asymmetricSecurityBindingElement.EndpointSupportingTokenParameters.SetKeyDerivation(false);

            var myBinding = new CustomBinding();

            myBinding.Elements.Add(asymmetricSecurityBindingElement);
            myBinding.Elements.Add(new TextMessageEncodingBindingElement(MessageVersion.Soap11, System.Text.Encoding.UTF8));
            myBinding.Elements.Add(httpsBindingElement);

            return myBinding;
        }

    }
}
