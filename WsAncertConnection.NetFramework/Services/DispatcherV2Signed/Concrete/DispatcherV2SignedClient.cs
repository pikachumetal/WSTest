﻿using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Interfaces;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Concrete
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
        
        public XmlElement SendMessage(Header header, XmlElement request)
        {
            var inValue = new processRequest
            {
                Header = header, 
                Request = request
            };
            var retVal = ((IDispatcherV2SignedService)(this)).SendMessage(inValue);
            return retVal.Response;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        processResponse IDispatcherV2SignedService.SendMessage(processRequest request) => Channel.SendMessage(request);
    }
}