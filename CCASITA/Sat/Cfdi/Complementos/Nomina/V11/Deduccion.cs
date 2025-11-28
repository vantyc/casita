using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Nodo para expresar la información detallada de una deducción
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    public class Deduccion
    {
        private int _tipoDeduccion;
        private string _clave;
        private string _concepto;
        private decimal _importeGravado;
        private decimal _importeExento;

        /// <summary>
        /// Clave agrupadora. Clasifica la deducción conforme al catálogo publicado en el portal del SAT en internet
        /// </summary>
        [XmlAttribute]
        public int TipoDeduccion
        {
            get
            {
                return _tipoDeduccion;
            }
            set
            {
                _tipoDeduccion = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la clave de deducción de nómina propia de la contabilidad de cada patrón, puede conformarse desde 3 hasta 15 caracteres
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
        /// Atributo requerido para la descripción del concepto de deducción
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
        /// Atributo requerido, representa el importe gravado de un concepto de deducción
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
        /// Atributo requerido, representa el importe exento de un concepto de deducción
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
