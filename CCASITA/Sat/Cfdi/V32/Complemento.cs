using System;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

using Imploc10 = LaCasita.Sat.Cfdi.Complementos.ImpuestosLocales.V10.ImpuestosLocales;
using Nomina11 = LaCasita.Sat.Cfdi.Complementos.Nomina.V11.Nomina;
using Tifidi10 = LaCasita.Sat.Cfdi.Complementos.TimbreFiscalDigital.V10.TimbreFiscalDigital;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo opcional donde se incluirá el complemento Timbre Fiscal Digital de manera obligatoria y los nodos complementarios determinados por el SAT, de acuerdo a las disposiciones particulares a un sector o actividad específica.
    /// </summary>
    [Serializable] 
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Complemento
    {
        private List<XmlElement> _any;
        [XmlAnyElement(Order = 0)]
        public List<XmlElement> Any
        {
            get
            {
                if ((_any == null)) { _any = new List<XmlElement>(); }
                return _any;
            }
            set { _any = value; }
        }

        [XmlIgnore]
        public Tifidi10 TimbreFiscalDigital
        {
            get
            {
                foreach (var xmlElement in _any)
                {
                    switch (xmlElement.Name)
                    {
                        case "tfd:TimbreFiscalDigital":
                            return Tifidi10.Deserialize(xmlElement.OuterXml);
                    }
                }
                return null;
            }
        }

        [XmlIgnore]
        public Nomina11 Nomina
        {
            get
            {
                foreach (var xmlElement in _any)
                {
                    switch (xmlElement.Name)
                    {
                        case "nomina:Nomina":
                            return Nomina11.Deserialize(xmlElement.OuterXml);
                    }
                }
                return null;
            }
        }

        [XmlIgnore]
        public Imploc10 ImpuestosLocales
        {
            get
            {
                foreach (var xmlElement in _any)
                {
                    switch (xmlElement.Name)
                    {
                        case "implocal:ImpuestosLocales":
                            return Imploc10.Deserialize(xmlElement.OuterXml);
                    }
                }
                return null;
            }
        }

    }
}