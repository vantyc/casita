using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.Complementos.ImpuestosLocales.V10
{
    /// <summary>
    /// Complemento al Comprobante Fiscal Digital para Impuestos Locales
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/implocal")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/implocal", IsNullable = false)]
    public class ImpuestosLocales
    {

        private List<RetencionesLocales> _retencionesLocales;
        private List<TrasladosLocales> _trasladosLocales;
        private string _version;
        private decimal _totaldeRetenciones;
        private decimal _totaldeTraslados;
        private static XmlSerializer _serializer;

        /// <summary>
        /// ImpuestosLocales class constructor
        /// </summary>
        public ImpuestosLocales()
        {
            _version = "1.0";
        }

        /// <summary>
        /// Nodo opcional para la expresión de los impuestos locales retenidos
        /// </summary>
        [XmlElement("RetencionesLocales")]
        public List<RetencionesLocales> RetencionesLocales
        {
            get { return _retencionesLocales; }
            set { _retencionesLocales = value; }
        }

        /// <summary>
        /// Nodo opcional para la expresión de los impuestos locales trasladados
        /// </summary>
        [XmlElement("TrasladosLocales")]
        public List<TrasladosLocales> TrasladosLocales
        {
            get { return _trasladosLocales; }
            set { _trasladosLocales = value; }
        }

        /// <summary>
        /// Atributo requerido para expresar la versión del complemento
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
        /// Atributo requerido para expresar la suma total de Retenciones aplicables
        /// </summary>
        [XmlAttribute]
        public decimal TotaldeRetenciones
        {
            get
            {
                return _totaldeRetenciones;
            }
            set
            {
                _totaldeRetenciones = value;
            }
        }

        /// <summary>
        /// Atributo requerido para expresar la suma total de traslados aplicables
        /// </summary>
        [XmlAttribute]
        public decimal TotaldeTraslados
        {
            get
            {
                return _totaldeTraslados;
            }
            set
            {
                _totaldeTraslados = value;
            }
        }

        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializerFactory().CreateSerializer(typeof(ImpuestosLocales));
                }
                return _serializer;
            }
        }

        /// <summary>
        /// Test whether RetencionesLocales should be serialized
        /// </summary>
        public virtual bool ShouldSerializeRetencionesLocales()
        {
            return RetencionesLocales != null && RetencionesLocales.Count > 0;
        }


        /// <summary>
        /// Test whether TrasladosLocales should be serialized
        /// </summary>
        public virtual bool ShouldSerializeTrasladosLocales()
        {
            return TrasladosLocales != null && TrasladosLocales.Count > 0;
        }


        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ImpuestosLocales object into an XML string
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(Encoding encoding)
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                var xmlWriterSettings = new XmlWriterSettings {Encoding = encoding, Indent = true};
                var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream, encoding);
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

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ImpuestosLocales object
        /// </summary>
        /// <param name="input">string workflow markup to deserialize</param>
        /// <param name="obj">Output ImpuestosLocales object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out ImpuestosLocales obj, out Exception exception)
        {
            exception = null;
            obj = default(ImpuestosLocales);
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

        public static bool Deserialize(string input, out ImpuestosLocales obj)
        {
            Exception exception;
            return Deserialize(input, out obj, out exception);
        }

        public static ImpuestosLocales Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((ImpuestosLocales)(Serializer.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ImpuestosLocales Deserialize(Stream s)
        {
            return ((ImpuestosLocales)(Serializer.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current ImpuestosLocales object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="encoding"></param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, Encoding encoding, out Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, Encoding encoding)
        {
            StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ImpuestosLocales object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="encoding"></param>
        /// <param name="obj">Output ImpuestosLocales object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, Encoding encoding, out ImpuestosLocales obj, out Exception exception)
        {
            exception = null;
            obj = default(ImpuestosLocales);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ImpuestosLocales obj, out Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ImpuestosLocales obj)
        {
            Exception exception;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ImpuestosLocales LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static ImpuestosLocales LoadFromFile(string fileName, Encoding encoding)
        {
            FileStream file = null;
            StreamReader sr = null;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }

    }
}
