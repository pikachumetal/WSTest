using System;
using System.Xml.Serialization;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [Serializable]
    [XmlType(TypeName = "SERVICE_DISPATCHER", AnonymousType = true, Namespace = WebServiceData.Namespace)]
    public class ServiceDispatcher
    {
        /// <remarks/>
        [XmlElement(ElementName = "TIMESTAMP", Order = 0)]
        public DateTime TIMESTAMP { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "TIPO_MSJ", Order = 1)]
        public sbyte TIPO_MENSAJE { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "EMISOR", Order = 2)]
        public string EMISOR { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "RECEP", Order = 3)]
        public string RECEPTOR { get; set; } = "CGN";

        /// <remarks/>
        [XmlElement(ElementName = "CUV", Order = 4)]
        public string CUV { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "SERVICIO", Order = 5)]
        public string SERVICIO { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "GENERADOR", Order = 6)]
        public Generador GENERADOR { get; set; }
    }
}
