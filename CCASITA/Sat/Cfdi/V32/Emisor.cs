using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo requerido para expresar la información del contribuyente emisor del comprobante.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Emisor
    {
        private UbicacionFiscal _domicilioFiscal;
        private Ubicacion _expedidoEn;
        private List<RegimenFiscal> _regimenFiscal;
        private string _rfc;
        private string _nombre;
        /// <summary>
        /// Nodo opcional para precisar la información de ubicación del domicilio fiscal del contribuyente emisor
        /// </summary>
        [XmlElement(Order = 0)] public UbicacionFiscal DomicilioFiscal
        {
            get
            {
                //if ((_domicilioFiscal == null)) { _domicilioFiscal = new UbicacionFiscal(); }
                return _domicilioFiscal;
            }
            set { _domicilioFiscal = value; }
        }
        /// <summary>
        /// Nodo opcional para precisar la información de ubicación del domicilio en donde es emitido el comprobante fiscal en caso de que sea distinto del domicilio fiscal del contribuyente emisor.
        /// </summary>
        [XmlElement(Order = 1)] public Ubicacion ExpedidoEn
        {
            get
            {
                //if ((_expedidoEn == null)) { _expedidoEn = new Ubicacion(); }
                return _expedidoEn;
            }
            set { _expedidoEn = value; }
        }
        /// <summary>
        /// Nodo requerido para incorporar los regímenes en los que tributa el contribuyente emisor. Puede contener más de un régimen.
        /// </summary>
        [XmlElement("RegimenFiscal", Order = 2)]
        public List<RegimenFiscal> RegimenFiscal
        {
            get
            {
                if ((_regimenFiscal == null)) { _regimenFiscal = new List<RegimenFiscal>(); }
                return _regimenFiscal;
            }
            set { _regimenFiscal = value; }
        }
        /// <summary>
        /// Atributo requerido para la Clave del Registro Federal de Contribuyentes correspondiente al contribuyente emisor del comprobante sin guiones o espacios.
        /// </summary>
        [XmlAttribute("rfc")] public string Rfc
        {
            get { return _rfc; }
            set { _rfc = value; }
        }
        /// <summary>
        /// Atributo opcional para el nombre, denominación o razón social del contribuyente emisor del comprobante.
        /// </summary>
        [XmlAttribute("nombre")] public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
