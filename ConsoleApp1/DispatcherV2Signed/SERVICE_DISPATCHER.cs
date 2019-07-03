using System;
using System.Xml.Serialization;

namespace ConsoleApp1.DispatcherV2Signed
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://inti.notariado.org/XML")]
    public class SERVICE_DISPATCHER
    {
        /// <remarks/>
        [XmlElement(Order = 0)]
        public DateTime TIMESTAMP { get; set; }

        /// <remarks/>
        [XmlElement(Order = 1)]
        public sbyte TIPO_MSJ { get; set; }

        /// <remarks/>
        [XmlElement(Order = 2)]
        public string EMISOR { get; set; }

        /// <remarks/>
        [XmlElement(Order = 3)]
        public string RECEP { get; set; } = "CGN";

        /// <remarks/>
        [XmlElement(Order = 4)]
        public string CUV { get; set; }

        /// <remarks/>
        [XmlElement(Order = 5)]
        public string SERVICIO { get; set; }

        /// <remarks/>
        [XmlElement(Order = 6)]
        public GENERADORType GENERADOR { get; set; }
    }
}
