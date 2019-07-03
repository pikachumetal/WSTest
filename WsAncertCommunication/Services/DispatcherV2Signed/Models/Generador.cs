using System;
using System.Xml.Serialization;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [Serializable]
    [XmlType(TypeName = "GENERADORType", Namespace = WebServiceData.Namespace)]
    public class Generador
    {
        /// <remarks/>
        [XmlElement(ElementName = "NOMBRE_PROVEEDOR", Order = 0)]
        public string NOMBRE_PROVEEDOR { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "NOMBRE_APLICACION", Order = 1)]
        public string NOMBRE_APLICACION { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "VERSION_APLICACION", Order = 2)]
        public string VERSION_APLICACION { get; set; }
    }
}
