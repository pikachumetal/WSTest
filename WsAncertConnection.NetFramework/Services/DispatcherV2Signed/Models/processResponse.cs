using System.ServiceModel;
using System.Xml;
using WsAncertConnection.NetFramework.Constants;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models
{
    [MessageContract(IsWrapped = false)]
    public class processResponse
    {
        [MessageBodyMember(Namespace = WebServiceData.Namespace, Name = "SERVICE_DISPATCHER_RESPONSE", Order = 0)]
        public XmlElement Response;

        public processResponse() { }

        public processResponse(XmlElement response) { Response = response; }
    }
}
