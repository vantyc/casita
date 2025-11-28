using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Pdf32 = LaCasita.Sat.Cfdi.V32.Pdf;

namespace LaCasita.Sat.Cfdi.V32
{
    /// <summary>
    /// Estándar de Comprobante fiscal digital a través de Internet.
    /// </summary>
    [Serializable]
    //[DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
    public class Comprobante 
    {
        private Emisor _emisor;
        private Receptor _receptor;
        private List<Concepto> _conceptos;
        private Impuestos _impuestos;
        private Complemento _complemento;
        private Addenda _addenda;
        private string _version;
        private string _serie;
        private string _folio;
        private DateTime _fecha;
        private string _sello;
        private string _formaDePago;
        private string _noCertificado;
        private string _certificado;
        private string _condicionesDePago;
        private decimal _subTotal;
        private decimal? _descuento;
        private string _motivoDescuento;
        private string _tipoCambio;
        private string _moneda;
        private decimal _total;
        private TipoDeComprobante _tipoDeComprobante;
        private string _metodoDePago;
        private string _lugarExpedicion;
        private string _numCtaPago;
        private string _folioFiscalOrig;
        private string _serieFolioFiscalOrig;
        private DateTime? _fechaFolioFiscalOrig;
        private decimal? _montoFolioFiscalOrig;
        private static XmlSerializer _serializer;

