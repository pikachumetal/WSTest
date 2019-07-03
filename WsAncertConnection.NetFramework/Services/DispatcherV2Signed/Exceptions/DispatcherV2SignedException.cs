using ConsoleApp1.Helpers;
using System;
using System.Xml.Serialization;
using WsAncertConnection.NetFramework.Constants;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Exceptions
{
    [Serializable]
    [XmlType(Namespace = WebServiceData.Namespace)]
    public class DispatcherV2SignedException
    {
        [XmlElement(ElementName = "info", IsNullable = true, Order = 0)]
        public string Information { get; set; }
    }
}
