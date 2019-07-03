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
        public DispatcherV2SignedClient() { }

        public DispatcherV2SignedClient(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public DispatcherV2SignedClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress) { }

        public DispatcherV2SignedClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress) { }

        public DispatcherV2SignedClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { }
        
        public XmlElement process(SERVICE_DISPATCHER SERVICE_DISPATCHER, XmlElement SERVICE_DISPATCHER_REQUEST)
        {
            var inValue = new processRequest
            {
                SERVICE_DISPATCHER = SERVICE_DISPATCHER, 
                SERVICE_DISPATCHER_REQUEST = SERVICE_DISPATCHER_REQUEST
            };
            var retVal = ((IDispatcherV2SignedService)(this)).process(inValue);
            return retVal.SERVICE_DISPATCHER_RESPONSE;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        processResponse IDispatcherV2SignedService.process(processRequest request)
        {
            return Channel.process(request);
        }
    }
}
