using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo requerido para incorporar los regímenes en los que tributa el contribuyente emisor. Puede contener más de un régimen.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class RegimenFiscal
    {
        private string _regimen;
        /// <summary>
        /// Atributo requerido para incorporar el nombre del régimen en el que tributa el contribuyente emisor.
        /// </summary>
        [XmlAttribute] public string Regimen
        {
            get { return _regimen; }
            set { _regimen = value; }
        }
    }
}