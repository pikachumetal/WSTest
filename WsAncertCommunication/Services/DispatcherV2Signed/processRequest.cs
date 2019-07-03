using System.ServiceModel;
using System.Xml;

namespace WsAncertCommunication.Services.DispatcherV2Signed
{
    [MessageContract(IsWrapped = false)]
    public class processRequest
    {
        [MessageHeader(Namespace = "http://inti.notariado.org/XML")]
        public SERVICE_DISPATCHER SERVICE_DISPATCHER;

        [MessageBodyMember(Namespace = "http://inti.notariado.org/XML", Order = 0)]
        public XmlElement SERVICE_DISPATCHER_REQUEST;

        public processRequest() { }

        public processRequest(SERVICE_DISPATCHER SERVICE_DISPATCHER, XmlElement SERVICE_DISPATCHER_REQUEST)
        {
            this.SERVICE_DISPATCHER = SERVICE_DISPATCHER;
            this.SERVICE_DISPATCHER_REQUEST = SERVICE_DISPATCHER_REQUEST;
        }
    }
}
