using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Reflection;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace LaCasita.Sat.Cfdi.Complementos.TimbreFiscalDigital.V10
{
    /// <summary>
    /// Complemento requerido para el Timbrado Fiscal Digital que da valides a un Comprobante Fiscal Digital.
    /// </summary>
    [Serializable]
    //[DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
    public class TimbreFiscalDigital
    {

        private string _version;
        private string _uuid;
        private DateTime? _fechaTimbrado;
        private string _selloCfd;
        private string _noCertificadoSat;
        private string _selloSat;
        private static XmlSerializer _serializer;

        /// <summary>
        /// TimbreFiscalDigital class constructor
        /// </summary>
        public TimbreFiscalDigital()
        {
            _version = "1.0";
        }
        /// <summary>
        /// Atributo requerido para la expresión de la versión del estándar del Timbre Fiscal Digital
        /// </summary>
        [XmlAttribute("version")]
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
        /// <summary>
        /// Atributo requerido para expresar los 36 caracteres del UUID de la transacción de timbrado
        /// </summary>
        [XmlAttribute("UUID")]
        public string Uuid
        {
            get
            {
                return _uuid;
            }
            set
            {
                _uuid = value;
            }
        }
        /// <summary>
        /// Atributo requerido para expresar la fecha y hora de la generación del timbre
        /// </summary>
        [XmlIgnore]
        public DateTime? FechaTimbrado
        {
            get
            {
                return _fechaTimbrado;
            }
            set
            {
                _fechaTimbrado = value;
            }
        }
        /// <summary>
        /// Atributo requerido para expresar la fecha y hora de la generación del timbre
        /// </summary>
        [XmlAttribute("FechaTimbrado")]
        public string FechaTimbradoString
        {
            get
            {
                return string.Format("{0:s}", _fechaTimbrado);
            }
            set
            {
                _fechaTimbrado = DateTime.Parse(value);
            }
        }
        /// <summary>
        /// Atributo requerido para contener el sello digital del comprobante fiscal, que será timbrado. El sello deberá ser expresado cómo una cadena de texto en formato Base 64.
        /// </summary>
        [XmlAttribute("selloCFD")]
        public string SelloCfdBase64
        {
            get
            {
                return _selloCfd;
            }
            set
            {
                _selloCfd = value;
            }
        }
        /// <summary>
        /// Atributo requerido para expresar el número de serie del certificado del SAT usado para el Timbre
        /// </summary>
        [XmlAttribute("noCertificadoSAT")]
        public string NoCertificadoSat
        {
            get
            {
                return _noCertificadoSat;
            }
            set
            {
                _noCertificadoSat = value;
            }
        }
        /// <summary>
        /// Atributo requerido para contener el sello digital del Timbre Fiscal Digital, al que hacen referencia las reglas de resolución miscelánea aplicable. El sello deberá ser expresado cómo una cadena de texto en formato Base 64.
        /// </summary>
        [XmlAttribute("selloSAT")]
        public string SelloSatBase64
        {
            get
            {
                return _selloSat;
            }
            set
            {
                _selloSat = value;
            }
        }

        /// <summary>
        /// Atributo interno para contener el sello digital del comprobante fiscal "NO SE SERIALIZA"
        /// </summary>
        [XmlIgnore]
        public bool? SelloValido
        {
            get
            {
                using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("LaCasita.Sat.Cer." + _noCertificadoSat + ".cer"))
                {
                    if (stream == null) return null;
                    var certificadoBytes = new byte[stream.Length];
                    stream.Read(certificadoBytes, 0, certificadoBytes.Length);
                    var x509Certificate2 = new X509Certificate2();
                    x509Certificate2.Import(certificadoBytes);
                    var rsaCryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate2.PublicKey.Key;
                    return rsaCryptoServiceProvider.VerifyData(Encoding.UTF8.GetBytes(CadenaOriginal), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String(_selloSat));
                }
            }
        }

        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializerFactory().CreateSerializer(typeof(TimbreFiscalDigital));
                }
                return _serializer;
            }
        }
        /// <summary>
        /// Serializes current TimbreFiscalDigital object into an XML string
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                var xmlWriterSettings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };
                var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
                Serializer.Serialize(xmlWriter, this, xmlSerializerNamespaces);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream, Encoding.UTF8);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }
        /// <summary>
        /// Deserializes workflow markup into an TimbreFiscalDigital object
        /// </summary>
        /// <param name="input">string workflow markup to deserialize</param>
        /// <param name="obj">Output TimbreFiscalDigital object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out TimbreFiscalDigital obj, out Exception exception)
        {
            exception = null;
            obj = default(TimbreFiscalDigital);
            try
            {
                obj = Deserialize(input);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        public static bool Deserialize(string input, out TimbreFiscalDigital obj)
        {
            Exception exception;
            return Deserialize(input, out obj, out exception);
        }
        public static TimbreFiscalDigital Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((TimbreFiscalDigital)(Serializer.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
        public static TimbreFiscalDigital Deserialize(Stream s)
        {
            return ((TimbreFiscalDigital)(Serializer.Deserialize(s)));
        }
        /// <summary>
        /// Create a clone of this TimbreFiscalDigital object
        /// </summary>
        public virtual TimbreFiscalDigital Clone()
        {
            return ((TimbreFiscalDigital)(MemberwiseClone()));
        }
        [XmlIgnore]
        public string CadenaOriginal
        {
            get
            {
                using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("LaCasita.Sat.Xslt.cadenaoriginal_TFD_1_0.xslt"))
                {
                    if (stream == null) return null;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(stream);
                    using (var stringWriter = new StringWriter())
                    {
                        using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                        {
                            var xslCompiledTransform = new XslCompiledTransform();
                            xslCompiledTransform.Load(xmlDocument);
                            xslCompiledTransform.Transform(new XPathDocument(new StringReader(Serialize())), null, xmlTextWriter);
                            return stringWriter.ToString();
                        }
                    }
                }
            }
        }
    }
}
