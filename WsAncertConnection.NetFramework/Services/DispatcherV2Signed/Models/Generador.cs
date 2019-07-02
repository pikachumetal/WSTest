using System;
using System.Xml.Serialization;
using WsAncertConnection.NetFramework.Constants;

namespace WsAncertConnection.NetFramework.Services.DispatcherV2Signed.Models
{
    [Serializable]
    [XmlType(TypeName = "GENERADORType", Namespace = WebServiceData.Namespace)]
    public class Generador
    {
        /// <remarks/>
        [XmlElement(ElementName="NOMBRE_PROVEEDOR", Order = 0)]
        public string NombreProveedor { get; set; }

        /// <remarks/>
        [XmlElement(ElementName="NOMBRE_APLICACION", Order = 1)]
        public string NombreAplicacion { get; set; }

        /// <remarks/>
        [XmlElement(ElementName="VERSION_APLICACION", Order = 2)]
        public string VersionAplicacion { get; set; }
    }
}
