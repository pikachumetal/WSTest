using System.Net.Security;
using System.ServiceModel;

namespace ConsoleApp1.DispatcherV2Signed
{
    [ServiceContract(Namespace = "http://inti.notariado.org/XML", ProtectionLevel = ProtectionLevel.Sign)]
    public interface IDispatcherV2SignedService
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        [FaultContract(typeof(DispatcherV2SignedException), Action = "", Name = "fault")]
        [XmlSerializerFormat(SupportFaults = true)]
        processResponse process(processRequest request);
    }
}
