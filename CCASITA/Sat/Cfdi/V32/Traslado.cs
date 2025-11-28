using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo para la información detallada de un traslado de impuesto específico
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Traslado
    {
        private TrasladoImpuesto _impuesto;
        private decimal _tasa;
        private decimal _importe;
        /// <summary>
        /// Atributo requerido para señalar el tipo de impuesto trasladado
        /// </summary>
        [XmlAttribute("impuesto")] public TrasladoImpuesto Impuesto
        {
            get { return _impuesto; }
            set { _impuesto = value; }
        }
        /// <summary>
        /// Atributo requerido para señalar la tasa del impuesto que se traslada por cada concepto amparado en el comprobante
        /// </summary>
        [XmlAttribute("tasa")] public decimal Tasa
        {
            get { return _tasa; }
            set { _tasa = value; }
        }
        /// <summary>
        /// Atributo requerido para señalar el importe del impuesto trasladado
        /// </summary>
        [XmlAttribute("importe")] public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
    }
}


