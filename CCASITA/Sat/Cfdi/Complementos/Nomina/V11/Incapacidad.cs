using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{

    /// <summary>
    /// Nodo opcional para expresar información de las incapacidades
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class Incapacidad
    {

        private decimal _diasIncapacidad;

        private int _tipoIncapacidad;

        private decimal _descuento;

        /// <summary>
        /// Número de días que el trabajador se incapacitó en el periodo
        /// </summary>
        [XmlAttribute]
        public decimal DiasIncapacidad
        {
            get
            {
                return _diasIncapacidad;
            }
            set
            {
                _diasIncapacidad = value;
            }
        }

        /// <summary>
        /// Razón de la incapacidad: Catálogo publicado en el portal del SAT en internet
        /// </summary>
        [XmlAttribute]
        public int TipoIncapacidad
        {
            get
            {
                return _tipoIncapacidad;
            }
            set
            {
                _tipoIncapacidad = value;
            }
        }

        /// <summary>
        /// Monto del descuento por la incapacidad
        /// </summary>
        [XmlAttribute]
        public decimal Descuento
        {
            get
            {
                return _descuento;
            }
            set
            {
                _descuento = value;
            }
        }
    }
}
