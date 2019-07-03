using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processRequest
    {
        [MessageHeader(Namespace = WebServiceData.Namespace)]
        public ServiceDispatcher SERVICE_DISPATCHER;

        [MessageBodyMember(Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement SERVICE_DISPATCHER_REQUEST;

        public processRequest() { }

        public processRequest(ServiceDispatcher SERVICE_DISPATCHER, XmlElement SERVICE_DISPATCHER_REQUEST)
        {
            this.SERVICE_DISPATCHER = SERVICE_DISPATCHER;
            this.SERVICE_DISPATCHER_REQUEST = SERVICE_DISPATCHER_REQUEST;
        }
    }
}
