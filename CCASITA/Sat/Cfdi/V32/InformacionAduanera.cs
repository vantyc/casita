using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Tipo definido para expresar información aduanera
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class InformacionAduanera
    {
        private string _numero;
        private DateTime _fecha;
        private string _aduana;
        /// <summary>
        /// Atributo requerido para expresar el número del documento aduanero que ampara la importación del bien.
        /// </summary>
        [XmlAttribute("numero")]
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        /// <summary>
        /// Atributo requerido para expresar la fecha de expedición del documento aduanero que ampara la importación del bien. Se expresa en el formato aaaa-mm-dd
        /// </summary>
        [XmlAttribute("fecha", DataType = "date")]
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        /// <summary>
        /// Atributo opcional para precisar el nombre de la aduana por la que se efectuó la importación del bien.
        /// </summary>
        [XmlAttribute("aduana")]
        public string Aduana
        {
            get { return _aduana; }
            set { _aduana = value; }
        }
    }
}