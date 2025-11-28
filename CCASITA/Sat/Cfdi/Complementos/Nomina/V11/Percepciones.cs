using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Nodo opcional para expresar las percepciones aplicables
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class Percepciones
    {

        private List<Percepcion> _percepcion;
        private decimal _totalGravado;
        private decimal _totalExento;

        /// <summary>
        /// Nodo para expresar la información detallada de una percepción
        /// </summary>
        [XmlElement("Percepcion", Order = 0)]
        public List<Percepcion> Percepcion
        {
            get
            {
                if ((_percepcion == null))
                {
                    _percepcion = new List<Percepcion>();
                }
                return _percepcion;
            }
            set
            {
                _percepcion = value;
            }
        }

        /// <summary>
        /// Atributo requerido para expresar el total de percepciones gravadas que se relacionan en el comprobante
        /// </summary>
        [XmlAttribute]
        public decimal TotalGravado
        {
            get
            {
                return _totalGravado;
            }
            set
            {
                _totalGravado = value;
            }
        }

        /// <summary>
        /// Atributo requerido para expresar el total de percepciones exentas que se relacionan en el comprobante
        /// </summary>
        [XmlAttribute]
        public decimal TotalExento
        {
            get
            {
                return _totalExento;
            }
            set
            {
                _totalExento = value;
            }
        }
    }
}
