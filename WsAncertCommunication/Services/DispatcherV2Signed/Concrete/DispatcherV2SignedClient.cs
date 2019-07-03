using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using WsAncertCommunication.Services.DispatcherV2Signed.Interfaces;
using WsAncertCommunication.Services.DispatcherV2Signed.Models;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Concrete
{
    public class DispatcherV2SignedClient : ClientBase<IDispatcherV2SignedService>, IDispatcherV2SignedService
    {
        public DispatcherV2SignedClient(Binding binding, EndpointAddress remoteAddress)
              : base(binding, remoteAddress) { }

        public XmlElement Process(ServiceDispatcher header, XmlElement request)
        {
            var inValue = new ProcessRequest
            {
                Header = header,
                Request = request
            };
            var retVal = ((IDispatcherV2SignedService)(this)).Process(inValue);
            return retVal.Response;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        ProcessResponse IDispatcherV2SignedService.Process(ProcessRequest request)
        {
            return Channel.Process(request);
        }
    }
}
