using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Nodo opcional para expresar información de las horas extras
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class HorasExtra
    {

        private int _dias;
        private NominaHorasExtraTipoHoras _tipoHoras;
        private int _horasExtra;
        private decimal _importePagado;

        /// <summary>
        /// Número de días en que el trabajador realizó horas extra en el periodo
        /// </summary>
        [XmlAttribute]
        public int Dias
        {
            get
            {
                return _dias;
            }
            set
            {
                _dias = value;
            }
        }

        /// <summary>
        /// Tipo de pago de las horas extra: dobles o triples
        /// </summary>
        [XmlAttribute]
        public NominaHorasExtraTipoHoras TipoHoras
        {
            get
            {
                return _tipoHoras;
            }
            set
            {
                _tipoHoras = value;
            }
        }

        /// <summary>
        /// Número de horas extra trabajadas en el periodo
        /// </summary>
        [XmlAttribute("HorasExtra")]
        public int HorasExtras
        {
            get
            {
                return _horasExtra;
            }
            set
            {
                _horasExtra = value;
            }
        }

        /// <summary>
        /// Importe pagado por las horas extra
        /// </summary>
        [XmlAttribute]
        public decimal ImportePagado
        {
            get
            {
                return _importePagado;
            }
            set
            {
                _importePagado = value;
            }
        }

    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public enum NominaHorasExtraTipoHoras
    {
        Dobles,
        Triples,
    }


}
