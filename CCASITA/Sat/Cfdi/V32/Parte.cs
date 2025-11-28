using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo opcional para expresar las partes o componentes que integran la totalidad del concepto expresado en el comprobante fiscal digital a través de Internet
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Parte
    {
        private List<InformacionAduanera> _informacionAduanera;
        private decimal _cantidad;
        private string _unidad;
        private string _noIdentificacion;
        private string _descripcion;
        private decimal? _valorUnitario;
        private decimal? _importe;
        /// <summary>
        /// Nodo opcional para introducir la información aduanera aplicable cuando se trate de partes o componentes importados vendidos de primera mano.
        /// </summary>
        [XmlElement("InformacionAduanera", Order = 0)] public List<InformacionAduanera> InformacionAduanera
        {
            get
            {
                if ((_informacionAduanera == null)) { _informacionAduanera = new List<InformacionAduanera>(); }
                return _informacionAduanera;
            }
            set { _informacionAduanera = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la cantidad de bienes o servicios del tipo particular definido por la presente parte.
        /// </summary>
        [XmlAttribute("cantidad")] public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        /// <summary>
        /// Atributo opcional para precisar la unidad de medida aplicable para la cantidad expresada en la parte.
        /// </summary>
        [XmlAttribute("unidad")] public string Unidad
        {
            get { return _unidad; }
            set { _unidad = value; }
        }
        /// <summary>
        /// Atributo opcional para expresar el número de serie del bien o identificador del servicio amparado por la presente parte.
        /// </summary>
        [XmlAttribute("noIdentificacion")] public string NoIdentificacion
        {
            get { return _noIdentificacion; }
            set { _noIdentificacion = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la descripción del bien o servicio cubierto por la presente parte.
        /// </summary>
        [XmlAttribute("descripcion")] public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        /// <summary>
        /// Atributo opcional para precisar el valor o precio unitario del bien o servicio cubierto por la presente parte.
        /// </summary>
        [XmlAttribute("valorUnitario")] public decimal ValorUnitario
        {
            get { return _valorUnitario.HasValue ? _valorUnitario.Value : default(decimal); }
            set { _valorUnitario = value; }
        }
        /// <summary>
        /// Determina si se ha especificado el valor unitario
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore] public bool ValorUnitarioSpecified 
        {
            get { return _valorUnitario.HasValue; }
            set { if (value == false) { _valorUnitario = null; } }
        }
        /// <summary>
        /// Atributo opcional para precisar el importe total de los bienes o servicios de la presente parte. Debe ser equivalente al resultado de multiplicar la cantidad por el valor unitario expresado en la parte.
        /// </summary>
        [XmlAttribute("importe")] public decimal Importe
        {
            get { return _importe.HasValue ? _importe.Value : default(decimal); }
            set { _importe = value; }
        }
        /// <summary>
        /// Determina si se ha especificado el importe
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore] public bool ImporteSpecified
        {
            get { return _importe.HasValue; }
            set { if (value == false) { _importe = null; } }
        }
    }
}