        /// <summary>
        /// Constructor class Comprobante
        /// </summary>
        public Comprobante() { _version = "3.2"; }
        [XmlAttributeAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string XsiSchemaLocation;
        /// <summary>
        /// Nodo requerido para expresar la información del contribuyente emisor del comprobante.
        /// </summary>
        [XmlElement(Order = 0)]
        public Emisor Emisor
        {
            get
            {
                if ((_emisor == null)) { _emisor = new Emisor(); }
                return _emisor;
            }
            set { _emisor = value; }
        }
        /// <summary>
        /// Nodo requerido para precisar la información del contribuyente receptor del comprobante.
        /// </summary>
        [XmlElement(Order = 1)]
        public Receptor Receptor
        {
            get
            {
                if ((_receptor == null)) { _receptor = new Receptor(); }
                return _receptor;
            }
            set { _receptor = value; }
        }
        /// <summary>
        /// Nodo requerido para enlistar los conceptos cubiertos por el comprobante.
        /// </summary>
        [XmlArray(Order = 2)]
        [XmlArrayItem("Concepto", IsNullable = false)]
        public List<Concepto> Conceptos
        {
            get
            {
                if ((_conceptos == null)) { _conceptos = new List<Concepto>(); }
                return _conceptos;
            }
            set { _conceptos = value; }
        }
        /// <summary>
        /// Determina si Conceptos debe serializarse
        /// </summary>
        public virtual bool ShouldSerializeConceptos()
        {
            return Conceptos != null && Conceptos.Count > 0;
        }
        /// <summary>
        /// Nodo requerido para capturar los impuestos aplicables.
        /// </summary>
        [XmlElement(Order = 3)]
        public Impuestos Impuestos
        {
            get
            {
                if ((_impuestos == null)) { _impuestos = new Impuestos(); }
                return _impuestos;
            }
            set { _impuestos = value; }
        }
        /// <summary>
        /// Nodo opcional donde se incluirá el complemento Timbre Fiscal Digital de manera obligatoria y los nodos complementarios determinados por el SAT, de acuerdo a las disposiciones particulares a un sector o actividad específica.
        /// </summary>
        [XmlElement(Order = 4)]
        public Complemento Complemento
        {
            get
            {
                //if ((_complemento == null)) { _complemento = new Complemento(); }
                return _complemento;
            }
            set { _complemento = value; }
        }
        /// <summary>
        /// Nodo opcional para recibir las extensiones al presente formato que sean de utilidad al contribuyente. Para las reglas de uso del mismo, referirse al formato de origen.
        /// </summary>
        [XmlElement(Order = 5)]
        public Addenda Addenda
        {
            get
            {
                //if ((_addenda == null)) { _complemento = new Complemento(); }
                return _addenda;
            }
            set { _addenda = value; }
        }
        /// <summary>
        /// Determina si Addenda debe serializarse
        /// </summary>
        public virtual bool ShouldSerializeAddenda()
        {
            return Addenda != null && Addenda.Any.Count > 0;
        }
        /// <summary>
        /// Atributo requerido con valor prefijado a 3.2 que indica la versión del estándar bajo el que se encuentra expresado el comprobante.
        /// </summary>
        [XmlAttribute("version")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        /// Atributo opcional para precisar la serie para control interno del contribuyente. Este atributo acepta una cadena de caracteres alfabéticos de 1 a 25 caracteres sin incluir caracteres acentuados.
        /// </summary>
        [XmlAttribute("serie")]
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        /// <summary>
        /// Atributo opcional para control interno del contribuyente que acepta un valor numérico entero superior a 0 que expresa el folio del comprobante.
        /// </summary>
        [XmlAttribute("folio")]
        public string Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        /// <summary>
        /// Atributo interno para la expresión de la fecha y hora de expedición  del comprobante fiscal. "NO SE SERIALIZA"
        /// </summary>
        [XmlIgnore]
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        /// <summary>
        /// Atributo requerido para la expresión de la fecha y hora de expedición  del comprobante fiscal. Se expresa en la forma aaaa-mm-ddThh:mm:ss, de acuerdo con la especificación ISO 8601. se serializa como "fecha"
        /// </summary>
        [XmlAttribute("fecha")]
        public string FechaIso8601
        {
            get { return _fecha.ToString("s"); }
            set { _fecha = DateTime.Parse(value); }
        }
        /// <summary>
        /// Atributo requerido para contener el sello digital del comprobante fiscal, al que hacen referencia las reglas de resolución miscelánea aplicable. El sello deberá ser expresado cómo una cadena de texto en formato Base 64, se serializa como "sello"
        /// </summary>
        [XmlAttribute("sello")]
        public string SelloBase64
        {
            get { return _sello; }
            set { _sello = value; }
        }
        /// <summary>
        /// Atributo interno para contener el sello digital del comprobante fiscal "NO SE SERIALIZA"
        /// </summary>
        [XmlIgnore]
        public bool SelloValido
        {
            get
            {
                var certificadoX5092 = new X509Certificate2();
                certificadoX5092.Import(Convert.FromBase64String(CertificadoBase64));
                var rsaCryptoServiceProvider = (RSACryptoServiceProvider)certificadoX5092.PublicKey.Key;
                return rsaCryptoServiceProvider.VerifyData(Encoding.UTF8.GetBytes(CadenaOriginal), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String(_sello));
            }
        }
        /// <summary>
        /// Atributo requerido para precisar la forma de pago que aplica para este comprobante fiscal digital a través de Internet. Se utiliza para expresar Pago en una sola exhibición o número de parcialidad pagada contra el total de parcialidades, Parcialidad 1 de X.
        /// </summary>
        [XmlAttribute("formaDePago")]
        public string FormaDePago
        {
            get { return _formaDePago; }
            set { _formaDePago = value; }
        }
        /// <summary>
        /// Atributo requerido para expresar el número de serie del certificado de sello digital que ampara al comprobante, de acuerdo al acuse correspondiente a 20 posiciones otorgado por el sistema del SAT.
        /// </summary>
        [XmlAttribute("noCertificado")]
        public string NoCertificado
        {
            get { return _noCertificado; }
            set { _noCertificado = value; }
        }
        /// <summary>
        /// Atributo requerido que sirve para expresar el certificado de sello digital que ampara al comprobante como texto, en formato base 64.
        /// </summary>
        [XmlAttribute("certificado")]
        public string CertificadoBase64
        {
            get { return _certificado; }
            set { _certificado = value; }
        }
        /// <summary>
        /// Atributo opcional para expresar las condiciones comerciales aplicables para el pago del comprobante fiscal digital a través de Internet.
        /// </summary>
        [XmlAttribute("condicionesDePago")]
        public string CondicionesDePago
        {
            get { return _condicionesDePago; }
            set { _condicionesDePago = value; }
        }
        /// <summary>
        /// Atributo requerido para representar la suma de los importes antes de descuentos e impuestos.
        /// </summary>
        [XmlAttribute("subTotal")]
        public decimal SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }
        /// <summary>
        /// Atributo opcional para representar el importe total de los descuentos aplicables antes de impuestos.
        /// </summary>
        [XmlAttribute("descuento")]
        public decimal Descuento
        {
            get { return _descuento ?? default(decimal); }
            set { _descuento = value; }
        }
        /// <summary>
        /// Determina si se ha especificado un descuento
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore]
        public bool DescuentoSpecified
        {
            get { return _descuento.HasValue; }
            set { if (value == false) { _descuento = null; } }
        }
        /// <summary>
        /// Atributo opcional para expresar el motivo del descuento aplicable.
        /// </summary>
        [XmlAttribute("motivoDescuento")]
        public string MotivoDescuento
        {
            get { return _motivoDescuento; }
            set { _motivoDescuento = value; }
        }
        /// <summary>
        /// Atributo opcional para representar el tipo de cambio conforme a la moneda usada
        /// </summary>
        [XmlAttribute]
        public string TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }
        /// <summary>
        /// Atributo opcional para expresar la moneda utilizada para expresar los montos
        /// </summary>
        [XmlAttribute]
        public string Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }
        /// <summary>
        /// Atributo requerido para representar la suma del subtotal, menos los descuentos aplicables, más los impuestos trasladados, menos los impuestos retenidos.
        /// </summary>
        [XmlAttribute("total")]
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }
        /// <summary>
        /// Atributo requerido para expresar el efecto del comprobante fiscal para el contribuyente emisor.
        /// </summary>
        [XmlAttribute("tipoDeComprobante")]
        public TipoDeComprobante TipoDeComprobante
        {
            get { return _tipoDeComprobante; }
            set { _tipoDeComprobante = value; }
        }
        /// <summary>
        /// Atributo requerido de texto libre para expresar el método de pago de los bienes o servicios amparados por el comprobante. Se entiende como método de pago leyendas tales como: cheque, tarjeta de crédito o debito, depósito en cuenta, etc.
        /// </summary>
        [XmlAttribute("metodoDePago")]
        public string MetodoDePago
        {
            get { return _metodoDePago; }
            set { _metodoDePago = value; }
        }
        /// <summary>
        /// Atributo requerido para incorporar el lugar de expedición del comprobante.
        /// </summary>
        [XmlAttribute]
        public string LugarExpedicion
        {
            get { return _lugarExpedicion; }
            set { _lugarExpedicion = value; }
        }
        /// <summary>
        /// Atributo Opcional para incorporar al menos los cuatro últimos dígitos del número de cuenta con la que se realizó el pago.
        /// </summary>
        [XmlAttribute]
        public string NumCtaPago
        {
            get { return _numCtaPago; }
            set { _numCtaPago = value; }
        }
        /// <summary>
        /// Atributo opcional para señalar el número de folio fiscal del comprobante que se hubiese expedido por el valor total del comprobante, tratándose del pago en parcialidades.
        /// </summary>
        [XmlAttribute]
        public string FolioFiscalOrig
        {
            get { return _folioFiscalOrig; }
            set { _folioFiscalOrig = value; }
        }
        /// <summary>
        /// Atributo opcional para señalar la serie del folio del comprobante que se hubiese expedido por el valor total del comprobante, tratándose del pago en parcialidades.
        /// </summary>
        [XmlAttribute]
        public string SerieFolioFiscalOrig
        {
            get { return _serieFolioFiscalOrig; }
            set { _serieFolioFiscalOrig = value; }
        }
        /// <summary>
        /// Atributo opcional para señalar la fecha de expedición del comprobante que se hubiese emitido por el valor total del comprobante, tratándose del pago en parcialidades. Se expresa en la forma aaaa-mm-ddThh:mm:ss, de acuerdo con la especificación ISO 8601.
        /// </summary>
        [XmlAttribute]
        public DateTime FechaFolioFiscalOrig
        {
            get { return _fechaFolioFiscalOrig ?? default(DateTime); }
            set
            {
                _fechaFolioFiscalOrig = value;
            }
        }
        /// <summary>
        /// Determina si se ha especificado la fecha del folio fiscal original
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore]
        public bool FechaFolioFiscalOrigSpecified
        {
            get { return _fechaFolioFiscalOrig.HasValue; }
            set { if (value == false) { _fechaFolioFiscalOrig = null; }
            }
        }
        /// <summary>
        /// Atributo opcional para señalar el total del comprobante que se hubiese expedido por el valor total de la operación, tratándose del pago en parcialidades
        /// </summary>
        [XmlAttribute]
        public decimal MontoFolioFiscalOrig
        {
            get { return _montoFolioFiscalOrig ?? default(decimal); }
            set { _montoFolioFiscalOrig = value; }
        }
        /// <summary>
        /// Determina si se ha especificado el monto del folio fiscal original
        /// </summary>
        /// <returns>true o false </returns>
        [XmlIgnore] public bool MontoFolioFiscalOrigSpecified
        {
            get { return _montoFolioFiscalOrig.HasValue; }
            set { if (value == false) { _montoFolioFiscalOrig = null; }}
        }
        private static XmlSerializer Serializer
        {
            get 
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializerFactory().CreateSerializer(typeof(Comprobante));
                }
                return _serializer;
            }
        }
        /// <summary>
        /// Serializa objeto Comprobante en un String XML
        /// </summary>
        /// <returns>String XML</returns>
        public virtual string Serialize()
        {
            var xmlWriterSettings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };
            using (var memoryStream = new MemoryStream())
            {
                var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                XsiSchemaLocation = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd";
                foreach (var xmlNode in Complemento.Any)
                {
                    switch (xmlNode.Name)
                    {
                        case "nomina:Nomina":
                            xmlSerializerNamespaces.Add("nomina", "http://www.sat.gob.mx/nomina");
                            XsiSchemaLocation = XsiSchemaLocation + " http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina.xsd";
                            break;
                        case "implocal:ImpuestosLocales":
                            xmlSerializerNamespaces.Add("implocal", "http://www.sat.gob.mx/implocal");
                            XsiSchemaLocation = XsiSchemaLocation + " http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd";
                            break;                            
                    }
                }
                Serializer.Serialize(xmlWriter, this, xmlSerializerNamespaces);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// Deserializa string XML en un objeto Comprobante
        /// </summary>
        /// <param name="input">String XML a deserializar</param>
        /// <returns>true si puede deserializar el objeto; de lo contrario, false</returns>
        public static Comprobante Deserialize(string input)
        {
            using (var stringReader = new StringReader(input))
            {
                var comprobante = ((Comprobante)(Serializer.Deserialize(XmlReader.Create(stringReader))));
                comprobante.OriginalXmlString = input;
                return comprobante;
            }
        }
        /// <summary>
        /// Guarda objeto Comprobante en un archivo XML
        /// </summary>
        /// <param name="fileName">Nombre y ruta del archivo XML</param>
        /// <returns>true si puede serializar y guardar en archivo XML; de lo contrario, false</returns>
        public virtual void SaveToFile(string fileName)
        {
            using (var streamWriter = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                var xmlString = Serialize();
                streamWriter.WriteLine(xmlString);
            }
        }
        /// <summary>
        /// Xml original obtenido desde Deserialize
        /// </summary>
        /// <returns>string</returns>
        [XmlIgnore]
        public string OriginalXmlString { get; private set; }
        /// <summary>
        /// Lee archivo XML en un objeto Comprobante
        /// </summary>
        /// <param name="fileName">Nombre y ruta completa del archivo XML</param>
        /// <returns>true si puede deserializar y cargar en objeto Comprobante; de lo contrario, false</returns>
        public static Comprobante LoadFromFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var xmlString = streamReader.ReadToEnd();
                    return Deserialize(xmlString);
                }
            }
        }
        /// <summary>
        /// Crear un clon de este Comprobante
        /// </summary>
        public virtual Comprobante Clone()
        {
            return ((Comprobante)(MemberwiseClone()));
        }
        
        [XmlIgnore]
        public string CadenaOriginal
        {
            get
            {
                using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("LaCasita.Sat.Xslt.cadenaoriginal_3_2.xslt"))
                {
                    if (stream == null) return null;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(stream);
                    using (var stringWriter = new StringWriter())
                    {
                        using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                        {
                            var xslCompiledTransform = new XslCompiledTransform();
                            xslCompiledTransform.Load(xmlDocument, null, new Xslt.Resolver());
                            xslCompiledTransform.Transform(new XPathDocument(new StringReader(Serialize())), null, xmlTextWriter);
                            return stringWriter.ToString();
                        }
                    }
                }
            }
        }

        public virtual FileInfo Pdf(FileInfo fileInfo, bool autoSize = false, bool pagare = false, bool reporteValidacion = false)
        {
            return Pdf32.Get(this, fileInfo, autoSize, pagare, reporteValidacion);
        }



    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum RetencionImpuesto
    {
        /// <summary>
        /// Impuesto sobre la renta
        /// </summary>
        ISR,
        /// <summary>
        /// Impuesto al Valor Agregado
        /// </summary>
        IVA,
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum TrasladoImpuesto
    {
        /// <summary>
        /// Impuesto al Valor Agregado
        /// </summary>
        IVA,
        /// <summary>
        /// Impuesto especial sobre productos y servicios
        /// </summary>
        IEPS,
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum TipoDeComprobante
    {
        /// <summary>
        /// Comprobante de tipo Ingreso
        /// </summary>
        ingreso,
        /// <summary>
        /// Comprobante de tipo Egreso
        /// </summary>
        egreso,
        /// <summary>
        /// Comprobante de tipo Traslado
        /// </summary>
        traslado,
    }

}
