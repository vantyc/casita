using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Nodo para expresar la información detallada de una percepción
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class Percepcion
    {

        private int _tipoPercepcion;
        private string _clave;
        private string _concepto;
        private decimal _importeGravado;
        private decimal _importeExento;

        /// <summary>
        /// Clave agrupadora. Clasifica la percepción conforme al catálogo publicado en el portal del SAT en internet
        /// </summary>
        [XmlAttribute]
        public int TipoPercepcion
        {
            get
            {
                return _tipoPercepcion;
            }
            set
            {
                _tipoPercepcion = value;
            }
        }

        /// <summary>
        /// Atributo requerido, representa la clave de percepción de nómina propia de la contabilidad de cada patrón, puede conformarse desde 3 hasta 15 caracteres
        /// </summary>
        [XmlAttribute]
        public string Clave
        {
            get
            {
                return _clave;
            }
            set
            {
                _clave = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la descripción del concepto de percepción
        /// </summary>
        [XmlAttribute]
        public string Concepto
        {
            get
            {
                return _concepto;
            }
            set
            {
                _concepto = value;
            }
        }

        /// <summary>
        /// Atributo requerido, representa el importe gravado de un concepto de percepción
        /// </summary>
        [XmlAttribute]
        public decimal ImporteGravado
        {
            get
            {
                return _importeGravado;
            }
            set
            {
                _importeGravado = value;
            }
        }

        /// <summary>
        /// Atributo requerido, representa el importe exento de un concepto de percepción
        /// </summary>
        [XmlAttribute]
        public decimal ImporteExento
        {
            get
            {
                return _importeExento;
            }
            set
            {
                _importeExento = value;
            }
        }
    }
}
