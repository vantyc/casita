using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo para la información detallada de una retención de impuesto específico
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Retencion
    {
        private RetencionImpuesto _impuesto;
        private decimal _importe;
        /// <summary>
        /// Atributo requerido para señalar el tipo de impuesto retenido
        /// </summary>
        [XmlAttribute("impuesto")] public RetencionImpuesto Impuesto
        {
            get { return _impuesto; }
            set { _impuesto = value; }
        }
        /// <summary>
        /// Atributo requerido para señalar el importe o monto del impuesto retenido
        /// </summary>
        [XmlAttribute("importe")] public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
    }
}
