using System;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo opcional para recibir las extensiones al presente formato que sean de utilidad al contribuyente. Para las reglas de uso del mismo, referirse al formato de origen.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Addenda
    {
        private List<XmlElement> _any;
        [XmlAnyElement(Order = 0)] public List<XmlElement> Any
        {
            get
            {
                if ((_any == null)) { _any = new List<XmlElement>(); }
                return _any;
            }
            set { _any = value; }
        }
    }
}
