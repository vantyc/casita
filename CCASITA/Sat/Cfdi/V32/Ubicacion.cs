using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Tipo definido para expresar domicilios o direcciones
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Ubicacion
    {
        private string _calle;
        private string _noExterior;
        private string _noInterior;
        private string _colonia;
        private string _localidad;
        private string _referencia;
        private string _municipio;
        private string _estado;
        private string _pais;
        private string _codigoPostal;
        /// <summary>
        /// Este atributo opcional sirve para precisar la avenida, calle, camino o carretera donde se da la ubicación.
        /// </summary>
        [XmlAttribute("calle")] public string Calle
        {
            get { return _calle; }
            set { _calle = value; }
        }
        /// <summary>
        /// Este atributo opcional sirve para expresar el número particular en donde se da la ubicación sobre una calle dada.
        /// </summary>
        [XmlAttribute("noExterior")] public string NoExterior
        {
            get { return _noExterior; }
            set { _noExterior = value; }
        }
        /// <summary>
        /// Este atributo opcional sirve para expresar información adicional para especificar la ubicación cuando calle y número exterior (noExterior) no resulten suficientes para determinar la ubicación de forma precisa.
        /// </summary>
        [XmlAttribute("noInterior")] public string NoInterior
        {
            get { return _noInterior; }
            set { _noInterior = value; }
        }
        /// <summary>
        /// Este atributo opcional sirve para precisar la colonia en donde se da la ubicación cuando se desea ser más específico en casos de ubicaciones urbanas.
        /// </summary>
        [XmlAttribute("colonia")] public string Colonia
        {
            get { return _colonia; }
            set { _colonia = value; }
        }
        /// <summary>
        /// Atributo opcional que sirve para precisar la ciudad o población donde se da la ubicación.
        /// </summary>
        [XmlAttribute("localidad")] public string Localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }
        /// <summary>
        /// Atributo opcional para expresar una referencia de ubicación adicional.
        /// </summary>
        [XmlAttribute("referencia")] public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }
        /// <summary>
        /// Atributo opcional que sirve para precisar el municipio o delegación (en el caso del Distrito Federal) en donde se da la ubicación.
        /// </summary>
        [XmlAttribute("municipio")] public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }
        /// <summary>
        /// Atributo opcional que sirve para precisar el estado o entidad federativa donde se da la ubicación.
        /// </summary>
        [XmlAttribute("estado")] public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        /// <summary>
        /// Atributo requerido que sirve para precisar el país donde se da la ubicación.
        /// </summary>
        [XmlAttribute("pais")] public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        /// <summary>
        /// Atributo opcional que sirve para asentar el código postal en donde se da la ubicación.
        /// </summary>
        [XmlAttribute("codigoPostal")] public string CodigoPostal
        {
            get { return _codigoPostal; }
            set { _codigoPostal = value; }
        }
    }
}
