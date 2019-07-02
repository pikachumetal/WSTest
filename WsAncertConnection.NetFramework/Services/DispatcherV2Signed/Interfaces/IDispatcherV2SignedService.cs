using System.Net.Security;
using System.ServiceModel;
using WsAncertConnection.NetFramework.Constants;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Exceptions;
using WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Interfaces
{
    [ServiceContract(Namespace = WebServiceData.Namespace, ProtectionLevel = ProtectionLevel.Sign)]
    public interface IDispatcherV2SignedService
    {
        [OperationContract(Action = "process", ReplyAction = "*")]
        [FaultContract(typeof(DispatcherV2SignedException), Action = "", Name = "fault")]
        [XmlSerializerFormat(SupportFaults = true)]
        processResponse SendMessage(processRequest request);
    }
}
