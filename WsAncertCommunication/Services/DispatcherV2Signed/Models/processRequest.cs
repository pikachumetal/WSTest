using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class ProcessRequest
    {
        [MessageHeader(Name = "SERVICE_DISPATCHER", Namespace = WebServiceData.Namespace)]
        public ServiceDispatcher Header;

        [MessageBodyMember(Name = "SERVICE_DISPATCHER_REQUEST", Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement Request;

        public ProcessRequest() { }

        public ProcessRequest(ServiceDispatcher header, XmlElement request)
        {
            Header = header;
            Request = request;
        }
    }
}
