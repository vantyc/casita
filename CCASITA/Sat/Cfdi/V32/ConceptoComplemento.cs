using System;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo opcional donde se incluirán los nodos complementarios de extensión al concepto, definidos por el SAT, de acuerdo a disposiciones particulares a un sector o actividad especifica.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class ConceptoComplemento
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
