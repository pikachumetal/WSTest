using System;
using System.Xml.Serialization;

namespace WsAncertCommunication.DispatcherV2Signed
{
    [Serializable]
    [XmlType(Namespace = "http://inti.notariado.org/XML")]
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
