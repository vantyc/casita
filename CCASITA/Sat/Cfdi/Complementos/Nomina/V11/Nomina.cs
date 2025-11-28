using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.Complementos.Nomina.V11
{
    /// <summary>
    /// Complemento al Comprobante Fiscal Digital a través de Internet (CFDI) para el manejo de datos de Nómina.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/nomina")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/nomina", IsNullable = false)]
    public class Nomina
    {

        private Percepciones _percepciones;

        private Deducciones _deducciones;

        private List<Incapacidad> _incapacidades;

        private List<HorasExtra> _horasExtras;

        private string _version;

        private string _registroPatronal;

        private string _numEmpleado;

        private string _curp;

        private int _tipoRegimen;

        private string _numSeguridadSocial;

        private DateTime _fechaPago;

        private DateTime _fechaInicialPago;

        private DateTime _fechaFinalPago;

        private decimal _numDiasPagados;

        private string _departamento;

        private string _clabe;

        private int? _banco;

        private DateTime? _fechaInicioRelLaboral;

        private int? _antiguedad;

        private string _puesto;

        private string _tipoContrato;

        private string _tipoJornada;

        private string _periodicidadPago;

        private decimal? _salarioBaseCotApor;

        private int? _riesgoPuesto;

        private decimal? _salarioDiarioIntegrado;

        private static XmlSerializer _serializer;

        /// <summary>
        /// Nomina class constructor
        /// </summary>
        public Nomina()
        {
            _version = "1.1";
        }

        /// <summary>
        /// Nodo opcional para expresar las percepciones aplicables
        /// </summary>
        [XmlElement(Order = 0)]
        public Percepciones Percepciones
        {
            get
            {
                if ((_percepciones == null))
                {
                    _percepciones = new Percepciones();
                }
                return _percepciones;
            }
            set
            {
                _percepciones = value;
            }
        }

        /// <summary>
        /// Nodo opcional para expresar las deducciones aplicables
        /// </summary>
        [XmlElement(Order = 1)]
        public Deducciones Deducciones
        {
            get
            {
                if ((_deducciones == null))
                {
                    _deducciones = new Deducciones();
                }
                return _deducciones;
            }
            set
            {
                _deducciones = value;
            }
        }

        /// <summary>
        /// Nodo opcional para expresar las incapacidades aplicables
        /// </summary>
        [XmlArray(Order = 2)]
        [XmlArrayItem("Incapacidad", IsNullable = false)]
        public List<Incapacidad> Incapacidades
        {
            get
            {
                if ((_incapacidades == null))
                {
                    _incapacidades = new List<Incapacidad>();
                }
                return _incapacidades;
            }
            set
            {
                _incapacidades = value;
            }
        }

        /// <summary>
        /// Nodo opcional para expresar las horas extras aplicables
        /// </summary>
        [XmlArray(Order = 3)]
        [XmlArrayItem("HorasExtra", IsNullable = false)]
        public List<HorasExtra> HorasExtras
        {
            get
            {
                if ((_horasExtras == null))
                {
                    _horasExtras = new List<HorasExtra>();
                }
                return _horasExtras;
            }
            set
            {
                _horasExtras = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la versión del complemento
        /// </summary>
        [XmlAttribute]
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
        /// Atributo opcional para expresar el registro patronal a 20 posiciones máximo
        /// </summary>
        [XmlAttribute]
        public string RegistroPatronal
        {
            get
            {
                return _registroPatronal;
            }
            set
            {
                _registroPatronal = value;
            }
        }

        /// <summary>
        /// Atributo requerido para expresar el número de empleado de 1 a 15 posiciones
        /// </summary>
        [XmlAttribute]
        public string NumEmpleado
        {
            get
            {
                return _numEmpleado;
            }
            set
            {
                _numEmpleado = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la CURP del trabajador
        /// </summary>
        [XmlAttribute("CURP")]
        public string Curp
        {
            get
            {
                return _curp;
            }
            set
            {
                _curp = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la clave del régimen por el cual se tiene contratado al trabajador, conforme al catálogo publicado en el portal del SAT en internet
        /// </summary>
        [XmlAttribute]
        public int TipoRegimen
        {
            get
            {
                return _tipoRegimen;
            }
            set
            {
                _tipoRegimen = value;
            }
        }

        /// <summary>
        /// Atributo opcional para la expresión del número de seguridad social aplicable al trabajador
        /// </summary>
        [XmlAttribute]
        public string NumSeguridadSocial
        {
            get
            {
                return _numSeguridadSocial;
            }
            set
            {
                _numSeguridadSocial = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la fecha efectiva de erogación del gasto. Se expresa en la forma aaaa-mm-dd, de acuerdo con la especificación ISO 8601.
        /// </summary>
        [XmlAttribute(DataType = "date")]
        public DateTime FechaPago
        {
            get
            {
                return _fechaPago;
            }
            set
            {
                _fechaPago = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la fecha inicial del pago. Se expresa en la forma aaaa-mm-dd, de acuerdo con la especificación ISO 8601.
        /// </summary>
        [XmlAttribute(DataType = "date")]
        public DateTime FechaInicialPago
        {
            get
            {
                return _fechaInicialPago;
            }
            set
            {
                _fechaInicialPago = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión de la fecha final del pago. Se expresa en la forma aaaa-mm-dd, de acuerdo con la especificación ISO 8601.
        /// </summary>
        [XmlAttribute(DataType = "date")]
        public DateTime FechaFinalPago
        {
            get
            {
                return _fechaFinalPago;
            }
            set
            {
                _fechaFinalPago = value;
            }
        }

        /// <summary>
        /// Atributo requerido para la expresión del número de días pagados
        /// </summary>
        [XmlAttribute]
        public decimal NumDiasPagados
        {
            get
            {
                return _numDiasPagados;
            }
            set
            {
                _numDiasPagados = value;
            }
        }

        /// <summary>
        /// Atributo opcional para la expresión del departamento o área a la que pertenece el trabajador
        /// </summary>
        [XmlAttribute]
        public string Departamento
        {
            get
            {
                return _departamento;
            }
            set
            {
                _departamento = value;
            }
        }

        /// <summary>
        /// Atributo opcional para la expresión de la CLABE
        /// </summary>
        [XmlAttribute("CLABE", DataType = "integer")]
        public string Clabe
        {
            get
            {
                return _clabe;
            }
            set
            {
                _clabe = value;
            }
        }

        /// <summary>
        /// Atributo opcional para la expresión del Banco conforme al catálogo, donde se realiza un depósito de nómina
        /// </summary>
        [XmlAttribute]
        public int Banco
        {
            get
            {
                if (_banco.HasValue)
                {
                    return _banco.Value;
                }
                else
                {
                    return default(int);
                }
            }
            set
            {
                _banco = value;
            }
        }
        
        [XmlIgnore]
        public string BancoString
        {
            get 
            {
                return _banco.HasValue ? _banco.Value.ToString("000") : null;
            }
            set
            {
                _banco = Convert.ToByte(value);
            }
        }



        [XmlIgnore]
        public bool BancoSpecified
        {
            get
            {
                return _banco.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _banco = null;
                }
            }
        }

        /// <summary>
        /// Atributo opcional para expresar la fecha de inicio de la relación laboral entre el empleador y el empleado
        /// </summary>
        [XmlAttribute(DataType = "date")]
        public DateTime FechaInicioRelLaboral
        {
            get
            {
                if (_fechaInicioRelLaboral.HasValue)
                {
                    return _fechaInicioRelLaboral.Value;
                }
                else
                {
                    return default(DateTime);
                }
            }
            set
            {
                _fechaInicioRelLaboral = value;
            }
        }

        [XmlIgnore]
        public bool FechaInicioRelLaboralSpecified
        {
            get
            {
                return _fechaInicioRelLaboral.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _fechaInicioRelLaboral = null;
                }
            }
        }

        /// <summary>
        /// Número de semanas que el empleado ha mantenido relación laboral con el empleador
        /// </summary>
        [XmlAttribute]
        public int Antiguedad
        {
            get
            {
                if (_antiguedad.HasValue)
                {
                    return _antiguedad.Value;
                }
                else
                {
                    return default(int);
                }
            }
            set
            {
                _antiguedad = value;
            }
        }

        [XmlIgnore]
        public bool AntiguedadSpecified
        {
            get
            {
                return _antiguedad.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _antiguedad = null;
                }
            }
        }

        /// <summary>
        /// Puesto asignado al empleado o actividad que realiza
        /// </summary>
        [XmlAttribute]
        public string Puesto
        {
            get
            {
                return _puesto;
            }
            set
            {
                _puesto = value;
            }
        }

        /// <summary>
        /// Tipo de contrato que tiene el trabajador: Base, Eventual, Confianza, Sindicalizado, a prueba, etc.
        /// </summary>
        [XmlAttribute]
        public string TipoContrato
        {
            get
            {
                return _tipoContrato;
            }
            set
            {
                _tipoContrato = value;
            }
        }

        /// <summary>
        /// Tipo de jornada que cubre el trabajador: Diurna, nocturna, mixta, por hora, reducida, continuada, partida, por turnos, etc.
        /// </summary>
        [XmlAttribute]
        public string TipoJornada
        {
            get
            {
                return _tipoJornada;
            }
            set
            {
                _tipoJornada = value;
            }
        }

        /// <summary>
        /// Forma en que se establece el pago del salario: diario, semanal, quincenal, catorcenal mensual, bimestral, unidad de obra, comisión, precio alzado, etc.
        /// </summary>
        [XmlAttribute]
        public string PeriodicidadPago
        {
            get
            {
                return _periodicidadPago;
            }
            set
            {
                _periodicidadPago = value;
            }
        }

        /// <summary>
        /// Retribución otorgada al trabajador, que se integra por los pagos hechos en efectivo por cuota diaria, gratificaciones, percepciones, alimentación, habitación, primas, comisiones, prestaciones en especie y cualquiera otra cantidad o prestación que se entregue al trabajador por su trabajo
        /// </summary>
        [XmlAttribute]
        public decimal SalarioBaseCotApor
        {
            get
            {
                if (_salarioBaseCotApor.HasValue)
                {
                    return _salarioBaseCotApor.Value;
                }
                else
                {
                    return default(decimal);
                }
            }
            set
            {
                _salarioBaseCotApor = value;
            }
        }

        [XmlIgnore]
        public bool SalarioBaseCotAporSpecified
        {
            get
            {
                return _salarioBaseCotApor.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _salarioBaseCotApor = null;
                }
            }
        }

        /// <summary>
        /// Clave conforme a la Clase en que deben inscribirse los patrones, de acuerdo a las actividades que desempeñan sus trabajadores, según lo previsto en el artículo 196 del Reglamento en Materia de Afiliación Clasificación de Empresas, Recaudación y Fiscalización. Catálogo publicado en el portal del SAT en internet
        /// </summary>
        [XmlAttribute]
        public int RiesgoPuesto
        {
            get
            {
                if (_riesgoPuesto.HasValue)
                {
                    return _riesgoPuesto.Value;
                }
                else
                {
                    return default(int);
                }
            }
            set
            {
                _riesgoPuesto = value;
            }
        }

        [XmlIgnore]
        public bool RiesgoPuestoSpecified
        {
            get
            {
                return _riesgoPuesto.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _riesgoPuesto = null;
                }
            }
        }

        /// <summary>
        /// Salario diario integrado
        /// </summary>
        [XmlAttribute]
        public decimal SalarioDiarioIntegrado
        {
            get
            {
                if (_salarioDiarioIntegrado.HasValue)
                {
                    return _salarioDiarioIntegrado.Value;
                }
                else
                {
                    return default(decimal);
                }
            }
            set
            {
                _salarioDiarioIntegrado = value;
            }
        }

        [XmlIgnore]
        public bool SalarioDiarioIntegradoSpecified
        {
            get
            {
                return _salarioDiarioIntegrado.HasValue;
            }
            set
            {
                if (value == false)
                {
                    _salarioDiarioIntegrado = null;
                }
            }
        }

        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializerFactory().CreateSerializer(typeof(Nomina));
                }
                return _serializer;
            }
        }

        /// <summary>
        /// Test whether Incapacidades should be serialized
        /// </summary>
        public virtual bool ShouldSerializeIncapacidades()
        {
            return Incapacidades != null && Incapacidades.Count > 0;
        }

        /// <summary>
        /// Test whether HorasExtras should be serialized
        /// </summary>
        public virtual bool ShouldSerializeHorasExtras()
        {
            return HorasExtras != null && HorasExtras.Count > 0;
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current Nomina object into an XML string
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(Encoding encoding)
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                var xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                xmlWriterSettings.Indent = true;
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
        /// Deserializes workflow markup into an Nomina object
        /// </summary>
        /// <param name="input">string workflow markup to deserialize</param>
        /// <param name="obj">Output Nomina object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out Nomina obj, out Exception exception)
        {
            exception = null;
            obj = default(Nomina);
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

        public static bool Deserialize(string input, out Nomina obj)
        {
            Exception exception;
            return Deserialize(input, out obj, out exception);
        }

        public static Nomina Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((Nomina)(Serializer.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static Nomina Deserialize(Stream s)
        {
            return ((Nomina)(Serializer.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current Nomina object into file
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
        /// Deserializes xml markup from file into an Nomina object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="encoding"></param>
        /// <param name="obj">Output Nomina object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, Encoding encoding, out Nomina obj, out Exception exception)
        {
            exception = null;
            obj = default(Nomina);
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

        public static bool LoadFromFile(string fileName, out Nomina obj, out Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out Nomina obj)
        {
            Exception exception;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static Nomina LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static Nomina LoadFromFile(string fileName, Encoding encoding)
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
        /// Create a clone of this Nomina object
        /// </summary>
        public virtual Nomina Clone()
        {
            return ((Nomina)(MemberwiseClone()));
        }
        #endregion
    }


}
