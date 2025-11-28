using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo requerido para precisar la información del contribuyente receptor del comprobante.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Receptor
    {
        private Ubicacion _domicilio;
        private string _rfc;
        private string _nombre;
        /// <summary>
        /// Nodo opcional para la definición de la ubicación donde se da el domicilio del receptor del comprobante fiscal.
        /// </summary>
        [XmlElement(Order = 0)] public Ubicacion Domicilio
        {
            get
            {
                //if ((_domicilio == null)) { _domicilio = new Ubicacion(); }
                return _domicilio;
            }
            set { _domicilio = value; }
        }
        /// <summary>
        /// Atributo requerido para precisar la Clave del Registro Federal de Contribuyentes correspondiente al contribuyente receptor del comprobante.
        /// </summary>
        [XmlAttribute("rfc")] public string Rfc
        {
            get { return _rfc; }
            set { _rfc = value; }
        }
        /// <summary>
        /// Atributo opcional para el nombre, denominación o razón social del contribuyente receptor del comprobante.
        /// </summary>
        [XmlAttribute("nombre")] public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
