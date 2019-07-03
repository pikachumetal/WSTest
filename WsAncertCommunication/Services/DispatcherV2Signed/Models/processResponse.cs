using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processResponse
    {
        [MessageBodyMember(Name = "SERVICE_DISPATCHER_RESPONSE", Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement ServiceDispatcherResponse;

        public processResponse() { }

        public processResponse(XmlElement serviceDispatcherResponse)
        {
            ServiceDispatcherResponse = serviceDispatcherResponse;
        }
    }
}
