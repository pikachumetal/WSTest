using System.ServiceModel;
using System.Xml;

namespace WsAncertCommunication.Services.DispatcherV2Signed
{
    [MessageContract(IsWrapped = false)]
    public class processResponse
    {
        [MessageBodyMember(Namespace = "http://inti.notariado.org/XML", Order = 0)]
        public XmlElement SERVICE_DISPATCHER_RESPONSE;

        public processResponse() { }

        public processResponse(XmlElement SERVICE_DISPATCHER_RESPONSE)
        {
            this.SERVICE_DISPATCHER_RESPONSE = SERVICE_DISPATCHER_RESPONSE;
        }
    }
}
