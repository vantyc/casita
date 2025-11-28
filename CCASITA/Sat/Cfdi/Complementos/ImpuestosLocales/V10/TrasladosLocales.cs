using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LaCasita.Sat.Cfdi.Complementos.ImpuestosLocales.V10
{
    /// <summary>
    /// Nodo opcional para la expresión de los impuestos locales trasladados
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/implocal")]
    public class TrasladosLocales
    {

        private string _impLocTrasladado;
        private decimal _tasadeTraslado;
        private decimal _importe;
        private static XmlSerializer _serializer;

        /// <summary>
        /// Nombre del impuesto local trasladado
        /// </summary>
        [XmlAttribute]
        public string ImpLocTrasladado
        {
            get
            {
                return _impLocTrasladado;
            }
            set
            {
                _impLocTrasladado = value;
            }
        }

        /// <summary>
        /// Porcentaje de traslado del impuesto local
        /// </summary>
        [XmlAttribute]
        public decimal TasadeTraslado
        {
            get
            {
                return _tasadeTraslado;
            }
            set
            {
                _tasadeTraslado = value;
            }
        }

        /// <summary>
        /// Monto del impuesto local trasladado
        /// </summary>
        [XmlAttribute]
        public decimal Importe
        {
            get
            {
                return _importe;
            }
            set
            {
                _importe = value;
            }
        }

        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializerFactory().CreateSerializer(typeof(TrasladosLocales));
                }
                return _serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ImpuestosLocalesTrasladosLocales object into an XML string
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
        /// Deserializes workflow markup into an ImpuestosLocalesTrasladosLocales object
        /// </summary>
        /// <param name="input">string workflow markup to deserialize</param>
        /// <param name="obj">Output ImpuestosLocalesTrasladosLocales object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out TrasladosLocales obj, out Exception exception)
        {
            exception = null;
            obj = default(TrasladosLocales);
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

        public static bool Deserialize(string input, out TrasladosLocales obj)
        {
            Exception exception;
            return Deserialize(input, out obj, out exception);
        }

        public static TrasladosLocales Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((TrasladosLocales)(Serializer.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static TrasladosLocales Deserialize(Stream s)
        {
            return ((TrasladosLocales)(Serializer.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current ImpuestosLocalesTrasladosLocales object into file
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
        /// Deserializes xml markup from file into an ImpuestosLocalesTrasladosLocales object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="encoding"></param>
        /// <param name="obj">Output ImpuestosLocalesTrasladosLocales object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, Encoding encoding, out TrasladosLocales obj, out Exception exception)
        {
            exception = null;
            obj = default(TrasladosLocales);
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

        public static bool LoadFromFile(string fileName, out TrasladosLocales obj, out Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out TrasladosLocales obj)
        {
            Exception exception;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static TrasladosLocales LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static TrasladosLocales LoadFromFile(string fileName, Encoding encoding)
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

        #region Clone method
        /// <summary>
        /// Create a clone of this ImpuestosLocalesTrasladosLocales object
        /// </summary>
        public virtual TrasladosLocales Clone()
        {
            return ((TrasladosLocales)(MemberwiseClone()));
        }
        #endregion
    }
}
