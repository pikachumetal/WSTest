using System;
using System.Xml.Serialization;
using WsAncertCommunication.Helpers;

namespace WsAncertCommunication.Services.DispatcherV2Signed.Models
{
    [Serializable]
    [XmlType(Namespace = WebServiceData.Namespace)]
    public class GENERADORType
    {
        /// <remarks/>
        [XmlElement(Order = 0)]
        public string NOMBRE_PROVEEDOR { get; set; }

        /// <remarks/>
        [XmlElement(Order = 1)]
        public string NOMBRE_APLICACION { get; set; }

        /// <remarks/>
        [XmlElement(Order = 2)]
        public string VERSION_APLICACION { get; set; }
    }
}
