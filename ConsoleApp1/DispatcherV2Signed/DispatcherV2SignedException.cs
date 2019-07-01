using System;
using System.Xml.Serialization;

namespace ConsoleApp1.DispatcherV2Signed
{
    [Serializable]
    [XmlType(Namespace = "http://inti.notariado.org/XML")]
    public class DispatcherV2SignedException
    {
        [XmlElement(IsNullable = true, Order = 0)]
        public string info { get; set; }
    }
}
