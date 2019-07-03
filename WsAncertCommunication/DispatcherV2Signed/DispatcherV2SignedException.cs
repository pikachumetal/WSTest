using System;
using System.Xml.Serialization;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.DispatcherV2Signed
{
    [Serializable]
    [XmlType(Namespace = WebServiceData.Namespace)]
    public class DispatcherV2SignedException
    {
        [XmlElement(IsNullable = true, Order = 0)]
        public string info { get; set; }
    }
}
