using System.Net.Security;
using System.ServiceModel;
using WsAncertCommunication.Helpers;
using WsAncertCommunication.Services.DispatcherV2Signed.Exceptions;
using WsAncertCommunication.Services.DispatcherV2Signed.Models;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Interfaces
{
    [ServiceContract(Namespace = WebServiceData.Namespace, ProtectionLevel = ProtectionLevel.Sign)]
    public interface IDispatcherV2SignedService
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        [FaultContract(typeof(DispatcherV2SignedException), Action = "", Name = "fault")]
        [XmlSerializerFormat(SupportFaults = true)]
        processResponse process(processRequest request);
    }
}
