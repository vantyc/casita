using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Nodo requerido para capturar los impuestos aplicables.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public class Impuestos
    {
        private List<Retencion> _retenciones;
        private List<Traslado> _traslados;
        private decimal? _totalImpuestosRetenidos;
        private decimal? _totalImpuestosTrasladados;
        /// <summary>
        /// Nodo opcional para capturar los impuestos retenidos aplicables
        /// </summary>
        [XmlArray(Order = 0)] [XmlArrayItem("Retencion", IsNullable = false)] public List<Retencion> Retenciones
        {
            get
            {
                if ((_retenciones == null)) { _retenciones = new List<Retencion>(); }
                return _retenciones;
            }
            set { _retenciones = value; }
        }

        /// <summary>
        /// Determina si Retenciones debe serializarse
        /// </summary>
        public virtual bool ShouldSerializeRetenciones()
        {
            return Retenciones != null && Retenciones.Count > 0;
        }
        /// <summary>
        /// Nodo opcional para asentar o referir los impuestos trasladados aplicables
        /// </summary>
        [XmlArray(Order = 1)] [XmlArrayItem("Traslado", IsNullable = false)] public List<Traslado> Traslados
        {
            get
            {
                if ((_traslados == null)) { _traslados = new List<Traslado>(); }
                return _traslados;
            }
            set { _traslados = value; }
        }
        /// <summary>
        /// Determina si Retenciones debe serializarse
        /// </summary>
        public virtual bool ShouldSerializeTraslados()
        {
            return Traslados != null && Traslados.Count > 0;
        }
        /// <summary>
        /// Atributo opcional para expresar el total de los impuestos retenidos que se desprenden de los conceptos expresados en el comprobante fiscal digital a través de Internet.
        /// </summary>
        [XmlAttribute("totalImpuestosRetenidos")] public decimal TotalImpuestosRetenidos
        {
            get { return _totalImpuestosRetenidos.HasValue ? _totalImpuestosRetenidos.Value : default(decimal); }
            set { _totalImpuestosRetenidos = value; }
        }
        /// <summary>
        /// Determina si se ha especificado el total de impuestos retenidos
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore] public bool TotalImpuestosRetenidosSpecified
        {
            get { return _totalImpuestosRetenidos.HasValue; }
            set { if (value == false) { _totalImpuestosRetenidos = null; }
            }
        }
        /// <summary>
        /// Atributo opcional para expresar el total de los impuestos trasladados que se desprenden de los conceptos expresados en el comprobante fiscal digital a través de Internet.
        /// </summary>
        [XmlAttribute("totalImpuestosTrasladados")] public decimal TotalImpuestosTrasladados
        {
            get { return _totalImpuestosTrasladados.HasValue ? _totalImpuestosTrasladados.Value : default(decimal); }
            set { _totalImpuestosTrasladados = value; }
        }
        /// <summary>
        /// Determina si se ha especificado el total de impuestos trasladados
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore] public bool TotalImpuestosTrasladadosSpecified
        {
            get { return _totalImpuestosTrasladados.HasValue; }
            set { if (value == false) { _totalImpuestosTrasladados = null; }
            }
        }
    }
}