using System.ServiceModel;
using System.Xml;
using WsAncertConnection.NetFramework.Constants;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processRequest
    {
        [MessageHeader(Namespace = WebServiceData.Namespace, Name = "SERVICE_DISPATCHER")]
        public Header Header;

        [MessageBodyMember(Namespace = WebServiceData.Namespace, Name = "SERVICE_DISPATCHER_REQUEST", Order = 0)]
        public XmlElement Request;

        public processRequest() { }

        public processRequest(Header header, XmlElement request)
        {
            Header = header;
            Request = request;
        }
    }
}
