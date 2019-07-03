using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processRequest
    {
        [MessageHeader(Name = "SERVICE_DISPATCHER", Namespace = WebServiceData.Namespace)]
        public ServiceDispatcher ServiceDispatcher;

        [MessageBodyMember(Name = "SERVICE_DISPATCHER_REQUEST", Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement ServiceDispatcherRequest;

        public processRequest() { }

        public processRequest(ServiceDispatcher serviceDispatcher, XmlElement serviceDispatcherRequest)
        {
            ServiceDispatcher = serviceDispatcher;
            ServiceDispatcherRequest = serviceDispatcherRequest;
        }
    }
}
