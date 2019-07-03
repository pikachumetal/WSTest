using System.ServiceModel;
using System.Xml;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class ProcessResponse
    {
        [MessageBodyMember(Name = "SERVICE_DISPATCHER_RESPONSE", Namespace = WebServiceData.Namespace, Order = 0)]
        public XmlElement Response;

        public ProcessResponse() { }

        public ProcessResponse(XmlElement response)
        {
            Response = response;
        }
    }
}
