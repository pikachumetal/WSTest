using System.ServiceModel;
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
            
            var bindings = new WSHttpBinding(SecurityMode.TransportWithMessageCredential);
            var endpoint = new EndpointAddress("https://test.e-notario.notariado.org/dispatcher-web/DispatcherV2Signed");
            var client = new DispatcherV2SignedClient(bindings, endpoint);

            var header = new SERVICE_DISPATCHER();
            var xDoc = new XmlDocument();
            var body = xDoc.CreateElement("TEST");

            var response = client.process(header, body);
        }
    }
}
