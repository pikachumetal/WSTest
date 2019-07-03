using ConsoleApp1.Helpers;
using System;
using System.Xml.Serialization;

namespace ConsoleApp1.DispatcherV2Signed
{
    [Serializable]
    [XmlType(Namespace = WebServiceData.Namespace)]
    public class DispatcherV2SignedException
    {
        [XmlElement(IsNullable = true, Order = 0)]
        public string info { get; set; }
    }
}
