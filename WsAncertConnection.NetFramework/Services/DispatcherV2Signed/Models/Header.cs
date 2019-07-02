using System;
using System.Xml.Serialization;
using WsAncertConnection.NetFramework.Constants;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = WebServiceData.Namespace)]
    public class Header
    {
        /// <remarks/>
        [XmlElement(ElementName = "TIMESTAMP", Order = 0)]
        public DateTime Timestamp { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "TIPO_MSJ", Order = 1)]
        public sbyte TipoMensaje { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "EMISOR", Order = 2)]
        public string Emisor { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "RECEP", Order = 3)]
        public string Receptor { get; set; } = "CGN";

        /// <remarks/>
        [XmlElement(ElementName = "CUV", Order = 4)]
        public string Cuv { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "SERVICIO", Order = 5)]
        public string Servicio { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "GENERADOR", Order = 6)]
        public Generador Generador { get; set; }
    }
}
