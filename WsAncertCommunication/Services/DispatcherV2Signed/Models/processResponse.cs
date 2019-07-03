using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processResponse
    {
        [MessageBodyMember(Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement SERVICE_DISPATCHER_RESPONSE;

        public processResponse() { }

        public processResponse(XmlElement SERVICE_DISPATCHER_RESPONSE)
        {
            this.SERVICE_DISPATCHER_RESPONSE = SERVICE_DISPATCHER_RESPONSE;
        }
    }
}
