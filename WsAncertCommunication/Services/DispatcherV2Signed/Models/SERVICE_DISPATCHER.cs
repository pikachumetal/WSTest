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
