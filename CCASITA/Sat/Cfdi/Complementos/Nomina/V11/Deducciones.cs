using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Nodo opcional para expresar las deducciones aplicables
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class Deducciones
    {

        private List<Deduccion> _deduccion;
        private decimal _totalGravado;
        private decimal _totalExento;

        /// <summary>
        /// Nodo para expresar la información detallada de una deducción
        /// </summary>
        [XmlElement("Deduccion", Order = 0)]
        public List<Deduccion> Deduccion
        {
            get
            {
                if ((_deduccion == null))
                {
                    _deduccion = new List<Deduccion>();
                }
                return _deduccion;
            }
            set
            {
                _deduccion = value;
            }
        }

        /// <summary>
        /// Atributo requerido para expresar el total de deducciones gravadas que se relacionan en el comprobante
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
        /// Atributo requerido para expresar el total de deducciones exentas que se relacionan en el comprobante
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
