using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo para introducir la información detallada de un bien o servicio amparado en el comprobante.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Concepto
    {
        private List<object> _items;
        private decimal _cantidad;
        private string _unidad;
        private string _noIdentificacion;
        private string _descripcion;
        private decimal _valorUnitario;
        private decimal _importe;
        [XmlElement("ComplementoConcepto", typeof(ConceptoComplemento), Order = 0)]
        [XmlElement("CuentaPredial", typeof(CuentaPredial), Order = 0)]
        [XmlElement("InformacionAduanera", typeof(InformacionAduanera), Order = 0)]
        [XmlElement("Parte", typeof(Parte), Order = 0)]
        public List<object> Items
        {
            get
            {
                if ((_items == null)) { _items = new List<object>(); }
                return _items;
            }
            set { _items = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la cantidad de bienes o servicios del tipo particular definido por el presente concepto.
        /// </summary>
        [XmlAttribute("cantidad")] public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la unidad de medida aplicable para la cantidad expresada en el concepto.
        /// </summary>
        [XmlAttribute("unidad")] public string Unidad
        {
            get { return _unidad; }
            set { _unidad = value; }
        }
        /// <summary>
        /// Atributo opcional para expresar el número de serie del bien o identificador del servicio amparado por el presente concepto.
        /// </summary>
        [XmlAttribute("noIdentificacion")] public string NoIdentificacion
        {
            get { return _noIdentificacion; }
            set { _noIdentificacion = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la descripción del bien o servicio cubierto por el presente concepto.
        /// </summary>
        [XmlAttribute("descripcion")] public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar el valor o precio unitario del bien o servicio cubierto por el presente concepto.
        /// </summary>
        [XmlAttribute("valorUnitario")] public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set { _valorUnitario = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar el importe total de los bienes o servicios del presente concepto. Debe ser equivalente al resultado de multiplicar la cantidad por el valor unitario expresado en el concepto.
        /// </summary>
        [XmlAttribute("importe")] public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
    }
}
